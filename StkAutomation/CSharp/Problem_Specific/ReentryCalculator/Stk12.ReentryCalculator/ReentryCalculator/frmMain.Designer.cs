namespace ReentryCalculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateSatcatDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTleFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.gbTleData = new System.Windows.Forms.GroupBox();
            this.lblMeanMotion = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRevNumber = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblEccentricity = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblInclination = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSatNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTleEpoch = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateScenarioTle = new System.Windows.Forms.Button();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPropagator = new System.Windows.Forms.ComboBox();
            this.gbSatData = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbCr = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbSolarArea = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbCd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDragArea = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbMass = new System.Windows.Forms.TextBox();
            this.gbUncertainty = new System.Windows.Forms.GroupBox();
            this.tbRadialPosUnc = new System.Windows.Forms.TextBox();
            this.tbCrosstrackPosUnc = new System.Windows.Forms.TextBox();
            this.tbIntrackVelUnc = new System.Windows.Forms.TextBox();
            this.tbRadialVelUnc = new System.Windows.Forms.TextBox();
            this.tbCrosstrackVelUnc = new System.Windows.Forms.TextBox();
            this.tbIntrackPosUnc = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnRunAnalysis = new System.Windows.Forms.Button();
            this.gbAnalysis = new System.Windows.Forms.GroupBox();
            this.btnCalculateFoM = new System.Windows.Forms.Button();
            this.btnGenerateMto = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.nudNRuns = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnCreateScenarioSatcat = new System.Windows.Forms.Button();
            this.tcInput = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnImportTle = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.tbKeyword = new System.Windows.Forms.TextBox();
            this.dgvSatList = new System.Windows.Forms.DataGridView();
            this.IntDesignator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CatalogNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbEventLogger = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.gbTleData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            this.gbSatData.SuspendLayout();
            this.gbUncertainty.SuspendLayout();
            this.gbAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNRuns)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tcInput.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSatList)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(554, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateSatcatDataToolStripMenuItem,
            this.saveLogToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // updateSatcatDataToolStripMenuItem
            // 
            this.updateSatcatDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("updateSatcatDataToolStripMenuItem.Image")));
            this.updateSatcatDataToolStripMenuItem.Name = "updateSatcatDataToolStripMenuItem";
            this.updateSatcatDataToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.updateSatcatDataToolStripMenuItem.Text = "Update SATCAT Data";
            // 
            // saveLogToolStripMenuItem
            // 
            this.saveLogToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveLogToolStripMenuItem.Image")));
            this.saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem";
            this.saveLogToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.saveLogToolStripMenuItem.Text = "Save Log";
            this.saveLogToolStripMenuItem.Click += new System.EventHandler(this.saveLogToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("clearLogToolStripMenuItem.Image")));
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearLogToolStripMenuItem.Text = "Clear Log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // openTleFileDialog
            // 
            this.openTleFileDialog.Filter = "TLE Files|*.tle";
            // 
            // gbTleData
            // 
            this.gbTleData.Controls.Add(this.lblMeanMotion);
            this.gbTleData.Controls.Add(this.label8);
            this.gbTleData.Controls.Add(this.lblRevNumber);
            this.gbTleData.Controls.Add(this.label4);
            this.gbTleData.Controls.Add(this.lblEccentricity);
            this.gbTleData.Controls.Add(this.label7);
            this.gbTleData.Controls.Add(this.lblInclination);
            this.gbTleData.Controls.Add(this.label5);
            this.gbTleData.Controls.Add(this.lblSatNumber);
            this.gbTleData.Controls.Add(this.label3);
            this.gbTleData.Controls.Add(this.lblTleEpoch);
            this.gbTleData.Controls.Add(this.label1);
            this.gbTleData.Location = new System.Drawing.Point(242, 10);
            this.gbTleData.Name = "gbTleData";
            this.gbTleData.Size = new System.Drawing.Size(251, 191);
            this.gbTleData.TabIndex = 1;
            this.gbTleData.TabStop = false;
            this.gbTleData.Text = "TLE Data";
            // 
            // lblMeanMotion
            // 
            this.lblMeanMotion.AutoSize = true;
            this.lblMeanMotion.Location = new System.Drawing.Point(110, 159);
            this.lblMeanMotion.Name = "lblMeanMotion";
            this.lblMeanMotion.Size = new System.Drawing.Size(0, 13);
            this.lblMeanMotion.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Mean Motion";
            // 
            // lblRevNumber
            // 
            this.lblRevNumber.AutoSize = true;
            this.lblRevNumber.Location = new System.Drawing.Point(110, 130);
            this.lblRevNumber.Name = "lblRevNumber";
            this.lblRevNumber.Size = new System.Drawing.Size(0, 13);
            this.lblRevNumber.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Rev Number";
            // 
            // lblEccentricity
            // 
            this.lblEccentricity.AutoSize = true;
            this.lblEccentricity.Location = new System.Drawing.Point(109, 104);
            this.lblEccentricity.Name = "lblEccentricity";
            this.lblEccentricity.Size = new System.Drawing.Size(0, 13);
            this.lblEccentricity.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Eccentricity";
            // 
            // lblInclination
            // 
            this.lblInclination.AutoSize = true;
            this.lblInclination.Location = new System.Drawing.Point(110, 78);
            this.lblInclination.Name = "lblInclination";
            this.lblInclination.Size = new System.Drawing.Size(0, 13);
            this.lblInclination.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Inclination";
            // 
            // lblSatNumber
            // 
            this.lblSatNumber.AutoSize = true;
            this.lblSatNumber.Location = new System.Drawing.Point(109, 52);
            this.lblSatNumber.Name = "lblSatNumber";
            this.lblSatNumber.Size = new System.Drawing.Size(0, 13);
            this.lblSatNumber.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sat Number";
            // 
            // lblTleEpoch
            // 
            this.lblTleEpoch.AutoSize = true;
            this.lblTleEpoch.Location = new System.Drawing.Point(110, 26);
            this.lblTleEpoch.Name = "lblTleEpoch";
            this.lblTleEpoch.Size = new System.Drawing.Size(0, 13);
            this.lblTleEpoch.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TLE Epoch";
            // 
            // btnCreateScenarioTle
            // 
            this.btnCreateScenarioTle.Enabled = false;
            this.btnCreateScenarioTle.Location = new System.Drawing.Point(146, 250);
            this.btnCreateScenarioTle.Name = "btnCreateScenarioTle";
            this.btnCreateScenarioTle.Size = new System.Drawing.Size(173, 30);
            this.btnCreateScenarioTle.TabIndex = 14;
            this.btnCreateScenarioTle.Text = "Create Scenario from TLE";
            this.btnCreateScenarioTle.UseVisualStyleBackColor = true;
            this.btnCreateScenarioTle.Click += new System.EventHandler(this.btnCreateScenarioTle_Click);
            // 
            // nudDuration
            // 
            this.nudDuration.Location = new System.Drawing.Point(91, 257);
            this.nudDuration.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.Size = new System.Drawing.Size(49, 20);
            this.nudDuration.TabIndex = 13;
            this.nudDuration.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Duration (days)";
            // 
            // cbPropagator
            // 
            this.cbPropagator.FormattingEnabled = true;
            this.cbPropagator.Items.AddRange(new object[] {
            "Jacchia-Roberts",
            "NRLMSISE 2000"});
            this.cbPropagator.Location = new System.Drawing.Point(97, 24);
            this.cbPropagator.Name = "cbPropagator";
            this.cbPropagator.Size = new System.Drawing.Size(130, 21);
            this.cbPropagator.TabIndex = 3;
            this.cbPropagator.Text = "Select...";
            // 
            // gbSatData
            // 
            this.gbSatData.Controls.Add(this.label17);
            this.gbSatData.Controls.Add(this.tbCr);
            this.gbSatData.Controls.Add(this.label18);
            this.gbSatData.Controls.Add(this.tbSolarArea);
            this.gbSatData.Controls.Add(this.label10);
            this.gbSatData.Controls.Add(this.tbCd);
            this.gbSatData.Controls.Add(this.label9);
            this.gbSatData.Controls.Add(this.tbDragArea);
            this.gbSatData.Controls.Add(this.label6);
            this.gbSatData.Controls.Add(this.tbMass);
            this.gbSatData.Enabled = false;
            this.gbSatData.Location = new System.Drawing.Point(10, 286);
            this.gbSatData.Name = "gbSatData";
            this.gbSatData.Size = new System.Drawing.Size(251, 192);
            this.gbSatData.TabIndex = 4;
            this.gbSatData.TabStop = false;
            this.gbSatData.Text = "Satellite Data";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 134);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Cr";
            // 
            // tbCr
            // 
            this.tbCr.Location = new System.Drawing.Point(182, 131);
            this.tbCr.Name = "tbCr";
            this.tbCr.Size = new System.Drawing.Size(52, 20);
            this.tbCr.TabIndex = 8;
            this.tbCr.Text = "2.1";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 108);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 13);
            this.label18.TabIndex = 7;
            this.label18.Text = "Solar Area (m^2)";
            // 
            // tbSolarArea
            // 
            this.tbSolarArea.Location = new System.Drawing.Point(182, 105);
            this.tbSolarArea.Name = "tbSolarArea";
            this.tbSolarArea.Size = new System.Drawing.Size(52, 20);
            this.tbSolarArea.TabIndex = 6;
            this.tbSolarArea.Text = "5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Cd";
            // 
            // tbCd
            // 
            this.tbCd.Location = new System.Drawing.Point(182, 79);
            this.tbCd.Name = "tbCd";
            this.tbCd.Size = new System.Drawing.Size(52, 20);
            this.tbCd.TabIndex = 4;
            this.tbCd.Text = "2.1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Drag Area (m^2)";
            // 
            // tbDragArea
            // 
            this.tbDragArea.Location = new System.Drawing.Point(182, 53);
            this.tbDragArea.Name = "tbDragArea";
            this.tbDragArea.Size = new System.Drawing.Size(52, 20);
            this.tbDragArea.TabIndex = 2;
            this.tbDragArea.Text = "5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Mass (kg)";
            // 
            // tbMass
            // 
            this.tbMass.Location = new System.Drawing.Point(182, 27);
            this.tbMass.Name = "tbMass";
            this.tbMass.Size = new System.Drawing.Size(52, 20);
            this.tbMass.TabIndex = 0;
            this.tbMass.Text = "500";
            // 
            // gbUncertainty
            // 
            this.gbUncertainty.Controls.Add(this.tbRadialPosUnc);
            this.gbUncertainty.Controls.Add(this.tbCrosstrackPosUnc);
            this.gbUncertainty.Controls.Add(this.tbIntrackVelUnc);
            this.gbUncertainty.Controls.Add(this.tbRadialVelUnc);
            this.gbUncertainty.Controls.Add(this.tbCrosstrackVelUnc);
            this.gbUncertainty.Controls.Add(this.tbIntrackPosUnc);
            this.gbUncertainty.Controls.Add(this.label14);
            this.gbUncertainty.Controls.Add(this.label15);
            this.gbUncertainty.Controls.Add(this.label16);
            this.gbUncertainty.Controls.Add(this.label13);
            this.gbUncertainty.Controls.Add(this.label12);
            this.gbUncertainty.Controls.Add(this.label11);
            this.gbUncertainty.Enabled = false;
            this.gbUncertainty.Location = new System.Drawing.Point(267, 286);
            this.gbUncertainty.Name = "gbUncertainty";
            this.gbUncertainty.Size = new System.Drawing.Size(251, 192);
            this.gbUncertainty.TabIndex = 5;
            this.gbUncertainty.TabStop = false;
            this.gbUncertainty.Text = "Initial State 1-Sigma Uncertainties";
            // 
            // tbRadialPosUnc
            // 
            this.tbRadialPosUnc.Location = new System.Drawing.Point(182, 53);
            this.tbRadialPosUnc.Name = "tbRadialPosUnc";
            this.tbRadialPosUnc.Size = new System.Drawing.Size(52, 20);
            this.tbRadialPosUnc.TabIndex = 17;
            this.tbRadialPosUnc.Text = "0.1";
            // 
            // tbCrosstrackPosUnc
            // 
            this.tbCrosstrackPosUnc.Location = new System.Drawing.Point(182, 79);
            this.tbCrosstrackPosUnc.Name = "tbCrosstrackPosUnc";
            this.tbCrosstrackPosUnc.Size = new System.Drawing.Size(52, 20);
            this.tbCrosstrackPosUnc.TabIndex = 16;
            this.tbCrosstrackPosUnc.Text = "0.1";
            // 
            // tbIntrackVelUnc
            // 
            this.tbIntrackVelUnc.Location = new System.Drawing.Point(182, 105);
            this.tbIntrackVelUnc.Name = "tbIntrackVelUnc";
            this.tbIntrackVelUnc.Size = new System.Drawing.Size(52, 20);
            this.tbIntrackVelUnc.TabIndex = 15;
            this.tbIntrackVelUnc.Text = "0";
            // 
            // tbRadialVelUnc
            // 
            this.tbRadialVelUnc.Location = new System.Drawing.Point(182, 131);
            this.tbRadialVelUnc.Name = "tbRadialVelUnc";
            this.tbRadialVelUnc.Size = new System.Drawing.Size(52, 20);
            this.tbRadialVelUnc.TabIndex = 14;
            this.tbRadialVelUnc.Text = "0";
            // 
            // tbCrosstrackVelUnc
            // 
            this.tbCrosstrackVelUnc.Location = new System.Drawing.Point(182, 157);
            this.tbCrosstrackVelUnc.Name = "tbCrosstrackVelUnc";
            this.tbCrosstrackVelUnc.Size = new System.Drawing.Size(52, 20);
            this.tbCrosstrackVelUnc.TabIndex = 13;
            this.tbCrosstrackVelUnc.Text = "0";
            // 
            // tbIntrackPosUnc
            // 
            this.tbIntrackPosUnc.Location = new System.Drawing.Point(182, 27);
            this.tbIntrackPosUnc.Name = "tbIntrackPosUnc";
            this.tbIntrackPosUnc.Size = new System.Drawing.Size(52, 20);
            this.tbIntrackPosUnc.TabIndex = 12;
            this.tbIntrackPosUnc.Text = "0.5";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 134);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Radial Velocity (m/sec)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 160);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(143, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "Cross-Track Velocity (m/sec)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 108);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(126, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "In-Track Velocity (m/sec)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 56);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Radial Position (km)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Cross-Track Position (km)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "In-Track Position (km)";
            // 
            // btnRunAnalysis
            // 
            this.btnRunAnalysis.Location = new System.Drawing.Point(350, 24);
            this.btnRunAnalysis.Name = "btnRunAnalysis";
            this.btnRunAnalysis.Size = new System.Drawing.Size(141, 30);
            this.btnRunAnalysis.TabIndex = 15;
            this.btnRunAnalysis.Text = "Run Analysis";
            this.btnRunAnalysis.UseVisualStyleBackColor = true;
            this.btnRunAnalysis.Click += new System.EventHandler(this.btnRunAnalysis_Click);
            // 
            // gbAnalysis
            // 
            this.gbAnalysis.Controls.Add(this.btnCalculateFoM);
            this.gbAnalysis.Controls.Add(this.btnGenerateMto);
            this.gbAnalysis.Controls.Add(this.label20);
            this.gbAnalysis.Controls.Add(this.nudNRuns);
            this.gbAnalysis.Controls.Add(this.btnRunAnalysis);
            this.gbAnalysis.Controls.Add(this.label19);
            this.gbAnalysis.Controls.Add(this.cbPropagator);
            this.gbAnalysis.Enabled = false;
            this.gbAnalysis.Location = new System.Drawing.Point(10, 484);
            this.gbAnalysis.Name = "gbAnalysis";
            this.gbAnalysis.Size = new System.Drawing.Size(508, 139);
            this.gbAnalysis.TabIndex = 16;
            this.gbAnalysis.TabStop = false;
            this.gbAnalysis.Text = "Analysis";
            // 
            // btnCalculateFoM
            // 
            this.btnCalculateFoM.Enabled = false;
            this.btnCalculateFoM.Location = new System.Drawing.Point(350, 96);
            this.btnCalculateFoM.Name = "btnCalculateFoM";
            this.btnCalculateFoM.Size = new System.Drawing.Size(141, 30);
            this.btnCalculateFoM.TabIndex = 18;
            this.btnCalculateFoM.Text = "Calculate FoM";
            this.btnCalculateFoM.UseVisualStyleBackColor = true;
            this.btnCalculateFoM.Click += new System.EventHandler(this.btnCalculateFoM_Click);
            // 
            // btnGenerateMto
            // 
            this.btnGenerateMto.Enabled = false;
            this.btnGenerateMto.Location = new System.Drawing.Point(350, 60);
            this.btnGenerateMto.Name = "btnGenerateMto";
            this.btnGenerateMto.Size = new System.Drawing.Size(141, 30);
            this.btnGenerateMto.TabIndex = 17;
            this.btnGenerateMto.Text = "Generate MTO";
            this.btnGenerateMto.UseVisualStyleBackColor = true;
            this.btnGenerateMto.Click += new System.EventHandler(this.btnGenerateMto_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(16, 27);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(74, 13);
            this.label20.TabIndex = 16;
            this.label20.Text = "Density Model";
            // 
            // nudNRuns
            // 
            this.nudNRuns.Location = new System.Drawing.Point(175, 56);
            this.nudNRuns.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudNRuns.Name = "nudNRuns";
            this.nudNRuns.Size = new System.Drawing.Size(52, 20);
            this.nudNRuns.TabIndex = 15;
            this.nudNRuns.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(18, 58);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(43, 13);
            this.label19.TabIndex = 14;
            this.label19.Text = "N Runs";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 692);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(554, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabPage1);
            this.tcMain.Controls.Add(this.tabPage2);
            this.tcMain.Location = new System.Drawing.Point(12, 27);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(535, 658);
            this.tcMain.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.btnCreateScenarioSatcat);
            this.tabPage1.Controls.Add(this.btnCreateScenarioTle);
            this.tabPage1.Controls.Add(this.tcInput);
            this.tabPage1.Controls.Add(this.nudDuration);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.gbSatData);
            this.tabPage1.Controls.Add(this.gbAnalysis);
            this.tabPage1.Controls.Add(this.gbUncertainty);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(527, 632);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Input";
            // 
            // btnCreateScenarioSatcat
            // 
            this.btnCreateScenarioSatcat.Enabled = false;
            this.btnCreateScenarioSatcat.Location = new System.Drawing.Point(328, 250);
            this.btnCreateScenarioSatcat.Name = "btnCreateScenarioSatcat";
            this.btnCreateScenarioSatcat.Size = new System.Drawing.Size(173, 30);
            this.btnCreateScenarioSatcat.TabIndex = 18;
            this.btnCreateScenarioSatcat.Text = "Create Scenario from SATCAT";
            this.btnCreateScenarioSatcat.UseVisualStyleBackColor = true;
            this.btnCreateScenarioSatcat.Click += new System.EventHandler(this.btnCreateScenarioSatcat_Click);
            // 
            // tcInput
            // 
            this.tcInput.Controls.Add(this.tabPage3);
            this.tcInput.Controls.Add(this.tabPage4);
            this.tcInput.Location = new System.Drawing.Point(6, 6);
            this.tcInput.Name = "tcInput";
            this.tcInput.SelectedIndex = 0;
            this.tcInput.Size = new System.Drawing.Size(514, 236);
            this.tcInput.TabIndex = 17;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.btnImportTle);
            this.tabPage3.Controls.Add(this.gbTleData);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(506, 210);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "TLE";
            // 
            // btnImportTle
            // 
            this.btnImportTle.Location = new System.Drawing.Point(21, 19);
            this.btnImportTle.Name = "btnImportTle";
            this.btnImportTle.Size = new System.Drawing.Size(141, 30);
            this.btnImportTle.TabIndex = 19;
            this.btnImportTle.Text = "Import TLE";
            this.btnImportTle.UseVisualStyleBackColor = true;
            this.btnImportTle.Click += new System.EventHandler(this.btnImportTle_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.btnSearch);
            this.tabPage4.Controls.Add(this.label21);
            this.tabPage4.Controls.Add(this.tbKeyword);
            this.tabPage4.Controls.Add(this.dgvSatList);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(506, 210);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "SATCAT";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(350, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(141, 30);
            this.btnSearch.TabIndex = 267;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(9, 17);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 13);
            this.label21.TabIndex = 266;
            this.label21.Text = "Search SATCAT for...";
            // 
            // tbKeyword
            // 
            this.tbKeyword.Location = new System.Drawing.Point(125, 14);
            this.tbKeyword.Name = "tbKeyword";
            this.tbKeyword.Size = new System.Drawing.Size(172, 20);
            this.tbKeyword.TabIndex = 265;
            // 
            // dgvSatList
            // 
            this.dgvSatList.AllowUserToAddRows = false;
            this.dgvSatList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSatList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSatList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IntDesignator,
            this.CatalogNumber,
            this.CommonName});
            this.dgvSatList.Location = new System.Drawing.Point(6, 50);
            this.dgvSatList.MultiSelect = false;
            this.dgvSatList.Name = "dgvSatList";
            this.dgvSatList.Size = new System.Drawing.Size(485, 154);
            this.dgvSatList.TabIndex = 263;
            this.dgvSatList.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSatList_RowHeaderMouseClick);
            // 
            // IntDesignator
            // 
            this.IntDesignator.HeaderText = "International Designator";
            this.IntDesignator.Name = "IntDesignator";
            this.IntDesignator.ReadOnly = true;
            this.IntDesignator.Width = 140;
            // 
            // CatalogNumber
            // 
            this.CatalogNumber.HeaderText = "Catalog Number";
            this.CatalogNumber.Name = "CatalogNumber";
            this.CatalogNumber.ReadOnly = true;
            this.CatalogNumber.Width = 110;
            // 
            // CommonName
            // 
            this.CommonName.HeaderText = "Common Name";
            this.CommonName.Name = "CommonName";
            this.CommonName.ReadOnly = true;
            this.CommonName.Width = 170;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.tbEventLogger);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(527, 632);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Output";
            // 
            // tbEventLogger
            // 
            this.tbEventLogger.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbEventLogger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbEventLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbEventLogger.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEventLogger.ForeColor = System.Drawing.SystemColors.InfoText;
            this.tbEventLogger.Location = new System.Drawing.Point(3, 3);
            this.tbEventLogger.Name = "tbEventLogger";
            this.tbEventLogger.Size = new System.Drawing.Size(521, 626);
            this.tbEventLogger.TabIndex = 2;
            this.tbEventLogger.Text = "";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 714);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Reentry Calculator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbTleData.ResumeLayout(false);
            this.gbTleData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            this.gbSatData.ResumeLayout(false);
            this.gbSatData.PerformLayout();
            this.gbUncertainty.ResumeLayout(false);
            this.gbUncertainty.PerformLayout();
            this.gbAnalysis.ResumeLayout(false);
            this.gbAnalysis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNRuns)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tcInput.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSatList)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openTleFileDialog;
        private System.Windows.Forms.GroupBox gbTleData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTleEpoch;
        private System.Windows.Forms.Label lblEccentricity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblInclination;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSatNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMeanMotion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblRevNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreateScenarioTle;
        private System.Windows.Forms.NumericUpDown nudDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbPropagator;
        private System.Windows.Forms.GroupBox gbSatData;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbCd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbDragArea;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbMass;
        private System.Windows.Forms.GroupBox gbUncertainty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbRadialPosUnc;
        private System.Windows.Forms.TextBox tbCrosstrackPosUnc;
        private System.Windows.Forms.TextBox tbIntrackVelUnc;
        private System.Windows.Forms.TextBox tbRadialVelUnc;
        private System.Windows.Forms.TextBox tbCrosstrackVelUnc;
        private System.Windows.Forms.TextBox tbIntrackPosUnc;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnRunAnalysis;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbCr;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbSolarArea;
        private System.Windows.Forms.GroupBox gbAnalysis;
        private System.Windows.Forms.NumericUpDown nudNRuns;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.RichTextBox tbEventLogger;
        private System.Windows.Forms.Button btnCalculateFoM;
        private System.Windows.Forms.Button btnGenerateMto;
        private System.Windows.Forms.TabControl tcInput;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvSatList;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbKeyword;
        private System.Windows.Forms.Button btnCreateScenarioSatcat;
        private System.Windows.Forms.Button btnImportTle;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolStripMenuItem updateSatcatDataToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntDesignator;
        private System.Windows.Forms.DataGridViewTextBoxColumn CatalogNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommonName;
        private System.Windows.Forms.ToolStripMenuItem saveLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
    }
}

