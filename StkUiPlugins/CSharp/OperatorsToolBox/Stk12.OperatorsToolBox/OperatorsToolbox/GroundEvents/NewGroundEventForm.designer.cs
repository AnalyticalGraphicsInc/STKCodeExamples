namespace OperatorsToolbox.GroundEvents
{
    partial class NewGroundEventForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGroundEventForm));
            this.ManualSSR = new System.Windows.Forms.RadioButton();
            this.SSRFromFile = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CreateButton = new System.Windows.Forms.Button();
            this.IDText = new System.Windows.Forms.TextBox();
            this.CountryText = new System.Windows.Forms.TextBox();
            this.Latitude = new System.Windows.Forms.TextBox();
            this.Longitude = new System.Windows.Forms.TextBox();
            this.StartTimeText = new System.Windows.Forms.TextBox();
            this.StopTimeText = new System.Windows.Forms.TextBox();
            this.DesciptionText = new System.Windows.Forms.TextBox();
            this.FileText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ImportAll = new System.Windows.Forms.RadioButton();
            this.ImportActive = new System.Windows.Forms.RadioButton();
            this.Browse = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.TypeSelect = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ManualSSR
            // 
            this.ManualSSR.AutoSize = true;
            this.ManualSSR.Checked = true;
            this.ManualSSR.ForeColor = System.Drawing.Color.White;
            this.ManualSSR.Location = new System.Drawing.Point(9, 10);
            this.ManualSSR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ManualSSR.Name = "ManualSSR";
            this.ManualSSR.Size = new System.Drawing.Size(95, 17);
            this.ManualSSR.TabIndex = 0;
            this.ManualSSR.TabStop = true;
            this.ManualSSR.Text = "Manually Enter";
            this.ManualSSR.UseVisualStyleBackColor = true;
            this.ManualSSR.CheckedChanged += new System.EventHandler(this.ManualSSR_CheckedChanged);
            // 
            // SSRFromFile
            // 
            this.SSRFromFile.AutoSize = true;
            this.SSRFromFile.ForeColor = System.Drawing.Color.White;
            this.SSRFromFile.Location = new System.Drawing.Point(116, 10);
            this.SSRFromFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SSRFromFile.Name = "SSRFromFile";
            this.SSRFromFile.Size = new System.Drawing.Size(111, 17);
            this.SSRFromFile.TabIndex = 1;
            this.SSRFromFile.Text = "From Spreadsheet";
            this.SSRFromFile.UseVisualStyleBackColor = true;
            this.SSRFromFile.CheckedChanged += new System.EventHandler(this.SSRFromFile_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "AOR: ";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Latitude: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 117);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Longitude: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 156);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Start Time(UTCG): ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 197);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Stop Time(UTCG): ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 262);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Description(optional):";
            // 
            // CreateButton
            // 
            this.CreateButton.BackColor = System.Drawing.Color.SteelBlue;
            this.CreateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CreateButton.ForeColor = System.Drawing.Color.White;
            this.CreateButton.Location = new System.Drawing.Point(99, 496);
            this.CreateButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(85, 36);
            this.CreateButton.TabIndex = 9;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = false;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // IDText
            // 
            this.IDText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IDText.BackColor = System.Drawing.Color.DimGray;
            this.IDText.ForeColor = System.Drawing.Color.White;
            this.IDText.Location = new System.Drawing.Point(155, 11);
            this.IDText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.IDText.Name = "IDText";
            this.IDText.Size = new System.Drawing.Size(154, 20);
            this.IDText.TabIndex = 11;
            // 
            // CountryText
            // 
            this.CountryText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CountryText.BackColor = System.Drawing.Color.DimGray;
            this.CountryText.ForeColor = System.Drawing.Color.White;
            this.CountryText.Location = new System.Drawing.Point(155, 46);
            this.CountryText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CountryText.Name = "CountryText";
            this.CountryText.Size = new System.Drawing.Size(154, 20);
            this.CountryText.TabIndex = 12;
            // 
            // Latitude
            // 
            this.Latitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Latitude.BackColor = System.Drawing.Color.DimGray;
            this.Latitude.ForeColor = System.Drawing.Color.White;
            this.Latitude.Location = new System.Drawing.Point(155, 84);
            this.Latitude.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Latitude.Name = "Latitude";
            this.Latitude.Size = new System.Drawing.Size(154, 20);
            this.Latitude.TabIndex = 13;
            // 
            // Longitude
            // 
            this.Longitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Longitude.BackColor = System.Drawing.Color.DimGray;
            this.Longitude.ForeColor = System.Drawing.Color.White;
            this.Longitude.Location = new System.Drawing.Point(155, 118);
            this.Longitude.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Longitude.Name = "Longitude";
            this.Longitude.Size = new System.Drawing.Size(154, 20);
            this.Longitude.TabIndex = 14;
            // 
            // StartTimeText
            // 
            this.StartTimeText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartTimeText.BackColor = System.Drawing.Color.DimGray;
            this.StartTimeText.ForeColor = System.Drawing.Color.White;
            this.StartTimeText.Location = new System.Drawing.Point(155, 157);
            this.StartTimeText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StartTimeText.Name = "StartTimeText";
            this.StartTimeText.Size = new System.Drawing.Size(154, 20);
            this.StartTimeText.TabIndex = 15;
            // 
            // StopTimeText
            // 
            this.StopTimeText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StopTimeText.BackColor = System.Drawing.Color.DimGray;
            this.StopTimeText.ForeColor = System.Drawing.Color.White;
            this.StopTimeText.Location = new System.Drawing.Point(155, 197);
            this.StopTimeText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StopTimeText.Name = "StopTimeText";
            this.StopTimeText.Size = new System.Drawing.Size(154, 20);
            this.StopTimeText.TabIndex = 16;
            // 
            // DesciptionText
            // 
            this.DesciptionText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DesciptionText.BackColor = System.Drawing.Color.DimGray;
            this.DesciptionText.ForeColor = System.Drawing.Color.White;
            this.DesciptionText.Location = new System.Drawing.Point(155, 260);
            this.DesciptionText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DesciptionText.Multiline = true;
            this.DesciptionText.Name = "DesciptionText";
            this.DesciptionText.Size = new System.Drawing.Size(154, 71);
            this.DesciptionText.TabIndex = 17;
            // 
            // FileText
            // 
            this.FileText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileText.BackColor = System.Drawing.Color.DimGray;
            this.FileText.ForeColor = System.Drawing.Color.White;
            this.FileText.Location = new System.Drawing.Point(20, 30);
            this.FileText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FileText.Name = "FileText";
            this.FileText.Size = new System.Drawing.Size(211, 20);
            this.FileText.TabIndex = 18;
            this.FileText.TextChanged += new System.EventHandler(this.FileText_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ImportAll);
            this.groupBox1.Controls.Add(this.ImportActive);
            this.groupBox1.Controls.Add(this.Browse);
            this.groupBox1.Controls.Add(this.FileText);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(5, 392);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(315, 98);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "From Spreadsheet";
            // 
            // ImportAll
            // 
            this.ImportAll.AutoSize = true;
            this.ImportAll.Location = new System.Drawing.Point(132, 60);
            this.ImportAll.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImportAll.Name = "ImportAll";
            this.ImportAll.Size = new System.Drawing.Size(68, 17);
            this.ImportAll.TabIndex = 21;
            this.ImportAll.TabStop = true;
            this.ImportAll.Text = "Import All";
            this.ImportAll.UseVisualStyleBackColor = true;
            this.ImportAll.CheckedChanged += new System.EventHandler(this.ImportAll_CheckedChanged);
            // 
            // ImportActive
            // 
            this.ImportActive.AutoSize = true;
            this.ImportActive.Location = new System.Drawing.Point(20, 60);
            this.ImportActive.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImportActive.Name = "ImportActive";
            this.ImportActive.Size = new System.Drawing.Size(107, 17);
            this.ImportActive.TabIndex = 20;
            this.ImportActive.TabStop = true;
            this.ImportActive.Text = "Import Open Only";
            this.ImportActive.UseVisualStyleBackColor = true;
            this.ImportActive.CheckedChanged += new System.EventHandler(this.ImportActive_CheckedChanged);
            // 
            // Browse
            // 
            this.Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Browse.BackColor = System.Drawing.Color.SteelBlue;
            this.Browse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Browse.ForeColor = System.Drawing.Color.White;
            this.Browse.Location = new System.Drawing.Point(236, 29);
            this.Browse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(27, 20);
            this.Browse.TabIndex = 19;
            this.Browse.Text = "...";
            this.Browse.UseVisualStyleBackColor = false;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 229);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Category: ";
            // 
            // TypeSelect
            // 
            this.TypeSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TypeSelect.BackColor = System.Drawing.Color.DimGray;
            this.TypeSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TypeSelect.ForeColor = System.Drawing.Color.White;
            this.TypeSelect.FormattingEnabled = true;
            this.TypeSelect.Location = new System.Drawing.Point(154, 230);
            this.TypeSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TypeSelect.Name = "TypeSelect";
            this.TypeSelect.Size = new System.Drawing.Size(155, 21);
            this.TypeSelect.TabIndex = 21;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.TypeSelect);
            this.groupBox2.Controls.Add(this.DesciptionText);
            this.groupBox2.Controls.Add(this.StopTimeText);
            this.groupBox2.Controls.Add(this.StartTimeText);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.Longitude);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.Latitude);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.CountryText);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.IDText);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(5, 38);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(315, 349);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            // 
            // NewGroundEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(323, 540);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.SSRFromFile);
            this.Controls.Add(this.ManualSSR);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "NewGroundEventForm";
            this.Text = "Event Definition";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton ManualSSR;
        private System.Windows.Forms.RadioButton SSRFromFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.TextBox IDText;
        private System.Windows.Forms.TextBox CountryText;
        private System.Windows.Forms.TextBox Latitude;
        private System.Windows.Forms.TextBox Longitude;
        private System.Windows.Forms.TextBox StartTimeText;
        private System.Windows.Forms.TextBox StopTimeText;
        private System.Windows.Forms.TextBox DesciptionText;
        private System.Windows.Forms.TextBox FileText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox TypeSelect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton ImportActive;
        private System.Windows.Forms.RadioButton ImportAll;
    }
}