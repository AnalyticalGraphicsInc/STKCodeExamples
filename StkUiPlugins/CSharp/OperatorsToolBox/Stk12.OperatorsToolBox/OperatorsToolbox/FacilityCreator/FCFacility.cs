namespace OperatorsToolbox.FacilityCreator
{
    public class FcFacility
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Altitude { get; set; }
        public string CadanceName { get; set; }
        public bool IsOpt { get; set; }
        public RadarParams RParams { get; set; }
        public OpticalParams OParams { get; set; }
        public bool UseDefaultCnst { get; set; }

        public FcFacility(FcFacility curFac)
        {
            Name = curFac.Name;
            Type = curFac.Type;
            Latitude = curFac.Latitude;
            Longitude = curFac.Longitude;
            Altitude = curFac.Altitude;
            CadanceName = curFac.CadanceName;
            IsOpt = curFac.IsOpt;
            RParams = new RadarParams(curFac.RParams);
            OParams = new OpticalParams(curFac.OParams);
            UseDefaultCnst = curFac.UseDefaultCnst;
        }

        public FcFacility()
        {

        }

    }
}
