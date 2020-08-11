namespace InsertTLEFromUDL
{
    partial class Form1
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
            this.tb_userName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_start = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_end = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_ssc = new System.Windows.Forms.TextBox();
            this.btn_makeRequest = new System.Windows.Forms.Button();
            this.lbl_status = new System.Windows.Forms.Label();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_userName
            // 
            this.tb_userName.Location = new System.Drawing.Point(15, 35);
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(107, 20);
            this.tb_userName.TabIndex = 0;
            this.tb_userName.Text = "usernamehere";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // dtp_start
            // 
            this.dtp_start.CustomFormat = "yyyy-MM-ddTHH:mm:ss";
            this.dtp_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_start.Location = new System.Drawing.Point(15, 138);
            this.dtp_start.Name = "dtp_start";
            this.dtp_start.Size = new System.Drawing.Size(152, 20);
            this.dtp_start.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Start Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "End Date";
            // 
            // dtp_end
            // 
            this.dtp_end.CustomFormat = "yyyy-MM-ddTHH:mm:ss";
            this.dtp_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_end.Location = new System.Drawing.Point(15, 190);
            this.dtp_end.Name = "dtp_end";
            this.dtp_end.Size = new System.Drawing.Size(152, 20);
            this.dtp_end.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Satellite SSCs";
            // 
            // tb_ssc
            // 
            this.tb_ssc.Location = new System.Drawing.Point(15, 252);
            this.tb_ssc.Multiline = true;
            this.tb_ssc.Name = "tb_ssc";
            this.tb_ssc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_ssc.Size = new System.Drawing.Size(181, 163);
            this.tb_ssc.TabIndex = 9;
            this.tb_ssc.Text = "ssc #s";
            // 
            // btn_makeRequest
            // 
            this.btn_makeRequest.Location = new System.Drawing.Point(15, 431);
            this.btn_makeRequest.Name = "btn_makeRequest";
            this.btn_makeRequest.Size = new System.Drawing.Size(181, 29);
            this.btn_makeRequest.TabIndex = 10;
            this.btn_makeRequest.Text = "Get TLEs and Create Satellites";
            this.btn_makeRequest.UseVisualStyleBackColor = true;
            this.btn_makeRequest.Click += new System.EventHandler(this.btn_makeRequest_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(15, 475);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(51, 13);
            this.lbl_status.TabIndex = 11;
            this.lbl_status.Text = "lbl_status";
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(142, 6);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(54, 23);
            this.btn_refresh.TabIndex = 12;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(15, 82);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(181, 20);
            this.tb_password.TabIndex = 1;
            this.tb_password.Text = "passwordhere";
            this.tb_password.UseSystemPasswordChar = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 511);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.btn_makeRequest);
            this.Controls.Add(this.tb_ssc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtp_end);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtp_start);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_userName);
            this.Name = "Form1";
            this.Text = "TLEs from UDL";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_userName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp_start;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_end;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_ssc;
        private System.Windows.Forms.Button btn_makeRequest;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.TextBox tb_password;
    }
}

