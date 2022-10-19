namespace Agi.UiPlugin.MoveMtoTime
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
            this.refreshButton = new System.Windows.Forms.Button();
            this.mtoTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.earlierRadioButton = new System.Windows.Forms.RadioButton();
            this.laterRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.daysTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hoursTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.minutesTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.secondsTextBox = new System.Windows.Forms.TextBox();
            this.moveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(20, 15);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(100, 28);
            this.refreshButton.TabIndex = 0;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // mtoTreeView
            // 
            this.mtoTreeView.CheckBoxes = true;
            this.mtoTreeView.Location = new System.Drawing.Point(20, 50);
            this.mtoTreeView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtoTreeView.Name = "mtoTreeView";
            this.mtoTreeView.Size = new System.Drawing.Size(421, 243);
            this.mtoTreeView.TabIndex = 2;
            this.mtoTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.mtoTreeView_AfterCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 331);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Move Selected Tracks";
            // 
            // earlierRadioButton
            // 
            this.earlierRadioButton.AutoSize = true;
            this.earlierRadioButton.Location = new System.Drawing.Point(175, 314);
            this.earlierRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.earlierRadioButton.Name = "earlierRadioButton";
            this.earlierRadioButton.Size = new System.Drawing.Size(88, 26);
            this.earlierRadioButton.TabIndex = 4;
            this.earlierRadioButton.Text = "Earlier";
            this.earlierRadioButton.UseVisualStyleBackColor = true;
            // 
            // laterRadioButton
            // 
            this.laterRadioButton.AutoSize = true;
            this.laterRadioButton.Checked = true;
            this.laterRadioButton.Location = new System.Drawing.Point(175, 342);
            this.laterRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.laterRadioButton.Name = "laterRadioButton";
            this.laterRadioButton.Size = new System.Drawing.Size(78, 26);
            this.laterRadioButton.TabIndex = 5;
            this.laterRadioButton.TabStop = true;
            this.laterRadioButton.Text = "Later";
            this.laterRadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 331);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "by";
            // 
            // daysTextBox
            // 
            this.daysTextBox.Location = new System.Drawing.Point(285, 302);
            this.daysTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.daysTextBox.Name = "daysTextBox";
            this.daysTextBox.Size = new System.Drawing.Size(40, 22);
            this.daysTextBox.TabIndex = 7;
            this.daysTextBox.Text = "0";
            this.daysTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 305);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "days";
            // 
            // hoursTextBox
            // 
            this.hoursTextBox.Location = new System.Drawing.Point(285, 329);
            this.hoursTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.hoursTextBox.Name = "hoursTextBox";
            this.hoursTextBox.Size = new System.Drawing.Size(40, 22);
            this.hoursTextBox.TabIndex = 9;
            this.hoursTextBox.Text = "1";
            this.hoursTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(329, 331);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "hours";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(329, 359);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "min";
            // 
            // minutesTextBox
            // 
            this.minutesTextBox.Location = new System.Drawing.Point(285, 356);
            this.minutesTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.minutesTextBox.Name = "minutesTextBox";
            this.minutesTextBox.Size = new System.Drawing.Size(40, 22);
            this.minutesTextBox.TabIndex = 11;
            this.minutesTextBox.Text = "0";
            this.minutesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(329, 386);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "sec";
            // 
            // secondsTextBox
            // 
            this.secondsTextBox.Location = new System.Drawing.Point(285, 384);
            this.secondsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.secondsTextBox.Name = "secondsTextBox";
            this.secondsTextBox.Size = new System.Drawing.Size(40, 22);
            this.secondsTextBox.TabIndex = 13;
            this.secondsTextBox.Text = "0";
            this.secondsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(377, 325);
            this.moveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(65, 28);
            this.moveButton.TabIndex = 15;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.secondsTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.minutesTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hoursTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.daysTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.laterRadioButton);
            this.Controls.Add(this.earlierRadioButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mtoTreeView);
            this.Controls.Add(this.refreshButton);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(465, 423);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.TreeView mtoTreeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton earlierRadioButton;
        private System.Windows.Forms.RadioButton laterRadioButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox daysTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox hoursTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox minutesTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox secondsTextBox;
        private System.Windows.Forms.Button moveButton;
    }
}
