namespace ConstrainedAttitude.UiPlugin.Copier
{
    partial class SetupForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUIPluginLocation = new System.Windows.Forms.TextBox();
            this.btnUIPluginLocation = new System.Windows.Forms.Button();
            this.btnRunSetup = new System.Windows.Forms.Button();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.txtAssemblyName = new System.Windows.Forms.TextBox();
            this.lblAssamblyName = new System.Windows.Forms.Label();
            this.txtTypeName = new System.Windows.Forms.TextBox();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "UI Plugin Location:";
            // 
            // txtUIPluginLocation
            // 
            this.txtUIPluginLocation.Location = new System.Drawing.Point(116, 14);
            this.txtUIPluginLocation.Name = "txtUIPluginLocation";
            this.txtUIPluginLocation.Size = new System.Drawing.Size(347, 20);
            this.txtUIPluginLocation.TabIndex = 1;
            // 
            // btnUIPluginLocation
            // 
            this.btnUIPluginLocation.Location = new System.Drawing.Point(492, 14);
            this.btnUIPluginLocation.Name = "btnUIPluginLocation";
            this.btnUIPluginLocation.Size = new System.Drawing.Size(40, 23);
            this.btnUIPluginLocation.TabIndex = 2;
            this.btnUIPluginLocation.Text = "...";
            this.btnUIPluginLocation.UseVisualStyleBackColor = true;
            this.btnUIPluginLocation.Click += new System.EventHandler(this.btnUIPluginLocation_Click);
            // 
            // btnRunSetup
            // 
            this.btnRunSetup.Location = new System.Drawing.Point(376, 189);
            this.btnRunSetup.Name = "btnRunSetup";
            this.btnRunSetup.Size = new System.Drawing.Size(75, 33);
            this.btnRunSetup.TabIndex = 3;
            this.btnRunSetup.Text = "Run Setup";
            this.btnRunSetup.UseVisualStyleBackColor = true;
            this.btnRunSetup.Click += new System.EventHandler(this.btnRunSetup_Click);
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(116, 56);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(347, 20);
            this.txtDisplayName.TabIndex = 5;
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(13, 59);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(75, 13);
            this.lblDisplayName.TabIndex = 4;
            this.lblDisplayName.Text = "Display Name:";
            // 
            // txtAssemblyName
            // 
            this.txtAssemblyName.Location = new System.Drawing.Point(116, 98);
            this.txtAssemblyName.Name = "txtAssemblyName";
            this.txtAssemblyName.Size = new System.Drawing.Size(347, 20);
            this.txtAssemblyName.TabIndex = 7;
            // 
            // lblAssamblyName
            // 
            this.lblAssamblyName.AutoSize = true;
            this.lblAssamblyName.Location = new System.Drawing.Point(13, 101);
            this.lblAssamblyName.Name = "lblAssamblyName";
            this.lblAssamblyName.Size = new System.Drawing.Size(85, 13);
            this.lblAssamblyName.TabIndex = 6;
            this.lblAssamblyName.Text = "Assembly Name:";
            // 
            // txtTypeName
            // 
            this.txtTypeName.Location = new System.Drawing.Point(116, 140);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.Size = new System.Drawing.Size(347, 20);
            this.txtTypeName.TabIndex = 9;
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.Location = new System.Drawing.Point(13, 143);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(65, 13);
            this.lblTypeName.TabIndex = 8;
            this.lblTypeName.Text = "Type Name:";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(457, 189);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 33);
            this.btnHelp.TabIndex = 10;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 234);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.txtTypeName);
            this.Controls.Add(this.lblTypeName);
            this.Controls.Add(this.txtAssemblyName);
            this.Controls.Add(this.lblAssamblyName);
            this.Controls.Add(this.txtDisplayName);
            this.Controls.Add(this.lblDisplayName);
            this.Controls.Add(this.btnRunSetup);
            this.Controls.Add(this.btnUIPluginLocation);
            this.Controls.Add(this.txtUIPluginLocation);
            this.Controls.Add(this.label1);
            this.Name = "SetupForm";
            this.Text = "Setup Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUIPluginLocation;
        private System.Windows.Forms.Button btnUIPluginLocation;
        private System.Windows.Forms.Button btnRunSetup;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.TextBox txtAssemblyName;
        private System.Windows.Forms.Label lblAssamblyName;
        private System.Windows.Forms.TextBox txtTypeName;
        private System.Windows.Forms.Label lblTypeName;
        private System.Windows.Forms.Button btnHelp;
    }
}

