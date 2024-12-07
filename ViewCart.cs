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
        private Customer currentCustomer;
        public ViewCart(Customer customer)
        {
            this.currentCustomer = customer;
            InitializeComponent();
        }

        private void ViewCart_Load(object sender, EventArgs e)
        {
            cartDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadCartItems();
        }

        private void LoadCartItems()
        {
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";
            string query = @"
        SELECT c.CartID, c.ProductID, c.CustomerID, c.Quantity, 
               p.ProductName, p.UnitPrice, p.Brand, p.Rating, p.ShippingOptions, 
               cat.CategoryName
        FROM Cart c
        INNER JOIN Product p ON c.ProductID = p.ProductID
        INNER JOIN Category cat ON p.CategoryID = cat.CategoryID
        WHERE c.CustomerID = @CustomerID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Pass the current customer ID to the query
                        cmd.Parameters.AddWithValue("@CustomerID", currentCustomer.CustomerID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Clear existing rows in DataGridView
                            cartDataGridView.Rows.Clear();

                            while (reader.Read())
                            {
                                // Calculate the total price (Quantity * Unit Price)
                                decimal price = Convert.ToDecimal(reader["UnitPrice"]);
                                int quantity = Convert.ToInt32(reader["Quantity"]);
                                decimal totalPrice = price * quantity;

                                // Add the row with the calculated total price
                                DataGridViewRow row = new DataGridViewRow();
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = reader["ProductName"].ToString() });
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = reader["CategoryName"].ToString() });
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = totalPrice.ToString("C") });  // Format the total price as currency
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = reader["Brand"].ToString() });
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = reader["Rating"].ToString() });
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = reader["ShippingOptions"].ToString() });
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = quantity.ToString() });

                                // Store the CartID in the Tag property
                                row.Tag = reader["CartID"];

                                // Add the row to the DataGridView
                                cartDataGridView.Rows.Add(row);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the cart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                int cartId = Convert.ToInt32(cartDataGridView.SelectedRows[0].Tag);  // Get CartID from Tag

                // Remove from the database (Cart table)
                RemoveItemFromCart(cartId);

                // Remove from the DataGridView
                cartDataGridView.Rows.RemoveAt(selectedIndex);
                MessageBox.Show("Item removed from cart.");
            }
            else
            {
                MessageBox.Show("Please select an item to remove.");
            }
        }

        private void RemoveItemFromCart(int cartId)
        {
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";
            string query = "DELETE FROM Cart WHERE CartID = @CartID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CartID", cartId);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while removing the item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            CustomerOptionChoice coc = new CustomerOptionChoice(currentCustomer);
            this.Hide();
            coc.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cartDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = cartDataGridView.SelectedRows[0].Index;
                int cartId = Convert.ToInt32(cartDataGridView.SelectedRows[0].Tag);
                int newQuantity = (int)numericUpDown1.Value;

                // Update quantity in the database
                UpdateCartItemQuantity(cartId, newQuantity);

                // Update the quantity in DataGridView
                cartDataGridView.SelectedRows[0].Cells["Quantity"].Value = newQuantity.ToString();
                MessageBox.Show("Quantity updated.");
            }
            else
            {
                MessageBox.Show("Please select an item to update quantity.");
            }
        }

        // Method to update quantity in the database
        private void UpdateCartItemQuantity(int cartId, int quantity)
        {
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                // If quantity is 0, remove the product from the cart
                if (quantity == 0)
                {
                    string deleteQuery = "DELETE FROM Cart WHERE CartID = @CartID";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CartID", cartId);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Item removed from cart.");
                }
                else
                {
                    // Update the quantity if it's greater than 0
                    string updateQuery = "UPDATE Cart SET Quantity = @Quantity WHERE CartID = @CartID";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@CartID", cartId);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Quantity updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the cart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Checkout c = new Checkout(currentCustomer);
            this.Hide();
            c.Show();
        }
    }
}
