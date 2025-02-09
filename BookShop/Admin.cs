using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShop
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {

        }

        private void cancellbl_Click(object sender, EventArgs e)
        {
            Login  obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void Loginbtn_Click_1(object sender, EventArgs e)
        {
            if (passtb.Text == "Pasword")
            {
                Books obj = new Books();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect Password");
            }
        }
    }
}
