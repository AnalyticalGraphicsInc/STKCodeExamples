using AGI.STKObjects;
using System.Collections.Generic;

namespace OperatorsToolbox.SmartView
{
    public class ObjectData
    {
        public string SimpleName { get; set; }
        public string ClassName { get; set; }
        public string SimplePath { get; set; }
        public string LongPath { get; set; }
        public bool HideShow { get; set; }
        public bool ModifyLeadTrail { get; set; }
        public double LeadTime { get; set; }
        public double TrailTime { get; set; }
        public AgELeadTrailData LeadSetting3D { get; set; }
        public AgELeadTrailData TrailSetting3D { get; set; }
        public AgELeadTrailData LeadSetting2D { get; set; }
        public AgELeadTrailData TrailSetting2D { get; set; }
        public string CoordSys { get; set; }
        public Dictionary<string, AgEGeometricElemType> ActiveVgtComponents { get; set; }

        public ObjectData()
        {
            ActiveVgtComponents = new Dictionary<string, AgEGeometricElemType>();
        }
    }
}
