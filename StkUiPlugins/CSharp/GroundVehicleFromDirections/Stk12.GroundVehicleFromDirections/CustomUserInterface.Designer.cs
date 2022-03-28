namespace Agi.Ui.Directions
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
            this.trafficDirectionsTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toLabel = new System.Windows.Forms.Label();
            this.toTextBox = new System.Windows.Forms.TextBox();
            this.fromLabel = new System.Windows.Forms.Label();
            this.fromTextBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.fromObjectComboBox = new System.Windows.Forms.ComboBox();
            this.toLabel2 = new System.Windows.Forms.Label();
            this.fromLabel2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.numVehicleTextBox = new System.Windows.Forms.TextBox();
            this.numVehicleLabel = new System.Windows.Forms.Label();
            this.boundingGroupBox = new System.Windows.Forms.GroupBox();
            this.minLatLabel = new System.Windows.Forms.Label();
            this.maxLonLabel = new System.Windows.Forms.Label();
            this.minLonLabel = new System.Windows.Forms.Label();
            this.maxLatLabel = new System.Windows.Forms.Label();
            this.maxLonTextBox = new System.Windows.Forms.TextBox();
            this.minLonTextBox = new System.Windows.Forms.TextBox();
            this.minLatTextBox = new System.Windows.Forms.TextBox();
            this.maxLatTextBox = new System.Windows.Forms.TextBox();
            this.speedGroupBox = new System.Windows.Forms.GroupBox();
            this.speedTextBox = new System.Windows.Forms.TextBox();
            this.speedUnitsComboBox = new System.Windows.Forms.ComboBox();
            this.altGroupBox = new System.Windows.Forms.GroupBox();
            this.altUnitsComboBox = new System.Windows.Forms.ComboBox();
            this.altOffsetTextBox = new System.Windows.Forms.TextBox();
            this.terrainCheckBox = new System.Windows.Forms.CheckBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.LoadGV = new System.Windows.Forms.Button();
            this.toObjectComboBox = new System.Windows.Forms.ComboBox();
            this.trafficDirectionsTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.boundingGroupBox.SuspendLayout();
            this.speedGroupBox.SuspendLayout();
            this.altGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // trafficDirectionsTabControl
            // 
            this.trafficDirectionsTabControl.Controls.Add(this.tabPage1);
            this.trafficDirectionsTabControl.Controls.Add(this.tabPage3);
            this.trafficDirectionsTabControl.Controls.Add(this.tabPage2);
            this.trafficDirectionsTabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.trafficDirectionsTabControl.Location = new System.Drawing.Point(0, 0);
            this.trafficDirectionsTabControl.Name = "trafficDirectionsTabControl";
            this.trafficDirectionsTabControl.SelectedIndex = 0;
            this.trafficDirectionsTabControl.Size = new System.Drawing.Size(276, 140);
            this.trafficDirectionsTabControl.TabIndex = 17;
            this.trafficDirectionsTabControl.SelectedIndexChanged += new System.EventHandler(this.trafficDirectionsTabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.toLabel);
            this.tabPage1.Controls.Add(this.toTextBox);
            this.tabPage1.Controls.Add(this.fromLabel);
            this.tabPage1.Controls.Add(this.fromTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(268, 114);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Address";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(8, 56);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(16, 13);
            this.toLabel.TabIndex = 22;
            this.toLabel.Text = "to";
            // 
            // toTextBox
            // 
            this.toTextBox.Location = new System.Drawing.Point(8, 75);
            this.toTextBox.Name = "toTextBox";
            this.toTextBox.Size = new System.Drawing.Size(250, 20);
            this.toTextBox.TabIndex = 21;
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Location = new System.Drawing.Point(8, 10);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(27, 13);
            this.fromLabel.TabIndex = 20;
            this.fromLabel.Text = "from";
            // 
            // fromTextBox
            // 
            this.fromTextBox.Location = new System.Drawing.Point(8, 29);
            this.fromTextBox.Name = "fromTextBox";
            this.fromTextBox.Size = new System.Drawing.Size(250, 20);
            this.fromTextBox.TabIndex = 19;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.toObjectComboBox);
            this.tabPage3.Controls.Add(this.fromObjectComboBox);
            this.tabPage3.Controls.Add(this.toLabel2);
            this.tabPage3.Controls.Add(this.fromLabel2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(268, 114);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Points";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // fromObjectComboBox
            // 
            this.fromObjectComboBox.FormattingEnabled = true;
            this.fromObjectComboBox.Location = new System.Drawing.Point(18, 31);
            this.fromObjectComboBox.Name = "fromObjectComboBox";
            this.fromObjectComboBox.Size = new System.Drawing.Size(231, 21);
            this.fromObjectComboBox.TabIndex = 25;
            // 
            // toLabel2
            // 
            this.toLabel2.AutoSize = true;
            this.toLabel2.Location = new System.Drawing.Point(15, 54);
            this.toLabel2.Name = "toLabel2";
            this.toLabel2.Size = new System.Drawing.Size(16, 13);
            this.toLabel2.TabIndex = 24;
            this.toLabel2.Text = "to";
            // 
            // fromLabel2
            // 
            this.fromLabel2.AutoSize = true;
            this.fromLabel2.Location = new System.Drawing.Point(15, 8);
            this.fromLabel2.Name = "fromLabel2";
            this.fromLabel2.Size = new System.Drawing.Size(27, 13);
            this.fromLabel2.TabIndex = 23;
            this.fromLabel2.Text = "from";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.numVehicleTextBox);
            this.tabPage2.Controls.Add(this.numVehicleLabel);
            this.tabPage2.Controls.Add(this.boundingGroupBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(268, 114);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Traffic";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // numVehicleTextBox
            // 
            this.numVehicleTextBox.Location = new System.Drawing.Point(160, 30);
            this.numVehicleTextBox.Name = "numVehicleTextBox";
            this.numVehicleTextBox.Size = new System.Drawing.Size(70, 20);
            this.numVehicleTextBox.TabIndex = 35;
            this.numVehicleTextBox.Text = "10";
            // 
            // numVehicleLabel
            // 
            this.numVehicleLabel.AutoSize = true;
            this.numVehicleLabel.Location = new System.Drawing.Point(157, 14);
            this.numVehicleLabel.Name = "numVehicleLabel";
            this.numVehicleLabel.Size = new System.Drawing.Size(98, 13);
            this.numVehicleLabel.TabIndex = 34;
            this.numVehicleLabel.Text = "Number of vehicles";
            // 
            // boundingGroupBox
            // 
            this.boundingGroupBox.Controls.Add(this.minLatLabel);
            this.boundingGroupBox.Controls.Add(this.maxLonLabel);
            this.boundingGroupBox.Controls.Add(this.minLonLabel);
            this.boundingGroupBox.Controls.Add(this.maxLatLabel);
            this.boundingGroupBox.Controls.Add(this.maxLonTextBox);
            this.boundingGroupBox.Controls.Add(this.minLonTextBox);
            this.boundingGroupBox.Controls.Add(this.minLatTextBox);
            this.boundingGroupBox.Controls.Add(this.maxLatTextBox);
            this.boundingGroupBox.Location = new System.Drawing.Point(6, 8);
            this.boundingGroupBox.Name = "boundingGroupBox";
            this.boundingGroupBox.Size = new System.Drawing.Size(129, 100);
            this.boundingGroupBox.TabIndex = 0;
            this.boundingGroupBox.TabStop = false;
            this.boundingGroupBox.Text = "Lat/Lon Bounding Box";
            // 
            // minLatLabel
            // 
            this.minLatLabel.AutoSize = true;
            this.minLatLabel.Location = new System.Drawing.Point(81, 77);
            this.minLatLabel.Name = "minLatLabel";
            this.minLatLabel.Size = new System.Drawing.Size(14, 13);
            this.minLatLabel.TabIndex = 6;
            this.minLatLabel.Text = "S";
            // 
            // maxLonLabel
            // 
            this.maxLonLabel.AutoSize = true;
            this.maxLonLabel.Location = new System.Drawing.Point(111, 48);
            this.maxLonLabel.Name = "maxLonLabel";
            this.maxLonLabel.Size = new System.Drawing.Size(14, 13);
            this.maxLonLabel.TabIndex = 5;
            this.maxLonLabel.Text = "E";
            // 
            // minLonLabel
            // 
            this.minLonLabel.AutoSize = true;
            this.minLonLabel.Location = new System.Drawing.Point(45, 49);
            this.minLonLabel.Name = "minLonLabel";
            this.minLonLabel.Size = new System.Drawing.Size(18, 13);
            this.minLonLabel.TabIndex = 4;
            this.minLonLabel.Text = "W";
            // 
            // maxLatLabel
            // 
            this.maxLatLabel.AutoSize = true;
            this.maxLatLabel.Location = new System.Drawing.Point(81, 22);
            this.maxLatLabel.Name = "maxLatLabel";
            this.maxLatLabel.Size = new System.Drawing.Size(15, 13);
            this.maxLatLabel.TabIndex = 1;
            this.maxLatLabel.Text = "N";
            // 
            // maxLonTextBox
            // 
            this.maxLonTextBox.Location = new System.Drawing.Point(73, 45);
            this.maxLonTextBox.Name = "maxLonTextBox";
            this.maxLonTextBox.Size = new System.Drawing.Size(38, 20);
            this.maxLonTextBox.TabIndex = 3;
            this.maxLonTextBox.Text = "-75.55";
            // 
            // minLonTextBox
            // 
            this.minLonTextBox.Location = new System.Drawing.Point(6, 46);
            this.minLonTextBox.Name = "minLonTextBox";
            this.minLonTextBox.Size = new System.Drawing.Size(38, 20);
            this.minLonTextBox.TabIndex = 2;
            this.minLonTextBox.Text = "-75.65";
            // 
            // minLatTextBox
            // 
            this.minLatTextBox.Location = new System.Drawing.Point(41, 74);
            this.minLatTextBox.Name = "minLatTextBox";
            this.minLatTextBox.Size = new System.Drawing.Size(38, 20);
            this.minLatTextBox.TabIndex = 1;
            this.minLatTextBox.Text = "40.0";
            // 
            // maxLatTextBox
            // 
            this.maxLatTextBox.Location = new System.Drawing.Point(41, 19);
            this.maxLatTextBox.Name = "maxLatTextBox";
            this.maxLatTextBox.Size = new System.Drawing.Size(38, 20);
            this.maxLatTextBox.TabIndex = 0;
            this.maxLatTextBox.Text = "40.1";
            // 
            // speedGroupBox
            // 
            this.speedGroupBox.Controls.Add(this.speedTextBox);
            this.speedGroupBox.Controls.Add(this.speedUnitsComboBox);
            this.speedGroupBox.Location = new System.Drawing.Point(286, 80);
            this.speedGroupBox.Name = "speedGroupBox";
            this.speedGroupBox.Size = new System.Drawing.Size(117, 44);
            this.speedGroupBox.TabIndex = 27;
            this.speedGroupBox.TabStop = false;
            this.speedGroupBox.Text = "speed";
            // 
            // speedTextBox
            // 
            this.speedTextBox.Location = new System.Drawing.Point(6, 17);
            this.speedTextBox.Name = "speedTextBox";
            this.speedTextBox.Size = new System.Drawing.Size(36, 20);
            this.speedTextBox.TabIndex = 9;
            this.speedTextBox.Text = "60";
            // 
            // speedUnitsComboBox
            // 
            this.speedUnitsComboBox.FormattingEnabled = true;
            this.speedUnitsComboBox.Items.AddRange(new object[] {
            "km/h",
            "mph",
            "m/s"});
            this.speedUnitsComboBox.Location = new System.Drawing.Point(48, 17);
            this.speedUnitsComboBox.Name = "speedUnitsComboBox";
            this.speedUnitsComboBox.Size = new System.Drawing.Size(58, 21);
            this.speedUnitsComboBox.TabIndex = 11;
            this.speedUnitsComboBox.Text = "mph";
            // 
            // altGroupBox
            // 
            this.altGroupBox.Controls.Add(this.altUnitsComboBox);
            this.altGroupBox.Controls.Add(this.altOffsetTextBox);
            this.altGroupBox.Location = new System.Drawing.Point(403, 30);
            this.altGroupBox.Name = "altGroupBox";
            this.altGroupBox.Size = new System.Drawing.Size(108, 44);
            this.altGroupBox.TabIndex = 26;
            this.altGroupBox.TabStop = false;
            this.altGroupBox.Text = "alt. offset";
            // 
            // altUnitsComboBox
            // 
            this.altUnitsComboBox.FormattingEnabled = true;
            this.altUnitsComboBox.Items.AddRange(new object[] {
            "m",
            "km",
            "ft"});
            this.altUnitsComboBox.Location = new System.Drawing.Point(53, 15);
            this.altUnitsComboBox.Name = "altUnitsComboBox";
            this.altUnitsComboBox.Size = new System.Drawing.Size(43, 21);
            this.altUnitsComboBox.TabIndex = 14;
            this.altUnitsComboBox.Text = "m";
            // 
            // altOffsetTextBox
            // 
            this.altOffsetTextBox.Location = new System.Drawing.Point(6, 16);
            this.altOffsetTextBox.Name = "altOffsetTextBox";
            this.altOffsetTextBox.Size = new System.Drawing.Size(41, 20);
            this.altOffsetTextBox.TabIndex = 13;
            this.altOffsetTextBox.Text = "0.0";
            // 
            // terrainCheckBox
            // 
            this.terrainCheckBox.AutoSize = true;
            this.terrainCheckBox.Checked = true;
            this.terrainCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.terrainCheckBox.Location = new System.Drawing.Point(423, 97);
            this.terrainCheckBox.Name = "terrainCheckBox";
            this.terrainCheckBox.Size = new System.Drawing.Size(55, 17);
            this.terrainCheckBox.TabIndex = 25;
            this.terrainCheckBox.Text = "terrain";
            this.terrainCheckBox.UseVisualStyleBackColor = true;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(289, 29);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(70, 13);
            this.nameLabel.TabIndex = 24;
            this.nameLabel.Text = "vehicle name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(292, 46);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(70, 20);
            this.nameTextBox.TabIndex = 23;
            this.nameTextBox.Text = "GV1";
            // 
            // LoadGV
            // 
            this.LoadGV.Location = new System.Drawing.Point(516, 91);
            this.LoadGV.Name = "LoadGV";
            this.LoadGV.Size = new System.Drawing.Size(52, 23);
            this.LoadGV.TabIndex = 17;
            this.LoadGV.Text = "load";
            this.LoadGV.UseVisualStyleBackColor = true;
            this.LoadGV.Click += new System.EventHandler(this.Button1_Click);
            // 
            // toObjectComboBox
            // 
            this.toObjectComboBox.FormattingEnabled = true;
            this.toObjectComboBox.Location = new System.Drawing.Point(18, 75);
            this.toObjectComboBox.Name = "toObjectComboBox";
            this.toObjectComboBox.Size = new System.Drawing.Size(231, 21);
            this.toObjectComboBox.TabIndex = 26;
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.speedGroupBox);
            this.Controls.Add(this.trafficDirectionsTabControl);
            this.Controls.Add(this.altGroupBox);
            this.Controls.Add(this.LoadGV);
            this.Controls.Add(this.terrainCheckBox);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(586, 140);
            this.trafficDirectionsTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.boundingGroupBox.ResumeLayout(false);
            this.boundingGroupBox.PerformLayout();
            this.speedGroupBox.ResumeLayout(false);
            this.speedGroupBox.PerformLayout();
            this.altGroupBox.ResumeLayout(false);
            this.altGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl trafficDirectionsTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox speedGroupBox;
        private System.Windows.Forms.TextBox speedTextBox;
        private System.Windows.Forms.ComboBox speedUnitsComboBox;
        private System.Windows.Forms.GroupBox altGroupBox;
        private System.Windows.Forms.ComboBox altUnitsComboBox;
        private System.Windows.Forms.TextBox altOffsetTextBox;
        private System.Windows.Forms.CheckBox terrainCheckBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.TextBox toTextBox;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.TextBox fromTextBox;
        private System.Windows.Forms.Button LoadGV;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox boundingGroupBox;
        private System.Windows.Forms.Label minLatLabel;
        private System.Windows.Forms.Label maxLonLabel;
        private System.Windows.Forms.Label minLonLabel;
        private System.Windows.Forms.Label maxLatLabel;
        private System.Windows.Forms.TextBox maxLonTextBox;
        private System.Windows.Forms.TextBox minLonTextBox;
        private System.Windows.Forms.TextBox minLatTextBox;
        private System.Windows.Forms.TextBox maxLatTextBox;
        private System.Windows.Forms.TextBox numVehicleTextBox;
        private System.Windows.Forms.Label numVehicleLabel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox fromObjectComboBox;
        private System.Windows.Forms.Label toLabel2;
        private System.Windows.Forms.Label fromLabel2;
        private System.Windows.Forms.ComboBox toObjectComboBox;
    }
}
