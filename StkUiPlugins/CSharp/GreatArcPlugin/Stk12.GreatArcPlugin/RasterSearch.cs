using System;
using System.Collections.Generic;
using System.Text;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using AGI.STKUtil;
using System.Threading;

using System.Configuration;

using System.Windows.Forms;

namespace Agi.Ui.GreatArc.Stk12
{
    class RasterSearch
    {
        private AgStkObjectRoot root;
        private string installDir;

        public RasterSearch(AgStkObjectRoot m_root)
        {
            root = m_root;
            AGI.STKUtil.IAgExecCmdResult result = m_root.ExecuteCommand("GetDirectory / STKHome");
            installDir = result[0];

        }

        public enum SwathWidthType
        {
            NumberOfPasses,
            SensorFOV,
            GroundElev,
            SlantRange
        }

        public IAgAircraft CreateAircraft(string seed, bool createSensor, double sensorFOV, double speedKnots)
        {
            string aircraftName = uniqueName(seed, AgESTKObjectType.eAircraft);
            IAgAircraft aircraft = root.CurrentScenario.Children.New(AgESTKObjectType.eAircraft, aircraftName) as IAgAircraft;
            root.ExecuteCommand("SetAttitude */Aircraft/" + aircraftName + " Profile AircraftCoordTurn 10");
            IAgVOModel acModel = aircraft.VO.Model;
            acModel.ModelType = AgEModelType.eModelFile;
            IAgVOModelFile modelFile = acModel.ModelData as IAgVOModelFile;
            if (speedKnots < 200)
            {
                modelFile.Filename = installDir + @"STKData\VO\Models\Air\single_engine.mdl";
            }
            else if (speedKnots > 200 & speedKnots < 500)
            {
                modelFile.Filename = installDir + @"STKData\VO\Models\Air\uav.mdl";
            }
            else if (speedKnots > 300 & speedKnots < 500)
            {
                modelFile.Filename = installDir + @"STKData\VO\Models\Air\e-3a_sentry_awacs.mdl";
            }
            else if (speedKnots > 500 & speedKnots < 1200)
            {
                modelFile.Filename = installDir + @"STKData\VO\Models\Air\f-117a_nighthawk.mdl";
            }
            else
            {
                modelFile.Filename = installDir + @"STKData\VO\Models\Air\f-35_jsf_stovl.mdl";
            }

            if (createSensor)
            {
                IAgSensor sensor = ((IAgStkObject)aircraft).Children.New(AgESTKObjectType.eSensor, "Sensor_" + sensorFOV.ToString() + "deg") as IAgSensor;
                sensor.SetPatternType(AgESnPattern.eSnSimpleConic);
                AgSnSimpleConicPattern conic = sensor.Pattern as AgSnSimpleConicPattern;
                conic.ConeAngle = sensorFOV;
            }

            return aircraft;

        }

        public void CheckFlightEndurance(IAgAircraft flight, string enduranceUnit, double enduranceValue)
        {
            if (enduranceUnit.Equals("mi") || enduranceUnit.Equals("km") || enduranceUnit.Equals("nm"))
            {
                root.UnitPreferences["Distance"].SetCurrentUnit(enduranceUnit);
            }
            else
            {
                root.UnitPreferences["Time"].SetCurrentUnit(enduranceUnit);
            }
            bool tooLong = true;
            root.BeginUpdate();
            while (tooLong)
            {

                IAgDataPrvTimeVar dpDistance = ((IAgStkObject)flight).DataProviders["Distance"] as IAgDataPrvTimeVar;
                string evalTime = (flight.Route as IAgVePropagatorGreatArc).EphemerisInterval.FindStopTime().ToString();
                evalTime = evalTime.Substring(0, evalTime.IndexOf("."));

                IAgDrResult dpResult = dpDistance.ExecSingle(evalTime);

                Array distance = dpResult.DataSets.GetDataSetByName("Dist from start").GetValues();
                Array duration = dpResult.DataSets.GetDataSetByName("Time from start").GetValues();

                double distanceTraveled = (double)distance.GetValue(0);
                double timeTraveled = (double)duration.GetValue(0);

                if (enduranceUnit.Equals("mi") || enduranceUnit.Equals("km") || enduranceUnit.Equals("nm"))
                {
                    if (distanceTraveled < enduranceValue)
                    {
                        tooLong = false;
                    }
                }
                else if (timeTraveled < enduranceValue)
                {
                    tooLong = false;
                }
                     
                if (tooLong)
                {
                    IAgVePropagatorGreatArc route = (IAgVePropagatorGreatArc)flight.Route;
                    route.Waypoints.RemoveAt(route.Waypoints.Count - 2);
                    route.Propagate();
                }

            }
            root.UnitPreferences.ResetUnits();
            root.EndUpdate();

        }

