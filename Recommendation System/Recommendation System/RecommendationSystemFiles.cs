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
        public double[,] LoadRatingsFile(string ratingsFilePath)
        {
            //Reading all lines in file
            string[] lines = File.ReadAllLines(ratingsFilePath);

            //Read each line and fill it into the ratings array
            string[] DataLine;
            DataLine = lines[0].Split('\t');
            this.Users = int.Parse(DataLine[0]);
            this.Movies = int.Parse(DataLine[1]);
            MessageBox.Show("Users: " + this.Users + "\n" + "Movies: " + this.Movies);

            //Check for errors
            if (this.Users <= 0 || this.Movies <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(ratingsFilePath))
                throw new MissingFieldException("Please enter a file path!");
            if (!File.Exists(ratingsFilePath))
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
         * Write data on a file
         */
         public void CreateRatingsFile(double[,] Ratings, string ratingFilePath)
         {
            using (StreamWriter file = new StreamWriter(@"Ratings.txt", true))
            {
                string dataLine = "";
                for (int i = 0; i < this.Users; i++)
                {
                    dataLine = (i + 1).ToString() + "\t";

                    for (int j = 0; j < this.Movies; j++)
                    {
                        dataLine += Ratings[i, j].ToString() + "\t";
                    }

                    file.WriteLine(dataLine);
                }
            }
         }

        /*
         * Load data from correlation file
         */
        public double[,] LoadUsersCorrelationsFile(string correlationFilePath)
        {
            //Reading all lines in file
            string[] lines = File.ReadAllLines(correlationFilePath);

            //Read each line and fill it into the ratings array
            string[] DataLine;
            DataLine = lines[0].Split('\t');
            this.Users = int.Parse(DataLine[0]);

            //Check for errors
            if (this.Users <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(correlationFilePath))
                throw new MissingFieldException("Please enter a file path!");
            if (!System.IO.File.Exists(correlationFilePath))
                throw new FileNotFoundException("File not found!");

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

            for (int i = 1; i < lines.Length; i++)
            {
                DataLine = lines[i].Split('\t');
                UsersCorrelations[int.Parse(DataLine[0]), int.Parse(DataLine[1])] = double.Parse(DataLine[2]);
                UsersCorrelations[int.Parse(DataLine[1]), int.Parse(DataLine[0])] = double.Parse(DataLine[2]);
            }

            return UsersCorrelations;
        }

        /*
         * Create correlations file
         */
         public void CreateCorrelationsFile(double[,] Correlations, string correlationFilePath)
         {
            using (StreamWriter file = new StreamWriter(@"Correlations.txt", true))
            {
                string dataLine = "";
                for (int i = 0; i < this.Users; i++)
                {
                    dataLine = (i + 1).ToString() + "\t";

                    for (int j = 0; j < this.Users; j++)
                    {
                        dataLine += Correlations[i, j].ToString() + "\t";
                    }

                    file.WriteLine(dataLine);
                }
            }
         }

        /*
         * Load data from neighbors file
         */
        public int[,] LoadUsersNeighborsFile(string neighborsFilePath)
        {
            //Reading all lines in file
            string[] lines = File.ReadAllLines(neighborsFilePath);

            //Read each line and fill it into the ratings array
            string[] DataLine;
            DataLine = lines[0].Split('\t');
            this.Users = int.Parse(DataLine[0]);
            this.Neighbors = int.Parse(DataLine[1]);

            //Check for errors
            if (this.Users <= 0 || this.Neighbors <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(neighborsFilePath))
                throw new MissingFieldException("Please enter a file path!");
            if (!System.IO.File.Exists(neighborsFilePath))
                throw new FileNotFoundException("File not found!");

            int[,] UsersNeighbors = new int[this.Users, this.Neighbors];

            //Read each line and fill it into the users correlation array

            for (int i = 1; i < lines.Length; i++)
            {
                DataLine = lines[i].Split('\t');
                for (int j = 0; j < this.Neighbors; j++)
                    UsersNeighbors[int.Parse(DataLine[0]), j] = int.Parse(DataLine[j + 1]);
            }

            return UsersNeighbors;
        }

        /*
         * Create Neighbors fileNeighbors
         */
        public void CreateNeighborsFile(int[,] Neighbors, string neighborsFilePath)
        {
            using (StreamWriter file = new StreamWriter(@"Neighbors.txt", true))
            {
                string dataLine = "";
                for (int i = 0; i < this.Users; i++)
                {
                    dataLine = (i + 1).ToString() + "\t";

                    for (int j = 0; j < this.Users; j++)
                    {
                        dataLine += Neighbors[i, j].ToString() + "\t";
                    }
                    
                    file.WriteLine(dataLine);
                }
            }
        }

        /*
         * Load data from recommendations file
         */
        public int[,] LoadRecommendationsFile(string recommendationsFilePath)
        {
            //Reading all lines in file
            string[] lines = File.ReadAllLines(recommendationsFilePath);

            //Read each line and fill it into the ratings array
            string[] DataLine;
            DataLine = lines[0].Split('\t');
            this.Users = int.Parse(DataLine[0]);
            this.TopNrecommendations = int.Parse(DataLine[1]);

            //Check for errors
            if (this.Users <= 0 || this.Movies <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(recommendationsFilePath))
                throw new MissingFieldException("Please enter a file path!");
            if (!System.IO.File.Exists(recommendationsFilePath))
                throw new FileNotFoundException("File not found!");

            int[,] RecommendedMovies = new int[this.Users, this.Movies];

            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < this.Movies; j++)
                    RecommendedMovies[i, j] = 0;


                    //Read each line and fill it into the users correlation array

            for (int i = 1; i < lines.Length; i++)
            {
                DataLine = lines[i].Split('\t');
                for (int j = 1; j < this.TopNrecommendations; j++)
                {
                    RecommendedMovies[int.Parse(DataLine[0]), j-1] = int.Parse(DataLine[j]);
                }
            }

            return RecommendedMovies;
        }

        /*
         * Create recommendation file
         */
         public void CreateRecommendationsFile(int[,] RecommendedMovies, string recommendationsFilePath)
         {
            using (StreamWriter file = new StreamWriter(@"Recommendations.txt", true))
            {
                string dataLine = "";
                for (int i = 0; i < this.Users; i++)
                {
                    dataLine = (i + 1).ToString() + "\t";

                    for (int j = 0; j < this.Users; j++)
                    {
                        dataLine += RecommendedMovies[i, j].ToString() + "\t";
                    }

                    file.WriteLine(dataLine);
                }
            }
        }
    }
}
