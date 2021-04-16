using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsToolbox.FacilityCreator
{
    public class FacilityJsonInput
    {
        public List<FacilityInformation> data { get; set; }
    }
    public class FacilityInformation
    {
        public string type { get; set; }
        public FacilityGeometry geometry { get; set; }
        public FacilityProperties properties { get; set; }
        public FacilityDescription description { get; set; }

    }

    public class FacilityGeometry
    {
        public string type { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public double alt { get; set; }
    }

    public class FacilityProperties
    {
        public string name { get; set; }
    }

    public class FacilityDescription
    {
        public string title { get; set; }
        public string html { get; set; }

        public string text { get; set; }
    }
}
