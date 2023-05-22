using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PHOENICIA_HOTELS
{
    class Levenshtein
    {
        public int min(int x,int y)
        {
            if (x > y)
                return y;
            else
                return x;
        }
        public string finalcity = "Buzau";
        public  int  distance(string a,string b)
        {
            int n = a.Length;
            int m = b.Length;
            int[,] Matrix = new int[100, 100];
            for (int i = 0; i <= n; i++)
            {
                Matrix[i , 0] = i;
            }
            for (int i = 0; i <= m; i++)
            {
                Matrix[0, i] = i;
            }
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    if (a[i - 1] == b[j - 1])
                        Matrix[i,j] = Matrix[i - 1,j - 1];
                    else
                    {
                        Matrix[i,j] = min(min(Matrix[i,j - 1], Matrix[i - 1,j - 1]), Matrix[i - 1,j]) + 1;
                    }
                }
            return Matrix[n, m];
        }
        string text = File.ReadAllText("cities.txt");
        public Levenshtein(string city, int number)
        {
            
            string[] cities = text.Split(' ');
            foreach(string element in cities)
            {
                if(distance(city,element)<=number && distance(city, element) >0)
                {
                    finalcity = element;
                }
            }

        }
    }
}
