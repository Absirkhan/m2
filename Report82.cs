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
    public partial class Report82 : Form
    {
        public Report82()
        {
            InitializeComponent();
        }

        private void Report82_Load(object sender, EventArgs e)
        {
            // Set the timeframe in days
            int timeFrameInDays = 7; // Example value

            // Specify the path to your RDLC report
            reportViewer1.LocalReport.ReportPath = @"C:\Users\Fast\source\repos\Absirkhan\m2\Report82.rdlc";

            // Fetch data from stored procedures
            DataTable abandonmentReasonsData = GetDataFromProcedure("GetCartAbandonmentReasons");
            DataTable abandonedProductsData = GetDataFromProcedure("GetProductsInAbandonedCarts", timeFrameInDays);

            // Add datasets to the report
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Reasons", abandonmentReasonsData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ProductsAbandoned", abandonedProductsData));

            // Set the parameters for the report
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]
            {
                new ReportParameter("TimeFrameInDays", timeFrameInDays.ToString())
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

        private void button1_Click(object sender, EventArgs e)
        {
            Report81 rr = new Report81();
            this.Hide();
            rr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report9 rr = new Report9();
            this.Hide(); rr.Show();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
        private DataTable GetDataFromProcedure(string procedureName, int timeFrameInDays = 0)
        {
            // Define the connection string to your database
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";

            // Create a DataTable to hold the data from the stored procedure
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter if required
                    if (timeFrameInDays > 0)
                    {
                        cmd.Parameters.AddWithValue("@TimeFrameInDays", timeFrameInDays);
                    }

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
