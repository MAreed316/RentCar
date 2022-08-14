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
    public partial class Customers : Form
    {
        public String Uname;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9ISL201;Initial Catalog=dbRentACar;Integrated Security=True;Pooling=False");
        public Customers()
        {
            InitializeComponent();
        }
        public void disp_data()
        {
            //CusName,CusAddress,CusPhone
            con.Open();
            string query = "select * from tblCustomers";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            bunifuCustomDataGrid1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void clear_fields()
        {
            txtCusCNIC.Clear();
            txtCusName.Clear();
            txtCusAddress.Clear();
            txtCusPhone.Clear();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (txtCusCNIC.Text == "" || txtCusName.Text == "" || txtCusAddress.Text == "" || txtCusPhone.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            else
            {
                try
                {
                    con.Open();
                    //int cuscnic = Convert.ToInt32(txtCusCNIC.Text);
                    string query = "insert into tblCustomers values('" + txtCusCNIC.Text + "','" + txtCusName.Text + "','" + txtCusAddress.Text + "','" + txtCusPhone.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Inserted Successfully", "Message");
                    con.Close();
                    disp_data();
                    clear_fields();


                }
                catch (Exception myex)
                {
                    MessageBox.Show(myex.Message);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (txtCusCNIC.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from tblCustomers where CusCNIC = '" + txtCusCNIC.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted Successfully", "Message");
                    con.Close();
                    disp_data();
                    clear_fields();
                }
                catch (Exception myex)
                {
                    MessageBox.Show(myex.Message);
                }
            }
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCusCNIC.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[0].Value.ToString();
            txtCusName.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[1].Value.ToString();
            txtCusAddress.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[2].Value.ToString();
            txtCusPhone.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[3].Value.ToString();

            /*if(txtCusName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(bunifuCustomDataGrid1.SelectedRows[0].Cells[0].Value.ToString());
            }*/
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtCusCNIC.Text == "" || txtCusName.Text == "" || txtCusAddress.Text == "" || txtCusPhone.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update tblCustomers set CusCNIC = '" + txtCusCNIC.Text + "', CusName = '" + txtCusName.Text + "', CusAddress = '" + txtCusAddress.Text + "', CusPhone = '" + txtCusPhone.Text+"' where CusCNIC = '" + txtCusCNIC.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated Successfully", "Message");
                    con.Close();
                    disp_data();
                    clear_fields();
                }
                catch (Exception myex)
                {
                    MessageBox.Show(myex.Message);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            clear_fields();
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            lblloguser.Text = Uname;
            disp_data();
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           
            Login log = new Login();
            this.Hide();
            log.Show();
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

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            DashBoard d = new DashBoard();
            d.Uname = lblloguser.Text;
            this.Hide();
            d.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Returns rt = new Returns();
            rt.Uname = lblloguser.Text;
            this.Hide();
            rt.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Rents rc = new Rents();
            rc.Uname = lblloguser.Text;
            this.Hide();
            rc.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Cars c = new Cars();
            c.Uname = lblloguser.Text;
            this.Hide();
            c.Show();
        }

        private void lblloguser_Click(object sender, EventArgs e)
        {

        }
    }
}
