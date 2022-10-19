//=====================================================//
//  Copyright 2012, Analytical Graphics, Inc.          //
//=====================================================//
using System;

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace Agi.VGT.Vector.Plugin.Examples.CSharp
{
    // NOTE:  Name your custom COM Interface with the identical name as your
    // plugin class's name, and append an I to the beginning of it.
    public interface IExample1
    {
        double MyDouble { get; set; }
        string MyString { get; set; }
    }
}
//=====================================================//
//  Copyright 2012, Analytical Graphics, Inc.          //
//=====================================================//