//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//
using System;

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace AGI.Hpop.Plugin.Examples.Stk.ForceModeling.CSharp
{
	// NOTE:  Name your custom COM Interface with the identical name as your
	// plugin class's name, and append an I to the beginning of it.
	public interface IExample1
	{
		// NOTE:  Add your custom COM Interface Property configuration settings here.
		//        Follow the standard C# rules for exposing properties.
		string	Name					{ get; set; }
		bool	Enabled					{ get; set; }
		string	VectorName				{ get; set; }
		string	AccelRefFrame			        { get; set; }
		object[] AccelRefFrameChoices	                { get;      }
		double	AccelX					{ get; set; }
		double	AccelY					{ get; set; }
		double	AccelZ					{ get; set; }
		bool	MsgStatus				{ get; set; }
		int		EvalMsgInterval			{ get; set; }
		int		PostEvalMsgInterval		{ get; set; }
		int		PreNextMsgInterval		{ get; set; }
	}
}
//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//