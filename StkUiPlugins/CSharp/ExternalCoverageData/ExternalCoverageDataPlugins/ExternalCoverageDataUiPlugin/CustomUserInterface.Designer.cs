namespace LoadExternalCoverageData
{
    partial class CustomUserInterface
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
            this.Button1 = new System.Windows.Forms.Button();
            this.externalDataFilePathTextBox = new System.Windows.Forms.TextBox();
            this.externalDataFileSelectionButton = new System.Windows.Forms.Button();
            this.externalDataFileGroupBox = new System.Windows.Forms.GroupBox();
            this.externalDataFileDescriptionLabel2 = new System.Windows.Forms.Label();
            this.externalDataFileDescriptionLabel1 = new System.Windows.Forms.Label();
            this.createPntFileCheckBox = new System.Windows.Forms.CheckBox();
            this.fomGroupBox = new System.Windows.Forms.GroupBox();
            this.pntFilePathLabel = new System.Windows.Forms.Label();
            this.pntFilePathTextBox = new System.Windows.Forms.TextBox();
            this.selectPntFileButton = new System.Windows.Forms.Button();
            this.computeCoverageGroupBox = new System.Windows.Forms.GroupBox();
            this.adjustGraphicsLabel = new System.Windows.Forms.Label();
            this.externalDataFileGroupBox.SuspendLayout();
            this.fomGroupBox.SuspendLayout();
            this.computeCoverageGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(78, 19);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(135, 23);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Load External Data";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // externalDataFilePathTextBox
            // 
            this.externalDataFilePathTextBox.Location = new System.Drawing.Point(6, 19);
            this.externalDataFilePathTextBox.Name = "externalDataFilePathTextBox";
            this.externalDataFilePathTextBox.Size = new System.Drawing.Size(276, 20);
            this.externalDataFilePathTextBox.TabIndex = 2;
            // 
            // externalDataFileSelectionButton
            // 
            this.externalDataFileSelectionButton.Location = new System.Drawing.Point(288, 17);
            this.externalDataFileSelectionButton.Name = "externalDataFileSelectionButton";
            this.externalDataFileSelectionButton.Size = new System.Drawing.Size(24, 23);
            this.externalDataFileSelectionButton.TabIndex = 3;
            this.externalDataFileSelectionButton.Text = "...";
            this.externalDataFileSelectionButton.UseVisualStyleBackColor = true;
            this.externalDataFileSelectionButton.Click += new System.EventHandler(this.externalDataFileSelectionButton_Click);
            // 
            // externalDataFileGroupBox
            // 
            this.externalDataFileGroupBox.Controls.Add(this.externalDataFileDescriptionLabel2);
            this.externalDataFileGroupBox.Controls.Add(this.externalDataFileDescriptionLabel1);
            this.externalDataFileGroupBox.Controls.Add(this.externalDataFilePathTextBox);
            this.externalDataFileGroupBox.Controls.Add(this.externalDataFileSelectionButton);
            this.externalDataFileGroupBox.Location = new System.Drawing.Point(17, 20);
            this.externalDataFileGroupBox.Name = "externalDataFileGroupBox";
            this.externalDataFileGroupBox.Size = new System.Drawing.Size(324, 93);
            this.externalDataFileGroupBox.TabIndex = 4;
            this.externalDataFileGroupBox.TabStop = false;
            this.externalDataFileGroupBox.Text = "1. Specify External Data";
            // 
            // externalDataFileDescriptionLabel2
            // 
            this.externalDataFileDescriptionLabel2.Location = new System.Drawing.Point(33, 67);
            this.externalDataFileDescriptionLabel2.Name = "externalDataFileDescriptionLabel2";
            this.externalDataFileDescriptionLabel2.Size = new System.Drawing.Size(211, 23);
            this.externalDataFileDescriptionLabel2.TabIndex = 5;
            this.externalDataFileDescriptionLabel2.Text = "Time (EpSec), lat (deg), lon (deg), value";
            // 
            // externalDataFileDescriptionLabel1
            // 
            this.externalDataFileDescriptionLabel1.Location = new System.Drawing.Point(8, 46);
            this.externalDataFileDescriptionLabel1.Name = "externalDataFileDescriptionLabel1";
            this.externalDataFileDescriptionLabel1.Size = new System.Drawing.Size(185, 23);
            this.externalDataFileDescriptionLabel1.TabIndex = 4;
            this.externalDataFileDescriptionLabel1.Text = "Expected format:";
            // 
            // createPntFileCheckBox
            // 
            this.createPntFileCheckBox.AutoSize = true;
            this.createPntFileCheckBox.Checked = true;
            this.createPntFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createPntFileCheckBox.Location = new System.Drawing.Point(23, 28);
            this.createPntFileCheckBox.Name = "createPntFileCheckBox";
            this.createPntFileCheckBox.Size = new System.Drawing.Size(94, 17);
            this.createPntFileCheckBox.TabIndex = 5;
            this.createPntFileCheckBox.Text = "Create .pnt file";
            this.createPntFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // fomGroupBox
            // 
            this.fomGroupBox.Controls.Add(this.pntFilePathLabel);
            this.fomGroupBox.Controls.Add(this.pntFilePathTextBox);
            this.fomGroupBox.Controls.Add(this.selectPntFileButton);
            this.fomGroupBox.Controls.Add(this.createPntFileCheckBox);
            this.fomGroupBox.Location = new System.Drawing.Point(17, 132);
            this.fomGroupBox.Name = "fomGroupBox";
            this.fomGroupBox.Size = new System.Drawing.Size(324, 94);
            this.fomGroupBox.TabIndex = 7;
            this.fomGroupBox.TabStop = false;
            this.fomGroupBox.Text = "2. Select/Create Point file";
            // 
            // pntFilePathLabel
            // 
            this.pntFilePathLabel.AutoSize = true;
            this.pntFilePathLabel.Location = new System.Drawing.Point(20, 58);
            this.pntFilePathLabel.Name = "pntFilePathLabel";
            this.pntFilePathLabel.Size = new System.Drawing.Size(49, 13);
            this.pntFilePathLabel.TabIndex = 15;
            this.pntFilePathLabel.Text = ".pnt path";
            // 
            // pntFilePathTextBox
            // 
            this.pntFilePathTextBox.Location = new System.Drawing.Point(78, 55);
            this.pntFilePathTextBox.Name = "pntFilePathTextBox";
            this.pntFilePathTextBox.Size = new System.Drawing.Size(204, 20);
            this.pntFilePathTextBox.TabIndex = 14;
            // 
            // selectPntFileButton
            // 
            this.selectPntFileButton.Location = new System.Drawing.Point(288, 53);
            this.selectPntFileButton.Name = "selectPntFileButton";
            this.selectPntFileButton.Size = new System.Drawing.Size(24, 23);
            this.selectPntFileButton.TabIndex = 13;
            this.selectPntFileButton.Text = "...";
            this.selectPntFileButton.UseVisualStyleBackColor = true;
            this.selectPntFileButton.Click += new System.EventHandler(this.selectPntFileButton_Click);
            // 
            // computeCoverageGroupBox
            // 
            this.computeCoverageGroupBox.Controls.Add(this.Button1);
            this.computeCoverageGroupBox.Location = new System.Drawing.Point(17, 249);
            this.computeCoverageGroupBox.Name = "computeCoverageGroupBox";
            this.computeCoverageGroupBox.Size = new System.Drawing.Size(324, 53);
            this.computeCoverageGroupBox.TabIndex = 8;
            this.computeCoverageGroupBox.TabStop = false;
            this.computeCoverageGroupBox.Text = "3. Display the Coverage data";
            // 
            // adjustGraphicsLabel
            // 
            this.adjustGraphicsLabel.AutoSize = true;
            this.adjustGraphicsLabel.Location = new System.Drawing.Point(22, 321);
            this.adjustGraphicsLabel.Name = "adjustGraphicsLabel";
            this.adjustGraphicsLabel.Size = new System.Drawing.Size(208, 13);
            this.adjustGraphicsLabel.TabIndex = 9;
            this.adjustGraphicsLabel.Text = "4. Adjust the graphics in the Figure of Merit";
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.adjustGraphicsLabel);
            this.Controls.Add(this.computeCoverageGroupBox);
            this.Controls.Add(this.fomGroupBox);
            this.Controls.Add(this.externalDataFileGroupBox);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(362, 364);
            this.externalDataFileGroupBox.ResumeLayout(false);
            this.externalDataFileGroupBox.PerformLayout();
            this.fomGroupBox.ResumeLayout(false);
            this.fomGroupBox.PerformLayout();
            this.computeCoverageGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.TextBox externalDataFilePathTextBox;
        private System.Windows.Forms.Button externalDataFileSelectionButton;
        private System.Windows.Forms.GroupBox externalDataFileGroupBox;
        private System.Windows.Forms.CheckBox createPntFileCheckBox;
        private System.Windows.Forms.GroupBox fomGroupBox;
        private System.Windows.Forms.GroupBox computeCoverageGroupBox;
        private System.Windows.Forms.Label adjustGraphicsLabel;
        private System.Windows.Forms.Label pntFilePathLabel;
        private System.Windows.Forms.TextBox pntFilePathTextBox;
        private System.Windows.Forms.Button selectPntFileButton;
        private System.Windows.Forms.Label externalDataFileDescriptionLabel2;
        private System.Windows.Forms.Label externalDataFileDescriptionLabel1;
    }
}
