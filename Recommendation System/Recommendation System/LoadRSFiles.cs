using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using RecommendationSystem;

namespace RecommendationSystemFiles
{
    class RSFile : RecommendationEngine
    {
        public RSFile()
        {
            this.Users = 0;
            this.Movies = 0;
            this.Neighbors = 0;
        }

        ~RSFile()
        {

        }

        /*
         * Load data from ratings file
         */
        public double[,] LoadRatingsFile(string ratingsFile)
        {
            //Reading all lines in file
            string[] lines = File.ReadAllLines(ratingsFile);

            //Read each line and fill it into the ratings array
            string[] DataLine;
            DataLine = lines[0].Split('\t');
            this.Users = int.Parse(DataLine[0]);
            this.Movies = int.Parse(DataLine[1]);

            //Check for errors
            if (this.Users <= 0 || this.Movies <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(ratingsFile))
                throw new MissingFieldException("Please enter a file path!");
            if (!File.Exists(ratingsFile))
                throw new FileNotFoundException("File not found!");

            //Set and initialize ratings array
            double[,] Ratings = new double[this.Users, this.Movies];
            
            for (int i = 0; i < this.Users; i++)
                for (int j = 0; j < this.Movies; j++)
                    Ratings[i, j] = 0.0;

            for (int i = 1; i < lines.Length; i++)
            {
                DataLine = lines[i].Split('\t');
                Ratings[int.Parse(DataLine[0]) - 1, int.Parse(DataLine[1]) - 1] = double.Parse(DataLine[2]);
            }

            return Ratings;
        }

        /*
         * Load data from correlation file
         */
        public double[,] ReadUsersCorrelationsFile(string correlationFile)
        {
            //Check for errors
            if (this.Users <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(correlationFile))
                throw new MissingFieldException("Please enter a file path!");
            if (!System.IO.File.Exists(correlationFile))
                throw new FileNotFoundException("File not found!");

            //Read all lines from file
            string[] lines = System.IO.File.ReadAllLines(correlationFile);


            //Set and initialize the users correlation array
            double[,] UsersCorrelations = new double[this.Users, this.Users];

            for (int i = 0; i < this.Users; i++)
                for (int j = 0; j < this.Users; j++)
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
            if (this.Users <= 0 || this.Neighbors <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(neighborsFile))
                throw new MissingFieldException("Please enter a file path!");
            if (!System.IO.File.Exists(neighborsFile))
                throw new FileNotFoundException("File not found!");

            //Read all lines from file
            string[] lines = System.IO.File.ReadAllLines(neighborsFile);

            int[,] UsersNeighbors = new int[this.Users, this.Neighbors];

            //Read each line and fill it into the users correlation array
            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');
                for (int i = 0; i < this.Neighbors; i++)
                    UsersNeighbors[int.Parse(DataLine[0]), i] = int.Parse(DataLine[i + 1]);
            }
            return UsersNeighbors;
        }
    }
}
