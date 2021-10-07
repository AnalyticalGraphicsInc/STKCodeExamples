namespace ConstrainedAttitude.UiPlugin
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
            this.alignedVectorLabel = new System.Windows.Forms.Label();
            this.alignedBodyBox = new System.Windows.Forms.GroupBox();
            this.alignedZTextBox = new System.Windows.Forms.TextBox();
            this.alignedYTextBox = new System.Windows.Forms.TextBox();
            this.alignedXTextBox = new System.Windows.Forms.TextBox();
            this.alignedZLabel = new System.Windows.Forms.Label();
            this.alignedYLabel = new System.Windows.Forms.Label();
            this.alignedXLabel = new System.Windows.Forms.Label();
            this.constrainedVectorLabel = new System.Windows.Forms.Label();
            this.constrainedBodyBox = new System.Windows.Forms.GroupBox();
            this.constrainedZTextBox = new System.Windows.Forms.TextBox();
            this.constrainedYTextBox = new System.Windows.Forms.TextBox();
            this.constrainedXTextBox = new System.Windows.Forms.TextBox();
            this.constrainedZLabel = new System.Windows.Forms.Label();
            this.constrainedYLabel = new System.Windows.Forms.Label();
            this.constrainedXLabel = new System.Windows.Forms.Label();
            this.alignedVectorComboBox = new System.Windows.Forms.ComboBox();
            this.constrainedVectorComboBox = new System.Windows.Forms.ComboBox();
            this.angleLimitLabel = new System.Windows.Forms.Label();
            this.angleLimitTextBox = new System.Windows.Forms.TextBox();
            this.createAttitudeButton = new System.Windows.Forms.Button();
            this.showGraphicsCheckBox = new System.Windows.Forms.CheckBox();
            this.showGraphicsLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.alignedBodyBox.SuspendLayout();
            this.constrainedBodyBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // alignedVectorLabel
            // 
            this.alignedVectorLabel.AutoSize = true;
            this.alignedVectorLabel.Location = new System.Drawing.Point(24, 24);
            this.alignedVectorLabel.Name = "alignedVectorLabel";
            this.alignedVectorLabel.Size = new System.Drawing.Size(104, 17);
            this.alignedVectorLabel.TabIndex = 0;
            this.alignedVectorLabel.Text = "Aligned Vector:";
            // 
            // alignedBodyBox
            // 
            this.alignedBodyBox.Controls.Add(this.alignedZTextBox);
            this.alignedBodyBox.Controls.Add(this.alignedYTextBox);
            this.alignedBodyBox.Controls.Add(this.alignedXTextBox);
            this.alignedBodyBox.Controls.Add(this.alignedZLabel);
            this.alignedBodyBox.Controls.Add(this.alignedYLabel);
            this.alignedBodyBox.Controls.Add(this.alignedXLabel);
            this.alignedBodyBox.Location = new System.Drawing.Point(277, 24);
            this.alignedBodyBox.Name = "alignedBodyBox";
            this.alignedBodyBox.Size = new System.Drawing.Size(187, 147);
            this.alignedBodyBox.TabIndex = 1;
            this.alignedBodyBox.TabStop = false;
            this.alignedBodyBox.Text = "Body";
            // 
            // alignedZTextBox
            // 
            this.alignedZTextBox.Location = new System.Drawing.Point(33, 103);
            this.alignedZTextBox.Name = "alignedZTextBox";
            this.alignedZTextBox.Size = new System.Drawing.Size(141, 22);
            this.alignedZTextBox.TabIndex = 5;
            this.alignedZTextBox.Text = "0";
            this.alignedZTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // alignedYTextBox
            // 
            this.alignedYTextBox.Location = new System.Drawing.Point(33, 65);
            this.alignedYTextBox.Name = "alignedYTextBox";
            this.alignedYTextBox.Size = new System.Drawing.Size(141, 22);
            this.alignedYTextBox.TabIndex = 4;
            this.alignedYTextBox.Text = "0";
            this.alignedYTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // alignedXTextBox
            // 
            this.alignedXTextBox.Location = new System.Drawing.Point(33, 27);
            this.alignedXTextBox.Name = "alignedXTextBox";
            this.alignedXTextBox.Size = new System.Drawing.Size(141, 22);
            this.alignedXTextBox.TabIndex = 3;
            this.alignedXTextBox.Text = "1";
            this.alignedXTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // alignedZLabel
            // 
            this.alignedZLabel.AutoSize = true;
            this.alignedZLabel.Location = new System.Drawing.Point(6, 106);
            this.alignedZLabel.Name = "alignedZLabel";
            this.alignedZLabel.Size = new System.Drawing.Size(21, 17);
            this.alignedZLabel.TabIndex = 2;
            this.alignedZLabel.Text = "Z:";
            // 
            // alignedYLabel
            // 
            this.alignedYLabel.AutoSize = true;
            this.alignedYLabel.Location = new System.Drawing.Point(6, 68);
            this.alignedYLabel.Name = "alignedYLabel";
            this.alignedYLabel.Size = new System.Drawing.Size(21, 17);
            this.alignedYLabel.TabIndex = 1;
            this.alignedYLabel.Text = "Y:";
            // 
            // alignedXLabel
            // 
            this.alignedXLabel.AutoSize = true;
            this.alignedXLabel.Location = new System.Drawing.Point(6, 30);
            this.alignedXLabel.Name = "alignedXLabel";
            this.alignedXLabel.Size = new System.Drawing.Size(21, 17);
            this.alignedXLabel.TabIndex = 0;
            this.alignedXLabel.Text = "X:";
            // 
            // constrainedVectorLabel
            // 
            this.constrainedVectorLabel.AutoSize = true;
            this.constrainedVectorLabel.Location = new System.Drawing.Point(24, 210);
            this.constrainedVectorLabel.Name = "constrainedVectorLabel";
            this.constrainedVectorLabel.Size = new System.Drawing.Size(133, 17);
            this.constrainedVectorLabel.TabIndex = 4;
            this.constrainedVectorLabel.Text = "Constrained Vector:";
            // 
            // constrainedBodyBox
            // 
            this.constrainedBodyBox.Controls.Add(this.constrainedZTextBox);
            this.constrainedBodyBox.Controls.Add(this.constrainedYTextBox);
            this.constrainedBodyBox.Controls.Add(this.constrainedXTextBox);
            this.constrainedBodyBox.Controls.Add(this.constrainedZLabel);
            this.constrainedBodyBox.Controls.Add(this.constrainedYLabel);
            this.constrainedBodyBox.Controls.Add(this.constrainedXLabel);
            this.constrainedBodyBox.Location = new System.Drawing.Point(277, 210);
            this.constrainedBodyBox.Name = "constrainedBodyBox";
            this.constrainedBodyBox.Size = new System.Drawing.Size(187, 147);
            this.constrainedBodyBox.TabIndex = 6;
            this.constrainedBodyBox.TabStop = false;
            this.constrainedBodyBox.Text = "Body";
            // 
            // constrainedZTextBox
            // 
            this.constrainedZTextBox.Location = new System.Drawing.Point(33, 103);
            this.constrainedZTextBox.Name = "constrainedZTextBox";
            this.constrainedZTextBox.Size = new System.Drawing.Size(141, 22);
            this.constrainedZTextBox.TabIndex = 5;
            this.constrainedZTextBox.Text = "1";
            this.constrainedZTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // constrainedYTextBox
            // 
            this.constrainedYTextBox.Location = new System.Drawing.Point(33, 65);
            this.constrainedYTextBox.Name = "constrainedYTextBox";
            this.constrainedYTextBox.Size = new System.Drawing.Size(141, 22);
            this.constrainedYTextBox.TabIndex = 4;
            this.constrainedYTextBox.Text = "0";
            this.constrainedYTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // constrainedXTextBox
            // 
            this.constrainedXTextBox.Location = new System.Drawing.Point(33, 27);
            this.constrainedXTextBox.Name = "constrainedXTextBox";
            this.constrainedXTextBox.Size = new System.Drawing.Size(141, 22);
            this.constrainedXTextBox.TabIndex = 3;
            this.constrainedXTextBox.Text = "0";
            this.constrainedXTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // constrainedZLabel
            // 
            this.constrainedZLabel.AutoSize = true;
            this.constrainedZLabel.Location = new System.Drawing.Point(6, 106);
            this.constrainedZLabel.Name = "constrainedZLabel";
            this.constrainedZLabel.Size = new System.Drawing.Size(21, 17);
            this.constrainedZLabel.TabIndex = 2;
            this.constrainedZLabel.Text = "Z:";
            // 
            // constrainedYLabel
            // 
            this.constrainedYLabel.AutoSize = true;
            this.constrainedYLabel.Location = new System.Drawing.Point(6, 68);
            this.constrainedYLabel.Name = "constrainedYLabel";
            this.constrainedYLabel.Size = new System.Drawing.Size(21, 17);
            this.constrainedYLabel.TabIndex = 1;
            this.constrainedYLabel.Text = "Y:";
            // 
            // constrainedXLabel
            // 
            this.constrainedXLabel.AutoSize = true;
            this.constrainedXLabel.Location = new System.Drawing.Point(6, 30);
            this.constrainedXLabel.Name = "constrainedXLabel";
            this.constrainedXLabel.Size = new System.Drawing.Size(21, 17);
            this.constrainedXLabel.TabIndex = 0;
            this.constrainedXLabel.Text = "X:";
            // 
            // alignedVectorComboBox
            // 
            this.alignedVectorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.alignedVectorComboBox.Location = new System.Drawing.Point(27, 51);
            this.alignedVectorComboBox.Name = "alignedVectorComboBox";
            this.alignedVectorComboBox.Size = new System.Drawing.Size(244, 24);
            this.alignedVectorComboBox.TabIndex = 7;
            // 
            // constrainedVectorComboBox
            // 
            this.constrainedVectorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.constrainedVectorComboBox.FormattingEnabled = true;
            this.constrainedVectorComboBox.Location = new System.Drawing.Point(27, 237);
            this.constrainedVectorComboBox.Name = "constrainedVectorComboBox";
            this.constrainedVectorComboBox.Size = new System.Drawing.Size(244, 24);
            this.constrainedVectorComboBox.TabIndex = 8;
            // 
            // angleLimitLabel
            // 
            this.angleLimitLabel.AutoSize = true;
            this.angleLimitLabel.Location = new System.Drawing.Point(24, 281);
            this.angleLimitLabel.Name = "angleLimitLabel";
            this.angleLimitLabel.Size = new System.Drawing.Size(161, 17);
            this.angleLimitLabel.TabIndex = 9;
            this.angleLimitLabel.Text = "Angle Offset Limit (deg):";
            // 
            // angleLimitTextBox
            // 
            this.angleLimitTextBox.Location = new System.Drawing.Point(191, 278);
            this.angleLimitTextBox.Name = "angleLimitTextBox";
            this.angleLimitTextBox.Size = new System.Drawing.Size(80, 22);
            this.angleLimitTextBox.TabIndex = 10;
            this.angleLimitTextBox.Text = "45";
            // 
            // createAttitudeButton
            // 
            this.createAttitudeButton.Location = new System.Drawing.Point(310, 363);
            this.createAttitudeButton.Name = "createAttitudeButton";
            this.createAttitudeButton.Size = new System.Drawing.Size(141, 37);
            this.createAttitudeButton.TabIndex = 11;
            this.createAttitudeButton.Text = "Create Attitude";
            this.createAttitudeButton.UseVisualStyleBackColor = true;
            this.createAttitudeButton.Click += new System.EventHandler(this.CreateAttitudeButton_Click);
            // 
            // showGraphicsCheckBox
            // 
            this.showGraphicsCheckBox.AutoSize = true;
            this.showGraphicsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.showGraphicsCheckBox.Checked = true;
            this.showGraphicsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGraphicsCheckBox.Location = new System.Drawing.Point(191, 315);
            this.showGraphicsCheckBox.Name = "showGraphicsCheckBox";
            this.showGraphicsCheckBox.Size = new System.Drawing.Size(18, 17);
            this.showGraphicsCheckBox.TabIndex = 13;
            this.showGraphicsCheckBox.UseVisualStyleBackColor = true;
            // 
            // showGraphicsLabel
            // 
            this.showGraphicsLabel.AutoSize = true;
            this.showGraphicsLabel.Location = new System.Drawing.Point(24, 316);
            this.showGraphicsLabel.Name = "showGraphicsLabel";
            this.showGraphicsLabel.Size = new System.Drawing.Size(159, 17);
            this.showGraphicsLabel.TabIndex = 14;
            this.showGraphicsLabel.Text = "Show Attitude Graphics:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(27, 379);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(182, 21);
            this.progressBar.TabIndex = 15;
            this.progressBar.Visible = false;
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.showGraphicsCheckBox);
            this.Controls.Add(this.createAttitudeButton);
            this.Controls.Add(this.angleLimitTextBox);
            this.Controls.Add(this.angleLimitLabel);
            this.Controls.Add(this.constrainedVectorComboBox);
            this.Controls.Add(this.alignedVectorComboBox);
            this.Controls.Add(this.constrainedBodyBox);
            this.Controls.Add(this.constrainedVectorLabel);
            this.Controls.Add(this.alignedBodyBox);
            this.Controls.Add(this.alignedVectorLabel);
            this.Controls.Add(this.showGraphicsLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(550, 441);
            this.alignedBodyBox.ResumeLayout(false);
            this.alignedBodyBox.PerformLayout();
            this.constrainedBodyBox.ResumeLayout(false);
            this.constrainedBodyBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label alignedVectorLabel;
        private System.Windows.Forms.GroupBox alignedBodyBox;
        private System.Windows.Forms.TextBox alignedZTextBox;
        private System.Windows.Forms.TextBox alignedYTextBox;
        private System.Windows.Forms.TextBox alignedXTextBox;
        private System.Windows.Forms.Label alignedZLabel;
        private System.Windows.Forms.Label alignedYLabel;
        private System.Windows.Forms.Label alignedXLabel;
        private System.Windows.Forms.Label constrainedVectorLabel;
        private System.Windows.Forms.GroupBox constrainedBodyBox;
        private System.Windows.Forms.TextBox constrainedZTextBox;
        private System.Windows.Forms.TextBox constrainedYTextBox;
        private System.Windows.Forms.TextBox constrainedXTextBox;
        private System.Windows.Forms.Label constrainedZLabel;
        private System.Windows.Forms.Label constrainedYLabel;
        private System.Windows.Forms.Label constrainedXLabel;
        private System.Windows.Forms.ComboBox constrainedVectorComboBox;
        private System.Windows.Forms.ComboBox alignedVectorComboBox;
        private System.Windows.Forms.Label angleLimitLabel;
        private System.Windows.Forms.TextBox angleLimitTextBox;
        private System.Windows.Forms.Button createAttitudeButton;
        private System.Windows.Forms.CheckBox showGraphicsCheckBox;
        private System.Windows.Forms.Label showGraphicsLabel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
