<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OutputEntity
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
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.m_symbol = New System.Windows.Forms.TextBox
        Me.m_affiliation = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(13, 42)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(90, 13)
        Me.label2.TabIndex = 7
        Me.label2.Text = "Mil2525b Symbol:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(51, 15)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(52, 13)
        Me.label1.TabIndex = 6
        Me.label1.Text = "Affiliation:"
        '
        'm_symbol
        '
        Me.m_symbol.Location = New System.Drawing.Point(109, 39)
        Me.m_symbol.Name = "m_symbol"
        Me.m_symbol.Size = New System.Drawing.Size(121, 20)
        Me.m_symbol.TabIndex = 5
        '
        'm_affiliation
        '
        Me.m_affiliation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.m_affiliation.FormattingEnabled = True
        Me.m_affiliation.Location = New System.Drawing.Point(109, 12)
        Me.m_affiliation.Name = "m_affiliation"
        Me.m_affiliation.Size = New System.Drawing.Size(121, 21)
        Me.m_affiliation.TabIndex = 4
        '
        'OutputEntity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(250, 75)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.m_symbol)
        Me.Controls.Add(Me.m_affiliation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "OutputEntity"
        Me.ShowInTaskbar = False
        Me.Text = "OutputEntity"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents m_symbol As System.Windows.Forms.TextBox
    Private WithEvents m_affiliation As System.Windows.Forms.ComboBox
End Class
