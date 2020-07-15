using System;

namespace RecommendationSystem
{
    class RecommendationEngine
    {
        private int users, movies, neighbors, n;

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

        public int N
        {
            get { return n; }
            set { n = value; }
        }

        public RecommendationEngine()
        {
            this.users = 0;
            this.movies = 0;
            this.neighbors = 0;
            this.n = 0;
        }

        ~RecommendationEngine()
        {

        }

        /*
        * Calculate pearson correlation among all users
        */
        public double[,] CalculateCorrelations(double[,] Ratings)
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
            double[,] UsersCorrelation = new double[users, users];
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
        public int[,] FindNeighbors(double[,] Correlations)
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
            int[,] UserNeighbors = new int[users, neighbors];

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
        public int[,] TopNRecommendations(double[,] Ratings, int[,] Neighbors)
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
            if (n <= 0)
                throw new InvalidTopNrecommendationsValueException("TopNrecommendations count is zero or less!");

            // index 0 ==> summation
            // index 1 ==> counter
            double[,] TempNeighborsInfo = new double[users * 2, movies];
            double[,] NeighborsInfo = new double[users, movies];

            // Rows:Users ; Columns:# of Recommended movies
            int[,] RecommendedMovies = new int[users, n];

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
                for (int j = 0; j < n; j++)
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
         * PredictiveMatrix
         */
        public double[,] PredictiveMatrix(double[,] Ratings, int[,] NeighborsMatrix)
        {
            this.Users = Ratings.GetLength(0);
            this.Movies = Ratings.GetLength(1);
            this.Neighbors = NeighborsMatrix.GetLength(1);

            //index 0 ==> summation
            //index 1 ==> counter
            double[,] TempNeighborsInfo = new double[this.Users * 2, this.Movies];
            double[,] PredictiveRatings = new double[this.Users, this.Movies];

            for (int i = 0; i < this.Users; i++)
                for (int j = 0; j < this.Movies; j++)
                {
                    TempNeighborsInfo[i * 2, j] = 0;
                    TempNeighborsInfo[i * 2 + 1, j] = 0;
                }

            for (int i = 0; i < this.Users; i++)
            {
                for (int j = 0; j < this.Neighbors; j++)
                    for (int k = 0; k < this.Movies; k++)
                    {
                        if(Ratings[NeighborsMatrix[i, j], k] > 0)
                        {
                            TempNeighborsInfo[i * 2, k] += Ratings[NeighborsMatrix[i, j], k];
                            TempNeighborsInfo[i * 2 + 1, k]++;
                        }
                    }
            }

            for (int i = 0; i < this.Users; i++)
                for (int j = 0; j < this.Movies; j++)
                    if (TempNeighborsInfo[i * 2 + 1, j] > 10 && Ratings[i,j] == 0)
                        PredictiveRatings[i, j] = TempNeighborsInfo[i * 2, j] / TempNeighborsInfo[i * 2 + 1, j];
                    else
                        PredictiveRatings[i, j] = 0;

            return PredictiveRatings;
        }
    }


    /*
     *  The exceptions classes 
     */

    // If users attribute is zero or less
    public class InvalidUsersValueException : Exception
    {
        public InvalidUsersValueException()
        {
        }

        public InvalidUsersValueException(string message) : base(message)
        {
        }

        public InvalidUsersValueException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    // If movies attribute is zero or less
    public class InvalidMoviesValueException : Exception
    {
        public InvalidMoviesValueException()
        {
        }

        public InvalidMoviesValueException(string message) : base(message)
        {
        }

        public InvalidMoviesValueException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    // If neighbors attribute is zero or less
    public class InvalidNeighborsValueException : Exception
    {
        public InvalidNeighborsValueException()
        {
        }

        public InvalidNeighborsValueException(string message) : base(message)
        {
        }

        public InvalidNeighborsValueException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    // If RecommendedMoviesCount attribute is zero or less
    public class InvalidTopNrecommendationsValueException : Exception
    {
        public InvalidTopNrecommendationsValueException()
        {
        }

        public InvalidTopNrecommendationsValueException(string message) : base(message)
        {
        }

        public InvalidTopNrecommendationsValueException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
