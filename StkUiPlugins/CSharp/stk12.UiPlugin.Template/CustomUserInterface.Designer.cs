
namespace $safeprojectname$
{
    partial class CustomUserInterface
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTestProgressBar = new System.Windows.Forms.Button();
            this.lblStkObjects = new System.Windows.Forms.Label();
            this.cbStkObjects = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnTestProgressBar
            // 
            this.btnTestProgressBar.Location = new System.Drawing.Point(259, 234);
            this.btnTestProgressBar.Name = "btnTestProgressBar";
            this.btnTestProgressBar.Size = new System.Drawing.Size(116, 40);
            this.btnTestProgressBar.TabIndex = 0;
            this.btnTestProgressBar.Text = "Test Progress Bar";
            this.btnTestProgressBar.UseVisualStyleBackColor = true;
            this.btnTestProgressBar.Click += new System.EventHandler(this.btnTestProgressBar_Click);
            // 
            // lblStkObjects
            // 
            this.lblStkObjects.AutoSize = true;
            this.lblStkObjects.Location = new System.Drawing.Point(14, 29);
            this.lblStkObjects.Name = "lblStkObjects";
            this.lblStkObjects.Size = new System.Drawing.Size(65, 13);
            this.lblStkObjects.TabIndex = 1;
            this.lblStkObjects.Text = "Stk Objects:";
            // 
            // cbStkObjects
            // 
            this.cbStkObjects.FormattingEnabled = true;
            this.cbStkObjects.Location = new System.Drawing.Point(85, 26);
            this.cbStkObjects.Name = "cbStkObjects";
            this.cbStkObjects.Size = new System.Drawing.Size(290, 21);
            this.cbStkObjects.TabIndex = 2;
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbStkObjects);
            this.Controls.Add(this.lblStkObjects);
            this.Controls.Add(this.btnTestProgressBar);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(390, 288);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTestProgressBar;
        private System.Windows.Forms.Label lblStkObjects;
        private System.Windows.Forms.ComboBox cbStkObjects;
    }
}
