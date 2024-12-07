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
    public partial class AddCategory : Form
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        private void AddCategory_Load(object sender, EventArgs e)
        {
        }


        private void button1_Click(object sender, EventArgs e)
        {

            //enter button

            string categoryName = textBox3.Text.Trim();

            // Validate that the category name is not empty
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                MessageBox.Show("Please enter a category name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Define the connection string (change as needed for your environment)
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            // SQL query to insert the new category into the database
            string insertCategoryQuery = "INSERT INTO Category (CategoryName) VALUES (@CategoryName)";

            try
            {
                // Establish the database connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(insertCategoryQuery, conn))
                    {
                        // Add the category name parameter to the query
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                        // Execute the insert command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if the insert was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Optionally, clear the text box after successful insertion
                            textBox3.Clear();
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while adding the category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during the database operation
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
            //categoryname
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //back
            ProductAndCategoryManagementForm pf = new ProductAndCategoryManagementForm();
            this.Hide();
            pf.Show();
        }

    }
}
