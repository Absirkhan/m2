using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace m2
{
    public partial class SellerListings : Form
    {
        private Seller currentSeller;
        public SellerListings(Seller seller)
        {
            this.currentSeller = seller;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellerOptions opts = new SellerOptions(currentSeller);
            this.Hide();
            opts.Show();
        }

        private void SellerListings_Load(object sender, EventArgs e)
        {
            // Connection string
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            // SQL query to get product details for the current seller
            string query = @"
            SELECT p.ProductID, p.ProductName, p.Brand, p.UnitPrice, p.ShippingOptions, p.Rating, p.Quantity, c.CategoryName
            FROM Product p
            INNER JOIN Category c ON p.CategoryID = c.CategoryID
            WHERE p.SellerID = @SellerID";

            // Creating a connection to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Creating a command to execute the SQL query
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Adding the SellerID parameter to the query
                        cmd.Parameters.AddWithValue("@SellerID", currentSeller.SellerID);

                        // Executing the query and reading the results
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if data is available
                            if (reader.HasRows)
                            {
                                // Create a DataTable to store the result of the query
                                DataTable dataTable = new DataTable();

                                // Load the result of the query into the DataTable
                                dataTable.Load(reader);

                                // Bind the DataTable to the DataGridView
                                dataGridView1.DataSource = dataTable;
                            }
                            else
                            {
                                MessageBox.Show("No products found for the current seller.", "No Products", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //add product
            AddProduct addProduct = new AddProduct(currentSeller);
            this.Hide();
            addProduct.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

            // Retrieve the ProductID from the selected row
            int selectedProductID = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value);

            // Create an instance of UpdateProduct form and pass the selected ProductID
            UpdateProduct up = new UpdateProduct(currentSeller, selectedProductID);

            // Hide the current form and show the UpdateProduct form
            this.Hide();
            up.Show();
        }
    }
}
