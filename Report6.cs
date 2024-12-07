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
    public partial class Report6 : Form
    {
        public Report6()
        {
            InitializeComponent();
        }

        private void Report6_Load(object sender, EventArgs e)
        {

            reportViewer1.LocalReport.ReportPath = @"C:\Users\Fast\source\repos\Absirkhan\m2\Report61.rdlc"; // Adjust the path accordingly

            // Fetch the data from stored procedures
            DataTable averageRatingData = GetDataFromProcedure("GetAverageProductRatingPerSeller");
            DataTable returnRefundRateData = GetDataFromProcedure("GetReturnAndRefundRatePerSeller");
            DataTable totalSalesData = GetDataFromProcedure("GetTotalSalesPerSeller");

            // Add datasets to the report
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("AverageProductRating", averageRatingData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ReturnAndRefund", returnRefundRateData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SellerTotalSales", totalSalesData));

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
            Report51 rr = new Report51();
            this.Hide();
            rr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report7 rr = new Report7();
            this.Hide(); rr.Show();
        }
        private DataTable GetDataFromProcedure(string procedureName)
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
