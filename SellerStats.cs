using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Data.SqlClient;
using System.Reflection.Emit;


namespace m2
{
    public partial class SellerStats : Form
    {
        private Seller currentSeller;
        public SellerStats(Seller seller)
        {
            this.currentSeller = seller;
            InitializeComponent();
            InitializeProductGrid();
        }
        private void LoadBestSellingProductsChart()
        {
            chart1.Series.Clear();
            Series series = chart1.Series.Add("Best Selling Products");
            series.ChartType = SeriesChartType.Bar;
            series.Color = Color.CornflowerBlue;

            // SQL query with correct reference to Product.SellerID
            string query = @"
        SELECT TOP 5 Product.ProductName, SUM(OrderDetails.Quantity) AS UnitsSold
        FROM OrderDetails
        INNER JOIN Product ON OrderDetails.ProductID = Product.ProductID
        INNER JOIN [Order] ON OrderDetails.OrderID = [Order].OrderID
        WHERE Product.SellerID = @SellerID
        GROUP BY Product.ProductName
        ORDER BY UnitsSold DESC;";

            using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SellerID", currentSeller.SellerID); // Add the SellerID parameter

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string productName = reader["ProductName"].ToString();
                            int unitsSold = Convert.ToInt32(reader["UnitsSold"]);
                            series.Points.AddXY(productName, unitsSold);
                        }
                    }
                }
            }
        }

        // Initialize the DataGridView and populate it with dummy data
        private void InitializeProductGrid()
        {
            if (dataGridView1.Columns.Count == 0)  // Only define columns if not already defined
            {
                // Add columns manually
                dataGridView1.Columns.Add("ProductName", "Product Name");
                dataGridView1.Columns.Add("Category", "Category");
                dataGridView1.Columns.Add("Price", "Price");
                dataGridView1.Columns.Add("Brand", "Brand");
                dataGridView1.Columns.Add("Rating", "Rating");
                dataGridView1.Columns.Add("ShippingOption", "Shipping Option");
            }
            // SQL query with JOIN to get the category name
            string query = @"
        SELECT 
            Product.ProductID, 
            Product.ProductName, 
            Category.CategoryName,  -- Joining with Category table to get CategoryName
            Product.UnitPrice, 
            Product.Brand, 
            Product.Rating, 
            Product.ShippingOptions
        FROM Product
        INNER JOIN Category ON Product.CategoryID = Category.CategoryID  -- Join condition
        WHERE Product.SellerID = @SellerID;";

            using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SellerID", currentSeller.SellerID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear existing rows before populating
                        dataGridView1.Rows.Clear();

                        while (reader.Read())
                        {
                            int rowIndex = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowIndex];

                            row.Cells["ProductName"].Value = reader["ProductName"].ToString();
                            row.Cells["Category"].Value = reader["CategoryName"].ToString(); // Get the category name
                            row.Cells["Price"].Value = reader["UnitPrice"].ToString();
                            row.Cells["Brand"].Value = reader["Brand"].ToString();
                            row.Cells["Rating"].Value = reader["Rating"].ToString();
                            row.Cells["ShippingOption"].Value = reader["ShippingOptions"].ToString();
                        }
                    }
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            LoadBestSellingProductsChart();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            SellerOptions so = new SellerOptions(currentSeller);
            this.Hide();
            so.Show();
        }

        private void SellerStats_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Click for total sales
            string query = @"
        SELECT SUM(OrderDetails.Quantity * OrderDetails.UnitPrice) AS TotalSales
        FROM OrderDetails
        INNER JOIN Product ON OrderDetails.ProductID = Product.ProductID
        INNER JOIN [Order] ON OrderDetails.OrderID = [Order].OrderID
        WHERE Product.SellerID = @SellerID;";

            using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SellerID", currentSeller.SellerID);
                    object result = cmd.ExecuteScalar();
                    label1.Text = $"Total Sales: {(result != DBNull.Value ? result.ToString() : "0")}";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Click for average order value
            string query = @"
        SELECT AVG([Order].TotalPrice) AS AverageOrderValue
        FROM [Order]
        INNER JOIN OrderDetails ON [Order].OrderID = OrderDetails.OrderID
        INNER JOIN Product ON OrderDetails.ProductID = Product.ProductID
        WHERE Product.SellerID = @SellerID;";

            using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SellerID", currentSeller.SellerID);
                    object result = cmd.ExecuteScalar();
                    label2.Text = $"Average Order Value: {(result != DBNull.Value ? result.ToString() : "0")}";
                }
            }
        }

    }
}
