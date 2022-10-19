Imports Agi.Ui.Plugins
Imports Agi.STKObjects
Imports AGI.STKUtil


Public Class CustomConfigPage
    Implements IAgUiPluginConfigurationPageActions

    Private _site As IAgUiPluginConfigurationPageSite
    Private _plugin As Agi.Ui.Plugins.VB_Net.ReentryHeating.ReentryHeatingPlugin

    Public Function OnApply() As Boolean Implements Agi.Ui.Plugins.IAgUiPluginConfigurationPageActions.OnApply
        _plugin.Integrate = CheckBox1.Checked
        Return True
    End Function

    Public Sub OnCancel() Implements AGI.Ui.Plugins.IAgUiPluginConfigurationPageActions.OnCancel
        'Intentionally left empty
    End Sub

    Public Sub OnCreated(ByVal Site As AGI.Ui.Plugins.IAgUiPluginConfigurationPageSite) Implements AGI.Ui.Plugins.IAgUiPluginConfigurationPageActions.OnCreated
        _site = Site
        _plugin = DirectCast(_site.Plugin, ReentryHeatingPlugin)
        CheckBox1.Checked = _plugin.Integrate
    End Sub

    Public Sub OnOK() Implements AGI.Ui.Plugins.IAgUiPluginConfigurationPageActions.OnOK
        _plugin.Integrate = CheckBox1.Checked
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        'Enable apply button
        If Not (_site Is Nothing) Then
            _site.SetModified(True)
        End If
    End Sub

    Private Sub btnHelpDocument_Click(sender As System.Object, e As System.EventArgs) Handles btnHelpDocument.Click
        'open the Help Document
        Dim root As AgStkObjectRoot
        root = _plugin.STKRoot

        Dim stkInstallDirectory
        stkInstallDirectory = root.ExecuteCommand("GetDirectory / STKHome")


        Dim helpFileDirectory As String
        helpFileDirectory = CStr(stkInstallDirectory(0)) + "Plugins\ReentryHeatingPlugin\Reentry_Heating_Plugin_README.pdf"


        Try

            System.Diagnostics.Process.Start(helpFileDirectory)

        Catch

        End Try


    End Sub
End Class
