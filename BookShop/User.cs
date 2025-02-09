using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShop
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Documents\Bookshop.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            conn.Open();
            string query = "select * from Usertbl";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sqlDataAdapter);
            var ds = new DataSet();
            sqlDataAdapter.Fill(ds);
            UserGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (usertx.Text == "" || addresstx.Text == "" || numbertx.Text == "" || paswordtx.Text == "")
            {
                MessageBox.Show("Invalid Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "Insert into Usertbl values('" + usertx.Text + "','" + addresstx.Text + "','" + numbertx.Text + "','" + paswordtx.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved Suiccessfully");
                    conn.Close();
                    populate();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void reset()
        {
            usertx.Text = "";
            addresstx.Text = "";
            numbertx.Text = "";
            paswordtx.Text = "";
        }
        private void resetbtn_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Invalid Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "delete from UserTbl where U_ID =  " + key + ";";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Delete Successfully");
                    conn.Close();
                    populate();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        int key = 0;
        private void UserGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            usertx.Text = UserGV.SelectedRows[0].Cells[1].Value.ToString();
            addresstx.Text = UserGV.SelectedRows[0].Cells[2].Value.ToString();
            numbertx.Text = UserGV.SelectedRows[0].Cells[3].Value.ToString();
            paswordtx.Text = UserGV.SelectedRows[0].Cells[4].Value.ToString();
            if (usertx.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Savebtn_Click_1(object sender, EventArgs e)
        {
            if (usertx.Text == "" || addresstx.Text == "" || numbertx.Text == "" || paswordtx.Text == "" )
            {
                MessageBox.Show("Invalid Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "Insert into UserTbl values('" + usertx.Text + "','" + addresstx.Text + "','" + numbertx.Text + "','" + paswordtx.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved Suiccessfully");
                    conn.Close();
                    populate();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (usertx.Text == "" || addresstx.Text == "" || numbertx.Text == "" || paswordtx.Text == "" )
            {
                MessageBox.Show("Invalid Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "update UserTbl set U_Name = '" + usertx.Text + "',U_PNO = '" + numbertx.Text + "',U_Address ='" + addresstx.Text + "',U_Password = '" + paswordtx.Text + "'where U_Id = " + key + ";";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Update Successfully");
                    conn.Close();
                    populate();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

        private void label9_Click(object sender, EventArgs e)
        {
            DashBoard obj = new DashBoard();
            obj.Show();
            this.Hide();
        }
    }
}
