namespace OperatorsToolbox.Coverage
{
    partial class CoveragePlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoveragePlugin));
            this.label13 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.EditCoverage = new System.Windows.Forms.Button();
            this.CoverageList = new System.Windows.Forms.ListView();
            this.CoverageName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.CoverageDetails = new System.Windows.Forms.TextBox();
            this.RemoveCoverage = new System.Windows.Forms.Button();
            this.NewCoverage = new System.Windows.Forms.Button();
            this.HomeView = new System.Windows.Forms.Button();
            this.Compute = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ClearLegends = new System.Windows.Forms.Button();
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
            this.label13.Size = new System.Drawing.Size(104, 23);
            this.label13.TabIndex = 33;
            this.label13.Text = "Coverage";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ForeColor = System.Drawing.Color.White;
            this.Cancel.ImageIndex = 3;
            this.Cancel.ImageList = this.imageList3;
            this.Cancel.Location = new System.Drawing.Point(318, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 32;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "add.png");
            this.imageList3.Images.SetKeyName(1, "paint-brush.png");
            this.imageList3.Images.SetKeyName(2, "delete.png");
            this.imageList3.Images.SetKeyName(3, "x-mark.png");
            this.imageList3.Images.SetKeyName(4, "refresh.png");
            this.imageList3.Images.SetKeyName(5, "home.png");
            this.imageList3.Images.SetKeyName(6, "broom.png");
            // 
            // EditCoverage
            // 
            this.EditCoverage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EditCoverage.ImageIndex = 1;
            this.EditCoverage.ImageList = this.imageList3;
            this.EditCoverage.Location = new System.Drawing.Point(-2, 86);
            this.EditCoverage.Margin = new System.Windows.Forms.Padding(2);
            this.EditCoverage.Name = "EditCoverage";
            this.EditCoverage.Size = new System.Drawing.Size(40, 40);
            this.EditCoverage.TabIndex = 47;
            this.EditCoverage.UseVisualStyleBackColor = true;
            this.EditCoverage.Click += new System.EventHandler(this.EditCoverage_Click);
            this.EditCoverage.MouseHover += new System.EventHandler(this.EditCoverage_MouseHover);
            // 
            // CoverageList
            // 
            this.CoverageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CoverageList.BackColor = System.Drawing.Color.DimGray;
            this.CoverageList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CoverageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CoverageName});
            this.CoverageList.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CoverageList.ForeColor = System.Drawing.Color.White;
            this.CoverageList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.CoverageList.HideSelection = false;
            this.CoverageList.LargeImageList = this.imageList3;
            this.CoverageList.Location = new System.Drawing.Point(40, 48);
            this.CoverageList.Margin = new System.Windows.Forms.Padding(2);
            this.CoverageList.MultiSelect = false;
            this.CoverageList.Name = "CoverageList";
            this.CoverageList.Size = new System.Drawing.Size(301, 265);
            this.CoverageList.SmallImageList = this.imageList3;
            this.CoverageList.TabIndex = 46;
            this.CoverageList.UseCompatibleStateImageBehavior = false;
            this.CoverageList.View = System.Windows.Forms.View.Details;
            this.CoverageList.SelectedIndexChanged += new System.EventHandler(this.CoverageList_SelectedIndexChanged);
            // 
            // CoverageName
            // 
            this.CoverageName.Width = 275;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(7, 318);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 17);
            this.label1.TabIndex = 45;
            this.label1.Text = "Coverage Details";
            // 
            // CoverageDetails
            // 
            this.CoverageDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CoverageDetails.BackColor = System.Drawing.Color.DimGray;
            this.CoverageDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CoverageDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CoverageDetails.ForeColor = System.Drawing.Color.White;
            this.CoverageDetails.Location = new System.Drawing.Point(9, 342);
            this.CoverageDetails.Margin = new System.Windows.Forms.Padding(2);
            this.CoverageDetails.Multiline = true;
            this.CoverageDetails.Name = "CoverageDetails";
            this.CoverageDetails.ReadOnly = true;
            this.CoverageDetails.Size = new System.Drawing.Size(333, 163);
            this.CoverageDetails.TabIndex = 44;
            // 
            // RemoveCoverage
            // 
            this.RemoveCoverage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveCoverage.ImageIndex = 2;
            this.RemoveCoverage.ImageList = this.imageList3;
            this.RemoveCoverage.Location = new System.Drawing.Point(-2, 174);
            this.RemoveCoverage.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveCoverage.Name = "RemoveCoverage";
            this.RemoveCoverage.Size = new System.Drawing.Size(40, 40);
            this.RemoveCoverage.TabIndex = 43;
            this.RemoveCoverage.UseVisualStyleBackColor = true;
            this.RemoveCoverage.Click += new System.EventHandler(this.RemoveCoverage_Click);
            this.RemoveCoverage.MouseHover += new System.EventHandler(this.RemoveCoverage_MouseHover);
            // 
            // NewCoverage
            // 
            this.NewCoverage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NewCoverage.ImageIndex = 0;
            this.NewCoverage.ImageList = this.imageList3;
            this.NewCoverage.Location = new System.Drawing.Point(-2, 42);
            this.NewCoverage.Margin = new System.Windows.Forms.Padding(2);
            this.NewCoverage.Name = "NewCoverage";
            this.NewCoverage.Size = new System.Drawing.Size(40, 40);
            this.NewCoverage.TabIndex = 42;
            this.NewCoverage.UseVisualStyleBackColor = true;
            this.NewCoverage.Click += new System.EventHandler(this.NewCoverage_Click);
            this.NewCoverage.MouseHover += new System.EventHandler(this.NewCoverage_MouseHover);
            // 
            // HomeView
            // 
            this.HomeView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.HomeView.ImageIndex = 5;
            this.HomeView.ImageList = this.imageList3;
            this.HomeView.Location = new System.Drawing.Point(-2, 218);
            this.HomeView.Margin = new System.Windows.Forms.Padding(2);
            this.HomeView.Name = "HomeView";
            this.HomeView.Size = new System.Drawing.Size(40, 40);
            this.HomeView.TabIndex = 41;
            this.HomeView.UseVisualStyleBackColor = true;
            this.HomeView.Click += new System.EventHandler(this.HomeView_Click);
            this.HomeView.MouseHover += new System.EventHandler(this.HomeView_MouseHover);
            // 
            // Compute
            // 
            this.Compute.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Compute.ImageIndex = 4;
            this.Compute.ImageList = this.imageList3;
            this.Compute.Location = new System.Drawing.Point(-2, 130);
            this.Compute.Margin = new System.Windows.Forms.Padding(2);
            this.Compute.Name = "Compute";
            this.Compute.Size = new System.Drawing.Size(40, 40);
            this.Compute.TabIndex = 48;
            this.Compute.UseVisualStyleBackColor = true;
            this.Compute.Click += new System.EventHandler(this.Compute_Click);
            this.Compute.MouseHover += new System.EventHandler(this.Compute_MouseHover);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ClearLegends
            // 
            this.ClearLegends.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearLegends.ImageIndex = 6;
            this.ClearLegends.ImageList = this.imageList3;
            this.ClearLegends.Location = new System.Drawing.Point(-2, 262);
            this.ClearLegends.Margin = new System.Windows.Forms.Padding(2);
            this.ClearLegends.Name = "ClearLegends";
            this.ClearLegends.Size = new System.Drawing.Size(40, 40);
            this.ClearLegends.TabIndex = 49;
            this.ClearLegends.UseVisualStyleBackColor = true;
            this.ClearLegends.Click += new System.EventHandler(this.ClearLegends_Click);
            this.ClearLegends.MouseHover += new System.EventHandler(this.ClearLegends_MouseHover);
            // 
            // CoveragePlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.ClearLegends);
            this.Controls.Add(this.Compute);
            this.Controls.Add(this.EditCoverage);
            this.Controls.Add(this.CoverageList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CoverageDetails);
            this.Controls.Add(this.RemoveCoverage);
            this.Controls.Add(this.NewCoverage);
            this.Controls.Add(this.HomeView);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Cancel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CoveragePlugin";
            this.Size = new System.Drawing.Size(344, 624);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.Button EditCoverage;
        private System.Windows.Forms.ListView CoverageList;
        private System.Windows.Forms.ColumnHeader CoverageName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CoverageDetails;
        private System.Windows.Forms.Button RemoveCoverage;
        private System.Windows.Forms.Button NewCoverage;
        private System.Windows.Forms.Button HomeView;
        private System.Windows.Forms.Button Compute;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button ClearLegends;
    }
}