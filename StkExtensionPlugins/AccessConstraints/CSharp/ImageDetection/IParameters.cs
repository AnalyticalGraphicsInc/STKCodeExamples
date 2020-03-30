//=====================================================//
//  Copyright 2006-2007, Analytical Graphics, Inc.     //
//=====================================================//

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace AGI.Access.Constraint.Plugin.ImageDetection
{
	// By not declaring otherwise, an auto-generated COM interface
	// will be created for this class (a IDispatch interface)
	public interface IParameters
	{
		double	Diameter			{ get; set; }
		double	Wavelength			{ get; set; }
		double	OpticalRatio		{ get; set; }
		double	NIIRS_a				{ get; set; }
		double	NIIRS_b				{ get; set; }
		double	NIIRS_RER			{ get; set; }
        double  TargetSize          { get; set; }
		bool	DebugMode			{ get; set; }
		int		MsgInterval			{ get; set; }
	}
}
//=====================================================//
//  Copyright 2006-2007, Analytical Graphics, Inc      //
//=====================================================//