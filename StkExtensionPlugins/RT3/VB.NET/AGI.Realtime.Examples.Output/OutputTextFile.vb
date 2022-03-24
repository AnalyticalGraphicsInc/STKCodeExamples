Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports AGI.Attr
Imports AGI.Entity

#Region "IProvideTextFile"

''' <summary>
''' An IDispatch interface that defines parameters for specifying a text
''' file for writing.  This interface exists to satisfy IAgAttrConfig.
''' which you will read more about below. Your plug-in should also have a
''' corresponding interface with any configuration options exposed.
''' It should be marked as InterfaceIsDual and have a unique GUID.
''' Generate a new GUID your class using GUIDGen, located in
''' "Tools->Create GUID" in Visual Studio or use http://www.guidgen.com/.
''' </summary>
<Guid("D87D5565-FFE4-4d4a-A1E6-C73511A09E06")> _
<InterfaceType(ComInterfaceType.InterfaceIsDual)> _
Public Interface IProvideTextFile
    Inherits IAgRtProvideTrackingData
    Inherits IAgAttrConfig

    ''' <summary>
    ''' Gets or sets the text file to be output.
    ''' </summary>
    Property Filename() As String

    ''' <summary>
    ''' Settings for configured entities
    ''' </summary>
    Property Settings() As String

End Interface

#End Region

Public Enum Affiliation
    Friendly
    Hostile
    Neutral
End Enum

Public Class OutputSettings
    Public Sub New(ByVal affiliation As Affiliation, ByVal symbology As String)
        m_affiliation = affiliation
        m_symbology = symbology
    End Sub

    Public Property Affiliation() As Affiliation
        Get
            Affiliation = m_affiliation
        End Get
        Set(ByVal value As Affiliation)
            m_affiliation = value
        End Set
    End Property

    Public Property Symbology() As String
        Get
            Symbology = m_symbology
        End Get
        Set(ByVal value As String)
            m_symbology = value
        End Set
    End Property

    Private m_affiliation As Affiliation
    Private m_symbology As String
End Class

''' <summary>
''' An output plug-in which write to a file which can be read back
''' int with OutputTextFile.
''' 
''' This class implements IProvideTextFile, which itself
''' derives from IAgRtProvideTrackingData and IAgAttrConfig.
''' IAgRtProvideTrackingData is the minimum interface necessary for defining
''' an output plugin.  IAgAttrConfig is implemented to allow for saving and
''' loading of configuration via AgRt3FileOps.  It also allows for AGI based
''' products to automatically generate a simple user interface for
''' configuring options.  IProvideTextFile is the IDispatch
''' interface which defines the configuration options.  It is
''' required in order for IAgAttrConfig to have late-bound access to class
''' properties for OutputTextFile.
''' 
''' To use this class in RT3, simply compile the library and load the
''' included reg file into the Windows registry.
''' 
''' For more information on RT3 interfaces and class, please see the RT3
''' Development Kit documentation.
''' </summary>
<ComClass(OutputTextFile.ClassId, _
          OutputTextFile.InterfaceId)> _
Public Class OutputTextFile
    Implements IProvideTextFile

#Region "OutputTextFile Members"

    ''' <summary>
    ''' Set how a specified entity will be output.
    ''' </summary>
    ''' <param name="entityID">The entity ID</param>
    ''' <param name="settings">The configuration to use</param>
    Public Sub SetConfiguraiton(ByVal entityID As String, ByRef settings As OutputSettings)
        m_settings(entityID) = settings
    End Sub

    ''' <summary>
    ''' Get how a specified entity will be output.
    ''' ''' </summary>
    ''' <param name="entityID">The entity ID</param>
    ''' <param name="settings">The configuration being used</param>
    ''' <returns>True if a configuration existed</returns>
    Public Function GetConfiguration(ByVal entityID As String, ByRef settings As OutputSettings) As Boolean
        GetConfiguration = m_settings.TryGetValue(entityID, settings)
    End Function

#End Region

#Region "IProvideTextFile Members"
    ''' <summary>
    ''' Gets or sets the text file to be output.
    ''' </summary>
    Public Property Filename() As String Implements IProvideTextFile.Filename
        Get
            Filename = m_filename
        End Get
        Set(ByVal value As String)
            m_filename = value
        End Set
    End Property

    ''' <summary>
    ''' Settings for configured entities
    ''' </summary>
    Public Property Settings() As String Implements IProvideTextFile.Settings
        Get
            Dim configurationString As New StringBuilder()
            For Each item As KeyValuePair(Of String, OutputSettings) In m_settings

                configurationString.Append(item.Key)
                configurationString.Append(" ")
                configurationString.Append(CType(item.Value.Affiliation, Integer))
                configurationString.Append(" ")
                configurationString.AppendLine(item.Value.Symbology)
            Next
            Return configurationString.ToString()
        End Get
        Set(ByVal value As String)
            m_settings.Clear()

            Dim stringReader As New StringReader(value)
            Dim line As String = stringReader.ReadLine()
            While Not line Is Nothing
                Dim tmp As String() = line.Split(CChar(" "))
                Dim settings As New OutputSettings(CType(Integer.Parse(tmp(1)), Affiliation), tmp(2))
                m_settings(tmp(0)) = settings
                line = stringReader.ReadLine()
            End While
        End Set
    End Property
