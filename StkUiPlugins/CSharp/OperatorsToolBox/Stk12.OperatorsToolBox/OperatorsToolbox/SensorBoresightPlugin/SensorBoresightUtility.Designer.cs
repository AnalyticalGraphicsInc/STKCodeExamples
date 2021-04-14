namespace OperatorsToolbox.SensorBoresightPlugin
{
    partial class SensorBoresightUtility
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SensorBoresightUtility));
            this.x = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.SelectedSensor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCreateSensorView = new System.Windows.Forms.Button();
            this.textboxPixels = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gboxDisplayOptions = new System.Windows.Forms.GroupBox();
            this.cbCrosshairs = new System.Windows.Forms.CheckBox();
            this.gboxCrosshairType = new System.Windows.Forms.GroupBox();
            this.rbGrid = new System.Windows.Forms.RadioButton();
            this.rbCircular = new System.Windows.Forms.RadioButton();
            this.rbSquare = new System.Windows.Forms.RadioButton();
            this.cbLLA = new System.Windows.Forms.CheckBox();
            this.cbCompass = new System.Windows.Forms.CheckBox();
            this.cbRulers = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LinkToView = new System.Windows.Forms.CheckBox();
            this.SelectedView = new System.Windows.Forms.ComboBox();
            this.AutoUpVector = new System.Windows.Forms.CheckBox();
            this.UpVector = new System.Windows.Forms.ComboBox();
            this.gboxDisplayOptions.SuspendLayout();
            this.gboxCrosshairType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // x
            // 
            this.x.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("x.ImageStream")));
            this.x.TransparentColor = System.Drawing.Color.Transparent;
            this.x.Images.SetKeyName(0, "x-mark.png");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Selected Sensor:";
            // 
            // SelectedSensor
            // 
            this.SelectedSensor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedSensor.BackColor = System.Drawing.Color.DimGray;
            this.SelectedSensor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectedSensor.ForeColor = System.Drawing.Color.White;
            this.SelectedSensor.FormattingEnabled = true;
            this.SelectedSensor.Location = new System.Drawing.Point(98, 44);
            this.SelectedSensor.Name = "SelectedSensor";
            this.SelectedSensor.Size = new System.Drawing.Size(184, 21);
            this.SelectedSensor.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Pixels";
            // 
            // buttonCreateSensorView
            // 
            this.buttonCreateSensorView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateSensorView.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonCreateSensorView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCreateSensorView.Location = new System.Drawing.Point(84, 392);
            this.buttonCreateSensorView.Name = "buttonCreateSensorView";
            this.buttonCreateSensorView.Size = new System.Drawing.Size(148, 28);
            this.buttonCreateSensorView.TabIndex = 35;
            this.buttonCreateSensorView.Text = "Create Sensor View";
            this.buttonCreateSensorView.UseVisualStyleBackColor = false;
            this.buttonCreateSensorView.Click += new System.EventHandler(this.buttonCreateSensorView_Click_1);
            // 
            // textboxPixels
            // 
            this.textboxPixels.Location = new System.Drawing.Point(120, 79);
            this.textboxPixels.Name = "textboxPixels";
            this.textboxPixels.Size = new System.Drawing.Size(46, 20);
            this.textboxPixels.TabIndex = 33;
            this.textboxPixels.Text = "600";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Vertical Window Size:";
            // 
            // gboxDisplayOptions
            // 
            this.gboxDisplayOptions.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.gboxDisplayOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxDisplayOptions.Controls.Add(this.UpVector);
            this.gboxDisplayOptions.Controls.Add(this.AutoUpVector);
            this.gboxDisplayOptions.Controls.Add(this.cbCrosshairs);
            this.gboxDisplayOptions.Controls.Add(this.gboxCrosshairType);
            this.gboxDisplayOptions.Controls.Add(this.cbLLA);
            this.gboxDisplayOptions.Controls.Add(this.cbCompass);
            this.gboxDisplayOptions.Controls.Add(this.cbRulers);
            this.gboxDisplayOptions.ForeColor = System.Drawing.Color.White;
            this.gboxDisplayOptions.Location = new System.Drawing.Point(4, 105);
            this.gboxDisplayOptions.Name = "gboxDisplayOptions";
            this.gboxDisplayOptions.Size = new System.Drawing.Size(335, 187);
            this.gboxDisplayOptions.TabIndex = 34;
            this.gboxDisplayOptions.TabStop = false;
            this.gboxDisplayOptions.Text = "Display Options";
            // 
            // cbCrosshairs
            // 
            this.cbCrosshairs.AutoSize = true;
            this.cbCrosshairs.Checked = true;
            this.cbCrosshairs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCrosshairs.Location = new System.Drawing.Point(13, 79);
            this.cbCrosshairs.Name = "cbCrosshairs";
            this.cbCrosshairs.Size = new System.Drawing.Size(104, 17);
            this.cbCrosshairs.TabIndex = 10;
            this.cbCrosshairs.Text = "Show Crosshairs";
            this.cbCrosshairs.UseVisualStyleBackColor = true;
            this.cbCrosshairs.CheckedChanged += new System.EventHandler(this.cbCrosshairs_CheckedChanged);
            // 
            // gboxCrosshairType
            // 
            this.gboxCrosshairType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxCrosshairType.Controls.Add(this.rbGrid);
            this.gboxCrosshairType.Controls.Add(this.rbCircular);
            this.gboxCrosshairType.Controls.Add(this.rbSquare);
            this.gboxCrosshairType.ForeColor = System.Drawing.Color.White;
            this.gboxCrosshairType.Location = new System.Drawing.Point(6, 102);
            this.gboxCrosshairType.Name = "gboxCrosshairType";
            this.gboxCrosshairType.Size = new System.Drawing.Size(323, 52);
            this.gboxCrosshairType.TabIndex = 9;
            this.gboxCrosshairType.TabStop = false;
            this.gboxCrosshairType.Text = "Crosshair Type";
            // 
            // rbGrid
            // 
            this.rbGrid.AutoSize = true;
            this.rbGrid.Location = new System.Drawing.Point(163, 19);
            this.rbGrid.Name = "rbGrid";
            this.rbGrid.Size = new System.Drawing.Size(44, 17);
            this.rbGrid.TabIndex = 2;
            this.rbGrid.Text = "Grid";
            this.rbGrid.UseVisualStyleBackColor = true;
            // 
            // rbCircular
            // 
            this.rbCircular.AutoSize = true;
            this.rbCircular.Location = new System.Drawing.Point(86, 19);
            this.rbCircular.Name = "rbCircular";
            this.rbCircular.Size = new System.Drawing.Size(60, 17);
            this.rbCircular.TabIndex = 1;
            this.rbCircular.Text = "Circular";
            this.rbCircular.UseVisualStyleBackColor = true;
            // 
            // rbSquare
            // 
            this.rbSquare.AutoSize = true;
            this.rbSquare.Checked = true;
            this.rbSquare.Location = new System.Drawing.Point(19, 19);
            this.rbSquare.Name = "rbSquare";
            this.rbSquare.Size = new System.Drawing.Size(59, 17);
            this.rbSquare.TabIndex = 0;
            this.rbSquare.TabStop = true;
            this.rbSquare.Text = "Square";
            this.rbSquare.UseVisualStyleBackColor = true;
            // 
            // cbLLA
            // 
            this.cbLLA.AutoSize = true;
            this.cbLLA.Checked = true;
            this.cbLLA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLLA.Location = new System.Drawing.Point(13, 28);
            this.cbLLA.Name = "cbLLA";
            this.cbLLA.Size = new System.Drawing.Size(139, 17);
            this.cbLLA.TabIndex = 7;
            this.cbLLA.Text = "Show Lat/Lon/Alt/Time";
            this.cbLLA.UseVisualStyleBackColor = true;
            // 
            // cbCompass
            // 
            this.cbCompass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCompass.AutoSize = true;
            this.cbCompass.Checked = true;
            this.cbCompass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCompass.Location = new System.Drawing.Point(179, 28);
            this.cbCompass.Name = "cbCompass";
            this.cbCompass.Size = new System.Drawing.Size(99, 17);
            this.cbCompass.TabIndex = 6;
            this.cbCompass.Text = "Show Compass";
            this.cbCompass.UseVisualStyleBackColor = true;
            this.cbCompass.CheckedChanged += new System.EventHandler(this.cbCompass_CheckedChanged);
            // 
            // cbRulers
            // 
            this.cbRulers.AutoSize = true;
            this.cbRulers.Location = new System.Drawing.Point(13, 51);
            this.cbRulers.Name = "cbRulers";
            this.cbRulers.Size = new System.Drawing.Size(86, 17);
            this.cbRulers.TabIndex = 5;
            this.cbRulers.Text = "Show Rulers";
            this.cbRulers.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Firebrick;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 23);
            this.label1.TabIndex = 31;
            this.label1.Text = "Sensor Boresight View";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.x;
            this.Cancel.Location = new System.Drawing.Point(324, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 30;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.LinkToView);
            this.groupBox1.Controls.Add(this.SelectedView);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(4, 298);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 88);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Smart View Options";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Smart View:";
            // 
            // LinkToView
            // 
            this.LinkToView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkToView.AutoSize = true;
            this.LinkToView.Location = new System.Drawing.Point(25, 19);
            this.LinkToView.Name = "LinkToView";
            this.LinkToView.Size = new System.Drawing.Size(118, 17);
            this.LinkToView.TabIndex = 11;
            this.LinkToView.Text = "Link To Smart View";
            this.LinkToView.UseVisualStyleBackColor = true;
            this.LinkToView.CheckedChanged += new System.EventHandler(this.LinkToView_CheckedChanged);
            // 
            // SelectedView
            // 
            this.SelectedView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedView.BackColor = System.Drawing.Color.DimGray;
            this.SelectedView.Enabled = false;
            this.SelectedView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectedView.ForeColor = System.Drawing.Color.White;
            this.SelectedView.FormattingEnabled = true;
            this.SelectedView.Location = new System.Drawing.Point(104, 42);
            this.SelectedView.Name = "SelectedView";
            this.SelectedView.Size = new System.Drawing.Size(174, 21);
            this.SelectedView.TabIndex = 40;
            // 
            // AutoUpVector
            // 
            this.AutoUpVector.AutoSize = true;
            this.AutoUpVector.Checked = true;
            this.AutoUpVector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoUpVector.Location = new System.Drawing.Point(13, 160);
            this.AutoUpVector.Name = "AutoUpVector";
            this.AutoUpVector.Size = new System.Drawing.Size(132, 17);
            this.AutoUpVector.TabIndex = 11;
            this.AutoUpVector.Text = "Auto Select Up Vector";
            this.AutoUpVector.UseVisualStyleBackColor = true;
            this.AutoUpVector.CheckedChanged += new System.EventHandler(this.AutoUpVector_CheckedChanged);
            // 
            // UpVector
            // 
            this.UpVector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpVector.BackColor = System.Drawing.Color.DimGray;
            this.UpVector.Enabled = false;
            this.UpVector.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UpVector.ForeColor = System.Drawing.Color.White;
            this.UpVector.FormattingEnabled = true;
            this.UpVector.Location = new System.Drawing.Point(161, 158);
            this.UpVector.Name = "UpVector";
            this.UpVector.Size = new System.Drawing.Size(62, 21);
            this.UpVector.TabIndex = 40;
            // 
            // SensorBoresightUtility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SelectedSensor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCreateSensorView);
            this.Controls.Add(this.textboxPixels);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gboxDisplayOptions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "SensorBoresightUtility";
            this.Size = new System.Drawing.Size(350, 700);
            this.gboxDisplayOptions.ResumeLayout(false);
            this.gboxDisplayOptions.PerformLayout();
            this.gboxCrosshairType.ResumeLayout(false);
            this.gboxCrosshairType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ImageList x;
        private System.Windows.Forms.Button buttonCreateSensorView;
        private System.Windows.Forms.TextBox textboxPixels;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gboxDisplayOptions;
        private System.Windows.Forms.CheckBox cbCrosshairs;
        private System.Windows.Forms.GroupBox gboxCrosshairType;
        private System.Windows.Forms.RadioButton rbGrid;
        private System.Windows.Forms.RadioButton rbCircular;
        private System.Windows.Forms.RadioButton rbSquare;
        private System.Windows.Forms.CheckBox cbLLA;
        private System.Windows.Forms.CheckBox cbCompass;
        private System.Windows.Forms.CheckBox cbRulers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SelectedSensor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox LinkToView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox SelectedView;
        private System.Windows.Forms.CheckBox AutoUpVector;
        private System.Windows.Forms.ComboBox UpVector;
    }
}