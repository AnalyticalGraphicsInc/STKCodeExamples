namespace CameraControlAutomator
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
            this.lblStkObjects = new System.Windows.Forms.Label();
            this.cbStkObjects = new System.Windows.Forms.ComboBox();
            this.NewPath = new System.Windows.Forms.RadioButton();
            this.AddToPath = new System.Windows.Forms.RadioButton();
            this.AddPathName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PathName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.FOVValue = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.StopDeg = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.StartDeg = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Rotation2Axis = new System.Windows.Forms.ComboBox();
            this.Rotation2Deg = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Rotation1Axis = new System.Windows.Forms.ComboBox();
            this.Rotation1Deg = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MinorAxisLength = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MajorAxisLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.UseCurrentTime = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.NumPoints = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.Duration = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.StartTime = new System.Windows.Forms.TextBox();
            this.Create = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.ZOffset = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.PostRotation = new System.Windows.Forms.RadioButton();
            this.PreRotation = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStkObjects
            // 
            this.lblStkObjects.AutoSize = true;
            this.lblStkObjects.Location = new System.Drawing.Point(19, 113);
            this.lblStkObjects.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStkObjects.Name = "lblStkObjects";
            this.lblStkObjects.Size = new System.Drawing.Size(102, 17);
            this.lblStkObjects.TabIndex = 1;
            this.lblStkObjects.Text = "Central Object:";
            this.lblStkObjects.Click += new System.EventHandler(this.lblStkObjects_Click);
            // 
            // cbStkObjects
            // 
            this.cbStkObjects.FormattingEnabled = true;
            this.cbStkObjects.Location = new System.Drawing.Point(129, 110);
            this.cbStkObjects.Margin = new System.Windows.Forms.Padding(4);
            this.cbStkObjects.Name = "cbStkObjects";
            this.cbStkObjects.Size = new System.Drawing.Size(258, 24);
            this.cbStkObjects.TabIndex = 2;
            this.cbStkObjects.SelectedIndexChanged += new System.EventHandler(this.cbStkObjects_SelectedIndexChanged);
            // 
            // NewPath
            // 
            this.NewPath.AutoSize = true;
            this.NewPath.Location = new System.Drawing.Point(18, 26);
            this.NewPath.Name = "NewPath";
            this.NewPath.Size = new System.Drawing.Size(104, 21);
            this.NewPath.TabIndex = 3;
            this.NewPath.TabStop = true;
            this.NewPath.Text = "Create Path";
            this.NewPath.UseVisualStyleBackColor = true;
            this.NewPath.CheckedChanged += new System.EventHandler(this.NewPath_CheckedChanged);
            // 
            // AddToPath
            // 
            this.AddToPath.AutoSize = true;
            this.AddToPath.Location = new System.Drawing.Point(18, 67);
            this.AddToPath.Name = "AddToPath";
            this.AddToPath.Size = new System.Drawing.Size(103, 21);
            this.AddToPath.TabIndex = 5;
            this.AddToPath.TabStop = true;
            this.AddToPath.Text = "Add to Path";
            this.AddToPath.UseVisualStyleBackColor = true;
            this.AddToPath.CheckedChanged += new System.EventHandler(this.AddToPath_CheckedChanged);
            // 
            // AddPathName
            // 
            this.AddPathName.FormattingEnabled = true;
            this.AddPathName.Location = new System.Drawing.Point(129, 67);
            this.AddPathName.Margin = new System.Windows.Forms.Padding(4);
            this.AddPathName.Name = "AddPathName";
            this.AddPathName.Size = new System.Drawing.Size(258, 24);
            this.AddPathName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Path Name:";
            // 
            // PathName
            // 
            this.PathName.Location = new System.Drawing.Point(233, 26);
            this.PathName.Name = "PathName";
            this.PathName.Size = new System.Drawing.Size(154, 22);
            this.PathName.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.FOVValue);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.cbStkObjects);
            this.groupBox1.Controls.Add(this.PathName);
            this.groupBox1.Controls.Add(this.lblStkObjects);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.NewPath);
            this.groupBox1.Controls.Add(this.AddPathName);
            this.groupBox1.Controls.Add(this.AddToPath);
            this.groupBox1.Location = new System.Drawing.Point(10, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 184);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(290, 152);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(32, 17);
            this.label20.TabIndex = 29;
            this.label20.Text = "deg";
            // 
            // FOVValue
            // 
            this.FOVValue.Location = new System.Drawing.Point(129, 149);
            this.FOVValue.Name = "FOVValue";
            this.FOVValue.Size = new System.Drawing.Size(154, 22);
            this.FOVValue.TabIndex = 10;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(81, 149);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 17);
            this.label19.TabIndex = 9;
            this.label19.Text = "FOV:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PreRotation);
            this.groupBox2.Controls.Add(this.PostRotation);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.ZOffset);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.StopDeg);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.StartDeg);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.Rotation2Axis);
            this.groupBox2.Controls.Add(this.Rotation2Deg);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.Rotation1Axis);
            this.groupBox2.Controls.Add(this.Rotation1Deg);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.MinorAxisLength);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.MajorAxisLength);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(10, 197);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 232);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Path Definition";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(240, 113);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(131, 17);
            this.label14.TabIndex = 28;
            this.label14.Text = "*Relative to +X-Axis";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(208, 127);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 17);
            this.label12.TabIndex = 27;
            this.label12.Text = "deg";
            // 
            // StopDeg
            // 
            this.StopDeg.Location = new System.Drawing.Point(129, 124);
            this.StopDeg.Name = "StopDeg";
            this.StopDeg.Size = new System.Drawing.Size(72, 22);
            this.StopDeg.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 124);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 17);
            this.label13.TabIndex = 25;
            this.label13.Text = "Stop Angle:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(208, 96);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "deg";
            // 
            // StartDeg
            // 
            this.StartDeg.Location = new System.Drawing.Point(129, 93);
            this.StartDeg.Name = "StartDeg";
            this.StartDeg.Size = new System.Drawing.Size(72, 22);
            this.StartDeg.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 93);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 17);
            this.label11.TabIndex = 22;
            this.label11.Text = "Start Angle:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(339, 191);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 17);
            this.label8.TabIndex = 21;
            this.label8.Text = "deg";
            // 
            // Rotation2Axis
            // 
            this.Rotation2Axis.FormattingEnabled = true;
            this.Rotation2Axis.Location = new System.Drawing.Point(91, 186);
            this.Rotation2Axis.Margin = new System.Windows.Forms.Padding(4);
            this.Rotation2Axis.Name = "Rotation2Axis";
            this.Rotation2Axis.Size = new System.Drawing.Size(132, 24);
            this.Rotation2Axis.TabIndex = 18;
            this.Rotation2Axis.SelectedIndexChanged += new System.EventHandler(this.Rotation2Axis_SelectedIndexChanged);
            // 
            // Rotation2Deg
            // 
            this.Rotation2Deg.Location = new System.Drawing.Point(230, 186);
            this.Rotation2Deg.Name = "Rotation2Deg";
            this.Rotation2Deg.Size = new System.Drawing.Size(102, 22);
            this.Rotation2Deg.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 189);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "Rotation 2:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(339, 163);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "deg";
            // 
            // Rotation1Axis
            // 
            this.Rotation1Axis.FormattingEnabled = true;
            this.Rotation1Axis.Location = new System.Drawing.Point(91, 158);
            this.Rotation1Axis.Margin = new System.Windows.Forms.Padding(4);
            this.Rotation1Axis.Name = "Rotation1Axis";
            this.Rotation1Axis.Size = new System.Drawing.Size(132, 24);
            this.Rotation1Axis.TabIndex = 9;
            this.Rotation1Axis.SelectedIndexChanged += new System.EventHandler(this.Rotation1Axis_SelectedIndexChanged);
            // 
            // Rotation1Deg
            // 
            this.Rotation1Deg.Location = new System.Drawing.Point(230, 158);
            this.Rotation1Deg.Name = "Rotation1Deg";
            this.Rotation1Deg.Size = new System.Drawing.Size(102, 22);
            this.Rotation1Deg.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 161);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Rotation 1:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 64);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "m";
            // 
            // MinorAxisLength
            // 
            this.MinorAxisLength.Location = new System.Drawing.Point(129, 62);
            this.MinorAxisLength.Name = "MinorAxisLength";
            this.MinorAxisLength.Size = new System.Drawing.Size(72, 22);
            this.MinorAxisLength.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 62);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Y-Axis Length:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "m";
            // 
            // MajorAxisLength
            // 
            this.MajorAxisLength.Location = new System.Drawing.Point(129, 34);
            this.MajorAxisLength.Name = "MajorAxisLength";
            this.MajorAxisLength.Size = new System.Drawing.Size(72, 22);
            this.MajorAxisLength.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "X-Axis Length:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.UseCurrentTime);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.NumPoints);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.Duration);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.StartTime);
            this.groupBox3.Location = new System.Drawing.Point(10, 438);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(411, 128);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Time Options";
            // 
            // UseCurrentTime
            // 
            this.UseCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UseCurrentTime.Location = new System.Drawing.Point(304, 22);
            this.UseCurrentTime.Name = "UseCurrentTime";
            this.UseCurrentTime.Size = new System.Drawing.Size(101, 31);
            this.UseCurrentTime.TabIndex = 13;
            this.UseCurrentTime.Text = "Use Current Time";
            this.UseCurrentTime.UseVisualStyleBackColor = true;
            this.UseCurrentTime.Click += new System.EventHandler(this.UseCurrentTime_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 84);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(121, 17);
            this.label18.TabIndex = 35;
            this.label18.Text = "Number of Points:";
            // 
            // NumPoints
            // 
            this.NumPoints.Location = new System.Drawing.Point(139, 84);
            this.NumPoints.Name = "NumPoints";
            this.NumPoints.Size = new System.Drawing.Size(86, 22);
            this.NumPoints.TabIndex = 36;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(231, 59);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 17);
            this.label15.TabIndex = 34;
            this.label15.Text = "sec";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 56);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 17);
            this.label17.TabIndex = 32;
            this.label17.Text = "Duration:";
            // 
            // Duration
            // 
            this.Duration.Location = new System.Drawing.Point(139, 56);
            this.Duration.Name = "Duration";
            this.Duration.Size = new System.Drawing.Size(86, 22);
            this.Duration.TabIndex = 33;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 28);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 17);
            this.label16.TabIndex = 29;
            this.label16.Text = "Start Time:";
            // 
            // StartTime
            // 
            this.StartTime.Location = new System.Drawing.Point(139, 26);
            this.StartTime.Name = "StartTime";
            this.StartTime.Size = new System.Drawing.Size(87, 22);
            this.StartTime.TabIndex = 30;
            // 
            // Create
            // 
            this.Create.Location = new System.Drawing.Point(139, 572);
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(120, 51);
            this.Create.TabIndex = 12;
            this.Create.Text = "Create";
            this.Create.UseVisualStyleBackColor = true;
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(231, 28);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(49, 17);
            this.label21.TabIndex = 37;
            this.label21.Text = "EpSec";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(381, 38);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(19, 17);
            this.label22.TabIndex = 31;
            this.label22.Text = "m";
            // 
            // ZOffset
            // 
            this.ZOffset.Location = new System.Drawing.Point(306, 36);
            this.ZOffset.Name = "ZOffset";
            this.ZOffset.Size = new System.Drawing.Size(72, 22);
            this.ZOffset.TabIndex = 30;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(240, 38);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(63, 17);
            this.label23.TabIndex = 29;
            this.label23.Text = "Z Offset:";
            // 
            // PostRotation
            // 
            this.PostRotation.AutoSize = true;
            this.PostRotation.Location = new System.Drawing.Point(293, 64);
            this.PostRotation.Name = "PostRotation";
            this.PostRotation.Size = new System.Drawing.Size(115, 21);
            this.PostRotation.TabIndex = 30;
            this.PostRotation.TabStop = true;
            this.PostRotation.Text = "Post-Rotation";
            this.PostRotation.UseVisualStyleBackColor = true;
            this.PostRotation.CheckedChanged += new System.EventHandler(this.PostRotation_CheckedChanged);
            // 
            // PreRotation
            // 
            this.PreRotation.AutoSize = true;
            this.PreRotation.Location = new System.Drawing.Point(293, 89);
            this.PreRotation.Name = "PreRotation";
            this.PreRotation.Size = new System.Drawing.Size(109, 21);
            this.PreRotation.TabIndex = 32;
            this.PreRotation.TabStop = true;
            this.PreRotation.Text = "Pre-Rotation";
            this.PreRotation.UseVisualStyleBackColor = true;
            this.PreRotation.CheckedChanged += new System.EventHandler(this.PreRotation_CheckedChanged);
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Create);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(434, 640);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblStkObjects;
        private System.Windows.Forms.ComboBox cbStkObjects;
        private System.Windows.Forms.RadioButton NewPath;
        private System.Windows.Forms.RadioButton AddToPath;
        private System.Windows.Forms.ComboBox AddPathName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PathName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MinorAxisLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MajorAxisLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox StopDeg;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox StartDeg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox Rotation2Axis;
        private System.Windows.Forms.TextBox Rotation2Deg;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Rotation1Axis;
        private System.Windows.Forms.TextBox Rotation1Deg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox FOVValue;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox NumPoints;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox Duration;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox StartTime;
        private System.Windows.Forms.Button Create;
        private System.Windows.Forms.Button UseCurrentTime;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox ZOffset;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RadioButton PreRotation;
        private System.Windows.Forms.RadioButton PostRotation;
    }
}
