namespace Stk12.UiPlugin.TetkExample
{
    partial class LoadDataUserInterface
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_ImportDataMappings = new System.Windows.Forms.Button();
            this.button_LoadOwnship = new System.Windows.Forms.Button();
            this.button_LoadAdditionalData = new System.Windows.Forms.Button();
            this.button_LoadAssociatedState = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_ImportDataMappings);
            this.groupBox1.Controls.Add(this.button_LoadOwnship);
            this.groupBox1.Controls.Add(this.button_LoadAdditionalData);
            this.groupBox1.Controls.Add(this.button_LoadAssociatedState);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 318);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load Data Files:";
            // 
            // button_ImportDataMappings
            // 
            this.button_ImportDataMappings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ImportDataMappings.Location = new System.Drawing.Point(7, 29);
            this.button_ImportDataMappings.Name = "button_ImportDataMappings";
            this.button_ImportDataMappings.Size = new System.Drawing.Size(153, 64);
            this.button_ImportDataMappings.TabIndex = 3;
            this.button_ImportDataMappings.Text = "Import Data Mappings";
            this.button_ImportDataMappings.UseVisualStyleBackColor = true;
            this.button_ImportDataMappings.Click += new System.EventHandler(this.button_ImportDataMappings_Click);
            // 
            // button_LoadOwnship
            // 
            this.button_LoadOwnship.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_LoadOwnship.Location = new System.Drawing.Point(7, 99);
            this.button_LoadOwnship.Name = "button_LoadOwnship";
            this.button_LoadOwnship.Size = new System.Drawing.Size(153, 64);
            this.button_LoadOwnship.TabIndex = 2;
            this.button_LoadOwnship.Text = "Load Ownship";
            this.button_LoadOwnship.UseVisualStyleBackColor = true;
            this.button_LoadOwnship.Click += new System.EventHandler(this.button_LoadOwnship_Click);
            // 
            // button_LoadAdditionalData
            // 
            this.button_LoadAdditionalData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_LoadAdditionalData.Location = new System.Drawing.Point(7, 239);
            this.button_LoadAdditionalData.Name = "button_LoadAdditionalData";
            this.button_LoadAdditionalData.Size = new System.Drawing.Size(153, 64);
            this.button_LoadAdditionalData.TabIndex = 1;
            this.button_LoadAdditionalData.Text = "Load Additional Data";
            this.button_LoadAdditionalData.UseVisualStyleBackColor = true;
            this.button_LoadAdditionalData.Click += new System.EventHandler(this.button_LoadAdditionalData_Click);
            // 
            // button_LoadAssociatedState
            // 
            this.button_LoadAssociatedState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_LoadAssociatedState.Location = new System.Drawing.Point(7, 169);
            this.button_LoadAssociatedState.Name = "button_LoadAssociatedState";
            this.button_LoadAssociatedState.Size = new System.Drawing.Size(153, 64);
            this.button_LoadAssociatedState.TabIndex = 0;
            this.button_LoadAssociatedState.Text = "Load Associated States";
            this.button_LoadAssociatedState.UseVisualStyleBackColor = true;
            this.button_LoadAssociatedState.Click += new System.EventHandler(this.button_LoadAssociatedState_Click);
            // 
            // LoadDataUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "LoadDataUserInterface";
            this.Size = new System.Drawing.Size(383, 387);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_LoadAdditionalData;
        private System.Windows.Forms.Button button_LoadAssociatedState;
        private System.Windows.Forms.Button button_LoadOwnship;
        private System.Windows.Forms.Button button_ImportDataMappings;
    }
}
