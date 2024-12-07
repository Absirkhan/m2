using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace m2
{
    public partial class Report9 : Form
    {
        public Report9()
        {
            InitializeComponent();
        }

        private void Report9_Load(object sender, EventArgs e)
        {
            // Set the date range parameters for the reports
            DateTime startDate = new DateTime(2024, 1, 1); // Example start date
            DateTime endDate = new DateTime(2024, 12, 31); // Example end date

            // Specify the path to your RDLC report
            reportViewer1.LocalReport.ReportPath = @"C:\Users\Fast\source\repos\Absirkhan\m2\Report91.rdlc";

            // Fetch data for each procedure
            DataTable activeUserRatioData = GetDataFromProcedure("GetActiveUserRatio");
            DataTable churnRateData = GetDataFromProcedure("GetChurnRate");
            DataTable userEngagementData = GetDataFromProcedure("GetUserEngagementMetrics", startDate, endDate);
            DataTable newUserRegistrationsData = GetDataFromProcedure("GetNewUserRegistrations", startDate, endDate);

            // Add datasets to the report
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("UserRatio", activeUserRatioData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ChurnRate", churnRateData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Metrics", userEngagementData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("NewUsers", newUserRegistrationsData));

            // Set the parameters for the report
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
            Report82 rr = new Report82();
            this.Hide();
            rr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report101 rr = new Report101();
            this.Hide();
            rr.Show();
        }

        /// <summary>
        /// General method to fetch data from a stored procedure with optional parameters.
        /// </summary>
        private DataTable GetDataFromProcedure(string procedureName, DateTime? startDate = null, DateTime? endDate = null)
        {
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters conditionally
                    if (startDate.HasValue)
                        cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                    if (endDate.HasValue)
                        cmd.Parameters.AddWithValue("@EndDate", endDate.Value);

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
