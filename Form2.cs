﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;



namespace m2
{
    public partial class Form2 : Form
    {
        private string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

        public Form2()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            customerSignUp csu = new customerSignUp();
            csu.Show();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the pressed key is Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger the login button click event
                button1.PerformClick();
            }
        }

        private void ValidateAndSubmit()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // SQL query to verify login
                    string query = "SELECT COUNT(*) FROM Customer WHERE CustomerUsername = @username AND CustomerPassword = @pass";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@username", textBox2.Text);
                        cmd.Parameters.AddWithValue("@pass", textBox1.Text);

                        int result = (int)cmd.ExecuteScalar();

                        if (result > 0)
                        {
                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CustomerOptionChoice coc = new CustomerOptionChoice();
                            this.Hide();
                            coc.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ValidateAndSubmit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.PasswordChar = '\0';
            }
            else
            {
                textBox1.PasswordChar = '*';
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sellerORcustomer soc = new sellerORcustomer();
            this.Hide();
            soc.Show();
        }
    }
}
