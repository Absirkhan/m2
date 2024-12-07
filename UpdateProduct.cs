using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace m2
{
    public partial class UpdateProduct : Form
    {
        private Seller currentSeller;
        private int productID;

        // Constructor to accept the Seller and ProductID
        public UpdateProduct(Seller seller, int productID)
        {
            this.currentSeller = seller;
            this.productID = productID;
            InitializeComponent();
        }

        // Form Load event to fetch the current product details
        private void UpdateProduct_Load(object sender, EventArgs e)
        {
            // Connection string
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            // SQL query to fetch product details based on ProductID
            string query = @"
                SELECT p.UnitPrice, p.ShippingOptions, p.Quantity, p.Rating
                FROM Product p
                WHERE p.ProductID = @ProductID";

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
                        // Adding the ProductID parameter to the query
                        cmd.Parameters.AddWithValue("@ProductID", productID);

                        // Executing the query and reading the result
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if data is available
                            if (reader.Read())
                            {
                                // Load the current product details into the textboxes
                                textBox1.Text = reader["UnitPrice"].ToString();    // UnitPrice
                                textBox2.Text = reader["ShippingOptions"].ToString(); // ShippingOptions
                                textBox3.Text = reader["Quantity"].ToString();     // Quantity
                                textBox4.Text = reader["Rating"].ToString();       // Rating
                            }
                            else
                            {
                                MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the product details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Button click event to update the product details
        private void button1_Click(object sender, EventArgs e)
        {
            // Check if any of the textboxes are empty
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without updating
            }
            // Get the updated values from the textboxes
            decimal unitPrice = Convert.ToDecimal(textBox1.Text);
            string shippingOptions = textBox2.Text;
            int quantity = Convert.ToInt32(textBox3.Text);
            decimal rating = Convert.ToDecimal(textBox4.Text);

            // Connection string
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            // SQL query to update the product details
            string query = @"
                UPDATE Product
                SET UnitPrice = @UnitPrice, ShippingOptions = @ShippingOptions, Quantity = @Quantity, Rating = @Rating
                WHERE ProductID = @ProductID";

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
                        // Adding the parameters for the update query
                        cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        cmd.Parameters.AddWithValue("@ShippingOptions", shippingOptions);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Rating", rating);
                        cmd.Parameters.AddWithValue("@ProductID", productID);

                        // Executing the update command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if the update was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update product details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating the product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Navigate back to the SellerListings form
            SellerListings sl = new SellerListings(currentSeller);
            this.Hide();
            sl.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //back button
            SellerListings sl = new SellerListings(currentSeller);
            this.Hide();
            sl.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //unit price
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //shipping op
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //quantity
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //rating
        }

    }
}
