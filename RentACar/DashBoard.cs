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
namespace RentACar
{
    public partial class DashBoard : Form
    {
        
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9ISL201;Initial Catalog=dbRentACar;Integrated Security=True;Pooling=False");
        public String Uname;
        public DashBoard()
        {
            
            InitializeComponent();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            lblloguser.Text = Uname;

            string querycar = "select Count(*) from tblCars where Available = '" + "Yes" + "'";
            SqlDataAdapter sdacar = new SqlDataAdapter(querycar,con);
            DataTable dtcar = new DataTable();
            sdacar.Fill(dtcar);
            lblAvailableCars.Text = dtcar.Rows[0][0].ToString();

            string queryrcar = "select Count(*) from tblCars where Available = '" + "No" + "'";
            SqlDataAdapter sdarcar = new SqlDataAdapter(queryrcar, con);
            DataTable dtrcar = new DataTable();
            sdarcar.Fill(dtrcar);
            lblRentedCars.Text = dtrcar.Rows[0][0].ToString();

            string querycustomer = "select Count(*) from tblCustomers";
            SqlDataAdapter sdacus = new SqlDataAdapter(querycustomer, con);
            DataTable dtcus = new DataTable();
            sdacus.Fill(dtcus);
            lblCustomers.Text = dtcus.Rows[0][0].ToString();

            string queryuser = "select Count(*) from tblUsers";
            SqlDataAdapter sdauser = new SqlDataAdapter(queryuser, con);
            DataTable dtuser = new DataTable();
            sdauser.Fill(dtuser);
            lblUsers.Text = dtuser.Rows[0][0].ToString();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            /*if(lblloguser.Text == "admin" || lblloguser.Text == "Admin" || lblloguser.Text == "ADMIN")
            {
                Admin_Login Alog = new Admin_Login();
                this.Hide();
                Alog.Show();
            }
            else
            {
                Login log = new Login();
                this.Hide();
                log.Show();
            }*/
            Login log = new Login();
            this.Hide();
            log.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Cars c = new Cars();
            c.Uname = lblloguser.Text;
            this.Hide();
            c.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Rents rc = new Rents();
            rc.Uname = lblloguser.Text;
            this.Hide();
            rc.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Returns rt = new Returns();
            rt.Uname = lblloguser.Text;
            this.Hide();
            rt.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Customers cus = new Customers();
            cus.Uname = lblloguser.Text;
            this.Hide();
            cus.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(lblloguser.Text == "Admin" || lblloguser.Text == "ADMIN" || lblloguser.Text == "admin")
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

        private void lblCars_Click(object sender, EventArgs e)
        {

        }
    }
}
