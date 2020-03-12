using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace RecommendationSystem
{
    class Recommendation
    {
        private int users, movies, neighbors;
        private string ratingsFile, correlationFile, neighborsFile;

        /*
         * Property methods
         */
        public int Users
        {
            get { return users; }
            set { users = value; }
        }

        public int Movies
        {
            get { return movies; }
            set { movies = value; }
        }

        public int Neighbors
        {
            get { return neighbors; }
            set { neighbors = value; }
        }

        public string RatingsFile
        {
            get { return ratingsFile; }
            set { ratingsFile = value; }
        }

        public string CorrelationFile
        {
            get { return correlationFile; }
            set { correlationFile = value; }
        }

        public string NeighborsFile
        {
            get { return neighborsFile; }
            set { neighborsFile = value; }
        }

        public Recommendation()
        {
            this.users = 0;
            this.movies = 0;
            this.neighbors = 0;
        }
        ~Recommendation()
        {

        }

        /*
         * Load data from ratings file
         */
        public int[,] LoadRatings()
        {
            //Check for errors
            if(users <= 0 || movies <= 0)
                throw new ArgumentException("Wrong arguments value!");
            if (string.IsNullOrEmpty(ratingsFile))
                throw new MissingFieldException("Please enter file path!");

            //Set and initialize ratings array
            int[,] Ratings = new int[users, movies];
            //Reading all lines in file
            string[] lines = System.IO.File.ReadAllLines(ratingsFile);

            for (int i = 0; i < users; i++)
                for (int j = 0; j < movies; j++)
                    Ratings[i, j] = 0;
            
            //Read each line and fill it into the ratings array
            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');
                //-1 because there is no movie or user with id 0
                Ratings[int.Parse(DataLine[0]) - 1, int.Parse(DataLine[1]) - 1] = int.Parse(DataLine[2]);
            }

            return Ratings;
        }

        /*
         * Load data from correlation file
         */
        public double[,] LoadUsersCorrelations()
        {
            //Check for errors
            if (users <= 0)
                throw new ArgumentException("Wrong arguments value!");
            if (string.IsNullOrEmpty(correlationFile))
                throw new MissingFieldException("Please enter file path!");

            //Read all lines from file
            string[] lines = System.IO.File.ReadAllLines(correlationFile);

            //Set and initialize the users correlation array
            double[,] UsersCorrelations = new double[users, users];

            for (int i = 0; i < users; i++)
                for (int j = 0; j < users; j++)
                {
                    if (i == j)
                        UsersCorrelations[i, j] = 1;
                    else
                        UsersCorrelations[i, j] = 0.0;
                }

            //Read each line and fill it into the users correlation array
            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');
                UsersCorrelations[int.Parse(DataLine[0]), int.Parse(DataLine[1])] = double.Parse(DataLine[2]);
                UsersCorrelations[int.Parse(DataLine[1]), int.Parse(DataLine[0])] = double.Parse(DataLine[2]);
            }
            return UsersCorrelations;
        }

        /*
         * Load data from neighbors file
         */
        public double[,] LoadUsersNeighbors()
        {
            //Check for errors
            if (users <= 0)
                throw new ArgumentException("Wrong arguments value!");
            if (string.IsNullOrEmpty(neighborsFile))
                throw new MissingFieldException("Please enter file path!");

            //Read all lines from file
            string[] lines = System.IO.File.ReadAllLines(neighborsFile);

            double[,] UsersNeighbors = new double[users, neighbors];

            //Read each line and fill it into the users correlation array
            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');
                for (int i = 0; i < neighbors; i++)
                    UsersNeighbors[int.Parse(DataLine[0]), i] = double.Parse(DataLine[i + 1]);
            }
            return UsersNeighbors;
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



    }
}
