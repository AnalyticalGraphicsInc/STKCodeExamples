Imports System.Runtime.InteropServices
Imports AGI.Ui.Plugins
Imports System.Windows.Forms
Imports System.Reflection
Imports System.Drawing
Imports AGI.Ui.Core
Imports AGI.STKObjects
Imports AGI.Ui.Application

<Guid("AE0BFA87-585D-4B10-BBC7-E78A5BDFBC28"), _
ProgId("Agi.Ui.Plugins.VB_Net.ReentryHeating"), _
ClassInterface(ClassInterfaceType.None)> _
Public Class ReentryHeatingPlugin
    Implements IAgUiPlugin
    Implements IAgUiPluginCommandTarget

    Dim m_pSite As IAgUiPluginSite
    Dim m_customUserInterface As CustomUserInterface
    Dim WithEvents m_root As AgStkObjectRoot
    Dim m_progress As IAgProgressTrackCancel

    Dim m_integrate As Boolean = True

    Public Sub OnDisplayConfigurationPage(ByVal ConfigPageBuilder As Agi.Ui.Plugins.IAgUiPluginConfigurationPageBuilder) Implements Agi.Ui.Plugins.IAgUiPlugin.OnDisplayConfigurationPage
        'Add a Configuration Page
        ConfigPageBuilder.AddCustomUserControlPage(Me, Me.GetType().Assembly.Location, GetType(CustomConfigPage).FullName, "Reentry Heating Plugin")
    End Sub

    Public Sub OnDisplayContextMenu(ByVal MenuBuilder As Agi.Ui.Plugins.IAgUiPluginMenuBuilder) Implements Agi.Ui.Plugins.IAgUiPlugin.OnDisplayContextMenu
        Dim picture As stdole.IPictureDisp

        picture = Microsoft.VisualBasic.Compatibility.VB6.Support.ImageToIPicture(My.Resources.Reentry)
        'picture = Microsoft.VisualBasic.Compatibility.VB6.Support.IconToIPicture(My.Resources.STK)
        'Add a Menu Item
        If m_integrate Then
            MenuBuilder.AddMenuItem("AGI.ReentryHeatingPlugin.MyFirstContextMenuCommand", "Spacecraft Reentry Heating Tool", "Open Reentry Heating Tool", picture)
        End If
    End Sub

    Public Sub OnInitializeToolbar(ByVal ToolbarBuilder As Agi.Ui.Plugins.IAgUiPluginToolbarBuilder) Implements Agi.Ui.Plugins.IAgUiPlugin.OnInitializeToolbar
        Dim picture As stdole.IPictureDisp

        'converting an ico file to be used as the image for toolbat button
        picture = Microsoft.VisualBasic.Compatibility.VB6.Support.ImageToIPicture(My.Resources.Reentry)
        'picture = Microsoft.VisualBasic.Compatibility.VB6.Support.IconToIPicture(My.Resources.STK)
        'Add a Toolbar Button
        ToolbarBuilder.AddButton("AGI.ReentryHeatingPlugin.MyFirstCommand", "Spacecraft Reentry Heating Tool", "Open Reentry Heating Tool.", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, picture)
    End Sub

    Public Sub OnShutdown() Implements Agi.Ui.Plugins.IAgUiPlugin.OnShutdown
        m_pSite = Nothing
    End Sub

    Public Sub OnStartup(ByVal PluginSite As Agi.Ui.Plugins.IAgUiPluginSite) Implements Agi.Ui.Plugins.IAgUiPlugin.OnStartup
        m_pSite = PluginSite
        'Get the AgStkObjectRoot
        Dim AgUiApp As IAgUiApplication = m_pSite.Application
        m_root = DirectCast(AgUiApp.Personality2, AgStkObjectRoot)
    End Sub

    Public Sub Exec(ByVal CommandName As String, ByVal TrackCancel As Agi.Ui.Plugins.IAgProgressTrackCancel, ByVal Parameters As Agi.Ui.Plugins.IAgUiPluginCommandParameters) Implements Agi.Ui.Plugins.IAgUiPluginCommandTarget.Exec
        'Controls what a command does
        If (String.Compare(CommandName, "AGI.ReentryHeatingPlugin.MyFirstCommand", True) = 0) Or (String.Compare(CommandName, "AGI.ReentryHeatingPlugin.MyFirstContextMenuCommand", True) = 0) Then
            m_progress = TrackCancel
            OpenUserInterface()
        End If
    End Sub

    Public Function QueryState(ByVal CommandName As String) As Agi.Ui.Plugins.AgEUiPluginCommandState Implements Agi.Ui.Plugins.IAgUiPluginCommandTarget.QueryState
        'Enable commands
        If (String.Compare(CommandName, "AGI.ReentryHeatingPlugin.MyFirstCommand", True) = 0) Or (String.Compare(CommandName, "AGI.ReentryHeatingPlugin.MyFirstContextMenuCommand", True) = 0) Then
            Return AgEUiPluginCommandState.eUiPluginCommandStateEnabled Or AgEUiPluginCommandState.eUiPluginCommandStateSupported
        End If
        Return AgEUiPluginCommandState.eUiPluginCommandStateNone
    End Function

    Public Property customUI() As CustomUserInterface
        Get
            Return m_customUserInterface
        End Get
        Set(ByVal value As CustomUserInterface)
            m_customUserInterface = value
        End Set
    End Property

    Public Property Integrate() As Boolean
        Get
            Return m_integrate
        End Get
        Set(ByVal value As Boolean)
            m_integrate = value
        End Set
    End Property

    Public ReadOnly Property STKRoot() As AgStkObjectRoot
        Get
            Return m_root
        End Get
    End Property

    Public ReadOnly Property ProgressBar() As IAgProgressTrackCancel
        Get
            Return m_progress
        End Get
    End Property

    Public Sub OpenUserInterface()
        'Open a User Interface
        Dim windows As IAgUiPluginWindowSite = TryCast(m_pSite, IAgUiPluginWindowSite)
        If windows Is Nothing Then
            MessageBox.Show("Host application is unable to open windows.")
        Else
            Dim params As AgUiPluginWindowCreateParameters = windows.CreateParameters()
            params.AllowMultiple = False
            params.AssemblyPath = Me.[GetType]().Assembly.Location
            params.UserControlFullName = GetType(CustomUserInterface).FullName
            params.Caption = "Spacecraft Reentry Heating Tool"
            params.DockStyle = AgEDockStyle.eDockStyleDockedRight
            params.Width = 275
            Dim obj As Object = windows.CreateNetToolWindowParam(Me, params)
        End If
    End Sub

End Class
