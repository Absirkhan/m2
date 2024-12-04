using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Data.SqlClient;

namespace m2
{
    public partial class ViewCart : Form
    {
        private List<Product> cartItems;

        public ViewCart(List<Product> cart)
        {
            InitializeComponent();
            cartItems = cart;
        }

        private void ViewCart_Load(object sender, EventArgs e)
        {
            cartDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (var product in cartItems)
            {
                cartDataGridView.Rows.Add(
                    product.Name,
                    product.Category,
                    product.Price.ToString("C"),
                    product.Brand,
                    product.Rating,
                    product.ShippingOption
                );
            }
        }

        private void cartDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cartDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = cartDataGridView.SelectedRows[0].Index;
                cartItems.RemoveAt(selectedIndex); // Remove from the cart list
                cartDataGridView.Rows.RemoveAt(selectedIndex); // Remove from the DataGridView
                MessageBox.Show("Item removed from cart.");
            }
            else
            {
                MessageBox.Show("Please select an item to remove.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ProductsShown ps = new ProductsShown();
            //this.Hide();
            //ps.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cartDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = cartDataGridView.SelectedRows[0].Index;
                int newQuantity = (int)numericUpDown1.Value;

                // Update the quantity in DataGridView
                cartDataGridView.SelectedRows[0].Cells["Quantity"].Value = newQuantity.ToString();
                MessageBox.Show("Quantity updated.");
            }
            else
            {
                MessageBox.Show("Please select an item to update quantity.");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Checkout c = new Checkout(cartItems);
            this.Hide();
            c.Show();
        }
    }
}
