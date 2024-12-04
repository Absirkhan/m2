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
    public partial class CustomerOptionChoice : Form
    {
        public CustomerOptionChoice()
        {
            InitializeComponent();
        }

       
        private void CustomerOptionChoice_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Connection string
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            string query = @"
            SELECT p.Image_url, p.ProductName, p.Brand, p.UnitPrice, p.ShippingOptions, p.Rating
            FROM Product p
            INNER JOIN Category c ON p.CategoryID = c.CategoryID
            WHERE 1=1"; // Ensures no filters still return all products


            // Apply filters based on user inputs
            if (comboBox1.SelectedItem != null) // Category filter
            {
                query += " AND c.CategoryName = @Category";
            }

            if (numericUpDown1.Value > 0) // Price filter
            {
                query += " AND UnitPrice <= @Price";
            }

            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked) // Rating filter
            {
                int rating = 0;
                if (radioButton1.Checked) rating = 1;
                else if (radioButton2.Checked) rating = 2;
                else if (radioButton3.Checked) rating = 3;
                else if (radioButton4.Checked) rating = 4;

                query += " AND Rating <= @Rating";
            }

            if (comboBox2.SelectedItem != null) // Brand filter
            {
                query += " AND Brand = @Brand";
            }

            if (checkBox1.Checked || checkBox2.Checked) // Shipping filter
            {
                query += " AND ShippingOptions IN (";

                if (checkBox1.Checked)
                    query += "'Free Shipping',";
                if (checkBox2.Checked)
                    query += "'Standard Shipping',";

                query = query.TrimEnd(',') + ")";
            }

            // Execute the query and display results
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        if (comboBox1.SelectedItem != null)
                            cmd.Parameters.AddWithValue("@Category", comboBox1.SelectedItem.ToString());

                        if (numericUpDown1.Value > 0)
                            cmd.Parameters.AddWithValue("@Price", numericUpDown1.Value);

                        if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked)
                        {
                            int rating = 0;
                            if (radioButton1.Checked) rating = 1;
                            else if (radioButton2.Checked) rating = 2;
                            else if (radioButton3.Checked) rating = 3;
                            else if (radioButton4.Checked) rating = 4;

                            cmd.Parameters.AddWithValue("@Rating", rating);
                        }

                        if (comboBox2.SelectedItem != null)
                            cmd.Parameters.AddWithValue("@Brand", comboBox2.SelectedItem.ToString());

                        // Read the results
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable productsTable = new DataTable();
                            productsTable.Load(reader);

                            // Check if there are results
                            if (productsTable.Rows.Count > 0)
                            {
                                MessageBox.Show($"Rows in DataTable: {productsTable.Rows.Count}");

                                // Pass results to the next form
                                ProductsShown ps = new ProductsShown(productsTable);
                                this.Hide();
                                ps.Show();
                            }
                            else
                            {
                                MessageBox.Show("No products match the selected criteria.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SubmitReviews sr = new SubmitReviews();
            this.Hide();
            sr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewProfile vp = new ViewProfile();
            this.Hide();
            vp.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewPastOrders vpo = new ViewPastOrders();
            this.Hide();
            vpo.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //category
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //price
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //rating 3
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //rating 4
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //rating 2
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //rating 1
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //brands
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //free shipping
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //paid shipping
        }
    }
}
