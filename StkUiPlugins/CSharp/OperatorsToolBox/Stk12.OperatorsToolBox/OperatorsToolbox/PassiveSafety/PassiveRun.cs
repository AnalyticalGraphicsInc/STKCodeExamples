using System;

namespace OperatorsToolbox.PassiveSafety
{
    public class PassiveRun
    {
        public Array Range { get; set; }
        public Array Intrack { get; set; }
        public Array Radial { get; set; }
        public Array Crosstrack { get; set; }
        public double MinRange { get; set; }
        public double MinIntrack { get; set; }
        public double MinRadial { get; set; }
        public double MinCrosstrack { get; set; }
        public double Vx { get; set; }
        public double Vy { get; set; }
        public double Vz { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double PropTime { get; set; }
        public string ManeuverTime { get; set; }
        public double UserMinR { get; set; }
        public double UserMinI { get; set; }
        public double UserMinC { get; set; }
        public double UserMinRange { get; set; }
        public bool IsSpherical { get; set; }
        public bool Safe { get; set; }
    }
}
