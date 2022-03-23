namespace AGI.Realtime.Examples.Output
{
    partial class OutputEntity
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
            this.m_affiliation = new System.Windows.Forms.ComboBox();
            this.m_symbol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_affiliation
            // 
            this.m_affiliation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_affiliation.FormattingEnabled = true;
            this.m_affiliation.Location = new System.Drawing.Point(108, 12);
            this.m_affiliation.Name = "m_affiliation";
            this.m_affiliation.Size = new System.Drawing.Size(121, 21);
            this.m_affiliation.TabIndex = 0;
            this.m_affiliation.SelectedIndexChanged += new System.EventHandler(this.UiModified);
            // 
            // m_symbol
            // 
            this.m_symbol.Location = new System.Drawing.Point(108, 39);
            this.m_symbol.Name = "m_symbol";
            this.m_symbol.Size = new System.Drawing.Size(121, 20);
            this.m_symbol.TabIndex = 1;
            this.m_symbol.TextChanged += new System.EventHandler(this.UiModified);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Affiliation:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mil2525b Symbol:";
            // 
            // OutputEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 75);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_symbol);
            this.Controls.Add(this.m_affiliation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OutputEntity";
            this.ShowInTaskbar = false;
            this.Text = "OutputEntity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox m_affiliation;
        private System.Windows.Forms.TextBox m_symbol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}