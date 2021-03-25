namespace OperatorsToolbox.Coverage
{
    partial class ATManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ATManager));
            this.label4 = new System.Windows.Forms.Label();
            this.AdditionType = new System.Windows.Forms.ComboBox();
            this.SingleGroup = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SingleATName = new System.Windows.Forms.ComboBox();
            this.Add = new System.Windows.Forms.Button();
            this.GroupGroup = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupName = new System.Windows.Forms.TextBox();
            this.ATList = new System.Windows.Forms.CheckedListBox();
            this.SingleGroup.SuspendLayout();
            this.GroupGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(9, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Addition Type:";
            // 
            // AdditionType
            // 
            this.AdditionType.BackColor = System.Drawing.Color.DimGray;
            this.AdditionType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AdditionType.ForeColor = System.Drawing.Color.White;
            this.AdditionType.FormattingEnabled = true;
            this.AdditionType.Location = new System.Drawing.Point(93, 6);
            this.AdditionType.Name = "AdditionType";
            this.AdditionType.Size = new System.Drawing.Size(184, 21);
            this.AdditionType.TabIndex = 39;
            this.AdditionType.SelectedIndexChanged += new System.EventHandler(this.AdditionType_SelectedIndexChanged);
            // 
            // SingleGroup
            // 
            this.SingleGroup.Controls.Add(this.label1);
            this.SingleGroup.Controls.Add(this.SingleATName);
            this.SingleGroup.ForeColor = System.Drawing.Color.White;
            this.SingleGroup.Location = new System.Drawing.Point(12, 41);
            this.SingleGroup.Name = "SingleGroup";
            this.SingleGroup.Size = new System.Drawing.Size(264, 181);
            this.SingleGroup.TabIndex = 41;
            this.SingleGroup.TabStop = false;
            this.SingleGroup.Text = "Export Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Area Target:";
            // 
            // SingleATName
            // 
            this.SingleATName.BackColor = System.Drawing.Color.DimGray;
            this.SingleATName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SingleATName.ForeColor = System.Drawing.Color.White;
            this.SingleATName.FormattingEnabled = true;
            this.SingleATName.Location = new System.Drawing.Point(81, 28);
            this.SingleATName.Name = "SingleATName";
            this.SingleATName.Size = new System.Drawing.Size(171, 21);
            this.SingleATName.TabIndex = 42;
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.SteelBlue;
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Add.ForeColor = System.Drawing.Color.White;
            this.Add.Location = new System.Drawing.Point(12, 227);
            this.Add.Margin = new System.Windows.Forms.Padding(2);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(101, 31);
            this.Add.TabIndex = 54;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // GroupGroup
            // 
            this.GroupGroup.Controls.Add(this.ATList);
            this.GroupGroup.Controls.Add(this.GroupName);
            this.GroupGroup.Controls.Add(this.label2);
            this.GroupGroup.ForeColor = System.Drawing.Color.White;
            this.GroupGroup.Location = new System.Drawing.Point(12, 41);
            this.GroupGroup.Name = "GroupGroup";
            this.GroupGroup.Size = new System.Drawing.Size(264, 181);
            this.GroupGroup.TabIndex = 44;
            this.GroupGroup.TabStop = false;
            this.GroupGroup.Text = "Export Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Group Name:";
            // 
            // GroupName
            // 
            this.GroupName.BackColor = System.Drawing.Color.DimGray;
            this.GroupName.ForeColor = System.Drawing.Color.White;
            this.GroupName.Location = new System.Drawing.Point(82, 28);
            this.GroupName.Name = "GroupName";
            this.GroupName.Size = new System.Drawing.Size(168, 20);
            this.GroupName.TabIndex = 44;
            // 
            // ATList
            // 
            this.ATList.BackColor = System.Drawing.Color.DimGray;
            this.ATList.CheckOnClick = true;
            this.ATList.ForeColor = System.Drawing.Color.White;
            this.ATList.FormattingEnabled = true;
            this.ATList.Location = new System.Drawing.Point(9, 62);
            this.ATList.Name = "ATList";
            this.ATList.Size = new System.Drawing.Size(241, 109);
            this.ATList.TabIndex = 45;
            // 
            // ATManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(298, 283);
            this.Controls.Add(this.GroupGroup);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.SingleGroup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AdditionType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ATManager";
            this.Text = "AT Manager";
            this.SingleGroup.ResumeLayout(false);
            this.SingleGroup.PerformLayout();
            this.GroupGroup.ResumeLayout(false);
            this.GroupGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox AdditionType;
        private System.Windows.Forms.GroupBox SingleGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SingleATName;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.GroupBox GroupGroup;
        private System.Windows.Forms.CheckedListBox ATList;
        private System.Windows.Forms.TextBox GroupName;
        private System.Windows.Forms.Label label2;
    }
}