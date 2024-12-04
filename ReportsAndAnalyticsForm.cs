using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace m2
{
    public partial class ReportsAndAnalyticsForm : Form
    {
        public ReportsAndAnalyticsForm()
        {
            InitializeComponent();
        }

        private void ReportsAndAnalyticsForm_Load(object sender, EventArgs e)
        {

        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerateSalesReport_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            // Logic to fetch and display sales data in DataGridView
            MessageBox.Show($"Generating sales report from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}");
        }

        private void dgvSalesReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbUserTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerateUserActivityReport_Click(object sender, EventArgs e)
        {
            string userType = cmbUserTypeFilter.SelectedItem.ToString();

            // Logic to fetch and display user activity data in the Chart
            MessageBox.Show($"Generating user activity report for: {userType}");
        }

        private void chartUserActivity_Click(object sender, EventArgs e)
        {

        }

        private void btnViewPerformanceMetric_Click(object sender, EventArgs e)
        {
            string selectedMetric = lstPerformanceMetrics.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedMetric))
            {
                // Logic to display the selected metric in the Chart
                MessageBox.Show($"Displaying metric: {selectedMetric}");
            }
            else
            {
                MessageBox.Show("Please select a metric to view.");
            }
        }

        private void lstPerformanceMetrics_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chartPerformanceMetrics_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDashboard ad = new AdminDashboard();
            this.Hide();
            ad.Show();
        }
    }
}
