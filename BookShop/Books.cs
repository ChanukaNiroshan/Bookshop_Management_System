using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BookShop
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
            populate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void Books_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Documents\Bookshop.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            conn.Open();
            string query = "select * from Booktbl";
            SqlDataAdapter Adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(Adapter);
            var ds = new DataSet();
            Adapter.Fill(ds);
            BookGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void Filter()
        {
            conn.Open();
            string query = "select * from Booktbl where B_Type = '"+BlistTb.SelectedItem.ToString()+"'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sqlDataAdapter);
            var ds = new DataSet();
            sqlDataAdapter.Fill(ds);
            BookGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (BTitle.Text == "" || BAuthor.Text == "" || Quantity.Text == "" || Price.Text == "" || Btype.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "Insert into Booktbl values('" + BTitle.Text + "','" + BAuthor.Text + "','" + Btype.SelectedItem.ToString() + "','" + Quantity.Text + "','" + Price.Text + "')";
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int key = 0;
        private void BookGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            BTitle.Text = BookGV.SelectedRows[0].Cells[1].Value.ToString();
            BAuthor.Text = BookGV.SelectedRows[0].Cells[2].Value.ToString();
            Btype.SelectedItem = BookGV.SelectedRows[0].Cells[3].Value.ToString();
            Quantity.Text = BookGV.SelectedRows[0].Cells[4].Value.ToString();
            Price.Text = BookGV.SelectedRows[0].Cells[5].Value.ToString();
            if(BTitle.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BookGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }
        private void Btype_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        private void BlistTb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void refeshbtn_Click(object sender, EventArgs e)
        {
            populate();
            BlistTb.SelectedIndex = -1;
        }
        
        private void reset()
        {
            BTitle.Text = "";
            BAuthor.Text = "";
            Btype.SelectedItem = null;
            Quantity.Text = "";
            Price.Text = "";
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
                    string query = "delete from Booktbl where B_Id =  " + key + ";";
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

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (BTitle.Text == "" || BAuthor.Text == "" || Quantity.Text == "" || Price.Text == "" || Btype.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "update BookTbl set B_Name = '" + BTitle.Text + "',B_Author = '" + BAuthor.Text + "',B_Type ='" + Btype.SelectedItem.ToString() + "',Quntity = '" + Quantity.Text + "',Price = '" + Price.Text + "' where B_Id = " + key + ";";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Update Successfully");
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

        private void label8_Click(object sender, EventArgs e)
        {
            User obj = new User();
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
