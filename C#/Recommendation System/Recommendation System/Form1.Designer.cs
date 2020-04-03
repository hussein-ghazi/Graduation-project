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
            this.SFD = new System.Windows.Forms.SaveFileDialog();
            this.RecommendationsTab = new System.Windows.Forms.TabPage();
            this.RecommendationDG = new System.Windows.Forms.DataGridView();
            this.CalculateCorrelations = new System.Windows.Forms.Button();
            this.FindNeighbors = new System.Windows.Forms.Button();
            this.RecommendationsBtn = new System.Windows.Forms.Button();
            this.DataPreparationTab = new System.Windows.Forms.TabPage();
            this.LoadFilesDG = new System.Windows.Forms.DataGridView();
            this.RatingsButton = new System.Windows.Forms.Button();
            this.CorrelationButton = new System.Windows.Forms.Button();
            this.NeighborsButton = new System.Windows.Forms.Button();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.UserRecommendations = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.UserIDBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RecommendationsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecommendationDG)).BeginInit();
            this.DataPreparationTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoadFilesDG)).BeginInit();
            this.TabControl.SuspendLayout();
            this.UserRecommendations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // RecommendationsTab
            // 
            this.RecommendationsTab.Controls.Add(this.RecommendationsBtn);
            this.RecommendationsTab.Controls.Add(this.FindNeighbors);
            this.RecommendationsTab.Controls.Add(this.CalculateCorrelations);
            this.RecommendationsTab.Controls.Add(this.RecommendationDG);
            this.RecommendationsTab.Location = new System.Drawing.Point(4, 25);
            this.RecommendationsTab.Name = "RecommendationsTab";
            this.RecommendationsTab.Padding = new System.Windows.Forms.Padding(3);
            this.RecommendationsTab.Size = new System.Drawing.Size(752, 508);
            this.RecommendationsTab.TabIndex = 1;
            this.RecommendationsTab.Text = "Recommendations";
            this.RecommendationsTab.UseVisualStyleBackColor = true;
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
            // TabControl
            // 
            this.TabControl.Controls.Add(this.DataPreparationTab);
            this.TabControl.Controls.Add(this.RecommendationsTab);
            this.TabControl.Controls.Add(this.UserRecommendations);
            this.TabControl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl.Location = new System.Drawing.Point(12, 12);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(760, 537);
            this.TabControl.TabIndex = 0;
            // 
            // UserRecommendations
            // 
            this.UserRecommendations.Controls.Add(this.label1);
            this.UserRecommendations.Controls.Add(this.UserIDBox);
            this.UserRecommendations.Controls.Add(this.button1);
            this.UserRecommendations.Controls.Add(this.button2);
            this.UserRecommendations.Controls.Add(this.button3);
            this.UserRecommendations.Controls.Add(this.dataGridView1);
            this.UserRecommendations.Location = new System.Drawing.Point(4, 25);
            this.UserRecommendations.Name = "UserRecommendations";
            this.UserRecommendations.Padding = new System.Windows.Forms.Padding(3);
            this.UserRecommendations.Size = new System.Drawing.Size(752, 508);
            this.UserRecommendations.TabIndex = 2;
            this.UserRecommendations.Text = "User Recommendations";
            this.UserRecommendations.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(260, 468);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 37);
            this.button1.TabIndex = 10;
            this.button1.Text = "Recommndations";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(520, 468);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(229, 37);
            this.button2.TabIndex = 9;
            this.button2.Text = "Find Neighbors";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(3, 468);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(229, 37);
            this.button3.TabIndex = 8;
            this.button3.Text = "Calculate Correlations";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(752, 420);
            this.dataGridView1.TabIndex = 7;
            // 
            // UserIDBox
            // 
            this.UserIDBox.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserIDBox.Location = new System.Drawing.Point(344, 426);
            this.UserIDBox.Name = "UserIDBox";
            this.UserIDBox.Size = new System.Drawing.Size(145, 30);
            this.UserIDBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(256, 429);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "User ID";
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
            this.RecommendationsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RecommendationDG)).EndInit();
            this.DataPreparationTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LoadFilesDG)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.UserRecommendations.ResumeLayout(false);
            this.UserRecommendations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog SFD;
        private System.Windows.Forms.TabPage RecommendationsTab;
        private System.Windows.Forms.Button RecommendationsBtn;
        private System.Windows.Forms.Button FindNeighbors;
        private System.Windows.Forms.Button CalculateCorrelations;
        private System.Windows.Forms.DataGridView RecommendationDG;
        private System.Windows.Forms.TabPage DataPreparationTab;
        private System.Windows.Forms.Button NeighborsButton;
        private System.Windows.Forms.Button CorrelationButton;
        private System.Windows.Forms.Button RatingsButton;
        private System.Windows.Forms.DataGridView LoadFilesDG;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage UserRecommendations;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserIDBox;
    }
}

