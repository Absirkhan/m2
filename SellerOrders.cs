using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace m2

{
    public partial class SellerOrders : Form
    {
        public SellerOrders()
        {
            InitializeComponent();
            InitializeProductGrid();
        }

        public class Product
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
            public string Brand { get; set; }
            public int Rating { get; set; }
            public string ShippingOption { get; set; }
            public string ImagePath { get; set; }
        }

        // Initialize the DataGridView and populate it with dummy data
        private void InitializeProductGrid()
        {
            // Configure DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowTemplate.Height = 60; // Set row height for better image display

            // Add an Image column
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = "ProductImage",
                HeaderText = "Image",
                ImageLayout = DataGridViewImageCellLayout.Zoom // Adjust image layout
            };
            dataGridView1.Columns.Insert(0, imageColumn); // Insert the image column as the first column

            // Add other columns
            dataGridView1.Columns.Add("Name", "Product Name");
            dataGridView1.Columns.Add("Category", "Category");
            dataGridView1.Columns.Add("Price", "Price");
            dataGridView1.Columns.Add("Brand", "Brand");
            dataGridView1.Columns.Add("Rating", "Rating");
            dataGridView1.Columns.Add("ShippingOption", "Shipping Option");

            // Sample product list
            List<Product> products = GetDummyProducts();

            // Populate the DataGridView with products
            PopulateProductGrid(products);
        }

        // Method to provide dummy product data
        private List<Product> GetDummyProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Laptop",
                    Category = "Electronics",
                    Price = 999.99m,
                    Brand = "Dell",
                    Rating = 4,
                    ShippingOption = "Free",
                    ImagePath = @"C:\Users\absir\Desktop\uni\5th sem\DB\final project\m2\Logo.png"
                },
                new Product
                {
                    Name = "T-Shirt",
                    Category = "Clothing",
                    Price = 19.99m,
                    Brand = "Nike",
                    Rating = 5,
                    ShippingOption = "Paid",
                    ImagePath = @"C:\Users\absir\Desktop\uni\5th sem\DB\final project\m2\Logo.png"
                },
                new Product
                {
                    Name = "Blender",
                    Category = "Home Appliances",
                    Price = 49.99m,
                    Brand = "Philips",
                    Rating = 3,
                    ShippingOption = "Free",
                    ImagePath = @"C:\Users\absir\Desktop\uni\5th sem\DB\final project\m2\Logo.png"
                },
                new Product
                {
                    Name = "Headphones",
                    Category = "Electronics",
                    Price = 29.99m,
                    Brand = "Sony",
                    Rating = 4,
                    ShippingOption = "Paid",
                    ImagePath = @"C:\Users\absir\Desktop\uni\5th sem\DB\final project\m2\Logo.png"
                }
            };
        }

        // Method to populate the DataGridView
        private void PopulateProductGrid(List<Product> products)
        {
            foreach (var product in products)
            {
                // Load the image from the file path
                Image productImage = null;
                if (File.Exists(product.ImagePath))
                {
                    productImage = Image.FromFile(product.ImagePath);
                }

                // Add a row with image and product details
                dataGridView1.Rows.Add(
                    productImage,               // Image
                    product.Name,               // Product Name
                    product.Category,           // Category
                    product.Price.ToString("C"),// Price (formatted as currency)
                    product.Brand,              // Brand
                    product.Rating,             // Rating
                    product.ShippingOption      // Shipping Option
                );
            }
        }

        private string GenerateShippingLabel(DataGridViewRow row)
        {
            string shippingLabel = $"Order ID: {row.Cells["OrderID"].Value}\n" +
                                   $"Customer Name: {row.Cells["CustomerName"].Value}\n" +
                                   $"Address: {row.Cells["ShippingAddress"].Value}\n" +
                                   $"Product: {row.Cells["ProductName"].Value}\n" +
                                   $"Quantity: {row.Cells["Quantity"].Value}\n" +
                                   $"Shipping Method: {row.Cells["ShippingMethod"].Value}\n" +
                                   $"Date: {DateTime.Now.ToShortDateString()}";
            return shippingLabel;
        }



        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to print.");
                return;
            }

            // Get the selected order
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string shippingLabel = GenerateShippingLabel(selectedRow);

            // Initialize PrintDocument
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (s, ev) =>
            {
                ev.Graphics.DrawString(shippingLabel, new Font("Arial", 12), Brushes.Black, 10, 10);
            };

            // Show Print Preview
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument;
            previewDialog.ShowDialog();


        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to update.");
                return;
            }

            string orderId = dataGridView1.SelectedRows[0].Cells["OrderID"].Value.ToString();
            //UpdateOrderStatus(orderId, "Shipped");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SellerOptions List = new SellerOptions();
            this.Hide();
            List.Show();
        }

        private void SellerOrders_Load(object sender, EventArgs e)
        {

        }
    }
}
