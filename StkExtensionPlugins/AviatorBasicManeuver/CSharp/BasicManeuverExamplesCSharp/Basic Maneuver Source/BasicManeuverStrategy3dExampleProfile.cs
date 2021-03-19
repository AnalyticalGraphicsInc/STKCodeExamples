using System;
using System.Runtime.InteropServices;
using AGI.AgComUtilities;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace AGI.AgFlight.BasicManeuverExamples.Basic_Maneuver_Source
{
    [ComVisible(true),
    Guid("D9336141-6F9D-4FBA-BC13-BA7D6275AF49"),
    ProgId("AGI12.BasicManeuverExamples.BasicManeuverStrategy3dExampleProfile"),
    ClassInterface(ClassInterfaceType.None)]
    public class BasicManeuverStrategy3dExampleProfile :
        IAgFlightBasicManeuverStrategy,
        IPersistPropertyBag
    {
        public void Configure(IAgFlightProcedure pParentProc, IAgFlightBasicManeuverStrategy pOtherStrategy)
        {
            // nothing to do for this example
        }

        public IAgComUnkCollection GetValidProfileStrategyFactories(IAgComUnkCollection pRegisteredStratFactories)
        {
            // convention is to return the profile strategy factory
            IAgFlightBasicManeuverStrategyFactory pProfileStratFactory =
                (IAgFlightBasicManeuverStrategyFactory) new BasicManeuverStrategyFactory3dExampleProfile();

            IAgComUnkCollectionInit pValidStratFactoryColl =
                (IAgComUnkCollectionInit) new AGI.AgComUtilities.AgComUnkCollection();

            pValidStratFactoryColl.Add(pProfileStratFactory);

            return (IAgComUnkCollection)pValidStratFactoryColl;
        }

        public void SetInitialState(AgEFlightPerformanceMode ePerfMode, AgEFlightPhaseOfFlight ePhaseOfFlight, IAgFlightBasicManeuverState pInitState, IAgFlightDoubleArray presultAuxInitState)
        {
            // nothing to do for this example
        }

        public bool QueryDerivatives(IAgFlightBasicManeuverState pCurrentState, IAgFlightBasicManeuverDerivs presultCurrentDerivs, bool vbAllowThresholdDetection)
        {
            // nothing to do for this example, all the action gets done in the Nav strategy
            return true;
        }

        public bool RefineIntegrateThreshold(AgEFlightIntegThresholdType eThresholdType, IAgFlightBasicManeuverState pLastGoodState, IAgFlightBasicManeuverState pCurrentState)
        {
            // nothing to do for this example, all the action gets done in the Nav strategy
            return true;
        }

        public bool DataIsHeld(object pDataUnknown)
        {
            return false;
        }

        public Array GetSpecialTimes()
        {
            // nothing to do for this example, all the action gets done in the Nav strategy
            return null;
        }

        public void Copy()
        {
            // not supported
        }

        public void Paste()
        {
            // not supported
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
            get { return false; }
        }

        public bool IsProfileStrategy
        {
            get { return true; }
        }

        public bool CanPaste
        {
            get { return false; }
        }

        public int GetClassID([Out] out Guid pClassID)
        {
            Attribute myClassAttribute = Attribute.GetCustomAttribute(
                            typeof(BasicManeuverStrategy3dExampleProfile), typeof(GuidAttribute));

            pClassID = new Guid(((GuidAttribute)myClassAttribute).Value);

            return 0;
        }

        public int InitNew()
        {
            return 0;
        }

        public int Load(IPropertyBag pPropBag, [In, MarshalAs(UnmanagedType.Interface)] object pErrorLog)
        {
            object stratExampleType = "Example3dProfile";
            int retval = pPropBag.Read("StratExampleType", ref stratExampleType, pErrorLog);
            return retval;
        }

        public int Save(IPropertyBag pPropBag, [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty, [In, MarshalAs(UnmanagedType.Bool)] bool fSaveAllProperties)
        {
            object stratExampleType = "Example3dProfile";
            int retval = pPropBag.Write("StratExampleType", ref stratExampleType);
            return retval;
        }
    }
}
