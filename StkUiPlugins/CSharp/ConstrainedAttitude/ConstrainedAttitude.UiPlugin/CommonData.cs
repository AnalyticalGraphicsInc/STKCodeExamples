using AGI.STKObjects;
using AGI.Ui.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrainedAttitude.UiPlugin
{
    public static class CommonData
    {
        public static AgStkObjectRoot StkRoot { get; set; }
        public static string StkDateFormat
        {
            get { return "dd MMM yyyy HH:mm:ss.ffffff"; }
        }

        public static IAgUiPluginSite Site { get; set; }
    }
}
