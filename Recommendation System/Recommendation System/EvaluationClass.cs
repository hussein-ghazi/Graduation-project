using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecommendationSystem;
using System.IO;

namespace EvaluationModel
{
    class Evaluation : RecommendationEngine
    {
        public int[,] GenerateTestingData(int[,] Ratings, string RemovedRatingsFile)
        {
            // created to generate random movie index
            Random randomObj = new Random();
            int randomMovieIndex;

            // count of removed ratings for each user
            int RemovedRatings = 0;

            // record in Removed Ratings File -->  U    M   M   M
            string userMoviesString;

            int[,] TestingData = new int[this.Users, this.Movies];

            // each value represnts number of rated movies for each user
            // RatingsCount[i] = number of rated movies for Users[i]
            int[] RatingsCount = new int[this.Users];

            for (int i = 0; i < this.Users; i++)
                RatingsCount[i] = 0;

            // number of rated movies for each user
            for (int i = 0; i < this.Users; i++)
                for (int j = 0; j < this.Movies; j++)
                {
                    TestingData[i, j] = Ratings[i, j];

                    if (Ratings[i, j] > 0)
                        RatingsCount[i]++;
                }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(RemovedRatingsFile))
            {
                for (int i = 0 ; i < this.Users ; i++)
                {
                    RemovedRatings = RatingsCount[i] / 5;
                    userMoviesString = i.ToString() + "\t";

                    while (RemovedRatings > 0)
                    {
                        randomMovieIndex = randomObj.Next(0, this.Movies);
                        if (TestingData[i, randomMovieIndex] != 0)
                        {
                            userMoviesString += randomMovieIndex.ToString() + "\t";
                            TestingData[i, randomMovieIndex] = 0;
                            RemovedRatings--;
                        }
                    }
                    file.WriteLine(userMoviesString);
                }
            }
            return TestingData;
        }

        public double[,] RatingsPrediction(int[,] Ratings,int[,] NeighborsMatrix)
        {
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
                for (int j = 0; j < this.Neighbors ; j++)
                    for (int k = 0; k < this.Movies ; k++)
                    {
                        if (Ratings[Convert.ToInt16(NeighborsMatrix[i, j]), k] > 0)
                        {
                            TempNeighborsInfo[i * 2, k] += Ratings[Convert.ToInt16(NeighborsMatrix[i, j]), k];
                            TempNeighborsInfo[i * 2 + 1, k]++;
                        }
                    }
            }

            for (int i = 0; i < this.Users; i++)
                for (int j = 0; j < this.Movies; j++)
                    if (TempNeighborsInfo[i * 2 + 1, j] > 10 && PredictiveRatings[i, j] == 0)
                        PredictiveRatings[i, j] = TempNeighborsInfo[i * 2, j] / TempNeighborsInfo[i * 2 + 1, j];
                    else
                        PredictiveRatings[i, j] = 0;

            return PredictiveRatings;
        }

        public double[] MAE(int[,] Ratings,double[,] PredictiveRatings, string RemovedRatingsFile)
        {
            string[] lines = File.ReadAllLines(RemovedRatingsFile);
            double sum = 0;
            int count = 0;
            int userIndex, movieIndex;
            int predictiveZeroRatedCount = 0;
            double[] MAEStatistics = new double[3];

            //Read each line and fill it into the ratings array
            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');
                userIndex = int.Parse(DataLine[0]);
                for (int j = 1; j < DataLine.Length - 1; j++)
                {
                    movieIndex = int.Parse(DataLine[j]);
                    sum += Math.Abs(PredictiveRatings[userIndex, movieIndex] - Ratings[userIndex, movieIndex]);
                    count++;
                    if (PredictiveRatings[userIndex, movieIndex] == 0)
                        predictiveZeroRatedCount++;
                }
            }

            MAEStatistics[0] = sum / count;
            MAEStatistics[1] = count;
            MAEStatistics[2] = predictiveZeroRatedCount;

            return MAEStatistics;
        }

        public double[] RMSE(int[,] Ratings, double[,] PredictiveRatings, string RemovedRatingsFile)
        {
            string[] lines = File.ReadAllLines(RemovedRatingsFile);
            double sum = 0;
            int count = 0;
            int userIndex, movieIndex;
            int predictiveZeroRatedCount = 0;
            double[] RMSEStatistics = new double[3];

            //Read each line and fill it into the ratings array
            string[] DataLine;
            foreach (string line in lines)
            {
                DataLine = line.Split('\t');
                userIndex = int.Parse(DataLine[0]);
                for (int j = 1; j < DataLine.Length - 1; j++)
                {
                    movieIndex = int.Parse(DataLine[j]);
                    sum += Math.Pow(PredictiveRatings[userIndex, movieIndex] - Ratings[userIndex, movieIndex], 2);
                    count++;
                    if (PredictiveRatings[userIndex, movieIndex] == 0)
                        predictiveZeroRatedCount++;
                }
            }

            RMSEStatistics[0] = Math.Sqrt(sum / count);
            RMSEStatistics[1] = count;
            RMSEStatistics[2] = predictiveZeroRatedCount;

            return RMSEStatistics;
        }
    }
}
