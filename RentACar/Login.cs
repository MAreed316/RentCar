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
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9ISL201;Initial Catalog=dbRentACar;Integrated Security=True;Pooling=False");
        public Login()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUname.Clear();
            txtUpass.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUname.Text == "")
            {
                MessageBox.Show("User Name Is Empty", "Alert");
            }
            else if (txtUpass.Text == "")
            {
                MessageBox.Show("Password Is Empty", "Alert");
            }
            else
            {
                con.Open();
                string query = "select Count(*) from tblUsers where Uname = '" + txtUname.Text + "' and Upass = '" + txtUpass.Text + "' ";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    DashBoard dashboard = new DashBoard();
                    dashboard.Uname = txtUname.Text;
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong UserName Or Password", "Alert");
                }
                con.Close();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*Admin_Login ALog = new Admin_Login();
            this.Hide();
            ALog.Show();*/
            
        }
    }
}
