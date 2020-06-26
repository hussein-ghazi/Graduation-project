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
            this.RecommendationsTab = new System.Windows.Forms.TabPage();
            this.RecommendationsBtn = new System.Windows.Forms.Button();
            this.FindNeighbors = new System.Windows.Forms.Button();
            this.CalculateCorrelations = new System.Windows.Forms.Button();
            this.RecommendationDG = new System.Windows.Forms.DataGridView();
            this.DataPreparationTab = new System.Windows.Forms.TabPage();
            this.NeighborsButton = new System.Windows.Forms.Button();
            this.CorrelationButton = new System.Windows.Forms.Button();
            this.RatingsButton = new System.Windows.Forms.Button();
            this.LoadFilesDG = new System.Windows.Forms.DataGridView();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.EvaluationTab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RmseZerosBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RmseRRBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RmseBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MaeZerosBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MaeRRBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MaeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EvaluationBtn = new System.Windows.Forms.Button();
            this.SFD = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.RecommendationsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecommendationDG)).BeginInit();
            this.DataPreparationTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoadFilesDG)).BeginInit();
            this.TabControl.SuspendLayout();
            this.EvaluationTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.RecommendationsTab.Text = "Recommendation Process";
            this.RecommendationsTab.UseVisualStyleBackColor = true;
            // 
            // RecommendationsBtn
            // 
            this.RecommendationsBtn.BackColor = System.Drawing.Color.SteelBlue;
            this.RecommendationsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RecommendationsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecommendationsBtn.ForeColor = System.Drawing.Color.White;
            this.RecommendationsBtn.Location = new System.Drawing.Point(245, 468);
            this.RecommendationsBtn.Name = "RecommendationsBtn";
            this.RecommendationsBtn.Size = new System.Drawing.Size(254, 37);
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
            this.FindNeighbors.Location = new System.Drawing.Point(505, 468);
            this.FindNeighbors.Name = "FindNeighbors";
            this.FindNeighbors.Size = new System.Drawing.Size(241, 37);
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
            this.CalculateCorrelations.Size = new System.Drawing.Size(236, 37);
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
            // DataPreparationTab
            // 
            this.DataPreparationTab.BackColor = System.Drawing.Color.Transparent;
            this.DataPreparationTab.Controls.Add(this.button2);
            this.DataPreparationTab.Controls.Add(this.button1);
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
            this.NeighborsButton.Size = new System.Drawing.Size(141, 37);
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
            this.CorrelationButton.Location = new System.Drawing.Point(151, 468);
            this.CorrelationButton.Name = "CorrelationButton";
            this.CorrelationButton.Size = new System.Drawing.Size(153, 37);
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
            this.RatingsButton.Location = new System.Drawing.Point(310, 468);
            this.RatingsButton.Name = "RatingsButton";
            this.RatingsButton.Size = new System.Drawing.Size(114, 37);
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
            // TabControl
            // 
            this.TabControl.Controls.Add(this.DataPreparationTab);
            this.TabControl.Controls.Add(this.RecommendationsTab);
            this.TabControl.Controls.Add(this.EvaluationTab);
            this.TabControl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl.Location = new System.Drawing.Point(12, 12);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(760, 537);
            this.TabControl.TabIndex = 0;
            // 
            // EvaluationTab
            // 
            this.EvaluationTab.Controls.Add(this.groupBox2);
            this.EvaluationTab.Controls.Add(this.groupBox1);
            this.EvaluationTab.Controls.Add(this.label3);
            this.EvaluationTab.Controls.Add(this.EvaluationBtn);
            this.EvaluationTab.Location = new System.Drawing.Point(4, 25);
            this.EvaluationTab.Name = "EvaluationTab";
            this.EvaluationTab.Padding = new System.Windows.Forms.Padding(3);
            this.EvaluationTab.Size = new System.Drawing.Size(752, 508);
            this.EvaluationTab.TabIndex = 2;
            this.EvaluationTab.Text = "Evaluation";
            this.EvaluationTab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RmseZerosBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.RmseRRBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.RmseBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(396, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 398);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RMSE Statistics";
            // 
            // RmseZerosBox
            // 
            this.RmseZerosBox.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RmseZerosBox.Location = new System.Drawing.Point(6, 337);
            this.RmseZerosBox.Name = "RmseZerosBox";
            this.RmseZerosBox.ReadOnly = true;
            this.RmseZerosBox.Size = new System.Drawing.Size(339, 32);
            this.RmseZerosBox.TabIndex = 18;
            this.RmseZerosBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 308);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(334, 24);
            this.label7.TabIndex = 19;
            this.label7.Text = "# zeros in prediction";
            // 
            // RmseRRBox
            // 
            this.RmseRRBox.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RmseRRBox.Location = new System.Drawing.Point(6, 211);
            this.RmseRRBox.Name = "RmseRRBox";
            this.RmseRRBox.ReadOnly = true;
            this.RmseRRBox.Size = new System.Drawing.Size(338, 32);
            this.RmseRRBox.TabIndex = 12;
            this.RmseRRBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(338, 58);
            this.label4.TabIndex = 13;
            this.label4.Text = "# removed ratings from ratings matrix";
            // 
            // RmseBox
            // 
            this.RmseBox.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RmseBox.Location = new System.Drawing.Point(10, 68);
            this.RmseBox.Name = "RmseBox";
            this.RmseBox.ReadOnly = true;
            this.RmseBox.Size = new System.Drawing.Size(334, 32);
            this.RmseBox.TabIndex = 10;
            this.RmseBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "RMSE";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MaeZerosBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.MaeRRBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.MaeBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 398);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MAE Statistics";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // MaeZerosBox
            // 
            this.MaeZerosBox.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaeZerosBox.Location = new System.Drawing.Point(6, 337);
            this.MaeZerosBox.Name = "MaeZerosBox";
            this.MaeZerosBox.ReadOnly = true;
            this.MaeZerosBox.Size = new System.Drawing.Size(339, 32);
            this.MaeZerosBox.TabIndex = 16;
            this.MaeZerosBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(334, 24);
            this.label6.TabIndex = 17;
            this.label6.Text = "# zeros in prediction";
            // 
            // MaeRRBox
            // 
            this.MaeRRBox.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaeRRBox.Location = new System.Drawing.Point(6, 211);
            this.MaeRRBox.Name = "MaeRRBox";
            this.MaeRRBox.ReadOnly = true;
            this.MaeRRBox.Size = new System.Drawing.Size(338, 32);
            this.MaeRRBox.TabIndex = 14;
            this.MaeRRBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(343, 58);
            this.label5.TabIndex = 15;
            this.label5.Text = "# removed ratings from ratings matrix";
            // 
            // MaeBox
            // 
            this.MaeBox.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaeBox.Location = new System.Drawing.Point(6, 68);
            this.MaeBox.Name = "MaeBox";
            this.MaeBox.ReadOnly = true;
            this.MaeBox.Size = new System.Drawing.Size(338, 32);
            this.MaeBox.TabIndex = 8;
            this.MaeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "MAE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(117, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(501, 55);
            this.label3.TabIndex = 12;
            this.label3.Text = "Evaluation Statistics";
            // 
            // EvaluationBtn
            // 
            this.EvaluationBtn.BackColor = System.Drawing.Color.SteelBlue;
            this.EvaluationBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EvaluationBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EvaluationBtn.ForeColor = System.Drawing.Color.White;
            this.EvaluationBtn.Location = new System.Drawing.Point(243, 465);
            this.EvaluationBtn.Name = "EvaluationBtn";
            this.EvaluationBtn.Size = new System.Drawing.Size(254, 37);
            this.EvaluationBtn.TabIndex = 7;
            this.EvaluationBtn.Text = "Evaluation";
            this.EvaluationBtn.UseVisualStyleBackColor = false;
            this.EvaluationBtn.Click += new System.EventHandler(this.EvaluationBtn_Click);
            // 
            // SFD
            // 
            this.SFD.FileOk += new System.ComponentModel.CancelEventHandler(this.SFD_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(430, 472);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Load Jester";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(603, 469);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 32);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
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
            this.EvaluationTab.ResumeLayout(false);
            this.EvaluationTab.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.SaveFileDialog SFD;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage EvaluationTab;
        private System.Windows.Forms.Button EvaluationBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RmseBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MaeBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox RmseRRBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox MaeZerosBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox MaeRRBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox RmseZerosBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

