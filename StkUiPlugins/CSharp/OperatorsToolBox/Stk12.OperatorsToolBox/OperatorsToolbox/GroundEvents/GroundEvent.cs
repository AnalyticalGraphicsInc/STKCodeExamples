using System.Collections.Generic;
using System.Drawing;

namespace OperatorsToolbox.GroundEvents
{
    public class GroundEvent
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string StartTime { get; set; }
        public string MilStartTime { get; set; }
        public string MilStopTime { get; set; }
        public string StopTime { get; set; }
        public string SsrType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Poc { get; set; }
        public string PocPhone { get; set; }
        public string PocEmail { get; set; }
        public string Classification { get; set; }
        public List<string> AssetList { get; set; }
        public List<SubObject> SubObjects { get; set; }
        
        public GroundEvent()
        {
            SubObjects = new List<SubObject>();
            AssetList = new List<string>();
        }
    }
}
