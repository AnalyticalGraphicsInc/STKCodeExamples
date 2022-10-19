Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

<ComSourceInterfaces(GetType(IAgUiRtWindowHandleEvents))> _
Public Class OutputEntity
    Implements IAgUiRtWindowHandle

    Public Sub New(ByVal entityID As String, ByVal plugin As OutputTextFile)
        InitializeComponent()

        m_plugin = plugin
        m_entityID = entityID

        m_affiliation.Items.Add(Affiliation.Friendly)
        m_affiliation.Items.Add(Affiliation.Hostile)
        m_affiliation.Items.Add(Affiliation.Neutral)

        Dim settings As OutputSettings = Nothing
        If m_plugin.GetConfiguration(m_entityID, settings) Then
            m_affiliation.SelectedItem = settings.Affiliation
            m_symbol.Text = settings.Symbology
        Else
            m_affiliation.SelectedItem = Affiliation.Friendly
            m_symbol.Text = "SFAPMF----*****"
        End If
    End Sub

    Public Sub Apply() Implements IAgUiRtWindowHandle.Apply
        Dim settings As New OutputSettings(CType(m_affiliation.SelectedItem, Affiliation), m_symbol.Text)
        m_plugin.SetConfiguraiton(m_entityID, settings)
    End Sub

    Public Property Data() As Object Implements IAgUiRtWindowHandle.Data
        Get
            Data = m_plugin
        End Get
        Set(ByVal value As Object)
            m_plugin = CType(value, OutputTextFile)
        End Set
    End Property

    Public ReadOnly Property HWND() As Integer Implements IAgUiRtWindowHandle.HWND
        Get
            HWND = Me.Handle.ToInt32()
        End Get
    End Property

    Public ReadOnly Property IAgUiRtWindowHandle_Name() As String Implements IAgUiRtWindowHandle.Name
        Get
            IAgUiRtWindowHandle_Name = "OutputTextFileForm"
        End Get
    End Property

    Private Sub UiModified(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_affiliation.SelectedIndexChanged, m_symbol.TextChanged
        RaiseEvent OnUiModified(Me)
    End Sub

    <DispId(AgERtEventDispatchID.eProviderStartEvent)> Public Event OnUiModified(ByVal Sender As IAgUiRtWindowHandle)

    Private m_plugin As OutputTextFile
    Private m_entityID As String
End Class