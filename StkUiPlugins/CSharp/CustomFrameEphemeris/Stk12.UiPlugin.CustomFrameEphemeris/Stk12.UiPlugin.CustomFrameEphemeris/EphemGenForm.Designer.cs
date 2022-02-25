namespace Stk12.UiPlugin.CustomFrameEphemeris
{
    partial class EphemGenForm
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
            this.components = new System.ComponentModel.Container();
            this.lblStkObjects = new System.Windows.Forms.Label();
            this.cbStkObjects = new System.Windows.Forms.ComboBox();
            this.coordinateSysBox = new System.Windows.Forms.TextBox();
            this.coordinateSysLabel = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.stepSizeLabel = new System.Windows.Forms.Label();
            this.stepSizeBox = new System.Windows.Forms.TextBox();
            this.computeEphemerisButton = new System.Windows.Forms.Button();
            this.createNewCheckBox = new System.Windows.Forms.CheckBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStkObjects
            // 
            this.lblStkObjects.AutoSize = true;
            this.lblStkObjects.Location = new System.Drawing.Point(19, 36);
            this.lblStkObjects.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStkObjects.Name = "lblStkObjects";
            this.lblStkObjects.Size = new System.Drawing.Size(105, 17);
            this.lblStkObjects.TabIndex = 1;
            this.lblStkObjects.Text = "Choose Object:";
            // 
            // cbStkObjects
            // 
            this.cbStkObjects.FormattingEnabled = true;
            this.cbStkObjects.Location = new System.Drawing.Point(132, 33);
            this.cbStkObjects.Margin = new System.Windows.Forms.Padding(4);
            this.cbStkObjects.Name = "cbStkObjects";
            this.cbStkObjects.Size = new System.Drawing.Size(378, 24);
            this.cbStkObjects.TabIndex = 2;
            // 
            // coordinateSysBox
            // 
            this.coordinateSysBox.Location = new System.Drawing.Point(156, 74);
            this.coordinateSysBox.Name = "coordinateSysBox";
            this.coordinateSysBox.Size = new System.Drawing.Size(354, 22);
            this.coordinateSysBox.TabIndex = 3;
            // 
            // coordinateSysLabel
            // 
            this.coordinateSysLabel.Location = new System.Drawing.Point(19, 77);
            this.coordinateSysLabel.Name = "coordinateSysLabel";
            this.coordinateSysLabel.Size = new System.Drawing.Size(131, 19);
            this.coordinateSysLabel.TabIndex = 4;
            this.coordinateSysLabel.Text = "Coordinate System:";
            // 
            // ToolTip
            // 
            this.ToolTip.ToolTipTitle = "Coordinate Frame Formatting";
            // 
            // fileNameBox
            // 
            this.fileNameBox.Location = new System.Drawing.Point(100, 114);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.ReadOnly = true;
            this.fileNameBox.Size = new System.Drawing.Size(371, 22);
            this.fileNameBox.TabIndex = 5;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(19, 117);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(67, 17);
            this.fileNameLabel.TabIndex = 6;
            this.fileNameLabel.Text = "File Path:";
            // 
            // stepSizeLabel
            // 
            this.stepSizeLabel.AutoSize = true;
            this.stepSizeLabel.Location = new System.Drawing.Point(19, 155);
            this.stepSizeLabel.Name = "stepSizeLabel";
            this.stepSizeLabel.Size = new System.Drawing.Size(108, 17);
            this.stepSizeLabel.TabIndex = 7;
            this.stepSizeLabel.Text = "Step Size (sec):";
            // 
            // stepSizeBox
            // 
            this.stepSizeBox.Location = new System.Drawing.Point(132, 152);
            this.stepSizeBox.Name = "stepSizeBox";
            this.stepSizeBox.Size = new System.Drawing.Size(59, 22);
            this.stepSizeBox.TabIndex = 8;
            // 
            // computeEphemerisButton
            // 
            this.computeEphemerisButton.Location = new System.Drawing.Point(347, 155);
            this.computeEphemerisButton.Name = "computeEphemerisButton";
            this.computeEphemerisButton.Size = new System.Drawing.Size(163, 47);
            this.computeEphemerisButton.TabIndex = 9;
            this.computeEphemerisButton.Text = "Compute Ephemeris";
            this.computeEphemerisButton.UseVisualStyleBackColor = true;
            this.computeEphemerisButton.Click += new System.EventHandler(this.computeEphemerisButton_Click);
            // 
            // createNewCheckBox
            // 
            this.createNewCheckBox.AutoSize = true;
            this.createNewCheckBox.Location = new System.Drawing.Point(22, 187);
            this.createNewCheckBox.Name = "createNewCheckBox";
            this.createNewCheckBox.Size = new System.Drawing.Size(200, 21);
            this.createNewCheckBox.TabIndex = 11;
            this.createNewCheckBox.Text = "Create New Object From .e";
            this.createNewCheckBox.UseVisualStyleBackColor = true;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(477, 110);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(33, 31);
            this.browseButton.TabIndex = 12;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // EphemGenForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.createNewCheckBox);
            this.Controls.Add(this.computeEphemerisButton);
            this.Controls.Add(this.stepSizeBox);
            this.Controls.Add(this.stepSizeLabel);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.fileNameBox);
            this.Controls.Add(this.coordinateSysLabel);
            this.Controls.Add(this.coordinateSysBox);
            this.Controls.Add(this.cbStkObjects);
            this.Controls.Add(this.lblStkObjects);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1500, 1500);
            this.MinimumSize = new System.Drawing.Size(530, 230);
            this.Name = "EphemGenForm";
            this.Size = new System.Drawing.Size(1035, 288);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStkObjects;
        private System.Windows.Forms.ComboBox cbStkObjects;
        private System.Windows.Forms.TextBox coordinateSysBox;
        private System.Windows.Forms.Label coordinateSysLabel;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.TextBox fileNameBox;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Label stepSizeLabel;
        private System.Windows.Forms.TextBox stepSizeBox;
        private System.Windows.Forms.Button computeEphemerisButton;
        private System.Windows.Forms.CheckBox createNewCheckBox;
        private System.Windows.Forms.Button browseButton;
    }
}
