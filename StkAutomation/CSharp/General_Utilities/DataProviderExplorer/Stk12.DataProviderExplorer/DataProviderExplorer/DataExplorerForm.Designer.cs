namespace DataProviderExplorer
{
    partial class DataExplorerForm
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
            this.comboBox_stkObjects = new System.Windows.Forms.ComboBox();
            this.comboBox_DataProviders = new System.Windows.Forms.ComboBox();
            this.comboBox_Elements = new System.Windows.Forms.ComboBox();
            this.comboBox_Groups = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.button_GetData = new System.Windows.Forms.Button();
            this.btnGetAnsysData = new System.Windows.Forms.Button();
            this.txtStepSize = new System.Windows.Forms.TextBox();
            this.lblStepSize = new System.Windows.Forms.Label();
            this.txtRenameParameter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_stkObjects
            // 
            this.comboBox_stkObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_stkObjects.FormattingEnabled = true;
            this.comboBox_stkObjects.Location = new System.Drawing.Point(12, 28);
            this.comboBox_stkObjects.Name = "comboBox_stkObjects";
            this.comboBox_stkObjects.Size = new System.Drawing.Size(471, 21);
            this.comboBox_stkObjects.TabIndex = 0;
            this.comboBox_stkObjects.SelectedIndexChanged += new System.EventHandler(this.comboBox_stkObjects_SelectedIndexChanged);
            // 
            // comboBox_DataProviders
            // 
            this.comboBox_DataProviders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_DataProviders.FormattingEnabled = true;
            this.comboBox_DataProviders.Location = new System.Drawing.Point(12, 68);
            this.comboBox_DataProviders.Name = "comboBox_DataProviders";
            this.comboBox_DataProviders.Size = new System.Drawing.Size(344, 21);
            this.comboBox_DataProviders.TabIndex = 1;
            this.comboBox_DataProviders.SelectedIndexChanged += new System.EventHandler(this.comboBox_DataProviders_SelectedIndexChanged);
            // 
            // comboBox_Elements
            // 
            this.comboBox_Elements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Elements.FormattingEnabled = true;
            this.comboBox_Elements.Location = new System.Drawing.Point(12, 108);
            this.comboBox_Elements.Name = "comboBox_Elements";
            this.comboBox_Elements.Size = new System.Drawing.Size(344, 21);
            this.comboBox_Elements.TabIndex = 2;
            this.comboBox_Elements.SelectedIndexChanged += new System.EventHandler(this.comboBox_Elements_SelectedIndexChanged);
            // 
            // comboBox_Groups
            // 
            this.comboBox_Groups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Groups.FormattingEnabled = true;
            this.comboBox_Groups.Location = new System.Drawing.Point(362, 68);
            this.comboBox_Groups.Name = "comboBox_Groups";
            this.comboBox_Groups.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Groups.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "STK Object";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Data Providers";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(359, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Group";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Data Elements";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 178);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(471, 103);
            this.dataGridView1.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Data";
            // 
            // button_GetData
            // 
            this.button_GetData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_GetData.Location = new System.Drawing.Point(362, 106);
            this.button_GetData.Name = "button_GetData";
            this.button_GetData.Size = new System.Drawing.Size(121, 23);
            this.button_GetData.TabIndex = 10;
            this.button_GetData.Text = "Get Data";
            this.button_GetData.UseVisualStyleBackColor = true;
            this.button_GetData.Click += new System.EventHandler(this.button_GetData_Click);
            // 
            // btnGetAnsysData
            // 
            this.btnGetAnsysData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetAnsysData.Location = new System.Drawing.Point(12, 287);
            this.btnGetAnsysData.Name = "btnGetAnsysData";
            this.btnGetAnsysData.Size = new System.Drawing.Size(121, 23);
            this.btnGetAnsysData.TabIndex = 11;
            this.btnGetAnsysData.Text = "Get Ansys Data";
            this.btnGetAnsysData.UseVisualStyleBackColor = true;
            this.btnGetAnsysData.Click += new System.EventHandler(this.btnGetAnsysData_Click);
            // 
            // txtStepSize
            // 
            this.txtStepSize.Location = new System.Drawing.Point(95, 137);
            this.txtStepSize.Name = "txtStepSize";
            this.txtStepSize.Size = new System.Drawing.Size(74, 20);
            this.txtStepSize.TabIndex = 12;
            this.txtStepSize.Text = "60";
            this.txtStepSize.TextChanged += new System.EventHandler(this.txtStepSize_TextChanged);
            // 
            // lblStepSize
            // 
            this.lblStepSize.AutoSize = true;
            this.lblStepSize.Location = new System.Drawing.Point(11, 140);
            this.lblStepSize.Name = "lblStepSize";
            this.lblStepSize.Size = new System.Drawing.Size(78, 13);
            this.lblStepSize.TabIndex = 13;
            this.lblStepSize.Text = "Step Size (sec)";
            // 
            // txtRenameParameter
            // 
            this.txtRenameParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRenameParameter.Location = new System.Drawing.Point(225, 289);
            this.txtRenameParameter.Name = "txtRenameParameter";
            this.txtRenameParameter.Size = new System.Drawing.Size(258, 20);
            this.txtRenameParameter.TabIndex = 14;
            this.txtRenameParameter.Text = "Heat Flux";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(139, 292);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Rename Param";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 322);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRenameParameter);
            this.Controls.Add(this.lblStepSize);
            this.Controls.Add(this.txtStepSize);
            this.Controls.Add(this.btnGetAnsysData);
            this.Controls.Add(this.button_GetData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_Groups);
            this.Controls.Add(this.comboBox_Elements);
            this.Controls.Add(this.comboBox_DataProviders);
            this.Controls.Add(this.comboBox_stkObjects);
            this.MinimumSize = new System.Drawing.Size(380, 310);
            this.Name = "Form1";
            this.Text = "Execute Data Providers";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_stkObjects;
        private System.Windows.Forms.ComboBox comboBox_DataProviders;
        private System.Windows.Forms.ComboBox comboBox_Elements;
        private System.Windows.Forms.ComboBox comboBox_Groups;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_GetData;
        private System.Windows.Forms.Button btnGetAnsysData;
        private System.Windows.Forms.TextBox txtStepSize;
        private System.Windows.Forms.Label lblStepSize;
        private System.Windows.Forms.TextBox txtRenameParameter;
        private System.Windows.Forms.Label label6;
    }
}

