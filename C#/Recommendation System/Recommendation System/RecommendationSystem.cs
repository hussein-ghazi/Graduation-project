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

        /*
         * Load data from ratings file
         */
        public int[,] LoadRatings()
        {
            //Check for errors
            if(users <= 0 || movies <= 0)
                throw new ArgumentException("Wrong arguments value!");
            if (ratingsFile == "")
                throw new IOException("Empty file name!");

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
            if (correlationFile == "")
                throw new IOException("Empty file name!");

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
            if (NeighborsFile == "")
                throw new IOException("Empty file name!");

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
    }
}
