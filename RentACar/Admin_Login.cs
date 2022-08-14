using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentACar
{
    public partial class Admin_Login : Form
    {
        public Admin_Login()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAname.Text == "")
            {
                MessageBox.Show("Name Is Empty", "Alert");
            }
            else if (txtApass.Text == "")
            {
                MessageBox.Show("Password Is Empty", "Alert");
            }
            else if (txtAname.Text == "Admin" || txtAname.Text == "ADMIN" || txtAname.Text == "admin" || txtApass.Text == "Admin" || txtAname.Text == "ADMIN" || txtAname.Text == "admin")
            {
                Users us = new Users();
                //us.Uname = txtAname.Text;

                this.Hide();
                us.Show();
            }
                
            else
           {
                MessageBox.Show("Wrong UserName Or Password", "Alert");
           }
                
         }

        private void button2_Click(object sender, EventArgs e)
        {
            txtAname.Clear();
            txtApass.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            this.Hide();
            log.Show();
        }
    }
}
