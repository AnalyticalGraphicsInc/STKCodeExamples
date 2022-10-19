Imports System.IO
Imports System.Threading
Imports System.Runtime.InteropServices

Imports AGI.Attr
Imports AGI.Entity

#Region "IProvideEntitiesFromTextFile"

''' <summary>
''' An IDispatch interface that defines parameters for specifying a text
''' file for parsing.  This interface exists to satisfy IAgAttrConfig,
''' and IAgRtProvideEntitiesEvents which you will read more about below.
''' Your plug-in should also have a corresponding interface with any
''' configuration options exposed.  It should be marked as InterfaceIsDual
''' and have a unique GUID.  Generate a new GUID your class using GUIDGen,
''' located in "Tools->Create GUID" in Visual Studio or use
''' http://www.guidgen.com/.
''' </summary>
Public Interface IProvideEntitiesFromTextFile
    Inherits IAgRtProvideEntities
    Inherits IAgAttrConfig

    ''' <summary>
    ''' Gets or sets the path to the file.
    ''' </summary>
    Property Filename() As String

    'COM event calls are latebound, and therefore our events must match
    'the DispId of the orignal interface.  This block of code, along with
    'the event signatures above, can be copied unchanged into your new
    'provider plug-in.
    <DispId(AgERtEventDispatchID.eProviderStartEvent)> Event OnProviderStart(ByVal Provider As IAgRtProvideEntities)
    <DispId(AgERtEventDispatchID.eProviderStopEvent)> Event OnProviderStop(ByVal Provider As IAgRtProvideEntities)
End Interface

#End Region

''' <summary>
''' A provider which reads all of its data from the specified text file.
''' 
''' This class implements IProvideEntitiesFromTextFile, which itself
''' derives from IAgRtProvideEntities and IAgAttrConfig.
''' IAgRtProvideEntities is the minimum interface necessary for defining a
''' provider.  IAgAttrConfig is implemented to allow for saving and loading
''' of configuration via AgRt3FileOps.  It also allows for AGI based
''' products to automatically generate a simple user interface for
''' configuring options.  IProvideEntitiesFromTextFile is the IDispatch
''' interface which defines the configuration options and events.  It is
''' required in order for IAgAttrConfig to have late-bound access to class
''' properties and for ProvideEntitiesFromTextFile to properly implement
''' IAgRtProvideEntitiesEvents.
''' 
''' To use this class in RT3, simply compile the library and load the
''' included reg file into the Windows registry.
''' 
''' For more information on RT3 interfaces and class, please see the RT3
''' Development Kit documentation.
''' </summary>
<ComClass(ProvideEntitiesFromTextFile.ClassId, _
          ProvideEntitiesFromTextFile.InterfaceId, _
          ProvideEntitiesFromTextFile.IAgRtProvideEntitiesEventsId)> _
Public Class ProvideEntitiesFromTextFile
    Implements IProvideEntitiesFromTextFile

