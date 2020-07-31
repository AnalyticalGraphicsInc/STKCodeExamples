namespace OperatorsToolbox.VolumeCreator
{
    partial class FacilitySelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacilitySelectForm));
            this.FacilityList = new System.Windows.Forms.ListBox();
            this.Select = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FacilityList
            // 
            this.FacilityList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FacilityList.BackColor = System.Drawing.SystemColors.Desktop;
            this.FacilityList.ForeColor = System.Drawing.SystemColors.Window;
            this.FacilityList.FormattingEnabled = true;
            this.FacilityList.Location = new System.Drawing.Point(6, 5);
            this.FacilityList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FacilityList.Name = "FacilityList";
            this.FacilityList.Size = new System.Drawing.Size(186, 199);
            this.FacilityList.TabIndex = 0;
            // 
            // Select
            // 
            this.Select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Select.BackColor = System.Drawing.Color.SteelBlue;
            this.Select.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Select.ForeColor = System.Drawing.Color.White;
            this.Select.Location = new System.Drawing.Point(6, 217);
            this.Select.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(81, 29);
            this.Select.TabIndex = 1;
            this.Select.Text = "Select";
            this.Select.UseVisualStyleBackColor = false;
            this.Select.Click += new System.EventHandler(this.Select_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ForeColor = System.Drawing.Color.White;
            this.Cancel.Location = new System.Drawing.Point(110, 217);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(81, 29);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // FacilitySelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(199, 256);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Select);
            this.Controls.Add(this.FacilityList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FacilitySelectForm";
            this.Text = "Select Facility";
            this.Load += new System.EventHandler(this.FacilitySelectForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox FacilityList;
        private System.Windows.Forms.Button Select;
        private System.Windows.Forms.Button Cancel;
    }
}