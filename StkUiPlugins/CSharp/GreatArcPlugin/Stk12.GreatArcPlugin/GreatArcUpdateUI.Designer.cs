namespace Agi.Ui.GreatArc.Stk12
{
    partial class GreatArcUpdateUI
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
            this.label_Property = new System.Windows.Forms.Label();
            this.textbox_Parameter = new System.Windows.Forms.TextBox();
            this.combobox_Units = new System.Windows.Forms.ComboBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Property
            // 
            this.label_Property.AutoSize = true;
            this.label_Property.Location = new System.Drawing.Point(28, 23);
            this.label_Property.Name = "label_Property";
            this.label_Property.Size = new System.Drawing.Size(41, 13);
            this.label_Property.TabIndex = 13;
            this.label_Property.Text = "Speed:";
            // 
            // textbox_Parameter
            // 
            this.textbox_Parameter.Location = new System.Drawing.Point(31, 39);
            this.textbox_Parameter.Name = "textbox_Parameter";
            this.textbox_Parameter.Size = new System.Drawing.Size(36, 20);
            this.textbox_Parameter.TabIndex = 12;
            this.textbox_Parameter.Text = "60";
            // 
            // combobox_Units
            // 
            this.combobox_Units.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.combobox_Units.FormattingEnabled = true;
            this.combobox_Units.Items.AddRange(new object[] {
            "km/h",
            "mph",
            "m/s"});
            this.combobox_Units.Location = new System.Drawing.Point(73, 39);
            this.combobox_Units.Name = "combobox_Units";
            this.combobox_Units.Size = new System.Drawing.Size(58, 21);
            this.combobox_Units.TabIndex = 14;
            this.combobox_Units.Text = "mph";
            this.combobox_Units.SelectedIndexChanged += new System.EventHandler(this.combobox_Units_SelectedIndexChanged);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(119, 95);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 15;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(38, 95);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 16;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_Property);
            this.groupBox1.Controls.Add(this.textbox_Parameter);
            this.groupBox1.Controls.Add(this.combobox_Units);
            this.groupBox1.Location = new System.Drawing.Point(18, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 77);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Update Parameter";
            // 
            // GreatArcUpdateUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 131);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GreatArcUpdateUI";
            this.Text = "GreatArcUpdateUI";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Property;
        private System.Windows.Forms.TextBox textbox_Parameter;
        private System.Windows.Forms.ComboBox combobox_Units;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}