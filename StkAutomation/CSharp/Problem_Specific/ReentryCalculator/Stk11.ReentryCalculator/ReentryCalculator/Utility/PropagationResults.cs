using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReentryCalculator.Utility
{
    class PropagationResults
    {
        private string _nRun;
        private bool _isDecayed;
        private string _ephemerisPath;
        private string _impactLat;
        private string _impactLon;
        private string _impactAlt;
        private string _impactEpoch;

        public bool IsDecayed
        {
            get { return _isDecayed; }
            set { _isDecayed = value; }
        }

        public string ImpactLat
        {
            get { return _impactLat; }
            set { _impactLat = value; }
        }

        public string ImpactLon
        {
            get { return _impactLon; }
            set { _impactLon = value; }
        }

        public string ImpactAlt
        {
            get { return _impactAlt; }
            set { _impactAlt = value; }
        }

        public string ImpactEpoch
        {
            get { return _impactEpoch; }
            set { _impactEpoch = value; }
        }

        public string RunNumber
        {
            get { return _nRun; }
            set { _nRun = value; }
        }

        public string EphemerisFilePath
        {
            get { return _ephemerisPath; }
            set { _ephemerisPath = value; }
        }
    }
}
