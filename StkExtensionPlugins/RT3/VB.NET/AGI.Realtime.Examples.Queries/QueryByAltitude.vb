Imports System.Collections.Generic
Imports System.Runtime.InteropServices

Imports AGI.Attr
imports AGI.Entity
imports AGI.Realtime.Tracking

#Region "IQueryByAltitude"
''' <summary>
''' An IDispatch interface that defines parameters for specifying a query
''' which has a minimum and maximum altitude.  This interface exists to
''' satisfy IAgAttrConfig, and IAgRt3QueryEvents which you will read more
''' about below.  Your plug-in should also have a corresponding interface
''' with any configuration options exposed.  It should be marked as
''' InterfaceIsDual and have a unique GUID.  Generate a new GUID your class
''' using GUIDGen, located in "Tools->Create GUID" in Visual Studio or use
''' http://www.guidgen.com/.
''' </summary>
Public Interface IQueryByAltitude
    Inherits IAgRt3Query
    Inherits IAgAttrConfig

    ''' <summary>
    ''' Gets or sets the minimum altitude
    ''' </summary>
    Property MinimumAltitude() As Double

    ''' <summary>
    ''' Gets or sets the maximum altitude
    ''' </summary>
    Property MaximumAltitude() As Double

    'COM event calls are latebound, and therefore our events must match
    'the DispId of the orignal interface.  This block of code, along with
    'the event signatures above, can be copied unchanged into your new
    'query plug-in.
    <DispId(AgERt3EventDispatchID.eQueryChangedEvent)> Event OnQueryChanged(ByVal Query As IAgRt3Query)
    <DispId(AgERt3EventDispatchID.eQueryEnabledEvent)> Event OnQueryEnabled(ByVal Query As IAgRt3Query)
    <DispId(AgERt3EventDispatchID.eQueryDisabledEvent)> Event OnQueryDisabled(ByVal Query As IAgRt3Query)
End Interface

#End Region

''' <summary>
''' A query which matches against entities in the specified altitude range.
''' 
''' This class implements IQueryByAltitude, which itself
''' derives from IAgRt3Query and IAgAttrConfig. IAgRt3Query is the minimum
''' interface necessary for defining a query.  IAgAttrConfig is
''' implemented to allow for saving and loading of configuration via
''' AgRt3FileOps.  It also allows for AGI based products to automatically
''' generate a simple user interface for configuring options.
''' IQueryByAltitude is the IDispatch interface which defines the
''' configuration options and events.  It is required in order for
''' IAgAttrConfig to have late-bound access to class properties and for
''' QueryByAltitude to properly implement IAgRt3QueryEvents.
''' 
''' To use this class in RT3, simply compile the library and load the
''' included reg file into the Windows registry.
''' 
''' For more information on RT3 interfaces and class, please see the RT3
''' Development Kit documentation.
''' </summary>
<ComClass(QueryByAltitude.ClassId, _
          QueryByAltitude.InterfaceId, _
          QueryByAltitude.IAgRt3QueryEventsId)> _
Public Class QueryByAltitude
    Implements IQueryByAltitude

#Region "IQueryByAltitude Members"
    ''' <summary>
    ''' Gets or sets the minimum altitude
    ''' </summary>
    Public Property MinimumAltitude() As Double Implements IQueryByAltitude.MinimumAltitude
        Get
            MinimumAltitude = m_minimumAltitude
        End Get
        Set(ByVal value As Double)
            m_minimumAltitude = value
            RaiseEvent OnQueryChanged(Me)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the maximum altitude
    ''' </summary>
    Public Property MaximumAltitude() As Double Implements IQueryByAltitude.MaximumAltitude
        Get
            MaximumAltitude = m_maximumAltitude
        End Get
        Set(ByVal value As Double)
            m_maximumAltitude = value
            RaiseEvent OnQueryChanged(Me)
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

            'Expose a Quantity option, in meters, for MinimumAltitude
            AttrBuilder.AddQuantityDispatchProperty2(m_options, _
                "MinimumAltitude", _
                "MinimumAltitude", _
                "MinimumAltitude", _
                "Distance", _
                "Meters", _
                "Meters", _
                AgEAttrAddFlags.eAddFlagNone)

            'Expose a Quantity option, in meters, for MaximumAltitude
            AttrBuilder.AddQuantityDispatchProperty2(m_options, _
                "MaximumAltitude", _
                "MaximumAltitude", _
                "MaximumAltitude", _
                "Distance", _
                "Meters", _
                "Meters", _
                AgEAttrAddFlags.eAddFlagNone)
        End If
        GetConfig = m_options
    End Function
#End Region

