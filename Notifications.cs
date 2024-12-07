using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System;

namespace m2
{
    public partial class Notifications : Form
    {
        private string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";  // Replace with your actual connection string
        private Timer statusUpdateTimer;

        public Notifications()
        {
            InitializeComponent();
            InitializeTimer();  // Initialize the timer to check for status updates periodically
        }

        // Initialize a timer to check for delivery status updates every 30 seconds
        private void InitializeTimer()
        {
            statusUpdateTimer = new Timer();
            statusUpdateTimer.Interval = 30000;  // 30 seconds
            statusUpdateTimer.Tick += StatusUpdateTimer_Tick;
            statusUpdateTimer.Start();
        }

        // This method is called every time the timer ticks (every 30 seconds in this case)
        private void StatusUpdateTimer_Tick(object sender, EventArgs e)
        {
            LoadDeliveryStatusUpdates();  // Check for delivery status updates in the database
        }

        // Method to load delivery status updates from the database
        private void LoadDeliveryStatusUpdates()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT o.OrderID, s.ShipmentID, t.Status, t.Timestamp
                        FROM [Order] o
                        JOIN Shipment s ON o.OrderID = s.OrderID
                        JOIN Tracking t ON s.ShipmentID = t.ShipmentID
                        WHERE t.Timestamp > @LastCheckedTime
                        ORDER BY t.Timestamp DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Parameter to check for updates since the last time the status was checked
                        cmd.Parameters.AddWithValue("@LastCheckedTime", DateTime.Now.AddMinutes(-1));  // Check for updates in the last minute

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // If there are new status updates, bind them to the DataGridView
                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dataTable;
                            dataGridView1.Refresh(); // Refresh the DataGridView to ensure it reflects the latest data
                            // Optionally, show a system tray notification
                            NotifyUser("New delivery status update received.");
                        }
                        else
                        {
                            // Optionally handle no updates found
                            dataGridView1.DataSource = null;
                            MessageBox.Show("No new delivery updates found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading delivery status updates: {ex.Message}");
            }
        }

        // Method to notify the user (can use SystemTray or MessageBox)
        private void NotifyUser(string message)
        {
            // Show a message box as an example. You could also use a system tray notification here.
            MessageBox.Show(message);
        }

        // Button to navigate to LogisticsMenu (as per your existing code)
        private void button1_Click(object sender, EventArgs e)
        {
            LogisticsMenu obj = new LogisticsMenu();
            this.Hide();
            obj.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // You can add logic here to handle the click event on the DataGridView cells if needed
        }

        // Notifications form load event (you can load initial data if needed)
        private void Notifications_Load(object sender, EventArgs e)
        {
            // Optionally, load the initial tracking data on form load
            LoadDeliveryStatusUpdates();
        }
    }
}