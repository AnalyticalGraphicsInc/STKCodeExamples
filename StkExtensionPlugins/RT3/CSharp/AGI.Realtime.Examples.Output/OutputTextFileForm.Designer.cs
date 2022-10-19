namespace AGI.Realtime.Examples.Output
{
    partial class OutputTextFileForm
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
            this.m_filename = new System.Windows.Forms.TextBox();
            this.m_browse = new System.Windows.Forms.Button();
            this.m_cancel = new System.Windows.Forms.Button();
            this.m_ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_filename
            // 
            this.m_filename.Location = new System.Drawing.Point(67, 12);
            this.m_filename.Name = "m_filename";
            this.m_filename.ReadOnly = true;
            this.m_filename.Size = new System.Drawing.Size(329, 20);
            this.m_filename.TabIndex = 0;
            this.m_filename.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_browse
            // 
            this.m_browse.Location = new System.Drawing.Point(402, 12);
            this.m_browse.Name = "m_browse";
            this.m_browse.Size = new System.Drawing.Size(25, 20);
            this.m_browse.TabIndex = 1;
            this.m_browse.Text = "...";
            this.m_browse.UseVisualStyleBackColor = true;
            this.m_browse.Click += new System.EventHandler(this.m_browse_Click);
            // 
            // m_cancel
            // 
            this.m_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancel.Location = new System.Drawing.Point(352, 38);
            this.m_cancel.Name = "m_cancel";
            this.m_cancel.Size = new System.Drawing.Size(75, 23);
            this.m_cancel.TabIndex = 2;
            this.m_cancel.Text = "Cancel";
            this.m_cancel.UseVisualStyleBackColor = true;
            // 
            // m_ok
            // 
            this.m_ok.Location = new System.Drawing.Point(271, 38);
            this.m_ok.Name = "m_ok";
            this.m_ok.Size = new System.Drawing.Size(75, 23);
            this.m_ok.TabIndex = 3;
            this.m_ok.Text = "OK";
            this.m_ok.UseVisualStyleBackColor = true;
            this.m_ok.Click += new System.EventHandler(this.m_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Filename";
            // 
            // OutputTextFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 71);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_ok);
            this.Controls.Add(this.m_cancel);
            this.Controls.Add(this.m_browse);
            this.Controls.Add(this.m_filename);
            this.Name = "OutputTextFileForm";
            this.Text = "Please selected an input file.";
            this.Load += new System.EventHandler(this.OutputTextFileForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_filename;
        private System.Windows.Forms.Button m_browse;
        private System.Windows.Forms.Button m_cancel;
        private System.Windows.Forms.Button m_ok;
        private System.Windows.Forms.Label label1;
    }
}