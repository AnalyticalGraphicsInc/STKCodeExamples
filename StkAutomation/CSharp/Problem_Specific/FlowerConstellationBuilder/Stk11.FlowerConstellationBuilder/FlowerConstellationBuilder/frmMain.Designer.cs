namespace FlowerConstellationBuilder
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.nudDays = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudPetals = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudSatellites = new System.Windows.Forms.NumericUpDown();
            this.btnCreateConstellation = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nudFn = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudFd = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudPerigeeAlt = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudInclination = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.tbConstellationName = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pnlCurrentColor = new System.Windows.Forms.Panel();
            this.btnColor = new System.Windows.Forms.Button();
            this.cbConstellationObj = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudFh = new System.Windows.Forms.NumericUpDown();
            this.gbCommonParameters = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.nudArgOfPerigee = new System.Windows.Forms.NumericUpDown();
            this.bgPhasing = new System.Windows.Forms.GroupBox();
            this.gbConstellationParameters = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.nudInitialMeanAnomaly = new System.Windows.Forms.NumericUpDown();
            this.nudInitialRAAN = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbFixedFrame = new System.Windows.Forms.CheckBox();
            this.cbInertialFrame = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.examplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loneStarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondaryClosedPathsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbDeriverParameter = new System.Windows.Forms.GroupBox();
            this.tbEcc = new System.Windows.Forms.TextBox();
            this.tbApogeeHeight = new System.Windows.Forms.TextBox();
            this.tbSma = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.molnyiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPetals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSatellites)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPerigeeAlt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInclination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFh)).BeginInit();
            this.gbCommonParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArgOfPerigee)).BeginInit();
            this.bgPhasing.SuspendLayout();
            this.gbConstellationParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInitialMeanAnomaly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInitialRAAN)).BeginInit();
            this.gbOptions.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gbDeriverParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudDays
            // 
            this.nudDays.Location = new System.Drawing.Point(176, 75);
            this.nudDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDays.Name = "nudDays";
            this.nudDays.Size = new System.Drawing.Size(63, 20);
            this.nudDays.TabIndex = 3;
            this.toolTip1.SetToolTip(this.nudDays, "Number of days to repeat ground track");
            this.nudDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDays.ValueChanged += new System.EventHandler(this.nudDays_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of Days to Repeat";
            // 
            // nudPetals
            // 
            this.nudPetals.Location = new System.Drawing.Point(176, 101);
            this.nudPetals.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPetals.Name = "nudPetals";
            this.nudPetals.Size = new System.Drawing.Size(63, 20);
            this.nudPetals.TabIndex = 4;
            this.toolTip1.SetToolTip(this.nudPetals, "Number of revolutions to repeat ground track");
            this.nudPetals.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudPetals.ValueChanged += new System.EventHandler(this.nudPetals_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of Petals";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Number of Satellites";
            // 
            // nudSatellites
            // 
            this.nudSatellites.Location = new System.Drawing.Point(176, 49);
            this.nudSatellites.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSatellites.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSatellites.Name = "nudSatellites";
            this.nudSatellites.Size = new System.Drawing.Size(63, 20);
            this.nudSatellites.TabIndex = 2;
            this.toolTip1.SetToolTip(this.nudSatellites, "Number of aatellites");
            this.nudSatellites.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudSatellites.ValueChanged += new System.EventHandler(this.nudSatellites_ValueChanged);
            // 
            // btnCreateConstellation
            // 
            this.btnCreateConstellation.Location = new System.Drawing.Point(274, 400);
            this.btnCreateConstellation.Name = "btnCreateConstellation";
            this.btnCreateConstellation.Size = new System.Drawing.Size(173, 48);
            this.btnCreateConstellation.TabIndex = 6;
            this.btnCreateConstellation.Text = "Create Constellation";
            this.btnCreateConstellation.UseVisualStyleBackColor = true;
            this.btnCreateConstellation.Click += new System.EventHandler(this.btnCreateConstellation_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Fn";
            // 
            // nudFn
            // 
            this.nudFn.Location = new System.Drawing.Point(61, 24);
            this.nudFn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFn.Name = "nudFn";
            this.nudFn.Size = new System.Drawing.Size(63, 20);
            this.nudFn.TabIndex = 10;
            this.toolTip1.SetToolTip(this.nudFn, "Phasing numerator parameter");
            this.nudFn.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudFn.ValueChanged += new System.EventHandler(this.nudFn_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Fd";
            // 
            // nudFd
            // 
            this.nudFd.Location = new System.Drawing.Point(61, 50);
            this.nudFd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFd.Name = "nudFd";
            this.nudFd.Size = new System.Drawing.Size(63, 20);
            this.nudFd.TabIndex = 11;
            this.toolTip1.SetToolTip(this.nudFd, "Phasing denumerator parameter");
            this.nudFd.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudFd.ValueChanged += new System.EventHandler(this.nudFd_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Perigee Altitude (km)";
            // 
            // nudPerigeeAlt
            // 
            this.nudPerigeeAlt.DecimalPlaces = 1;
            this.nudPerigeeAlt.Location = new System.Drawing.Point(176, 20);
            this.nudPerigeeAlt.Maximum = new decimal(new int[] {
            36000,
            0,
            0,
            0});
            this.nudPerigeeAlt.Minimum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.nudPerigeeAlt.Name = "nudPerigeeAlt";
            this.nudPerigeeAlt.Size = new System.Drawing.Size(63, 20);
            this.nudPerigeeAlt.TabIndex = 7;
            this.nudPerigeeAlt.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudPerigeeAlt.ValueChanged += new System.EventHandler(this.nudPerigeeAlt_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Inclination (deg)";
            // 
            // nudInclination
            // 
            this.nudInclination.DecimalPlaces = 1;
            this.nudInclination.Location = new System.Drawing.Point(176, 48);
            this.nudInclination.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nudInclination.Name = "nudInclination";
            this.nudInclination.Size = new System.Drawing.Size(63, 20);
            this.nudInclination.TabIndex = 8;
            this.nudInclination.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.nudInclination.ValueChanged += new System.EventHandler(this.nudInclination_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Constellation Name";
            // 
            // tbConstellationName
            // 
            this.tbConstellationName.Location = new System.Drawing.Point(139, 23);
            this.tbConstellationName.Name = "tbConstellationName";
            this.tbConstellationName.Size = new System.Drawing.Size(100, 20);
            this.tbConstellationName.TabIndex = 1;
            this.tbConstellationName.Text = "Flower";
            this.toolTip1.SetToolTip(this.tbConstellationName, "Constellation Name");
            // 
            // pnlCurrentColor
            // 
            this.pnlCurrentColor.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlCurrentColor.Location = new System.Drawing.Point(9, 22);
            this.pnlCurrentColor.Name = "pnlCurrentColor";
            this.pnlCurrentColor.Size = new System.Drawing.Size(23, 23);
            this.pnlCurrentColor.TabIndex = 17;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(38, 22);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(101, 23);
            this.btnColor.TabIndex = 18;
            this.btnColor.Text = "Choose Color...";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // cbConstellationObj
            // 
            this.cbConstellationObj.AutoSize = true;
            this.cbConstellationObj.Checked = true;
            this.cbConstellationObj.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbConstellationObj.Location = new System.Drawing.Point(9, 73);
            this.cbConstellationObj.Name = "cbConstellationObj";
            this.cbConstellationObj.Size = new System.Drawing.Size(154, 17);
            this.cbConstellationObj.TabIndex = 19;
            this.cbConstellationObj.Text = "Create Constellation Object";
            this.toolTip1.SetToolTip(this.cbConstellationObj, "Create a constellation object STK");
            this.cbConstellationObj.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Fh";
            // 
            // nudFh
            // 
            this.nudFh.Location = new System.Drawing.Point(61, 78);
            this.nudFh.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudFh.Name = "nudFh";
            this.nudFh.Size = new System.Drawing.Size(63, 20);
            this.nudFh.TabIndex = 12;
            this.toolTip1.SetToolTip(this.nudFh, "Inter-plane phase parameter");
            this.nudFh.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFh.ValueChanged += new System.EventHandler(this.nudFh_ValueChanged);
            // 
            // gbCommonParameters
            // 
            this.gbCommonParameters.Controls.Add(this.label10);
            this.gbCommonParameters.Controls.Add(this.label6);
            this.gbCommonParameters.Controls.Add(this.nudArgOfPerigee);
            this.gbCommonParameters.Controls.Add(this.nudPerigeeAlt);
            this.gbCommonParameters.Controls.Add(this.nudInclination);
            this.gbCommonParameters.Controls.Add(this.label7);
            this.gbCommonParameters.Location = new System.Drawing.Point(12, 233);
            this.gbCommonParameters.Name = "gbCommonParameters";
            this.gbCommonParameters.Size = new System.Drawing.Size(256, 105);
            this.gbCommonParameters.TabIndex = 22;
            this.gbCommonParameters.TabStop = false;
            this.gbCommonParameters.Text = "Common Orbit Parameters";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(130, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Argument of Perigee (deg)";
            // 
            // nudArgOfPerigee
            // 
            this.nudArgOfPerigee.DecimalPlaces = 1;
            this.nudArgOfPerigee.Location = new System.Drawing.Point(176, 74);
            this.nudArgOfPerigee.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudArgOfPerigee.Name = "nudArgOfPerigee";
            this.nudArgOfPerigee.Size = new System.Drawing.Size(63, 20);
            this.nudArgOfPerigee.TabIndex = 9;
            this.nudArgOfPerigee.ValueChanged += new System.EventHandler(this.nudArgOfPerigee_ValueChanged);
            // 
            // bgPhasing
            // 
            this.bgPhasing.Controls.Add(this.nudFn);
            this.bgPhasing.Controls.Add(this.label4);
            this.bgPhasing.Controls.Add(this.label9);
            this.bgPhasing.Controls.Add(this.nudFd);
            this.bgPhasing.Controls.Add(this.nudFh);
            this.bgPhasing.Controls.Add(this.label5);
            this.bgPhasing.Location = new System.Drawing.Point(274, 40);
            this.bgPhasing.Name = "bgPhasing";
            this.bgPhasing.Size = new System.Drawing.Size(173, 116);
            this.bgPhasing.TabIndex = 23;
            this.bgPhasing.TabStop = false;
            this.bgPhasing.Text = "Phasing Parameters";
            // 
            // gbConstellationParameters
            // 
            this.gbConstellationParameters.Controls.Add(this.label11);
            this.gbConstellationParameters.Controls.Add(this.label8);
            this.gbConstellationParameters.Controls.Add(this.nudInitialMeanAnomaly);
            this.gbConstellationParameters.Controls.Add(this.nudDays);
            this.gbConstellationParameters.Controls.Add(this.nudInitialRAAN);
            this.gbConstellationParameters.Controls.Add(this.label12);
            this.gbConstellationParameters.Controls.Add(this.label1);
            this.gbConstellationParameters.Controls.Add(this.nudPetals);
            this.gbConstellationParameters.Controls.Add(this.label2);
            this.gbConstellationParameters.Controls.Add(this.nudSatellites);
            this.gbConstellationParameters.Controls.Add(this.tbConstellationName);
            this.gbConstellationParameters.Controls.Add(this.label3);
            this.gbConstellationParameters.Location = new System.Drawing.Point(12, 40);
            this.gbConstellationParameters.Name = "gbConstellationParameters";
            this.gbConstellationParameters.Size = new System.Drawing.Size(256, 187);
            this.gbConstellationParameters.TabIndex = 24;
            this.gbConstellationParameters.TabStop = false;
            this.gbConstellationParameters.Text = "Constellation Parameters";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 155);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(145, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Initial Mean Anomomaly (deg)";
            // 
            // nudInitialMeanAnomaly
            // 
            this.nudInitialMeanAnomaly.DecimalPlaces = 1;
            this.nudInitialMeanAnomaly.Location = new System.Drawing.Point(176, 153);
            this.nudInitialMeanAnomaly.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudInitialMeanAnomaly.Name = "nudInitialMeanAnomaly";
            this.nudInitialMeanAnomaly.Size = new System.Drawing.Size(63, 20);
            this.nudInitialMeanAnomaly.TabIndex = 6;
            this.toolTip1.SetToolTip(this.nudInitialMeanAnomaly, "Mean anomaly of the first satellite");
            // 
            // nudInitialRAAN
            // 
            this.nudInitialRAAN.DecimalPlaces = 1;
            this.nudInitialRAAN.Location = new System.Drawing.Point(176, 127);
            this.nudInitialRAAN.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudInitialRAAN.Name = "nudInitialRAAN";
            this.nudInitialRAAN.Size = new System.Drawing.Size(63, 20);
            this.nudInitialRAAN.TabIndex = 5;
            this.toolTip1.SetToolTip(this.nudInitialRAAN, "RAAN of the first satellite");
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Initial RAAN (deg)";
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbFixedFrame);
            this.gbOptions.Controls.Add(this.cbInertialFrame);
            this.gbOptions.Controls.Add(this.btnColor);
            this.gbOptions.Controls.Add(this.pnlCurrentColor);
            this.gbOptions.Controls.Add(this.cbConstellationObj);
            this.gbOptions.Location = new System.Drawing.Point(274, 162);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(173, 176);
            this.gbOptions.TabIndex = 25;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // cbFixedFrame
            // 
            this.cbFixedFrame.AutoSize = true;
            this.cbFixedFrame.Checked = true;
            this.cbFixedFrame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFixedFrame.Location = new System.Drawing.Point(9, 119);
            this.cbFixedFrame.Name = "cbFixedFrame";
            this.cbFixedFrame.Size = new System.Drawing.Size(124, 17);
            this.cbFixedFrame.TabIndex = 21;
            this.cbFixedFrame.Text = "Show in Fixed Frame";
            this.toolTip1.SetToolTip(this.cbFixedFrame, "Add 3D orbit in Fixed frame");
            this.cbFixedFrame.UseVisualStyleBackColor = true;
            // 
            // cbInertialFrame
            // 
            this.cbInertialFrame.AutoSize = true;
            this.cbInertialFrame.Location = new System.Drawing.Point(9, 96);
            this.cbInertialFrame.Name = "cbInertialFrame";
            this.cbInertialFrame.Size = new System.Drawing.Size(130, 17);
            this.cbInertialFrame.TabIndex = 20;
            this.cbInertialFrame.Text = "Show in Inertial Frame";
            this.toolTip1.SetToolTip(this.cbInertialFrame, "Add 3D orbit in Inertial frame");
            this.cbInertialFrame.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(459, 22);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(459, 24);
            this.menuStrip1.TabIndex = 27;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.examplesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // examplesToolStripMenuItem
            // 
            this.examplesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loneStarToolStripMenuItem,
            this.secondaryClosedPathsToolStripMenuItem,
            this.molnyiaToolStripMenuItem});
            this.examplesToolStripMenuItem.Name = "examplesToolStripMenuItem";
            this.examplesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.examplesToolStripMenuItem.Text = "Examples";
            // 
            // loneStarToolStripMenuItem
            // 
            this.loneStarToolStripMenuItem.Name = "loneStarToolStripMenuItem";
            this.loneStarToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.loneStarToolStripMenuItem.Text = "Lone Star";
            this.loneStarToolStripMenuItem.Click += new System.EventHandler(this.loneStarToolStripMenuItem_Click);
            // 
            // secondaryClosedPathsToolStripMenuItem
            // 
            this.secondaryClosedPathsToolStripMenuItem.Name = "secondaryClosedPathsToolStripMenuItem";
            this.secondaryClosedPathsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.secondaryClosedPathsToolStripMenuItem.Text = "Secondary Closed Paths";
            this.secondaryClosedPathsToolStripMenuItem.Click += new System.EventHandler(this.secondaryClosedPathsToolStripMenuItem_Click);
            // 
            // gbDeriverParameter
            // 
            this.gbDeriverParameter.Controls.Add(this.tbEcc);
            this.gbDeriverParameter.Controls.Add(this.tbApogeeHeight);
            this.gbDeriverParameter.Controls.Add(this.tbSma);
            this.gbDeriverParameter.Controls.Add(this.label13);
            this.gbDeriverParameter.Controls.Add(this.label14);
            this.gbDeriverParameter.Controls.Add(this.label15);
            this.gbDeriverParameter.Location = new System.Drawing.Point(12, 344);
            this.gbDeriverParameter.Name = "gbDeriverParameter";
            this.gbDeriverParameter.Size = new System.Drawing.Size(256, 104);
            this.gbDeriverParameter.TabIndex = 29;
            this.gbDeriverParameter.TabStop = false;
            this.gbDeriverParameter.Text = "Derived Parameters";
            // 
            // tbEcc
            // 
            this.tbEcc.BackColor = System.Drawing.SystemColors.Control;
            this.tbEcc.Enabled = false;
            this.tbEcc.Location = new System.Drawing.Point(155, 73);
            this.tbEcc.Name = "tbEcc";
            this.tbEcc.Size = new System.Drawing.Size(84, 20);
            this.tbEcc.TabIndex = 30;
            // 
            // tbApogeeHeight
            // 
            this.tbApogeeHeight.BackColor = System.Drawing.SystemColors.Control;
            this.tbApogeeHeight.Enabled = false;
            this.tbApogeeHeight.Location = new System.Drawing.Point(155, 47);
            this.tbApogeeHeight.Name = "tbApogeeHeight";
            this.tbApogeeHeight.Size = new System.Drawing.Size(84, 20);
            this.tbApogeeHeight.TabIndex = 29;
            // 
            // tbSma
            // 
            this.tbSma.BackColor = System.Drawing.SystemColors.Control;
            this.tbSma.Enabled = false;
            this.tbSma.Location = new System.Drawing.Point(155, 19);
            this.tbSma.Name = "tbSma";
            this.tbSma.Size = new System.Drawing.Size(84, 20);
            this.tbSma.TabIndex = 28;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 76);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Eccentricity";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 13);
            this.label14.TabIndex = 25;
            this.label14.Text = "Semimajor Axis (km)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 50);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(101, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "Apogee Height (km)";
            // 
            // molnyiaToolStripMenuItem
            // 
            this.molnyiaToolStripMenuItem.Name = "molnyiaToolStripMenuItem";
            this.molnyiaToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.molnyiaToolStripMenuItem.Text = "Molnyia";
            this.molnyiaToolStripMenuItem.Click += new System.EventHandler(this.molnyiaToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 482);
            this.Controls.Add(this.gbDeriverParameter);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.gbConstellationParameters);
            this.Controls.Add(this.bgPhasing);
            this.Controls.Add(this.gbCommonParameters);
            this.Controls.Add(this.btnCreateConstellation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Flower Constellation Builder v0.4";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPetals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSatellites)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPerigeeAlt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInclination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFh)).EndInit();
            this.gbCommonParameters.ResumeLayout(false);
            this.gbCommonParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArgOfPerigee)).EndInit();
            this.bgPhasing.ResumeLayout(false);
            this.bgPhasing.PerformLayout();
            this.gbConstellationParameters.ResumeLayout(false);
            this.gbConstellationParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInitialMeanAnomaly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInitialRAAN)).EndInit();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbDeriverParameter.ResumeLayout(false);
            this.gbDeriverParameter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudDays;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudPetals;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudSatellites;
        private System.Windows.Forms.Button btnCreateConstellation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudFn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudFd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudPerigeeAlt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudInclination;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbConstellationName;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel pnlCurrentColor;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.CheckBox cbConstellationObj;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudFh;
        private System.Windows.Forms.GroupBox gbCommonParameters;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudArgOfPerigee;
        private System.Windows.Forms.GroupBox bgPhasing;
        private System.Windows.Forms.GroupBox gbConstellationParameters;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudInitialMeanAnomaly;
        private System.Windows.Forms.NumericUpDown nudInitialRAAN;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbFixedFrame;
        private System.Windows.Forms.CheckBox cbInertialFrame;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem examplesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loneStarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondaryClosedPathsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.GroupBox gbDeriverParameter;
        private System.Windows.Forms.TextBox tbEcc;
        private System.Windows.Forms.TextBox tbApogeeHeight;
        private System.Windows.Forms.TextBox tbSma;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ToolStripMenuItem molnyiaToolStripMenuItem;
    }
}

