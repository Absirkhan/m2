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
    public partial class OrderOversightForm : Form
    {
        public OrderOversightForm()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            string query = @"
            SELECT 
                oh.OrderID,
                p.ProductName,
                c.CustomerName,
                s.SellerName,
                oh.OrderDate,
                o.Status,            -- Fetching Status from the Order table
                oh.Bill, 
                o.PaymentMethod      -- Assuming PaymentMethod is in Customer table
            FROM 
                OrderHistory oh
            JOIN 
                [Order] o ON oh.OrderID = o.OrderID  -- Joining Order table to get Status
            JOIN 
                Customer c ON oh.CustomerID = c.CustomerID
            JOIN 
                Product p ON oh.ProductID = p.ProductID
            JOIN 
                Seller s ON p.SellerID = s.SellerID";


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

                        // Set the DataSource for the DataGridView
                        dataGridView1.DataSource = dt;

                        // Set column headers
                        dataGridView1.Columns["OrderID"].HeaderText = "Order ID";
                        dataGridView1.Columns["ProductName"].HeaderText = "Product Name";
                        dataGridView1.Columns["CustomerName"].HeaderText = "Customer Name";
                        dataGridView1.Columns["SellerName"].HeaderText = "Seller Name";
                        dataGridView1.Columns["OrderDate"].HeaderText = "Order Date";
                        dataGridView1.Columns["Status"].HeaderText = "Status";
                        dataGridView1.Columns["Bill"].HeaderText = "Bill";
                        dataGridView1.Columns["PaymentMethod"].HeaderText = "Payment Method";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading orders: {ex.Message}");
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string selectedStatus = cmbOrderStatus.SelectedItem.ToString();
            // Add logic to filter the DataGridView based on the selected status
            //MessageBox.Show($"Filtering orders by status: {selectedStatus}");
        }

        private void OrderOversightForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSearchOrders_Click(object sender, EventArgs e)
        {
            // Retrieve the search query from the textbox
            string searchQuery = txtSearchOrders.Text.Trim();

            if (int.TryParse(searchQuery, out int orderId)) // Check if input is a valid integer
            {
                string query = @"
            SELECT 
                oh.OrderID,
                p.ProductName,
                c.CustomerName,
                s.SellerName,
                oh.OrderDate,
                o.Status,
                oh.Bill, 
                o.PaymentMethod      -- Assuming PaymentMethod is in Customer table
            FROM 
                OrderHistory oh
            JOIN 
                [Order] o ON oh.OrderID = o.OrderID
            JOIN 
                Customer c ON oh.CustomerID = c.CustomerID
            JOIN 
                Product p ON oh.ProductID = p.ProductID
            JOIN 
                Seller s ON p.SellerID = s.SellerID
            WHERE 
                oh.OrderID = @OrderID"; 

                try
                {
                    using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderId); // Add parameter value
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            dataAdapter.Fill(dt);

                            // Bind the filtered data to the DataGridView
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while searching for the order: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric Order ID.");
            }
        }


        private void txtSearchOrders_TextChanged(object sender, EventArgs e)
        {
            //searching textbox
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //this datagrid should display all the orders.
            //columns should be OrderID, ProductName (ProductID exists in order table so use joins), CustomerName(CustomerID exists in order table so use joins), also retrieve SellerName by using joing as SellerID exisst in the Product Table, OrderDate, Status, TotalPrice, PaymentMethod
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDashboard ad = new AdminDashboard();
            this.Hide();
            ad.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textbox for entering customer name
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Retrieve the CustomerName from textBox1
            string customerName = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(customerName))
            {
                string query = @"
            SELECT 
                oh.OrderID,
                p.ProductName,
                c.CustomerName,
                s.SellerName,
                oh.OrderDate,
                o.Status,
                oh.Bill,
                o.PaymentMethod
            FROM 
                OrderHistory oh
            JOIN 
                [Order] o ON oh.OrderID = o.OrderID
            JOIN 
                Customer c ON oh.CustomerID = c.CustomerID
            JOIN 
                Product p ON oh.ProductID = p.ProductID
            JOIN 
                Seller s ON p.SellerID = s.SellerID
            WHERE 
                c.CustomerName LIKE @CustomerName";

                try
                {
                    using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CustomerName", "%" + customerName + "%"); // Search for partial matches
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            dataAdapter.Fill(dt);

                            // Bind the filtered data to the DataGridView
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while searching for the customer: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter a customer name to search.");
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //textbox for entering seller name
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Retrieve the SellerName from textBox2
            string sellerName = textBox2.Text.Trim();

            if (!string.IsNullOrEmpty(sellerName))
            {
                string query = @"
            SELECT 
                oh.OrderID,
                p.ProductName,
                c.CustomerName,
                s.SellerName,
                oh.OrderDate,
                o.Status,
                oh.Bill,
                o.PaymentMethod
            FROM 
                OrderHistory oh
            JOIN 
                [Order] o ON oh.OrderID = o.OrderID
            JOIN 
                Customer c ON oh.CustomerID = c.CustomerID
            JOIN 
                Product p ON oh.ProductID = p.ProductID
            JOIN 
                Seller s ON p.SellerID = s.SellerID
            WHERE 
                s.SellerName LIKE @SellerName";

                try
                {
                    using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@SellerName", "%" + sellerName + "%"); // Search for partial matches
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            dataAdapter.Fill(dt);

                            // Bind the filtered data to the DataGridView
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while searching for the seller: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter a seller name to search.");
            }
        }

    }
}
