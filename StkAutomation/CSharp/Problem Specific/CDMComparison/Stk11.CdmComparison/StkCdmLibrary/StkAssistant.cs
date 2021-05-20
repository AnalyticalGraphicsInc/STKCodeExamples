using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
using AGI.STKX;
using IAgExecCmdResult = AGI.STKUtil.IAgExecCmdResult;

namespace StkCdmLibrary
{
    public static class StkAssistant
    {
        public static IAgScenario Scenario
        {
            get
            {
                return (Root.CurrentScenario as IAgScenario);
            }
        }

        public static string ScenarioStartTime
        {
            get
            {
                return Scenario.StartTime.ToString();
            }
        }

        public static string ScenarioStopTime
        {
            get
            {
                return Scenario.StopTime.ToString();
            }
        }
        public static string StkDataDirectory
        {
            get
            {
                return Path.GetDirectoryName((Root.CurrentScenario as IAgScenario).EarthData.EOPFilename);
            }
        }

        public static string StkInstallDirectory
        {
            get
            {
                IAgExecCmdResult cmdResult = Root.ExecuteCommand("GetDirectory / STKHome");
                return cmdResult[0].ToString();
            }
        }

        private static AgStkObjectRoot _root;
        private static AgSTKXApplication stkxApp;

        public static AgStkObjectRoot Root
        {
            get
            {
                if (_root == null)
                {
                    try
                    {
                        Object app = Marshal.GetActiveObject("STK11.Application");
                        _root = app.GetType().InvokeMember("Personality2", BindingFlags.GetProperty, null, app, null) as AgStkObjectRoot;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message + " " + e.InnerException);
                        stkxApp = new AgSTKXApplication();
                        _root = new AgStkObjectRoot();

                    }

                    if (_root.CurrentScenario == null)
                    {
                        _root.NewScenario("CDM_Comparison");
                    }
                    string command, cmdResult;
                    command = "Units_Set * Connect Date ISO-YMD Distance m";
                    TryConnect(command, out cmdResult);
                    _root.UnitPreferences.SetCurrentUnit("DateFormat", "ISO-YMD");
                    _root.UnitPreferences.SetCurrentUnit("DistanceUnit", "m");
                }

                return _root;
            }

            set
            {
                _root = value;
            }
        }

        public static string ValidateDateFormat(string inputDate)
        {
            string pattern = "(?<year>\\d+)-(?<month>\\d+)-(?<day>\\d+)T(?<hours>\\d+):(?<minutes>\\d+):(?<seconds>\\d+)(.(?<decimal>\\d+))?";
            Regex dateRegex = new Regex(pattern);
            Match match = dateRegex.Match(inputDate);

            if (match.Success)
            {
                return inputDate;
            }

            pattern = "(?<year>\\d+)-(?<day>\\d+)T(?<hours>\\d+):(?<minutes>\\d+):(?<seconds>\\d+)(.(?<decimal>\\d+))?";
            dateRegex = new Regex(pattern);
            match = dateRegex.Match(inputDate);

            string isoyd = Root.ConversionUtility.ConvertDate("ISO-YD", "ISO-YMD", inputDate);

            return isoyd;

        }

        public static void SetAnalysisIntervalFromTCA(string tcaISOYMD)
        {
            tcaISOYMD = ValidateDateFormat(tcaISOYMD);
            IAgDate tca = Root.ConversionUtility.NewDate("ISO-YMD", tcaISOYMD);
            Scenario.AnalysisInterval.SetStartAndStopTimes(tca.Subtract("day", 1).Format("ISO-YMD"), tca.Add("day", 1).Format("ISO-YMD"));
            SetAnimationTime(tcaISOYMD, "ISO-YMD");
        }

        public static void SetAnimationTime(string time, string format)
        {
            IAgDate animDate = null;
            double epochSeconds;
            try
            {
                animDate = Root.ConversionUtility.NewDate(format, time);
                epochSeconds = Convert.ToDouble(animDate.Format("epsec"));
            }
            catch
            {
                throw new Exception("Invalid date definition: " + time + " " + format);
            }

            (Root as AgStkObjectRoot).CurrentTime = epochSeconds;
        }

        public static bool TryConnect(string command, out string result)
        {
            bool success;
            result = "";
            try
            {
                IAgExecCmdResult cmdResult = Root.ExecuteCommand(command);
                if (cmdResult.Count > 0)
                {
                    for (int i = 0; i < cmdResult.Count; i++)
                    {
                        result += string.IsNullOrEmpty(result) ? cmdResult[i] : Environment.NewLine + cmdResult[i];
                    }
                }
                success = cmdResult.IsSucceeded;
            }
            catch (Exception e)
            {
                result = e.Message.ToString();
                success = false;
            }

            return success;
        }

