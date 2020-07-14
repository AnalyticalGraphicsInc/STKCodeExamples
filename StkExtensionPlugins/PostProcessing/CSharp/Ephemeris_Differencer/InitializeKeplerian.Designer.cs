using System;
namespace EphemerisDifferencer
{
    partial class InitializeKeplerian
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
            this.label1 = new System.Windows.Forms.Label();
            this.ZdotLabel = new System.Windows.Forms.Label();
            this.YdotLabel = new System.Windows.Forms.Label();
            this.XdotLabel = new System.Windows.Forms.Label();
            this.AnomalyBox = new System.Windows.Forms.MaskedTextBox();
            this.ArgOfPeriapsisBox = new System.Windows.Forms.MaskedTextBox();
            this.RAANBox = new System.Windows.Forms.MaskedTextBox();
            this.ZLabel = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.InclinationBox = new System.Windows.Forms.MaskedTextBox();
            this.EccentricityBox = new System.Windows.Forms.MaskedTextBox();
            this.SemiMajorAxisBox = new System.Windows.Forms.MaskedTextBox();
            this.InitializeButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EpochTextBox = new System.Windows.Forms.MaskedTextBox();
            this.GravityTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 102;
            this.label1.Text = "Keplerian Elements";
            // 
            // ZdotLabel
            // 
            this.ZdotLabel.AutoSize = true;
            this.ZdotLabel.Location = new System.Drawing.Point(116, 237);
            this.ZdotLabel.Name = "ZdotLabel";
            this.ZdotLabel.Size = new System.Drawing.Size(98, 13);
            this.ZdotLabel.TabIndex = 101;
            this.ZdotLabel.Text = "TrueAnomaly [Deg]";
            // 
            // YdotLabel
            // 
            this.YdotLabel.AutoSize = true;
            this.YdotLabel.Location = new System.Drawing.Point(116, 211);
            this.YdotLabel.Name = "YdotLabel";
            this.YdotLabel.Size = new System.Drawing.Size(134, 13);
            this.YdotLabel.TabIndex = 100;
            this.YdotLabel.Text = "ArgumentOfPeriapsis [Deg]";
            // 
            // XdotLabel
            // 
            this.XdotLabel.AutoSize = true;
            this.XdotLabel.Location = new System.Drawing.Point(116, 185);
            this.XdotLabel.Name = "XdotLabel";
            this.XdotLabel.Size = new System.Drawing.Size(197, 13);
            this.XdotLabel.TabIndex = 99;
            this.XdotLabel.Text = "RightAscensionOfAscendingNode [Deg]";
            // 
            // AnomalyBox
            // 
            this.AnomalyBox.Location = new System.Drawing.Point(10, 234);
            this.AnomalyBox.Name = "AnomalyBox";
            this.AnomalyBox.Size = new System.Drawing.Size(100, 20);
            this.AnomalyBox.TabIndex = 98;
            this.AnomalyBox.Text = "-1.0";
            // 
            // ArgOfPeriapsisBox
            // 
            this.ArgOfPeriapsisBox.Location = new System.Drawing.Point(10, 208);
            this.ArgOfPeriapsisBox.Name = "ArgOfPeriapsisBox";
            this.ArgOfPeriapsisBox.Size = new System.Drawing.Size(100, 20);
            this.ArgOfPeriapsisBox.TabIndex = 97;
            this.ArgOfPeriapsisBox.Text = "8.3";
            // 
            // RAANBox
            // 
            this.RAANBox.Location = new System.Drawing.Point(10, 182);
            this.RAANBox.Name = "RAANBox";
            this.RAANBox.Size = new System.Drawing.Size(100, 20);
            this.RAANBox.TabIndex = 96;
            this.RAANBox.Text = "0";
            // 
            // ZLabel
            // 
            this.ZLabel.AutoSize = true;
            this.ZLabel.Location = new System.Drawing.Point(116, 156);
            this.ZLabel.Name = "ZLabel";
            this.ZLabel.Size = new System.Drawing.Size(84, 13);
            this.ZLabel.TabIndex = 95;
            this.ZLabel.Text = "Inclination [Deg]";
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(116, 130);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(62, 13);
            this.YLabel.TabIndex = 94;
            this.YLabel.Text = "Eccentricity";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(116, 104);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(98, 13);
            this.XLabel.TabIndex = 93;            
            // 
            // InclinationBox
            // 
            this.InclinationBox.Location = new System.Drawing.Point(10, 153);
            this.InclinationBox.Name = "InclinationBox";
            this.InclinationBox.Size = new System.Drawing.Size(100, 20);
            this.InclinationBox.TabIndex = 92;
            this.InclinationBox.Text = "0";
            // 
            // EccentricityBox
            // 
            this.EccentricityBox.Location = new System.Drawing.Point(10, 127);
            this.EccentricityBox.Name = "EccentricityBox";
            this.EccentricityBox.Size = new System.Drawing.Size(100, 20);
            this.EccentricityBox.TabIndex = 91;
            this.EccentricityBox.Text = "0";
            // 
            // SemiMajorAxisBox
            // 
            this.SemiMajorAxisBox.CausesValidation = false;
            this.SemiMajorAxisBox.Location = new System.Drawing.Point(10, 101);
            this.SemiMajorAxisBox.Name = "SemiMajorAxisBox";
            this.SemiMajorAxisBox.Size = new System.Drawing.Size(100, 20);
            this.SemiMajorAxisBox.TabIndex = 90;            
            // 
            // InitializeButton
            // 
            this.InitializeButton.Location = new System.Drawing.Point(119, 264);
            this.InitializeButton.Name = "InitializeButton";
            this.InitializeButton.Size = new System.Drawing.Size(75, 23);
            this.InitializeButton.TabIndex = 103;
            this.InitializeButton.Text = "Initialize";
            this.InitializeButton.UseVisualStyleBackColor = true;
            this.InitializeButton.Click += new System.EventHandler(this.InitializeButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(116, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 108;
            this.label4.Text = "JulianDate [UTC]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 107;
            this.label3.Text = "Epoch";
            // 
            // EpochTextBox
            // 
            this.EpochTextBox.Location = new System.Drawing.Point(10, 23);
            this.EpochTextBox.Mask = "0000000.00000";
            this.EpochTextBox.Name = "EpochTextBox";
            this.EpochTextBox.Size = new System.Drawing.Size(100, 20);
            this.EpochTextBox.TabIndex = 106;
            //* The following is needed to conform to the mask representing 2451545.0
            this.EpochTextBox.Text = "245154500000";
            // 
            // GravityTextBox
            // 
            this.GravityTextBox.Location = new System.Drawing.Point(10, 62);
            this.GravityTextBox.Name = "GravityTextBox";
            this.GravityTextBox.Size = new System.Drawing.Size(100, 20);
            this.GravityTextBox.TabIndex = 109;            
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 110;
            this.label2.Text = "Gravity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 111;            
            // 
            // InitializeKeplerian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 299);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GravityTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.EpochTextBox);
            this.Controls.Add(this.InitializeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ZdotLabel);
            this.Controls.Add(this.YdotLabel);
            this.Controls.Add(this.XdotLabel);
            this.Controls.Add(this.AnomalyBox);
            this.Controls.Add(this.ArgOfPeriapsisBox);
            this.Controls.Add(this.RAANBox);
            this.Controls.Add(this.ZLabel);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.InclinationBox);
            this.Controls.Add(this.EccentricityBox);
            this.Controls.Add(this.SemiMajorAxisBox);
            this.Name = "InitializeKeplerian";
            this.Text = "InitializeKeplerian";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ZdotLabel;
        private System.Windows.Forms.Label YdotLabel;
        private System.Windows.Forms.Label XdotLabel;
        private System.Windows.Forms.MaskedTextBox AnomalyBox;
        private System.Windows.Forms.MaskedTextBox ArgOfPeriapsisBox;
        private System.Windows.Forms.MaskedTextBox RAANBox;
        private System.Windows.Forms.Label ZLabel;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.MaskedTextBox InclinationBox;
        private System.Windows.Forms.MaskedTextBox EccentricityBox;
        private System.Windows.Forms.MaskedTextBox SemiMajorAxisBox;
        private System.Windows.Forms.Button InitializeButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox EpochTextBox;
        private System.Windows.Forms.MaskedTextBox GravityTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;

    }
}