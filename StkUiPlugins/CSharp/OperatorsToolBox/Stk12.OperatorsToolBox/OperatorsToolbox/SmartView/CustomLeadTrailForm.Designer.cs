namespace OperatorsToolbox.SmartView
{
    partial class CustomLeadTrailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomLeadTrailForm));
            this.ObjectGrid = new System.Windows.Forms.DataGridView();
            this.Modify = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ObjectNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeadType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TrailType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Frame = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.VvlhTarget = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Apply = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectGrid)).BeginInit();
            this.RightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ObjectGrid
            // 
            this.ObjectGrid.AllowUserToAddRows = false;
            this.ObjectGrid.AllowUserToDeleteRows = false;
            this.ObjectGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectGrid.BackgroundColor = System.Drawing.Color.DimGray;
            this.ObjectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ObjectGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Modify,
            this.ObjectNames,
            this.LeadType,
            this.TrailType,
            this.Frame,
            this.VvlhTarget});
            this.ObjectGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ObjectGrid.Location = new System.Drawing.Point(11, 11);
            this.ObjectGrid.Margin = new System.Windows.Forms.Padding(2);
            this.ObjectGrid.MultiSelect = false;
            this.ObjectGrid.Name = "ObjectGrid";
            this.ObjectGrid.RowHeadersVisible = false;
            this.ObjectGrid.RowTemplate.Height = 24;
            this.ObjectGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ObjectGrid.Size = new System.Drawing.Size(733, 300);
            this.ObjectGrid.TabIndex = 41;
            this.ObjectGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ObjectGrid_CellMouseUp);
            // 
            // Modify
            // 
            this.Modify.HeaderText = "Modify";
            this.Modify.Name = "Modify";
            this.Modify.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Modify.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Modify.Width = 50;
            // 
            // ObjectNames
            // 
            this.ObjectNames.HeaderText = "Object";
            this.ObjectNames.Name = "ObjectNames";
            this.ObjectNames.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ObjectNames.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ObjectNames.Width = 200;
            // 
            // LeadType
            // 
            this.LeadType.HeaderText = "Lead Type";
            this.LeadType.Name = "LeadType";
            this.LeadType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LeadType.Width = 120;
            // 
            // TrailType
            // 
            this.TrailType.HeaderText = "Trail Type";
            this.TrailType.Name = "TrailType";
            this.TrailType.Width = 120;
            // 
            // Frame
            // 
            this.Frame.HeaderText = "Frame";
            this.Frame.Name = "Frame";
            this.Frame.Width = 120;
            // 
            // VvlhTarget
            // 
            this.VvlhTarget.HeaderText = "VVLH Ref";
            this.VvlhTarget.Name = "VvlhTarget";
            this.VvlhTarget.Width = 120;
            // 
            // Apply
            // 
            this.Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Apply.BackColor = System.Drawing.Color.SteelBlue;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Apply.Location = new System.Drawing.Point(11, 315);
            this.Apply.Margin = new System.Windows.Forms.Padding(2);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(82, 28);
            this.Apply.TabIndex = 42;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cancel.BackColor = System.Drawing.Color.SteelBlue;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.Location = new System.Drawing.Point(112, 315);
            this.Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 28);
            this.Cancel.TabIndex = 43;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.RightClickMenu.Name = "RightClickMenu";
            this.RightClickMenu.Size = new System.Drawing.Size(166, 26);
            this.RightClickMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.RightClickMenu_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItem1.Text = "Apply to Column";
            // 
            // CustomLeadTrailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(755, 351);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.ObjectGrid);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomLeadTrailForm";
            this.Text = "Custom Lead Trail";
            ((System.ComponentModel.ISupportInitialize)(this.ObjectGrid)).EndInit();
            this.RightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ObjectGrid;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Modify;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectNames;
        private System.Windows.Forms.DataGridViewComboBoxColumn LeadType;
        private System.Windows.Forms.DataGridViewComboBoxColumn TrailType;
        private System.Windows.Forms.DataGridViewComboBoxColumn Frame;
        private System.Windows.Forms.DataGridViewComboBoxColumn VvlhTarget;
        private System.Windows.Forms.ContextMenuStrip RightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}