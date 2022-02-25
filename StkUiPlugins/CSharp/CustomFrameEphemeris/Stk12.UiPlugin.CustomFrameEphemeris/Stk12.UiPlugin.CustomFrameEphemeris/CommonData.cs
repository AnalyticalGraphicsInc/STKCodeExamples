using AGI.STKObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stk12.UiPlugin.CustomFrameEphemeris
{
    public static class CommonData
    {
        public static AgStkObjectRoot StkRoot { get; set; }
        public static string StkDateFormat
        {
            get { return "dd MMM yyyy HH:mm:ss.ffffff"; }
        }
    }
}
