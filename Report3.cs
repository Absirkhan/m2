using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace m2
{
    public partial class Report_3 : Form
    {
        public Report_3()
        {
            InitializeComponent();
        }

        private void Report_3_Load(object sender, EventArgs e)
        {
            // Define parameters or thresholds for your procedures
            DateTime startDate = new DateTime(2024, 1, 1);
            DateTime endDate = new DateTime(2024, 12, 31);
            int lowStockThreshold = 40; // Example threshold for low stock

            try
            {
                // Fetch data for datasets
                DataTable lowStockProductsData = GetDataFromProcedure("GetLowStockProducts", threshold: lowStockThreshold); // Only @Threshold
                DataTable deadStockProductsData = GetDataFromProcedure("GetDeadStockProducts", startDate, endDate); // Only @StartDate and @EndDate

                // Add datasets to the report
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("LowStock", lowStockProductsData));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DeadStock", deadStockProductsData));

                // Set parameters for the report
                reportViewer1.LocalReport.SetParameters(new ReportParameter[]
                {
                    new ReportParameter("StartDate", startDate.ToString("yyyy-MM-dd")),
                    new ReportParameter("EndDate", endDate.ToString("yyyy-MM-dd")),
                    new ReportParameter("LowStockThreshold", lowStockThreshold.ToString())
                });

                // Refresh the ReportViewer
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private DataTable GetDataFromProcedure(string procedureName, DateTime startDate = default, DateTime endDate = default, int? threshold = null)
        {
            // Connection string to your database
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters conditionally
                    if (startDate != default && endDate != default)
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                    }

                    if (threshold.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@Threshold", threshold.Value);
                    }

                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report2 rr = new Report2();
            this.Hide();
            rr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report31 rr =new Report31();
            this.Hide();
            rr.Show();
        }
    }
}
