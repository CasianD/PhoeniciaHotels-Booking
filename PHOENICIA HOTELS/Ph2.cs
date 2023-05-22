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
    public partial class Ph2 : MetroFramework.Forms.MetroForm
    {

        public string finalcity,City,PRICE,firstname,lastname;
        public int stars = 6, minbudget = 0, maxbudget = 200, sea = 0, city = 0, breakfast = 0, parking = 0, Nr;
        public string DAYS;
        public struct hotel
        {
            public int id, stars, reviews, parking, breakfast, seaview, cityview;
            public string name, description, city, picture;
        };
        hotel[] vector = new hotel[12];
        public Ph2(string Firstname,string Lastname)
        {
            InitializeComponent();
            metroLink1.Visible = false;
            firstname = Firstname;
            lastname = Lastname;
        }
        public void filters()
        {
            if(string.IsNullOrEmpty(metroComboBox1.Text.ToString())  == false)
            {
                if(metroComboBox1.Text == "All")
                {
                    stars = 6;
                }
                else
                    stars = Int32.Parse(metroComboBox1.Text.ToString());
            }
            if (checkBox1.Checked == true)
            {
                sea = 1;
            }
            if (checkBox2.Checked == true)
            {
                city = 1;
            }
            if (checkBox3.Checked == true)
            {
                breakfast = 1;
            }
            if (checkBox4.Checked == true)
            {
                parking = 1;
            }
            if (string.IsNullOrEmpty(trackBar1.Value.ToString()) == false)
            {
                minbudget = Int32.Parse(trackBar1.Value.ToString());
            }
            if (string.IsNullOrEmpty(trackBar1.Value.ToString()) == false)
            {
                maxbudget = Int32.Parse(trackBar2.Value.ToString());
            }
            if (string.IsNullOrEmpty(metroComboBox2.Text.ToString()) == false)
            {
                Selection_sort(metroComboBox2.Text);
            }
            DateTime pickerDate2 = metroDateTime2.Value;
            DateTime pickerDate1 = metroDateTime1.Value;

            TimeSpan tspan = pickerDate2 - pickerDate1;

            int differenceInDays = tspan.Days;

            DAYS = differenceInDays.ToString();

        }
        public bool pass(int index)
        {
            if (stars != 0 && stars !=6 && vector[index].stars != stars)
            {
                return false;
            }
            if (vector[index].city != City && vector[index].city != finalcity)
            {
                return false;
            }
            if (city != 0 && vector[index].cityview != city)
            {
                return false;
            }
            if (sea != 0 && vector[index].seaview != sea)
            {
                return false;
            }
            if (breakfast != 0 && vector[index].breakfast != breakfast)
            {
                return false;
            }
            if (parking != 0 && vector[index].parking != parking)
            {
                return false;
            }
            return true;

        }
        public bool valid()
        {
            if (metroDateTime2.Value <= metroDateTime1.Value)
                return false;
            if (metroDateTime1.Value <= DateTime.Today)
                return false;
            if (string.IsNullOrEmpty(metroTextBox1.Text)!=false || string.IsNullOrEmpty(metroTextBox2.Text) || string.IsNullOrEmpty(metroDateTime1.Value.ToString()) || string.IsNullOrEmpty(metroDateTime1.Value.ToString()))
            {
                return false;
            }
            if (trackBar1.Value > trackBar2.Value && string.IsNullOrEmpty(trackBar1.Value.ToString()) == false && string.IsNullOrEmpty(trackBar2.Value.ToString()) == false)
                return false;
            if (Int32.Parse(metroTextBox2.Text.ToString()) > 3)
                return false;
            return true;

        }
        public void readHotels()
        {
            ReadHotels reading = new ReadHotels();//citire date
            reading.PullData();
            Nr = reading.cnt;
            for (int i = 1; i <= reading.cnt; i++)//prelucrare
            {
                vector[i].id = reading.vector[i].id;
                vector[i].name = reading.vector[i].name;
                vector[i].description = reading.vector[i].description;
                vector[i].city = reading.vector[i].city;
                vector[i].stars = reading.vector[i].stars;
                vector[i].reviews = reading.vector[i].reviews;
                vector[i].parking = reading.vector[i].parking;
                vector[i].breakfast = reading.vector[i].breakfast;
                vector[i].seaview = reading.vector[i].seaview;
                vector[i].cityview = reading.vector[i].cityview;
                vector[i].picture = reading.vector[i].picture;
            }
        }
        public bool price_ok(int i)
        {
            SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
            string hotel = vector[i].name;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand com = new SqlCommand("SELECT MIN (Price) FROM roomtype WHERE hotel =@hotel " , conn);
            com.Parameters.AddWithValue("@hotel", hotel);
            PRICE = com.ExecuteScalar().ToString();
            if (string.IsNullOrEmpty(minimum.Text.ToString()) == false && string.IsNullOrEmpty(maximum.Text.ToString()) == false)
                if (Int32.Parse(PRICE) >= Int32.Parse(minimum.Text) && Int32.Parse(PRICE) <=Int32.Parse(maximum.Text))
                    return true;
                else
                    return false;
            return true;

        }
        public void Selection_sort(string C)//stars price rev
        {
            if (C == "Stars ASC")
            {
                for (int i = 1; i < Nr; i++)
                    for (int j = i + 1; j <= Nr; j++)
                    {
                        if (vector[i].stars > vector[j].stars)
                        {
                            hotel aux;
                            aux = vector[i];
                            vector[i] = vector[j];
                            vector[j] = aux;
                        }
                    }
            }
            else
                if (C == "Stars DESC")
            {
                for (int i = 1; i < Nr; i++)
                    for (int j = i + 1; j <= Nr; j++)
                    {
                        if (vector[i].stars < vector[j].stars)
                        {
                            hotel aux;
                            aux = vector[i];
                            vector[i] = vector[j];
                            vector[j] = aux;
                        }
                    }
            }
            else
                    if (C == "Price ASC")
            {
                SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
                int[] Prices = new int[15];
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                for (int i = 1; i <= Nr; i++)
                {
                    SqlCommand com = new SqlCommand("SELECT MIN(Price) FROM roomtype WHERE hotel =@nume ", conn);
                    com.Parameters.AddWithValue("@nume", vector[i].name);
                    Prices[i] = Int32.Parse(com.ExecuteScalar().ToString()) ;
                    
                }
                for (int i = 1; i < Nr; i++)
                    for (int j = i + 1; j <= Nr; j++)
                    {
                        if (Prices[i] > Prices[j])
                        {
                            int aux;
                            aux = Prices[i];
                            Prices[i] = Prices[j];
                            Prices[j] = aux;
                            hotel aux1;
                            aux1 = vector[i];
                            vector[i] = vector[j];
                            vector[j] = aux1;
                        }
                    }
            }
            else
                if (C == "Price DESC")
            {
                SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
                int[] Prices = new int[15];
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                for (int i = 1; i <= Nr; i++)
                {
                    SqlCommand com = new SqlCommand("SELECT MIN(Price) FROM roomtype WHERE hotel =@nume ", conn);
                    com.Parameters.AddWithValue("@nume", vector[i].name);
                    Prices[i] = Int32.Parse(com.ExecuteScalar().ToString());
                }
                for (int i = 1; i < Nr; i++)
                    for (int j = i + 1; j <= Nr; j++)
                    {
                        if (Prices[i] < Prices[j])
                        {
                            int aux;
                            aux = Prices[i];
                            Prices[i] = Prices[j];
                            Prices[j] = aux;
                            hotel aux1;
                            aux1 = vector[i];
                            vector[i] = vector[j];
                            vector[j] = aux1;
                        }
                    }

            }
            else
            {
                for (int i = 1; i < Nr; i++)
                    for (int j = i + 1; j <= Nr; j++)
                    {
                        if (vector[i].reviews < vector[j].reviews)
                        {
                            hotel aux1;
                            aux1 = vector[i];
                            vector[i] = vector[j];
                            vector[j] = aux1;
                        }
                    }

            }
        }
        public  void Output()
        {
            int x = 30, y = 30;
            for (int i = 1;i<=Nr;i++)
            {
                if (pass(i) && price_ok(i))
                  {
                    string imagine = vector[i].picture;
                    Hotel gb = new Hotel();
                    gb.Location = new Point(x, y);
                    groupBox1.Controls.Add(gb);
                    gb.metroButton1.Text = "";
                    gb.metroButton1.BackgroundImage = Image.FromFile(@"C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\BUTTON.jpg");
                    gb.metroButton1.BackgroundImageLayout = ImageLayout.Stretch;
                    gb.metroButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
                    gb.BackColor = Color.FromArgb(100,20,200,240);
                    gb.metroButton1.BackColor = Color.FromArgb(100, 20, 200, 240);
                    gb.Size = new Size(1100, 100);
                    gb.label1.Text = vector[i].name;
                    gb.pictureBox1.Image = Image.FromFile(imagine);
                    gb.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    gb.metroLink1.Text = vector[i].city;
                    y += 150;
                    gb.metroButton1.Click += (sender, args) =>
                    {
                        //string s = (sender as Button).Text;
                        //int val = Int32.Parse(s.ToString());
                        SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                        SqlCommand com = new SqlCommand("Select Id from Hotels  where Name = @hotell ", conn);
                        com.Parameters.AddWithValue("@hotell", gb.label1.Text.ToString());
                        int val = Int32.Parse(com.ExecuteScalar().ToString());
                        PH3 forma3;
                        if (string.IsNullOrEmpty(minimum.Text.ToString()) == false && string.IsNullOrEmpty(maximum.Text.ToString()) == false)
                            forma3 = new PH3(val, minimum.Text, maximum.Text, firstname, lastname, DAYS, metroDateTime1.Value, metroDateTime2.Value);
                        else
                            forma3 = new PH3(val, firstname, lastname, DAYS, metroDateTime1.Value, metroDateTime2.Value);
                        forma3.Show();


                    };
                }
            }
        }
        
        private void Ph2_Load(object sender, EventArgs e)
        {
            
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ///min budget
            trackBar1.Minimum = 50;
            trackBar1.Maximum = 200;
            minimum.Text = trackBar1.Value.ToString();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            //max budget
            trackBar2.Minimum = 50;
            trackBar2.Maximum = 200;
            maximum.Text = trackBar2.Value.ToString();
        }


        public void ClearPanels(GroupBox control)
        {
            foreach (Hotel childControl in control.Controls)
                childControl.ResetText();
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            //Apply
            
            stars = 6; minbudget = 0; maxbudget = 200; sea = 0; city = 0; breakfast = 0; parking = 0;
            readHotels();
            //MessageBox.Show("Corect");
            string city1 = metroTextBox1.Text;
            City = city1;
            Levenshtein edit_distance = new Levenshtein(city1, 3);
            if (edit_distance.finalcity != "Buzau" )
            {
                metroLink1.Visible = true;
                metroLabel12.Text = metroTextBox1.Text;
                finalcity = edit_distance.finalcity;
                metroLink1.Text = "Did you mean '" + finalcity + "' ?";
            }
            else
                finalcity = city1;
            if (valid())
            {
                ClearPanels(groupBox1);
                filters();
                Output();
            }
            else
                MessageBox.Show("Invalid");
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            metroLabel12.Text = finalcity;
            metroTextBox1.Text = finalcity;
            metroLink1.Visible = false;
        }
        private void metroLabel8_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel7_Click(object sender, EventArgs e)
        {

        }


        private void minimum_Click(object sender, EventArgs e)
        {

        }


        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void metroLabel12_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel10_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroLabel9_Click(object sender, EventArgs e)
        {

        }

        private void maximum_Click(object sender, EventArgs e)
        {

        }
    }
}
