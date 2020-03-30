//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//

namespace AGI.Access.Constraint.Plugin.CSharp.ReadFromFile
{
    public class ExternalValue
	{
		public double Latitude;
		public double Longitude;
		public double Value;
		public ExternalValue()
		{
		}
			public ExternalValue(string line)
		{
			var lineParts = line.Split(',');
			Latitude = double.Parse(lineParts[0].Trim());
			Longitude = double.Parse(lineParts[1].Trim());
			Value = double.Parse(lineParts[2].Trim());

		}
	}
}
//=====================================================//
//  Copyright 2006, Analytical Graphics, Inc.          //
//=====================================================//