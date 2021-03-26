namespace Stk12.UiPlugin.ObjectModelTutorial
{
    partial class CustomUserInterface
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
            this.cbAccessFrom = new System.Windows.Forms.ComboBox();
            this.AccessFrom = new System.Windows.Forms.Label();
            this.AccessTo = new System.Windows.Forms.Label();
            this.cbAccessTo = new System.Windows.Forms.ComboBox();
            this.lblStkObjects = new System.Windows.Forms.Label();
            this.computeAccess = new System.Windows.Forms.Button();
            this.animate = new System.Windows.Forms.Button();
            this.pause = new System.Windows.Forms.Button();
            this.reset = new System.Windows.Forms.Button();
            this.createFacility = new System.Windows.Forms.Button();
            this.facilityName = new System.Windows.Forms.TextBox();
            this.facilityNameLabel = new System.Windows.Forms.Label();
            this.latLabel = new System.Windows.Forms.Label();
            this.latTb = new System.Windows.Forms.TextBox();
            this.lonLabel = new System.Windows.Forms.Label();
            this.lonTb = new System.Windows.Forms.TextBox();
            this.altLabel = new System.Windows.Forms.Label();
            this.altTb = new System.Windows.Forms.TextBox();
            this.scenarioControlsLabel = new System.Windows.Forms.Label();
            this.addObjectsLabel = new System.Windows.Forms.Label();
            this.createSensorButton = new System.Windows.Forms.Button();
            this.addSatelliteButton = new System.Windows.Forms.Button();
            this.tleNumberTb = new System.Windows.Forms.TextBox();
            this.tleNumberLabel = new System.Windows.Forms.Label();
            this.dataGridViewAccess = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Az = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.El = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Range = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createAircraft = new System.Windows.Forms.Button();
            this.addLLAWaypointBtn = new System.Windows.Forms.Button();
            this.createVectorButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccess)).BeginInit();
            this.SuspendLayout();
            // 
            // cbAccessFrom
            // 
            this.cbAccessFrom.FormattingEnabled = true;
            this.cbAccessFrom.Location = new System.Drawing.Point(46, 303);
            this.cbAccessFrom.Name = "cbAccessFrom";
            this.cbAccessFrom.Size = new System.Drawing.Size(174, 21);
            this.cbAccessFrom.TabIndex = 2;
            // 
            // AccessFrom
            // 
            this.AccessFrom.AutoSize = true;
            this.AccessFrom.Location = new System.Drawing.Point(8, 303);
            this.AccessFrom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AccessFrom.Name = "AccessFrom";
            this.AccessFrom.Size = new System.Drawing.Size(33, 13);
            this.AccessFrom.TabIndex = 3;
            this.AccessFrom.Text = "From:";
            // 
            // AccessTo
            // 
            this.AccessTo.AutoSize = true;
            this.AccessTo.Location = new System.Drawing.Point(8, 329);
            this.AccessTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AccessTo.Name = "AccessTo";
            this.AccessTo.Size = new System.Drawing.Size(23, 13);
            this.AccessTo.TabIndex = 4;
            this.AccessTo.Text = "To:";
            // 
            // cbAccessTo
            // 
            this.cbAccessTo.FormattingEnabled = true;
            this.cbAccessTo.Location = new System.Drawing.Point(46, 329);
            this.cbAccessTo.Name = "cbAccessTo";
            this.cbAccessTo.Size = new System.Drawing.Size(174, 21);
            this.cbAccessTo.TabIndex = 5;
            // 
            // lblStkObjects
            // 
            this.lblStkObjects.AutoSize = true;
            this.lblStkObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStkObjects.Location = new System.Drawing.Point(63, 275);
            this.lblStkObjects.Name = "lblStkObjects";
            this.lblStkObjects.Size = new System.Drawing.Size(270, 20);
            this.lblStkObjects.TabIndex = 1;
            this.lblStkObjects.Text = "Access Calculations and VGT Vector";
            // 
            // computeAccess
            // 
            this.computeAccess.Location = new System.Drawing.Point(224, 303);
            this.computeAccess.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.computeAccess.Name = "computeAccess";
            this.computeAccess.Size = new System.Drawing.Size(60, 47);
            this.computeAccess.TabIndex = 6;
            this.computeAccess.Text = "Compute Access";
            this.computeAccess.UseVisualStyleBackColor = true;
            this.computeAccess.Click += new System.EventHandler(this.computeAccess_Click);
            // 
            // animate
            // 
            this.animate.Location = new System.Drawing.Point(78, 36);
            this.animate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.animate.Name = "animate";
            this.animate.Size = new System.Drawing.Size(72, 26);
            this.animate.TabIndex = 8;
            this.animate.Text = "Animate";
            this.animate.UseVisualStyleBackColor = true;
            this.animate.Click += new System.EventHandler(this.animate_Click);
            // 
            // pause
            // 
            this.pause.Location = new System.Drawing.Point(154, 36);
            this.pause.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(71, 26);
            this.pause.TabIndex = 9;
            this.pause.Text = "Pause";
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(230, 36);
            this.reset.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(68, 26);
            this.reset.TabIndex = 10;
            this.reset.Text = "Reset";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // createFacility
            // 
            this.createFacility.Location = new System.Drawing.Point(164, 105);
            this.createFacility.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createFacility.Name = "createFacility";
            this.createFacility.Size = new System.Drawing.Size(84, 49);
            this.createFacility.TabIndex = 11;
            this.createFacility.Text = "Create Facility at LLA";
            this.createFacility.UseVisualStyleBackColor = true;
            this.createFacility.Click += new System.EventHandler(this.createFacility_Click);
            // 
            // facilityName
            // 
            this.facilityName.Location = new System.Drawing.Point(84, 105);
            this.facilityName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.facilityName.Name = "facilityName";
            this.facilityName.Size = new System.Drawing.Size(76, 20);
            this.facilityName.TabIndex = 12;
            this.facilityName.Text = "MyFacility";
            // 
            // facilityNameLabel
            // 
            this.facilityNameLabel.AutoSize = true;
            this.facilityNameLabel.Location = new System.Drawing.Point(8, 105);
            this.facilityNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.facilityNameLabel.Name = "facilityNameLabel";
            this.facilityNameLabel.Size = new System.Drawing.Size(73, 13);
            this.facilityNameLabel.TabIndex = 13;
            this.facilityNameLabel.Text = "Facility Name:";
            this.facilityNameLabel.Click += new System.EventHandler(this.facilityNameLabel_Click);
            // 
            // latLabel
            // 
            this.latLabel.AutoSize = true;
            this.latLabel.Location = new System.Drawing.Point(8, 132);
            this.latLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.latLabel.Name = "latLabel";
            this.latLabel.Size = new System.Drawing.Size(25, 13);
            this.latLabel.TabIndex = 15;
            this.latLabel.Text = "Lat:";
            // 
            // latTb
            // 
            this.latTb.Location = new System.Drawing.Point(84, 132);
            this.latTb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.latTb.Name = "latTb";
            this.latTb.Size = new System.Drawing.Size(76, 20);
            this.latTb.TabIndex = 14;
            this.latTb.Text = "34.0";
            // 
            // lonLabel
            // 
            this.lonLabel.AutoSize = true;
            this.lonLabel.Location = new System.Drawing.Point(8, 158);
            this.lonLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lonLabel.Name = "lonLabel";
            this.lonLabel.Size = new System.Drawing.Size(28, 13);
            this.lonLabel.TabIndex = 17;
            this.lonLabel.Text = "Lon:";
            // 
            // lonTb
            // 
            this.lonTb.Location = new System.Drawing.Point(84, 158);
            this.lonTb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lonTb.Name = "lonTb";
            this.lonTb.Size = new System.Drawing.Size(76, 20);
            this.lonTb.TabIndex = 16;
            this.lonTb.Text = "-118.2";
            this.lonTb.TextChanged += new System.EventHandler(this.lonTb_TextChanged);
            // 
            // altLabel
            // 
            this.altLabel.AutoSize = true;
            this.altLabel.Location = new System.Drawing.Point(8, 180);
            this.altLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.altLabel.Name = "altLabel";
            this.altLabel.Size = new System.Drawing.Size(22, 13);
            this.altLabel.TabIndex = 19;
            this.altLabel.Text = "Alt:";
            // 
            // altTb
            // 
            this.altTb.Location = new System.Drawing.Point(84, 180);
            this.altTb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.altTb.Name = "altTb";
            this.altTb.Size = new System.Drawing.Size(76, 20);
            this.altTb.TabIndex = 18;
            this.altTb.Text = "0.0";
            // 
            // scenarioControlsLabel
            // 
            this.scenarioControlsLabel.AutoSize = true;
            this.scenarioControlsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scenarioControlsLabel.Location = new System.Drawing.Point(86, 8);
            this.scenarioControlsLabel.Name = "scenarioControlsLabel";
            this.scenarioControlsLabel.Size = new System.Drawing.Size(231, 20);
            this.scenarioControlsLabel.TabIndex = 20;
            this.scenarioControlsLabel.Text = "Scenario Controls with Connect";
            // 
            // addObjectsLabel
            // 
            this.addObjectsLabel.AutoSize = true;
            this.addObjectsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addObjectsLabel.Location = new System.Drawing.Point(107, 74);
            this.addObjectsLabel.Name = "addObjectsLabel";
            this.addObjectsLabel.Size = new System.Drawing.Size(177, 20);
            this.addObjectsLabel.TabIndex = 21;
            this.addObjectsLabel.Text = "Add and Modify Objects";
            // 
            // createSensorButton
            // 
            this.createSensorButton.Location = new System.Drawing.Point(252, 105);
            this.createSensorButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createSensorButton.Name = "createSensorButton";
            this.createSensorButton.Size = new System.Drawing.Size(82, 49);
            this.createSensorButton.TabIndex = 22;
            this.createSensorButton.Text = "Create Sensor on Facility";
            this.createSensorButton.UseVisualStyleBackColor = true;
            this.createSensorButton.Click += new System.EventHandler(this.createSensorButton_Click);
            // 
            // addSatelliteButton
            // 
            this.addSatelliteButton.Location = new System.Drawing.Point(164, 217);
            this.addSatelliteButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.addSatelliteButton.Name = "addSatelliteButton";
            this.addSatelliteButton.Size = new System.Drawing.Size(84, 43);
            this.addSatelliteButton.TabIndex = 23;
            this.addSatelliteButton.Text = "Add TLE Satellite";
            this.addSatelliteButton.UseVisualStyleBackColor = true;
            this.addSatelliteButton.Click += new System.EventHandler(this.addSatelliteButton_Click);
            // 
            // tleNumberTb
            // 
            this.tleNumberTb.Location = new System.Drawing.Point(84, 229);
            this.tleNumberTb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tleNumberTb.Name = "tleNumberTb";
            this.tleNumberTb.Size = new System.Drawing.Size(76, 20);
            this.tleNumberTb.TabIndex = 24;
            this.tleNumberTb.Text = "25544";
            // 
            // tleNumberLabel
            // 
            this.tleNumberLabel.AutoSize = true;
            this.tleNumberLabel.Location = new System.Drawing.Point(8, 229);
            this.tleNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tleNumberLabel.Name = "tleNumberLabel";
            this.tleNumberLabel.Size = new System.Drawing.Size(70, 13);
            this.tleNumberLabel.TabIndex = 25;
            this.tleNumberLabel.Text = "TLE Number:";
            // 
            // dataGridViewAccess
            // 
            this.dataGridViewAccess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAccess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Az,
            this.El,
            this.Range});
            this.dataGridViewAccess.Location = new System.Drawing.Point(17, 366);
            this.dataGridViewAccess.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridViewAccess.Name = "dataGridViewAccess";
            this.dataGridViewAccess.RowHeadersWidth = 51;
            this.dataGridViewAccess.RowTemplate.Height = 24;
            this.dataGridViewAccess.Size = new System.Drawing.Size(362, 160);
            this.dataGridViewAccess.TabIndex = 26;
            this.dataGridViewAccess.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAccess_CellContentClick);
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.MinimumWidth = 6;
            this.Time.Name = "Time";
            this.Time.Width = 125;
            // 
            // Az
            // 
            this.Az.HeaderText = "Az";
            this.Az.MinimumWidth = 6;
            this.Az.Name = "Az";
            this.Az.Width = 75;
            // 
            // El
            // 
            this.El.HeaderText = "El";
            this.El.MinimumWidth = 6;
            this.El.Name = "El";
            this.El.Width = 75;
            // 
            // Range
            // 
            this.Range.HeaderText = "Range";
            this.Range.MinimumWidth = 6;
            this.Range.Name = "Range";
            this.Range.Width = 75;
            // 
            // createAircraft
            // 
            this.createAircraft.Location = new System.Drawing.Point(164, 158);
            this.createAircraft.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createAircraft.Name = "createAircraft";
            this.createAircraft.Size = new System.Drawing.Size(84, 43);
            this.createAircraft.TabIndex = 27;
            this.createAircraft.Text = "Create Aircraft";
            this.createAircraft.UseVisualStyleBackColor = true;
            this.createAircraft.Click += new System.EventHandler(this.createAircraft_Click);
            // 
            // addLLAWaypointBtn
            // 
            this.addLLAWaypointBtn.Location = new System.Drawing.Point(252, 158);
            this.addLLAWaypointBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.addLLAWaypointBtn.Name = "addLLAWaypointBtn";
            this.addLLAWaypointBtn.Size = new System.Drawing.Size(82, 44);
            this.addLLAWaypointBtn.TabIndex = 28;
            this.addLLAWaypointBtn.Text = "Add LLA Waypoint";
            this.addLLAWaypointBtn.UseVisualStyleBackColor = true;
            this.addLLAWaypointBtn.Click += new System.EventHandler(this.addLLAWaypointBtn_Click);
            // 
            // createVectorButton
            // 
            this.createVectorButton.Location = new System.Drawing.Point(288, 303);
            this.createVectorButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createVectorButton.Name = "createVectorButton";
            this.createVectorButton.Size = new System.Drawing.Size(91, 47);
            this.createVectorButton.TabIndex = 29;
            this.createVectorButton.Text = "Create Displacement Vector";
            this.createVectorButton.UseVisualStyleBackColor = true;
            this.createVectorButton.Click += new System.EventHandler(this.createVectorButton_Click);
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.createVectorButton);
            this.Controls.Add(this.addLLAWaypointBtn);
            this.Controls.Add(this.createAircraft);
            this.Controls.Add(this.dataGridViewAccess);
            this.Controls.Add(this.tleNumberLabel);
            this.Controls.Add(this.tleNumberTb);
            this.Controls.Add(this.addSatelliteButton);
            this.Controls.Add(this.createSensorButton);
            this.Controls.Add(this.addObjectsLabel);
            this.Controls.Add(this.scenarioControlsLabel);
            this.Controls.Add(this.altLabel);
            this.Controls.Add(this.altTb);
            this.Controls.Add(this.lonLabel);
            this.Controls.Add(this.lonTb);
            this.Controls.Add(this.latLabel);
            this.Controls.Add(this.latTb);
            this.Controls.Add(this.facilityNameLabel);
            this.Controls.Add(this.facilityName);
            this.Controls.Add(this.createFacility);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.animate);
            this.Controls.Add(this.computeAccess);
            this.Controls.Add(this.cbAccessTo);
            this.Controls.Add(this.AccessTo);
            this.Controls.Add(this.AccessFrom);
            this.Controls.Add(this.cbAccessFrom);
            this.Controls.Add(this.lblStkObjects);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(463, 683);
            this.Load += new System.EventHandler(this.CustomUserInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbAccessFrom;
        private System.Windows.Forms.Label AccessFrom;
        private System.Windows.Forms.Label AccessTo;
        private System.Windows.Forms.ComboBox cbAccessTo;
        private System.Windows.Forms.Label lblStkObjects;
        private System.Windows.Forms.Button computeAccess;
        private System.Windows.Forms.Button animate;
        private System.Windows.Forms.Button pause;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button createFacility;
        private System.Windows.Forms.TextBox facilityName;
        private System.Windows.Forms.Label facilityNameLabel;
        private System.Windows.Forms.Label latLabel;
        private System.Windows.Forms.TextBox latTb;
        private System.Windows.Forms.Label lonLabel;
        private System.Windows.Forms.TextBox lonTb;
        private System.Windows.Forms.Label altLabel;
        private System.Windows.Forms.TextBox altTb;
        private System.Windows.Forms.Label scenarioControlsLabel;
        private System.Windows.Forms.Label addObjectsLabel;
        private System.Windows.Forms.Button createSensorButton;
        private System.Windows.Forms.Button addSatelliteButton;
        private System.Windows.Forms.TextBox tleNumberTb;
        private System.Windows.Forms.Label tleNumberLabel;
        private System.Windows.Forms.DataGridView dataGridViewAccess;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Az;
        private System.Windows.Forms.DataGridViewTextBoxColumn El;
        private System.Windows.Forms.DataGridViewTextBoxColumn Range;
        private System.Windows.Forms.Button createAircraft;
        private System.Windows.Forms.Button addLLAWaypointBtn;
        private System.Windows.Forms.Button createVectorButton;
    }
}
