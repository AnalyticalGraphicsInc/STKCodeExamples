namespace OperatorsToolbox.FacilityCreator
{
    public class OpticalParams
    {
        public string LunarExAngle { get; set; }
        public string SunElAngle { get; set; }
        public string MaxAlt { get; set; }
        public string MinRange { get; set; }
        public string MaxRange { get; set; }
        public string MinEl { get; set; }
        public string MaxEl { get; set; }
        public string HalfAngle { get; set; }

        public OpticalParams()
        {

        }

        public OpticalParams(OpticalParams data)
        {
            if (data==null)
            {
                MinEl = "0";
                MaxEl = "90";
                MinRange = "4800";
                MaxRange = "90000";
                LunarExAngle = "10";
                SunElAngle = "-12";
                HalfAngle = "70";
            }
            else
            {
                LunarExAngle = data.LunarExAngle;
                SunElAngle = data.SunElAngle;
                MaxAlt = data.MaxAlt;
                MinRange = data.MinRange;
                MaxRange = data.MaxRange;
                MinEl = data.MinEl;
                MaxEl = data.MaxEl;
                HalfAngle = data.HalfAngle;
            }
        }
    }
}
