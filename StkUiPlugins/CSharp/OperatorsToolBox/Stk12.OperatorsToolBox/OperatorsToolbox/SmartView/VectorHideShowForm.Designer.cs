namespace OperatorsToolbox.SmartView
{
    partial class VectorHideShowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VectorHideShowForm));
            this.AvailableVectorList = new System.Windows.Forms.TreeView();
            this.Apply = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.AllOff = new System.Windows.Forms.Button();
            this.TrimUnselected = new System.Windows.Forms.Button();
            this.AllOn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AvailableVectorList
            // 
            this.AvailableVectorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AvailableVectorList.BackColor = System.Drawing.Color.DimGray;
            this.AvailableVectorList.CheckBoxes = true;
            this.AvailableVectorList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvailableVectorList.ForeColor = System.Drawing.Color.White;
            this.AvailableVectorList.Location = new System.Drawing.Point(12, 12);
            this.AvailableVectorList.Name = "AvailableVectorList";
            this.AvailableVectorList.Size = new System.Drawing.Size(315, 437);
            this.AvailableVectorList.TabIndex = 0;
            this.AvailableVectorList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.AvailableVectorList_AfterCheck);
            // 
            // Apply
            // 
            this.Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Apply.BackColor = System.Drawing.Color.SteelBlue;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Apply.ForeColor = System.Drawing.Color.White;
            this.Apply.Location = new System.Drawing.Point(12, 454);
            this.Apply.Margin = new System.Windows.Forms.Padding(2);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(82, 28);
            this.Apply.TabIndex = 8;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ForeColor = System.Drawing.Color.White;
            this.Cancel.Location = new System.Drawing.Point(98, 454);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 28);
            this.Cancel.TabIndex = 9;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AllOff
            // 
            this.AllOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AllOff.BackColor = System.Drawing.Color.SteelBlue;
            this.AllOff.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AllOff.ForeColor = System.Drawing.Color.White;
            this.AllOff.Location = new System.Drawing.Point(332, 73);
            this.AllOff.Margin = new System.Windows.Forms.Padding(2);
            this.AllOff.Name = "AllOff";
            this.AllOff.Size = new System.Drawing.Size(82, 28);
            this.AllOff.TabIndex = 10;
            this.AllOff.Text = "All Off";
            this.AllOff.UseVisualStyleBackColor = false;
            this.AllOff.Click += new System.EventHandler(this.AllOff_Click);
            // 
            // TrimUnselected
            // 
            this.TrimUnselected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TrimUnselected.BackColor = System.Drawing.Color.SteelBlue;
            this.TrimUnselected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TrimUnselected.ForeColor = System.Drawing.Color.White;
            this.TrimUnselected.Location = new System.Drawing.Point(332, 12);
            this.TrimUnselected.Margin = new System.Windows.Forms.Padding(2);
            this.TrimUnselected.Name = "TrimUnselected";
            this.TrimUnselected.Size = new System.Drawing.Size(82, 57);
            this.TrimUnselected.TabIndex = 11;
            this.TrimUnselected.Text = "Trim Unselected";
            this.TrimUnselected.UseVisualStyleBackColor = false;
            this.TrimUnselected.Click += new System.EventHandler(this.TrimUnselected_Click);
            // 
            // AllOn
            // 
            this.AllOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AllOn.BackColor = System.Drawing.Color.SteelBlue;
            this.AllOn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AllOn.ForeColor = System.Drawing.Color.White;
            this.AllOn.Location = new System.Drawing.Point(332, 105);
            this.AllOn.Margin = new System.Windows.Forms.Padding(2);
            this.AllOn.Name = "AllOn";
            this.AllOn.Size = new System.Drawing.Size(82, 28);
            this.AllOn.TabIndex = 12;
            this.AllOn.Text = "All On";
            this.AllOn.UseVisualStyleBackColor = false;
            this.AllOn.Click += new System.EventHandler(this.AllOn_Click);
            // 
            // VectorHideShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(420, 491);
            this.Controls.Add(this.AllOn);
            this.Controls.Add(this.TrimUnselected);
            this.Controls.Add(this.AllOff);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.AvailableVectorList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VectorHideShowForm";
            this.Text = "Vector Display Options";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView AvailableVectorList;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button AllOff;
        private System.Windows.Forms.Button TrimUnselected;
        private System.Windows.Forms.Button AllOn;
    }
}