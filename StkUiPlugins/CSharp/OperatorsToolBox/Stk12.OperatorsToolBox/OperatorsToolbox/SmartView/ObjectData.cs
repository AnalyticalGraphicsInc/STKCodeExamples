using AGI.STKObjects;

namespace OperatorsToolbox.SmartView
{
    public class ObjectData
    {
        public string SimpleName { get; set; }
        public string ClassName { get; set; }
        public string SimplePath { get; set; }
        public string LongPath { get; set; }
        public bool HideShow { get; set; }
        public AgELeadTrailData LeadSetting3D { get; set; }
        public AgELeadTrailData TrailSetting3D { get; set; }
        public AgELeadTrailData LeadSetting2D { get; set; }
        public AgELeadTrailData TrailSetting2D { get; set; }
        public string CoordSys { get; set; }
    }
}