        #region Vector Geometry Stuff

        public static string GetUniqueAWBName(string baseName, IAgStkObject stkObject, AgECrdnKind awbType)
        {
            IEnumerable<IAgCrdn> vgtCollection;

            switch (awbType)
            {
                case AgECrdnKind.eCrdnKindAngle:
                    vgtCollection = stkObject.Vgt.Angles.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindAxes:
                    vgtCollection = stkObject.Vgt.Axes.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindCalcScalar:
                    vgtCollection = stkObject.Vgt.CalcScalars.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindCondition:
                    vgtCollection = stkObject.Vgt.Conditions.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindConditionSet:
                    vgtCollection = stkObject.Vgt.ConditionSets.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindEvent:
                    vgtCollection = stkObject.Vgt.Events.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindEventArray:
                    vgtCollection = stkObject.Vgt.EventArrays.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindEventInterval:
                    vgtCollection = stkObject.Vgt.EventIntervals.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindEventIntervalCollection:
                    vgtCollection = stkObject.Vgt.EventIntervalCollections.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindEventIntervalList:
                    vgtCollection = stkObject.Vgt.EventIntervalLists.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindParameterSet:
                    vgtCollection = stkObject.Vgt.ParameterSets.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindPlane:
                    vgtCollection = stkObject.Vgt.Planes.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindPoint:
                    vgtCollection = stkObject.Vgt.Points.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindSystem:
                    vgtCollection = stkObject.Vgt.Systems.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindVector:
                    vgtCollection = stkObject.Vgt.Vectors.OfType<IAgCrdn>();
                    break;
                case AgECrdnKind.eCrdnKindInvalid:
                case AgECrdnKind.eCrdnKindUnknown:
                default:
                    vgtCollection = null;
                    break;
            }

            string uniqueName = baseName;
            int count = 1;
            while (vgtCollection.Any(awb => awb.Name == uniqueName))
            {
                uniqueName = baseName + count.ToString();
                ++count;
            }

            return uniqueName;
        }

