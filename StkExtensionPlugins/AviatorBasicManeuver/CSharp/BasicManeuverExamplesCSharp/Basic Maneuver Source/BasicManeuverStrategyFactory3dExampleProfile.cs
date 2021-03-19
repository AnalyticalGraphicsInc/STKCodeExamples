using System.Runtime.InteropServices;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace AGI.AgFlight.BasicManeuverExamples.Basic_Maneuver_Source
{
    [ComVisible(true),
    Guid("ECC3F3D5-DB3D-4BDB-8E82-6B45994E4929"),
    ProgId("AGI12.BasicManeuverExamples.BasicManeuverStrategyFactory3dExampleProfile"),
    ClassInterface(ClassInterfaceType.None)]
    public class BasicManeuverStrategyFactory3dExampleProfile : IAgFlightBasicManeuverStrategyFactory
    {
        public string CanCreateStrategy(IAgFlightProcedure pParentProc, IAgFlightBasicManeuverStrategy pNavStrategy)
        {
            try
            {
                BasicManeuverStrategy3dExampleNav pOtherNavValid = (BasicManeuverStrategy3dExampleNav)pNavStrategy;
                if (pOtherNavValid != null)
                {
                    return "Strategy is valid";
                }
            }
            catch { }

            throw new COMException("Strategy must be paired with a 3dExampleNav strategy");
        }

        public IAgFlightBasicManeuverStrategy CreateStrategy(IAgFlightProcedure pParentProc, IAgFlightBasicManeuverStrategy pNavStrategy)
        {
            IAgFlightBasicManeuverStrategy p3dExampleNav = (IAgFlightBasicManeuverStrategy)new BasicManeuverStrategy3dExampleProfile();
            p3dExampleNav.Configure(pParentProc, pNavStrategy);
            return p3dExampleNav;
        }

        public string StrategyType
        {
            get { return "BasicManeuverStrategy3dExampleCSharp"; }
        }

        public bool IsNavigationStrategy
        {
            get { return false; }
        }

        public bool IsProfileStrategy
        {
            get { return true; }
        }
    }
}
