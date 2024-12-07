using Microsoft.Reporting.WinForms;
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

namespace m2
{
    public partial class Report41 : Form
    {
        public Report41()
        {
            InitializeComponent();
        }

        private void Report41_Load(object sender, EventArgs e)
        {

            DateTime startDate = new DateTime(2024, 1, 1); // Example start date
            DateTime endDate = new DateTime(2024, 12, 31); // Example end date
            reportViewer1.LocalReport.ReportPath = @"C:\Users\Fast\source\repos\Absirkhan\m2\Report4.rdlc";
            // Fetch the data for revenue per category
            DataTable categoryRevenueData = GetDataFromProcedure("GetRevenuePerCategory", startDate, endDate);

            // Fetch the category contribution data (no TotalRevenue parameter now)
            DataTable categoryContributionData = GetDataFromProcedure("GetCategoryContribution", startDate, endDate);
            if (categoryRevenueData.Rows.Count == 0)
            {
                MessageBox.Show("No data found for CategoryRevenue.");
            }

            if (categoryContributionData.Rows.Count == 0)
            {
                MessageBox.Show("No data found for CategoryContribution.");
            }

            // Add the datasets to the report
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CategoryRevenue", categoryRevenueData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CategoryContribution", categoryContributionData));

            // Set parameters for the report
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                new ReportParameter("StartDate", startDate.ToString("yyyy-MM-dd")),
                new ReportParameter("EndDate", endDate.ToString("yyyy-MM-dd"))
            });

            try
            {
                // Refresh the ReportViewer
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report31 rr = new Report31();
            this.Hide();
            rr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report42 rr = new Report42();
            this.Hide(); rr.Show();
        }
        private DataTable GetDataFromProcedure(string procedureName, DateTime startDate, DateTime endDate)
        {
            // Connection string to your database
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";

            // Create a DataTable to hold the data from the stored procedure
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    // Open the connection and fill the data into the DataTable
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }
        //private float GetTotalRevenue(DateTime startDate, DateTime endDate)
        //{
        //    // Define the connection string to your database
        //    string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";

        //    float totalRevenue = 0;

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("GetRevenuePerCategory", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@StartDate", startDate);
        //            cmd.Parameters.AddWithValue("@EndDate", endDate);

        //            // Open the connection and fetch the revenue data
        //            conn.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    totalRevenue += Convert.ToSingle(reader["Revenue"]);
        //                }
        //            }
        //        }
        //    }

        //    return totalRevenue;
        //}
        //private DataTable GetCategoryContributionData(DateTime startDate, DateTime endDate, float totalRevenue)
        //{
        //    // Define the connection string to your database
        //    string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";

        //    // Create a DataTable to hold the data from the stored procedure
        //    DataTable dt = new DataTable();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("GetCategoryContribution", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@StartDate", startDate);
        //            cmd.Parameters.AddWithValue("@EndDate", endDate);
        //            cmd.Parameters.AddWithValue("@TotalRevenue", totalRevenue);

        //            // Open the connection and fill the data into the DataTable
        //            conn.Open();
        //            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        //            {
        //                adapter.Fill(dt);
        //            }
        //        }
        //    }

        //    return dt;
        //}
    }
}