        public List<Waypoint> ExpandingSquareWaypointGenerator(
           string areaTargetPath, double speed, double altitude, double turnRadius,
            SwathWidthType swathType, double swathParameter)
        {
            List<Waypoint> waypoints = new List<Waypoint>();

            IAgStkObject areaTarget = root.GetObjectFromPath(areaTargetPath);

            IAgAreaTarget areaTargetObj = (IAgAreaTarget)areaTarget;

            double minLat = 90;
            double maxLat = -90;
            double minLon = 180;
            double maxLon = -180;
            root.UnitPreferences["Angle"].SetCurrentUnit("deg");
            switch (areaTargetObj.AreaType)
            {
                case AgEAreaType.eEllipse:
                    IAgDataProviderGroup boundingProvider = areaTarget.DataProviders["Bounding Rectangle"] as IAgDataProviderGroup;
                    IAgDataPrvFixed dpElements = boundingProvider.Group["Corner Points"] as IAgDataPrvFixed;
                    IAgDrResult atDataPrvResult = dpElements.Exec();

                    Array atLats = atDataPrvResult.DataSets.GetDataSetByName("Geodetic-Lat").GetValues();
                    Array atLons = atDataPrvResult.DataSets.GetDataSetByName("Geodetic-Lon").GetValues();
                    foreach (object item in atLats)
                    {
                        if ((double)item > maxLat)
                        {
                            maxLat = (double)item;
                        }
                        if ((double)item < minLat)
                        {
                            minLat = (double)item;
                        }
                    }
                    foreach (object item in atLons)
                    {
                        if ((double)item > maxLon)
                        {
                            maxLon = (double)item;
                        }
                        if ((double)item < minLon)
                        {
                            minLon = (double)item;
                        }
                    }
                    break;
                case AgEAreaType.ePattern:
                    IAgAreaTypePatternCollection boundary = areaTargetObj.AreaTypeData as IAgAreaTypePatternCollection;
            
                    foreach (IAgAreaTypePattern item in boundary)
                    {
                        if ((double)item.Lat > maxLat)
                        {
                            maxLat = (double)item.Lat;
                        }
                        if ((double)item.Lat < minLat)
                        {
                            minLat = (double)item.Lat;
                        }

                        if ((double)item.Lon > maxLon)
                        {
                            maxLon = (double)item.Lon;
                        }
                        if ((double)item.Lon < minLon)
                        {
                            minLon = (double)item.Lon;
                        }
                    }

                    break;
                default:
                    break;
            }
            double deltaLat = maxLat - minLat;
            double deltaLon = maxLon - minLon;
            double swathWidth;
            double gridAngleStep;

            if (swathType != SwathWidthType.NumberOfPasses)
            {
                swathWidth = DetermineSwathWidth(swathType, swathParameter, altitude);
                gridAngleStep = swathWidth / 111000; // degrees longitude...ish
            }
            else
            {
                gridAngleStep = (((maxLat - minLat) + (maxLon - minLon)) / 2) / swathParameter;
            }

            double initialLat, initialLon;
            initialLat = maxLat - ((maxLat - minLat) / 2);
            initialLon = maxLon - ((maxLon - minLon) / 2);
            //IAgPosition centroid = (areaTarget as IAgAreaTarget).Position;
            
            //object centroidLat;
            //object centroidLon;
            //double centroidAlt;
            //centroid.QueryPlanetodetic(out centroidLat, out centroidLon, out centroidAlt);
            //initialLat = (double)centroidLat ;
            //initialLon = (double)centroidLon  ;

            Waypoint waypointInit = new Waypoint();
            waypointInit.Latitude = (double)initialLat;
            waypointInit.Longitude = (double)initialLon;
            waypointInit.Altitude = altitude;
            waypointInit.SurfaceAltitude = 0;
            waypointInit.Speed = speed;
            waypointInit.TurnRadius = turnRadius;

            waypoints.Add(waypointInit);
            bool keepCircling = true;
            Waypoint waypoint1, waypoint2, waypoint3, waypoint4; 
            double multiplier = 1;

            while (keepCircling)
            {

                waypoint1 = new Waypoint();
                waypoint1.Latitude = waypoints[waypoints.Count-1].Latitude + multiplier * gridAngleStep;
                waypoint1.Longitude = waypoints[waypoints.Count - 1].Longitude;
                waypoint1.Altitude = altitude;
                waypoint1.SurfaceAltitude = 0;
                waypoint1.Speed = speed;
                waypoint1.TurnRadius = turnRadius;
                waypoints.Add(waypoint1);

                waypoint2 = new Waypoint();
                waypoint2.Latitude = waypoints[waypoints.Count - 1].Latitude;
                waypoint2.Longitude = waypoints[waypoints.Count - 1].Longitude + multiplier * gridAngleStep;
                waypoint2.Altitude = altitude;
                waypoint2.SurfaceAltitude = 0;
                waypoint2.Speed = speed;
                waypoint2.TurnRadius = turnRadius;
                waypoints.Add(waypoint2);
                multiplier += 1.0;

                waypoint3 = new Waypoint();
                waypoint3.Latitude = waypoints[waypoints.Count - 1].Latitude - multiplier * gridAngleStep;
                waypoint3.Longitude = waypoints[waypoints.Count - 1].Longitude;
                waypoint3.Altitude = altitude;
                waypoint3.SurfaceAltitude = 0;
                waypoint3.Speed = speed;
                waypoint3.TurnRadius = turnRadius;
                waypoints.Add(waypoint3);

                waypoint4 = new Waypoint();
                waypoint4.Latitude = waypoints[waypoints.Count - 1].Latitude;
                waypoint4.Longitude = waypoints[waypoints.Count - 1].Longitude - multiplier * gridAngleStep;
                waypoint4.Altitude = altitude;
                waypoint4.SurfaceAltitude = 0;
                waypoint4.Speed = speed;
                waypoint4.TurnRadius = turnRadius;
                waypoints.Add(waypoint4);
                multiplier += 1.0;

                if ((waypoint4.Latitude > maxLat & waypoint4.Longitude > maxLon ) ||
                    (waypoint4.Latitude < minLat & waypoint4.Longitude < minLon))
                {
                    keepCircling = false;
                }
            }
                
            return waypoints;
        }

