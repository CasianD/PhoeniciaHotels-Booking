using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PHOENICIA_HOTELS
{
    public partial class PH : MetroFramework.Forms.MetroForm
    {
        private int time;
        public PH()
        {

            InitializeComponent();
            metroProgressBar1.Maximum = 100;
            metroProgressBar1.Minimum = 0;
            metroProgressBar1.Step = 20;
            metroProgressBar1.Value = 0;
            timer1.Interval = 1000;
            metroLabel1.Text = "Powered by Casian Drugea";
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            time += 2;
            metroProgressBar1.PerformStep();
            if (metroProgressBar1.Value == 60)
            {
                metroLabel1.Text = "Welcome and  have a nice day";
               
            }
            if(metroProgressBar1.Value == metroProgressBar1.Maximum)
            {
                timer1.Stop();
                metroProgressBar1.Enabled = false;
                PH1 forma =new PH1();    
                forma.Show();
                this.Hide();

            }

        }
    }
}
