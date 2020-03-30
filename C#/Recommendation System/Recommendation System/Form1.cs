using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RecommendationSystem;

namespace Recommendation_System
{
    public partial class MainWindow : Form
    {
        // Public variables
        RecommendationEngine RE = new RecommendationEngine() 
        {
            Users = 943,
            Movies = 1682,
            Neighbors = 50,
            NoOfRecommendedMovies = 1682
        };
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowData<T>(ref T[,] DataArray, string ColumnHeader, string RowHeader, int ColumnsCount, int DataGrid)
        {
            if(DataGrid == 1)
            {
                // Clear the datagridview 
                DataPreperationDataGrid.Rows.Clear();

                // Get count of rows
                int RowsNo = DataArray.GetLength(0);

                // If the ColumnsCount is 0 ==> show all columns of DataArray
                if (ColumnsCount == 0)
                    ColumnsCount = DataArray.GetLength(1);

                // Setting column header
                DataPreperationDataGrid.ColumnCount = ColumnsCount + 1;
                DataPreperationDataGrid.Columns[0].Name = RowHeader + "\\" + ColumnHeader;
                for (int i = 1; i < ColumnsCount + 1; i++)
                    DataPreperationDataGrid.Columns[i].Name = ColumnHeader + (i);

                // Process of adding rows
                string[] Row = new string[ColumnsCount + 1];
                for (int i = 0; i < RowsNo; i++)
                {
                    Row[0] = RowHeader + (i + 1);

                    for (int j = 0; j < ColumnsCount; j++)
                        Row[j + 1] = DataArray[i, j].ToString();
                    DataPreperationDataGrid.Rows.Add(Row);
                }
            }
            if (DataGrid == 2)
            {
                // Clear the datagridview 
                RecommendationDataGrid.Rows.Clear();

                // Get count of rows
                int RowsNo = DataArray.GetLength(0);

                // If the ColumnsCount is 0 ==> show all columns of DataArray
                if (ColumnsCount == 0)
                    ColumnsCount = DataArray.GetLength(1);

                // Setting column header
                RecommendationDataGrid.ColumnCount = ColumnsCount + 1;
                RecommendationDataGrid.Columns[0].Name = RowHeader + "\\" + ColumnHeader;
                for (int i = 1; i < ColumnsCount + 1; i++)
                    RecommendationDataGrid.Columns[i].Name = ColumnHeader + (i);

                // Process of adding rows
                string[] Row = new string[ColumnsCount + 1];
                for (int i = 0; i < RowsNo; i++)
                {
                    Row[0] = RowHeader + (i + 1);

                    for (int j = 0; j < ColumnsCount; j++)
                        Row[j + 1] = DataArray[i, j].ToString();
                    RecommendationDataGrid.Rows.Add(Row);
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void DataPreparationTab_Click(object sender, EventArgs e)
        {

        }

        private void RatingsButton_Click(object sender, EventArgs e)
        {
            int[,] Ratings = new int[RE.Users, RE.Movies];
            try
            {
                Ratings = RE.ReadRatingsFile("Ratings.txt");
                ShowData(ref Ratings, "M", "U", 6, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NeighborsButton_Click(object sender, EventArgs e)
        {
            double[,] Neighbors = new double[RE.Users, RE.Neighbors];
            try
            {
                Neighbors = RE.ReadUsersNeighborsFile("Neighbors.txt");
                ShowData(ref Neighbors, "U", "U", 6, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataPreperationDataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 38;
        }

        private void CorrelationButton_Click(object sender, EventArgs e)
        {
            double[,] Correlations = new double[RE.Users, RE.Users];
            try
            {
                Correlations = RE.ReadUsersCorrelationsFile("Correlation.txt");
                ShowData(ref Correlations, "U", "U", 6, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CalculateCorrelations_Click(object sender, EventArgs e)
        {
            int[,] Ratings = new int[RE.Users, RE.Movies];
            double[,] Correlations = new double[RE.Users, RE.Users];
            try
            {
                Ratings = RE.ReadRatingsFile("Ratings.txt");
                Correlations = RE.CalculateCorrelations(Ratings);
                ShowData(ref Correlations, "U", "U", 6, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindNeighbors_Click(object sender, EventArgs e)
        {
            int[,] Ratings = new int[RE.Users, RE.Movies];
            double[,] Correlations = new double[RE.Users, RE.Users];
            double[,] Neighbors = new double[RE.Users, RE.Neighbors];
            try
            {
                Ratings = RE.ReadRatingsFile("Ratings.txt");
                Correlations = RE.ReadUsersCorrelationsFile("Correlation.txt");
                Neighbors = RE.FindNearestNeighbors(Correlations);
                ShowData(ref Neighbors, "U", "U", 6, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RecommendationsBtn_Click(object sender, EventArgs e)
        {
            int[,] Ratings = new int[RE.Users, RE.Movies];
            double[,] Correlations = new double[RE.Users, RE.Users];
            double[,] Neighbors = new double[RE.Users, RE.Neighbors];
            double[,] Recommendations = new double[RE.Users, RE.NoOfRecommendedMovies];

            try
            {
                Ratings = RE.ReadRatingsFile("Ratings.txt");
                Correlations = RE.ReadUsersCorrelationsFile("Correlation.txt");
                Neighbors = RE.ReadUsersNeighborsFile("Neighbors.txt");
                Recommendations = RE.Recommendations(Ratings,Neighbors);
                ShowData(ref Recommendations, "M", "U", 6, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
