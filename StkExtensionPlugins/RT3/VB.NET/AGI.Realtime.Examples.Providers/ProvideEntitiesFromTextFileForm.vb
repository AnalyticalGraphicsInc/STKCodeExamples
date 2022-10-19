Imports System.Runtime.InteropServices
Imports System.Windows.Forms

''' <summary>
''' A dialog plug-in which configures an instace of ProvideEntitiesFromFile.
''' 
''' This class implements IAgUiRtDialog, which allows for an application to
''' discover a GUI at runtime which can be used to configure the associated
''' plug-in.  It is the only required interface for a dialog plug-in.
''' 
''' To use this class in RT3, simply compile the library and load the
''' included reg file into the Windows registry.
''' 
''' For more information on RT3 interfaces and class, please see the RT3
''' Development Kit documentation.
''' </summary>
<ComClass(ProvideEntitiesFromTextFileForm.ClassId, _
          ProvideEntitiesFromTextFileForm.InterfaceId)> _
Public Class ProvideEntitiesFromTextFileForm
    Implements IAgUiRtDialog
#Region "ProvideEntitiesFromTextFileForm Members"
    Private Sub ProvideEntitiesFromTextFileForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_filename.Text = m_plugin.Filename

        'If we are running, make the GUI read-only
        If m_plugin.Active Then
            m_browse.Enabled = False
        End If
    End Sub

    Private Sub m_browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_browse.Click
        Dim dlg As New OpenFileDialog
        dlg.FileName = m_filename.Text
        If dlg.ShowDialog() = DialogResult.OK Then
            m_filename.Text = dlg.FileName
        End If
    End Sub

    Private Sub m_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_ok.Click
        m_plugin.Filename = m_filename.Text
        DialogResult = DialogResult.OK
    End Sub
#End Region

#Region "IAgUiRtDialog Members"
    ''' <summary>
    ''' Gets or sets the plug-in to be configured.
    ''' </summary>
    Public Property Data() As Object Implements IAgUiRtDialog.Data
        Get
            Data = m_plugin
        End Get
        Set(ByVal value As Object)
            m_plugin = CType(value, ProvideEntitiesFromTextFile)
        End Set
    End Property

    ''' <summary>
    ''' Displays the dialog, either modal or modeless, and configures
    ''' the plug-in that has been passed in as the Data property.
    ''' </summary>
    ''' <param name="Parent">The HWND of the parent window.</param>
    ''' <returns>The result of the dialog.</returns>
    Public Function IAgUiRtDialog_Activate(ByRef Parent As Integer) As AgEUiRtDialogResult Implements IAgUiRtDialog.Activate
        If ShowDialog() = DialogResult.Cancel Then
            IAgUiRtDialog_Activate = AgEUiRtDialogResult.eUiRtDialogResultCancel
        Else
            IAgUiRtDialog_Activate = AgEUiRtDialogResult.eUiRtDialogResultOK
        End If
    End Function

    Public ReadOnly Property IAgUiRtDialog_Name() As String Implements IAgUiRtDialog.Name
        Get
            IAgUiRtDialog_Name = "ProvideEntitiesFromTextFileForm"
        End Get
    End Property
#End Region

#Region "Variables, Constants, Events & Delegates"
    'Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
    'your class using GUIDGen, located in "Tools->Create GUID" in Visual
    'Studio or http://www.guidgen.com/
    Public Const ClassId As String = "5F941829-D5C2-4f45-AC13-D1F47B81B823"
    Public Const InterfaceId As String = "CA8E9600-4877-4dea-95AA-669DD9FBC9FD"
    Private m_plugin As ProvideEntitiesFromTextFile
#End Region
End Class