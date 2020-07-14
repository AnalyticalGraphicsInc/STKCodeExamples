namespace EphemerisDifferencer
{
    partial class GraphicalUserInterface
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenOne = new System.Windows.Forms.Button();
            this.OpenTwo = new System.Windows.Forms.Button();
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FirstElementsBox = new System.Windows.Forms.ComboBox();
            this.SecondElementsBox = new System.Windows.Forms.ComboBox();
            this.InputBoxOne = new System.Windows.Forms.TextBox();
            this.InputBoxTwo = new System.Windows.Forms.TextBox();
            this.NumberOfPointsBox = new System.Windows.Forms.NumericUpDown();
            this.StartTimeBox = new System.Windows.Forms.MaskedTextBox();
            this.TimeStepBox = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GraphButton = new System.Windows.Forms.Button();
            this.ShowGraphCheckBox = new System.Windows.Forms.CheckBox();
            this.ReferenceFrameComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TabCheckBox = new System.Windows.Forms.CheckBox();
            this.ApparentCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.UnitsBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfPointsBox)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // OpenOne
            // 
            this.OpenOne.Location = new System.Drawing.Point(12, 37);
            this.OpenOne.Name = "OpenOne";
            this.OpenOne.Size = new System.Drawing.Size(165, 29);
            this.OpenOne.TabIndex = 0;
            this.OpenOne.Text = "Initialize First Ephemeris";
            this.OpenOne.UseVisualStyleBackColor = true;
            this.OpenOne.Click += new System.EventHandler(this.OpenOne_Click);
            // 
            // OpenTwo
            // 
            this.OpenTwo.Location = new System.Drawing.Point(12, 271);
            this.OpenTwo.Name = "OpenTwo";
            this.OpenTwo.Size = new System.Drawing.Size(165, 30);
            this.OpenTwo.TabIndex = 1;
            this.OpenTwo.Text = "Initialize Second Ephemeris";
            this.OpenTwo.UseVisualStyleBackColor = true;
            this.OpenTwo.Click += new System.EventHandler(this.OpenTwo_Click);
            // 
            // OutputBox
            // 
            this.OutputBox.AcceptsReturn = true;
            this.OutputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputBox.Location = new System.Drawing.Point(221, 37);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputBox.Size = new System.Drawing.Size(868, 681);
            this.OutputBox.TabIndex = 3;
            this.OutputBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 533);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Number of Points";
            // 
            // FirstElementsBox
            // 
            this.FirstElementsBox.FormattingEnabled = true;
            this.FirstElementsBox.Items.AddRange(new object[] {
            "Ephemeris from File",
            "Cartesian Elements",
            "Keplerian Elements"});
            this.FirstElementsBox.Location = new System.Drawing.Point(12, 10);
            this.FirstElementsBox.Name = "FirstElementsBox";
            this.FirstElementsBox.Size = new System.Drawing.Size(165, 21);
            this.FirstElementsBox.TabIndex = 7;
            this.FirstElementsBox.Text = "Ephemeris from File";
            this.FirstElementsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Lock_ComboBox);
            // 
            // SecondElementsBox
            // 
            this.SecondElementsBox.FormattingEnabled = true;
            this.SecondElementsBox.Items.AddRange(new object[] {
            "Ephemeris from File",
            "Cartesian Elements",
            "Keplerian Elements"});
            this.SecondElementsBox.Location = new System.Drawing.Point(12, 244);
            this.SecondElementsBox.Name = "SecondElementsBox";
            this.SecondElementsBox.Size = new System.Drawing.Size(165, 21);
            this.SecondElementsBox.TabIndex = 8;
            this.SecondElementsBox.Text = "Ephemeris from File";
            this.SecondElementsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Lock_ComboBox);
            // 
            // InputBoxOne
            // 
            this.InputBoxOne.Location = new System.Drawing.Point(12, 72);
            this.InputBoxOne.Multiline = true;
            this.InputBoxOne.Name = "InputBoxOne";
            this.InputBoxOne.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InputBoxOne.Size = new System.Drawing.Size(189, 154);
            this.InputBoxOne.TabIndex = 9;
            this.InputBoxOne.Text = "Please initialize an ephemeris...";
            // 
            // InputBoxTwo
            // 
            this.InputBoxTwo.Location = new System.Drawing.Point(12, 307);
            this.InputBoxTwo.Multiline = true;
            this.InputBoxTwo.Name = "InputBoxTwo";
            this.InputBoxTwo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InputBoxTwo.Size = new System.Drawing.Size(189, 160);
            this.InputBoxTwo.TabIndex = 10;
            this.InputBoxTwo.Text = "Please initialize an ephemeris...";
            // 
            // NumberOfPointsBox
            // 
            this.NumberOfPointsBox.Location = new System.Drawing.Point(12, 530);
            this.NumberOfPointsBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumberOfPointsBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumberOfPointsBox.Name = "NumberOfPointsBox";
            this.NumberOfPointsBox.Size = new System.Drawing.Size(50, 20);
            this.NumberOfPointsBox.TabIndex = 11;
            this.NumberOfPointsBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.NumberOfPointsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOfPointsBox_Return);
            // 
            // StartTimeBox
            // 
            this.StartTimeBox.Location = new System.Drawing.Point(12, 478);
            this.StartTimeBox.Mask = "0000000.00000";
            this.StartTimeBox.Name = "StartTimeBox";
            this.StartTimeBox.Size = new System.Drawing.Size(100, 20);
            this.StartTimeBox.TabIndex = 12;
            this.StartTimeBox.Text = "245154500000";
            // 
            // TimeStepBox
            // 
            this.TimeStepBox.Location = new System.Drawing.Point(12, 504);
            this.TimeStepBox.Mask = "####.###";
            this.TimeStepBox.Name = "TimeStepBox";
            this.TimeStepBox.Size = new System.Drawing.Size(100, 20);
            this.TimeStepBox.TabIndex = 13;
            this.TimeStepBox.Text = "0060000";
            this.TimeStepBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 481);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Julian Start Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 507);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Time Step [Sec]";
            // 
            // GraphButton
            // 
            this.GraphButton.Location = new System.Drawing.Point(12, 678);
            this.GraphButton.Name = "GraphButton";
            this.GraphButton.Size = new System.Drawing.Size(165, 40);
            this.GraphButton.TabIndex = 16;
            this.GraphButton.Text = "Compute Ephemeris Difference";
            this.GraphButton.UseVisualStyleBackColor = true;
            this.GraphButton.Click += new System.EventHandler(this.GraphButton_Click);
            // 
            // ShowGraphCheckBox
            // 
            this.ShowGraphCheckBox.AutoSize = true;
            this.ShowGraphCheckBox.Checked = true;
            this.ShowGraphCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowGraphCheckBox.Location = new System.Drawing.Point(27, 632);
            this.ShowGraphCheckBox.Name = "ShowGraphCheckBox";
            this.ShowGraphCheckBox.Size = new System.Drawing.Size(129, 17);
            this.ShowGraphCheckBox.TabIndex = 17;
            this.ShowGraphCheckBox.Text = "Display Graph of Error";
            this.ShowGraphCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReferenceFrameComboBox
            // 
            this.ReferenceFrameComboBox.FormattingEnabled = true;
            this.ReferenceFrameComboBox.Items.AddRange(new object[] {
            "Earth J2000 XYZ",
            "Earth Fixed XYZ",
            "Earth J2000 RIC"});
            this.ReferenceFrameComboBox.Location = new System.Drawing.Point(12, 556);
            this.ReferenceFrameComboBox.Name = "ReferenceFrameComboBox";
            this.ReferenceFrameComboBox.Size = new System.Drawing.Size(111, 21);
            this.ReferenceFrameComboBox.TabIndex = 18;
            this.ReferenceFrameComboBox.Text = "Earth J2000 XYZ";
            this.ReferenceFrameComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Lock_ComboBox);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 559);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "ReferenceFrame";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(331, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(632, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "Difference Represented by the Distance from the First Ephemeris to the Second Eph" +
                "emeris";
            // 
            // TabCheckBox
            // 
            this.TabCheckBox.AutoSize = true;
            this.TabCheckBox.Location = new System.Drawing.Point(27, 609);
            this.TabCheckBox.Name = "TabCheckBox";
            this.TabCheckBox.Size = new System.Drawing.Size(91, 17);
            this.TabCheckBox.TabIndex = 21;
            this.TabCheckBox.Text = "Tab Delimited";
            this.TabCheckBox.UseVisualStyleBackColor = true;
            // 
            // ApparentCheckBox
            // 
            this.ApparentCheckBox.AutoSize = true;
            this.ApparentCheckBox.Location = new System.Drawing.Point(27, 655);
            this.ApparentCheckBox.Name = "ApparentCheckBox";
            this.ApparentCheckBox.Size = new System.Drawing.Size(169, 17);
            this.ApparentCheckBox.TabIndex = 22;
            this.ApparentCheckBox.Text = "Account For Light Travel Time";
            this.ApparentCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(129, 586);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Units";
            // 
            // UnitsBox
            // 
            this.UnitsBox.FormattingEnabled = true;
            this.UnitsBox.Items.AddRange(new object[] {
            "Meters",
            "Kilometers"});
            this.UnitsBox.Location = new System.Drawing.Point(12, 583);
            this.UnitsBox.Name = "UnitsBox";
            this.UnitsBox.Size = new System.Drawing.Size(111, 21);
            this.UnitsBox.TabIndex = 23;
            this.UnitsBox.Text = "Kilometers";
            this.UnitsBox.SelectedIndexChanged += new System.EventHandler(this.UnitsBox_SelectedIndexChanged);
            // 
            // GraphicalUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 730);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.UnitsBox);
            this.Controls.Add(this.ApparentCheckBox);
            this.Controls.Add(this.TabCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ReferenceFrameComboBox);
            this.Controls.Add(this.ShowGraphCheckBox);
            this.Controls.Add(this.GraphButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TimeStepBox);
            this.Controls.Add(this.StartTimeBox);
            this.Controls.Add(this.NumberOfPointsBox);
            this.Controls.Add(this.InputBoxTwo);
            this.Controls.Add(this.InputBoxOne);
            this.Controls.Add(this.SecondElementsBox);
            this.Controls.Add(this.FirstElementsBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.OpenTwo);
            this.Controls.Add(this.OpenOne);
            this.MinimumSize = new System.Drawing.Size(500, 709);
            this.Name = "GraphicalUserInterface";
            this.Text = "Ephemeris Differencing MiniApp";
            this.Load += new System.EventHandler(this.GraphicalUserInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfPointsBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button OpenOne;
        private System.Windows.Forms.Button OpenTwo;
        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox FirstElementsBox;
        private System.Windows.Forms.ComboBox SecondElementsBox;
        private System.Windows.Forms.TextBox InputBoxOne;
        private System.Windows.Forms.TextBox InputBoxTwo;
        private System.Windows.Forms.NumericUpDown NumberOfPointsBox;
        private System.Windows.Forms.MaskedTextBox StartTimeBox;
        private System.Windows.Forms.MaskedTextBox TimeStepBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button GraphButton;
        private System.Windows.Forms.CheckBox ShowGraphCheckBox;
        private System.Windows.Forms.ComboBox ReferenceFrameComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox TabCheckBox;
        private System.Windows.Forms.CheckBox ApparentCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox UnitsBox;
    }
}