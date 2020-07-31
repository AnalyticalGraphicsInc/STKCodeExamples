namespace OperatorsToolbox.SmartView
{
    partial class EditGeoDriftForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditGeoDriftForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.WindowSelect = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.HideShowOptions = new System.Windows.Forms.Button();
            this.ObjectHideShow = new System.Windows.Forms.CheckBox();
            this.GEODriftDefinitionBox = new System.Windows.Forms.GroupBox();
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
            this.GEOViewTarget = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.GEOUseDataDisplay = new System.Windows.Forms.CheckBox();
            this.GEODataDisplayOptions = new System.Windows.Forms.GroupBox();
            this.GEODisplayObject = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.GEODisplayReport = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.GEODisplayLocation = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.GEOViewName = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.UseCurrentTime = new System.Windows.Forms.CheckBox();
            this.RefreshTime = new System.Windows.Forms.Button();
            this.CurrentTime = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.GEODriftDefinitionBox.SuspendLayout();
            this.GeoBoxOptions.SuspendLayout();
            this.GEODataDisplayOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.WindowSelect);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(352, 61);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Window Definition";
            // 
            // WindowSelect
            // 
            this.WindowSelect.BackColor = System.Drawing.Color.DimGray;
            this.WindowSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.WindowSelect.ForeColor = System.Drawing.Color.White;
            this.WindowSelect.FormattingEnabled = true;
            this.WindowSelect.Location = new System.Drawing.Point(57, 25);
            this.WindowSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.WindowSelect.Name = "WindowSelect";
            this.WindowSelect.Size = new System.Drawing.Size(128, 21);
            this.WindowSelect.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Window:";
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.Location = new System.Drawing.Point(184, 488);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 28);
            this.Cancel.TabIndex = 27;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Apply
            // 
            this.Apply.BackColor = System.Drawing.Color.SteelBlue;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Apply.Location = new System.Drawing.Point(82, 488);
            this.Apply.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(82, 28);
            this.Apply.TabIndex = 26;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // HideShowOptions
            // 
            this.HideShowOptions.BackColor = System.Drawing.Color.SteelBlue;
            this.HideShowOptions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.HideShowOptions.Location = new System.Drawing.Point(194, 447);
            this.HideShowOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.HideShowOptions.Name = "HideShowOptions";
            this.HideShowOptions.Size = new System.Drawing.Size(106, 28);
            this.HideShowOptions.TabIndex = 25;
            this.HideShowOptions.Text = "Hide/Show Options";
            this.HideShowOptions.UseVisualStyleBackColor = false;
            this.HideShowOptions.Click += new System.EventHandler(this.HideShowOptions_Click);
            // 
            // ObjectHideShow
            // 
            this.ObjectHideShow.AutoSize = true;
            this.ObjectHideShow.Location = new System.Drawing.Point(26, 450);
            this.ObjectHideShow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ObjectHideShow.Name = "ObjectHideShow";
            this.ObjectHideShow.Size = new System.Drawing.Size(171, 17);
            this.ObjectHideShow.TabIndex = 24;
            this.ObjectHideShow.Text = "Incorporate Object Hide/Show";
            this.ObjectHideShow.UseVisualStyleBackColor = true;
            this.ObjectHideShow.CheckedChanged += new System.EventHandler(this.ObjectHideShow_CheckedChanged);
            // 
            // GEODriftDefinitionBox
            // 
            this.GEODriftDefinitionBox.Controls.Add(this.UseGEOBox);
            this.GEODriftDefinitionBox.Controls.Add(this.GeoBoxOptions);
            this.GEODriftDefinitionBox.Controls.Add(this.GEOViewTarget);
            this.GEODriftDefinitionBox.Controls.Add(this.label36);
            this.GEODriftDefinitionBox.Controls.Add(this.GEOUseDataDisplay);
            this.GEODriftDefinitionBox.Controls.Add(this.GEODataDisplayOptions);
            this.GEODriftDefinitionBox.Controls.Add(this.GEOViewName);
            this.GEODriftDefinitionBox.Controls.Add(this.label32);
            this.GEODriftDefinitionBox.ForeColor = System.Drawing.Color.White;
            this.GEODriftDefinitionBox.Location = new System.Drawing.Point(9, 100);
            this.GEODriftDefinitionBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEODriftDefinitionBox.Name = "GEODriftDefinitionBox";
            this.GEODriftDefinitionBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEODriftDefinitionBox.Size = new System.Drawing.Size(352, 342);
            this.GEODriftDefinitionBox.TabIndex = 28;
            this.GEODriftDefinitionBox.TabStop = false;
            this.GEODriftDefinitionBox.Text = "GEO Drift View Definition";
            this.GEODriftDefinitionBox.Enter += new System.EventHandler(this.GEODriftDefinitionBox_Enter);
            // 
            // UseGEOBox
            // 
            this.UseGEOBox.AutoSize = true;
            this.UseGEOBox.Location = new System.Drawing.Point(14, 112);
            this.UseGEOBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UseGEOBox.Name = "UseGEOBox";
            this.UseGEOBox.Size = new System.Drawing.Size(148, 17);
            this.UseGEOBox.TabIndex = 32;
            this.UseGEOBox.Text = "Enable Geostationary Box";
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
            this.GeoBoxOptions.Location = new System.Drawing.Point(14, 137);
            this.GeoBoxOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GeoBoxOptions.Name = "GeoBoxOptions";
            this.GeoBoxOptions.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GeoBoxOptions.Size = new System.Drawing.Size(323, 76);
            this.GeoBoxOptions.TabIndex = 31;
            this.GeoBoxOptions.TabStop = false;
            this.GeoBoxOptions.Text = "Geostationary Box Options";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(287, 46);
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
            this.GEOEastWest.Location = new System.Drawing.Point(227, 45);
            this.GEOEastWest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEOEastWest.Name = "GEOEastWest";
            this.GEOEastWest.Size = new System.Drawing.Size(60, 20);
            this.GEOEastWest.TabIndex = 43;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(168, 47);
            this.label44.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(61, 13);
            this.label44.TabIndex = 42;
            this.label44.Text = "East/West:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(287, 24);
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
            this.GEORadius.Location = new System.Drawing.Point(227, 22);
            this.GEORadius.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEORadius.Name = "GEORadius";
            this.GEORadius.Size = new System.Drawing.Size(60, 20);
            this.GEORadius.TabIndex = 40;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(183, 24);
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
            this.GeoNorthSouth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GeoNorthSouth.Name = "GeoNorthSouth";
            this.GeoNorthSouth.Size = new System.Drawing.Size(60, 20);
            this.GeoNorthSouth.TabIndex = 37;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(7, 47);
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
            this.GEOLongitude.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            // GEOViewTarget
            // 
            this.GEOViewTarget.BackColor = System.Drawing.Color.DimGray;
            this.GEOViewTarget.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GEOViewTarget.ForeColor = System.Drawing.Color.White;
            this.GEOViewTarget.FormattingEnabled = true;
            this.GEOViewTarget.Location = new System.Drawing.Point(88, 55);
            this.GEOViewTarget.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEOViewTarget.Name = "GEOViewTarget";
            this.GEOViewTarget.Size = new System.Drawing.Size(126, 21);
            this.GEOViewTarget.TabIndex = 30;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(18, 56);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(67, 13);
            this.label36.TabIndex = 29;
            this.label36.Text = "View Target:";
            // 
            // GEOUseDataDisplay
            // 
            this.GEOUseDataDisplay.AutoSize = true;
            this.GEOUseDataDisplay.Location = new System.Drawing.Point(14, 214);
            this.GEOUseDataDisplay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEOUseDataDisplay.Name = "GEOUseDataDisplay";
            this.GEOUseDataDisplay.Size = new System.Drawing.Size(122, 17);
            this.GEOUseDataDisplay.TabIndex = 29;
            this.GEOUseDataDisplay.Text = "Enable Data Display";
            this.GEOUseDataDisplay.UseVisualStyleBackColor = true;
            this.GEOUseDataDisplay.CheckedChanged += new System.EventHandler(this.GEOUseDataDisplay_CheckedChanged);
            // 
            // GEODataDisplayOptions
            // 
            this.GEODataDisplayOptions.Controls.Add(this.GEODisplayObject);
            this.GEODataDisplayOptions.Controls.Add(this.label33);
            this.GEODataDisplayOptions.Controls.Add(this.GEODisplayReport);
            this.GEODataDisplayOptions.Controls.Add(this.label34);
            this.GEODataDisplayOptions.Controls.Add(this.GEODisplayLocation);
            this.GEODataDisplayOptions.Controls.Add(this.label35);
            this.GEODataDisplayOptions.ForeColor = System.Drawing.Color.White;
            this.GEODataDisplayOptions.Location = new System.Drawing.Point(14, 240);
            this.GEODataDisplayOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEODataDisplayOptions.Name = "GEODataDisplayOptions";
            this.GEODataDisplayOptions.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEODataDisplayOptions.Size = new System.Drawing.Size(324, 96);
            this.GEODataDisplayOptions.TabIndex = 27;
            this.GEODataDisplayOptions.TabStop = false;
            this.GEODataDisplayOptions.Text = "Data Display Options";
            // 
            // GEODisplayObject
            // 
            this.GEODisplayObject.BackColor = System.Drawing.Color.DimGray;
            this.GEODisplayObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GEODisplayObject.ForeColor = System.Drawing.Color.White;
            this.GEODisplayObject.FormattingEnabled = true;
            this.GEODisplayObject.Location = new System.Drawing.Point(99, 24);
            this.GEODisplayObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEODisplayObject.Name = "GEODisplayObject";
            this.GEODisplayObject.Size = new System.Drawing.Size(138, 21);
            this.GEODisplayObject.TabIndex = 25;
            this.GEODisplayObject.SelectedIndexChanged += new System.EventHandler(this.GEODisplayObject_SelectedIndexChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(8, 27);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(78, 13);
            this.label33.TabIndex = 24;
            this.label33.Text = "Display Object:";
            // 
            // GEODisplayReport
            // 
            this.GEODisplayReport.BackColor = System.Drawing.Color.DimGray;
            this.GEODisplayReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GEODisplayReport.ForeColor = System.Drawing.Color.White;
            this.GEODisplayReport.FormattingEnabled = true;
            this.GEODisplayReport.Location = new System.Drawing.Point(99, 48);
            this.GEODisplayReport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEODisplayReport.Name = "GEODisplayReport";
            this.GEODisplayReport.Size = new System.Drawing.Size(138, 21);
            this.GEODisplayReport.TabIndex = 23;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(8, 50);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(79, 13);
            this.label34.TabIndex = 22;
            this.label34.Text = "Display Report:";
            // 
            // GEODisplayLocation
            // 
            this.GEODisplayLocation.BackColor = System.Drawing.Color.DimGray;
            this.GEODisplayLocation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GEODisplayLocation.ForeColor = System.Drawing.Color.White;
            this.GEODisplayLocation.FormattingEnabled = true;
            this.GEODisplayLocation.Location = new System.Drawing.Point(99, 72);
            this.GEODisplayLocation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEODisplayLocation.Name = "GEODisplayLocation";
            this.GEODisplayLocation.Size = new System.Drawing.Size(138, 21);
            this.GEODisplayLocation.TabIndex = 21;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(8, 75);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(88, 13);
            this.label35.TabIndex = 20;
            this.label35.Text = "Display Location:";
            // 
            // GEOViewName
            // 
            this.GEOViewName.BackColor = System.Drawing.Color.DimGray;
            this.GEOViewName.ForeColor = System.Drawing.Color.White;
            this.GEOViewName.Location = new System.Drawing.Point(88, 24);
            this.GEOViewName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GEOViewName.Name = "GEOViewName";
            this.GEOViewName.Size = new System.Drawing.Size(126, 20);
            this.GEOViewName.TabIndex = 30;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(22, 26);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(64, 13);
            this.label32.TabIndex = 29;
            this.label32.Text = "View Name:";
            // 
            // UseCurrentTime
            // 
            this.UseCurrentTime.AutoSize = true;
            this.UseCurrentTime.Location = new System.Drawing.Point(9, 78);
            this.UseCurrentTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UseCurrentTime.Name = "UseCurrentTime";
            this.UseCurrentTime.Size = new System.Drawing.Size(157, 17);
            this.UseCurrentTime.TabIndex = 31;
            this.UseCurrentTime.Text = "Use Current Animation Time";
            this.UseCurrentTime.UseVisualStyleBackColor = true;
            this.UseCurrentTime.CheckedChanged += new System.EventHandler(this.UseCurrentTime_CheckedChanged);
            // 
            // RefreshTime
            // 
            this.RefreshTime.BackColor = System.Drawing.Color.SteelBlue;
            this.RefreshTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RefreshTime.Location = new System.Drawing.Point(292, 75);
            this.RefreshTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RefreshTime.Name = "RefreshTime";
            this.RefreshTime.Size = new System.Drawing.Size(69, 23);
            this.RefreshTime.TabIndex = 45;
            this.RefreshTime.Text = "Refresh";
            this.RefreshTime.UseVisualStyleBackColor = false;
            this.RefreshTime.Click += new System.EventHandler(this.RefreshTime_Click);
            // 
            // CurrentTime
            // 
            this.CurrentTime.BackColor = System.Drawing.Color.DimGray;
            this.CurrentTime.ForeColor = System.Drawing.Color.White;
            this.CurrentTime.Location = new System.Drawing.Point(160, 77);
            this.CurrentTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CurrentTime.Name = "CurrentTime";
            this.CurrentTime.Size = new System.Drawing.Size(128, 20);
            this.CurrentTime.TabIndex = 46;
            // 
            // EditGeoDriftForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(374, 530);
            this.Controls.Add(this.RefreshTime);
            this.Controls.Add(this.CurrentTime);
            this.Controls.Add(this.UseCurrentTime);
            this.Controls.Add(this.GEODriftDefinitionBox);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.HideShowOptions);
            this.Controls.Add(this.ObjectHideShow);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "EditGeoDriftForm";
            this.Text = "Edit View";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GEODriftDefinitionBox.ResumeLayout(false);
            this.GEODriftDefinitionBox.PerformLayout();
            this.GeoBoxOptions.ResumeLayout(false);
            this.GeoBoxOptions.PerformLayout();
            this.GEODataDisplayOptions.ResumeLayout(false);
            this.GEODataDisplayOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox WindowSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button HideShowOptions;
        private System.Windows.Forms.CheckBox ObjectHideShow;
        private System.Windows.Forms.GroupBox GEODriftDefinitionBox;
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
        private System.Windows.Forms.ComboBox GEOViewTarget;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.CheckBox GEOUseDataDisplay;
        private System.Windows.Forms.GroupBox GEODataDisplayOptions;
        private System.Windows.Forms.ComboBox GEODisplayObject;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox GEODisplayReport;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox GEODisplayLocation;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox GEOViewName;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox UseCurrentTime;
        private System.Windows.Forms.Button RefreshTime;
        private System.Windows.Forms.TextBox CurrentTime;
    }
}