using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using OperatorsToolbox.SatelliteCreator;
using OperatorsToolbox.SmartView;
using OperatorsToolbox.VolumeCreator;

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
                    Formatting = Formatting.Indented
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
            //File.WriteAllText(fileStr, String.Empty);
            //string fullText = null;
            //string section = null;
            //foreach (var item in CommonData.SavedViewList)
            //{
            //    if (item.ViewObjectData!=null &&item.ObjectHideShow)
            //    {
            //        section = CreateObjectDataSection(item);
            //        fullText = fullText + section;
            //    }
            //}

            //using (StreamWriter writer = new StreamWriter(@fileStr))
            //{
            //    writer.WriteLine(fullText);

            //}

        }

        //Read satellite catalog 
        public static void ReadSatCat()
        {
            CommonData.SatCatItemList.Clear();
            CommonData.SatCatFofo.Clear();
            CommonData.SatCatConstellations.Clear();
            CommonData.SatCatNations.Clear();
            Excel.Workbook myBook = null;
            Excel.Application myApp = null;
            Excel.Worksheet mySheet = null;

            myApp = new Excel.Application();
            myApp.Visible = false;
            try
            {
                myBook = myApp.Workbooks.Open(CommonData.Preferences.SatCatLocation);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not open satellite catalog");
                return;
            }
            mySheet = (Excel.Worksheet)myBook.Sheets[1];
            Excel.Range range = mySheet.UsedRange;
            object[,] xlRange = range.Value2;
            var commonName = xlRange[2, 3];
            var otherName = xlRange[2, 4];
            var constellation = xlRange[2, 5];
            var nation = xlRange[2, 6];
            var fofo = xlRange[2, 7];
            var type = xlRange[2, 8];
            var fov = xlRange[2, 11];
            int rows = range.Rows.Count;

            for (int i = 2; i < rows; i++)
            {
                SatCatItem item = new SatCatItem();
                item.Ssc = xlRange[i, 2].ToString();
                commonName = xlRange[i, 3];
                otherName = xlRange[i, 4];
                constellation = xlRange[i, 5];
                nation = xlRange[i, 6];
                fofo = xlRange[i, 7];
                type = xlRange[i, 8];
                fov = xlRange[i, 11];
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
                if (constellation != null)
                {
                    item.Constellation = constellation.ToString();
                    if (!CommonData.SatCatConstellations.Contains(item.Constellation))
                    {
                        CommonData.SatCatConstellations.Add(item.Constellation);
                    }
                }
                else
                {
                    item.Constellation = "Unspecified";
                    if (!CommonData.SatCatConstellations.Contains("Unspecified"))
                    {
                        CommonData.SatCatConstellations.Add("Unspecified");
                    }
                }

                if (nation != null)
                {
                    item.Nation = nation.ToString();
                    if (!CommonData.SatCatNations.Contains(item.Nation))
                    {
                        CommonData.SatCatNations.Add(item.Nation);
                    }
                }
                else
                {
                    item.Nation = "Unspecified";
                    if (!CommonData.SatCatNations.Contains("Unspecified"))
                    {
                        CommonData.SatCatNations.Add("Unspecified");
                    }
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

                if (type != null)
                {
                    item.Type = type.ToString();
                    if (!CommonData.SatCatTypes.Contains(item.Type))
                    {
                        CommonData.SatCatTypes.Add(item.Type);
                    }
                }
                else
                {
                    item.Type = "Unspecified";
                    if (!CommonData.SatCatTypes.Contains("Unspecified"))
                    {
                        CommonData.SatCatTypes.Add("Unspecified");
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
                CommonData.SatCatConstellations.Sort();
                CommonData.SatCatFofo.Sort();
                CommonData.SatCatNations.Sort();
                CommonData.SatCatTypes.Sort();
            }
            myBook.Close();
            myApp.Quit();


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
                    SerializeObject(CommonData.Cadences, dbFileStr, true);
                else
                    SerializeObject(CommonData.Cadences, localFileStr, true);
        }

        public static List<SensorCadance> ReadCadences(string fileStr)
        {
            return DeserializeObject<List<SensorCadance>>(fileStr);
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
            if (CommonData.SelectedIndex == -1) return;
                CommonData.CurrentEvents.RemoveAt(CommonData.SelectedIndex);
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

        public static void ImportEventSheet(string fileStr, int importOption)
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

        public static void ImportTemplate(string dirPath, List<string> objectNames)
        {
            if (File.Exists(dirPath+"Order.txt"))
            {
                int errorNum = 0;
                string errorMes = "Could not load the following objects: \n";
                CommonData.StkRoot.ExecuteCommand("BatchGraphics * On");
                using (StreamReader reader = new StreamReader(dirPath + "Order.txt"))
                {
                    string line = null;
                    string objPath = null;
                    line = reader.ReadLine();
                    IAgStkObjectCollection children = CommonData.StkRoot.CurrentScenario.Children;                  
                    while ((line != null && line != ""))
                    {
                        string[] linepeices = line.Split('.');
                        if (objectNames.Contains(linepeices[0]))
                        {
                            try
                            {
                                children.ImportObject(Path.Combine(dirPath,line));
                            }
                            catch (Exception e)
                            {
                                errorMes = errorMes + linepeices[0] + " " + e.Message + "\n";
                                errorNum++;
                            }
                        }
                        line = reader.ReadLine();
                    }
                }
                CommonData.StkRoot.ExecuteCommand("BatchGraphics * Off");
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
            return ext;
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

    }
}
