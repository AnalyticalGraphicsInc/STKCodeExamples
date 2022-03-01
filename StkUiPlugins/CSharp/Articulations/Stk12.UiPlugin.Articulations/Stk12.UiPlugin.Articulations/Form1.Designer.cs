namespace Stk12.UiPlugin.Articulations
{
    partial class Form1
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
            this.PossibleArtic = new System.Windows.Forms.ComboBox();
            this.CreatedArtic = new System.Windows.Forms.ListBox();
            this.Remove = new System.Windows.Forms.Button();
            this.RemoveAll = new System.Windows.Forms.Button();
            this.AddArtic = new System.Windows.Forms.Button();
            this.LoadArticFile = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PossibleArtic
            // 
            this.PossibleArtic.FormattingEnabled = true;
            this.PossibleArtic.Location = new System.Drawing.Point(12, 86);
            this.PossibleArtic.Name = "PossibleArtic";
            this.PossibleArtic.Size = new System.Drawing.Size(285, 24);
            this.PossibleArtic.TabIndex = 0;
            this.PossibleArtic.SelectedIndexChanged += new System.EventHandler(this.PossibleArtic_SelectedIndexChanged);
            // 
            // CreatedArtic
            // 
            this.CreatedArtic.FormattingEnabled = true;
            this.CreatedArtic.ItemHeight = 16;
            this.CreatedArtic.Location = new System.Drawing.Point(324, 86);
            this.CreatedArtic.Name = "CreatedArtic";
            this.CreatedArtic.Size = new System.Drawing.Size(397, 292);
            this.CreatedArtic.TabIndex = 1;
            this.CreatedArtic.SelectedIndexChanged += new System.EventHandler(this.CreatedArtic_SelectedIndexChanged);
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(739, 129);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(121, 30);
            this.Remove.TabIndex = 2;
            this.Remove.Text = "Remove";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // RemoveAll
            // 
            this.RemoveAll.Location = new System.Drawing.Point(739, 183);
            this.RemoveAll.Name = "RemoveAll";
            this.RemoveAll.Size = new System.Drawing.Size(120, 30);
            this.RemoveAll.TabIndex = 3;
            this.RemoveAll.Text = "Remove All";
            this.RemoveAll.UseVisualStyleBackColor = true;
            this.RemoveAll.Click += new System.EventHandler(this.RemoveAll_Click);
            // 
            // AddArtic
            // 
            this.AddArtic.Location = new System.Drawing.Point(89, 128);
            this.AddArtic.Name = "AddArtic";
            this.AddArtic.Size = new System.Drawing.Size(134, 33);
            this.AddArtic.TabIndex = 4;
            this.AddArtic.Text = "Add";
            this.AddArtic.UseVisualStyleBackColor = true;
            this.AddArtic.Click += new System.EventHandler(this.AddArtic_Click);
            // 
            // LoadArticFile
            // 
            this.LoadArticFile.Location = new System.Drawing.Point(209, 435);
            this.LoadArticFile.Name = "LoadArticFile";
            this.LoadArticFile.Size = new System.Drawing.Size(175, 37);
            this.LoadArticFile.TabIndex = 5;
            this.LoadArticFile.Text = "Load Articulation File";
            this.LoadArticFile.UseVisualStyleBackColor = true;
            this.LoadArticFile.Click += new System.EventHandler(this.LoadArticFile_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(415, 435);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(175, 37);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Choose Articulation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(422, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Created Articulations";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 484);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.LoadArticFile);
            this.Controls.Add(this.AddArtic);
            this.Controls.Add(this.RemoveAll);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.CreatedArtic);
            this.Controls.Add(this.PossibleArtic);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PossibleArtic;
        private System.Windows.Forms.ListBox CreatedArtic;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button RemoveAll;
        private System.Windows.Forms.Button AddArtic;
        private System.Windows.Forms.Button LoadArticFile;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}