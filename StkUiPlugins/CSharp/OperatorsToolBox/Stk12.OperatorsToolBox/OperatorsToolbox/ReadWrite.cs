using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using AGI.STKObjects;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OperatorsToolbox.Coverage;
using OperatorsToolbox.FacilityCreator;
using OperatorsToolbox.GroundEvents;
using OperatorsToolbox.PlaneCrossingUtility;
using OperatorsToolbox.SatelliteCreator;
using OperatorsToolbox.SmartView;
using OperatorsToolbox.VolumeCreator;
using System.Text.RegularExpressions;
using OperatorsToolbox.Templates;
using System.Diagnostics;

namespace OperatorsToolbox
{
    public static class ReadWrite
    {
        private static void SerializeObject<T>(T objectToSerialize, string fileName, bool append = false)
            where T : new()
        {
            try
            {
                var setting = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Newtonsoft.Json.Formatting.Indented
                };

                var data = JsonConvert.SerializeObject(objectToSerialize, setting);

                if (append)
                {
                    using var stream = new StreamReader(fileName);
                    if (!string.IsNullOrEmpty(stream.ReadToEnd()))
                    {
                        JObject.Parse(data).Merge(JObject.Parse(stream.ReadToEnd()));
                    }
                }

                using (var stream = new StreamWriter(fileName, append))
                {
                    stream.Write(data);
                }
            }
            catch (IOException ioException)
            {
                MessageBox.Show(ioException.Message);
            }
        }

