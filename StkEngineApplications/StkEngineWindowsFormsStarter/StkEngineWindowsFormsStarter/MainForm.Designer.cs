namespace StkEngineWindowsFormsStarter

{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.axAgUiAx2DCntrl1 = new AGI.STKX.Controls.AxAgUiAx2DCntrl();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNewScenario = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Panel2.Controls.Add(this.btnNewScenario);
            this.splitContainer1.Size = new System.Drawing.Size(1076, 770);
            this.splitContainer1.SplitterDistance = 673;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.axAgUiAxVOCntrl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.axAgUiAx2DCntrl1);
            this.splitContainer2.Size = new System.Drawing.Size(1076, 673);
            this.splitContainer2.SplitterDistance = 553;
            this.splitContainer2.TabIndex = 0;
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAxVOCntrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAxVOCntrl1.Picture")));
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(553, 673);
            this.axAgUiAxVOCntrl1.TabIndex = 0;
            this.axAgUiAxVOCntrl1.MouseDownEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEventHandler(this.axAgUiAxVOCntrl1_MouseDownEvent);
            this.axAgUiAxVOCntrl1.MouseUpEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEventHandler(this.axAgUiAxVOCntrl1_MouseUpEvent);
            // 
            // axAgUiAx2DCntrl1
            // 
            this.axAgUiAx2DCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAx2DCntrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAgUiAx2DCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAx2DCntrl1.Name = "axAgUiAx2DCntrl1";
            this.axAgUiAx2DCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAx2DCntrl1.Picture")));
            this.axAgUiAx2DCntrl1.Size = new System.Drawing.Size(519, 673);
            this.axAgUiAx2DCntrl1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(974, 45);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 36);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNewScenario
            // 
            this.btnNewScenario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewScenario.Location = new System.Drawing.Point(12, 45);
            this.btnNewScenario.Name = "btnNewScenario";
            this.btnNewScenario.Size = new System.Drawing.Size(90, 36);
            this.btnNewScenario.TabIndex = 0;
            this.btnNewScenario.Text = "New Scenario";
            this.btnNewScenario.UseVisualStyleBackColor = true;
            this.btnNewScenario.Click += new System.EventHandler(this.btnNewScenario_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 770);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
        private AGI.STKX.Controls.AxAgUiAx2DCntrl axAgUiAx2DCntrl1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNewScenario;
    }
}

