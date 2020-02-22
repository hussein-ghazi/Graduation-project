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
        private static readonly int users = 943, movies = 1682, neighbors = 50;

        //Variables used to show data
        private static readonly int UsersShow = 50, MoviesShow = 10, NeighborsShow = 10;

        //Users x Movies array
        private readonly int[,] ratings_array = new int[users, movies];

        //Users x Users array
        private readonly double[,] users_correlation = new double[users, users];

        //Neighbors array
        private readonly double[,] user_neighbors = new double[users, neighbors];

        //Raw ratings file path
        private readonly string RatingsFile = "Ratings.txt";

        //Correlation file path
        private readonly string CorrelationFile = "Correlation.txt";

        //Correlation file path
        private readonly string NeighborsFile = "Neighbors.txt";

        /*
         * Load ratings from the raw ratings file into an array 
         */
        private void LoadRatings()
        {
            //Reading all lines in file
            string[] lines = System.IO.File.ReadAllLines(RatingsFile);

            //Initialize the ratings array
            for (int i = 0; i < users; i++)
                for (int j = 0; j < movies; j++)
                    ratings_array[i, j] = 0;

            //Read each line and fill it into the ratings array
            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');
                //-1 because there is no movie or user with id 0
                ratings_array[int.Parse(DataLine[0]) - 1, int.Parse(DataLine[1]) - 1] = int.Parse(DataLine[2]);
            }
        }

        /*
         * Show ratings array on data grid view
         */
        private void ShowRatings()
        {
            // All +1 index is for presenting purposes
            //Initialize the columns with names
            dataGridView1.ColumnCount = MoviesShow + 1;
            for (int i = 0; i < MoviesShow; i++)
                dataGridView1.Columns[i + 1].Name = "Movie " + (i + 1).ToString();

            //Reading from ratings array and fill it into the data grid view
            string[] MoviesArray = new string[MoviesShow + 1];
            for (int i = 0; i < UsersShow; i++)
            {
                MoviesArray[0] = "User " + (i + 1).ToString();
                for (int j = 0; j < MoviesShow; j++)
                    MoviesArray[j + 1] = ratings_array[i, j].ToString();
                dataGridView1.Rows.Add(MoviesArray);
            }
        }


        /*
         * Calculate pearson correlation among users
         */
        private void CalculatePearson()
        {
            //Initialize the users correlation array
            for (int i = 0; i < users; i++)
                for (int j = 0; j < users; j++)
                    users_correlation[i, j] = 1.0;

            //Initialize the needed variables 
            double xbar, ybar;
            double sumx = 0, sumy = 0;
            double upersum = 0, lower;
            double xi_xbar, yi_ybar;
            double sumxipowr2 = 0, sumyipowr2 = 0;
            double r;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(CorrelationFile))
            {
                for (int i = 0; i < users; i++)
                {
                    for (int j = i; j < users - 1; j++)
                    {
                        //Find the summation of vectors x and y
                        for (int k = 0; k < movies; k++)
                        {
                            sumx += ratings_array[i, k];      
                            sumy += ratings_array[j + 1, k];  
                        }

                        //If summation of any is 0 then zeroing and continue the loop
                        if (sumx == 0 || sumy == 0)
                        {
                            users_correlation[i, j + 1] = 0;
                            users_correlation[j + 1, i] = 0;

                            sumx = 0;
                            sumy = 0;
                            continue;
                        }

                        //Calculate the mean of x and y
                        xbar = sumx / movies;                  
                        ybar = sumy / movies; 
                        
                        for (int k = 0; k < movies; k++)
                        {
                            xi_xbar = ratings_array[i, k] - xbar;
                            yi_ybar = ratings_array[j + 1, k] - ybar;
                            upersum += xi_xbar * yi_ybar;

                            sumxipowr2 += Math.Pow(xi_xbar, 2);
                            sumyipowr2 += Math.Pow(yi_ybar, 2);
                        }
                        lower = Math.Sqrt(sumxipowr2 * sumyipowr2);

                        //Calculate the correlation and round off the result
                        r = upersum / lower;
                        r= Math.Round(r, 4, MidpointRounding.ToEven);

                        //Insert the result into the users correlation array
                        users_correlation[i, j + 1] = r;
                        users_correlation[j + 1, i] = r;

                        //Write the data into the file
                        string line = i + "\t" + (j+1) + "\t" + r;
                        file.WriteLine(line);

                        //Zeroing the needed variables 
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
            //Read all lines from file
            string[] lines = System.IO.File.ReadAllLines(CorrelationFile);

            //Initialize the users correlation array
            for (int i = 0; i < users; i++)
                for (int j = 0; j < users; j++)
                {
                    if (i == j)
                        users_correlation[i, j] = 1;
                    else
                        users_correlation[i, j] = 0.0;
                }

            //Read each line and fill it into the users correlation array
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
            //Initialize the columns with names
            dataGridView1.ColumnCount = UsersShow + 1;
            for (int i = 0; i < UsersShow; i++)
                dataGridView1.Columns[i+1].Name = "User " + (i + 1);

            //Reading from users correlation array and fill it into the data grid view
            string[] UsersArray = new string[UsersShow + 1];
            for (int i = 0; i < UsersShow; i++)
            {
                UsersArray[0] = "User" + (i + 1);

                for (int j = 0; j < UsersShow; j++)
                    UsersArray[j+1] = users_correlation[i, j].ToString();
              
                dataGridView1.Rows.Add(UsersArray);
            }
        }

        /*
         *  Find nearest neighbors from pearson correlation array
         */
        private void FindNearestNeighbors()
        {
            int NeighborIndex = 0;
            double Max = -1;
            string NeighborsString; 

            LoadPearson();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(NeighborsFile))
            {
                for (int i = 0; i < users; i++)
                {
                    NeighborsString = i.ToString();

                    for (int j = 0; j < neighbors; j++)
                    {

                        for (int k = 0; k < users; k++)
                        {
                            if (users_correlation[i, k] > Max && i != k)
                            {
                                Max = users_correlation[i, k];
                                NeighborIndex = k;
                            }
                        }
                        users_correlation[i, NeighborIndex] = -1;
                        Max = -1;

                        //Neighbor index + 1 = User ID
                        user_neighbors[i, j] = NeighborIndex;
                        NeighborsString += '\t' + NeighborIndex.ToString() ;
                    }
                    file.WriteLine(NeighborsString);
                }
            }
        }

        private void LoadNeighbors()
        {
            //Read all lines from file
            string[] lines = System.IO.File.ReadAllLines(NeighborsFile);

            //Read each line and fill it into the users correlation array
            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');

                for(int i = 0; i < neighbors; i++)
                    user_neighbors[int.Parse(DataLine[0]), i] = double.Parse(DataLine[i+1]);
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


            string[] NeighborsArray = new string[neighbors + 1];
            for (int i = 0; i < users; i++)
            {
                NeighborsArray[0] = "User" + (i + 1);

                for (int j = 0; j < neighbors; j++)
                    NeighborsArray[j + 1] = user_neighbors[i, j].ToString();

                dataGridView1.Rows.Add(NeighborsArray);
            }
        }


        private void UserRecommendation(int UserID)
        {
            double[,] NeighborsInfo = new double[2, movies];

            for (int i = 0; i < movies; i++)
            {
                NeighborsInfo[0, i] = 0 ;
                NeighborsInfo[1, i] = 0 ;
            }
                


            

            for (int i = 0; i < 50; i++)
                for (int j =0; j<movies; j++)
                {
                    if(ratings_array[Convert.ToInt16(user_neighbors[UserID, i]) + 1, j] > 0)
                    {
                        NeighborsInfo[0, j] += ratings_array[Convert.ToInt16(user_neighbors[UserID, i]) + 1, j];
                        NeighborsInfo[1, j]++;
                    }
                }

            ////////////////////////////
            dataGridView1.ColumnCount = movies;
            for (int i = 0; i < movies; i++)
                dataGridView1.Columns[i].Name = "Movie " + (i);

            string[] UsersArray = new string[movies];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < movies; j++)
                    UsersArray[j] = NeighborsInfo[i, j].ToString();
                dataGridView1.Rows.Add(UsersArray);
            }
            //////////////////////////////////



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

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            LoadNeighbors();
            ShowNeighbors();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            LoadNeighbors();
            LoadRatings();
            LoadPearson();
            UserRecommendation(10);
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
