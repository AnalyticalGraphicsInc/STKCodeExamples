namespace OperatorsToolbox.SmartView
{
    partial class EditTargetThreatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditTargetThreatForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.WindowSelect = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TTDefinitionBox = new System.Windows.Forms.GroupBox();
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
            this.EnableEllipsoid = new System.Windows.Forms.CheckBox();
            this.TTViewName = new System.Windows.Forms.TextBox();
            this.ThreatList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label31 = new System.Windows.Forms.Label();
            this.UseProxBox = new System.Windows.Forms.CheckBox();
            this.TTUseDataDisplay = new System.Windows.Forms.CheckBox();
            this.TTDataDisplayOptions = new System.Windows.Forms.GroupBox();
            this.TTDisplayObject = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.TTDisplayReport = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.TTDisplayLocation = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.Unselect = new System.Windows.Forms.Button();
            this.Select = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.TargetSatellite = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.HideShowOptions = new System.Windows.Forms.Button();
            this.ObjectHideShow = new System.Windows.Forms.CheckBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.UseCurrentTime = new System.Windows.Forms.CheckBox();
            this.RefreshTime = new System.Windows.Forms.Button();
            this.CurrentTime = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.TTDefinitionBox.SuspendLayout();
            this.EllipsoidDefinition.SuspendLayout();
            this.TTDataDisplayOptions.SuspendLayout();
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
            this.groupBox1.TabIndex = 18;
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
            // TTDefinitionBox
            // 
            this.TTDefinitionBox.Controls.Add(this.EllipsoidDefinition);
            this.TTDefinitionBox.Controls.Add(this.EnableEllipsoid);
            this.TTDefinitionBox.Controls.Add(this.TTViewName);
            this.TTDefinitionBox.Controls.Add(this.ThreatList);
            this.TTDefinitionBox.Controls.Add(this.label31);
            this.TTDefinitionBox.Controls.Add(this.UseProxBox);
            this.TTDefinitionBox.Controls.Add(this.TTUseDataDisplay);
            this.TTDefinitionBox.Controls.Add(this.TTDataDisplayOptions);
            this.TTDefinitionBox.Controls.Add(this.Unselect);
            this.TTDefinitionBox.Controls.Add(this.Select);
            this.TTDefinitionBox.Controls.Add(this.label27);
            this.TTDefinitionBox.Controls.Add(this.TargetSatellite);
            this.TTDefinitionBox.Controls.Add(this.label26);
            this.TTDefinitionBox.ForeColor = System.Drawing.Color.White;
            this.TTDefinitionBox.Location = new System.Drawing.Point(9, 101);
            this.TTDefinitionBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TTDefinitionBox.Name = "TTDefinitionBox";
            this.TTDefinitionBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TTDefinitionBox.Size = new System.Drawing.Size(352, 342);
            this.TTDefinitionBox.TabIndex = 19;
            this.TTDefinitionBox.TabStop = false;
            this.TTDefinitionBox.Text = "Target/Threat View Definition";
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
            this.EllipsoidDefinition.Location = new System.Drawing.Point(6, 117);
            this.EllipsoidDefinition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EllipsoidDefinition.Name = "EllipsoidDefinition";
            this.EllipsoidDefinition.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EllipsoidDefinition.Size = new System.Drawing.Size(205, 89);
            this.EllipsoidDefinition.TabIndex = 33;
            this.EllipsoidDefinition.TabStop = false;
            this.EllipsoidDefinition.Text = "Ellipsoid Size";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(76, 52);
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
            this.EllipsoidZ.Location = new System.Drawing.Point(22, 50);
            this.EllipsoidZ.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EllipsoidZ.Name = "EllipsoidZ";
            this.EllipsoidZ.Size = new System.Drawing.Size(53, 20);
            this.EllipsoidZ.TabIndex = 39;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(4, 51);
            this.label50.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(17, 13);
            this.label50.TabIndex = 38;
            this.label50.Text = "Z:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(177, 21);
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
            this.EllipsoidY.Location = new System.Drawing.Point(123, 19);
            this.EllipsoidY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EllipsoidY.Name = "EllipsoidY";
            this.EllipsoidY.Size = new System.Drawing.Size(53, 20);
            this.EllipsoidY.TabIndex = 36;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(106, 20);
            this.label48.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(17, 13);
            this.label48.TabIndex = 35;
            this.label48.Text = "Y:";
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
            this.EllipsoidX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.label45.Size = new System.Drawing.Size(17, 13);
            this.label45.TabIndex = 32;
            this.label45.Text = "X:";
            // 
            // EnableEllipsoid
            // 
            this.EnableEllipsoid.AutoSize = true;
            this.EnableEllipsoid.Location = new System.Drawing.Point(4, 98);
            this.EnableEllipsoid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EnableEllipsoid.Name = "EnableEllipsoid";
            this.EnableEllipsoid.Size = new System.Drawing.Size(144, 17);
            this.EnableEllipsoid.TabIndex = 32;
            this.EnableEllipsoid.Text = "Enable Proximity Ellipsoid";
            this.EnableEllipsoid.UseVisualStyleBackColor = true;
            this.EnableEllipsoid.CheckedChanged += new System.EventHandler(this.EnableEllipsoid_CheckedChanged);
            // 
            // TTViewName
            // 
            this.TTViewName.BackColor = System.Drawing.Color.DimGray;
            this.TTViewName.ForeColor = System.Drawing.Color.White;
            this.TTViewName.Location = new System.Drawing.Point(86, 26);
            this.TTViewName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TTViewName.Name = "TTViewName";
            this.TTViewName.Size = new System.Drawing.Size(126, 20);
            this.TTViewName.TabIndex = 28;
            // 
            // ThreatList
            // 
            this.ThreatList.BackColor = System.Drawing.Color.DimGray;
            this.ThreatList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.ThreatList.ForeColor = System.Drawing.Color.White;
            this.ThreatList.HideSelection = false;
            this.ThreatList.Location = new System.Drawing.Point(222, 35);
            this.ThreatList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ThreatList.Name = "ThreatList";
            this.ThreatList.Size = new System.Drawing.Size(121, 134);
            this.ThreatList.TabIndex = 18;
            this.ThreatList.UseCompatibleStateImageBehavior = false;
            this.ThreatList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 160;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(22, 28);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(64, 13);
            this.label31.TabIndex = 27;
            this.label31.Text = "View Name:";
            // 
            // UseProxBox
            // 
            this.UseProxBox.AutoSize = true;
            this.UseProxBox.Location = new System.Drawing.Point(5, 76);
            this.UseProxBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UseProxBox.Name = "UseProxBox";
            this.UseProxBox.Size = new System.Drawing.Size(124, 17);
            this.UseProxBox.TabIndex = 20;
            this.UseProxBox.Text = "Enable Proximity Box";
            this.UseProxBox.UseVisualStyleBackColor = true;
            // 
            // TTUseDataDisplay
            // 
            this.TTUseDataDisplay.AutoSize = true;
            this.TTUseDataDisplay.Location = new System.Drawing.Point(9, 211);
            this.TTUseDataDisplay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TTUseDataDisplay.Name = "TTUseDataDisplay";
            this.TTUseDataDisplay.Size = new System.Drawing.Size(122, 17);
            this.TTUseDataDisplay.TabIndex = 20;
            this.TTUseDataDisplay.Text = "Enable Data Display";
            this.TTUseDataDisplay.UseVisualStyleBackColor = true;
            this.TTUseDataDisplay.CheckedChanged += new System.EventHandler(this.TTUseDataDisplay_CheckedChanged);
            // 
            // TTDataDisplayOptions
            // 
            this.TTDataDisplayOptions.Controls.Add(this.TTDisplayObject);
            this.TTDataDisplayOptions.Controls.Add(this.label28);
            this.TTDataDisplayOptions.Controls.Add(this.TTDisplayReport);
            this.TTDataDisplayOptions.Controls.Add(this.label29);
            this.TTDataDisplayOptions.Controls.Add(this.TTDisplayLocation);
            this.TTDataDisplayOptions.Controls.Add(this.label30);
            this.TTDataDisplayOptions.ForeColor = System.Drawing.Color.White;
            this.TTDataDisplayOptions.Location = new System.Drawing.Point(12, 233);
            this.TTDataDisplayOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TTDataDisplayOptions.Name = "TTDataDisplayOptions";
            this.TTDataDisplayOptions.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TTDataDisplayOptions.Size = new System.Drawing.Size(324, 96);
            this.TTDataDisplayOptions.TabIndex = 26;
            this.TTDataDisplayOptions.TabStop = false;
            this.TTDataDisplayOptions.Text = "Data Display Options";
            // 
            // TTDisplayObject
            // 
            this.TTDisplayObject.BackColor = System.Drawing.Color.DimGray;
            this.TTDisplayObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TTDisplayObject.ForeColor = System.Drawing.Color.White;
            this.TTDisplayObject.FormattingEnabled = true;
            this.TTDisplayObject.Location = new System.Drawing.Point(99, 24);
            this.TTDisplayObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TTDisplayObject.Name = "TTDisplayObject";
            this.TTDisplayObject.Size = new System.Drawing.Size(138, 21);
            this.TTDisplayObject.TabIndex = 25;
            this.TTDisplayObject.SelectedIndexChanged += new System.EventHandler(this.TTDisplayObject_SelectedIndexChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 27);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(78, 13);
            this.label28.TabIndex = 24;
            this.label28.Text = "Display Object:";
            // 
            // TTDisplayReport
            // 
            this.TTDisplayReport.BackColor = System.Drawing.Color.DimGray;
            this.TTDisplayReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TTDisplayReport.ForeColor = System.Drawing.Color.White;
            this.TTDisplayReport.FormattingEnabled = true;
            this.TTDisplayReport.Location = new System.Drawing.Point(99, 48);
            this.TTDisplayReport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TTDisplayReport.Name = "TTDisplayReport";
            this.TTDisplayReport.Size = new System.Drawing.Size(138, 21);
            this.TTDisplayReport.TabIndex = 23;
            this.TTDisplayReport.SelectedIndexChanged += new System.EventHandler(this.TTDisplayReport_SelectedIndexChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 50);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(79, 13);
            this.label29.TabIndex = 22;
            this.label29.Text = "Display Report:";
            // 
            // TTDisplayLocation
            // 
            this.TTDisplayLocation.BackColor = System.Drawing.Color.DimGray;
            this.TTDisplayLocation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TTDisplayLocation.ForeColor = System.Drawing.Color.White;
            this.TTDisplayLocation.FormattingEnabled = true;
            this.TTDisplayLocation.Location = new System.Drawing.Point(99, 72);
            this.TTDisplayLocation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TTDisplayLocation.Name = "TTDisplayLocation";
            this.TTDisplayLocation.Size = new System.Drawing.Size(138, 21);
            this.TTDisplayLocation.TabIndex = 21;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 75);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(88, 13);
            this.label30.TabIndex = 20;
            this.label30.Text = "Display Location:";
            // 
            // Unselect
            // 
            this.Unselect.BackColor = System.Drawing.Color.SteelBlue;
            this.Unselect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Unselect.Location = new System.Drawing.Point(288, 172);
            this.Unselect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Unselect.Name = "Unselect";
            this.Unselect.Size = new System.Drawing.Size(54, 28);
            this.Unselect.TabIndex = 20;
            this.Unselect.Text = "Unselect";
            this.Unselect.UseVisualStyleBackColor = false;
            this.Unselect.Click += new System.EventHandler(this.Unselect_Click);
            // 
            // Select
            // 
            this.Select.BackColor = System.Drawing.Color.SteelBlue;
            this.Select.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Select.Location = new System.Drawing.Point(222, 173);
            this.Select.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(54, 28);
            this.Select.TabIndex = 19;
            this.Select.Text = "Select";
            this.Select.UseVisualStyleBackColor = false;
            this.Select.Click += new System.EventHandler(this.Select_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(245, 15);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(76, 13);
            this.label27.TabIndex = 19;
            this.label27.Text = "Select Threats";
            // 
            // TargetSatellite
            // 
            this.TargetSatellite.BackColor = System.Drawing.Color.DimGray;
            this.TargetSatellite.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TargetSatellite.ForeColor = System.Drawing.Color.White;
            this.TargetSatellite.FormattingEnabled = true;
            this.TargetSatellite.Location = new System.Drawing.Point(86, 52);
            this.TargetSatellite.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TargetSatellite.Name = "TargetSatellite";
            this.TargetSatellite.Size = new System.Drawing.Size(126, 21);
            this.TargetSatellite.TabIndex = 17;
            this.TargetSatellite.SelectedIndexChanged += new System.EventHandler(this.TargetSatellite_SelectedIndexChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(3, 54);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(81, 13);
            this.label26.TabIndex = 16;
            this.label26.Text = "Target Satellite:";
            // 
            // HideShowOptions
            // 
            this.HideShowOptions.BackColor = System.Drawing.Color.SteelBlue;
            this.HideShowOptions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.HideShowOptions.Location = new System.Drawing.Point(177, 447);
            this.HideShowOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.HideShowOptions.Name = "HideShowOptions";
            this.HideShowOptions.Size = new System.Drawing.Size(106, 28);
            this.HideShowOptions.TabIndex = 21;
            this.HideShowOptions.Text = "Hide/Show Options";
            this.HideShowOptions.UseVisualStyleBackColor = false;
            this.HideShowOptions.Click += new System.EventHandler(this.HideShowOptions_Click);
            // 
            // ObjectHideShow
            // 
            this.ObjectHideShow.AutoSize = true;
            this.ObjectHideShow.Location = new System.Drawing.Point(9, 450);
            this.ObjectHideShow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ObjectHideShow.Name = "ObjectHideShow";
            this.ObjectHideShow.Size = new System.Drawing.Size(171, 17);
            this.ObjectHideShow.TabIndex = 20;
            this.ObjectHideShow.Text = "Incorporate Object Hide/Show";
            this.ObjectHideShow.UseVisualStyleBackColor = true;
            this.ObjectHideShow.CheckedChanged += new System.EventHandler(this.ObjectHideShow_CheckedChanged);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.Location = new System.Drawing.Point(167, 488);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 28);
            this.Cancel.TabIndex = 23;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Apply
            // 
            this.Apply.BackColor = System.Drawing.Color.SteelBlue;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Apply.Location = new System.Drawing.Point(66, 488);
            this.Apply.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(82, 28);
            this.Apply.TabIndex = 22;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // UseCurrentTime
            // 
            this.UseCurrentTime.AutoSize = true;
            this.UseCurrentTime.Location = new System.Drawing.Point(9, 79);
            this.UseCurrentTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UseCurrentTime.Name = "UseCurrentTime";
            this.UseCurrentTime.Size = new System.Drawing.Size(157, 17);
            this.UseCurrentTime.TabIndex = 30;
            this.UseCurrentTime.Text = "Use Current Animation Time";
            this.UseCurrentTime.UseVisualStyleBackColor = true;
            this.UseCurrentTime.CheckedChanged += new System.EventHandler(this.UseCurrentTime_CheckedChanged);
            // 
            // RefreshTime
            // 
            this.RefreshTime.BackColor = System.Drawing.Color.SteelBlue;
            this.RefreshTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RefreshTime.Location = new System.Drawing.Point(293, 76);
            this.RefreshTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RefreshTime.Name = "RefreshTime";
            this.RefreshTime.Size = new System.Drawing.Size(69, 23);
            this.RefreshTime.TabIndex = 47;
            this.RefreshTime.Text = "Refresh";
            this.RefreshTime.UseVisualStyleBackColor = false;
            this.RefreshTime.Click += new System.EventHandler(this.RefreshTime_Click);
            // 
            // CurrentTime
            // 
            this.CurrentTime.BackColor = System.Drawing.Color.DimGray;
            this.CurrentTime.ForeColor = System.Drawing.Color.White;
            this.CurrentTime.Location = new System.Drawing.Point(161, 79);
            this.CurrentTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CurrentTime.Name = "CurrentTime";
            this.CurrentTime.Size = new System.Drawing.Size(128, 20);
            this.CurrentTime.TabIndex = 48;
            this.CurrentTime.TextChanged += new System.EventHandler(this.CurrentTime_TextChanged);
            // 
            // EditTargetThreatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(372, 530);
            this.Controls.Add(this.RefreshTime);
            this.Controls.Add(this.CurrentTime);
            this.Controls.Add(this.UseCurrentTime);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.HideShowOptions);
            this.Controls.Add(this.ObjectHideShow);
            this.Controls.Add(this.TTDefinitionBox);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "EditTargetThreatForm";
            this.Text = "Edit View";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TTDefinitionBox.ResumeLayout(false);
            this.TTDefinitionBox.PerformLayout();
            this.EllipsoidDefinition.ResumeLayout(false);
            this.EllipsoidDefinition.PerformLayout();
            this.TTDataDisplayOptions.ResumeLayout(false);
            this.TTDataDisplayOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox WindowSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox TTDefinitionBox;
        private System.Windows.Forms.TextBox TTViewName;
        private System.Windows.Forms.ListView ThreatList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.CheckBox UseProxBox;
        private System.Windows.Forms.CheckBox TTUseDataDisplay;
        private System.Windows.Forms.GroupBox TTDataDisplayOptions;
        private System.Windows.Forms.ComboBox TTDisplayObject;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox TTDisplayReport;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox TTDisplayLocation;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button Unselect;
        private System.Windows.Forms.Button Select;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox TargetSatellite;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button HideShowOptions;
        private System.Windows.Forms.CheckBox ObjectHideShow;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Apply;
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
        private System.Windows.Forms.CheckBox EnableEllipsoid;
        private System.Windows.Forms.CheckBox UseCurrentTime;
        private System.Windows.Forms.Button RefreshTime;
        private System.Windows.Forms.TextBox CurrentTime;
    }
}