using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using RSExceptions;

namespace RecommendationSystem
{
    class RecommendationEngine
    {
        private int users, movies, neighbors, topNrecommendations;

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

        public int TopNrecommendations
        {
            get { return topNrecommendations; }
            set { topNrecommendations = value; }
        }

        public RecommendationEngine()
        {
            this.users = 0;
            this.movies = 0;
            this.neighbors = 0;
            this.topNrecommendations = 0;
        }

        ~RecommendationEngine()
        {

        }

        /*
        * Calculate pearson correlation among all users
        */
        public double[,] CalculateCorrelations(int[,] Ratings)
        {
            // Get the length of the matrix
            users = Ratings.GetLength(0);
            movies = Ratings.GetLength(1);

            // Check for errors
            if (users <= 0)
                throw new InvalidUsersValueException("Users count is zero or less!");
            if (movies <= 0)
                throw new InvalidMoviesValueException("Movies count is zero or less!");

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
                    // Find the summation of vectors x and y
                    for (int k = 0; k < movies; k++)
                    {
                        sumx += Ratings[i, k];
                        sumy += Ratings[j + 1, k];
                    }

                    // If summation of any is 0 then zeroing and continue the loop
                    if (sumx == 0 || sumy == 0)
                    {
                        UsersCorrelation[i, j + 1] = 0;
                        UsersCorrelation[j + 1, i] = 0;
                        sumx = 0;
                        sumy = 0;
                        continue;
                    }

                    // Calculate the mean of x and y
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

                    // Calculate the correlation and round off the result
                    r = upersum / lower;
                    r = Math.Round(r, 4, MidpointRounding.ToEven);

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
        public double[,] FindNeighbors(double[,] Correlations)
        {
            users = Correlations.GetLength(0);

            // Check for errors
            if (users <= 0)
                throw new InvalidUsersValueException("Users count is zero or less!");
            if (neighbors <= 0)
                throw new InvalidNeighborsValueException("Neighbors count is zero or less!");

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

                    // Neighbor index + 1 (C#) = Real User ID (MovieLens)
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

            // Check for errors
            if (users <= 0)
                throw new InvalidUsersValueException("Users count is zero or less!");
            if (movies <= 0)
                throw new InvalidMoviesValueException("Movies count is zero or less!");
            if (neighbors <= 0)
                throw new InvalidNeighborsValueException("Neighbors count is zero or less!");
            if (topNrecommendations <= 0)
                throw new InvalidTopNrecommendationsValueException("TopNrecommendations count is zero or less!");

            // index 0 ==> summation
            // index 1 ==> counter
            double[,] TempNeighborsInfo = new double[users * 2, movies];
            double[,] NeighborsInfo = new double[users, movies];

            // Rows:Users ; Columns:# of Recommended movies
            double[,] RecommendedMovies = new double[users, topNrecommendations];

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
                    if (TempNeighborsInfo[i * 2 + 1, j] > 10 && Ratings[i, j] == 0)   // # people who rate the movie > 10
                        NeighborsInfo[i, j] = TempNeighborsInfo[i * 2, j] / TempNeighborsInfo[i * 2 + 1, j];
                    else
                        NeighborsInfo[i, j] = 0;

            // Needed variables
            double Max = 0;
            int MovieIndex = 0;

            // Sorting the array for n movies
            for (int i = 0; i < users; i++)
            {
                for (int j = 0; j < topNrecommendations; j++)
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

        /*
        * Calculate pearson correlation for one user
        */
        public double[] CalculateUserCorrelations(int UserID, int[,] Ratings)
        {
            // Get the length of the matrix
            users = Ratings.GetLength(0);
            movies = Ratings.GetLength(1);

            double[] UserCorrelation = new double[users];
            // Initialize the user correlation array
            for (int i = 0; i < users; i++)
                UserCorrelation[i] = 1.0;

            // Initialize the needed variables 
            double xbar, ybar;
            double sumx = 0, sumy = 0;
            double upersum = 0, lower;
            double xi_xbar, yi_ybar;
            double sumxipowr2 = 0, sumyipowr2 = 0;
            double r;

            for (int j = 0; j < users; j++)
            {
                if (UserID != j)
                {
                    for (int k = 0; k < movies; k++)
                    {
                        sumx += Ratings[UserID, k];
                        sumy += Ratings[j, k];
                    }

                    if (sumx == 0 || sumy == 0)
                    {
                        UserCorrelation[j] = 0;
                        UserCorrelation[j] = 0;
                        sumx = 0;
                        sumy = 0;
                        continue;
                    }

                    xbar = sumx / movies;
                    ybar = sumy / movies;

                    for (int k = 0; k < movies; k++)
                    {
                        xi_xbar = Ratings[UserID, k] - xbar;
                        yi_ybar = Ratings[j, k] - ybar;
                        upersum += xi_xbar * yi_ybar;

                        sumxipowr2 += Math.Pow(xi_xbar, 2);
                        sumyipowr2 += Math.Pow(yi_ybar, 2);
                    }
                    lower = Math.Sqrt(sumxipowr2 * sumyipowr2);

                    r = upersum / lower;
                    r = Math.Round(r, 4, MidpointRounding.ToEven);

                    UserCorrelation[j] = r;

                    sumx = 0;
                    sumy = 0;
                    upersum = 0;
                    sumxipowr2 = 0;
                    sumyipowr2 = 0;
                }
                else
                    UserCorrelation[j] = 1;
            }
            return UserCorrelation;
        }

        /*
        * Find nearest neighbors for one user
        */
        public double[] FindUserNeighbors(int UserID, double[] UserCorrelation)
        {
            users = UserCorrelation.GetLength(0);

            int NeighborIndex = 0;
            double Max = -1;
            double[] UserNeighbors = new double[users];

            for (int j = 0; j < neighbors; j++)
            {
                for (int k = 0; k < users; k++)
                {
                    if (UserCorrelation[k] > Max && UserID != k)
                    {
                        Max = UserCorrelation[k];
                        NeighborIndex = k;
                    }
                }
                UserCorrelation[NeighborIndex] = -1;
                Max = -1;
                UserNeighbors[j] = NeighborIndex;
            }
            return UserNeighbors;
        }

        /*
        * Recommend for one user
        */
        public double[] UserRecommendations(int UserID, int[,] Ratings, double[] Neighbors)
        {
            movies = Ratings.GetLength(1);

            //0 => Sum , 1 => Count
            double[,] NeighborsInfo = new double[2, movies];

            //Rows:Movie ID , Movie Rating ; Columns:# movies
            double[] UserMovies = new double[movies];

            //Zeroing the array
            for (int i = 0; i < movies; i++)
            {
                NeighborsInfo[0, i] = 0;
                NeighborsInfo[1, i] = 0;
            }

            //Calculate the sum and count for each movie and for all neighbors
            for (int i = 0; i < neighbors; i++)
                for (int j = 0; j < movies; j++)
                {
                    if (Ratings[Convert.ToInt16(Neighbors[i]), j] > 0)
                    {
                        NeighborsInfo[0, j] += Ratings[Convert.ToInt16(Neighbors[i]), j];
                        NeighborsInfo[1, j]++;
                    }
                }

            //Calculate the average rating for each movie if seen by more than 10 users
            for (int i = 0; i < movies; i++)
                if (NeighborsInfo[1, i] > 10 && Ratings[UserID, i] == 0)
                    UserMovies[i] = NeighborsInfo[0, i] / NeighborsInfo[1, i];

            return UserMovies;
        }
    }
}
