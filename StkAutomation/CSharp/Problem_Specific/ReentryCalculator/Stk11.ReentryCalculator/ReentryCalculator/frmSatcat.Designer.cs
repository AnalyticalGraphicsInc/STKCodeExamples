namespace ReentryCalculator
{
    partial class frmSatcat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSatcat));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateSatcatDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.tbKeyword = new System.Windows.Forms.TextBox();
            this.btnGetInitialState = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvSatList = new System.Windows.Forms.DataGridView();
            this.IntDesignator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CatalogNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEpoch = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSatelliteList = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSatList)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(746, 24);
            this.menuStrip1.TabIndex = 261;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateSatcatDataToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // updateSatcatDataToolStripMenuItem
            // 
            this.updateSatcatDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("updateSatcatDataToolStripMenuItem.Image")));
            this.updateSatcatDataToolStripMenuItem.Name = "updateSatcatDataToolStripMenuItem";
            this.updateSatcatDataToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.updateSatcatDataToolStripMenuItem.Text = "Update SATCAT Data";
            this.updateSatcatDataToolStripMenuItem.Click += new System.EventHandler(this.updateSatcatDataToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 264;
            this.label2.Text = "Search SATCAT for...";
            // 
            // tbKeyword
            // 
            this.tbKeyword.Location = new System.Drawing.Point(17, 128);
            this.tbKeyword.Name = "tbKeyword";
            this.tbKeyword.Size = new System.Drawing.Size(172, 20);
            this.tbKeyword.TabIndex = 255;
            // 
            // btnGetInitialState
            // 
            this.btnGetInitialState.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGetInitialState.Location = new System.Drawing.Point(17, 190);
            this.btnGetInitialState.Name = "btnGetInitialState";
            this.btnGetInitialState.Size = new System.Drawing.Size(172, 30);
            this.btnGetInitialState.TabIndex = 263;
            this.btnGetInitialState.Text = "Get Initial State";
            this.btnGetInitialState.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSearch.Location = new System.Drawing.Point(17, 154);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(172, 30);
            this.btnSearch.TabIndex = 259;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvSatList
            // 
            this.dgvSatList.AllowUserToAddRows = false;
            this.dgvSatList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSatList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSatList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IntDesignator,
            this.CatalogNumber,
            this.CommonName});
            this.dgvSatList.Location = new System.Drawing.Point(204, 48);
            this.dgvSatList.MultiSelect = false;
            this.dgvSatList.Name = "dgvSatList";
            this.dgvSatList.Size = new System.Drawing.Size(514, 172);
            this.dgvSatList.TabIndex = 262;
            // 
            // IntDesignator
            // 
            this.IntDesignator.HeaderText = "International Designator";
            this.IntDesignator.Name = "IntDesignator";
            this.IntDesignator.ReadOnly = true;
            this.IntDesignator.Width = 160;
            // 
            // CatalogNumber
            // 
            this.CatalogNumber.HeaderText = "Catalog Number";
            this.CatalogNumber.Name = "CatalogNumber";
            this.CatalogNumber.ReadOnly = true;
            this.CatalogNumber.Width = 110;
            // 
            // CommonName
            // 
            this.CommonName.HeaderText = "Common Name";
            this.CommonName.Name = "CommonName";
            this.CommonName.ReadOnly = true;
            this.CommonName.Width = 180;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar1,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 242);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(746, 22);
            this.statusStrip1.TabIndex = 260;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(122, 17);
            this.lblStatus.Text = "Not connected to STK";
            // 
            // lblEpoch
            // 
            this.lblEpoch.AutoSize = true;
            this.lblEpoch.Location = new System.Drawing.Point(17, 80);
            this.lblEpoch.Name = "lblEpoch";
            this.lblEpoch.Size = new System.Drawing.Size(41, 13);
            this.lblEpoch.TabIndex = 258;
            this.lblEpoch.Text = "Epoch:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 257;
            this.label1.Text = "Satellite";
            // 
            // cbSatelliteList
            // 
            this.cbSatelliteList.FormattingEnabled = true;
            this.cbSatelliteList.Location = new System.Drawing.Point(17, 48);
            this.cbSatelliteList.Name = "cbSatelliteList";
            this.cbSatelliteList.Size = new System.Drawing.Size(172, 21);
            this.cbSatelliteList.TabIndex = 256;
            this.cbSatelliteList.Text = "Select...";
            // 
            // frmSatcat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 264);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbKeyword);
            this.Controls.Add(this.btnGetInitialState);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvSatList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblEpoch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSatelliteList);
            this.Name = "frmSatcat";
            this.Text = "Browse from SATCAT";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSatList)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateSatcatDataToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbKeyword;
        private System.Windows.Forms.Button btnGetInitialState;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvSatList;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntDesignator;
        private System.Windows.Forms.DataGridViewTextBoxColumn CatalogNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommonName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Label lblEpoch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSatelliteList;
    }
}