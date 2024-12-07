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
    public partial class AddProduct : Form
    {
        private Seller currentSeller;
        public AddProduct(Seller seller)
        {
            this.currentSeller = seller;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SellerListings sl = new SellerListings(currentSeller);
            this.Hide();
            sl.Show();
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve values from the textboxes
            string productName = textBox1.Text;
            decimal unitPrice;
            int quantity;
            string brand = textBox5.Text;
            string shippingOptions = textBox4.Text;
            decimal rating;

            // Validate the input fields
            if (string.IsNullOrEmpty(productName) || !decimal.TryParse(textBox2.Text, out unitPrice) ||
                !int.TryParse(textBox6.Text, out quantity) || string.IsNullOrEmpty(brand) ||
                string.IsNullOrEmpty(shippingOptions) || !decimal.TryParse(textBox7.Text, out rating))
            {
                MessageBox.Show("Please fill in all fields with valid data.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve CategoryID by CategoryName (using join)
            string categoryName = textBox3.Text; // Assuming category name is in textBox3
            int categoryID = 0;

            // Database connection string
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";
            string categoryQuery = @"
        SELECT c.CategoryID 
        FROM Category c
        WHERE c.CategoryName = @CategoryName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Command to retrieve CategoryID
                    using (SqlCommand cmd = new SqlCommand(categoryQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            categoryID = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("Category not found. Please enter a valid category name.", "Category Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching the category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Optionally, handle the Image URL (if image uploading is implemented)
            string imageUrl = "default_image_url"; // Replace with actual logic to get the image URL

            // SQL Insert query
            string query = @"
        INSERT INTO Product (SellerID, CategoryID, ProductName, Image_url, UnitPrice, Quantity, Brand, ShippingOptions, Rating)
        VALUES (@SellerID, @CategoryID, @ProductName, @ImageUrl, @UnitPrice, @Quantity, @Brand, @ShippingOptions, @Rating)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Create a command to execute the SQL query
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@SellerID", currentSeller.SellerID);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                        cmd.Parameters.AddWithValue("@ProductName", productName); // Add product name
                        cmd.Parameters.AddWithValue("@ImageUrl", imageUrl); // Change this if image handling is done differently
                        cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Brand", brand);
                        cmd.Parameters.AddWithValue("@ShippingOptions", shippingOptions);
                        cmd.Parameters.AddWithValue("@Rating", rating);

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }

                    // Notify user of successful insertion
                    MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally, go back to the seller listings page
                    SellerListings sl = new SellerListings(currentSeller);
                    this.Hide();
                    sl.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while adding the product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //product name
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //unit price
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //category name
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //shipping options
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //brand
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //quantity
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //rating
        }
    }
}
