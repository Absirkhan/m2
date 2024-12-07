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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace m2
{
    public partial class Shipment : Form
    {
        private string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

        public Shipment()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LogisticsMenu lm = new LogisticsMenu();
            this.Hide();
            lm.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //pickup
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //delivery
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // confirm pickup
            int orderID = GetSelectedOrderID(); // Get the selected order ID from the DataGridView
            int agentID = GetSelectedAgentID(); // Get the selected Agent ID
            DateTime pickupDate = dateTimePicker1.Value;

            if (orderID > 0 && agentID > 0)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Check if the shipment has already been delivered
                        string checkStatusQuery = "SELECT DeliveryStatus FROM Shipment WHERE OrderID = @OrderID";
                        string currentStatus = string.Empty;

                        using (SqlCommand cmd = new SqlCommand(checkStatusQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderID);
                            currentStatus = cmd.ExecuteScalar()?.ToString();
                        }

                        // If the status is already 'Delivered', do not insert the 'Picked Up' status
                        if (currentStatus != "Delivered")
                        {
                            string insertShipmentQuery = @"
                        INSERT INTO Shipment (OrderID, AgentID, ShipmentDate, DeliveryStatus, EstimatedDeliveryDate)
                        VALUES (@OrderID, @AgentID, @ShipmentDate, @DeliveryStatus, @EstimatedDeliveryDate)";

                            using (SqlCommand cmd = new SqlCommand(insertShipmentQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@OrderID", orderID);
                                cmd.Parameters.AddWithValue("@AgentID", agentID);
                                cmd.Parameters.AddWithValue("@ShipmentDate", pickupDate);
                                cmd.Parameters.AddWithValue("@DeliveryStatus", "Picked Up");
                                cmd.Parameters.AddWithValue("@EstimatedDeliveryDate", DBNull.Value); // Set this value based on your logic
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Pickup confirmed successfully.");
                            LoadShipmentData(); // Refresh the DataGridView to show updated data
                        }
                        else
                        {
                            MessageBox.Show("This order has already been delivered and cannot be marked as picked up.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error confirming pickup: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid order and agent to confirm pickup.");
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // confirm delivery
            int orderID = GetSelectedOrderID(); // Get the selected order ID from the DataGridView
            int agentID = GetSelectedAgentID(); // Get the selected Agent ID
            DateTime deliveryDate = dateTimePicker2.Value;

            if (orderID > 0 && agentID > 0)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Check the current DeliveryStatus of the shipment before updating it to 'Delivered'
                        string checkStatusQuery = "SELECT DeliveryStatus FROM Shipment WHERE OrderID = @OrderID";
                        string currentStatus = string.Empty;

                        using (SqlCommand cmd = new SqlCommand(checkStatusQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderID);
                            currentStatus = cmd.ExecuteScalar()?.ToString();
                        }

                        // Only proceed if the status is not 'Picked Up' (or 'Delivered' already)
                        if (currentStatus != "Picked Up" && currentStatus != "Delivered")
                        {
                            // Update the DeliveryStatus to 'Delivered'
                            string updateShipmentQuery = @"
                        UPDATE Shipment
                        SET DeliveryStatus = 'Delivered', EstimatedDeliveryDate = @DeliveryDate
                        WHERE OrderID = @OrderID";

                            using (SqlCommand cmd = new SqlCommand(updateShipmentQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@DeliveryDate", deliveryDate);
                                cmd.Parameters.AddWithValue("@OrderID", orderID);
                                cmd.ExecuteNonQuery();
                            }

                            // Insert tracking information
                            string insertTrackingQuery = @"
                        INSERT INTO Tracking (ShipmentID, AgentID, Location, Status, Timestamp)
                        SELECT s.ShipmentID, @AgentID, @Location, 'Delivered', @Timestamp
                        FROM Shipment s
                        JOIN [Order] o ON s.OrderID = o.OrderID
                        WHERE o.OrderID = @OrderID";

                            using (SqlCommand cmd = new SqlCommand(insertTrackingQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@OrderID", orderID);
                                cmd.Parameters.AddWithValue("@AgentID", agentID);
                                cmd.Parameters.AddWithValue("@Location", "Destination"); // Update this with actual location
                                cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }

                            // Insert into DeliveryLog
                            string insertDeliveryLogQuery = @"
                        INSERT INTO DeliveryLog (AgentID, OrderID, DeliveryAttemptDate, DeliveryStatus, Comments)
                        VALUES (@AgentID, @OrderID, @DeliveryDate, 'Delivered', @Comments)";

                            using (SqlCommand cmd = new SqlCommand(insertDeliveryLogQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@AgentID", agentID);
                                cmd.Parameters.AddWithValue("@OrderID", orderID);
                                cmd.Parameters.AddWithValue("@DeliveryDate", deliveryDate);
                                cmd.Parameters.AddWithValue("@Comments", "Successfully delivered");
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Delivery confirmed successfully.");
                            LoadShipmentData(); // Refresh the DataGridView to show updated data
                        }
                        else
                        {
                            MessageBox.Show("This order cannot be marked as delivered because it has already been picked up or delivered.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error confirming delivery: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid order and agent to confirm delivery.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // mark delayed button, this makes the order status changed to Delayed.
            int orderID = GetSelectedOrderID(); // Get the selected order ID from the DataGridView

            if (orderID > 0)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // First, check the current delivery status
                        string checkStatusQuery = "SELECT DeliveryStatus FROM Shipment WHERE OrderID = @OrderID";
                        string currentStatus = string.Empty;

                        using (SqlCommand cmd = new SqlCommand(checkStatusQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderID);
                            currentStatus = cmd.ExecuteScalar()?.ToString();
                        }

                        // Check if the current status is not 'Picked Up' or 'Delivered'
                        if (currentStatus != "Picked Up" && currentStatus != "Delivered")
                        {
                            // If not, update the status to 'Delayed'
                            string updateQuery = @"
                        UPDATE Shipment
                        SET DeliveryStatus = 'Delayed'
                        WHERE OrderID = @OrderID";

                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@OrderID", orderID);
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Order marked as delayed.");
                            LoadShipmentData(); // Refresh the DataGridView to show updated data
                        }
                        else
                        {
                            // If the status is 'Picked Up' or 'Delivered', show a message
                            MessageBox.Show("The order cannot be marked as delayed because it has already been picked up or delivered.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error marking order as delayed: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid order to mark as delayed.");
            }
        }

        private int GetSelectedOrderID()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                return Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderID"].Value);
            }
            return -1; // Return -1 if no order is selected
        }

        // Get the selected AgentID from a ComboBox or other UI element
        private int GetSelectedAgentID()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                return Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["AgentID"].Value);
            }
            return -1;
        }

        private void Shipment_Load(object sender, EventArgs e)
        {
            // Call method to load data into the DataGridView
            LoadShipmentData();
        }

        private void LoadShipmentData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT s.ShipmentID, o.OrderID, a.AgentID, s.ShipmentDate, s.DeliveryStatus, s.EstimatedDeliveryDate
                FROM Shipment s
                JOIN [Order] o ON s.OrderID = o.OrderID
                JOIN Agent a ON s.AgentID = a.AgentID";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Set the DataSource of the DataGridView to the loaded DataTable
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading shipment data: {ex.Message}");
            }
        }


    }
}
