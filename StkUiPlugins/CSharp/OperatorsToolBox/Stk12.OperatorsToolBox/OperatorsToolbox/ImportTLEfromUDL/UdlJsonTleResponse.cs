using System;

namespace OperatorsToolbox.ImportTLEfromUDL
{
    class UdlJsonTleResponse
    {

        public string IdElset { get; set; }
        public string ClassificationMarking { get; set; }
        public int SatNo { get; set; }
        public DateTime Epoch { get; set; }
        public double MeanMotion { get; set; }
        public string IdOnOrbit { get; set; }
        public double Eccentricity { get; set; }
        public double Inclination { get; set; }
        public double Raan { get; set; }
        public double ArgOfPerigee { get; set; }
        public double MeanAnomaly { get; set; }
        public int RevNo { get; set; }
        public double BStar { get; set; }
        public double MeanMotionDot { get; set; }
        public double MeanMotionDDot { get; set; }
        public double SemiMajorAxis { get; set; }
        public double Period { get; set; }
        public double Apogee { get; set; }
        public double Perigee { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Source { get; set; }
        public string DataMode { get; set; }

    }
}
