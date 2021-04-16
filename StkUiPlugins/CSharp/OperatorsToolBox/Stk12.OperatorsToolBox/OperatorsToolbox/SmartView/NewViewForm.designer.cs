namespace OperatorsToolbox.SmartView
{
    partial class NewViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewViewForm));
            this.TypeSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WindowSelect = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CameraPathName = new System.Windows.Forms.ComboBox();
            this.UseCameraPath = new System.Windows.Forms.CheckBox();
            this.UseCurrentTime = new System.Windows.Forms.CheckBox();
            this.UseCurrentViewPoint = new System.Windows.Forms.CheckBox();
            this.ViewDefinitionBox3D = new System.Windows.Forms.GroupBox();
            this.AdvancedDisplay = new System.Windows.Forms.Button();
            this.EnableUniversalOrbitTrack = new System.Windows.Forms.CheckBox();
            this.ViewName3D = new System.Windows.Forms.TextBox();
            this.FocusedItem = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.OrbitTrackBox = new System.Windows.Forms.GroupBox();
            this.UniqueLeadTrail = new System.Windows.Forms.RadioButton();
            this.UniversalLeadTrail = new System.Windows.Forms.RadioButton();
            this.CustomLeadTrail = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.OrbitTrailTime = new System.Windows.Forms.TextBox();
            this.OrbitLeadTime = new System.Windows.Forms.TextBox();
            this.TrailType3D = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.LeadType3D = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.UseDataDisplay = new System.Windows.Forms.CheckBox();
            this.DataDisplayOptions = new System.Windows.Forms.GroupBox();
            this.PredataObject = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.DisplayObject = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.DisplayReport = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.DisplayLocation = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.VectorHideShow = new System.Windows.Forms.Button();
            this.UseVectorHideShow = new System.Windows.Forms.CheckBox();
            this.HideShowOptions = new System.Windows.Forms.Button();
            this.ObjectHideShow = new System.Windows.Forms.CheckBox();
            this.Create = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.ViewDefinitionBox2D = new System.Windows.Forms.GroupBox();
            this.EnableUniversalGroundTrack = new System.Windows.Forms.CheckBox();
            this.ViewName2D = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.GroundTrackBox = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.TrailType2D = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.GroundTrailTime = new System.Windows.Forms.TextBox();
            this.LeadType2D = new System.Windows.Forms.ComboBox();
            this.GroundLeadTime = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ZoomDelta = new System.Windows.Forms.TextBox();
            this.ZoomCenterLong = new System.Windows.Forms.TextBox();
            this.ZoomCenterLat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ObjectName2D = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TypeSelect2D = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.ViewDefinitionBox3D.SuspendLayout();
            this.OrbitTrackBox.SuspendLayout();
            this.DataDisplayOptions.SuspendLayout();
            this.ViewDefinitionBox2D.SuspendLayout();
            this.GroundTrackBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TypeSelect
            // 
            this.TypeSelect.BackColor = System.Drawing.Color.DimGray;
            this.TypeSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TypeSelect.ForeColor = System.Drawing.Color.White;
            this.TypeSelect.FormattingEnabled = true;
            this.TypeSelect.Location = new System.Drawing.Point(34, 24);
            this.TypeSelect.Margin = new System.Windows.Forms.Padding(2);
            this.TypeSelect.Name = "TypeSelect";
            this.TypeSelect.Size = new System.Drawing.Size(125, 21);
            this.TypeSelect.TabIndex = 0;
            this.TypeSelect.SelectedIndexChanged += new System.EventHandler(this.TypeSelect_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Window:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // WindowSelect
            // 
            this.WindowSelect.BackColor = System.Drawing.Color.DimGray;
            this.WindowSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.WindowSelect.ForeColor = System.Drawing.Color.White;
            this.WindowSelect.FormattingEnabled = true;
            this.WindowSelect.Location = new System.Drawing.Point(217, 24);
            this.WindowSelect.Margin = new System.Windows.Forms.Padding(2);
            this.WindowSelect.Name = "WindowSelect";
            this.WindowSelect.Size = new System.Drawing.Size(128, 21);
            this.WindowSelect.TabIndex = 2;
            this.WindowSelect.SelectedIndexChanged += new System.EventHandler(this.WindowSelect_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CameraPathName);
            this.groupBox1.Controls.Add(this.WindowSelect);
            this.groupBox1.Controls.Add(this.UseCameraPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TypeSelect);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.UseCurrentTime);
            this.groupBox1.Controls.Add(this.UseCurrentViewPoint);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(364, 114);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Window Definition";
            // 
            // CameraPathName
            // 
            this.CameraPathName.BackColor = System.Drawing.Color.DimGray;
            this.CameraPathName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CameraPathName.ForeColor = System.Drawing.Color.White;
            this.CameraPathName.FormattingEnabled = true;
            this.CameraPathName.Location = new System.Drawing.Point(132, 81);
            this.CameraPathName.Margin = new System.Windows.Forms.Padding(2);
            this.CameraPathName.Name = "CameraPathName";
            this.CameraPathName.Size = new System.Drawing.Size(213, 21);
            this.CameraPathName.TabIndex = 34;
            // 
            // UseCameraPath
            // 
            this.UseCameraPath.AutoSize = true;
            this.UseCameraPath.Location = new System.Drawing.Point(8, 83);
            this.UseCameraPath.Margin = new System.Windows.Forms.Padding(2);
            this.UseCameraPath.Name = "UseCameraPath";
            this.UseCameraPath.Size = new System.Drawing.Size(109, 17);
            this.UseCameraPath.TabIndex = 31;
            this.UseCameraPath.Text = "Use Camera Path";
            this.UseCameraPath.UseVisualStyleBackColor = true;
            this.UseCameraPath.CheckedChanged += new System.EventHandler(this.UseCameraPath_CheckedChanged);
            // 
            // UseCurrentTime
            // 
            this.UseCurrentTime.AutoSize = true;
            this.UseCurrentTime.Checked = true;
            this.UseCurrentTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseCurrentTime.Location = new System.Drawing.Point(7, 57);
            this.UseCurrentTime.Margin = new System.Windows.Forms.Padding(2);
            this.UseCurrentTime.Name = "UseCurrentTime";
            this.UseCurrentTime.Size = new System.Drawing.Size(157, 17);
            this.UseCurrentTime.TabIndex = 29;
            this.UseCurrentTime.Text = "Use Current Animation Time";
            this.UseCurrentTime.UseVisualStyleBackColor = true;
            // 
            // UseCurrentViewPoint
            // 
            this.UseCurrentViewPoint.AutoSize = true;
            this.UseCurrentViewPoint.Checked = true;
            this.UseCurrentViewPoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseCurrentViewPoint.Location = new System.Drawing.Point(203, 57);
            this.UseCurrentViewPoint.Margin = new System.Windows.Forms.Padding(2);
            this.UseCurrentViewPoint.Name = "UseCurrentViewPoint";
            this.UseCurrentViewPoint.Size = new System.Drawing.Size(131, 17);
            this.UseCurrentViewPoint.TabIndex = 30;
            this.UseCurrentViewPoint.Text = "Use Current Viewpoint";
            this.UseCurrentViewPoint.UseVisualStyleBackColor = true;
            this.UseCurrentViewPoint.CheckedChanged += new System.EventHandler(this.UseCurrentViewPoint_CheckedChanged);
            // 
            // ViewDefinitionBox3D
            // 
            this.ViewDefinitionBox3D.Controls.Add(this.AdvancedDisplay);
            this.ViewDefinitionBox3D.Controls.Add(this.EnableUniversalOrbitTrack);
            this.ViewDefinitionBox3D.Controls.Add(this.ViewName3D);
            this.ViewDefinitionBox3D.Controls.Add(this.FocusedItem);
            this.ViewDefinitionBox3D.Controls.Add(this.label16);
            this.ViewDefinitionBox3D.Controls.Add(this.label18);
            this.ViewDefinitionBox3D.Controls.Add(this.OrbitTrackBox);
            this.ViewDefinitionBox3D.Controls.Add(this.UseDataDisplay);
            this.ViewDefinitionBox3D.Controls.Add(this.DataDisplayOptions);
            this.ViewDefinitionBox3D.ForeColor = System.Drawing.Color.White;
            this.ViewDefinitionBox3D.Location = new System.Drawing.Point(4, 124);
            this.ViewDefinitionBox3D.Margin = new System.Windows.Forms.Padding(2);
            this.ViewDefinitionBox3D.Name = "ViewDefinitionBox3D";
            this.ViewDefinitionBox3D.Padding = new System.Windows.Forms.Padding(2);
            this.ViewDefinitionBox3D.Size = new System.Drawing.Size(364, 381);
            this.ViewDefinitionBox3D.TabIndex = 5;
            this.ViewDefinitionBox3D.TabStop = false;
            this.ViewDefinitionBox3D.Text = "3D View Definition";
            // 
            // AdvancedDisplay
            // 
            this.AdvancedDisplay.BackColor = System.Drawing.Color.SteelBlue;
            this.AdvancedDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AdvancedDisplay.Location = new System.Drawing.Point(274, 24);
            this.AdvancedDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.AdvancedDisplay.Name = "AdvancedDisplay";
            this.AdvancedDisplay.Size = new System.Drawing.Size(86, 42);
            this.AdvancedDisplay.TabIndex = 31;
            this.AdvancedDisplay.Text = "Advanced Displays";
            this.AdvancedDisplay.UseVisualStyleBackColor = false;
            this.AdvancedDisplay.Click += new System.EventHandler(this.AdvancedDisplay_Click);
            // 
            // EnableUniversalOrbitTrack
            // 
            this.EnableUniversalOrbitTrack.AutoSize = true;
            this.EnableUniversalOrbitTrack.Location = new System.Drawing.Point(8, 83);
            this.EnableUniversalOrbitTrack.Margin = new System.Windows.Forms.Padding(2);
            this.EnableUniversalOrbitTrack.Name = "EnableUniversalOrbitTrack";
            this.EnableUniversalOrbitTrack.Size = new System.Drawing.Size(135, 17);
            this.EnableUniversalOrbitTrack.TabIndex = 20;
            this.EnableUniversalOrbitTrack.Text = "Enable Track Graphics";
            this.EnableUniversalOrbitTrack.UseVisualStyleBackColor = true;
            this.EnableUniversalOrbitTrack.CheckedChanged += new System.EventHandler(this.EnableUniversalOrbitTrack_CheckedChanged);
            // 
            // ViewName3D
            // 
            this.ViewName3D.BackColor = System.Drawing.Color.DimGray;
            this.ViewName3D.ForeColor = System.Drawing.Color.White;
            this.ViewName3D.Location = new System.Drawing.Point(70, 24);
            this.ViewName3D.Margin = new System.Windows.Forms.Padding(2);
            this.ViewName3D.Name = "ViewName3D";
            this.ViewName3D.Size = new System.Drawing.Size(200, 20);
            this.ViewName3D.TabIndex = 17;
            // 
            // FocusedItem
            // 
            this.FocusedItem.BackColor = System.Drawing.Color.DimGray;
            this.FocusedItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FocusedItem.ForeColor = System.Drawing.Color.White;
            this.FocusedItem.FormattingEnabled = true;
            this.FocusedItem.Location = new System.Drawing.Point(85, 50);
            this.FocusedItem.Margin = new System.Windows.Forms.Padding(2);
            this.FocusedItem.Name = "FocusedItem";
            this.FocusedItem.Size = new System.Drawing.Size(185, 21);
            this.FocusedItem.TabIndex = 17;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 24);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 16;
            this.label16.Text = "View Name:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 53);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 13);
            this.label18.TabIndex = 16;
            this.label18.Text = "Focused Item:";
            // 
            // OrbitTrackBox
            // 
            this.OrbitTrackBox.Controls.Add(this.UniqueLeadTrail);
            this.OrbitTrackBox.Controls.Add(this.UniversalLeadTrail);
            this.OrbitTrackBox.Controls.Add(this.CustomLeadTrail);
            this.OrbitTrackBox.Controls.Add(this.label23);
            this.OrbitTrackBox.Controls.Add(this.label22);
            this.OrbitTrackBox.Controls.Add(this.OrbitTrailTime);
            this.OrbitTrackBox.Controls.Add(this.OrbitLeadTime);
            this.OrbitTrackBox.Controls.Add(this.TrailType3D);
            this.OrbitTrackBox.Controls.Add(this.label14);
            this.OrbitTrackBox.Controls.Add(this.LeadType3D);
            this.OrbitTrackBox.Controls.Add(this.label15);
            this.OrbitTrackBox.ForeColor = System.Drawing.Color.White;
            this.OrbitTrackBox.Location = new System.Drawing.Point(6, 99);
            this.OrbitTrackBox.Margin = new System.Windows.Forms.Padding(2);
            this.OrbitTrackBox.Name = "OrbitTrackBox";
            this.OrbitTrackBox.Padding = new System.Windows.Forms.Padding(2);
            this.OrbitTrackBox.Size = new System.Drawing.Size(354, 117);
            this.OrbitTrackBox.TabIndex = 18;
            this.OrbitTrackBox.TabStop = false;
            this.OrbitTrackBox.Text = "Track Data";
            // 
            // UniqueLeadTrail
            // 
            this.UniqueLeadTrail.AutoSize = true;
            this.UniqueLeadTrail.Location = new System.Drawing.Point(97, 18);
            this.UniqueLeadTrail.Name = "UniqueLeadTrail";
            this.UniqueLeadTrail.Size = new System.Drawing.Size(60, 17);
            this.UniqueLeadTrail.TabIndex = 33;
            this.UniqueLeadTrail.Text = "Custom";
            this.UniqueLeadTrail.UseVisualStyleBackColor = true;
            this.UniqueLeadTrail.CheckedChanged += new System.EventHandler(this.UniqueLeadTrail_CheckedChanged);
            // 
            // UniversalLeadTrail
            // 
            this.UniversalLeadTrail.AutoSize = true;
            this.UniversalLeadTrail.Checked = true;
            this.UniversalLeadTrail.Location = new System.Drawing.Point(9, 18);
            this.UniversalLeadTrail.Name = "UniversalLeadTrail";
            this.UniversalLeadTrail.Size = new System.Drawing.Size(69, 17);
            this.UniversalLeadTrail.TabIndex = 32;
            this.UniversalLeadTrail.TabStop = true;
            this.UniversalLeadTrail.Text = "Universal";
            this.UniversalLeadTrail.UseVisualStyleBackColor = true;
            this.UniversalLeadTrail.CheckedChanged += new System.EventHandler(this.UniversalLeadTrail_CheckedChanged);
            // 
            // CustomLeadTrail
            // 
            this.CustomLeadTrail.BackColor = System.Drawing.Color.SteelBlue;
            this.CustomLeadTrail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CustomLeadTrail.Location = new System.Drawing.Point(270, 56);
            this.CustomLeadTrail.Margin = new System.Windows.Forms.Padding(2);
            this.CustomLeadTrail.Name = "CustomLeadTrail";
            this.CustomLeadTrail.Size = new System.Drawing.Size(71, 35);
            this.CustomLeadTrail.TabIndex = 32;
            this.CustomLeadTrail.Text = "Custom";
            this.CustomLeadTrail.UseVisualStyleBackColor = false;
            this.CustomLeadTrail.Click += new System.EventHandler(this.CustomLeadTrail_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(238, 85);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(24, 13);
            this.label23.TabIndex = 28;
            this.label23.Text = "sec";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(238, 50);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(24, 13);
            this.label22.TabIndex = 20;
            this.label22.Text = "sec";
            // 
            // OrbitTrailTime
            // 
            this.OrbitTrailTime.BackColor = System.Drawing.Color.DimGray;
            this.OrbitTrailTime.ForeColor = System.Drawing.Color.White;
            this.OrbitTrailTime.Location = new System.Drawing.Point(169, 83);
            this.OrbitTrailTime.Margin = new System.Windows.Forms.Padding(2);
            this.OrbitTrailTime.Name = "OrbitTrailTime";
            this.OrbitTrailTime.Size = new System.Drawing.Size(67, 20);
            this.OrbitTrailTime.TabIndex = 27;
            // 
            // OrbitLeadTime
            // 
            this.OrbitLeadTime.BackColor = System.Drawing.Color.DimGray;
            this.OrbitLeadTime.ForeColor = System.Drawing.Color.White;
            this.OrbitLeadTime.Location = new System.Drawing.Point(169, 48);
            this.OrbitLeadTime.Margin = new System.Windows.Forms.Padding(2);
            this.OrbitLeadTime.Name = "OrbitLeadTime";
            this.OrbitLeadTime.Size = new System.Drawing.Size(67, 20);
            this.OrbitLeadTime.TabIndex = 26;
            // 
            // TrailType3D
            // 
            this.TrailType3D.BackColor = System.Drawing.Color.DimGray;
            this.TrailType3D.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TrailType3D.ForeColor = System.Drawing.Color.White;
            this.TrailType3D.FormattingEnabled = true;
            this.TrailType3D.Location = new System.Drawing.Point(72, 83);
            this.TrailType3D.Margin = new System.Windows.Forms.Padding(2);
            this.TrailType3D.Name = "TrailType3D";
            this.TrailType3D.Size = new System.Drawing.Size(93, 21);
            this.TrailType3D.TabIndex = 17;
            this.TrailType3D.SelectedIndexChanged += new System.EventHandler(this.TrailType3D_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 85);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "Trail Type:";
            // 
            // LeadType3D
            // 
            this.LeadType3D.BackColor = System.Drawing.Color.DimGray;
            this.LeadType3D.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LeadType3D.ForeColor = System.Drawing.Color.White;
            this.LeadType3D.FormattingEnabled = true;
            this.LeadType3D.Location = new System.Drawing.Point(72, 48);
            this.LeadType3D.Margin = new System.Windows.Forms.Padding(2);
            this.LeadType3D.Name = "LeadType3D";
            this.LeadType3D.Size = new System.Drawing.Size(93, 21);
            this.LeadType3D.TabIndex = 15;
            this.LeadType3D.SelectedIndexChanged += new System.EventHandler(this.LeadType3D_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 50);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "Lead Type:";
            // 
            // UseDataDisplay
            // 
            this.UseDataDisplay.AutoSize = true;
            this.UseDataDisplay.Location = new System.Drawing.Point(7, 220);
            this.UseDataDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.UseDataDisplay.Name = "UseDataDisplay";
            this.UseDataDisplay.Size = new System.Drawing.Size(122, 17);
            this.UseDataDisplay.TabIndex = 16;
            this.UseDataDisplay.Text = "Enable Data Display";
            this.UseDataDisplay.UseVisualStyleBackColor = true;
            this.UseDataDisplay.CheckedChanged += new System.EventHandler(this.UseDataDisplay_CheckedChanged);
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
            this.DataDisplayOptions.Location = new System.Drawing.Point(6, 241);
            this.DataDisplayOptions.Margin = new System.Windows.Forms.Padding(2);
            this.DataDisplayOptions.Name = "DataDisplayOptions";
            this.DataDisplayOptions.Padding = new System.Windows.Forms.Padding(2);
            this.DataDisplayOptions.Size = new System.Drawing.Size(354, 140);
            this.DataDisplayOptions.TabIndex = 0;
            this.DataDisplayOptions.TabStop = false;
            this.DataDisplayOptions.Text = "Data Display Options";
            // 
            // PredataObject
            // 
            this.PredataObject.BackColor = System.Drawing.Color.DimGray;
            this.PredataObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PredataObject.ForeColor = System.Drawing.Color.White;
            this.PredataObject.FormattingEnabled = true;
            this.PredataObject.Location = new System.Drawing.Point(99, 112);
            this.PredataObject.Margin = new System.Windows.Forms.Padding(2);
            this.PredataObject.Name = "PredataObject";
            this.PredataObject.Size = new System.Drawing.Size(138, 21);
            this.PredataObject.TabIndex = 27;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 114);
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
            this.label21.Location = new System.Drawing.Point(8, 53);
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
            this.label20.Location = new System.Drawing.Point(8, 82);
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
            this.label19.Location = new System.Drawing.Point(8, 24);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 13);
            this.label19.TabIndex = 20;
            this.label19.Text = "Display Location:";
            // 
            // VectorHideShow
            // 
            this.VectorHideShow.BackColor = System.Drawing.Color.SteelBlue;
            this.VectorHideShow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.VectorHideShow.Location = new System.Drawing.Point(184, 52);
            this.VectorHideShow.Margin = new System.Windows.Forms.Padding(2);
            this.VectorHideShow.Name = "VectorHideShow";
            this.VectorHideShow.Size = new System.Drawing.Size(115, 30);
            this.VectorHideShow.TabIndex = 21;
            this.VectorHideShow.Text = "Hide/Show Options";
            this.VectorHideShow.UseVisualStyleBackColor = false;
            this.VectorHideShow.Click += new System.EventHandler(this.VectorHideShow_Click);
            // 
            // UseVectorHideShow
            // 
            this.UseVectorHideShow.AutoSize = true;
            this.UseVectorHideShow.Location = new System.Drawing.Point(14, 58);
            this.UseVectorHideShow.Margin = new System.Windows.Forms.Padding(2);
            this.UseVectorHideShow.Name = "UseVectorHideShow";
            this.UseVectorHideShow.Size = new System.Drawing.Size(171, 17);
            this.UseVectorHideShow.TabIndex = 20;
            this.UseVectorHideShow.Text = "Incorporate Vector Hide/Show";
            this.UseVectorHideShow.UseVisualStyleBackColor = true;
            this.UseVectorHideShow.CheckedChanged += new System.EventHandler(this.UseVectorHideShow_CheckedChanged);
            // 
            // HideShowOptions
            // 
            this.HideShowOptions.BackColor = System.Drawing.Color.SteelBlue;
            this.HideShowOptions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.HideShowOptions.Location = new System.Drawing.Point(184, 16);
            this.HideShowOptions.Margin = new System.Windows.Forms.Padding(2);
            this.HideShowOptions.Name = "HideShowOptions";
            this.HideShowOptions.Size = new System.Drawing.Size(115, 30);
            this.HideShowOptions.TabIndex = 17;
            this.HideShowOptions.Text = "Hide/Show Options";
            this.HideShowOptions.UseVisualStyleBackColor = false;
            this.HideShowOptions.Click += new System.EventHandler(this.HideShowOptions_Click);
            // 
            // ObjectHideShow
            // 
            this.ObjectHideShow.AutoSize = true;
            this.ObjectHideShow.Location = new System.Drawing.Point(14, 22);
            this.ObjectHideShow.Margin = new System.Windows.Forms.Padding(2);
            this.ObjectHideShow.Name = "ObjectHideShow";
            this.ObjectHideShow.Size = new System.Drawing.Size(171, 17);
            this.ObjectHideShow.TabIndex = 16;
            this.ObjectHideShow.Text = "Incorporate Object Hide/Show";
            this.ObjectHideShow.UseVisualStyleBackColor = true;
            this.ObjectHideShow.CheckedChanged += new System.EventHandler(this.ObjectHideShow_CheckedChanged);
            // 
            // Create
            // 
            this.Create.BackColor = System.Drawing.Color.SteelBlue;
            this.Create.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Create.Location = new System.Drawing.Point(78, 606);
            this.Create.Margin = new System.Windows.Forms.Padding(2);
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(82, 28);
            this.Create.TabIndex = 6;
            this.Create.Text = "Create";
            this.Create.UseVisualStyleBackColor = false;
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.Location = new System.Drawing.Point(179, 606);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 28);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ViewDefinitionBox2D
            // 
            this.ViewDefinitionBox2D.Controls.Add(this.EnableUniversalGroundTrack);
            this.ViewDefinitionBox2D.Controls.Add(this.ViewName2D);
            this.ViewDefinitionBox2D.Controls.Add(this.label13);
            this.ViewDefinitionBox2D.Controls.Add(this.GroundTrackBox);
            this.ViewDefinitionBox2D.Controls.Add(this.label10);
            this.ViewDefinitionBox2D.Controls.Add(this.label9);
            this.ViewDefinitionBox2D.Controls.Add(this.label8);
            this.ViewDefinitionBox2D.Controls.Add(this.ZoomDelta);
            this.ViewDefinitionBox2D.Controls.Add(this.ZoomCenterLong);
            this.ViewDefinitionBox2D.Controls.Add(this.ZoomCenterLat);
            this.ViewDefinitionBox2D.Controls.Add(this.label7);
            this.ViewDefinitionBox2D.Controls.Add(this.label6);
            this.ViewDefinitionBox2D.Controls.Add(this.label5);
            this.ViewDefinitionBox2D.Controls.Add(this.ObjectName2D);
            this.ViewDefinitionBox2D.Controls.Add(this.label4);
            this.ViewDefinitionBox2D.Controls.Add(this.TypeSelect2D);
            this.ViewDefinitionBox2D.Controls.Add(this.label3);
            this.ViewDefinitionBox2D.ForeColor = System.Drawing.Color.White;
            this.ViewDefinitionBox2D.Location = new System.Drawing.Point(4, 124);
            this.ViewDefinitionBox2D.Margin = new System.Windows.Forms.Padding(2);
            this.ViewDefinitionBox2D.Name = "ViewDefinitionBox2D";
            this.ViewDefinitionBox2D.Padding = new System.Windows.Forms.Padding(2);
            this.ViewDefinitionBox2D.Size = new System.Drawing.Size(364, 381);
            this.ViewDefinitionBox2D.TabIndex = 6;
            this.ViewDefinitionBox2D.TabStop = false;
            this.ViewDefinitionBox2D.Text = "2D View Definition";
            // 
            // EnableUniversalGroundTrack
            // 
            this.EnableUniversalGroundTrack.AutoSize = true;
            this.EnableUniversalGroundTrack.Location = new System.Drawing.Point(4, 174);
            this.EnableUniversalGroundTrack.Margin = new System.Windows.Forms.Padding(2);
            this.EnableUniversalGroundTrack.Name = "EnableUniversalGroundTrack";
            this.EnableUniversalGroundTrack.Size = new System.Drawing.Size(175, 17);
            this.EnableUniversalGroundTrack.TabIndex = 21;
            this.EnableUniversalGroundTrack.Text = "Enable Universal Ground Track";
            this.EnableUniversalGroundTrack.UseVisualStyleBackColor = true;
            this.EnableUniversalGroundTrack.CheckedChanged += new System.EventHandler(this.EnableUniversalGroundTrack_CheckedChanged);
            // 
            // ViewName2D
            // 
            this.ViewName2D.BackColor = System.Drawing.Color.DimGray;
            this.ViewName2D.ForeColor = System.Drawing.Color.White;
            this.ViewName2D.Location = new System.Drawing.Point(74, 24);
            this.ViewName2D.Margin = new System.Windows.Forms.Padding(2);
            this.ViewName2D.Name = "ViewName2D";
            this.ViewName2D.Size = new System.Drawing.Size(130, 20);
            this.ViewName2D.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 24);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "View Name:";
            // 
            // GroundTrackBox
            // 
            this.GroundTrackBox.Controls.Add(this.label24);
            this.GroundTrackBox.Controls.Add(this.TrailType2D);
            this.GroundTrackBox.Controls.Add(this.label25);
            this.GroundTrackBox.Controls.Add(this.label12);
            this.GroundTrackBox.Controls.Add(this.GroundTrailTime);
            this.GroundTrackBox.Controls.Add(this.LeadType2D);
            this.GroundTrackBox.Controls.Add(this.GroundLeadTime);
            this.GroundTrackBox.Controls.Add(this.label11);
            this.GroundTrackBox.ForeColor = System.Drawing.Color.White;
            this.GroundTrackBox.Location = new System.Drawing.Point(6, 198);
            this.GroundTrackBox.Margin = new System.Windows.Forms.Padding(2);
            this.GroundTrackBox.Name = "GroundTrackBox";
            this.GroundTrackBox.Padding = new System.Windows.Forms.Padding(2);
            this.GroundTrackBox.Size = new System.Drawing.Size(350, 94);
            this.GroundTrackBox.TabIndex = 13;
            this.GroundTrackBox.TabStop = false;
            this.GroundTrackBox.Text = "Ground Track";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(266, 60);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(24, 13);
            this.label24.TabIndex = 32;
            this.label24.Text = "sec";
            // 
            // TrailType2D
            // 
            this.TrailType2D.BackColor = System.Drawing.Color.DimGray;
            this.TrailType2D.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TrailType2D.ForeColor = System.Drawing.Color.White;
            this.TrailType2D.FormattingEnabled = true;
            this.TrailType2D.Location = new System.Drawing.Point(79, 58);
            this.TrailType2D.Margin = new System.Windows.Forms.Padding(2);
            this.TrailType2D.Name = "TrailType2D";
            this.TrailType2D.Size = new System.Drawing.Size(115, 21);
            this.TrailType2D.TabIndex = 17;
            this.TrailType2D.SelectedIndexChanged += new System.EventHandler(this.TrailType2D_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(266, 25);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(24, 13);
            this.label25.TabIndex = 29;
            this.label25.Text = "sec";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 60);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Trail Type:";
            // 
            // GroundTrailTime
            // 
            this.GroundTrailTime.BackColor = System.Drawing.Color.DimGray;
            this.GroundTrailTime.ForeColor = System.Drawing.Color.White;
            this.GroundTrailTime.Location = new System.Drawing.Point(197, 58);
            this.GroundTrailTime.Margin = new System.Windows.Forms.Padding(2);
            this.GroundTrailTime.Name = "GroundTrailTime";
            this.GroundTrailTime.Size = new System.Drawing.Size(67, 20);
            this.GroundTrailTime.TabIndex = 31;
            // 
            // LeadType2D
            // 
            this.LeadType2D.BackColor = System.Drawing.Color.DimGray;
            this.LeadType2D.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LeadType2D.ForeColor = System.Drawing.Color.White;
            this.LeadType2D.FormattingEnabled = true;
            this.LeadType2D.Location = new System.Drawing.Point(79, 23);
            this.LeadType2D.Margin = new System.Windows.Forms.Padding(2);
            this.LeadType2D.Name = "LeadType2D";
            this.LeadType2D.Size = new System.Drawing.Size(115, 21);
            this.LeadType2D.TabIndex = 15;
            this.LeadType2D.SelectedIndexChanged += new System.EventHandler(this.LeadType2D_SelectedIndexChanged);
            // 
            // GroundLeadTime
            // 
            this.GroundLeadTime.BackColor = System.Drawing.Color.DimGray;
            this.GroundLeadTime.ForeColor = System.Drawing.Color.White;
            this.GroundLeadTime.Location = new System.Drawing.Point(197, 23);
            this.GroundLeadTime.Margin = new System.Windows.Forms.Padding(2);
            this.GroundLeadTime.Name = "GroundLeadTime";
            this.GroundLeadTime.Size = new System.Drawing.Size(67, 20);
            this.GroundLeadTime.TabIndex = 30;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 25);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Lead Type:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(205, 154);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "deg";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(205, 123);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "deg";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(204, 91);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "deg";
            // 
            // ZoomDelta
            // 
            this.ZoomDelta.BackColor = System.Drawing.Color.DimGray;
            this.ZoomDelta.ForeColor = System.Drawing.Color.White;
            this.ZoomDelta.Location = new System.Drawing.Point(132, 150);
            this.ZoomDelta.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomDelta.Name = "ZoomDelta";
            this.ZoomDelta.Size = new System.Drawing.Size(68, 20);
            this.ZoomDelta.TabIndex = 9;
            // 
            // ZoomCenterLong
            // 
            this.ZoomCenterLong.BackColor = System.Drawing.Color.DimGray;
            this.ZoomCenterLong.ForeColor = System.Drawing.Color.White;
            this.ZoomCenterLong.Location = new System.Drawing.Point(132, 120);
            this.ZoomCenterLong.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomCenterLong.Name = "ZoomCenterLong";
            this.ZoomCenterLong.Size = new System.Drawing.Size(68, 20);
            this.ZoomCenterLong.TabIndex = 8;
            // 
            // ZoomCenterLat
            // 
            this.ZoomCenterLat.BackColor = System.Drawing.Color.DimGray;
            this.ZoomCenterLat.ForeColor = System.Drawing.Color.White;
            this.ZoomCenterLat.Location = new System.Drawing.Point(132, 91);
            this.ZoomCenterLat.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomCenterLat.Name = "ZoomCenterLat";
            this.ZoomCenterLat.Size = new System.Drawing.Size(68, 20);
            this.ZoomCenterLat.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 150);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Zoom Delta Latitude:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 120);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Zoom Center Longitude:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 91);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Zoom Center Latitude:";
            // 
            // ObjectName2D
            // 
            this.ObjectName2D.BackColor = System.Drawing.Color.DimGray;
            this.ObjectName2D.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ObjectName2D.ForeColor = System.Drawing.Color.White;
            this.ObjectName2D.FormattingEnabled = true;
            this.ObjectName2D.Location = new System.Drawing.Point(220, 54);
            this.ObjectName2D.Margin = new System.Windows.Forms.Padding(2);
            this.ObjectName2D.Name = "ObjectName2D";
            this.ObjectName2D.Size = new System.Drawing.Size(126, 21);
            this.ObjectName2D.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 56);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Object:";
            // 
            // TypeSelect2D
            // 
            this.TypeSelect2D.BackColor = System.Drawing.Color.DimGray;
            this.TypeSelect2D.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TypeSelect2D.ForeColor = System.Drawing.Color.White;
            this.TypeSelect2D.FormattingEnabled = true;
            this.TypeSelect2D.Location = new System.Drawing.Point(44, 54);
            this.TypeSelect2D.Margin = new System.Windows.Forms.Padding(2);
            this.TypeSelect2D.Name = "TypeSelect2D";
            this.TypeSelect2D.Size = new System.Drawing.Size(115, 21);
            this.TypeSelect2D.TabIndex = 1;
            this.TypeSelect2D.SelectedIndexChanged += new System.EventHandler(this.TypeSelect2D_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Type:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.HideShowOptions);
            this.groupBox2.Controls.Add(this.VectorHideShow);
            this.groupBox2.Controls.Add(this.ObjectHideShow);
            this.groupBox2.Controls.Add(this.UseVectorHideShow);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(4, 510);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 91);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hide/Show";
            // 
            // NewViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(376, 644);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ViewDefinitionBox2D);
            this.Controls.Add(this.ViewDefinitionBox3D);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Create);
            this.Controls.Add(this.Cancel);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "NewViewForm";
            this.Text = "New View";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ViewDefinitionBox3D.ResumeLayout(false);
            this.ViewDefinitionBox3D.PerformLayout();
            this.OrbitTrackBox.ResumeLayout(false);
            this.OrbitTrackBox.PerformLayout();
            this.DataDisplayOptions.ResumeLayout(false);
            this.DataDisplayOptions.PerformLayout();
            this.ViewDefinitionBox2D.ResumeLayout(false);
            this.ViewDefinitionBox2D.PerformLayout();
            this.GroundTrackBox.ResumeLayout(false);
            this.GroundTrackBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox TypeSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox WindowSelect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox ViewDefinitionBox3D;
        private System.Windows.Forms.Button Create;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.GroupBox ViewDefinitionBox2D;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ZoomDelta;
        private System.Windows.Forms.TextBox ZoomCenterLong;
        private System.Windows.Forms.TextBox ZoomCenterLat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ObjectName2D;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox TypeSelect2D;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox GroundTrackBox;
        private System.Windows.Forms.ComboBox TrailType2D;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox LeadType2D;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox ViewName2D;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox UseDataDisplay;
        private System.Windows.Forms.GroupBox DataDisplayOptions;
        private System.Windows.Forms.GroupBox OrbitTrackBox;
        private System.Windows.Forms.ComboBox TrailType3D;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox LeadType3D;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox ViewName3D;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox FocusedItem;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox DisplayObject;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox DisplayReport;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox DisplayLocation;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox ObjectHideShow;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox OrbitTrailTime;
        private System.Windows.Forms.TextBox OrbitLeadTime;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox GroundTrailTime;
        private System.Windows.Forms.TextBox GroundLeadTime;
        private System.Windows.Forms.Button HideShowOptions;
        private System.Windows.Forms.CheckBox UseCurrentTime;
        private System.Windows.Forms.CheckBox UseCurrentViewPoint;
        private System.Windows.Forms.CheckBox EnableUniversalOrbitTrack;
        private System.Windows.Forms.CheckBox EnableUniversalGroundTrack;
        private System.Windows.Forms.ComboBox CameraPathName;
        private System.Windows.Forms.CheckBox UseCameraPath;
        private System.Windows.Forms.Button VectorHideShow;
        private System.Windows.Forms.CheckBox UseVectorHideShow;
        private System.Windows.Forms.Button AdvancedDisplay;
        private System.Windows.Forms.RadioButton UniqueLeadTrail;
        private System.Windows.Forms.RadioButton UniversalLeadTrail;
        private System.Windows.Forms.Button CustomLeadTrail;
        private System.Windows.Forms.ComboBox PredataObject;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}