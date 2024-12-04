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
    public partial class SubmitReviews : Form
    {
        public SubmitReviews()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void SubmitReviews_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = 4;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a review.");
            }
            else
            {
                MessageBox.Show("Review Submitted!");
                
                CustomerOptionChoice coc = new CustomerOptionChoice();
                this.Hide();
                coc.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomerOptionChoice coc = new CustomerOptionChoice();
            this.Hide();
            coc.Show();
        }
    }
}
