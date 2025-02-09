using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShop
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }

        private void label5_Click(object sender, EventArgs e)
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
            string query = "select * from Booktbl";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sqlDataAdapter);
            var ds = new DataSet();
            sqlDataAdapter.Fill(ds);
            BookGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void UpdateBooks()
        {
            int Qty = stock - Convert.ToInt32(Quantity.Text);
            try
            {
                conn.Open();
                string query = "update BookTbl set Quntity = " + Qty + " where B_Id = " + key + ";";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Book Update Successfully");
                conn.Close();
                populate();
                //reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int x = 0 , Ctotal = 0 ;
        private void Savebtn_Click(object sender, EventArgs e)
        {
            
            if (Quantity.Text == "" || Convert.ToInt32(Quantity.Text) > stock)
            {
                MessageBox.Show("Not Enough Stock");
            }
            else
            {
                int total = Convert.ToInt32(Quantity.Text) * Convert.ToInt32(Price.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDVG);
                newRow.Cells[0].Value = x + 1;
                newRow.Cells[1].Value = BTitle.Text;
                newRow.Cells[2].Value = Quantity.Text;
                newRow.Cells[3].Value = Price.Text;
                newRow.Cells[4].Value = total;
                BillDVG.Rows.Add(newRow);
                x++;
                UpdateBooks();
                Ctotal = Ctotal + total;
                totallbl.Text = "Rs" +"."+ Ctotal;


            }
        }
        int key = 0, stock = 0;
        private void BookGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitle.Text = BookGV.SelectedRows[0].Cells[1].Value.ToString();
            //Quantity.Text = BookGV.SelectedRows[0].Cells[4].Value.ToString();
            Price.Text = BookGV.SelectedRows[0].Cells[5].Value.ToString();
            if (BTitle.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(BookGV.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(BookGV.SelectedRows[0].Cells[4].Value.ToString());
            }
        }

        int BookId, Book_Quntity, Book_Price, Total, Pos = 60;

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj =new Login();
            obj.Show();
            this.Hide();

        }

        private void Billing_Load(object sender, EventArgs e)
        {
            userlbl.Text = Login.UserName;
        }

        string Book_Name;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Book Shop", new Font("Century Gothic", 12 , FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID PRODUCT PRICE QUNTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridViewRow row in BillDVG.Rows)
            {
                BookId = Convert.ToInt32(row.Cells["Column1"].Value);
                Book_Name = "" + row.Cells["Column2"].Value;
                Book_Quntity = Convert.ToInt32(row.Cells["Column3"].Value);
                Book_Price = Convert.ToInt32(row.Cells["Column4"].Value);
                Total = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + BookId, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(26, Pos));
                e.Graphics.DrawString("" + Book_Name, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(45, Pos));
                e.Graphics.DrawString("" + Book_Quntity, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(170, Pos));
                e.Graphics.DrawString("" + Book_Price, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(120, Pos));
                e.Graphics.DrawString("" + Total, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(235, Pos));
                Pos = Pos + 20;

            }
            e.Graphics.DrawString("Total : Rs. " + Ctotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, Pos + 50));
            e.Graphics.DrawString("----------Book Shop----------", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(40, Pos + 895));
            BillDVG.Rows.Clear();
            BillDVG.Refresh();
            Pos = 100;
        }

        private void printbtn_Click(object sender, EventArgs e)
        {
            
            if (CName.Text == "" || BTitle.Text == "") 
            {
                MessageBox.Show("Select Customer Name");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "Insert into BillTbl values('" + userlbl.Text + "','" + CName.Text + "','" + Ctotal + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved Successfully");
                    conn.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
               

            }
        }

        private void reset()
        {
            BTitle.Text = "";
            Quantity.Text = "";
            Price.Text = "";
            CName.Text = "";
        }
        private void Editbtn_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
