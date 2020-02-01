using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pearson_Correlation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int users = 944, movies = 1683;
        int[,] ratings_array = new int[users, movies];

        private void Form1_Load(object sender, EventArgs e)
        {
            string FilePath = "data.txt";
            string[] lines = System.IO.File.ReadAllLines(FilePath);

            for (int i = 0; i < users; i++)
                for (int j = 0; j < movies; j++)
                    ratings_array[i, j] = 0;

            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');
                ratings_array[int.Parse(DataLine[0]), int.Parse(DataLine[1])] = int.Parse(DataLine[2]);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 38;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double max = 0;
            int iindex = 0, jindex = 0;


            double[,] users_corlation = new double[users, movies];

            for (int i = 0; i < users; i++)
                for (int j = 0; j < users; j++)
                    users_corlation[i, j] = 1.0;
                
            double xbar, ybar;
            double sumx = 0, sumy = 0;
            double upersum = 0, lower;
            double xi_xbar, yi_ybar;
            double sumxipowr2 = 0, sumyipowr2 = 0;
            double r;

            for (int i = 0; i < users; i++)
            {
                for (int j = i; j < users - 1; j++)
                {
                    for (int k = 0; k < movies; k++)
                    {
                        sumx += ratings_array[i, k];       //find sumation of x
                        sumy += ratings_array[j + 1, k];  //find sumation of y
                    }
                    if (sumx == 0 || sumy == 0)
                    {
                        users_corlation[i, j + 1] = 0;
                        users_corlation[j + 1, i] = 0;
                        sumx = 0;
                        sumy = 0;
                        continue;
                    }
                    xbar = sumx / movies;                  // find x = mean (x = xbar)
                    ybar = sumy / movies;                    // find y = mean (y = ybar)
                    for (int k = 0; k < movies; k++)
                    {
                        xi_xbar = ratings_array[i, k] - xbar;
                        yi_ybar = ratings_array[j + 1, k] - ybar;
                        upersum += xi_xbar * yi_ybar;

                        sumxipowr2 += Math.Pow(xi_xbar, 2);
                        sumyipowr2 += Math.Pow(yi_ybar, 2);
                    }
                    lower = Math.Sqrt(sumxipowr2 * sumyipowr2);
                    r = upersum / lower;
                    if (r < max)
                    {
                        max = r;
                        iindex = i;
                        jindex = j + 1;
                    }
                    users_corlation[i, j + 1] = r;
                    users_corlation[j + 1, i] = r;
                    sumx = 0;
                    sumy = 0;
                    upersum = 0;
                    sumxipowr2 = 0;
                    sumyipowr2 = 0;
                }
            }


            dataGridView1.ColumnCount = users;
            for (int i = 0; i < users; i++)
                dataGridView1.Columns[i].Name = "User " + i.ToString();


            string[] arr = new string[users];
            for (int i = 0; i < users; i++)
            {
                for (int j = 0; j < users; j++)
                    arr[j] = users_corlation[i, j].ToString();
                dataGridView1.Rows.Add(arr);
            }

            //richTextBox1.Text += "max" + max + "  i:" + iindex.ToString() + "  J;" + jindex.ToString();
            textBox1.Text = max.ToString();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = users;

            for (int i = 0; i < users; i++)
                dataGridView1.Columns[i].Name = "Movie " + i.ToString();

            string[] arr = new string[movies];
            for (int i = 0; i < users; i++)
            {
                for (int j = 0; j < movies; j++)
                    arr[j] = ratings_array[i, j].ToString();
                dataGridView1.Rows.Add(arr);
            }

        }
    }
}
