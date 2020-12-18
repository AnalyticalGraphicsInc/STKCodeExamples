namespace OperatorsToolbox.EpochUpdater
{
    partial class SatelliteEpochUpdatePlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SatelliteEpochUpdatePlugin));
            this.label13 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_end = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_start = new System.Windows.Forms.DateTimePicker();
            this.SatelliteList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RemoveSatellite = new System.Windows.Forms.Button();
            this.AddSatellite = new System.Windows.Forms.Button();
            this.ASTGRunCheck = new System.Windows.Forms.CheckBox();
            this.Update = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.label13.Size = new System.Drawing.Size(143, 23);
            this.label13.TabIndex = 32;
            this.label13.Text = "Epoch Update";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.imageList1;
            this.Cancel.Location = new System.Drawing.Point(229, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 31;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(20, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "End Date";
            // 
            // dtp_end
            // 
            this.dtp_end.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_end.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dtp_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_end.Location = new System.Drawing.Point(20, 108);
            this.dtp_end.Name = "dtp_end";
            this.dtp_end.Size = new System.Drawing.Size(152, 20);
            this.dtp_end.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(20, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Start Date";
            // 
            // dtp_start
            // 
            this.dtp_start.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_start.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dtp_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_start.Location = new System.Drawing.Point(20, 56);
            this.dtp_start.Name = "dtp_start";
            this.dtp_start.Size = new System.Drawing.Size(152, 20);
            this.dtp_start.TabIndex = 40;
            // 
            // SatelliteList
            // 
            this.SatelliteList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SatelliteList.BackColor = System.Drawing.Color.DimGray;
            this.SatelliteList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SatelliteList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.SatelliteList.ForeColor = System.Drawing.Color.White;
            this.SatelliteList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.SatelliteList.HideSelection = false;
            this.SatelliteList.Location = new System.Drawing.Point(39, 145);
            this.SatelliteList.Margin = new System.Windows.Forms.Padding(2);
            this.SatelliteList.Name = "SatelliteList";
            this.SatelliteList.Size = new System.Drawing.Size(205, 169);
            this.SatelliteList.TabIndex = 44;
            this.SatelliteList.UseCompatibleStateImageBehavior = false;
            this.SatelliteList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 250;
            // 
            // RemoveSatellite
            // 
            this.RemoveSatellite.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveSatellite.ForeColor = System.Drawing.SystemColors.Desktop;
            this.RemoveSatellite.ImageIndex = 1;
            this.RemoveSatellite.ImageList = this.imageList1;
            this.RemoveSatellite.Location = new System.Drawing.Point(3, 183);
            this.RemoveSatellite.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveSatellite.Name = "RemoveSatellite";
            this.RemoveSatellite.Size = new System.Drawing.Size(35, 35);
            this.RemoveSatellite.TabIndex = 46;
            this.RemoveSatellite.UseVisualStyleBackColor = true;
            this.RemoveSatellite.Click += new System.EventHandler(this.RemoveSatellite_Click);
            // 
            // AddSatellite
            // 
            this.AddSatellite.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddSatellite.ForeColor = System.Drawing.SystemColors.Desktop;
            this.AddSatellite.ImageIndex = 2;
            this.AddSatellite.ImageList = this.imageList1;
            this.AddSatellite.Location = new System.Drawing.Point(3, 145);
            this.AddSatellite.Margin = new System.Windows.Forms.Padding(2);
            this.AddSatellite.Name = "AddSatellite";
            this.AddSatellite.Size = new System.Drawing.Size(35, 35);
            this.AddSatellite.TabIndex = 45;
            this.AddSatellite.UseVisualStyleBackColor = true;
            this.AddSatellite.Click += new System.EventHandler(this.AddSatellite_Click);
            // 
            // ASTGRunCheck
            // 
            this.ASTGRunCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ASTGRunCheck.AutoSize = true;
            this.ASTGRunCheck.ForeColor = System.Drawing.Color.White;
            this.ASTGRunCheck.Location = new System.Drawing.Point(14, 332);
            this.ASTGRunCheck.Margin = new System.Windows.Forms.Padding(2);
            this.ASTGRunCheck.Name = "ASTGRunCheck";
            this.ASTGRunCheck.Size = new System.Drawing.Size(189, 17);
            this.ASTGRunCheck.TabIndex = 47;
            this.ASTGRunCheck.Text = "Run all Astrogator MCS on Update";
            this.ASTGRunCheck.UseVisualStyleBackColor = true;
            this.ASTGRunCheck.CheckedChanged += new System.EventHandler(this.ASTGRunCheck_CheckedChanged);
            // 
            // Update
            // 
            this.Update.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Update.BackColor = System.Drawing.Color.SteelBlue;
            this.Update.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Update.ForeColor = System.Drawing.Color.White;
            this.Update.Location = new System.Drawing.Point(81, 362);
            this.Update.Margin = new System.Windows.Forms.Padding(2);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(68, 30);
            this.Update.TabIndex = 48;
            this.Update.Text = "Update";
            this.Update.UseVisualStyleBackColor = false;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // SatelliteEpochUpdatePlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.Update);
            this.Controls.Add(this.ASTGRunCheck);
            this.Controls.Add(this.RemoveSatellite);
            this.Controls.Add(this.AddSatellite);
            this.Controls.Add(this.SatelliteList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtp_end);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtp_start);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Cancel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SatelliteEpochUpdatePlugin";
            this.Size = new System.Drawing.Size(255, 569);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_end;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_start;
        private System.Windows.Forms.ListView SatelliteList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button RemoveSatellite;
        private System.Windows.Forms.Button AddSatellite;
        private System.Windows.Forms.CheckBox ASTGRunCheck;
        private System.Windows.Forms.Button Update;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}