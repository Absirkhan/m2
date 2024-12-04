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
    public partial class OrderOversightForm : Form
    {
        public OrderOversightForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = cmbOrderStatus.SelectedItem.ToString();
            // Add logic to filter the DataGridView based on the selected status
            MessageBox.Show($"Filtering orders by status: {selectedStatus}");
        }

        private void OrderOversightForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSearchOrders_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearchOrders.Text;
            // Add logic to filter the DataGridView based on the search query
            MessageBox.Show($"Searching for orders containing: {searchQuery}");
        }

        private void txtSearchOrders_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
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
