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
    public partial class ViewPastOrders : Form
    {
        public ViewPastOrders()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerOptionChoice coc = new CustomerOptionChoice();
            this.Hide();
            coc.Show();
        }

        private void ViewPastOrders_Load(object sender, EventArgs e)
        {

        }
    }
}
