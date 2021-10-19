namespace Stk12.UiPlugin.Articulations
{
    partial class LinkToListForm
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
            this.Cancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbStkObjects = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Link = new System.Windows.Forms.Button();
            this.Events = new System.Windows.Forms.ComboBox();
            this.StartTimeLink = new System.Windows.Forms.RadioButton();
            this.StopTimeLink = new System.Windows.Forms.RadioButton();
            this.IncrementCheck = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.IncrementStepValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(179, 186);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(134, 47);
            this.Cancel.TabIndex = 31;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(96, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 30;
            this.label3.Text = "Parent Object";
            // 
            // cbStkObjects
            // 
            this.cbStkObjects.FormattingEnabled = true;
            this.cbStkObjects.Location = new System.Drawing.Point(28, 48);
            this.cbStkObjects.Name = "cbStkObjects";
            this.cbStkObjects.Size = new System.Drawing.Size(285, 24);
            this.cbStkObjects.TabIndex = 29;
            this.cbStkObjects.SelectedIndexChanged += new System.EventHandler(this.cbStkObjects_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(73, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "Choose Interval List";
            // 
            // Link
            // 
            this.Link.Location = new System.Drawing.Point(28, 186);
            this.Link.Name = "Link";
            this.Link.Size = new System.Drawing.Size(134, 47);
            this.Link.TabIndex = 27;
            this.Link.Text = "Link to Interval List";
            this.Link.UseVisualStyleBackColor = true;
            this.Link.Click += new System.EventHandler(this.Link_Click);
            // 
            // Events
            // 
            this.Events.FormattingEnabled = true;
            this.Events.Location = new System.Drawing.Point(28, 117);
            this.Events.Name = "Events";
            this.Events.Size = new System.Drawing.Size(285, 24);
            this.Events.TabIndex = 26;
            this.Events.SelectedIndexChanged += new System.EventHandler(this.Events_SelectedIndexChanged);
            // 
            // StartTimeLink
            // 
            this.StartTimeLink.AutoSize = true;
            this.StartTimeLink.Checked = true;
            this.StartTimeLink.Location = new System.Drawing.Point(355, 18);
            this.StartTimeLink.Name = "StartTimeLink";
            this.StartTimeLink.Size = new System.Drawing.Size(94, 21);
            this.StartTimeLink.TabIndex = 32;
            this.StartTimeLink.TabStop = true;
            this.StartTimeLink.Text = "Start Time";
            this.StartTimeLink.UseVisualStyleBackColor = true;
            this.StartTimeLink.CheckedChanged += new System.EventHandler(this.StartTimeLink_CheckedChanged);
            // 
            // StopTimeLink
            // 
            this.StopTimeLink.AutoSize = true;
            this.StopTimeLink.Location = new System.Drawing.Point(356, 63);
            this.StopTimeLink.Name = "StopTimeLink";
            this.StopTimeLink.Size = new System.Drawing.Size(93, 21);
            this.StopTimeLink.TabIndex = 33;
            this.StopTimeLink.Text = "Stop Time";
            this.StopTimeLink.UseVisualStyleBackColor = true;
            this.StopTimeLink.CheckedChanged += new System.EventHandler(this.StopTimeLink_CheckedChanged);
            // 
            // IncrementCheck
            // 
            this.IncrementCheck.AutoSize = true;
            this.IncrementCheck.Location = new System.Drawing.Point(356, 108);
            this.IncrementCheck.Name = "IncrementCheck";
            this.IncrementCheck.Size = new System.Drawing.Size(206, 21);
            this.IncrementCheck.TabIndex = 34;
            this.IncrementCheck.Text = "Increment Articulation Value";
            this.IncrementCheck.UseVisualStyleBackColor = true;
            this.IncrementCheck.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(364, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 36;
            this.label4.Text = "Step Value :";
            // 
            // IncrementStepValue
            // 
            this.IncrementStepValue.Location = new System.Drawing.Point(456, 141);
            this.IncrementStepValue.Name = "IncrementStepValue";
            this.IncrementStepValue.Size = new System.Drawing.Size(73, 22);
            this.IncrementStepValue.TabIndex = 38;
            this.IncrementStepValue.TextChanged += new System.EventHandler(this.IncrementStepValue_TextChanged);
            // 
            // LinkToListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 320);
            this.Controls.Add(this.IncrementStepValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IncrementCheck);
            this.Controls.Add(this.StopTimeLink);
            this.Controls.Add(this.StartTimeLink);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbStkObjects);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Link);
            this.Controls.Add(this.Events);
            this.Name = "LinkToListForm";
            this.Text = "Link to Interval List";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbStkObjects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Link;
        private System.Windows.Forms.ComboBox Events;
        private System.Windows.Forms.RadioButton StartTimeLink;
        private System.Windows.Forms.RadioButton StopTimeLink;
        private System.Windows.Forms.CheckBox IncrementCheck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox IncrementStepValue;
    }
}