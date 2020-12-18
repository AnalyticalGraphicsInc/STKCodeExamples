namespace OperatorsToolbox.FacilityCreator
{
    partial class CadanceDefinitionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadanceDefinitionForm));
            this.CadanceList = new System.Windows.Forms.ListView();
            this.FormName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Trackers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DeleteFacility = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.AddFacility = new System.Windows.Forms.Button();
            this.FacilityName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SensorType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Altitude = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Longitude = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Latitude = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CadanceName = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NumRadar = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.NumOptical = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.DefaultConstraints = new System.Windows.Forms.CheckBox();
            this.DefineConstraints = new System.Windows.Forms.Button();
            this.Duplicate = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.ColorSelection = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CadanceList
            // 
            this.CadanceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CadanceList.BackColor = System.Drawing.Color.DimGray;
            this.CadanceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FormName,
            this.Type,
            this.Trackers,
            this.columnHeader1,
            this.columnHeader2});
            this.CadanceList.ForeColor = System.Drawing.Color.White;
            this.CadanceList.FullRowSelect = true;
            this.CadanceList.HideSelection = false;
            this.CadanceList.Location = new System.Drawing.Point(8, 32);
            this.CadanceList.Margin = new System.Windows.Forms.Padding(2);
            this.CadanceList.MultiSelect = false;
            this.CadanceList.Name = "CadanceList";
            this.CadanceList.Size = new System.Drawing.Size(606, 176);
            this.CadanceList.TabIndex = 42;
            this.CadanceList.UseCompatibleStateImageBehavior = false;
            this.CadanceList.View = System.Windows.Forms.View.Details;
            this.CadanceList.SelectedIndexChanged += new System.EventHandler(this.CadanceList_SelectedIndexChanged);
            // 
            // FormName
            // 
            this.FormName.Text = "Name";
            this.FormName.Width = 200;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 100;
            // 
            // Trackers
            // 
            this.Trackers.Text = "Latitude [deg]";
            this.Trackers.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Longitude [deg]";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Altitude [km]";
            this.columnHeader2.Width = 100;
            // 
            // DeleteFacility
            // 
            this.DeleteFacility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteFacility.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteFacility.ForeColor = System.Drawing.SystemColors.Desktop;
            this.DeleteFacility.ImageIndex = 1;
            this.DeleteFacility.ImageList = this.imageList1;
            this.DeleteFacility.Location = new System.Drawing.Point(97, 213);
            this.DeleteFacility.Margin = new System.Windows.Forms.Padding(2);
            this.DeleteFacility.Name = "DeleteFacility";
            this.DeleteFacility.Size = new System.Drawing.Size(40, 40);
            this.DeleteFacility.TabIndex = 45;
            this.DeleteFacility.UseVisualStyleBackColor = true;
            this.DeleteFacility.Click += new System.EventHandler(this.DeleteFacility_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "paint-brush.png");
            this.imageList1.Images.SetKeyName(1, "delete.png");
            this.imageList1.Images.SetKeyName(2, "x-mark.png");
            this.imageList1.Images.SetKeyName(3, "add.png");
            this.imageList1.Images.SetKeyName(4, "duplicate.png");
            // 
            // AddFacility
            // 
            this.AddFacility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddFacility.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddFacility.ForeColor = System.Drawing.SystemColors.Desktop;
            this.AddFacility.ImageIndex = 3;
            this.AddFacility.ImageList = this.imageList1;
            this.AddFacility.Location = new System.Drawing.Point(9, 213);
            this.AddFacility.Margin = new System.Windows.Forms.Padding(2);
            this.AddFacility.Name = "AddFacility";
            this.AddFacility.Size = new System.Drawing.Size(40, 40);
            this.AddFacility.TabIndex = 43;
            this.AddFacility.UseVisualStyleBackColor = true;
            this.AddFacility.Click += new System.EventHandler(this.AddFacility_Click);
            // 
            // FacilityName
            // 
            this.FacilityName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FacilityName.BackColor = System.Drawing.Color.DimGray;
            this.FacilityName.ForeColor = System.Drawing.Color.White;
            this.FacilityName.Location = new System.Drawing.Point(712, 55);
            this.FacilityName.Margin = new System.Windows.Forms.Padding(2);
            this.FacilityName.Name = "FacilityName";
            this.FacilityName.Size = new System.Drawing.Size(102, 20);
            this.FacilityName.TabIndex = 62;
            this.FacilityName.TextChanged += new System.EventHandler(this.FacilityName_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(625, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 61;
            this.label3.Text = "Facility Name:";
            // 
            // SensorType
            // 
            this.SensorType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SensorType.BackColor = System.Drawing.Color.DimGray;
            this.SensorType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SensorType.ForeColor = System.Drawing.Color.White;
            this.SensorType.FormattingEnabled = true;
            this.SensorType.Location = new System.Drawing.Point(712, 82);
            this.SensorType.Margin = new System.Windows.Forms.Padding(2);
            this.SensorType.Name = "SensorType";
            this.SensorType.Size = new System.Drawing.Size(102, 21);
            this.SensorType.TabIndex = 60;
            this.SensorType.SelectedIndexChanged += new System.EventHandler(this.SensorType_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(789, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 74;
            this.label7.Text = "km";
            // 
            // Altitude
            // 
            this.Altitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Altitude.BackColor = System.Drawing.Color.DimGray;
            this.Altitude.ForeColor = System.Drawing.Color.White;
            this.Altitude.Location = new System.Drawing.Point(712, 154);
            this.Altitude.Margin = new System.Windows.Forms.Padding(2);
            this.Altitude.Name = "Altitude";
            this.Altitude.Size = new System.Drawing.Size(74, 20);
            this.Altitude.TabIndex = 73;
            this.Altitude.TextChanged += new System.EventHandler(this.Altitude_TextChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(625, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 72;
            this.label8.Text = "Altitude:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(789, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 71;
            this.label5.Text = "deg";
            // 
            // Longitude
            // 
            this.Longitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Longitude.BackColor = System.Drawing.Color.DimGray;
            this.Longitude.ForeColor = System.Drawing.Color.White;
            this.Longitude.Location = new System.Drawing.Point(712, 131);
            this.Longitude.Margin = new System.Windows.Forms.Padding(2);
            this.Longitude.Name = "Longitude";
            this.Longitude.Size = new System.Drawing.Size(74, 20);
            this.Longitude.TabIndex = 70;
            this.Longitude.TextChanged += new System.EventHandler(this.Longitude_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(625, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 69;
            this.label6.Text = "Longitude:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(789, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 68;
            this.label4.Text = "deg";
            // 
            // Latitude
            // 
            this.Latitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Latitude.BackColor = System.Drawing.Color.DimGray;
            this.Latitude.ForeColor = System.Drawing.Color.White;
            this.Latitude.Location = new System.Drawing.Point(712, 108);
            this.Latitude.Margin = new System.Windows.Forms.Padding(2);
            this.Latitude.Name = "Latitude";
            this.Latitude.Size = new System.Drawing.Size(74, 20);
            this.Latitude.TabIndex = 65;
            this.Latitude.TextChanged += new System.EventHandler(this.Latitude_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(625, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Latitude:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(625, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Facility Type:";
            // 
            // CadanceName
            // 
            this.CadanceName.BackColor = System.Drawing.Color.DimGray;
            this.CadanceName.ForeColor = System.Drawing.Color.White;
            this.CadanceName.Location = new System.Drawing.Point(108, 10);
            this.CadanceName.Margin = new System.Windows.Forms.Padding(2);
            this.CadanceName.Name = "CadanceName";
            this.CadanceName.Size = new System.Drawing.Size(132, 20);
            this.CadanceName.TabIndex = 66;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.ForeColor = System.Drawing.Color.White;
            this.NameLabel.Location = new System.Drawing.Point(6, 11);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(84, 13);
            this.NameLabel.TabIndex = 67;
            this.NameLabel.Text = "Cadence Name:";
            // 
            // NumRadar
            // 
            this.NumRadar.BackColor = System.Drawing.Color.DimGray;
            this.NumRadar.ForeColor = System.Drawing.Color.White;
            this.NumRadar.Location = new System.Drawing.Point(300, 11);
            this.NumRadar.Margin = new System.Windows.Forms.Padding(2);
            this.NumRadar.Name = "NumRadar";
            this.NumRadar.ReadOnly = true;
            this.NumRadar.Size = new System.Drawing.Size(53, 20);
            this.NumRadar.TabIndex = 76;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(246, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 75;
            this.label9.Text = "# Radars:";
            // 
            // NumOptical
            // 
            this.NumOptical.BackColor = System.Drawing.Color.DimGray;
            this.NumOptical.ForeColor = System.Drawing.Color.White;
            this.NumOptical.Location = new System.Drawing.Point(412, 11);
            this.NumOptical.Margin = new System.Windows.Forms.Padding(2);
            this.NumOptical.Name = "NumOptical";
            this.NumOptical.ReadOnly = true;
            this.NumOptical.Size = new System.Drawing.Size(53, 20);
            this.NumOptical.TabIndex = 78;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(358, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 77;
            this.label10.Text = "# Optical:";
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Save.BackColor = System.Drawing.Color.SteelBlue;
            this.Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Save.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Save.Location = new System.Drawing.Point(9, 268);
            this.Save.Margin = new System.Windows.Forms.Padding(2);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(96, 32);
            this.Save.TabIndex = 79;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // DefaultConstraints
            // 
            this.DefaultConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultConstraints.AutoSize = true;
            this.DefaultConstraints.ForeColor = System.Drawing.Color.White;
            this.DefaultConstraints.Location = new System.Drawing.Point(621, 196);
            this.DefaultConstraints.Margin = new System.Windows.Forms.Padding(2);
            this.DefaultConstraints.Name = "DefaultConstraints";
            this.DefaultConstraints.Size = new System.Drawing.Size(115, 17);
            this.DefaultConstraints.TabIndex = 82;
            this.DefaultConstraints.Text = "Default Constraints";
            this.DefaultConstraints.UseVisualStyleBackColor = true;
            this.DefaultConstraints.CheckedChanged += new System.EventHandler(this.DefaultConstraints_CheckedChanged);
            // 
            // DefineConstraints
            // 
            this.DefineConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DefineConstraints.BackColor = System.Drawing.Color.SteelBlue;
            this.DefineConstraints.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DefineConstraints.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DefineConstraints.Location = new System.Drawing.Point(737, 186);
            this.DefineConstraints.Margin = new System.Windows.Forms.Padding(2);
            this.DefineConstraints.Name = "DefineConstraints";
            this.DefineConstraints.Size = new System.Drawing.Size(82, 35);
            this.DefineConstraints.TabIndex = 81;
            this.DefineConstraints.Text = "Define Constraints";
            this.DefineConstraints.UseVisualStyleBackColor = false;
            this.DefineConstraints.Click += new System.EventHandler(this.DefineConstraints_Click);
            // 
            // Duplicate
            // 
            this.Duplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Duplicate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Duplicate.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Duplicate.ImageIndex = 4;
            this.Duplicate.ImageList = this.imageList1;
            this.Duplicate.Location = new System.Drawing.Point(53, 212);
            this.Duplicate.Margin = new System.Windows.Forms.Padding(2);
            this.Duplicate.Name = "Duplicate";
            this.Duplicate.Size = new System.Drawing.Size(40, 40);
            this.Duplicate.TabIndex = 83;
            this.Duplicate.UseVisualStyleBackColor = true;
            this.Duplicate.Click += new System.EventHandler(this.Duplicate_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(432, 213);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 15);
            this.label11.TabIndex = 85;
            this.label11.Text = "Color:";
            // 
            // ColorSelection
            // 
            this.ColorSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorSelection.BackColor = System.Drawing.Color.DimGray;
            this.ColorSelection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ColorSelection.ForeColor = System.Drawing.Color.White;
            this.ColorSelection.FormattingEnabled = true;
            this.ColorSelection.Location = new System.Drawing.Point(476, 212);
            this.ColorSelection.Margin = new System.Windows.Forms.Padding(2);
            this.ColorSelection.Name = "ColorSelection";
            this.ColorSelection.Size = new System.Drawing.Size(138, 21);
            this.ColorSelection.TabIndex = 84;
            // 
            // CadanceDefinitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(823, 311);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ColorSelection);
            this.Controls.Add(this.Duplicate);
            this.Controls.Add(this.DefaultConstraints);
            this.Controls.Add(this.DefineConstraints);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.NumOptical);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.NumRadar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.FacilityName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SensorType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Altitude);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Longitude);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Latitude);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CadanceName);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.CadanceList);
            this.Controls.Add(this.DeleteFacility);
            this.Controls.Add(this.AddFacility);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CadanceDefinitionForm";
            this.Text = "Cadence Definition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView CadanceList;
        private System.Windows.Forms.ColumnHeader FormName;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Trackers;
        private System.Windows.Forms.Button DeleteFacility;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button AddFacility;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox FacilityName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SensorType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Altitude;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Longitude;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Latitude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CadanceName;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox NumRadar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox NumOptical;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.CheckBox DefaultConstraints;
        private System.Windows.Forms.Button DefineConstraints;
        private System.Windows.Forms.Button Duplicate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ColorSelection;
    }
}