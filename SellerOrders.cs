using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace m2

{
    public partial class SellerOrders : Form
    {
        private Seller currentSeller;
        public SellerOrders(Seller seller)
        {
            this.currentSeller = seller;
            InitializeComponent();;
        }

        private void SellerOrders_Load(object sender, EventArgs e)
        {
            // Connection string
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            // SQL query to get seller's orders
            string query = @"
            SELECT o.OrderID, o.OrderDate, od.Quantity AS OrderQuantity, o.TotalPrice, o.Status,
                   p.ProductName, p.Brand, p.UnitPrice, p.ShippingOptions, p.Rating, 
                   c.CategoryName
            FROM [Order] o
            INNER JOIN OrderDetails od ON o.OrderID = od.OrderID
            INNER JOIN Product p ON od.ProductID = p.ProductID
            INNER JOIN Category c ON p.CategoryID = c.CategoryID
            WHERE p.SellerID = @SellerID";

            // Creating a connection to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Creating a command to execute the SQL query
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Adding the SellerID parameter to the query
                        cmd.Parameters.AddWithValue("@SellerID", currentSeller.SellerID);

                        // Executing the query and reading the results
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if data is available
                            if (reader.HasRows)
                            {
                                // Create a DataTable to store the result of the query
                                DataTable dataTable = new DataTable();

                                // Load the result of the query into the DataTable
                                dataTable.Load(reader);

                                // Bind the DataTable to the DataGridView
                                dataGridView1.DataSource = dataTable;
                            }
                            else
                            {
                                MessageBox.Show("No orders found for the current seller.", "No Orders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ensure an order is selected
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to print the shipping label.", "No Order Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected order's details from the DataGridView
            int orderID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderID"].Value);
            string status = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
            decimal totalPrice = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["TotalPrice"].Value);
            DateTime orderDate = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["OrderDate"].Value);

            if (status != "Shipped")
            {
                MessageBox.Show("Please mark the order as shipped before printing the shipping label.", "Order Not Shipped", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Initialize customer name variable
            string customerName = string.Empty;

            // Connection string
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            // SQL query to retrieve the customer name using a join
            string query = @"
        SELECT c.CustomerName
        FROM [Order] o
        INNER JOIN Customer c ON o.CustomerID = c.CustomerID
        WHERE o.OrderID = @OrderID";

            // Fetch the customer name from the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerName = reader["CustomerName"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Customer details not found for the selected order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching customer details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Prepare the shipping label details for printing
            ShippingLabelDetails shippingDetails = new ShippingLabelDetails
            {
                OrderID = orderID,
                TotalPrice = totalPrice,
                CustomerName = customerName,
                OrderDate = orderDate
            };

            // Initialize PrintDocument
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (s, ev) => PrintShippingLabel(ev, shippingDetails);

            // Show the print dialog
            PrintDialog printDialog = new PrintDialog
            {
                Document = printDocument
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        // Struct to hold shipping label details
        private struct ShippingLabelDetails
        {
            public int OrderID;
            public decimal TotalPrice;
            public string CustomerName;
            public DateTime OrderDate;
        }

        // Method to handle the PrintPage event
        private void PrintShippingLabel(PrintPageEventArgs e, ShippingLabelDetails details)
        {
            Font font = new Font("Arial", 12);
            Brush brush = Brushes.Black;
            int x = 50; // Horizontal position
            int y = 50; // Vertical position
            int lineHeight = 25;

            e.Graphics.DrawString("Shipping Label", new Font("Arial", 16, FontStyle.Bold), brush, x, y);
            y += lineHeight * 2;

            e.Graphics.DrawString($"Order ID: {details.OrderID}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Customer Name: {details.CustomerName}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Order Date: {details.OrderDate.ToShortDateString()}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString($"Total Price: ${details.TotalPrice:F2}", font, brush, x, y);
            y += lineHeight;

            e.Graphics.DrawString("Thank you for your purchase!", font, brush, x, y);
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Ensure an order is selected
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to mark as shipped.", "No Order Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected OrderID from the DataGridView
            int selectedOrderID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderID"].Value);

            // Connection string
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            // SQL query to update the status of the order
            string query = "UPDATE [Order] SET Status = 'Shipped' WHERE OrderID = @OrderID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Create the command
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the OrderID parameter to the query
                        cmd.Parameters.AddWithValue("@OrderID", selectedOrderID);

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if the update was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Order marked as shipped successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to reflect the updated status
                            SellerOrders_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the order status. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating the order status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            //go back
            SellerOptions List = new SellerOptions(currentSeller);
            this.Hide();
            List.Show();
        }

    }
}
