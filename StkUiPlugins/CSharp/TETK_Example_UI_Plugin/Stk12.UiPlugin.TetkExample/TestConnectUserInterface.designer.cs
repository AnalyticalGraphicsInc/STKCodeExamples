namespace Stk12.UiPlugin.TetkExample
{
    partial class TestConnectUserInterface
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
            this.textBox_ConnectCommand = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button_RunConnectCommand = new System.Windows.Forms.Button();
            this.button_ExportConnectCmdsToList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_ConnectCommand
            // 
            this.textBox_ConnectCommand.Location = new System.Drawing.Point(21, 38);
            this.textBox_ConnectCommand.Name = "textBox_ConnectCommand";
            this.textBox_ConnectCommand.Size = new System.Drawing.Size(471, 20);
            this.textBox_ConnectCommand.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(21, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(130, 13);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "Enter Connect Command:";
            // 
            // button_RunConnectCommand
            // 
            this.button_RunConnectCommand.Location = new System.Drawing.Point(178, 64);
            this.button_RunConnectCommand.Name = "button_RunConnectCommand";
            this.button_RunConnectCommand.Size = new System.Drawing.Size(134, 33);
            this.button_RunConnectCommand.TabIndex = 2;
            this.button_RunConnectCommand.Text = "Run Connect Command";
            this.button_RunConnectCommand.UseVisualStyleBackColor = true;
            this.button_RunConnectCommand.Click += new System.EventHandler(this.button_RunConnectCommand_Click);
            // 
            // button_ExportConnectCmdsToList
            // 
            this.button_ExportConnectCmdsToList.Location = new System.Drawing.Point(129, 103);
            this.button_ExportConnectCmdsToList.Name = "button_ExportConnectCmdsToList";
            this.button_ExportConnectCmdsToList.Size = new System.Drawing.Size(225, 41);
            this.button_ExportConnectCmdsToList.TabIndex = 3;
            this.button_ExportConnectCmdsToList.Text = "Export All Connect Commands\r\nRun in Plugin to .txt";
            this.button_ExportConnectCmdsToList.UseVisualStyleBackColor = true;
            this.button_ExportConnectCmdsToList.Click += new System.EventHandler(this.button_ExportConnectCmdsToList_Click);
            // 
            // TestConnectUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_ExportConnectCmdsToList);
            this.Controls.Add(this.button_RunConnectCommand);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox_ConnectCommand);
            this.Name = "TestConnectUserInterface";
            this.Size = new System.Drawing.Size(517, 296);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ConnectCommand;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button_RunConnectCommand;
        private System.Windows.Forms.Button button_ExportConnectCmdsToList;
    }
}
