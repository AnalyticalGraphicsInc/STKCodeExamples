namespace OperatorsToolbox.VolumeCreator
{
    partial class LocationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationForm));
            this.SelectFacility = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.Long = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.Lat = new System.Windows.Forms.TextBox();
            this.LocationName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Create = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectFacility
            // 
            this.SelectFacility.BackColor = System.Drawing.Color.SteelBlue;
            this.SelectFacility.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectFacility.ForeColor = System.Drawing.Color.White;
            this.SelectFacility.Location = new System.Drawing.Point(161, 54);
            this.SelectFacility.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SelectFacility.Name = "SelectFacility";
            this.SelectFacility.Size = new System.Drawing.Size(82, 24);
            this.SelectFacility.TabIndex = 11;
            this.SelectFacility.Text = "Select Facility";
            this.SelectFacility.UseVisualStyleBackColor = false;
            this.SelectFacility.Click += new System.EventHandler(this.SelectFacility_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(130, 69);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(25, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "deg";
            // 
            // Long
            // 
            this.Long.BackColor = System.Drawing.Color.DimGray;
            this.Long.ForeColor = System.Drawing.Color.White;
            this.Long.Location = new System.Drawing.Point(53, 69);
            this.Long.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Long.Name = "Long";
            this.Long.Size = new System.Drawing.Size(73, 20);
            this.Long.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(130, 46);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "deg";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 72);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(34, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "Long:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 46);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Lat:";
            // 
            // Lat
            // 
            this.Lat.BackColor = System.Drawing.Color.DimGray;
            this.Lat.ForeColor = System.Drawing.Color.White;
            this.Lat.Location = new System.Drawing.Point(53, 43);
            this.Lat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Lat.Name = "Lat";
            this.Lat.Size = new System.Drawing.Size(73, 20);
            this.Lat.TabIndex = 9;
            // 
            // LocationName
            // 
            this.LocationName.BackColor = System.Drawing.Color.DimGray;
            this.LocationName.ForeColor = System.Drawing.Color.White;
            this.LocationName.Location = new System.Drawing.Point(53, 20);
            this.LocationName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LocationName.Name = "LocationName";
            this.LocationName.Size = new System.Drawing.Size(188, 20);
            this.LocationName.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Name:";
            // 
            // Create
            // 
            this.Create.BackColor = System.Drawing.Color.SteelBlue;
            this.Create.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Create.ForeColor = System.Drawing.Color.White;
            this.Create.Location = new System.Drawing.Point(30, 105);
            this.Create.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(82, 24);
            this.Create.TabIndex = 15;
            this.Create.Text = "Create";
            this.Create.UseVisualStyleBackColor = false;
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LocationName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SelectFacility);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.Long);
            this.groupBox1.Controls.Add(this.Lat);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(5, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(248, 93);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Definition";
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.ForeColor = System.Drawing.Color.White;
            this.Cancel.Location = new System.Drawing.Point(125, 105);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 24);
            this.Cancel.TabIndex = 17;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // LocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(257, 140);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Create);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LocationForm";
            this.Text = "Create Location";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button SelectFacility;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox Long;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox Lat;
        private System.Windows.Forms.TextBox LocationName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Create;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Cancel;
    }
}