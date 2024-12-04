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
    public partial class ViewProfile : Form
    {
        public ViewProfile()
        {
            InitializeComponent();
        }

        private void viewProfile_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text) ||
                string.IsNullOrWhiteSpace(textBox12.Text))
            {
                // Show an error message if any required textbox is empty
                MessageBox.Show("Please fill all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Proceed with the sign-up process if all fields are filled
                MessageBox.Show("Customer successfully signed up!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();

                // Implement the sign-up logic here (e.g., save the data to the database)
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Saved Successfully!");
            CustomerOptionChoice coc = new CustomerOptionChoice();
            this.Hide();
            coc.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CustomerOptionChoice coc = new CustomerOptionChoice();
            this.Hide();
            coc.Show();
        }
    }
}
