using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using AGI.Attr;
using AGI.Entity;
using AGI.Realtime.Tracking;

namespace AGI.Realtime.Examples.Queries
{
    #region IQueryByAltitude

    //In order to implement the events defined in IAgRt3QueryEvents, which is
    //required by all query plug-ins, we must define the signature of the
    //events here.
    public delegate void OnQueryChangedDelegate(IAgRt3Query Query);
    public delegate void OnQueryEnabledDelegate(IAgRt3Query Query);
    public delegate void OnQueryDisabledDelegate(IAgRt3Query Query);

    /// <summary>
    /// An IDispatch interface that defines parameters for specifying a query
    /// which has a minimum and maximum altitude.  This interface exists to
    /// satisfy IAgAttrConfig, and IAgRt3QueryEvents which you will read more
    /// about below.  Your plug-in should also have a corresponding interface
    /// with any configuration options exposed.  It should be marked as
    /// InterfaceIsDual and have a unique GUID.  Generate a new GUID your class
    /// using GUIDGen, located in "Tools->Create GUID" in Visual Studio or use
    /// http://www.guidgen.com/.
    /// </summary>
    [Guid("600CAC46-09DF-40d0-806C-2A9FF48A59C9")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IQueryByAltitude : IAgRt3Query, IAgAttrConfig
    {
        /// <summary>
        /// Gets or sets the minimum altitude
        /// </summary>
        double MinimumAltitude { get;set;}

        /// <summary>
        /// Gets or sets the maximum altitude
        /// </summary>
        double MaximumAltitude { get;set;}

        //COM event calls are latebound, and therefore our events must match
        //the DispId of the orignal interface.  This block of code, along with
        //the event signatures above, can be copied unchanged into your new
        //query plug-in.

        [DispId((int)AgERt3EventDispatchID.eQueryChangedEvent)]
        event OnQueryChangedDelegate OnQueryChanged;

        [DispId((int)AgERt3EventDispatchID.eQueryEnabledEvent)]
        event OnQueryEnabledDelegate OnQueryEnabled;

        [DispId((int)AgERt3EventDispatchID.eQueryDisabledEvent)]
        event OnQueryDisabledDelegate OnQueryDisabled;
    }

    #endregion

    /// <summary>
    /// A query which matches against entities in the specified altitude range.
    /// 
    /// This class implements IQueryByAltitude, which itself
    /// derives from IAgRt3Query and IAgAttrConfig. IAgRt3Query is the minimum
    /// interface necessary for defining a query.  IAgAttrConfig is
    /// implemented to allow for saving and loading of configuration via
    /// AgRt3FileOps.  It also allows for AGI based products to automatically
    /// generate a simple user interface for configuring options.
    /// IQueryByAltitude is the IDispatch interface which defines the
    /// configuration options and events.  It is required in order for
    /// IAgAttrConfig to have late-bound access to class properties and for
    /// QueryByAltitude to properly implement IAgRt3QueryEvents.
    /// 
    /// To use this class in RT3, simply compile the library and load the
    /// included reg file into the Windows registry.
    /// 
    /// For more information on RT3 interfaces and class, please see the RT3
    /// Development Kit documentation.
    /// </summary>
    [Guid(QueryByAltitude.ClassId),
    ProgId(QueryByAltitude.ProgID),
    ClassInterface(ClassInterfaceType.None),
    ComSourceInterfaces(typeof(IAgRt3QueryEvents))]
    public class QueryByAltitude : IQueryByAltitude
    {
        #region IQueryByAltitude Members

        /// <summary>
        /// Gets or sets the minimum altitude
        /// </summary>
        public double MinimumAltitude
        {
            get
            {
                return m_minimumAltitude;
            }
            set
            {
                m_minimumAltitude = value;
                if (OnQueryChanged != null)
                {
                    OnQueryChanged(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum altitude
        /// </summary>
        public double MaximumAltitude
        {
            get
            {
                return m_maximumAltitude;
            }
            set
            {
                m_maximumAltitude = value;
                if (OnQueryChanged != null)
                {
                    OnQueryChanged(this);
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
        public object GetConfig(AgAttrBuilder attrBuilder)
        {
            //If this is the first time, we need to create the options
            //which we will then cache for future use.
            if (m_options == null)
            {
                m_options = attrBuilder.NewScope();

                //Expose a Quantity option, in meters, for MinimumAltitude
                attrBuilder.AddQuantityDispatchProperty2(m_options,
                    "MinimumAltitude",
                    "MinimumAltitude",
                    "MinimumAltitude",
                    "Distance",
                    "Meters",
                    "Meters",
                    (int)AgEAttrAddFlags.eAddFlagNone);

                //Expose a Quantity option, in meters, for MaximumAltitude
                attrBuilder.AddQuantityDispatchProperty2(m_options,
                    "MaximumAltitude",
                    "MaximumAltitude",
                    "MaximumAltitude",
                    "Distance",
                    "Meters",
                    "Meters",
                    (int)AgEAttrAddFlags.eAddFlagNone);
            }
            
            return m_options;
        }

        #endregion

        #region IAgRt3Query Members

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
        /// Gets or sets the instance of AgRt3QueryGraphics associated with
        /// this query.  This will be set automatically by RT3 when the
        /// plug-in is registered with the AgRt3EntityManager class.
        /// </summary>
        public IAgRt3QueryGraphics Graphics
        {
            get
            {
                return m_graphics;
            }
            set
            {
                m_graphics = value;
            }
        }

        /// <summary>
        /// Gets or sets the provider plug-in this query is associated with.
        /// This will be set automatically by RT3 when the plug-in is
        /// registered with the AgRt3EntityManager class.
        /// </summary>
        public IAgRtProvideEntities Provider
        {
            get
            {
                return m_provider;
            }
            set
            {
                m_provider = value;
            }
        }

        /// <summary>
        /// Gets or sets the enabled property which indicates if the query
        /// should be considered when distributing entities.  If false, no
        /// entities will be matched against this query.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                m_enabled = value;

                if (m_enabled && OnQueryEnabled != null)
                {
                    OnQueryEnabled(this);
                }
                else if (!m_enabled && OnQueryDisabled != null)
                {
                    OnQueryDisabled(this);
                }
            }
        }

        /// <summary>
        /// Called by the RT3 engine to determine if the provided entity
        /// belongs in this query.
        /// </summary>
        /// <param name="Entity">The entity to be matched against.</param>
        /// <returns>True if the entity matches this query criteria, false otherwise.</returns>
        public bool IsMatch(IAgEntity Entity)
        {
            bool Result = false;

            IAgPointEntity Point = Entity as IAgPointEntity;
            if (Point != null && Point.Position != null)
            {
                Result = (Point.Position.Altitude >= m_minimumAltitude) && (Point.Position.Altitude  <= m_maximumAltitude);
            }

            IAgPolylineEntity Poly = Entity as IAgPolylineEntity;
            if (Poly != null && Poly.Positions != null)
            {
                foreach (IAgEntityPosition Position in Poly.Positions)
                {
                    Result = (Position.Altitude >= m_minimumAltitude) && (Position.Altitude <= m_maximumAltitude);
                    if (Result)
                    {
                        break;
                    }
                }
            }

            return Result;
        }

        #endregion

        #region Variables, Constants, Events & Delegates

        //Events defined by IAgRt3QueryEvents
        public event OnQueryChangedDelegate OnQueryChanged;
        public event OnQueryEnabledDelegate OnQueryEnabled;
        public event OnQueryDisabledDelegate OnQueryDisabled;

        //Every RT3 plug-in needs to have a unique ProgID.  You should change
        //the below ProgID to match any new classes you create.
        public const string ProgID = "AGI.Realtime.Examples.Queries.QueryByAltitude";

        //Each RT3 plug-in needs to have it's own GUID.  Generate a new GUID
        //your class using GUIDGen, located in "Tools->Create GUID" in Visual
        //Studio or http://www.guidgen.com/
        public const string ClassId = "375FA54F-4493-47fe-BE3D-6338BD5C57CE";

        //Each RT3 plug-in must have it's own descriptive type-name which RT3
        //will use to distinguish it from other plug-ins.  For GUI based
        //applications, this is the name which will be shown to the end-user.
        private const string m_typeName = "Altitude Query";

        private bool m_enabled = false;
        private double m_minimumAltitude = 0;
        private double m_maximumAltitude = 1000;
        private IAgRt3QueryGraphics m_graphics = null;
        private IAgRtProvideEntities m_provider = null;
        private object m_options = null;
        private string m_instanceName;
        #endregion
    }
}
