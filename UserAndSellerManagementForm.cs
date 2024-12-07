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
    public partial class UserAndSellerManagementForm : Form
    {
        public UserAndSellerManagementForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSellers();
            LoadCustomers();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //adding customer
        private void button1_Click(object sender, EventArgs e)
        {
            //adding customer
            AddCustomer ac = new AddCustomer();
            this.Hide();
            ac.Show();

        }

        //adding seller
        private void btnSuspendSeller_Click(object sender, EventArgs e)
        {
            //adding seller
            //AddSeller as = new AddSeller();
            //this.Hide();
            //as.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AdminDashboard ad = new AdminDashboard();
            this.Hide();
            ad.Show();
        }

        private void btnSearchUsers_Click(object sender, EventArgs e)
        {
            // Get the CustomerID from the textbox
            string customerId = textBox1.Text.Trim();

            // Define the base query
            string query = @"
        SELECT CustomerID, CustomerName, CustomerEmail, CustomerUsername, CustomerPassword, CustomerPaymentOption
        FROM Customer";

            // Check if the customerId is provided, and modify the query if necessary
            if (!string.IsNullOrEmpty(customerId))
            {
                query += " WHERE CustomerID = @CustomerID";
            }

            using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add the parameter only if customerId is not empty
                    if (!string.IsNullOrEmpty(customerId))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    }

                    // Create a data adapter to fill the DataGridView
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Check if any data was returned
                    if (dt.Rows.Count > 0)
                    {
                        // Bind the data to the DataGridView
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No customer found with the given Customer ID.");
                        dataGridView1.DataSource = null; // Clear DataGridView
                    }
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //enter id here
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sellerId = textBox2.Text.Trim();

            string query = @"
                SELECT SellerID, SellerName, SellerEmail, SellerUsername, SellerPassword, SellerPaymentOption
                FROM Seller";


            if (!string.IsNullOrEmpty(sellerId))
            {
                query += " WHERE SellerID = @SellerID";
            }

            using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SellerID", sellerId);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Rows found: " + dt.Rows.Count); // Debugging log
                        dataGridView2.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No seller found with the given Seller ID."); // Log for empty result
                        dataGridView2.DataSource = null;
                    }
                }
            }
                
                
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadCustomers()
        {
            string query = "SELECT CustomerID, CustomerName, CustomerEmail, CustomerUsername, CustomerPassword, CustomerPaymentOption FROM Customer";

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading customers: {ex.Message}");
            }
        }

        private void LoadSellers()
        {
            string query = "SELECT SellerID, SellerName, SellerEmail, SellerUsername, SellerPassword, SellerPaymentOption FROM Seller";

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);

                        dataGridView2.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading sellers: {ex.Message}");
            }
        }



        //deleting seller
        private void btnApproveSeller_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Get the SellerID of the selected row
                string sellerId = dataGridView2.SelectedRows[0].Cells["SellerID"].Value.ToString();

                // Confirm deletion with the user
                var confirmResult = MessageBox.Show(
                    "Are you sure you want to delete the selected seller?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    // SQL query to delete the seller
                    string query = "DELETE FROM Seller WHERE SellerID = @SellerID";

                    try
                    {
                        using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@SellerID", sellerId);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Seller deleted successfully.");

                                    // Refresh the DataGridView
                                    LoadSellers();
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete the seller. Please try again.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the seller: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a seller to delete.");
            }
        }


        //updating seller
        private void button3_Click(object sender, EventArgs e)
        {
            //updating seller
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Retrieve data from the selected row
                int sellerId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["SellerID"].Value);
                string username = dataGridView2.SelectedRows[0].Cells["SellerUsername"].Value.ToString();
                string fullName = dataGridView2.SelectedRows[0].Cells["SellerName"].Value.ToString();
                string password = dataGridView2.SelectedRows[0].Cells["SellerPassword"].Value.ToString();
                string email = dataGridView2.SelectedRows[0].Cells["SellerEmail"].Value.ToString();
                string paymentOp = dataGridView2.SelectedRows[0].Cells["SellerPaymentOption"].Value.ToString();

                // Create a Customer object
                Seller seller = new Seller
                {
                    SellerID = sellerId,
                    Username = username,
                    FullName = fullName,
                    Password = password,
                    Email = email,
                    PaymentOp = paymentOp
                };

                // Pass the Customer object to the UpdateCustomer form
                UpdateSeller uc = new UpdateSeller(seller);

                this.Hide();
                uc.Show();
            }
            else
            {
                MessageBox.Show("Please select a customer to update.");
            }
        }

        //deleting customer
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the CustomerID of the selected row
                string customerId = dataGridView1.SelectedRows[0].Cells["CustomerID"].Value.ToString();

                // Confirm deletion with the user
                var confirmResult = MessageBox.Show(
                    "Are you sure you want to delete the selected customer?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    // SQL query to delete the customer
                    string query = "DELETE FROM Customer WHERE CustomerID = @CustomerID";

                    try
                    {
                        using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Customer deleted successfully.");

                                    // Refresh the DataGridView
                                    LoadCustomers();
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete the customer. Please try again.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the customer: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }


        //updating customer
        private void button5_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Retrieve data from the selected row
                int customerId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CustomerID"].Value);
                string username = dataGridView1.SelectedRows[0].Cells["CustomerUsername"].Value.ToString();
                string fullName = dataGridView1.SelectedRows[0].Cells["CustomerName"].Value.ToString();
                string password = dataGridView1.SelectedRows[0].Cells["CustomerPassword"].Value.ToString();
                string email = dataGridView1.SelectedRows[0].Cells["CustomerEmail"].Value.ToString();
                string paymentOp = dataGridView1.SelectedRows[0].Cells["CustomerPaymentOption"].Value.ToString();

                // Create a Customer object
                Customer customer = new Customer
                {
                    CustomerID = customerId,
                    Username = username,
                    FullName = fullName,
                    Password = password,
                    Email = email,
                    PaymentOp = paymentOp
                };

                // Pass the Customer object to the UpdateCustomer form
                UpdateCustomer uc = new UpdateCustomer(customer);

                // Hide the current form and show the UpdateCustomer form
                this.Hide();
                uc.Show();
            }
            else
            {
                MessageBox.Show("Please select a customer to update.");
            }
        }

    }
}