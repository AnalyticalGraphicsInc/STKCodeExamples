using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsToolbox.FacilityCreator
{
    public class FCSensor
    {
        public string SensorName { get; set; }
        public OpticalParams OParams { get; set; }
        public RadarParams RParams { get; set; }

        public FCSensor(FCSensor sensor)
        {
            RParams = new RadarParams(sensor.RParams);
            OParams = new OpticalParams(sensor.OParams);
            SensorName = sensor.SensorName;
        }

        public FCSensor()
        {

        }
    }
}
