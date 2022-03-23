using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agi.Radar.ClutterMap.CSharp.Example
{
    public interface IExample1
    {
        double ConstantCoefficient { get; set; }
        bool ApplyGrazingMask { get; set; }
    }
}
