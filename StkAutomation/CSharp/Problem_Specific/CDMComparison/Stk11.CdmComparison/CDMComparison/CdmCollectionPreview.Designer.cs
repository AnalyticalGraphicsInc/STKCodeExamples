namespace CDMComparison
{
    partial class CdmCollectionPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CdmCollectionPreview));
            this.dgvCDMs = new System.Windows.Forms.DataGridView();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.lblDirectory = new System.Windows.Forms.Label();
            this.btnMixMatch = new System.Windows.Forms.Button();
            this.cbAutoEstimate = new System.Windows.Forms.CheckBox();
            this.btnTrend = new System.Windows.Forms.Button();
            this.btnBinCdms = new System.Windows.Forms.Button();
            this.btnConjunctionBin = new System.Windows.Forms.Button();
            this.btnGetCDMsFromComspoc = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtJobNumber = new System.Windows.Forms.TextBox();
            this.lblCaNum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetCDMsFromJSPOC = new System.Windows.Forms.Button();
            this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCDMs)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCDMs
            // 
            this.dgvCDMs.AllowUserToAddRows = false;
            this.dgvCDMs.AllowUserToDeleteRows = false;
            this.dgvCDMs.AllowUserToResizeRows = false;
            this.dgvCDMs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCDMs.BackgroundColor = System.Drawing.Color.White;
            this.dgvCDMs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCDMs.Location = new System.Drawing.Point(18, 70);
            this.dgvCDMs.Name = "dgvCDMs";
            this.dgvCDMs.RowHeadersVisible = false;
            this.dgvCDMs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCDMs.Size = new System.Drawing.Size(966, 282);
            this.dgvCDMs.TabIndex = 6;
            this.dgvCDMs.DoubleClick += new System.EventHandler(this.dgvCDMs_DoubleClick);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAnalyze.Location = new System.Drawing.Point(18, 363);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(136, 23);
            this.btnAnalyze.TabIndex = 7;
            this.btnAnalyze.Text = "Individual CDM Analysis";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // lblDirectory
            // 
            this.lblDirectory.AutoSize = true;
            this.lblDirectory.Location = new System.Drawing.Point(53, 17);
            this.lblDirectory.Name = "lblDirectory";
            this.lblDirectory.Size = new System.Drawing.Size(118, 13);
            this.lblDirectory.TabIndex = 8;
            this.lblDirectory.Text = "Select a CDM Directory";
            // 
            // btnMixMatch
            // 
            this.btnMixMatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMixMatch.Location = new System.Drawing.Point(848, 363);
            this.btnMixMatch.Name = "btnMixMatch";
            this.btnMixMatch.Size = new System.Drawing.Size(136, 23);
            this.btnMixMatch.TabIndex = 9;
            this.btnMixMatch.Text = "Mix and Match";
            this.btnMixMatch.UseVisualStyleBackColor = true;
            this.btnMixMatch.Click += new System.EventHandler(this.btnMixMatch_Click);
            // 
            // cbAutoEstimate
            // 
            this.cbAutoEstimate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAutoEstimate.AutoSize = true;
            this.cbAutoEstimate.Location = new System.Drawing.Point(842, 16);
            this.cbAutoEstimate.Name = "cbAutoEstimate";
            this.cbAutoEstimate.Size = new System.Drawing.Size(142, 17);
            this.cbAutoEstimate.TabIndex = 10;
            this.cbAutoEstimate.Text = "Auto Estimate Probability";
            this.cbAutoEstimate.UseVisualStyleBackColor = true;
            // 
            // btnTrend
            // 
            this.btnTrend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTrend.Location = new System.Drawing.Point(160, 363);
            this.btnTrend.Name = "btnTrend";
            this.btnTrend.Size = new System.Drawing.Size(136, 23);
            this.btnTrend.TabIndex = 11;
            this.btnTrend.Text = "CDM Trending";
            this.btnTrend.UseVisualStyleBackColor = true;
            this.btnTrend.Click += new System.EventHandler(this.btnTrend_Click);
            // 
            // btnBinCdms
            // 
            this.btnBinCdms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBinCdms.Location = new System.Drawing.Point(302, 363);
            this.btnBinCdms.Name = "btnBinCdms";
            this.btnBinCdms.Size = new System.Drawing.Size(136, 23);
            this.btnBinCdms.TabIndex = 12;
            this.btnBinCdms.Text = "Bin by Min Range";
            this.btnBinCdms.UseVisualStyleBackColor = true;
            this.btnBinCdms.Click += new System.EventHandler(this.btnBinCdms_Click);
            // 
            // btnConjunctionBin
            // 
            this.btnConjunctionBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConjunctionBin.Location = new System.Drawing.Point(444, 363);
            this.btnConjunctionBin.Name = "btnConjunctionBin";
            this.btnConjunctionBin.Size = new System.Drawing.Size(136, 23);
            this.btnConjunctionBin.TabIndex = 13;
            this.btnConjunctionBin.Text = "Bin by Conjunction";
            this.btnConjunctionBin.UseVisualStyleBackColor = true;
            this.btnConjunctionBin.Click += new System.EventHandler(this.btnConjunctionBin_Click);
            // 
            // btnGetCDMsFromComspoc
            // 
            this.btnGetCDMsFromComspoc.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGetCDMsFromComspoc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnGetCDMsFromComspoc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnGetCDMsFromComspoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetCDMsFromComspoc.ForeColor = System.Drawing.SystemColors.Control;
            this.btnGetCDMsFromComspoc.Image = global::CDMComparison.Properties.Resources.Import;
            this.btnGetCDMsFromComspoc.Location = new System.Drawing.Point(18, 41);
            this.btnGetCDMsFromComspoc.Name = "btnGetCDMsFromComspoc";
            this.btnGetCDMsFromComspoc.Size = new System.Drawing.Size(29, 23);
            this.btnGetCDMsFromComspoc.TabIndex = 14;
            this.btnGetCDMsFromComspoc.UseVisualStyleBackColor = true;
            this.btnGetCDMsFromComspoc.Click += new System.EventHandler(this.btnGetCDMsFromComspoc_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBrowse.Image = global::CDMComparison.Properties.Resources.folder;
            this.btnBrowse.Location = new System.Drawing.Point(18, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(29, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtJobNumber
            // 
            this.txtJobNumber.Location = new System.Drawing.Point(160, 43);
            this.txtJobNumber.Name = "txtJobNumber";
            this.txtJobNumber.Size = new System.Drawing.Size(100, 20);
            this.txtJobNumber.TabIndex = 15;
            // 
            // lblCaNum
            // 
            this.lblCaNum.AutoSize = true;
            this.lblCaNum.Location = new System.Drawing.Point(53, 46);
            this.lblCaNum.Name = "lblCaNum";
            this.lblCaNum.Size = new System.Drawing.Size(103, 13);
            this.lblCaNum.TabIndex = 16;
            this.lblCaNum.Text = "ComSpOC CA Job #";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "JSpOC Create Date";
            // 
            // btnGetCDMsFromJSPOC
            // 
            this.btnGetCDMsFromJSPOC.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGetCDMsFromJSPOC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnGetCDMsFromJSPOC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnGetCDMsFromJSPOC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetCDMsFromJSPOC.ForeColor = System.Drawing.SystemColors.Control;
            this.btnGetCDMsFromJSPOC.Image = global::CDMComparison.Properties.Resources.Import;
            this.btnGetCDMsFromJSPOC.Location = new System.Drawing.Point(278, 41);
            this.btnGetCDMsFromJSPOC.Name = "btnGetCDMsFromJSPOC";
            this.btnGetCDMsFromJSPOC.Size = new System.Drawing.Size(29, 23);
            this.btnGetCDMsFromJSPOC.TabIndex = 17;
            this.btnGetCDMsFromJSPOC.UseVisualStyleBackColor = true;
            this.btnGetCDMsFromJSPOC.Click += new System.EventHandler(this.btnGetCDMsFromJSPOC_Click);
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreateDate.Location = new System.Drawing.Point(419, 43);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.Size = new System.Drawing.Size(200, 20);
            this.dtpCreateDate.TabIndex = 20;
            // 
            // CdmCollectionPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 398);
            this.Controls.Add(this.dtpCreateDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetCDMsFromJSPOC);
            this.Controls.Add(this.lblCaNum);
            this.Controls.Add(this.txtJobNumber);
            this.Controls.Add(this.btnGetCDMsFromComspoc);
            this.Controls.Add(this.btnConjunctionBin);
            this.Controls.Add(this.btnBinCdms);
            this.Controls.Add(this.btnTrend);
            this.Controls.Add(this.cbAutoEstimate);
            this.Controls.Add(this.btnMixMatch);
            this.Controls.Add(this.lblDirectory);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.dgvCDMs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CdmCollectionPreview";
            this.Text = "CDM Collection";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCDMs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DataGridView dgvCDMs;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Label lblDirectory;
        private System.Windows.Forms.Button btnMixMatch;
        private System.Windows.Forms.CheckBox cbAutoEstimate;
        private System.Windows.Forms.Button btnTrend;
        private System.Windows.Forms.Button btnBinCdms;
        private System.Windows.Forms.Button btnConjunctionBin;
        private System.Windows.Forms.Button btnGetCDMsFromComspoc;
        private System.Windows.Forms.TextBox txtJobNumber;
        private System.Windows.Forms.Label lblCaNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetCDMsFromJSPOC;
        private System.Windows.Forms.DateTimePicker dtpCreateDate;
    }
}