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
    public partial class FeedbackModerationForm : Form
    {
        public FeedbackModerationForm()
        {
            InitializeComponent();
        }

        private void FeedbackModerationForm_Load(object sender, EventArgs e)
        {
            LoadReviews();
        }

        private void LoadReviews(string reviewId = "", string customerId = "", string productId = "")
        {
            string query = @"
            SELECT 
                r.ReviewID, 
                r.Review_txt, 
                r.Rating, 
                r.ProductID, 
                p.ProductName, 
                r.CustomerID, 
                c.CustomerName
            FROM 
                Review r
            INNER JOIN 
                Product p ON r.ProductID = p.ProductID
            INNER JOIN 
                Customer c ON r.CustomerID = c.CustomerID
            WHERE 
                (@ReviewID = '' OR r.ReviewID = @ReviewID)
                AND (@CustomerID = '' OR r.CustomerID = @CustomerID)
                AND (@ProductID = '' OR r.ProductID = @ProductID)";

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to the query
                        cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@ProductID", productId);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);

                        // Bind the data to a DataGridView (assuming dgvReviews is the name of the DataGridView)
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading reviews: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //go back
            AdminDashboard ad = new AdminDashboard();
            this.Hide();
            ad.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //delete review
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int reviewId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ReviewID"].Value);
                DeleteReview(reviewId);
            }
            else
            {
                MessageBox.Show("Please select a review to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteReview(int reviewId)
        {
            string query = "DELETE FROM Review WHERE ReviewID = @ReviewID";

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReviewID", reviewId);

                        // Execute the delete query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Review deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Reload reviews after deletion
                            LoadReviews();
                        }
                        else
                        {
                            MessageBox.Show("Review not found or couldn't be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the review: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Search button click event
        private void button3_Click(object sender, EventArgs e)
        {
            string reviewId = textBox3.Text.Trim();
            string customerId = textBox1.Text.Trim();
            string productId = textBox2.Text.Trim();

            // Call the LoadReviews function with search parameters
            LoadReviews(reviewId, customerId, productId);
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //customer id 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //product id
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //review id
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
