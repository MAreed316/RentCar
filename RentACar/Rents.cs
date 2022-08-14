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
    public partial class Rents : Form
    {
        public String Uname;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9ISL201;Initial Catalog=dbRentACar;Integrated Security=True;Pooling=False");
        public Rents()
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
        public void fillCarRegNum()
        {
            con.Open();
            string query = "select RegNum from tblCars where Available = '"+"Yes"+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNum", typeof(string));
            dt.Load(rdr);
            cbCarRegNum.ValueMember = "RegNum";
            cbCarRegNum.DataSource = dt;
            con.Close();
        }

        public void fillCusCNIC()
        {
            con.Open();
            string query = "select CusCNIC from tblCustomers";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CusCNIC", typeof(string));
            dt.Load(rdr);
            cbCusCNIC.ValueMember = "CusCNIC";
            cbCusCNIC.DataSource = dt;
            con.Close();
        }

        public void fetchCustomers()
        {
            con.Open();
            string query = "select * from tblCustomers where CusCNIC = '"+cbCusCNIC.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                txtCusName.Text = dr["CusName"].ToString();
            }
            con.Close();
        }
        int Cost = 0;
        public void fetchCarRate()
        {
            con.Open();
            string query = "select * from tblCars where RegNum = '" + cbCarRegNum.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                //txtCusName.Text = dr["CusName"].ToString();
                Cost = Convert.ToInt32(dr["Price"].ToString());
            }
            con.Close();
        }
        public void calculate_fees()
        {
            DateTime d1 = timerRentRetDate.Value.Date;
            DateTime d2 = timerRentDate.Value.Date;
            TimeSpan t = d1 - d2;
            double Days = t.TotalDays;
            double fees = Days * Cost;
            txtRentFee.Text = Convert.ToString(fees);
        }
        private void UpdateOnRent()
        {
            con.Open();
            string query = "update tblCars set Available = '"+"No"+"' where RegNum = '" + cbCarRegNum.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        

        private void Rents_Load(object sender, EventArgs e)
        {
            lblloguser.Text = Uname;
            fillCarRegNum();
            fillCusCNIC();
            disp_data();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbCarRegNum_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCarRate();
        }

        private void cbCusId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCustomers();
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //txtrentid.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[0].Value.ToString();
            //cbCarRegNum.SelectedValue = bunifuCustomDataGrid1.SelectedRows[0].Cells[1].Value.ToString();
            //cbCusId.SelectedValue = bunifuCustomDataGrid1.SelectedRows[0].Cells[2].Value.ToString();
            txtCusName.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[3].Value.ToString();
            txtRentFee.Text = bunifuCustomDataGrid1.SelectedRows[0].Cells[6].Value.ToString();
        }
        public void clear_fields()
        {
            cbCarRegNum.SelectedIndex = -1;
            cbCusCNIC.SelectedIndex = -1;
            txtCusName.Clear();
            txtRentFee.Clear();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (cbCarRegNum.SelectedItem.ToString() == "" ||cbCusCNIC.SelectedItem.ToString() == "" || txtCusName.Text == "" || txtRentFee.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into tblRent values('" + cbCarRegNum.SelectedValue.ToString()+ "','" + cbCusCNIC.SelectedValue.ToString() + "','" + txtCusName.Text + "','" + timerRentDate.Value.Date + "','" + timerRentRetDate.Value.Date + "','" + txtRentFee.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Rented Successfully", "Message");
                    con.Close();
                    UpdateOnRent();
                    disp_data();
                    fillCarRegNum();
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

        private void button9_Click(object sender, EventArgs e)
        {
            /*if (txtRentId.Text == "")
            {
                MessageBox.Show("Missing Information", "Alert");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from tblRent where RentId = '" + txtRentId.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Is Remove From Rent", "Message");
                    con.Close();
                    //UpdateOnRentDelete();
                    disp_data();
                    txtRentId.Clear();
                    txtCusName.Clear();
                    txtRentFee.Clear();
                    cbCarRegNum.Text = "";
                    cbCusId.Text = "";
                }
                catch (Exception myex)
                {
                    MessageBox.Show(myex.Message);
                }
            }*/
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void timerRentRetDate_ValueChanged(object sender, EventArgs e)
        {
            calculate_fees();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Returns rt = new Returns();
            rt.Uname = lblloguser.Text;
            this.Hide();
            rt.Show();
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
    }
}
