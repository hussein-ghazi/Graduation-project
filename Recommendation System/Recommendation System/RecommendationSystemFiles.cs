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
         * Write correlations file
         */
         public void WriteCorrelationsFile(double[,] Correlations, string correlationFilePath)
         {
            this.Users = Correlations.GetLength(0);

            using (StreamWriter file = new StreamWriter(correlationFilePath + @"\Correlations.txt", true))
            {
                file.WriteLine(this.Users);
                string dataLine = "";
                for (int i = 0; i < this.Users; i++)
                {
                    for (int j = i + 1; j < this.Users; j++)
                    {
                        dataLine = i.ToString() + "\t" + j.ToString() + "\t" + Correlations[i, j];
                        file.WriteLine(dataLine);
                    }
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
         * Write Neighbors fileNeighbors
         */
        public void WriteNeighborsFile(int[,] Neighbors, string neighborsFilePath)
        {
            using (StreamWriter file = new StreamWriter(neighborsFilePath + @"\Neighbors.txt", true))
            {
                this.Users = Neighbors.GetLength(0);
                this.Neighbors = Neighbors.GetLength(1);

                string dataLine = "";
                file.WriteLine(this.Users.ToString() + "\t" + this.Neighbors.ToString());
                for (int i = 0; i < this.Users; i++)
                {
                    dataLine = i.ToString() + "\t";
                    for (int j = 0; j < this.Neighbors; j++)
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
            this.N = int.Parse(DataLine[1]);

            //Check for errors
            if (this.Users <= 0 || this.N <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(recommendationsFilePath))
                throw new MissingFieldException("Please enter a file path!");
            if (!File.Exists(recommendationsFilePath))
                throw new FileNotFoundException("File not found!");

            //Set and initialize ratings array
            int[,] Recommendations = new int[this.Users, this.N];

            for (int i = 0; i < this.Users; i++)
                for (int j = 0; j < this.N; j++)
                    Recommendations[i, j] = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                DataLine = lines[i].Split('\t');
                Recommendations[int.Parse(DataLine[0]), int.Parse(DataLine[1])] = int.Parse(DataLine[2]);
            }
            return Recommendations;
        }

        /*
         * Load data from Predctive file
         */
        public double[,] LoadPredctiveFile(string predctiveFilePath)
        {
            //Reading all lines in file
            string[] lines = File.ReadAllLines(predctiveFilePath);

            //Read each line and fill it into the ratings array
            string[] DataLine;
            DataLine = lines[0].Split('\t');
            this.Users = int.Parse(DataLine[0]);
            this.Movies = int.Parse(DataLine[1]);

            //Check for errors
            if (this.Users <= 0 || this.Movies <= 0)
                throw new IOException("Please enter appropriate numbers");
            if (string.IsNullOrEmpty(predctiveFilePath))
                throw new MissingFieldException("Please enter a file path!");
            if (!File.Exists(predctiveFilePath))
                throw new FileNotFoundException("File not found!");

            //Set and initialize ratings array
            double[,] PredctiveMatrix = new double[this.Users, this.Movies];

            for (int i = 0; i < this.Users; i++)
                for (int j = 0; j < this.Movies; j++)
                    PredctiveMatrix[i, j] = 0.0;

            for (int i = 1; i < lines.Length; i++)
            {
                DataLine = lines[i].Split('\t');
                PredctiveMatrix[int.Parse(DataLine[0]), int.Parse(DataLine[1])] = double.Parse(DataLine[2]);
            }
            return PredctiveMatrix;
        }

        /*
         * Write recommendation file
         */
        public void WriteRecommendationsFile(int[,] RecommendedMovies, string recommendationsFilePath)
        {
            this.Users = RecommendedMovies.GetLength(0);
            this.N = RecommendedMovies.GetLength(1);

            using (StreamWriter file = new StreamWriter(recommendationsFilePath + @"\Recommendations.txt", true))
            {
                file.WriteLine(this.Users.ToString() + "\t" + this.N.ToString());

                string dataLine = "";
                for (int i = 0; i < this.Users; i++)
                {
                    for (int j = 0; j < this.N; j++)
                    {
                        if (RecommendedMovies[i, j] != 0)
                        {
                            dataLine = i.ToString() + "\t" + j.ToString() + "\t" + RecommendedMovies[i, j].ToString();
                            file.WriteLine(dataLine);
                        }
                    }
                }
            }
        }

        /*
        * Write Predctive file
        */
        public void WritePredctiveFile(double[,] PredctiveMatrix, string predctiveFilePath)
        {
            this.Users = PredctiveMatrix.GetLength(0);
            this.Movies = PredctiveMatrix.GetLength(1);

            using(StreamWriter file = new StreamWriter(predctiveFilePath + @"\PredctiveMatrix.txt", true))
            {
                file.WriteLine(this.Users.ToString() + "\t" + this.Movies.ToString());

                string dataLine = "";
                for (int i = 0; i < this.Users; i++)
                {
                    for (int j = 0; j < this.Movies; j++)
                    {
                        if(PredctiveMatrix[i,j] != 0)
                        {
                            dataLine = i.ToString() + "\t" + j.ToString() + "\t" + PredctiveMatrix[i, j].ToString();
                            file.WriteLine(dataLine);
                        }
                    }
                }
            }
        }
    }
}
