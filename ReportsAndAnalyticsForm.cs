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
    public partial class ReportsAndAnalyticsForm : Form
    {
        public ReportsAndAnalyticsForm()
        {
            InitializeComponent();
        }

        private string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";


        private void ReportsAndAnalyticsForm_Load(object sender, EventArgs e)
        {
            cmbUserTypeFilter.Items.Add("Customer");
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerateSalesReport_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            string query = @"
            SELECT 
                p.ProductName,
                SUM(od.Quantity) AS QuantitySold,
                SUM(od.Quantity * od.UnitPrice) AS TotalRevenue
            FROM 
                [Order] o
            INNER JOIN 
                OrderDetails od ON o.OrderID = od.OrderID
            INNER JOIN 
                Product p ON od.ProductID = p.ProductID
            WHERE 
                o.OrderDate BETWEEN @StartDate AND @EndDate
            GROUP BY 
                p.ProductName";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind data to the DataGridView
                        dgvSalesReport.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating sales report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSalesReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbUserTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerateUserActivityReport_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT 
            o.CustomerID,
            COUNT(o.OrderID) AS OrdersPlaced
        FROM 
            [Order] o
        GROUP BY 
            o.CustomerID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        chartUserActivity.Series.Clear();
                        chartUserActivity.Series.Add("User Activity");

                        // Set chart titles and axis labels
                        chartUserActivity.ChartAreas[0].AxisX.Title = "Customer ID";
                        chartUserActivity.ChartAreas[0].AxisY.Title = "Orders Placed";
                        chartUserActivity.Series["User Activity"].IsValueShownAsLabel = true;

                        foreach (DataRow row in dt.Rows)
                        {
                            string customerID = row["CustomerID"].ToString();
                            int ordersPlaced = Convert.ToInt32(row["OrdersPlaced"]);

                            // Add data points to the chart
                            chartUserActivity.Series["User Activity"].Points.AddXY(customerID, ordersPlaced);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating user activity report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void chartUserActivity_Click(object sender, EventArgs e)
        {

        }

        private void btnViewPerformanceMetric_Click(object sender, EventArgs e)
        {
            string selectedMetric = lstPerformanceMetrics.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedMetric))
            {
                string query = "";
                string chartTitle = "";

                // Define queries based on the selected metric
                switch (selectedMetric)
                {
                    case "Active Users (Last 30 Days).":
                        query = @"
                    SELECT 
                        FORMAT(o.OrderDate, 'yyyy-MM-dd') AS Date, 
                        COUNT(DISTINCT o.CustomerID) AS ActiveUsers
                    FROM 
                        [Order] o
                    WHERE 
                        o.OrderDate >= DATEADD(DAY, -30, GETDATE())
                    GROUP BY 
                        FORMAT(o.OrderDate, 'yyyy-MM-dd')
                    ORDER BY 
                        Date";
                        chartTitle = "Active Users (Last 30 Days)";
                        break;

                    case "Total Transactions.":
                        query = @"
                    SELECT 
                        FORMAT(o.OrderDate, 'yyyy-MM-dd') AS Date, 
                        COUNT(o.OrderID) AS TotalTransactions
                    FROM 
                        [Order] o
                    GROUP BY 
                        FORMAT(o.OrderDate, 'yyyy-MM-dd')
                    ORDER BY 
                        Date";
                        chartTitle = "Total Transactions";
                        break;

                    default:
                        MessageBox.Show("Invalid metric selected.");
                        return;
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            chartPerformanceMetrics.Series.Clear();
                            chartPerformanceMetrics.Series.Add(chartTitle);

                            // Set chart titles and axis labels
                            chartPerformanceMetrics.ChartAreas[0].AxisX.Title = "Date";
                            chartPerformanceMetrics.ChartAreas[0].AxisY.Title = selectedMetric;
                            chartPerformanceMetrics.Series[chartTitle].IsValueShownAsLabel = true;

                            // Add data points to the chart
                            foreach (DataRow row in dt.Rows)
                            {
                                string date = row["Date"].ToString();
                                double value = Convert.ToDouble(row[1]); // Use index 1 for the metric value

                                chartPerformanceMetrics.Series[chartTitle].Points.AddXY(date, value);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error generating performance metric: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a metric to view.");
            }
        }


        private void lstPerformanceMetrics_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chartPerformanceMetrics_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDashboard ad = new AdminDashboard();
            this.Hide();
            ad.Show();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
