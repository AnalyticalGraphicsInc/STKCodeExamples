<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomGUI
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
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.CheckedListBox2 = New System.Windows.Forms.CheckedListBox
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.StatusBox = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(192, 395)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Copy Schedule"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select Source"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.CheckOnClick = True
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(32, 98)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(202, 244)
        Me.CheckedListBox1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(264, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Select Destination(s)"
        '
        'CheckedListBox2
        '
        Me.CheckedListBox2.CheckOnClick = True
        Me.CheckedListBox2.FormattingEnabled = True
        Me.CheckedListBox2.Location = New System.Drawing.Point(267, 98)
        Me.CheckedListBox2.Name = "CheckedListBox2"
        Me.CheckedListBox2.Size = New System.Drawing.Size(202, 244)
        Me.CheckedListBox2.TabIndex = 4
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(143, 363)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(218, 17)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Clear existing schedule(s) before copying"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(74, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(356, 18)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Custom Data / Residual Editing Schedule Copy Utility"
        '
        'StatusBox
        '
        Me.StatusBox.Location = New System.Drawing.Point(32, 450)
        Me.StatusBox.Name = "StatusBox"
        Me.StatusBox.ReadOnly = True
        Me.StatusBox.Size = New System.Drawing.Size(437, 20)
        Me.StatusBox.TabIndex = 7
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(394, 395)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Reset"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(118, 47)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(121, 17)
        Me.RadioButton1.TabIndex = 9
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Custom Data Editing"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(261, 47)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(101, 17)
        Me.RadioButton2.TabIndex = 10
        Me.RadioButton2.Text = "Residual Editing"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'CustomGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.StatusBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.CheckedListBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "CustomGUI"
        Me.Size = New System.Drawing.Size(505, 620)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckedListBox2 As System.Windows.Forms.CheckedListBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents StatusBox As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton

End Class
