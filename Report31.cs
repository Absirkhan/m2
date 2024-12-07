using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace m2
{
    public partial class Report31 : Form
    {
        public Report31()
        {
            InitializeComponent();
        }

       
        private DataTable GetTurnoverData()
        {
            // Define the connection string to your database
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";

            // Create a DataTable to hold the data from the stored procedure
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetStockTurnoverRate", conn))
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
        private DataTable GetMostReturnedItemsData(int ratingThreshold)
        {
            // Define the connection string to your database
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";

            // Create a DataTable to hold the data from the stored procedure
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetMostReturnedItems", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add the parameter for rating threshold
                    cmd.Parameters.AddWithValue("@RatingThreshold", ratingThreshold);

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


        private void Report31_Load_1(object sender, EventArgs e)
        {
            // Fetch data from GetStockTurnoverRate
            DataTable turnoverData = GetTurnoverData();

            // Fetch data from GetMostReturnedItems (e.g., for ratings ≤ 2)
            int ratingThreshold = 2; // Adjust this as needed
            DataTable mostReturnedItemsData = GetMostReturnedItemsData(ratingThreshold);

            // Clear any existing data sources in the report
            reportViewer1.LocalReport.DataSources.Clear();

            // Add the turnover data as DataSet1
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", turnoverData));

            // Add the most returned items data as DataSet2
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", mostReturnedItemsData));

            // Refresh the report viewer to display the data
            reportViewer1.RefreshReport();
        }


        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report_3 rr = new Report_3();
            this.Hide();
            rr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report41 rr = new Report41();
            this.Hide();
            rr.Show();
        }
    }
}
