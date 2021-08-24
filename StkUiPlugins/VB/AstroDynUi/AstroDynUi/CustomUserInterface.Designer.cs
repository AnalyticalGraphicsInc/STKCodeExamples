namespace AstroDynUi
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
            this.gbSat1 = new System.Windows.Forms.GroupBox();
            this.btnSat1del = new System.Windows.Forms.Button();
            this.lblSat1TrueAnomaly = new System.Windows.Forms.Label();
            this.lblSat1RAAN = new System.Windows.Forms.Label();
            this.lblSat1AoP = new System.Windows.Forms.Label();
            this.lblSat1Inclination = new System.Windows.Forms.Label();
            this.lblSat1Eccentricity = new System.Windows.Forms.Label();
            this.lblSat1SemiMajorAxis = new System.Windows.Forms.Label();
            this.btnSat1Prop = new System.Windows.Forms.Button();
            this.rbSat1J2 = new System.Windows.Forms.RadioButton();
            this.rbSat1TwoBody = new System.Windows.Forms.RadioButton();
            this.tbSat1TrueAnomaly = new System.Windows.Forms.TextBox();
            this.tbSat1RAAN = new System.Windows.Forms.TextBox();
            this.tbSat1AoP = new System.Windows.Forms.TextBox();
            this.tbSat1Inclination = new System.Windows.Forms.TextBox();
            this.tbSat1Eccentricity = new System.Windows.Forms.TextBox();
            this.tbSat1SemiMajorAxis = new System.Windows.Forms.TextBox();
            this.gbSat2 = new System.Windows.Forms.GroupBox();
            this.btnSat2Del = new System.Windows.Forms.Button();
            this.lblSat2TrueAnomaly = new System.Windows.Forms.Label();
            this.lblSat2RAAN = new System.Windows.Forms.Label();
            this.lblSat2AoP = new System.Windows.Forms.Label();
            this.lblSat2Inclination = new System.Windows.Forms.Label();
            this.lblSat2Eccentricity = new System.Windows.Forms.Label();
            this.lblSat2SemiMajorAxis = new System.Windows.Forms.Label();
            this.btnSat2Prop = new System.Windows.Forms.Button();
            this.rbSat2J2 = new System.Windows.Forms.RadioButton();
            this.rbSat2TwoBody = new System.Windows.Forms.RadioButton();
            this.tbSat2TrueAnomaly = new System.Windows.Forms.TextBox();
            this.tbSat2RAAN = new System.Windows.Forms.TextBox();
            this.tbSat2AoP = new System.Windows.Forms.TextBox();
            this.tbSat2Inclination = new System.Windows.Forms.TextBox();
            this.tbSat2Eccentricity = new System.Windows.Forms.TextBox();
            this.tbSat2SemiMajorAxis = new System.Windows.Forms.TextBox();
            this.cbOrbitPlane = new System.Windows.Forms.CheckBox();
            this.cbInclination = new System.Windows.Forms.CheckBox();
            this.cbFixedAxes = new System.Windows.Forms.CheckBox();
            this.cbInertialAxes = new System.Windows.Forms.CheckBox();
            this.cbEquatorialPlane = new System.Windows.Forms.CheckBox();
            this.cbRaan = new System.Windows.Forms.CheckBox();
            this.cbTrueAnomaly = new System.Windows.Forms.CheckBox();
            this.cbAoP = new System.Windows.Forms.CheckBox();
            this.gbEarth = new System.Windows.Forms.GroupBox();
            this.cbSunVector = new System.Windows.Forms.CheckBox();
            this.gbOrbit = new System.Windows.Forms.GroupBox();
            this.btnCopySat2 = new System.Windows.Forms.Button();
            this.btnCopySat1 = new System.Windows.Forms.Button();
            this.gbSat1.SuspendLayout();
            this.gbSat2.SuspendLayout();
            this.gbEarth.SuspendLayout();
            this.gbOrbit.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSat1
            // 
            this.gbSat1.Controls.Add(this.btnCopySat2);
            this.gbSat1.Controls.Add(this.btnSat1del);
            this.gbSat1.Controls.Add(this.lblSat1TrueAnomaly);
            this.gbSat1.Controls.Add(this.lblSat1RAAN);
            this.gbSat1.Controls.Add(this.lblSat1AoP);
            this.gbSat1.Controls.Add(this.lblSat1Inclination);
            this.gbSat1.Controls.Add(this.lblSat1Eccentricity);
            this.gbSat1.Controls.Add(this.lblSat1SemiMajorAxis);
            this.gbSat1.Controls.Add(this.btnSat1Prop);
            this.gbSat1.Controls.Add(this.rbSat1J2);
            this.gbSat1.Controls.Add(this.rbSat1TwoBody);
            this.gbSat1.Controls.Add(this.tbSat1TrueAnomaly);
            this.gbSat1.Controls.Add(this.tbSat1RAAN);
            this.gbSat1.Controls.Add(this.tbSat1AoP);
            this.gbSat1.Controls.Add(this.tbSat1Inclination);
            this.gbSat1.Controls.Add(this.tbSat1Eccentricity);
            this.gbSat1.Controls.Add(this.tbSat1SemiMajorAxis);
            this.gbSat1.Location = new System.Drawing.Point(17, 20);
            this.gbSat1.Name = "gbSat1";
            this.gbSat1.Size = new System.Drawing.Size(172, 282);
            this.gbSat1.TabIndex = 14;
            this.gbSat1.TabStop = false;
            this.gbSat1.Text = "Sat1";
            // 
            // btnSat1del
            // 
            this.btnSat1del.Location = new System.Drawing.Point(85, 246);
            this.btnSat1del.Name = "btnSat1del";
            this.btnSat1del.Size = new System.Drawing.Size(75, 23);
            this.btnSat1del.TabIndex = 15;
            this.btnSat1del.Text = "Delete";
            this.btnSat1del.UseVisualStyleBackColor = true;
            this.btnSat1del.Click += new System.EventHandler(this.btnSat1del_Click);
            // 
            // lblSat1TrueAnomaly
            // 
            this.lblSat1TrueAnomaly.AutoSize = true;
            this.lblSat1TrueAnomaly.Location = new System.Drawing.Point(83, 151);
            this.lblSat1TrueAnomaly.Name = "lblSat1TrueAnomaly";
            this.lblSat1TrueAnomaly.Size = new System.Drawing.Size(72, 13);
            this.lblSat1TrueAnomaly.TabIndex = 14;
            this.lblSat1TrueAnomaly.Text = "True Anomaly";
            // 
            // lblSat1RAAN
            // 
            this.lblSat1RAAN.AutoSize = true;
            this.lblSat1RAAN.Location = new System.Drawing.Point(83, 125);
            this.lblSat1RAAN.Name = "lblSat1RAAN";
            this.lblSat1RAAN.Size = new System.Drawing.Size(37, 13);
            this.lblSat1RAAN.TabIndex = 13;
            this.lblSat1RAAN.Text = "RAAN";
            // 
            // lblSat1AoP
            // 
            this.lblSat1AoP.AutoSize = true;
            this.lblSat1AoP.Location = new System.Drawing.Point(83, 99);
            this.lblSat1AoP.Name = "lblSat1AoP";
            this.lblSat1AoP.Size = new System.Drawing.Size(74, 13);
            this.lblSat1AoP.TabIndex = 12;
            this.lblSat1AoP.Text = "Arg of Perigee";
            // 
            // lblSat1Inclination
            // 
            this.lblSat1Inclination.AutoSize = true;
            this.lblSat1Inclination.Location = new System.Drawing.Point(83, 73);
            this.lblSat1Inclination.Name = "lblSat1Inclination";
            this.lblSat1Inclination.Size = new System.Drawing.Size(55, 13);
            this.lblSat1Inclination.TabIndex = 11;
            this.lblSat1Inclination.Text = "Inclination";
            // 
            // lblSat1Eccentricity
            // 
            this.lblSat1Eccentricity.AutoSize = true;
            this.lblSat1Eccentricity.Location = new System.Drawing.Point(83, 47);
            this.lblSat1Eccentricity.Name = "lblSat1Eccentricity";
            this.lblSat1Eccentricity.Size = new System.Drawing.Size(62, 13);
            this.lblSat1Eccentricity.TabIndex = 10;
            this.lblSat1Eccentricity.Text = "Eccentricity";
            // 
            // lblSat1SemiMajorAxis
            // 
            this.lblSat1SemiMajorAxis.AutoSize = true;
            this.lblSat1SemiMajorAxis.Location = new System.Drawing.Point(83, 21);
            this.lblSat1SemiMajorAxis.Name = "lblSat1SemiMajorAxis";
            this.lblSat1SemiMajorAxis.Size = new System.Drawing.Size(77, 13);
            this.lblSat1SemiMajorAxis.TabIndex = 9;
            this.lblSat1SemiMajorAxis.Text = "Semimajor Axis";
            // 
            // btnSat1Prop
            // 
            this.btnSat1Prop.Location = new System.Drawing.Point(85, 219);
            this.btnSat1Prop.Name = "btnSat1Prop";
            this.btnSat1Prop.Size = new System.Drawing.Size(75, 23);
            this.btnSat1Prop.TabIndex = 8;
            this.btnSat1Prop.Text = "Propagate";
            this.btnSat1Prop.UseVisualStyleBackColor = true;
            this.btnSat1Prop.Click += new System.EventHandler(this.btnSat1Prop_Click);
            // 
            // rbSat1J2
            // 
            this.rbSat1J2.AutoSize = true;
            this.rbSat1J2.Location = new System.Drawing.Point(7, 246);
            this.rbSat1J2.Name = "rbSat1J2";
            this.rbSat1J2.Size = new System.Drawing.Size(36, 17);
            this.rbSat1J2.TabIndex = 7;
            this.rbSat1J2.Text = "J2";
            this.rbSat1J2.UseVisualStyleBackColor = true;
            // 
            // rbSat1TwoBody
            // 
            this.rbSat1TwoBody.AutoSize = true;
            this.rbSat1TwoBody.Checked = true;
            this.rbSat1TwoBody.Location = new System.Drawing.Point(7, 223);
            this.rbSat1TwoBody.Name = "rbSat1TwoBody";
            this.rbSat1TwoBody.Size = new System.Drawing.Size(73, 17);
            this.rbSat1TwoBody.TabIndex = 6;
            this.rbSat1TwoBody.TabStop = true;
            this.rbSat1TwoBody.Text = "Two Body";
            this.rbSat1TwoBody.UseVisualStyleBackColor = true;
            // 
            // tbSat1TrueAnomaly
            // 
            this.tbSat1TrueAnomaly.Location = new System.Drawing.Point(7, 148);
            this.tbSat1TrueAnomaly.Name = "tbSat1TrueAnomaly";
            this.tbSat1TrueAnomaly.Size = new System.Drawing.Size(70, 20);
            this.tbSat1TrueAnomaly.TabIndex = 5;
            this.tbSat1TrueAnomaly.Text = "0.01";
            this.tbSat1TrueAnomaly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSat1RAAN
            // 
            this.tbSat1RAAN.Location = new System.Drawing.Point(7, 122);
            this.tbSat1RAAN.Name = "tbSat1RAAN";
            this.tbSat1RAAN.Size = new System.Drawing.Size(70, 20);
            this.tbSat1RAAN.TabIndex = 4;
            this.tbSat1RAAN.Text = "0.0";
            this.tbSat1RAAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSat1AoP
            // 
            this.tbSat1AoP.Location = new System.Drawing.Point(7, 96);
            this.tbSat1AoP.Name = "tbSat1AoP";
            this.tbSat1AoP.Size = new System.Drawing.Size(70, 20);
            this.tbSat1AoP.TabIndex = 3;
            this.tbSat1AoP.Text = "0.0";
            this.tbSat1AoP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSat1Inclination
            // 
            this.tbSat1Inclination.Location = new System.Drawing.Point(7, 70);
            this.tbSat1Inclination.Name = "tbSat1Inclination";
            this.tbSat1Inclination.Size = new System.Drawing.Size(70, 20);
            this.tbSat1Inclination.TabIndex = 2;
            this.tbSat1Inclination.Text = "28.5";
            this.tbSat1Inclination.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSat1Eccentricity
            // 
            this.tbSat1Eccentricity.Location = new System.Drawing.Point(7, 44);
            this.tbSat1Eccentricity.Name = "tbSat1Eccentricity";
            this.tbSat1Eccentricity.Size = new System.Drawing.Size(70, 20);
            this.tbSat1Eccentricity.TabIndex = 1;
            this.tbSat1Eccentricity.Text = "0.001";
            this.tbSat1Eccentricity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSat1SemiMajorAxis
            // 
            this.tbSat1SemiMajorAxis.Location = new System.Drawing.Point(7, 18);
            this.tbSat1SemiMajorAxis.Name = "tbSat1SemiMajorAxis";
            this.tbSat1SemiMajorAxis.Size = new System.Drawing.Size(70, 20);
            this.tbSat1SemiMajorAxis.TabIndex = 0;
            this.tbSat1SemiMajorAxis.Text = "7000";
            this.tbSat1SemiMajorAxis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gbSat2
            // 
            this.gbSat2.Controls.Add(this.btnCopySat1);
            this.gbSat2.Controls.Add(this.btnSat2Del);
            this.gbSat2.Controls.Add(this.lblSat2TrueAnomaly);
            this.gbSat2.Controls.Add(this.lblSat2RAAN);
            this.gbSat2.Controls.Add(this.lblSat2AoP);
            this.gbSat2.Controls.Add(this.lblSat2Inclination);
            this.gbSat2.Controls.Add(this.lblSat2Eccentricity);
            this.gbSat2.Controls.Add(this.lblSat2SemiMajorAxis);
            this.gbSat2.Controls.Add(this.btnSat2Prop);
            this.gbSat2.Controls.Add(this.rbSat2J2);
            this.gbSat2.Controls.Add(this.rbSat2TwoBody);
            this.gbSat2.Controls.Add(this.tbSat2TrueAnomaly);
            this.gbSat2.Controls.Add(this.tbSat2RAAN);
            this.gbSat2.Controls.Add(this.tbSat2AoP);
            this.gbSat2.Controls.Add(this.tbSat2Inclination);
            this.gbSat2.Controls.Add(this.tbSat2Eccentricity);
            this.gbSat2.Controls.Add(this.tbSat2SemiMajorAxis);
            this.gbSat2.Location = new System.Drawing.Point(195, 20);
            this.gbSat2.Name = "gbSat2";
            this.gbSat2.Size = new System.Drawing.Size(172, 282);
            this.gbSat2.TabIndex = 16;
            this.gbSat2.TabStop = false;
            this.gbSat2.Text = "Sat2";
            // 
            // btnSat2Del
            // 
            this.btnSat2Del.Location = new System.Drawing.Point(86, 246);
            this.btnSat2Del.Name = "btnSat2Del";
            this.btnSat2Del.Size = new System.Drawing.Size(75, 23);
            this.btnSat2Del.TabIndex = 15;
            this.btnSat2Del.Text = "Delete";
            this.btnSat2Del.UseVisualStyleBackColor = true;
            this.btnSat2Del.Click += new System.EventHandler(this.btnSat2Del_Click);
            // 
            // lblSat2TrueAnomaly
            // 
            this.lblSat2TrueAnomaly.AutoSize = true;
            this.lblSat2TrueAnomaly.Location = new System.Drawing.Point(83, 151);
            this.lblSat2TrueAnomaly.Name = "lblSat2TrueAnomaly";
            this.lblSat2TrueAnomaly.Size = new System.Drawing.Size(72, 13);
            this.lblSat2TrueAnomaly.TabIndex = 14;
            this.lblSat2TrueAnomaly.Text = "True Anomaly";
            // 
            // lblSat2RAAN
            // 
            this.lblSat2RAAN.AutoSize = true;
            this.lblSat2RAAN.Location = new System.Drawing.Point(83, 125);
            this.lblSat2RAAN.Name = "lblSat2RAAN";
            this.lblSat2RAAN.Size = new System.Drawing.Size(37, 13);
            this.lblSat2RAAN.TabIndex = 13;
            this.lblSat2RAAN.Text = "RAAN";
            // 
            // lblSat2AoP
            // 
            this.lblSat2AoP.AutoSize = true;
            this.lblSat2AoP.Location = new System.Drawing.Point(83, 99);
            this.lblSat2AoP.Name = "lblSat2AoP";
            this.lblSat2AoP.Size = new System.Drawing.Size(74, 13);
            this.lblSat2AoP.TabIndex = 12;
            this.lblSat2AoP.Text = "Arg of Perigee";
            // 
            // lblSat2Inclination
            // 
            this.lblSat2Inclination.AutoSize = true;
            this.lblSat2Inclination.Location = new System.Drawing.Point(83, 73);
            this.lblSat2Inclination.Name = "lblSat2Inclination";
            this.lblSat2Inclination.Size = new System.Drawing.Size(55, 13);
            this.lblSat2Inclination.TabIndex = 11;
            this.lblSat2Inclination.Text = "Inclination";
            // 
            // lblSat2Eccentricity
            // 
            this.lblSat2Eccentricity.AutoSize = true;
            this.lblSat2Eccentricity.Location = new System.Drawing.Point(83, 47);
            this.lblSat2Eccentricity.Name = "lblSat2Eccentricity";
            this.lblSat2Eccentricity.Size = new System.Drawing.Size(62, 13);
            this.lblSat2Eccentricity.TabIndex = 10;
            this.lblSat2Eccentricity.Text = "Eccentricity";
            // 
            // lblSat2SemiMajorAxis
            // 
            this.lblSat2SemiMajorAxis.AutoSize = true;
            this.lblSat2SemiMajorAxis.Location = new System.Drawing.Point(83, 21);
            this.lblSat2SemiMajorAxis.Name = "lblSat2SemiMajorAxis";
            this.lblSat2SemiMajorAxis.Size = new System.Drawing.Size(81, 13);
            this.lblSat2SemiMajorAxis.TabIndex = 9;
            this.lblSat2SemiMajorAxis.Text = "Semi Major Axis";
            // 
            // btnSat2Prop
            // 
            this.btnSat2Prop.Location = new System.Drawing.Point(86, 217);
            this.btnSat2Prop.Name = "btnSat2Prop";
            this.btnSat2Prop.Size = new System.Drawing.Size(75, 23);
            this.btnSat2Prop.TabIndex = 8;
            this.btnSat2Prop.Text = "Propagate";
            this.btnSat2Prop.UseVisualStyleBackColor = true;
            this.btnSat2Prop.Click += new System.EventHandler(this.btnSat2Prop_Click);
            // 
            // rbSat2J2
            // 
            this.rbSat2J2.AutoSize = true;
            this.rbSat2J2.Checked = true;
            this.rbSat2J2.Location = new System.Drawing.Point(7, 246);
            this.rbSat2J2.Name = "rbSat2J2";
            this.rbSat2J2.Size = new System.Drawing.Size(36, 17);
            this.rbSat2J2.TabIndex = 7;
            this.rbSat2J2.TabStop = true;
            this.rbSat2J2.Text = "J2";
            this.rbSat2J2.UseVisualStyleBackColor = true;
            // 
            // rbSat2TwoBody
            // 
            this.rbSat2TwoBody.AutoSize = true;
            this.rbSat2TwoBody.Location = new System.Drawing.Point(7, 220);
            this.rbSat2TwoBody.Name = "rbSat2TwoBody";
            this.rbSat2TwoBody.Size = new System.Drawing.Size(73, 17);
            this.rbSat2TwoBody.TabIndex = 6;
            this.rbSat2TwoBody.Text = "Two Body";
            this.rbSat2TwoBody.UseVisualStyleBackColor = true;
            // 
            // tbSat2TrueAnomaly
            // 
            this.tbSat2TrueAnomaly.Location = new System.Drawing.Point(7, 148);
            this.tbSat2TrueAnomaly.Name = "tbSat2TrueAnomaly";
            this.tbSat2TrueAnomaly.Size = new System.Drawing.Size(70, 20);
            this.tbSat2TrueAnomaly.TabIndex = 5;
            this.tbSat2TrueAnomaly.Tag = "";
            this.tbSat2TrueAnomaly.Text = "0.01";
            this.tbSat2TrueAnomaly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSat2RAAN
            // 
            this.tbSat2RAAN.Location = new System.Drawing.Point(7, 122);
            this.tbSat2RAAN.Name = "tbSat2RAAN";
            this.tbSat2RAAN.Size = new System.Drawing.Size(70, 20);
            this.tbSat2RAAN.TabIndex = 4;
            this.tbSat2RAAN.Text = "0.0";
            this.tbSat2RAAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSat2AoP
            // 
            this.tbSat2AoP.Location = new System.Drawing.Point(7, 96);
            this.tbSat2AoP.Name = "tbSat2AoP";
            this.tbSat2AoP.Size = new System.Drawing.Size(70, 20);
            this.tbSat2AoP.TabIndex = 3;
            this.tbSat2AoP.Text = "0.0";
            this.tbSat2AoP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSat2Inclination
            // 
            this.tbSat2Inclination.Location = new System.Drawing.Point(7, 70);
            this.tbSat2Inclination.Name = "tbSat2Inclination";
            this.tbSat2Inclination.Size = new System.Drawing.Size(70, 20);
            this.tbSat2Inclination.TabIndex = 2;
            this.tbSat2Inclination.Text = "28.5";
            this.tbSat2Inclination.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSat2Eccentricity
            // 
            this.tbSat2Eccentricity.Location = new System.Drawing.Point(7, 44);
            this.tbSat2Eccentricity.Name = "tbSat2Eccentricity";
            this.tbSat2Eccentricity.Size = new System.Drawing.Size(70, 20);
            this.tbSat2Eccentricity.TabIndex = 1;
            this.tbSat2Eccentricity.Text = "0.001";
            this.tbSat2Eccentricity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbSat2SemiMajorAxis
            // 
            this.tbSat2SemiMajorAxis.Location = new System.Drawing.Point(7, 18);
            this.tbSat2SemiMajorAxis.Name = "tbSat2SemiMajorAxis";
            this.tbSat2SemiMajorAxis.Size = new System.Drawing.Size(70, 20);
            this.tbSat2SemiMajorAxis.TabIndex = 0;
            this.tbSat2SemiMajorAxis.Text = "7000";
            this.tbSat2SemiMajorAxis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbOrbitPlane
            // 
            this.cbOrbitPlane.AutoSize = true;
            this.cbOrbitPlane.Location = new System.Drawing.Point(6, 19);
            this.cbOrbitPlane.Name = "cbOrbitPlane";
            this.cbOrbitPlane.Size = new System.Drawing.Size(108, 17);
            this.cbOrbitPlane.TabIndex = 17;
            this.cbOrbitPlane.Text = "Show Orbit Plane";
            this.cbOrbitPlane.UseVisualStyleBackColor = true;
            this.cbOrbitPlane.CheckedChanged += new System.EventHandler(this.cbOrbitPlane_CheckedChanged);
            // 
            // cbInclination
            // 
            this.cbInclination.AutoSize = true;
            this.cbInclination.Location = new System.Drawing.Point(6, 44);
            this.cbInclination.Name = "cbInclination";
            this.cbInclination.Size = new System.Drawing.Size(104, 17);
            this.cbInclination.TabIndex = 18;
            this.cbInclination.Text = "Show Inclination";
            this.cbInclination.UseVisualStyleBackColor = true;
            this.cbInclination.CheckedChanged += new System.EventHandler(this.cbInclination_CheckedChanged);
            // 
            // cbFixedAxes
            // 
            this.cbFixedAxes.AutoSize = true;
            this.cbFixedAxes.Location = new System.Drawing.Point(6, 67);
            this.cbFixedAxes.Name = "cbFixedAxes";
            this.cbFixedAxes.Size = new System.Drawing.Size(107, 17);
            this.cbFixedAxes.TabIndex = 20;
            this.cbFixedAxes.Text = "Show Fixed Axes";
            this.cbFixedAxes.UseVisualStyleBackColor = true;
            this.cbFixedAxes.CheckedChanged += new System.EventHandler(this.cbFixedAxes_CheckedChanged);
            // 
            // cbInertialAxes
            // 
            this.cbInertialAxes.AutoSize = true;
            this.cbInertialAxes.Location = new System.Drawing.Point(6, 42);
            this.cbInertialAxes.Name = "cbInertialAxes";
            this.cbInertialAxes.Size = new System.Drawing.Size(113, 17);
            this.cbInertialAxes.TabIndex = 19;
            this.cbInertialAxes.Text = "Show Inertial Axes";
            this.cbInertialAxes.UseVisualStyleBackColor = true;
            this.cbInertialAxes.CheckedChanged += new System.EventHandler(this.cbInertialAxes_CheckedChanged);
            // 
            // cbEquatorialPlane
            // 
            this.cbEquatorialPlane.AutoSize = true;
            this.cbEquatorialPlane.Location = new System.Drawing.Point(6, 19);
            this.cbEquatorialPlane.Name = "cbEquatorialPlane";
            this.cbEquatorialPlane.Size = new System.Drawing.Size(133, 17);
            this.cbEquatorialPlane.TabIndex = 21;
            this.cbEquatorialPlane.Text = "Show Equatorial Plane";
            this.cbEquatorialPlane.UseVisualStyleBackColor = true;
            this.cbEquatorialPlane.CheckedChanged += new System.EventHandler(this.cbEquatorialPlane_CheckedChanged);
            // 
            // cbRaan
            // 
            this.cbRaan.AutoSize = true;
            this.cbRaan.Location = new System.Drawing.Point(6, 90);
            this.cbRaan.Name = "cbRaan";
            this.cbRaan.Size = new System.Drawing.Size(86, 17);
            this.cbRaan.TabIndex = 22;
            this.cbRaan.Text = "Show RAAN";
            this.cbRaan.UseVisualStyleBackColor = true;
            this.cbRaan.CheckedChanged += new System.EventHandler(this.cbRaan_CheckedChanged);
            // 
            // cbTrueAnomaly
            // 
            this.cbTrueAnomaly.AutoSize = true;
            this.cbTrueAnomaly.Location = new System.Drawing.Point(6, 113);
            this.cbTrueAnomaly.Name = "cbTrueAnomaly";
            this.cbTrueAnomaly.Size = new System.Drawing.Size(121, 17);
            this.cbTrueAnomaly.TabIndex = 23;
            this.cbTrueAnomaly.Text = "Show True Anomaly";
            this.cbTrueAnomaly.UseVisualStyleBackColor = true;
            this.cbTrueAnomaly.CheckedChanged += new System.EventHandler(this.cbTrueAnomaly_CheckedChanged);
            // 
            // cbAoP
            // 
            this.cbAoP.AutoSize = true;
            this.cbAoP.Location = new System.Drawing.Point(6, 67);
            this.cbAoP.Name = "cbAoP";
            this.cbAoP.Size = new System.Drawing.Size(123, 17);
            this.cbAoP.TabIndex = 24;
            this.cbAoP.Text = "Show Arg of Perigee";
            this.cbAoP.UseVisualStyleBackColor = true;
            this.cbAoP.CheckedChanged += new System.EventHandler(this.cbAoP_CheckedChanged);
            // 
            // gbEarth
            // 
            this.gbEarth.Controls.Add(this.cbSunVector);
            this.gbEarth.Controls.Add(this.cbEquatorialPlane);
            this.gbEarth.Controls.Add(this.cbInertialAxes);
            this.gbEarth.Controls.Add(this.cbFixedAxes);
            this.gbEarth.Location = new System.Drawing.Point(373, 23);
            this.gbEarth.Name = "gbEarth";
            this.gbEarth.Size = new System.Drawing.Size(155, 113);
            this.gbEarth.TabIndex = 25;
            this.gbEarth.TabStop = false;
            this.gbEarth.Text = "Earth";
            // 
            // cbSunVector
            // 
            this.cbSunVector.AutoSize = true;
            this.cbSunVector.Location = new System.Drawing.Point(6, 90);
            this.cbSunVector.Name = "cbSunVector";
            this.cbSunVector.Size = new System.Drawing.Size(109, 17);
            this.cbSunVector.TabIndex = 22;
            this.cbSunVector.Text = "Show Sun Vector";
            this.cbSunVector.UseVisualStyleBackColor = true;
            this.cbSunVector.CheckedChanged += new System.EventHandler(this.cbSunVector_CheckedChanged);
            // 
            // gbOrbit
            // 
            this.gbOrbit.Controls.Add(this.cbOrbitPlane);
            this.gbOrbit.Controls.Add(this.cbInclination);
            this.gbOrbit.Controls.Add(this.cbAoP);
            this.gbOrbit.Controls.Add(this.cbRaan);
            this.gbOrbit.Controls.Add(this.cbTrueAnomaly);
            this.gbOrbit.Location = new System.Drawing.Point(373, 142);
            this.gbOrbit.Name = "gbOrbit";
            this.gbOrbit.Size = new System.Drawing.Size(155, 160);
            this.gbOrbit.TabIndex = 26;
            this.gbOrbit.TabStop = false;
            this.gbOrbit.Text = "Orbit";
            // 
            // btnCopySat2
            // 
            this.btnCopySat2.Location = new System.Drawing.Point(7, 179);
            this.btnCopySat2.Name = "btnCopySat2";
            this.btnCopySat2.Size = new System.Drawing.Size(154, 23);
            this.btnCopySat2.TabIndex = 16;
            this.btnCopySat2.Text = "Copy from Sat2";
            this.btnCopySat2.UseVisualStyleBackColor = true;
            this.btnCopySat2.Click += new System.EventHandler(this.btnCopySat2_Click);
            // 
            // btnCopySat1
            // 
            this.btnCopySat1.Location = new System.Drawing.Point(7, 179);
            this.btnCopySat1.Name = "btnCopySat1";
            this.btnCopySat1.Size = new System.Drawing.Size(154, 23);
            this.btnCopySat1.TabIndex = 17;
            this.btnCopySat1.Text = "Copy from Sat1";
            this.btnCopySat1.UseVisualStyleBackColor = true;
            this.btnCopySat1.Click += new System.EventHandler(this.btnCopySat1_Click);
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOrbit);
            this.Controls.Add(this.gbEarth);
            this.Controls.Add(this.gbSat2);
            this.Controls.Add(this.gbSat1);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(541, 327);
            this.gbSat1.ResumeLayout(false);
            this.gbSat1.PerformLayout();
            this.gbSat2.ResumeLayout(false);
            this.gbSat2.PerformLayout();
            this.gbEarth.ResumeLayout(false);
            this.gbEarth.PerformLayout();
            this.gbOrbit.ResumeLayout(false);
            this.gbOrbit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbSat1;
        internal System.Windows.Forms.Button btnSat1del;
        internal System.Windows.Forms.Label lblSat1TrueAnomaly;
        internal System.Windows.Forms.Label lblSat1RAAN;
        internal System.Windows.Forms.Label lblSat1AoP;
        internal System.Windows.Forms.Label lblSat1Inclination;
        internal System.Windows.Forms.Label lblSat1Eccentricity;
        internal System.Windows.Forms.Label lblSat1SemiMajorAxis;
        internal System.Windows.Forms.Button btnSat1Prop;
        internal System.Windows.Forms.RadioButton rbSat1J2;
        internal System.Windows.Forms.RadioButton rbSat1TwoBody;
        internal System.Windows.Forms.TextBox tbSat1TrueAnomaly;
        internal System.Windows.Forms.TextBox tbSat1RAAN;
        internal System.Windows.Forms.TextBox tbSat1AoP;
        internal System.Windows.Forms.TextBox tbSat1Inclination;
        internal System.Windows.Forms.TextBox tbSat1Eccentricity;
        internal System.Windows.Forms.TextBox tbSat1SemiMajorAxis;
        internal System.Windows.Forms.GroupBox gbSat2;
        internal System.Windows.Forms.Button btnSat2Del;
        internal System.Windows.Forms.Label lblSat2TrueAnomaly;
        internal System.Windows.Forms.Label lblSat2RAAN;
        internal System.Windows.Forms.Label lblSat2AoP;
        internal System.Windows.Forms.Label lblSat2Inclination;
        internal System.Windows.Forms.Label lblSat2Eccentricity;
        internal System.Windows.Forms.Label lblSat2SemiMajorAxis;
        internal System.Windows.Forms.Button btnSat2Prop;
        internal System.Windows.Forms.RadioButton rbSat2J2;
        internal System.Windows.Forms.RadioButton rbSat2TwoBody;
        internal System.Windows.Forms.TextBox tbSat2TrueAnomaly;
        internal System.Windows.Forms.TextBox tbSat2RAAN;
        internal System.Windows.Forms.TextBox tbSat2AoP;
        internal System.Windows.Forms.TextBox tbSat2Inclination;
        internal System.Windows.Forms.TextBox tbSat2Eccentricity;
        internal System.Windows.Forms.TextBox tbSat2SemiMajorAxis;
        private System.Windows.Forms.CheckBox cbOrbitPlane;
        private System.Windows.Forms.CheckBox cbInclination;
        private System.Windows.Forms.CheckBox cbFixedAxes;
        private System.Windows.Forms.CheckBox cbInertialAxes;
        private System.Windows.Forms.CheckBox cbEquatorialPlane;
        private System.Windows.Forms.CheckBox cbRaan;
        private System.Windows.Forms.CheckBox cbTrueAnomaly;
        private System.Windows.Forms.CheckBox cbAoP;
        private System.Windows.Forms.GroupBox gbEarth;
        private System.Windows.Forms.CheckBox cbSunVector;
        private System.Windows.Forms.GroupBox gbOrbit;
        internal System.Windows.Forms.Button btnCopySat2;
        internal System.Windows.Forms.Button btnCopySat1;
    }
}