#Region "IAgRt3Query Members"
    ''' <summary>
    ''' Gets the type name of this class.
    ''' </summary>
    Public ReadOnly Property Name() As String Implements IAgRtPlugin.Name, Tracking.IAgRt3Query.Name
        Get
            Name = m_typeName
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the name for a specific instance of this class.
    ''' </summary>
    Public Property InstanceName() As String Implements IAgRtPlugin.InstanceName, Tracking.IAgRt3Query.InstanceName
        Get
            InstanceName = m_instanceName
        End Get
        Set(ByVal value As String)
            m_instanceName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the instance of AgRt3QueryGraphics associated with
    ''' this query.  This will be set automatically by RT3 when the
    ''' plug-in is registered with the AgRt3EntityManager class.
    ''' </summary>
    Public Property Graphics() As Tracking.IAgRt3QueryGraphics Implements Tracking.IAgRt3Query.Graphics
        Get
            Graphics = m_graphics
        End Get
        Set(ByVal value As Tracking.IAgRt3QueryGraphics)
            m_graphics = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the provider plug-in this query is associated with.
    ''' This will be set automatically by RT3 when the plug-in is
    ''' registered with the AgRt3EntityManager class.
    ''' </summary>
    ''' <value></value>
    Public Property Provider() As IAgRtProvideEntities Implements Tracking.IAgRt3Query.Provider
        Get
            Provider = m_provider
        End Get
        Set(ByVal value As IAgRtProvideEntities)
            m_provider = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the enabled property which indicates if the query
    ''' should be considered when distributing entities.  If false, no
    ''' entities will be matched against this query.
    ''' </summary>
    Public Property Enabled() As Boolean Implements Tracking.IAgRt3Query.Enabled
        Get
            Enabled = m_enabled
        End Get
        Set(ByVal value As Boolean)
            m_enabled = value

            If (m_enabled) Then
                RaiseEvent OnQueryEnabled(Me)
            Else
                RaiseEvent OnQueryDisabled(Me)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Called by the RT3 engine to determine if the provided entity
    ''' belongs in this query.
    ''' </summary>
    ''' <param name="Entity">The entity to be matched against.</param>
    ''' <returns>True if the entity matches this query criteria, false otherwise.</returns>
    Public Function IsMatch(ByVal Entity As Entity.IAgEntity) As Boolean Implements Tracking.IAgRt3Query.IsMatch
        Dim Result As Boolean = False

        If TypeOf (Entity) Is IAgPointEntity Then
            Dim Point As IAgPointEntity = CType(Entity, IAgPointEntity)
            Result = (Point.Position.Altitude >= m_minimumAltitude) And (Point.Position.Altitude <= m_maximumAltitude)
        End If

        If TypeOf (Entity) Is IAgPolylineEntity Then
            Dim Poly As IAgPolylineEntity = CType(Entity, IAgPolylineEntity)
            For Each Position As IAgEntityPosition In Poly.Positions
                Result = (Position.Altitude >= m_minimumAltitude) And (Position.Altitude <= m_maximumAltitude)
                If Result Then
                    Exit For
                End If
            Next
        End If

        IsMatch = Result
    End Function
#End Region

#Region "Variables, Constants, Events & Delegates"

    'Events defined by IAgRt3QueryEvents
    <DispId(AgERt3EventDispatchID.eQueryChangedEvent)> Public Event OnQueryChanged(ByVal Query As Tracking.IAgRt3Query) Implements IQueryByAltitude.OnQueryChanged
    <DispId(AgERt3EventDispatchID.eQueryDisabledEvent)> Public Event OnQueryDisabled(ByVal Query As Tracking.IAgRt3Query) Implements IQueryByAltitude.OnQueryDisabled
    <DispId(AgERt3EventDispatchID.eQueryEnabledEvent)> Public Event OnQueryEnabled(ByVal Query As Tracking.IAgRt3Query) Implements IQueryByAltitude.OnQueryEnabled

    'Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
    'your class using GUIDGen, located in "Tools->Create GUID" in Visual
    'Studio or http://www.guidgen.com/
    Public Const ClassId As String = "E96D1CBC-24F4-4892-B0C2-1347A4B0CA5A"
    Public Const InterfaceId As String = "0006BFA4-0547-4179-AB6A-C51306BE0A0C"

    'The EventsID MUST match the GUID for IAgRt3QueryEvents
    Public Const IAgRt3QueryEventsId As String = "DDA4A06B-18CC-4953-9F81-E6E0DED2A054"

    'Each RT3 plug-in must have it's own descriptive type-name which RT3
    'will use to distinguish it from other plug-ins.  For GUI based
    'applications, this is the name which will be shown to the end-user.
    Private Const m_typeName As String = "Altitude Query"

    Private m_enabled As Boolean = False
    Private m_minimumAltitude As Double = 0
    Private m_maximumAltitude As Double = 1000
    Private m_graphics As IAgRt3QueryGraphics
    Private m_provider As IAgRtProvideEntities
    Private m_options As Object
    Private m_instanceName As String
#End Region
End Class
