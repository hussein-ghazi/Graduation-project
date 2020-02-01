using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            string[] arr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                arr[i] = i.ToString();
            }
            dataGridView1.ColumnCount = 10;
            dataGridView1.Rows.Add(arr);
            */
            // dataGrid1.DataSource = new ArrayDataView(array);
            //File Path --> \Reading from txt file\Reading from txt file\bin\Debug
            string FilePath = "data.txt";
            string[] lines = System.IO.File.ReadAllLines(FilePath);


            //Number of Users: 943
            //Number of Movies: 1682
            // x,y are the RatingArray dimations
            int x = 944, y = 1683;
            int[,] RatingArray = new int[x, y];

            //inithialize RatingArray with zeros
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    RatingArray[i, j] = 0;

            //fetch ratings from file
            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');
                RatingArray[int.Parse(DataLine[0]), int.Parse(DataLine[1])] = int.Parse(DataLine[2]);
            }
            /////////////////////////////////////////////////////

            /////////////////////////////////////////////////// delete this line to get all users 
            double max = 0;
            int iindex=0, jindex=0;


            double[,] users_corlation = new double[x, x];
            //tow loops to visit users_corlation cells
            for (int i = 0; i < x; i++)
                for (int j = 0; j < x; j++)
                {
                    users_corlation[i, j] = 1.0;
                }
            /*
            int t = 1;
            for (int i = 0; i < x; i++)
                for (int j = i; j < x-1; j++)
                {
                    users_corlation[i, j+1] = i.ToString()+"__"+(j+1).ToString();
                    users_corlation[j + 1, i] = "xxx";
                    t++;
                }
           */
            int U = x; // RatingArray.GetLength(0);
            int I = y; // RatingArray.GetLength(1);
            double xbar, ybar;
            double sumx = 0, sumy = 0;
            double upersum = 0, lower;
            double xi_xbar, yi_ybar;
            double sumxipowr2 = 0, sumyipowr2 = 0;
            double r;
            for (int i = 0; i < U; i++)
            {
                for (int j = i; j < U - 1; j++)
                {
                    for (int k = 0; k < I; k++)
                    {
                        sumx += RatingArray[i, k];       //find sumation of x
                        sumy += RatingArray[j + 1, k];  //find sumation of y
                    }
                    if (sumx == 0 || sumy == 0)
                    {
                        users_corlation[i, j + 1] = 0;
                        users_corlation[j + 1, i] = 0;
                        sumx = 0;
                        sumy = 0;
                        continue;
                    }
                    xbar = sumx / I;                  // find x = mean (x = xbar)
                    ybar = sumy / I;                    // find y = mean (y = ybar)
                    for (int k = 0; k < I; k++)
                    {
                        xi_xbar = RatingArray[i, k] - xbar;
                        yi_ybar = RatingArray[j + 1, k] - ybar;
                        upersum += xi_xbar * yi_ybar;

                        sumxipowr2 += Math.Pow(xi_xbar, 2);
                        sumyipowr2 += Math.Pow(yi_ybar, 2);
                    }
                    lower = Math.Sqrt(sumxipowr2 * sumyipowr2);
                    r = upersum / lower;
                    if(r<max)
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


            ///////////////////////////////////////////////////////
            dataGridView1.ColumnCount = x;
            for (int i = 0; i < x; i++)
                dataGridView1.Columns[i].Name = "M" + i.ToString();

            
            string[] arr = new string[x];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                    arr[j] = users_corlation[i, j].ToString();
                dataGridView1.Rows.Add(arr);
            }


            richTextBox1.Text += "max" + max + "  i:" + iindex.ToString() + "  J;" + jindex.ToString();
            

            

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            



        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 38;    
        }
    }
}
