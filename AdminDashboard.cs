using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using m2;
using System.Data.SqlClient;


namespace m2
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserAndSellerManagementForm userform = new UserAndSellerManagementForm();
            this.Hide();
            userform.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ProductAndCategoryManagementForm productForm = new ProductAndCategoryManagementForm();
            this.Hide();
            productForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OrderOversightForm orderForm = new OrderOversightForm();
            this.Hide();
            orderForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReportsAndAnalyticsForm reportsForm = new ReportsAndAnalyticsForm();
            this.Hide();
            reportsForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FeedbackModerationForm feedbackForm = new FeedbackModerationForm();
            this.Hide();
            feedbackForm.Show();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            sellerORcustomer soc = new sellerORcustomer();
            this.Hide();
            soc.Show();
        }
    }
}