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
    public partial class LogisticsMenu : Form
    {
        public LogisticsMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrackShip ts = new TrackShip();
            this.Hide();
            ts.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sellerORcustomer soc = new sellerORcustomer();
            this.Hide();
            soc.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AssignAgents aa = new AssignAgents();
            this.Hide();
            aa.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Shipment s = new Shipment();
            this.Hide();    
            s.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Notifications notifications = new Notifications();
            this.Hide();
            notifications.Show();
        }

        private void LogisticsMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
