Imports System.Net.Mail
Imports System.Text

Imports AGI.Attr
Imports AGI.Entity
Imports AGI.Realtime.Tracking


#Region "IEmailAction"

''' <summary>
''' An IDispatch interface that defines parameters for sending an email
''' over an SMTP server when an event is triggered.  This interface exists
''' to satisfy IAgAttrConfig, which you will read more about below.  Your
''' plug-in should also have a corresponding interface with any
''' configuration options exposed.  It should be marked as InterfaceIsDual
''' and have a unique GUID.  Generate a new GUID your class using GUIDGen,
''' located in "Tools->Create GUID" in Visual Studio or use
''' http://www.guidgen.com/.
''' </summary>
Public Interface IEmailAction
    Inherits IAgRt3Action
    Inherits IAgAttrConfig

    ''' <summary>
    ''' Gets or sets the recipient of the email.
    ''' </summary>
    Property ToAddress() As String

    ''' <summary>
    ''' Gets or sets the sender of the email.
    ''' </summary>
    Property FromAddress() As String

    ''' <summary>
    ''' Gets or sets the SMTP server to use to send the email.
    ''' </summary>
    Property SmtpServer() As String

    ''' <summary>
    ''' Gets or sets  wether or not one email should be sent for all
    ''' events, or if an email should be sent for each event.
    ''' </summary>
    Property OnlyFireOnce() As Boolean

End Interface

#End Region

''' <summary>
''' An Action which generates an email each time the event it is associated
''' with is fired.
''' 
''' This class implements IEmailAction, which itself derives from
''' IAgRt3Action and IAgAttrConfig.  IAgRt3Action is the minimum interface
''' necessary for defining an event.  IAgAttrConfig is implemented to allow
''' for saving and loading of configuration via AgRt3FileOps.  It also
''' allows for AGI based products to automatically generate a simple user
''' interface for configuring options.  IEmailAction is the IDispatch
''' interface which defines the configuration options.  It is required in
''' order for IAgAttrConfig to have late-bound access to class properties.
''' If you are implementing a custom solution that does not generic
''' need save and load of RT3 options, IAgRt3Action is the only needed
''' interface.
''' 
''' To use this class in RT3, simply compile the library and load the
''' included reg file into the Windows registry.
''' 
''' For more information on RT3 interfaces and class, please see the RT3
''' Development Kit documentation.
''' </summary>
<ComClass(EmailAction.ClassId, _
          EmailAction.InterfaceId)> _
Public Class EmailAction
    Implements IEmailAction

#Region "IEmailAction Members"
    ''' <summary>
    ''' Gets or sets the recipient of the email.
    ''' </summary>
    Public Property ToAddress() As String Implements IEmailAction.ToAddress
        Get
            ToAddress = m_toEmailAdress
        End Get
        Set(ByVal value As String)
            m_toEmailAdress = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the sender of the email.
    ''' </summary>
    Public Property FromAddress() As String Implements IEmailAction.FromAddress
        Get
            FromAddress = m_fromEmailAdress
        End Get
        Set(ByVal value As String)
            m_fromEmailAdress = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the SMTP server to use to send the email.
    ''' </summary>
    Public Property SmtpServer() As String Implements IEmailAction.SmtpServer
        Get
            SmtpServer = m_smtpServer
        End Get
        Set(ByVal value As String)
            m_smtpServer = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets  wether or not one email should be sent for all
    ''' events, or if an email should be sent for each event.
    ''' </summary>
    Public Property OnlyFireOnce() As Boolean Implements IEmailAction.OnlyFireOnce
        Get
            OnlyFireOnce = m_fireOnce
        End Get
        Set(ByVal value As Boolean)
            m_fireOnce = value
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

            'Expose a string option for the FromAddress property
            AttrBuilder.AddStringDispatchProperty(m_options, _
                                "FromAddress", _
                                "FromAddress", _
                                "FromAddress", _
                                AGI.Attr.AgEAttrAddFlags.eAddFlagNone)

            'Expose a string option for the ToAddress property
            AttrBuilder.AddStringDispatchProperty(m_options, _
                                "ToAddress", _
                                "ToAddress", _
                                "ToAddress", _
                                AGI.Attr.AgEAttrAddFlags.eAddFlagNone)

            'Expose a string option for the SmtpServer property
            AttrBuilder.AddStringDispatchProperty(m_options, _
                                "SMTPServer", _
                                "SmtpServer", _
                                "SmtpServer", _
                                AGI.Attr.AgEAttrAddFlags.eAddFlagNone)

            'Expose a boolean option for the OnlyFireOnce property
            AttrBuilder.AddBoolDispatchProperty(m_options, _
                                "OnlyFireOnce", _
                                "OnlyFireOnce", _
                                "OnlyFireOnce", _
                                AGI.Attr.AgEAttrAddFlags.eAddFlagNone)
        End If
        GetConfig = m_options
    End Function
