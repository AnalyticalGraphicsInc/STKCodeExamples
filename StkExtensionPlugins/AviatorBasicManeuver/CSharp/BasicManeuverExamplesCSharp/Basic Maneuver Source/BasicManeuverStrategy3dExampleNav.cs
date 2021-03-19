using System;
using System.Runtime.InteropServices;
using AGI.AgComUtilities;
using AGI.AgFlight;

namespace AGI.AgFlight.BasicManeuverExamples.Basic_Maneuver_Source
{
    [ComVisible(true),
     Guid("502F0989-652F-42BC-BF09-0D3DDEEE0F81"),
     ProgId("AGI12.BasicManeuverExamples.BasicManeuverStrategy3dExampleNav"),
     ClassInterface(ClassInterfaceType.None)]
    public class BasicManeuverStrategy3dExampleNav :
        IAgFlightBasicManeuverStrategy,
        IAgFlightBasicManeuver3DNavStrategy,
        IPersistPropertyBag
    {
        static BasicManeuverStrategy3dExampleNav m_navCopy;

        private AgCartVec3  m_accelCommand,
            m_coriolisAccel,
            m_centripetalAccel,
            m_PQR,
            m_earthOmegaInRefAxes;

        private AgQuaternion m_ecfToRefAxesQuat;

        public BasicManeuverStrategy3dExampleNav()
        {
            m_accelCommand = new AgCartVec3();
            m_coriolisAccel = new AgCartVec3();
            m_centripetalAccel = new AgCartVec3();
            m_PQR = new AgCartVec3();
            m_earthOmegaInRefAxes = new AgCartVec3();

            m_ecfToRefAxesQuat = new AgQuaternion();
        }

        public void Configure(IAgFlightProcedure pParentProc, IAgFlightBasicManeuverStrategy pOtherStrategy)
        {
            // nothing to do for this example
        }

        public IAgComUnkCollection GetValidProfileStrategyFactories(IAgComUnkCollection pRegisteredStratFactories)
        {
            IAgFlightBasicManeuverStrategyFactory pProfileStratFactory =
                (IAgFlightBasicManeuverStrategyFactory)new BasicManeuverStrategyFactory3dExampleProfile();

            IAgComUnkCollectionInit pValidStratFactoryColl =
                (IAgComUnkCollectionInit)new AGI.AgComUtilities.AgComUnkCollection();

            pValidStratFactoryColl.Add(pProfileStratFactory);

            return (IAgComUnkCollection)pValidStratFactoryColl;
        }

        public void SetInitialState(AgEFlightPerformanceMode ePerfMode, AgEFlightPhaseOfFlight ePhaseOfFlight, IAgFlightBasicManeuverState pInitState, IAgFlightDoubleArray presultAuxInitState)
        {
            AgQuaternion pRefAxesQuat = new AgQuaternion();
            AgCartVec3 pRefAxesAngVel = new AgCartVec3();

            // generate the default ref axes coordinate frame ...
            pInitState.ComputeStandardECFToRefAxesFrame(
                true, AgEFlightAxesOrientation.eFwdRightDown, pRefAxesQuat, pRefAxesAngVel);

            // tell the system to use that ref axes frame (can only be done during SetInitialState)...
            pInitState.SetInitialECFToRefAxesFrame(AgEFlightAxesOrientation.eFwdRightDown, pRefAxesQuat, pRefAxesAngVel);

        }

        public bool QueryDerivatives(IAgFlightBasicManeuverState pCurrentState, IAgFlightBasicManeuverDerivs presultCurrentDerivs, bool vbAllowThresholdDetection)
        {
            // the strategy extrapolates the starting flight condition ...

            // model the effects of coriolis without compensating for it ...
            presultCurrentDerivs.SetVertPlaneCompensateForCoriolisAccel(false);
            presultCurrentDerivs.SetHorizPlaneCompensateForCoriolisAccel(false);

            // totalECFAccel = pitchRateNormalAccel + speedDotLongAccel + coriolisAccel - centripetalAccel
            pCurrentState.QueryECFCoriolisAccel(m_coriolisAccel);
            pCurrentState.QueryECFCentripetalAccel(m_centripetalAccel);
            m_accelCommand.SetToZero(); // pitchRateNormalAccel = speedDotLongAccel = 0
            m_accelCommand.Add(m_coriolisAccel);
            m_accelCommand.Subtract(m_centripetalAccel);

            presultCurrentDerivs.SetEcfAccelCommand(m_accelCommand);

            // compute ref axes PQRs
            pCurrentState.QueryECFToRefAxesAttRefFrame(m_ecfToRefAxesQuat, null);
            pCurrentState.QueryOmegaEarthInFrame(m_ecfToRefAxesQuat, m_earthOmegaInRefAxes);

            double dCommandFPADot = 0.0;
            m_PQR.ConstructFromComponents(0.0, dCommandFPADot, 0.0);
            m_PQR.Add(m_earthOmegaInRefAxes);

            presultCurrentDerivs.SetRefAxesOmegaCommand(m_PQR);

            // Tgo is used for calc progress and to refine stopping condition
            presultCurrentDerivs.NavTimeToGo = 1.0;
            presultCurrentDerivs.ProfileTimeToGo = 1.0;

            return true;
        }

        public bool RefineIntegrateThreshold(AgEFlightIntegThresholdType eThresholdType, IAgFlightBasicManeuverState pLastGoodState, IAgFlightBasicManeuverState pCurrentState)
        {
            return true;
        }

        public bool DataIsHeld(object pDataUnknown)
        {
            return false;
        }

        public Array GetSpecialTimes()
        {
            return null;
        }

        public void Copy()
        {
            BasicManeuverStrategy3dExampleNav pCopy = new BasicManeuverStrategy3dExampleNav();

            AgFlightPersistHelper pPersistHelper = new AgFlightPersistHelper();
            pPersistHelper.Merge((AGI.AgComUtilities.IPersistPropertyBag)this,
                (AGI.AgComUtilities.IPersistPropertyBag)pCopy);

            m_navCopy = pCopy;
        }

        public void Paste()
        {
            AgFlightPersistHelper pPersistHelper = new AgFlightPersistHelper();
            pPersistHelper.Merge((AGI.AgComUtilities.IPersistPropertyBag)m_navCopy,
                (AGI.AgComUtilities.IPersistPropertyBag)this);
        }

        public string StrategyType
        {
            get { return "BasicManeuverStrategy3dExampleCSharp"; }
        }

        public string StrategyDescription
        {
            get { return "Example 3D Strategy for C#"; }
        }

        public bool IsNavigationStrategy
        {
            get { return true; }
        }

        public bool IsProfileStrategy
        {
            get { return false; }
        }

        public bool CanPaste
        {
            get
            {
                if (m_navCopy == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public int GetClassID([Out] out Guid pClassID)
        {
            Attribute myClassAttribute = Attribute.GetCustomAttribute(
                typeof(BasicManeuverStrategy3dExampleNav), typeof(GuidAttribute));

            pClassID = new Guid(((GuidAttribute)myClassAttribute).Value);

            return 0;
        }

        public int InitNew()
        {
            return 0;
        }

        public int Load(IPropertyBag pPropBag, [In, MarshalAs(UnmanagedType.Interface)] object pErrorLog)
        {
            object stratExampleType = "Example3dNav";
            int retval = pPropBag.Read("StratExampleType", ref stratExampleType, pErrorLog);
            return retval;
        }

        public int Save(IPropertyBag pPropBag, [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty, [In, MarshalAs(UnmanagedType.Bool)] bool fSaveAllProperties)
        {
            object stratExampleType = "Example3dNav";
            int retval = pPropBag.Write("StratExampleType", ref stratExampleType);
            return retval;
        }
    }

    [ComVisible(true), ComImport,
     Guid("0000010C-0000-0000-C000-000000000046"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersist
    {
        [PreserveSig]
        int GetClassID([Out] out Guid pClassID);
    }

    [ComVisible(true), ComImport,
     Guid("37D84F60-42CB-11CE-8135-00AA004BB851"),//Guid("5738E040-B67F-11d0-BD4D-00A0C911CE86"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersistPropertyBag : IPersist
    {
        #region IPersist
        [PreserveSig]
        new int GetClassID([Out] out Guid pClassID);
        #endregion

        [PreserveSig]
        int InitNew();

        [PreserveSig]
        int Load(
            IPropertyBag pPropBag,
            [In, MarshalAs(UnmanagedType.Interface)] object pErrorLog
        );

        [PreserveSig]
        int Save(
            IPropertyBag pPropBag,
            [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty,
            [In, MarshalAs(UnmanagedType.Bool)] bool fSaveAllProperties
        );
    }

    [ComVisible(true), ComImport,
     Guid("55272A00-42CB-11CE-8135-00AA004BB851"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyBag
    {
        [PreserveSig]
        int Read(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [In, Out, MarshalAs(UnmanagedType.Struct)] ref object pVar,
            [In, MarshalAs(UnmanagedType.Interface)] object pErrorLog);

        [PreserveSig]
        int Write(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [In, MarshalAs(UnmanagedType.Struct)] ref object pVar);
    }
}