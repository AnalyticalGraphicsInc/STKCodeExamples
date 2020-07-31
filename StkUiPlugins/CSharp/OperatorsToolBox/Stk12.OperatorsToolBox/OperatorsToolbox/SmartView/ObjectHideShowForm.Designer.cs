namespace OperatorsToolbox.SmartView
{
    partial class ObjectHideShowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectHideShowForm));
            this.ObjectList = new System.Windows.Forms.TreeView();
            this.Apply = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SelectAll = new System.Windows.Forms.CheckBox();
            this.ToggleSensors = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FilterType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ObjectList
            // 
            this.ObjectList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectList.BackColor = System.Drawing.Color.DimGray;
            this.ObjectList.CheckBoxes = true;
            this.ObjectList.ForeColor = System.Drawing.Color.White;
            this.ObjectList.Location = new System.Drawing.Point(6, 54);
            this.ObjectList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ObjectList.Name = "ObjectList";
            this.ObjectList.Size = new System.Drawing.Size(294, 305);
            this.ObjectList.TabIndex = 0;
            this.ObjectList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ObjectList_AfterCheck);
            this.ObjectList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ObjectList_AfterSelect);
            this.ObjectList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ObjectList_NodeMouseClick);
            // 
            // Apply
            // 
            this.Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Apply.BackColor = System.Drawing.Color.SteelBlue;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Apply.ForeColor = System.Drawing.Color.White;
            this.Apply.Location = new System.Drawing.Point(6, 363);
            this.Apply.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(73, 34);
            this.Apply.TabIndex = 1;
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
            this.Cancel.Location = new System.Drawing.Point(82, 363);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(73, 34);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SelectAll
            // 
            this.SelectAll.AutoSize = true;
            this.SelectAll.ForeColor = System.Drawing.Color.White;
            this.SelectAll.Location = new System.Drawing.Point(8, 34);
            this.SelectAll.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SelectAll.Name = "SelectAll";
            this.SelectAll.Size = new System.Drawing.Size(117, 17);
            this.SelectAll.TabIndex = 3;
            this.SelectAll.Text = "Select/Unselect All";
            this.SelectAll.UseVisualStyleBackColor = true;
            this.SelectAll.CheckedChanged += new System.EventHandler(this.SelectAll_CheckedChanged);
            // 
            // ToggleSensors
            // 
            this.ToggleSensors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ToggleSensors.AutoSize = true;
            this.ToggleSensors.ForeColor = System.Drawing.Color.White;
            this.ToggleSensors.Location = new System.Drawing.Point(121, 34);
            this.ToggleSensors.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ToggleSensors.Name = "ToggleSensors";
            this.ToggleSensors.Size = new System.Drawing.Size(114, 17);
            this.ToggleSensors.TabIndex = 4;
            this.ToggleSensors.Text = "Toggle All Sensors";
            this.ToggleSensors.UseVisualStyleBackColor = true;
            this.ToggleSensors.CheckedChanged += new System.EventHandler(this.ToggleSensors_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Filter By:";
            // 
            // FilterType
            // 
            this.FilterType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterType.BackColor = System.Drawing.Color.DimGray;
            this.FilterType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FilterType.ForeColor = System.Drawing.Color.White;
            this.FilterType.FormattingEnabled = true;
            this.FilterType.Location = new System.Drawing.Point(56, 10);
            this.FilterType.Margin = new System.Windows.Forms.Padding(2);
            this.FilterType.Name = "FilterType";
            this.FilterType.Size = new System.Drawing.Size(126, 21);
            this.FilterType.TabIndex = 7;
            this.FilterType.SelectedIndexChanged += new System.EventHandler(this.FilterType_SelectedIndexChanged);
            // 
            // ObjectHideShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(310, 407);
            this.Controls.Add(this.FilterType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ToggleSensors);
            this.Controls.Add(this.SelectAll);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.ObjectList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ObjectHideShowForm";
            this.Text = "Object Hide/Show Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ObjectList;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox SelectAll;
        private System.Windows.Forms.CheckBox ToggleSensors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox FilterType;
    }
}