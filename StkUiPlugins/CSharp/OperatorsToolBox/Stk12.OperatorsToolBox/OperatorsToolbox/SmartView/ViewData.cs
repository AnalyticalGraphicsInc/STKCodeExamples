using System.Collections.Generic;

namespace OperatorsToolbox.SmartView
{
    public class ViewData
    {
        public string Name { get; set; }
        public string WindowName {get; set;}
        public int WindowId { get; set; }
        public string ViewAxes { get; set; }
        public string ViewTarget { get; set; }
        public string ViewType { get; set; }
        public string ViewType2D { get; set; }
        public bool EnableUniversalOrbitTrack { get; set; }
        public bool EnableUniversalGroundTrack { get; set; }
        public string LeadType { get; set; }
        public string LeadTime { get; set; }
        public string TrailType { get; set; }
        public string TrailTime { get; set; }
        public bool DataDisplayActive { get; set; }
        public string DataDisplayObject { get; set; }
        public string DataDisplayReportName { get; set; }
        public string DataDisplayLocation { get; set; }
        public bool ObjectHideShow { get; set; }
        public string ZoomCenterLat { get; set; }
        public string ZoomCenterLong { get; set; }
        public string ZoomCenterDelta { get; set; }
        public bool ShowGroundSensors { get; set; }
        public bool ShowAerialSensors { get; set; }
        public List<ObjectData> ViewObjectData { get; set; }
        public string TargetSatellite { get; set; }
        public List<string> ThreatSatNames { get; set; }
        public bool TtDataDisplayActive { get; set; }
        public string TtDataDisplayObject { get; set; }
        public string TtDataDisplayReportName { get; set; }
        public string TtDataDisplayLocation { get; set; }
        public bool EnableProximityBox { get; set; }
        public bool EnableProximityEllipsoid { get; set; }
        public string EllipsoidX { get; set; }
        public string EllipsoidY { get; set; }
        public string EllipsoidZ { get; set; }
        public bool EnableGeoBox { get; set; }
        public string GeoLongitude { get; set; }
        public string GeoEastWest { get; set; }
        public string GeoNorthSouth { get; set; }
        public string GeoRadius { get; set; }
        public bool GeoDataDisplayActive { get; set; }
        public string GeoDataDisplayObject { get; set; }
        public string GeoDataDisplayReportName { get; set; }
        public string GeoDataDisplayLocation { get; set; }
        public bool UseAnimationTime { get; set; }
        public string AnimationTime { get; set; }
        public bool UseStoredView { get; set; }
        public string StoredViewName { get; set; }
    }
}
