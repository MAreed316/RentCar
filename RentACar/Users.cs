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
    public partial class Users : Form
    {
        
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9ISL201;Initial Catalog=dbRentACar;Integrated Security=True;Pooling=False");
        public String Uname;
        public Users()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void disp_data()
        {
            con.Open();
            string query = "select * from tblUsers";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            bunifuCustomDataGrid1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(txtUname.Text == "" || txtUpass.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into tblUsers values('"+txtUname.Text+"','"+txtUpass.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Inserted Successfully","Message");
                    con.Close();
                    disp_data();
                    txtUname.Clear();
                    txtUpass.Clear();
                }
                catch(Exception myex)
                {
                    MessageBox.Show(myex.Message);
                }
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            lblloguser.Text = Uname;
            disp_data();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (txtUname.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from tblUsers where Uname = '" + txtUname.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully", "Message");
                    con.Close();
                    disp_data();
                    txtUname.Clear();
                    txtUpass.Clear();
                }
                catch (Exception myex)
                {
                    MessageBox.Show(myex.Message);
                }
            }
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtUname.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[1].Value.ToString();
            txtUpass.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtUname.Text == "" || txtUpass.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update tblUsers set Uname = '" + txtUname.Text + "', Upass = '" + txtUpass.Text + "' where Uname = '" + txtUname.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully", "Message");
                    con.Close();
                    disp_data();
                    txtUname.Clear();
                    txtUpass.Clear();
                }
                catch (Exception myex)
                {
                    MessageBox.Show(myex.Message);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txtUname.Clear();
            txtUpass.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DashBoard d = new DashBoard();
            d.Uname = lblloguser.Text;
            this.Hide();
            d.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Cars c = new Cars();
            c.Uname = lblloguser.Text;
            this.Hide();
            c.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Rents rc = new Rents();
            rc.Uname = lblloguser.Text;
            this.Hide();
            rc.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Returns rt = new Returns();
            rt.Uname = lblloguser.Text;
            this.Hide();
            rt.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Customers cus = new Customers();
            cus.Uname = lblloguser.Text;
            this.Hide();
            cus.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            /*if (lblloguser.Text == "admin" || lblloguser.Text == "Admin" || lblloguser.Text == "ADMIN")
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

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void lblloguser_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblloguser_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
