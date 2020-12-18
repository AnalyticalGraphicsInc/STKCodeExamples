using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsToolbox.PlaneCrossingUtility
{
    public class PlaneCrossingGroup
    {
        public string PlaneReferenceObjectName { get; set; }

        public string CrossingObjectName { get; set; }
        public string AnalysisStartTime { get; set; }
        public string AnalysisStopTime { get; set; }

        public List<PlaneCrossing> PlaneCrossings { get; set; }

        public PlaneCrossingGroup()
        {
            PlaneCrossings = new List<PlaneCrossing>();
        }
    }
}
