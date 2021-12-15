namespace Stk12.UiPlugin.Articulations
{
    partial class CustomUserInterface
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LoadArticFile = new System.Windows.Forms.Button();
            this.AddArtic = new System.Windows.Forms.Button();
            this.RemoveAll = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.CreatedArtic = new System.Windows.Forms.ListBox();
            this.PossibleArtic = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbStkObjects = new System.Windows.Forms.ComboBox();
            this.Edit = new System.Windows.Forms.Button();
            this.LinkToTime = new System.Windows.Forms.Button();
            this.RemoveLink = new System.Windows.Forms.Button();
            this.LinkToList = new System.Windows.Forms.Button();
            this.LinkToAttitude = new System.Windows.Forms.CheckBox();
            this.AttitudeCoordFrame = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LoadAttitude = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.MainBody = new System.Windows.Forms.ComboBox();
            this.UnloadArtic = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Paste = new System.Windows.Forms.Button();
            this.Copy = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(432, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Created Articulations";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Choose Articulation";
            // 
            // LoadArticFile
            // 
            this.LoadArticFile.Location = new System.Drawing.Point(15, 242);
            this.LoadArticFile.Name = "LoadArticFile";
            this.LoadArticFile.Size = new System.Drawing.Size(134, 53);
            this.LoadArticFile.TabIndex = 14;
            this.LoadArticFile.Text = "Load Articulation File";
            this.LoadArticFile.UseVisualStyleBackColor = true;
            this.LoadArticFile.Click += new System.EventHandler(this.LoadArticFile_Click);
            // 
            // AddArtic
            // 
            this.AddArtic.Location = new System.Drawing.Point(103, 181);
            this.AddArtic.Name = "AddArtic";
            this.AddArtic.Size = new System.Drawing.Size(134, 33);
            this.AddArtic.TabIndex = 13;
            this.AddArtic.Text = "Add";
            this.AddArtic.UseVisualStyleBackColor = true;
            this.AddArtic.Click += new System.EventHandler(this.AddArtic_Click);
            // 
            // RemoveAll
            // 
            this.RemoveAll.Location = new System.Drawing.Point(759, 359);
            this.RemoveAll.Name = "RemoveAll";
            this.RemoveAll.Size = new System.Drawing.Size(120, 30);
            this.RemoveAll.TabIndex = 12;
            this.RemoveAll.Text = "Remove All";
            this.RemoveAll.UseVisualStyleBackColor = true;
            this.RemoveAll.Click += new System.EventHandler(this.RemoveAll_Click);
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(758, 303);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(121, 30);
            this.Remove.TabIndex = 11;
            this.Remove.Text = "Remove";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // CreatedArtic
            // 
            this.CreatedArtic.FormattingEnabled = true;
            this.CreatedArtic.ItemHeight = 16;
            this.CreatedArtic.Location = new System.Drawing.Point(335, 49);
            this.CreatedArtic.Name = "CreatedArtic";
            this.CreatedArtic.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.CreatedArtic.Size = new System.Drawing.Size(397, 340);
            this.CreatedArtic.TabIndex = 10;
            this.CreatedArtic.SelectedIndexChanged += new System.EventHandler(this.CreatedArtic_SelectedIndexChanged);
            // 
            // PossibleArtic
            // 
            this.PossibleArtic.FormattingEnabled = true;
            this.PossibleArtic.Location = new System.Drawing.Point(16, 118);
            this.PossibleArtic.Name = "PossibleArtic";
            this.PossibleArtic.Size = new System.Drawing.Size(285, 24);
            this.PossibleArtic.TabIndex = 9;
            this.PossibleArtic.SelectedIndexChanged += new System.EventHandler(this.PossibleArtic_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(84, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Choose Object";
            // 
            // cbStkObjects
            // 
            this.cbStkObjects.FormattingEnabled = true;
            this.cbStkObjects.Location = new System.Drawing.Point(16, 49);
            this.cbStkObjects.Name = "cbStkObjects";
            this.cbStkObjects.Size = new System.Drawing.Size(285, 24);
            this.cbStkObjects.TabIndex = 18;
            this.cbStkObjects.SelectedIndexChanged += new System.EventHandler(this.cbStkObjects_SelectedIndexChanged);
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(759, 242);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(121, 31);
            this.Edit.TabIndex = 20;
            this.Edit.Text = "Edit";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // LinkToTime
            // 
            this.LinkToTime.Location = new System.Drawing.Point(759, 32);
            this.LinkToTime.Name = "LinkToTime";
            this.LinkToTime.Size = new System.Drawing.Size(121, 56);
            this.LinkToTime.TabIndex = 22;
            this.LinkToTime.Text = "Link to Time Instant";
            this.LinkToTime.UseVisualStyleBackColor = true;
            this.LinkToTime.Click += new System.EventHandler(this.LinkToTime_Click);
            // 
            // RemoveLink
            // 
            this.RemoveLink.Location = new System.Drawing.Point(758, 181);
            this.RemoveLink.Name = "RemoveLink";
            this.RemoveLink.Size = new System.Drawing.Size(121, 31);
            this.RemoveLink.TabIndex = 23;
            this.RemoveLink.Text = "Remove Link";
            this.RemoveLink.UseVisualStyleBackColor = true;
            this.RemoveLink.Click += new System.EventHandler(this.RemoveLink_Click);
            // 
            // LinkToList
            // 
            this.LinkToList.Location = new System.Drawing.Point(758, 101);
            this.LinkToList.Name = "LinkToList";
            this.LinkToList.Size = new System.Drawing.Size(121, 56);
            this.LinkToList.TabIndex = 24;
            this.LinkToList.Text = "Link to Interval List";
            this.LinkToList.UseVisualStyleBackColor = true;
            this.LinkToList.Click += new System.EventHandler(this.LinkToList_Click);
            // 
            // LinkToAttitude
            // 
            this.LinkToAttitude.AutoSize = true;
            this.LinkToAttitude.Location = new System.Drawing.Point(14, 65);
            this.LinkToAttitude.Name = "LinkToAttitude";
            this.LinkToAttitude.Size = new System.Drawing.Size(269, 21);
            this.LinkToAttitude.TabIndex = 25;
            this.LinkToAttitude.Text = "Link Articulations to Analytical Attitude";
            this.LinkToAttitude.UseVisualStyleBackColor = true;
            this.LinkToAttitude.CheckedChanged += new System.EventHandler(this.LinkToAttitude_CheckedChanged);
            // 
            // AttitudeCoordFrame
            // 
            this.AttitudeCoordFrame.FormattingEnabled = true;
            this.AttitudeCoordFrame.Location = new System.Drawing.Point(142, 144);
            this.AttitudeCoordFrame.Name = "AttitudeCoordFrame";
            this.AttitudeCoordFrame.Size = new System.Drawing.Size(166, 24);
            this.AttitudeCoordFrame.TabIndex = 26;
            this.AttitudeCoordFrame.SelectedIndexChanged += new System.EventHandler(this.AttitudeCoordFrame_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "Coordinate Frame :";
            // 
            // LoadAttitude
            // 
            this.LoadAttitude.Location = new System.Drawing.Point(41, 188);
            this.LoadAttitude.Name = "LoadAttitude";
            this.LoadAttitude.Size = new System.Drawing.Size(175, 35);
            this.LoadAttitude.TabIndex = 28;
            this.LoadAttitude.Text = "Load Attitude FIle";
            this.LoadAttitude.UseVisualStyleBackColor = true;
            this.LoadAttitude.Click += new System.EventHandler(this.LoadAttitude_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 17);
            this.label5.TabIndex = 30;
            this.label5.Text = "Model Main Body :";
            // 
            // MainBody
            // 
            this.MainBody.FormattingEnabled = true;
            this.MainBody.Location = new System.Drawing.Point(142, 102);
            this.MainBody.Name = "MainBody";
            this.MainBody.Size = new System.Drawing.Size(166, 24);
            this.MainBody.TabIndex = 29;
            this.MainBody.SelectedIndexChanged += new System.EventHandler(this.MainBody_SelectedIndexChanged);
            // 
            // UnloadArtic
            // 
            this.UnloadArtic.Location = new System.Drawing.Point(179, 242);
            this.UnloadArtic.Name = "UnloadArtic";
            this.UnloadArtic.Size = new System.Drawing.Size(134, 53);
            this.UnloadArtic.TabIndex = 31;
            this.UnloadArtic.Text = "Unload Articulation File";
            this.UnloadArtic.UseVisualStyleBackColor = true;
            this.UnloadArtic.Click += new System.EventHandler(this.UnloadArtic_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.MainBody);
            this.groupBox1.Controls.Add(this.LoadAttitude);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.AttitudeCoordFrame);
            this.groupBox1.Controls.Add(this.LinkToAttitude);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 507);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 257);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Attitude";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Paste);
            this.groupBox2.Controls.Add(this.Copy);
            this.groupBox2.Controls.Add(this.UnloadArtic);
            this.groupBox2.Controls.Add(this.LinkToList);
            this.groupBox2.Controls.Add(this.RemoveLink);
            this.groupBox2.Controls.Add(this.LinkToTime);
            this.groupBox2.Controls.Add(this.Edit);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbStkObjects);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.LoadArticFile);
            this.groupBox2.Controls.Add(this.AddArtic);
            this.groupBox2.Controls.Add(this.RemoveAll);
            this.groupBox2.Controls.Add(this.Remove);
            this.groupBox2.Controls.Add(this.CreatedArtic);
            this.groupBox2.Controls.Add(this.PossibleArtic);
            this.groupBox2.Location = new System.Drawing.Point(4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(891, 496);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            // 
            // Paste
            // 
            this.Paste.Location = new System.Drawing.Point(760, 460);
            this.Paste.Name = "Paste";
            this.Paste.Size = new System.Drawing.Size(120, 30);
            this.Paste.TabIndex = 33;
            this.Paste.Text = "Paste";
            this.Paste.UseVisualStyleBackColor = true;
            this.Paste.Click += new System.EventHandler(this.Paste_Click);
            // 
            // Copy
            // 
            this.Copy.Location = new System.Drawing.Point(758, 411);
            this.Copy.Name = "Copy";
            this.Copy.Size = new System.Drawing.Size(120, 30);
            this.Copy.TabIndex = 32;
            this.Copy.Text = "Copy";
            this.Copy.UseVisualStyleBackColor = true;
            this.Copy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(294, 17);
            this.label6.TabIndex = 31;
            this.label6.Text = "Note: Will only work for satellites and aircrafts";
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(909, 794);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LoadArticFile;
        private System.Windows.Forms.Button AddArtic;
        private System.Windows.Forms.Button RemoveAll;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.ComboBox PossibleArtic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbStkObjects;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.ListBox CreatedArtic;
        private System.Windows.Forms.Button LinkToTime;
        private System.Windows.Forms.Button RemoveLink;
        private System.Windows.Forms.Button LinkToList;
        private System.Windows.Forms.CheckBox LinkToAttitude;
        private System.Windows.Forms.ComboBox AttitudeCoordFrame;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button LoadAttitude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox MainBody;
        private System.Windows.Forms.Button UnloadArtic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Paste;
        private System.Windows.Forms.Button Copy;
        private System.Windows.Forms.Label label6;
    }
}
