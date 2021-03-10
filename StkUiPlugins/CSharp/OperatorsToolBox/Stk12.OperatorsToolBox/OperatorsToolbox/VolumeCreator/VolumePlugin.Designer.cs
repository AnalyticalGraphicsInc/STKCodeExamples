namespace OperatorsToolbox.VolumeCreator
{
    partial class VolumePlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VolumePlugin));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.DeleteLocation = new System.Windows.Forms.Button();
            this.LocationList = new System.Windows.Forms.ComboBox();
            this.CreateLocation = new System.Windows.Forms.Button();
            this.EditLocation = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ExistingVolumes = new System.Windows.Forms.ComboBox();
            this.Delete = new System.Windows.Forms.Button();
            this.CreateNew = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.Generate = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.ColorSelection = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.DeleteLocation);
            this.groupBox4.Controls.Add(this.LocationList);
            this.groupBox4.Controls.Add(this.CreateLocation);
            this.groupBox4.Controls.Add(this.EditLocation);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(29, 206);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(231, 146);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Location";
            // 
            // DeleteLocation
            // 
            this.DeleteLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteLocation.ForeColor = System.Drawing.Color.Black;
            this.DeleteLocation.Location = new System.Drawing.Point(28, 106);
            this.DeleteLocation.Margin = new System.Windows.Forms.Padding(2);
            this.DeleteLocation.Name = "DeleteLocation";
            this.DeleteLocation.Size = new System.Drawing.Size(162, 26);
            this.DeleteLocation.TabIndex = 9;
            this.DeleteLocation.Text = "Delete";
            this.DeleteLocation.UseVisualStyleBackColor = true;
            this.DeleteLocation.Click += new System.EventHandler(this.DeleteLocation_Click);
            // 
            // LocationList
            // 
            this.LocationList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LocationList.BackColor = System.Drawing.Color.DimGray;
            this.LocationList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LocationList.ForeColor = System.Drawing.Color.White;
            this.LocationList.FormattingEnabled = true;
            this.LocationList.Location = new System.Drawing.Point(10, 20);
            this.LocationList.Margin = new System.Windows.Forms.Padding(2);
            this.LocationList.Name = "LocationList";
            this.LocationList.Size = new System.Drawing.Size(196, 21);
            this.LocationList.TabIndex = 13;
            this.LocationList.SelectedIndexChanged += new System.EventHandler(this.LocationList_SelectedIndexChanged);
            // 
            // CreateLocation
            // 
            this.CreateLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateLocation.ForeColor = System.Drawing.Color.Black;
            this.CreateLocation.Location = new System.Drawing.Point(28, 44);
            this.CreateLocation.Margin = new System.Windows.Forms.Padding(2);
            this.CreateLocation.Name = "CreateLocation";
            this.CreateLocation.Size = new System.Drawing.Size(162, 26);
            this.CreateLocation.TabIndex = 7;
            this.CreateLocation.Text = "Create Location";
            this.CreateLocation.UseVisualStyleBackColor = true;
            this.CreateLocation.Click += new System.EventHandler(this.CreateLocation_Click);
            // 
            // EditLocation
            // 
            this.EditLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EditLocation.ForeColor = System.Drawing.Color.Black;
            this.EditLocation.Location = new System.Drawing.Point(28, 75);
            this.EditLocation.Margin = new System.Windows.Forms.Padding(2);
            this.EditLocation.Name = "EditLocation";
            this.EditLocation.Size = new System.Drawing.Size(162, 26);
            this.EditLocation.TabIndex = 8;
            this.EditLocation.Text = "Edit";
            this.EditLocation.UseVisualStyleBackColor = true;
            this.EditLocation.Click += new System.EventHandler(this.EditLocation_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ExistingVolumes);
            this.groupBox1.Controls.Add(this.Delete);
            this.groupBox1.Controls.Add(this.CreateNew);
            this.groupBox1.Controls.Add(this.Edit);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(29, 45);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(231, 157);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Volume Profile";
            // 
            // ExistingVolumes
            // 
            this.ExistingVolumes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExistingVolumes.BackColor = System.Drawing.Color.DimGray;
            this.ExistingVolumes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExistingVolumes.ForeColor = System.Drawing.Color.White;
            this.ExistingVolumes.FormattingEnabled = true;
            this.ExistingVolumes.Location = new System.Drawing.Point(12, 25);
            this.ExistingVolumes.Margin = new System.Windows.Forms.Padding(2);
            this.ExistingVolumes.Name = "ExistingVolumes";
            this.ExistingVolumes.Size = new System.Drawing.Size(205, 21);
            this.ExistingVolumes.TabIndex = 6;
            this.ExistingVolumes.SelectedIndexChanged += new System.EventHandler(this.ExistingMissiles_SelectedIndexChanged);
            // 
            // Delete
            // 
            this.Delete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Delete.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Delete.Location = new System.Drawing.Point(28, 111);
            this.Delete.Margin = new System.Windows.Forms.Padding(2);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(162, 26);
            this.Delete.TabIndex = 5;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // CreateNew
            // 
            this.CreateNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateNew.ForeColor = System.Drawing.SystemColors.Desktop;
            this.CreateNew.Location = new System.Drawing.Point(28, 50);
            this.CreateNew.Margin = new System.Windows.Forms.Padding(2);
            this.CreateNew.Name = "CreateNew";
            this.CreateNew.Size = new System.Drawing.Size(162, 26);
            this.CreateNew.TabIndex = 0;
            this.CreateNew.Text = "Create New Volume";
            this.CreateNew.UseVisualStyleBackColor = true;
            this.CreateNew.Click += new System.EventHandler(this.CreateNew_Click);
            // 
            // Edit
            // 
            this.Edit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Edit.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Edit.Location = new System.Drawing.Point(28, 80);
            this.Edit.Margin = new System.Windows.Forms.Padding(2);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(162, 26);
            this.Edit.TabIndex = 4;
            this.Edit.Text = "Edit";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Generate
            // 
            this.Generate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Generate.BackColor = System.Drawing.Color.SteelBlue;
            this.Generate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Generate.Location = new System.Drawing.Point(57, 401);
            this.Generate.Margin = new System.Windows.Forms.Padding(2);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(162, 26);
            this.Generate.TabIndex = 21;
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
            this.Cancel.Location = new System.Drawing.Point(268, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 23;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "x-mark.png");
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
            this.label13.Size = new System.Drawing.Size(156, 23);
            this.label13.TabIndex = 32;
            this.label13.Text = "Volume Creator";
            // 
            // ColorSelection
            // 
            this.ColorSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorSelection.BackColor = System.Drawing.Color.DimGray;
            this.ColorSelection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ColorSelection.ForeColor = System.Drawing.Color.White;
            this.ColorSelection.FormattingEnabled = true;
            this.ColorSelection.Location = new System.Drawing.Point(122, 361);
            this.ColorSelection.Margin = new System.Windows.Forms.Padding(2);
            this.ColorSelection.Name = "ColorSelection";
            this.ColorSelection.Size = new System.Drawing.Size(138, 21);
            this.ColorSelection.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(26, 362);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 33;
            this.label1.Text = "Volume Color:";
            // 
            // VolumePlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ColorSelection);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Generate);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VolumePlugin";
            this.Size = new System.Drawing.Size(294, 569);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button DeleteLocation;
        private System.Windows.Forms.ComboBox LocationList;
        private System.Windows.Forms.Button CreateLocation;
        private System.Windows.Forms.Button EditLocation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ExistingVolumes;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button CreateNew;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox ColorSelection;
        private System.Windows.Forms.Label label1;
    }
}