namespace OperatorsToolbox.StationAccess
{
    partial class StationAccessPlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationAccessPlugin));
            this.AccessName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DataType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ShowReport = new System.Windows.Forms.CheckBox();
            this.ExportCSV = new System.Windows.Forms.CheckBox();
            this.FromUnselect = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.FromSelect = new System.Windows.Forms.Button();
            this.ToUnselect = new System.Windows.Forms.Button();
            this.ToSelect = new System.Windows.Forms.Button();
            this.FromObjectList = new System.Windows.Forms.ListView();
            this.FromName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ToObjectList = new System.Windows.Forms.ListView();
            this.ToName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FromObjectType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ToObjectType = new System.Windows.Forms.ComboBox();
            this.Generate = new System.Windows.Forms.Button();
            this.lblStkObjects = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccessName
            // 
            this.AccessName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccessName.BackColor = System.Drawing.Color.DimGray;
            this.AccessName.ForeColor = System.Drawing.Color.White;
            this.AccessName.Location = new System.Drawing.Point(98, 32);
            this.AccessName.Margin = new System.Windows.Forms.Padding(2);
            this.AccessName.Name = "AccessName";
            this.AccessName.Size = new System.Drawing.Size(140, 20);
            this.AccessName.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Access Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DataType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ShowReport);
            this.groupBox1.Controls.Add(this.ExportCSV);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(8, 364);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(230, 117);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Output Options";
            // 
            // DataType
            // 
            this.DataType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataType.BackColor = System.Drawing.Color.DimGray;
            this.DataType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DataType.ForeColor = System.Drawing.Color.White;
            this.DataType.FormattingEnabled = true;
            this.DataType.Location = new System.Drawing.Point(48, 25);
            this.DataType.Margin = new System.Windows.Forms.Padding(2);
            this.DataType.Name = "DataType";
            this.DataType.Size = new System.Drawing.Size(162, 21);
            this.DataType.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Type:";
            // 
            // ShowReport
            // 
            this.ShowReport.AutoSize = true;
            this.ShowReport.Location = new System.Drawing.Point(4, 59);
            this.ShowReport.Margin = new System.Windows.Forms.Padding(2);
            this.ShowReport.Name = "ShowReport";
            this.ShowReport.Size = new System.Drawing.Size(105, 17);
            this.ShowReport.TabIndex = 15;
            this.ShowReport.Text = "Generate Report";
            this.ShowReport.UseVisualStyleBackColor = true;
            // 
            // ExportCSV
            // 
            this.ExportCSV.AutoSize = true;
            this.ExportCSV.Location = new System.Drawing.Point(4, 81);
            this.ExportCSV.Margin = new System.Windows.Forms.Padding(2);
            this.ExportCSV.Name = "ExportCSV";
            this.ExportCSV.Size = new System.Drawing.Size(130, 17);
            this.ExportCSV.TabIndex = 14;
            this.ExportCSV.Text = "Export Access to CSV";
            this.ExportCSV.UseVisualStyleBackColor = true;
            // 
            // FromUnselect
            // 
            this.FromUnselect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FromUnselect.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FromUnselect.ImageIndex = 1;
            this.FromUnselect.ImageList = this.imageList1;
            this.FromUnselect.Location = new System.Drawing.Point(-2, 276);
            this.FromUnselect.Margin = new System.Windows.Forms.Padding(2);
            this.FromUnselect.Name = "FromUnselect";
            this.FromUnselect.Size = new System.Drawing.Size(40, 40);
            this.FromUnselect.TabIndex = 29;
            this.FromUnselect.UseVisualStyleBackColor = true;
            this.FromUnselect.Click += new System.EventHandler(this.FromUnselect_Click);
            this.FromUnselect.MouseHover += new System.EventHandler(this.FromUnselect_MouseHover);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "x-mark.png");
            this.imageList1.Images.SetKeyName(1, "delete.png");
            this.imageList1.Images.SetKeyName(2, "add.png");
            this.imageList1.Images.SetKeyName(3, "filter.png");
            // 
            // FromSelect
            // 
            this.FromSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FromSelect.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FromSelect.ImageIndex = 2;
            this.FromSelect.ImageList = this.imageList1;
            this.FromSelect.Location = new System.Drawing.Point(-2, 234);
            this.FromSelect.Margin = new System.Windows.Forms.Padding(2);
            this.FromSelect.Name = "FromSelect";
            this.FromSelect.Size = new System.Drawing.Size(40, 40);
            this.FromSelect.TabIndex = 28;
            this.FromSelect.UseVisualStyleBackColor = true;
            this.FromSelect.Click += new System.EventHandler(this.FromSelect_Click);
            this.FromSelect.MouseHover += new System.EventHandler(this.FromSelect_MouseHover);
            // 
            // ToUnselect
            // 
            this.ToUnselect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ToUnselect.ForeColor = System.Drawing.SystemColors.Desktop;
            this.ToUnselect.ImageIndex = 1;
            this.ToUnselect.ImageList = this.imageList1;
            this.ToUnselect.Location = new System.Drawing.Point(0, 126);
            this.ToUnselect.Margin = new System.Windows.Forms.Padding(2);
            this.ToUnselect.Name = "ToUnselect";
            this.ToUnselect.Size = new System.Drawing.Size(40, 40);
            this.ToUnselect.TabIndex = 27;
            this.ToUnselect.UseVisualStyleBackColor = true;
            this.ToUnselect.Click += new System.EventHandler(this.ToUnselect_Click);
            this.ToUnselect.MouseHover += new System.EventHandler(this.ToUnselect_MouseHover);
            // 
            // ToSelect
            // 
            this.ToSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ToSelect.ForeColor = System.Drawing.SystemColors.Desktop;
            this.ToSelect.ImageIndex = 2;
            this.ToSelect.ImageList = this.imageList1;
            this.ToSelect.Location = new System.Drawing.Point(0, 84);
            this.ToSelect.Margin = new System.Windows.Forms.Padding(2);
            this.ToSelect.Name = "ToSelect";
            this.ToSelect.Size = new System.Drawing.Size(40, 40);
            this.ToSelect.TabIndex = 26;
            this.ToSelect.UseVisualStyleBackColor = true;
            this.ToSelect.Click += new System.EventHandler(this.ToSelect_Click);
            this.ToSelect.MouseHover += new System.EventHandler(this.ToSelect_MouseHover);
            // 
            // FromObjectList
            // 
            this.FromObjectList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FromObjectList.BackColor = System.Drawing.Color.DimGray;
            this.FromObjectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FromName});
            this.FromObjectList.ForeColor = System.Drawing.Color.White;
            this.FromObjectList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.FromObjectList.Location = new System.Drawing.Point(42, 234);
            this.FromObjectList.Margin = new System.Windows.Forms.Padding(2);
            this.FromObjectList.Name = "FromObjectList";
            this.FromObjectList.Size = new System.Drawing.Size(196, 114);
            this.FromObjectList.TabIndex = 25;
            this.FromObjectList.UseCompatibleStateImageBehavior = false;
            this.FromObjectList.View = System.Windows.Forms.View.Details;
            // 
            // FromName
            // 
            this.FromName.Text = "Object Name";
            this.FromName.Width = 240;
            // 
            // ToObjectList
            // 
            this.ToObjectList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ToObjectList.BackColor = System.Drawing.Color.DimGray;
            this.ToObjectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ToName});
            this.ToObjectList.ForeColor = System.Drawing.Color.White;
            this.ToObjectList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ToObjectList.Location = new System.Drawing.Point(42, 84);
            this.ToObjectList.Margin = new System.Windows.Forms.Padding(2);
            this.ToObjectList.Name = "ToObjectList";
            this.ToObjectList.Size = new System.Drawing.Size(196, 114);
            this.ToObjectList.TabIndex = 24;
            this.ToObjectList.UseCompatibleStateImageBehavior = false;
            this.ToObjectList.View = System.Windows.Forms.View.Details;
            // 
            // ToName
            // 
            this.ToName.Text = "Object Name";
            this.ToName.Width = 240;
            // 
            // FromObjectType
            // 
            this.FromObjectType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FromObjectType.BackColor = System.Drawing.Color.DimGray;
            this.FromObjectType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FromObjectType.ForeColor = System.Drawing.Color.White;
            this.FromObjectType.FormattingEnabled = true;
            this.FromObjectType.Location = new System.Drawing.Point(116, 207);
            this.FromObjectType.Margin = new System.Windows.Forms.Padding(2);
            this.FromObjectType.Name = "FromObjectType";
            this.FromObjectType.Size = new System.Drawing.Size(122, 21);
            this.FromObjectType.TabIndex = 23;
            this.FromObjectType.SelectedIndexChanged += new System.EventHandler(this.FromObjectType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "From Object Type:";
            // 
            // ToObjectType
            // 
            this.ToObjectType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ToObjectType.BackColor = System.Drawing.Color.DimGray;
            this.ToObjectType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ToObjectType.ForeColor = System.Drawing.Color.White;
            this.ToObjectType.FormattingEnabled = true;
            this.ToObjectType.Location = new System.Drawing.Point(116, 59);
            this.ToObjectType.Margin = new System.Windows.Forms.Padding(2);
            this.ToObjectType.Name = "ToObjectType";
            this.ToObjectType.Size = new System.Drawing.Size(122, 21);
            this.ToObjectType.TabIndex = 21;
            this.ToObjectType.SelectedIndexChanged += new System.EventHandler(this.ToObjectType_SelectedIndexChanged);
            // 
            // Generate
            // 
            this.Generate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Generate.BackColor = System.Drawing.Color.SteelBlue;
            this.Generate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Generate.ForeColor = System.Drawing.Color.White;
            this.Generate.Location = new System.Drawing.Point(62, 486);
            this.Generate.Margin = new System.Windows.Forms.Padding(2);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(96, 26);
            this.Generate.TabIndex = 20;
            this.Generate.Text = "Generate Access";
            this.Generate.UseVisualStyleBackColor = false;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // lblStkObjects
            // 
            this.lblStkObjects.AutoSize = true;
            this.lblStkObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStkObjects.Location = new System.Drawing.Point(5, 62);
            this.lblStkObjects.Name = "lblStkObjects";
            this.lblStkObjects.Size = new System.Drawing.Size(99, 13);
            this.lblStkObjects.TabIndex = 19;
            this.lblStkObjects.Text = "To Object Type:";
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
            this.label13.Size = new System.Drawing.Size(144, 23);
            this.label13.TabIndex = 34;
            this.label13.Text = "Station Access";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ForeColor = System.Drawing.Color.White;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.imageList1;
            this.Cancel.Location = new System.Drawing.Point(229, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 33;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // StationAccessPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(255, 569);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.AccessName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FromUnselect);
            this.Controls.Add(this.FromSelect);
            this.Controls.Add(this.ToUnselect);
            this.Controls.Add(this.ToSelect);
            this.Controls.Add(this.FromObjectList);
            this.Controls.Add(this.ToObjectList);
            this.Controls.Add(this.FromObjectType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ToObjectType);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.lblStkObjects);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StationAccessPlugin";
            this.Text = "StationAccessPlugin";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AccessName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox DataType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ShowReport;
        private System.Windows.Forms.CheckBox ExportCSV;
        private System.Windows.Forms.Button FromUnselect;
        private System.Windows.Forms.Button FromSelect;
        private System.Windows.Forms.Button ToUnselect;
        private System.Windows.Forms.Button ToSelect;
        private System.Windows.Forms.ListView FromObjectList;
        private System.Windows.Forms.ColumnHeader FromName;
        private System.Windows.Forms.ListView ToObjectList;
        private System.Windows.Forms.ColumnHeader ToName;
        private System.Windows.Forms.ComboBox FromObjectType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ToObjectType;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Label lblStkObjects;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ImageList imageList1;
    }
}