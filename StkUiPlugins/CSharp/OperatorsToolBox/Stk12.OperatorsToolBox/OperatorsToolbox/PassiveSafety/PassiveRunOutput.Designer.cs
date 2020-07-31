namespace OperatorsToolbox.PassiveSafety
{
    partial class PassiveRunOutput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassiveRunOutput));
            this.ProxGeometry = new System.Windows.Forms.CheckBox();
            this.DisplaySelected = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ManeuverNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ManeuverTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MinRange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MinRadial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MinIntrack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MinCrosstrack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ProxGeometry
            // 
            this.ProxGeometry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProxGeometry.AutoSize = true;
            this.ProxGeometry.ForeColor = System.Drawing.Color.White;
            this.ProxGeometry.Location = new System.Drawing.Point(129, 355);
            this.ProxGeometry.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ProxGeometry.Name = "ProxGeometry";
            this.ProxGeometry.Size = new System.Drawing.Size(152, 17);
            this.ProxGeometry.TabIndex = 5;
            this.ProxGeometry.Text = "Display Proximity Geometry";
            this.ProxGeometry.UseVisualStyleBackColor = true;
            this.ProxGeometry.CheckedChanged += new System.EventHandler(this.ProxGeometry_CheckedChanged);
            // 
            // DisplaySelected
            // 
            this.DisplaySelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DisplaySelected.BackColor = System.Drawing.Color.SteelBlue;
            this.DisplaySelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DisplaySelected.ForeColor = System.Drawing.Color.White;
            this.DisplaySelected.Location = new System.Drawing.Point(9, 348);
            this.DisplaySelected.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DisplaySelected.Name = "DisplaySelected";
            this.DisplaySelected.Size = new System.Drawing.Size(106, 30);
            this.DisplaySelected.TabIndex = 4;
            this.DisplaySelected.Text = "DIsplay Selected";
            this.DisplaySelected.UseVisualStyleBackColor = false;
            this.DisplaySelected.Click += new System.EventHandler(this.DisplaySelected_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.DimGray;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ManeuverNum,
            this.ManeuverTime,
            this.MinRange,
            this.MinRadial,
            this.MinIntrack,
            this.MinCrosstrack});
            this.listView1.ForeColor = System.Drawing.Color.White;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(9, 5);
            this.listView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(537, 334);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // ManeuverNum
            // 
            this.ManeuverNum.Text = "Number";
            // 
            // ManeuverTime
            // 
            this.ManeuverTime.Text = "Maneuver Time [UTCG]";
            this.ManeuverTime.Width = 175;
            // 
            // MinRange
            // 
            this.MinRange.Text = "Min Range [km]";
            this.MinRange.Width = 110;
            // 
            // MinRadial
            // 
            this.MinRadial.Text = "Min Radial [km]";
            this.MinRadial.Width = 110;
            // 
            // MinIntrack
            // 
            this.MinIntrack.Text = "Min Intrack [km]";
            this.MinIntrack.Width = 110;
            // 
            // MinCrosstrack
            // 
            this.MinCrosstrack.Text = "Min Crosstrack [km]";
            this.MinCrosstrack.Width = 132;
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.imageList1;
            this.Cancel.Location = new System.Drawing.Point(548, 3);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 28;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "x-mark.png");
            // 
            // PassiveRunOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(574, 387);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.ProxGeometry);
            this.Controls.Add(this.DisplaySelected);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PassiveRunOutput";
            this.Text = "PassiveRunOutput";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ProxGeometry;
        private System.Windows.Forms.Button DisplaySelected;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ManeuverNum;
        private System.Windows.Forms.ColumnHeader ManeuverTime;
        private System.Windows.Forms.ColumnHeader MinRange;
        private System.Windows.Forms.ColumnHeader MinRadial;
        private System.Windows.Forms.ColumnHeader MinIntrack;
        private System.Windows.Forms.ColumnHeader MinCrosstrack;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ImageList imageList1;
    }
}