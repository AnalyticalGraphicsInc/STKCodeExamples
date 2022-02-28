namespace Stk12.UiPlugin.Articulations
{
    partial class LinkToTimeForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbStkObjects = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Link = new System.Windows.Forms.Button();
            this.Events = new System.Windows.Forms.ComboBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(80, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "Parent Object";
            // 
            // cbStkObjects
            // 
            this.cbStkObjects.FormattingEnabled = true;
            this.cbStkObjects.Location = new System.Drawing.Point(12, 57);
            this.cbStkObjects.Name = "cbStkObjects";
            this.cbStkObjects.Size = new System.Drawing.Size(285, 24);
            this.cbStkObjects.TabIndex = 23;
            this.cbStkObjects.SelectedIndexChanged += new System.EventHandler(this.cbStkObjects_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(80, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Choose Event";
            // 
            // Link
            // 
            this.Link.Location = new System.Drawing.Point(12, 182);
            this.Link.Name = "Link";
            this.Link.Size = new System.Drawing.Size(134, 47);
            this.Link.TabIndex = 21;
            this.Link.Text = "Link to Time Instance";
            this.Link.UseVisualStyleBackColor = true;
            this.Link.Click += new System.EventHandler(this.Link_Click);
            // 
            // Events
            // 
            this.Events.FormattingEnabled = true;
            this.Events.Location = new System.Drawing.Point(12, 126);
            this.Events.Name = "Events";
            this.Events.Size = new System.Drawing.Size(285, 24);
            this.Events.TabIndex = 20;
            this.Events.SelectedIndexChanged += new System.EventHandler(this.Events_SelectedIndexChanged);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(163, 182);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(134, 47);
            this.Cancel.TabIndex = 25;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // LinkToTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 285);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbStkObjects);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Link);
            this.Controls.Add(this.Events);
            this.Name = "LinkToTimeForm";
            this.Text = "LinkToTimeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbStkObjects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Link;
        private System.Windows.Forms.ComboBox Events;
        private System.Windows.Forms.Button Cancel;
    }
}