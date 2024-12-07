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
    public partial class ViewProfile : Form
    {
        private Customer currentCustomer;
        public ViewProfile(Customer currentCustomer)
        {
            InitializeComponent();
            this.currentCustomer = currentCustomer;
        }

        private void viewProfile_Load(object sender, EventArgs e)
        {
            // Load current customer data into text boxes
            textBox3.Text = currentCustomer.FullName;
            textBox5.Text = currentCustomer.Email;
            textBox8.Text = currentCustomer.Username;
            textBox11.Text = currentCustomer.Password;
            textBox12.Text = currentCustomer.PaymentOp;

            // Make text boxes read-only initially
            SetTextBoxReadOnly(true);
        }

        private void SetTextBoxReadOnly(bool isReadOnly)
        {
            textBox3.ReadOnly = isReadOnly;
            textBox5.ReadOnly = isReadOnly;
            textBox8.ReadOnly = isReadOnly;
            textBox11.ReadOnly = isReadOnly;
            textBox12.ReadOnly = isReadOnly;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Edit button clicked: make text boxes editable
            SetTextBoxReadOnly(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Save button clicked: validate inputs, save to database, and make text boxes read-only
            if (string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text) ||
                string.IsNullOrWhiteSpace(textBox12.Text))
            {
                MessageBox.Show("Please fill all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update customer attributes
            currentCustomer.FullName = textBox3.Text;
            currentCustomer.Email = textBox5.Text;
            currentCustomer.Username = textBox8.Text;
            currentCustomer.Password = textBox11.Text;
            currentCustomer.PaymentOp = textBox12.Text;

            // Save to database
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";
            string updateQuery = @"
                UPDATE Customer
                SET CustomerName = @Name,
                    CustomerEmail = @Email,
                    CustomerUsername = @Username,
                    CustomerPassword = @Password,
                    CustomerPaymentOption = @PaymentOption
                WHERE CustomerID = @CustomerID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", currentCustomer.FullName);
                        cmd.Parameters.AddWithValue("@Email", currentCustomer.Email);
                        cmd.Parameters.AddWithValue("@Username", currentCustomer.Username);
                        cmd.Parameters.AddWithValue("@Password", currentCustomer.Password);
                        cmd.Parameters.AddWithValue("@PaymentOption", currentCustomer.PaymentOp);
                        cmd.Parameters.AddWithValue("@CustomerID", currentCustomer.CustomerID);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Make text boxes read-only after saving
                SetTextBoxReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

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


        private void button3_Click(object sender, EventArgs e)
        {
            //back

            CustomerOptionChoice coc = new CustomerOptionChoice(currentCustomer);
            this.Hide();
            coc.Show();
        }
    }
}