        public List<Waypoint> RasterSearchWaypointGenerator(
           string areaTargetPath, string heading, double speed, double altitude, double turnRadius,
            SwathWidthType swathType, double swathParameter)
        {
            List<Waypoint> waypoints = new List<Waypoint>();

            IAgStkObject areaTarget = root.GetObjectFromPath(areaTargetPath);

            IAgAreaTarget areaTargetObj = (IAgAreaTarget)areaTarget;
            bool areaTargetElActive = areaTarget.AccessConstraints.IsConstraintActive(AgEAccessConstraints.eCstrElevationAngle);
            double restoreAngle = 0;
            if (!areaTargetElActive)
            {
                IAgAccessCnstrAngle elevationMin = areaTarget.AccessConstraints.AddConstraint(AgEAccessConstraints.eCstrElevationAngle) as IAgAccessCnstrAngle;
                root.UnitPreferences["Angle"].SetCurrentUnit("deg");
                elevationMin.Angle = 90;
            }
            else
            {
                IAgAccessCnstrAngle elevationMin = areaTarget.AccessConstraints.GetActiveConstraint(AgEAccessConstraints.eCstrElevationAngle) as IAgAccessCnstrAngle;
                root.UnitPreferences["Angle"].SetCurrentUnit("deg");
                restoreAngle = (double)elevationMin.Angle;
                elevationMin.Angle = 90;
            }

            double minLat = 90;
            double maxLat = -90;
            double minLon = 180;
            double maxLon = -180;
            root.UnitPreferences["Angle"].SetCurrentUnit("deg");
            switch (areaTargetObj.AreaType)
            {
                case AgEAreaType.eEllipse:
                    IAgDataProviderGroup boundingProvider = areaTarget.DataProviders["Bounding Rectangle"] as IAgDataProviderGroup;
                    IAgDataPrvFixed dpElements = boundingProvider.Group["Corner Points"] as IAgDataPrvFixed;
                    IAgDrResult atDataPrvResult = dpElements.Exec();

                    Array atLats = atDataPrvResult.DataSets.GetDataSetByName("Geodetic-Lat").GetValues();
                    Array atLons = atDataPrvResult.DataSets.GetDataSetByName("Geodetic-Lon").GetValues();
                    foreach (object item in atLats)
                    {
                        if ((double)item > maxLat)
                        {
                            maxLat = (double)item;
                        }
                        if ((double)item < minLat)
                        {
                            minLat = (double)item;
                        }
                    }
                    foreach (object item in atLons)
                    {
                        if ((double)item > maxLon)
                        {
                            maxLon = (double)item;
                        }
                        if ((double)item < minLon)
                        {
                            minLon = (double)item;
                        }
                    }
                    break;
                case AgEAreaType.ePattern:
                    IAgAreaTypePatternCollection boundary = areaTargetObj.AreaTypeData as IAgAreaTypePatternCollection;

                    foreach (IAgAreaTypePattern item in boundary)
                    {
                        if ((double)item.Lat > maxLat)
                        {
                            maxLat = (double)item.Lat;
                        }
                        if ((double)item.Lat < minLat)
                        {
                            minLat = (double)item.Lat;
                        }

                        if ((double)item.Lon > maxLon)
                        {
                            maxLon = (double)item.Lon;
                        }
                        if ((double)item.Lon < minLon)
                        {
                            minLon = (double)item.Lon;
                        }
                    }

                    break;
                default:
                    break;
            }
            double deltaLat = maxLat - minLat;
            double deltaLon = maxLon - minLon;

            IAgExecCmdResult cmdResult;
            switch (heading)
            {
                case "NorthSouth":
                    cmdResult = root.ExecuteCommand("MeasureSurfaceDistance * " +
                minLat + " " + minLon + " " + minLat + " " + maxLon);
                    break;
                case "EastWest":
                    cmdResult = root.ExecuteCommand("MeasureSurfaceDistance * " +
                minLat + " " + minLon + " " + maxLat + " " + minLon);
                    break;
                default:
                    cmdResult = root.ExecuteCommand("MeasureSurfaceDistance * " +
                minLat + " " + minLon + " " + maxLat + " " + minLon);
                    break;
            }



            double regionWidth = double.Parse(cmdResult[0]);

            int numPasses = DetermineNumPasses(swathType, swathParameter, altitude, regionWidth, null);

            if (numPasses < 1)
            {
                MessageBox.Show("No Passes.  Try Adjusting Swath Width Parameters");
                return waypoints;
            }
            root.BeginUpdate();

            string gvName = uniqueName("gridTester", AgESTKObjectType.eGroundVehicle);
            IAgGroundVehicle groundVehicle = root.CurrentScenario.Children.New(AgESTKObjectType.eGroundVehicle, gvName) as IAgGroundVehicle;
            groundVehicle.Graphics.SetAttributesType(AgEVeGfxAttributes.eAttributesBasic);
            IAgVeGfxAttributesBasic gvGfx = groundVehicle.Graphics.Attributes as IAgVeGfxAttributesBasic;
            gvGfx.Inherit = false;
            gvGfx.IsVisible = false;

            IAgVePropagatorGreatArc route = groundVehicle.Route as IAgVePropagatorGreatArc;
            //route.ArcGranularity = 51.333;
            route.SetAltitudeRefType(AgEVeAltitudeRef.eWayPtAltRefWGS84);
            route.Method = AgEVeWayPtCompMethod.eDetermineTimeAccFromVel;

            Waypoint waypoint1 = new Waypoint();
            Waypoint waypoint2 = new Waypoint();
            bool headEast = true;


            double loopMin;
            double loopMax;
            double gridAngleStep;
            switch (heading)
            {
                case "NorthSouth":
                    loopMin = minLon;
                    loopMax = maxLon;
                    gridAngleStep = (maxLon - minLon) / numPasses;
                    break;
                case "EastWest":
                    loopMin = minLat;
                    loopMax = maxLat;
                    gridAngleStep = (maxLat - minLat) / numPasses;
                    break;
                default:
                    loopMin = minLat;
                    loopMax = maxLat;
                    gridAngleStep = .95 * (maxLat - minLat) / numPasses;
                    break;
            }

            for (double eval = loopMin; eval <= loopMax; eval += gridAngleStep)
            {
                route.Waypoints.RemoveAll();
                IAgVeWaypointsElement thisWaypoint1;
                IAgVeWaypointsElement thisWaypoint2;
                switch (heading)
                {
                    case "NorthSouth":
                        thisWaypoint1 = route.Waypoints.Add();
                        thisWaypoint1.Latitude = minLat - gridAngleStep;
                        thisWaypoint1.Longitude = eval;
                        thisWaypoint1.Altitude = 0;

                        thisWaypoint2 = route.Waypoints.Add();
                        thisWaypoint2.Latitude = maxLat + gridAngleStep;
                        thisWaypoint2.Longitude = eval;
                        thisWaypoint2.Altitude = 0;
                        break;
                    case "EastWest":
                    default:
                        thisWaypoint1 = route.Waypoints.Add();
                        thisWaypoint1.Latitude = eval;
                        thisWaypoint1.Longitude = minLon - gridAngleStep;
                        thisWaypoint1.Altitude = 0;

                        thisWaypoint2 = route.Waypoints.Add();
                        thisWaypoint2.Latitude = eval;
                        thisWaypoint2.Longitude = maxLon + gridAngleStep;
                        thisWaypoint2.Altitude = 0;
                        break;
                }
                route.Propagate();

                IAgStkAccess access = areaTarget.GetAccessToObject((IAgStkObject)groundVehicle);
                access.ComputeAccess();

                IAgDataPrvInterval dpAccess = access.DataProviders["Access Data"] as IAgDataPrvInterval;
                IAgScenario scen = root.CurrentScenario as IAgScenario;
                IAgDrResult result = dpAccess.Exec(scen.StartTime, scen.StopTime);


                if (result.DataSets.Count > 0)
                {
                    Array startTimes = result.DataSets.GetDataSetByName("Start Time").GetValues();
                    Array stopTimes = result.DataSets.GetDataSetByName("Stop Time").GetValues();

                    string startTime = (string)startTimes.GetValue(0);
                    string stopTime = (string)stopTimes.GetValue(stopTimes.GetLength(0) - 1);

                    IAgDataProviderGroup dpLLA = ((IAgStkObject)groundVehicle).DataProviders["LLA State"] as IAgDataProviderGroup;
                    IAgDataPrvTimeVar dpElements = dpLLA.Group["Fixed"] as IAgDataPrvTimeVar;
                    IAgDrResult DataPrvResult = dpElements.ExecSingle(startTime);

                    Array Lats = DataPrvResult.DataSets.GetDataSetByName("Lat").GetValues();
                    Array Lons = DataPrvResult.DataSets.GetDataSetByName("Lon").GetValues();

                    waypoint1 = new Waypoint();
                    waypoint1.Latitude = (double)Lats.GetValue(0);
                    waypoint1.Longitude = (double)Lons.GetValue(0);
                    waypoint1.Altitude = altitude;
                    waypoint1.SurfaceAltitude = 0;
                    waypoint1.Speed = speed;
                    waypoint1.TurnRadius = turnRadius;


                    IAgDataProviderGroup dpLLA1 = ((IAgStkObject)groundVehicle).DataProviders["LLA State"] as IAgDataProviderGroup;
                    IAgDataPrvTimeVar dpElements1 = dpLLA1.Group["Fixed"] as IAgDataPrvTimeVar;
                    IAgDrResult DataPrvResult1 = dpElements1.ExecSingle(stopTime);

                    Array Lats1 = DataPrvResult1.DataSets.GetDataSetByName("Lat").GetValues();
                    Array Lons1 = DataPrvResult1.DataSets.GetDataSetByName("Lon").GetValues();

                    waypoint2 = new Waypoint();
                    waypoint2.Latitude = (double)Lats1.GetValue(0);
                    waypoint2.Longitude = (double)Lons1.GetValue(0);
                    waypoint2.Altitude = altitude;
                    waypoint2.SurfaceAltitude = 0;
                    waypoint2.Speed = speed;
                    waypoint2.TurnRadius = turnRadius;


                    if (headEast)
                    {
                        waypoints.Add(waypoint1);
                        waypoints.Add(waypoint2);
                    }
                    else
                    {
                        waypoints.Add(waypoint2);
                        waypoints.Add(waypoint1);
                    }
                    headEast = !headEast;

                }
                access.RemoveAccess();
            }

            ((IAgStkObject)groundVehicle).Unload();

            if (!areaTargetElActive)
            {
                areaTarget.AccessConstraints.RemoveConstraint(AgEAccessConstraints.eCstrElevationAngle);
            }
            else
            {
                IAgAccessCnstrAngle elevationMin = areaTarget.AccessConstraints.GetActiveConstraint(AgEAccessConstraints.eCstrElevationAngle) as IAgAccessCnstrAngle;
                root.UnitPreferences["Angle"].SetCurrentUnit("deg");
                elevationMin.Angle = restoreAngle;
            }

            root.EndUpdate();
            root.UnitPreferences.ResetUnits();
            return waypoints;
        }


