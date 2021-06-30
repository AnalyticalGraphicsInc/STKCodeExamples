namespace CDMComparison
{
    partial class CdmViewer
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
            this.secondary = new System.Windows.Forms.TabPage();
            this.zSecondary = new ZedGraph.ZedGraphControl();
            this.primary = new System.Windows.Forms.TabPage();
            this.zPrimary = new ZedGraph.ZedGraphControl();
            this.cdmGraph = new System.Windows.Forms.TabPage();
            this.zConjunction = new ZedGraph.ZedGraphControl();
            this.cdmTable = new System.Windows.Forms.TabPage();
            this.txtCdmResults = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnSnapshot = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.secondary.SuspendLayout();
            this.primary.SuspendLayout();
            this.cdmGraph.SuspendLayout();
            this.cdmTable.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // secondary
            // 
            this.secondary.Controls.Add(this.zSecondary);
            this.secondary.Location = new System.Drawing.Point(4, 22);
            this.secondary.Name = "secondary";
            this.secondary.Padding = new System.Windows.Forms.Padding(3);
            this.secondary.Size = new System.Drawing.Size(848, 503);
            this.secondary.TabIndex = 2;
            this.secondary.Text = "Secondary";
            this.secondary.UseVisualStyleBackColor = true;
            // 
            // zSecondary
            // 
            this.zSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zSecondary.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zSecondary.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zSecondary.IsAutoScrollRange = false;
            this.zSecondary.IsEnableHEdit = false;
            this.zSecondary.IsEnableHPan = true;
            this.zSecondary.IsEnableHZoom = true;
            this.zSecondary.IsEnableVEdit = false;
            this.zSecondary.IsEnableVPan = true;
            this.zSecondary.IsEnableVZoom = true;
            this.zSecondary.IsPrintFillPage = true;
            this.zSecondary.IsPrintKeepAspectRatio = true;
            this.zSecondary.IsScrollY2 = false;
            this.zSecondary.IsShowContextMenu = true;
            this.zSecondary.IsShowCopyMessage = true;
            this.zSecondary.IsShowCursorValues = false;
            this.zSecondary.IsShowHScrollBar = false;
            this.zSecondary.IsShowPointValues = false;
            this.zSecondary.IsShowVScrollBar = false;
            this.zSecondary.IsSynchronizeXAxes = false;
            this.zSecondary.IsSynchronizeYAxes = false;
            this.zSecondary.IsZoomOnMouseCenter = false;
            this.zSecondary.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zSecondary.LinkModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zSecondary.Location = new System.Drawing.Point(3, 3);
            this.zSecondary.Name = "zSecondary";
            this.zSecondary.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zSecondary.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zSecondary.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zSecondary.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zSecondary.PointDateFormat = "g";
            this.zSecondary.PointValueFormat = "G";
            this.zSecondary.ScrollMaxX = 0D;
            this.zSecondary.ScrollMaxY = 0D;
            this.zSecondary.ScrollMaxY2 = 0D;
            this.zSecondary.ScrollMinX = 0D;
            this.zSecondary.ScrollMinY = 0D;
            this.zSecondary.ScrollMinY2 = 0D;
            this.zSecondary.Size = new System.Drawing.Size(842, 497);
            this.zSecondary.TabIndex = 4;
            this.zSecondary.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zSecondary.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zSecondary.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zSecondary.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zSecondary.ZoomStepFraction = 0.1D;
            // 
            // primary
            // 
            this.primary.Controls.Add(this.zPrimary);
            this.primary.Location = new System.Drawing.Point(4, 22);
            this.primary.Name = "primary";
            this.primary.Padding = new System.Windows.Forms.Padding(3);
            this.primary.Size = new System.Drawing.Size(848, 503);
            this.primary.TabIndex = 1;
            this.primary.Text = "Primary";
            this.primary.UseVisualStyleBackColor = true;
            // 
            // zPrimary
            // 
            this.zPrimary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zPrimary.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zPrimary.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zPrimary.IsAutoScrollRange = false;
            this.zPrimary.IsEnableHEdit = false;
            this.zPrimary.IsEnableHPan = true;
            this.zPrimary.IsEnableHZoom = true;
            this.zPrimary.IsEnableVEdit = false;
            this.zPrimary.IsEnableVPan = true;
            this.zPrimary.IsEnableVZoom = true;
            this.zPrimary.IsPrintFillPage = true;
            this.zPrimary.IsPrintKeepAspectRatio = true;
            this.zPrimary.IsScrollY2 = false;
            this.zPrimary.IsShowContextMenu = true;
            this.zPrimary.IsShowCopyMessage = true;
            this.zPrimary.IsShowCursorValues = false;
            this.zPrimary.IsShowHScrollBar = false;
            this.zPrimary.IsShowPointValues = false;
            this.zPrimary.IsShowVScrollBar = false;
            this.zPrimary.IsSynchronizeXAxes = false;
            this.zPrimary.IsSynchronizeYAxes = false;
            this.zPrimary.IsZoomOnMouseCenter = false;
            this.zPrimary.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zPrimary.LinkModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zPrimary.Location = new System.Drawing.Point(3, 3);
            this.zPrimary.Name = "zPrimary";
            this.zPrimary.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zPrimary.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zPrimary.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zPrimary.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zPrimary.PointDateFormat = "g";
            this.zPrimary.PointValueFormat = "G";
            this.zPrimary.ScrollMaxX = 0D;
            this.zPrimary.ScrollMaxY = 0D;
            this.zPrimary.ScrollMaxY2 = 0D;
            this.zPrimary.ScrollMinX = 0D;
            this.zPrimary.ScrollMinY = 0D;
            this.zPrimary.ScrollMinY2 = 0D;
            this.zPrimary.Size = new System.Drawing.Size(842, 497);
            this.zPrimary.TabIndex = 5;
            this.zPrimary.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zPrimary.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zPrimary.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zPrimary.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zPrimary.ZoomStepFraction = 0.1D;
            // 
            // cdmGraph
            // 
            this.cdmGraph.Controls.Add(this.zConjunction);
            this.cdmGraph.Location = new System.Drawing.Point(4, 22);
            this.cdmGraph.Name = "cdmGraph";
            this.cdmGraph.Padding = new System.Windows.Forms.Padding(3);
            this.cdmGraph.Size = new System.Drawing.Size(848, 503);
            this.cdmGraph.TabIndex = 0;
            this.cdmGraph.Text = "CDM Graph";
            this.cdmGraph.UseVisualStyleBackColor = true;
            // 
            // zConjunction
            // 
            this.zConjunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zConjunction.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zConjunction.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zConjunction.IsAutoScrollRange = false;
            this.zConjunction.IsEnableHEdit = false;
            this.zConjunction.IsEnableHPan = true;
            this.zConjunction.IsEnableHZoom = true;
            this.zConjunction.IsEnableVEdit = false;
            this.zConjunction.IsEnableVPan = true;
            this.zConjunction.IsEnableVZoom = true;
            this.zConjunction.IsPrintFillPage = true;
            this.zConjunction.IsPrintKeepAspectRatio = true;
            this.zConjunction.IsScrollY2 = false;
            this.zConjunction.IsShowContextMenu = true;
            this.zConjunction.IsShowCopyMessage = true;
            this.zConjunction.IsShowCursorValues = false;
            this.zConjunction.IsShowHScrollBar = false;
            this.zConjunction.IsShowPointValues = false;
            this.zConjunction.IsShowVScrollBar = false;
            this.zConjunction.IsSynchronizeXAxes = false;
            this.zConjunction.IsSynchronizeYAxes = false;
            this.zConjunction.IsZoomOnMouseCenter = false;
            this.zConjunction.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zConjunction.LinkModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zConjunction.Location = new System.Drawing.Point(3, 3);
            this.zConjunction.Name = "zConjunction";
            this.zConjunction.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zConjunction.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zConjunction.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zConjunction.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zConjunction.PointDateFormat = "g";
            this.zConjunction.PointValueFormat = "G";
            this.zConjunction.ScrollMaxX = 0D;
            this.zConjunction.ScrollMaxY = 0D;
            this.zConjunction.ScrollMaxY2 = 0D;
            this.zConjunction.ScrollMinX = 0D;
            this.zConjunction.ScrollMinY = 0D;
            this.zConjunction.ScrollMinY2 = 0D;
            this.zConjunction.Size = new System.Drawing.Size(842, 497);
            this.zConjunction.TabIndex = 5;
            this.zConjunction.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zConjunction.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zConjunction.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zConjunction.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zConjunction.ZoomStepFraction = 0.1D;
            // 
            // cdmTable
            // 
            this.cdmTable.Controls.Add(this.txtCdmResults);
            this.cdmTable.Location = new System.Drawing.Point(4, 22);
            this.cdmTable.Name = "cdmTable";
            this.cdmTable.Padding = new System.Windows.Forms.Padding(3);
            this.cdmTable.Size = new System.Drawing.Size(848, 503);
            this.cdmTable.TabIndex = 3;
            this.cdmTable.Text = "CDM Table";
            this.cdmTable.UseVisualStyleBackColor = true;
            // 
            // txtCdmResults
            // 
            this.txtCdmResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCdmResults.Font = new System.Drawing.Font("Aerial Mono", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCdmResults.Location = new System.Drawing.Point(6, 6);
            this.txtCdmResults.Multiline = true;
            this.txtCdmResults.Name = "txtCdmResults";
            this.txtCdmResults.Size = new System.Drawing.Size(836, 491);
            this.txtCdmResults.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.cdmTable);
            this.tabControl1.Controls.Add(this.cdmGraph);
            this.tabControl1.Controls.Add(this.primary);
            this.tabControl1.Controls.Add(this.secondary);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(856, 529);
            this.tabControl1.TabIndex = 5;
            // 
            // btnSnapshot
            // 
            this.btnSnapshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSnapshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSnapshot.Image = global::CDMComparison.Properties.Resources.Camera;
            this.btnSnapshot.Location = new System.Drawing.Point(836, 3);
            this.btnSnapshot.Name = "btnSnapshot";
            this.btnSnapshot.Size = new System.Drawing.Size(25, 25);
            this.btnSnapshot.TabIndex = 8;
            this.btnSnapshot.UseVisualStyleBackColor = true;
            this.btnSnapshot.Click += new System.EventHandler(this.btnSnapshot_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(716, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 21);
            this.button1.TabIndex = 9;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CdmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 553);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSnapshot);
            this.Controls.Add(this.tabControl1);
            this.Name = "CdmViewer";
            this.Text = "Individual CDM Viewer";
            this.Resize += new System.EventHandler(this.TestUI_Resize);
            this.secondary.ResumeLayout(false);
            this.primary.ResumeLayout(false);
            this.cdmGraph.ResumeLayout(false);
            this.cdmTable.ResumeLayout(false);
            this.cdmTable.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage secondary;
        private ZedGraph.ZedGraphControl zSecondary;
        private System.Windows.Forms.TabPage primary;
        private ZedGraph.ZedGraphControl zPrimary;
        private System.Windows.Forms.TabPage cdmGraph;
        private ZedGraph.ZedGraphControl zConjunction;
        private System.Windows.Forms.TabPage cdmTable;
        private System.Windows.Forms.TextBox txtCdmResults;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnSnapshot;
        private System.Windows.Forms.Button button1;
    }
}

