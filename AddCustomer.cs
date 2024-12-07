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
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(textBox3.Text) || // CustomerName
                string.IsNullOrWhiteSpace(textBox5.Text) || // CustomerEmail
                string.IsNullOrWhiteSpace(textBox8.Text) || // CustomerUsername
                string.IsNullOrWhiteSpace(textBox11.Text) || // CustomerPassword
                string.IsNullOrWhiteSpace(textBox12.Text)) // CustomerPaymentOption
            {
                MessageBox.Show("Please fill all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Connection string to the database
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            // SQL query to insert the customer details
            string query = @"INSERT INTO Customer 
                     (CustomerName, CustomerEmail, CustomerUsername, CustomerPassword, CustomerPaymentOption) 
                     VALUES (@CustomerName, @CustomerEmail, @CustomerUsername, @CustomerPassword, @CustomerPaymentOption)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@CustomerName", textBox3.Text);
                        cmd.Parameters.AddWithValue("@CustomerEmail", textBox5.Text);
                        cmd.Parameters.AddWithValue("@CustomerUsername", textBox8.Text);
                        cmd.Parameters.AddWithValue("@CustomerPassword", textBox11.Text);
                        cmd.Parameters.AddWithValue("@CustomerPaymentOption", textBox12.Text);

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer successfully signed up!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UserAndSellerManagementForm usfm = new UserAndSellerManagementForm();
                            this.Hide();
                            usfm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Sign-up failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Display any errors that occur during the database operation
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //name
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //email
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            //username
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            //payment op
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            //password
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserAndSellerManagementForm usfm = new UserAndSellerManagementForm();
            this.Hide();
            usfm.Show();
        }
    }
}