        public List<Waypoint> BoundingParallelTrackWaypointGenerator(
            string areaTargetPath, double speed, double altitude, double turnRadius, SwathWidthType swathType, double swathParameter)
        {
            List<Waypoint> waypoints = new List<Waypoint>();

            IAgStkObject areaTarget = root.GetObjectFromPath(areaTargetPath);
            IAgDataProviderGroup boundingProvider = areaTarget.DataProviders["Bounding Rectangle"] as IAgDataProviderGroup;
            IAgDataPrvFixed dpElements = boundingProvider.Group["Corner Points"] as IAgDataPrvFixed;
            IAgDrResult atDataPrvResult = dpElements.Exec();

            Array atLats = atDataPrvResult.DataSets.GetDataSetByName("Geodetic-Lat").GetValues();
            Array atLons = atDataPrvResult.DataSets.GetDataSetByName("Geodetic-Lon").GetValues();

            IAgExecCmdResult cmdResult = root.ExecuteCommand("MeasureSurfaceDistance * " +
                atLats.GetValue(0).ToString() + " " + atLons.GetValue(0).ToString()
                + " " + atLats.GetValue(1).ToString() + " " + atLons.GetValue(1).ToString());

            double regionWidth = double.Parse(cmdResult[0]);

            int numPasses = DetermineNumPasses(swathType, swathParameter, altitude, regionWidth, null);

            if (numPasses < 1)
            {
                MessageBox.Show("No Passes.  Try Adjusting Swath Width Parameters");
                return waypoints;
            }

            double topStepLat = (((double)atLats.GetValue(1) - (double)atLats.GetValue(0)) / (double)numPasses);
            double bottomStepLat = (((double)atLats.GetValue(2) - (double)atLats.GetValue(3)) / (double)numPasses);
            double topStepLon = (((double)atLons.GetValue(1) - (double)atLons.GetValue(0)) / (double)numPasses);
            double bottomStepLon = (((double)atLons.GetValue(2) - (double)atLons.GetValue(3)) / (double)numPasses);

            double topLat0 = (double)atLats.GetValue(0);
            double topLon0 = (double)atLons.GetValue(0);
            double bottomLat0 = (double)atLats.GetValue(3);
            double bottomLon0 = (double)atLons.GetValue(3);

            Waypoint waypoint = new Waypoint();

            for (int i = 0; i <= numPasses; ++i)
            {
                int test = i % 2;

                double latTopEnroute = topLat0 + (double)i * topStepLat;
                double lonTopEnroute = topLon0 + (double)i * topStepLon;

                double latBottomEnroute = bottomLat0 + (double)i * bottomStepLat;
                double lonBottomEnroute = bottomLon0 + (double)i * bottomStepLon;


                if (test == 0)
                {
                    waypoint = new Waypoint();
                    waypoint.Latitude = latTopEnroute;
                    waypoint.Longitude = lonTopEnroute;
                    waypoint.Altitude = altitude;
                    waypoint.SurfaceAltitude = 0;
                    waypoint.Speed = speed;
                    waypoint.TurnRadius = turnRadius;
                    waypoints.Add(waypoint);

                    waypoint = new Waypoint();
                    waypoint.Latitude = latBottomEnroute;
                    waypoint.Longitude = lonBottomEnroute;
                    waypoint.Altitude = altitude;
                    waypoint.Speed = speed;
                    waypoint.TurnRadius = turnRadius;
                    waypoints.Add(waypoint);

                }
                else
                {
                    waypoint = new Waypoint();
                    waypoint.Latitude = latBottomEnroute;
                    waypoint.Longitude = lonBottomEnroute;
                    waypoint.Altitude = altitude;
                    waypoint.SurfaceAltitude = 0;
                    waypoint.Speed = speed;
                    waypoint.TurnRadius = turnRadius;
                    waypoints.Add(waypoint);

                    waypoint = new Waypoint();
                    waypoint.Latitude = latTopEnroute;
                    waypoint.Longitude = lonTopEnroute;
                    waypoint.Altitude = altitude;
                    waypoint.Speed = speed;
                    waypoint.TurnRadius = turnRadius;
                    waypoints.Add(waypoint);
                }

            }

            return waypoints;
        }

