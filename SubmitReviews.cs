using System.Data.SqlClient;
using System.Windows.Forms;
using System;

namespace m2
{
    public partial class SubmitReviews : Form
    {
        private Customer currentCustomer;

        public SubmitReviews(Customer currentCustomer)
        {
            InitializeComponent();
            this.currentCustomer = currentCustomer;
        }

        private void SubmitReviews_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = 4; // Set maximum rating
        }

        private bool HasOrderedProduct(int productId, int customerId)
        {
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";
            string query = @"
        SELECT COUNT(*) 
        FROM OrderHistory 
        WHERE CustomerID = @CustomerID AND ProductID = @ProductID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@ProductID", productId);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0; // Returns true if the product exists in order history
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while checking order history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Check if the review text is entered
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a review.");
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text)) // Check if product name is entered
            {
                MessageBox.Show("Please enter a product name.");
            }
            else
            {
                // Get the ProductID from the ProductName entered
                int productId = GetProductIdByName(textBox2.Text);

                if (HasOrderedProduct(productId, currentCustomer.CustomerID))
                {
                    // Submit the review to the database
                    SubmitReviewToDatabase(productId, currentCustomer.CustomerID, (int)numericUpDown1.Value, textBox1.Text);

                    MessageBox.Show("Review Submitted!");

                    // Redirect back to the customer options
                    CustomerOptionChoice coc = new CustomerOptionChoice(currentCustomer);
                    this.Hide();
                    coc.Show();
                }
                else
                {
                    MessageBox.Show("You cannot review a product you have not purchased.", "Review Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private int GetProductIdByName(string productName)
        {
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";
            string query = "SELECT ProductID FROM Product WHERE ProductName = @ProductName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        object result = cmd.ExecuteScalar();

                        // Return the ProductID if found, otherwise 0 (not found)
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
        }

        private void SubmitReviewToDatabase(int productId, int customerId, int rating, string reviewText)
        {
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";
            string query = "INSERT INTO Review (ProductID, CustomerID, Rating, Review_txt) VALUES (@ProductID, @CustomerID, @Rating, @Review_txt)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@Rating", rating);
                        cmd.Parameters.AddWithValue("@Review_txt", reviewText);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while submitting the review: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomerOptionChoice coc = new CustomerOptionChoice(currentCustomer);
            this.Hide();
            coc.Show();
        }

        // Other event handlers
        private void textBox1_TextChanged(object sender, EventArgs e) { /* Review text here */ }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { /* Rating handling here */ }
        private void textBox2_TextChanged(object sender, EventArgs e) { /* Product name handling here */ }
    }
}