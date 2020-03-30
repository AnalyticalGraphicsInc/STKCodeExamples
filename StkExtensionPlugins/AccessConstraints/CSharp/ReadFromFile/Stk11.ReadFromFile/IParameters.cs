//=====================================================//
//  Copyright 2006-2007, Analytical Graphics, Inc.     //
//=====================================================//

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace AGI.Access.Constraint.Plugin.CSharp.ReadFromFile
{
	// By not declaring otherwise, an auto-generated COM interface
	// will be created for this class (a IDispatch interface)
	public interface IParameters
	{
		string	ExternalFilePath			{ get; set; }

		bool	DebugMode			{ get; set; }
		int		MsgInterval			{ get; set; }
	}
}
//=====================================================//
//  Copyright 2006-2007, Analytical Graphics, Inc      //
//=====================================================//