        public Waypoint LocationWaypointGenerator(string locationPath, double altitude, double speed, double turnRadius)
        {
            IAgStkObject location = root.GetObjectFromPath(locationPath);
            IAgDataPrvFixed locationDP = location.DataProviders["LLA State"] as IAgDataPrvFixed;
            IAgDrResult locationResult = locationDP.Exec();
            Array latResults = locationResult.DataSets.GetDataSetByName("Lat").GetValues();
            Array lonResults = locationResult.DataSets.GetDataSetByName("Lon").GetValues();
            Array altResults = locationResult.DataSets.GetDataSetByName("Ground Alt MSL").GetValues();
            double locationLat = (double)latResults.GetValue(0);
            double locationLon = (double)lonResults.GetValue(0);
            double locationAlt = (double)altResults.GetValue(0);

            Waypoint waypoint = new Waypoint();
            waypoint.Latitude = locationLat;
            waypoint.Longitude = locationLon;
            waypoint.Altitude = altitude;
            waypoint.SurfaceAltitude = locationAlt;
            waypoint.Speed = speed;
            waypoint.TurnRadius = turnRadius;

            return waypoint;
        }

        public void WaypointsToGreatArc(IAgAircraft aircraft, List<Waypoint> waypoints, bool useTakeoffLanding)
        {
            //Set propagator to GreatArc 
            aircraft.SetRouteType(AgEVePropagatorType.ePropagatorGreatArc);
            IAgVePropagatorGreatArc route = aircraft.Route as IAgVePropagatorGreatArc;
            route.ArcGranularity = 51.333;

            //Set Ref type to WayPtAltRefMSL 
            route.SetAltitudeRefType(AgEVeAltitudeRef.eWayPtAltRefMSL);
            route.Method = AgEVeWayPtCompMethod.eDetermineTimeAccFromVel;

            route.Waypoints.RemoveAll();

            if (useTakeoffLanding)
            {
                waypoints[0].Altitude = waypoints[0].SurfaceAltitude;
                waypoints[waypoints.Count - 1].Altitude = waypoints[waypoints.Count - 1].SurfaceAltitude;
            }

            foreach (Waypoint waypoint in waypoints)
            {
                IAgVeWaypointsElement thisWaypoint = route.Waypoints.Add();
                thisWaypoint.Latitude = waypoint.Latitude;
                thisWaypoint.Longitude = waypoint.Longitude;
                thisWaypoint.Altitude = waypoint.Altitude;
                thisWaypoint.Speed = waypoint.Speed;
                thisWaypoint.TurnRadius = waypoint.TurnRadius;
            }

            route.Propagate();
        }



