namespace OperatorsToolbox.PlaneCrossingUtility
{
    partial class CrossingOutputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrossingOutputForm));
            this.CrossingOutputTable = new System.Windows.Forms.ListView();
            this.CrossingObjectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CrossingTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LowerBoundTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UpperBoundTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SetAnimationTime = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CrossingOutputTable
            // 
            this.CrossingOutputTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrossingOutputTable.BackColor = System.Drawing.Color.DimGray;
            this.CrossingOutputTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CrossingObjectName,
            this.CrossingTime,
            this.LowerBoundTime,
            this.UpperBoundTime});
            this.CrossingOutputTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CrossingOutputTable.ForeColor = System.Drawing.Color.White;
            this.CrossingOutputTable.FullRowSelect = true;
            this.CrossingOutputTable.GridLines = true;
            this.CrossingOutputTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.CrossingOutputTable.HideSelection = false;
            this.CrossingOutputTable.Location = new System.Drawing.Point(12, 12);
            this.CrossingOutputTable.MultiSelect = false;
            this.CrossingOutputTable.Name = "CrossingOutputTable";
            this.CrossingOutputTable.Size = new System.Drawing.Size(764, 368);
            this.CrossingOutputTable.TabIndex = 0;
            this.CrossingOutputTable.UseCompatibleStateImageBehavior = false;
            this.CrossingOutputTable.View = System.Windows.Forms.View.Details;
            // 
            // CrossingObjectName
            // 
            this.CrossingObjectName.Text = "Crossing Object";
            this.CrossingObjectName.Width = 120;
            // 
            // CrossingTime
            // 
            this.CrossingTime.Text = "Crossing Time (UTCG)";
            this.CrossingTime.Width = 200;
            // 
            // LowerBoundTime
            // 
            this.LowerBoundTime.Text = "Lower Bound Crossing Time (UTCG)";
            this.LowerBoundTime.Width = 220;
            // 
            // UpperBoundTime
            // 
            this.UpperBoundTime.Text = "Upper Bound Crossing Time (UTCG)";
            this.UpperBoundTime.Width = 220;
            // 
            // SetAnimationTime
            // 
            this.SetAnimationTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SetAnimationTime.BackColor = System.Drawing.Color.SteelBlue;
            this.SetAnimationTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SetAnimationTime.Location = new System.Drawing.Point(12, 386);
            this.SetAnimationTime.Name = "SetAnimationTime";
            this.SetAnimationTime.Size = new System.Drawing.Size(84, 50);
            this.SetAnimationTime.TabIndex = 1;
            this.SetAnimationTime.Text = "Set Animation Time";
            this.SetAnimationTime.UseVisualStyleBackColor = false;
            this.SetAnimationTime.Click += new System.EventHandler(this.SetAnimationTime_Click);
            // 
            // CrossingOutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(788, 445);
            this.Controls.Add(this.SetAnimationTime);
            this.Controls.Add(this.CrossingOutputTable);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CrossingOutputForm";
            this.Text = "Crossing Output";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView CrossingOutputTable;
        private System.Windows.Forms.ColumnHeader CrossingObjectName;
        private System.Windows.Forms.ColumnHeader CrossingTime;
        private System.Windows.Forms.ColumnHeader LowerBoundTime;
        private System.Windows.Forms.ColumnHeader UpperBoundTime;
        private System.Windows.Forms.Button SetAnimationTime;
    }
}