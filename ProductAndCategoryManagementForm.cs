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
    public partial class ProductAndCategoryManagementForm : Form
    {
        public ProductAndCategoryManagementForm()
        {
            InitializeComponent();
        }

        private void cmbFilterProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchProducts_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text;
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                lstCategories.Items.Add(categoryName); // Add to ListBox
                MessageBox.Show("Category added successfully!");
            }
            else
            {
                MessageBox.Show("Category name cannot be empty.");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItem != null)
            {
                lstCategories.Items[lstCategories.SelectedIndex] = txtCategoryName.Text; // Edit selected category
                MessageBox.Show("Category updated successfully!");
            }
            else
            {
                MessageBox.Show("Select a category to edit.");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItem != null)
            {
                lstCategories.Items.Remove(lstCategories.SelectedItem); // Remove selected category
                MessageBox.Show("Category deleted successfully!");
            }
            else
            {
                MessageBox.Show("Select a category to delete.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get selected product from DataGridView
            if (dgvProducts.SelectedRows.Count > 0)
            {
                // Logic to approve the selected product
                MessageBox.Show("Product approved successfully!");
            }
            else
            {
                MessageBox.Show("Select a product to approve.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                // Logic to reject the selected product
                MessageBox.Show("Product rejected successfully!");
            }
            else
            {
                MessageBox.Show("Select a product to reject.");
            }
        }

        private void btnFlagProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                // Logic to flag the selected product
                MessageBox.Show("Product flagged successfully!");
            }
            else
            {
                MessageBox.Show("Select a product to flag.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AdminDashboard ad = new AdminDashboard();
            this.Hide();
            ad.Show();
        }

        private void ProductAndCategoryManagementForm_Load(object sender, EventArgs e)
        {

        }
    }
}
