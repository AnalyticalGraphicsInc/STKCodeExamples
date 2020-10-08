namespace SensorBoresightViewPlugin
{
    partial class SensorViewSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SensorViewSetup));
            this.gboxDisplayOptions = new System.Windows.Forms.GroupBox();
            this.cbCrosshairs = new System.Windows.Forms.CheckBox();
            this.gboxCrosshairType = new System.Windows.Forms.GroupBox();
            this.rbGrid = new System.Windows.Forms.RadioButton();
            this.rbCircular = new System.Windows.Forms.RadioButton();
            this.rbSquare = new System.Windows.Forms.RadioButton();
            this.cbLLA = new System.Windows.Forms.CheckBox();
            this.cbCompass = new System.Windows.Forms.CheckBox();
            this.cbRulers = new System.Windows.Forms.CheckBox();
            this.textboxPixels = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCreateSensorView = new System.Windows.Forms.Button();
            this.gboxDisplayOptions.SuspendLayout();
            this.gboxCrosshairType.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboxDisplayOptions
            // 
            this.gboxDisplayOptions.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.gboxDisplayOptions.Controls.Add(this.cbCrosshairs);
            this.gboxDisplayOptions.Controls.Add(this.gboxCrosshairType);
            this.gboxDisplayOptions.Controls.Add(this.cbLLA);
            this.gboxDisplayOptions.Controls.Add(this.cbCompass);
            this.gboxDisplayOptions.Controls.Add(this.cbRulers);
            this.gboxDisplayOptions.Location = new System.Drawing.Point(12, 38);
            this.gboxDisplayOptions.Name = "gboxDisplayOptions";
            this.gboxDisplayOptions.Size = new System.Drawing.Size(182, 227);
            this.gboxDisplayOptions.TabIndex = 11;
            this.gboxDisplayOptions.TabStop = false;
            this.gboxDisplayOptions.Text = "Display Options";
            // 
            // cbCrosshairs
            // 
            this.cbCrosshairs.AutoSize = true;
            this.cbCrosshairs.Checked = true;
            this.cbCrosshairs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCrosshairs.Location = new System.Drawing.Point(23, 97);
            this.cbCrosshairs.Name = "cbCrosshairs";
            this.cbCrosshairs.Size = new System.Drawing.Size(104, 17);
            this.cbCrosshairs.TabIndex = 10;
            this.cbCrosshairs.Text = "Show Crosshairs";
            this.cbCrosshairs.UseVisualStyleBackColor = true;
            // 
            // gboxCrosshairType
            // 
            this.gboxCrosshairType.Controls.Add(this.rbGrid);
            this.gboxCrosshairType.Controls.Add(this.rbCircular);
            this.gboxCrosshairType.Controls.Add(this.rbSquare);
            this.gboxCrosshairType.Location = new System.Drawing.Point(16, 124);
            this.gboxCrosshairType.Name = "gboxCrosshairType";
            this.gboxCrosshairType.Size = new System.Drawing.Size(146, 88);
            this.gboxCrosshairType.TabIndex = 9;
            this.gboxCrosshairType.TabStop = false;
            this.gboxCrosshairType.Text = "Crosshair Type";
            // 
            // rbGrid
            // 
            this.rbGrid.AutoSize = true;
            this.rbGrid.Location = new System.Drawing.Point(19, 65);
            this.rbGrid.Name = "rbGrid";
            this.rbGrid.Size = new System.Drawing.Size(44, 17);
            this.rbGrid.TabIndex = 2;
            this.rbGrid.Text = "Grid";
            this.rbGrid.UseVisualStyleBackColor = true;
            // 
            // rbCircular
            // 
            this.rbCircular.AutoSize = true;
            this.rbCircular.Location = new System.Drawing.Point(19, 42);
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
            this.cbLLA.Location = new System.Drawing.Point(23, 74);
            this.cbLLA.Name = "cbLLA";
            this.cbLLA.Size = new System.Drawing.Size(139, 17);
            this.cbLLA.TabIndex = 7;
            this.cbLLA.Text = "Show Lat/Lon/Alt/Time";
            this.cbLLA.UseVisualStyleBackColor = true;
            // 
            // cbCompass
            // 
            this.cbCompass.AutoSize = true;
            this.cbCompass.Checked = true;
            this.cbCompass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCompass.Location = new System.Drawing.Point(23, 51);
            this.cbCompass.Name = "cbCompass";
            this.cbCompass.Size = new System.Drawing.Size(99, 17);
            this.cbCompass.TabIndex = 6;
            this.cbCompass.Text = "Show Compass";
            this.cbCompass.UseVisualStyleBackColor = true;
            // 
            // cbRulers
            // 
            this.cbRulers.AutoSize = true;
            this.cbRulers.Checked = true;
            this.cbRulers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRulers.Location = new System.Drawing.Point(23, 28);
            this.cbRulers.Name = "cbRulers";
            this.cbRulers.Size = new System.Drawing.Size(86, 17);
            this.cbRulers.TabIndex = 5;
            this.cbRulers.Text = "Show Rulers";
            this.cbRulers.UseVisualStyleBackColor = true;
            // 
            // textboxPixels
            // 
            this.textboxPixels.Location = new System.Drawing.Point(128, 12);
            this.textboxPixels.Name = "textboxPixels";
            this.textboxPixels.Size = new System.Drawing.Size(46, 20);
            this.textboxPixels.TabIndex = 4;
            this.textboxPixels.Text = "600";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vertical Window Size:";
            // 
            // buttonCreateSensorView
            // 
            this.buttonCreateSensorView.Location = new System.Drawing.Point(12, 271);
            this.buttonCreateSensorView.Name = "buttonCreateSensorView";
            this.buttonCreateSensorView.Size = new System.Drawing.Size(182, 23);
            this.buttonCreateSensorView.TabIndex = 12;
            this.buttonCreateSensorView.Text = "Create Sensor View";
            this.buttonCreateSensorView.UseVisualStyleBackColor = true;
            this.buttonCreateSensorView.Click += new System.EventHandler(this.buttonCreateSensorView_Click);
            // 
            // SensorViewSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 305);
            this.Controls.Add(this.buttonCreateSensorView);
            this.Controls.Add(this.textboxPixels);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gboxDisplayOptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SensorViewSetup";
            this.Text = "Sensor View Setup";
            this.gboxDisplayOptions.ResumeLayout(false);
            this.gboxDisplayOptions.PerformLayout();
            this.gboxCrosshairType.ResumeLayout(false);
            this.gboxCrosshairType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gboxDisplayOptions;
        private System.Windows.Forms.TextBox textboxPixels;
        private System.Windows.Forms.CheckBox cbCrosshairs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gboxCrosshairType;
        private System.Windows.Forms.RadioButton rbGrid;
        private System.Windows.Forms.RadioButton rbCircular;
        private System.Windows.Forms.RadioButton rbSquare;
        private System.Windows.Forms.CheckBox cbLLA;
        private System.Windows.Forms.CheckBox cbCompass;
        private System.Windows.Forms.CheckBox cbRulers;
        private System.Windows.Forms.Button buttonCreateSensorView;
    }
}