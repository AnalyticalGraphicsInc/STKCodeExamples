namespace OperatorsToolbox.Templates
{
    partial class ModifyScriptForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifyScriptForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PostImportBrowse = new System.Windows.Forms.Button();
            this.PostImportScriptPath = new System.Windows.Forms.TextBox();
            this.UsePostImportScript = new System.Windows.Forms.CheckBox();
            this.PreImportBrowse = new System.Windows.Forms.Button();
            this.PreImportScriptPath = new System.Windows.Forms.TextBox();
            this.UsePreImportScript = new System.Windows.Forms.CheckBox();
            this.Save = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.PostArgsText = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.PreArgsText = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.TestingArgsText = new System.Windows.Forms.TextBox();
            this.TestingScriptText = new System.Windows.Forms.TextBox();
            this.TestingBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ExecuteTestScript = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.PostArgsText);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.PreArgsText);
            this.groupBox1.Controls.Add(this.PostImportBrowse);
            this.groupBox1.Controls.Add(this.PostImportScriptPath);
            this.groupBox1.Controls.Add(this.UsePostImportScript);
            this.groupBox1.Controls.Add(this.PreImportBrowse);
            this.groupBox1.Controls.Add(this.PreImportScriptPath);
            this.groupBox1.Controls.Add(this.UsePreImportScript);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 125);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Script Options";
            // 
            // PostImportBrowse
            // 
            this.PostImportBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PostImportBrowse.BackColor = System.Drawing.Color.SteelBlue;
            this.PostImportBrowse.Enabled = false;
            this.PostImportBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PostImportBrowse.ForeColor = System.Drawing.Color.White;
            this.PostImportBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PostImportBrowse.Location = new System.Drawing.Point(371, 72);
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
            this.PostImportScriptPath.Location = new System.Drawing.Point(142, 76);
            this.PostImportScriptPath.Margin = new System.Windows.Forms.Padding(2);
            this.PostImportScriptPath.Name = "PostImportScriptPath";
            this.PostImportScriptPath.Size = new System.Drawing.Size(225, 13);
            this.PostImportScriptPath.TabIndex = 40;
            // 
            // UsePostImportScript
            // 
            this.UsePostImportScript.AutoSize = true;
            this.UsePostImportScript.Location = new System.Drawing.Point(6, 75);
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
            this.PreImportBrowse.Location = new System.Drawing.Point(371, 16);
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
            this.PreImportScriptPath.Size = new System.Drawing.Size(225, 13);
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
            // Save
            // 
            this.Save.BackColor = System.Drawing.Color.SteelBlue;
            this.Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Save.ForeColor = System.Drawing.Color.White;
            this.Save.Location = new System.Drawing.Point(11, 189);
            this.Save.Margin = new System.Windows.Forms.Padding(2);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(114, 29);
            this.Save.TabIndex = 29;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(47, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 49;
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
            this.PostArgsText.Size = new System.Drawing.Size(225, 13);
            this.PostArgsText.TabIndex = 48;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(47, 39);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(85, 13);
            this.label20.TabIndex = 47;
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
            this.PreArgsText.Size = new System.Drawing.Size(225, 13);
            this.PreArgsText.TabIndex = 46;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(427, 172);
            this.tabControl1.TabIndex = 30;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.ForeColor = System.Drawing.Color.White;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(419, 146);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Template Scripts";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.tabPage2.Controls.Add(this.ExecuteTestScript);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.TestingBrowse);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.TestingArgsText);
            this.tabPage2.Controls.Add(this.TestingScriptText);
            this.tabPage2.ForeColor = System.Drawing.Color.White;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(419, 146);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Testing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Argument String:";
            // 
            // TestingArgsText
            // 
            this.TestingArgsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestingArgsText.BackColor = System.Drawing.Color.DimGray;
            this.TestingArgsText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TestingArgsText.ForeColor = System.Drawing.Color.White;
            this.TestingArgsText.Location = new System.Drawing.Point(103, 40);
            this.TestingArgsText.Margin = new System.Windows.Forms.Padding(2);
            this.TestingArgsText.Name = "TestingArgsText";
            this.TestingArgsText.Size = new System.Drawing.Size(279, 13);
            this.TestingArgsText.TabIndex = 51;
            // 
            // TestingScriptText
            // 
            this.TestingScriptText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestingScriptText.BackColor = System.Drawing.Color.DimGray;
            this.TestingScriptText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TestingScriptText.ForeColor = System.Drawing.Color.White;
            this.TestingScriptText.Location = new System.Drawing.Point(103, 20);
            this.TestingScriptText.Margin = new System.Windows.Forms.Padding(2);
            this.TestingScriptText.Name = "TestingScriptText";
            this.TestingScriptText.Size = new System.Drawing.Size(279, 13);
            this.TestingScriptText.TabIndex = 50;
            // 
            // TestingBrowse
            // 
            this.TestingBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TestingBrowse.BackColor = System.Drawing.Color.SteelBlue;
            this.TestingBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TestingBrowse.ForeColor = System.Drawing.Color.White;
            this.TestingBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.TestingBrowse.Location = new System.Drawing.Point(386, 16);
            this.TestingBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.TestingBrowse.Name = "TestingBrowse";
            this.TestingBrowse.Size = new System.Drawing.Size(28, 21);
            this.TestingBrowse.TabIndex = 53;
            this.TestingBrowse.Text = "...";
            this.TestingBrowse.UseVisualStyleBackColor = false;
            this.TestingBrowse.Click += new System.EventHandler(this.TestingBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Script Path:";
            // 
            // ExecuteTestScript
            // 
            this.ExecuteTestScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExecuteTestScript.BackColor = System.Drawing.Color.SteelBlue;
            this.ExecuteTestScript.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExecuteTestScript.ForeColor = System.Drawing.Color.White;
            this.ExecuteTestScript.Location = new System.Drawing.Point(268, 57);
            this.ExecuteTestScript.Margin = new System.Windows.Forms.Padding(2);
            this.ExecuteTestScript.Name = "ExecuteTestScript";
            this.ExecuteTestScript.Size = new System.Drawing.Size(114, 29);
            this.ExecuteTestScript.TabIndex = 31;
            this.ExecuteTestScript.Text = "Execute";
            this.ExecuteTestScript.UseVisualStyleBackColor = false;
            this.ExecuteTestScript.Click += new System.EventHandler(this.ExecuteTestScript_Click);
            // 
            // ModifyScriptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(452, 227);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Save);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModifyScriptForm";
            this.Text = "Modify Workflow Scripts";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button PostImportBrowse;
        private System.Windows.Forms.TextBox PostImportScriptPath;
        private System.Windows.Forms.CheckBox UsePostImportScript;
        private System.Windows.Forms.Button PreImportBrowse;
        private System.Windows.Forms.TextBox PreImportScriptPath;
        private System.Windows.Forms.CheckBox UsePreImportScript;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PostArgsText;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox PreArgsText;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button ExecuteTestScript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button TestingBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TestingArgsText;
        private System.Windows.Forms.TextBox TestingScriptText;
    }
}