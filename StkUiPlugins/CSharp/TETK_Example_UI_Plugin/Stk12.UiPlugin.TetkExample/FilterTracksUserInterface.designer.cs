namespace Stk12.UiPlugin.TetkExample
{
    partial class CreateTracksUserInterface
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox_TrackID = new System.Windows.Forms.ComboBox();
            this.groupBox_AddFilteredTracks = new System.Windows.Forms.GroupBox();
            this.button_AddAllTracks = new System.Windows.Forms.Button();
            this.button_AddIndividualTrack = new System.Windows.Forms.Button();
            this.button_AddRawTrack = new System.Windows.Forms.Button();
            this.groupBox_PromoteTrack = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button_PromoteTrack = new System.Windows.Forms.Button();
            this.comboBox_TracksToPromote = new System.Windows.Forms.ComboBox();
            this.groupBox_AddFilteredTracks.SuspendLayout();
            this.groupBox_PromoteTrack.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(130, 13);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Select Track ID to Filter By:";
            // 
            // comboBox_TrackID
            // 
            this.comboBox_TrackID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_TrackID.Items.AddRange(new object[] {
            "101",
            "106",
            "111",
            "116",
            "201",
            "211",
            "216",
            "221",
            "226",
            "301",
            "306",
            "311",
            "316",
            "321",
            "401",
            "406",
            "411",
            "416",
            "421",
            "501",
            "601",
            "606",
            "611",
            "616",
            "701",
            "706",
            "711",
            "716",
            "721",
            "801",
            "806",
            "901",
            "906",
            "911",
            "916",
            "921",
            "926",
            "1001",
            "1006",
            "1011"});
            this.comboBox_TrackID.Location = new System.Drawing.Point(6, 38);
            this.comboBox_TrackID.Name = "comboBox_TrackID";
            this.comboBox_TrackID.Size = new System.Drawing.Size(175, 21);
            this.comboBox_TrackID.TabIndex = 1;
            // 
            // groupBox_AddFilteredTracks
            // 
            this.groupBox_AddFilteredTracks.Controls.Add(this.button_AddAllTracks);
            this.groupBox_AddFilteredTracks.Controls.Add(this.button_AddIndividualTrack);
            this.groupBox_AddFilteredTracks.Controls.Add(this.comboBox_TrackID);
            this.groupBox_AddFilteredTracks.Controls.Add(this.textBox1);
            this.groupBox_AddFilteredTracks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_AddFilteredTracks.Location = new System.Drawing.Point(13, 73);
            this.groupBox_AddFilteredTracks.Name = "groupBox_AddFilteredTracks";
            this.groupBox_AddFilteredTracks.Size = new System.Drawing.Size(360, 117);
            this.groupBox_AddFilteredTracks.TabIndex = 2;
            this.groupBox_AddFilteredTracks.TabStop = false;
            this.groupBox_AddFilteredTracks.Text = "Add Filtered Tracks";
            // 
            // button_AddAllTracks
            // 
            this.button_AddAllTracks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_AddAllTracks.Location = new System.Drawing.Point(201, 71);
            this.button_AddAllTracks.Name = "button_AddAllTracks";
            this.button_AddAllTracks.Size = new System.Drawing.Size(144, 40);
            this.button_AddAllTracks.TabIndex = 3;
            this.button_AddAllTracks.Text = "Add All Tracks";
            this.button_AddAllTracks.UseVisualStyleBackColor = true;
            this.button_AddAllTracks.Click += new System.EventHandler(this.button_AddAllTracks_Click);
            // 
            // button_AddIndividualTrack
            // 
            this.button_AddIndividualTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_AddIndividualTrack.Location = new System.Drawing.Point(201, 27);
            this.button_AddIndividualTrack.Name = "button_AddIndividualTrack";
            this.button_AddIndividualTrack.Size = new System.Drawing.Size(144, 40);
            this.button_AddIndividualTrack.TabIndex = 2;
            this.button_AddIndividualTrack.Text = "Add Individual Track";
            this.button_AddIndividualTrack.UseVisualStyleBackColor = true;
            this.button_AddIndividualTrack.Click += new System.EventHandler(this.button_AddIndividualTrack_Click);
            // 
            // button_AddRawTrack
            // 
            this.button_AddRawTrack.Location = new System.Drawing.Point(13, 16);
            this.button_AddRawTrack.Name = "button_AddRawTrack";
            this.button_AddRawTrack.Size = new System.Drawing.Size(119, 41);
            this.button_AddRawTrack.TabIndex = 3;
            this.button_AddRawTrack.Text = "Add Raw Track";
            this.button_AddRawTrack.UseVisualStyleBackColor = true;
            this.button_AddRawTrack.Click += new System.EventHandler(this.button_AddRawTrack_Click);
            // 
            // groupBox_PromoteTrack
            // 
            this.groupBox_PromoteTrack.Controls.Add(this.textBox2);
            this.groupBox_PromoteTrack.Controls.Add(this.button_PromoteTrack);
            this.groupBox_PromoteTrack.Controls.Add(this.comboBox_TracksToPromote);
            this.groupBox_PromoteTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_PromoteTrack.Location = new System.Drawing.Point(13, 200);
            this.groupBox_PromoteTrack.Name = "groupBox_PromoteTrack";
            this.groupBox_PromoteTrack.Size = new System.Drawing.Size(360, 87);
            this.groupBox_PromoteTrack.TabIndex = 4;
            this.groupBox_PromoteTrack.TabStop = false;
            this.groupBox_PromoteTrack.Text = "Promote Tracks";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(6, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(130, 13);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "Select Track to Promote:";
            this.textBox2.WordWrap = false;
            // 
            // button_PromoteTrack
            // 
            this.button_PromoteTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_PromoteTrack.Location = new System.Drawing.Point(201, 22);
            this.button_PromoteTrack.Name = "button_PromoteTrack";
            this.button_PromoteTrack.Size = new System.Drawing.Size(143, 40);
            this.button_PromoteTrack.TabIndex = 2;
            this.button_PromoteTrack.Text = "Promote Individual Track";
            this.button_PromoteTrack.UseVisualStyleBackColor = true;
            this.button_PromoteTrack.Click += new System.EventHandler(this.button_PromoteTrack_Click);
            // 
            // comboBox_TracksToPromote
            // 
            this.comboBox_TracksToPromote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_TracksToPromote.FormattingEnabled = true;
            this.comboBox_TracksToPromote.Location = new System.Drawing.Point(6, 41);
            this.comboBox_TracksToPromote.Name = "comboBox_TracksToPromote";
            this.comboBox_TracksToPromote.Size = new System.Drawing.Size(175, 21);
            this.comboBox_TracksToPromote.TabIndex = 0;
            // 
            // CreateTracksUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_PromoteTrack);
            this.Controls.Add(this.button_AddRawTrack);
            this.Controls.Add(this.groupBox_AddFilteredTracks);
            this.Name = "CreateTracksUserInterface";
            this.Size = new System.Drawing.Size(390, 288);
            this.groupBox_AddFilteredTracks.ResumeLayout(false);
            this.groupBox_AddFilteredTracks.PerformLayout();
            this.groupBox_PromoteTrack.ResumeLayout(false);
            this.groupBox_PromoteTrack.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox_TrackID;
        private System.Windows.Forms.GroupBox groupBox_AddFilteredTracks;
        private System.Windows.Forms.Button button_AddIndividualTrack;
        private System.Windows.Forms.Button button_AddRawTrack;
        private System.Windows.Forms.Button button_AddAllTracks;
        private System.Windows.Forms.GroupBox groupBox_PromoteTrack;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button_PromoteTrack;
        private System.Windows.Forms.ComboBox comboBox_TracksToPromote;
    }
}
