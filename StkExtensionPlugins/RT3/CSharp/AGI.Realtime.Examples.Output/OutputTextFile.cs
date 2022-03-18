using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using AGI.Attr;
using AGI.Entity;

namespace AGI.Realtime.Examples.Output
{
    #region IProvideTextFile

    /// <summary>
    /// An IDispatch interface that defines parameters for specifying a text
    /// file for writing.  This interface exists to satisfy IAgAttrConfig.
    /// which you will read more about below. Your plug-in should also have a
    /// corresponding interface with any configuration options exposed.
    /// It should be marked as InterfaceIsDual and have a unique GUID.
    /// Generate a new GUID your class using GUIDGen, located in
    /// "Tools->Create GUID" in Visual Studio or use http://www.guidgen.com/.
    /// </summary>
    [Guid("2340C9E4-CEC4-4dac-BB27-ED0233CDD8DB")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IProvideTextFile : IAgRtProvideTrackingData, IAgAttrConfig
    {
        /// <summary>
        /// Gets or sets the text file to be output.
        /// </summary>
        string Filename
        {
            get;
            set;
        }

        /// <summary>
        /// Settings for configured entities
        /// </summary>
        string Settings
        {
            get;
            set;
        }
    }

    #endregion

    public enum Affiliation
    {
        Friendly,
        Hostile,
        Neutral
    }

    public class OutputSettings
    {
        public OutputSettings(Affiliation affiliation, string symbology)
        {
            m_affiliation = affiliation;
            m_symbology = symbology;
        }

        public Affiliation Affiliation { get { return m_affiliation; } }
        public string Symbology { get { return m_symbology; } }

        private Affiliation m_affiliation;
        private string m_symbology;
    }

    /// <summary>
    /// An output plug-in which write to a file which can be read back
    /// int with OutputTextFile.
    /// 
    /// This class implements IProvideTextFile, which itself
    /// derives from IAgRtProvideTrackingData and IAgAttrConfig.
    /// IAgRtProvideTrackingData is the minimum interface necessary for defining
    /// an output plugin.  IAgAttrConfig is implemented to allow for saving and
    /// loading of configuration via AgRt3FileOps.  It also allows for AGI based
    /// products to automatically generate a simple user interface for
    /// configuring options.  IProvideTextFile is the IDispatch
    /// interface which defines the configuration options.  It is
    /// required in order for IAgAttrConfig to have late-bound access to class
    /// properties for OutputTextFile.
    /// 
    /// To use this class in RT3, simply compile the library and load the
    /// included reg file into the Windows registry.
    /// 
    /// For more information on RT3 interfaces and class, please see the RT3
    /// Development Kit documentation.
    /// </summary>
    [Guid(OutputTextFile.ClassId),
    ProgId(OutputTextFile.ProgID),
     ClassInterface(ClassInterfaceType.None)]
    public class OutputTextFile : IProvideTextFile
    {
        #region OutputTextFile Members

        /// <summary>
        /// Set how a specified entity will be output.
        /// </summary>
        /// <param name="entityID">The entity ID</param>
        /// <param name="settings">The configuration to use</param>
        public void SetConfiguraiton(string entityID, OutputSettings settings)
        {
            m_settings[entityID] = settings;
        }

        /// <summary>
        /// Get how a specified entity will be output.
        /// /// </summary>
        /// <param name="entityID">The entity ID</param>
        /// <param name="settings">The configuration being used</param>
        /// <returns>True if a configuration existed</returns>
        public bool GetConfiguration(string entityID, out OutputSettings settings)
        {
            return m_settings.TryGetValue(entityID, out settings);
        }

        #endregion

        #region IProvideTextFile Members
        /// <summary>
        /// Gets or sets the text file to be output.
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

        /// <summary>
        /// Settings for configured entities
        /// </summary>
        public string Settings
        {
            get
            {
                StringBuilder configurationString = new StringBuilder();
                foreach (KeyValuePair<string, OutputSettings> item in m_settings)
                {
                    configurationString.Append(item.Key);
                    configurationString.Append(" ");
                    configurationString.Append((int) item.Value.Affiliation);
                    configurationString.Append(" ");
                    configurationString.AppendLine(item.Value.Symbology);
                }

                return configurationString.ToString();
            }
            set
            {
                m_settings.Clear();

                StringReader stringReader = new StringReader(value);
                string line = stringReader.ReadLine();
                while (line != null)
                {
                    string[] tmp = line.Split(' ');
                    OutputSettings settings = new OutputSettings((Affiliation) int.Parse(tmp[1]), tmp[2]);
                    m_settings[tmp[0]] = settings;
                    line = stringReader.ReadLine();
                }
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

                //Serialized version of m_settings
                AttrBuilder.AddMultiLineStringDispatchProperty(m_options,
                    "Settings",
                    "Settings",
                    "Settings",
                    (int)AgEAttrAddFlags.eAddFlagNone);
            }

            return m_options;
        }

        #endregion

        #region IAgRtProvideTrackingData Members

        /// <summary>
        /// Gets the type name of this class.
        /// </summary>
        public string Name
        {
            get { return m_typeName; }
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
        /// Throws a COMException if the provider is not properly configured
        /// and therefore unable to be started.
        /// </summary>
        public void Validate()
        {
            //Always valid
        }

        /// <summary>
        /// Prepares the plugin for output.
        /// </summary>
        public void Initialize()
        {
            if (!m_active)
            {
                m_active = true;
                m_textWriter = File.CreateText(m_filename);
                m_timeLastWrite = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Stops output
        /// </summary>
        public void Uninitialize()
        {
            if (m_active)
            {
                m_active = false;
                m_textWriter.Dispose();
                m_textWriter = null;
            }
        }

        /// <summary>
        /// Write's the specified entity to the text file
        /// </summary>
        /// <param name="entity"></param>
        public void OutputEntity(IAgEntity entity)
        {
            DateTime utcNow = DateTime.UtcNow;
            int millisecondSinceLastWrite = (int)(utcNow - m_timeLastWrite).TotalMilliseconds;
            m_timeLastWrite = utcNow;

            OutputSettings settings;
            if (!m_settings.TryGetValue(entity.ID, out settings))
            {
                settings = new OutputSettings(Affiliation.Friendly, "SFAPMF----*****");
            }

            AgPointEntity pointEntity = entity as AgPointEntity;
            StringBuilder entry = new StringBuilder();
            entry.Append(pointEntity.DisplayName);
            entry.Append(" ");
            entry.Append(Math.Round(pointEntity.Position.Latitude, 6));
            entry.Append(" ");
            entry.Append(Math.Round(pointEntity.Position.Longitude, 6));
            entry.Append(" ");
            entry.Append(Math.Round(pointEntity.Position.Altitude, 6));
            entry.Append(" ");
            entry.Append(settings.Affiliation);
            entry.Append(" ");
            entry.Append(settings.Symbology);
            entry.Append(" ");
            entry.Append(millisecondSinceLastWrite);

            m_textWriter.WriteLine(entry.ToString());
        }

        #endregion

        #region Variables, Constants, Events & Delegates

        //Every RT3 plug-in needs to have a unique ProgID.  You should change
        //the below ProgID to match any new classes you create.
        public const string ProgID = "AGI.Realtime.Examples.Output.OutputTextFile";

        //Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
        //your class using GUIDGen, located in "Tools->Create GUID" in Visual
        //Studio or http://www.guidgen.com/
        public const string ClassId = "17586062-F608-4c33-B1D2-835B3BB7A5BF";

        //Each RT3 plug-in must have it's own descriptive type-name which RT3
        //will use to distinguish it from other plug-ins.  For GUI based
        //applications, this is the name which will be shown to the end-user.
        private const string m_typeName = "Example Text File Output";

        private bool m_active = false;
        private object m_options;
        private string m_filename;
        private string m_instanceName;
        private TextWriter m_textWriter;
        private DateTime m_timeLastWrite;
        private Dictionary<string, OutputSettings> m_settings = new Dictionary<string, OutputSettings>();
        #endregion
    }
}
