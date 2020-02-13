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

        //Count of users, movies and neighbors
        private static readonly int users = 944, movies = 1683, neighbors = 50;

        //Users x Movies array
        readonly int[,] ratings_array = new int[users, movies];

        //Users x Users array
        readonly double[,] users_correlation = new double[users, users];

        //Neighbors array
        readonly double[,] user_neighbors = new double[users, neighbors];

        //Raw ratings file path
        readonly string RatingsFile = "data.txt";

        //Correlation file path
        readonly string CorrelationFile = "Correlation.txt";

        /*
         * Load ratings from the raw ratings file into an array 
         */
        private void LoadRatings()
        {
            string[] lines = System.IO.File.ReadAllLines(RatingsFile);

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

        /*
         * Show ratings array on data grid view
         */
        private void ShowRatings()
        {
            dataGridView1.ColumnCount = movies;

            for (int i = 0; i < movies; i++)
                dataGridView1.Columns[i].Name = "Movie " + i.ToString();

            string[] arr = new string[movies];
            for (int i = 1; i < users; i++)
            {
                for (int j = 1; j < movies; j++)
                    arr[j] = ratings_array[i, j].ToString();
                dataGridView1.Rows.Add(arr);
            }
        }


        /*
         * Calculate pearson correlation among users
         */
        private void CalculatePearson()
        {
            for (int i = 1; i < users; i++)
                for (int j = 1; j < users; j++)
                    users_correlation[i, j] = 1.0;

            double xbar, ybar;
            double sumx = 0, sumy = 0;
            double upersum = 0, lower;
            double xi_xbar, yi_ybar;
            double sumxipowr2 = 0, sumyipowr2 = 0;
            double r;

            //this line allow us to write on file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(CorrelationFile))
            {
                for (int i = 1; i < users; i++)
                {
                    for (int j = i; j < users - 1; j++)
                    {
                        for (int k = 1; k < movies; k++)
                        {
                            sumx += ratings_array[i, k];       //find sumation of x
                            sumy += ratings_array[j + 1, k];  //find sumation of y
                        }
                        if (sumx == 0 || sumy == 0)
                        {
                            users_correlation[i, j + 1] = 0;
                            users_correlation[j + 1, i] = 0;

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
                        r= Math.Round(r, 4, MidpointRounding.ToEven);
                        users_correlation[i, j + 1] = r;
                        users_correlation[j + 1, i] = r;

                        string line = i + "\t" + (j+1) + "\t" + r;
                        file.WriteLine(line);

                        sumx = 0;
                        sumy = 0;
                        upersum = 0;
                        sumxipowr2 = 0;
                        sumyipowr2 = 0;
                    }
                }

            }
        }

        /*
         * Load pearson correlation data from file
         */
        private void LoadPearson()
        {
            string[] lines = System.IO.File.ReadAllLines(CorrelationFile);

            for (int i = 1; i < users; i++)
                for (int j = 1; j < users; j++)
                {
                    if (i == j)
                        users_correlation[i, j] = 1;
                    else
                        users_correlation[i, j] = 0.0;
                }

            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');

                users_correlation[int.Parse(DataLine[0]), int.Parse(DataLine[1])] = double.Parse(DataLine[2]);
                users_correlation[int.Parse(DataLine[1]), int.Parse(DataLine[0])] = double.Parse(DataLine[2]);
            }
        }

        /*
        * Show pearson correlation array on data grid view
        */
        private void ShowPearson()
        {
            int temp_users = users;
            dataGridView1.ColumnCount = temp_users + 1;
            for (int i = 0; i < temp_users; i++)
                dataGridView1.Columns[i+1].Name = "User " + (i+1).ToString();


            string[] arr = new string[temp_users];
            for (int i = 1; i < temp_users; i++)
            {
                arr[0] = "User" + i;

                for (int j = 1; j < temp_users; j++)
                    arr[j] = users_correlation[i, j].ToString();
              
                dataGridView1.Rows.Add(arr);
            }
        }

        /*
         *  Find nearest neighbors from pearson correlation array
         */
        private void FindNearestNeighbors()
        {
            int temp_k = 0;
            double max = -1;
            //int neighbor_index = 0;

            string[] lines = System.IO.File.ReadAllLines(CorrelationFile);

            for (int i = 1; i < users; i++)
                for (int j = 1; j < users; j++)
                {
                    if (i == j)
                        users_correlation[i, j] = 1;
                    else
                        users_correlation[i, j] = 0.0;
                }

            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');

                users_correlation[int.Parse(DataLine[0]), int.Parse(DataLine[1])] = double.Parse(DataLine[2]);
                users_correlation[int.Parse(DataLine[1]), int.Parse(DataLine[0])] = double.Parse(DataLine[2]);
            }


            for (int i = 1; i < users - 1; i++)
            {
                for (int j = 0; j < neighbors; j++)
                {
                    for (int k = 1; k < users - 1; k++)
                    {
                        if (users_correlation[i, k] > max && i != k)
                        {
                            max = users_correlation[i, k];
                            temp_k = k;
                        }
                    }
                    users_correlation[i, temp_k] = -1;
                    max = -1;

                    user_neighbors[i - 1, j] = temp_k;
                }
            }
        }

        /*
         * Show Neighbors array
         */
        private void ShowNeighbors()
        {
            dataGridView1.ColumnCount = neighbors + 1;
            for (int i = 0; i < neighbors; i++)
                dataGridView1.Columns[i + 1].Name = "Neighbor " + (i + 1).ToString();


            string[] arr = new string[neighbors + 1];
            for (int i = 0; i < users; i++)
            {
                arr[0] = "User" + (i + 1);

                for (int j = 1; j < neighbors + 1; j++)
                    arr[j] = user_neighbors[i, j - 1].ToString();

                dataGridView1.Rows.Add(arr);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            
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
            dataGridView1.Rows.Clear();
            LoadRatings();
            CalculatePearson();
            ShowPearson();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            FindNearestNeighbors();
            ShowNeighbors();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            LoadPearson();
            ShowPearson();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            LoadRatings();
            ShowRatings();
        }
    }
}
