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
        private int users, movies, neighbors, noOfRecommendedMovies;

        /*
         * Property methods
         */
        public int Users
        {
            get { return users; }
        }

        public int Movies
        {
            get { return movies; }
        }

        public int Neighbors
        {
            get { return neighbors; }
            set { neighbors = value; }
        }

        public int NoOfRecommendedMovies
        {
            get { return noOfRecommendedMovies; }
            set { noOfRecommendedMovies = value; }
        }

        public RecommendationEngine()
        {
            this.users = 0;
            this.movies = 0;
            this.neighbors = 0;
            this.noOfRecommendedMovies = 0;
        }

        ~RecommendationEngine()
        {

        }

        /*
        * Calculate pearson correlation among users
        */
        public double[,] CalculateCorrelations(int[,] Ratings)
        {
            // Get the length of the matrix
            users = Ratings.GetLength(0);
            movies = Ratings.GetLength(1);

            // Check for errors
            if (users <= 0 || movies <= 0)
                throw new IOException("Please enter appropriate numbers");

            // Initialize the users correlation array
            double[,] UsersCorrelation = new double[users, movies];
            for (int i = 0; i < users; i++)
                for (int j = 0; j < users; j++)
                    UsersCorrelation[i, j] = 1.0;

            // Initialize the needed variables 
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

        /*
        * Find nearest neighbors for all users
        */
        public double[,] FindNearestNeighbors(double[,] Correlations)
        {
            users = Correlations.GetLength(0);

            //Check for errors
            if (users <= 0 || neighbors <= 0)
                throw new IOException("Please enter appropriate numbers");

            int NeighborIndex = 0;
            double Max = -1;
            string NeighborsString;
            double[,] UserNeighbors = new double[users,neighbors];

            for (int i = 0; i < users; i++)
            {
                NeighborsString = i.ToString();

                for (int j = 0; j < neighbors; j++)
                {
                    for (int k = 0; k < users; k++)
                    {
                        if (Correlations[i, k] > Max && i != k)
                        {
                            Max = Correlations[i, k];
                            NeighborIndex = k;
                        }
                    }
                    Correlations[i, NeighborIndex] = -1;
                    Max = -1;

                    //Neighbor index + 1 (C#) = Real User ID (MovieLens)
                    UserNeighbors[i, j] = NeighborIndex;
                }
            }
            return UserNeighbors;
        }

        /*
         * Recommend for all users
         */
        public double[,] Recommendations(int[,] Ratings, double[,] Neighbors)
        {
            // Get the length of the matrix
            users = Ratings.GetLength(0);
            movies = Ratings.GetLength(1);

            //Check for errors
            if (users <= 0 || neighbors <= 0 || movies<= 0 || noOfRecommendedMovies <=0)
                throw new IOException("Please enter appropriate numbers");

            //index 0 ==> summation
            //index 1 ==> counter
            double[,] TempNeighborsInfo = new double[users * 2, movies];
            double[,] NeighborsInfo = new double[users, movies];

            //Rows:Users ; Columns:# of Recommended movies
            double[,] RecommendedMovies = new double[users, noOfRecommendedMovies];

            for (int i = 0; i < users; i++)
                for (int j = 0; j < movies; j++)
                {
                    TempNeighborsInfo[i * 2, j] = 0;
                    TempNeighborsInfo[i * 2 + 1, j] = 0;
                }

            for (int i = 0; i < users; i++)
            {
                for (int j = 0; j < neighbors; j++)
                    for (int k = 0; k < movies; k++)
                    {
                        if (Ratings[Convert.ToInt16(Neighbors[i, j]), k] > 0)
                        {
                            TempNeighborsInfo[i * 2, k] += Ratings[Convert.ToInt16(Neighbors[i, j]), k];
                            TempNeighborsInfo[i * 2 + 1, k]++;
                        }
                    }
            }

            for (int i = 0; i < users; i++)
                for (int j = 0; j < movies; j++)
                    if (TempNeighborsInfo[i * 2 + 1, j] > 10 && Ratings[i, j] == 0)   // count > 10
                        NeighborsInfo[i, j] = TempNeighborsInfo[i * 2, j] / TempNeighborsInfo[i * 2 + 1, j];
                    else
                        NeighborsInfo[i, j] = 0;


            //Needed variables
            double Max = 0;
            int MovieIndex = 0;

            //Sorting the array for n movies
            for (int i = 0; i < users; i++)
            {
                for (int j = 0; j < noOfRecommendedMovies; j++)
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
            return RecommendedMovies;
        }
    }
}
