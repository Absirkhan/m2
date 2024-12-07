using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace m2
{
    public partial class Report2 : Form
    {
        public Report2()
        {
            InitializeComponent();
        }

        private void Report2_Load(object sender, EventArgs e)
        {
            // Set report path
            reportViewer1.LocalReport.ReportPath = @"C:\Users\Fast\source\repos\Absirkhan\m2\Report2.rdlc";

            // Define date range
            DateTime startDate = new DateTime(2024, 1, 1);
            DateTime endDate = new DateTime(2024, 12, 31);

            // Fetch data from stored procedures
            DataTable mostActiveCustomersData = GetDataFromProcedure("GetMostActiveCustomers", startDate, endDate);
            DataTable avgSpendData = GetDataFromProcedure("GetAverageSpendPerCustomer", startDate, endDate);
            DataTable repeatedPurchaseData = GetRepeatPurchaseDataAsDataTable(startDate, endDate);

            // Add datasets to the report
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ActiveCustomers", mostActiveCustomersData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("AverageSpend", avgSpendData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("RepeatedPurchase", repeatedPurchaseData));

            // Add date range parameters to the report
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

        // Fetch data for most active customers and average spend
        private DataTable GetDataFromProcedure(string procedureName, DateTime startDate, DateTime endDate)
        {
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // Fetch repeat purchase rate data
        private DataTable GetRepeatPurchaseDataAsDataTable(DateTime startDate, DateTime endDate)
        {
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetRepeatPurchaseRate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt); // Fill the DataTable with the repeat purchase data
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
            Report1 rr = new Report1();
            this.Hide();
            rr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report_3 report = new Report_3();
            this.Hide();
            report.Show();
        }
    }
}
