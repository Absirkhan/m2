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
    public partial class SellerOptions : Form
    {
        public SellerOptions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellerListings stats = new SellerListings();
            stats.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SellerOrders orders = new SellerOrders();
            orders.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SellerStats stats = new SellerStats();
            stats.Show();
            this.Hide();

            //Seller
        }

        private void SellerOptions_Load(object sender, EventArgs e)
        {

        }
    }
}
