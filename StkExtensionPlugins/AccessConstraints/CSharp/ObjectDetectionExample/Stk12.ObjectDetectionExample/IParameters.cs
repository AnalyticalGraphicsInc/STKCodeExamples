//=====================================================//
//  Copyright 2020, Analytical Graphics, Inc.     //
//=====================================================//

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace Stk12.AccessConstraint.ObjectDetection
{
	// By not declaring otherwise, an auto-generated COM interface
	// will be created for this class (a IDispatch interface)
	public interface IParameters
	{
		double TargetCrossSectionSqMeters { get; set; }

		bool	DebugMode			{ get; set; }
		int		MsgInterval			{ get; set; }
	}
}
//=====================================================//
//  Copyright 2020, Analytical Graphics, Inc      //
//=====================================================//