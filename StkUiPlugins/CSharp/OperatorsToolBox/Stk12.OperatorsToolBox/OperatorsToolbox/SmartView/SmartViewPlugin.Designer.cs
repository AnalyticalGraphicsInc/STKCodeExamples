namespace OperatorsToolbox.SmartView
{
    partial class SmartViewPlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmartViewPlugin));
            this.SelectView = new System.Windows.Forms.Button();
            this.Refresh = new System.Windows.Forms.Button();
            this.SideButtons = new System.Windows.Forms.ImageList(this.components);
            this.Reset = new System.Windows.Forms.Button();
            this.StoredViewList = new System.Windows.Forms.ListView();
            this.ViewName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EditView = new System.Windows.Forms.Button();
            this.RemoveView = new System.Windows.Forms.Button();
            this.NewView = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.x = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // SelectView
            // 
            this.SelectView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectView.BackColor = System.Drawing.Color.SteelBlue;
            this.SelectView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectView.ForeColor = System.Drawing.Color.White;
            this.SelectView.Location = new System.Drawing.Point(9, 252);
            this.SelectView.Margin = new System.Windows.Forms.Padding(2);
            this.SelectView.Name = "SelectView";
            this.SelectView.Size = new System.Drawing.Size(96, 31);
            this.SelectView.TabIndex = 15;
            this.SelectView.Text = "Select View";
            this.SelectView.UseVisualStyleBackColor = false;
            this.SelectView.Click += new System.EventHandler(this.SelectView_Click);
            // 
            // Refresh
            // 
            this.Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Refresh.ImageIndex = 3;
            this.Refresh.ImageList = this.SideButtons;
            this.Refresh.Location = new System.Drawing.Point(266, 164);
            this.Refresh.Margin = new System.Windows.Forms.Padding(2);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(40, 40);
            this.Refresh.TabIndex = 14;
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            this.Refresh.MouseHover += new System.EventHandler(this.Refresh_MouseHover);
            // 
            // SideButtons
            // 
            this.SideButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SideButtons.ImageStream")));
            this.SideButtons.TransparentColor = System.Drawing.Color.Transparent;
            this.SideButtons.Images.SetKeyName(0, "add.png");
            this.SideButtons.Images.SetKeyName(1, "delete.png");
            this.SideButtons.Images.SetKeyName(2, "paint-brush.png");
            this.SideButtons.Images.SetKeyName(3, "refresh.png");
            this.SideButtons.Images.SetKeyName(4, "reset.png");
            // 
            // Reset
            // 
            this.Reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Reset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Reset.ImageIndex = 4;
            this.Reset.ImageList = this.SideButtons;
            this.Reset.Location = new System.Drawing.Point(266, 208);
            this.Reset.Margin = new System.Windows.Forms.Padding(2);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(40, 40);
            this.Reset.TabIndex = 13;
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            this.Reset.MouseHover += new System.EventHandler(this.Reset_MouseHover);
            // 
            // StoredViewList
            // 
            this.StoredViewList.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.StoredViewList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StoredViewList.BackColor = System.Drawing.SystemColors.Desktop;
            this.StoredViewList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ViewName,
            this.Type});
            this.StoredViewList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StoredViewList.ForeColor = System.Drawing.SystemColors.Window;
            this.StoredViewList.FullRowSelect = true;
            this.StoredViewList.GridLines = true;
            this.StoredViewList.HideSelection = false;
            this.StoredViewList.Location = new System.Drawing.Point(9, 32);
            this.StoredViewList.Margin = new System.Windows.Forms.Padding(2);
            this.StoredViewList.MultiSelect = false;
            this.StoredViewList.Name = "StoredViewList";
            this.StoredViewList.Size = new System.Drawing.Size(254, 216);
            this.StoredViewList.TabIndex = 12;
            this.StoredViewList.UseCompatibleStateImageBehavior = false;
            this.StoredViewList.View = System.Windows.Forms.View.Details;
            this.StoredViewList.SelectedIndexChanged += new System.EventHandler(this.StoredViewList_SelectedIndexChanged);
            // 
            // ViewName
            // 
            this.ViewName.Text = "View Name";
            this.ViewName.Width = 165;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 85;
            // 
            // EditView
            // 
            this.EditView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EditView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EditView.ImageIndex = 2;
            this.EditView.ImageList = this.SideButtons;
            this.EditView.Location = new System.Drawing.Point(266, 76);
            this.EditView.Margin = new System.Windows.Forms.Padding(2);
            this.EditView.Name = "EditView";
            this.EditView.Size = new System.Drawing.Size(40, 40);
            this.EditView.TabIndex = 11;
            this.EditView.UseVisualStyleBackColor = true;
            this.EditView.Click += new System.EventHandler(this.EditView_Click);
            this.EditView.MouseHover += new System.EventHandler(this.EditView_MouseHover);
            // 
            // RemoveView
            // 
            this.RemoveView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveView.ImageIndex = 1;
            this.RemoveView.ImageList = this.SideButtons;
            this.RemoveView.Location = new System.Drawing.Point(266, 120);
            this.RemoveView.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveView.Name = "RemoveView";
            this.RemoveView.Size = new System.Drawing.Size(40, 40);
            this.RemoveView.TabIndex = 10;
            this.RemoveView.UseVisualStyleBackColor = true;
            this.RemoveView.Click += new System.EventHandler(this.RemoveView_Click);
            this.RemoveView.MouseHover += new System.EventHandler(this.RemoveView_MouseHover);
            // 
            // NewView
            // 
            this.NewView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NewView.ImageIndex = 0;
            this.NewView.ImageList = this.SideButtons;
            this.NewView.Location = new System.Drawing.Point(266, 32);
            this.NewView.Margin = new System.Windows.Forms.Padding(2);
            this.NewView.Name = "NewView";
            this.NewView.Size = new System.Drawing.Size(40, 40);
            this.NewView.TabIndex = 9;
            this.NewView.UseVisualStyleBackColor = true;
            this.NewView.Click += new System.EventHandler(this.NewView_Click);
            this.NewView.MouseHover += new System.EventHandler(this.NewView_MouseHover);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.x;
            this.Cancel.Location = new System.Drawing.Point(291, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 28;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // x
            // 
            this.x.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("x.ImageStream")));
            this.x.TransparentColor = System.Drawing.Color.Transparent;
            this.x.Images.SetKeyName(0, "x-mark.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Firebrick;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 23);
            this.label1.TabIndex = 29;
            this.label1.Text = "Smart View";
            // 
            // ButtonInfo
            // 
            this.ButtonInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ButtonInfo.Name = "ButtonInfo";
            this.ButtonInfo.Size = new System.Drawing.Size(61, 4);
            // 
            // SmartViewPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.SelectView);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.StoredViewList);
            this.Controls.Add(this.EditView);
            this.Controls.Add(this.RemoveView);
            this.Controls.Add(this.NewView);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SmartViewPlugin";
            this.Size = new System.Drawing.Size(317, 569);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectView;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.ListView StoredViewList;
        private System.Windows.Forms.ColumnHeader ViewName;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.Button EditView;
        private System.Windows.Forms.Button RemoveView;
        private System.Windows.Forms.Button NewView;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ImageList x;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList SideButtons;
        private System.Windows.Forms.ContextMenuStrip ButtonInfo;
    }
}