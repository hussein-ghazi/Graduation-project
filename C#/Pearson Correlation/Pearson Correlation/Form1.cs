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
        private static readonly int users = 943, movies = 1682, neighbors = 50, NoOfRecommendedMovies = 200;

        //Users x Movies array
        private int[,] ratings_array = new int[users, movies];

        //Users x Users array
        private double[,] users_correlation = new double[users, users];

        //Neighbors array
        private double[,] user_neighbors = new double[users, neighbors];

        //Rows:Movie ID , Movie Rating ; Columns:# of Recommended movies
        private double[,] RecommendedMovies = new double[users, NoOfRecommendedMovies];

        //Raw ratings file path
        private readonly string RatingsFile = "Ratings.txt";

        //Correlation file path
        private readonly string CorrelationFile = "Correlation.txt";

        //Correlation file path
        private readonly string NeighborsFile = "Neighbors.txt";

        /*
        * Generic show function
        */
        private void ShowData<T>(ref T[,] DataArray, string ColumnHeader, string RowHeader, int ColumnsCount)
        {
            //Clear the datagridview 
            dataGridView1.Rows.Clear();

            int RowsNo = DataArray.GetLength(0);

            //If the ColumnsCount is 0 ==> show all columns of DataArray
            if (ColumnsCount == 0)
                ColumnsCount = DataArray.GetLength(1);

            //Setting column header
            dataGridView1.ColumnCount = ColumnsCount + 1;
            dataGridView1.Columns[0].Name = RowHeader + "\\" + ColumnHeader;
            for (int i = 1; i < ColumnsCount + 1; i++)
                dataGridView1.Columns[i].Name = ColumnHeader + (i);

            //Process of adding rows
            string[] Row = new string[ColumnsCount + 1];
            for (int i = 0; i < RowsNo; i++)
            {
                Row[0] = RowHeader + (i + 1);

                for (int j = 0; j < ColumnsCount; j++)
                    Row[j + 1] = DataArray[i, j].ToString();
                dataGridView1.Rows.Add(Row);
            }
        }

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

            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(CorrelationFile))
            //{
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
                        r = Math.Round(r, 4, MidpointRounding.ToEven);

                        //Insert the result into the users correlation array
                        users_correlation[i, j + 1] = r;
                        users_correlation[j + 1, i] = r;

                        //Write the data into the file
                        //string line = i + "\t" + (j + 1) + "\t" + r;
                        //file.WriteLine(line);

                        //Zeroing the needed variables 
                        sumx = 0;
                        sumy = 0;
                        upersum = 0;
                        sumxipowr2 = 0;
                        sumyipowr2 = 0;
                    }
                }

            //}
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
         *  Find nearest neighbors from pearson correlation array
         */
        private void FindNearestNeighbors()
        {
            int NeighborIndex = 0;
            double Max = -1;
            string NeighborsString;

            LoadPearson();
            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(NeighborsFile))
            //{
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

                        //Neighbor index + 1 (C++) = Real User ID (MovieLens)
                        user_neighbors[i, j] = NeighborIndex;
                       // NeighborsString += '\t' + NeighborIndex.ToString();
                    }
                   // file.WriteLine(NeighborsString);
                }
            //}
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

                for (int i = 0; i < neighbors; i++)
                    user_neighbors[int.Parse(DataLine[0]), i] = double.Parse(DataLine[i + 1]);
            }
        }

        private void AllUserRecommendation()
        {
            //index 0 ==> summation
            //index 1 ==> counter
            double[,] TempNeighborsInfo = new double[users * 2, movies];
            double[,] NeighborsInfo = new double[users, movies];

            for (int i = 0; i < users; i++)
                for (int j = 0; j < movies; j++)
                {
                    TempNeighborsInfo[i * 2, j] = 0;
                    TempNeighborsInfo[i * 2 + 1, j] = 0;
                }

            for (int i = 0; i < users; i++)
            {
                for (int j = 0; j < 50; j++)
                    for (int k = 0; k < movies; k++)
                    {
                        if (ratings_array[Convert.ToInt16(user_neighbors[i, j]), k] > 0)
                        {
                            TempNeighborsInfo[i * 2, k] += ratings_array[Convert.ToInt16(user_neighbors[i, j]), k];
                            TempNeighborsInfo[i * 2 + 1, k]++;
                        }
                    }
            }

            for (int i = 0; i < users; i++)
                for (int j = 0; j < movies; j++)
                    if (TempNeighborsInfo[i * 2 + 1, j] > 10 && ratings_array[i, j] == 0)   // count > 10
                        NeighborsInfo[i, j] = TempNeighborsInfo[i * 2, j] / TempNeighborsInfo[i * 2 + 1, j];
                    else
                        NeighborsInfo[i, j] = 0;


            //Needed variables
            double Max = 0;
            int MovieIndex = 0;

            //Sorting the array for n movies
            for (int i = 0; i < users; i++)
            {
                for (int j = 0; j < NoOfRecommendedMovies; j++)
                {
                    for (int k = 0; k < movies; k++)
                    {
                        if (NeighborsInfo[i, k] > Max)
                        {
                            Max = NeighborsInfo[i, k];
                            MovieIndex = k;
                        }
                    }
                    RecommendedMovies[i, j] = MovieIndex + 1;
                    NeighborsInfo[i, MovieIndex] = 0;
                    Max = 0;
                }
            }

        ShowData(ref RecommendedMovies,"User ","Movie ",NoOfRecommendedMovies);
        }

        private void UserRecommendation(int UserID)
        {
            //0 => Sum , 1 => Count
            double[,] NeighborsInfo = new double[2, movies];

            //Zeroing the array
            for (int i = 0; i < movies; i++)
            {
                NeighborsInfo[0, i] = 0 ; 
                NeighborsInfo[1, i] = 0 ;
            }
                
            //Calculate the sum and count for each movie and for all neighbors
            for (int i = 0; i < 50; i++)
                for (int j = 0; j < movies; j++)
                {
                    if(ratings_array[Convert.ToInt16(user_neighbors[UserID, i]) , j] > 0)
                    {
                        NeighborsInfo[0, j] += ratings_array[Convert.ToInt16(user_neighbors[UserID, i]), j];
                        NeighborsInfo[1, j]++;
                    }
                }

            //Calculate the average rating for each movie if seen by more than 10 users
            for (int i = 0; i < movies; i++)
                if (NeighborsInfo[1, i] > 10 && ratings_array[UserID, i] == 0)
                    NeighborsInfo[0, i] = NeighborsInfo[0, i] / NeighborsInfo[1, i];
                else
                    NeighborsInfo[0, i] = 0;

            ShowData(ref NeighborsInfo, "Movie ", "Info. ", NoOfRecommendedMovies);


            /*
            //Needed variables
            double Max = 0 ;
            int MovieIndex = 0;

            //Sorting the array for n movies
            for (int i = 0; i < 1; i++)
            {
            {
                for (int j = 0; j < NoOfRecommendedMovies; j++)
                {
                    for (int k = 0; k < movies; k++)
                    {
                        if (NeighborsInfo[0, k] > Max)
                        {
                            Max = NeighborsInfo[0, k];
                            MovieIndex = k;
                        }
                    }
                    RecommendedMovies[0, j] = MovieIndex + 1;
                    RecommendedMovies[1, j] = Max;
                    NeighborsInfo[0, MovieIndex] = 0;
                    Max = 0;
                }
            }
            */


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
            LoadNeighbors();
            ShowData(ref user_neighbors, "Neighbor ", "User ",0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadNeighbors();
            LoadRatings();
            LoadPearson();
            UserRecommendation(int.Parse(textBox3.Text) - 1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadNeighbors();
            LoadRatings();
            AllUserRecommendation();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //LoadRatings();
            CalculatePearson();
            MessageBox.Show("pearson calculated");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ratings_array[int.Parse(textBox1.Text) - 1, int.Parse(textBox2.Text) - 1] = 0;
            MessageBox.Show("[user,movie] [" + textBox1.Text + "," + textBox2.Text + "] now = 0");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ShowData(ref ratings_array, "Movie ", "User ", 10);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FindNearestNeighbors();
            MessageBox.Show("neighbors found");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadPearson();
            ShowData(ref users_correlation,"User ","User ",10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadRatings();
            ShowData(ref ratings_array,"Movie ","User ",1000);
        }
    }
}
