using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsToolbox.SensorBoresightPlugin
{
    public class SensorViewData
    {
        public string SelectedSensor { get; set; }
        public bool LinkToView { get; set; }
        public string ViewName { get; set; }
        public int VertWinSize { get; set; }
        public bool ShowLatLon { get; set; }
        public bool ShowCompass { get; set; }
        public bool ShowRulers { get; set; }
        public bool ShowCrosshairs { get; set; }
        public bool AutoUpVector { get; set; }
        public string UpVector { get; set; }
        public SensorViewClass.CrosshairType CrosshairType { get; set; }

        public SensorViewData()
        {
            SelectedSensor = null;
            LinkToView = false;
            VertWinSize = 400;
            ShowCompass = false;
            ShowCrosshairs = false;
            ShowRulers = false;
            ShowLatLon = false;
            AutoUpVector = true;
            CrosshairType = SensorViewClass.CrosshairType.Square;
        }
    }
}
