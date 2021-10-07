namespace OperatorsToolbox.Templates
{
    partial class TemplateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateForm));
            this.AvailableList = new System.Windows.Forms.ListView();
            this.AvObjectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TemplateList = new System.Windows.Forms.ListView();
            this.TpObjectNames = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AddToTemplate = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.RemoveFromTemplate = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TemplateName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PostImportBrowse = new System.Windows.Forms.Button();
            this.PostImportScriptPath = new System.Windows.Forms.TextBox();
            this.UsePostImportScript = new System.Windows.Forms.CheckBox();
            this.PreImportBrowse = new System.Windows.Forms.Button();
            this.PreImportScriptPath = new System.Windows.Forms.TextBox();
            this.UsePreImportScript = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.PreArgsText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PostArgsText = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AvailableList
            // 
            this.AvailableList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.AvailableList.BackColor = System.Drawing.Color.DimGray;
            this.AvailableList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AvailableList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AvObjectName});
            this.AvailableList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvailableList.ForeColor = System.Drawing.Color.White;
            this.AvailableList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.AvailableList.HideSelection = false;
            this.AvailableList.Location = new System.Drawing.Point(10, 64);
            this.AvailableList.Margin = new System.Windows.Forms.Padding(2);
            this.AvailableList.Name = "AvailableList";
            this.AvailableList.Size = new System.Drawing.Size(184, 294);
            this.AvailableList.TabIndex = 0;
            this.AvailableList.UseCompatibleStateImageBehavior = false;
            this.AvailableList.View = System.Windows.Forms.View.Details;
            // 
            // AvObjectName
            // 
            this.AvObjectName.Text = "";
            this.AvObjectName.Width = 240;
            // 
            // TemplateList
            // 
            this.TemplateList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TemplateList.BackColor = System.Drawing.Color.DimGray;
            this.TemplateList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TemplateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TpObjectNames});
            this.TemplateList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemplateList.ForeColor = System.Drawing.Color.White;
            this.TemplateList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.TemplateList.HideSelection = false;
            this.TemplateList.Location = new System.Drawing.Point(237, 65);
            this.TemplateList.Margin = new System.Windows.Forms.Padding(2);
            this.TemplateList.Name = "TemplateList";
            this.TemplateList.Size = new System.Drawing.Size(184, 294);
            this.TemplateList.TabIndex = 1;
            this.TemplateList.UseCompatibleStateImageBehavior = false;
            this.TemplateList.View = System.Windows.Forms.View.Details;
            // 
            // TpObjectNames
            // 
            this.TpObjectNames.Text = "";
            this.TpObjectNames.Width = 240;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(264, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Template Objects";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(35, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Available Objects";
            // 
            // AddToTemplate
            // 
            this.AddToTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddToTemplate.ImageKey = "right.png";
            this.AddToTemplate.ImageList = this.imageList1;
            this.AddToTemplate.Location = new System.Drawing.Point(198, 137);
            this.AddToTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.AddToTemplate.Name = "AddToTemplate";
            this.AddToTemplate.Size = new System.Drawing.Size(35, 35);
            this.AddToTemplate.TabIndex = 4;
            this.AddToTemplate.UseVisualStyleBackColor = true;
            this.AddToTemplate.Click += new System.EventHandler(this.AddToTemplate_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "left.png");
            this.imageList1.Images.SetKeyName(1, "right.png");
            // 
            // RemoveFromTemplate
            // 
            this.RemoveFromTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveFromTemplate.ImageKey = "left.png";
            this.RemoveFromTemplate.ImageList = this.imageList1;
            this.RemoveFromTemplate.Location = new System.Drawing.Point(198, 176);
            this.RemoveFromTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveFromTemplate.Name = "RemoveFromTemplate";
            this.RemoveFromTemplate.Size = new System.Drawing.Size(35, 35);
            this.RemoveFromTemplate.TabIndex = 5;
            this.RemoveFromTemplate.UseVisualStyleBackColor = true;
            this.RemoveFromTemplate.Click += new System.EventHandler(this.RemoveFromTemplate_Click);
            // 
            // Save
            // 
            this.Save.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Save.BackColor = System.Drawing.Color.SteelBlue;
            this.Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Save.ForeColor = System.Drawing.Color.White;
            this.Save.Location = new System.Drawing.Point(158, 499);
            this.Save.Margin = new System.Windows.Forms.Padding(2);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(114, 42);
            this.Save.TabIndex = 24;
            this.Save.Text = "Save Template";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(34, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Template Name:";
            // 
            // TemplateName
            // 
            this.TemplateName.BackColor = System.Drawing.Color.DimGray;
            this.TemplateName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TemplateName.ForeColor = System.Drawing.Color.White;
            this.TemplateName.Location = new System.Drawing.Point(158, 11);
            this.TemplateName.Margin = new System.Windows.Forms.Padding(2);
            this.TemplateName.Name = "TemplateName";
            this.TemplateName.Size = new System.Drawing.Size(171, 13);
            this.TemplateName.TabIndex = 26;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.PostArgsText);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.PostImportBrowse);
            this.groupBox1.Controls.Add(this.PostImportScriptPath);
            this.groupBox1.Controls.Add(this.PreArgsText);
            this.groupBox1.Controls.Add(this.UsePostImportScript);
            this.groupBox1.Controls.Add(this.PreImportBrowse);
            this.groupBox1.Controls.Add(this.PreImportScriptPath);
            this.groupBox1.Controls.Add(this.UsePreImportScript);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(10, 364);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 130);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Workflow Scripts";
            // 
            // PostImportBrowse
            // 
            this.PostImportBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PostImportBrowse.BackColor = System.Drawing.Color.SteelBlue;
            this.PostImportBrowse.Enabled = false;
            this.PostImportBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PostImportBrowse.ForeColor = System.Drawing.Color.White;
            this.PostImportBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PostImportBrowse.Location = new System.Drawing.Point(376, 73);
            this.PostImportBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.PostImportBrowse.Name = "PostImportBrowse";
            this.PostImportBrowse.Size = new System.Drawing.Size(28, 21);
            this.PostImportBrowse.TabIndex = 41;
            this.PostImportBrowse.Text = "...";
            this.PostImportBrowse.UseVisualStyleBackColor = false;
            this.PostImportBrowse.Click += new System.EventHandler(this.PostImportBrowse_Click);
            // 
            // PostImportScriptPath
            // 
            this.PostImportScriptPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PostImportScriptPath.BackColor = System.Drawing.Color.DimGray;
            this.PostImportScriptPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PostImportScriptPath.Enabled = false;
            this.PostImportScriptPath.ForeColor = System.Drawing.Color.White;
            this.PostImportScriptPath.Location = new System.Drawing.Point(142, 77);
            this.PostImportScriptPath.Margin = new System.Windows.Forms.Padding(2);
            this.PostImportScriptPath.Name = "PostImportScriptPath";
            this.PostImportScriptPath.Size = new System.Drawing.Size(230, 13);
            this.PostImportScriptPath.TabIndex = 40;
            // 
            // UsePostImportScript
            // 
            this.UsePostImportScript.AutoSize = true;
            this.UsePostImportScript.Location = new System.Drawing.Point(6, 76);
            this.UsePostImportScript.Name = "UsePostImportScript";
            this.UsePostImportScript.Size = new System.Drawing.Size(131, 17);
            this.UsePostImportScript.TabIndex = 39;
            this.UsePostImportScript.Text = "Use Post-Import Script";
            this.UsePostImportScript.UseVisualStyleBackColor = true;
            this.UsePostImportScript.CheckedChanged += new System.EventHandler(this.UsePostImportScript_CheckedChanged);
            // 
            // PreImportBrowse
            // 
            this.PreImportBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PreImportBrowse.BackColor = System.Drawing.Color.SteelBlue;
            this.PreImportBrowse.Enabled = false;
            this.PreImportBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PreImportBrowse.ForeColor = System.Drawing.Color.White;
            this.PreImportBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PreImportBrowse.Location = new System.Drawing.Point(376, 16);
            this.PreImportBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.PreImportBrowse.Name = "PreImportBrowse";
            this.PreImportBrowse.Size = new System.Drawing.Size(28, 21);
            this.PreImportBrowse.TabIndex = 38;
            this.PreImportBrowse.Text = "...";
            this.PreImportBrowse.UseVisualStyleBackColor = false;
            this.PreImportBrowse.Click += new System.EventHandler(this.PreImportBrowse_Click);
            // 
            // PreImportScriptPath
            // 
            this.PreImportScriptPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreImportScriptPath.BackColor = System.Drawing.Color.DimGray;
            this.PreImportScriptPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PreImportScriptPath.Enabled = false;
            this.PreImportScriptPath.ForeColor = System.Drawing.Color.White;
            this.PreImportScriptPath.Location = new System.Drawing.Point(142, 20);
            this.PreImportScriptPath.Margin = new System.Windows.Forms.Padding(2);
            this.PreImportScriptPath.Name = "PreImportScriptPath";
            this.PreImportScriptPath.Size = new System.Drawing.Size(230, 13);
            this.PreImportScriptPath.TabIndex = 28;
            // 
            // UsePreImportScript
            // 
            this.UsePreImportScript.AutoSize = true;
            this.UsePreImportScript.Location = new System.Drawing.Point(6, 19);
            this.UsePreImportScript.Name = "UsePreImportScript";
            this.UsePreImportScript.Size = new System.Drawing.Size(126, 17);
            this.UsePreImportScript.TabIndex = 0;
            this.UsePreImportScript.Text = "Use Pre-Import Script";
            this.UsePreImportScript.UseVisualStyleBackColor = true;
            this.UsePreImportScript.CheckedChanged += new System.EventHandler(this.UsePreImportScript_CheckedChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(47, 39);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(85, 13);
            this.label20.TabIndex = 43;
            this.label20.Text = "Argument String:";
            // 
            // PreArgsText
            // 
            this.PreArgsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreArgsText.BackColor = System.Drawing.Color.DimGray;
            this.PreArgsText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PreArgsText.ForeColor = System.Drawing.Color.White;
            this.PreArgsText.Location = new System.Drawing.Point(142, 39);
            this.PreArgsText.Margin = new System.Windows.Forms.Padding(2);
            this.PreArgsText.Name = "PreArgsText";
            this.PreArgsText.Size = new System.Drawing.Size(230, 13);
            this.PreArgsText.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(47, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "Argument String:";
            // 
            // PostArgsText
            // 
            this.PostArgsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PostArgsText.BackColor = System.Drawing.Color.DimGray;
            this.PostArgsText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PostArgsText.ForeColor = System.Drawing.Color.White;
            this.PostArgsText.Location = new System.Drawing.Point(142, 96);
            this.PostArgsText.Margin = new System.Windows.Forms.Padding(2);
            this.PostArgsText.Name = "PostArgsText";
            this.PostArgsText.Size = new System.Drawing.Size(230, 13);
            this.PostArgsText.TabIndex = 44;
            // 
            // TemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(430, 547);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TemplateName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.RemoveFromTemplate);
            this.Controls.Add(this.AddToTemplate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TemplateList);
            this.Controls.Add(this.AvailableList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TemplateForm";
            this.Text = "Template Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView AvailableList;
        private System.Windows.Forms.ListView TemplateList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddToTemplate;
        private System.Windows.Forms.Button RemoveFromTemplate;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TemplateName;
        private System.Windows.Forms.ColumnHeader AvObjectName;
        private System.Windows.Forms.ColumnHeader TpObjectNames;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox PreImportScriptPath;
        private System.Windows.Forms.CheckBox UsePreImportScript;
        private System.Windows.Forms.Button PreImportBrowse;
        private System.Windows.Forms.Button PostImportBrowse;
        private System.Windows.Forms.TextBox PostImportScriptPath;
        private System.Windows.Forms.CheckBox UsePostImportScript;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PostArgsText;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox PreArgsText;
    }
}