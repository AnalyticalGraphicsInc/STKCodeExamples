namespace OperatorsToolbox.FacilityCreator
{
    partial class FacilityCreatorPlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacilityCreatorPlugin));
            this.DeleteCadance = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.EditCadance = new System.Windows.Forms.Button();
            this.AddCadance = new System.Windows.Forms.Button();
            this.CadanceList = new System.Windows.Forms.ListView();
            this.ColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Trackers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Generate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Duplicate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SaveData = new System.Windows.Forms.CheckBox();
            this.FileBrowse = new System.Windows.Forms.Button();
            this.FilenameText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ImportFromFile = new System.Windows.Forms.RadioButton();
            this.ManualInput = new System.Windows.Forms.RadioButton();
            this.FacilityName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GenerateSingle = new System.Windows.Forms.Button();
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
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ConstType = new System.Windows.Forms.ComboBox();
            this.ConstName = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.ExistingConst = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeleteCadance
            // 
            this.DeleteCadance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteCadance.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteCadance.ForeColor = System.Drawing.SystemColors.Desktop;
            this.DeleteCadance.ImageIndex = 1;
            this.DeleteCadance.ImageList = this.imageList1;
            this.DeleteCadance.Location = new System.Drawing.Point(289, 188);
            this.DeleteCadance.Margin = new System.Windows.Forms.Padding(2);
            this.DeleteCadance.Name = "DeleteCadance";
            this.DeleteCadance.Size = new System.Drawing.Size(40, 40);
            this.DeleteCadance.TabIndex = 41;
            this.DeleteCadance.UseVisualStyleBackColor = true;
            this.DeleteCadance.Click += new System.EventHandler(this.DeleteCadance_Click);
            this.DeleteCadance.MouseHover += new System.EventHandler(this.DeleteCadance_MouseHover);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "add.png");
            this.imageList1.Images.SetKeyName(1, "delete.png");
            this.imageList1.Images.SetKeyName(2, "duplicate.png");
            this.imageList1.Images.SetKeyName(3, "paint-brush.png");
            this.imageList1.Images.SetKeyName(4, "x-mark.png");
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
            this.label13.Size = new System.Drawing.Size(151, 23);
            this.label13.TabIndex = 40;
            this.label13.Text = "Facility Creator";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ForeColor = System.Drawing.Color.White;
            this.Cancel.ImageIndex = 4;
            this.Cancel.ImageList = this.imageList1;
            this.Cancel.Location = new System.Drawing.Point(314, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 39;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // EditCadance
            // 
            this.EditCadance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditCadance.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EditCadance.ForeColor = System.Drawing.SystemColors.Desktop;
            this.EditCadance.ImageIndex = 3;
            this.EditCadance.ImageList = this.imageList1;
            this.EditCadance.Location = new System.Drawing.Point(245, 188);
            this.EditCadance.Margin = new System.Windows.Forms.Padding(2);
            this.EditCadance.Name = "EditCadance";
            this.EditCadance.Size = new System.Drawing.Size(40, 40);
            this.EditCadance.TabIndex = 38;
            this.EditCadance.UseVisualStyleBackColor = true;
            this.EditCadance.Click += new System.EventHandler(this.EditCadance_Click);
            this.EditCadance.MouseHover += new System.EventHandler(this.EditCadance_MouseHover);
            // 
            // AddCadance
            // 
            this.AddCadance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddCadance.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddCadance.ForeColor = System.Drawing.SystemColors.Desktop;
            this.AddCadance.ImageIndex = 0;
            this.AddCadance.ImageList = this.imageList1;
            this.AddCadance.Location = new System.Drawing.Point(157, 188);
            this.AddCadance.Margin = new System.Windows.Forms.Padding(2);
            this.AddCadance.Name = "AddCadance";
            this.AddCadance.Size = new System.Drawing.Size(40, 40);
            this.AddCadance.TabIndex = 37;
            this.AddCadance.UseVisualStyleBackColor = true;
            this.AddCadance.Click += new System.EventHandler(this.AddCadance_Click);
            this.AddCadance.MouseHover += new System.EventHandler(this.AddCadance_MouseHover);
            // 
            // CadanceList
            // 
            this.CadanceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CadanceList.BackColor = System.Drawing.Color.DimGray;
            this.CadanceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnName,
            this.Type,
            this.Trackers});
            this.CadanceList.ForeColor = System.Drawing.Color.White;
            this.CadanceList.FullRowSelect = true;
            this.CadanceList.HideSelection = false;
            this.CadanceList.Location = new System.Drawing.Point(3, 17);
            this.CadanceList.Margin = new System.Windows.Forms.Padding(2);
            this.CadanceList.Name = "CadanceList";
            this.CadanceList.Size = new System.Drawing.Size(328, 169);
            this.CadanceList.TabIndex = 36;
            this.CadanceList.UseCompatibleStateImageBehavior = false;
            this.CadanceList.View = System.Windows.Forms.View.Details;
            this.CadanceList.SelectedIndexChanged += new System.EventHandler(this.CadanceList_SelectedIndexChanged);
            // 
            // ColumnName
            // 
            this.ColumnName.Text = "Name";
            this.ColumnName.Width = 172;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 91;
            // 
            // Trackers
            // 
            this.Trackers.Text = " Trackers";
            this.Trackers.Width = 61;
            // 
            // Generate
            // 
            this.Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Generate.BackColor = System.Drawing.Color.SteelBlue;
            this.Generate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Generate.ForeColor = System.Drawing.Color.White;
            this.Generate.Location = new System.Drawing.Point(3, 190);
            this.Generate.Margin = new System.Windows.Forms.Padding(2);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(96, 32);
            this.Generate.TabIndex = 42;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = false;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Duplicate);
            this.groupBox1.Controls.Add(this.Generate);
            this.groupBox1.Controls.Add(this.CadanceList);
            this.groupBox1.Controls.Add(this.DeleteCadance);
            this.groupBox1.Controls.Add(this.AddCadance);
            this.groupBox1.Controls.Add(this.EditCadance);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(4, 51);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(334, 236);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sensor Cadences";
            // 
            // Duplicate
            // 
            this.Duplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Duplicate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Duplicate.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Duplicate.ImageIndex = 2;
            this.Duplicate.ImageList = this.imageList1;
            this.Duplicate.Location = new System.Drawing.Point(201, 188);
            this.Duplicate.Margin = new System.Windows.Forms.Padding(2);
            this.Duplicate.Name = "Duplicate";
            this.Duplicate.Size = new System.Drawing.Size(40, 40);
            this.Duplicate.TabIndex = 43;
            this.Duplicate.UseVisualStyleBackColor = true;
            this.Duplicate.Click += new System.EventHandler(this.Duplicate_Click);
            this.Duplicate.MouseHover += new System.EventHandler(this.Duplicate_MouseHover);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.SaveData);
            this.groupBox2.Controls.Add(this.FileBrowse);
            this.groupBox2.Controls.Add(this.FilenameText);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.ImportFromFile);
            this.groupBox2.Controls.Add(this.ManualInput);
            this.groupBox2.Controls.Add(this.FacilityName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.GenerateSingle);
            this.groupBox2.Controls.Add(this.SensorType);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.Altitude);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.Longitude);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.Latitude);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ConstType);
            this.groupBox2.Controls.Add(this.ConstName);
            this.groupBox2.Controls.Add(this.NameLabel);
            this.groupBox2.Controls.Add(this.ExistingConst);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(4, 292);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(334, 290);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Additional Options";
            // 
            // SaveData
            // 
            this.SaveData.AutoSize = true;
            this.SaveData.Checked = true;
            this.SaveData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SaveData.Enabled = false;
            this.SaveData.Location = new System.Drawing.Point(212, 18);
            this.SaveData.Name = "SaveData";
            this.SaveData.Size = new System.Drawing.Size(51, 17);
            this.SaveData.TabIndex = 65;
            this.SaveData.Text = "Save";
            this.SaveData.UseVisualStyleBackColor = true;
            // 
            // FileBrowse
            // 
            this.FileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FileBrowse.BackColor = System.Drawing.Color.SteelBlue;
            this.FileBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FileBrowse.ForeColor = System.Drawing.Color.White;
            this.FileBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.FileBrowse.Location = new System.Drawing.Point(297, 214);
            this.FileBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.FileBrowse.Name = "FileBrowse";
            this.FileBrowse.Size = new System.Drawing.Size(28, 21);
            this.FileBrowse.TabIndex = 64;
            this.FileBrowse.Text = "...";
            this.FileBrowse.UseVisualStyleBackColor = false;
            this.FileBrowse.Click += new System.EventHandler(this.FileBrowse_Click);
            // 
            // FilenameText
            // 
            this.FilenameText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilenameText.BackColor = System.Drawing.Color.DimGray;
            this.FilenameText.ForeColor = System.Drawing.Color.White;
            this.FilenameText.Location = new System.Drawing.Point(89, 214);
            this.FilenameText.Margin = new System.Windows.Forms.Padding(2);
            this.FilenameText.Name = "FilenameText";
            this.FilenameText.Size = new System.Drawing.Size(204, 20);
            this.FilenameText.TabIndex = 62;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 215);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 63;
            this.label9.Text = "Filename:";
            // 
            // ImportFromFile
            // 
            this.ImportFromFile.AutoSize = true;
            this.ImportFromFile.Location = new System.Drawing.Point(107, 17);
            this.ImportFromFile.Name = "ImportFromFile";
            this.ImportFromFile.Size = new System.Drawing.Size(99, 17);
            this.ImportFromFile.TabIndex = 61;
            this.ImportFromFile.TabStop = true;
            this.ImportFromFile.Text = "Import From File";
            this.ImportFromFile.UseVisualStyleBackColor = true;
            this.ImportFromFile.CheckedChanged += new System.EventHandler(this.ImportFromFile_CheckedChanged);
            // 
            // ManualInput
            // 
            this.ManualInput.AutoSize = true;
            this.ManualInput.Location = new System.Drawing.Point(14, 17);
            this.ManualInput.Name = "ManualInput";
            this.ManualInput.Size = new System.Drawing.Size(87, 17);
            this.ManualInput.TabIndex = 60;
            this.ManualInput.TabStop = true;
            this.ManualInput.Text = "Manual Input";
            this.ManualInput.UseVisualStyleBackColor = true;
            this.ManualInput.CheckedChanged += new System.EventHandler(this.ManualInput_CheckedChanged);
            // 
            // FacilityName
            // 
            this.FacilityName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FacilityName.BackColor = System.Drawing.Color.DimGray;
            this.FacilityName.ForeColor = System.Drawing.Color.White;
            this.FacilityName.Location = new System.Drawing.Point(89, 40);
            this.FacilityName.Margin = new System.Windows.Forms.Padding(2);
            this.FacilityName.Name = "FacilityName";
            this.FacilityName.Size = new System.Drawing.Size(235, 20);
            this.FacilityName.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(11, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Facility Name:";
            // 
            // GenerateSingle
            // 
            this.GenerateSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GenerateSingle.BackColor = System.Drawing.Color.SteelBlue;
            this.GenerateSingle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GenerateSingle.ForeColor = System.Drawing.Color.White;
            this.GenerateSingle.Location = new System.Drawing.Point(3, 244);
            this.GenerateSingle.Margin = new System.Windows.Forms.Padding(2);
            this.GenerateSingle.Name = "GenerateSingle";
            this.GenerateSingle.Size = new System.Drawing.Size(96, 32);
            this.GenerateSingle.TabIndex = 43;
            this.GenerateSingle.Text = "Generate";
            this.GenerateSingle.UseVisualStyleBackColor = false;
            this.GenerateSingle.Click += new System.EventHandler(this.GenerateSingle_Click);
            // 
            // SensorType
            // 
            this.SensorType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SensorType.BackColor = System.Drawing.Color.DimGray;
            this.SensorType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SensorType.ForeColor = System.Drawing.Color.White;
            this.SensorType.FormattingEnabled = true;
            this.SensorType.Location = new System.Drawing.Point(89, 67);
            this.SensorType.Margin = new System.Windows.Forms.Padding(2);
            this.SensorType.Name = "SensorType";
            this.SensorType.Size = new System.Drawing.Size(235, 21);
            this.SensorType.TabIndex = 45;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(231, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "km";
            // 
            // Altitude
            // 
            this.Altitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Altitude.BackColor = System.Drawing.Color.DimGray;
            this.Altitude.ForeColor = System.Drawing.Color.White;
            this.Altitude.Location = new System.Drawing.Point(89, 138);
            this.Altitude.Margin = new System.Windows.Forms.Padding(2);
            this.Altitude.Name = "Altitude";
            this.Altitude.Size = new System.Drawing.Size(139, 20);
            this.Altitude.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(11, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 57;
            this.label8.Text = "Altitude:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(231, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "deg";
            // 
            // Longitude
            // 
            this.Longitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Longitude.BackColor = System.Drawing.Color.DimGray;
            this.Longitude.ForeColor = System.Drawing.Color.White;
            this.Longitude.Location = new System.Drawing.Point(89, 116);
            this.Longitude.Margin = new System.Windows.Forms.Padding(2);
            this.Longitude.Name = "Longitude";
            this.Longitude.Size = new System.Drawing.Size(139, 20);
            this.Longitude.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(11, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Longitude:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(231, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "deg";
            // 
            // Latitude
            // 
            this.Latitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Latitude.BackColor = System.Drawing.Color.DimGray;
            this.Latitude.ForeColor = System.Drawing.Color.White;
            this.Latitude.Location = new System.Drawing.Point(89, 93);
            this.Latitude.Margin = new System.Windows.Forms.Padding(2);
            this.Latitude.Name = "Latitude";
            this.Latitude.Size = new System.Drawing.Size(139, 20);
            this.Latitude.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(11, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Latitude:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 165);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 13);
            this.label11.TabIndex = 52;
            this.label11.Text = "Constellation Option:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Facility Type:";
            // 
            // ConstType
            // 
            this.ConstType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstType.BackColor = System.Drawing.Color.DimGray;
            this.ConstType.ForeColor = System.Drawing.Color.White;
            this.ConstType.FormattingEnabled = true;
            this.ConstType.Location = new System.Drawing.Point(119, 163);
            this.ConstType.Margin = new System.Windows.Forms.Padding(2);
            this.ConstType.Name = "ConstType";
            this.ConstType.Size = new System.Drawing.Size(209, 21);
            this.ConstType.TabIndex = 51;
            this.ConstType.SelectedIndexChanged += new System.EventHandler(this.ConstType_SelectedIndexChanged);
            // 
            // ConstName
            // 
            this.ConstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstName.BackColor = System.Drawing.Color.DimGray;
            this.ConstName.ForeColor = System.Drawing.Color.White;
            this.ConstName.Location = new System.Drawing.Point(119, 190);
            this.ConstName.Margin = new System.Windows.Forms.Padding(2);
            this.ConstName.Name = "ConstName";
            this.ConstName.Size = new System.Drawing.Size(211, 20);
            this.ConstName.TabIndex = 49;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(11, 191);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(101, 13);
            this.NameLabel.TabIndex = 50;
            this.NameLabel.Text = "Constellation Name:";
            // 
            // ExistingConst
            // 
            this.ExistingConst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExistingConst.BackColor = System.Drawing.Color.DimGray;
            this.ExistingConst.ForeColor = System.Drawing.Color.White;
            this.ExistingConst.FormattingEnabled = true;
            this.ExistingConst.Location = new System.Drawing.Point(119, 189);
            this.ExistingConst.Margin = new System.Windows.Forms.Padding(2);
            this.ExistingConst.Name = "ExistingConst";
            this.ExistingConst.Size = new System.Drawing.Size(211, 21);
            this.ExistingConst.TabIndex = 48;
            // 
            // FacilityCreatorPlugin
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FacilityCreatorPlugin";
            this.Size = new System.Drawing.Size(340, 700);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DeleteCadance;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button EditCadance;
        private System.Windows.Forms.Button AddCadance;
        private System.Windows.Forms.ListView CadanceList;
        private System.Windows.Forms.ColumnHeader ColumnName;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FacilityName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SensorType;
        private System.Windows.Forms.Button GenerateSingle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Altitude;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Longitude;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Latitude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ConstType;
        private System.Windows.Forms.TextBox ConstName;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.ComboBox ExistingConst;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Trackers;
        private System.Windows.Forms.Button Duplicate;
        private System.Windows.Forms.TextBox FilenameText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton ImportFromFile;
        private System.Windows.Forms.RadioButton ManualInput;
        private System.Windows.Forms.Button FileBrowse;
        private System.Windows.Forms.CheckBox SaveData;
    }
}