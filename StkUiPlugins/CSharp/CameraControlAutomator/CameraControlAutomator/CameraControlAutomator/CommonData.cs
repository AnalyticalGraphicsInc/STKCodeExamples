using AGI.STKObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraControlAutomator
{
    public static class CommonData
    {
        public static AgStkObjectRoot StkRoot { get; set; }
        public static string SelectedObjectName { get; set; }
        public static string SelectedObjectClass { get; set; }
        public static string StkDateFormat
        {
            get { return "dd MMM yyyy HH:mm:ss.ffffff"; }
        }
    }
}
