using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

using AGI.Attr;
using AGI.Entity;

namespace AGI.Realtime.Examples.Providers
{
    #region IProvideEntitiesFromTextFile

    //In order to implement the events defined in IAgRtProvideEntitiesEvents,
    //which is required by all provider plug-ins, we must define the signature
    //of the events here.
    public delegate void OnProviderStartDelegate(IAgRtProvideEntities Sender);
    public delegate void OnProviderStopDelegate(IAgRtProvideEntities Sender);

    /// <summary>
    /// An IDispatch interface that defines parameters for specifying a text
    /// file for parsing.  This interface exists to satisfy IAgAttrConfig,
    /// and IAgRtProvideEntitiesEvents which you will read more about below.
    /// Your plug-in should also have a corresponding interface with any
    /// configuration options exposed.  It should be marked as InterfaceIsDual
    /// and have a unique GUID.  Generate a new GUID your class using GUIDGen,
    /// located in "Tools->Create GUID" in Visual Studio or use
    /// http://www.guidgen.com/.
    /// </summary>
    [Guid("CA378ED6-B64A-4bff-A109-8BA1ED1149C3")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IProvideEntitiesFromTextFile : IAgRtProvideEntities, IAgAttrConfig
    {
        /// <summary>
        /// Gets or sets the path to the file.
        /// </summary>
        string Filename
        {
            get;
            set;
        }


        //COM event calls are latebound, and therefore our events must match
        //the DispId of the orignal interface.  This block of code, along with
        //the event signatures above, can be copied unchanged into your new
        //provider plug-in.

        [DispId((int)AgERtEventDispatchID.eProviderStartEvent)]
        event OnProviderStartDelegate OnProviderStart;

        [DispId((int)AgERtEventDispatchID.eProviderStopEvent)]
        event OnProviderStopDelegate OnProviderStop;
    }

    #endregion

    /// <summary>
    /// A provider which reads all of its data from the specified text file.
    /// 
    /// This class implements IProvideEntitiesFromTextFile, which itself
    /// derives from IAgRtProvideEntities and IAgAttrConfig.
    /// IAgRtProvideEntities is the minimum interface necessary for defining a
    /// provider.  IAgAttrConfig is implemented to allow for saving and loading
    /// of configuration via AgRt3FileOps.  It also allows for AGI based
    /// products to automatically generate a simple user interface for
    /// configuring options.  IProvideEntitiesFromTextFile is the IDispatch
    /// interface which defines the configuration options and events.  It is
    /// required in order for IAgAttrConfig to have late-bound access to class
    /// properties and for ProvideEntitiesFromTextFile to properly implement
    /// IAgRtProvideEntitiesEvents.
    /// 
    /// To use this class in RT3, simply compile the library and load the
    /// included reg file into the Windows registry.
    /// 
    /// For more information on RT3 interfaces and class, please see the RT3
    /// Development Kit documentation.
    /// </summary>
    [Guid(ProvideEntitiesFromTextFile.ClassId),
     ProgId(ProvideEntitiesFromTextFile.ProgID),
     ClassInterface(ClassInterfaceType.None),
     ComSourceInterfaces(typeof(IAgRtProvideEntitiesEvents))]
    public class ProvideEntitiesFromTextFile : IProvideEntitiesFromTextFile
    {
        #region IProvideEntitiesFromTextFile Members
        /// <summary>
        /// Gets or sets the text file to be read by the provider.
        /// </summary>
        public string Filename
        {
            get
            {
                return m_filename;
            }
            set
            {
                m_filename = value;
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

                //Expose a string option for the filename
                AttrBuilder.AddStringDispatchProperty(m_options,
                    "Filename",
                    "Filename",
                    "Filename",
                    (int)AgEAttrAddFlags.eAddFlagNone);
            }

            return m_options;
        }

        #endregion

        #region IAgRtProvideEntities Members

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
        /// Gets or sets the ID associated with the instance of this provider.
        /// This will be set automatically by RT3 when the plug-in is registered
        /// with the AgRt3Application class.
        /// </summary>
        public string ID
        {
            get
            {
                return m_id;
            }
            set
            {
                m_id = value;
            }
        }

        /// <summary>
        /// Indicates if the provider is currently processing data or not.
        /// </summary>
        public bool Active
        {
            get
            {
                return m_active;
            }
        }

        /// <summary>
        /// Gets the array of metadata fields that will be used by the
        /// provider.  This enables other objects, for example
        /// AgRt3SimpleQuery, to determine available fields, and their type,
        /// at runtime.
        /// </summary>
        public Array MetaDataDictionary
        {
            get
            {
                if (m_metaDataDictionary == null)
                {
                    ArrayList Values = new ArrayList();

                    //Add a field that will indicate friend or hostile.
                    AgRtDataDictionaryEntry Entry = new AgRtDataDictionaryEntry();
                    Entry.Name = "Affiliation";
                    Entry.Sample = "Friendly";
                    Values.Add(Entry);

                    //Add a field that will hold the 16 character Mil2525b symbology
                    Entry = new AgRtDataDictionaryEntry();
                    Entry.Name = "Symbology";
                    Entry.Sample = "SFAPMF----*****";
                    Values.Add(Entry);

                    m_metaDataDictionary = Values.ToArray();
                }

                return m_metaDataDictionary;
            }
        }

        /// <summary>
        /// Gets or sets the collection to be used by the provider to store the
        /// entities it creates.  This will be set automatically by RT3 when
        /// the plug-in is registered with the AgRt3Application class.
        /// </summary>
        public IAgEntityCollection Entities
        {
            get
            {
                return m_entities;
            }
            set
            {
                m_entities = value;
            }
        }

        /// <summary>
        /// Starts the provider processing.  This particular provider creates
        /// a thread, but a callback or timer could be just as easily used.
        /// It is very important that start returns after initialization, it cannot
        /// simply perform it's work in this function and loop forever.
        /// </summary>
        public void Start()
        {
            if (!m_active)
            {
                Validate();

                m_threadControl = new Thread(new ThreadStart(ThreadFunction));
                m_threadControl.SetApartmentState(ApartmentState.MTA);
                m_active = true;
                m_threadControl.Start();
            }
        }

        /// <summary>
        /// Terminates processing and restores it to the ready state.
        /// </summary>
        public void Stop()
        {
            if (m_active)
            {
                m_active = false;
                m_threadControl.Join();
                m_threadControl = null;
            }
        }

        /// <summary>
        /// Throws a COMException if the provider is not properly configured
        /// and therefore unable to be started.
        /// </summary>
        public void Validate()
        {
            if (m_entities == null)
            {
                throw new COMException("Entities property has not been set.");
            }
            else
            {
                if (!File.Exists(m_filename))
                {
                    throw new COMException("The file: " + m_filename + "does not exist.");
                }
            }
        }
        #endregion

        #region ProvideEntitiesFromTextFile Members
        /// <summary>
        /// ThreadFunction is where all of the actual substance happens.
        /// Using the Stream object, we read a line of data and parse
        /// it for the entity ID, Position and our meta-data.
        /// This data is in a simple format laid out as follows.
        /// Each line is 80 characters plus linefeed and carriage return
        /// Each token is seperated by a single whitespace with the order being
        /// NAME LAT LON ALT AFFILIATION MIL2525B TIMETONEXTUPDATE
        /// ReferenceProviderData.txt in the Samples\Data directory contains
        /// sample data which can be used to run the plug-in.
        /// </summary>
        public void ThreadFunction()
        {
            //If we have any subscribers, generate the start event
            if (OnProviderStart != null)
            {
                OnProviderStart(this);
            }

            while (m_active)
            {
                //Open the text file for reading
                using (TextReader textReader = new StreamReader(m_filename))
                {
                    string line = textReader.ReadLine();
                    while (m_active && (line != null))
                    {
                        //Parse a line
                        string[] elements = textReader.ReadLine().Split(' ');
                        string entityName = elements[0];
                        double latitude = Convert.ToDouble(elements[1]);
                        double longitude = Convert.ToDouble(elements[2]);
                        double altitude = Convert.ToDouble(elements[3]);
                        string affiliation = elements[4];
                        string mil2525b = elements[5];
                        int timeDelay = Convert.ToInt32(elements[6]);

                        //Now we'll see if the entity has already been
                        //inserted into the system.  If it has, we simply
                        //update it and call CommitUpdate, if it hasn't
                        //we need to call Add
                        bool NewEntity = false;
                        IAgEntity Entity;
                        IAgPointEntity PointEntity;
                        Entity = Entities.Find(entityName);
                        if (Entity == null)
                        {
                            PointEntity = new AgPointEntity() as IAgPointEntity;
                            Entity = PointEntity as IAgEntity;
                            Entity.ID = entityName;
                            PointEntity.Position = new AgEntityPosition() as IAgEntityPosition;
                            NewEntity = true;
                        }
                        else
                        {
                            PointEntity = Entity as IAgPointEntity;
                        }

                        //Our data does not have time so we always assume the
                        //position time is now.  RT3 uses UTC.
                        Entity.Time = DateTime.UtcNow;

                        //Set the position
                        PointEntity.Position.Set(latitude, longitude, altitude);

                        //Set the meta-data
                        IAgEntityMetaDataCollection metaData = Entity.MetaData;
                        metaData.Set("Affiliation", affiliation);
                        metaData.Set("Symbology", mil2525b);

                        //Call add or update
                        if (NewEntity)
                        {
                            Entities.Add(Entity);
                        }
                        else
                        {
                            PointEntity.CommitUpdate(AgEEntityUpdate.eEntityUpdate);
                        }

                        //Sleep the desired wait time
                        if (timeDelay > 0)
                        {
                            Thread.Sleep(timeDelay);
                        }
                        line = textReader.ReadLine();
                    }

                    //Since we're just reading from a file, when we hit the
                    //end we'll remove everything and then start over.
                    //This way we never run out of sample data.
                    Entities.RemoveAll();
                    Thread.Sleep(1000);
                }
            }

            //When we stop, make sure we trigger the OnProviderStop event.
            if (OnProviderStop != null)
            {
                OnProviderStop(this);
            }
        }
        #endregion

        #region Variables, Constants, Events & Delegates

        //Events defined by IAgRtProvideEntitiesEvents
        public event OnProviderStartDelegate OnProviderStart;
        public event OnProviderStopDelegate OnProviderStop;

        //Every RT3 plug-in needs to have a unique ProgID.  You should change
        //the below ProgID to match any new classes you create.
        public const string ProgID = "AGI.Realtime.Examples.Providers.ProvideEntitiesFromTextFile";

        //Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
        //your class using GUIDGen, located in "Tools->Create GUID" in Visual
        //Studio or http://www.guidgen.com/
        public const string ClassId = "407BF1A5-D616-4b50-B240-C5CC8FECCB43";

        //Each RT3 plug-in must have it's own descriptive type-name which RT3
        //will use to distinguish it from other plug-ins.  For GUI based
        //applications, this is the name which will be shown to the end-user.
        private const string m_typeName = "Example Text File Provider";

        private Array m_metaDataDictionary;
        private bool m_active = false;
        private IAgEntityCollection m_entities;
        private object m_options;
        private string m_id;
        private string m_filename;
        private string m_instanceName;
        private Thread m_threadControl;

        #endregion
    }
}
