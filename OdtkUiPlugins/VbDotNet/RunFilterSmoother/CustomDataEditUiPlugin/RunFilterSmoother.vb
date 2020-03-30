Imports System.Runtime.InteropServices
Imports AGI.Ui.Plugins
Imports System.Windows.Forms
Imports System.Reflection
Imports System.Drawing
Imports AGI.Ui.Core

<Guid("504D1D15-D99B-4156-BD0D-F15A7A94B771"), _
ProgId("Agi.RunFilterSmoother"), _
ClassInterface(ClassInterfaceType.None)> _
Public Class RunFilterSmoother
    Implements AGI.Ui.Plugins.IAgUiPlugin
    Implements AGI.Ui.Plugins.IAgUiPluginCommandTarget

    Dim m_pSite As AGI.Ui.Plugins.IAgUiPluginSite
    Dim m_odtk As Object

    Public ReadOnly Property ODRoot() As Object
        Get
            Return m_odtk
        End Get
    End Property

    'Generic plugin code
    Public Sub OnDisplayConfigurationPage(ByVal ConfigPageBuilder As AGI.Ui.Plugins.IAgUiPluginConfigurationPageBuilder) Implements AGI.Ui.Plugins.IAgUiPlugin.OnDisplayConfigurationPage

    End Sub

    Public Sub OnDisplayContextMenu(ByVal MenuBuilder As AGI.Ui.Plugins.IAgUiPluginMenuBuilder) Implements AGI.Ui.Plugins.IAgUiPlugin.OnDisplayContextMenu

    End Sub

    Public Sub OnInitializeToolbar(ByVal ToolbarBuilder As AGI.Ui.Plugins.IAgUiPluginToolbarBuilder) Implements AGI.Ui.Plugins.IAgUiPlugin.OnInitializeToolbar
        Dim m_picture1 As stdole.IPictureDisp
        Dim m_picture2 As stdole.IPictureDisp
        Dim m_picture3 As stdole.IPictureDisp
        Dim imageResource1 As String
        Dim imageResource2 As String
        Dim imageResource3 As String
        Dim currentAssembly As Assembly
        Dim iconImage1 As Icon
        Dim iconImage2 As Icon
        Dim iconImage3 As Icon

        imageResource1 = "CustomDataEditUiPlugin.Filter1.ico"
        imageResource2 = "CustomDataEditUiPlugin.smoother.ico"
        imageResource3 = "CustomDataEditUiPlugin.filterSmoother4.ico"
        currentAssembly = Assembly.GetExecutingAssembly()
        iconImage1 = New Icon(currentAssembly.GetManifestResourceStream(imageResource1))
        iconImage2 = New Icon(currentAssembly.GetManifestResourceStream(imageResource2))
        iconImage3 = New Icon(currentAssembly.GetManifestResourceStream(imageResource3))
        m_picture1 = OlePictureHelper.OlePictureFromImage(iconImage1)
        m_picture2 = OlePictureHelper.OlePictureFromImage(iconImage2)
        m_picture3 = OlePictureHelper.OlePictureFromImage(iconImage3)
        ToolbarBuilder.AddButton("Agi.RunFilter", "Run Filter", "Run Filter", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture1)
        ToolbarBuilder.AddButton("Agi.RunSmoother", "Run Smoother", "Run Smoother", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture2)
        ToolbarBuilder.AddButton("Agi.RunFilterSmoother", "Run Filter and Smoother", "Run Filter and Smoother", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture3)
    End Sub

    Public Sub OnShutdown() Implements AGI.Ui.Plugins.IAgUiPlugin.OnShutdown
        m_pSite = Nothing
    End Sub

    Public Sub OnStartup(ByVal PluginSite As AGI.Ui.Plugins.IAgUiPluginSite) Implements AGI.Ui.Plugins.IAgUiPlugin.OnStartup
        m_pSite = PluginSite
        m_odtk = m_pSite.Application.Personality
    End Sub

    Public Sub Exec(ByVal CommandName As String, ByVal TrackCancel As AGI.Ui.Plugins.IAgProgressTrackCancel, ByVal Parameters As AGI.Ui.Plugins.IAgUiPluginCommandParameters) Implements AGI.Ui.Plugins.IAgUiPluginCommandTarget.Exec

        If (String.Compare(CommandName, "Agi.RunFilter", True) = 0) Then
            Dim scen As Object = m_odtk.Scenario(0).Filter(0).Go()
        ElseIf (String.Compare(CommandName, "Agi.RunSmoother", True) = 0) Then
            Dim scen As Object = m_odtk.Scenario(0).Smoother(0).Go()
        ElseIf (String.Compare(CommandName, "Agi.RunFilterSmoother", True) = 0) Then
            Dim scen As Object = m_odtk.Scenario(0).Filter(0).Go()
            Dim scen2 As Object = m_odtk.Scenario(0).Smoother(0).Go()
        End If

    End Sub

    Public Function QueryState(ByVal CommandName As String) As AGI.Ui.Plugins.AgEUiPluginCommandState Implements AGI.Ui.Plugins.IAgUiPluginCommandTarget.QueryState
        If (String.Compare(CommandName, "Agi.RunFilter", True) = 0) Then
            Return AgEUiPluginCommandState.eUiPluginCommandStateEnabled Or AgEUiPluginCommandState.eUiPluginCommandStateSupported
        ElseIf (String.Compare(CommandName, "Agi.RunSmoother", True) = 0) Then
            Return AgEUiPluginCommandState.eUiPluginCommandStateEnabled Or AgEUiPluginCommandState.eUiPluginCommandStateSupported
        ElseIf (String.Compare(CommandName, "Agi.RunFilterSmoother", True) = 0) Then
            Return AgEUiPluginCommandState.eUiPluginCommandStateEnabled Or AgEUiPluginCommandState.eUiPluginCommandStateSupported
        End If
        Return AgEUiPluginCommandState.eUiPluginCommandStateNone
    End Function

End Class
