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
    public partial class ProductsShown : Form

    {
        public ProductsShown(DataTable productsTable)
        {
            InitializeComponent();

            // Disable AutoGenerateColumns to manually define columns
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Add a column for the image
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                HeaderText = "Image",
                DataPropertyName = "Image_url",  // Data binding to the Image_url field
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            dataGridView1.Columns.Add(imageColumn);

            // Define other columns manually
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Brand",
                DataPropertyName = "Brand",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Price",
                DataPropertyName = "UnitPrice",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Shipping Options",
                DataPropertyName = "ShippingOptions",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Rating",
                DataPropertyName = "Rating",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Set DataSource
            dataGridView1.DataSource = productsTable;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;

            // Optional: Debugging information
            Console.WriteLine($"Rows in DataTable: {productsTable.Rows.Count}, Columns: {productsTable.Columns.Count}");
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the current cell is the image column
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "Image_url")
            {
                string imagePath = e.Value.ToString();
                try
                {
                    // Convert the image path to an Image object
                    e.Value = Image.FromFile(imagePath);
                }
                catch (Exception ex)
                {
                    // Handle invalid paths (e.g., if the image is not found)
                    MessageBox.Show($"Error loading image: {ex.Message}");
                    e.Value = null;  // Optionally set a default image here
                }
            }
        }



        private void ProductsShown_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            CustomerOptionChoice customerOptionChoice= new CustomerOptionChoice();
            this.Hide();
            customerOptionChoice.Show();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
          //  ViewCart vc = new ViewCart(shoppingCart);
           // this.Hide();
            //vc.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}