using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace m2
{
    public partial class Report102 : Form
    {
        public Report102()
        {
            InitializeComponent();
        }

        private void Report102_Load(object sender, EventArgs e)
        {
            // Specify the path to your RDLC report
            reportViewer1.LocalReport.ReportPath = @"C:\Users\Fast\source\repos\Absirkhan\m2\Report102.rdlc";

            // Fetch data for each procedure
            DataTable ageDistributionData = GetDataFromProcedure("GetAgeDistribution");
            DataTable genderAnalysisData = GetDataFromProcedure("GetGenderAnalysis");
            DataTable locationInsightsData = GetDataFromProcedure("GetLocationBasedInsights");

            // Add datasets to the report
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("AgeDistribution", ageDistributionData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("GenderAnalysis", genderAnalysisData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("LocationInsights", locationInsightsData));

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
            // Any additional logic for report viewer load if necessary
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report101 rr = new Report101();
            this.Hide();
            rr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        // Method to execute the stored procedure and return the results as a DataTable
        private DataTable GetDataFromProcedure(string procedureName)
        {
            string connectionString = "Server=.\\SQLEXPRESS;Database=m3;Trusted_Connection=True;";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

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
