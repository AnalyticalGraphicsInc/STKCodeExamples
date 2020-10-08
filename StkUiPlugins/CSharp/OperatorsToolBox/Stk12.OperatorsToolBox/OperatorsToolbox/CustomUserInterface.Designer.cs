namespace OperatorsToolbox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomUserInterface));
            this.PluginPanel = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SmartViewDropdown = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Coverage = new System.Windows.Forms.Button();
            this.GroundEvents = new System.Windows.Forms.Button();
            this.SolarPhase = new System.Windows.Forms.Button();
            this.ThreatVolume = new System.Windows.Forms.Button();
            this.PassiveSafety = new System.Windows.Forms.Button();
            this.GroundStationAccess = new System.Windows.Forms.Button();
            this.SmartView = new System.Windows.Forms.Button();
            this.FacilityCreator = new System.Windows.Forms.Button();
            this.SatelliteCreator = new System.Windows.Forms.Button();
            this.Templates = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.Button();
            this.SatelliteImportDropdown = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PluginPanel
            // 
            this.PluginPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PluginPanel.Location = new System.Drawing.Point(63, 1);
            this.PluginPanel.Margin = new System.Windows.Forms.Padding(2);
            this.PluginPanel.Name = "PluginPanel";
            this.PluginPanel.Size = new System.Drawing.Size(291, 607);
            this.PluginPanel.TabIndex = 7;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "edit-tools.png");
            this.imageList1.Images.SetKeyName(1, "radio-antenna.png");
            this.imageList1.Images.SetKeyName(2, "rebound.png");
            this.imageList1.Images.SetKeyName(3, "retinal-scan.png");
            this.imageList1.Images.SetKeyName(4, "rocket.png");
            this.imageList1.Images.SetKeyName(5, "earth.png");
            this.imageList1.Images.SetKeyName(6, "shield-64.png");
            this.imageList1.Images.SetKeyName(7, "timeline.png");
            this.imageList1.Images.SetKeyName(8, "worldwide.png");
            this.imageList1.Images.SetKeyName(9, "layout.png");
            this.imageList1.Images.SetKeyName(10, "setting.png");
            // 
            // SmartViewDropdown
            // 
            this.SmartViewDropdown.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.SmartViewDropdown.Name = "SmartViewDropdown";
            this.SmartViewDropdown.Size = new System.Drawing.Size(61, 4);
            this.SmartViewDropdown.Opening += new System.ComponentModel.CancelEventHandler(this.SmartViewDropdown_Opening);
            this.SmartViewDropdown.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.SmartViewDropDown_ItemClicked);
            // 
            // Coverage
            // 
            this.Coverage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Coverage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Coverage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Coverage.ForeColor = System.Drawing.Color.White;
            this.Coverage.ImageIndex = 8;
            this.Coverage.ImageList = this.imageList1;
            this.Coverage.Location = new System.Drawing.Point(2, 332);
            this.Coverage.Margin = new System.Windows.Forms.Padding(2);
            this.Coverage.MinimumSize = new System.Drawing.Size(39, 42);
            this.Coverage.Name = "Coverage";
            this.Coverage.Size = new System.Drawing.Size(51, 51);
            this.Coverage.TabIndex = 9;
            this.Coverage.UseVisualStyleBackColor = true;
            this.Coverage.Click += new System.EventHandler(this.Coverage_Click_1);
            this.Coverage.MouseHover += new System.EventHandler(this.Coverage_MouseHover);
            // 
            // GroundEvents
            // 
            this.GroundEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroundEvents.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GroundEvents.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GroundEvents.ForeColor = System.Drawing.Color.White;
            this.GroundEvents.ImageIndex = 7;
            this.GroundEvents.ImageList = this.imageList1;
            this.GroundEvents.Location = new System.Drawing.Point(2, 167);
            this.GroundEvents.Margin = new System.Windows.Forms.Padding(2);
            this.GroundEvents.MinimumSize = new System.Drawing.Size(39, 42);
            this.GroundEvents.Name = "GroundEvents";
            this.GroundEvents.Size = new System.Drawing.Size(51, 51);
            this.GroundEvents.TabIndex = 8;
            this.GroundEvents.UseVisualStyleBackColor = true;
            this.GroundEvents.Click += new System.EventHandler(this.GroundEvents_Click);
            this.GroundEvents.MouseHover += new System.EventHandler(this.GroundEvents_MouseHover);
            // 
            // SolarPhase
            // 
            this.SolarPhase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SolarPhase.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SolarPhase.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SolarPhase.ForeColor = System.Drawing.Color.White;
            this.SolarPhase.ImageIndex = 2;
            this.SolarPhase.ImageList = this.imageList1;
            this.SolarPhase.Location = new System.Drawing.Point(2, 497);
            this.SolarPhase.Margin = new System.Windows.Forms.Padding(2);
            this.SolarPhase.MinimumSize = new System.Drawing.Size(39, 42);
            this.SolarPhase.Name = "SolarPhase";
            this.SolarPhase.Size = new System.Drawing.Size(51, 51);
            this.SolarPhase.TabIndex = 6;
            this.SolarPhase.UseVisualStyleBackColor = true;
            this.SolarPhase.Click += new System.EventHandler(this.SolarPhase_Click);
            this.SolarPhase.MouseHover += new System.EventHandler(this.SolarPhase_MouseHover);
            // 
            // ThreatVolume
            // 
            this.ThreatVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ThreatVolume.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ThreatVolume.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ThreatVolume.ForeColor = System.Drawing.Color.White;
            this.ThreatVolume.ImageIndex = 4;
            this.ThreatVolume.ImageList = this.imageList1;
            this.ThreatVolume.Location = new System.Drawing.Point(2, 442);
            this.ThreatVolume.Margin = new System.Windows.Forms.Padding(2);
            this.ThreatVolume.MinimumSize = new System.Drawing.Size(39, 42);
            this.ThreatVolume.Name = "ThreatVolume";
            this.ThreatVolume.Size = new System.Drawing.Size(51, 51);
            this.ThreatVolume.TabIndex = 5;
            this.ThreatVolume.UseVisualStyleBackColor = true;
            this.ThreatVolume.Click += new System.EventHandler(this.ThreatVolume_Click);
            this.ThreatVolume.MouseHover += new System.EventHandler(this.ThreatVolume_MouseHover);
            // 
            // PassiveSafety
            // 
            this.PassiveSafety.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PassiveSafety.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PassiveSafety.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PassiveSafety.ForeColor = System.Drawing.Color.White;
            this.PassiveSafety.ImageIndex = 6;
            this.PassiveSafety.ImageList = this.imageList1;
            this.PassiveSafety.Location = new System.Drawing.Point(2, 387);
            this.PassiveSafety.Margin = new System.Windows.Forms.Padding(2);
            this.PassiveSafety.MinimumSize = new System.Drawing.Size(39, 42);
            this.PassiveSafety.Name = "PassiveSafety";
            this.PassiveSafety.Size = new System.Drawing.Size(51, 51);
            this.PassiveSafety.TabIndex = 4;
            this.PassiveSafety.UseVisualStyleBackColor = true;
            this.PassiveSafety.Click += new System.EventHandler(this.PassiveSafety_Click);
            this.PassiveSafety.MouseHover += new System.EventHandler(this.PassiveSafety_MouseHover);
            // 
            // GroundStationAccess
            // 
            this.GroundStationAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroundStationAccess.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GroundStationAccess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GroundStationAccess.ForeColor = System.Drawing.Color.White;
            this.GroundStationAccess.ImageIndex = 0;
            this.GroundStationAccess.ImageList = this.imageList1;
            this.GroundStationAccess.Location = new System.Drawing.Point(2, 277);
            this.GroundStationAccess.Margin = new System.Windows.Forms.Padding(2);
            this.GroundStationAccess.MinimumSize = new System.Drawing.Size(39, 42);
            this.GroundStationAccess.Name = "GroundStationAccess";
            this.GroundStationAccess.Size = new System.Drawing.Size(51, 51);
            this.GroundStationAccess.TabIndex = 3;
            this.GroundStationAccess.UseVisualStyleBackColor = true;
            this.GroundStationAccess.Click += new System.EventHandler(this.GroundStationAccess_Click);
            this.GroundStationAccess.MouseHover += new System.EventHandler(this.GroundStationAccess_MouseHover);
            // 
            // SmartView
            // 
            this.SmartView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SmartView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SmartView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SmartView.ForeColor = System.Drawing.Color.White;
            this.SmartView.ImageIndex = 3;
            this.SmartView.ImageList = this.imageList1;
            this.SmartView.Location = new System.Drawing.Point(2, 222);
            this.SmartView.Margin = new System.Windows.Forms.Padding(2);
            this.SmartView.MinimumSize = new System.Drawing.Size(39, 42);
            this.SmartView.Name = "SmartView";
            this.SmartView.Size = new System.Drawing.Size(51, 51);
            this.SmartView.TabIndex = 2;
            this.SmartView.UseVisualStyleBackColor = true;
            this.SmartView.Click += new System.EventHandler(this.SmartView_Click);
            this.SmartView.MouseHover += new System.EventHandler(this.SmartView_MouseHover);
            // 
            // FacilityCreator
            // 
            this.FacilityCreator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FacilityCreator.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FacilityCreator.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FacilityCreator.ForeColor = System.Drawing.Color.White;
            this.FacilityCreator.ImageIndex = 1;
            this.FacilityCreator.ImageList = this.imageList1;
            this.FacilityCreator.Location = new System.Drawing.Point(2, 112);
            this.FacilityCreator.Margin = new System.Windows.Forms.Padding(2);
            this.FacilityCreator.MinimumSize = new System.Drawing.Size(39, 42);
            this.FacilityCreator.Name = "FacilityCreator";
            this.FacilityCreator.Size = new System.Drawing.Size(51, 51);
            this.FacilityCreator.TabIndex = 1;
            this.FacilityCreator.UseVisualStyleBackColor = true;
            this.FacilityCreator.Click += new System.EventHandler(this.FacilityCreator_Click);
            this.FacilityCreator.MouseHover += new System.EventHandler(this.FacilityCreator_MouseHover);
            // 
            // SatelliteCreator
            // 
            this.SatelliteCreator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SatelliteCreator.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SatelliteCreator.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SatelliteCreator.ForeColor = System.Drawing.Color.White;
            this.SatelliteCreator.Image = global::OperatorsToolbox.Properties.Resources.Orbit;
            this.SatelliteCreator.Location = new System.Drawing.Point(2, 57);
            this.SatelliteCreator.Margin = new System.Windows.Forms.Padding(2);
            this.SatelliteCreator.MinimumSize = new System.Drawing.Size(39, 42);
            this.SatelliteCreator.Name = "SatelliteCreator";
            this.SatelliteCreator.Size = new System.Drawing.Size(51, 51);
            this.SatelliteCreator.TabIndex = 0;
            this.SatelliteCreator.UseVisualStyleBackColor = true;
            this.SatelliteCreator.Click += new System.EventHandler(this.SatelliteCreator_Click);
            this.SatelliteCreator.MouseHover += new System.EventHandler(this.SatelliteCreator_MouseHover);
            // 
            // Templates
            // 
            this.Templates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Templates.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Templates.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Templates.ForeColor = System.Drawing.Color.White;
            this.Templates.ImageKey = "layout.png";
            this.Templates.ImageList = this.imageList1;
            this.Templates.Location = new System.Drawing.Point(2, 2);
            this.Templates.Margin = new System.Windows.Forms.Padding(2);
            this.Templates.MinimumSize = new System.Drawing.Size(39, 42);
            this.Templates.Name = "Templates";
            this.Templates.Size = new System.Drawing.Size(51, 51);
            this.Templates.TabIndex = 10;
            this.Templates.UseVisualStyleBackColor = true;
            this.Templates.Click += new System.EventHandler(this.Templates_Click);
            this.Templates.MouseHover += new System.EventHandler(this.Templates_MouseHover);
            // 
            // Settings
            // 
            this.Settings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Settings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Settings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Settings.ForeColor = System.Drawing.Color.White;
            this.Settings.ImageIndex = 10;
            this.Settings.ImageList = this.imageList1;
            this.Settings.Location = new System.Drawing.Point(2, 552);
            this.Settings.Margin = new System.Windows.Forms.Padding(2);
            this.Settings.MinimumSize = new System.Drawing.Size(39, 42);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(51, 51);
            this.Settings.TabIndex = 11;
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            this.Settings.MouseHover += new System.EventHandler(this.Settings_MouseHover);
            // 
            // SatelliteImportDropdown
            // 
            this.SatelliteImportDropdown.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.SatelliteImportDropdown.Name = "SatelliteImportDropdown";
            this.SatelliteImportDropdown.Size = new System.Drawing.Size(61, 4);
            this.SatelliteImportDropdown.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.SatelliteImportDropdown_ItemClicked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Controls.Add(this.Templates, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FacilityCreator, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.SatelliteCreator, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Settings, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.Coverage, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.SolarPhase, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.GroundEvents, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ThreatVolume, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.SmartView, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.PassiveSafety, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.GroundStationAccess, 0, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(55, 700);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.PluginPanel);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(354, 610);
            this.Resize += new System.EventHandler(this.CustomUserInterface_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SatelliteCreator;
        private System.Windows.Forms.Button FacilityCreator;
        private System.Windows.Forms.Button SmartView;
        private System.Windows.Forms.Button GroundStationAccess;
        private System.Windows.Forms.Button PassiveSafety;
        private System.Windows.Forms.Button ThreatVolume;
        private System.Windows.Forms.Button SolarPhase;
        private System.Windows.Forms.Panel PluginPanel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip SmartViewDropdown;
        private System.Windows.Forms.Button GroundEvents;
        private System.Windows.Forms.Button Coverage;
        private System.Windows.Forms.Button Templates;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.ContextMenuStrip SatelliteImportDropdown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
