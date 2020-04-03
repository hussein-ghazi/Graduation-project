namespace Recommendation_System
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl = new System.Windows.Forms.TabControl();
            this.DataPreparationTab = new System.Windows.Forms.TabPage();
            this.NeighborsButton = new System.Windows.Forms.Button();
            this.CorrelationButton = new System.Windows.Forms.Button();
            this.RatingsButton = new System.Windows.Forms.Button();
            this.LoadFilesDG = new System.Windows.Forms.DataGridView();
            this.RecommendationTab = new System.Windows.Forms.TabPage();
            this.RecommendationsBtn = new System.Windows.Forms.Button();
            this.FindNeighbors = new System.Windows.Forms.Button();
            this.CalculateCorrelations = new System.Windows.Forms.Button();
            this.RecommendationDG = new System.Windows.Forms.DataGridView();
            this.WriteFiles = new System.Windows.Forms.TabPage();
            this.SFD = new System.Windows.Forms.SaveFileDialog();
            this.SelectFolderBtn = new System.Windows.Forms.Button();
            this.TabControl.SuspendLayout();
            this.DataPreparationTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoadFilesDG)).BeginInit();
            this.RecommendationTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecommendationDG)).BeginInit();
            this.WriteFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.DataPreparationTab);
            this.TabControl.Controls.Add(this.RecommendationTab);
            this.TabControl.Controls.Add(this.WriteFiles);
            this.TabControl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl.Location = new System.Drawing.Point(12, 12);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(760, 537);
            this.TabControl.TabIndex = 0;
            // 
            // DataPreparationTab
            // 
            this.DataPreparationTab.Controls.Add(this.NeighborsButton);
            this.DataPreparationTab.Controls.Add(this.CorrelationButton);
            this.DataPreparationTab.Controls.Add(this.RatingsButton);
            this.DataPreparationTab.Controls.Add(this.LoadFilesDG);
            this.DataPreparationTab.Location = new System.Drawing.Point(4, 25);
            this.DataPreparationTab.Name = "DataPreparationTab";
            this.DataPreparationTab.Padding = new System.Windows.Forms.Padding(3);
            this.DataPreparationTab.Size = new System.Drawing.Size(752, 508);
            this.DataPreparationTab.TabIndex = 0;
            this.DataPreparationTab.Text = "Load data from files";
            this.DataPreparationTab.UseVisualStyleBackColor = true;
            this.DataPreparationTab.Click += new System.EventHandler(this.DataPreparationTab_Click);
            // 
            // NeighborsButton
            // 
            this.NeighborsButton.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.NeighborsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NeighborsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NeighborsButton.ForeColor = System.Drawing.Color.White;
            this.NeighborsButton.Location = new System.Drawing.Point(4, 468);
            this.NeighborsButton.Name = "NeighborsButton";
            this.NeighborsButton.Size = new System.Drawing.Size(229, 37);
            this.NeighborsButton.TabIndex = 3;
            this.NeighborsButton.Text = "Load Neighbors";
            this.NeighborsButton.UseVisualStyleBackColor = false;
            this.NeighborsButton.Click += new System.EventHandler(this.NeighborsButton_Click);
            // 
            // CorrelationButton
            // 
            this.CorrelationButton.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.CorrelationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CorrelationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CorrelationButton.ForeColor = System.Drawing.Color.White;
            this.CorrelationButton.Location = new System.Drawing.Point(260, 468);
            this.CorrelationButton.Name = "CorrelationButton";
            this.CorrelationButton.Size = new System.Drawing.Size(229, 37);
            this.CorrelationButton.TabIndex = 2;
            this.CorrelationButton.Text = "Load Correlations";
            this.CorrelationButton.UseVisualStyleBackColor = false;
            this.CorrelationButton.Click += new System.EventHandler(this.CorrelationButton_Click);
            // 
            // RatingsButton
            // 
            this.RatingsButton.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.RatingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RatingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RatingsButton.ForeColor = System.Drawing.Color.White;
            this.RatingsButton.Location = new System.Drawing.Point(520, 468);
            this.RatingsButton.Name = "RatingsButton";
            this.RatingsButton.Size = new System.Drawing.Size(229, 37);
            this.RatingsButton.TabIndex = 1;
            this.RatingsButton.Text = "Load Ratings";
            this.RatingsButton.UseVisualStyleBackColor = false;
            this.RatingsButton.Click += new System.EventHandler(this.RatingsButton_Click);
            // 
            // LoadFilesDG
            // 
            this.LoadFilesDG.AllowUserToAddRows = false;
            this.LoadFilesDG.AllowUserToDeleteRows = false;
            this.LoadFilesDG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LoadFilesDG.Location = new System.Drawing.Point(0, 0);
            this.LoadFilesDG.Name = "LoadFilesDG";
            this.LoadFilesDG.ReadOnly = true;
            this.LoadFilesDG.Size = new System.Drawing.Size(752, 462);
            this.LoadFilesDG.TabIndex = 0;
            this.LoadFilesDG.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataPreperationDataGrid_ColumnAdded);
            // 
            // RecommendationTab
            // 
            this.RecommendationTab.Controls.Add(this.RecommendationsBtn);
            this.RecommendationTab.Controls.Add(this.FindNeighbors);
            this.RecommendationTab.Controls.Add(this.CalculateCorrelations);
            this.RecommendationTab.Controls.Add(this.RecommendationDG);
            this.RecommendationTab.Location = new System.Drawing.Point(4, 25);
            this.RecommendationTab.Name = "RecommendationTab";
            this.RecommendationTab.Padding = new System.Windows.Forms.Padding(3);
            this.RecommendationTab.Size = new System.Drawing.Size(752, 508);
            this.RecommendationTab.TabIndex = 1;
            this.RecommendationTab.Text = "Recommendation";
            this.RecommendationTab.UseVisualStyleBackColor = true;
            // 
            // RecommendationsBtn
            // 
            this.RecommendationsBtn.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.RecommendationsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RecommendationsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecommendationsBtn.ForeColor = System.Drawing.Color.White;
            this.RecommendationsBtn.Location = new System.Drawing.Point(260, 468);
            this.RecommendationsBtn.Name = "RecommendationsBtn";
            this.RecommendationsBtn.Size = new System.Drawing.Size(229, 37);
            this.RecommendationsBtn.TabIndex = 6;
            this.RecommendationsBtn.Text = "Recommndations";
            this.RecommendationsBtn.UseVisualStyleBackColor = false;
            this.RecommendationsBtn.Click += new System.EventHandler(this.RecommendationsBtn_Click);
            // 
            // FindNeighbors
            // 
            this.FindNeighbors.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.FindNeighbors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindNeighbors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindNeighbors.ForeColor = System.Drawing.Color.White;
            this.FindNeighbors.Location = new System.Drawing.Point(520, 468);
            this.FindNeighbors.Name = "FindNeighbors";
            this.FindNeighbors.Size = new System.Drawing.Size(229, 37);
            this.FindNeighbors.TabIndex = 5;
            this.FindNeighbors.Text = "Find Neighbors";
            this.FindNeighbors.UseVisualStyleBackColor = false;
            this.FindNeighbors.Click += new System.EventHandler(this.FindNeighbors_Click);
            // 
            // CalculateCorrelations
            // 
            this.CalculateCorrelations.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.CalculateCorrelations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CalculateCorrelations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalculateCorrelations.ForeColor = System.Drawing.Color.White;
            this.CalculateCorrelations.Location = new System.Drawing.Point(3, 468);
            this.CalculateCorrelations.Name = "CalculateCorrelations";
            this.CalculateCorrelations.Size = new System.Drawing.Size(229, 37);
            this.CalculateCorrelations.TabIndex = 4;
            this.CalculateCorrelations.Text = "Calculate Correlations";
            this.CalculateCorrelations.UseVisualStyleBackColor = false;
            this.CalculateCorrelations.Click += new System.EventHandler(this.CalculateCorrelations_Click);
            // 
            // RecommendationDG
            // 
            this.RecommendationDG.AllowUserToAddRows = false;
            this.RecommendationDG.AllowUserToDeleteRows = false;
            this.RecommendationDG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RecommendationDG.Location = new System.Drawing.Point(0, 0);
            this.RecommendationDG.Name = "RecommendationDG";
            this.RecommendationDG.ReadOnly = true;
            this.RecommendationDG.Size = new System.Drawing.Size(752, 462);
            this.RecommendationDG.TabIndex = 1;
            // 
            // WriteFiles
            // 
            this.WriteFiles.BackColor = System.Drawing.Color.Transparent;
            this.WriteFiles.Controls.Add(this.SelectFolderBtn);
            this.WriteFiles.Location = new System.Drawing.Point(4, 25);
            this.WriteFiles.Name = "WriteFiles";
            this.WriteFiles.Padding = new System.Windows.Forms.Padding(3);
            this.WriteFiles.Size = new System.Drawing.Size(752, 508);
            this.WriteFiles.TabIndex = 2;
            this.WriteFiles.Text = "Write data on files";
            // 
            // SelectFolderBtn
            // 
            this.SelectFolderBtn.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.SelectFolderBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectFolderBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectFolderBtn.ForeColor = System.Drawing.Color.White;
            this.SelectFolderBtn.Location = new System.Drawing.Point(3, 6);
            this.SelectFolderBtn.Name = "SelectFolderBtn";
            this.SelectFolderBtn.Size = new System.Drawing.Size(229, 37);
            this.SelectFolderBtn.TabIndex = 5;
            this.SelectFolderBtn.Text = "Select folder";
            this.SelectFolderBtn.UseVisualStyleBackColor = false;
            this.SelectFolderBtn.Click += new System.EventHandler(this.SelectFolderBtn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recommendation System";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.TabControl.ResumeLayout(false);
            this.DataPreparationTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LoadFilesDG)).EndInit();
            this.RecommendationTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RecommendationDG)).EndInit();
            this.WriteFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage DataPreparationTab;
        private System.Windows.Forms.TabPage RecommendationTab;
        private System.Windows.Forms.DataGridView LoadFilesDG;
        private System.Windows.Forms.DataGridView RecommendationDG;
        private System.Windows.Forms.Button NeighborsButton;
        private System.Windows.Forms.Button CorrelationButton;
        private System.Windows.Forms.Button RatingsButton;
        private System.Windows.Forms.Button CalculateCorrelations;
        private System.Windows.Forms.Button FindNeighbors;
        private System.Windows.Forms.Button RecommendationsBtn;
        private System.Windows.Forms.TabPage WriteFiles;
        private System.Windows.Forms.Button SelectFolderBtn;
        private System.Windows.Forms.SaveFileDialog SFD;
    }
}

