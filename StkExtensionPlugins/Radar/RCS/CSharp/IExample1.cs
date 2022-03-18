using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agi.Radar.RCS.CSharp.Example
{
    public interface IExample1
    {
        double ConstantRCS { get; set; }
        bool EnablePolarization { get; set; }
    }
}
