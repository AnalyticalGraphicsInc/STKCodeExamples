namespace XPlaneToSTK
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
            this.txtPortNumber = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.buttonConnectXplane = new System.Windows.Forms.Button();
            this.latitudeLabel = new System.Windows.Forms.Label();
            this.longitudeLabel = new System.Windows.Forms.Label();
            this.altitudeLabel = new System.Windows.Forms.Label();
            this.headingLabel = new System.Windows.Forms.Label();
            this.pitchLabel = new System.Windows.Forms.Label();
            this.rollLabel = new System.Windows.Forms.Label();
            this.latitudeTextBox = new System.Windows.Forms.TextBox();
            this.longitudeTextBox = new System.Windows.Forms.TextBox();
            this.altitudeTextBox = new System.Windows.Forms.TextBox();
            this.headingTextBox = new System.Windows.Forms.TextBox();
            this.pitchTextBox = new System.Windows.Forms.TextBox();
            this.rollTextBox = new System.Windows.Forms.TextBox();
            this.buttonConnectStk = new System.Windows.Forms.Button();
            this.buttonCreateAircraft = new System.Windows.Forms.Button();
            this.defaultPortLabel = new System.Windows.Forms.Label();
            this.noticeLabel = new System.Windows.Forms.Label();
            this.altitudeOffsetTextBox = new System.Windows.Forms.TextBox();
            this.altitudeOffsetLabel = new System.Windows.Forms.Label();
            this.useCurrentScenario = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtPortNumber
            // 
            this.txtPortNumber.Location = new System.Drawing.Point(63, 21);
            this.txtPortNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtPortNumber.Name = "txtPortNumber";
            this.txtPortNumber.Size = new System.Drawing.Size(132, 22);
            this.txtPortNumber.TabIndex = 0;
            this.txtPortNumber.Text = "49000";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(16, 25);
            this.portLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(38, 17);
            this.portLabel.TabIndex = 1;
            this.portLabel.Text = "Port:";
            // 
            // buttonConnectXplane
            // 
            this.buttonConnectXplane.Location = new System.Drawing.Point(96, 68);
            this.buttonConnectXplane.Margin = new System.Windows.Forms.Padding(4);
            this.buttonConnectXplane.Name = "buttonConnectXplane";
            this.buttonConnectXplane.Size = new System.Drawing.Size(100, 28);
            this.buttonConnectXplane.TabIndex = 2;
            this.buttonConnectXplane.Text = "Connect";
            this.buttonConnectXplane.UseVisualStyleBackColor = true;
            this.buttonConnectXplane.Click += new System.EventHandler(this.ButtonConnectXPlane_Click);
            // 
            // latitudeLabel
            // 
            this.latitudeLabel.AutoSize = true;
            this.latitudeLabel.Location = new System.Drawing.Point(250, 24);
            this.latitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.latitudeLabel.Name = "latitudeLabel";
            this.latitudeLabel.Size = new System.Drawing.Size(101, 17);
            this.latitudeLabel.TabIndex = 3;
            this.latitudeLabel.Text = "Latitude (deg):";
            this.latitudeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // longitudeLabel
            // 
            this.longitudeLabel.AutoSize = true;
            this.longitudeLabel.Location = new System.Drawing.Point(238, 65);
            this.longitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.longitudeLabel.Name = "longitudeLabel";
            this.longitudeLabel.Size = new System.Drawing.Size(113, 17);
            this.longitudeLabel.TabIndex = 4;
            this.longitudeLabel.Text = "Longitude (deg):";
            this.longitudeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // altitudeLabel
            // 
            this.altitudeLabel.AutoSize = true;
            this.altitudeLabel.Location = new System.Drawing.Point(270, 105);
            this.altitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.altitudeLabel.Name = "altitudeLabel";
            this.altitudeLabel.Size = new System.Drawing.Size(81, 17);
            this.altitudeLabel.TabIndex = 5;
            this.altitudeLabel.Text = "Altitude (ft):";
            this.altitudeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // headingLabel
            // 
            this.headingLabel.AutoSize = true;
            this.headingLabel.Location = new System.Drawing.Point(248, 148);
            this.headingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.headingLabel.Name = "headingLabel";
            this.headingLabel.Size = new System.Drawing.Size(103, 17);
            this.headingLabel.TabIndex = 6;
            this.headingLabel.Text = "Heading (deg):";
            this.headingLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pitchLabel
            // 
            this.pitchLabel.AutoSize = true;
            this.pitchLabel.Location = new System.Drawing.Point(270, 193);
            this.pitchLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pitchLabel.Name = "pitchLabel";
            this.pitchLabel.Size = new System.Drawing.Size(81, 17);
            this.pitchLabel.TabIndex = 7;
            this.pitchLabel.Text = "Pitch (deg):";
            this.pitchLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // rollLabel
            // 
            this.rollLabel.AutoSize = true;
            this.rollLabel.Location = new System.Drawing.Point(277, 239);
            this.rollLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rollLabel.Name = "rollLabel";
            this.rollLabel.Size = new System.Drawing.Size(74, 17);
            this.rollLabel.TabIndex = 8;
            this.rollLabel.Text = "Roll (deg):";
            this.rollLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // latitudeTextBox
            // 
            this.latitudeTextBox.Location = new System.Drawing.Point(376, 21);
            this.latitudeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.latitudeTextBox.Name = "latitudeTextBox";
            this.latitudeTextBox.ReadOnly = true;
            this.latitudeTextBox.Size = new System.Drawing.Size(132, 22);
            this.latitudeTextBox.TabIndex = 9;
            // 
            // longitudeTextBox
            // 
            this.longitudeTextBox.Location = new System.Drawing.Point(376, 62);
            this.longitudeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.longitudeTextBox.Name = "longitudeTextBox";
            this.longitudeTextBox.ReadOnly = true;
            this.longitudeTextBox.Size = new System.Drawing.Size(132, 22);
            this.longitudeTextBox.TabIndex = 10;
            // 
            // altitudeTextBox
            // 
            this.altitudeTextBox.Location = new System.Drawing.Point(376, 102);
            this.altitudeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.altitudeTextBox.Name = "altitudeTextBox";
            this.altitudeTextBox.ReadOnly = true;
            this.altitudeTextBox.Size = new System.Drawing.Size(132, 22);
            this.altitudeTextBox.TabIndex = 11;
            // 
            // headingTextBox
            // 
            this.headingTextBox.Location = new System.Drawing.Point(376, 145);
            this.headingTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.headingTextBox.Name = "headingTextBox";
            this.headingTextBox.ReadOnly = true;
            this.headingTextBox.Size = new System.Drawing.Size(132, 22);
            this.headingTextBox.TabIndex = 12;
            // 
            // pitchTextBox
            // 
            this.pitchTextBox.Location = new System.Drawing.Point(376, 190);
            this.pitchTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.pitchTextBox.Name = "pitchTextBox";
            this.pitchTextBox.ReadOnly = true;
            this.pitchTextBox.Size = new System.Drawing.Size(132, 22);
            this.pitchTextBox.TabIndex = 13;
            // 
            // rollTextBox
            // 
            this.rollTextBox.Location = new System.Drawing.Point(376, 236);
            this.rollTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.rollTextBox.Name = "rollTextBox";
            this.rollTextBox.ReadOnly = true;
            this.rollTextBox.Size = new System.Drawing.Size(132, 22);
            this.rollTextBox.TabIndex = 14;
            // 
            // buttonConnectStk
            // 
            this.buttonConnectStk.Location = new System.Drawing.Point(20, 234);
            this.buttonConnectStk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonConnectStk.Name = "buttonConnectStk";
            this.buttonConnectStk.Size = new System.Drawing.Size(176, 28);
            this.buttonConnectStk.TabIndex = 15;
            this.buttonConnectStk.Text = "Connect To STK";
            this.buttonConnectStk.UseVisualStyleBackColor = true;
            this.buttonConnectStk.Click += new System.EventHandler(this.ButtonConnectSTK_Click);
            // 
            // buttonCreateAircraft
            // 
            this.buttonCreateAircraft.Location = new System.Drawing.Point(20, 291);
            this.buttonCreateAircraft.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCreateAircraft.Name = "buttonCreateAircraft";
            this.buttonCreateAircraft.Size = new System.Drawing.Size(176, 28);
            this.buttonCreateAircraft.TabIndex = 16;
            this.buttonCreateAircraft.Text = "Create Aircraft";
            this.buttonCreateAircraft.UseVisualStyleBackColor = true;
            this.buttonCreateAircraft.Click += new System.EventHandler(this.ButtonCreateAircraft_Click);
            // 
            // defaultPortLabel
            // 
            this.defaultPortLabel.AutoSize = true;
            this.defaultPortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultPortLabel.Location = new System.Drawing.Point(39, 49);
            this.defaultPortLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.defaultPortLabel.Name = "defaultPortLabel";
            this.defaultPortLabel.Size = new System.Drawing.Size(160, 15);
            this.defaultPortLabel.TabIndex = 17;
            this.defaultPortLabel.Text = "Default XPlane Port (49000)";
            // 
            // noticeLabel
            // 
            this.noticeLabel.AutoSize = true;
            this.noticeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noticeLabel.Location = new System.Drawing.Point(17, 100);
            this.noticeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.noticeLabel.Name = "noticeLabel";
            this.noticeLabel.Size = new System.Drawing.Size(185, 45);
            this.noticeLabel.TabIndex = 18;
            this.noticeLabel.Text = "Note: This is a UDP connection.  \r\nMay take a few moments to \r\nconnect to data st" +
    "ream.";
            // 
            // altitudeOffsetTextBox
            // 
            this.altitudeOffsetTextBox.Location = new System.Drawing.Point(145, 190);
            this.altitudeOffsetTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.altitudeOffsetTextBox.Name = "altitudeOffsetTextBox";
            this.altitudeOffsetTextBox.Size = new System.Drawing.Size(49, 22);
            this.altitudeOffsetTextBox.TabIndex = 19;
            this.altitudeOffsetTextBox.Text = "-70";
            // 
            // altitudeOffsetLabel
            // 
            this.altitudeOffsetLabel.AutoSize = true;
            this.altitudeOffsetLabel.Location = new System.Drawing.Point(16, 193);
            this.altitudeOffsetLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.altitudeOffsetLabel.Name = "altitudeOffsetLabel";
            this.altitudeOffsetLabel.Size = new System.Drawing.Size(123, 17);
            this.altitudeOffsetLabel.TabIndex = 20;
            this.altitudeOffsetLabel.Text = "Altitude Offset (ft):";
            // 
            // useCurrentScenario
            // 
            this.useCurrentScenario.AutoSize = true;
            this.useCurrentScenario.Location = new System.Drawing.Point(20, 263);
            this.useCurrentScenario.Name = "useCurrentScenario";
            this.useCurrentScenario.Size = new System.Drawing.Size(166, 21);
            this.useCurrentScenario.TabIndex = 21;
            this.useCurrentScenario.Text = "Use Current Scenario";
            this.useCurrentScenario.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 332);
            this.Controls.Add(this.useCurrentScenario);
            this.Controls.Add(this.altitudeOffsetLabel);
            this.Controls.Add(this.altitudeOffsetTextBox);
            this.Controls.Add(this.noticeLabel);
            this.Controls.Add(this.defaultPortLabel);
            this.Controls.Add(this.buttonCreateAircraft);
            this.Controls.Add(this.buttonConnectStk);
            this.Controls.Add(this.rollTextBox);
            this.Controls.Add(this.pitchTextBox);
            this.Controls.Add(this.headingTextBox);
            this.Controls.Add(this.altitudeTextBox);
            this.Controls.Add(this.longitudeTextBox);
            this.Controls.Add(this.latitudeTextBox);
            this.Controls.Add(this.rollLabel);
            this.Controls.Add(this.pitchLabel);
            this.Controls.Add(this.headingLabel);
            this.Controls.Add(this.altitudeLabel);
            this.Controls.Add(this.longitudeLabel);
            this.Controls.Add(this.latitudeLabel);
            this.Controls.Add(this.buttonConnectXplane);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.txtPortNumber);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "XPlane To STK 12";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XPlaneToSTK_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPortNumber;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Button buttonConnectXplane;
        private System.Windows.Forms.Label latitudeLabel;
        private System.Windows.Forms.Label longitudeLabel;
        private System.Windows.Forms.Label altitudeLabel;
        private System.Windows.Forms.Label headingLabel;
        private System.Windows.Forms.Label pitchLabel;
        private System.Windows.Forms.Label rollLabel;
        private System.Windows.Forms.TextBox latitudeTextBox;
        private System.Windows.Forms.TextBox longitudeTextBox;
        private System.Windows.Forms.TextBox altitudeTextBox;
        private System.Windows.Forms.TextBox headingTextBox;
        private System.Windows.Forms.TextBox pitchTextBox;
        private System.Windows.Forms.TextBox rollTextBox;
        private System.Windows.Forms.Button buttonConnectStk;
        private System.Windows.Forms.Button buttonCreateAircraft;
        private System.Windows.Forms.Label defaultPortLabel;
        private System.Windows.Forms.Label noticeLabel;
        private System.Windows.Forms.TextBox altitudeOffsetTextBox;
        private System.Windows.Forms.Label altitudeOffsetLabel;
        private System.Windows.Forms.CheckBox useCurrentScenario;
    }
}

