namespace OperatorsToolbox.SatelliteCreator
{
    partial class FilterForm
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
            this.FilterOptionTypes = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OptionList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FilterOptionTypes
            // 
            this.FilterOptionTypes.BackColor = System.Drawing.Color.DimGray;
            this.FilterOptionTypes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FilterOptionTypes.ForeColor = System.Drawing.Color.White;
            this.FilterOptionTypes.FormattingEnabled = true;
            this.FilterOptionTypes.Location = new System.Drawing.Point(3, 4);
            this.FilterOptionTypes.Name = "FilterOptionTypes";
            this.FilterOptionTypes.Size = new System.Drawing.Size(191, 21);
            this.FilterOptionTypes.TabIndex = 0;
            this.FilterOptionTypes.SelectedIndexChanged += new System.EventHandler(this.FilterOptionTypes_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OptionList);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 238);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // OptionList
            // 
            this.OptionList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OptionList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OptionList.CheckOnClick = true;
            this.OptionList.ForeColor = System.Drawing.Color.White;
            this.OptionList.FormattingEnabled = true;
            this.OptionList.Location = new System.Drawing.Point(6, 16);
            this.OptionList.Name = "OptionList";
            this.OptionList.Size = new System.Drawing.Size(179, 210);
            this.OptionList.TabIndex = 0;
            this.OptionList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.OptionList_ItemCheck);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(199, 270);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FilterOptionTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FilterForm";
            this.Text = "FilterForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FilterForm_Paint);
            this.MouseEnter += new System.EventHandler(this.FilterForm_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.FilterForm_MouseLeave);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox FilterOptionTypes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox OptionList;
    }
}