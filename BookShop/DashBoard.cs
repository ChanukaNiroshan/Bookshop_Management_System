using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShop
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            User obj = new User();
            obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Documents\Bookshop.mdf;Integrated Security=True;Connect Timeout=30");
        private void DashBoard_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select sum(Quntity) from BookTbl",conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            bookStoklbl.Text = dataTable.Rows[0][0].ToString();
            SqlDataAdapter adapter1 = new SqlDataAdapter("select sum(Amount) from BillTbl", conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            Amountlbl.Text = dataTable1.Rows[0][0].ToString();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from UserTbl", conn);
            DataTable dataTable2 = new DataTable();
            sda.Fill(dataTable2);
            UListlbl.Text = dataTable2.Rows[0][0].ToString();
            conn.Close();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            User obj = new User();
            obj.Show();
            this.Hide();
        }

        private void logoutlbl_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
