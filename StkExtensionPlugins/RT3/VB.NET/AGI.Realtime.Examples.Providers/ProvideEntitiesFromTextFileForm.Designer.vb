<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProvideEntitiesFromTextFileForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.label1 = New System.Windows.Forms.Label
        Me.m_ok = New System.Windows.Forms.Button
        Me.m_cancel = New System.Windows.Forms.Button
        Me.m_browse = New System.Windows.Forms.Button
        Me.m_filename = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 14)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(49, 13)
        Me.label1.TabIndex = 9
        Me.label1.Text = "Filename"
        '
        'm_ok
        '
        Me.m_ok.Location = New System.Drawing.Point(271, 37)
        Me.m_ok.Name = "m_ok"
        Me.m_ok.Size = New System.Drawing.Size(75, 23)
        Me.m_ok.TabIndex = 8
        Me.m_ok.Text = "OK"
        Me.m_ok.UseVisualStyleBackColor = True
        '
        'm_cancel
        '
        Me.m_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.m_cancel.Location = New System.Drawing.Point(352, 37)
        Me.m_cancel.Name = "m_cancel"
        Me.m_cancel.Size = New System.Drawing.Size(75, 23)
        Me.m_cancel.TabIndex = 7
        Me.m_cancel.Text = "Cancel"
        Me.m_cancel.UseVisualStyleBackColor = True
        '
        'm_browse
        '
        Me.m_browse.Location = New System.Drawing.Point(402, 11)
        Me.m_browse.Name = "m_browse"
        Me.m_browse.Size = New System.Drawing.Size(25, 20)
        Me.m_browse.TabIndex = 6
        Me.m_browse.Text = "..."
        Me.m_browse.UseVisualStyleBackColor = True
        '
        'm_filename
        '
        Me.m_filename.Location = New System.Drawing.Point(67, 11)
        Me.m_filename.Name = "m_filename"
        Me.m_filename.ReadOnly = True
        Me.m_filename.Size = New System.Drawing.Size(329, 20)
        Me.m_filename.TabIndex = 5
        Me.m_filename.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ProvideEntitiesFromTextFileForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(439, 71)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.m_ok)
        Me.Controls.Add(Me.m_cancel)
        Me.Controls.Add(Me.m_browse)
        Me.Controls.Add(Me.m_filename)
        Me.Name = "ProvideEntitiesFromTextFileForm"
        Me.Text = "Please selected an input file."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents m_ok As System.Windows.Forms.Button
    Private WithEvents m_cancel As System.Windows.Forms.Button
    Private WithEvents m_browse As System.Windows.Forms.Button
    Private WithEvents m_filename As System.Windows.Forms.TextBox
End Class
