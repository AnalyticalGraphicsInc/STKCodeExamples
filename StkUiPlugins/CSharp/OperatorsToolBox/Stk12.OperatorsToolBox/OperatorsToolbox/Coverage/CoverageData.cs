using System.Collections.Generic;

namespace OperatorsToolbox.Coverage
{
    public class CoverageData
    {
        public string CdName { get; set; }
        public int FomType { get; set; }
        public List<string> AssetList { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string ContourStart { get; set; }
        public string ContourStop { get; set; }
        public string ContourStep { get; set; }
        public string TargetName{ get; set;}
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CoverageShape { get; set; }
        public string ObjectName { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public bool IsEllipse { get; set; }
        public double BoundSize { get; set; }
        public double MajorAxis { get; set; }
        public double MinorAxis { get; set; }
        public string PointGran { get; set; }
        public bool IsCustom { get; set; }
        public List<string> TargetList { get; set; }
        public bool UseConstraint { get; set; }
        public string ConstraintObject { get; set; }

        public CoverageData()
        {
            AssetList = new List<string>();
            TargetList = new List<string>();
            UseConstraint = false;
        }

        public CoverageData(CoverageData data)
        {
            CdName = data.CdName;
            FomType = data.FomType;
            ContourStart = data.ContourStart;
            ContourStop = data.ContourStop;
            ContourStep = data.ContourStep;
            Country = data.Country;
            AssetList = new List<string>(data.AssetList);
            TargetList = new List<string>(data.TargetList);
            Type = data.Type;
            TargetName = data.TargetName;
            Latitude = data.Latitude;
            Longitude = data.Longitude;
            CoverageShape = data.CoverageShape;
            ObjectName = data.ObjectName;
            Country = data.Country;
            StartTime = data.StartTime;
            StopTime = data.StopTime;
            IsEllipse = data.IsEllipse;
            BoundSize = data.BoundSize;
            MajorAxis = data.MajorAxis;
            MinorAxis = data.MinorAxis;
            PointGran = data.PointGran;
            IsCustom = data.IsCustom;
            UseConstraint = data.UseConstraint;
            ConstraintObject = data.ConstraintObject;
        }
    }
}
