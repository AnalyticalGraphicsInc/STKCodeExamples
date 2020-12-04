using System.Collections.Generic;

namespace OperatorsToolbox.FacilityCreator
{
    public class SensorCadance
    {
        public string Name { get; set; }
        public int NumRadars { get; set; }
        public int NumOptical { get; set; }
        public string Type { get; set; }
        public List<FcFacility> FacilityList { get; set; }
        public bool SaveToDatabase { get; set; }

        public string CadenceColor { get; set; }
    }
}
