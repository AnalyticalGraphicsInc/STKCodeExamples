namespace OperatorsToolbox.PlaneCrossingUtility
{
    partial class PlaneCrossingPlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaneCrossingPlugin));
            this.label3 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Cancel = new System.Windows.Forms.Button();
            this.PlaneDefinitionGB = new System.Windows.Forms.GroupBox();
            this.ConditionalUB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ConditionalLB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ConditionalCrossing = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PlaneSatellite = new System.Windows.Forms.ComboBox();
            this.CrossingObjectsGB = new System.Windows.Forms.GroupBox();
            this.RemoveObject = new System.Windows.Forms.Button();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.CrossingObjectsList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddObject = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ObjectClass = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ExportToTxt = new System.Windows.Forms.CheckBox();
            this.AddToTimeline = new System.Windows.Forms.CheckBox();
            this.Calculate = new System.Windows.Forms.Button();
            this.ShowTimes = new System.Windows.Forms.Button();
            this.PlaneDefinitionGB.SuspendLayout();
            this.CrossingObjectsGB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Firebrick;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 23);
            this.label3.TabIndex = 34;
            this.label3.Text = "Plane Crossings";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "x-mark.png");
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.imageList1;
            this.Cancel.Location = new System.Drawing.Point(283, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(25, 25);
            this.Cancel.TabIndex = 33;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // PlaneDefinitionGB
            // 
            this.PlaneDefinitionGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaneDefinitionGB.Controls.Add(this.ConditionalUB);
            this.PlaneDefinitionGB.Controls.Add(this.label6);
            this.PlaneDefinitionGB.Controls.Add(this.label7);
            this.PlaneDefinitionGB.Controls.Add(this.ConditionalLB);
            this.PlaneDefinitionGB.Controls.Add(this.label5);
            this.PlaneDefinitionGB.Controls.Add(this.label4);
            this.PlaneDefinitionGB.Controls.Add(this.ConditionalCrossing);
            this.PlaneDefinitionGB.Controls.Add(this.label1);
            this.PlaneDefinitionGB.Controls.Add(this.PlaneSatellite);
            this.PlaneDefinitionGB.ForeColor = System.Drawing.Color.White;
            this.PlaneDefinitionGB.Location = new System.Drawing.Point(4, 39);
            this.PlaneDefinitionGB.Name = "PlaneDefinitionGB";
            this.PlaneDefinitionGB.Size = new System.Drawing.Size(303, 124);
            this.PlaneDefinitionGB.TabIndex = 35;
            this.PlaneDefinitionGB.TabStop = false;
            this.PlaneDefinitionGB.Text = "Plane Definition";
            // 
            // ConditionalUB
            // 
            this.ConditionalUB.BackColor = System.Drawing.Color.DimGray;
            this.ConditionalUB.ForeColor = System.Drawing.Color.White;
            this.ConditionalUB.Location = new System.Drawing.Point(85, 90);
            this.ConditionalUB.Name = "ConditionalUB";
            this.ConditionalUB.Size = new System.Drawing.Size(46, 20);
            this.ConditionalUB.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(137, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "deg";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Upper Bound:";
            // 
            // ConditionalLB
            // 
            this.ConditionalLB.BackColor = System.Drawing.Color.DimGray;
            this.ConditionalLB.ForeColor = System.Drawing.Color.White;
            this.ConditionalLB.Location = new System.Drawing.Point(85, 64);
            this.ConditionalLB.Name = "ConditionalLB";
            this.ConditionalLB.Size = new System.Drawing.Size(46, 20);
            this.ConditionalLB.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(137, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "deg";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Lower Bound:";
            // 
            // ConditionalCrossing
            // 
            this.ConditionalCrossing.AutoSize = true;
            this.ConditionalCrossing.Location = new System.Drawing.Point(6, 44);
            this.ConditionalCrossing.Name = "ConditionalCrossing";
            this.ConditionalCrossing.Size = new System.Drawing.Size(157, 17);
            this.ConditionalCrossing.TabIndex = 2;
            this.ConditionalCrossing.Text = "Enable Conditional Crossing";
            this.ConditionalCrossing.UseVisualStyleBackColor = true;
            this.ConditionalCrossing.CheckedChanged += new System.EventHandler(this.ConditionalCrossing_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plane Reference:";
            // 
            // PlaneSatellite
            // 
            this.PlaneSatellite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaneSatellite.BackColor = System.Drawing.Color.DimGray;
            this.PlaneSatellite.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PlaneSatellite.ForeColor = System.Drawing.Color.White;
            this.PlaneSatellite.FormattingEnabled = true;
            this.PlaneSatellite.Location = new System.Drawing.Point(114, 20);
            this.PlaneSatellite.Name = "PlaneSatellite";
            this.PlaneSatellite.Size = new System.Drawing.Size(183, 21);
            this.PlaneSatellite.TabIndex = 0;
            this.PlaneSatellite.SelectedIndexChanged += new System.EventHandler(this.PlaneSatellite_SelectedIndexChanged);
            // 
            // CrossingObjectsGB
            // 
            this.CrossingObjectsGB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrossingObjectsGB.Controls.Add(this.RemoveObject);
            this.CrossingObjectsGB.Controls.Add(this.CrossingObjectsList);
            this.CrossingObjectsGB.Controls.Add(this.AddObject);
            this.CrossingObjectsGB.Controls.Add(this.label2);
            this.CrossingObjectsGB.Controls.Add(this.ObjectClass);
            this.CrossingObjectsGB.ForeColor = System.Drawing.Color.White;
            this.CrossingObjectsGB.Location = new System.Drawing.Point(4, 169);
            this.CrossingObjectsGB.Name = "CrossingObjectsGB";
            this.CrossingObjectsGB.Size = new System.Drawing.Size(303, 226);
            this.CrossingObjectsGB.TabIndex = 36;
            this.CrossingObjectsGB.TabStop = false;
            this.CrossingObjectsGB.Text = "Crossing Objects";
            // 
            // RemoveObject
            // 
            this.RemoveObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveObject.ImageIndex = 1;
            this.RemoveObject.ImageList = this.imageList3;
            this.RemoveObject.Location = new System.Drawing.Point(9, 87);
            this.RemoveObject.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveObject.Name = "RemoveObject";
            this.RemoveObject.Size = new System.Drawing.Size(40, 40);
            this.RemoveObject.TabIndex = 51;
            this.RemoveObject.UseVisualStyleBackColor = true;
            this.RemoveObject.Click += new System.EventHandler(this.RemoveObject_Click);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "add.png");
            this.imageList3.Images.SetKeyName(1, "delete.png");
            this.imageList3.Images.SetKeyName(2, "paint-brush.png");
            this.imageList3.Images.SetKeyName(3, "x-mark.png");
            this.imageList3.Images.SetKeyName(4, "home.png");
            this.imageList3.Images.SetKeyName(5, "refresh.png");
            // 
            // CrossingObjectsList
            // 
            this.CrossingObjectsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrossingObjectsList.BackColor = System.Drawing.Color.DimGray;
            this.CrossingObjectsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.CrossingObjectsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CrossingObjectsList.ForeColor = System.Drawing.Color.White;
            this.CrossingObjectsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.CrossingObjectsList.HideSelection = false;
            this.CrossingObjectsList.Location = new System.Drawing.Point(54, 46);
            this.CrossingObjectsList.Name = "CrossingObjectsList";
            this.CrossingObjectsList.Size = new System.Drawing.Size(243, 174);
            this.CrossingObjectsList.TabIndex = 4;
            this.CrossingObjectsList.UseCompatibleStateImageBehavior = false;
            this.CrossingObjectsList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 235;
            // 
            // AddObject
            // 
            this.AddObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddObject.ImageIndex = 0;
            this.AddObject.ImageList = this.imageList3;
            this.AddObject.Location = new System.Drawing.Point(9, 46);
            this.AddObject.Margin = new System.Windows.Forms.Padding(2);
            this.AddObject.Name = "AddObject";
            this.AddObject.Size = new System.Drawing.Size(40, 40);
            this.AddObject.TabIndex = 50;
            this.AddObject.UseVisualStyleBackColor = true;
            this.AddObject.Click += new System.EventHandler(this.AddObject_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Object Class:";
            // 
            // ObjectClass
            // 
            this.ObjectClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectClass.BackColor = System.Drawing.Color.DimGray;
            this.ObjectClass.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ObjectClass.ForeColor = System.Drawing.Color.White;
            this.ObjectClass.FormattingEnabled = true;
            this.ObjectClass.Location = new System.Drawing.Point(114, 19);
            this.ObjectClass.Name = "ObjectClass";
            this.ObjectClass.Size = new System.Drawing.Size(183, 21);
            this.ObjectClass.TabIndex = 2;
            this.ObjectClass.SelectedIndexChanged += new System.EventHandler(this.ObjectClass_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ExportToTxt);
            this.groupBox1.Controls.Add(this.AddToTimeline);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(4, 401);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 78);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Options";
            // 
            // ExportToTxt
            // 
            this.ExportToTxt.AutoSize = true;
            this.ExportToTxt.Location = new System.Drawing.Point(9, 42);
            this.ExportToTxt.Name = "ExportToTxt";
            this.ExportToTxt.Size = new System.Drawing.Size(82, 17);
            this.ExportToTxt.TabIndex = 1;
            this.ExportToTxt.Text = "Export to txt";
            this.ExportToTxt.UseVisualStyleBackColor = true;
            // 
            // AddToTimeline
            // 
            this.AddToTimeline.AutoSize = true;
            this.AddToTimeline.Checked = true;
            this.AddToTimeline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AddToTimeline.Location = new System.Drawing.Point(9, 19);
            this.AddToTimeline.Name = "AddToTimeline";
            this.AddToTimeline.Size = new System.Drawing.Size(199, 17);
            this.AddToTimeline.TabIndex = 0;
            this.AddToTimeline.Text = "Add Crossing Times to Timeline View";
            this.AddToTimeline.UseVisualStyleBackColor = true;
            // 
            // Calculate
            // 
            this.Calculate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Calculate.BackColor = System.Drawing.Color.SteelBlue;
            this.Calculate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Calculate.ForeColor = System.Drawing.Color.White;
            this.Calculate.Location = new System.Drawing.Point(21, 484);
            this.Calculate.Margin = new System.Windows.Forms.Padding(2);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(114, 37);
            this.Calculate.TabIndex = 54;
            this.Calculate.Text = "Calculate";
            this.Calculate.UseVisualStyleBackColor = false;
            this.Calculate.Click += new System.EventHandler(this.Calculate_Click);
            // 
            // ShowTimes
            // 
            this.ShowTimes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowTimes.BackColor = System.Drawing.Color.SteelBlue;
            this.ShowTimes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ShowTimes.ForeColor = System.Drawing.Color.White;
            this.ShowTimes.Location = new System.Drawing.Point(180, 484);
            this.ShowTimes.Margin = new System.Windows.Forms.Padding(2);
            this.ShowTimes.Name = "ShowTimes";
            this.ShowTimes.Size = new System.Drawing.Size(114, 37);
            this.ShowTimes.TabIndex = 55;
            this.ShowTimes.Text = "Show Times";
            this.ShowTimes.UseVisualStyleBackColor = false;
            this.ShowTimes.Click += new System.EventHandler(this.ShowTimes_Click);
            // 
            // PlaneCrossingPlugin
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.ShowTimes);
            this.Controls.Add(this.Calculate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CrossingObjectsGB);
            this.Controls.Add(this.PlaneDefinitionGB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cancel);
            this.Name = "PlaneCrossingPlugin";
            this.Size = new System.Drawing.Size(310, 606);
            this.PlaneDefinitionGB.ResumeLayout(false);
            this.PlaneDefinitionGB.PerformLayout();
            this.CrossingObjectsGB.ResumeLayout(false);
            this.CrossingObjectsGB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox PlaneDefinitionGB;
        private System.Windows.Forms.GroupBox CrossingObjectsGB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox ConditionalUB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ConditionalLB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ConditionalCrossing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PlaneSatellite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ObjectClass;
        private System.Windows.Forms.CheckBox ExportToTxt;
        private System.Windows.Forms.CheckBox AddToTimeline;
        private System.Windows.Forms.ListView CrossingObjectsList;
        private System.Windows.Forms.Button RemoveObject;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.Button AddObject;
        private System.Windows.Forms.Button Calculate;
        private System.Windows.Forms.Button ShowTimes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