        private static T DeserializeObject<T>(string filePath) where T : new()
        {
            try
            {
                using var reader = new StreamReader(filePath);
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
            catch (IOException ioException)
            {
                MessageBox.Show(ioException.Message);
                return default;
            }
        }

        public static List<string> GetLicensingData()
        {
            List<string> validLicenses = new List<string>();
            string xmlString = CommonData.StkRoot.GetLicensingReport();
            string status;
            XmlReader xmlReader = XmlReader.Create(new StringReader(xmlString));
            while (xmlReader.Read())
            {
                if (xmlReader.Name == "LicenseStatus")
                {
                    if (xmlReader.HasAttributes)
                    {
                        status = xmlReader.GetAttribute("Status");
                        if (status == "1")
                        {
                            if (!validLicenses.Contains(xmlReader.GetAttribute("Product")))
                            {
                                validLicenses.Add(xmlReader.GetAttribute("Product"));
                            }
                        }
                    }
                }
            }


            return validLicenses;
        }
        //Smart View functions
        public static List<ViewData> ReadSavedViews(string fileStr)
        {
            return DeserializeObject<List<ViewData>>(fileStr);
        }

        public static void WriteSavedViews(string fileStr)
        {
            SerializeObject(CommonData.SavedViewList, fileStr);
        }

        public static void WriteObjectData(string fileStr)
        {
            SerializeObject(CommonData.SavedViewList, fileStr);
        }

        //Read satellite catalog 
        public static void ReadSatCat()
        {
            CommonData.SatCatItemList.Clear();
            CommonData.SatCatFofo.Clear();
            CommonData.MetadataOptions1.Clear();
            CommonData.MetadataOptions2.Clear();
            CommonData.MetadataOptions3.Clear();
            CommonData.MetadataOptions4.Clear();
            CommonData.MetadataOptions5.Clear();
            CommonData.MetadataTypeList.Clear();
            Excel.Workbook myBook = null;
            Excel.Application myApp = null;
            Excel.Worksheet mySheet = null;

            myApp = new Excel.Application();
            myApp.Visible = false;
            //Mostly likely failure is due to lack of excel install or inproper path to the catalog
            try
            {
                myBook = myApp.Workbooks.Open(CommonData.Preferences.SatCatLocation);
                mySheet = (Excel.Worksheet)myBook.Sheets[1];
                Excel.Range range = mySheet.UsedRange;
                object[,] xlRange = range.Value2;
                var commonName = xlRange[2, 3];
                var otherName = xlRange[2, 4];
                var fofo = xlRange[2, 5];
                var fov = xlRange[2, 11];
                //Initialize primary metadata types and headers
                var metadata1 = xlRange[2, 6];
                var metadata2 = xlRange[2, 7];
                var metadata3 = xlRange[2, 8];
                var metadata4 = xlRange[2, 9];
                var metadata5 = xlRange[2, 10];
                string type1 = range.Cells[1, 6].Value;
                string type2 = range.Cells[1, 7].Value;
                string type3 = range.Cells[1, 8].Value;
                string type4 = range.Cells[1, 9].Value;
                string type5 = range.Cells[1, 10].Value;

                CommonData.MetadataTypeList.Add(type1);
                CommonData.MetadataTypeList.Add(type2);
                CommonData.MetadataTypeList.Add(type3);
                CommonData.MetadataTypeList.Add(type4);
                CommonData.MetadataTypeList.Add(type5);
                CommonData.MetadataTypeList.Add("Color");
                int rows = range.Rows.Count;

                for (int i = 2; i < rows; i++)
                {
                    SatCatItem item = new SatCatItem();
                    item.MetadataTypes.Add(type1);
                    item.MetadataTypes.Add(type2);
                    item.MetadataTypes.Add(type3);
                    item.MetadataTypes.Add(type4);
                    item.MetadataTypes.Add(type5);
                    item.Scc = xlRange[i, 2].ToString();
                    commonName = xlRange[i, 3];
                    otherName = xlRange[i, 4];
                    fofo = xlRange[i, 5];
                    metadata1 = xlRange[i, 6];
                    metadata2 = xlRange[i, 7];
                    metadata3 = xlRange[i, 8];
                    metadata4 = xlRange[i, 9];
                    metadata5 = xlRange[i, 10];
                    fov = xlRange[i, 11];
                    #region Check variables in cells
                    if (commonName != null)
                    {
                        item.CommonName = commonName.ToString();
                    }
                    else
                    {
                        item.CommonName = "Unspecified";
                    }
                    if (otherName != null)
                    {
                        item.OtherName = otherName.ToString();
                    }
                    else
                    {
                        item.OtherName = "Unspecified";
                    }

                    if (fofo != null)
                    {
                        item.Fofo = fofo.ToString();
                        if (!CommonData.SatCatFofo.Contains(item.Fofo))
                        {
                            CommonData.SatCatFofo.Add(item.Fofo);
                        }
                    }
                    else
                    {
                        item.Fofo = "Unspecified";
                        if (!CommonData.SatCatFofo.Contains("Unspecified"))
                        {
                            CommonData.SatCatFofo.Add("Unspecified");
                        }
                    }

                    if (metadata1 != null)
                    {
                        item.Metadata1 = metadata1.ToString();
                        if (!CommonData.MetadataOptions1.Contains(item.Metadata1))
                        {
                            CommonData.MetadataOptions1.Add(item.Metadata1);
                        }
                    }
                    else
                    {
                        item.Metadata1 = "Unspecified";
                        if (!CommonData.MetadataOptions1.Contains("Unspecified"))
                        {
                            CommonData.MetadataOptions1.Add("Unspecified");
                        }
                    }

                    if (metadata2 != null)
                    {
                        item.Metadata2 = metadata2.ToString();
                        if (!CommonData.MetadataOptions2.Contains(item.Metadata2))
                        {
                            CommonData.MetadataOptions2.Add(item.Metadata2);
                        }
                    }
                    else
                    {
                        item.Metadata2 = "Unspecified";
                        if (!CommonData.MetadataOptions2.Contains("Unspecified"))
                        {
                            CommonData.MetadataOptions2.Add("Unspecified");
                        }
                    }

                    if (metadata3 != null)
                    {
                        item.Metadata3 = metadata3.ToString();
                        if (!CommonData.MetadataOptions3.Contains(item.Metadata3))
                        {
                            CommonData.MetadataOptions3.Add(item.Metadata3);
                        }
                    }
                    else
                    {
                        item.Metadata3 = "Unspecified";
                        if (!CommonData.MetadataOptions3.Contains("Unspecified"))
                        {
                            CommonData.MetadataOptions3.Add("Unspecified");
                        }
                    }

                    if (metadata4 != null)
                    {
                        item.Metadata4 = metadata4.ToString();
                        if (!CommonData.MetadataOptions4.Contains(item.Metadata4))
                        {
                            CommonData.MetadataOptions4.Add(item.Metadata4);
                        }
                    }
                    else
                    {
                        item.Metadata4 = "Unspecified";
                        if (!CommonData.MetadataOptions4.Contains("Unspecified"))
                        {
                            CommonData.MetadataOptions4.Add("Unspecified");
                        }
                    }

                    if (metadata5 != null)
                    {
                        item.Metadata5 = metadata5.ToString();
                        if (!CommonData.MetadataOptions5.Contains(item.Metadata5))
                        {
                            CommonData.MetadataOptions5.Add(item.Metadata5);
                        }
                    }
                    else
                    {
                        item.Metadata5 = "Unspecified";
                        if (!CommonData.MetadataOptions5.Contains("Unspecified"))
                        {
                            CommonData.MetadataOptions5.Add("Unspecified");
                        }
                    }

                    if (fov != null)
                    {
                        item.Fov = Double.Parse(fov.ToString());
                    }
                    else
                    {
                        item.Fov = 0;
                    }
                    CommonData.SatCatItemList.Add(item);
                    CommonData.MetadataOptions1.Sort();
                    CommonData.SatCatFofo.Sort();
                    CommonData.MetadataOptions2.Sort();
                    CommonData.MetadataOptions4.Sort();
                    #endregion
                }
                myBook.Close();
                myApp.Quit();
            }
            catch (Exception)
            {
                MessageBox.Show("Could not open satellite catalog");
                return;
            }

        }

        //Volume Creator Functions
        //Read saved missile configs from file
        public static List<VolumeConfig> ReadVolumeConfigFile(string fileStr)
        {
            return DeserializeObject<List<VolumeConfig>>(fileStr);
        }

        //ReadSaved Location Configs from file
        public static List<LocationConfig> ReadLocationFile(string fileStr)
        {
            return DeserializeObject<List<LocationConfig>>(fileStr);
        }

        //write saved missile configs to file
        public static void WriteVolumeConfigFile(string fileStr)
        {
            SerializeObject(CommonData.VolumeList, fileStr);
        }

        //write saved location configs to file
        public static void WriteLocationFile(string fileStr)
        {
            SerializeObject(CommonData.LocationList, fileStr);
        }

        //Facility Creator Functions
        public static void WriteCadenceDatabase()
        {
            string dbFileStr = Path.Combine(@CommonData.InstallDir, "Databases\\CadenceDatabase.json");
            string localFileStr = CommonData.DirectoryStr + "\\CadenceData.json";
            File.WriteAllText(dbFileStr, String.Empty);
            File.WriteAllText(localFileStr, String.Empty);

            foreach (var item in CommonData.Cadences)
                if (item.SaveToDatabase)
                    SerializeObject(CommonData.Cadences, dbFileStr, false);
                else
                    SerializeObject(CommonData.Cadences, localFileStr, true);
        }

        public static List<SensorCadance> ReadCadences(string fileStr)
        {
            return DeserializeObject<List<SensorCadance>>(fileStr);
        }

        public static void ImportCadenceFromFile(string fileStr)
        {
            using (StreamReader reader = new StreamReader(@fileStr))
            {
                string line = reader.ReadLine();
                string[] linepeices;
                while (line != null || reader.EndOfStream)
                {
                    GroundLocation location = new GroundLocation();
                    linepeices = line.Split();
                    location.LocationName = linepeices[0];
                    location.Latitude = Double.Parse(linepeices[1]);
                    location.Longitude = Double.Parse(linepeices[2]);
                    location.Altitude = Double.Parse(linepeices[3]);
                }
            }
        }

        public static List<GroundLocation> ReadFacilityFile(string filestr)
        {
            List<GroundLocation> locations = new List<GroundLocation>();
            bool exists = File.Exists(filestr);
            if (exists)
            {
                using (StreamReader reader = new StreamReader(@filestr))
                {
                    string line;
                    string errorString = "There was an error importing the following facilities:\n";
                    int errorCount = 0;
                    int localErrorCount = 0;
                    int lineCounter = 0;
                    double temp;
                    bool isNumerical;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineCounter++;
                        GroundLocation loc = new GroundLocation();
                        string[] lineComponents = line.Split(',');
                        //Check for right number of inputs
                        if (lineComponents.Length == 4)
                        {
                            localErrorCount = 0;
                            //Get Name of Facility
                            loc.LocationName = Regex.Replace(lineComponents[0], @"[^0-9a-zA-Z_]+", "");
                            //Check if LLA values are numerical
                            isNumerical = Double.TryParse(lineComponents[1].Trim(), out temp);
                            if (isNumerical)
                            {
                                loc.Latitude = temp;
                            }
                            else
                            {
                                errorString = errorString + "Line " + lineCounter.ToString() + " - Latitude not a valid number\n";
                                errorCount++;
                                localErrorCount++;
                            }
                            isNumerical = Double.TryParse(lineComponents[2].Trim(), out temp);
                            if (isNumerical)
                            {
                                loc.Longitude = temp;
                            }
                            else
                            {
                                errorString = errorString + "Line " + lineCounter.ToString() + " - Latitude not a valid number\n";
                                errorCount++;
                                localErrorCount++;
                            }
                            isNumerical = Double.TryParse(lineComponents[3].Trim(), out temp);
                            if (isNumerical)
                            {
                                loc.Altitude = temp;
                            }
                            else
                            {
                                errorString = errorString + "Line " + lineCounter.ToString() + " - Latitude not a valid number\n";
                                errorCount++;
                                localErrorCount++;
                            }
                            //Add facility to list if there are no errors on the line
                            if (localErrorCount == 0)
                            {
                                locations.Add(loc);
                            }
                        }
                        else
                        {
                            errorString = errorString + "Line " + lineCounter.ToString() + " - Invalid format\n";
                            errorCount++;
                        }
                    }
                    if (errorCount != 0)
                    {
                        MessageBox.Show(errorString);
                    }
                }
            }
            else
            {
                MessageBox.Show("Filepath does not exist");
            }
            return locations;
        }

