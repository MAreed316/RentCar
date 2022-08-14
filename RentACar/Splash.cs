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
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        int startpoint = -1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            startpoint += 1;
            label3.Text = "" + startpoint;
            if(progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                timer1.Stop();
                Login log = new Login();
                log.Show();
                this.Hide();
                timer1.Enabled = false;
            }
        }
    }
}
