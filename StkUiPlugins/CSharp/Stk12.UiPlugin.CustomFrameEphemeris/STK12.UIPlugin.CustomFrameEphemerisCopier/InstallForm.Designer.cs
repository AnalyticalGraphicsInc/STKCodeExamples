namespace STK12.UIPlugin.CustomFrameEphemerisCopier
{
    partial class InstallForm
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
            this.txtBinaryLocation = new System.Windows.Forms.TextBox();
            this.lblBinaryLocation = new System.Windows.Forms.Label();
            this.btnBinaryLocation = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBinaryLocation
            // 
            this.txtBinaryLocation.Location = new System.Drawing.Point(147, 58);
            this.txtBinaryLocation.Name = "txtBinaryLocation";
            this.txtBinaryLocation.Size = new System.Drawing.Size(487, 20);
            this.txtBinaryLocation.TabIndex = 0;
            // 
            // lblBinaryLocation
            // 
            this.lblBinaryLocation.AutoSize = true;
            this.lblBinaryLocation.Location = new System.Drawing.Point(12, 61);
            this.lblBinaryLocation.Name = "lblBinaryLocation";
            this.lblBinaryLocation.Size = new System.Drawing.Size(129, 13);
            this.lblBinaryLocation.TabIndex = 1;
            this.lblBinaryLocation.Text = "UI Plugin Binary Location:";
            // 
            // btnBinaryLocation
            // 
            this.btnBinaryLocation.Location = new System.Drawing.Point(640, 55);
            this.btnBinaryLocation.Name = "btnBinaryLocation";
            this.btnBinaryLocation.Size = new System.Drawing.Size(41, 25);
            this.btnBinaryLocation.TabIndex = 6;
            this.btnBinaryLocation.Text = "...";
            this.btnBinaryLocation.UseVisualStyleBackColor = true;
            this.btnBinaryLocation.Click += new System.EventHandler(this.btnBinaryLocation_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(490, 155);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(94, 36);
            this.btnInstall.TabIndex = 7;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(590, 155);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 36);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // InstallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 203);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnBinaryLocation);
            this.Controls.Add(this.lblBinaryLocation);
            this.Controls.Add(this.txtBinaryLocation);
            this.Name = "InstallForm";
            this.Text = "Install Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBinaryLocation;
        private System.Windows.Forms.Label lblBinaryLocation;
        private System.Windows.Forms.Button btnBinaryLocation;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnClose;
    }
}