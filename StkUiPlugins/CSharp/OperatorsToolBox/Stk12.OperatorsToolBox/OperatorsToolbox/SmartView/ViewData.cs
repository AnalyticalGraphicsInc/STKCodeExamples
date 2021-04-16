using OperatorsToolbox.SensorBoresightPlugin;
using System.Collections.Generic;

namespace OperatorsToolbox.SmartView
{
    public class ViewData
    {
        public string Name { get; set; }
        public string WindowName {get; set;}
        public int WindowId { get; set; }
        public bool LinkToSensorView { get; set; }
        public SensorViewData SensorBoresightData { get; set; }
        public string ViewAxes { get; set; }
        public string ViewTarget { get; set; }
        public string ViewType { get; set; }
        public string ViewType2D { get; set; }
        public bool EnableUniversalOrbitTrack { get; set; }
        public bool UniqueLeadTrail { get; set; }
        public bool EnableUniversalGroundTrack { get; set; }
        public string LeadType { get; set; }
        public string LeadTime { get; set; }
        public string TrailType { get; set; }
        public string TrailTime { get; set; }
        public SVDataDisplay PrimaryDataDisplay { get; set; }
        public SVDataDisplay SecondaryDataDisplay { get; set; }
        public bool ObjectHideShow { get; set; }
        public bool VectorHideShow { get; set; }
        public string ZoomCenterLat { get; set; }
        public string ZoomCenterLong { get; set; }
        public string ZoomCenterDelta { get; set; }
        public List<ObjectData> ViewObjectData { get; set; }
        public bool EnableProximityBox { get; set; }
        public string ProxGridSpacing { get; set; }
        public bool EnableProximityEllipsoid { get; set; }
        public string EllipsoidX { get; set; }
        public string EllipsoidY { get; set; }
        public string EllipsoidZ { get; set; }
        public bool EnableGeoBox { get; set; }
        public string GeoLongitude { get; set; }
        public string GeoEastWest { get; set; }
        public string GeoNorthSouth { get; set; }
        public string GeoRadius { get; set; }
        public bool UseAnimationTime { get; set; }
        public string AnimationTime { get; set; }
        public bool UseStoredView { get; set; }
        public string StoredViewName { get; set; }
        public bool UseCameraPath { get; set; }
        public string CameraPathName { get; set; }
        public bool ApplyVectorScaling { get; set; }
        public double VectorScalingValue { get; set; }
        public bool OverrideTimeStep { get; set; }
        public string TimeStep { get; set; }

        public ViewData()
        {
            PrimaryDataDisplay = new SVDataDisplay();
            SecondaryDataDisplay = new SVDataDisplay();
            ViewObjectData = new List<ObjectData>();
            SensorBoresightData = new SensorViewData();
            LinkToSensorView = false;
        }
    }
}