        public static void CreateSensorFromVector(IAgCrdnVector vector, double fovAngle, double displayRange = double.NaN, string[] displayTimes = null)
        {
            string[] pathParts = (vector as IAgCrdn).QualifiedPath.Split(' ');
            string parentObjectPath = "*/" + pathParts[0];
            IAgStkObject parentObject = Root.GetObjectFromPath(parentObjectPath);
            IAgCrdnVector zenith = parentObject.Vgt.Vectors["Zenith(Detic)"];
            string sensorName = (vector as IAgCrdn).Name;
            int count = 1;
            while (Root.ObjectExists(parentObject.Path + "/Sensor/" + sensorName))
            {
                sensorName = (vector as IAgCrdn).Name + "_" + count.ToString();
                ++count;
            }

            IAgStkObject sensor = parentObject.Children.New(AgESTKObjectType.eSensor, sensorName);
            (sensor as IAgSensor).SetPatternType(AgESnPattern.eSnSimpleConic);
            ((sensor as IAgSensor).Pattern as IAgSnSimpleConicPattern).ConeAngle = fovAngle;
            string command = "Point " + sensor.Path + " AlongVector \"" +
                             (vector as IAgCrdn).Path + "\" \"" + (zenith as IAgCrdn).Path + "\"";
            string result;

            if (!TryConnect(command, out result))
            {
                //MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (displayRange != double.NaN)
            {
                command = "VO " + sensor.Path + " Projection SpaceProjection " + displayRange.ToString();
                if (!TryConnect(command, out result))
                {
                    //MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (displayTimes != null)
            {
                string displayTimesCommand = "";
                foreach (string time in displayTimes)
                {
                    displayTimesCommand += "\"" + time + "\" ";
                }

                command = "DisplayTimes " + sensor.Path + " Intervals Add " + (displayTimes.Length/2).ToString()
                          + " " + displayTimesCommand;
                if (!TryConnect(command, out result))
                {
                    //MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        public static IAgVORefCrdnCollection GetRefCrdns(IAgStkObject stkObject)
        {
            switch (stkObject.ClassType)
            {
                case AgESTKObjectType.eAircraft:
                    IAgAircraft _aircraft = stkObject as IAgAircraft;
                    return _aircraft.VO.Vector.RefCrdns;
                case AgESTKObjectType.eFacility:
                    IAgFacility fac = stkObject as IAgFacility;
                    return fac.VO.Vector.RefCrdns;
                case AgESTKObjectType.eGroundVehicle:
                    IAgGroundVehicle gv = stkObject as IAgGroundVehicle;
                    return gv.VO.Vector.RefCrdns;
                case AgESTKObjectType.eMissile:
                    IAgMissile miss = stkObject as IAgMissile;
                    return miss.VO.Vector.RefCrdns;
                case AgESTKObjectType.ePlace:
                    IAgPlace place = stkObject as IAgPlace;
                    return place.VO.Vector.RefCrdns;
                case AgESTKObjectType.eSatellite:
                    IAgSatellite sat = stkObject as IAgSatellite;
                    return sat.VO.Vector.RefCrdns;
                case AgESTKObjectType.eSensor:
                    IAgSensor sensor = stkObject as IAgSensor;
                    return sensor.VO.Vector.RefCrdns;
                case AgESTKObjectType.eShip:
                    IAgShip ship = stkObject as IAgShip;
                    return ship.VO.Vector.RefCrdns;
                case AgESTKObjectType.eTarget:
                    IAgTarget target = stkObject as IAgTarget;
                    return target.VO.Vector.RefCrdns;
                default:
                    return null;
            }
        }

        public static IAgVORefCrdnVector DisplayVector(string vectorName, IAgStkObject stkObject, Color vectorColor)
        {
            if (!stkObject.Vgt.Vectors.Contains(vectorName))
            {
                return null;
            }

            IAgCrdn _vectorVGT = stkObject.Vgt.Vectors[vectorName] as IAgCrdn;

            IAgVORefCrdnCollection refCrdns = GetRefCrdns(stkObject);

            List<string> displayVectors = new List<string>();
            foreach (IAgVORefCrdn item in refCrdns)
            {
                displayVectors.Add(item.Name);
            }

            IAgVORefCrdnVector _vectorVO;
            if (displayVectors.Contains(_vectorVGT.QualifiedPath))
            {
                _vectorVO = refCrdns.GetCrdnByName(AgEGeometricElemType.eVectorElem, _vectorVGT.QualifiedPath) as IAgVORefCrdnVector;
            }
            else
            {
                _vectorVO = refCrdns.Add(AgEGeometricElemType.eVectorElem, _vectorVGT.QualifiedPath) as IAgVORefCrdnVector;
            }

            _vectorVO.Visible = true;
            _vectorVO.Color = vectorColor;
            _vectorVO.ArrowType = AgEArrowType.e3D;
            _vectorVO.LabelVisible = true;
            _vectorVO.MagnitudeVisible = true;
            //_vectorVO.MagnitudeUnitAbrv = "nm";
            return _vectorVO;
        }

        public static void DisplayAxes(string axesName, IAgCrdnPoint displayOrigin, IAgStkObject stkObject, Color axesColor)
        {
            IAgCrdn axesVGT = stkObject.Vgt.Axes[axesName] as IAgCrdn;
            string cmd = "VO " + stkObject.Path + " SetVectorGeometry Add \""
                         + axesVGT.QualifiedPath + "\" Show On"
                         + " Color " + axesColor.Name
                         + " ShowLabel On"
                         + " OriginPointDef \"" + (displayOrigin as IAgCrdn).QualifiedPath.Replace(" Point", "") + "\"";
            string cmdResult;

            TryConnect(cmd, out cmdResult);
        }

        public static IAgVORefCrdnAngle DisplayAngle(string angleName, IAgStkObject stkObject, Color vectorColor)
        {
            IAgAircraft _aircraft = stkObject as IAgAircraft;
            IAgCrdn _angleVGT = stkObject.Vgt.Angles[angleName] as IAgCrdn;

            IAgVORefCrdnCollection refCrdns = GetRefCrdns(stkObject); //_aircraft.VO.Vector.RefCrdns;

            List<string> displayVectors = new List<string>();
            foreach (IAgVORefCrdn item in refCrdns)
            {
                displayVectors.Add(item.Name);
            }

            IAgVORefCrdnAngle _angleVO;
            if (displayVectors.Contains(_angleVGT.QualifiedPath))
            {
                _angleVO = refCrdns.GetCrdnByName(
                    AgEGeometricElemType.eAngleElem, _angleVGT.QualifiedPath) as IAgVORefCrdnAngle;
            }
            else
            {
                _angleVO = refCrdns.Add(
                    AgEGeometricElemType.eAngleElem, _angleVGT.QualifiedPath) as IAgVORefCrdnAngle;
            }

            _angleVO.AngleValueVisible = true;
            _angleVO.Color = vectorColor;
            _angleVO.LabelVisible = true;

            return _angleVO;
        }

        public static void VectorChangePoint(string vectorName, string OriginPointName, IAgStkObject stkObject)
        {
            string VectorDisplayOrigin = OriginPointName;

            //IAgAircraft _aircraft = stkObject as IAgAircraft;
            IAgCrdn vector = stkObject.Vgt.Vectors[vectorName] as IAgCrdn;
            IAgCrdn point = stkObject.Vgt.Points[OriginPointName] as IAgCrdn;
            IAgVORefCrdnCollection refCrdns = GetRefCrdns(stkObject);
            IAgVORefCrdnVector vectorVO = refCrdns.GetCrdnByName(AgEGeometricElemType.eVectorElem, vector.QualifiedPath) as IAgVORefCrdnVector;
            vectorVO.DrawAtPoint = true;
            vectorVO.Point = point.QualifiedPath;
        }

        public static void VectorChangeSize(string vectorName, double ScaleValue, IAgStkObject stkObject)
        {
            if (stkObject.ClassName == "Aircraft")
            {
                IAgAircraft _aircraft = stkObject as IAgAircraft;
                IAgCrdn vector = stkObject.Vgt.Vectors[vectorName] as IAgCrdn;
                IAgVOVector vectorVO = _aircraft.VO.Vector;
                vectorVO.ScaleRelativeToModel = true;
                vectorVO.VectorSizeScale = ScaleValue;
            }
        }

        public static void VectorChangeColor(string vectorName, IAgStkObject stkObject, Color SelectedColor)
        {
            IAgAircraft _aircraft = stkObject as IAgAircraft;
            IAgCrdn _vectorVGT = stkObject.Vgt.Vectors[vectorName] as IAgCrdn;
            IAgVORefCrdnVector _vectorVO = _aircraft.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eVectorElem, _vectorVGT.QualifiedPath) as IAgVORefCrdnVector;
            _vectorVO.Color = SelectedColor;
        }

        #endregion Vector Geometry Stuff

        public static DateTime ParseISOYMD(string dateISOYMD)
        {
            try
            {

                string pattern = "(?<year>\\d+)-(?<day>\\d+)T(?<hours>\\d+):(?<minutes>\\d+):(?<seconds>\\d+)(.(?<decimal>\\d+))?";
                Regex dateRegex = new Regex(pattern);
                Match match = dateRegex.Match(dateISOYMD);
                if (!match.Success)
                {
                    dateISOYMD = Root.ConversionUtility.ConvertDate("ISO-YD", "ISO-YMD", dateISOYMD);
                }
                // 2015-02-21T07:39:59.097
                // 012345678901234567890123

                pattern = "(?<year>\\d+)-(?<month>\\d+)-(?<day>\\d+)T(?<hours>\\d+):(?<minutes>\\d+):(?<seconds>\\d+)(.(?<decimal>\\d+))?";
                dateRegex = new Regex(pattern);
                match = dateRegex.Match(dateISOYMD);


                int year = int.Parse(match.Groups["year"].Value);
                int month = int.Parse(match.Groups["month"].Value);
                int day = int.Parse(match.Groups["day"].Value);
                int hrs = int.Parse(match.Groups["hours"].Value);
                int mins = int.Parse(match.Groups["minutes"].Value);
                int secs = int.Parse(match.Groups["seconds"].Value);
                int millisec = 0;
                if (match.Groups["decimal"].Success)
                {
                    millisec = int.Parse(match.Groups["decimal"].Value.Substring(0, 3));
                }
                DateTime time = new DateTime(year, month, day, hrs, mins, secs, millisec);
                return time;
            }
            catch
            {
                return DateTime.MinValue;
            }

        }


        public static double GetSatellitePeriod(string satPath, string dateISOYMD)
        {
            IAgDataPrvTimeVar dpTimeVarying = GetClassicalElementsDP(satPath);
            Array elements = new object[] {"Period"};
            IAgDrResult dpResult = dpTimeVarying.ExecSingleElements(dateISOYMD, elements);

            double period = 0;
            if (dpResult.DataSets.Count > 0)
            {
                period = (double)dpResult.DataSets.GetDataSetByName(elements.GetValue(0).ToString()).GetValues().GetValue(0);
            }
            return period;
        }

        public static double GetSatelliteApogee(string satPath, string dateISOYMD)
        {
            IAgDataPrvTimeVar dpTimeVarying = GetClassicalElementsDP(satPath);
            Array elements = new object[] {"Apogee Radius"};
            IAgDrResult dpResult = dpTimeVarying.ExecSingleElements(dateISOYMD, elements);

            double apogee = (double)dpResult.DataSets.GetDataSetByName(elements.GetValue(0).ToString()).GetValues().GetValue(0);

            return apogee;
        }

        public static double GetSatellitePerigee(string satPath, string dateISOYMD)
        {
            IAgDataPrvTimeVar dpTimeVarying = GetClassicalElementsDP(satPath);
            Array elements = new object[] {"Perigee Radius"};
            IAgDrResult dpResult = dpTimeVarying.ExecSingleElements(dateISOYMD, elements);

            double perigee = (double)dpResult.DataSets.GetDataSetByName(elements.GetValue(0).ToString()).GetValues().GetValue(0);

            return perigee;
        }

        private static IAgDataPrvTimeVar GetClassicalElementsDP(string satPath)
        {
            IAgStkObject primary = Root.GetObjectFromPath(satPath);

            IAgDataProviderGroup classical = primary.DataProviders["Classical Elements"] as IAgDataProviderGroup;
            IAgDataProvider dataProvider = classical.Group["ICRF"] as IAgDataProvider;
            //Time varyign data is given as an array of time based values 
            IAgDataPrvTimeVar dpTimeVarying = dataProvider as IAgDataPrvTimeVar;
            return dpTimeVarying;
        }

        public static void LoadAllEphemerisFromDirectory(DirectoryInfo dirInfo)
        {
            foreach (var file in dirInfo.GetFiles("*.e"))
            {
                LoadSatelliteFromEphemeris(file);
            }
        }

        public static IAgSatellite LoadSatelliteFromEphemeris(FileInfo eFile)
        {
            var satName = Path.GetFileNameWithoutExtension(eFile.Name).ToStkSafeName();

            while (Root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, satName))
            {
                satName = IncrementName(satName);
            }
            IAgSatellite sat;
            try
            {
                sat = (IAgSatellite) Root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, satName);
            }
            catch (Exception e)
            {
                return null;
            }

            sat.SetPropagatorType(AgEVePropagatorType.ePropagatorStkExternal);
            var prop = (IAgVePropagatorStkExternal) sat.Propagator;
            prop.Filename = eFile.FullName;
            prop.Propagate();
            return sat;

        }

        private static string IncrementName(string name)
        {
            //add a digit to deconflict, unless already a digit, then add one to digit? why not.
            return char.IsDigit(name.Last())
                ? name.Substring(0, name.Length - 1) + (int) (char.GetNumericValue(name.Last()) + 1)
                : name += "1";
        }

        public static void MergeEphemeris(IAgStkObject[] objectArray, string outputFilePath)
        {
            Dictionary<double, IAgStkObject> ephems = new Dictionary<double, IAgStkObject>();
            Root.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec");
            foreach (IAgStkObject stkObject in objectArray)
            {
                if (stkObject is IAgSatellite
                    && (stkObject as IAgSatellite).PropagatorType == AgEVePropagatorType.ePropagatorStkExternal)
                {
                    IAgVePropagatorStkExternal exProp =
                        (stkObject as IAgSatellite).Propagator as IAgVePropagatorStkExternal;
                    if (!ephems.ContainsKey(double.Parse(exProp.StartTime.ToString())))
                    {
                        ephems.Add(double.Parse(exProp.StartTime.ToString()), stkObject);
                    }
                }
            }

            List<KeyValuePair<double, IAgStkObject>> testing = ephems.OrderBy(i => i.Key).ToList();
            List<string> ephemLines = new List<string>();

            for (int i = 0; i < testing.Count(); i++)
            {
                IAgDataProviderGroup dp = (IAgDataProviderGroup) testing[i].Value.DataProviders["Cartesian Position"];
                IAgDataProvider dpICRF = (IAgDataProvider)dp.Group["ICRF"];
                IAgDataPrvTimeVar dpTimeVarying = (IAgDataPrvTimeVar)dpICRF;

                string stopTime = i < (testing.Count() - 1)
                    ? testing[i + 1].Key.ToString()
                    : ((testing[i].Value as IAgSatellite).Propagator as IAgVePropagatorStkExternal).StopTime.ToString();
                string startTime = testing[i].Key.ToString();
                IAgDrResult results = dpTimeVarying.ExecNativeTimes(startTime, stopTime);
                Array times = results.DataSets.GetDataSetByName("Time").GetValues();
                Array xs = results.DataSets.GetDataSetByName("x").GetValues();
                Array ys = results.DataSets.GetDataSetByName("y").GetValues();
                Array zs = results.DataSets.GetDataSetByName("z").GetValues();

                dp = (IAgDataProviderGroup) testing[i].Value.DataProviders["Cartesian Velocity"];
                dpICRF = (IAgDataProvider)dp.Group["ICRF"];
                dpTimeVarying = (IAgDataPrvTimeVar)dpICRF;
                
                results = dpTimeVarying.ExecNativeTimes(startTime, stopTime);
                Array xds = results.DataSets.GetDataSetByName("x").GetValues();
                Array yds = results.DataSets.GetDataSetByName("y").GetValues();
                Array zds = results.DataSets.GetDataSetByName("z").GetValues();

                for (int j = 0; j < (times.Length - 2); j++)
                {
                    ephemLines.Add(string.Format("{0}     {1}     {2}     {3}     {4}     {5}     {6}",
                        times.GetValue(j), xs.GetValue(j), ys.GetValue(j), zs.GetValue(j), xds.GetValue(j),
                        yds.GetValue(j), zds.GetValue(j)));
                }
                //MessageBox.Show("stored values");

            }
            int count = ephemLines.Count;
            ephemLines.Insert(0, "stk.v.10.0");
            ephemLines.Insert(1, "BEGIN Ephemeris");
            ephemLines.Insert(2, "NumberOfEphemerisPoints " + count);
            ephemLines.Insert(3, "InterpolationMethod     Lagrange");
            ephemLines.Insert(4, "InterpolationOrder  5");
            ephemLines.Insert(5,
                "ScenarioEpoch " +
                Root.ConversionUtility.ConvertDate(Root.UnitPreferences.GetCurrentUnitAbbrv("DateFormat"), "UTCG",
                    (Root.CurrentScenario as IAgScenario).Epoch.ToString()));
            ephemLines.Insert(6, "CentralBody             Earth");
            ephemLines.Insert(7, "CoordinateSystem        ICRF");
            ephemLines.Insert(8, "DistanceUnit        Meters");
            ephemLines.Insert(9, "EphemerisTimePosVel");

            ephemLines.Add("END Ephemeris");

            File.WriteAllLines(outputFilePath, ephemLines.ToArray());
        }
    }
    public static class NameExtensions
    {
        public static string ToStkSafeName(this string input)
        {
            var invalidChars = Path.GetInvalidFileNameChars().ToList();
            invalidChars.Add(' ');
            invalidChars.Add('.');
            return invalidChars.Aggregate(input, (current, c) => current.Replace(c.ToString(), "_"));
        }
    }


    //public static DateTime ParseStringDate(string date)
        //{
        //    try
        //    {
        //        // 02/03/2009 05:53:24.204
        //        // 012345678901234567890123
        //        Regex dateRegex;
        //        Match match;


        //        string patternDMY = "(?<day>\\d+)/(?<month>\\d+)/(?<year>\\d+).(?<hours>\\d+):(?<minutes>\\d+):(?<seconds>\\d+).(?<decimal>\\d+)";
        //        string patternISOYMD = "(?<year>\\d+)-(?<month>\\d+)-(?<day>\\d+)T(?<hours>\\d+):(?<minutes>\\d+):(?<seconds>\\d+).(?<decimal>\\d+)";

        //        dateRegex = new Regex(patternISOYMD);
        //        match = dateRegex.Match(date);

        //        int year = int.Parse(match.Groups["year"].Value);
        //        int month = int.Parse(match.Groups["month"].Value);
        //        int day = int.Parse(match.Groups["day"].Value);
        //        int hrs = int.Parse(match.Groups["hours"].Value);
        //        int mins = int.Parse(match.Groups["minutes"].Value);
        //        int secs = int.Parse(match.Groups["seconds"].Value);
        //        int millisec = int.Parse(match.Groups["decimal"].Value.Substring(0, 3));

        //        DateTime time = new DateTime(year, month, day, hrs, mins, secs, millisec);
        //        return time;
        //    }
        //    catch
        //    {
        //        return DateTime.MinValue;
        //    }

        //}
}
