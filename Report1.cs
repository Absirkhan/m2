using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace m2
{
    public partial class Report1 : Form
    {
        public Report1()
        {
            InitializeComponent();
        }

        private void Report1_Load(object sender, EventArgs e)
        {

            reportViewer1.LocalReport.ReportPath = @"C:\Users\Fast\source\repos\Absirkhan\m2\report1.rdlc";

            // Define date range (you can modify this to use user input)
            DateTime startDate = new DateTime(2024, 1, 1);
            DateTime endDate = new DateTime(2024, 12, 31);

            // Fetch data from stored procedures
            DataTable totalSalesData = GetDataFromProcedure("GetTotalSales", startDate, endDate);
            DataTable bestSellingProductsData = GetDataFromProcedure("GetBestSellingProducts", startDate, endDate);
            DataTable topCategoriesData = GetDataFromProcedure("GetTopCategories", startDate, endDate);

            // Add datasets to the report
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", totalSalesData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", bestSellingProductsData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", topCategoriesData));

            // Set parameters for the report
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                new ReportParameter("StartDate", startDate.ToString("2024-01-01")),
                new ReportParameter("EndDate", endDate.ToString("2024-12-31"))
            });

            try
            {
                // Refresh the ReportViewer
                reportViewer1.RefreshReport();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }


        }
        private DataTable GetDataFromProcedure(string procedureName, DateTime startDate, DateTime endDate)
        {
            // Connection string to your database
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;\r\n";

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
    


    private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report2 rr = new Report2();
            this.Hide();
            rr.Show();
        }
    }
}
