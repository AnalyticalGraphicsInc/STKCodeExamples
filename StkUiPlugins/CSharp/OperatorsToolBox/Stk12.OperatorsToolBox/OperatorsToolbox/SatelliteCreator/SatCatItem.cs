using System.Collections.Generic;

namespace OperatorsToolbox.SatelliteCreator
{
    public class SatCatItem
    {
        public string Scc { get; set; }
        public string CommonName { get; set; }
        public string OtherName { get; set; }
        public string Fofo { get; set; }
        public List<string> MetadataTypes { get; set; }
        public string Metadata1 { get; set; }
        public string Metadata2 { get; set; }
        public string Metadata3 { get; set; }
        public string Metadata4 { get; set; }
        public string Metadata5 { get; set; }
        public double Fov { get; set; }

        public SatCatItem()
        {
            MetadataTypes = new List<string>();
        }
    }
}
