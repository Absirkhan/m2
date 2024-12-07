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
    public partial class EditCategory : Form
    {
        public EditCategory()
        {
            InitializeComponent();
        }

        private void Add_Edit_Category_Load(object sender, EventArgs e)
        {
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //enter button
            // Save button clicked: validate inputs, save to database, and make text boxes read-only
            if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please fill all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int categoryId;
            // Validate CategoryID input (ensure it's a valid integer)
            if (!int.TryParse(textBox3.Text, out categoryId))
            {
                MessageBox.Show("Please enter a valid Category ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string categoryName = textBox1.Text.Trim();

            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            // Check if the category exists in the database
            string checkCategoryQuery = "SELECT COUNT(*) FROM Category WHERE CategoryID = @CategoryID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(checkCategoryQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                        int categoryCount = (int)cmd.ExecuteScalar();

                        if (categoryCount == 0)
                        {
                            // If category doesn't exist, show a message
                            MessageBox.Show("Category ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // Category exists, proceed with the update
                            string updateCategoryQuery = "UPDATE Category SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";

                            using (SqlCommand updateCmd = new SqlCommand(updateCategoryQuery, conn))
                            {
                                // Set parameters to avoid SQL injection
                                updateCmd.Parameters.AddWithValue("@CategoryName", categoryName);
                                updateCmd.Parameters.AddWithValue("@CategoryID", categoryId);

                                // Execute the update
                                int rowsAffected = updateCmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // Success: Show a success message
                                    MessageBox.Show("Category updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Optionally, clear the textboxes after update
                                    textBox3.Clear();
                                    textBox1.Clear();
                                }
                                else
                                {
                                    // If no rows were affected
                                    MessageBox.Show("Category could not be updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }



        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //categoryid
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //back
            ProductAndCategoryManagementForm pf = new ProductAndCategoryManagementForm();
            this.Hide();
            pf.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //category name
        }
    }
}
