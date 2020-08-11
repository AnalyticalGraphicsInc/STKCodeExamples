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
            this.SuspendLayout();
            // 
            // TargetsList
            // 
            this.TargetsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetsList.BackColor = System.Drawing.Color.DimGray;
            this.TargetsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TargetsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TargetName});
            this.TargetsList.ForeColor = System.Drawing.Color.White;
            this.TargetsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.TargetsList.Location = new System.Drawing.Point(152, 93);
            this.TargetsList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TargetsList.Name = "TargetsList";
            this.TargetsList.Size = new System.Drawing.Size(137, 152);
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
            this.ObserversList.BackColor = System.Drawing.Color.DimGray;
            this.ObserversList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ObserversList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ObserverName});
            this.ObserversList.ForeColor = System.Drawing.Color.White;
            this.ObserversList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ObserversList.Location = new System.Drawing.Point(6, 93);
            this.ObserversList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ObserversList.Name = "ObserversList";
            this.ObserversList.Size = new System.Drawing.Size(133, 152);
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
            this.Calculate.Location = new System.Drawing.Point(63, 289);
            this.Calculate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(159, 42);
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
            this.UnselectTarget.Location = new System.Drawing.Point(233, 250);
            this.UnselectTarget.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UnselectTarget.Name = "UnselectTarget";
            this.UnselectTarget.Size = new System.Drawing.Size(56, 25);
            this.UnselectTarget.TabIndex = 22;
            this.UnselectTarget.Text = "unselect";
            this.UnselectTarget.UseVisualStyleBackColor = false;
            this.UnselectTarget.Click += new System.EventHandler(this.UnselectTarget_Click);
            // 
            // SelectTarget
            // 
            this.SelectTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectTarget.BackColor = System.Drawing.Color.SteelBlue;
            this.SelectTarget.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectTarget.ForeColor = System.Drawing.Color.White;
            this.SelectTarget.Location = new System.Drawing.Point(152, 249);
            this.SelectTarget.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SelectTarget.Name = "SelectTarget";
            this.SelectTarget.Size = new System.Drawing.Size(52, 25);
            this.SelectTarget.TabIndex = 21;
            this.SelectTarget.Text = "Select";
            this.SelectTarget.UseVisualStyleBackColor = false;
            this.SelectTarget.Click += new System.EventHandler(this.SelectTarget_Click);
            // 
            // UnselectObserver
            // 
            this.UnselectObserver.BackColor = System.Drawing.Color.SteelBlue;
            this.UnselectObserver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UnselectObserver.ForeColor = System.Drawing.Color.White;
            this.UnselectObserver.Location = new System.Drawing.Point(84, 250);
            this.UnselectObserver.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.SelectObserver.Location = new System.Drawing.Point(6, 250);
            this.SelectObserver.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SelectObserver.Name = "SelectObserver";
            this.SelectObserver.Size = new System.Drawing.Size(50, 25);
            this.SelectObserver.TabIndex = 19;
            this.SelectObserver.Text = "Select";
            this.SelectObserver.UseVisualStyleBackColor = false;
            this.SelectObserver.Click += new System.EventHandler(this.SelectObserver_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(208, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Targets";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(30, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Observers";
            // 
            // ObserverType
            // 
            this.ObserverType.BackColor = System.Drawing.Color.DimGray;
            this.ObserverType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ObserverType.ForeColor = System.Drawing.Color.White;
            this.ObserverType.FormattingEnabled = true;
            this.ObserverType.Location = new System.Drawing.Point(6, 51);
            this.ObserverType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ObserverType.Name = "ObserverType";
            this.ObserverType.Size = new System.Drawing.Size(133, 21);
            this.ObserverType.TabIndex = 16;
            this.ObserverType.SelectedIndexChanged += new System.EventHandler(this.ObserverType_SelectedIndexChanged);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.imageList1;
            this.Cancel.Location = new System.Drawing.Point(274, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.label3.Size = new System.Drawing.Size(112, 23);
            this.label3.TabIndex = 30;
            this.label3.Text = "Beta Angle";
            // 
            // TargetType
            // 
            this.TargetType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetType.BackColor = System.Drawing.Color.DimGray;
            this.TargetType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TargetType.ForeColor = System.Drawing.Color.White;
            this.TargetType.FormattingEnabled = true;
            this.TargetType.Location = new System.Drawing.Point(152, 51);
            this.TargetType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TargetType.Name = "TargetType";
            this.TargetType.Size = new System.Drawing.Size(137, 21);
            this.TargetType.TabIndex = 32;
            this.TargetType.SelectedIndexChanged += new System.EventHandler(this.TargetType_SelectedIndexChanged);
            // 
            // SolarPhasePlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(300, 569);
            this.Controls.Add(this.TargetType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.TargetsList);
            this.Controls.Add(this.ObserversList);
            this.Controls.Add(this.Calculate);
            this.Controls.Add(this.UnselectTarget);
            this.Controls.Add(this.SelectTarget);
            this.Controls.Add(this.UnselectObserver);
            this.Controls.Add(this.SelectObserver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ObserverType);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SolarPhasePlugin";
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
    }
}