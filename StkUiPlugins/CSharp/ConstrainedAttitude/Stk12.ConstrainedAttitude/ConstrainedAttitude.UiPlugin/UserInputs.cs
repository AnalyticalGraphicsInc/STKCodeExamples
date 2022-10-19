using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrainedAttitude.UiPlugin
{
    public class UserInputs
    {
        public string AlignedVectorName { get; set; }

        public string AlignedBodyAxis { get; set; }

        public string ConstrainedVectorName { get; set; }

        public string ConstrainedBodyAxis {get; set;}

        public double AngleLimit { get; set; }
    }
}
