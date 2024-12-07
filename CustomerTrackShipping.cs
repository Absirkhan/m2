using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace m2
{
    public partial class CustomerTrackShipping : Form
    {
        private Customer currentCustomer;
        private string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

        public CustomerTrackShipping(Customer customer)
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

        private void CustomerTrackShipping_Load(object sender, EventArgs e)
        {
            LoadShippingData();
        }

        private void LoadShippingData()
        {
            string query = @"
                SELECT o.OrderID, s.ShipmentID, t.Status, t.Timestamp
                FROM [Order] o
                JOIN Shipment s ON o.OrderID = s.OrderID
                JOIN Tracking t ON s.ShipmentID = t.ShipmentID
                WHERE o.CustomerID = @CustomerID
                ORDER BY t.Timestamp DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", currentCustomer.CustomerID);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable trackingData = new DataTable();
                        dataAdapter.Fill(trackingData);

                        dataGridView1.DataSource = trackingData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading shipping data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
