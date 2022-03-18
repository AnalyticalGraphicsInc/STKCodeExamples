using AGI.STKObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stk12.UiPlugin.Articulations
{
    public static class CommonData
    {
        public static AgStkObjectRoot StkRoot { get; set; }
        public static List<string> objectPaths { get; set; }
        public static string simpleName { get; set; }
        public static string objectName { get; set; }
        public static string objectClass { get; set; }
        public static string articName { get; set; }
        public static string currentSection { get; set; }
        public static int createdArticCount { get; set; }
        public static List<Section> sectionList;
        public static List<Section> ClipboardSectionList;
        public static int totalSectionCount { get; set; }
        public static int selectedArtic { get; set; }
        public static string directoryStr { get; set; }
        public static string fileStr { get; set; }
        public static Boolean added { get; set; }
        public static Boolean applied { get; set; }
        public static Boolean linkChanged { get; set; }
        public static int previousSelected { get; set; }
        public static List<Array> AttitudeData { get; set; }
        public static string MainBody { get; set; }
        public static string StkDateFormat
        {
            get { return "dd MMM yyyy HH:mm:ss.ffffff"; }
        }
    }
}
