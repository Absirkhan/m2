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
    public partial class AssignAgents : Form
    {
        private string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

        public AssignAgents()
        {
            InitializeComponent();
        }

        private void AssignAgents_Load(object sender, EventArgs e)
        {
            LoadOrdersWithoutAgents();
            LoadAgents();
        }

        private void LoadOrdersWithoutAgents()
        {
            string query = @"
                SELECT 
                    o.OrderID, 
                    c.CustomerName, 
                    o.OrderDate, 
                    o.Status
                FROM 
                    [Order] o
                INNER JOIN 
                    Customer c ON o.CustomerID = c.CustomerID
                WHERE 
                    o.AgentID IS NULL";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt; // Assuming dgvOrders is the DataGridView for orders
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAgents()
        {
            string query = "SELECT AgentID, AgentName FROM Agent";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBox1.DataSource = dt; // Assuming cmbAgents is the ComboBox for selecting agents
                    comboBox1.DisplayMember = "AgentName";
                    comboBox1.ValueMember = "AgentID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading agents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && comboBox1.SelectedValue != null)
            {
                int selectedOrderId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderID"].Value);
                int selectedAgentId = Convert.ToInt32(comboBox1.SelectedValue);

                string query = "UPDATE [Order] SET AgentID = @AgentID, Status = 'Assigned' WHERE OrderID = @OrderID";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@AgentID", selectedAgentId);
                            cmd.Parameters.AddWithValue("@OrderID", selectedOrderId);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Agent assigned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrdersWithoutAgents(); // Refresh the orders
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error assigning agent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an order and an agent.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TrackShip obj = new TrackShip();
            this.Hide();
            obj.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogisticsMenu lm = new LogisticsMenu();
            this.Hide();
            lm.Show();
        }
    }
}
