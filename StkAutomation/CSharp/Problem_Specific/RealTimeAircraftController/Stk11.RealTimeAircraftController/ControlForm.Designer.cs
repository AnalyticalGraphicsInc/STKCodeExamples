namespace RealtimeACcontrol
{
    partial class ControlForm
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
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.scroll_Throttle = new System.Windows.Forms.VScrollBar();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_PitchUp = new System.Windows.Forms.Button();
            this.btn_leftTurn = new System.Windows.Forms.Button();
            this.btn_PitchDown = new System.Windows.Forms.Button();
            this.btn_turnRight = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Reset
            // 
            this.btn_Reset.BackColor = System.Drawing.Color.LightPink;
            this.btn_Reset.Location = new System.Drawing.Point(12, 159);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(242, 23);
            this.btn_Reset.TabIndex = 4;
            this.btn_Reset.Text = "Stop";
            this.btn_Reset.UseVisualStyleBackColor = false;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.btn_Start.Location = new System.Drawing.Point(12, 130);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(242, 23);
            this.btn_Start.TabIndex = 7;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = false;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // scroll_Throttle
            // 
            this.scroll_Throttle.Location = new System.Drawing.Point(266, 22);
            this.scroll_Throttle.Name = "scroll_Throttle";
            this.scroll_Throttle.Size = new System.Drawing.Size(62, 160);
            this.scroll_Throttle.TabIndex = 8;
            this.scroll_Throttle.Tag = "Throttle";
            this.scroll_Throttle.Value = 100;
            this.scroll_Throttle.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scroll_Throttle_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(273, 5);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Throttle";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_PitchUp);
            this.groupBox1.Controls.Add(this.btn_leftTurn);
            this.groupBox1.Controls.Add(this.btn_PitchDown);
            this.groupBox1.Controls.Add(this.btn_turnRight);
            this.groupBox1.Location = new System.Drawing.Point(12, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 105);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Aircraft Controls";
            // 
            // btn_PitchUp
            // 
            this.btn_PitchUp.Location = new System.Drawing.Point(84, 16);
            this.btn_PitchUp.Name = "btn_PitchUp";
            this.btn_PitchUp.Size = new System.Drawing.Size(75, 23);
            this.btn_PitchUp.TabIndex = 2;
            this.btn_PitchUp.Text = "Pitch Up";
            this.btn_PitchUp.UseVisualStyleBackColor = true;
            this.btn_PitchUp.Click += new System.EventHandler(this.btn_PitchUp_Click);
            // 
            // btn_leftTurn
            // 
            this.btn_leftTurn.Location = new System.Drawing.Point(6, 43);
            this.btn_leftTurn.Name = "btn_leftTurn";
            this.btn_leftTurn.Size = new System.Drawing.Size(75, 23);
            this.btn_leftTurn.TabIndex = 0;
            this.btn_leftTurn.Text = "Turn Left";
            this.btn_leftTurn.UseVisualStyleBackColor = true;
            this.btn_leftTurn.Click += new System.EventHandler(this.btn_leftTurn_Click);
            // 
            // btn_PitchDown
            // 
            this.btn_PitchDown.Location = new System.Drawing.Point(84, 70);
            this.btn_PitchDown.Name = "btn_PitchDown";
            this.btn_PitchDown.Size = new System.Drawing.Size(75, 23);
            this.btn_PitchDown.TabIndex = 3;
            this.btn_PitchDown.Text = "Pitch Down";
            this.btn_PitchDown.UseVisualStyleBackColor = true;
            this.btn_PitchDown.Click += new System.EventHandler(this.btn_PitchDown_Click);
            // 
            // btn_turnRight
            // 
            this.btn_turnRight.Location = new System.Drawing.Point(162, 43);
            this.btn_turnRight.Name = "btn_turnRight";
            this.btn_turnRight.Size = new System.Drawing.Size(75, 23);
            this.btn_turnRight.TabIndex = 1;
            this.btn_turnRight.Text = "Turn Right";
            this.btn_turnRight.UseVisualStyleBackColor = true;
            this.btn_turnRight.Click += new System.EventHandler(this.btn_turnRight_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 191);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.scroll_Throttle);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.btn_Reset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ControlForm";
            this.Text = "Real Aircraft Control";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.VScrollBar scroll_Throttle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_PitchUp;
        private System.Windows.Forms.Button btn_leftTurn;
        private System.Windows.Forms.Button btn_PitchDown;
        private System.Windows.Forms.Button btn_turnRight;

    }
}

