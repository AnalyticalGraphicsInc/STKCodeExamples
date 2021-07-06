using System;
using AGI.STKUtil;
using AGI.STKObjects;
using AGI.STKX;

namespace OrbitSwitcher
{
    // Enumerations for variables 
    public enum SizeShapeTypes { Altitude, MeanMotion, Period, Radius, SemimajorAxis }

    public enum AscNodeTypes { LAN, RAAN }

    public enum LocationTypes { ArgumentOfLatitude, EccentricAnomaly, MeanAnomaly, TimePastAN, TimePastPerigee, TrueAnomaly }

    class Program
    {
        /* This code takes a set of Cartesian coordinates in a fixed frame and converts them into Keplerian orbital elements in an inertial frame
         * without using a satellite object (using conversion utility within STK Engine). The Keplerian elements you would like to return can be specified
         * by changing the enum values near the top of the Main function.
         */
        static void Main(string[] args)
        {
            AgSTKXApplication app = new AgSTKXApplication();
            app.NoGraphics = true;
            AgStkObjectRoot root = new AgStkObjectRoot();

            // Here are some preliminary variables for the classical coordinate elements you would like to obtain 
            SizeShapeTypes SizeShapeType = SizeShapeTypes.Altitude;
            AscNodeTypes AscNodeType = AscNodeTypes.RAAN;
            LocationTypes LocationType = LocationTypes.ArgumentOfLatitude;

            // Here we create a new AgOrbitState object 
            IAgConversionUtility ConversionUtility = root.ConversionUtility;
            IAgOrbitState cartesianOrbit = ConversionUtility.NewOrbitStateOnEarth();

            // Here is how you display and change the epoch 
            Console.WriteLine("Epoch:");
            cartesianOrbit.Epoch = "1 Jun 2003 17:00:00.000";
            Console.WriteLine(cartesianOrbit.Epoch);

            // Here we assign whatever Cartesian coordinates we would like to in a fixed frame
            cartesianOrbit.AssignCartesian(AgECoordinateSystem.eCoordinateSystemFixed, 5598.42, -14988.6, 4.80738, 3.408, 1.27376, 2.60903);

            // Now we convert the orbit to a classical orbit state 
            IAgOrbitStateClassical classicalOrbit = cartesianOrbit.ConvertTo(AgEOrbitStateType.eOrbitStateClassical) as IAgOrbitStateClassical;
            Console.WriteLine(classicalOrbit.CoordinateSystemType);

            // Prints out the first two classical orbit elements 
            switch (SizeShapeType)
            {

                case SizeShapeTypes.Altitude:
                    classicalOrbit.SizeShapeType = AgEClassicalSizeShape.eSizeShapeAltitude;
                    IAgClassicalSizeShapeAltitude sizeShapeAltitude = classicalOrbit.SizeShape as IAgClassicalSizeShapeAltitude;
                    Console.WriteLine("Apogee Altitude:");
                    Console.WriteLine(sizeShapeAltitude.ApogeeAltitude);
                    Console.WriteLine("Perigee Altitude:");
                    Console.WriteLine(sizeShapeAltitude.PerigeeAltitude);
                    break;

                case SizeShapeTypes.MeanMotion:
                    classicalOrbit.SizeShapeType = AgEClassicalSizeShape.eSizeShapeMeanMotion;
                    IAgClassicalSizeShapeMeanMotion sizeShapeMeanMotion = classicalOrbit.SizeShape as IAgClassicalSizeShapeMeanMotion;
                    Console.WriteLine("Mean Motion:");
                    Console.WriteLine(sizeShapeMeanMotion.MeanMotion);
                    Console.WriteLine("Eccentricity:");
                    Console.WriteLine(sizeShapeMeanMotion.Eccentricity);
                    break;

                case SizeShapeTypes.Period:
                    classicalOrbit.SizeShapeType = AgEClassicalSizeShape.eSizeShapePeriod;
                    IAgClassicalSizeShapePeriod sizeShapePeriod = classicalOrbit.SizeShape as IAgClassicalSizeShapePeriod;
                    Console.WriteLine("Period:");
                    Console.WriteLine(sizeShapePeriod.Period);
                    Console.WriteLine("Eccentricity:");
                    Console.WriteLine(sizeShapePeriod.Eccentricity);
                    break;

                case SizeShapeTypes.Radius:
                    classicalOrbit.SizeShapeType = AgEClassicalSizeShape.eSizeShapeRadius;
                    IAgClassicalSizeShapeRadius sizeShapeRadius = classicalOrbit.SizeShape as IAgClassicalSizeShapeRadius;
                    Console.WriteLine("Apogee Radius:");
                    Console.WriteLine(sizeShapeRadius.ApogeeRadius);
                    Console.WriteLine("Perigee Radius:");
                    Console.WriteLine(sizeShapeRadius.PerigeeRadius);
                    break;

                case SizeShapeTypes.SemimajorAxis:
                    classicalOrbit.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
                    IAgClassicalSizeShapeSemimajorAxis sizeShapeSemimajorAxis = classicalOrbit.SizeShape as IAgClassicalSizeShapeSemimajorAxis;
                    Console.WriteLine("Semimajor Axis:");
                    Console.WriteLine(sizeShapeSemimajorAxis.SemiMajorAxis);
                    Console.WriteLine("Eccentricity:");
                    Console.WriteLine(sizeShapeSemimajorAxis.Eccentricity);
                    break;
            }

            // Prints the inclination and argument of perigee 
            IAgClassicalOrientation orientation = classicalOrbit.Orientation;
            Console.WriteLine("Inclination:");
            Console.WriteLine(orientation.Inclination);
            Console.WriteLine("Argument of Perigee:");
            Console.WriteLine(orientation.ArgOfPerigee);

            // This section prints the ascending node value 
            Console.WriteLine("Ascending Node:");
            switch (AscNodeType)
            {

                case AscNodeTypes.RAAN:
                    orientation.AscNodeType = AgEOrientationAscNode.eAscNodeRAAN;
                    IAgOrientationAscNodeRAAN ascNodeRAAN = orientation.AscNode as IAgOrientationAscNodeRAAN;
                    Console.WriteLine(ascNodeRAAN.Value);
                    break;

                case AscNodeTypes.LAN:
                    orientation.AscNodeType = AgEOrientationAscNode.eAscNodeLAN;
                    IAgOrientationAscNodeLAN ascNodeLAN = orientation.AscNode as IAgOrientationAscNodeLAN;
                    Console.WriteLine(ascNodeLAN.Value);
                    break;
            }

            // This section prints the location of the satellite along the orbit in terms of whatever you would like 
            Console.WriteLine("Location:");
            switch (LocationType)
            {

                case LocationTypes.ArgumentOfLatitude:
                    classicalOrbit.LocationType = AgEClassicalLocation.eLocationArgumentOfLatitude;
                    IAgClassicalLocationArgumentOfLatitude locationArgumentOfLatitude = classicalOrbit.Location as IAgClassicalLocationArgumentOfLatitude;
                    Console.WriteLine(locationArgumentOfLatitude.Value);
                    break;

                case LocationTypes.EccentricAnomaly:
                    classicalOrbit.LocationType = AgEClassicalLocation.eLocationEccentricAnomaly;
                    IAgClassicalLocationEccentricAnomaly locationSpecificEccentricAnomaly = classicalOrbit.Location as IAgClassicalLocationEccentricAnomaly;
                    Console.WriteLine(locationSpecificEccentricAnomaly.Value);
                    break;

                case LocationTypes.MeanAnomaly:
                    classicalOrbit.LocationType = AgEClassicalLocation.eLocationMeanAnomaly;
                    IAgClassicalLocationMeanAnomaly locationSpecificMeanAnomaly = classicalOrbit.Location as IAgClassicalLocationMeanAnomaly;
                    Console.WriteLine(locationSpecificMeanAnomaly.Value);
                    break;

                case LocationTypes.TimePastAN:
                    classicalOrbit.LocationType = AgEClassicalLocation.eLocationTimePastAN;
                    IAgClassicalLocationTimePastAN locationSpecificTimePastAN = classicalOrbit.Location as IAgClassicalLocationTimePastAN;
                    Console.WriteLine(locationSpecificTimePastAN.Value);
                    break;

                case LocationTypes.TimePastPerigee:
                    classicalOrbit.LocationType = AgEClassicalLocation.eLocationTimePastPerigee;
                    IAgClassicalLocationTimePastPerigee locationSpecificTimePastPerigee = classicalOrbit.Location as IAgClassicalLocationTimePastPerigee;
                    Console.WriteLine(locationSpecificTimePastPerigee.Value);
                    break;

                case LocationTypes.TrueAnomaly:
                    classicalOrbit.LocationType = AgEClassicalLocation.eLocationTrueAnomaly;
                    IAgClassicalLocationTrueAnomaly locationTrueAnomaly = classicalOrbit.Location as IAgClassicalLocationTrueAnomaly;
                    Console.WriteLine(locationTrueAnomaly.Value);
                    break;
            }

            Console.ReadLine();
        }
    }
}