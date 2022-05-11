
namespace Stk12.UiPlugin.HorizonsEphemImporter
{
    partial class ImporterGUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btSearch = new System.Windows.Forms.Button();
            this.txtBodySearch = new System.Windows.Forms.TextBox();
            this.lblTgtBody = new System.Windows.Forms.Label();
            this.lblBodyInfo = new System.Windows.Forms.Label();
            this.lblMinStart = new System.Windows.Forms.Label();
            this.lblMaxStop = new System.Windows.Forms.Label();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.comboBoxMultiBody = new System.Windows.Forms.ComboBox();
            this.comboBoxStep = new System.Windows.Forms.ComboBox();
            this.lblStepSize = new System.Windows.Forms.Label();
            this.grpBoxImport = new System.Windows.Forms.GroupBox();
            this.txtSTKObjName = new System.Windows.Forms.TextBox();
            this.lblSTKObj = new System.Windows.Forms.Label();
            this.btReset = new System.Windows.Forms.Button();
            this.lblStopTime = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.txtStopTime = new System.Windows.Forms.TextBox();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.btImport = new System.Windows.Forms.Button();
            this.btSaveDialog = new System.Windows.Forms.Button();
            this.txtSaveDir = new System.Windows.Forms.TextBox();
            this.lblSaveDir = new System.Windows.Forms.Label();
            this.lblValidStep = new System.Windows.Forms.Label();
            this.txtStep = new System.Windows.Forms.TextBox();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxSearch.SuspendLayout();
            this.grpBoxImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(206, 41);
            this.btSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(93, 21);
            this.btSearch.TabIndex = 1;
            this.btSearch.Text = "Search Body";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // txtBodySearch
            // 
            this.txtBodySearch.Location = new System.Drawing.Point(92, 17);
            this.txtBodySearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtBodySearch.Name = "txtBodySearch";
            this.txtBodySearch.Size = new System.Drawing.Size(207, 20);
            this.txtBodySearch.TabIndex = 0;
            this.txtBodySearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBodySearch_KeyDown);
            // 
            // lblTgtBody
            // 
            this.lblTgtBody.AutoSize = true;
            this.lblTgtBody.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTgtBody.Location = new System.Drawing.Point(7, 20);
            this.lblTgtBody.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTgtBody.Name = "lblTgtBody";
            this.lblTgtBody.Size = new System.Drawing.Size(68, 13);
            this.lblTgtBody.TabIndex = 2;
            this.lblTgtBody.Text = "Target Body:";
            // 
            // lblBodyInfo
            // 
            this.lblBodyInfo.AutoSize = true;
            this.lblBodyInfo.Location = new System.Drawing.Point(7, 65);
            this.lblBodyInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBodyInfo.Name = "lblBodyInfo";
            this.lblBodyInfo.Size = new System.Drawing.Size(34, 13);
            this.lblBodyInfo.TabIndex = 4;
            this.lblBodyInfo.Text = "Body:";
            // 
            // lblMinStart
            // 
            this.lblMinStart.AutoSize = true;
            this.lblMinStart.Location = new System.Drawing.Point(7, 88);
            this.lblMinStart.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMinStart.Name = "lblMinStart";
            this.lblMinStart.Size = new System.Drawing.Size(78, 13);
            this.lblMinStart.TabIndex = 5;
            this.lblMinStart.Text = "Min Start Time:";
            // 
            // lblMaxStop
            // 
            this.lblMaxStop.AutoSize = true;
            this.lblMaxStop.Location = new System.Drawing.Point(7, 113);
            this.lblMaxStop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxStop.Name = "lblMaxStop";
            this.lblMaxStop.Size = new System.Drawing.Size(81, 13);
            this.lblMaxStop.TabIndex = 6;
            this.lblMaxStop.Text = "Max Stop Time:";
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Controls.Add(this.comboBoxMultiBody);
            this.groupBoxSearch.Controls.Add(this.lblMaxStop);
            this.groupBoxSearch.Controls.Add(this.lblMinStart);
            this.groupBoxSearch.Controls.Add(this.lblBodyInfo);
            this.groupBoxSearch.Controls.Add(this.lblTgtBody);
            this.groupBoxSearch.Controls.Add(this.txtBodySearch);
            this.groupBoxSearch.Controls.Add(this.btSearch);
            this.groupBoxSearch.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(308, 139);
            this.groupBoxSearch.TabIndex = 7;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "Body Lookup";
            // 
            // comboBoxMultiBody
            // 
            this.comboBoxMultiBody.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMultiBody.Enabled = false;
            this.comboBoxMultiBody.FormattingEnabled = true;
            this.comboBoxMultiBody.Location = new System.Drawing.Point(92, 17);
            this.comboBoxMultiBody.Name = "comboBoxMultiBody";
            this.comboBoxMultiBody.Size = new System.Drawing.Size(207, 21);
            this.comboBoxMultiBody.TabIndex = 18;
            this.comboBoxMultiBody.Visible = false;
            // 
            // comboBoxStep
            // 
            this.comboBoxStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStep.Enabled = false;
            this.comboBoxStep.FormattingEnabled = true;
            this.comboBoxStep.Items.AddRange(new object[] {
            "minutes",
            "days",
            "hours",
            "months",
            "years",
            "intervals (unitless)"});
            this.comboBoxStep.Location = new System.Drawing.Point(181, 70);
            this.comboBoxStep.Name = "comboBoxStep";
            this.comboBoxStep.Size = new System.Drawing.Size(118, 21);
            this.comboBoxStep.TabIndex = 3;
            // 
            // lblStepSize
            // 
            this.lblStepSize.AutoSize = true;
            this.lblStepSize.Location = new System.Drawing.Point(7, 74);
            this.lblStepSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStepSize.Name = "lblStepSize";
            this.lblStepSize.Size = new System.Drawing.Size(55, 13);
            this.lblStepSize.TabIndex = 7;
            this.lblStepSize.Text = "Step Size:";
            // 
            // grpBoxImport
            // 
            this.grpBoxImport.Controls.Add(this.txtSTKObjName);
            this.grpBoxImport.Controls.Add(this.lblSTKObj);
            this.grpBoxImport.Controls.Add(this.btReset);
            this.grpBoxImport.Controls.Add(this.lblStopTime);
            this.grpBoxImport.Controls.Add(this.lblStartTime);
            this.grpBoxImport.Controls.Add(this.txtStopTime);
            this.grpBoxImport.Controls.Add(this.txtStartTime);
            this.grpBoxImport.Controls.Add(this.btImport);
            this.grpBoxImport.Controls.Add(this.btSaveDialog);
            this.grpBoxImport.Controls.Add(this.txtSaveDir);
            this.grpBoxImport.Controls.Add(this.lblSaveDir);
            this.grpBoxImport.Controls.Add(this.lblValidStep);
            this.grpBoxImport.Controls.Add(this.txtStep);
            this.grpBoxImport.Controls.Add(this.comboBoxStep);
            this.grpBoxImport.Controls.Add(this.lblStepSize);
            this.grpBoxImport.Location = new System.Drawing.Point(3, 148);
            this.grpBoxImport.Name = "grpBoxImport";
            this.grpBoxImport.Size = new System.Drawing.Size(308, 193);
            this.grpBoxImport.TabIndex = 9;
            this.grpBoxImport.TabStop = false;
            this.grpBoxImport.Text = "Ephemeris Import";
            // 
            // txtSTKObjName
            // 
            this.txtSTKObjName.Enabled = false;
            this.txtSTKObjName.Location = new System.Drawing.Point(92, 95);
            this.txtSTKObjName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSTKObjName.Name = "txtSTKObjName";
            this.txtSTKObjName.Size = new System.Drawing.Size(207, 20);
            this.txtSTKObjName.TabIndex = 5;
            // 
            // lblSTKObj
            // 
            this.lblSTKObj.AutoSize = true;
            this.lblSTKObj.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSTKObj.Location = new System.Drawing.Point(8, 98);
            this.lblSTKObj.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSTKObj.Name = "lblSTKObj";
            this.lblSTKObj.Size = new System.Drawing.Size(84, 13);
            this.lblSTKObj.TabIndex = 17;
            this.lblSTKObj.Text = "STK Obj. Name:";
            // 
            // btReset
            // 
            this.btReset.Location = new System.Drawing.Point(10, 167);
            this.btReset.Margin = new System.Windows.Forms.Padding(2);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(93, 21);
            this.btReset.TabIndex = 8;
            this.btReset.Text = "Reset Form";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // lblStopTime
            // 
            this.lblStopTime.AutoSize = true;
            this.lblStopTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStopTime.Location = new System.Drawing.Point(7, 48);
            this.lblStopTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStopTime.Name = "lblStopTime";
            this.lblStopTime.Size = new System.Drawing.Size(83, 13);
            this.lblStopTime.TabIndex = 15;
            this.lblStopTime.Text = "Stop Time TDB:";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStartTime.Location = new System.Drawing.Point(7, 21);
            this.lblStartTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(83, 13);
            this.lblStartTime.TabIndex = 7;
            this.lblStartTime.Text = "Start Time TDB:";
            // 
            // txtStopTime
            // 
            this.txtStopTime.Enabled = false;
            this.txtStopTime.Location = new System.Drawing.Point(92, 45);
            this.txtStopTime.Margin = new System.Windows.Forms.Padding(2);
            this.txtStopTime.Name = "txtStopTime";
            this.txtStopTime.Size = new System.Drawing.Size(207, 20);
            this.txtStopTime.TabIndex = 1;
            // 
            // txtStartTime
            // 
            this.txtStartTime.Enabled = false;
            this.txtStartTime.Location = new System.Drawing.Point(92, 18);
            this.txtStartTime.Margin = new System.Windows.Forms.Padding(2);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(207, 20);
            this.txtStartTime.TabIndex = 0;
            // 
            // btImport
            // 
            this.btImport.Enabled = false;
            this.btImport.Location = new System.Drawing.Point(206, 167);
            this.btImport.Margin = new System.Windows.Forms.Padding(2);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(93, 21);
            this.btImport.TabIndex = 7;
            this.btImport.Text = "Import";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // btSaveDialog
            // 
            this.btSaveDialog.Enabled = false;
            this.btSaveDialog.Location = new System.Drawing.Point(275, 120);
            this.btSaveDialog.Name = "btSaveDialog";
            this.btSaveDialog.Size = new System.Drawing.Size(24, 19);
            this.btSaveDialog.TabIndex = 6;
            this.btSaveDialog.Text = "...";
            this.btSaveDialog.UseVisualStyleBackColor = true;
            this.btSaveDialog.Click += new System.EventHandler(this.btSaveDialog_Click);
            // 
            // txtSaveDir
            // 
            this.txtSaveDir.Location = new System.Drawing.Point(92, 119);
            this.txtSaveDir.Margin = new System.Windows.Forms.Padding(2);
            this.txtSaveDir.Name = "txtSaveDir";
            this.txtSaveDir.ReadOnly = true;
            this.txtSaveDir.Size = new System.Drawing.Size(178, 20);
            this.txtSaveDir.TabIndex = 7;
            // 
            // lblSaveDir
            // 
            this.lblSaveDir.AutoSize = true;
            this.lblSaveDir.Location = new System.Drawing.Point(8, 123);
            this.lblSaveDir.Name = "lblSaveDir";
            this.lblSaveDir.Size = new System.Drawing.Size(80, 13);
            this.lblSaveDir.TabIndex = 10;
            this.lblSaveDir.Text = "Save Directory:";
            // 
            // lblValidStep
            // 
            this.lblValidStep.AutoSize = true;
            this.lblValidStep.ForeColor = System.Drawing.Color.Crimson;
            this.lblValidStep.Location = new System.Drawing.Point(60, 146);
            this.lblValidStep.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblValidStep.Name = "lblValidStep";
            this.lblValidStep.Size = new System.Drawing.Size(0, 13);
            this.lblValidStep.TabIndex = 9;
            // 
            // txtStep
            // 
            this.txtStep.Enabled = false;
            this.txtStep.Location = new System.Drawing.Point(92, 71);
            this.txtStep.Margin = new System.Windows.Forms.Padding(2);
            this.txtStep.Name = "txtStep";
            this.txtStep.Size = new System.Drawing.Size(80, 20);
            this.txtStep.TabIndex = 2;
            this.txtStep.Text = "1";
            this.txtStep.TextChanged += new System.EventHandler(this.txtStep_TextChanged);
            // 
            // saveDialog
            // 
            this.saveDialog.DefaultExt = "e";
            this.saveDialog.DereferenceLinks = false;
            this.saveDialog.InitialDirectory = "Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)";
            // 
            // ImporterGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.grpBoxImport);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ImporterGUI";
            this.Size = new System.Drawing.Size(314, 347);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.grpBoxImport.ResumeLayout(false);
            this.grpBoxImport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.TextBox txtBodySearch;
        private System.Windows.Forms.Label lblTgtBody;
        private System.Windows.Forms.Label lblBodyInfo;
        private System.Windows.Forms.Label lblMinStart;
        private System.Windows.Forms.Label lblMaxStop;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.ComboBox comboBoxStep;
        private System.Windows.Forms.Label lblStepSize;
        private System.Windows.Forms.GroupBox grpBoxImport;
        private System.Windows.Forms.TextBox txtStep;
        private System.Windows.Forms.Label lblValidStep;
        private System.Windows.Forms.Button btSaveDialog;
        private System.Windows.Forms.TextBox txtSaveDir;
        private System.Windows.Forms.Label lblSaveDir;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.Button btImport;
        private System.Windows.Forms.Label lblStopTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.TextBox txtStopTime;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.TextBox txtSTKObjName;
        private System.Windows.Forms.Label lblSTKObj;
        private System.Windows.Forms.ComboBox comboBoxMultiBody;
    }
}
