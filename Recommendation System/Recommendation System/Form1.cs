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
using LoadRSFiles;
using EvaluationModel;

namespace Recommendation_System
{
    public partial class MainWindow : Form
    {
        // Public variables
        RecommendationEngine RE = new RecommendationEngine() 
        {
            Neighbors = 50,
            TopNrecommendations = 1682
        };

        LoadRSFile RSF = new LoadRSFile()
        {
            Users = 943,
            Movies = 1682,
            Neighbors = 50,
        };

        Evaluation Ev = new Evaluation()
        {
            Users = 943,
            Movies = 1682,
            Neighbors = 50
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
                LoadFilesDG.Rows.Clear();

                // Get count of rows
                int RowsNo = DataArray.GetLength(0);

                // If the ColumnsCount is 0 ==> show all columns of DataArray
                if (ColumnsCount == 0)
                    ColumnsCount = DataArray.GetLength(1);

                // Setting column header
                LoadFilesDG.ColumnCount = ColumnsCount + 1;
                LoadFilesDG.Columns[0].Name = RowHeader + "\\" + ColumnHeader;
                for (int i = 1; i < ColumnsCount + 1; i++)
                    LoadFilesDG.Columns[i].Name = ColumnHeader + (i);

                // Process of adding rows
                string[] Row = new string[ColumnsCount + 1];
                for (int i = 0; i < RowsNo; i++)
                {
                    Row[0] = RowHeader + (i + 1);

                    for (int j = 0; j < ColumnsCount; j++)
                        Row[j + 1] = DataArray[i, j].ToString();
                    LoadFilesDG.Rows.Add(Row);
                }
            }
            if (DataGrid == 2)
            {
                // Clear the datagridview 
                RecommendationDG.Rows.Clear();

                // Get count of rows
                int RowsNo = DataArray.GetLength(0);

                // If the ColumnsCount is 0 ==> show all columns of DataArray
                if (ColumnsCount == 0)
                    ColumnsCount = DataArray.GetLength(1);

                // Setting column header
                RecommendationDG.ColumnCount = ColumnsCount + 1;
                RecommendationDG.Columns[0].Name = RowHeader + "\\" + ColumnHeader;
                for (int i = 1; i < ColumnsCount + 1; i++)
                    RecommendationDG.Columns[i].Name = ColumnHeader + (i);

                // Process of adding rows
                string[] Row = new string[ColumnsCount + 1];
                for (int i = 0; i < RowsNo; i++)
                {
                    Row[0] = RowHeader + (i + 1);

                    for (int j = 0; j < ColumnsCount; j++)
                        Row[j + 1] = DataArray[i, j].ToString();
                    RecommendationDG.Rows.Add(Row);
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
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        int[,] Ratings = RSF.ReadRatingsFile(filePath);
                        ShowData(ref Ratings, "M", "U", 10, 1);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NeighborsButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        int[,] Neighbors = RSF.ReadUsersNeighborsFile(filePath);
                        ShowData(ref Neighbors, "U", "U", 10, 1);
                    }
                }
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
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        double[,] Correlations = RSF.ReadUsersCorrelationsFile(filePath);
                        ShowData(ref Correlations, "U", "U", 10, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CalculateCorrelations_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Select ratings file please.");
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;

                        int[,] Ratings = RSF.ReadRatingsFile(filePath);
                        double[,] Correlations = RE.CalculateCorrelations(Ratings);
                        ShowData(ref Correlations, "U", "U", 10, 2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindNeighbors_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Select correlations file please.");
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        double[,] Correlations = RSF.ReadUsersCorrelationsFile(filePath);
                        double[,] Neighbors = RE.FindNeighbors(Correlations);
                        ShowData(ref Neighbors, "U", "U", 10, 2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RecommendationsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int[,] Ratings = new int[RSF.Users,RSF.Movies];
                int[,] Neighbors = new int[RSF.Users,RSF.Neighbors];

                MessageBox.Show("Select ratings file please.");
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    Ratings = RSF.ReadRatingsFile(filePath);
                }

                MessageBox.Show("Select neighbors file please.");
                openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    Neighbors = RSF.ReadUsersNeighborsFile(filePath);
                }

                double[,] Recommendations = RE.Recommendations(Ratings, Neighbors);
                ShowData(ref Recommendations, "M", "U", 10, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // test code for saving files
        private void SelectFolderBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = "Text Files (*.txt)|*.txt|Comma-Separated Values  (*.csv)|*.csv";
            SFD.DefaultExt = "txt";
            SFD.AddExtension = true;
            //SFD.FileName = "Correlations";

            if (SFD.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(SFD.FileName);
            }
        }

        private void SFD_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void EvaluationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int[,] Ratings = new int[Ev.Users, Ev.Movies];
                int[,] Neighbors = new int[Ev.Users, Ev.Neighbors];

                MessageBox.Show("Select ratings file please.");
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    Ratings = RSF.ReadRatingsFile(filePath);
                }

                MessageBox.Show("Select neighbors file please.");
                openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    Neighbors = RSF.ReadUsersNeighborsFile(filePath);
                }

                string RemovedRatingsFile = "";
                MessageBox.Show("Select path to save removed ratings file");
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.ShowNewFolderButton = false;
                folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
                DialogResult result = folderBrowserDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    RemovedRatingsFile = folderBrowserDialog1.SelectedPath;
                }

                RemovedRatingsFile += "\\RemovedRatingsFile.txt";

                int[,] TestingData = Ev.GenerateTestingData(Ratings, RemovedRatingsFile);
                double[,] PredictiveRatings = Ev.RatingsPrediction(Ratings, Neighbors);
                double MAE = Ev.MAE(Ratings, PredictiveRatings, RemovedRatingsFile);

                MessageBox.Show(MAE.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
