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
    public partial class PH3 : MetroFramework.Forms.MetroForm
    {
        public int Nr, n;
        public struct hotel
        {
            public int id, stars, reviews, parking, breakfast, seaview, cityview;
            public string name, description, city, picture;
        };
        hotel[] vector = new hotel[12];
        public string Name, Description, city,Firstname,Lastname;
        public int days;
        struct comment
        {
            public string firstname, lastname, country, hotel, text, date;
        };
        comment[] C = new comment[1000];
        struct rooms
        {
            public string hotel, type;
            public int nr, price;
        };
        struct order
        {
            public string hotel, type, firstname, lastname, checkin, checkout;
            public int price, howmany;
        };
        public int N = 0;
        public DateTime Checkin, Checkout;
        order[] O = new order[1000];
        rooms[] R = new rooms[1000];
        public int cnt = 0,Cnt = 0,minimum = 50,maximum = 200;
        public SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");

        public void PullDataC()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select  * from Comments", conn);
            SqlDataReader sda = cmd.ExecuteReader();
            while (sda.Read())
            {
                ++cnt;
                C[cnt].firstname = sda[0].ToString();
                C[cnt].lastname = sda[1].ToString();
                C[cnt].hotel = sda[2].ToString();
                C[cnt].text = sda[3].ToString();

            }
            cmd.Dispose();
            conn.Close();
        }
        public void PullDataR()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select  * from Roomtype", conn);
            SqlDataReader sda = cmd.ExecuteReader();
            while (sda.Read())
            {
                ++Cnt;
                R[Cnt].hotel = sda[0].ToString();
                R[Cnt].type = sda[1].ToString();
                R[Cnt].nr = Int32.Parse(sda[2].ToString());
                R[Cnt].price= Int32.Parse(sda[3].ToString());

            }
            cmd.Dispose();
            conn.Close();
        }
        public void Load_comments()
        {
            int x = 20, y = 20;
            bool impar = true;
            for (int i = 1; i <= cnt; i++)
            {
                if (C[i].hotel == Name)
                {
                    Comment com = new Comment();
                    metroTabPage3.Controls.Add(com);
                    com.richTextBox1.ReadOnly = true;
                    com.Location = new Point(x, y);
                    if (impar == false)
                    {
                        y += 200;
                        x -= 400;
                        impar = !impar;
                    }
                    else
                    {
                        x += 400;
                        impar = !impar;
                    }

                    com.label1.Text = C[i].firstname + ' ' + C[i].lastname;
                    com.richTextBox1.Text = C[i].text;
                }
            }
        }
        public string X, Y,P;
        public void Load_rooms()
        {
            int x = 20, y = 30;
            for(int i = 1;i<=Cnt;i++)
            {
                if(R[i].hotel == Name && R[i].price >=minimum && R[i].price<=maximum)
                {
                    Room room = new Room();
                    metroTabPage2.Controls.Add(room);
                    room.Location = new Point(x, y);
                    room.label1.Text = R[i].type + " " + "room";
                    y += 100;
                    SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                   SqlCommand com = new SqlCommand("Select SUM(Many) from Rooms where hotel = @hotell and Type = @type and (NOT(@checkinn>=Checkout or @checkoutc<= Checkin)) ", conn);
                    com.Parameters.AddWithValue("@hotell", Name);
                    com.Parameters.AddWithValue("@type", R[i].type);
                    com.Parameters.AddWithValue("@checkoutc", Checkout);//al comenzii
                    com.Parameters.AddWithValue("@checkinn", Checkin);
                    int xxx,camere;
                    bool camer = int.TryParse(com.ExecuteScalar().ToString(),out xxx);//cate sunt ocupate
                    if (camer)
                    {
                        camere = Int32.Parse(com.ExecuteScalar().ToString());
                    }
                    else
                        camere = 0;
                    com.Dispose();
                    com = new SqlCommand("Select Number from Roomtype where Hotel = @hotell and Type = @type ", conn);
                    com.Parameters.AddWithValue("@hotell", Name);
                    com.Parameters.AddWithValue("@type", R[i].type);
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    int result =  Int32.Parse(com.ExecuteScalar().ToString())-camere;
                    com.Dispose();
                    com = new SqlCommand("Select Price from Roomtype where Hotel = @hotell and Type = @type ", conn);
                    com.Parameters.AddWithValue("@hotell", Name);
                    com.Parameters.AddWithValue("@type", R[i].type);
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    int pret = Int32.Parse(com.ExecuteScalar().ToString());
                    com.Dispose();

                    for (int j = 1; j <= result; j++)
                    {
                        string a = j.ToString() + " " + (j * pret).ToString()+" "+"$";
                        room.metroComboBox1.Items.Add(a);
                        
                    }
                    room.metroComboBox1.DropDownClosed += (sender, args) =>
                    {
                        if (string.IsNullOrEmpty(room.metroComboBox1.Text) == false)
                        {
                            X = room.metroComboBox1.Text;
                            Y = ""; P = "";
                            bool l = false;
                            for (int k = 0; k + 2 < X.Length; k++)
                            {
                                if (l == false && X[k] != ' ')
                                    P += X[k];
                                if (X[k] == ' ')
                                {
                                    l = true;
                                }
                                else
                                    if (l == true)
                                {
                                    Y += X[k];
                                }
                            }
                            int val = Int32.Parse(Y.ToString());
                            string old = metroTextBox1.Text.ToString();
                            int p = Int32.Parse(P.ToString());
                            metroTextBox1.Text = metroTextBox1.Text.Replace(metroTextBox1.Text.ToString(), "");
                            int Old = Int32.Parse(old.ToString());
                            Old += val*days;
                            metroTextBox1.Text = Old.ToString();
                            room.metroComboBox1.Enabled = false;
                            O[++N].firstname = Firstname;
                            O[N].lastname = Lastname;
                            O[N].hotel = Name;
                            O[N].howmany = p;
                            string aa ="";
                            string qq =  room.label1.Text.ToString();
                            int kk = 0;
                            while(qq[kk]!=' ')
                            {
                                aa += qq[kk];
                                kk++;
                            }
                            O[N].type = aa.ToString();
                            O[N].price = val;
                            O[N].checkin = Checkin.ToString();
                            O[N].checkout = Checkout.ToString();
                            
                        }
                    };
                }
            }
        }
        public PH3(int  index,string min,string max,string firstname,string lastname,string DAYS, DateTime checkin, DateTime checkout)
        {
          
            InitializeComponent();
            Checkin = checkin;
            Checkout = checkout;
            days = Int32.Parse(DAYS);
            Firstname = firstname;
            Lastname = lastname;
            minimum = Int32.Parse(min);
            maximum = Int32.Parse(max);
            textBox2.Text = "TOTAL PRICE FOR :"+ days.ToString() + " DAYS";
            ReadHotels();
            identify(index);
            execute();
            metroLabel1.Text = Name;
            metroLink1.Text = city;
            richTextBox1.Text = Description;
            PullDataC();//com
            Load_comments();
            PullDataR();
            Load_rooms();
            //button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            richTextBox2.Visible = false;
        }
        public PH3(int index,string firstname,string lastname,string DAYS,DateTime checkin,DateTime checkout)
        {
            InitializeComponent();
            Checkin = checkin;
            Checkout = checkout;
            days = Int32.Parse(DAYS);
            Firstname = firstname;
            Lastname = lastname;
            textBox2.Text = "TOTAL PRICE FOR :" + days.ToString() + " DAYS";
            ReadHotels();
            identify(index);
            execute();
            metroLabel1.Text = Name;
            metroLink1.Text = city;
            richTextBox1.Text = Description;
            PullDataC();//com
            Load_comments();
            PullDataR();
            Load_rooms();
            //button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            richTextBox2.Visible = false;

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //submit
  
                conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into Comments  Values('" + Firstname + "','" + Lastname + "','" + Name + "', @tt)", conn);
                cmd.Parameters.AddWithValue("@tt", richTextBox2.Text);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                richTextBox2.Visible = false;
                SqlCommand comm = new SqlCommand("Update Hotels Set Reviews = Reviews+1 where Name = @hotell", conn);
                comm.Parameters.AddWithValue("@hotell", Name);
                comm.ExecuteNonQuery();
                comm.Dispose();
                MessageBox.Show("Comment added !");

        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            //discard
            button2.Visible = true;
            button3.Visible = false;
            button4.Visible = false;
            richTextBox2.Visible = false;
            richTextBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //add review
            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("Select count (firstname) from Rooms where Firstname =  '" + Firstname + "'and Lastname = '" + Lastname + "'and Hotel = '" + Name + "'", conn);
            int nr = Int32.Parse(cmd.ExecuteScalar().ToString());
            cmd.Dispose();
                if (nr != 0)
                {
                    cmd = new SqlCommand("Select Min(Checkout) from Rooms where Firstname = '" + Firstname + "'and Lastname = '" + Lastname + "'and Hotel = '" + Name + "'", conn);
                    DateTime after = DateTime.Parse(cmd.ExecuteScalar().ToString());
                    if (after <= DateTime.Now)
                    {
                        button2.Visible = false;
                        button3.Visible = true;
                        button4.Visible = true;
                        richTextBox2.Visible = true; /// PASTA MEDIE!!!
                    }
                }
                else
                {
                    MessageBox.Show("No booking for this Hotel available");
                }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void E_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ADD ORDER
            if (metroToggle1.Checked == true)
            {
                conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                for (int i = 1; i <= N; i++)
                {
                    SqlCommand cmd = new SqlCommand("Insert into Rooms Values('" + O[i].type + "','" + O[i].hotel + "','" + O[i].checkin + "','" + O[i].checkout + "','" + O[i].firstname + "','" + O[i].lastname + "','" + O[i].howmany + "','" + O[i].price + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                MessageBox.Show("Order completed successfully. Can't wait to see you at  '" + Checkin.ToString() + "'");
            }
            else
                MessageBox.Show("Error");
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        public void identify(int index)
        {
            for(int i = 1;i<=Nr;i++)
            {
                if(vector[i].id == index)
                {
                    n = i;
                    break;
                }
            }
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {
            //maps
            MAP mapa = new MAP(Name);
            mapa.Show();

        }

        private void execute()
        {
            Name = vector[n].name;
            Description = vector[n].description;
            city = vector[n].city;
        }
        public void ReadHotels()
        {
            ReadHotels reading = new ReadHotels();//citire date
            reading.PullData();
            Nr = reading.cnt;
            for (int i = 1; i <=Nr; i++)//prelucrare
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
        private void PH3_Load(object sender, EventArgs e)
        {

        }
    }
}
