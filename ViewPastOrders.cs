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
    public partial class ViewPastOrders : Form
    {
        private Customer currentCustomer;
        public ViewPastOrders(Customer customer)
        {
            this.currentCustomer = customer;
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerOptionChoice coc = new CustomerOptionChoice(currentCustomer);
            this.Hide();
            coc.Show();
        }

        private void ViewPastOrders_Load(object sender, EventArgs e)
        {
            // Connection string
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            // SQL query to retrieve past orders from OrderHistory
            string query = @"
        SELECT 
            OH.OrderID,
            P.ProductName,
            OH.Bill,
            OH.OrderDate
        FROM OrderHistory OH
        INNER JOIN Product P ON OH.ProductID = P.ProductID
        WHERE OH.CustomerID = @CustomerID";

            try
            {
                // Create a new connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter for CustomerID
                        cmd.Parameters.AddWithValue("@CustomerID", currentCustomer.CustomerID);

                        // Execute the query and load the results into a DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable orderHistoryTable = new DataTable();
                            adapter.Fill(orderHistoryTable);

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = orderHistoryTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading past orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
