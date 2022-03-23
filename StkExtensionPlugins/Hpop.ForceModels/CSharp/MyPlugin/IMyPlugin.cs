//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//
using System;

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace MYPLUGIN_NAMESPACE
{
	// NOTE:  Name your custom COM Interface with the identical name as your
	// plugin class's name, and append an I to the beginning of it.
	public interface IMYPLUGIN
	{
		// NOTE:  Add your custom COM Interface Property configuration settings here.
		//        Follow the standard C# rules for exposing properties.
		bool	Enabled					{ get; set; }
		bool	DebugMode				{ get; set; }
		int		EvalMsgInterval			{ get; set; }
		int		PostEvalMsgInterval		{ get; set; }
		int		PreNextMsgInterval		{ get; set; }
	}
}
//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//