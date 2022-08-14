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
using System.IO;
namespace RentACar
{
    public partial class Returns : Form
    {
        public String Uname;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9ISL201;Initial Catalog=dbRentACar;Integrated Security=True;Pooling=False");
        public Returns()
        {
            InitializeComponent();
        }
        public void disp_data()
        {
            con.Open();
            string query = "select * from tblRent";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            bunifuCustomDataGrid1.DataSource = ds.Tables[0];
            con.Close();
        }
        public void disp_data_Return()
        {
            con.Open();
            string query = "select * from tblReturn";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            bunifuCustomDataGrid2.DataSource = ds.Tables[0];
            con.Close();
        }
        private void delete_On_Return()
        {
            int rentid;
            rentid = Convert.ToInt32(bunifuCustomDataGrid1.SelectedRows[0].Cells[0].Value.ToString());
            con.Open();
            string query = "delete from tblRent where RentId = '" + rentid + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            //MessageBox.Show("Car Is Remove From Rent", "Message");
            con.Close();
            //UpdateOnRentDelete();
            disp_data();
        }
        private void UpdateOnReturn()
        {
            con.Open();
            string query = "update tblCars set Available = '" + "Yes" + "' where RegNum = '" + txtCarRegNum.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void txtRentId_TextChanged(object sender, EventArgs e)
        {

        }

        private void Returns_Load(object sender, EventArgs e)
        {
            lblloguser.Text = Uname;
            disp_data();
            disp_data_Return();
        }
        public void clear_fields()
        {
            txtCarRegNum.Clear();
            txtCusName.Clear();
            txtDelay.Clear();
            txtFine.Clear();
        }
        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCarRegNum.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[1].Value.ToString();
            txtCusName.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[3].Value.ToString();
            timerRentRetDate.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[5].Value.ToString();
            DateTime d1 = timerRentRetDate.Value.Date;
            DateTime d2 = DateTime.Now;
            TimeSpan t = d2 - d1;
            int NOD = Convert.ToInt32(t.TotalDays);
            if(NOD <= 0)
            {
                txtDelay.Text = "No Delay";
                txtFine.Text = "0";
            }
            else
            {
                txtDelay.Text = "" + NOD + " Days";
                txtFine.Text = "" + (NOD * 250);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txtCarRegNum.Text == "" || txtCusName.Text == "" || txtDelay.Text == "" || txtFine.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into tblReturn values('" + txtCarRegNum.Text + "','" + txtCusName.Text + "','" + timerRentRetDate.Text + "','" + txtDelay.Text + "','" + txtFine.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Dully Returned", "Message");
                    con.Close();
                    UpdateOnReturn();
                    disp_data_Return();
                    delete_On_Return();
                    clear_fields();

                }
                catch (Exception myex)
                {
                    MessageBox.Show(myex.Message);
                }
                /*String path = Directory.GetCurrentDirectory() + "\\Report.txt";
                if (File.Exists(path) == true)
                {
                    StreamWriter writer = new StreamWriter(path, append: true);
                    writer.WriteLine("Monthly Sales Report");
                    writer.WriteLine(txtname.Text + "\t" + txtfname.Text + "\t\t" + txtphone.Text);
                    writer.Close();
                    MessageBox.Show("Content Added Successfully");
                    Application.Exit();
                }
                else
                {
                    StreamWriter writer = new StreamWriter(path);
                    writer.WriteLine("Name\tFatherName\tPhone No");
                    writer.WriteLine("====\t==========\t========");
                    writer.WriteLine(txtname.Text + "\t" + txtfname.Text + "\t\t" + txtphone.Text);
                    writer.Close();
                    MessageBox.Show("Content Added Successfully");
                    Application.Exit();
                }*/
            }
        }

        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
            Login log = new Login();
            this.Hide();
            log.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DashBoard d = new DashBoard();
            d.Uname = lblloguser.Text;
            this.Hide();
            d.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Cars c = new Cars();
            c.Uname = lblloguser.Text;
            this.Hide();
            c.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Rents rc = new Rents();
            rc.Uname = lblloguser.Text;
            this.Hide();
            rc.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Customers cus = new Customers();
            cus.Uname = lblloguser.Text;
            this.Hide();
            cus.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lblloguser.Text == "Admin" || lblloguser.Text == "ADMIN" || lblloguser.Text == "admin")
            {
                Users us = new Users();
                us.Uname = lblloguser.Text;
                this.Hide();
                us.Show();
            }
            else
            {
                MessageBox.Show("You Cannot Access", "Alert");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Return Report", new Font("Times New Roman", 35, FontStyle.Bold), Brushes.Red, new Point(260));
            e.Graphics.DrawString("---------------------------------------------------", new Font("Times New Roman", 35, FontStyle.Bold), Brushes.Red, new Point(-1,30));
            e.Graphics.DrawString("Car Registration: " + bunifuCustomDataGrid2.SelectedRows[0].Cells[1].Value.ToString(), new Font("Times New Roman", 25, FontStyle.Bold), Brushes.Black, new Point(70,100));
            //e.Graphics.DrawString("Customer CNIC: " + bunifuCustomDataGrid1.SelectedRows[0].Cells[2].Value.ToString(), new Font("Times New Roman", 20, FontStyle.Bold), Brushes.Blue, new Point(70, 150));
            e.Graphics.DrawString("Customer Name: " + bunifuCustomDataGrid2.SelectedRows[0].Cells[2].Value.ToString(), new Font("Times New Roman", 25, FontStyle.Bold), Brushes.Black, new Point(70, 200));
            e.Graphics.DrawString("Return Date: " + bunifuCustomDataGrid2.SelectedRows[0].Cells[3].Value.ToString(), new Font("Times New Roman", 25, FontStyle.Bold), Brushes.Black, new Point(70, 250));
            e.Graphics.DrawString("Delay: " + bunifuCustomDataGrid2.SelectedRows[0].Cells[4].Value.ToString(), new Font("Times New Roman", 25, FontStyle.Bold), Brushes.Black, new Point(70, 300));
            e.Graphics.DrawString("Fine: " + bunifuCustomDataGrid2.SelectedRows[0].Cells[5].Value.ToString(), new Font("Times New Roman", 25, FontStyle.Bold), Brushes.Black, new Point(70, 350));
            e.Graphics.DrawString("---------------------------------------------------", new Font("Times New Roman", 35, FontStyle.Bold), Brushes.Red, new Point(-1,1010));
            e.Graphics.DrawString("Car Rental", new Font("Times New Roman", 35, FontStyle.Bold), Brushes.Red, new Point(280,1050));
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
    }
}
