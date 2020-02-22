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

        //Users x Movies array
        private int[,] ratings_array = new int[users, movies];

        //Users x Users array
        private double[,] users_correlation = new double[users, users];

        //Neighbors array
        private double[,] user_neighbors = new double[users, neighbors];

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
            dataGridView1.Columns[0].Name = ColumnHeader + "/" + RowHeader;
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

        private void AllUserRecommendation()
        {
            //index 0 ==> summation
            //index 1 ==> counter
            double[,] TempNeighborsInfo = new double[users * 2, movies];
            double[,] NeighborsInfo = new double[users , movies];
            int UserID = 0;

            for (int i = 0; i < users * 2; i++)
                for (int j = 0; j < movies; j++)
                {
                    TempNeighborsInfo[i, j] = 0;
                    TempNeighborsInfo[i, j] = 0;
                }

            for (int i = 0; i < users * 2; i += 2)
            {
                for (int j = 0; j < 50; j++)
                    for (int k = 0; k < movies; k++)
                    {
                        if (ratings_array[Convert.ToInt16(user_neighbors[UserID, j]), k] > 0)
                        {
                            TempNeighborsInfo[i, k] += ratings_array[Convert.ToInt16(user_neighbors[UserID, j]), k];
                            TempNeighborsInfo[i + 1, k]++;
                        }
                    }
                UserID++;
            }

            for (int i = 0; i < users * 2; i += 2)
                for (int j = 0; j < movies; j++)
                        if (TempNeighborsInfo[i+1, j] > 40)   // count > 10
                            TempNeighborsInfo[i, j] = TempNeighborsInfo[i, j] / TempNeighborsInfo[i+1, j];
                        else
                            TempNeighborsInfo[i, j] = 0;
            int temp = 0;
            for (int i = 0; i < users * 2; i += 2)
            {
                for (int j = 0; j < movies; j++)
                    NeighborsInfo[temp, j] = TempNeighborsInfo[i, j];
                temp++;
            }

            ShowData(ref NeighborsInfo,"Movie ","Rating ",10);
        }

        private void UserRecommendation(int UserID)
        {
            //index 0 ==> summation
            //index 1 ==> counter
            double[,] NeighborsInfo = new double[2, movies];

            for (int i = 0; i < movies; i++)
            {
                NeighborsInfo[0, i] = 0 ; 
                NeighborsInfo[1, i] = 0 ;
            }
                
            for (int i = 0; i < 50; i++)
                for (int j =0; j < movies; j++)
                {
                    if(ratings_array[Convert.ToInt16(user_neighbors[UserID, i]) + 1, j] > 0)
                    {
                        NeighborsInfo[0, j] += ratings_array[Convert.ToInt16(user_neighbors[UserID, i]) + 1, j];
                        NeighborsInfo[1, j]++;
                    }
                }

            for (int i = 0; i < movies; i++)
                if (NeighborsInfo[1, i] > 10)
                    NeighborsInfo[0, i] = NeighborsInfo[0, i] / NeighborsInfo[1, i];
                else
                    NeighborsInfo[0, i] = 0;
            //Wrong RowHeader Name
            ShowData(ref NeighborsInfo, "Movie ", "Rating ", 10);
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
            UserRecommendation(10);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadNeighbors();
            LoadRatings();
            AllUserRecommendation();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadRatings();
            CalculatePearson();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FindNearestNeighbors();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadPearson();
            ShowData(ref users_correlation,"User ","User ",10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadRatings();
            ShowData(ref ratings_array,"Movie ","User ",10);
        }
    }
}
