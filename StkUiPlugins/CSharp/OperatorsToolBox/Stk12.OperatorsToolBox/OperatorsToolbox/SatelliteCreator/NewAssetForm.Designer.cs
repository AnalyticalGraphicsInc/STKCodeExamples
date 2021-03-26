namespace OperatorsToolbox.SatelliteCreator
{
    partial class NewAssetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAssetForm));
            this.TCEFile = new System.Windows.Forms.TextBox();
            this.Browse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.SlipTime = new System.Windows.Forms.TextBox();
            this.TimeSlip = new System.Windows.Forms.CheckBox();
            this.ElementsBox = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.NameValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TAValue = new System.Windows.Forms.TextBox();
            this.AoPValue = new System.Windows.Forms.TextBox();
            this.RAANValue = new System.Windows.Forms.TextBox();
            this.IncValue = new System.Windows.Forms.TextBox();
            this.EccValue = new System.Windows.Forms.TextBox();
            this.SMAValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Generate = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ConstType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CoordSystem = new System.Windows.Forms.ComboBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.ConstName = new System.Windows.Forms.TextBox();
            this.ExistingConst = new System.Windows.Forms.ComboBox();
            this.DatabaseBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SccTextBox = new System.Windows.Forms.TextBox();
            this.filterButton3 = new System.Windows.Forms.Button();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.filterButton2 = new System.Windows.Forms.Button();
            this.filterButton1 = new System.Windows.Forms.Button();
            this.SensorToggle = new System.Windows.Forms.CheckBox();
            this.UnselectAll = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.Unselect = new System.Windows.Forms.Button();
            this.SelectAll = new System.Windows.Forms.Button();
            this.Select = new System.Windows.Forms.Button();
            this.SatelliteList = new System.Windows.Forms.ListView();
            this.SSC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CommonName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Constellation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImportType = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.WaitTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.ElementsBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.DatabaseBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TCEFile
            // 
            this.TCEFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TCEFile.BackColor = System.Drawing.Color.DimGray;
            this.TCEFile.ForeColor = System.Drawing.Color.White;
            this.TCEFile.Location = new System.Drawing.Point(8, 24);
            this.TCEFile.Margin = new System.Windows.Forms.Padding(2);
            this.TCEFile.Name = "TCEFile";
            this.TCEFile.Size = new System.Drawing.Size(283, 20);
            this.TCEFile.TabIndex = 2;
            // 
            // Browse
            // 
            this.Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Browse.BackColor = System.Drawing.Color.SteelBlue;
            this.Browse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Browse.Location = new System.Drawing.Point(295, 25);
            this.Browse.Margin = new System.Windows.Forms.Padding(2);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(25, 20);
            this.Browse.TabIndex = 3;
            this.Browse.Text = "...";
            this.Browse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Browse.UseVisualStyleBackColor = false;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TimeLabel);
            this.groupBox1.Controls.Add(this.SlipTime);
            this.groupBox1.Controls.Add(this.TimeSlip);
            this.groupBox1.Controls.Add(this.Browse);
            this.groupBox1.Controls.Add(this.TCEFile);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(2, 68);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(338, 89);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "From File";
            // 
            // TimeLabel
            // 
            this.TimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(291, 56);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(37, 13);
            this.TimeLabel.TabIndex = 8;
            this.TimeLabel.Text = "UTCG";
            // 
            // SlipTime
            // 
            this.SlipTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SlipTime.BackColor = System.Drawing.Color.DimGray;
            this.SlipTime.ForeColor = System.Drawing.Color.White;
            this.SlipTime.Location = new System.Drawing.Point(79, 54);
            this.SlipTime.Margin = new System.Windows.Forms.Padding(2);
            this.SlipTime.Name = "SlipTime";
            this.SlipTime.Size = new System.Drawing.Size(212, 20);
            this.SlipTime.TabIndex = 14;
            // 
            // TimeSlip
            // 
            this.TimeSlip.AutoSize = true;
            this.TimeSlip.Location = new System.Drawing.Point(10, 55);
            this.TimeSlip.Margin = new System.Windows.Forms.Padding(2);
            this.TimeSlip.Name = "TimeSlip";
            this.TimeSlip.Size = new System.Drawing.Size(69, 17);
            this.TimeSlip.TabIndex = 4;
            this.TimeSlip.Text = "Time Slip";
            this.TimeSlip.UseVisualStyleBackColor = true;
            this.TimeSlip.CheckedChanged += new System.EventHandler(this.TimeSlip_CheckedChanged);
            // 
            // ElementsBox
            // 
            this.ElementsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ElementsBox.Controls.Add(this.label17);
            this.ElementsBox.Controls.Add(this.label16);
            this.ElementsBox.Controls.Add(this.label15);
            this.ElementsBox.Controls.Add(this.label14);
            this.ElementsBox.Controls.Add(this.label12);
            this.ElementsBox.Controls.Add(this.NameValue);
            this.ElementsBox.Controls.Add(this.label7);
            this.ElementsBox.Controls.Add(this.TAValue);
            this.ElementsBox.Controls.Add(this.AoPValue);
            this.ElementsBox.Controls.Add(this.RAANValue);
            this.ElementsBox.Controls.Add(this.IncValue);
            this.ElementsBox.Controls.Add(this.EccValue);
            this.ElementsBox.Controls.Add(this.SMAValue);
            this.ElementsBox.Controls.Add(this.label6);
            this.ElementsBox.Controls.Add(this.label5);
            this.ElementsBox.Controls.Add(this.label4);
            this.ElementsBox.Controls.Add(this.label3);
            this.ElementsBox.Controls.Add(this.label2);
            this.ElementsBox.Controls.Add(this.label1);
            this.ElementsBox.ForeColor = System.Drawing.Color.White;
            this.ElementsBox.Location = new System.Drawing.Point(1, 68);
            this.ElementsBox.Margin = new System.Windows.Forms.Padding(2);
            this.ElementsBox.Name = "ElementsBox";
            this.ElementsBox.Padding = new System.Windows.Forms.Padding(2);
            this.ElementsBox.Size = new System.Drawing.Size(339, 271);
            this.ElementsBox.TabIndex = 5;
            this.ElementsBox.TabStop = false;
            this.ElementsBox.Text = "Orbital Elements";
            this.ElementsBox.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(214, 210);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(25, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "deg";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(214, 172);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "deg";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(214, 137);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 17;
            this.label15.Text = "deg";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(214, 103);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "deg";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(214, 35);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "km";
            // 
            // NameValue
            // 
            this.NameValue.BackColor = System.Drawing.Color.DimGray;
            this.NameValue.ForeColor = System.Drawing.Color.White;
            this.NameValue.Location = new System.Drawing.Point(115, 240);
            this.NameValue.Margin = new System.Windows.Forms.Padding(2);
            this.NameValue.Name = "NameValue";
            this.NameValue.Size = new System.Drawing.Size(138, 20);
            this.NameValue.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 242);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Satellite Name:";
            // 
            // TAValue
            // 
            this.TAValue.BackColor = System.Drawing.Color.DimGray;
            this.TAValue.ForeColor = System.Drawing.Color.White;
            this.TAValue.Location = new System.Drawing.Point(115, 207);
            this.TAValue.Margin = new System.Windows.Forms.Padding(2);
            this.TAValue.Name = "TAValue";
            this.TAValue.Size = new System.Drawing.Size(98, 20);
            this.TAValue.TabIndex = 11;
            // 
            // AoPValue
            // 
            this.AoPValue.BackColor = System.Drawing.Color.DimGray;
            this.AoPValue.ForeColor = System.Drawing.Color.White;
            this.AoPValue.Location = new System.Drawing.Point(115, 170);
            this.AoPValue.Margin = new System.Windows.Forms.Padding(2);
            this.AoPValue.Name = "AoPValue";
            this.AoPValue.Size = new System.Drawing.Size(98, 20);
            this.AoPValue.TabIndex = 10;
            // 
            // RAANValue
            // 
            this.RAANValue.BackColor = System.Drawing.Color.DimGray;
            this.RAANValue.ForeColor = System.Drawing.Color.White;
            this.RAANValue.Location = new System.Drawing.Point(115, 136);
            this.RAANValue.Margin = new System.Windows.Forms.Padding(2);
            this.RAANValue.Name = "RAANValue";
            this.RAANValue.Size = new System.Drawing.Size(98, 20);
            this.RAANValue.TabIndex = 9;
            // 
            // IncValue
            // 
            this.IncValue.BackColor = System.Drawing.Color.DimGray;
            this.IncValue.ForeColor = System.Drawing.Color.White;
            this.IncValue.Location = new System.Drawing.Point(115, 102);
            this.IncValue.Margin = new System.Windows.Forms.Padding(2);
            this.IncValue.Name = "IncValue";
            this.IncValue.Size = new System.Drawing.Size(98, 20);
            this.IncValue.TabIndex = 8;
            // 
            // EccValue
            // 
            this.EccValue.BackColor = System.Drawing.Color.DimGray;
            this.EccValue.ForeColor = System.Drawing.Color.White;
            this.EccValue.Location = new System.Drawing.Point(115, 67);
            this.EccValue.Margin = new System.Windows.Forms.Padding(2);
            this.EccValue.Name = "EccValue";
            this.EccValue.Size = new System.Drawing.Size(98, 20);
            this.EccValue.TabIndex = 7;
            // 
            // SMAValue
            // 
            this.SMAValue.BackColor = System.Drawing.Color.DimGray;
            this.SMAValue.ForeColor = System.Drawing.Color.White;
            this.SMAValue.Location = new System.Drawing.Point(115, 32);
            this.SMAValue.Margin = new System.Windows.Forms.Padding(2);
            this.SMAValue.Name = "SMAValue";
            this.SMAValue.Size = new System.Drawing.Size(98, 20);
            this.SMAValue.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 210);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Mean Anomaly:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 174);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Argument of Perigee:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 136);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "RAAN:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Inclination:  ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Eccentricity: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Semi-Major Axis: ";
            // 
            // Generate
            // 
            this.Generate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Generate.BackColor = System.Drawing.Color.SteelBlue;
            this.Generate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Generate.ForeColor = System.Drawing.Color.White;
            this.Generate.Location = new System.Drawing.Point(81, 615);
            this.Generate.Margin = new System.Windows.Forms.Padding(2);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(163, 30);
            this.Generate.TabIndex = 6;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = false;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ForeColor = System.Drawing.Color.White;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.imageList1;
            this.Cancel.Location = new System.Drawing.Point(324, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 7;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "x-mark.png");
            this.imageList1.Images.SetKeyName(1, "delete.png");
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.ConstType);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.CoordSystem);
            this.groupBox3.Controls.Add(this.NameLabel);
            this.groupBox3.Controls.Add(this.ConstName);
            this.groupBox3.Controls.Add(this.ExistingConst);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(2, 498);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(343, 113);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Constellation Options";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 20);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Definition:";
            // 
            // ConstType
            // 
            this.ConstType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstType.BackColor = System.Drawing.Color.DimGray;
            this.ConstType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConstType.ForeColor = System.Drawing.Color.White;
            this.ConstType.FormattingEnabled = true;
            this.ConstType.Location = new System.Drawing.Point(189, 17);
            this.ConstType.Margin = new System.Windows.Forms.Padding(2);
            this.ConstType.Name = "ConstType";
            this.ConstType.Size = new System.Drawing.Size(146, 21);
            this.ConstType.TabIndex = 32;
            this.ConstType.SelectedIndexChanged += new System.EventHandler(this.ConstType_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 74);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Coordinate Representation:";
            // 
            // CoordSystem
            // 
            this.CoordSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CoordSystem.BackColor = System.Drawing.Color.DimGray;
            this.CoordSystem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CoordSystem.ForeColor = System.Drawing.Color.White;
            this.CoordSystem.FormattingEnabled = true;
            this.CoordSystem.Location = new System.Drawing.Point(189, 72);
            this.CoordSystem.Margin = new System.Windows.Forms.Padding(2);
            this.CoordSystem.Name = "CoordSystem";
            this.CoordSystem.Size = new System.Drawing.Size(146, 21);
            this.CoordSystem.TabIndex = 6;
            this.CoordSystem.SelectedIndexChanged += new System.EventHandler(this.CoordSystem_SelectedIndexChanged);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(5, 46);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(101, 13);
            this.NameLabel.TabIndex = 5;
            this.NameLabel.Text = "Constellation Name:";
            // 
            // ConstName
            // 
            this.ConstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstName.BackColor = System.Drawing.Color.DimGray;
            this.ConstName.ForeColor = System.Drawing.Color.White;
            this.ConstName.Location = new System.Drawing.Point(189, 44);
            this.ConstName.Margin = new System.Windows.Forms.Padding(2);
            this.ConstName.Name = "ConstName";
            this.ConstName.Size = new System.Drawing.Size(146, 20);
            this.ConstName.TabIndex = 4;
            // 
            // ExistingConst
            // 
            this.ExistingConst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExistingConst.BackColor = System.Drawing.Color.DimGray;
            this.ExistingConst.ForeColor = System.Drawing.Color.White;
            this.ExistingConst.FormattingEnabled = true;
            this.ExistingConst.Location = new System.Drawing.Point(189, 44);
            this.ExistingConst.Margin = new System.Windows.Forms.Padding(2);
            this.ExistingConst.Name = "ExistingConst";
            this.ExistingConst.Size = new System.Drawing.Size(146, 21);
            this.ExistingConst.TabIndex = 3;
            // 
            // DatabaseBox
            // 
            this.DatabaseBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DatabaseBox.Controls.Add(this.label8);
            this.DatabaseBox.Controls.Add(this.SccTextBox);
            this.DatabaseBox.Controls.Add(this.filterButton3);
            this.DatabaseBox.Controls.Add(this.filterButton2);
            this.DatabaseBox.Controls.Add(this.filterButton1);
            this.DatabaseBox.Controls.Add(this.SensorToggle);
            this.DatabaseBox.Controls.Add(this.UnselectAll);
            this.DatabaseBox.Controls.Add(this.Unselect);
            this.DatabaseBox.Controls.Add(this.SelectAll);
            this.DatabaseBox.Controls.Add(this.Select);
            this.DatabaseBox.Controls.Add(this.SatelliteList);
            this.DatabaseBox.ForeColor = System.Drawing.Color.White;
            this.DatabaseBox.Location = new System.Drawing.Point(1, 63);
            this.DatabaseBox.Margin = new System.Windows.Forms.Padding(2);
            this.DatabaseBox.Name = "DatabaseBox";
            this.DatabaseBox.Padding = new System.Windows.Forms.Padding(2);
            this.DatabaseBox.Size = new System.Drawing.Size(343, 431);
            this.DatabaseBox.TabIndex = 5;
            this.DatabaseBox.TabStop = false;
            this.DatabaseBox.Text = "Database Selection";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(168, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "SCC:";
            // 
            // SccTextBox
            // 
            this.SccTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SccTextBox.BackColor = System.Drawing.Color.DimGray;
            this.SccTextBox.ForeColor = System.Drawing.Color.White;
            this.SccTextBox.Location = new System.Drawing.Point(202, 27);
            this.SccTextBox.Name = "SccTextBox";
            this.SccTextBox.Size = new System.Drawing.Size(132, 20);
            this.SccTextBox.TabIndex = 36;
            this.SccTextBox.TextChanged += new System.EventHandler(this.SccTextBox_TextChanged);
            // 
            // filterButton3
            // 
            this.filterButton3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.filterButton3.ImageIndex = 1;
            this.filterButton3.ImageList = this.imageList3;
            this.filterButton3.Location = new System.Drawing.Point(115, 18);
            this.filterButton3.Name = "filterButton3";
            this.filterButton3.Size = new System.Drawing.Size(34, 34);
            this.filterButton3.TabIndex = 35;
            this.filterButton3.UseVisualStyleBackColor = true;
            this.filterButton3.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "funnel (1).png");
            this.imageList3.Images.SetKeyName(1, "filter (1).png");
            // 
            // filterButton2
            // 
            this.filterButton2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.filterButton2.ImageIndex = 1;
            this.filterButton2.ImageList = this.imageList3;
            this.filterButton2.Location = new System.Drawing.Point(61, 18);
            this.filterButton2.Name = "filterButton2";
            this.filterButton2.Size = new System.Drawing.Size(34, 34);
            this.filterButton2.TabIndex = 34;
            this.filterButton2.UseVisualStyleBackColor = true;
            this.filterButton2.Click += new System.EventHandler(this.filterButton2_Click);
            // 
            // filterButton1
            // 
            this.filterButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.filterButton1.ImageIndex = 1;
            this.filterButton1.ImageList = this.imageList3;
            this.filterButton1.Location = new System.Drawing.Point(9, 18);
            this.filterButton1.Name = "filterButton1";
            this.filterButton1.Size = new System.Drawing.Size(34, 34);
            this.filterButton1.TabIndex = 33;
            this.filterButton1.UseVisualStyleBackColor = true;
            this.filterButton1.Click += new System.EventHandler(this.filterButton1_Click);
            // 
            // SensorToggle
            // 
            this.SensorToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SensorToggle.AutoSize = true;
            this.SensorToggle.Location = new System.Drawing.Point(14, 362);
            this.SensorToggle.Margin = new System.Windows.Forms.Padding(2);
            this.SensorToggle.Name = "SensorToggle";
            this.SensorToggle.Size = new System.Drawing.Size(172, 17);
            this.SensorToggle.TabIndex = 32;
            this.SensorToggle.Text = "Generate Sensor if Applicabale";
            this.SensorToggle.UseVisualStyleBackColor = true;
            // 
            // UnselectAll
            // 
            this.UnselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UnselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnselectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnselectAll.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.UnselectAll.ImageIndex = 0;
            this.UnselectAll.ImageList = this.imageList2;
            this.UnselectAll.Location = new System.Drawing.Point(294, 386);
            this.UnselectAll.Margin = new System.Windows.Forms.Padding(2);
            this.UnselectAll.Name = "UnselectAll";
            this.UnselectAll.Size = new System.Drawing.Size(40, 40);
            this.UnselectAll.TabIndex = 7;
            this.UnselectAll.Text = "ALL";
            this.UnselectAll.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.UnselectAll.UseVisualStyleBackColor = true;
            this.UnselectAll.Click += new System.EventHandler(this.UnselectAll_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "delete.png");
            this.imageList2.Images.SetKeyName(1, "checked.png");
            this.imageList2.Images.SetKeyName(2, "filter (1).png");
            this.imageList2.Images.SetKeyName(3, "filter (2).png");
            // 
            // Unselect
            // 
            this.Unselect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Unselect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Unselect.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Unselect.ImageIndex = 0;
            this.Unselect.ImageList = this.imageList2;
            this.Unselect.Location = new System.Drawing.Point(230, 386);
            this.Unselect.Margin = new System.Windows.Forms.Padding(2);
            this.Unselect.Name = "Unselect";
            this.Unselect.Size = new System.Drawing.Size(40, 40);
            this.Unselect.TabIndex = 6;
            this.Unselect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Unselect.UseVisualStyleBackColor = true;
            this.Unselect.Click += new System.EventHandler(this.Unselect_Click);
            // 
            // SelectAll
            // 
            this.SelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAll.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SelectAll.ImageIndex = 1;
            this.SelectAll.ImageList = this.imageList2;
            this.SelectAll.Location = new System.Drawing.Point(74, 386);
            this.SelectAll.Margin = new System.Windows.Forms.Padding(2);
            this.SelectAll.Name = "SelectAll";
            this.SelectAll.Size = new System.Drawing.Size(40, 40);
            this.SelectAll.TabIndex = 5;
            this.SelectAll.Text = "ALL";
            this.SelectAll.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SelectAll.UseVisualStyleBackColor = true;
            this.SelectAll.Click += new System.EventHandler(this.SelectAll_Click);
            // 
            // Select
            // 
            this.Select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Select.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Select.ImageIndex = 1;
            this.Select.ImageList = this.imageList2;
            this.Select.Location = new System.Drawing.Point(11, 386);
            this.Select.Margin = new System.Windows.Forms.Padding(2);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(40, 40);
            this.Select.TabIndex = 4;
            this.Select.UseVisualStyleBackColor = true;
            this.Select.Click += new System.EventHandler(this.Select_Click);
            // 
            // SatelliteList
            // 
            this.SatelliteList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SatelliteList.BackColor = System.Drawing.Color.DimGray;
            this.SatelliteList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SSC,
            this.CommonName,
            this.Constellation});
            this.SatelliteList.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SatelliteList.ForeColor = System.Drawing.Color.White;
            this.SatelliteList.FullRowSelect = true;
            this.SatelliteList.HideSelection = false;
            this.SatelliteList.Location = new System.Drawing.Point(4, 61);
            this.SatelliteList.Margin = new System.Windows.Forms.Padding(2);
            this.SatelliteList.Name = "SatelliteList";
            this.SatelliteList.Size = new System.Drawing.Size(335, 294);
            this.SatelliteList.TabIndex = 3;
            this.SatelliteList.UseCompatibleStateImageBehavior = false;
            this.SatelliteList.View = System.Windows.Forms.View.Details;
            // 
            // SSC
            // 
            this.SSC.Text = "SCC";
            this.SSC.Width = 65;
            // 
            // CommonName
            // 
            this.CommonName.Text = "Common Name";
            this.CommonName.Width = 137;
            // 
            // Constellation
            // 
            this.Constellation.Text = "Constellaton";
            this.Constellation.Width = 127;
            // 
            // ImportType
            // 
            this.ImportType.BackColor = System.Drawing.Color.DimGray;
            this.ImportType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ImportType.ForeColor = System.Drawing.Color.White;
            this.ImportType.FormattingEnabled = true;
            this.ImportType.Location = new System.Drawing.Point(2, 38);
            this.ImportType.Margin = new System.Windows.Forms.Padding(2);
            this.ImportType.Name = "ImportType";
            this.ImportType.Size = new System.Drawing.Size(157, 21);
            this.ImportType.TabIndex = 8;
            this.ImportType.SelectedIndexChanged += new System.EventHandler(this.ImportType_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Firebrick;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(159, 23);
            this.label13.TabIndex = 31;
            this.label13.Text = "Satellite Creator";
            // 
            // WaitTimer
            // 
            this.WaitTimer.Interval = 500;
            this.WaitTimer.Tick += new System.EventHandler(this.WaitTimer_Tick);
            // 
            // NewAssetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.DatabaseBox);
            this.Controls.Add(this.ElementsBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ImportType);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "NewAssetForm";
            this.Size = new System.Drawing.Size(350, 700);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ElementsBox.ResumeLayout(false);
            this.ElementsBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.DatabaseBox.ResumeLayout(false);
            this.DatabaseBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TCEFile;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox ElementsBox;
        private System.Windows.Forms.TextBox TAValue;
        private System.Windows.Forms.TextBox AoPValue;
        private System.Windows.Forms.TextBox RAANValue;
        private System.Windows.Forms.TextBox IncValue;
        private System.Windows.Forms.TextBox EccValue;
        private System.Windows.Forms.TextBox SMAValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TextBox NameValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox ConstName;
        private System.Windows.Forms.ComboBox ExistingConst;
        private System.Windows.Forms.GroupBox DatabaseBox;
        private System.Windows.Forms.Button UnselectAll;
        private System.Windows.Forms.Button Unselect;
        private System.Windows.Forms.Button SelectAll;
        private System.Windows.Forms.Button Select;
        private System.Windows.Forms.ListView SatelliteList;
        private System.Windows.Forms.ColumnHeader SSC;
        private System.Windows.Forms.ColumnHeader CommonName;
        private System.Windows.Forms.ColumnHeader Constellation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox CoordSystem;
        private System.Windows.Forms.ComboBox ImportType;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ConstType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Timer WaitTimer;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.TextBox SlipTime;
        private System.Windows.Forms.CheckBox TimeSlip;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox SensorToggle;
        private System.Windows.Forms.Button filterButton2;
        private System.Windows.Forms.Button filterButton1;
        private System.Windows.Forms.Button filterButton3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox SccTextBox;
        private System.Windows.Forms.ImageList imageList3;
    }
}