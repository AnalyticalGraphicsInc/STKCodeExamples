using System.Runtime.InteropServices;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace AGI.AgFlight.BasicManeuverExamples.Basic_Maneuver_Source
{
    [ComVisible(true),
    Guid("0E04E4FB-783D-44F3-AB80-0B155695C8B2"),
    ProgId("AGI12.BasicManeuverExamples.BasicManeuverStrategyFactory3dExampleNav"),
    ClassInterface(ClassInterfaceType.None)]
    public class BasicManeuverStrategyFactory3dExampleNav : IAgFlightBasicManeuverStrategyFactory
    {
        public string CanCreateStrategy(IAgFlightProcedure pParentProc, IAgFlightBasicManeuverStrategy pNavStrategy)
        {
            return "Strategy is valid";
        }

        public IAgFlightBasicManeuverStrategy CreateStrategy(IAgFlightProcedure pParentProc, IAgFlightBasicManeuverStrategy pNavStrategy)
        {
            IAgFlightBasicManeuverStrategy p3dExampleNav = (IAgFlightBasicManeuverStrategy) new BasicManeuverStrategy3dExampleNav();
            p3dExampleNav.Configure(pParentProc, null);
            return p3dExampleNav;
        }

        public string StrategyType 
        {
            get { return "BasicManeuverStrategy3dExampleCSharp"; }
        }

        public bool IsNavigationStrategy
        {
            get { return true; }
        }
        public bool IsProfileStrategy
        { 
            get { return false; } 
        }
    }
}
