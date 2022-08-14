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
    public partial class Cars : Form
    {
        public String Uname;

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9ISL201;Initial Catalog=dbRentACar;Integrated Security=True;Pooling=False");
        public Cars()
        {
            InitializeComponent();
        }
        
        private void txtUname_TextChanged(object sender, EventArgs e)
        {

        }
        public void disp_data()
        {
            con.Open();
            string query = "select * from tblCars";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            bunifuCustomDataGrid1.DataSource = ds.Tables[0];
            con.Close();
        }
        public void clear_fields()
        {
            txtRegNum.Clear();
            cbBrand.SelectedIndex = -1;
            txtModel.Clear();
            txtPrice.Clear();
            txtColor.Clear();
            cbAvailable.SelectedIndex = -1;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (txtRegNum.Text == "" || cbBrand.SelectedItem.ToString() == "" || txtModel.Text == "" || txtPrice.Text == "" || cbAvailable.SelectedItem.ToString() == "" || txtColor.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into tblCars values('" + txtRegNum.Text + "','" + cbBrand.SelectedItem.ToString() + "','" + txtModel.Text + "','" + txtPrice.Text + "','" + txtColor.Text + "','" + cbAvailable.SelectedItem.ToString()+ "')";
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

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Cars_Load(object sender, EventArgs e)
        {
            lblloguser.Text = Uname;
            disp_data();
        }
        
        private void button9_Click(object sender, EventArgs e)
        {
            if (txtRegNum.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from tblCars where RegNum = '" + txtRegNum.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Deleted Successfully", "Message");
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
            txtRegNum.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[1].Value.ToString();
            cbBrand.SelectedItem = bunifuCustomDataGrid1.SelectedRows[0].Cells[2].Value.ToString();
            txtModel.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[3].Value.ToString();
            txtPrice.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[4].Value.ToString();
            txtColor.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[5].Value.ToString();
            cbAvailable.SelectedItem = bunifuCustomDataGrid1.SelectedRows[0].Cells[6].Value.ToString();

            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtRegNum.Text == "" || cbBrand.SelectedItem.ToString() == "" || txtModel.Text == "" || txtPrice.Text == "" || cbAvailable.SelectedItem.ToString() == "" || txtColor.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            
            else
            {
                try
                {
                    con.Open();
                    //string query = "update tblCars set  RegNum ='" + txtRegNum.Text + "',Brand = '" + cbBrand.SelectedItem.ToString() + "', Model = '" + txtModel.Text + "', Price = '" + txtPrice.Text + "', Color = '" + txtColor.Text + "', Available = '" + cbAvailable.SelectedItem.ToString() + "' where CarId = '" + key + "'";
                    string query = "update tblCars set Brand = '" + cbBrand.SelectedItem.ToString() + "', Model = '" + txtModel.Text + "', Price = '" + txtPrice.Text + "', Color = '" + txtColor.Text + "', Available = '" + cbAvailable.SelectedItem.ToString() + "' where RegNum = '" + txtRegNum.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Updated Successfully", "Message");
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

        private void button11_Click(object sender, EventArgs e)
        {
            cbRefreshCar.Text = "";
            disp_data();
        }

        private void cbRefreshCar_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string flag = "";
            if(cbRefreshCar.SelectedItem.ToString()=="Available")
            {
                flag = "Yes";
            }
            else
            {
                flag = "No";
            }
            con.Open();
            string query = "select * from tblCars where Available = '"+flag+"'";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            bunifuCustomDataGrid1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            Login log = new Login();
            this.Hide();
            log.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DashBoard d = new DashBoard();
            d.Uname = lblloguser.Text;
            this.Hide();
            d.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Customers cus = new Customers();
            cus.Uname = lblloguser.Text;
            this.Hide();
            cus.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Returns rt = new Returns();
            rt.Uname = lblloguser.Text;
            this.Hide();
            rt.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Rents rc = new Rents();
            rc.Uname = lblloguser.Text;
            this.Hide();
            rc.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void cbRefreshCar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
