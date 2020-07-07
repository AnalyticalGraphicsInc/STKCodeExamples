using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using AGI.STKObjects;

namespace StkMetadataExtractor
{
    public class StkObjectSummary
    {
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [XmlAttribute] 
        public string Name { get; set; }
        [XmlAttribute]
        public string Type { get; set; }
        public List<Property> Properties { get; set; }
        public List<StkObjectSummary> Children { get; set; }


        public StkObjectSummary()
        {
            
        }

        public static StkObjectSummary SummarizeStkObject(IAgStkObject stkObject)
        {
            var summary = new StkObjectSummary
            {
                ShortDescription = stkObject.ShortDescription,
                LongDescription = stkObject.LongDescription,
                Name = stkObject.InstanceName,
                Type = stkObject.ClassName,
                Properties = new List<Property>(),
                Children = new List<StkObjectSummary>()
            };

            // Determine the object type and capture the appropriate stats
            switch (stkObject.ClassType)
            {
                case AgESTKObjectType.eAdvCat:
                    break;
                case AgESTKObjectType.eAircraft:
                    var aircraft = stkObject as IAgAircraft;
                    summary.Properties.Add(new Property("RouteType", aircraft.RouteType.ToString()));
                    break;
                case AgESTKObjectType.eAreaTarget:
                    var areaTarget = stkObject as IAgAreaTarget;
                    var centroidPosition = areaTarget.Position.QueryPlanetocentricArray();
                    summary.Properties.Add(new Property("CentroidLatitude", centroidPosition.GetValue(0).ToString()));
                    summary.Properties.Add(new Property("CentroidLongitude", centroidPosition.GetValue(1).ToString()));
                    summary.Properties.Add(new Property("CentroidAltitude", centroidPosition.GetValue(2).ToString()));
                    break;
                case AgESTKObjectType.eAttitudeCoverage:
                    break;
                case AgESTKObjectType.eChain:
                    var chain = stkObject as IAgChain;
                    summary.Properties.Add(new Property("NumberOfLinks", chain.Objects.Count.ToString()));
                    break;
                case AgESTKObjectType.eCommSystem:
                    break;
                case AgESTKObjectType.eConstellation:
                    var constellation = stkObject as IAgConstellation;
                    summary.Properties.Add(new Property("NumberOfItems", constellation.Objects.Count.ToString()));
                    break;
                case AgESTKObjectType.eCoverageDefinition:
                    var coverage = stkObject as IAgCoverageDefinition;
                    summary.Properties.Add(new Property("NumberOfAssets", coverage.AssetList.Count.ToString()));
                    summary.Properties.Add(new Property("ResolutionType", coverage.Grid.ResolutionType.ToString()));
                    summary.Properties.Add(new Property("BoundsType", coverage.Grid.BoundsType.ToString()));
                    break;
                case AgESTKObjectType.eFacility:
                    var facility = stkObject as IAgFacility;
                    var position = facility.Position.QueryPlanetodeticArray();
                    summary.Properties.Add(new Property("Position",
                        $"({position.GetValue(0)},{position.GetValue(1)},{position.GetValue(2)})"));
                    break;
                case AgESTKObjectType.eGroundVehicle:
                    var groundVehicle = stkObject as IAgGroundVehicle;
                    summary.Properties.Add(new Property("RouteType", groundVehicle.RouteType.ToString()));
                    if (groundVehicle.RouteType == AgEVePropagatorType.ePropagatorGreatArc)
                        summary.Properties.Add(new Property("NumberOfWaypoints",
                            (groundVehicle.Route as IAgVePropagatorGreatArc).Waypoints.Count.ToString()));
                    break;
                case AgESTKObjectType.eLaunchVehicle:
                    var lv = stkObject as IAgLaunchVehicle;
                    summary.Properties.Add(new Property("TrajectoryType", lv.TrajectoryType.ToString()));
                    break;
                case AgESTKObjectType.eLineTarget:
                    break;
                case AgESTKObjectType.eMTO:
                    break;
                case AgESTKObjectType.eMissile:
                    var missile = stkObject as IAgMissile;
                    summary.Properties.Add(new Property("TrajectoryType", missile.TrajectoryType.ToString()));

                    break;
                case AgESTKObjectType.eMissileSystem:
                    break;
                case AgESTKObjectType.ePlanet:
                    break;
                case AgESTKObjectType.eRadar:
                    break;
                case AgESTKObjectType.eReceiver:
                    var receiver = stkObject as IAgReceiver;
                    summary.Properties.Add(new Property("ReceiverModelType", receiver.Model.Type.ToString()));
                    break;
                case AgESTKObjectType.eSatellite:
                    var satellite = stkObject as IAgSatellite;
                    summary.Properties.Add(new Property("PropagatorType", satellite.PropagatorType.ToString()));
                    break;
                case AgESTKObjectType.eScenario:
                    var scenario = stkObject as IAgScenario;
                    summary.Properties.Add(new Property("AnalysisStartTime", scenario.StartTime.ToString()));
                    summary.Properties.Add(new Property("AnalysisEndTime", scenario.StopTime.ToString()));
                    break;
                case AgESTKObjectType.eSensor:
                    var sensor = stkObject as IAgSensor;
                    summary.Properties.Add(new Property("PatternType", sensor.PatternType.ToString()));
                    summary.Properties.Add(new Property("PointingType", sensor.PointingType.ToString()));
                    break;
                case AgESTKObjectType.eShip:
                    break;
                case AgESTKObjectType.eStar:
                    var star = stkObject as IAgStar;
                    summary.Properties.Add(new Property("Declination", star.LocationDeclination.ToString()));
                    summary.Properties.Add(new Property("RightAscension", star.LocationRightAscension.ToString()));
                    break;
                case AgESTKObjectType.eTarget:
                    var target = stkObject as IAgTarget;
                    var positionTarget = target.Position.QueryPlanetodeticArray();
                    summary.Properties.Add(new Property("Position",
                        $"({positionTarget.GetValue(0)},{positionTarget.GetValue(1)},{positionTarget.GetValue(2)})"));

                    break;
                case AgESTKObjectType.eTransmitter:
                    var transmitter = stkObject as IAgTransmitter;
                    summary.Properties.Add(new Property("ModelName", transmitter.Model.Name));
                    break;
                case AgESTKObjectType.eFigureOfMerit:
                    var fom = stkObject as IAgFigureOfMerit;
                    summary.Properties.Add(new Property("FigureOfMeritType",fom.DefinitionType.ToString()));
                    break;
                case AgESTKObjectType.eRoot:
                    break;
                case AgESTKObjectType.eAccess:
                    //var access = stkObject as IAgStkAccess;
                    summary.Properties.Add(new Property("Access", stkObject.InstanceName));
                    break;
                case AgESTKObjectType.eObjectCoverage:
                    break;
                case AgESTKObjectType.eAttitudeFigureOfMerit:
                    break;
                case AgESTKObjectType.eSubmarine:
                    break;
                case AgESTKObjectType.eAntenna:
                    var antenna = stkObject as IAgAntenna;
                    summary.Properties.Add(new Property("ModelName",antenna.Model.Name));
                    break;
                case AgESTKObjectType.ePlace:
                    var place = stkObject as IAgPlace;
                    var positionPlace = place.Position.QueryPlanetodeticArray();
                    summary.Properties.Add(new Property("Position",
                        $"({positionPlace.GetValue(0)},{positionPlace.GetValue(1)},{positionPlace.GetValue(2)})"));

                    break;
                case AgESTKObjectType.eVolumetric:
                    break;
            }

            foreach (IAgStkObject stkObjectChild in stkObject.Children)
            {
                summary.Children.Add(SummarizeStkObject(stkObjectChild));
            }

            return summary;
        }

  
    }


    [Serializable]
    public class Property
    {
        public Property()
        {
        }

        public Property(string key, string value)
        {
            Name = key;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}