namespace Stk12.UiPlugin.Articulations
{
    partial class EditArticForm
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
            this.sectionName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.Period = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DCDeltaValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DcValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AcValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DBValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EndValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartValue = new System.Windows.Forms.TextBox();
            this.StartValueText = new System.Windows.Forms.Label();
            this.DurationValue = new System.Windows.Forms.TextBox();
            this.DurationText = new System.Windows.Forms.Label();
            this.StartTimeValue = new System.Windows.Forms.TextBox();
            this.StartTimeText = new System.Windows.Forms.Label();
            this.UseCurrentTime = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.PossibleArtics = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // sectionName
            // 
            this.sectionName.Location = new System.Drawing.Point(306, 62);
            this.sectionName.Name = "sectionName";
            this.sectionName.Size = new System.Drawing.Size(162, 22);
            this.sectionName.TabIndex = 47;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(129, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(171, 20);
            this.label9.TabIndex = 46;
            this.label9.Text = "Articulation Name :";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(374, 375);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(158, 41);
            this.CancelButton.TabIndex = 45;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(153, 375);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(158, 41);
            this.ApplyButton.TabIndex = 44;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(509, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 25);
            this.label8.TabIndex = 43;
            this.label8.Text = "Optional";
            // 
            // Period
            // 
            this.Period.Location = new System.Drawing.Point(559, 337);
            this.Period.Name = "Period";
            this.Period.Size = new System.Drawing.Size(103, 22);
            this.Period.TabIndex = 42;
            this.Period.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(486, 337);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 20);
            this.label7.TabIndex = 41;
            this.label7.Text = "Period :";
            // 
            // DCDeltaValue
            // 
            this.DCDeltaValue.Location = new System.Drawing.Point(559, 292);
            this.DCDeltaValue.Name = "DCDeltaValue";
            this.DCDeltaValue.Size = new System.Drawing.Size(103, 22);
            this.DCDeltaValue.TabIndex = 40;
            this.DCDeltaValue.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(407, 292);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 20);
            this.label6.TabIndex = 39;
            this.label6.Text = "Duty Cycle Delta :";
            // 
            // DcValue
            // 
            this.DcValue.Location = new System.Drawing.Point(559, 246);
            this.DcValue.Name = "DcValue";
            this.DcValue.Size = new System.Drawing.Size(103, 22);
            this.DcValue.TabIndex = 38;
            this.DcValue.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(421, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 20);
            this.label5.TabIndex = 37;
            this.label5.Text = "Decel Duration :";
            // 
            // AcValue
            // 
            this.AcValue.Location = new System.Drawing.Point(559, 203);
            this.AcValue.Name = "AcValue";
            this.AcValue.Size = new System.Drawing.Size(103, 22);
            this.AcValue.TabIndex = 36;
            this.AcValue.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(423, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 20);
            this.label4.TabIndex = 35;
            this.label4.Text = "Accel Duration :";
            // 
            // DBValue
            // 
            this.DBValue.Location = new System.Drawing.Point(559, 160);
            this.DBValue.Name = "DBValue";
            this.DBValue.Size = new System.Drawing.Size(103, 22);
            this.DBValue.TabIndex = 34;
            this.DBValue.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(389, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 20);
            this.label3.TabIndex = 33;
            this.label3.Text = "Deadband Duration :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 32;
            this.label2.Text = "Required";
            // 
            // EndValue
            // 
            this.EndValue.Location = new System.Drawing.Point(116, 294);
            this.EndValue.Name = "EndValue";
            this.EndValue.Size = new System.Drawing.Size(103, 22);
            this.EndValue.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "End Value :";
            // 
            // StartValue
            // 
            this.StartValue.Location = new System.Drawing.Point(116, 248);
            this.StartValue.Name = "StartValue";
            this.StartValue.Size = new System.Drawing.Size(103, 22);
            this.StartValue.TabIndex = 29;
            // 
            // StartValueText
            // 
            this.StartValueText.AutoSize = true;
            this.StartValueText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartValueText.Location = new System.Drawing.Point(8, 246);
            this.StartValueText.Name = "StartValueText";
            this.StartValueText.Size = new System.Drawing.Size(102, 20);
            this.StartValueText.TabIndex = 28;
            this.StartValueText.Text = "Start Value :";
            // 
            // DurationValue
            // 
            this.DurationValue.Location = new System.Drawing.Point(116, 205);
            this.DurationValue.Name = "DurationValue";
            this.DurationValue.Size = new System.Drawing.Size(103, 22);
            this.DurationValue.TabIndex = 27;
            // 
            // DurationText
            // 
            this.DurationText.AutoSize = true;
            this.DurationText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DurationText.Location = new System.Drawing.Point(27, 203);
            this.DurationText.Name = "DurationText";
            this.DurationText.Size = new System.Drawing.Size(83, 20);
            this.DurationText.TabIndex = 26;
            this.DurationText.Text = "Duration :";
            // 
            // StartTimeValue
            // 
            this.StartTimeValue.Location = new System.Drawing.Point(116, 164);
            this.StartTimeValue.Name = "StartTimeValue";
            this.StartTimeValue.Size = new System.Drawing.Size(103, 22);
            this.StartTimeValue.TabIndex = 25;
            this.StartTimeValue.TextChanged += new System.EventHandler(this.StartTimeValue_TextChanged);
            // 
            // StartTimeText
            // 
            this.StartTimeText.AutoSize = true;
            this.StartTimeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartTimeText.Location = new System.Drawing.Point(13, 162);
            this.StartTimeText.Name = "StartTimeText";
            this.StartTimeText.Size = new System.Drawing.Size(97, 20);
            this.StartTimeText.TabIndex = 24;
            this.StartTimeText.Text = "Start Time :";
            // 
            // UseCurrentTime
            // 
            this.UseCurrentTime.Location = new System.Drawing.Point(225, 149);
            this.UseCurrentTime.Name = "UseCurrentTime";
            this.UseCurrentTime.Size = new System.Drawing.Size(140, 44);
            this.UseCurrentTime.TabIndex = 48;
            this.UseCurrentTime.Text = "Use Current Animation TIme";
            this.UseCurrentTime.UseVisualStyleBackColor = true;
            this.UseCurrentTime.Click += new System.EventHandler(this.UseCurrentTime_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(177, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 20);
            this.label10.TabIndex = 49;
            this.label10.Text = "Articulation  :";
            // 
            // PossibleArtics
            // 
            this.PossibleArtics.FormattingEnabled = true;
            this.PossibleArtics.Location = new System.Drawing.Point(307, 29);
            this.PossibleArtics.Name = "PossibleArtics";
            this.PossibleArtics.Size = new System.Drawing.Size(225, 24);
            this.PossibleArtics.TabIndex = 50;
            this.PossibleArtics.SelectedIndexChanged += new System.EventHandler(this.PossibleArtics_SelectedIndexChanged);
            // 
            // EditArticForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 469);
            this.Controls.Add(this.PossibleArtics);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.UseCurrentTime);
            this.Controls.Add(this.sectionName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Period);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DCDeltaValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DcValue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AcValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DBValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EndValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartValue);
            this.Controls.Add(this.StartValueText);
            this.Controls.Add(this.DurationValue);
            this.Controls.Add(this.DurationText);
            this.Controls.Add(this.StartTimeValue);
            this.Controls.Add(this.StartTimeText);
            this.Name = "EditArticForm";
            this.Text = "EditArticForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sectionName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Period;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox DCDeltaValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox DcValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox AcValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DBValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EndValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox StartValue;
        private System.Windows.Forms.Label StartValueText;
        private System.Windows.Forms.TextBox DurationValue;
        private System.Windows.Forms.Label DurationText;
        private System.Windows.Forms.TextBox StartTimeValue;
        private System.Windows.Forms.Label StartTimeText;
        private System.Windows.Forms.Button UseCurrentTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox PossibleArtics;
    }
}