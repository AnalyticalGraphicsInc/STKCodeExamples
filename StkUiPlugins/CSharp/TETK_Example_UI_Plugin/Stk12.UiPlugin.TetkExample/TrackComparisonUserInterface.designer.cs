namespace Stk12.UiPlugin.TetkExample
{
    partial class TrackComparisonUserInterface
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_CreateAzDifferenceDataDisplay = new System.Windows.Forms.Button();
            this.button_CreateMissAngleVsTimeGraph = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox_TcTracks = new System.Windows.Forms.ComboBox();
            this.button_CreateSlantRangeDiffVsTimeGraph = new System.Windows.Forms.Button();
            this.button_ComputeTrackComparison = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox_MeasuredObj = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox_TruthObj = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_CreateAzDifferenceDataDisplay);
            this.groupBox1.Controls.Add(this.button_CreateMissAngleVsTimeGraph);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.comboBox_TcTracks);
            this.groupBox1.Controls.Add(this.button_CreateSlantRangeDiffVsTimeGraph);
            this.groupBox1.Controls.Add(this.button_ComputeTrackComparison);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.comboBox_MeasuredObj);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.comboBox_TruthObj);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(14, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 393);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Track Comparison";
            // 
            // button_CreateAzDifferenceDataDisplay
            // 
            this.button_CreateAzDifferenceDataDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_CreateAzDifferenceDataDisplay.Location = new System.Drawing.Point(6, 323);
            this.button_CreateAzDifferenceDataDisplay.Name = "button_CreateAzDifferenceDataDisplay";
            this.button_CreateAzDifferenceDataDisplay.Size = new System.Drawing.Size(159, 45);
            this.button_CreateAzDifferenceDataDisplay.TabIndex = 15;
            this.button_CreateAzDifferenceDataDisplay.Text = "Create Azimuth Diff. Data Display";
            this.button_CreateAzDifferenceDataDisplay.UseVisualStyleBackColor = true;
            this.button_CreateAzDifferenceDataDisplay.Click += new System.EventHandler(this.button_CreateAzimuthDifferenceDataDisplay_Click);
            // 
            // button_CreateMissAngleVsTimeGraph
            // 
            this.button_CreateMissAngleVsTimeGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_CreateMissAngleVsTimeGraph.Location = new System.Drawing.Point(6, 272);
            this.button_CreateMissAngleVsTimeGraph.Name = "button_CreateMissAngleVsTimeGraph";
            this.button_CreateMissAngleVsTimeGraph.Size = new System.Drawing.Size(159, 45);
            this.button_CreateMissAngleVsTimeGraph.TabIndex = 14;
            this.button_CreateMissAngleVsTimeGraph.Text = "Create Miss Angle vs Time Graph";
            this.button_CreateMissAngleVsTimeGraph.UseVisualStyleBackColor = true;
            this.button_CreateMissAngleVsTimeGraph.Click += new System.EventHandler(this.button_CreateMissAngleVsTimeGraph_Click);
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(6, 19);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 13);
            this.textBox3.TabIndex = 13;
            this.textBox3.Text = "Select Track:";
            // 
            // comboBox_TcTracks
            // 
            this.comboBox_TcTracks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_TcTracks.FormattingEnabled = true;
            this.comboBox_TcTracks.Location = new System.Drawing.Point(6, 38);
            this.comboBox_TcTracks.Name = "comboBox_TcTracks";
            this.comboBox_TcTracks.Size = new System.Drawing.Size(250, 21);
            this.comboBox_TcTracks.TabIndex = 12;
            // 
            // button_CreateSlantRangeDiffVsTimeGraph
            // 
            this.button_CreateSlantRangeDiffVsTimeGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_CreateSlantRangeDiffVsTimeGraph.Location = new System.Drawing.Point(6, 221);
            this.button_CreateSlantRangeDiffVsTimeGraph.Name = "button_CreateSlantRangeDiffVsTimeGraph";
            this.button_CreateSlantRangeDiffVsTimeGraph.Size = new System.Drawing.Size(159, 45);
            this.button_CreateSlantRangeDiffVsTimeGraph.TabIndex = 11;
            this.button_CreateSlantRangeDiffVsTimeGraph.Text = "Create Slant Range Diff. vs Time Graph";
            this.button_CreateSlantRangeDiffVsTimeGraph.UseVisualStyleBackColor = true;
            this.button_CreateSlantRangeDiffVsTimeGraph.Click += new System.EventHandler(this.button_CreateSlantRangeDiffVsTimeGraph_Click);
            // 
            // button_ComputeTrackComparison
            // 
            this.button_ComputeTrackComparison.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ComputeTrackComparison.Location = new System.Drawing.Point(6, 170);
            this.button_ComputeTrackComparison.Name = "button_ComputeTrackComparison";
            this.button_ComputeTrackComparison.Size = new System.Drawing.Size(159, 45);
            this.button_ComputeTrackComparison.TabIndex = 10;
            this.button_ComputeTrackComparison.Text = "Compute Track Comparison";
            this.button_ComputeTrackComparison.UseVisualStyleBackColor = true;
            this.button_ComputeTrackComparison.Click += new System.EventHandler(this.button_ComputeTrackComparison_Click);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(6, 110);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 13);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "Select Measured Obj:";
            // 
            // comboBox_MeasuredObj
            // 
            this.comboBox_MeasuredObj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_MeasuredObj.FormattingEnabled = true;
            this.comboBox_MeasuredObj.Location = new System.Drawing.Point(6, 129);
            this.comboBox_MeasuredObj.Name = "comboBox_MeasuredObj";
            this.comboBox_MeasuredObj.Size = new System.Drawing.Size(250, 21);
            this.comboBox_MeasuredObj.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 13);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Select Truth Obj:";
            // 
            // comboBox_TruthObj
            // 
            this.comboBox_TruthObj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_TruthObj.FormattingEnabled = true;
            this.comboBox_TruthObj.Location = new System.Drawing.Point(6, 83);
            this.comboBox_TruthObj.Name = "comboBox_TruthObj";
            this.comboBox_TruthObj.Size = new System.Drawing.Size(250, 21);
            this.comboBox_TruthObj.TabIndex = 6;
            // 
            // TrackComparisonUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "TrackComparisonUserInterface";
            this.Size = new System.Drawing.Size(390, 430);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox_MeasuredObj;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox_TruthObj;
        private System.Windows.Forms.Button button_CreateSlantRangeDiffVsTimeGraph;
        private System.Windows.Forms.Button button_ComputeTrackComparison;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox comboBox_TcTracks;
        private System.Windows.Forms.Button button_CreateMissAngleVsTimeGraph;
        private System.Windows.Forms.Button button_CreateAzDifferenceDataDisplay;
    }
}
