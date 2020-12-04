namespace OperatorsToolbox.GroundEvents
{
    partial class GroundEventsPlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroundEventsPlugin));
            this.label13 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SubObjectsClick = new System.Windows.Forms.Button();
            this.EditSSR = new System.Windows.Forms.Button();
            this.ListSSR = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Icon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.SSRDetails = new System.Windows.Forms.TextBox();
            this.RemoveSSR = new System.Windows.Forms.Button();
            this.NewSSR = new System.Windows.Forms.Button();
            this.HomeView = new System.Windows.Forms.Button();
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
            this.label13.Size = new System.Drawing.Size(142, 23);
            this.label13.TabIndex = 33;
            this.label13.Text = "Ground Events";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ForeColor = System.Drawing.Color.White;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.imageList2;
            this.Cancel.Location = new System.Drawing.Point(314, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 32;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "x-mark.png");
            this.imageList2.Images.SetKeyName(1, "add.png");
            this.imageList2.Images.SetKeyName(2, "delete.png");
            this.imageList2.Images.SetKeyName(3, "layers.png");
            this.imageList2.Images.SetKeyName(4, "paint-brush.png");
            this.imageList2.Images.SetKeyName(5, "home.png");
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SubObjectsClick
            // 
            this.SubObjectsClick.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SubObjectsClick.ImageIndex = 3;
            this.SubObjectsClick.ImageList = this.imageList2;
            this.SubObjectsClick.Location = new System.Drawing.Point(-2, 135);
            this.SubObjectsClick.Margin = new System.Windows.Forms.Padding(2);
            this.SubObjectsClick.Name = "SubObjectsClick";
            this.SubObjectsClick.Size = new System.Drawing.Size(40, 40);
            this.SubObjectsClick.TabIndex = 41;
            this.SubObjectsClick.UseVisualStyleBackColor = true;
            this.SubObjectsClick.Click += new System.EventHandler(this.SubObjectsClick_Click);
            this.SubObjectsClick.MouseHover += new System.EventHandler(this.SubObjectsClick_MouseHover);
            // 
            // EditSSR
            // 
            this.EditSSR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EditSSR.ImageIndex = 4;
            this.EditSSR.ImageList = this.imageList2;
            this.EditSSR.Location = new System.Drawing.Point(-2, 98);
            this.EditSSR.Margin = new System.Windows.Forms.Padding(2);
            this.EditSSR.Name = "EditSSR";
            this.EditSSR.Size = new System.Drawing.Size(40, 40);
            this.EditSSR.TabIndex = 40;
            this.EditSSR.UseVisualStyleBackColor = true;
            this.EditSSR.Click += new System.EventHandler(this.EditSSR_Click);
            this.EditSSR.MouseHover += new System.EventHandler(this.EditSSR_MouseHover);
            // 
            // ListSSR
            // 
            this.ListSSR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListSSR.BackColor = System.Drawing.Color.DimGray;
            this.ListSSR.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListSSR.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Icon});
            this.ListSSR.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListSSR.ForeColor = System.Drawing.Color.White;
            this.ListSSR.FullRowSelect = true;
            this.ListSSR.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ListSSR.HideSelection = false;
            this.ListSSR.LargeImageList = this.imageList1;
            this.ListSSR.Location = new System.Drawing.Point(40, 59);
            this.ListSSR.Margin = new System.Windows.Forms.Padding(2);
            this.ListSSR.MultiSelect = false;
            this.ListSSR.Name = "ListSSR";
            this.ListSSR.Size = new System.Drawing.Size(297, 328);
            this.ListSSR.SmallImageList = this.imageList1;
            this.ListSSR.TabIndex = 39;
            this.ListSSR.UseCompatibleStateImageBehavior = false;
            this.ListSSR.View = System.Windows.Forms.View.Details;
            this.ListSSR.SelectedIndexChanged += new System.EventHandler(this.ListSSR_SelectedIndexChanged);
            // 
            // ID
            // 
            this.ID.Text = "";
            this.ID.Width = 275;
            // 
            // Icon
            // 
            this.Icon.Text = "";
            this.Icon.Width = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(7, 397);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 38;
            this.label1.Text = "Event Details";
            // 
            // SSRDetails
            // 
            this.SSRDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SSRDetails.BackColor = System.Drawing.Color.DimGray;
            this.SSRDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SSRDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSRDetails.ForeColor = System.Drawing.Color.White;
            this.SSRDetails.Location = new System.Drawing.Point(9, 420);
            this.SSRDetails.Margin = new System.Windows.Forms.Padding(2);
            this.SSRDetails.Multiline = true;
            this.SSRDetails.Name = "SSRDetails";
            this.SSRDetails.ReadOnly = true;
            this.SSRDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SSRDetails.Size = new System.Drawing.Size(329, 170);
            this.SSRDetails.TabIndex = 37;
            // 
            // RemoveSSR
            // 
            this.RemoveSSR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveSSR.ImageIndex = 2;
            this.RemoveSSR.ImageList = this.imageList2;
            this.RemoveSSR.Location = new System.Drawing.Point(-2, 172);
            this.RemoveSSR.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveSSR.Name = "RemoveSSR";
            this.RemoveSSR.Size = new System.Drawing.Size(40, 40);
            this.RemoveSSR.TabIndex = 36;
            this.RemoveSSR.UseVisualStyleBackColor = true;
            this.RemoveSSR.Click += new System.EventHandler(this.RemoveSSR_Click);
            this.RemoveSSR.MouseHover += new System.EventHandler(this.RemoveSSR_MouseHover);
            // 
            // NewSSR
            // 
            this.NewSSR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NewSSR.ImageIndex = 1;
            this.NewSSR.ImageList = this.imageList2;
            this.NewSSR.Location = new System.Drawing.Point(-2, 60);
            this.NewSSR.Margin = new System.Windows.Forms.Padding(2);
            this.NewSSR.Name = "NewSSR";
            this.NewSSR.Size = new System.Drawing.Size(40, 40);
            this.NewSSR.TabIndex = 35;
            this.NewSSR.UseVisualStyleBackColor = true;
            this.NewSSR.Click += new System.EventHandler(this.NewSSR_Click);
            this.NewSSR.MouseHover += new System.EventHandler(this.NewSSR_MouseHover);
            // 
            // HomeView
            // 
            this.HomeView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.HomeView.ImageIndex = 5;
            this.HomeView.ImageList = this.imageList2;
            this.HomeView.Location = new System.Drawing.Point(-2, 210);
            this.HomeView.Margin = new System.Windows.Forms.Padding(2);
            this.HomeView.Name = "HomeView";
            this.HomeView.Size = new System.Drawing.Size(40, 40);
            this.HomeView.TabIndex = 34;
            this.HomeView.UseVisualStyleBackColor = true;
            this.HomeView.Click += new System.EventHandler(this.HomeView_Click);
            this.HomeView.MouseHover += new System.EventHandler(this.HomeView_MouseHover);
            // 
            // GroundEventsPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.SubObjectsClick);
            this.Controls.Add(this.EditSSR);
            this.Controls.Add(this.ListSSR);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SSRDetails);
            this.Controls.Add(this.RemoveSSR);
            this.Controls.Add(this.NewSSR);
            this.Controls.Add(this.HomeView);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Cancel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GroundEventsPlugin";
            this.Size = new System.Drawing.Size(340, 700);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button SubObjectsClick;
        private System.Windows.Forms.Button EditSSR;
        private System.Windows.Forms.ListView ListSSR;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Icon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SSRDetails;
        private System.Windows.Forms.Button RemoveSSR;
        private System.Windows.Forms.Button NewSSR;
        private System.Windows.Forms.Button HomeView;
        private System.Windows.Forms.ImageList imageList2;
    }
}