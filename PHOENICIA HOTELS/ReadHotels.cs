using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PHOENICIA_HOTELS
{
    
     class ReadHotels
    {
        public string s;
        public struct hotel
        {
             public int id,stars,reviews,parking,breakfast,seaview,cityview;
            public string name, description, city, picture;
        };
        public hotel[] vector = new hotel[15];
        public int cnt;
        public SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Casian\Desktop\PHOENICIA HOTELS\bin\Debug\PHotel.mdf; Integrated Security = True; Connect Timeout = 30");
        public void PullData()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select  * from Hotels", conn);
            SqlDataReader sda = cmd.ExecuteReader();
            while(sda.Read())//id name description city stars review parking breakfast sea cityview picture
            {
                ++cnt;
                vector[cnt].id = Int32.Parse(sda[0].ToString());
                vector[cnt].name = sda[1].ToString();
                vector[cnt].description=sda[2].ToString();
                vector[cnt].city = sda[3].ToString();
                vector[cnt].stars = Int32.Parse(sda[4].ToString());
                vector[cnt].reviews = Int32.Parse(sda[5].ToString());
                vector[cnt].parking = Int32.Parse(sda[6].ToString());
                vector[cnt].breakfast = Int32.Parse(sda[7].ToString());
                vector[cnt].seaview = Int32.Parse(sda[8].ToString());
                vector[cnt].cityview = Int32.Parse(sda[9].ToString());
                vector[cnt].picture= sda[10].ToString();

            }
            cmd.Dispose();
            conn.Close();
        }

    }
}
