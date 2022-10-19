namespace Stk12.UiPlugin.ObjectModelTutorial
{
    partial class CustomConfigPage
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
            this.lblStringValue = new System.Windows.Forms.Label();
            this.lblDoubleValue = new System.Windows.Forms.Label();
            this.txtStringValue = new System.Windows.Forms.TextBox();
            this.nudDoubleValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudDoubleValue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStringValue
            // 
            this.lblStringValue.AutoSize = true;
            this.lblStringValue.Location = new System.Drawing.Point(19, 38);
            this.lblStringValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStringValue.Name = "lblStringValue";
            this.lblStringValue.Size = new System.Drawing.Size(93, 17);
            this.lblStringValue.TabIndex = 0;
            this.lblStringValue.Text = "String Value: ";
            // 
            // lblDoubleValue
            // 
            this.lblDoubleValue.AutoSize = true;
            this.lblDoubleValue.Location = new System.Drawing.Point(19, 105);
            this.lblDoubleValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDoubleValue.Name = "lblDoubleValue";
            this.lblDoubleValue.Size = new System.Drawing.Size(101, 17);
            this.lblDoubleValue.TabIndex = 1;
            this.lblDoubleValue.Text = "Double Value: ";
            // 
            // txtStringValue
            // 
            this.txtStringValue.Location = new System.Drawing.Point(156, 34);
            this.txtStringValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStringValue.Name = "txtStringValue";
            this.txtStringValue.Size = new System.Drawing.Size(239, 22);
            this.txtStringValue.TabIndex = 2;
            // 
            // nudDoubleValue
            // 
            this.nudDoubleValue.DecimalPlaces = 3;
            this.nudDoubleValue.Location = new System.Drawing.Point(156, 102);
            this.nudDoubleValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudDoubleValue.Name = "nudDoubleValue";
            this.nudDoubleValue.Size = new System.Drawing.Size(176, 22);
            this.nudDoubleValue.TabIndex = 3;
            // 
            // CustomConfigPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudDoubleValue);
            this.Controls.Add(this.txtStringValue);
            this.Controls.Add(this.lblDoubleValue);
            this.Controls.Add(this.lblStringValue);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CustomConfigPage";
            this.Size = new System.Drawing.Size(559, 546);
            ((System.ComponentModel.ISupportInitialize)(this.nudDoubleValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStringValue;
        private System.Windows.Forms.Label lblDoubleValue;
        private System.Windows.Forms.TextBox txtStringValue;
        private System.Windows.Forms.NumericUpDown nudDoubleValue;
    }
}
