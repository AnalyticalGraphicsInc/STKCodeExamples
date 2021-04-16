using AGI.STKObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperatorsToolbox.Coverage;
using OperatorsToolbox.FacilityCreator;
using OperatorsToolbox.GroundEvents;
using OperatorsToolbox.PassiveSafety;
using OperatorsToolbox.SatelliteCreator;
using OperatorsToolbox.SmartView;
using OperatorsToolbox.VolumeCreator;

namespace OperatorsToolbox
{
    public static class CommonData
    {
        //Application variables
        public static AppPreferences Preferences { get; set; }
        public static int _numPlugins = 14;
        public static List<string> PluginOptions;
        public static AgStkObjectRoot StkRoot { get; set; }
        public static string InstallDir { get; set; }
        public static int PanelHeight { get; set; }
        public static bool PluginRemoved { get; set; }
        //Solar Angles
        public static List<string> SelectedObservers;
        public static List<string> SelectedTargets;
        //Passive Safety
        public static List<PassiveRun> RunList { get; set; }
        public static bool BeenRun { get; set; }
        public static bool HasManeuvers { get; set; }
        public static string TargetName { get; set; }
        public static string ActorName { get; set; }
        //Smart View
        public static List<ViewData> SavedViewList;
        public static bool NewView { get; set; }
        public static bool UpdatedView { get; set; }
        public static string DirectoryStr { get; set; }
        public static string PreviousDataDisplayObject { get; set; }
        public static int SelectedIndex { get; set; }
        public static List<ObjectData> InitialObjectData { get; set; }
        public static List<ObjectData> CurrentViewObjectData { get; set; }
        //Satellite database metadata lists
        public static List<string> MetadataTypeList;
        public static List<string> MetadataOptions1;
        public static List<string> MetadataOptions2;
        public static List<string> MetadataOptions3;
        public static List<string> MetadataOptions4;
        public static List<string> MetadataOptions5;
        public static List<string> SatCatFofo;
        public static List<SatCatItem> SatCatItemList;
        //Volume creator
        public static string LocationFilePath { get; set; }
        public static string MissileFilePath { get; set; }
        public static string FacilityLat { get; set; }
        public static string FacilityLong { get; set; }
        public static string FacilityName { get; set; }
        public static bool FacilitySelected { get; set; }
        public static List<VolumeConfig> VolumeList;
        public static List<LocationConfig> LocationList;
        public static int TvSelectedIndex { get; set; }
        public static string VolumeName { get; set; }
        public static string LocationName { get; set; }
        public static bool FromEdit { get; set; }
        public static int LocationIndex { get; set; }
        //Station Access
        public static List<string> ToObjectNames { get; set; }
        public static List<string> FromObjectNames { get; set; }
        public static List<string> SensorNames { get; set; }
        public static List<string> SensorParentNames { get; set; }
        //Facility Creator
        public static bool CadenceEdit { get; set; }
        public static int CadenceSelected { get; set; }
        public static List<SensorCadance> Cadences { get; set; }
        public static bool CadenceSaved { get; set; }
        //Ground Events
        public static List<GroundEvent> CurrentEvents;
        public static bool NewSsrCreated;
        public static bool TypeChanged { get; set; }
        public static int SubObjectIndex { get; set; }
        public static int EventSelectedIndex { get; set; }
        public static string EventFileStr { get; set; }
        //Coverage
        public static List<CoverageData> CoverageList { get; set; }
        public static int CoverageIndex{ get; set; }
        public static bool CovEdit { get; set; }
        public static bool CovDefFail { get; set; }
        public static bool NewCoverage { get; set; }
        public static string OaName { get; set; }
        public static bool CoverageCompute { get; set; }

        public static string StkDateFormat
        {
            get { return "dd MMM yyyy HH:mm:ss.ffffff"; }
        }
    }
}
