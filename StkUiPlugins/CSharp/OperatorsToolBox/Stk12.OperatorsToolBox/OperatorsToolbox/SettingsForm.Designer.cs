namespace OperatorsToolbox
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TemplatesBrowse = new System.Windows.Forms.Button();
            this.TemplatesPath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.UdlAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.AreaTargetBrowse = new System.Windows.Forms.Button();
            this.AreaTargetPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SatCatBrowse = new System.Windows.Forms.Button();
            this.SatCatPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SatDataBrowse = new System.Windows.Forms.Button();
            this.SatDataPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ImagePath = new System.Windows.Forms.TextBox();
            this.ImageLocationBrowse = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.NameText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RemoveType = new System.Windows.Forms.Button();
            this.AddType = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.PanelHeightPixels = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RemoveLegends = new System.Windows.Forms.Button();
            this.IslandToggle = new System.Windows.Forms.CheckBox();
            this.BordersToggle = new System.Windows.Forms.CheckBox();
            this.SensorGraphics = new System.Windows.Forms.CheckBox();
            this.Apply = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TemplatesBrowse);
            this.groupBox1.Controls.Add(this.TemplatesPath);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.UdlAddress);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.AreaTargetBrowse);
            this.groupBox1.Controls.Add(this.AreaTargetPath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.SatCatBrowse);
            this.groupBox1.Controls.Add(this.SatCatPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.SatDataBrowse);
            this.groupBox1.Controls.Add(this.SatDataPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(394, 206);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Locations";
            // 
            // TemplatesBrowse
            // 
            this.TemplatesBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TemplatesBrowse.BackColor = System.Drawing.Color.SteelBlue;
            this.TemplatesBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TemplatesBrowse.ForeColor = System.Drawing.Color.White;
            this.TemplatesBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.TemplatesBrowse.Location = new System.Drawing.Point(349, 136);
            this.TemplatesBrowse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TemplatesBrowse.Name = "TemplatesBrowse";
            this.TemplatesBrowse.Size = new System.Drawing.Size(28, 21);
            this.TemplatesBrowse.TabIndex = 13;
            this.TemplatesBrowse.Text = "...";
            this.TemplatesBrowse.UseVisualStyleBackColor = false;
            this.TemplatesBrowse.Click += new System.EventHandler(this.TemplatesBrowse_Click);
            // 
            // TemplatesPath
            // 
            this.TemplatesPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TemplatesPath.BackColor = System.Drawing.Color.DimGray;
            this.TemplatesPath.ForeColor = System.Drawing.Color.White;
            this.TemplatesPath.Location = new System.Drawing.Point(116, 136);
            this.TemplatesPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TemplatesPath.Name = "TemplatesPath";
            this.TemplatesPath.Size = new System.Drawing.Size(230, 20);
            this.TemplatesPath.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(7, 137);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Templates Directory:";
            // 
            // UdlAddress
            // 
            this.UdlAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UdlAddress.BackColor = System.Drawing.Color.DimGray;
            this.UdlAddress.ForeColor = System.Drawing.Color.White;
            this.UdlAddress.Location = new System.Drawing.Point(116, 176);
            this.UdlAddress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UdlAddress.Name = "UdlAddress";
            this.UdlAddress.Size = new System.Drawing.Size(230, 20);
            this.UdlAddress.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(40, 176);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "UDL Address:";
            // 
            // AreaTargetBrowse
            // 
            this.AreaTargetBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AreaTargetBrowse.BackColor = System.Drawing.Color.SteelBlue;
            this.AreaTargetBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AreaTargetBrowse.ForeColor = System.Drawing.Color.White;
            this.AreaTargetBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AreaTargetBrowse.Location = new System.Drawing.Point(349, 101);
            this.AreaTargetBrowse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AreaTargetBrowse.Name = "AreaTargetBrowse";
            this.AreaTargetBrowse.Size = new System.Drawing.Size(28, 21);
            this.AreaTargetBrowse.TabIndex = 8;
            this.AreaTargetBrowse.Text = "...";
            this.AreaTargetBrowse.UseVisualStyleBackColor = false;
            this.AreaTargetBrowse.Click += new System.EventHandler(this.AreaTargetBrowse_Click);
            // 
            // AreaTargetPath
            // 
            this.AreaTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AreaTargetPath.BackColor = System.Drawing.Color.DimGray;
            this.AreaTargetPath.ForeColor = System.Drawing.Color.White;
            this.AreaTargetPath.Location = new System.Drawing.Point(116, 101);
            this.AreaTargetPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AreaTargetPath.Name = "AreaTargetPath";
            this.AreaTargetPath.Size = new System.Drawing.Size(230, 20);
            this.AreaTargetPath.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(20, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Area Target CSV:";
            // 
            // SatCatBrowse
            // 
            this.SatCatBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SatCatBrowse.BackColor = System.Drawing.Color.SteelBlue;
            this.SatCatBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SatCatBrowse.ForeColor = System.Drawing.Color.White;
            this.SatCatBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SatCatBrowse.Location = new System.Drawing.Point(349, 59);
            this.SatCatBrowse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SatCatBrowse.Name = "SatCatBrowse";
            this.SatCatBrowse.Size = new System.Drawing.Size(28, 21);
            this.SatCatBrowse.TabIndex = 5;
            this.SatCatBrowse.Text = "...";
            this.SatCatBrowse.UseVisualStyleBackColor = false;
            this.SatCatBrowse.Click += new System.EventHandler(this.SatCatBrowse_Click);
            // 
            // SatCatPath
            // 
            this.SatCatPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SatCatPath.BackColor = System.Drawing.Color.DimGray;
            this.SatCatPath.ForeColor = System.Drawing.Color.White;
            this.SatCatPath.Location = new System.Drawing.Point(116, 59);
            this.SatCatPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SatCatPath.Name = "SatCatPath";
            this.SatCatPath.Size = new System.Drawing.Size(230, 20);
            this.SatCatPath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(24, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Satellite Catalog:";
            // 
            // SatDataBrowse
            // 
            this.SatDataBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SatDataBrowse.BackColor = System.Drawing.Color.SteelBlue;
            this.SatDataBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SatDataBrowse.ForeColor = System.Drawing.Color.White;
            this.SatDataBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SatDataBrowse.Location = new System.Drawing.Point(349, 20);
            this.SatDataBrowse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SatDataBrowse.Name = "SatDataBrowse";
            this.SatDataBrowse.Size = new System.Drawing.Size(28, 21);
            this.SatDataBrowse.TabIndex = 2;
            this.SatDataBrowse.Text = "...";
            this.SatDataBrowse.UseVisualStyleBackColor = false;
            this.SatDataBrowse.Click += new System.EventHandler(this.SatDataBrowse_Click);
            // 
            // SatDataPath
            // 
            this.SatDataPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SatDataPath.BackColor = System.Drawing.Color.DimGray;
            this.SatDataPath.ForeColor = System.Drawing.Color.White;
            this.SatDataPath.Location = new System.Drawing.Point(116, 20);
            this.SatDataPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SatDataPath.Name = "SatDataPath";
            this.SatDataPath.Size = new System.Drawing.Size(230, 20);
            this.SatDataPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Satellite Database:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ImagePath);
            this.groupBox2.Controls.Add(this.ImageLocationBrowse);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.NameText);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.RemoveType);
            this.groupBox2.Controls.Add(this.AddType);
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(9, 220);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(394, 210);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Event Types";
            // 
            // ImagePath
            // 
            this.ImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePath.BackColor = System.Drawing.Color.DimGray;
            this.ImagePath.ForeColor = System.Drawing.Color.White;
            this.ImagePath.Location = new System.Drawing.Point(213, 79);
            this.ImagePath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImagePath.Name = "ImagePath";
            this.ImagePath.Size = new System.Drawing.Size(130, 20);
            this.ImagePath.TabIndex = 10;
            this.ImagePath.TextChanged += new System.EventHandler(this.ImagePath_TextChanged);
            // 
            // ImageLocationBrowse
            // 
            this.ImageLocationBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageLocationBrowse.BackColor = System.Drawing.Color.SteelBlue;
            this.ImageLocationBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ImageLocationBrowse.ForeColor = System.Drawing.Color.White;
            this.ImageLocationBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ImageLocationBrowse.Location = new System.Drawing.Point(349, 78);
            this.ImageLocationBrowse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImageLocationBrowse.Name = "ImageLocationBrowse";
            this.ImageLocationBrowse.Size = new System.Drawing.Size(28, 21);
            this.ImageLocationBrowse.TabIndex = 9;
            this.ImageLocationBrowse.Text = "...";
            this.ImageLocationBrowse.UseVisualStyleBackColor = false;
            this.ImageLocationBrowse.Click += new System.EventHandler(this.ImageLocationBrowse_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(128, 81);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Image Location:";
            // 
            // NameText
            // 
            this.NameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameText.BackColor = System.Drawing.Color.DimGray;
            this.NameText.ForeColor = System.Drawing.Color.White;
            this.NameText.Location = new System.Drawing.Point(213, 38);
            this.NameText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(130, 20);
            this.NameText.TabIndex = 4;
            this.NameText.TextChanged += new System.EventHandler(this.NameText_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(172, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Name:";
            // 
            // RemoveType
            // 
            this.RemoveType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RemoveType.BackColor = System.Drawing.Color.SteelBlue;
            this.RemoveType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveType.ForeColor = System.Drawing.Color.White;
            this.RemoveType.Location = new System.Drawing.Point(62, 176);
            this.RemoveType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RemoveType.Name = "RemoveType";
            this.RemoveType.Size = new System.Drawing.Size(61, 20);
            this.RemoveType.TabIndex = 2;
            this.RemoveType.Text = "Remove";
            this.RemoveType.UseVisualStyleBackColor = false;
            this.RemoveType.Click += new System.EventHandler(this.RemoveType_Click);
            // 
            // AddType
            // 
            this.AddType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddType.BackColor = System.Drawing.Color.SteelBlue;
            this.AddType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddType.ForeColor = System.Drawing.Color.White;
            this.AddType.Location = new System.Drawing.Point(5, 176);
            this.AddType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AddType.Name = "AddType";
            this.AddType.Size = new System.Drawing.Size(53, 20);
            this.AddType.TabIndex = 1;
            this.AddType.Text = "Add";
            this.AddType.UseVisualStyleBackColor = false;
            this.AddType.Click += new System.EventHandler(this.AddType_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.BackColor = System.Drawing.Color.DimGray;
            this.listBox1.ForeColor = System.Drawing.Color.White;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(5, 38);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(119, 134);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.RemoveLegends);
            this.groupBox3.Controls.Add(this.IslandToggle);
            this.groupBox3.Controls.Add(this.BordersToggle);
            this.groupBox3.Controls.Add(this.SensorGraphics);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(11, 444);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(392, 155);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Graphics";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.PanelHeightPixels);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(172, 63);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Size = new System.Drawing.Size(215, 87);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Panel Setting";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(5, 58);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "*Aspect Ratio 2:1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(160, 34);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "pixels";
            // 
            // PanelHeightPixels
            // 
            this.PanelHeightPixels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelHeightPixels.BackColor = System.Drawing.Color.DimGray;
            this.PanelHeightPixels.ForeColor = System.Drawing.Color.White;
            this.PanelHeightPixels.Location = new System.Drawing.Point(99, 32);
            this.PanelHeightPixels.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PanelHeightPixels.Name = "PanelHeightPixels";
            this.PanelHeightPixels.Size = new System.Drawing.Size(58, 20);
            this.PanelHeightPixels.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(5, 34);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Min Panel Height:";
            // 
            // RemoveLegends
            // 
            this.RemoveLegends.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveLegends.BackColor = System.Drawing.Color.SteelBlue;
            this.RemoveLegends.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveLegends.ForeColor = System.Drawing.Color.White;
            this.RemoveLegends.Location = new System.Drawing.Point(285, 18);
            this.RemoveLegends.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RemoveLegends.Name = "RemoveLegends";
            this.RemoveLegends.Size = new System.Drawing.Size(90, 37);
            this.RemoveLegends.TabIndex = 11;
            this.RemoveLegends.Text = "Remove All FOM Legends";
            this.RemoveLegends.UseVisualStyleBackColor = false;
            this.RemoveLegends.Click += new System.EventHandler(this.RemoveLegends_Click);
            // 
            // IslandToggle
            // 
            this.IslandToggle.AutoSize = true;
            this.IslandToggle.ForeColor = System.Drawing.Color.White;
            this.IslandToggle.Location = new System.Drawing.Point(14, 93);
            this.IslandToggle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.IslandToggle.Name = "IslandToggle";
            this.IslandToggle.Size = new System.Drawing.Size(141, 17);
            this.IslandToggle.TabIndex = 3;
            this.IslandToggle.Text = "Toggle Island Coastlines";
            this.IslandToggle.UseVisualStyleBackColor = true;
            this.IslandToggle.CheckedChanged += new System.EventHandler(this.IslandToggle_CheckedChanged);
            // 
            // BordersToggle
            // 
            this.BordersToggle.AutoSize = true;
            this.BordersToggle.ForeColor = System.Drawing.Color.White;
            this.BordersToggle.Location = new System.Drawing.Point(14, 63);
            this.BordersToggle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BordersToggle.Name = "BordersToggle";
            this.BordersToggle.Size = new System.Drawing.Size(159, 17);
            this.BordersToggle.TabIndex = 2;
            this.BordersToggle.Text = "Toggle International Borders";
            this.BordersToggle.UseVisualStyleBackColor = true;
            this.BordersToggle.CheckedChanged += new System.EventHandler(this.BordersToggle_CheckedChanged);
            // 
            // SensorGraphics
            // 
            this.SensorGraphics.AutoSize = true;
            this.SensorGraphics.ForeColor = System.Drawing.Color.White;
            this.SensorGraphics.Location = new System.Drawing.Point(14, 28);
            this.SensorGraphics.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SensorGraphics.Name = "SensorGraphics";
            this.SensorGraphics.Size = new System.Drawing.Size(140, 17);
            this.SensorGraphics.TabIndex = 1;
            this.SensorGraphics.Text = "Toggle Sensor Graphics";
            this.SensorGraphics.UseVisualStyleBackColor = true;
            this.SensorGraphics.CheckedChanged += new System.EventHandler(this.SensorGraphics_CheckedChanged);
            // 
            // Apply
            // 
            this.Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Apply.BackColor = System.Drawing.Color.SteelBlue;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Apply.ForeColor = System.Drawing.Color.White;
            this.Apply.Location = new System.Drawing.Point(11, 613);
            this.Apply.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(61, 28);
            this.Apply.TabIndex = 3;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ForeColor = System.Drawing.Color.White;
            this.Cancel.Location = new System.Drawing.Point(81, 613);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(61, 28);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(420, 657);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button SatDataBrowse;
        private System.Windows.Forms.TextBox SatDataPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SatCatBrowse;
        private System.Windows.Forms.TextBox SatCatPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AreaTargetBrowse;
        private System.Windows.Forms.TextBox AreaTargetPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button AddType;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button ImageLocationBrowse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox NameText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button RemoveType;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox SensorGraphics;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TextBox ImagePath;
        private System.Windows.Forms.CheckBox BordersToggle;
        private System.Windows.Forms.CheckBox IslandToggle;
        private System.Windows.Forms.TextBox UdlAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button RemoveLegends;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox PanelHeightPixels;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button TemplatesBrowse;
        private System.Windows.Forms.TextBox TemplatesPath;
        private System.Windows.Forms.Label label10;
    }
}