        private int DetermineNumPasses(SwathWidthType swathType, double swathParameter, double altitude, double regionWidth, IAgAircraft aircraft)
        {
            int numPasses;
            double swathWidth;
            switch (swathType)
            {
                case SwathWidthType.NumberOfPasses:
                    numPasses = (int)swathParameter;
                    break;
                case SwathWidthType.SensorFOV:
                    swathWidth = altitude * Math.Tan((Math.PI / 180.0) * swathParameter) * 1000 * 2 * .9;
                    if (aircraft != null)
                    {
                        IAgSensor sensor = ((IAgStkObject)aircraft).Children.New(AgESTKObjectType.eSensor, "Sensor") as IAgSensor;
                        sensor.SetPatternType(AgESnPattern.eSnSimpleConic);
                        IAgSnSimpleConicPattern simplecone = sensor.Pattern as IAgSnSimpleConicPattern;
                        simplecone.ConeAngle = swathParameter;
                    }
                    numPasses = (int)(regionWidth / swathWidth);
                    break;
                case SwathWidthType.GroundElev:
                    swathWidth = altitude / Math.Tan((Math.PI / 180.0) * swathParameter) * 1000 * 2 * .9;
                    numPasses = (int)(regionWidth / swathWidth);

                    break;
                case SwathWidthType.SlantRange:
                    swathWidth = Math.Sqrt((swathParameter * swathParameter) - (altitude * altitude)) * 1000 * 2 * .9;
                    numPasses = (int)(regionWidth / swathWidth);

                    break;
                default:
                    numPasses = 10;
                    break;
            }

            return numPasses;
        }


