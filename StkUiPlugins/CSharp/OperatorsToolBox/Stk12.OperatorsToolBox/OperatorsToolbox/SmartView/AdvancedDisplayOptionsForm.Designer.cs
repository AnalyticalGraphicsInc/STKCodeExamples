namespace OperatorsToolbox.SmartView
{
    partial class AdvancedDisplayOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedDisplayOptionsForm));
            this.EnableEllipsoid = new System.Windows.Forms.CheckBox();
            this.EllipsoidDefinition = new System.Windows.Forms.GroupBox();
            this.label49 = new System.Windows.Forms.Label();
            this.EllipsoidZ = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.EllipsoidY = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.EllipsoidX = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.UseProxBox = new System.Windows.Forms.CheckBox();
            this.UseGEOBox = new System.Windows.Forms.CheckBox();
            this.GeoBoxOptions = new System.Windows.Forms.GroupBox();
            this.label43 = new System.Windows.Forms.Label();
            this.GEOEastWest = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.GEORadius = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.GeoNorthSouth = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.GEOLongitude = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.UseSecondaryDataDisplay = new System.Windows.Forms.CheckBox();
            this.DataDisplayOptions = new System.Windows.Forms.GroupBox();
            this.PredataObject = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.DisplayObject = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.DisplayReport = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.DisplayLocation = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.Apply = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.PlaneSettings = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GridSpacing = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VectorScalingFactor = new System.Windows.Forms.TextBox();
            this.UseVectorScaling = new System.Windows.Forms.CheckBox();
            this.RemoveSensorViewLink = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.OverrideTimeStep = new System.Windows.Forms.CheckBox();
            this.TimeStep = new System.Windows.Forms.TextBox();
            this.EllipsoidDefinition.SuspendLayout();
            this.GeoBoxOptions.SuspendLayout();
            this.DataDisplayOptions.SuspendLayout();
            this.PlaneSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // EnableEllipsoid
            // 
            this.EnableEllipsoid.AutoSize = true;
            this.EnableEllipsoid.Location = new System.Drawing.Point(11, 119);
            this.EnableEllipsoid.Margin = new System.Windows.Forms.Padding(2);
            this.EnableEllipsoid.Name = "EnableEllipsoid";
            this.EnableEllipsoid.Size = new System.Drawing.Size(144, 17);
            this.EnableEllipsoid.TabIndex = 35;
            this.EnableEllipsoid.Text = "Enable Proximity Ellipsoid";
            this.EnableEllipsoid.UseVisualStyleBackColor = true;
            this.EnableEllipsoid.CheckedChanged += new System.EventHandler(this.EnableEllipsoid_CheckedChanged);
            // 
            // EllipsoidDefinition
            // 
            this.EllipsoidDefinition.Controls.Add(this.label49);
            this.EllipsoidDefinition.Controls.Add(this.EllipsoidZ);
            this.EllipsoidDefinition.Controls.Add(this.label50);
            this.EllipsoidDefinition.Controls.Add(this.label47);
            this.EllipsoidDefinition.Controls.Add(this.EllipsoidY);
            this.EllipsoidDefinition.Controls.Add(this.label48);
            this.EllipsoidDefinition.Controls.Add(this.label46);
            this.EllipsoidDefinition.Controls.Add(this.EllipsoidX);
            this.EllipsoidDefinition.Controls.Add(this.label45);
            this.EllipsoidDefinition.ForeColor = System.Drawing.Color.White;
            this.EllipsoidDefinition.Location = new System.Drawing.Point(11, 141);
            this.EllipsoidDefinition.Margin = new System.Windows.Forms.Padding(2);
            this.EllipsoidDefinition.Name = "EllipsoidDefinition";
            this.EllipsoidDefinition.Padding = new System.Windows.Forms.Padding(2);
            this.EllipsoidDefinition.Size = new System.Drawing.Size(280, 116);
            this.EllipsoidDefinition.TabIndex = 34;
            this.EllipsoidDefinition.TabStop = false;
            this.EllipsoidDefinition.Text = "Ellipsoid Size";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(76, 73);
            this.label49.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(21, 13);
            this.label49.TabIndex = 40;
            this.label49.Text = "km";
            // 
            // EllipsoidZ
            // 
            this.EllipsoidZ.BackColor = System.Drawing.Color.DimGray;
            this.EllipsoidZ.ForeColor = System.Drawing.Color.White;
            this.EllipsoidZ.Location = new System.Drawing.Point(22, 71);
            this.EllipsoidZ.Margin = new System.Windows.Forms.Padding(2);
            this.EllipsoidZ.Name = "EllipsoidZ";
            this.EllipsoidZ.Size = new System.Drawing.Size(53, 20);
            this.EllipsoidZ.TabIndex = 39;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(4, 72);
            this.label50.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(18, 13);
            this.label50.TabIndex = 38;
            this.label50.Text = "R:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(76, 47);
            this.label47.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(21, 13);
            this.label47.TabIndex = 37;
            this.label47.Text = "km";
            // 
            // EllipsoidY
            // 
            this.EllipsoidY.BackColor = System.Drawing.Color.DimGray;
            this.EllipsoidY.ForeColor = System.Drawing.Color.White;
            this.EllipsoidY.Location = new System.Drawing.Point(22, 45);
            this.EllipsoidY.Margin = new System.Windows.Forms.Padding(2);
            this.EllipsoidY.Name = "EllipsoidY";
            this.EllipsoidY.Size = new System.Drawing.Size(53, 20);
            this.EllipsoidY.TabIndex = 36;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(5, 46);
            this.label48.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(17, 13);
            this.label48.TabIndex = 35;
            this.label48.Text = "C:";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(76, 21);
            this.label46.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(21, 13);
            this.label46.TabIndex = 34;
            this.label46.Text = "km";
            // 
            // EllipsoidX
            // 
            this.EllipsoidX.BackColor = System.Drawing.Color.DimGray;
            this.EllipsoidX.ForeColor = System.Drawing.Color.White;
            this.EllipsoidX.Location = new System.Drawing.Point(22, 19);
            this.EllipsoidX.Margin = new System.Windows.Forms.Padding(2);
            this.EllipsoidX.Name = "EllipsoidX";
            this.EllipsoidX.Size = new System.Drawing.Size(53, 20);
            this.EllipsoidX.TabIndex = 33;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(5, 20);
            this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(13, 13);
            this.label45.TabIndex = 32;
            this.label45.Text = "I:";
            // 
            // UseProxBox
            // 
            this.UseProxBox.AutoSize = true;
            this.UseProxBox.Location = new System.Drawing.Point(11, 44);
            this.UseProxBox.Margin = new System.Windows.Forms.Padding(2);
            this.UseProxBox.Name = "UseProxBox";
            this.UseProxBox.Size = new System.Drawing.Size(189, 17);
            this.UseProxBox.TabIndex = 33;
            this.UseProxBox.Text = "Use Proximity Plane (Satellite Only)";
            this.UseProxBox.UseVisualStyleBackColor = true;
            this.UseProxBox.CheckedChanged += new System.EventHandler(this.UseProxBox_CheckedChanged);
            // 
            // UseGEOBox
            // 
            this.UseGEOBox.AutoSize = true;
            this.UseGEOBox.Location = new System.Drawing.Point(11, 261);
            this.UseGEOBox.Margin = new System.Windows.Forms.Padding(2);
            this.UseGEOBox.Name = "UseGEOBox";
            this.UseGEOBox.Size = new System.Drawing.Size(218, 17);
            this.UseGEOBox.TabIndex = 37;
            this.UseGEOBox.Text = "Enable Geostationary Box (Satellite Only)";
            this.UseGEOBox.UseVisualStyleBackColor = true;
            this.UseGEOBox.CheckedChanged += new System.EventHandler(this.UseGEOBox_CheckedChanged);
            // 
            // GeoBoxOptions
            // 
            this.GeoBoxOptions.Controls.Add(this.label43);
            this.GeoBoxOptions.Controls.Add(this.GEOEastWest);
            this.GeoBoxOptions.Controls.Add(this.label44);
            this.GeoBoxOptions.Controls.Add(this.label41);
            this.GeoBoxOptions.Controls.Add(this.GEORadius);
            this.GeoBoxOptions.Controls.Add(this.label42);
            this.GeoBoxOptions.Controls.Add(this.label39);
            this.GeoBoxOptions.Controls.Add(this.GeoNorthSouth);
            this.GeoBoxOptions.Controls.Add(this.label40);
            this.GeoBoxOptions.Controls.Add(this.label38);
            this.GeoBoxOptions.Controls.Add(this.GEOLongitude);
            this.GeoBoxOptions.Controls.Add(this.label37);
            this.GeoBoxOptions.ForeColor = System.Drawing.Color.White;
            this.GeoBoxOptions.Location = new System.Drawing.Point(11, 286);
            this.GeoBoxOptions.Margin = new System.Windows.Forms.Padding(2);
            this.GeoBoxOptions.Name = "GeoBoxOptions";
            this.GeoBoxOptions.Padding = new System.Windows.Forms.Padding(2);
            this.GeoBoxOptions.Size = new System.Drawing.Size(280, 133);
            this.GeoBoxOptions.TabIndex = 36;
            this.GeoBoxOptions.TabStop = false;
            this.GeoBoxOptions.Text = "Geostationary Box Options";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(134, 93);
            this.label43.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(25, 13);
            this.label43.TabIndex = 44;
            this.label43.Text = "deg";
            // 
            // GEOEastWest
            // 
            this.GEOEastWest.BackColor = System.Drawing.Color.DimGray;
            this.GEOEastWest.ForeColor = System.Drawing.Color.White;
            this.GEOEastWest.Location = new System.Drawing.Point(74, 92);
            this.GEOEastWest.Margin = new System.Windows.Forms.Padding(2);
            this.GEOEastWest.Name = "GEOEastWest";
            this.GEOEastWest.Size = new System.Drawing.Size(60, 20);
            this.GEOEastWest.TabIndex = 43;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(12, 94);
            this.label44.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(61, 13);
            this.label44.TabIndex = 42;
            this.label44.Text = "East/West:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(135, 71);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(21, 13);
            this.label41.TabIndex = 41;
            this.label41.Text = "km";
            // 
            // GEORadius
            // 
            this.GEORadius.BackColor = System.Drawing.Color.DimGray;
            this.GEORadius.ForeColor = System.Drawing.Color.White;
            this.GEORadius.Location = new System.Drawing.Point(74, 69);
            this.GEORadius.Margin = new System.Windows.Forms.Padding(2);
            this.GEORadius.Name = "GEORadius";
            this.GEORadius.Size = new System.Drawing.Size(60, 20);
            this.GEORadius.TabIndex = 40;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(30, 71);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(43, 13);
            this.label42.TabIndex = 39;
            this.label42.Text = "Radius:";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(134, 46);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(25, 13);
            this.label39.TabIndex = 38;
            this.label39.Text = "deg";
            // 
            // GeoNorthSouth
            // 
            this.GeoNorthSouth.BackColor = System.Drawing.Color.DimGray;
            this.GeoNorthSouth.ForeColor = System.Drawing.Color.White;
            this.GeoNorthSouth.Location = new System.Drawing.Point(74, 45);
            this.GeoNorthSouth.Margin = new System.Windows.Forms.Padding(2);
            this.GeoNorthSouth.Name = "GeoNorthSouth";
            this.GeoNorthSouth.Size = new System.Drawing.Size(60, 20);
            this.GeoNorthSouth.TabIndex = 37;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(4, 47);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(69, 13);
            this.label40.TabIndex = 36;
            this.label40.Text = "North/South:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(134, 24);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(25, 13);
            this.label38.TabIndex = 35;
            this.label38.Text = "deg";
            // 
            // GEOLongitude
            // 
            this.GEOLongitude.BackColor = System.Drawing.Color.DimGray;
            this.GEOLongitude.ForeColor = System.Drawing.Color.White;
            this.GEOLongitude.Location = new System.Drawing.Point(74, 22);
            this.GEOLongitude.Margin = new System.Windows.Forms.Padding(2);
            this.GEOLongitude.Name = "GEOLongitude";
            this.GEOLongitude.Size = new System.Drawing.Size(60, 20);
            this.GEOLongitude.TabIndex = 34;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(15, 24);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(57, 13);
            this.label37.TabIndex = 33;
            this.label37.Text = "Longitude:";
            // 
            // UseSecondaryDataDisplay
            // 
            this.UseSecondaryDataDisplay.AutoSize = true;
            this.UseSecondaryDataDisplay.Location = new System.Drawing.Point(12, 426);
            this.UseSecondaryDataDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.UseSecondaryDataDisplay.Name = "UseSecondaryDataDisplay";
            this.UseSecondaryDataDisplay.Size = new System.Drawing.Size(176, 17);
            this.UseSecondaryDataDisplay.TabIndex = 39;
            this.UseSecondaryDataDisplay.Text = "Enable Secondary Data Display";
            this.UseSecondaryDataDisplay.UseVisualStyleBackColor = true;
            this.UseSecondaryDataDisplay.CheckedChanged += new System.EventHandler(this.UseSecondaryDataDisplay_CheckedChanged);
            // 
            // DataDisplayOptions
            // 
            this.DataDisplayOptions.Controls.Add(this.PredataObject);
            this.DataDisplayOptions.Controls.Add(this.label17);
            this.DataDisplayOptions.Controls.Add(this.DisplayObject);
            this.DataDisplayOptions.Controls.Add(this.label21);
            this.DataDisplayOptions.Controls.Add(this.DisplayReport);
            this.DataDisplayOptions.Controls.Add(this.label20);
            this.DataDisplayOptions.Controls.Add(this.DisplayLocation);
            this.DataDisplayOptions.Controls.Add(this.label19);
            this.DataDisplayOptions.ForeColor = System.Drawing.Color.White;
            this.DataDisplayOptions.Location = new System.Drawing.Point(11, 447);
            this.DataDisplayOptions.Margin = new System.Windows.Forms.Padding(2);
            this.DataDisplayOptions.Name = "DataDisplayOptions";
            this.DataDisplayOptions.Padding = new System.Windows.Forms.Padding(2);
            this.DataDisplayOptions.Size = new System.Drawing.Size(280, 140);
            this.DataDisplayOptions.TabIndex = 38;
            this.DataDisplayOptions.TabStop = false;
            this.DataDisplayOptions.Text = "Data Display Options";
            // 
            // PredataObject
            // 
            this.PredataObject.BackColor = System.Drawing.Color.DimGray;
            this.PredataObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PredataObject.ForeColor = System.Drawing.Color.White;
            this.PredataObject.FormattingEnabled = true;
            this.PredataObject.Location = new System.Drawing.Point(99, 108);
            this.PredataObject.Margin = new System.Windows.Forms.Padding(2);
            this.PredataObject.Name = "PredataObject";
            this.PredataObject.Size = new System.Drawing.Size(138, 21);
            this.PredataObject.TabIndex = 27;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(11, 110);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(81, 13);
            this.label17.TabIndex = 26;
            this.label17.Text = "Predata Object:";
            // 
            // DisplayObject
            // 
            this.DisplayObject.BackColor = System.Drawing.Color.DimGray;
            this.DisplayObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DisplayObject.ForeColor = System.Drawing.Color.White;
            this.DisplayObject.FormattingEnabled = true;
            this.DisplayObject.Location = new System.Drawing.Point(99, 50);
            this.DisplayObject.Margin = new System.Windows.Forms.Padding(2);
            this.DisplayObject.Name = "DisplayObject";
            this.DisplayObject.Size = new System.Drawing.Size(138, 21);
            this.DisplayObject.TabIndex = 25;
            this.DisplayObject.SelectedIndexChanged += new System.EventHandler(this.DisplayObject_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(14, 53);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(78, 13);
            this.label21.TabIndex = 24;
            this.label21.Text = "Display Object:";
            // 
            // DisplayReport
            // 
            this.DisplayReport.BackColor = System.Drawing.Color.DimGray;
            this.DisplayReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DisplayReport.ForeColor = System.Drawing.Color.White;
            this.DisplayReport.FormattingEnabled = true;
            this.DisplayReport.Location = new System.Drawing.Point(99, 80);
            this.DisplayReport.Margin = new System.Windows.Forms.Padding(2);
            this.DisplayReport.Name = "DisplayReport";
            this.DisplayReport.Size = new System.Drawing.Size(138, 21);
            this.DisplayReport.TabIndex = 23;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(13, 82);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 13);
            this.label20.TabIndex = 22;
            this.label20.Text = "Display Report:";
            // 
            // DisplayLocation
            // 
            this.DisplayLocation.BackColor = System.Drawing.Color.DimGray;
            this.DisplayLocation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DisplayLocation.ForeColor = System.Drawing.Color.White;
            this.DisplayLocation.FormattingEnabled = true;
            this.DisplayLocation.Location = new System.Drawing.Point(99, 21);
            this.DisplayLocation.Margin = new System.Windows.Forms.Padding(2);
            this.DisplayLocation.Name = "DisplayLocation";
            this.DisplayLocation.Size = new System.Drawing.Size(138, 21);
            this.DisplayLocation.TabIndex = 21;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(4, 24);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 13);
            this.label19.TabIndex = 20;
            this.label19.Text = "Display Location:";
            // 
            // Apply
            // 
            this.Apply.BackColor = System.Drawing.Color.SteelBlue;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Apply.Location = new System.Drawing.Point(53, 625);
            this.Apply.Margin = new System.Windows.Forms.Padding(2);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(82, 28);
            this.Apply.TabIndex = 40;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.Location = new System.Drawing.Point(154, 625);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 28);
            this.Cancel.TabIndex = 41;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // PlaneSettings
            // 
            this.PlaneSettings.Controls.Add(this.label1);
            this.PlaneSettings.Controls.Add(this.GridSpacing);
            this.PlaneSettings.Controls.Add(this.label2);
            this.PlaneSettings.ForeColor = System.Drawing.Color.White;
            this.PlaneSettings.Location = new System.Drawing.Point(11, 66);
            this.PlaneSettings.Name = "PlaneSettings";
            this.PlaneSettings.Size = new System.Drawing.Size(280, 48);
            this.PlaneSettings.TabIndex = 42;
            this.PlaneSettings.TabStop = false;
            this.PlaneSettings.Text = "Plane Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "km";
            // 
            // GridSpacing
            // 
            this.GridSpacing.BackColor = System.Drawing.Color.DimGray;
            this.GridSpacing.ForeColor = System.Drawing.Color.White;
            this.GridSpacing.Location = new System.Drawing.Point(81, 16);
            this.GridSpacing.Margin = new System.Windows.Forms.Padding(2);
            this.GridSpacing.Name = "GridSpacing";
            this.GridSpacing.Size = new System.Drawing.Size(53, 20);
            this.GridSpacing.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Grid Spacing:";
            // 
            // VectorScalingFactor
            // 
            this.VectorScalingFactor.BackColor = System.Drawing.Color.DimGray;
            this.VectorScalingFactor.ForeColor = System.Drawing.Color.White;
            this.VectorScalingFactor.Location = new System.Drawing.Point(139, 9);
            this.VectorScalingFactor.Margin = new System.Windows.Forms.Padding(2);
            this.VectorScalingFactor.Name = "VectorScalingFactor";
            this.VectorScalingFactor.Size = new System.Drawing.Size(53, 20);
            this.VectorScalingFactor.TabIndex = 45;
            // 
            // UseVectorScaling
            // 
            this.UseVectorScaling.AutoSize = true;
            this.UseVectorScaling.Location = new System.Drawing.Point(11, 11);
            this.UseVectorScaling.Margin = new System.Windows.Forms.Padding(2);
            this.UseVectorScaling.Name = "UseVectorScaling";
            this.UseVectorScaling.Size = new System.Drawing.Size(124, 17);
            this.UseVectorScaling.TabIndex = 46;
            this.UseVectorScaling.Text = "Apply Vector Scaling";
            this.UseVectorScaling.UseVisualStyleBackColor = true;
            this.UseVectorScaling.CheckedChanged += new System.EventHandler(this.UseVectorScaling_CheckedChanged);
            // 
            // RemoveSensorViewLink
            // 
            this.RemoveSensorViewLink.BackColor = System.Drawing.Color.SteelBlue;
            this.RemoveSensorViewLink.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveSensorViewLink.Location = new System.Drawing.Point(223, 9);
            this.RemoveSensorViewLink.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveSensorViewLink.Name = "RemoveSensorViewLink";
            this.RemoveSensorViewLink.Size = new System.Drawing.Size(68, 52);
            this.RemoveSensorViewLink.TabIndex = 47;
            this.RemoveSensorViewLink.Text = "Remove Sensor View Link";
            this.RemoveSensorViewLink.UseVisualStyleBackColor = false;
            this.RemoveSensorViewLink.Click += new System.EventHandler(this.RemoveSensorViewLink_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 593);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "sec";
            // 
            // OverrideTimeStep
            // 
            this.OverrideTimeStep.AutoSize = true;
            this.OverrideTimeStep.Location = new System.Drawing.Point(12, 593);
            this.OverrideTimeStep.Margin = new System.Windows.Forms.Padding(2);
            this.OverrideTimeStep.Name = "OverrideTimeStep";
            this.OverrideTimeStep.Size = new System.Drawing.Size(112, 17);
            this.OverrideTimeStep.TabIndex = 44;
            this.OverrideTimeStep.Text = "Override Timestep";
            this.OverrideTimeStep.UseVisualStyleBackColor = true;
            this.OverrideTimeStep.CheckedChanged += new System.EventHandler(this.OverrideTimeStep_CheckedChanged);
            // 
            // TimeStep
            // 
            this.TimeStep.BackColor = System.Drawing.Color.DimGray;
            this.TimeStep.ForeColor = System.Drawing.Color.White;
            this.TimeStep.Location = new System.Drawing.Point(128, 590);
            this.TimeStep.Margin = new System.Windows.Forms.Padding(2);
            this.TimeStep.Name = "TimeStep";
            this.TimeStep.Size = new System.Drawing.Size(53, 20);
            this.TimeStep.TabIndex = 45;
            // 
            // AdvancedDisplayOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(307, 667);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RemoveSensorViewLink);
            this.Controls.Add(this.UseVectorScaling);
            this.Controls.Add(this.OverrideTimeStep);
            this.Controls.Add(this.TimeStep);
            this.Controls.Add(this.VectorScalingFactor);
            this.Controls.Add(this.PlaneSettings);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.UseSecondaryDataDisplay);
            this.Controls.Add(this.DataDisplayOptions);
            this.Controls.Add(this.UseGEOBox);
            this.Controls.Add(this.GeoBoxOptions);
            this.Controls.Add(this.EnableEllipsoid);
            this.Controls.Add(this.EllipsoidDefinition);
            this.Controls.Add(this.UseProxBox);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdvancedDisplayOptionsForm";
            this.Text = "Advanced Displays";
            this.EllipsoidDefinition.ResumeLayout(false);
            this.EllipsoidDefinition.PerformLayout();
            this.GeoBoxOptions.ResumeLayout(false);
            this.GeoBoxOptions.PerformLayout();
            this.DataDisplayOptions.ResumeLayout(false);
            this.DataDisplayOptions.PerformLayout();
            this.PlaneSettings.ResumeLayout(false);
            this.PlaneSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox EnableEllipsoid;
        private System.Windows.Forms.GroupBox EllipsoidDefinition;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox EllipsoidZ;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox EllipsoidY;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox EllipsoidX;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.CheckBox UseProxBox;
        private System.Windows.Forms.CheckBox UseGEOBox;
        private System.Windows.Forms.GroupBox GeoBoxOptions;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox GEOEastWest;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox GEORadius;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox GeoNorthSouth;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox GEOLongitude;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.CheckBox UseSecondaryDataDisplay;
        private System.Windows.Forms.GroupBox DataDisplayOptions;
        private System.Windows.Forms.ComboBox PredataObject;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox DisplayObject;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox DisplayReport;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox DisplayLocation;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.GroupBox PlaneSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox GridSpacing;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox VectorScalingFactor;
        private System.Windows.Forms.CheckBox UseVectorScaling;
        private System.Windows.Forms.Button RemoveSensorViewLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox OverrideTimeStep;
        private System.Windows.Forms.TextBox TimeStep;
    }
}