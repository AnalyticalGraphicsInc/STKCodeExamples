namespace OperatorsToolbox.ImportTLEfromUDL
{
    partial class InsertTleFromUdl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsertTleFromUdl));
            this.x = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.lbl_status = new System.Windows.Forms.Label();
            this.btn_makeRequest = new System.Windows.Forms.Button();
            this.tb_ssc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_end = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_start = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.tb_userName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CreateSats = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ConstType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CoordSystem = new System.Windows.Forms.ComboBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.ConstName = new System.Windows.Forms.TextBox();
            this.ExistingConst = new System.Windows.Forms.ComboBox();
            this.NameBasedOnCatalog = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // x
            // 
            this.x.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("x.ImageStream")));
            this.x.TransparentColor = System.Drawing.Color.Transparent;
            this.x.Images.SetKeyName(0, "x-mark.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Firebrick;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 23);
            this.label1.TabIndex = 31;
            this.label1.Text = "Import TLE From UDL";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ImageIndex = 0;
            this.Cancel.ImageList = this.x;
            this.Cancel.Location = new System.Drawing.Point(324, 2);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(24, 26);
            this.Cancel.TabIndex = 30;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(7, 535);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(0, 13);
            this.lbl_status.TabIndex = 43;
            // 
            // btn_makeRequest
            // 
            this.btn_makeRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_makeRequest.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_makeRequest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_makeRequest.Location = new System.Drawing.Point(75, 639);
            this.btn_makeRequest.Name = "btn_makeRequest";
            this.btn_makeRequest.Size = new System.Drawing.Size(195, 29);
            this.btn_makeRequest.TabIndex = 42;
            this.btn_makeRequest.Text = "Make Request";
            this.btn_makeRequest.UseVisualStyleBackColor = false;
            this.btn_makeRequest.Click += new System.EventHandler(this.btn_makeRequest_Click);
            // 
            // tb_ssc
            // 
            this.tb_ssc.AcceptsReturn = true;
            this.tb_ssc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_ssc.BackColor = System.Drawing.Color.DimGray;
            this.tb_ssc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_ssc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ssc.ForeColor = System.Drawing.Color.White;
            this.tb_ssc.Location = new System.Drawing.Point(8, 106);
            this.tb_ssc.Multiline = true;
            this.tb_ssc.Name = "tb_ssc";
            this.tb_ssc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_ssc.Size = new System.Drawing.Size(329, 244);
            this.tb_ssc.TabIndex = 41;
            this.tb_ssc.Text = "scc #s";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Satellite SCCs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "End Date";
            // 
            // dtp_end
            // 
            this.dtp_end.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_end.CustomFormat = "yyyy-MM-ddTHH:mm:ss";
            this.dtp_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_end.Location = new System.Drawing.Point(159, 53);
            this.dtp_end.Name = "dtp_end";
            this.dtp_end.Size = new System.Drawing.Size(162, 20);
            this.dtp_end.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Start Date";
            // 
            // dtp_start
            // 
            this.dtp_start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_start.CustomFormat = "yyyy-MM-ddTHH:mm:ss";
            this.dtp_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_start.Location = new System.Drawing.Point(159, 18);
            this.dtp_start.Name = "dtp_start";
            this.dtp_start.Size = new System.Drawing.Size(162, 20);
            this.dtp_start.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Password:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "Username:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // tb_password
            // 
            this.tb_password.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_password.BackColor = System.Drawing.Color.DimGray;
            this.tb_password.ForeColor = System.Drawing.Color.White;
            this.tb_password.Location = new System.Drawing.Point(157, 49);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(167, 20);
            this.tb_password.TabIndex = 33;
            this.tb_password.Text = "passwordhere";
            this.tb_password.UseSystemPasswordChar = true;
            this.tb_password.TextChanged += new System.EventHandler(this.tb_password_TextChanged);
            // 
            // tb_userName
            // 
            this.tb_userName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_userName.BackColor = System.Drawing.Color.DimGray;
            this.tb_userName.ForeColor = System.Drawing.Color.White;
            this.tb_userName.Location = new System.Drawing.Point(157, 23);
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(167, 20);
            this.tb_userName.TabIndex = 32;
            this.tb_userName.Text = "usernamehere";
            this.tb_userName.TextChanged += new System.EventHandler(this.tb_userName_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.NameBasedOnCatalog);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.CreateSats);
            this.groupBox1.Controls.Add(this.dtp_start);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtp_end);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_ssc);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(4, 116);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(344, 399);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Satellite Options";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(22, 375);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 9);
            this.label7.TabIndex = 43;
            this.label7.Text = "Disabling will only create TLE file";
            // 
            // CreateSats
            // 
            this.CreateSats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreateSats.AutoSize = true;
            this.CreateSats.Location = new System.Drawing.Point(8, 356);
            this.CreateSats.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CreateSats.Name = "CreateSats";
            this.CreateSats.Size = new System.Drawing.Size(168, 17);
            this.CreateSats.TabIndex = 42;
            this.CreateSats.Text = "Create Satellites on Download";
            this.CreateSats.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tb_password);
            this.groupBox2.Controls.Add(this.tb_userName);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(4, 31);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(344, 80);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Credentials";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.ConstType);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.CoordSystem);
            this.groupBox3.Controls.Add(this.NameLabel);
            this.groupBox3.Controls.Add(this.ConstName);
            this.groupBox3.Controls.Add(this.ExistingConst);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(4, 520);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(344, 113);
            this.groupBox3.TabIndex = 46;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Constellation Options";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 20);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Definition:";
            // 
            // ConstType
            // 
            this.ConstType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstType.BackColor = System.Drawing.Color.DimGray;
            this.ConstType.ForeColor = System.Drawing.Color.White;
            this.ConstType.FormattingEnabled = true;
            this.ConstType.Location = new System.Drawing.Point(160, 18);
            this.ConstType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ConstType.Name = "ConstType";
            this.ConstType.Size = new System.Drawing.Size(176, 21);
            this.ConstType.TabIndex = 32;
            this.ConstType.SelectedIndexChanged += new System.EventHandler(this.ConstType_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 74);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Coordinate Representation:";
            // 
            // CoordSystem
            // 
            this.CoordSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CoordSystem.BackColor = System.Drawing.Color.DimGray;
            this.CoordSystem.ForeColor = System.Drawing.Color.White;
            this.CoordSystem.FormattingEnabled = true;
            this.CoordSystem.Location = new System.Drawing.Point(235, 72);
            this.CoordSystem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CoordSystem.Name = "CoordSystem";
            this.CoordSystem.Size = new System.Drawing.Size(102, 21);
            this.CoordSystem.TabIndex = 6;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(5, 46);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(101, 13);
            this.NameLabel.TabIndex = 5;
            this.NameLabel.Text = "Constellation Name:";
            // 
            // ConstName
            // 
            this.ConstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstName.BackColor = System.Drawing.Color.DimGray;
            this.ConstName.ForeColor = System.Drawing.Color.White;
            this.ConstName.Location = new System.Drawing.Point(205, 45);
            this.ConstName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ConstName.Name = "ConstName";
            this.ConstName.Size = new System.Drawing.Size(132, 20);
            this.ConstName.TabIndex = 4;
            // 
            // ExistingConst
            // 
            this.ExistingConst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExistingConst.BackColor = System.Drawing.Color.DimGray;
            this.ExistingConst.ForeColor = System.Drawing.Color.White;
            this.ExistingConst.FormattingEnabled = true;
            this.ExistingConst.Location = new System.Drawing.Point(203, 44);
            this.ExistingConst.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ExistingConst.Name = "ExistingConst";
            this.ExistingConst.Size = new System.Drawing.Size(133, 21);
            this.ExistingConst.TabIndex = 3;
            // 
            // NameBasedOnCatalog
            // 
            this.NameBasedOnCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NameBasedOnCatalog.AutoSize = true;
            this.NameBasedOnCatalog.Location = new System.Drawing.Point(190, 356);
            this.NameBasedOnCatalog.Margin = new System.Windows.Forms.Padding(2);
            this.NameBasedOnCatalog.Name = "NameBasedOnCatalog";
            this.NameBasedOnCatalog.Size = new System.Drawing.Size(141, 17);
            this.NameBasedOnCatalog.TabIndex = 44;
            this.NameBasedOnCatalog.Text = "Name Based on Catalog";
            this.NameBasedOnCatalog.UseVisualStyleBackColor = true;
            // 
            // InsertTleFromUdl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.btn_makeRequest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "InsertTleFromUdl";
            this.Size = new System.Drawing.Size(350, 700);
            this.Load += new System.EventHandler(this.InsertTLEFromUDL_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList x;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Button btn_makeRequest;
        private System.Windows.Forms.TextBox tb_ssc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_end;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_start;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.TextBox tb_userName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ConstType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox CoordSystem;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox ConstName;
        private System.Windows.Forms.ComboBox ExistingConst;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox CreateSats;
        private System.Windows.Forms.CheckBox NameBasedOnCatalog;
    }
}