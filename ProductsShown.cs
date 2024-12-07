using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace m2
{
    public partial class ProductsShown : Form

    {
        private Customer currentCustomer;
        public ProductsShown(DataTable productsTable, Customer customer)
        {
            this.currentCustomer = customer;
            InitializeComponent();

            // Disable AutoGenerateColumns to manually define columns
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Product ID",
                DataPropertyName = "ProductID", // Ensure this matches the column name in the DataTable
                //Visible = false // Hide the column
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Brand",
                DataPropertyName = "Brand",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Price",
                DataPropertyName = "UnitPrice",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Shipping Options",
                DataPropertyName = "ShippingOptions",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Rating",
                DataPropertyName = "Rating",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Set DataSource
            dataGridView1.DataSource = productsTable;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;

            // Optional: Debugging information
            Console.WriteLine($"Rows in DataTable: {productsTable.Rows.Count}, Columns: {productsTable.Columns.Count}");
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the current cell is the image column
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "Image_url")
            {
                string imagePath = e.Value.ToString();
                try
                {
                    // Convert the image path to an Image object
                    e.Value = Image.FromFile(imagePath);
                }
                catch (Exception ex)
                {
                    // Handle invalid paths (e.g., if the image is not found)
                    MessageBox.Show($"Error loading image: {ex.Message}");
                    e.Value = null;  // Optionally set a default image here
                }
            }
        }



        private void ProductsShown_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            CustomerOptionChoice customerOptionChoice= new CustomerOptionChoice(currentCustomer);
            this.Hide();
            customerOptionChoice.Show();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            //add to cart, no form should open at this click only messagebox showing whether added or not
            AddToDatabase("Cart");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //add to wishlist, no form should open at this click only messagebox showing whether added or not
            AddToDatabase("WishList");
        }

        private void AddToDatabase(string tableName)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to add.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int productId = Convert.ToInt32(selectedRow.Cells[0].Value); // Ensure ProductID column exists
            decimal unitPrice = Convert.ToDecimal(selectedRow.Cells[3].Value);
            string brand = selectedRow.Cells[2].Value.ToString();
            string shippingOptions = selectedRow.Cells[4].Value.ToString();

            // Retrieve CategoryID and SellerID from the database
            int categoryId = 0;
            int sellerId = 0;

            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Query to retrieve CategoryID and SellerID based on ProductID
                string categoryAndSellerQuery = @"
        SELECT CategoryID, SellerID
        FROM Product
        WHERE ProductID = @ProductID";

                using (SqlCommand cmd = new SqlCommand(categoryAndSellerQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                categoryId = Convert.ToInt32(reader["CategoryID"]);
                                sellerId = Convert.ToInt32(reader["SellerID"]);
                            }
                            else
                            {
                                MessageBox.Show("Product does not have category or seller information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Now proceed with adding or updating the product in the Cart or WishList
                        string query = "";
                        if (tableName == "Cart")
                        {
                            // Check if the product already exists in the Cart, if so update the quantity
                            query = @"
                    IF EXISTS (SELECT 1 FROM Cart WHERE ProductID = @ProductID AND CustomerID = @CustomerID)
                    BEGIN
                        UPDATE Cart
                        SET Quantity = Quantity + 1
                        WHERE ProductID = @ProductID AND CustomerID = @CustomerID
                    END
                    ELSE
                    BEGIN
                        INSERT INTO Cart (ProductID, CustomerID, Quantity)
                        VALUES (@ProductID, @CustomerID, @Quantity)
                    END";
                        }
                        else if (tableName == "WishList")
                        {
                            // Check if the product already exists in the WishList, if so update the quantity
                            query = @"
                    IF EXISTS (SELECT 1 FROM WishList WHERE ProductID = @ProductID AND CustomerID = @CustomerID)
                    BEGIN
                        UPDATE WishList
                        SET Quantity = Quantity + 1
                        WHERE ProductID = @ProductID AND CustomerID = @CustomerID
                    END
                    ELSE
                    BEGIN
                        INSERT INTO WishList (ProductID, CustomerID, UnitPrice, Brand, ShippingOptions, Quantity, CategoryID, SellerID)
                        VALUES (@ProductID, @CustomerID, @UnitPrice, @Brand, @ShippingOptions, @Quantity, @CategoryID, @SellerID)
                    END";
                        }

                        using (SqlCommand cmdInsert = new SqlCommand(query, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@ProductID", productId);
                            cmdInsert.Parameters.AddWithValue("@CustomerID", currentCustomer.CustomerID);

                            if (tableName == "Cart")
                            {
                                cmdInsert.Parameters.AddWithValue("@Quantity", 1); // Default quantity for Cart
                            }
                            else if (tableName == "WishList")
                            {
                                cmdInsert.Parameters.AddWithValue("@UnitPrice", unitPrice);
                                cmdInsert.Parameters.AddWithValue("@Brand", brand);
                                cmdInsert.Parameters.AddWithValue("@ShippingOptions", shippingOptions);
                                cmdInsert.Parameters.AddWithValue("@Quantity", 1); // Default quantity for WishList
                                cmdInsert.Parameters.AddWithValue("@CategoryID", categoryId); // Insert CategoryID
                                cmdInsert.Parameters.AddWithValue("@SellerID", sellerId); // Insert SellerID
                            }

                            cmdInsert.ExecuteNonQuery();
                        }

                        MessageBox.Show($"{tableName} updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
          ViewCart vc = new ViewCart(currentCustomer);
          this.Hide();
          vc.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}