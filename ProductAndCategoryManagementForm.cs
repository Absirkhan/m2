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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace m2
{
    public partial class ProductAndCategoryManagementForm : Form
    {
        public ProductAndCategoryManagementForm()
        {
            InitializeComponent();
        }

        private void LoadCategoriesAndProducts()
        {
            string query = @"
        SELECT 
            c.CategoryID,
            c.CategoryName,
            p.ProductID,
            p.ProductName
        FROM 
            Category c
        LEFT JOIN 
            Product p ON c.CategoryID = p.CategoryID";

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);

                        // Set the DataSource for the DataGridView
                        dataGridView1.DataSource = dt;

                        // Set column headers
                        dataGridView1.Columns["CategoryID"].HeaderText = "Category ID";
                        dataGridView1.Columns["CategoryName"].HeaderText = "Category Name";
                        dataGridView1.Columns["ProductID"].HeaderText = "Product ID";
                        dataGridView1.Columns["ProductName"].HeaderText = "Product Name";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading categories and products: {ex.Message}");
            }
        }

        private void LoadProducts()
        {
            string query = @"
            SELECT 
                p.ProductID,
                p.ProductName,
                s.SellerName,  -- Assuming Seller table contains SellerName
                c.CategoryName,  -- Join with Category table
                p.UnitPrice,
                p.Quantity,
                p.Brand,
                p.ShippingOptions,
                p.Rating
            FROM 
                Product p
            LEFT JOIN 
                Seller s ON p.SellerID = s.SellerID  -- Join Seller table to get SellerName
            INNER JOIN 
                Category c ON p.CategoryID = c.CategoryID"; 

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);

                        // Set the DataSource for the DataGridView
                        dgvProducts.DataSource = dt;

                        // Set column headers
                        dgvProducts.Columns["ProductID"].HeaderText = "Product ID";
                        dgvProducts.Columns["ProductName"].HeaderText = "Product Name";
                        dgvProducts.Columns["SellerName"].HeaderText = "Seller Name";
                        dgvProducts.Columns["CategoryName"].HeaderText = "Category Name";
                        dgvProducts.Columns["UnitPrice"].HeaderText = "Unit Price";
                        dgvProducts.Columns["Quantity"].HeaderText = "Quantity";
                        dgvProducts.Columns["Brand"].HeaderText = "Brand";
                        dgvProducts.Columns["ShippingOptions"].HeaderText = "Shipping Options";
                        dgvProducts.Columns["Rating"].HeaderText = "Rating";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading products: {ex.Message}");
            }
        }


        private void ProductAndCategoryManagementForm_Load(object sender, EventArgs e)
        {
            // Call the LoadCategoriesAndProducts method when the form loads
            LoadCategoriesAndProducts();
            LoadProducts();
        }


        private void cmbFilterProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchProducts_TextChanged(object sender, EventArgs e)
        {
            //search by product id
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //data grid view for products
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
            //search by name textbox
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            AddCategory adc = new AddCategory();
            this.Hide();
            adc.Show();
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            EditCategory adc = new EditCategory();
            this.Hide();
            adc.Show();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            // Check if a category is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the CategoryID from the selected row
                int selectedCategoryID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CategoryID"].Value);

                // Confirm deletion
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this category?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // Establish SQL connection to delete the category
                        using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                        {
                            conn.Open();

                            // SQL query to delete the category
                            string deleteQuery = "DELETE FROM Category WHERE CategoryID = @CategoryID";

                            using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                            {
                                // Add parameter to avoid SQL injection
                                cmd.Parameters.AddWithValue("@CategoryID", selectedCategoryID);

                                // Execute the delete command
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // Remove the selected row from DataGridView
                                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                                    // Show success message
                                    MessageBox.Show("Category deleted successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("Category not found or could not be deleted.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the category: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a category to delete.");
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


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //search by id textbox
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Get the values entered in the textboxes
            string categoryIdText = textBox1.Text.Trim();   // Category ID from TextBox1
            string categoryNameText = txtCategoryName.Text.Trim();  // Category Name from TextBox2

            // Construct the base query
            string query = @"
            SELECT 
                c.CategoryID,
                c.CategoryName,
                p.ProductID,
                p.ProductName
            FROM 
                Category c
            LEFT JOIN   
                Product p ON c.CategoryID = p.CategoryID
            WHERE 1=1";  // Starting point, will append conditions below

            // Append filtering conditions based on input
            if (!string.IsNullOrEmpty(categoryIdText))
            {
                query += " AND c.CategoryID = @CategoryID"; // Filter by Category ID
            }

            if (!string.IsNullOrEmpty(categoryNameText))
            {
                query += " AND c.CategoryName LIKE @CategoryName"; // Filter by Category Name
            }

            // Execute the query with parameters
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        if (!string.IsNullOrEmpty(categoryIdText))
                        {
                            cmd.Parameters.AddWithValue("@CategoryID", categoryIdText);
                        }
                        if (!string.IsNullOrEmpty(categoryNameText))
                        {
                            cmd.Parameters.AddWithValue("@CategoryName", "%" + categoryNameText + "%"); // LIKE search for category name
                        }

                        // Use SqlDataAdapter to fetch the data
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);

                        // Bind the result to the DataGridView
                        dataGridView1.DataSource = dt;

                        // Set the column headers
                        dataGridView1.Columns["CategoryID"].HeaderText = "Category ID";
                        dataGridView1.Columns["CategoryName"].HeaderText = "Category Name";
                        dataGridView1.Columns["ProductID"].HeaderText = "Product ID";
                        dataGridView1.Columns["ProductName"].HeaderText = "Product Name";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filtering categories and products: {ex.Message}");
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //search product by id
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //search product by name
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //search for product button
            // Get the values from the textboxes
            string productIdText = textBox2.Text.Trim();
            string productNameText = textBox3.Text.Trim();

            // Base query to fetch product details
            string query = @"
        SELECT 
            p.ProductID,
            p.ProductName,
            s.SellerName,
            c.CategoryName,
            p.UnitPrice,
            p.Quantity,
            p.Brand,
            p.ShippingOptions,
            p.Rating
        FROM 
            Product p
        LEFT JOIN 
            Seller s ON p.SellerID = s.SellerID
        INNER JOIN 
            Category c ON p.CategoryID = c.CategoryID
        WHERE 1=1"; // Starting condition

            // Add filtering based on ProductID or ProductName
            if (!string.IsNullOrEmpty(productIdText))
            {
                query += " AND p.ProductID = @ProductID"; // Filter by ProductID
            }

            if (!string.IsNullOrEmpty(productNameText))
            {
                query += " AND p.ProductName LIKE @ProductName"; // Filter by ProductName (using LIKE for partial matching)
            }

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        if (!string.IsNullOrEmpty(productIdText))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", productIdText);
                        }

                        if (!string.IsNullOrEmpty(productNameText))
                        {
                            cmd.Parameters.AddWithValue("@ProductName", "%" + productNameText + "%"); // Using LIKE for partial matches
                        }

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);

                        // Bind the result to the DataGridView
                        dgvProducts.DataSource = dt;

                        // Set column headers
                        dgvProducts.Columns["ProductID"].HeaderText = "Product ID";
                        dgvProducts.Columns["ProductName"].HeaderText = "Product Name";
                        dgvProducts.Columns["SellerName"].HeaderText = "Seller Name";
                        dgvProducts.Columns["CategoryName"].HeaderText = "Category Name";
                        dgvProducts.Columns["UnitPrice"].HeaderText = "Unit Price";
                        dgvProducts.Columns["Quantity"].HeaderText = "Quantity";
                        dgvProducts.Columns["Brand"].HeaderText = "Brand";
                        dgvProducts.Columns["ShippingOptions"].HeaderText = "Shipping Options";
                        dgvProducts.Columns["Rating"].HeaderText = "Rating";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching for products: {ex.Message}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {// Check if a product is selected in the DataGridView
            if (dgvProducts.SelectedRows.Count > 0)
            {
                // Get the ProductID from the selected row
                int productID = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ProductID"].Value);

                // Create an instance of the AdminUpdateProduct form, passing the ProductID
                AdminUpdateProduct ad = new AdminUpdateProduct(productID);

                // Hide the current form and show the AdminUpdateProduct form
                this.Hide();
                ad.Show();
            }
            else
            {
                // Show a message if no product is selected
                MessageBox.Show("Please select a product to edit.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //add product
            AdminAddProduct ad = new AdminAddProduct();
            this.Hide();
            ad.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Check if a product is selected in the DataGridView
            if (dgvProducts.SelectedRows.Count > 0)
            {
                // Get the ProductID from the selected row
                int productID = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ProductID"].Value);

                // Confirm deletion with the user
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this product?",
                                                            "Confirm Deletion",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    // SQL query to delete the product
                    string query = "DELETE FROM Product WHERE ProductID = @ProductID";

                    try
                    {
                        using (SqlConnection conn = new SqlConnection("Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True"))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                // Add the ProductID parameter to avoid SQL injection
                                cmd.Parameters.AddWithValue("@ProductID", productID);

                                // Execute the delete query
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // Remove the selected row from the DataGridView
                                    dgvProducts.Rows.RemoveAt(dgvProducts.SelectedRows[0].Index);

                                    // Show success message
                                    MessageBox.Show("Product deleted successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("Product could not be deleted.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the product: {ex.Message}");
                    }
                }
            }
            else
            {
                // Show a message if no product is selected
                MessageBox.Show("Please select a product to delete.");
            }
        }

    }
}
