using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SendEphemerisToSTK
{
    public static class SendToStk
    {
        private static dynamic _uiApp;
        private static dynamic _stkRoot;
        public static void SendEphemeris(string[] filePaths, out string errors)
        {
            errors = string.Empty;
            
            try
            {
                _uiApp = Marshal.GetActiveObject("STK.Application");
            }
            catch 
            {
                errors = "Unable to connect to STK.";
                return;
            }
            _stkRoot = _uiApp.Personality2;

            if (_stkRoot.CurrentScenario == null)
            {
                errors = "An STK scenario must be open before sending ephemeris.";
                return;
            }

            foreach (var filePath in filePaths)
            {
                string extension = Path.GetExtension(filePath);
                if (extension == null)
                {
                    continue;
                }

                const int ePropagatorStkExternal = 6;
                if (extension.ToUpperInvariant().Equals(".E"))
                {
                    var satName = Path.GetFileNameWithoutExtension(filePath);
                    satName = ToStkSafeName(satName);
                    
                    const int eSatellite = 18;
                    while (_stkRoot.CurrentScenario.Children.Contains(eSatellite, satName))
                    {
                        satName = IncrementName(satName);
                    }

                    dynamic sat;
                    try
                    {
                        sat = _stkRoot.CurrentScenario.Children.New(eSatellite, satName);
                    }
                    catch (Exception e)
                    {
                        errors = "Error adding ephemeris file, most likely an illegal character in object name " + satName + "." + Environment.NewLine + e.ToString();
                        continue;
                    }

                    sat.SetPropagatorType(ePropagatorStkExternal);
                    var prop = sat.Propagator;
                    prop.Filename = filePath;
                    prop.Propagate();

                    if (!IsGeoRegime(sat.Path))
                    {
                        continue;
                    }

                    string command = $"VO */Satellite/{sat.InstanceName} OrbitSystem Modify System \"FixedByWindow\" Color Default Show On";
                    _stkRoot.ExecuteCommand(command);
                    command = $"VO */Satellite/{sat.InstanceName} OrbitSystem Modify System \"InertialByWindow\" Show Off";
                    _stkRoot.ExecuteCommand(command);
                }
                else if (extension.ToUpperInvariant().Equals(".TLE") || extension.ToUpperInvariant().Equals(".TCE"))
                {
                    _stkRoot.ExecuteCommand("ImportTLEFile * \"" + filePath + "\" AutoPropagate On");
                }
                else if (extension.ToUpperInvariant().Equals(".GPX"))
                {
                    ReadGpx(filePath, out errors);
                }
            }
        }

        public static void ReadGpx(string gpxFilePath, out string errors)
        {
            errors = "";
            var gvName = Path.GetFileNameWithoutExtension(gpxFilePath);
            gvName = ToStkSafeName(gvName);

            const int eGroundVehicle = 9;
            while (_stkRoot.CurrentScenario.Children.Contains(eGroundVehicle, gvName))
            {
                gvName = IncrementName(gvName);
            }

            XmlDocument gpxDoc = new XmlDocument();
            gpxDoc.Load(gpxFilePath);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(gpxDoc.NameTable);
            nsmgr.AddNamespace("x", "http://www.topografix.com/GPX/1/1");
            XmlNodeList nl = gpxDoc.SelectNodes("//x:trkpt", nsmgr);
            _stkRoot.ExecuteCommand("BatchGraphics * On");
            _stkRoot.ExecuteCommand($"New / */GroundVehicle {gvName}");
            _stkRoot.ExecuteCommand($"AltitudeRef */GroundVehicle/{gvName} Ref MSL");
            _stkRoot.ExecuteCommand($"Graphics */GroundVehicle/{gvName} Waypoints Off");
            _stkRoot.ExecuteCommand($"Graphics */GroundVehicle/{gvName} Basic LineWidth 3");
            _stkRoot.ExecuteCommand("Units_Set * Connect DateFormat ISO-YMD");
            foreach (XmlNode xnode in nl)
            {
                try
                {
                    string name = xnode.Name;
                    string lat = xnode.Attributes["lat"].Value;
                    string lon = xnode.Attributes["lon"].Value;
                    string alt = xnode["ele"].InnerText;
                    string time = xnode["time"].InnerText.Replace("Z",string.Empty);
                
                    _stkRoot.ExecuteCommand($"AddWaypoint */GroundVehicle/{gvName} DetVelFromTime {lat} {lon} {alt} {time}");
                }
                catch 
                {
                    errors += $"Error adding waypoint {xnode.InnerText}{Environment.NewLine}";
                }
            }

            _stkRoot.ExecuteCommand("BatchGraphics * Off");
        }


        public static bool IsGeoRegime(string objectPath)
        {
            if (string.IsNullOrEmpty(objectPath)) return false;

            var period = GetSatellitePeriod(objectPath);
            return Math.Abs(86400 - period) / 864 < 8;
        }

        public static double GetSatellitePeriod(string satPath, string dateISOYMD = null)
        {
            var dpTimeVarying = GetClassicalElementsDp(satPath);
            Array elements = new object[] { "Period" };
            if (dateISOYMD == null)
            {
                dateISOYMD = _stkRoot.GetObjectFromPath(satPath).Propagator.StartTime;
            }
            var dpResult = dpTimeVarying.ExecSingleElements(dateISOYMD, elements);

            double period = 0;
            if (dpResult.DataSets.Count > 0)
            {
                period = (double)dpResult.DataSets.GetDataSetByName(elements.GetValue(0).ToString()).GetValues().GetValue(0);
            }
            return period;
        }

        private static dynamic GetClassicalElementsDp(string satPath)
        {
            var primary = _stkRoot.GetObjectFromPath(satPath);

            var classical = primary.DataProviders["Classical Elements"];
            var dataProvider = classical.Group["ICRF"];
            //Time varying data is given as an array of time based values 
            var dpTimeVarying = dataProvider;
            return dpTimeVarying;
        }

        public static string IncrementName(string name)
        {
            //add a digit to deconflict, unless already a digit, then add one to digit? why not.
            return char.IsDigit(name.Last()) ? 
                name.Substring(0, name.Length - 1) + (int)(char.GetNumericValue(name.Last()) + 1) :
                name += "1";
        }

        public static string ToStkSafeName(string input)
        {
            var invalidChars = Path.GetInvalidFileNameChars().ToList();
            invalidChars.Add(' ');
            invalidChars.Add('.');
            return invalidChars.Aggregate(input, (current, c) => current.Replace(c.ToString(), "_"));
        }

    }
}
