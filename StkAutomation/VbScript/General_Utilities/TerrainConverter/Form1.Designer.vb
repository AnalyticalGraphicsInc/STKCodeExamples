<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SelectInput = New System.Windows.Forms.Button()
        Me.DisplayInput = New System.Windows.Forms.TextBox()
        Me.Convert = New System.Windows.Forms.Button()
        Me.ToConvert = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SelectOutput = New System.Windows.Forms.Button()
        Me.DisplayOutput = New System.Windows.Forms.TextBox()
        Me.SubDirs = New System.Windows.Forms.CheckBox()
        Me.SelectInputDirBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.OutputSame = New System.Windows.Forms.CheckBox()
        Me.GetFiles = New System.Windows.Forms.Button()
        Me.ClearSelectedFIles = New System.Windows.Forms.Button()
        Me.KeepDatt = New System.Windows.Forms.CheckBox()
        Me.SelectOutputDirBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.Formats = New System.Windows.Forms.GroupBox()
        Me.DT5 = New System.Windows.Forms.CheckBox()
        Me.DT4 = New System.Windows.Forms.CheckBox()
        Me.DT3 = New System.Windows.Forms.CheckBox()
        Me.DT2 = New System.Windows.Forms.CheckBox()
        Me.DT0 = New System.Windows.Forms.CheckBox()
        Me.DT1 = New System.Windows.Forms.CheckBox()
        Me.G98 = New System.Windows.Forms.CheckBox()
        Me.DMED = New System.Windows.Forms.CheckBox()
        Me.HDR = New System.Windows.Forms.CheckBox()
        Me.DEM = New System.Windows.Forms.CheckBox()
        Me.DTE = New System.Windows.Forms.CheckBox()
        Me.OverwritePdtt = New System.Windows.Forms.CheckBox()
        Me.Formats.SuspendLayout()
        Me.SuspendLayout()
        '
        'SelectInput
        '
        Me.SelectInput.Location = New System.Drawing.Point(12, 17)
        Me.SelectInput.Name = "SelectInput"
        Me.SelectInput.Size = New System.Drawing.Size(128, 27)
        Me.SelectInput.TabIndex = 0
        Me.SelectInput.Text = "Select Input Directory"
        Me.SelectInput.UseVisualStyleBackColor = True
        '
        'DisplayInput
        '
        Me.DisplayInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayInput.Location = New System.Drawing.Point(146, 21)
        Me.DisplayInput.Name = "DisplayInput"
        Me.DisplayInput.Size = New System.Drawing.Size(346, 20)
        Me.DisplayInput.TabIndex = 1
        Me.DisplayInput.Text = "C:\terrain"
        '
        'Convert
        '
        Me.Convert.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Convert.Location = New System.Drawing.Point(386, 472)
        Me.Convert.Name = "Convert"
        Me.Convert.Size = New System.Drawing.Size(105, 33)
        Me.Convert.TabIndex = 2
        Me.Convert.Text = "Convert"
        Me.Convert.UseVisualStyleBackColor = True
        '
        'ToConvert
        '
        Me.ToConvert.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToConvert.Location = New System.Drawing.Point(12, 274)
        Me.ToConvert.Multiline = True
        Me.ToConvert.Name = "ToConvert"
        Me.ToConvert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ToConvert.Size = New System.Drawing.Size(480, 192)
        Me.ToConvert.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 255)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "files to be converted"
        '
        'SelectOutput
        '
        Me.SelectOutput.Location = New System.Drawing.Point(12, 192)
        Me.SelectOutput.Name = "SelectOutput"
        Me.SelectOutput.Size = New System.Drawing.Size(128, 27)
        Me.SelectOutput.TabIndex = 6
        Me.SelectOutput.Text = "Select Output Directory"
        Me.SelectOutput.UseVisualStyleBackColor = True
        '
        'DisplayOutput
        '
        Me.DisplayOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayOutput.Location = New System.Drawing.Point(145, 196)
        Me.DisplayOutput.Name = "DisplayOutput"
        Me.DisplayOutput.Size = New System.Drawing.Size(346, 20)
        Me.DisplayOutput.TabIndex = 7
        Me.DisplayOutput.Text = "c:\terrain"
        '
        'SubDirs
        '
        Me.SubDirs.AutoSize = True
        Me.SubDirs.Checked = True
        Me.SubDirs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SubDirs.Location = New System.Drawing.Point(25, 50)
        Me.SubDirs.Name = "SubDirs"
        Me.SubDirs.Size = New System.Drawing.Size(142, 17)
        Me.SubDirs.TabIndex = 8
        Me.SubDirs.Text = "Include all subdirectories"
        Me.SubDirs.UseVisualStyleBackColor = True
        '
        'SelectInputDirBrowser
        '
        Me.SelectInputDirBrowser.SelectedPath = "c:\"
        '
        'OutputSame
        '
        Me.OutputSame.AutoSize = True
        Me.OutputSame.Checked = True
        Me.OutputSame.CheckState = System.Windows.Forms.CheckState.Checked
        Me.OutputSame.Location = New System.Drawing.Point(25, 225)
        Me.OutputSame.Name = "OutputSame"
        Me.OutputSame.Size = New System.Drawing.Size(184, 17)
        Me.OutputSame.TabIndex = 9
        Me.OutputSame.Text = "Output the same as input direcory"
        Me.OutputSame.UseVisualStyleBackColor = True
        '
        'GetFiles
        '
        Me.GetFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GetFiles.Location = New System.Drawing.Point(12, 472)
        Me.GetFiles.Name = "GetFiles"
        Me.GetFiles.Size = New System.Drawing.Size(105, 33)
        Me.GetFiles.TabIndex = 10
        Me.GetFiles.Text = "Get Files"
        Me.GetFiles.UseVisualStyleBackColor = True
        '
        'ClearSelectedFIles
        '
        Me.ClearSelectedFIles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ClearSelectedFIles.Location = New System.Drawing.Point(123, 472)
        Me.ClearSelectedFIles.Name = "ClearSelectedFIles"
        Me.ClearSelectedFIles.Size = New System.Drawing.Size(44, 33)
        Me.ClearSelectedFIles.TabIndex = 11
        Me.ClearSelectedFIles.Text = "Clear"
        Me.ClearSelectedFIles.UseVisualStyleBackColor = True
        '
        'KeepDatt
        '
        Me.KeepDatt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.KeepDatt.AutoSize = True
        Me.KeepDatt.Location = New System.Drawing.Point(335, 225)
        Me.KeepDatt.Name = "KeepDatt"
        Me.KeepDatt.Size = New System.Drawing.Size(156, 17)
        Me.KeepDatt.TabIndex = 13
        Me.KeepDatt.Text = "Keep intermediate .datt files"
        Me.KeepDatt.UseVisualStyleBackColor = True
        '
        'SelectOutputDirBrowser
        '
        Me.SelectOutputDirBrowser.SelectedPath = "c:\"
        '
        'Formats
        '
        Me.Formats.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Formats.Controls.Add(Me.DT5)
        Me.Formats.Controls.Add(Me.DT4)
        Me.Formats.Controls.Add(Me.DT3)
        Me.Formats.Controls.Add(Me.DT2)
        Me.Formats.Controls.Add(Me.DT0)
        Me.Formats.Controls.Add(Me.DT1)
        Me.Formats.Controls.Add(Me.G98)
        Me.Formats.Controls.Add(Me.DMED)
        Me.Formats.Controls.Add(Me.HDR)
        Me.Formats.Controls.Add(Me.DEM)
        Me.Formats.Controls.Add(Me.DTE)
        Me.Formats.Location = New System.Drawing.Point(32, 73)
        Me.Formats.Name = "Formats"
        Me.Formats.Size = New System.Drawing.Size(437, 111)
        Me.Formats.TabIndex = 14
        Me.Formats.TabStop = False
        Me.Formats.Text = "Input Formats"
        '
        'DT5
        '
        Me.DT5.AutoSize = True
        Me.DT5.Checked = True
        Me.DT5.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT5.Location = New System.Drawing.Point(307, 65)
        Me.DT5.Name = "DT5"
        Me.DT5.Size = New System.Drawing.Size(124, 17)
        Me.DT5.TabIndex = 11
        Me.DT5.Text = "DT5 (DTED Level 5)"
        Me.DT5.UseVisualStyleBackColor = True
        '
        'DT4
        '
        Me.DT4.AutoSize = True
        Me.DT4.Checked = True
        Me.DT4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT4.Location = New System.Drawing.Point(307, 42)
        Me.DT4.Name = "DT4"
        Me.DT4.Size = New System.Drawing.Size(124, 17)
        Me.DT4.TabIndex = 10
        Me.DT4.Text = "DT4 (DTED Level 4)"
        Me.DT4.UseVisualStyleBackColor = True
        '
        'DT3
        '
        Me.DT3.AutoSize = True
        Me.DT3.Checked = True
        Me.DT3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT3.Location = New System.Drawing.Point(307, 19)
        Me.DT3.Name = "DT3"
        Me.DT3.Size = New System.Drawing.Size(124, 17)
        Me.DT3.TabIndex = 9
        Me.DT3.Text = "DT3 (DTED Level 3)"
        Me.DT3.UseVisualStyleBackColor = True
        '
        'DT2
        '
        Me.DT2.AutoSize = True
        Me.DT2.Checked = True
        Me.DT2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT2.Location = New System.Drawing.Point(150, 87)
        Me.DT2.Name = "DT2"
        Me.DT2.Size = New System.Drawing.Size(124, 17)
        Me.DT2.TabIndex = 8
        Me.DT2.Text = "DT2 (DTED Level 2)"
        Me.DT2.UseVisualStyleBackColor = True
        '
        'DT0
        '
        Me.DT0.AutoSize = True
        Me.DT0.Checked = True
        Me.DT0.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT0.Location = New System.Drawing.Point(150, 42)
        Me.DT0.Name = "DT0"
        Me.DT0.Size = New System.Drawing.Size(124, 17)
        Me.DT0.TabIndex = 7
        Me.DT0.Text = "DT0 (DTED Level 0)"
        Me.DT0.UseVisualStyleBackColor = True
        '
        'DT1
        '
        Me.DT1.AutoSize = True
        Me.DT1.Checked = True
        Me.DT1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT1.Location = New System.Drawing.Point(150, 65)
        Me.DT1.Name = "DT1"
        Me.DT1.Size = New System.Drawing.Size(124, 17)
        Me.DT1.TabIndex = 6
        Me.DT1.Text = "DT1 (DTED Level 1)"
        Me.DT1.UseVisualStyleBackColor = True
        '
        'G98
        '
        Me.G98.AutoSize = True
        Me.G98.Checked = True
        Me.G98.CheckState = System.Windows.Forms.CheckState.Checked
        Me.G98.Location = New System.Drawing.Point(150, 19)
        Me.G98.Name = "G98"
        Me.G98.Size = New System.Drawing.Size(146, 17)
        Me.G98.TabIndex = 5
        Me.G98.Text = "g98 (GEODAS Grid Data)"
        Me.G98.UseVisualStyleBackColor = True
        '
        'DMED
        '
        Me.DMED.AutoSize = True
        Me.DMED.Checked = True
        Me.DMED.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DMED.Location = New System.Drawing.Point(6, 87)
        Me.DMED.Name = "DMED"
        Me.DMED.Size = New System.Drawing.Size(122, 17)
        Me.DMED.TabIndex = 3
        Me.DMED.Text = "DMED (NIMA/NGA)"
        Me.DMED.UseVisualStyleBackColor = True
        '
        'HDR
        '
        Me.HDR.AutoSize = True
        Me.HDR.Checked = True
        Me.HDR.CheckState = System.Windows.Forms.CheckState.Checked
        Me.HDR.Location = New System.Drawing.Point(6, 65)
        Me.HDR.Name = "HDR"
        Me.HDR.Size = New System.Drawing.Size(127, 17)
        Me.HDR.TabIndex = 2
        Me.HDR.Text = "hdr (GTOPO30 DEM)"
        Me.HDR.UseVisualStyleBackColor = True
        '
        'DEM
        '
        Me.DEM.AutoSize = True
        Me.DEM.Checked = True
        Me.DEM.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DEM.Location = New System.Drawing.Point(6, 42)
        Me.DEM.Name = "DEM"
        Me.DEM.Size = New System.Drawing.Size(116, 17)
        Me.DEM.TabIndex = 1
        Me.DEM.Text = "DEM (USGS DEM)"
        Me.DEM.UseVisualStyleBackColor = True
        '
        'DTE
        '
        Me.DTE.AutoSize = True
        Me.DTE.Checked = True
        Me.DTE.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DTE.Location = New System.Drawing.Point(6, 19)
        Me.DTE.Name = "DTE"
        Me.DTE.Size = New System.Drawing.Size(133, 17)
        Me.DTE.TabIndex = 0
        Me.DTE.Text = "DTE (MUSE raster file)"
        Me.DTE.UseVisualStyleBackColor = True
        '
        'OverwritePdtt
        '
        Me.OverwritePdtt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OverwritePdtt.AutoSize = True
        Me.OverwritePdtt.Location = New System.Drawing.Point(335, 248)
        Me.OverwritePdtt.Name = "OverwritePdtt"
        Me.OverwritePdtt.Size = New System.Drawing.Size(157, 17)
        Me.OverwritePdtt.TabIndex = 15
        Me.OverwritePdtt.Text = "overwrite previous .pdtt files"
        Me.OverwritePdtt.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 517)
        Me.Controls.Add(Me.OverwritePdtt)
        Me.Controls.Add(Me.Formats)
        Me.Controls.Add(Me.KeepDatt)
        Me.Controls.Add(Me.ClearSelectedFIles)
        Me.Controls.Add(Me.GetFiles)
        Me.Controls.Add(Me.OutputSame)
        Me.Controls.Add(Me.SubDirs)
        Me.Controls.Add(Me.DisplayOutput)
        Me.Controls.Add(Me.SelectOutput)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToConvert)
        Me.Controls.Add(Me.Convert)
        Me.Controls.Add(Me.DisplayInput)
        Me.Controls.Add(Me.SelectInput)
        Me.Name = "Form1"
        Me.Text = "TerrainConverter"
        Me.Formats.ResumeLayout(False)
        Me.Formats.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SelectInput As System.Windows.Forms.Button
    Friend WithEvents DisplayInput As System.Windows.Forms.TextBox
    Friend WithEvents Convert As System.Windows.Forms.Button
    Friend WithEvents ToConvert As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SelectOutput As System.Windows.Forms.Button
    Friend WithEvents DisplayOutput As System.Windows.Forms.TextBox
    Friend WithEvents SubDirs As System.Windows.Forms.CheckBox
    Friend WithEvents SelectInputDirBrowser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OutputSame As System.Windows.Forms.CheckBox
    Friend WithEvents GetFiles As System.Windows.Forms.Button
    Friend WithEvents ClearSelectedFIles As System.Windows.Forms.Button
    Friend WithEvents KeepDatt As System.Windows.Forms.CheckBox
    Friend WithEvents SelectOutputDirBrowser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Formats As System.Windows.Forms.GroupBox
    Friend WithEvents DTE As System.Windows.Forms.CheckBox
    Friend WithEvents DMED As System.Windows.Forms.CheckBox
    Friend WithEvents HDR As System.Windows.Forms.CheckBox
    Friend WithEvents DEM As System.Windows.Forms.CheckBox
    Friend WithEvents DT5 As System.Windows.Forms.CheckBox
    Friend WithEvents DT4 As System.Windows.Forms.CheckBox
    Friend WithEvents DT3 As System.Windows.Forms.CheckBox
    Friend WithEvents DT2 As System.Windows.Forms.CheckBox
    Friend WithEvents DT0 As System.Windows.Forms.CheckBox
    Friend WithEvents DT1 As System.Windows.Forms.CheckBox
    Friend WithEvents G98 As System.Windows.Forms.CheckBox
    Friend WithEvents OverwritePdtt As System.Windows.Forms.CheckBox

End Class
