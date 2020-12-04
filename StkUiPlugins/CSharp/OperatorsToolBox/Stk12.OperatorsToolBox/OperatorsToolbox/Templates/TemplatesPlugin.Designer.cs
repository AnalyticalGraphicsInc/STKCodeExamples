namespace OperatorsToolbox.Templates
{
    partial class TemplatesPlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplatesPlugin));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.TemplateList = new System.Windows.Forms.ListView();
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RemoveTemplate = new System.Windows.Forms.Button();
            this.NewTemplate = new System.Windows.Forms.Button();
            this.ObjectList = new System.Windows.Forms.TreeView();
            this.Generate = new System.Windows.Forms.Button();
            this.EraseReplace = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "x-mark.png");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Firebrick;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 23);
            this.label3.TabIndex = 32;
            this.label3.Text = "Templates";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.imageList1;
            this.Cancel.Location = new System.Drawing.Point(264, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 31;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "add.png");
            this.imageList3.Images.SetKeyName(1, "delete.png");
            this.imageList3.Images.SetKeyName(2, "paint-brush.png");
            this.imageList3.Images.SetKeyName(3, "x-mark.png");
            this.imageList3.Images.SetKeyName(4, "home.png");
            this.imageList3.Images.SetKeyName(5, "refresh.png");
            // 
            // TemplateList
            // 
            this.TemplateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TemplateList.BackColor = System.Drawing.Color.DimGray;
            this.TemplateList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TemplateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Type});
            this.TemplateList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemplateList.ForeColor = System.Drawing.Color.White;
            this.TemplateList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.TemplateList.HideSelection = false;
            this.TemplateList.Location = new System.Drawing.Point(40, 37);
            this.TemplateList.Margin = new System.Windows.Forms.Padding(2);
            this.TemplateList.MultiSelect = false;
            this.TemplateList.Name = "TemplateList";
            this.TemplateList.Size = new System.Drawing.Size(247, 191);
            this.TemplateList.TabIndex = 50;
            this.TemplateList.UseCompatibleStateImageBehavior = false;
            this.TemplateList.View = System.Windows.Forms.View.Details;
            this.TemplateList.SelectedIndexChanged += new System.EventHandler(this.TemplateList_SelectedIndexChanged);
            // 
            // Type
            // 
            this.Type.Text = "";
            this.Type.Width = 280;
            // 
            // RemoveTemplate
            // 
            this.RemoveTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveTemplate.ImageIndex = 1;
            this.RemoveTemplate.ImageList = this.imageList3;
            this.RemoveTemplate.Location = new System.Drawing.Point(-2, 75);
            this.RemoveTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveTemplate.Name = "RemoveTemplate";
            this.RemoveTemplate.Size = new System.Drawing.Size(40, 40);
            this.RemoveTemplate.TabIndex = 49;
            this.RemoveTemplate.UseVisualStyleBackColor = true;
            this.RemoveTemplate.Click += new System.EventHandler(this.RemoveTemplate_Click);
            // 
            // NewTemplate
            // 
            this.NewTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NewTemplate.ImageIndex = 0;
            this.NewTemplate.ImageList = this.imageList3;
            this.NewTemplate.Location = new System.Drawing.Point(-2, 37);
            this.NewTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.NewTemplate.Name = "NewTemplate";
            this.NewTemplate.Size = new System.Drawing.Size(40, 40);
            this.NewTemplate.TabIndex = 48;
            this.NewTemplate.UseVisualStyleBackColor = true;
            this.NewTemplate.Click += new System.EventHandler(this.NewTemplate_Click);
            // 
            // ObjectList
            // 
            this.ObjectList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectList.BackColor = System.Drawing.Color.DimGray;
            this.ObjectList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ObjectList.CheckBoxes = true;
            this.ObjectList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ObjectList.ForeColor = System.Drawing.Color.White;
            this.ObjectList.Location = new System.Drawing.Point(4, 232);
            this.ObjectList.Margin = new System.Windows.Forms.Padding(2);
            this.ObjectList.Name = "ObjectList";
            this.ObjectList.Size = new System.Drawing.Size(284, 226);
            this.ObjectList.TabIndex = 52;
            // 
            // Generate
            // 
            this.Generate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Generate.BackColor = System.Drawing.Color.SteelBlue;
            this.Generate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Generate.ForeColor = System.Drawing.Color.White;
            this.Generate.Location = new System.Drawing.Point(69, 495);
            this.Generate.Margin = new System.Windows.Forms.Padding(2);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(149, 42);
            this.Generate.TabIndex = 53;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = false;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // EraseReplace
            // 
            this.EraseReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EraseReplace.AutoSize = true;
            this.EraseReplace.Checked = true;
            this.EraseReplace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EraseReplace.ForeColor = System.Drawing.Color.White;
            this.EraseReplace.Location = new System.Drawing.Point(4, 463);
            this.EraseReplace.Name = "EraseReplace";
            this.EraseReplace.Size = new System.Drawing.Size(214, 17);
            this.EraseReplace.TabIndex = 54;
            this.EraseReplace.Text = "If Object Exists then Erase and Replace";
            this.EraseReplace.UseVisualStyleBackColor = true;
            // 
            // TemplatesPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.EraseReplace);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.ObjectList);
            this.Controls.Add(this.TemplateList);
            this.Controls.Add(this.RemoveTemplate);
            this.Controls.Add(this.NewTemplate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cancel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TemplatesPlugin";
            this.Size = new System.Drawing.Size(290, 607);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ListView TemplateList;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.Button RemoveTemplate;
        private System.Windows.Forms.Button NewTemplate;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.TreeView ObjectList;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.CheckBox EraseReplace;
    }
}