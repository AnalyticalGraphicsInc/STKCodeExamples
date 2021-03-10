namespace OperatorsToolbox.BetaAngleCalculator
{
    partial class SolarPhasePlugin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolarPhasePlugin));
            this.TargetsList = new System.Windows.Forms.ListView();
            this.TargetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ObserversList = new System.Windows.Forms.ListView();
            this.ObserverName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Calculate = new System.Windows.Forms.Button();
            this.UnselectTarget = new System.Windows.Forms.Button();
            this.SelectTarget = new System.Windows.Forms.Button();
            this.UnselectObserver = new System.Windows.Forms.Button();
            this.SelectObserver = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ObserverType = new System.Windows.Forms.ComboBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.TargetType = new System.Windows.Forms.ComboBox();
            this.AngleType = new System.Windows.Forms.ComboBox();
            this.DefinitionGroupBox = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ConstraintOptions = new System.Windows.Forms.GroupBox();
            this.EnableConstraint = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ConstraintMin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ConstraintMax = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DefinitionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ConstraintOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TargetsList
            // 
            this.TargetsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetsList.BackColor = System.Drawing.Color.DimGray;
            this.TargetsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TargetsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TargetName});
            this.TargetsList.ForeColor = System.Drawing.Color.White;
            this.TargetsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.TargetsList.HideSelection = false;
            this.TargetsList.Location = new System.Drawing.Point(8, 54);
            this.TargetsList.Margin = new System.Windows.Forms.Padding(2);
            this.TargetsList.Name = "TargetsList";
            this.TargetsList.Size = new System.Drawing.Size(157, 152);
            this.TargetsList.TabIndex = 25;
            this.TargetsList.UseCompatibleStateImageBehavior = false;
            this.TargetsList.View = System.Windows.Forms.View.Details;
            // 
            // TargetName
            // 
            this.TargetName.Text = "";
            this.TargetName.Width = 158;
            // 
            // ObserversList
            // 
            this.ObserversList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObserversList.BackColor = System.Drawing.Color.DimGray;
            this.ObserversList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ObserversList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ObserverName});
            this.ObserversList.ForeColor = System.Drawing.Color.White;
            this.ObserversList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ObserversList.HideSelection = false;
            this.ObserversList.Location = new System.Drawing.Point(2, 53);
            this.ObserversList.Margin = new System.Windows.Forms.Padding(2);
            this.ObserversList.Name = "ObserversList";
            this.ObserversList.Size = new System.Drawing.Size(155, 152);
            this.ObserversList.TabIndex = 24;
            this.ObserversList.UseCompatibleStateImageBehavior = false;
            this.ObserversList.View = System.Windows.Forms.View.Details;
            // 
            // ObserverName
            // 
            this.ObserverName.Text = "";
            this.ObserverName.Width = 159;
            // 
            // Calculate
            // 
            this.Calculate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Calculate.BackColor = System.Drawing.Color.SteelBlue;
            this.Calculate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Calculate.ForeColor = System.Drawing.Color.White;
            this.Calculate.Location = new System.Drawing.Point(109, 448);
            this.Calculate.Margin = new System.Windows.Forms.Padding(2);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(147, 42);
            this.Calculate.TabIndex = 23;
            this.Calculate.Text = "Calculate Angles";
            this.Calculate.UseVisualStyleBackColor = false;
            this.Calculate.Click += new System.EventHandler(this.Calculate_Click);
            // 
            // UnselectTarget
            // 
            this.UnselectTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UnselectTarget.BackColor = System.Drawing.Color.SteelBlue;
            this.UnselectTarget.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UnselectTarget.ForeColor = System.Drawing.Color.White;
            this.UnselectTarget.Location = new System.Drawing.Point(108, 210);
            this.UnselectTarget.Margin = new System.Windows.Forms.Padding(2);
            this.UnselectTarget.Name = "UnselectTarget";
            this.UnselectTarget.Size = new System.Drawing.Size(56, 25);
            this.UnselectTarget.TabIndex = 22;
            this.UnselectTarget.Text = "unselect";
            this.UnselectTarget.UseVisualStyleBackColor = false;
            this.UnselectTarget.Click += new System.EventHandler(this.UnselectTarget_Click);
            // 
            // SelectTarget
            // 
            this.SelectTarget.BackColor = System.Drawing.Color.SteelBlue;
            this.SelectTarget.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectTarget.ForeColor = System.Drawing.Color.White;
            this.SelectTarget.Location = new System.Drawing.Point(8, 210);
            this.SelectTarget.Margin = new System.Windows.Forms.Padding(2);
            this.SelectTarget.Name = "SelectTarget";
            this.SelectTarget.Size = new System.Drawing.Size(52, 25);
            this.SelectTarget.TabIndex = 21;
            this.SelectTarget.Text = "Select";
            this.SelectTarget.UseVisualStyleBackColor = false;
            this.SelectTarget.Click += new System.EventHandler(this.SelectTarget_Click);
            // 
            // UnselectObserver
            // 
            this.UnselectObserver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UnselectObserver.BackColor = System.Drawing.Color.SteelBlue;
            this.UnselectObserver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UnselectObserver.ForeColor = System.Drawing.Color.White;
            this.UnselectObserver.Location = new System.Drawing.Point(103, 209);
            this.UnselectObserver.Margin = new System.Windows.Forms.Padding(2);
            this.UnselectObserver.Name = "UnselectObserver";
            this.UnselectObserver.Size = new System.Drawing.Size(55, 25);
            this.UnselectObserver.TabIndex = 20;
            this.UnselectObserver.Text = "unselect";
            this.UnselectObserver.UseVisualStyleBackColor = false;
            this.UnselectObserver.Click += new System.EventHandler(this.UnselectObserver_Click);
            // 
            // SelectObserver
            // 
            this.SelectObserver.BackColor = System.Drawing.Color.SteelBlue;
            this.SelectObserver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectObserver.ForeColor = System.Drawing.Color.White;
            this.SelectObserver.Location = new System.Drawing.Point(2, 210);
            this.SelectObserver.Margin = new System.Windows.Forms.Padding(2);
            this.SelectObserver.Name = "SelectObserver";
            this.SelectObserver.Size = new System.Drawing.Size(50, 25);
            this.SelectObserver.TabIndex = 19;
            this.SelectObserver.Text = "Select";
            this.SelectObserver.UseVisualStyleBackColor = false;
            this.SelectObserver.Click += new System.EventHandler(this.SelectObserver_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(64, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Targets";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(51, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Observers";
            // 
            // ObserverType
            // 
            this.ObserverType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObserverType.BackColor = System.Drawing.Color.DimGray;
            this.ObserverType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ObserverType.ForeColor = System.Drawing.Color.White;
            this.ObserverType.FormattingEnabled = true;
            this.ObserverType.Location = new System.Drawing.Point(2, 11);
            this.ObserverType.Margin = new System.Windows.Forms.Padding(2);
            this.ObserverType.Name = "ObserverType";
            this.ObserverType.Size = new System.Drawing.Size(155, 21);
            this.ObserverType.TabIndex = 16;
            this.ObserverType.SelectedIndexChanged += new System.EventHandler(this.ObserverType_SelectedIndexChanged);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.imageList1;
            this.Cancel.Location = new System.Drawing.Point(331, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 26;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "x-mark.png");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Firebrick;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 23);
            this.label3.TabIndex = 30;
            this.label3.Text = "Solar Angles Utility";
            // 
            // TargetType
            // 
            this.TargetType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetType.BackColor = System.Drawing.Color.DimGray;
            this.TargetType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TargetType.ForeColor = System.Drawing.Color.White;
            this.TargetType.FormattingEnabled = true;
            this.TargetType.Location = new System.Drawing.Point(8, 12);
            this.TargetType.Margin = new System.Windows.Forms.Padding(2);
            this.TargetType.Name = "TargetType";
            this.TargetType.Size = new System.Drawing.Size(157, 21);
            this.TargetType.TabIndex = 32;
            this.TargetType.SelectedIndexChanged += new System.EventHandler(this.TargetType_SelectedIndexChanged);
            // 
            // AngleType
            // 
            this.AngleType.BackColor = System.Drawing.Color.DimGray;
            this.AngleType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AngleType.ForeColor = System.Drawing.Color.White;
            this.AngleType.FormattingEnabled = true;
            this.AngleType.Location = new System.Drawing.Point(5, 18);
            this.AngleType.Margin = new System.Windows.Forms.Padding(2);
            this.AngleType.Name = "AngleType";
            this.AngleType.Size = new System.Drawing.Size(133, 21);
            this.AngleType.TabIndex = 33;
            // 
            // DefinitionGroupBox
            // 
            this.DefinitionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DefinitionGroupBox.Controls.Add(this.splitContainer1);
            this.DefinitionGroupBox.ForeColor = System.Drawing.Color.White;
            this.DefinitionGroupBox.Location = new System.Drawing.Point(3, 86);
            this.DefinitionGroupBox.Name = "DefinitionGroupBox";
            this.DefinitionGroupBox.Size = new System.Drawing.Size(350, 267);
            this.DefinitionGroupBox.TabIndex = 34;
            this.DefinitionGroupBox.TabStop = false;
            this.DefinitionGroupBox.Text = "Angle Definition";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ObserversList);
            this.splitContainer1.Panel1.Controls.Add(this.ObserverType);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.SelectObserver);
            this.splitContainer1.Panel1.Controls.Add(this.UnselectObserver);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TargetsList);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.TargetType);
            this.splitContainer1.Panel2.Controls.Add(this.SelectTarget);
            this.splitContainer1.Panel2.Controls.Add(this.UnselectTarget);
            this.splitContainer1.Size = new System.Drawing.Size(344, 248);
            this.splitContainer1.SplitterDistance = 168;
            this.splitContainer1.TabIndex = 0;
            // 
            // ConstraintOptions
            // 
            this.ConstraintOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstraintOptions.Controls.Add(this.label6);
            this.ConstraintOptions.Controls.Add(this.ConstraintMax);
            this.ConstraintOptions.Controls.Add(this.label7);
            this.ConstraintOptions.Controls.Add(this.label5);
            this.ConstraintOptions.Controls.Add(this.ConstraintMin);
            this.ConstraintOptions.Controls.Add(this.label4);
            this.ConstraintOptions.ForeColor = System.Drawing.Color.White;
            this.ConstraintOptions.Location = new System.Drawing.Point(4, 385);
            this.ConstraintOptions.Name = "ConstraintOptions";
            this.ConstraintOptions.Size = new System.Drawing.Size(349, 58);
            this.ConstraintOptions.TabIndex = 35;
            this.ConstraintOptions.TabStop = false;
            this.ConstraintOptions.Text = "Constraint Options";
            // 
            // EnableConstraint
            // 
            this.EnableConstraint.AutoSize = true;
            this.EnableConstraint.ForeColor = System.Drawing.Color.White;
            this.EnableConstraint.Location = new System.Drawing.Point(3, 362);
            this.EnableConstraint.Name = "EnableConstraint";
            this.EnableConstraint.Size = new System.Drawing.Size(107, 17);
            this.EnableConstraint.TabIndex = 0;
            this.EnableConstraint.Text = "Create Constraint";
            this.EnableConstraint.UseVisualStyleBackColor = true;
            this.EnableConstraint.CheckedChanged += new System.EventHandler(this.EnableConstraint_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(11, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Minimum:";
            // 
            // ConstraintMin
            // 
            this.ConstraintMin.BackColor = System.Drawing.Color.DimGray;
            this.ConstraintMin.ForeColor = System.Drawing.Color.White;
            this.ConstraintMin.Location = new System.Drawing.Point(68, 24);
            this.ConstraintMin.Name = "ConstraintMin";
            this.ConstraintMin.Size = new System.Drawing.Size(64, 20);
            this.ConstraintMin.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(138, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "deg";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(311, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "deg";
            // 
            // ConstraintMax
            // 
            this.ConstraintMax.BackColor = System.Drawing.Color.DimGray;
            this.ConstraintMax.ForeColor = System.Drawing.Color.White;
            this.ConstraintMax.Location = new System.Drawing.Point(241, 24);
            this.ConstraintMax.Name = "ConstraintMax";
            this.ConstraintMax.Size = new System.Drawing.Size(64, 20);
            this.ConstraintMax.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(184, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Maximum:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AngleType);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 49);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Angle Type";
            // 
            // SolarPhasePlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.EnableConstraint);
            this.Controls.Add(this.ConstraintOptions);
            this.Controls.Add(this.DefinitionGroupBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Calculate);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SolarPhasePlugin";
            this.Size = new System.Drawing.Size(357, 613);
            this.DefinitionGroupBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ConstraintOptions.ResumeLayout(false);
            this.ConstraintOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView TargetsList;
        private System.Windows.Forms.ColumnHeader TargetName;
        private System.Windows.Forms.ListView ObserversList;
        private System.Windows.Forms.ColumnHeader ObserverName;
        private System.Windows.Forms.Button Calculate;
        private System.Windows.Forms.Button UnselectTarget;
        private System.Windows.Forms.Button SelectTarget;
        private System.Windows.Forms.Button UnselectObserver;
        private System.Windows.Forms.Button SelectObserver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ObserverType;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TargetType;
        private System.Windows.Forms.ComboBox AngleType;
        private System.Windows.Forms.GroupBox DefinitionGroupBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox ConstraintOptions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ConstraintMax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ConstraintMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox EnableConstraint;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}