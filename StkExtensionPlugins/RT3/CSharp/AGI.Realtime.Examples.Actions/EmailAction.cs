using System;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using AGI.Attr;
using AGI.Entity;
using AGI.Realtime.Tracking;

namespace AGI.Realtime.Examples.Actions
{
    #region IEmailAction

    /// <summary>
    /// An IDispatch interface that defines parameters for sending an email
    /// over an SMTP server when an event is triggered.  This interface exists
    /// to satisfy IAgAttrConfig, which you will read more about below.  Your
    /// plug-in should also have a corresponding interface with any
    /// configuration options exposed.  It should be marked as InterfaceIsDual
    /// and have a unique GUID.  Generate a new GUID your class using GUIDGen,
    /// located in "Tools->Create GUID" in Visual Studio or use
    /// http://www.guidgen.com/.
    /// </summary>
    [Guid("88296560-8907-400a-A2E9-4CE1C98D606A")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IEmailAction : IAgRt3Action, IAgAttrConfig
    {
        /// <summary>
        /// Gets or sets the recipient of the email.
        /// </summary>
        string ToAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sender of the email.
        /// </summary>
        string FromAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the SMTP server to use to send the email.
        /// </summary>
        string SmtpServer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets  wether or not one email should be sent for all
        /// events, or if an email should be sent for each event.
        /// </summary>
        bool OnlyFireOnce
        {
            get;
            set;
        }
    }
    #endregion

    /// <summary>
    /// An Action which generates an email each time the event it is associated
    /// with is fired.
    /// 
    /// This class implements IEmailAction, which itself derives from
    /// IAgRt3Action and IAgAttrConfig.  IAgRt3Action is the minimum interface
    /// necessary for defining an event.  IAgAttrConfig is implemented to allow
    /// for saving and loading of configuration via AgRt3FileOps.  It also
    /// allows for AGI based products to automatically generate a simple user
    /// interface for configuring options.  IEmailAction is the IDispatch
    /// interface which defines the configuration options.  It is required in
    /// order for IAgAttrConfig to have late-bound access to class properties.
    /// If you are implementing a custom solution that does not generic
    /// need save and load of RT3 options, IAgRt3Action is the only needed
    /// interface.
    /// 
    /// To use this class in RT3, simply compile the library and load the
    /// included reg file into the Windows registry.
    /// 
    /// For more information on RT3 interfaces and class, please see the RT3
    /// Development Kit documentation.
    /// </summary>
    [Guid(EmailAction.ClassID),
     ProgId(EmailAction.ProgID),
     ClassInterface(ClassInterfaceType.None)]
    public class EmailAction : IEmailAction
    {
        #region IEmailAction Members

        /// <summary>
        /// Gets or sets the recipient of the email.
        /// </summary>
        public string ToAddress
        {
            get
            {
                return m_toEmailAdress;
            }
            set
            {
                m_toEmailAdress = value;
            }
        }

        /// <summary>
        /// Gets or sets the sender of the email.
        /// </summary>
        public string FromAddress
        {
            get
            {
                return m_fromEmailAdress;
            }
            set
            {
                m_fromEmailAdress = value;
            }
        }

        /// <summary>
        /// Gets or sets the SMTP server to use to send the email.
        /// </summary>
        public string SmtpServer
        {
            get
            {
                return m_smtpServer;
            }
            set
            {
                m_smtpServer = value;
            }
        }

        /// <summary>
        /// Gets or sets  wether or not one email should be sent for all
        /// events, or if an email should be sent for each event.
        /// </summary>
        public bool OnlyFireOnce
        {
            get
            {
                return m_fireOnce;
            }
            set
            {
                m_fireOnce = value;
            }
        }

        #endregion

        #region IAgAttrConfig Members

        /// <summary>
        /// Gets the configuration options for this plug-in.
        /// </summary>
        /// <param name="AttrBuilder">The helper classes used to build the options.</param>
        /// <returns>An instance of the object returned by AttrBuilder.NewScope() populated with options.</returns>
        public object GetConfig(AgAttrBuilder AttrBuilder)
        {
            //If this is the first time, we need to create the options
            //which we will then cache for future use.
            if (m_options == null)
            {
                m_options = AttrBuilder.NewScope();

                //Expose a string option for the FromAddress property
                AttrBuilder.AddStringDispatchProperty(m_options,
                                    "FromAddress",
                                    "FromAddress",
                                    "FromAddress",
                                    (int)AGI.Attr.AgEAttrAddFlags.eAddFlagNone);

                //Expose a string option for the ToAddress property
                AttrBuilder.AddStringDispatchProperty(m_options,
                                    "ToAddress",
                                    "ToAddress",
                                    "ToAddress",
                                    (int)AGI.Attr.AgEAttrAddFlags.eAddFlagNone);

                //Expose a string option for the SmtpServer property
                AttrBuilder.AddStringDispatchProperty(m_options,
                                    "SMTPServer",
                                    "SmtpServer",
                                    "SmtpServer",
                                    (int)AGI.Attr.AgEAttrAddFlags.eAddFlagNone);

                //Expose a boolean option for the OnlyFireOnce property
                AttrBuilder.AddBoolDispatchProperty(m_options,
                                    "OnlyFireOnce",
                                    "OnlyFireOnce",
                                    "OnlyFireOnce",
                                    (int)AGI.Attr.AgEAttrAddFlags.eAddFlagNone);
            }

            return m_options;
        }

        #endregion

        #region IAgRt3Action Members

        /// <summary>
        /// Gets the type name of this class.
        /// </summary>
        public string Name
        {
            get
            {
                return m_typeName;
            }
        }

        /// <summary>
        /// Gets or sets the name for a specific instance of this class.
        /// </summary>
        public string InstanceName
        {
            get
            {
                return m_instanceName;
            }
            set
            {
                m_instanceName = value;
            }
        }

        /// <summary>
        /// Connects to an SMTP mail sever and formats an email containing information
        /// about the triggered event.
        /// </summary>
        /// <param name="triggeredEvent">Information about the event which triggered the action.</param>
        public void Execute(IAgRtEventEntity triggeredEvent)
        {
            if (!m_fireOnce || (m_fireOnce && !m_eventFired))
            {
                m_eventFired = true;

                SmtpClient SmtpMail = new SmtpClient(m_smtpServer);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(m_fromEmailAdress);
                mail.To.Add(new MailAddress(m_toEmailAdress));
                mail.Subject = "RT3 Email Event Triggered";

                StringBuilder Message = new StringBuilder();
                Message.Append("An RT3 event has been triggered at ");
                Message.Append(triggeredEvent.Time.ToLocalTime().ToLongTimeString());
                Message.Append("\n");
                Message.Append("\n");
                Message.Append("Source: ");
                Message.Append(triggeredEvent.Source);
                Message.Append("\n");
                Message.Append("ID: ");
                Message.Append(triggeredEvent.ID);
                Message.Append("\n");
                Message.Append("Description: ");
                Message.Append(triggeredEvent.Description);
                Message.Append("\n");

                IAgEntityCollection Entities = triggeredEvent.MetaData.Get("Entities") as IAgEntityCollection;
                if (Entities != null)
                {
                    Message.Append("Entities: ");
                    foreach (IAgEntity entity in Entities)
                    {
                        Message.Append(entity.DisplayName);
                        Message.Append("");
                    }
                    Message.Append("\n");
                }

                mail.Body = Message.ToString();

                SmtpMail.Send(mail);
            }
        }

        #endregion

        #region Variables, Constants, Events & Delegates

        //Every RT3 plug-in needs to have a unique ProgID.  You should change
        //the below ProgID to match any new classes you create.
        public const string ProgID = "AGI.Realtime.Examples.Actions.EmailAction";

        //Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
        //your class using GUIDGen, located in "Tools->Create GUID" in Visual
        //Studio or http://www.guidgen.com/
        public const string ClassID = "D1CAAFB0-3768-4863-9DC1-9218E3195833";

        //Each RT3 plug-in must have it's own descriptive type-name which RT3
        //will use to distinguish it from other plug-ins.  For GUI based
        //applications, this is the name which will be shown to the end-user.
        private const string m_typeName = "Email";

        private bool m_fireOnce = true;
        private bool m_eventFired = false;
        private object m_options = null;
        private string m_fromEmailAdress = "Please enter the sender address.";
        private string m_toEmailAdress = "Please enter the destination address.";
        private string m_smtpServer = "Please enter the SMTP server.";
        private string m_instanceName;

        #endregion
    }
}
