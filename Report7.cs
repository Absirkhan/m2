using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace m2
{
    public partial class Report7 : Form
    {
        public Report7()
        {
            InitializeComponent();
        }

        private void Report7_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.ReportPath = @"C:\Users\Fast\source\repos\Absirkhan\m2\Report7.rdlc";

            // Fetch data from stored procedures
            DataTable averageFulfillmentTimeData = GetScalarDataFromProcedure("GetAverageFulfillmentTime");
            DataTable orderCompletionRateData = GetDataFromProcedure("GetOrderCompletionRate");

            // Add datasets to the report
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FulfillmentTime", averageFulfillmentTimeData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CompletionRate", orderCompletionRateData));

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
            Report6 rr = new Report6();
            this.Hide();
            rr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report81 rr = new Report81();
            this.Hide();
            rr.Show();
        }

        private DataTable GetDataFromProcedure(string procedureName)
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

        private DataTable GetScalarDataFromProcedure(string procedureName)
        {
            // Define the connection string to your database
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";

            // Create a DataTable to hold the scalar result
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Open the connection and get the scalar result
                    conn.Open();
                    object result = cmd.ExecuteScalar(); // ExecuteScalar for single value results

                    // Add the scalar result into a DataTable
                    if (result != null)
                    {
                        dt.Columns.Add("AverageFulfillmentTime");
                        DataRow row = dt.NewRow();
                        row["AverageFulfillmentTime"] = result;
                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }
    }
}
