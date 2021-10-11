namespace PullDataFromSTKExample
{
    partial class MainForm
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxAzimuth = new System.Windows.Forms.TextBox();
            this.comboBoxSatellite = new System.Windows.Forms.ComboBox();
            this.textBoxElevation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonOpen = new System.Windows.Forms.RadioButton();
            this.radioButtonClose = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxFacility = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(24, 15);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(167, 43);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect to STK";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // textBoxAzimuth
            // 
            this.textBoxAzimuth.Enabled = false;
            this.textBoxAzimuth.Location = new System.Drawing.Point(24, 310);
            this.textBoxAzimuth.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAzimuth.Name = "textBoxAzimuth";
            this.textBoxAzimuth.ReadOnly = true;
            this.textBoxAzimuth.Size = new System.Drawing.Size(165, 22);
            this.textBoxAzimuth.TabIndex = 1;
            this.textBoxAzimuth.Text = "No Values";
            // 
            // comboBoxSatellite
            // 
            this.comboBoxSatellite.FormattingEnabled = true;
            this.comboBoxSatellite.Location = new System.Drawing.Point(24, 170);
            this.comboBoxSatellite.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSatellite.Name = "comboBoxSatellite";
            this.comboBoxSatellite.Size = new System.Drawing.Size(165, 24);
            this.comboBoxSatellite.TabIndex = 2;
            // 
            // textBoxElevation
            // 
            this.textBoxElevation.Enabled = false;
            this.textBoxElevation.Location = new System.Drawing.Point(24, 374);
            this.textBoxElevation.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxElevation.Name = "textBoxElevation";
            this.textBoxElevation.ReadOnly = true;
            this.textBoxElevation.Size = new System.Drawing.Size(165, 22);
            this.textBoxElevation.TabIndex = 3;
            this.textBoxElevation.Text = "No Values";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 287);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Azimuth (deg)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 354);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Elevation (deg)";
            // 
            // radioButtonOpen
            // 
            this.radioButtonOpen.AutoSize = true;
            this.radioButtonOpen.Enabled = false;
            this.radioButtonOpen.Location = new System.Drawing.Point(24, 217);
            this.radioButtonOpen.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonOpen.Name = "radioButtonOpen";
            this.radioButtonOpen.Size = new System.Drawing.Size(147, 21);
            this.radioButtonOpen.TabIndex = 6;
            this.radioButtonOpen.TabStop = true;
            this.radioButtonOpen.Text = "Open Data Stream";
            this.radioButtonOpen.UseVisualStyleBackColor = true;
            this.radioButtonOpen.CheckedChanged += new System.EventHandler(this.RadioButtonOpen_CheckedChanged);
            // 
            // radioButtonClose
            // 
            this.radioButtonClose.AutoSize = true;
            this.radioButtonClose.Enabled = false;
            this.radioButtonClose.Location = new System.Drawing.Point(24, 246);
            this.radioButtonClose.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonClose.Name = "radioButtonClose";
            this.radioButtonClose.Size = new System.Drawing.Size(147, 21);
            this.radioButtonClose.TabIndex = 7;
            this.radioButtonClose.TabStop = true;
            this.radioButtonClose.Text = "Close Data Stream";
            this.radioButtonClose.UseVisualStyleBackColor = true;
            this.radioButtonClose.CheckedChanged += new System.EventHandler(this.RadioButtonClose_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 146);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Select Satellite";
            // 
            // comboBoxFacility
            // 
            this.comboBoxFacility.FormattingEnabled = true;
            this.comboBoxFacility.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.comboBoxFacility.Location = new System.Drawing.Point(24, 103);
            this.comboBoxFacility.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxFacility.Name = "comboBoxFacility";
            this.comboBoxFacility.Size = new System.Drawing.Size(165, 24);
            this.comboBoxFacility.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Select Facility";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 418);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxFacility);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButtonClose);
            this.Controls.Add(this.radioButtonOpen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxElevation);
            this.Controls.Add(this.comboBoxSatellite);
            this.Controls.Add(this.textBoxAzimuth);
            this.Controls.Add(this.buttonConnect);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Real Time AER";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxAzimuth;
        private System.Windows.Forms.ComboBox comboBoxSatellite;
        private System.Windows.Forms.TextBox textBoxElevation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonOpen;
        private System.Windows.Forms.RadioButton radioButtonClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxFacility;
        private System.Windows.Forms.Label label4;
    }
}

