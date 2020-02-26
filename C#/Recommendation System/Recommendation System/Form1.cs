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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowData<T>(ref T[,] DataArray, string ColumnHeader, string RowHeader, int ColumnsCount)
        {
            //Clear the datagridview 
            DataPreperationDataGrid.Rows.Clear();

            int RowsNo = DataArray.GetLength(0);

            //If the ColumnsCount is 0 ==> show all columns of DataArray
            if (ColumnsCount == 0)
                ColumnsCount = DataArray.GetLength(1);

            //Setting column header
            DataPreperationDataGrid.ColumnCount = ColumnsCount + 1;
            DataPreperationDataGrid.Columns[0].Name = RowHeader + "\\" + ColumnHeader;
            for (int i = 1; i < ColumnsCount + 1; i++)
                DataPreperationDataGrid.Columns[i].Name = ColumnHeader + (i);

            //Process of adding rows
            string[] Row = new string[ColumnsCount + 1];
            for (int i = 0; i < RowsNo; i++)
            {
                Row[0] = RowHeader + (i + 1);

                for (int j = 0; j < ColumnsCount; j++)
                    Row[j + 1] = DataArray[i, j].ToString();
                DataPreperationDataGrid.Rows.Add(Row);
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
            Recommendation R = new Recommendation();
            R.Users = 943; R.Movies = 1682;
            R.RatingsFile = "Ratings.txt";

            int[,] temp = new int[R.Users, R.Movies];
            temp = R.LoadRatings();
            ShowData(ref temp, "M ", "U ", 0);
            
        }

        private void NeighborsButton_Click(object sender, EventArgs e)
        {
            
        }

        private void DataPreperationDataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 38;
        }
    }
}
