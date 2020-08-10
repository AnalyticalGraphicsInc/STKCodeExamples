using System;
using System.Collections.Generic;
using System.Linq;
using AGI.STKObjects;
using AGI.STKUtil;

namespace OperatorsToolbox.Coverage
{
    public static class CoverageFunctions
    {
        public static void DefineCoverage(string cdName, string oaName,string pointGran)
        {
            try
            {
                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + cdName + " Grid AreaOfInterest Custom AreaTarget AreaTarget/" + oaName);
                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + cdName + " Grid PointGranularity LatLon " + pointGran);
                CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + cdName + " Static Points Off");
                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + cdName + " Access Clear");
                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + cdName + " Access AutoRecompute Off");
            }
            catch (Exception)
            {

            }

        }

        public static void DefineCoverage(string cdName, string pointGran)
        {
            try
            {
                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + cdName + " Grid PointGranularity LatLon " + pointGran);
                CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + cdName + " Static Points Off");
                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + cdName + " Access Clear");
                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + cdName + " Access AutoRecompute Off");
            }
            catch (Exception)
            {

            }

        }

        public static void DefineGlobalCoverage(string cdName, string pointGran)
        {
            try
            {
                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + cdName + " Grid AreaOfInterest Global");
                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + cdName + " Grid PointGranularity LatLon " + pointGran);
                CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + cdName + " Static Points Off");
            }
            catch (Exception)
            {

            }
        }

        public static void AssignAsset(string className, string cdName, string objName)
        {
            try
            {
                string nAsset = objName.Replace(' ', '_');
                string cmd = "Cov */CoverageDefinition/" + cdName + " Asset */" + className + "/" + nAsset + " Assign";
                CommonData.StkRoot.ExecuteCommand(cmd);
                cmd = "Cov */CoverageDefinition/" + cdName + " Asset */" + className + "/" + nAsset + " Activate";
                CommonData.StkRoot.ExecuteCommand(cmd);
            }
            catch (Exception)
            {

            }
        }

        public static void DefineEllipseAt(IAgAreaTarget at, double centerLat, double centerLong, double majorAxis, double minorAxis)
        {
            at.AreaType = AgEAreaType.eEllipse;
            AgAreaTypeEllipse ellipseData = (AgAreaTypeEllipse)at.AreaTypeData;
            at.Position.AssignGeodetic(centerLat, centerLong, 0);
            ellipseData.SemiMajorAxis = majorAxis;
            ellipseData.SemiMinorAxis = minorAxis;           
        }

        public static void DefineBoxAt(IAgAreaTarget at, double centerLat, double centerLong, double boundSize)
        {
            at.AreaType = AgEAreaType.ePattern;
            IAgAreaTypePatternCollection pattern = (IAgAreaTypePatternCollection)at.AreaTypeData;
            pattern.RemoveAll();
            pattern.Add(centerLat + boundSize, centerLong + boundSize);
            pattern.Add(centerLat + boundSize, centerLong - boundSize);
            pattern.Add(centerLat - boundSize, centerLong - boundSize);
            pattern.Add(centerLat - boundSize, centerLong + boundSize);
        }

        public static void SetGridConstraint(IAgCoverageDefinition cov, string className, string objectPath)
        {
            //Grid Seed can only be applied once for some reason so try statement is used in case coverage is in edit mode
            SetGridClass(cov, className);
            try
            {
                cov.PointDefinition.UseGridSeed = true;
            }
            catch (Exception)
            {

            }
            cov.PointDefinition.SeedInstance = objectPath;
        }

        public static void SetGridClass(IAgCoverageDefinition cov, string className)
        {
            if (className =="Facility")
            {
                cov.PointDefinition.GridClass = AgECvGridClass.eGridClassFacility;
            }
            else if (className == "Place")
            {
                cov.PointDefinition.GridClass = AgECvGridClass.eGridClassPlace;
            }
            else if (className == "Target")
            {
                cov.PointDefinition.GridClass = AgECvGridClass.eGridClassTarget;
            }
            else if (className == "Aircraft")
            {
                cov.PointDefinition.GridClass = AgECvGridClass.eGridClassAircraft;
            }
            else if (className == "Missile")
            {

            }
            else if (className == "Sensor")
            {
                cov.PointDefinition.GridClass = AgECvGridClass.eGridClassSensor;
            }
            else if (className == "Transmitter")
            {
                cov.PointDefinition.GridClass = AgECvGridClass.eGridClassTransmitter;
            }
            else if (className == "Receiver")
            {
                cov.PointDefinition.GridClass = AgECvGridClass.eGridClassReceiver;
            }
        }

        public static List<double> GetFomLimits(string oaName)
        {
            List<double> limits = new List<double>();

            double maxDouble = -99999;
            double minDouble = -99999;
            double stdDev = -99999;
            try
            {
                IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
                double currentTime = animationRoot.CurrentTime;
                string time = CommonData.StkRoot.ConversionUtility.ConvertDate("EpSec", "UTCG", currentTime.ToString());

                string name = "CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM";
                IAgStkObject fom = CommonData.StkRoot.GetObjectFromPath(name);
                IAgDataPrvFixed fomDp = fom.DataProviders["Overall Value"] as IAgDataPrvFixed;
                IAgDrResult result = fomDp.Exec();
                Array minLimit = result.DataSets.GetDataSetByName("Minimum").GetValues();
                minDouble = Double.Parse(minLimit.GetValue(0).ToString());

                Array maxLimit = result.DataSets.GetDataSetByName("Maximum").GetValues();
                maxDouble = Double.Parse(maxLimit.GetValue(0).ToString());

                Array std = result.DataSets.GetDataSetByName("Standard Deviation").GetValues();
                stdDev = Double.Parse(std.GetValue(0).ToString());
            }
            catch (Exception)
            {

            }
            limits.Add(Math.Round(minDouble,3));
            limits.Add(Math.Round(maxDouble,3));
            limits.Add(Math.Round(stdDev, 2));
            return limits;
        }

        public static List<double> GetFomLimits(string oaName, string time)
        {
            List<double> limits = new List<double>();

            double maxDouble = -99999;
            double minDouble = -99999;
            double stdDev = -99999;
            try
            {
                string name = "CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM";
                IAgStkObject fom = CommonData.StkRoot.GetObjectFromPath(name);
                IAgDataPrvTimeVar fomDp = fom.DataProviders["Overall Value by Time"] as IAgDataPrvTimeVar;
                IAgDrResult result = fomDp.ExecSingle(time);
                Array minLimit = result.DataSets.GetDataSetByName("Minimum").GetValues();
                minDouble = Double.Parse(minLimit.GetValue(0).ToString());

                Array maxLimit = result.DataSets.GetDataSetByName("Maximum").GetValues();
                maxDouble = Double.Parse(maxLimit.GetValue(0).ToString());

                Array std = result.DataSets.GetDataSetByName("Standard Deviation").GetValues();
                stdDev = Double.Parse(std.GetValue(0).ToString());
            }
            catch (Exception)
            {

            }
            limits.Add(Math.Round(minDouble, 3));
            limits.Add(Math.Round(maxDouble, 3));
            limits.Add(Math.Round(stdDev, 2));
            return limits;
        }

        public static void RemoveFomLegends()
        {
            //Remove all legends for previous coverage definitions that were computed 
            IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class CoverageDefinition");
            string[] constArray = result[0].Split(null);
            foreach (var item in constArray)
            {
                string covName = item.Split('/').Last();
                try
                {
                    CommonData.StkRoot.ExecuteCommand("VO */CoverageDefinition/" + covName + "/FigureOfMerit/" + covName + "_FOM FOMAttributes StaticLegendShow Off");
                }
                catch (Exception)
                {
                }
                try
                {
                    CommonData.StkRoot.ExecuteCommand("VO */CoverageDefinition/" + covName + "/FigureOfMerit/" + covName + "_FOM FOMAttributes DynamicLegendShow Off");
                }
                catch (Exception)
                {
                }
            }
        }

        public static void ShowLegend()
        {
            try
            {
                int index = CommonData.CoverageList[CommonData.CoverageIndex].FomType;
                string oaName = CommonData.CoverageList[CommonData.CoverageIndex].CdName;

                if (index == 0 || index == 5)
                {
                    IAgFigureOfMerit fom = CommonData.StkRoot.GetObjectFromPath("CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM") as IAgFigureOfMerit;
                    fom.Graphics.Static.IsVisible = true;
                    if (index != 5)
                    {
                        CommonData.StkRoot.ExecuteCommand("VO */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMAttributes StaticLegendShow On");
                        CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM Legend Static Title " + "\"" + "Coverage Time (% of Coverage Interval)" + "\"");
                    }
                }
                else
                {
                    CommonData.StkRoot.ExecuteCommand("VO */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMAttributes DynamicLegendShow On");
                    IAgFigureOfMerit fom = CommonData.StkRoot.GetObjectFromPath("CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM") as IAgFigureOfMerit;
                    fom.Graphics.Static.IsVisible = false;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
