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
    public partial class Checkout : Form
    {
        private List<Product> cartItems;

        public Checkout(List<Product> cartItems)
        {
            InitializeComponent();
            this.cartItems = cartItems;
        }

        private void Checkout_Load(object sender, EventArgs e)
        {
            decimal totalPrice = 0;
            StringBuilder summary = new StringBuilder();

            foreach (var product in cartItems)
            {
                summary.AppendLine($"Name: {product.Name}, Price: {product.Price:C}");
                totalPrice += product.Price;
            }

            // Display the summary and total price
            //textBox1.Text = summary.ToString();
            label1.Text = $"Total: {totalPrice:C}";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewCart vc = new ViewCart(cartItems);
            this.Hide();
            vc.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your order has been placed successfully. Thank you for your purchase!");
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SubmitReviews sr = new SubmitReviews();
            this.Hide();
            sr.Show();
        }
    }
}