#End Region

#Region "IAgAttrConfig Members"
    ''' <summary>
    ''' Gets the configuration options for this plug-in.
    ''' </summary>
    ''' <param name="AttrBuilder">The helper classes used to build the options.</param>
    ''' <returns>An instance of the object returned by AttrBuilder.NewScope() populated with options.</returns>
    Public Function GetConfig(ByVal AttrBuilder As Attr.AgAttrBuilder) As Object Implements Attr.IAgAttrConfig.GetConfig
        'If this is the first time, we need to create the options
        'which we will then cache for future use.
        If m_options Is Nothing Then
            m_options = AttrBuilder.NewScope()

            'Expose a string option for the filename
            AttrBuilder.AddStringDispatchProperty(m_options, _
                "Filename", _
                "Filename", _
                "Filename", _
                AgEAttrAddFlags.eAddFlagNone)

            'Serialized version of m_settings
            AttrBuilder.AddMultiLineStringDispatchProperty(m_options, _
                "Settings", _
                "Settings", _
                "Settings", _
                AgEAttrAddFlags.eAddFlagNone)
        End If

        GetConfig = m_options
    End Function
#End Region

#Region "IAgRtProvideTrackingData Members"

    ''' <summary>
    ''' Gets the type name of this class.
    ''' </summary>
    Public ReadOnly Property Name() As String Implements IAgRtPlugin.Name, IAgRtProvideTrackingData.Name
        Get
            Name = m_typeName
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the name for a specific instance of this class.
    ''' </summary>
    Public Property InstanceName() As String Implements IAgRtPlugin.InstanceName, IAgRtProvideTrackingData.InstanceName
        Get
            InstanceName = m_instanceName
        End Get
        Set(ByVal value As String)
            m_instanceName = value
        End Set
    End Property

    ''' <summary>
    ''' Throws a COMException if the provider is not properly configured
    ''' and therefore unable to be started.
    ''' </summary>
    Public Sub Validate() Implements IAgRtProvideTrackingData.Validate
        'Always valid
    End Sub

    ''' <summary>
    ''' Prepares the plugin for output.
    ''' </summary>
    Public Sub Initialize() Implements IAgRtProvideTrackingData.Initialize
        If Not m_active Then
            m_active = True
            m_textWriter = File.CreateText(m_filename)
            m_timeLastWrite = DateTime.UtcNow
        End If

    End Sub

    ''' <summary>
    ''' Stops output
    ''' </summary>
    Public Sub Uninitialize() Implements IAgRtProvideTrackingData.Uninitialize
        If m_active Then
            m_active = False
            m_textWriter.Dispose()
            m_textWriter = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Write's the specified entity to the text file
    ''' </summary>
    ''' <param name="entity"></param>
    Public Sub OutputEntity(ByVal Entity As IAgEntity) Implements IAgRtProvideTrackingData.OutputEntity
        Dim utcNow As DateTime = DateTime.UtcNow
        Dim millisecondSinceLastWrite As Integer = CInt((utcNow - m_timeLastWrite).TotalMilliseconds)
        m_timeLastWrite = utcNow

        Dim settings As OutputSettings = Nothing
        If Not m_settings.TryGetValue(Entity.ID, settings) Then
            settings = New OutputSettings(Affiliation.Friendly, "SFAPMF----*****")
        End If

        Dim pointEntity As AgPointEntity = CType(Entity, AgPointEntity)
        Dim entry As New StringBuilder()
        entry.Append(pointEntity.DisplayName)
        entry.Append(" ")
        entry.Append(Math.Round(pointEntity.Position.Latitude, 6))
        entry.Append(" ")
        entry.Append(Math.Round(pointEntity.Position.Longitude, 6))
        entry.Append(" ")
        entry.Append(Math.Round(pointEntity.Position.Altitude, 6))
        entry.Append(" ")
        entry.Append(settings.Affiliation)
        entry.Append(" ")
        entry.Append(settings.Symbology)
        entry.Append(" ")
        entry.Append(millisecondSinceLastWrite)

        m_textWriter.WriteLine(entry.ToString())
    End Sub

#End Region

#Region "Variables, Constants, Events & Delegates"

    'Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
    'your class using GUIDGen, located in "Tools->Create GUID" in Visual
    'Studio or http://www.guidgen.com/
    Public Const ClassId As String = "38A20BA0-CF2B-4086-96BF-5043D13F5F15"
    Public Const InterfaceId As String = "72E38AC5-DC58-43f7-90D6-2498447FDD15"

    'Each RT3 plug-in must have it's own descriptive type-name which RT3
    'will use to distinguish it from other plug-ins.  For GUI based
    'applications, this is the name which will be shown to the end-user.
    Private Const m_typeName As String = "Example Text File Output"

    Private m_active As Boolean = False
    Private m_options As Object
    Private m_filename As String
    Private m_instanceName As String
    Private m_textWriter As TextWriter
    Private m_timeLastWrite As DateTime
    Private m_settings As New Dictionary(Of String, OutputSettings)
#End Region

End Class
