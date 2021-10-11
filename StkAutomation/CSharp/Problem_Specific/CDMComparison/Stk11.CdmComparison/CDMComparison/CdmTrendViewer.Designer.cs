namespace CDMComparison
{
    partial class CdmTrendViewer
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.combined = new System.Windows.Forms.TabPage();
            this.zgCombined = new ZedGraph.ZedGraphControl();
            this.missDistance = new System.Windows.Forms.TabPage();
            this.zgMissDistance = new ZedGraph.ZedGraphControl();
            this.tca = new System.Windows.Forms.TabPage();
            this.zgTCA = new ZedGraph.ZedGraphControl();
            this.probability = new System.Windows.Forms.TabPage();
            this.zgProbability = new ZedGraph.ZedGraphControl();
            this.btnSnapshot = new System.Windows.Forms.Button();
            this.sigmaDilution = new System.Windows.Forms.TabPage();
            this.zgSigma = new ZedGraph.ZedGraphControl();
            this.tabControl1.SuspendLayout();
            this.combined.SuspendLayout();
            this.missDistance.SuspendLayout();
            this.tca.SuspendLayout();
            this.probability.SuspendLayout();
            this.sigmaDilution.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.combined);
            this.tabControl1.Controls.Add(this.missDistance);
            this.tabControl1.Controls.Add(this.tca);
            this.tabControl1.Controls.Add(this.probability);
            this.tabControl1.Controls.Add(this.sigmaDilution);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(991, 459);
            this.tabControl1.TabIndex = 6;
            // 
            // combined
            // 
            this.combined.Controls.Add(this.zgCombined);
            this.combined.Location = new System.Drawing.Point(4, 22);
            this.combined.Name = "combined";
            this.combined.Padding = new System.Windows.Forms.Padding(3);
            this.combined.Size = new System.Drawing.Size(983, 433);
            this.combined.TabIndex = 3;
            this.combined.Text = "TCA and Range";
            this.combined.UseVisualStyleBackColor = true;
            // 
            // zgCombined
            // 
            this.zgCombined.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgCombined.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgCombined.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgCombined.IsAutoScrollRange = false;
            this.zgCombined.IsEnableHEdit = false;
            this.zgCombined.IsEnableHPan = true;
            this.zgCombined.IsEnableHZoom = true;
            this.zgCombined.IsEnableVEdit = false;
            this.zgCombined.IsEnableVPan = true;
            this.zgCombined.IsEnableVZoom = true;
            this.zgCombined.IsPrintFillPage = true;
            this.zgCombined.IsPrintKeepAspectRatio = true;
            this.zgCombined.IsScrollY2 = false;
            this.zgCombined.IsShowContextMenu = true;
            this.zgCombined.IsShowCopyMessage = true;
            this.zgCombined.IsShowCursorValues = false;
            this.zgCombined.IsShowHScrollBar = false;
            this.zgCombined.IsShowPointValues = false;
            this.zgCombined.IsShowVScrollBar = false;
            this.zgCombined.IsSynchronizeXAxes = false;
            this.zgCombined.IsSynchronizeYAxes = false;
            this.zgCombined.IsZoomOnMouseCenter = false;
            this.zgCombined.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgCombined.LinkModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgCombined.Location = new System.Drawing.Point(3, 3);
            this.zgCombined.Name = "zgCombined";
            this.zgCombined.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgCombined.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zgCombined.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zgCombined.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgCombined.PointDateFormat = "g";
            this.zgCombined.PointValueFormat = "G";
            this.zgCombined.ScrollMaxX = 0D;
            this.zgCombined.ScrollMaxY = 0D;
            this.zgCombined.ScrollMaxY2 = 0D;
            this.zgCombined.ScrollMinX = 0D;
            this.zgCombined.ScrollMinY = 0D;
            this.zgCombined.ScrollMinY2 = 0D;
            this.zgCombined.Size = new System.Drawing.Size(977, 427);
            this.zgCombined.TabIndex = 6;
            this.zgCombined.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgCombined.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zgCombined.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zgCombined.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgCombined.ZoomStepFraction = 0.1D;
            // 
            // missDistance
            // 
            this.missDistance.Controls.Add(this.zgMissDistance);
            this.missDistance.Location = new System.Drawing.Point(4, 22);
            this.missDistance.Name = "missDistance";
            this.missDistance.Padding = new System.Windows.Forms.Padding(3);
            this.missDistance.Size = new System.Drawing.Size(983, 433);
            this.missDistance.TabIndex = 0;
            this.missDistance.Text = "Miss Distance";
            this.missDistance.UseVisualStyleBackColor = true;
            // 
            // zgMissDistance
            // 
            this.zgMissDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgMissDistance.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgMissDistance.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgMissDistance.IsAutoScrollRange = false;
            this.zgMissDistance.IsEnableHEdit = false;
            this.zgMissDistance.IsEnableHPan = true;
            this.zgMissDistance.IsEnableHZoom = true;
            this.zgMissDistance.IsEnableVEdit = false;
            this.zgMissDistance.IsEnableVPan = true;
            this.zgMissDistance.IsEnableVZoom = true;
            this.zgMissDistance.IsPrintFillPage = true;
            this.zgMissDistance.IsPrintKeepAspectRatio = true;
            this.zgMissDistance.IsScrollY2 = false;
            this.zgMissDistance.IsShowContextMenu = true;
            this.zgMissDistance.IsShowCopyMessage = true;
            this.zgMissDistance.IsShowCursorValues = false;
            this.zgMissDistance.IsShowHScrollBar = false;
            this.zgMissDistance.IsShowPointValues = false;
            this.zgMissDistance.IsShowVScrollBar = false;
            this.zgMissDistance.IsSynchronizeXAxes = false;
            this.zgMissDistance.IsSynchronizeYAxes = false;
            this.zgMissDistance.IsZoomOnMouseCenter = false;
            this.zgMissDistance.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgMissDistance.LinkModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgMissDistance.Location = new System.Drawing.Point(3, 3);
            this.zgMissDistance.Name = "zgMissDistance";
            this.zgMissDistance.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgMissDistance.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zgMissDistance.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zgMissDistance.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgMissDistance.PointDateFormat = "g";
            this.zgMissDistance.PointValueFormat = "G";
            this.zgMissDistance.ScrollMaxX = 0D;
            this.zgMissDistance.ScrollMaxY = 0D;
            this.zgMissDistance.ScrollMaxY2 = 0D;
            this.zgMissDistance.ScrollMinX = 0D;
            this.zgMissDistance.ScrollMinY = 0D;
            this.zgMissDistance.ScrollMinY2 = 0D;
            this.zgMissDistance.Size = new System.Drawing.Size(977, 427);
            this.zgMissDistance.TabIndex = 5;
            this.zgMissDistance.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgMissDistance.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zgMissDistance.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zgMissDistance.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgMissDistance.ZoomStepFraction = 0.1D;
            // 
            // tca
            // 
            this.tca.Controls.Add(this.zgTCA);
            this.tca.Location = new System.Drawing.Point(4, 22);
            this.tca.Name = "tca";
            this.tca.Padding = new System.Windows.Forms.Padding(3);
            this.tca.Size = new System.Drawing.Size(983, 433);
            this.tca.TabIndex = 1;
            this.tca.Text = "TCA";
            this.tca.UseVisualStyleBackColor = true;
            // 
            // zgTCA
            // 
            this.zgTCA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgTCA.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgTCA.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgTCA.IsAutoScrollRange = false;
            this.zgTCA.IsEnableHEdit = false;
            this.zgTCA.IsEnableHPan = true;
            this.zgTCA.IsEnableHZoom = true;
            this.zgTCA.IsEnableVEdit = false;
            this.zgTCA.IsEnableVPan = true;
            this.zgTCA.IsEnableVZoom = true;
            this.zgTCA.IsPrintFillPage = true;
            this.zgTCA.IsPrintKeepAspectRatio = true;
            this.zgTCA.IsScrollY2 = false;
            this.zgTCA.IsShowContextMenu = true;
            this.zgTCA.IsShowCopyMessage = true;
            this.zgTCA.IsShowCursorValues = false;
            this.zgTCA.IsShowHScrollBar = false;
            this.zgTCA.IsShowPointValues = false;
            this.zgTCA.IsShowVScrollBar = false;
            this.zgTCA.IsSynchronizeXAxes = false;
            this.zgTCA.IsSynchronizeYAxes = false;
            this.zgTCA.IsZoomOnMouseCenter = false;
            this.zgTCA.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgTCA.LinkModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgTCA.Location = new System.Drawing.Point(3, 3);
            this.zgTCA.Name = "zgTCA";
            this.zgTCA.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgTCA.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zgTCA.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zgTCA.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgTCA.PointDateFormat = "g";
            this.zgTCA.PointValueFormat = "G";
            this.zgTCA.ScrollMaxX = 0D;
            this.zgTCA.ScrollMaxY = 0D;
            this.zgTCA.ScrollMaxY2 = 0D;
            this.zgTCA.ScrollMinX = 0D;
            this.zgTCA.ScrollMinY = 0D;
            this.zgTCA.ScrollMinY2 = 0D;
            this.zgTCA.Size = new System.Drawing.Size(977, 427);
            this.zgTCA.TabIndex = 5;
            this.zgTCA.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgTCA.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zgTCA.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zgTCA.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgTCA.ZoomStepFraction = 0.1D;
            // 
            // probability
            // 
            this.probability.Controls.Add(this.zgProbability);
            this.probability.Location = new System.Drawing.Point(4, 22);
            this.probability.Name = "probability";
            this.probability.Padding = new System.Windows.Forms.Padding(3);
            this.probability.Size = new System.Drawing.Size(983, 433);
            this.probability.TabIndex = 2;
            this.probability.Text = "Probability";
            this.probability.UseVisualStyleBackColor = true;
            // 
            // zgProbability
            // 
            this.zgProbability.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgProbability.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgProbability.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgProbability.IsAutoScrollRange = false;
            this.zgProbability.IsEnableHEdit = false;
            this.zgProbability.IsEnableHPan = true;
            this.zgProbability.IsEnableHZoom = true;
            this.zgProbability.IsEnableVEdit = false;
            this.zgProbability.IsEnableVPan = true;
            this.zgProbability.IsEnableVZoom = true;
            this.zgProbability.IsPrintFillPage = true;
            this.zgProbability.IsPrintKeepAspectRatio = true;
            this.zgProbability.IsScrollY2 = false;
            this.zgProbability.IsShowContextMenu = true;
            this.zgProbability.IsShowCopyMessage = true;
            this.zgProbability.IsShowCursorValues = false;
            this.zgProbability.IsShowHScrollBar = false;
            this.zgProbability.IsShowPointValues = false;
            this.zgProbability.IsShowVScrollBar = false;
            this.zgProbability.IsSynchronizeXAxes = false;
            this.zgProbability.IsSynchronizeYAxes = false;
            this.zgProbability.IsZoomOnMouseCenter = false;
            this.zgProbability.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgProbability.LinkModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgProbability.Location = new System.Drawing.Point(3, 3);
            this.zgProbability.Name = "zgProbability";
            this.zgProbability.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgProbability.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zgProbability.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zgProbability.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgProbability.PointDateFormat = "g";
            this.zgProbability.PointValueFormat = "G";
            this.zgProbability.ScrollMaxX = 0D;
            this.zgProbability.ScrollMaxY = 0D;
            this.zgProbability.ScrollMaxY2 = 0D;
            this.zgProbability.ScrollMinX = 0D;
            this.zgProbability.ScrollMinY = 0D;
            this.zgProbability.ScrollMinY2 = 0D;
            this.zgProbability.Size = new System.Drawing.Size(977, 427);
            this.zgProbability.TabIndex = 4;
            this.zgProbability.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgProbability.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zgProbability.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zgProbability.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgProbability.ZoomStepFraction = 0.1D;
            // 
            // btnSnapshot
            // 
            this.btnSnapshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSnapshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSnapshot.Image = global::CDMComparison.Properties.Resources.Camera;
            this.btnSnapshot.Location = new System.Drawing.Point(974, 3);
            this.btnSnapshot.Name = "btnSnapshot";
            this.btnSnapshot.Size = new System.Drawing.Size(25, 25);
            this.btnSnapshot.TabIndex = 7;
            this.btnSnapshot.UseVisualStyleBackColor = true;
            this.btnSnapshot.Click += new System.EventHandler(this.btnSnapshot_Click);
            // 
            // sigmaDilution
            // 
            this.sigmaDilution.Controls.Add(this.zgSigma);
            this.sigmaDilution.Location = new System.Drawing.Point(4, 22);
            this.sigmaDilution.Name = "sigmaDilution";
            this.sigmaDilution.Padding = new System.Windows.Forms.Padding(3);
            this.sigmaDilution.Size = new System.Drawing.Size(983, 433);
            this.sigmaDilution.TabIndex = 4;
            this.sigmaDilution.Text = "Sigma Dilution";
            this.sigmaDilution.UseVisualStyleBackColor = true;
            // 
            // zgSigma
            // 
            this.zgSigma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgSigma.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgSigma.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgSigma.IsAutoScrollRange = false;
            this.zgSigma.IsEnableHEdit = false;
            this.zgSigma.IsEnableHPan = true;
            this.zgSigma.IsEnableHZoom = true;
            this.zgSigma.IsEnableVEdit = false;
            this.zgSigma.IsEnableVPan = true;
            this.zgSigma.IsEnableVZoom = true;
            this.zgSigma.IsPrintFillPage = true;
            this.zgSigma.IsPrintKeepAspectRatio = true;
            this.zgSigma.IsScrollY2 = false;
            this.zgSigma.IsShowContextMenu = true;
            this.zgSigma.IsShowCopyMessage = true;
            this.zgSigma.IsShowCursorValues = false;
            this.zgSigma.IsShowHScrollBar = false;
            this.zgSigma.IsShowPointValues = false;
            this.zgSigma.IsShowVScrollBar = false;
            this.zgSigma.IsSynchronizeXAxes = false;
            this.zgSigma.IsSynchronizeYAxes = false;
            this.zgSigma.IsZoomOnMouseCenter = false;
            this.zgSigma.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgSigma.LinkModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgSigma.Location = new System.Drawing.Point(3, 3);
            this.zgSigma.Name = "zgSigma";
            this.zgSigma.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgSigma.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zgSigma.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zgSigma.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgSigma.PointDateFormat = "g";
            this.zgSigma.PointValueFormat = "G";
            this.zgSigma.ScrollMaxX = 0D;
            this.zgSigma.ScrollMaxY = 0D;
            this.zgSigma.ScrollMaxY2 = 0D;
            this.zgSigma.ScrollMinX = 0D;
            this.zgSigma.ScrollMinY = 0D;
            this.zgSigma.ScrollMinY2 = 0D;
            this.zgSigma.Size = new System.Drawing.Size(977, 427);
            this.zgSigma.TabIndex = 5;
            this.zgSigma.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgSigma.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zgSigma.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zgSigma.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgSigma.ZoomStepFraction = 0.1D;
            // 
            // CdmTrendViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 483);
            this.Controls.Add(this.btnSnapshot);
            this.Controls.Add(this.tabControl1);
            this.Name = "CdmTrendViewer";
            this.Text = "CDM Trend";
            this.Load += new System.EventHandler(this.CdmTrendViewer_Load);
            this.tabControl1.ResumeLayout(false);
            this.combined.ResumeLayout(false);
            this.missDistance.ResumeLayout(false);
            this.tca.ResumeLayout(false);
            this.probability.ResumeLayout(false);
            this.sigmaDilution.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage missDistance;
        private ZedGraph.ZedGraphControl zgMissDistance;
        private System.Windows.Forms.TabPage tca;
        private ZedGraph.ZedGraphControl zgTCA;
        private System.Windows.Forms.TabPage probability;
        private ZedGraph.ZedGraphControl zgProbability;
        private System.Windows.Forms.TabPage combined;
        private ZedGraph.ZedGraphControl zgCombined;
        private System.Windows.Forms.Button btnSnapshot;
        private System.Windows.Forms.TabPage sigmaDilution;
        private ZedGraph.ZedGraphControl zgSigma;
    }
}