<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomUserInterface
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpReentryParams = New System.Windows.Forms.GroupBox()
        Me.btnReentryParamHelp = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtVelocity = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAzimuth = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtHorizFPA = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAltitude = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtLongitude = New System.Windows.Forms.TextBox()
        Me.txtLatitude = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.grpSpacecraftParams = New System.Windows.Forms.GroupBox()
        Me.btnSpacecraftParamHelp = New System.Windows.Forms.Button()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtNoseRadius = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtArea = New System.Windows.Forms.TextBox()
        Me.txtMass = New System.Windows.Forms.TextBox()
        Me.txtCd = New System.Windows.Forms.TextBox()
        Me.cboAtmosphereModel = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnCreateSat = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtStartTime = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtStopTime = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnGLoadReport = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btnGLoadGraph = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnHeatingReport = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.btnHeatingGraph = New System.Windows.Forms.Button()
        Me.grpReentryParams.SuspendLayout()
        Me.grpSpacecraftParams.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(13, 11)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(246, 17)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Spacecraft Reentry Heating Tool"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Latitude:"
        Me.ToolTip1.SetToolTip(Me.Label2, "Reentry Latitude")
        '
        'grpReentryParams
        '
        Me.grpReentryParams.Controls.Add(Me.btnReentryParamHelp)
        Me.grpReentryParams.Controls.Add(Me.Label12)
        Me.grpReentryParams.Controls.Add(Me.Label11)
        Me.grpReentryParams.Controls.Add(Me.Label10)
        Me.grpReentryParams.Controls.Add(Me.Label9)
        Me.grpReentryParams.Controls.Add(Me.Label8)
        Me.grpReentryParams.Controls.Add(Me.Label7)
        Me.grpReentryParams.Controls.Add(Me.txtVelocity)
        Me.grpReentryParams.Controls.Add(Me.Label6)
        Me.grpReentryParams.Controls.Add(Me.txtAzimuth)
        Me.grpReentryParams.Controls.Add(Me.Label5)
        Me.grpReentryParams.Controls.Add(Me.txtHorizFPA)
        Me.grpReentryParams.Controls.Add(Me.Label4)
        Me.grpReentryParams.Controls.Add(Me.txtAltitude)
        Me.grpReentryParams.Controls.Add(Me.Label3)
        Me.grpReentryParams.Controls.Add(Me.txtLongitude)
        Me.grpReentryParams.Controls.Add(Me.txtLatitude)
        Me.grpReentryParams.Controls.Add(Me.Label1)
        Me.grpReentryParams.Controls.Add(Me.Label2)
        Me.grpReentryParams.Location = New System.Drawing.Point(11, 6)
        Me.grpReentryParams.Name = "grpReentryParams"
        Me.grpReentryParams.Size = New System.Drawing.Size(243, 190)
        Me.grpReentryParams.TabIndex = 3
        Me.grpReentryParams.TabStop = False
        Me.grpReentryParams.Text = "Reentry Parameters"
        '
        'btnReentryParamHelp
        '
        Me.btnReentryParamHelp.Location = New System.Drawing.Point(209, 155)
        Me.btnReentryParamHelp.Name = "btnReentryParamHelp"
        Me.btnReentryParamHelp.Size = New System.Drawing.Size(26, 23)
        Me.btnReentryParamHelp.TabIndex = 14
        Me.btnReentryParamHelp.Text = "?"
        Me.btnReentryParamHelp.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(153, 158)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(49, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "(km/sec)"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(153, 132)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(33, 13)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "(Deg)"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(153, 106)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 13)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "(Deg)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(153, 80)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(27, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "(km)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(153, 54)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "(Deg)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(153, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "(Deg)"
        '
        'txtVelocity
        '
        Me.txtVelocity.Location = New System.Drawing.Point(79, 155)
        Me.txtVelocity.Name = "txtVelocity"
        Me.txtVelocity.Size = New System.Drawing.Size(68, 20)
        Me.txtVelocity.TabIndex = 13
        Me.txtVelocity.Text = "7.80"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 158)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Velocity:"
        Me.ToolTip1.SetToolTip(Me.Label6, "Reentry Velocity")
        '
        'txtAzimuth
        '
        Me.txtAzimuth.Location = New System.Drawing.Point(79, 129)
        Me.txtAzimuth.Name = "txtAzimuth"
        Me.txtAzimuth.Size = New System.Drawing.Size(68, 20)
        Me.txtAzimuth.TabIndex = 11
        Me.txtAzimuth.Text = "45.0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 132)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Azimuth:"
        Me.ToolTip1.SetToolTip(Me.Label5, "Reentry Azimuth")
        '
        'txtHorizFPA
        '
        Me.txtHorizFPA.Location = New System.Drawing.Point(79, 103)
        Me.txtHorizFPA.Name = "txtHorizFPA"
        Me.txtHorizFPA.Size = New System.Drawing.Size(68, 20)
        Me.txtHorizFPA.TabIndex = 9
        Me.txtHorizFPA.Text = "-3.5"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Horiz FPA:"
        Me.ToolTip1.SetToolTip(Me.Label4, "Horizontal Flight Path Angle")
        '
        'txtAltitude
        '
        Me.txtAltitude.Location = New System.Drawing.Point(79, 77)
        Me.txtAltitude.Name = "txtAltitude"
        Me.txtAltitude.Size = New System.Drawing.Size(68, 20)
        Me.txtAltitude.TabIndex = 7
        Me.txtAltitude.Text = "300"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Altitude:"
        Me.ToolTip1.SetToolTip(Me.Label3, "Reentry Altitude")
        '
        'txtLongitude
        '
        Me.txtLongitude.Location = New System.Drawing.Point(79, 51)
        Me.txtLongitude.Name = "txtLongitude"
        Me.txtLongitude.Size = New System.Drawing.Size(68, 20)
        Me.txtLongitude.TabIndex = 5
        Me.txtLongitude.Text = "0.00"
        '
        'txtLatitude
        '
        Me.txtLatitude.Location = New System.Drawing.Point(79, 25)
        Me.txtLatitude.Name = "txtLatitude"
        Me.txtLatitude.Size = New System.Drawing.Size(68, 20)
        Me.txtLatitude.TabIndex = 4
        Me.txtLatitude.Text = "0.00"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Longitude:"
        Me.ToolTip1.SetToolTip(Me.Label1, "Reentry Longitude")
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(4, 40)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(71, 13)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "Atmos Model:"
        Me.ToolTip1.SetToolTip(Me.Label13, "Atmospheric Density Model")
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(48, 67)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(23, 13)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Cd:"
        Me.ToolTip1.SetToolTip(Me.Label14, "Coefficient of Drag")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(36, 93)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(35, 13)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "Mass:"
        Me.ToolTip1.SetToolTip(Me.Label15, "Spacecraft Mass")
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(39, 119)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(32, 13)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "Area:"
        Me.ToolTip1.SetToolTip(Me.Label16, "Incident Surface Area")
        '
        'grpSpacecraftParams
        '
        Me.grpSpacecraftParams.Controls.Add(Me.btnSpacecraftParamHelp)
        Me.grpSpacecraftParams.Controls.Add(Me.Label21)
        Me.grpSpacecraftParams.Controls.Add(Me.Label20)
        Me.grpSpacecraftParams.Controls.Add(Me.txtNoseRadius)
        Me.grpSpacecraftParams.Controls.Add(Me.Label18)
        Me.grpSpacecraftParams.Controls.Add(Me.Label17)
        Me.grpSpacecraftParams.Controls.Add(Me.Label16)
        Me.grpSpacecraftParams.Controls.Add(Me.txtArea)
        Me.grpSpacecraftParams.Controls.Add(Me.Label15)
        Me.grpSpacecraftParams.Controls.Add(Me.txtMass)
        Me.grpSpacecraftParams.Controls.Add(Me.txtCd)
        Me.grpSpacecraftParams.Controls.Add(Me.Label14)
        Me.grpSpacecraftParams.Controls.Add(Me.Label13)
        Me.grpSpacecraftParams.Controls.Add(Me.cboAtmosphereModel)
        Me.grpSpacecraftParams.Location = New System.Drawing.Point(11, 202)
        Me.grpSpacecraftParams.Name = "grpSpacecraftParams"
        Me.grpSpacecraftParams.Size = New System.Drawing.Size(241, 186)
        Me.grpSpacecraftParams.TabIndex = 4
        Me.grpSpacecraftParams.TabStop = False
        Me.grpSpacecraftParams.Text = "Spacecraft Parameters"
        '
        'btnSpacecraftParamHelp
        '
        Me.btnSpacecraftParamHelp.Location = New System.Drawing.Point(209, 140)
        Me.btnSpacecraftParamHelp.Name = "btnSpacecraftParamHelp"
        Me.btnSpacecraftParamHelp.Size = New System.Drawing.Size(26, 23)
        Me.btnSpacecraftParamHelp.TabIndex = 13
        Me.btnSpacecraftParamHelp.Text = "?"
        Me.btnSpacecraftParamHelp.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(151, 145)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(21, 13)
        Me.Label21.TabIndex = 12
        Me.Label21.Text = "(m)"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(1, 145)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(71, 13)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "Nose Radius:"
        '
        'txtNoseRadius
        '
        Me.txtNoseRadius.Location = New System.Drawing.Point(77, 142)
        Me.txtNoseRadius.Name = "txtNoseRadius"
        Me.txtNoseRadius.Size = New System.Drawing.Size(68, 20)
        Me.txtNoseRadius.TabIndex = 10
        Me.txtNoseRadius.Text = "3.0"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(151, 119)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(33, 13)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "(m^2)"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(151, 93)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(25, 13)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "(kg)"
        '
        'txtArea
        '
        Me.txtArea.Location = New System.Drawing.Point(78, 116)
        Me.txtArea.Name = "txtArea"
        Me.txtArea.Size = New System.Drawing.Size(67, 20)
        Me.txtArea.TabIndex = 6
        Me.txtArea.Text = "35"
        '
        'txtMass
        '
        Me.txtMass.Location = New System.Drawing.Point(77, 90)
        Me.txtMass.Name = "txtMass"
        Me.txtMass.Size = New System.Drawing.Size(68, 20)
        Me.txtMass.TabIndex = 4
        Me.txtMass.Text = "5500"
        '
        'txtCd
        '
        Me.txtCd.Location = New System.Drawing.Point(77, 64)
        Me.txtCd.Name = "txtCd"
        Me.txtCd.Size = New System.Drawing.Size(68, 20)
        Me.txtCd.TabIndex = 3
        Me.txtCd.Text = "1.5"
        '
        'cboAtmosphereModel
        '
        Me.cboAtmosphereModel.FormattingEnabled = True
        Me.cboAtmosphereModel.Items.AddRange(New Object() {"Harris-Priester", "Jacchia 1960", "MSISE 1990", "NRLMSISE 2000", ""})
        Me.cboAtmosphereModel.Location = New System.Drawing.Point(77, 37)
        Me.cboAtmosphereModel.Name = "cboAtmosphereModel"
        Me.cboAtmosphereModel.Size = New System.Drawing.Size(121, 21)
        Me.cboAtmosphereModel.TabIndex = 0
        Me.cboAtmosphereModel.Text = "Select..."
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(72, 38)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(126, 15)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "Blunt Body Spacecraft"
        '
        'btnCreateSat
        '
        Me.btnCreateSat.Location = New System.Drawing.Point(97, 540)
        Me.btnCreateSat.Name = "btnCreateSat"
        Me.btnCreateSat.Size = New System.Drawing.Size(75, 23)
        Me.btnCreateSat.TabIndex = 6
        Me.btnCreateSat.Text = "Create"
        Me.btnCreateSat.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(3, 70)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(273, 448)
        Me.TabControl1.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage1.Controls.Add(Me.grpReentryParams)
        Me.TabPage1.Controls.Add(Me.grpSpacecraftParams)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(265, 422)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Orbit"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(265, 422)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Time Frame"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.txtStartTime)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.txtStopTime)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(253, 163)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(12, 27)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(229, 26)
        Me.Label24.TabIndex = 4
        Me.Label24.Text = "Current Scenario Simulation Start/Stop Times.  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Adust to control orbit propagati" & _
    "on times."
        '
        'txtStartTime
        '
        Me.txtStartTime.Location = New System.Drawing.Point(77, 80)
        Me.txtStartTime.Name = "txtStartTime"
        Me.txtStartTime.Size = New System.Drawing.Size(157, 20)
        Me.txtStartTime.TabIndex = 0
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(13, 109)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(58, 13)
        Me.Label23.TabIndex = 3
        Me.Label23.Text = "Stop Time:"
        '
        'txtStopTime
        '
        Me.txtStopTime.Location = New System.Drawing.Point(77, 106)
        Me.txtStopTime.Name = "txtStopTime"
        Me.txtStopTime.Size = New System.Drawing.Size(157, 20)
        Me.txtStopTime.TabIndex = 1
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(13, 83)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(58, 13)
        Me.Label22.TabIndex = 2
        Me.Label22.Text = "Start Time:"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.Controls.Add(Me.GroupBox2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(265, 422)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Report/Graph"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnGLoadReport)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.btnGLoadGraph)
        Me.GroupBox3.Location = New System.Drawing.Point(9, 173)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(243, 133)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        '
        'btnGLoadReport
        '
        Me.btnGLoadReport.Location = New System.Drawing.Point(25, 51)
        Me.btnGLoadReport.Name = "btnGLoadReport"
        Me.btnGLoadReport.Size = New System.Drawing.Size(75, 23)
        Me.btnGLoadReport.TabIndex = 0
        Me.btnGLoadReport.Text = "Report"
        Me.btnGLoadReport.UseVisualStyleBackColor = True
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(6, 16)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(56, 13)
        Me.Label26.TabIndex = 2
        Me.Label26.Text = "G-Loading"
        '
        'btnGLoadGraph
        '
        Me.btnGLoadGraph.Location = New System.Drawing.Point(25, 80)
        Me.btnGLoadGraph.Name = "btnGLoadGraph"
        Me.btnGLoadGraph.Size = New System.Drawing.Size(75, 23)
        Me.btnGLoadGraph.TabIndex = 1
        Me.btnGLoadGraph.Text = "Graph"
        Me.btnGLoadGraph.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnHeatingReport)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.btnHeatingGraph)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 20)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(243, 133)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'btnHeatingReport
        '
        Me.btnHeatingReport.Location = New System.Drawing.Point(25, 51)
        Me.btnHeatingReport.Name = "btnHeatingReport"
        Me.btnHeatingReport.Size = New System.Drawing.Size(75, 23)
        Me.btnHeatingReport.TabIndex = 0
        Me.btnHeatingReport.Text = "Report"
        Me.btnHeatingReport.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(6, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(125, 13)
        Me.Label25.TabIndex = 2
        Me.Label25.Text = "Stagnation Point Heating"
        '
        'btnHeatingGraph
        '
        Me.btnHeatingGraph.Location = New System.Drawing.Point(25, 80)
        Me.btnHeatingGraph.Name = "btnHeatingGraph"
        Me.btnHeatingGraph.Size = New System.Drawing.Size(75, 23)
        Me.btnHeatingGraph.TabIndex = 1
        Me.btnHeatingGraph.Text = "Graph"
        Me.btnHeatingGraph.UseVisualStyleBackColor = True
        '
        'CustomUserInterface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnCreateSat)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "CustomUserInterface"
        Me.Size = New System.Drawing.Size(279, 610)
        Me.grpReentryParams.ResumeLayout(False)
        Me.grpReentryParams.PerformLayout()
        Me.grpSpacecraftParams.ResumeLayout(False)
        Me.grpSpacecraftParams.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents grpReentryParams As System.Windows.Forms.GroupBox
    Friend WithEvents txtHorizFPA As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAltitude As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLongitude As System.Windows.Forms.TextBox
    Friend WithEvents txtLatitude As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAzimuth As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtVelocity As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grpSpacecraftParams As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtArea As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtMass As System.Windows.Forms.TextBox
    Friend WithEvents txtCd As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cboAtmosphereModel As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtNoseRadius As System.Windows.Forms.TextBox
    Friend WithEvents btnCreateSat As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtStopTime As System.Windows.Forms.TextBox
    Friend WithEvents txtStartTime As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnSpacecraftParamHelp As System.Windows.Forms.Button
    Friend WithEvents btnReentryParamHelp As System.Windows.Forms.Button
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGLoadReport As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents btnGLoadGraph As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnHeatingReport As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnHeatingGraph As System.Windows.Forms.Button

End Class
