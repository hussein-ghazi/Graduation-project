using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace LoadRSFiles
{
    class LoadRSFile
    {
        private int users, movies, neighbors ;

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

        public LoadRSFile()
        {
            this.users = 0;
            this.movies = 0;
            this.neighbors = 0;
        }

        ~LoadRSFile()
        {

        }

        /*
         * Load data from ratings file
         */
        public int[,] ReadRatingsFile(string ratingsFile)
        {
            //Check for errors
            if (users <= 0 || movies <= 0)
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

        public double[,] ReadJesterRatingsFile(string ratingsFile)
        {
            //Check for errors
            if (users <= 0 || movies <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(ratingsFile))
                throw new MissingFieldException("Please enter a file path!");
            if (!System.IO.File.Exists(ratingsFile))
                throw new FileNotFoundException("File not found!");

            //Set and initialize ratings array
            double[,] Ratings = new double[users, movies];
            //Reading all lines in file
            string[] lines = System.IO.File.ReadAllLines(ratingsFile);

            for (int i = 0; i < users; i++)
                for (int j = 0; j < movies; j++)
                    Ratings[i, j] = 99;

            //Read each line and fill it into the ratings array
            string[] DataLine;
            int userIndex = 0;
            foreach (string line in lines)
            {
                if (userIndex == 5000)
                    break;
                DataLine = line.Split('\t');
                
                for(int i = 1; i < movies; i++)
                {
                    Ratings[userIndex, i-1] = double.Parse(DataLine[i]);
                }
                userIndex++;
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
        public int[,] ReadUsersNeighborsFile(string neighborsFile)
        {
            //Check for errors
            if (users <= 0 || neighbors <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(neighborsFile))
                throw new MissingFieldException("Please enter a file path!");
            if (!System.IO.File.Exists(neighborsFile))
                throw new FileNotFoundException("File not found!");

            //Read all lines from file
            string[] lines = System.IO.File.ReadAllLines(neighborsFile);

            int[,] UsersNeighbors = new int[users, neighbors];

            //Read each line and fill it into the users correlation array
            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');
                for (int i = 0; i < neighbors; i++)
                    UsersNeighbors[int.Parse(DataLine[0]), i] = int.Parse(DataLine[i + 1]);
            }
            return UsersNeighbors;
        }
    }
}
