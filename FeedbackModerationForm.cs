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
    public partial class FeedbackModerationForm : Form
    {
        public FeedbackModerationForm()
        {
            InitializeComponent();
        }

        private void FeedbackModerationForm_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchFeedback_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchFeedback_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchFeedback.Text;
            string statusFilter = cmbReviewStatus.SelectedItem?.ToString();

            // Logic to filter the DataGridView based on the search criteria
            MessageBox.Show($"Searching reviews with keyword: '{keyword}' and status: '{statusFilter}'");
        }

        private void dgvFeedbackList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnApproveReview_Click(object sender, EventArgs e)
        {
            if (dgvFeedbackList.SelectedRows.Count > 0)
            {
                // Logic to approve the selected review (e.g., update database)
                MessageBox.Show("Review approved successfully!");
            }
            else
            {
                MessageBox.Show("Please select a review to approve.");
            }
        }

        private void btnFlagReview_Click(object sender, EventArgs e)
        {
            if (dgvFeedbackList.SelectedRows.Count > 0)
            {
                // Logic to flag the selected review as inappropriate
                MessageBox.Show("Review flagged as inappropriate.");
            }
            else
            {
                MessageBox.Show("Please select a review to flag.");
            }
        }

        private void btnDeleteReview_Click(object sender, EventArgs e)
        {
            if (dgvFeedbackList.SelectedRows.Count > 0)
            {
                // Logic to delete the selected review (e.g., remove from database)
                MessageBox.Show("Review deleted successfully.");
            }
            else
            {
                MessageBox.Show("Please select a review to delete.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDashboard ad = new AdminDashboard();
            this.Hide();
            ad.Show();
        }
    }
}
