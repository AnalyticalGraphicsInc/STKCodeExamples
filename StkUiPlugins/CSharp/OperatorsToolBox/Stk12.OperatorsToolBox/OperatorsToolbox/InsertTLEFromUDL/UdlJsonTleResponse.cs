using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertTLEFromUDL
{
    class UdlJsonTleResponse
    {

        public string idElset { get; set; }
        public string classificationMarking { get; set; }
        public int satNo { get; set; }
        public DateTime epoch { get; set; }
        public double meanMotion { get; set; }
        public string idOnOrbit { get; set; }
        public double eccentricity { get; set; }
        public double inclination { get; set; }
        public double raan { get; set; }
        public double argOfPerigee { get; set; }
        public double meanAnomaly { get; set; }
        public int revNo { get; set; }
        public double bStar { get; set; }
        public double meanMotionDot { get; set; }
        public double meanMotionDDot { get; set; }
        public double semiMajorAxis { get; set; }
        public double period { get; set; }
        public double apogee { get; set; }
        public double perigee { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string source { get; set; }
        public string dataMode { get; set; }

    }
}
