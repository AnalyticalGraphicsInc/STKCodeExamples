using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsToolbox.SatelliteCreator
{
    public class FilterConfig
    {
        public int FilterMetadataID { get; set; }
        public bool IsActive { get; set; }
        public string FilterType { get; set;}
        public List<string> SelectedOptions { get; set; }

        public FilterConfig()
        {
            FilterType = null;
            IsActive = false;
            FilterMetadataID = 0;
        }
    }
}
