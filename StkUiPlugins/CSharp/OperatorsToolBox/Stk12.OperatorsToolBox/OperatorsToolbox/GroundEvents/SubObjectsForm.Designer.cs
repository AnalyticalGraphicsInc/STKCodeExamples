namespace OperatorsToolbox.GroundEvents
{
    partial class SubObjectsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubObjectsForm));
            this.SubObjectList = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NameValue = new System.Windows.Forms.TextBox();
            this.LatitudeValue = new System.Windows.Forms.TextBox();
            this.LongitudeValue = new System.Windows.Forms.TextBox();
            this.SubObjectType = new System.Windows.Forms.ComboBox();
            this.Add = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.ZoomLevel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SubObjectList
            // 
            this.SubObjectList.BackColor = System.Drawing.Color.DimGray;
            this.SubObjectList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubObjectList.ForeColor = System.Drawing.Color.White;
            this.SubObjectList.HideSelection = false;
            this.SubObjectList.LargeImageList = this.imageList1;
            this.SubObjectList.Location = new System.Drawing.Point(1, 18);
            this.SubObjectList.Margin = new System.Windows.Forms.Padding(2);
            this.SubObjectList.MultiSelect = false;
            this.SubObjectList.Name = "SubObjectList";
            this.SubObjectList.Size = new System.Drawing.Size(152, 184);
            this.SubObjectList.SmallImageList = this.imageList1;
            this.SubObjectList.TabIndex = 0;
            this.SubObjectList.UseCompatibleStateImageBehavior = false;
            this.SubObjectList.View = System.Windows.Forms.View.SmallIcon;
            this.SubObjectList.SelectedIndexChanged += new System.EventHandler(this.SubObjectList_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "OrangeSquare");
            this.imageList1.Images.SetKeyName(1, "BlueTriangle");
            this.imageList1.Images.SetKeyName(2, "RedCircle");
            this.imageList1.Images.SetKeyName(3, "lightning.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Latitude:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Longitude:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(180, 142);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Type:";
            // 
            // NameValue
            // 
            this.NameValue.BackColor = System.Drawing.Color.DimGray;
            this.NameValue.ForeColor = System.Drawing.Color.White;
            this.NameValue.Location = new System.Drawing.Point(242, 38);
            this.NameValue.Margin = new System.Windows.Forms.Padding(2);
            this.NameValue.Name = "NameValue";
            this.NameValue.Size = new System.Drawing.Size(134, 20);
            this.NameValue.TabIndex = 5;
            // 
            // LatitudeValue
            // 
            this.LatitudeValue.BackColor = System.Drawing.Color.DimGray;
            this.LatitudeValue.ForeColor = System.Drawing.Color.White;
            this.LatitudeValue.Location = new System.Drawing.Point(242, 71);
            this.LatitudeValue.Margin = new System.Windows.Forms.Padding(2);
            this.LatitudeValue.Name = "LatitudeValue";
            this.LatitudeValue.Size = new System.Drawing.Size(134, 20);
            this.LatitudeValue.TabIndex = 6;
            // 
            // LongitudeValue
            // 
            this.LongitudeValue.BackColor = System.Drawing.Color.DimGray;
            this.LongitudeValue.ForeColor = System.Drawing.Color.White;
            this.LongitudeValue.Location = new System.Drawing.Point(242, 106);
            this.LongitudeValue.Margin = new System.Windows.Forms.Padding(2);
            this.LongitudeValue.Name = "LongitudeValue";
            this.LongitudeValue.Size = new System.Drawing.Size(134, 20);
            this.LongitudeValue.TabIndex = 7;
            // 
            // SubObjectType
            // 
            this.SubObjectType.BackColor = System.Drawing.Color.DimGray;
            this.SubObjectType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SubObjectType.ForeColor = System.Drawing.Color.White;
            this.SubObjectType.FormattingEnabled = true;
            this.SubObjectType.Location = new System.Drawing.Point(242, 142);
            this.SubObjectType.Margin = new System.Windows.Forms.Padding(2);
            this.SubObjectType.Name = "SubObjectType";
            this.SubObjectType.Size = new System.Drawing.Size(134, 21);
            this.SubObjectType.TabIndex = 8;
            this.SubObjectType.SelectedIndexChanged += new System.EventHandler(this.SubObjectType_SelectedIndexChanged);
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.SteelBlue;
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Add.Location = new System.Drawing.Point(1, 206);
            this.Add.Margin = new System.Windows.Forms.Padding(2);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(56, 29);
            this.Add.TabIndex = 9;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Remove
            // 
            this.Remove.BackColor = System.Drawing.Color.SteelBlue;
            this.Remove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Remove.Location = new System.Drawing.Point(61, 206);
            this.Remove.Margin = new System.Windows.Forms.Padding(2);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(59, 29);
            this.Remove.TabIndex = 10;
            this.Remove.Text = "Remove";
            this.Remove.UseVisualStyleBackColor = false;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // Apply
            // 
            this.Apply.BackColor = System.Drawing.Color.SteelBlue;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Apply.Location = new System.Drawing.Point(182, 232);
            this.Apply.Margin = new System.Windows.Forms.Padding(2);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(94, 32);
            this.Apply.TabIndex = 11;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.Location = new System.Drawing.Point(283, 232);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(94, 32);
            this.Cancel.TabIndex = 12;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ZoomLevel
            // 
            this.ZoomLevel.BackColor = System.Drawing.Color.DimGray;
            this.ZoomLevel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ZoomLevel.ForeColor = System.Drawing.Color.White;
            this.ZoomLevel.FormattingEnabled = true;
            this.ZoomLevel.Location = new System.Drawing.Point(242, 180);
            this.ZoomLevel.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomLevel.Name = "ZoomLevel";
            this.ZoomLevel.Size = new System.Drawing.Size(134, 21);
            this.ZoomLevel.TabIndex = 14;
            this.ZoomLevel.SelectedIndexChanged += new System.EventHandler(this.ZoomLevel_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 180);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Zoom:";
            // 
            // SubObjectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(386, 274);
            this.Controls.Add(this.ZoomLevel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.SubObjectType);
            this.Controls.Add(this.LongitudeValue);
            this.Controls.Add(this.LatitudeValue);
            this.Controls.Add(this.NameValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SubObjectList);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SubObjectsForm";
            this.Text = "Insert Sub-Object";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView SubObjectList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox NameValue;
        private System.Windows.Forms.TextBox LatitudeValue;
        private System.Windows.Forms.TextBox LongitudeValue;
        private System.Windows.Forms.ComboBox SubObjectType;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ComboBox ZoomLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ImageList imageList1;
    }
}