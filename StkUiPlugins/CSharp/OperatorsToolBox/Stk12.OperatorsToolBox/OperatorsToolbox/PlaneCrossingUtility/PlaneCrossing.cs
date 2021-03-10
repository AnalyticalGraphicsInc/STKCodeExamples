using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsToolbox.PlaneCrossingUtility
{
    public class PlaneCrossing
    {
        public string CrossingTime { get; set; }
        public bool IsBounded { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public string LowerBoundCrossingTime { get; set; }
        public string UpperBoundCrossingTime { get; set; }
    }
}
