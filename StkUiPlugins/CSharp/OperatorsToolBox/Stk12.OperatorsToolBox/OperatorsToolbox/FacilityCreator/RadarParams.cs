namespace OperatorsToolbox.FacilityCreator
{
    public class RadarParams
    {
        public string SolarExAngle { get; set; }
        public string MinRange { get; set; }
        public string MaxRange { get; set; }
        public string MinEl { get; set; }
        public string MaxEl { get; set; }
        public string HalfAngle { get; set; }
        public string MinAz { get; set; }
        public string MaxAz { get; set; }
        public string Az { get; set; }
        public string El { get; set; }


        public RadarParams()
        {

        }

        public RadarParams(RadarParams data)
        {
            if (data==null)
            {
                MinEl = "0";
                MaxEl = "90";
                MinRange = "1600";
                MaxRange = "40000";
                SolarExAngle = "10";
                HalfAngle = "85";
                MinAz = "0";
                MaxAz = "360";
                Az = "0";
                El = "90";
            }
            else
            {
                SolarExAngle = data.SolarExAngle;
                MinRange = data.MinRange;
                MaxRange = data.MaxRange;
                MinEl = data.MinEl;
                MaxEl = data.MaxEl;
                HalfAngle = data.HalfAngle;
                MinAz = data.MinAz;
                MaxAz = data.MaxAz;
                Az = data.Az;
                El = data.El;
            }
        }

    }
}
