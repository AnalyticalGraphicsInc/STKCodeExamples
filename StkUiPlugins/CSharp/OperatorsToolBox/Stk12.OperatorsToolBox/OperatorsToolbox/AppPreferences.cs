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
        //Command line events
        public bool ActiveSaveScript { get; set; }
        public string SaveScriptPath { get; set; }
        public string SaveScriptArgs { get; set; }
        public bool ActiveCloseScript { get; set; }
        public string CloseScriptPath { get; set; }
        public string CloseScriptArgs { get; set; }
        public bool ActiveObjAddedScript { get; set; }
        public string ObjAddedScriptPath { get; set; }
        public bool ActiveObjDeletedScript { get; set; }
        public string ObjDeletedScriptPath { get; set; }
        public bool ActivePlaybackScript { get; set; }
        public string PlaybackScriptPath { get; set; }
        public string PlaybackScriptArgs { get; set; }
        public bool ActivePauseScript { get; set; }
        public string PauseScriptPath { get; set; }
        public string PauseScriptArgs { get; set; }

        public AppPreferences()
        {
            EventTypeList = new List<string>();
            EventImageLocations = new List<string>();
            PluginConfigList = new List<int>();
            ActiveSaveScript = false;
            SaveScriptPath = "";
            ActiveCloseScript = false;
            CloseScriptPath = "";
            ActiveObjAddedScript = false;
            ObjAddedScriptPath = "";
            ActiveObjDeletedScript = false;
            ObjDeletedScriptPath = "";
            ActivePlaybackScript = false;
            PlaybackScriptPath = "";
            ActivePauseScript = false;
            PauseScriptPath = "";
        }
    }
}