#End Region

#Region "IAgRt3Action Members"
    ''' <summary>
    ''' Gets the type name of this class.
    ''' </summary>
    Public ReadOnly Property Name() As String Implements IAgRtPlugin.Name, Tracking.IAgRt3Action.Name
        Get
            Name = m_typeName
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the name for a specific instance of this class.
    ''' </summary>
    Public Property InstanceName() As String Implements IAgRtPlugin.InstanceName, Tracking.IAgRt3Action.InstanceName
        Get
            InstanceName = m_instanceName
        End Get
        Set(ByVal value As String)
            m_instanceName = value
        End Set
    End Property

    ''' <summary>
    ''' Connects to an SMTP mail sever and formats an email containing information
    ''' about the triggered event.
    ''' </summary>
    ''' <param name="triggeredEvent">Information about the event which triggered the action.</param>
    Public Sub Execute(ByVal triggeredEvent As IAgRtEventEntity) Implements Tracking.IAgRt3Action.Execute
        If Not m_fireOnce Or (m_fireOnce And Not m_eventFired) Then
            m_eventFired = True

            Dim SmtpMail As New SmtpClient(m_smtpServer)
            Dim mail As New MailMessage()
            mail.From = New MailAddress(m_fromEmailAdress)
            mail.To.Add(New MailAddress(m_toEmailAdress))
            mail.Subject = "RT3 Email Event Triggered"

            Dim Message As New StringBuilder()
            Message.Append("An RT3 event has been triggered at ")
            Message.AppendLine(triggeredEvent.Time.ToLocalTime().ToLongTimeString())
            Message.AppendLine()
            Message.Append("Source: ")
            Message.AppendLine(triggeredEvent.Source)
            Message.Append("ID: ")
            Message.AppendLine(triggeredEvent.ID)
            Message.Append("Description: ")
            Message.AppendLine(triggeredEvent.Description)

            Dim Entities As IAgEntityCollection = CType(triggeredEvent.MetaData.Get("Entities"), IAgEntityCollection)
            If Not Entities Is Nothing Then
                Message.Append("Entities: ")
                For Each entity As IAgEntity In Entities
                    Message.Append(entity.DisplayName)
                    Message.Append("")
                Next
                Message.AppendLine()
            End If

            mail.Body = Message.ToString()
            SmtpMail.Send(mail)
        End If
    End Sub
#End Region

#Region "Variables, Constants, Events & Delegates"
    'Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
    'your class using GUIDGen, located in "Tools->Create GUID" in Visual
    'Studio or http://www.guidgen.com/
    Public Const ClassId As String = "BD012999-083A-4bb0-B577-58FB9751E854"
    Public Const InterfaceId As String = "028FFD53-5F77-49ec-A36E-A19849A64A64"

    'Each RT3 plug-in must have it's own descriptive type-name which RT3
    'will use to distinguish it from other plug-ins.  For GUI based
    'applications, this is the name which will be shown to the end-user.
    Private Const m_typeName As String = "Email"

    Private m_fireOnce As Boolean = True
    Private m_eventFired As Boolean = False
    Private m_options As Object
    Private m_fromEmailAdress As String = "Please enter the sender address."
    Private m_toEmailAdress As String = "Please enter the destination address."
    Private m_smtpServer As String = "Please enter the SMTP server."
    Private m_instanceName As String
#End Region

End Class
