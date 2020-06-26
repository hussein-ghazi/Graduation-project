using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Reading all lines in file
            string[] lines = File.ReadAllLines(@"ratings.txt");
            string[] DataLine;
            string temp = "";

            int users = 0;
            int movies = 0;
            int count = 0; 

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Dataset.txt", true))
            {
                foreach(string line in lines)
                {
                    DataLine = line.Split(' ');
                    temp = DataLine[0] + "\t" + DataLine[1] + "\t" + DataLine[2] + "\t";

                    if (int.Parse(DataLine[0]) > users)
                        users = int.Parse(DataLine[0]);

                    if (int.Parse(DataLine[1]) > movies)
                        movies = int.Parse(DataLine[1]);

                    count++;

                    file.WriteLine(temp);
                }
            }

            Console.WriteLine("Count: " + count);
            Console.WriteLine("Users: " + users);
            Console.WriteLine("Movies: " + movies);
        }
    }
}