#Region "IProvideEntitiesFromTextFile Members"
    ''' <summary>
    ''' Gets or sets the text file to be read by the provider.
    ''' </summary>
    Public Property Filename() As String Implements IProvideEntitiesFromTextFile.Filename
        Get
            Filename = m_filename
        End Get
        Set(ByVal value As String)
            m_filename = value
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
        'hich we will then cache for future use.
        If m_options Is Nothing Then
            m_options = AttrBuilder.NewScope()

            'Expose a string option for the filename
            AttrBuilder.AddStringDispatchProperty(m_options, _
                "Filename", _
                "Filename", _
                "Filename", _
                AgEAttrAddFlags.eAddFlagNone)
        End If
        GetConfig = m_options
    End Function
#End Region

#Region "IAgRtProvideEntities Members"
    ''' <summary>
    ''' Gets the type name of this class.
    ''' </summary>
    Public ReadOnly Property Name() As String Implements IAgRtPlugin.Name, IAgRtProvideEntities.Name
        Get
            Name = m_typeName
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the name for a specific instance of this class.
    ''' </summary>
    Public Property InstanceName() As String Implements IAgRtPlugin.InstanceName, IAgRtProvideEntities.InstanceName
        Get
            InstanceName = m_instanceName
        End Get
        Set(ByVal value As String)
            m_instanceName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the ID associated with the instance of this provider.
    ''' This will be set automatically by RT3 when the plug-in is registered
    ''' with the AgRt3Application class.
    ''' </summary>
    Public Property ID() As String Implements IAgRtProvideEntities.ID
        Get
            ID = m_id
        End Get
        Set(ByVal value As String)
            m_id = value
        End Set
    End Property

    ''' <summary>
    ''' Indicates if the provider is currently processing data or not.
    ''' </summary>
    Public ReadOnly Property Active() As Boolean Implements IAgRtProvideEntities.Active
        Get
            Active = m_active
        End Get
    End Property

    ''' <summary>
    ''' Gets the array of metadata fields that will be used by the
    ''' provider.  This enables other objects, for example
    ''' AgRt3SimpleQuery, to determine available fields, and their type,
    ''' at runtime.
    ''' </summary>
    Public ReadOnly Property MetaDataDictionary() As System.Array Implements IAgRtProvideEntities.MetaDataDictionary
        Get
            If m_metaDataDictionary Is Nothing Then
                Dim Values As New ArrayList

                'Add a field that will indicate friend or hostile.
                Dim Entry As New AgRtDataDictionaryEntry
                Entry.Name = "Affiliation"
                Entry.Sample = "FRI"
                Values.Add(Entry)

                'Add a field that will hold the 16 character Mil2525b symbology
                Entry = New AgRtDataDictionaryEntry
                Entry.Name = "Symbology"
                Entry.Sample = "SFAPMF----*****"
                Values.Add(Entry)

                m_metaDataDictionary = Values.ToArray()
            End If

            MetaDataDictionary = m_metaDataDictionary
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the collection to be used by the provider to store the
    ''' entities it creates.  This will be set automatically by RT3 when
    ''' the plug-in is registered with the AgRt3Application class.
    ''' </summary>
    Public Property Entities() As Entity.IAgEntityCollection Implements IAgRtProvideEntities.Entities
        Get
            Entities = m_entities
        End Get
        Set(ByVal value As Entity.IAgEntityCollection)
            m_entities = value
        End Set
    End Property

    ''' <summary>
    ''' Starts the provider processing.  This particular provider creates
    ''' a thread, but a callback or timer could be just as easily used.
    ''' It is very important that start returns after initialization, it cannot
    ''' simply perform it's work in this function and loop forever.
    ''' </summary>
    Public Sub Start() Implements IAgRtProvideEntities.Start
        If Not m_active Then
            Validate()
            m_threadControl = New Thread(New ThreadStart(AddressOf ThreadFunction))
            m_threadControl.SetApartmentState(ApartmentState.MTA)
            m_active = True
            m_threadControl.Start()
        End If
    End Sub

    ''' <summary>
    ''' Terminates processing and restores it to the ready state.
    ''' </summary>
    Public Sub [Stop]() Implements IAgRtProvideEntities.Stop
        If m_active Then
            m_active = False
            m_threadControl.Join()
            m_threadControl = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Throws a COMException if the provider is not properly configured
    ''' and therefore unable to be started.
    ''' </summary>
    Public Sub Validate() Implements IAgRtProvideEntities.Validate
        If m_entities Is Nothing Then
            Throw New COMException("Entities property has not been set.")
        Else
            If Not File.Exists(m_filename) Then
                Throw New COMException("The file: " + m_filename + "does not exist.")
            End If
        End If
    End Sub
#End Region

#Region "ProvideEntitiesFromTextFile Members"
    ''' <summary>
    ''' ThreadFunction is where all of the actual substance happens.
    ''' Using the Stream object, we read a line of data and parse
    ''' it for the entity ID, Position and our meta-data.
    ''' This data is in a simple format laid out as follows.
    ''' Each line is 80 characters plus linefeed and carriage return
    ''' Each token is seperated by a single whitespace with the order being
    ''' NAME LAT LON ALT AFFILIATION MIL2525B TIMETONEXTUPDATE
    ''' ReferenceProviderData.txt in the Samples\Data directory contains
    ''' sample data which can be used to run the plug-in.
    ''' </summary>
    Sub ThreadFunction()
        'If we have any subscribers, generate the start event
        RaiseEvent OnProviderStart(Me)

        While m_active
            'Open the text file for reading
            Using textReader As New StreamReader(m_filename)
                Dim line As String = textReader.ReadLine()
                While m_active And Not (line Is Nothing)
                    'Parse a line
                    Dim elements As String() = textReader.ReadLine().Split ' '
                    Dim entityName As String = elements(0)
                    Dim latitude As Double = Convert.ToDouble(elements(1))
                    Dim longitude As Double = Convert.ToDouble(elements(2))
                    Dim altitude As Double = Convert.ToDouble(elements(3))
                    Dim affiliation As String = elements(4)
                    Dim mil2525b As String = elements(5)
                    Dim timeDelay As Integer = Convert.ToInt32(elements(6))

                    'Now we'll see if the entity has already been
                    'inserted into the system.  If it has, we simply
                    'update it and call CommitUpdate, if it hasn't
                    'we need to call Add
                    Dim NewEntity As Boolean = False
                    Dim Entity As IAgEntity
                    Dim PointEntity As IAgPointEntity
                    Entity = Entities.Find(entityName)
                    If Entity Is Nothing Then
                        PointEntity = DirectCast(New AgPointEntity(), IAgPointEntity)
                        Entity = PointEntity
                        Entity.ID = entityName
                        PointEntity.Position = DirectCast(New AgEntityPosition(), IAgEntityPosition)
                        NewEntity = True
                    Else
                        PointEntity = CType(Entity, IAgPointEntity)
                    End If

                    'Our data does not have time so we always assume the
                    'position time is now.  RT3 uses UTC.
                    Entity.Time = DateTime.UtcNow

                    'Set the position
                    PointEntity.Position.Set(latitude, longitude, altitude)

                    'Set the meta-data
                    Dim metaData As IAgEntityMetaDataCollection = Entity.MetaData
                    metaData.Set("Affiliation", affiliation)
                    metaData.Set("Symbology", mil2525b)

                    'Call add or update
                    If NewEntity Then
                        Entities.Add(Entity)
                    Else
                        PointEntity.CommitUpdate(AgEEntityUpdate.eEntityUpdate)
                    End If

                    'Sleep the desired wait time
                    If (timeDelay > 0) Then
                        Thread.Sleep(timeDelay)
                    End If

                    line = textReader.ReadLine()
                End While

                'Since we're just reading from a file, when we hit the
                'end we'll remove everything and then start over.
                'This way we never run out of sample data.
                Entities.RemoveAll()
                Thread.Sleep(1000)
            End Using
        End While
        'When we stop, make sure we trigger the OnProviderStop event.
        RaiseEvent OnProviderStop(Me)
    End Sub