        public static List<GroundLocation> ReadFacilityJson(string fileStr)
        {
            List<GroundLocation> locations = new List<GroundLocation>();
            FacilityJsonInput info = DeserializeObject<FacilityJsonInput>(fileStr);
            List<FacilityInformation> data = info.data;
            foreach (var item in data)
            {
                GroundLocation loc = new GroundLocation();
                loc.Latitude = item.geometry.lat;
                loc.Longitude = item.geometry.lon;
                loc.Altitude = item.geometry.alt / 1000; //file is in meters but import default is km
                loc.LocationName = item.properties.name;
                locations.Add(loc);
            }
            return locations;
        }

        //Ground Event Functions
        public static List<GroundEvent> ReadEventFile(string fileStr)
        {
            return DeserializeObject<List<GroundEvent>>(fileStr);
        }

        public static void WriteEventFile(string fileStr)
        {
            SerializeObject(CommonData.CurrentEvents, fileStr);
        }

        public static void RemoveEvent()
        {
            if (CommonData.EventSelectedIndex == -1) return;
                CommonData.CurrentEvents.RemoveAt(CommonData.EventSelectedIndex);
                WriteEventFile(CommonData.EventFileStr);
        }

        public static string WriteDetails()
        {
            string details = null;
            details = "ID: " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "\r\n";
            details = details + "AOR: " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Country + "\r\n";
            details = details + "Latitude: " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Latitude + "\r\n";
            details = details + "Longitude: " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Longitude + "\r\n";
            details = details + "Start Time: " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].StartTime + "\r\n";
            details = details + "Stop Time: " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].StopTime + "\r\n";
            details = details + "Description: " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Description + "\r\n";
            details = details + "POC: " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Poc + "\r\n";
            details = details + "POC Phone: " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].PocPhone + "\r\n";
            details = details + "POC Email: " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].PocEmail + "\r\n";

            return details;
        }

        public static void ImportEventSheet(string fileStr, int importOption, string colorOption)
        {
            Excel.Workbook myBook = null;
            Excel.Application myApp = null;
            Excel.Worksheet mySheet = null;
            string cmd = null;
            string path = null;

            myApp = new Excel.Application();
            myApp.Visible = false;
            try
            {
                myBook = myApp.Workbooks.Open(fileStr);
            }
            catch (Exception)
            {
                string mes = "Could not open specified file";
                MessageBox.Show(mes);

            }
            mySheet = (Excel.Worksheet)myBook.Sheets[1];
            Excel.Range range = mySheet.UsedRange;
            object[,] xlRange = range.Value2;
            int rows = range.Rows.Count;

            myBook.Close();
            myApp.Quit();

            var classification = xlRange[2, 1];
            var id = xlRange[2, 2];
            var status = xlRange[2, 3];
            var type = xlRange[2, 4];
            var aor = xlRange[2, 5];
            var bluf = xlRange[2, 6];
            var startTime = xlRange[2, 9];
            var stopTime = xlRange[2, 10];
            var poc = xlRange[2, 15];
            var pocPhone = xlRange[2, 16];
            var pocEmail = xlRange[2, 17];
            var lat = xlRange[2, 21];
            var longitude = xlRange[2, 22];
            string errorMes = "There was an error loading the following row(s): \n\r";
            string start = null;
            string stop = null;
            int errorCount = 0;
            for (int i = 3; i <= rows; i++)
            {
                try
                {
                    GroundEvent currentGroundEvent = new GroundEvent();
                    status = CheckNullCell(xlRange[i, 3]);
                    if (status.ToString() == "Closed" && importOption == 0)
                    {

                    }
                    else
                    {
                        id = xlRange[i, 2];
                        if (id != null)
                        {
                            if (CommonData.CurrentEvents.Any(p => p.Id == id.ToString()))
                            {
                                int index = CommonData.CurrentEvents.FindIndex(p => p.Id == id.ToString());

                                CommonData.CurrentEvents[index].Classification = CheckNullCell(xlRange[i, 1]);
                                CommonData.CurrentEvents[index].Status = CheckNullCell(xlRange[i, 3]);
                                string currentType = CommonData.CurrentEvents[index].SsrType;
                                if (currentType != xlRange[i, 4].ToString())
                                {
                                    CommonData.CurrentEvents[index].SsrType = CheckNullCell(xlRange[i, 4]).Replace(" ", "");
                                    CommonData.TypeChanged = true;

                                    string filePath = GroundEventFunctions.GetImagePath(CommonData.CurrentEvents[index].SsrType);

                                    cmd = "VO */Place/" + CommonData.CurrentEvents[index].Id + " marker show on markertype imagefile imagefile \"" + filePath + "\" Size 32";
                                    CommonData.StkRoot.ExecuteCommand(cmd);
                                }
                                CommonData.CurrentEvents[index].Country = CheckNullCell(xlRange[i, 5]);
                                CommonData.CurrentEvents[index].Description = CheckNullCell(xlRange[i, 6]);
                                start = CheckTimeCell(xlRange[i, 9]);
                                if (start == "Unspecified")
                                {
                                    CommonData.CurrentEvents[index].StartTime = "Unspecified";
                                    CommonData.CurrentEvents[index].MilStartTime = "Unspecified";
                                }
                                else
                                {
                                    CommonData.CurrentEvents[index].MilStartTime = xlRange[i, 9].ToString();
                                    CommonData.CurrentEvents[index].StartTime = start;
                                }

                                stop = CheckTimeCell(xlRange[i, 10]);
                                if (stop == "Unspecified")
                                {
                                    CommonData.CurrentEvents[index].StopTime = "Unspecified";
                                    CommonData.CurrentEvents[index].MilStopTime = "Unspecified";
                                }
                                else
                                {
                                    CommonData.CurrentEvents[index].MilStopTime = xlRange[i, 10].ToString();
                                    CommonData.CurrentEvents[index].StopTime = stop;
                                }
                                if (start == "Unspecified" || stop == "Unspecified")
                                {

                                    GroundEventFunctions.RemoveTimelineComponent(CommonData.CurrentEvents[index]);
                                }
                                else
                                {
                                    GroundEventFunctions.CreateTimelineComponent(CommonData.CurrentEvents[index]);
                                }

                                CommonData.CurrentEvents[index].Poc = CheckNullCell(xlRange[i, 15]);
                                CommonData.CurrentEvents[index].PocPhone = CheckNullCell(xlRange[i, 16]);
                                CommonData.CurrentEvents[index].PocEmail = CheckNullCell(xlRange[i, 17]);
                                CommonData.CurrentEvents[index].Latitude = xlRange[i, 21].ToString();
                                CommonData.CurrentEvents[index].Longitude = xlRange[i, 22].ToString();
                                //Reassign position
                                path = "Place/" + CommonData.CurrentEvents[index].Id;
                                IAgPlace place = CommonData.StkRoot.GetObjectFromPath(path) as IAgPlace;
                                place.Position.AssignGeodetic(Double.Parse(xlRange[i, 21].ToString()), Double.Parse(xlRange[i, 22].ToString()), 0);
                            }
                            else
                            {
                                currentGroundEvent.Classification = CheckNullCell(xlRange[i, 1]);
                                currentGroundEvent.Id = xlRange[i, 2].ToString();
                                currentGroundEvent.Status = CheckNullCell(xlRange[i, 3]);
                                currentGroundEvent.SsrType = CheckNullCell(xlRange[i, 4]).Replace(" ", "");
                                currentGroundEvent.Country = CheckNullCell(xlRange[i, 5]);
                                currentGroundEvent.Description = CheckNullCell(xlRange[i, 6]);
                                start = CheckTimeCell(xlRange[i, 9]);
                                if (start == "Unspecified")
                                {
                                    currentGroundEvent.StartTime = "Unspecified";
                                    currentGroundEvent.MilStartTime = "Unspecified";
                                }
                                else
                                {
                                    currentGroundEvent.MilStartTime = xlRange[i, 9].ToString();
                                    currentGroundEvent.StartTime = start;
                                }

                                stop = CheckTimeCell(xlRange[i, 10]);
                                if (stop == "Unspecified")
                                {
                                    currentGroundEvent.StopTime = "Unspecified";
                                    currentGroundEvent.MilStopTime = "Unspecified";
                                }
                                else
                                {
                                    currentGroundEvent.MilStopTime = xlRange[i, 10].ToString();
                                    currentGroundEvent.StopTime = stop;
                                }
                                currentGroundEvent.Poc = CheckNullCell(xlRange[i, 15]);
                                currentGroundEvent.PocPhone = CheckNullCell(xlRange[i, 16]);
                                currentGroundEvent.PocEmail = CheckNullCell(xlRange[i, 17]);
                                currentGroundEvent.Latitude = xlRange[i, 21].ToString();
                                currentGroundEvent.Longitude = xlRange[i, 22].ToString();

                                GroundEventFunctions.CreateGroundEvent(currentGroundEvent);
                                CreatorFunctions.ChangeObjectColor("Place/" + currentGroundEvent.Id, (CustomUserInterface.ColorOptions)Enum.Parse(typeof(CustomUserInterface.ColorOptions), colorOption));
                                CommonData.CurrentEvents.Add(currentGroundEvent);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    errorCount++;
                    errorMes = errorMes + i.ToString() + "\n\r";

                }
            }
            if (errorCount != 0)
            {
                MessageBox.Show(errorMes);
            }
            WriteEventFile(CommonData.EventFileStr);

        }

        //Coverage functions
        public static string WriteCoverageDetails(string cdName)
        {
            string details = null;
            string fom = null;
            int fomType = CommonData.CoverageList[CommonData.CoverageIndex].FomType;
            switch (fomType)
            {
                case 0:
                    fom = "Total Coverage Time (%)";
                    break;
                case 1:
                    fom = "Revisit Time (sec)";
                    break;
                case 2:
                    fom = "Positional Navigational Accuracy (m)";
                    break;
                case 3:
                    fom = "Dilution of Precision (m)";
                    break;
                case 4:
                    fom = "N Asset Coverage";
                    break;
                case 5:
                    fom = "Simple Coverage";
                    break;
                case 6:
                    fom = "Age of Data (sec)";
                    break;
                case 7:
                    fom = "Custom";
                    break;
                default:
                    break;
            }
            IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
            double currentTime = animationRoot.CurrentTime;
            string time = CommonData.StkRoot.ConversionUtility.ConvertDate("EpSec", "UTCG", currentTime.ToString());

            List<double> limits = CoverageFunctions.GetFomLimits(cdName,time);
            details = details + "FOM Type: " + fom + "\r\n";
            details = details + "Time: " + time + " UTCG\r\n";
            details = details + "Average Minimum: " + limits[0].ToString() +"\r\n";
            details = details + "Average Maximum: " + limits[1].ToString() + "\r\n";
            details = details + "Standard Deviation: " + limits[2].ToString() + "\r\n";

            return details;
        }

        public static List<CoverageData> ReadCoverageData(string fileStr)
        {
            return DeserializeObject<List<CoverageData>>(fileStr);
        }

        public static List<string> ReadATGroup(string filepath)
        {
            List<string> atGroup = new List<string>();
            using (StreamReader reader = new StreamReader(filepath))
            {                
                string line = null;
                line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    atGroup.Add(line);
                    line = reader.ReadLine();
                }
            }
            return atGroup;
        }

        public static void WriteCoverageData(string fileStr)
        {
            SerializeObject(CommonData.CoverageList, fileStr);
        }


        //Read/Write preferences
        public static void ReadPrefs(string fileStr)
        {
            CommonData.Preferences = DeserializeObject<AppPreferences>(fileStr);
        }

        public static void WritePrefs(string fileStr)
        {
            SerializeObject(CommonData.Preferences, fileStr);
        }

        //Template functions
        public static List<string> FindTemplates(string dirPath)
        {
            List<string> tempNames = new List<string>();
            string[] subdirs = Directory.GetDirectories(dirPath);
            string[] delim = { "\\" };
            foreach (string item in subdirs)
            {
                string newStr = item.Split(delim,StringSplitOptions.None).Last();
                tempNames.Add(newStr);
            }

            return tempNames;
        } 

        public static TemplateScriptData GetTemplateScriptData(string filePath)
        {
            TemplateScriptData data = new TemplateScriptData();
            data = DeserializeObject<TemplateScriptData>(filePath);
            return data;
        }

        public static void WriteTemplateScriptData(string filePath, TemplateScriptData data)
        {
            SerializeObject(data, filePath);
        }

        public static IAgStkObject ImportObject(string filePath, bool eraseReplace)
        {
            IAgStkObjectCollection children = CommonData.StkRoot.CurrentScenario.Children;
            string line = filePath.Split('\\').Last();
            string[] ext = line.Split('.');
            string className = GetClassFromExtension("." + ext[1]);
            bool objExists = CommonData.StkRoot.ObjectExists(className + "/" + ext[0]);
            if (objExists)
            {
                IAgStkObject oldObj = CommonData.StkRoot.GetObjectFromPath(className + "/" + ext[0]);
                if (eraseReplace)
                {
                    oldObj.Unload();
                    IAgStkObject myObj = children.ImportObject(filePath);
                    return myObj;
                }
                else
                {
                    return oldObj;
                }
            }
            else
            {
                IAgStkObject myObj = children.ImportObject(filePath);
                return myObj;
            }
        }

        public static void ImportTemplate(string dirPath, List<string> objectNames, bool eraseReplace)
        {
            if (File.Exists(dirPath+"Order.txt"))
            {
                int errorNum = 0;
                string errorMes = "Could not load the following objects: \n";
                TemplateScriptData data = null;
                if (File.Exists(dirPath + "ScriptData.json"))
                {
                    data = GetTemplateScriptData(dirPath + "ScriptData.json");
                    if (data.PreImportScriptActive)
                    {
                        ExecuteScript(data.PreImportScriptPath, data.PreImportArgs, true);
                    }
                }
                CommonData.StkRoot.ExecuteCommand("BatchGraphics * On");
                using (StreamReader reader = new StreamReader(dirPath + "Order.txt"))
                {
                    string line = null;
                    string className = null;
                    bool objExists = false;
                    string objPath = null;
                    line = reader.ReadLine();
                    IAgStkObjectCollection children = CommonData.StkRoot.CurrentScenario.Children;
                    while ((line != null && line != ""))
                    {
                        string[] linepeices = line.Split('.');
                        if (objectNames.Contains(linepeices[0]))
                        {
                            className = GetClassFromExtension("." + linepeices[1]);
                            objExists = CommonData.StkRoot.ObjectExists(className + "/" + linepeices[0]);
                            if (eraseReplace)
                            {
                                if (objExists)
                                {
                                    try
                                    {
                                        IAgStkObject oldObj = CommonData.StkRoot.GetObjectFromPath(className + "/" + linepeices[0]);
                                        oldObj.Unload();
                                    }
                                    catch (Exception)
                                    {
                                        //No exception message needed
                                    }
                                }
                                try
                                {
                                    children.ImportObject(Path.Combine(dirPath, line));
                                }
                                catch (Exception e)
                                {
                                    errorMes = errorMes + linepeices[0] + " " + e.Message + "\n";
                                    errorNum++;
                                }
                            }
                            else
                            {
                                if (!objExists)
                                {
                                    try
                                    {
                                        children.ImportObject(Path.Combine(dirPath, line));
                                    }
                                    catch (Exception e)
                                    {
                                        errorMes = errorMes + linepeices[0] + " " + e.Message + "\n";
                                        errorNum++;
                                    }
                                }
                            }

                        }
                        line = reader.ReadLine();
                    }
                }
                CommonData.StkRoot.ExecuteCommand("BatchGraphics * Off");
                if (data != null)
                {
                    if (data.PostImportScriptActive)
                    {
                        ExecuteScript(data.PostImportScriptPath, data.PostImportArgs, false);
                    }
                }
                if (errorNum>0)
                {
                    MessageBox.Show(errorMes);
                }
            }
            else
            {
                MessageBox.Show("Template order file does not exist");
            }
        }

        public static void WriteTemplate(List<string> objectNames, string dirPath)
        {
            string fileStr = dirPath + "Order.txt";
            File.WriteAllText(fileStr, String.Empty);
            string fullText = null;
            IAgStkObject stkObject;
            foreach (var name in objectNames)
            {
                stkObject = CommonData.StkRoot.CurrentScenario.Children[name];
                string ext = GetExtension(stkObject);
                fullText = fullText + name + ext + "\n";
                stkObject.Export(Path.Combine(dirPath, name));
            }
            using (StreamWriter writer = new StreamWriter(@fileStr))
            {
                writer.WriteLine(fullText);
            }
        }

        public static List<string> GetTemplateObjectNames(string dirPath)
        {
            List<string> objectNames = new List<string>();
            if (File.Exists(dirPath + "Order.txt"))
            {
                using (StreamReader reader = new StreamReader(dirPath + "Order.txt"))
                {
                    string line = null;
                    line = reader.ReadLine();
                    while ((line != null && line != ""))
                    {
                        string[] linepeices = line.Split('.');
                        if (linepeices[0] != null && linepeices[0] != "")
                        {
                            objectNames.Add(linepeices[0]);
                        }
                        line = reader.ReadLine();
                    }
                }
            }
            else
            {
                MessageBox.Show("Template order file does not exist");
            }
            return objectNames;
        }

        //Plane Crossing Utility Functions
        public static void WritePlaneCrossingOutput(string satRefName, List<PlaneCrossingGroup> crossingGroups)
        {
            string filename = satRefName + "_PlaneCrossings_" + DateTime.Now.ToString().Replace("/","-").Replace(":",".");
            string filepath = Path.Combine(@CommonData.DirectoryStr, filename);

            string fullText = null;
            foreach (var group in crossingGroups)
            {
                fullText = fullText + "Orbit Plane Reference: " + group.PlaneReferenceObjectName + "\n";
                fullText = fullText + "Crossing Object: " + group.CrossingObjectName + "\n";
                if (group.PlaneCrossings[0].IsBounded)
                {
                    fullText = fullText + "Is Bounded: " + group.PlaneCrossings[0].IsBounded.ToString() + " Lower Bound: " + group.PlaneCrossings[0].LowerBound.ToString() + " deg UpperBound: " + group.PlaneCrossings[0].UpperBound.ToString() + " deg" + "\n";
                    fullText = fullText + "Crossing Time (UTCG) | Lower Bound Crossing Time (UTCG) | Upper Bound Crossing Time (UTCG)\n";
                    foreach (var crossing in group.PlaneCrossings)
                    {
                        fullText = fullText +crossing.CrossingTime + " " + crossing.LowerBoundCrossingTime + " " + crossing.UpperBoundCrossingTime +"\n";
                    }
                }
                else
                {
                    fullText = fullText + "Is Bounded: " + group.PlaneCrossings[0].IsBounded.ToString() + "\n";
                    fullText = fullText + "Crossing Time (UTCG) |\n";
                    foreach (var crossing in group.PlaneCrossings)
                    {
                        fullText = fullText + crossing.CrossingTime + "\n";
                    }
                }

            }

            using (StreamWriter writer = new StreamWriter(@filepath))
            {
                writer.WriteLine(fullText);
            }
        }

        //Helper functions
        public static string GetExtension(IAgStkObject stkObject)
        {
            string ext = null;
            string className = stkObject.ClassName;
            if (className=="Satellite")
            {
                ext = ".sa";
            }
            else if (className == "Missile")
            {
                ext = ".mi";
            }
            else if (className == "Aircraft")
            {
                ext = ".ac";
            }
            else if (className == "LaunchVehicle")
            {
                ext = ".lv";
            }
            else if (className == "GroundVehicle")
            {
                ext = ".gv";
            }
            else if (className == "Ship")
            {
                ext = ".sh";
            }
            else if (className == "CoverageDefinition")
            {
                ext = ".cv";
            }
            else if (className == "CommSystem")
            {
                ext = ".cs";
            }
            else if (className == "Chain")
            {
                ext = ".c";
            }
            else if (className == "Constellation")
            {
                ext = ".cn";
            }
            else if (className == "AdvCAT")
            {
                ext = ".ca";
            }
            else if (className == "Facility")
            {
                ext = ".f";
            }
            else if (className == "Place")
            {
                ext = ".plc";
            }
            else if (className == "Target")
            {
                ext = ".t";
            }
            else if (className == "Volumetric")
            {
                ext = ".vo";
            }
            else if (className == "AreaTarget")
            {
                ext = ".at";
            }
            else if (className == "MTO")
            {
                ext = ".mt";
            }
            return ext;
        }

        public static string GetClassFromExtension(string ext)
        {
            string className = null;
            if (ext == ".sa")
            {
                className = "Satellite";
            }
            else if (ext == ".mi")
            {
                className = "Missile";
            }
            else if (ext == ".ac")
            {
                className = "Aircraft";
            }
            else if (ext == ".lv")
            {
                className = "LaunchVehicle";
            }
            else if (ext == ".gv")
            {
                className = "GroundVehicle";
            }
            else if (ext == ".sh")
            {
                className = "Ship";
            }
            else if (ext == ".cv")
            {
                className = "CoverageDefinition";
            }
            else if (ext == ".cs")
            {
                className = "CommSystem";
            }
            else if (ext == ".c")
            {
                className = "Chain";
            }
            else if (ext == ".cn")
            {
                className = "Constellation";
            }
            else if (ext == ".ca")
            {
                className = "AdvCAT";
            }
            else if (ext == ".f")
            {
                className = "Facility";
            }
            else if (ext == ".plc")
            {
                className = "Place";
            }
            else if (ext == ".t")
            {
                className = "Target";
            }
            else if (ext == ".vo")
            {
                className = "Volumetric";
            }
            else if (ext == ".at")
            {
                className = "AreaTarget";               
            }
            else if (ext == ".mt")
            {
                className = "MTO";
            }
            return className;
        }

        public static string CheckNullCell(object input)
        {
            string output = null;
            if (input == null)
            {
                output = "Unknown";
            }
            else
            {
                output = input.ToString();
            }
            return output;

        }

        public static string CheckTimeCell(object input)
        {
            string output = null;
            if (input == null)
            {
                output = "Unspecified";
            }
            else
            {
                string inputTime = input.ToString();
                string convertedTime = GroundEventFunctions.ConvertMilTime(inputTime);
                if (convertedTime != null)
                {
                    output = convertedTime;
                }
                else
                {
                    output = "Unspecified";
                }

            }

            return output;
        }

        //Execute external program or script using new process
        public static void ExecuteScript(string cmdStr, string args, bool executeSerial)
        {
            try
            {
                //Process newProcess = new Process();
                if (!String.IsNullOrEmpty(args))
                {
                    if (executeSerial)
                    {
                        System.Diagnostics.Process.Start(cmdStr, args).WaitForExit();
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(cmdStr, args);
                    }
                }
                else
                {
                    if (executeSerial)
                    {
                        System.Diagnostics.Process.Start(cmdStr).WaitForExit();
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(cmdStr);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Script Error");
            }
        }



    }
}
