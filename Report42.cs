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
    public partial class Report42 : Form
    {
        public Report42()
        {
            InitializeComponent();
        }

        private void Report42_Load(object sender, EventArgs e)
        {

            // Define the start and end dates for both periods
            DateTime startDate = new DateTime(2024, 1, 1);  // Example start date
            DateTime endDate = new DateTime(2024, 12, 31);  // Example end date
            DateTime startDateCurrent = new DateTime(2024, 6, 1);  // Example start date for current period
            DateTime endDateCurrent = new DateTime(2024, 6, 30);  // Example end date for current period

            // Specify the path to your RDLC report
            reportViewer1.LocalReport.ReportPath = @"C:\Users\Fast\source\repos\Absirkhan\m2\Report42.rdlc";

            // Fetch the data for revenue per category (passing all parameters)
            DataTable trendingCategoryData = GetDataFromProcedure("GetTrendingCategories", startDate, endDate, startDateCurrent, endDateCurrent);

            if (trendingCategoryData.Rows.Count == 0)
            {
                MessageBox.Show("No data returned for trending categories.");
            }


            // Add the datasets to the report
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TrendingCategory", trendingCategoryData));
            

            // Set the parameters for the report (StartDate, EndDate, StartDateCurrent, EndDateCurrent)
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                new ReportParameter("StartDate", startDate.ToString("yyyy-MM-dd")),
                new ReportParameter("EndDate", endDate.ToString("yyyy-MM-dd")),
                new ReportParameter("StartDateCurrent", startDateCurrent.ToString("yyyy-MM-dd")),
                new ReportParameter("EndDateCurrent", endDateCurrent.ToString("yyyy-MM-dd"))
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
            Report41 rr = new Report41();
            this.Hide();
            rr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report51 rr = new Report51();
            this.Hide(); rr.Show();
        }
        private DataTable GetDataFromProcedure(string procedureName, DateTime startDate, DateTime endDate, DateTime startDateCurrent, DateTime endDateCurrent)
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
                    cmd.Parameters.AddWithValue("@StartDateCurrent", startDateCurrent);
                    cmd.Parameters.AddWithValue("@EndDateCurrent", endDateCurrent);

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

    }
}
