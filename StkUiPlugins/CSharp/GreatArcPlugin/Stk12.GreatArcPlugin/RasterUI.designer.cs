namespace Agi.Ui.GreatArc.Stk12
{
    partial class RasterUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RasterUI));
            this.button_Close = new System.Windows.Forms.Button();
            this.button_LoadAC = new System.Windows.Forms.Button();
            this.button_Help = new System.Windows.Forms.Button();
            this.textBox_Speed = new System.Windows.Forms.TextBox();
            this.textBox_Altitude = new System.Windows.Forms.TextBox();
            this.textBox_TurnGs = new System.Windows.Forms.TextBox();
            this.label_Speed = new System.Windows.Forms.Label();
            this.label_Altitude = new System.Windows.Forms.Label();
            this.label_TurnGs = new System.Windows.Forms.Label();
            this.textBox_numPasses = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_SensorFOV = new System.Windows.Forms.TextBox();
            this.textBox_GrnElev = new System.Windows.Forms.TextBox();
            this.textBox_SlantRange = new System.Windows.Forms.TextBox();
            this.radioButton_NumPasses = new System.Windows.Forms.RadioButton();
            this.radioButton_SlantRange = new System.Windows.Forms.RadioButton();
            this.radioButton_GrnElev = new System.Windows.Forms.RadioButton();
            this.radioButton_SensorFOV = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.combobox_Units = new System.Windows.Forms.ComboBox();
            this.textBox_Endurance = new System.Windows.Forms.TextBox();
            this.checkBox_UseEndurance = new System.Windows.Forms.CheckBox();
            this.checkBox_useTakeoffLanding = new System.Windows.Forms.CheckBox();
            this.button_RemoveObject = new System.Windows.Forms.Button();
            this.button_AddObject = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.treeView_availableObjs = new System.Windows.Forms.TreeView();
            this.button_moveDown = new System.Windows.Forms.Button();
            this.button_moveUp = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(459, 283);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 26;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_LoadAC
            // 
            this.button_LoadAC.Location = new System.Drawing.Point(299, 283);
            this.button_LoadAC.Name = "button_LoadAC";
            this.button_LoadAC.Size = new System.Drawing.Size(154, 23);
            this.button_LoadAC.TabIndex = 22;
            this.button_LoadAC.Text = "Create Vehicle";
            this.button_LoadAC.UseVisualStyleBackColor = true;
            this.button_LoadAC.Click += new System.EventHandler(this.button_LoadAC_Click);
            // 
            // button_Help
            // 
            this.button_Help.Location = new System.Drawing.Point(538, 283);
            this.button_Help.Name = "button_Help";
            this.button_Help.Size = new System.Drawing.Size(75, 23);
            this.button_Help.TabIndex = 27;
            this.button_Help.Text = "Help";
            this.button_Help.UseVisualStyleBackColor = true;
            this.button_Help.Click += new System.EventHandler(this.button_Help_Click);
            // 
            // textBox_Speed
            // 
            this.textBox_Speed.Location = new System.Drawing.Point(96, 18);
            this.textBox_Speed.Name = "textBox_Speed";
            this.textBox_Speed.Size = new System.Drawing.Size(45, 20);
            this.textBox_Speed.TabIndex = 29;
            this.textBox_Speed.Text = "120";
            // 
            // textBox_Altitude
            // 
            this.textBox_Altitude.Location = new System.Drawing.Point(96, 41);
            this.textBox_Altitude.Name = "textBox_Altitude";
            this.textBox_Altitude.Size = new System.Drawing.Size(45, 20);
            this.textBox_Altitude.TabIndex = 30;
            this.textBox_Altitude.Text = "15000";
            // 
            // textBox_TurnGs
            // 
            this.textBox_TurnGs.Location = new System.Drawing.Point(96, 64);
            this.textBox_TurnGs.Name = "textBox_TurnGs";
            this.textBox_TurnGs.Size = new System.Drawing.Size(45, 20);
            this.textBox_TurnGs.TabIndex = 31;
            this.textBox_TurnGs.Text = "1";
            // 
            // label_Speed
            // 
            this.label_Speed.AutoSize = true;
            this.label_Speed.Location = new System.Drawing.Point(16, 21);
            this.label_Speed.Name = "label_Speed";
            this.label_Speed.Size = new System.Drawing.Size(76, 13);
            this.label_Speed.TabIndex = 32;
            this.label_Speed.Text = "Speed (knots):";
            // 
            // label_Altitude
            // 
            this.label_Altitude.AutoSize = true;
            this.label_Altitude.Location = new System.Drawing.Point(30, 44);
            this.label_Altitude.Name = "label_Altitude";
            this.label_Altitude.Size = new System.Drawing.Size(60, 13);
            this.label_Altitude.TabIndex = 33;
            this.label_Altitude.Text = "Altitude (ft):";
            // 
            // label_TurnGs
            // 
            this.label_TurnGs.AutoSize = true;
            this.label_TurnGs.Location = new System.Drawing.Point(42, 67);
            this.label_TurnGs.Name = "label_TurnGs";
            this.label_TurnGs.Size = new System.Drawing.Size(48, 13);
            this.label_TurnGs.TabIndex = 34;
            this.label_TurnGs.Text = "Turn Gs:";
            // 
            // textBox_numPasses
            // 
            this.textBox_numPasses.Location = new System.Drawing.Point(176, 89);
            this.textBox_numPasses.Name = "textBox_numPasses";
            this.textBox_numPasses.Size = new System.Drawing.Size(44, 20);
            this.textBox_numPasses.TabIndex = 35;
            this.textBox_numPasses.Text = "10";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_SensorFOV);
            this.groupBox1.Controls.Add(this.textBox_GrnElev);
            this.groupBox1.Controls.Add(this.textBox_SlantRange);
            this.groupBox1.Controls.Add(this.radioButton_NumPasses);
            this.groupBox1.Controls.Add(this.radioButton_SlantRange);
            this.groupBox1.Controls.Add(this.radioButton_GrnElev);
            this.groupBox1.Controls.Add(this.radioButton_SensorFOV);
            this.groupBox1.Controls.Add(this.textBox_numPasses);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 117);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Region Passes";
            // 
            // textBox_SensorFOV
            // 
            this.textBox_SensorFOV.Location = new System.Drawing.Point(176, 20);
            this.textBox_SensorFOV.Name = "textBox_SensorFOV";
            this.textBox_SensorFOV.Size = new System.Drawing.Size(44, 20);
            this.textBox_SensorFOV.TabIndex = 43;
            this.textBox_SensorFOV.Text = "15";
            // 
            // textBox_GrnElev
            // 
            this.textBox_GrnElev.Location = new System.Drawing.Point(176, 44);
            this.textBox_GrnElev.Name = "textBox_GrnElev";
            this.textBox_GrnElev.Size = new System.Drawing.Size(44, 20);
            this.textBox_GrnElev.TabIndex = 42;
            this.textBox_GrnElev.Text = "45";
            // 
            // textBox_SlantRange
            // 
            this.textBox_SlantRange.Location = new System.Drawing.Point(176, 66);
            this.textBox_SlantRange.Name = "textBox_SlantRange";
            this.textBox_SlantRange.Size = new System.Drawing.Size(44, 20);
            this.textBox_SlantRange.TabIndex = 41;
            this.textBox_SlantRange.Text = "5";
            // 
            // radioButton_NumPasses
            // 
            this.radioButton_NumPasses.AutoSize = true;
            this.radioButton_NumPasses.Location = new System.Drawing.Point(22, 90);
            this.radioButton_NumPasses.Name = "radioButton_NumPasses";
            this.radioButton_NumPasses.Size = new System.Drawing.Size(139, 17);
            this.radioButton_NumPasses.TabIndex = 40;
            this.radioButton_NumPasses.Text = "Fixed Number of Passes";
            this.radioButton_NumPasses.UseVisualStyleBackColor = true;
            this.radioButton_NumPasses.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_SlantRange
            // 
            this.radioButton_SlantRange.AutoSize = true;
            this.radioButton_SlantRange.Location = new System.Drawing.Point(22, 67);
            this.radioButton_SlantRange.Name = "radioButton_SlantRange";
            this.radioButton_SlantRange.Size = new System.Drawing.Size(130, 17);
            this.radioButton_SlantRange.TabIndex = 39;
            this.radioButton_SlantRange.Text = "Max Slant Range (km)";
            this.radioButton_SlantRange.UseVisualStyleBackColor = true;
            this.radioButton_SlantRange.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_GrnElev
            // 
            this.radioButton_GrnElev.AutoSize = true;
            this.radioButton_GrnElev.Location = new System.Drawing.Point(22, 44);
            this.radioButton_GrnElev.Name = "radioButton_GrnElev";
            this.radioButton_GrnElev.Size = new System.Drawing.Size(154, 17);
            this.radioButton_GrnElev.TabIndex = 38;
            this.radioButton_GrnElev.Text = "Min Ground Elevation (deg)";
            this.radioButton_GrnElev.UseVisualStyleBackColor = true;
            this.radioButton_GrnElev.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_SensorFOV
            // 
            this.radioButton_SensorFOV.AutoSize = true;
            this.radioButton_SensorFOV.Checked = true;
            this.radioButton_SensorFOV.Location = new System.Drawing.Point(22, 21);
            this.radioButton_SensorFOV.Name = "radioButton_SensorFOV";
            this.radioButton_SensorFOV.Size = new System.Drawing.Size(148, 17);
            this.radioButton_SensorFOV.TabIndex = 37;
            this.radioButton_SensorFOV.TabStop = true;
            this.radioButton_SensorFOV.Text = "Sensor Field of View (deg)";
            this.radioButton_SensorFOV.UseVisualStyleBackColor = true;
            this.radioButton_SensorFOV.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.combobox_Units);
            this.groupBox2.Controls.Add(this.textBox_Endurance);
            this.groupBox2.Controls.Add(this.checkBox_UseEndurance);
            this.groupBox2.Controls.Add(this.label_Speed);
            this.groupBox2.Controls.Add(this.textBox_Speed);
            this.groupBox2.Controls.Add(this.checkBox_useTakeoffLanding);
            this.groupBox2.Controls.Add(this.label_TurnGs);
            this.groupBox2.Controls.Add(this.textBox_Altitude);
            this.groupBox2.Controls.Add(this.label_Altitude);
            this.groupBox2.Controls.Add(this.textBox_TurnGs);
            this.groupBox2.Location = new System.Drawing.Point(269, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 117);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Aircraft Properties";
            // 
            // combobox_Units
            // 
            this.combobox_Units.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.combobox_Units.FormattingEnabled = true;
            this.combobox_Units.Items.AddRange(new object[] {
            "km/h",
            "mph",
            "m/s"});
            this.combobox_Units.Location = new System.Drawing.Point(257, 59);
            this.combobox_Units.Name = "combobox_Units";
            this.combobox_Units.Size = new System.Drawing.Size(58, 21);
            this.combobox_Units.TabIndex = 37;
            this.combobox_Units.Text = "hr";
            // 
            // textBox_Endurance
            // 
            this.textBox_Endurance.Location = new System.Drawing.Point(206, 60);
            this.textBox_Endurance.Name = "textBox_Endurance";
            this.textBox_Endurance.Size = new System.Drawing.Size(45, 20);
            this.textBox_Endurance.TabIndex = 36;
            this.textBox_Endurance.Text = "10";
            // 
            // checkBox_UseEndurance
            // 
            this.checkBox_UseEndurance.Location = new System.Drawing.Point(172, 40);
            this.checkBox_UseEndurance.Name = "checkBox_UseEndurance";
            this.checkBox_UseEndurance.Size = new System.Drawing.Size(156, 23);
            this.checkBox_UseEndurance.TabIndex = 35;
            this.checkBox_UseEndurance.Text = "Specify Endurance";
            this.checkBox_UseEndurance.UseVisualStyleBackColor = true;
            this.checkBox_UseEndurance.CheckedChanged += new System.EventHandler(this.checkBox_UseEndurance_CheckedChanged);
            // 
            // checkBox_useTakeoffLanding
            // 
            this.checkBox_useTakeoffLanding.Location = new System.Drawing.Point(172, 19);
            this.checkBox_useTakeoffLanding.Name = "checkBox_useTakeoffLanding";
            this.checkBox_useTakeoffLanding.Size = new System.Drawing.Size(156, 23);
            this.checkBox_useTakeoffLanding.TabIndex = 0;
            this.checkBox_useTakeoffLanding.Text = "Takeoff/Land at Endpoints";
            this.checkBox_useTakeoffLanding.UseVisualStyleBackColor = true;
            // 
            // button_RemoveObject
            // 
            this.button_RemoveObject.Image = global::Agi.Ui.GreatArc.Stk12.Properties.Resources.MoveLeft;
            this.button_RemoveObject.Location = new System.Drawing.Point(152, 210);
            this.button_RemoveObject.Name = "button_RemoveObject";
            this.button_RemoveObject.Size = new System.Drawing.Size(35, 22);
            this.button_RemoveObject.TabIndex = 43;
            this.button_RemoveObject.UseVisualStyleBackColor = true;
            this.button_RemoveObject.Click += new System.EventHandler(this.button_RemoveObject_Click);
            // 
            // button_AddObject
            // 
            this.button_AddObject.Image = global::Agi.Ui.GreatArc.Stk12.Properties.Resources.MoveRight;
            this.button_AddObject.Location = new System.Drawing.Point(152, 182);
            this.button_AddObject.Name = "button_AddObject";
            this.button_AddObject.Size = new System.Drawing.Size(35, 22);
            this.button_AddObject.TabIndex = 42;
            this.button_AddObject.UseVisualStyleBackColor = true;
            this.button_AddObject.Click += new System.EventHandler(this.button_AddObject_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Waypoints and Search Regions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Available Objects";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(191, 152);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(394, 121);
            this.dataGridView1.TabIndex = 47;
            // 
            // treeView_availableObjs
            // 
            this.treeView_availableObjs.Location = new System.Drawing.Point(12, 153);
            this.treeView_availableObjs.Name = "treeView_availableObjs";
            this.treeView_availableObjs.Size = new System.Drawing.Size(137, 153);
            this.treeView_availableObjs.TabIndex = 48;
            this.treeView_availableObjs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView_availableObjs_MouseDoubleClick);
            // 
            // button_moveDown
            // 
            this.button_moveDown.Image = global::Agi.Ui.GreatArc.Stk12.Properties.Resources.MoveDown;
            this.button_moveDown.Location = new System.Drawing.Point(591, 210);
            this.button_moveDown.Name = "button_moveDown";
            this.button_moveDown.Size = new System.Drawing.Size(22, 35);
            this.button_moveDown.TabIndex = 50;
            this.button_moveDown.UseVisualStyleBackColor = true;
            this.button_moveDown.Click += new System.EventHandler(this.button_moveDown_Click);
            // 
            // button_moveUp
            // 
            this.button_moveUp.Image = global::Agi.Ui.GreatArc.Stk12.Properties.Resources.MoveUp;
            this.button_moveUp.Location = new System.Drawing.Point(591, 169);
            this.button_moveUp.Name = "button_moveUp";
            this.button_moveUp.Size = new System.Drawing.Size(22, 35);
            this.button_moveUp.TabIndex = 49;
            this.button_moveUp.UseVisualStyleBackColor = true;
            this.button_moveUp.Click += new System.EventHandler(this.button_moveUp_Click);
            // 
            // RasterUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(627, 318);
            this.Controls.Add(this.button_moveDown);
            this.Controls.Add(this.button_moveUp);
            this.Controls.Add(this.treeView_availableObjs);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_RemoveObject);
            this.Controls.Add(this.button_AddObject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_LoadAC);
            this.Controls.Add(this.button_Help);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RasterUI";
            this.Text = "Aircraft from Search Pattern";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Button button_LoadAC;
        private System.Windows.Forms.Button button_Help;
        private System.Windows.Forms.TextBox textBox_Speed;
        private System.Windows.Forms.TextBox textBox_Altitude;
        private System.Windows.Forms.TextBox textBox_TurnGs;
        private System.Windows.Forms.Label label_Speed;
        private System.Windows.Forms.Label label_Altitude;
        private System.Windows.Forms.Label label_TurnGs;
        private System.Windows.Forms.TextBox textBox_numPasses;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_SensorFOV;
        private System.Windows.Forms.TextBox textBox_GrnElev;
        private System.Windows.Forms.TextBox textBox_SlantRange;
        private System.Windows.Forms.RadioButton radioButton_NumPasses;
        private System.Windows.Forms.RadioButton radioButton_SlantRange;
        private System.Windows.Forms.RadioButton radioButton_GrnElev;
        private System.Windows.Forms.RadioButton radioButton_SensorFOV;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_RemoveObject;
        private System.Windows.Forms.Button button_AddObject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_useTakeoffLanding;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox_Endurance;
        private System.Windows.Forms.CheckBox checkBox_UseEndurance;
        private System.Windows.Forms.ComboBox combobox_Units;
        private System.Windows.Forms.TreeView treeView_availableObjs;
        private System.Windows.Forms.Button button_moveDown;
        private System.Windows.Forms.Button button_moveUp;
    }
}