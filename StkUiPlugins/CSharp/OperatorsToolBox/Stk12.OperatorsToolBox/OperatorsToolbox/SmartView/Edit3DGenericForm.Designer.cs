namespace OperatorsToolbox.SmartView
{
    partial class Edit3DGenericForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Edit3DGenericForm));
            this.ObjectHideShow = new System.Windows.Forms.CheckBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.ViewDefinitionBox3D = new System.Windows.Forms.GroupBox();
            this.EnableUniversalOrbitTrack = new System.Windows.Forms.CheckBox();
            this.ViewType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.ViewName3D = new System.Windows.Forms.TextBox();
            this.FocusedItem = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.OrbitTrackBox = new System.Windows.Forms.GroupBox();
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
            this.DisplayObject = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.DisplayReport = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.DisplayLocation = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.WindowSelect = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HideShowOptions = new System.Windows.Forms.Button();
            this.RefreshTime = new System.Windows.Forms.Button();
            this.CurrentTime = new System.Windows.Forms.TextBox();
            this.UseCurrentTime = new System.Windows.Forms.CheckBox();
            this.UseCurrentViewPoint = new System.Windows.Forms.CheckBox();
            this.RefreshViewPoint = new System.Windows.Forms.Button();
            this.ViewDefinitionBox3D.SuspendLayout();
            this.OrbitTrackBox.SuspendLayout();
            this.DataDisplayOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ObjectHideShow
            // 
            this.ObjectHideShow.AutoSize = true;
            this.ObjectHideShow.Location = new System.Drawing.Point(9, 484);
            this.ObjectHideShow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ObjectHideShow.Name = "ObjectHideShow";
            this.ObjectHideShow.Size = new System.Drawing.Size(171, 17);
            this.ObjectHideShow.TabIndex = 21;
            this.ObjectHideShow.Text = "Incorporate Object Hide/Show";
            this.ObjectHideShow.UseVisualStyleBackColor = true;
            this.ObjectHideShow.CheckedChanged += new System.EventHandler(this.ObjectHideShow_CheckedChanged);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.Location = new System.Drawing.Point(167, 522);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 28);
            this.Cancel.TabIndex = 20;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Apply
            // 
            this.Apply.BackColor = System.Drawing.Color.SteelBlue;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Apply.Location = new System.Drawing.Point(66, 522);
            this.Apply.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(82, 28);
            this.Apply.TabIndex = 19;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // ViewDefinitionBox3D
            // 
            this.ViewDefinitionBox3D.Controls.Add(this.EnableUniversalOrbitTrack);
            this.ViewDefinitionBox3D.Controls.Add(this.ViewType);
            this.ViewDefinitionBox3D.Controls.Add(this.label17);
            this.ViewDefinitionBox3D.Controls.Add(this.ViewName3D);
            this.ViewDefinitionBox3D.Controls.Add(this.FocusedItem);
            this.ViewDefinitionBox3D.Controls.Add(this.label16);
            this.ViewDefinitionBox3D.Controls.Add(this.label18);
            this.ViewDefinitionBox3D.Controls.Add(this.OrbitTrackBox);
            this.ViewDefinitionBox3D.Controls.Add(this.UseDataDisplay);
            this.ViewDefinitionBox3D.Controls.Add(this.DataDisplayOptions);
            this.ViewDefinitionBox3D.ForeColor = System.Drawing.Color.White;
            this.ViewDefinitionBox3D.Location = new System.Drawing.Point(9, 129);
            this.ViewDefinitionBox3D.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ViewDefinitionBox3D.Name = "ViewDefinitionBox3D";
            this.ViewDefinitionBox3D.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ViewDefinitionBox3D.Size = new System.Drawing.Size(350, 342);
            this.ViewDefinitionBox3D.TabIndex = 18;
            this.ViewDefinitionBox3D.TabStop = false;
            this.ViewDefinitionBox3D.Text = "3D View Definition";
            // 
            // EnableUniversalOrbitTrack
            // 
            this.EnableUniversalOrbitTrack.AutoSize = true;
            this.EnableUniversalOrbitTrack.Location = new System.Drawing.Point(5, 104);
            this.EnableUniversalOrbitTrack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EnableUniversalOrbitTrack.Name = "EnableUniversalOrbitTrack";
            this.EnableUniversalOrbitTrack.Size = new System.Drawing.Size(167, 17);
            this.EnableUniversalOrbitTrack.TabIndex = 20;
            this.EnableUniversalOrbitTrack.Text = "Enable Universal Orbit Tracks";
            this.EnableUniversalOrbitTrack.UseVisualStyleBackColor = true;
            this.EnableUniversalOrbitTrack.CheckedChanged += new System.EventHandler(this.EnableUniversalOrbitTrack_CheckedChanged);
            // 
            // ViewType
            // 
            this.ViewType.BackColor = System.Drawing.Color.DimGray;
            this.ViewType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ViewType.ForeColor = System.Drawing.Color.White;
            this.ViewType.FormattingEnabled = true;
            this.ViewType.Location = new System.Drawing.Point(85, 80);
            this.ViewType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ViewType.Name = "ViewType";
            this.ViewType.Size = new System.Drawing.Size(115, 21);
            this.ViewType.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 82);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 13);
            this.label17.TabIndex = 18;
            this.label17.Text = "Coord Sys:";
            // 
            // ViewName3D
            // 
            this.ViewName3D.BackColor = System.Drawing.Color.DimGray;
            this.ViewName3D.ForeColor = System.Drawing.Color.White;
            this.ViewName3D.Location = new System.Drawing.Point(70, 24);
            this.ViewName3D.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ViewName3D.Name = "ViewName3D";
            this.ViewName3D.Size = new System.Drawing.Size(130, 20);
            this.ViewName3D.TabIndex = 17;
            // 
            // FocusedItem
            // 
            this.FocusedItem.BackColor = System.Drawing.Color.DimGray;
            this.FocusedItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FocusedItem.ForeColor = System.Drawing.Color.White;
            this.FocusedItem.FormattingEnabled = true;
            this.FocusedItem.Location = new System.Drawing.Point(85, 50);
            this.FocusedItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FocusedItem.Name = "FocusedItem";
            this.FocusedItem.Size = new System.Drawing.Size(115, 21);
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
            this.OrbitTrackBox.Controls.Add(this.label23);
            this.OrbitTrackBox.Controls.Add(this.label22);
            this.OrbitTrackBox.Controls.Add(this.OrbitTrailTime);
            this.OrbitTrackBox.Controls.Add(this.OrbitLeadTime);
            this.OrbitTrackBox.Controls.Add(this.TrailType3D);
            this.OrbitTrackBox.Controls.Add(this.label14);
            this.OrbitTrackBox.Controls.Add(this.LeadType3D);
            this.OrbitTrackBox.Controls.Add(this.label15);
            this.OrbitTrackBox.ForeColor = System.Drawing.Color.White;
            this.OrbitTrackBox.Location = new System.Drawing.Point(6, 120);
            this.OrbitTrackBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OrbitTrackBox.Name = "OrbitTrackBox";
            this.OrbitTrackBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OrbitTrackBox.Size = new System.Drawing.Size(322, 94);
            this.OrbitTrackBox.TabIndex = 18;
            this.OrbitTrackBox.TabStop = false;
            this.OrbitTrackBox.Text = "Orbit Track";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(273, 60);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(24, 13);
            this.label23.TabIndex = 28;
            this.label23.Text = "sec";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(273, 25);
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
            this.OrbitTrailTime.Location = new System.Drawing.Point(204, 58);
            this.OrbitTrailTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OrbitTrailTime.Name = "OrbitTrailTime";
            this.OrbitTrailTime.Size = new System.Drawing.Size(67, 20);
            this.OrbitTrailTime.TabIndex = 27;
            // 
            // OrbitLeadTime
            // 
            this.OrbitLeadTime.BackColor = System.Drawing.Color.DimGray;
            this.OrbitLeadTime.ForeColor = System.Drawing.Color.White;
            this.OrbitLeadTime.Location = new System.Drawing.Point(204, 23);
            this.OrbitLeadTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.TrailType3D.Location = new System.Drawing.Point(79, 58);
            this.TrailType3D.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TrailType3D.Name = "TrailType3D";
            this.TrailType3D.Size = new System.Drawing.Size(115, 21);
            this.TrailType3D.TabIndex = 17;
            this.TrailType3D.SelectedIndexChanged += new System.EventHandler(this.TrailType3D_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 60);
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
            this.LeadType3D.Location = new System.Drawing.Point(79, 23);
            this.LeadType3D.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LeadType3D.Name = "LeadType3D";
            this.LeadType3D.Size = new System.Drawing.Size(115, 21);
            this.LeadType3D.TabIndex = 15;
            this.LeadType3D.SelectedIndexChanged += new System.EventHandler(this.LeadType3D_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 25);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "Lead Type:";
            // 
            // UseDataDisplay
            // 
            this.UseDataDisplay.AutoSize = true;
            this.UseDataDisplay.Location = new System.Drawing.Point(14, 214);
            this.UseDataDisplay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UseDataDisplay.Name = "UseDataDisplay";
            this.UseDataDisplay.Size = new System.Drawing.Size(122, 17);
            this.UseDataDisplay.TabIndex = 16;
            this.UseDataDisplay.Text = "Enable Data Display";
            this.UseDataDisplay.UseVisualStyleBackColor = true;
            this.UseDataDisplay.CheckedChanged += new System.EventHandler(this.UseDataDisplay_CheckedChanged);
            // 
            // DataDisplayOptions
            // 
            this.DataDisplayOptions.Controls.Add(this.DisplayObject);
            this.DataDisplayOptions.Controls.Add(this.label21);
            this.DataDisplayOptions.Controls.Add(this.DisplayReport);
            this.DataDisplayOptions.Controls.Add(this.label20);
            this.DataDisplayOptions.Controls.Add(this.DisplayLocation);
            this.DataDisplayOptions.Controls.Add(this.label19);
            this.DataDisplayOptions.ForeColor = System.Drawing.Color.White;
            this.DataDisplayOptions.Location = new System.Drawing.Point(4, 233);
            this.DataDisplayOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DataDisplayOptions.Name = "DataDisplayOptions";
            this.DataDisplayOptions.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DataDisplayOptions.Size = new System.Drawing.Size(324, 104);
            this.DataDisplayOptions.TabIndex = 0;
            this.DataDisplayOptions.TabStop = false;
            this.DataDisplayOptions.Text = "Data Display Options";
            // 
            // DisplayObject
            // 
            this.DisplayObject.BackColor = System.Drawing.Color.DimGray;
            this.DisplayObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DisplayObject.ForeColor = System.Drawing.Color.White;
            this.DisplayObject.FormattingEnabled = true;
            this.DisplayObject.Location = new System.Drawing.Point(99, 50);
            this.DisplayObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.DisplayReport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.DisplayLocation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.WindowSelect);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(350, 61);
            this.groupBox1.TabIndex = 17;
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
            // HideShowOptions
            // 
            this.HideShowOptions.BackColor = System.Drawing.Color.SteelBlue;
            this.HideShowOptions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.HideShowOptions.Location = new System.Drawing.Point(177, 476);
            this.HideShowOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.HideShowOptions.Name = "HideShowOptions";
            this.HideShowOptions.Size = new System.Drawing.Size(108, 28);
            this.HideShowOptions.TabIndex = 22;
            this.HideShowOptions.Text = "Hide/Show Options";
            this.HideShowOptions.UseVisualStyleBackColor = false;
            this.HideShowOptions.Click += new System.EventHandler(this.HideShowOptions_Click);
            // 
            // RefreshTime
            // 
            this.RefreshTime.BackColor = System.Drawing.Color.SteelBlue;
            this.RefreshTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RefreshTime.Location = new System.Drawing.Point(290, 73);
            this.RefreshTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RefreshTime.Name = "RefreshTime";
            this.RefreshTime.Size = new System.Drawing.Size(69, 23);
            this.RefreshTime.TabIndex = 50;
            this.RefreshTime.Text = "Refresh";
            this.RefreshTime.UseVisualStyleBackColor = false;
            this.RefreshTime.Click += new System.EventHandler(this.RefreshTime_Click);
            // 
            // CurrentTime
            // 
            this.CurrentTime.BackColor = System.Drawing.Color.DimGray;
            this.CurrentTime.ForeColor = System.Drawing.Color.White;
            this.CurrentTime.Location = new System.Drawing.Point(158, 76);
            this.CurrentTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CurrentTime.Name = "CurrentTime";
            this.CurrentTime.Size = new System.Drawing.Size(128, 20);
            this.CurrentTime.TabIndex = 51;
            // 
            // UseCurrentTime
            // 
            this.UseCurrentTime.AutoSize = true;
            this.UseCurrentTime.Location = new System.Drawing.Point(5, 76);
            this.UseCurrentTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UseCurrentTime.Name = "UseCurrentTime";
            this.UseCurrentTime.Size = new System.Drawing.Size(157, 17);
            this.UseCurrentTime.TabIndex = 49;
            this.UseCurrentTime.Text = "Use Current Animation Time";
            this.UseCurrentTime.UseVisualStyleBackColor = true;
            this.UseCurrentTime.CheckedChanged += new System.EventHandler(this.UseCurrentTime_CheckedChanged);
            // 
            // UseCurrentViewPoint
            // 
            this.UseCurrentViewPoint.AutoSize = true;
            this.UseCurrentViewPoint.Location = new System.Drawing.Point(5, 105);
            this.UseCurrentViewPoint.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UseCurrentViewPoint.Name = "UseCurrentViewPoint";
            this.UseCurrentViewPoint.Size = new System.Drawing.Size(131, 17);
            this.UseCurrentViewPoint.TabIndex = 31;
            this.UseCurrentViewPoint.Text = "Use Current Viewpoint";
            this.UseCurrentViewPoint.UseVisualStyleBackColor = true;
            this.UseCurrentViewPoint.CheckedChanged += new System.EventHandler(this.UseCurrentViewPoint_CheckedChanged);
            // 
            // RefreshViewPoint
            // 
            this.RefreshViewPoint.BackColor = System.Drawing.Color.SteelBlue;
            this.RefreshViewPoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RefreshViewPoint.Location = new System.Drawing.Point(135, 102);
            this.RefreshViewPoint.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RefreshViewPoint.Name = "RefreshViewPoint";
            this.RefreshViewPoint.Size = new System.Drawing.Size(69, 23);
            this.RefreshViewPoint.TabIndex = 53;
            this.RefreshViewPoint.Text = "Refresh";
            this.RefreshViewPoint.UseVisualStyleBackColor = false;
            this.RefreshViewPoint.Click += new System.EventHandler(this.RefreshViewPoint_Click);
            // 
            // Edit3DGenericForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(371, 558);
            this.Controls.Add(this.RefreshViewPoint);
            this.Controls.Add(this.UseCurrentViewPoint);
            this.Controls.Add(this.RefreshTime);
            this.Controls.Add(this.CurrentTime);
            this.Controls.Add(this.UseCurrentTime);
            this.Controls.Add(this.HideShowOptions);
            this.Controls.Add(this.ObjectHideShow);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.ViewDefinitionBox3D);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Edit3DGenericForm";
            this.Text = "Edit View";
            this.ViewDefinitionBox3D.ResumeLayout(false);
            this.ViewDefinitionBox3D.PerformLayout();
            this.OrbitTrackBox.ResumeLayout(false);
            this.OrbitTrackBox.PerformLayout();
            this.DataDisplayOptions.ResumeLayout(false);
            this.DataDisplayOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ObjectHideShow;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.GroupBox ViewDefinitionBox3D;
        private System.Windows.Forms.ComboBox ViewType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox ViewName3D;
        private System.Windows.Forms.ComboBox FocusedItem;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox OrbitTrackBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox OrbitTrailTime;
        private System.Windows.Forms.TextBox OrbitLeadTime;
        private System.Windows.Forms.ComboBox TrailType3D;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox LeadType3D;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox UseDataDisplay;
        private System.Windows.Forms.GroupBox DataDisplayOptions;
        private System.Windows.Forms.ComboBox DisplayObject;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox DisplayReport;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox DisplayLocation;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox WindowSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button HideShowOptions;
        private System.Windows.Forms.Button RefreshTime;
        private System.Windows.Forms.TextBox CurrentTime;
        private System.Windows.Forms.CheckBox UseCurrentTime;
        private System.Windows.Forms.CheckBox UseCurrentViewPoint;
        private System.Windows.Forms.Button RefreshViewPoint;
        private System.Windows.Forms.CheckBox EnableUniversalOrbitTrack;
    }
}