#End Region

#Region "Variables, Constants, Events & Delegates"
    <DispId(AgERtEventDispatchID.eProviderStartEvent)> Public Event OnProviderStart(ByVal Provider As IAgRtProvideEntities) Implements IProvideEntitiesFromTextFile.OnProviderStart
    <DispId(AgERtEventDispatchID.eProviderStopEvent)> Public Event OnProviderStop(ByVal Provider As IAgRtProvideEntities) Implements IProvideEntitiesFromTextFile.OnProviderStop

    'Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
    'your class using GUIDGen, located in "Tools->Create GUID" in Visual
    'Studio or http://www.guidgen.com/
    Public Const ClassId As String = "FD617E53-41BA-4dce-9E43-C77E151D36B7"
    Public Const InterfaceId As String = "0C3D26FC-0B04-4736-B15C-D9DE11734880"

    'The EventsID MUST match the GUID for IAgRtProvideEntitiesEvents
    Public Const IAgRtProvideEntitiesEventsId As String = "B002820F-48CE-4395-95F6-DC87799A6F5A"

    'Each RT3 plug-in must have it's own descriptive type-name which RT3
    'will use to distinguish it from other plug-ins.  For GUI based
    'applications, this is the name which will be shown to the end-user.
    Private Const m_typeName As String = "Example Text File Provider"

    Private m_metaDataDictionary As Array
    Private m_active As Boolean = False
    Private m_entities As IAgEntityCollection
    Private m_options As Object
    Private m_id As String
    Private m_filename As String
    Private m_instanceName As String
    Private m_threadControl As Thread
#End Region

End Class
