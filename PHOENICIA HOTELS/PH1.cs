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

namespace PHOENICIA_HOTELS
{
    
    public partial class PH1 : MetroFramework.Forms.MetroForm
    {
        public string Firstname, Lastname;
        public PH1()
        {
            InitializeComponent();
            metroButton1.BackColor = Color.DarkRed;
            metroButton2.BackColor = Color.LightBlue;
        }
        public void delete_employee()
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            button1.Visible = false;
            metroTextBox1.Visible = false;
            metroTextBox2.Visible = false;
            metroTextBox3.Visible = false;
        }
        public void delete_client()
        {
            label5.Visible = false;
            label6.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            button2.Visible = false;
            metroTextBox4.Visible = false;
            metroTextBox5.Visible = false;
            metroTextBox6.Visible = false;
            linkLabel1.Visible = false;
        }
        //////////
        public void undelete_employee()
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            button1.Visible = true;
            metroTextBox1.Visible = true;
            metroTextBox2.Visible = true;
            metroTextBox3.Visible = true;
        }
        public void undelete_client()
        {
            label5.Visible = true;
            label6.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            button2.Visible = true;
            metroTextBox4.Visible = true;
            metroTextBox5.Visible = true;
            metroTextBox6.Visible = true;
            linkLabel1.Visible = true;
        }
        public void blur1()
        {
            pictureBox1.Image = Image.FromFile(@"C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\hotel1.jpg");
        }
        public void blur2()
        {
            pictureBox2.Image = Image.FromFile(@"C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\hotel2.jpg");

        }
        public void unblur1()
        {
            pictureBox1.Image = Image.FromFile(@"C:\Users\Casian\Desktop\PHOENICIA HOTELS\271916022.jpg");
        }
        public void unblur2()
        {
            pictureBox2.Image = Image.FromFile(@"C:\Users\Casian\Desktop\PHOENICIA HOTELS\31874308.jpg");

        }
        private void PH1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(@"C:\Users\Casian\Desktop\PHOENICIA HOTELS\271916022.jpg");
            pictureBox2.Image = Image.FromFile(@"C:\Users\Casian\Desktop\PHOENICIA HOTELS\31874308.jpg");
            metroButton1.BackColor = Color.DarkRed;
            metroButton2.BackColor = Color.LightBlue;
            delete_client();
            delete_employee();
            
        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //employee
            undelete_employee();
            if (metroTextBox6.Visible )
            {
                delete_client();
                unblur2();
            }
            blur1();
            MessageBox.Show("This option is not ready yet :)");
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //Client
            blur2();
            undelete_client();
            if (label1.Visible )
            {
                unblur1();
                delete_employee();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //label5.Visible = false;
            //label6.Visible = false;
            //label8.Visible = false;
            //label9.Visible = false;
            //button2.Visible = false;
            //metroTextBox4.Visible = false;
            //metroTextBox5.Visible = false;
            //metroTextBox6.Visible = false;
            Register inregistrare = new Register();
            inregistrare.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //employee login
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
            con.Open();
            SqlCommand com = new SqlCommand("Select * from Employee where Firstname =  '" + metroTextBox1.Text + "'  and Lastname =    '" + metroTextBox2.Text + "'  and Password =  '" + metroTextBox3.Text + "'  ",con);
            SqlDataReader dr = com.ExecuteReader();
            
            if(dr.Read())
            {
                MessageBox.Show("Esti in cont!");
            }
            else
            {
                string s = "Error";
                MessageBox.Show(s);
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //client login
            //pass 4 last 5 first 6
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
            con.Open();
            SqlCommand com = new SqlCommand("Select *from Clients where Firstname =  '" + metroTextBox6.Text + "'  and Lastname =    '" + metroTextBox5.Text + "'  and Password =  '" + metroTextBox4.Text + "'  ", con);
            SqlDataReader dr = com.ExecuteReader();

            if (dr.Read())
            {
                Firstname = metroTextBox6.Text;
                Lastname = metroTextBox5.Text;
                Ph2 forma = new Ph2(Firstname,Lastname);
                forma.Show();
            }
            else
            {
                string s = "Error";
                MessageBox.Show(s);
            }
            con.Close();
        }
    }
}
