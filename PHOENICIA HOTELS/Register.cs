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
    public partial class Register : MetroFramework.Forms.MetroForm
    {
        public  bool valid()
        {
            if(metroToggle1.Checked == false)
            {
                return false;
            }
            if (metroTextBox1.Text == "A" || metroTextBox2.Text == "A" || metroTextBox3.Text == "A" || metroTextBox4.Text == "A" || metroTextBox5.Text == "A" || metroTextBox6.Text == "A" || metroTextBox6.Text == "A")
                return false;
            if (Password.Text != metroTextBox7.Text || Password.Text.Length < 8)
                return false;
            return true;
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
        public Register()
        {
            InitializeComponent();
           
            
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void metroLabel6_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel9_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //sign up
            
            string Fn = "A", Ln = "A", Cn = "A", Ad = "A", Em = "A", Ph = "A", Pass = "A";
            Fn = metroTextBox1.Text;
            Ln = metroTextBox2.Text;
            Cn = metroTextBox3.Text;
            Ad = metroTextBox4.Text;
            Em = metroTextBox5.Text;
            Ph = metroTextBox6.Text;
            Pass = metroTextBox7.Text;
            if (valid())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into ClientS Values('" + Fn + "','" + Ln + "','" + Cn + "','" + Ad + "','" + Em + "','" + Ph + "','" + Pass + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("New account created");
                con.Close();
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("Sign up failed");
            }

        }
    }
}
