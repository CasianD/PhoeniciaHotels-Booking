using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PHOENICIA_HOTELS
{
    public partial class MAP : MetroFramework.Forms.MetroForm
    {
        public int x = 1;
        public string Name;
        public MAP(string name)
        {
            InitializeComponent();
            Name = name;
            
        }

        private void MAP_Load(object sender, EventArgs e)
        {
            string []types = new string[] {"m","k","h","p","e" };
            string url = string.Format("http://maps.google.com/maps?t={0}&q=loc:{1}", types[x], Name);
            webBrowser1.Navigate(url);
        }
    }
}
