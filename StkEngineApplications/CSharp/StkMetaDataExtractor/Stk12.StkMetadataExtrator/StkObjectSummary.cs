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
                    var radar = stkObject as IAgRadar;
                    IAgRadarModel radarModel = radar.Model as IAgRadarModel;
                    summary.Properties.Add(new Property("Radar Model", radarModel.Name.ToString()));
                    if (radarModel.Name.ToString().Equals("Monostatic"))
                    {
                        IAgRadarModelMonostatic radarMonostatic = radarModel as IAgRadarModelMonostatic;
                        summary.Properties.Add(new Property("Radar Mode", radarMonostatic.Mode.Name.ToString()));
                        IAgRadarReceiver radarReceiver = radarMonostatic.Receiver as IAgRadarReceiver;
                        summary.Properties.Add(new Property("Radar Receiver Frequency", radarReceiver.Frequency.ToString()));
                        summary.Properties.Add(new Property("Radar Receiver LNA Bandwidth", radarReceiver.LNABandwidth.ToString()));
                        summary.Properties.Add(new Property("Radar Receiver LNA Gain", radarReceiver.LnaGain.ToString()));
                        summary.Properties.Add(new Property("Radar Receiver LNA To Receiver Line Loss", radarReceiver.LnaToReceiverLineLoss.ToString()));
                        summary.Properties.Add(new Property("Radar Receiver Use Rain", radarReceiver.UseRain.ToString()));
                        summary.Properties.Add(new Property("Radar Receiver Rain Outage Percent", radarReceiver.RainOutagePercent.ToString()));
                        IAgRadarTransmitter radarTransmitter = radarMonostatic.Transmitter as IAgRadarTransmitter;
                        summary.Properties.Add(new Property("Radar Transmitter Frequency", radarTransmitter.Frequency.ToString()));
                        summary.Properties.Add(new Property("Radar Transmitter Power", radarTransmitter.Power.ToString()));
                        summary.Properties.Add(new Property("Radar Transmitter Power Amp Bandwidth", radarTransmitter.PowerAmpBandwidth.ToString()));
                        summary.Properties.Add(new Property("Radar Transmitter Wavelength", radarTransmitter.Wavelength.ToString()));
                    }
                    break;
                case AgESTKObjectType.eReceiver:
                    var receiver = stkObject as IAgReceiver;
                    summary.Properties.Add(new Property("Receiver Model", receiver.Model.Name.ToString()));
                    if (receiver.Model.Name.ToString().Equals("Complex Receiver Model"))
                    {
                        IAgReceiverModelComplex complexReceiver = receiver.Model as IAgReceiverModelComplex;
                        summary.Properties.Add(new Property("Frequency", complexReceiver.Frequency.ToString()));
                        summary.Properties.Add(new Property("Auto-Track Frequency", complexReceiver.AutoTrackFrequency.ToString()));
                        summary.Properties.Add(new Property("Use Rain", complexReceiver.UseRain.ToString()));
                        summary.Properties.Add(new Property("Rain Outage Percent", complexReceiver.RainOutagePercent.ToString()));
                        summary.Properties.Add(new Property("Bandwidth", complexReceiver.Bandwidth.ToString()));
                        summary.Properties.Add(new Property("Antenna To LNA Line Loss", complexReceiver.AntennaToLnaLineLoss.ToString()));
                        IAgAntennaModel antennaModel = complexReceiver.AntennaControl.EmbeddedModel as IAgAntennaModel;
                        summary.Properties.Add(new Property("Antenna Design Frequency", antennaModel.DesignFrequency.ToString()));
                        summary.Properties.Add(new Property("Antenna Model Name", antennaModel.Name.ToString()));
                        //Adding some "common" antenna types to reveal additional properties
                        if (antennaModel.Name.ToString().Equals("Gaussian"))
                        {
                            IAgAntennaModelGaussian antennaGaussian = complexReceiver.AntennaControl.EmbeddedModel as IAgAntennaModelGaussian;
                            summary.Properties.Add(new Property("Antenna Mainlobe Gain", antennaGaussian.MainlobeGain.ToString()));
                            summary.Properties.Add(new Property("Antenna Backlobe Gain", antennaGaussian.BacklobeGain.ToString()));
                            summary.Properties.Add(new Property("Antenna Diameter", antennaGaussian.Diameter.ToString()));
                            summary.Properties.Add(new Property("Antenna Beamwidth", antennaGaussian.Beamwidth.ToString()));
                        }
                        if (antennaModel.Name.ToString().Equals("Dipole"))
                        {
                            IAgAntennaModelDipole antennaDipole = complexReceiver.AntennaControl.EmbeddedModel as IAgAntennaModelDipole;
                            summary.Properties.Add(new Property("Antenna Length", antennaDipole.Length.ToString()));
                            summary.Properties.Add(new Property("Antenna Length/Wavelength Ratio", antennaDipole.LengthToWavelengthRatio.ToString()));
                        }
                        if (antennaModel.Name.ToString().Equals("Parabolic"))
                        {
                            IAgAntennaModelParabolic antennaParabolic = complexReceiver.AntennaControl.EmbeddedModel as IAgAntennaModelParabolic;
                            summary.Properties.Add(new Property("Antenna Mainlobe Gain", antennaParabolic.MainlobeGain.ToString()));
                            summary.Properties.Add(new Property("Antenna Backlobe Gain", antennaParabolic.BacklobeGain.ToString()));
                            summary.Properties.Add(new Property("Antenna Diameter", antennaParabolic.Diameter.ToString()));
                            summary.Properties.Add(new Property("Antenna Beamwidth", antennaParabolic.Beamwidth.ToString()));
                        }
                        if (antennaModel.Name.ToString().Equals("External Antenna Pattern"))
                        {
                            IAgAntennaModelExternal antennaExternal = complexReceiver.AntennaControl.EmbeddedModel as IAgAntennaModelExternal;
                            summary.Properties.Add(new Property("Antenna External File", antennaExternal.Filename.ToString()));
                        }
                        if (antennaModel.Name.ToString().Equals("Complex ANSYS ffd Format"))
                        {
                            IAgAntennaModelANSYSffdFormat antennaAnsysExternal = complexReceiver.AntennaControl.EmbeddedModel as IAgAntennaModelANSYSffdFormat;
                            summary.Properties.Add(new Property("Antenna External File", antennaAnsysExternal.Filename.ToString()));
                        }
                    }
                    if (receiver.Model.Name.ToString().Equals("Simple Receiver Model"))
                    {
                        IAgReceiverModelSimple simpleReceiver = receiver.Model as IAgReceiverModelSimple;
                        summary.Properties.Add(new Property("Frequency", simpleReceiver.Frequency.ToString()));
                        summary.Properties.Add(new Property("Auto-Track Frequency", simpleReceiver.AutoTrackFrequency.ToString()));
                        summary.Properties.Add(new Property("Use Rain", simpleReceiver.UseRain.ToString()));
                        summary.Properties.Add(new Property("Rain Outage Percent", simpleReceiver.RainOutagePercent.ToString()));
                        summary.Properties.Add(new Property("Bandwidth", simpleReceiver.Bandwidth.ToString()));
                        summary.Properties.Add(new Property("G/T", simpleReceiver.GOverT.ToString()));
                    }
                    if (receiver.Model.Name.ToString().Equals("Medium Receiver Model"))
                    {
                        IAgReceiverModelMedium mediumReceiver = receiver.Model as IAgReceiverModelMedium;
                        summary.Properties.Add(new Property("Frequency", mediumReceiver.Frequency.ToString()));
                        summary.Properties.Add(new Property("Auto-Track Frequency", mediumReceiver.AutoTrackFrequency.ToString()));
                        summary.Properties.Add(new Property("Use Rain", mediumReceiver.UseRain.ToString()));
                        summary.Properties.Add(new Property("Rain Outage Percent", mediumReceiver.RainOutagePercent.ToString()));
                        summary.Properties.Add(new Property("Bandwidth", mediumReceiver.Bandwidth.ToString()));
                        summary.Properties.Add(new Property("LNA Gain", mediumReceiver.LnaGain.ToString()));
                        summary.Properties.Add(new Property("LNA To Receiver Line Loss", mediumReceiver.LnaToReceiverLineLoss.ToString()));
                        summary.Properties.Add(new Property("Antenna Gain", mediumReceiver.AntennaGain.ToString()));
                    }
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
                    IAgTransmitter transmitter = stkObject as IAgTransmitter;
                    summary.Properties.Add(new Property("Transmitter Model", transmitter.Model.Name));
                    if (transmitter.Model.Name.ToString().Equals("Complex Transmitter Model"))
                    {
                        IAgTransmitterModelComplex complexTransmitter = transmitter.Model as IAgTransmitterModelComplex;
                        summary.Properties.Add(new Property("Frequency", complexTransmitter.Frequency.ToString()));
                        summary.Properties.Add(new Property("Power", complexTransmitter.Power.ToString()));
                        summary.Properties.Add(new Property("Data Rate", complexTransmitter.DataRate.ToString()));
                        summary.Properties.Add(new Property("Antenna Type", complexTransmitter.AntennaControl.EmbeddedModel.Name.ToString()));
                        IAgAntennaModel antennaModel = complexTransmitter.AntennaControl.EmbeddedModel as IAgAntennaModel;
                        summary.Properties.Add(new Property("Antenna Design Frequency", antennaModel.DesignFrequency.ToString()));
                        summary.Properties.Add(new Property("Antenna Model Name", antennaModel.Name.ToString()));
                        //Adding some "common" antenna types to reveal additional properties
                        if (antennaModel.Name.ToString().Equals("Gaussian"))
                        {
                            IAgAntennaModelGaussian antennaGaussian = complexTransmitter.AntennaControl.EmbeddedModel as IAgAntennaModelGaussian;
                            summary.Properties.Add(new Property("Antenna Mainlobe Gain", antennaGaussian.MainlobeGain.ToString()));
                            summary.Properties.Add(new Property("Antenna Backlobe Gain", antennaGaussian.BacklobeGain.ToString()));
                            summary.Properties.Add(new Property("Antenna Diameter", antennaGaussian.Diameter.ToString()));
                            summary.Properties.Add(new Property("Antenna Beamwidth", antennaGaussian.Beamwidth.ToString()));
                        }
                        if (antennaModel.Name.ToString().Equals("Dipole"))
                        {
                            IAgAntennaModelDipole antennaDipole = complexTransmitter.AntennaControl.EmbeddedModel as IAgAntennaModelDipole;
                            summary.Properties.Add(new Property("Antenna Length", antennaDipole.Length.ToString()));
                            summary.Properties.Add(new Property("Antenna Length/Wavelength Ratio", antennaDipole.LengthToWavelengthRatio.ToString()));
                        }
                        if (antennaModel.Name.ToString().Equals("Parabolic"))
                        {
                            IAgAntennaModelParabolic antennaParabolic = complexTransmitter.AntennaControl.EmbeddedModel as IAgAntennaModelParabolic;
                            summary.Properties.Add(new Property("Antenna Mainlobe Gain", antennaParabolic.MainlobeGain.ToString()));
                            summary.Properties.Add(new Property("Antenna Backlobe Gain", antennaParabolic.BacklobeGain.ToString()));
                            summary.Properties.Add(new Property("Antenna Diameter", antennaParabolic.Diameter.ToString()));
                            summary.Properties.Add(new Property("Antenna Beamwidth", antennaParabolic.Beamwidth.ToString()));
                        }
                        if (antennaModel.Name.ToString().Equals("External Antenna Pattern"))
                        {
                            IAgAntennaModelExternal antennaExternal = complexTransmitter.AntennaControl.EmbeddedModel as IAgAntennaModelExternal;
                            summary.Properties.Add(new Property("Antenna External File", antennaExternal.Filename.ToString()));
                        }
                        if (antennaModel.Name.ToString().Equals("Complex ANSYS ffd Format"))
                        {
                            IAgAntennaModelANSYSffdFormat antennaAnsysExternal = complexTransmitter.AntennaControl.EmbeddedModel as IAgAntennaModelANSYSffdFormat;
                            summary.Properties.Add(new Property("Antenna External File", antennaAnsysExternal.Filename.ToString()));
                        }

                    }
                    if (transmitter.Model.Name.ToString().Equals("Simple Transmitter Model"))
                    {
                        IAgTransmitterModelSimple simpleTransmitter = transmitter.Model as IAgTransmitterModelSimple;
                        summary.Properties.Add(new Property("Frequency", simpleTransmitter.Frequency.ToString()));
                        summary.Properties.Add(new Property("Power", simpleTransmitter.Eirp.ToString()));
                        summary.Properties.Add(new Property("Data Rate", simpleTransmitter.DataRate.ToString()));
                    }
                    if (transmitter.Model.Name.ToString().Equals("Medium Transmitter Model"))
                    {
                        IAgTransmitterModelMedium mediumTransmitter = transmitter.Model as IAgTransmitterModelMedium;
                        summary.Properties.Add(new Property("Frequency", mediumTransmitter.Frequency.ToString()));
                        summary.Properties.Add(new Property("Power", mediumTransmitter.Power.ToString()));
                        summary.Properties.Add(new Property("Data Rate", mediumTransmitter.DataRate.ToString()));
                        summary.Properties.Add(new Property("Antenna Gain", mediumTransmitter.AntennaGain.ToString()));
                    }
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
                    if (antenna.Model.Name.ToString().Equals("Gaussian"))
                    {
                        IAgAntennaModelGaussian antennaGaussian = antenna.Model as IAgAntennaModelGaussian;
                        summary.Properties.Add(new Property("Antenna Mainlobe Gain", antennaGaussian.MainlobeGain.ToString()));
                        summary.Properties.Add(new Property("Antenna Backlobe Gain", antennaGaussian.BacklobeGain.ToString()));
                        summary.Properties.Add(new Property("Antenna Diameter", antennaGaussian.Diameter.ToString()));
                        summary.Properties.Add(new Property("Antenna Beamwidth", antennaGaussian.Beamwidth.ToString()));
                    }
                    if (antenna.Model.Name.ToString().Equals("Dipole"))
                    {
                        IAgAntennaModelDipole antennaDipole = antenna.Model as IAgAntennaModelDipole;
                        summary.Properties.Add(new Property("Antenna Length", antennaDipole.Length.ToString()));
                        summary.Properties.Add(new Property("Antenna Length/Wavelength Ratio", antennaDipole.LengthToWavelengthRatio.ToString()));
                    }
                    if (antenna.Model.Name.ToString().Equals("Parabolic"))
                    {
                        IAgAntennaModelParabolic antennaParabolic = antenna.Model as IAgAntennaModelParabolic;
                        summary.Properties.Add(new Property("Antenna Mainlobe Gain", antennaParabolic.MainlobeGain.ToString()));
                        summary.Properties.Add(new Property("Antenna Backlobe Gain", antennaParabolic.BacklobeGain.ToString()));
                        summary.Properties.Add(new Property("Antenna Diameter", antennaParabolic.Diameter.ToString()));
                        summary.Properties.Add(new Property("Antenna Beamwidth", antennaParabolic.Beamwidth.ToString()));
                    }
                    if (antenna.Model.Name.ToString().Equals("External Antenna Pattern"))
                    {
                        IAgAntennaModelExternal antennaExternal = antenna.Model as IAgAntennaModelExternal;
                        summary.Properties.Add(new Property("Antenna External File", antennaExternal.Filename.ToString()));
                    }
                    if (antenna.Model.Name.ToString().Equals("Complex ANSYS ffd Format"))
                    {
                        IAgAntennaModelANSYSffdFormat antennaAnsysExternal = antenna.Model as IAgAntennaModelANSYSffdFormat;
                        summary.Properties.Add(new Property("Antenna External File", antennaAnsysExternal.Filename.ToString()));
                    }
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
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
    }
}