using System.Collections.Generic;

namespace OperatorsToolbox
{
    public class AppPreferences
    {
        //Plugin toolbar config
        public List<int> PluginConfigList;
        //Event Type Data
        public List<string> EventTypeList;
        public List<string> EventImageLocations;
        //Database Locations
        public string SatCatLocation { get; set; }
        public string SatDatabaseLocation { get; set; }
        public string AoiLocation { get; set; }
        public string UdlUrl { get; set; }
        public string TemplatesDirectory { get; set; }
        //Graphics Settings
        public bool SensorGraphicsDisplay { get; set; }
        public bool BordersDisplay { get; set; }
        public bool IslandDisplay { get; set; }

        public AppPreferences()
        {
            EventTypeList = new List<string>();
            EventImageLocations = new List<string>();
            PluginConfigList = new List<int>();
        }
    }
}