        private double DetermineSwathWidth(SwathWidthType swathType, double swathParameter, double altitude)
        {
            int numPasses;
            double swathWidth = 0;
            switch (swathType)
            {
                case SwathWidthType.NumberOfPasses:
                    numPasses = (int)swathParameter;
                    break;
                case SwathWidthType.SensorFOV:
                    swathWidth = altitude * Math.Tan((Math.PI / 180.0) * swathParameter) * 1000 * 2 * .9;
                    break;
                case SwathWidthType.GroundElev:
                    swathWidth = altitude / Math.Tan((Math.PI / 180.0) * swathParameter) * 1000 * 2 * .9;
                    break;
                case SwathWidthType.SlantRange:
                    swathWidth = Math.Sqrt((swathParameter * swathParameter) - (altitude * altitude)) * 1000 * 2 * .9;
                    break;
                default:
                    swathWidth = 10;
                    break;
            }

            return swathWidth;
        }

        private string uniqueName(string inputName, AgESTKObjectType objectType)
        {
            string outputName = inputName.Replace(' ', '_');

            bool nameExists = true;
            int counter = 1;
            while (nameExists)
            {
                if (root.CurrentScenario.Children.Contains(objectType, outputName))
                {
                    outputName = inputName + "_" + counter.ToString();
                    ++counter;
                }
                else
                {
                    nameExists = false;
                }
            }
            return outputName;

        }

        private String substring(string inputString, int length)
        {
            string returnString = inputString.Replace(' ', '_');

            if (inputString.Length > length)
            {
                returnString = inputString.Substring(0, length);
            }
            return returnString;

        }

        public class Waypoint
        {
            public double Latitude;
            public double Longitude;
            public double Altitude;
            public double Speed;
            public double TurnRadius;
            public double SurfaceAltitude;

            public Waypoint()
            {
            }
        }


    }
}
