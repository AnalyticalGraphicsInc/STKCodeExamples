using System;
using System.Collections.Generic;
using System.Text;

namespace AGI.SRP.LightReflection.Spherical.CSharp.Example
{
    // NOTE:  Name your custom COM Interface with the identical name as your
    // plugin class's name, and append an I to the beginning of it.

    public interface ISRPPluginExample
    {
        // NOTE:  Add your custom COM Interface Property configuration settings here.
        //        Follow the standard C# rules for exposing properties.
        string Name { get; set; }
        bool Enabled { get; set; }
        bool DebugMode { get; set; }
        int MsgInterval { get; set; }
        double SRPArea { get; set; }
        string RefFrame { get; set; }
    }

}
