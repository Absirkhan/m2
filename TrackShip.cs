using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace m2
{
    public partial class TrackShip : Form
    {
        // Connection string to connect to your database
        private string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";


        public TrackShip()
        {
            InitializeComponent();
        }

        // This will handle button click for fetching tracking info
        private void button3_Click(object sender, EventArgs e)
        {
            // Get the tracking ID entered by the user
            string trackingID = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(trackingID))
            {
                // Fetch the tracking data and display it in the DataGridView
                LoadTrackingData(trackingID);
            }
            else
            {
                MessageBox.Show("Please enter a valid tracking ID.");
            }
        }

        // Method to load tracking data into DataGridView
        private void LoadTrackingData(string trackingID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL query to fetch tracking data based on tracking ID
                    string query = @"
                        SELECT t.ShipmentID, t.AgentID, t.Location, t.Status, t.Timestamp
                        FROM Tracking t
                        WHERE t.TrackingID = @TrackingID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TrackingID", trackingID);

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView to display the results
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving tracking data: {ex.Message}");
            }
        }

        // This method can be used to initialize the form and load any initial data if necessary
        private void TrackShip_Load(object sender, EventArgs e)
        {
            // You can add logic here if you need to load data or initialize controls when the form loads
        }

        // Button to go back to the LogisticsMenu
        private void button2_Click(object sender, EventArgs e)
        {
            LogisticsMenu obj2 = new LogisticsMenu();
            this.Hide();
            obj2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }



        // Placeholder for any label click event logic
        private void label1_Click(object sender, EventArgs e)
        {
            // Add code here if you want to handle label clicks
        }

        // DataGridView cell content click event (if needed)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle the event when a cell is clicked in the DataGridView (optional)
        }

        // TextBox changed event (optional for handling text changes)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Code can be added here to handle text changes if needed
        }
    }
}
