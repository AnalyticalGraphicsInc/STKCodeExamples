namespace Agi.Ui.GreatArc
{
    partial class DirectionsUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectionsUI));
            this.comboBox_ToObj = new System.Windows.Forms.ComboBox();
            this.button_Close = new System.Windows.Forms.Button();
            this.speedUnitsComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.speedLabel = new System.Windows.Forms.Label();
            this.speedTextBox = new System.Windows.Forms.TextBox();
            this.terrainCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox_FromObj = new System.Windows.Forms.ComboBox();
            this.toTextBox = new System.Windows.Forms.TextBox();
            this.fromTextBox = new System.Windows.Forms.TextBox();
            this.fromLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_Text = new System.Windows.Forms.RadioButton();
            this.radioButton_Objs = new System.Windows.Forms.RadioButton();
            this.button_LoadGV = new System.Windows.Forms.Button();
            this.button_Help = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_ToObj
            // 
            this.comboBox_ToObj.FormattingEnabled = true;
            this.comboBox_ToObj.Location = new System.Drawing.Point(186, 34);
            this.comboBox_ToObj.Name = "comboBox_ToObj";
            this.comboBox_ToObj.Size = new System.Drawing.Size(160, 21);
            this.comboBox_ToObj.TabIndex = 16;
            this.comboBox_ToObj.SelectedIndexChanged += new System.EventHandler(this.GenerateGVName);
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(210, 214);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 26;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // speedUnitsComboBox
            // 
            this.speedUnitsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.speedUnitsComboBox.FormattingEnabled = true;
            this.speedUnitsComboBox.Items.AddRange(new object[] {
            "km/h",
            "mph",
            "m/s"});
            this.speedUnitsComboBox.Location = new System.Drawing.Point(95, 24);
            this.speedUnitsComboBox.Name = "speedUnitsComboBox";
            this.speedUnitsComboBox.Size = new System.Drawing.Size(58, 21);
            this.speedUnitsComboBox.TabIndex = 11;
            this.speedUnitsComboBox.Text = "mph";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.speedLabel);
            this.groupBox3.Controls.Add(this.speedTextBox);
            this.groupBox3.Controls.Add(this.speedUnitsComboBox);
            this.groupBox3.Controls.Add(this.terrainCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 153);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 84);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Object Properties";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(6, 27);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(41, 13);
            this.speedLabel.TabIndex = 10;
            this.speedLabel.Text = "Speed:";
            // 
            // speedTextBox
            // 
            this.speedTextBox.Location = new System.Drawing.Point(53, 24);
            this.speedTextBox.Name = "speedTextBox";
            this.speedTextBox.Size = new System.Drawing.Size(36, 20);
            this.speedTextBox.TabIndex = 9;
            this.speedTextBox.Text = "60";
            // 
            // terrainCheckBox
            // 
            this.terrainCheckBox.AutoSize = true;
            this.terrainCheckBox.Location = new System.Drawing.Point(9, 51);
            this.terrainCheckBox.Name = "terrainCheckBox";
            this.terrainCheckBox.Size = new System.Drawing.Size(119, 17);
            this.terrainCheckBox.TabIndex = 12;
            this.terrainCheckBox.Text = "Use Terrain Altitude";
            this.terrainCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox_ToObj);
            this.groupBox2.Controls.Add(this.comboBox_FromObj);
            this.groupBox2.Controls.Add(this.toTextBox);
            this.groupBox2.Controls.Add(this.fromTextBox);
            this.groupBox2.Controls.Add(this.fromLabel);
            this.groupBox2.Controls.Add(this.toLabel);
            this.groupBox2.Location = new System.Drawing.Point(12, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(354, 75);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Specify Endpoints";
            // 
            // comboBox_FromObj
            // 
            this.comboBox_FromObj.FormattingEnabled = true;
            this.comboBox_FromObj.Location = new System.Drawing.Point(9, 34);
            this.comboBox_FromObj.Name = "comboBox_FromObj";
            this.comboBox_FromObj.Size = new System.Drawing.Size(160, 21);
            this.comboBox_FromObj.TabIndex = 15;
            this.comboBox_FromObj.SelectedIndexChanged += new System.EventHandler(this.GenerateGVName);
            // 
            // toTextBox
            // 
            this.toTextBox.Location = new System.Drawing.Point(186, 35);
            this.toTextBox.Name = "toTextBox";
            this.toTextBox.Size = new System.Drawing.Size(160, 20);
            this.toTextBox.TabIndex = 5;
            this.toTextBox.Visible = false;
            this.toTextBox.TextChanged += new System.EventHandler(this.GenerateGVName);
            // 
            // fromTextBox
            // 
            this.fromTextBox.Location = new System.Drawing.Point(9, 35);
            this.fromTextBox.Name = "fromTextBox";
            this.fromTextBox.Size = new System.Drawing.Size(160, 20);
            this.fromTextBox.TabIndex = 3;
            this.fromTextBox.Visible = false;
            this.fromTextBox.TextChanged += new System.EventHandler(this.GenerateGVName);
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Location = new System.Drawing.Point(6, 19);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(33, 13);
            this.fromLabel.TabIndex = 4;
            this.fromLabel.Text = "From:";
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(186, 19);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(23, 13);
            this.toLabel.TabIndex = 6;
            this.toLabel.Text = "To:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_Text);
            this.groupBox1.Controls.Add(this.radioButton_Objs);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 54);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Endpoint Method";
            // 
            // radioButton_Text
            // 
            this.radioButton_Text.AutoSize = true;
            this.radioButton_Text.Location = new System.Drawing.Point(134, 21);
            this.radioButton_Text.Name = "radioButton_Text";
            this.radioButton_Text.Size = new System.Drawing.Size(91, 17);
            this.radioButton_Text.TabIndex = 13;
            this.radioButton_Text.Text = "Enter Address";
            this.radioButton_Text.UseVisualStyleBackColor = true;
            this.radioButton_Text.CheckedChanged += new System.EventHandler(this.radioButton_Text_CheckedChanged);
            // 
            // radioButton_Objs
            // 
            this.radioButton_Objs.AutoSize = true;
            this.radioButton_Objs.Checked = true;
            this.radioButton_Objs.Location = new System.Drawing.Point(10, 21);
            this.radioButton_Objs.Name = "radioButton_Objs";
            this.radioButton_Objs.Size = new System.Drawing.Size(118, 17);
            this.radioButton_Objs.TabIndex = 14;
            this.radioButton_Objs.TabStop = true;
            this.radioButton_Objs.Text = "Select STK Objects";
            this.radioButton_Objs.UseVisualStyleBackColor = true;
            this.radioButton_Objs.CheckedChanged += new System.EventHandler(this.radioButton_Objs_CheckedChanged);
            // 
            // button_LoadGV
            // 
            this.button_LoadGV.Location = new System.Drawing.Point(210, 184);
            this.button_LoadGV.Name = "button_LoadGV";
            this.button_LoadGV.Size = new System.Drawing.Size(156, 23);
            this.button_LoadGV.TabIndex = 22;
            this.button_LoadGV.Text = "Create Vehicle";
            this.button_LoadGV.UseVisualStyleBackColor = true;
            this.button_LoadGV.Click += new System.EventHandler(this.button_LoadGV_Click);
            // 
            // button_Help
            // 
            this.button_Help.Location = new System.Drawing.Point(291, 214);
            this.button_Help.Name = "button_Help";
            this.button_Help.Size = new System.Drawing.Size(75, 23);
            this.button_Help.TabIndex = 27;
            this.button_Help.Text = "Help";
            this.button_Help.UseVisualStyleBackColor = true;
            this.button_Help.Click += new System.EventHandler(this.button_Help_Click);
            // 
            // DirectionsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(377, 249);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_LoadGV);
            this.Controls.Add(this.button_Help);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DirectionsUI";
            this.Text = "Ground Vehicle from Directions";
            this.TopMost = true;
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_ToObj;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.ComboBox speedUnitsComboBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.TextBox speedTextBox;
        private System.Windows.Forms.CheckBox terrainCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox_FromObj;
        private System.Windows.Forms.TextBox toTextBox;
        private System.Windows.Forms.TextBox fromTextBox;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_Text;
        private System.Windows.Forms.RadioButton radioButton_Objs;
        private System.Windows.Forms.Button button_LoadGV;
        private System.Windows.Forms.Button button_Help;
    }
}