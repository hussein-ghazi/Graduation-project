using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace RecommendationSystem
{
    class RecommendationEngine
    {
        private int users, movies, neighbors;

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

        public RecommendationEngine()
        {
            this.users = 0;
            this.movies = 0;
            this.neighbors = 0;
        }
        ~RecommendationEngine()
        {

        }

        /*
         * Load data from ratings file
         */
        public int[,] ReadRatingsFile(string ratingsFile)
        {
            //Check for errors
            if(users <= 0 || movies <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(ratingsFile))
                throw new MissingFieldException("Please enter a file path!");
            if (!System.IO.File.Exists(ratingsFile))
                throw new FileNotFoundException("File not found!");

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
        public double[,] ReadUsersCorrelationsFile(string correlationFile)
        {
            //Check for errors
            if (users <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(correlationFile))
                throw new MissingFieldException("Please enter a file path!");
            if (!System.IO.File.Exists(correlationFile))
                throw new FileNotFoundException("File not found!");

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
        public double[,] ReadUsersNeighborsFile(string neighborsFile)
        {
            //Check for errors
            if (users <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(neighborsFile))
                throw new MissingFieldException("Please enter a file path!");
            if (!System.IO.File.Exists(neighborsFile))
                throw new FileNotFoundException("File not found!");

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
        public double[,] CalculateCorrelations(int[,] Ratings)
        {
            //Check for errors
            if (users <= 0 || movies <= 0)
                throw new IOException("Please enter appropriate numbers");

            //Initialize the users correlation array
            double[,] UsersCorrelation = new double[users, movies];
            for (int i = 0; i < users; i++)
                for (int j = 0; j < users; j++)
                    UsersCorrelation[i, j] = 1.0;

            //Initialize the needed variables 
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
                    //Find the summation of vectors x and y
                    for (int k = 0; k < movies; k++)
                    {
                        sumx += Ratings[i, k];
                        sumy += Ratings[j + 1, k];
                    }

                    //If summation of any is 0 then zeroing and continue the loop
                    if (sumx == 0 || sumy == 0)
                    {
                        UsersCorrelation[i, j + 1] = 0;
                        UsersCorrelation[j + 1, i] = 0;
                        sumx = 0;
                        sumy = 0;
                        continue;
                    }

                    //Calculate the mean of x and y
                    xbar = sumx / movies;
                    ybar = sumy / movies;

                    for (int k = 0; k < movies; k++)
                    {
                        xi_xbar = Ratings[i, k] - xbar;
                        yi_ybar = Ratings[j + 1, k] - ybar;
                        upersum += xi_xbar * yi_ybar;

                        sumxipowr2 += Math.Pow(xi_xbar, 2);
                        sumyipowr2 += Math.Pow(yi_ybar, 2);
                    }
                    lower = Math.Sqrt(sumxipowr2 * sumyipowr2);

                    //Calculate the correlation and round off the result
                    r = upersum / lower;
                    r = Math.Round(r, 4, MidpointRounding.ToEven);

                    //Insert the result into the users correlation array
                    UsersCorrelation[i, j + 1] = r;
                    UsersCorrelation[j + 1, i] = r;

                    sumx = 0;
                    sumy = 0;
                    upersum = 0;
                    sumxipowr2 = 0;
                    sumyipowr2 = 0;
                }
            }
            return UsersCorrelation;
        }
    }
}
