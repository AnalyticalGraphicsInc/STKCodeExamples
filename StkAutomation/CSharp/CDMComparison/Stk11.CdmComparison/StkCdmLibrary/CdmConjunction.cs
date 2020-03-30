using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using System.Drawing;
using AGI.STKVgt;
using AGI.STKObjects;
using System.IO;
using System.Text.RegularExpressions;

namespace StkCdmLibrary
{
    public class CdmConjunction
    {
        private double _stkRangeAtCdmTca = -1;
        private double _stkMinimumRange = -1;
        private string _stkTimeOfMinRange;

        public string ReferenceFrame;
        public string CreationDate, TCA, ID, ProbabilityMethod;
        public double MissDistance, RelativeSpeed;
        public double Probability;
        public double SigmaDilution;
        public double RelPosR, RelPosT, RelPosN, RelVelR, RelVelT, RelVelN;
        public CdmSatellite Primary;
        public CdmSatellite Secondary;
        public string CdmFilePath;

        public CdmConjunction()
        {

        }

        public CdmConjunction(CdmConjunction rhs)
        {
            this.ReferenceFrame = rhs.ReferenceFrame;
            this.CreationDate = rhs.CreationDate;
            this.TCA = rhs.TCA;
            this.ID = rhs.ID;
            this.ProbabilityMethod = rhs.ProbabilityMethod;
            this.MissDistance = rhs.MissDistance;
            this.RelativeSpeed = rhs.RelativeSpeed;
            this.Probability = rhs.Probability;
            this.SigmaDilution = rhs.SigmaDilution;
            this.RelPosR = rhs.RelPosR;
            this.RelPosT = rhs.RelPosT;
            this.RelPosN = rhs.RelPosN;
            this.RelVelR = rhs.RelVelR;
            this.RelVelT = rhs.RelVelT;
            this.RelVelN = rhs.RelVelN;
            this.Primary = new CdmSatellite(rhs.Primary);
            this.Secondary = new CdmSatellite(rhs.Secondary);

        }

        public static void BinConjunctionsByRange(string directoryPath, CdmConjunction[] conjunctions)
        {
            
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string notCountableDir = Path.Combine(directoryPath, "NotCountable_Beyond_50");
            string countableDir = Path.Combine(directoryPath, "Countable_Under_50");
            string reportableDir = Path.Combine(directoryPath, "Reportable_Under_15");
            string watchDir = Path.Combine(directoryPath, "Watch_Under_2");

            string watchSummary = "", reportableSummary = "", countableSummary = "";

            int notCountableCount = 0, countableCount = 0, reportableCount = 0, watchCount = 0;
            foreach (CdmConjunction cdm in conjunctions)
            {
                if (cdm.MissDistance < 2000)
                {
                    if (!Directory.Exists(watchDir))
                    {
                        Directory.CreateDirectory(watchDir);
                    }
                    File.Copy(cdm.CdmFilePath, Path.Combine(watchDir, Path.GetFileName(cdm.CdmFilePath)));
                    ++watchCount;
                    watchSummary += Environment.NewLine + String.Format("Watch conjunction #{0}: Object {1} ({2}) shows a close approach distance of {3} km at {4} UTC.", watchCount, cdm.Secondary.SSC, cdm.Secondary.SatName, (cdm.MissDistance / 1000.0), cdm.TCA.Replace('T', ' '));
                    //watchSummary += Environment.NewLine + string.Join(",", new string[] { cdm.Primary.SSC, cdm.Primary.SatName, cdm.Secondary.SSC, cdm.Secondary.SatName, cdm.MissDistance.ToString(), cdm.TCA });
                }
                else if (cdm.MissDistance < 15000)
                {
                    if (!Directory.Exists(reportableDir))
                    {
                        Directory.CreateDirectory(reportableDir);
                    }
                    File.Copy(cdm.CdmFilePath, Path.Combine(reportableDir, Path.GetFileName(cdm.CdmFilePath)));
                    ++reportableCount;
                    reportableSummary += Environment.NewLine + String.Format("Reportable conjunction #{0}: Object {1} ({2}) shows a close approach distance of {3} km at {4} UTC.", reportableCount, cdm.Secondary.SSC, cdm.Secondary.SatName, (cdm.MissDistance / 1000.0), cdm.TCA.Replace('T', ' '));
                    //reportableSummary += Environment.NewLine + string.Join(",", new string[] { cdm.Primary.SSC, cdm.Primary.SatName, cdm.Secondary.SSC, cdm.Secondary.SatName, cdm.MissDistance.ToString(), cdm.TCA });
                    
                }
                else if (cdm.MissDistance < 50000)
                {
                    if (!Directory.Exists(countableDir))
                    {
                        Directory.CreateDirectory(countableDir);
                    }

                    File.Copy(cdm.CdmFilePath, Path.Combine(countableDir, Path.GetFileName(cdm.CdmFilePath)));
                    ++countableCount;
                    countableSummary += Environment.NewLine + String.Format("Countable conjunction #{0}: Object {1} ({2}) shows a close approach distance of {3} km at {4} UTC.", countableCount, cdm.Secondary.SSC, cdm.Secondary.SatName, (cdm.MissDistance / 1000.0), cdm.TCA.Replace('T', ' '));
                    //countableSummary += Environment.NewLine + string.Join(",", new string[] { cdm.Primary.SSC, cdm.Primary.SatName, cdm.Secondary.SSC, cdm.Secondary.SatName, cdm.MissDistance.ToString(), cdm.TCA });
                    
                }
                else
                {
                    if (!Directory.Exists(notCountableDir))
                    {
                        Directory.CreateDirectory(notCountableDir);
                    }

                    File.Copy(cdm.CdmFilePath, Path.Combine(notCountableDir, Path.GetFileName(cdm.CdmFilePath)));
                    ++notCountableCount;
                }
            }

            File.WriteAllText(Path.Combine(directoryPath, "CDM_Summary.txt"), "CDM Summary " + DateTime.Now.ToUniversalTime().ToString() 
                + Environment.NewLine + "Under 2 km: " + watchCount.ToString() + Environment.NewLine + watchSummary 
                + Environment.NewLine + "Under 15 km: " + reportableCount.ToString() + Environment.NewLine + reportableSummary 
                + Environment.NewLine + "Under 50 km: " + countableCount.ToString() + Environment.NewLine + countableSummary );



        }

        private static bool WithinTimeframe(string timeISOYMD, string windowCenterISOYMD, double secondsFromCenter)
        {
            DateTime time = StkAssistant.ParseISOYMD(timeISOYMD);
            DateTime windowCenter = StkAssistant.ParseISOYMD(timeISOYMD);
            bool value = WithinTimeframe(time, windowCenter, secondsFromCenter);
            return value;
        }

        private static bool WithinTimeframe(DateTime time, DateTime windowCenter, double secondsFromCenter)
        {
            return Math.Abs(windowCenter.Subtract(time).TotalSeconds) < secondsFromCenter;
        }

        private static string AverageTime(params string[] timesISOYMD)
        {
            DateTime[] dateTimes = timesISOYMD.Select(t => StkAssistant.ParseISOYMD(t)).ToArray();
            DateTime avgTime = AverageTime(dateTimes);
            string isoYMD = avgTime.Year.ToString() + "-" + avgTime.Month.ToString("D2") + "-" + avgTime.Day.ToString("D2") + "T" + avgTime.ToLongTimeString();

            return isoYMD;
        }
        private static DateTime AverageTime(params DateTime[] dateTimes)
        {
            double value = 0;
            foreach (DateTime time in dateTimes)
            {
                value += time.ToOADate();
            }

            double avg = value / dateTimes.Length;

            return DateTime.FromOADate(avg);
        }

        private struct CdmBin
        {
            public string Name
            {
                get
                {
                    return MeanTCA + "_" + PrimarySSC + "_" + SecondarySSC;
                }
            }
            public string MeanTCA
            {
                get
                {
                    return AverageTime(Conjunctions.Select(t => t.TCA).ToArray());
                }
            }
            
            public string PrimarySSC;
            public string SecondarySSC;

            public List<CdmConjunction> Conjunctions;
                        

            public CdmBin(CdmConjunction conjunction)
            {
                Conjunctions = new List<CdmConjunction>();
                Conjunctions.Add(conjunction);
                PrimarySSC = conjunction.Primary.SSC;
                SecondarySSC = conjunction.Secondary.SSC;
            }
        }
        public static void BinConjunctions(string directoryPath, CdmConjunction[] conjunctions)
        {
            //string key is "PrimarySSC SecondarySSC AvgTCAISOYMD"
            //example;
            //12345 67890 2015-03-22T20:02:33.3333
            List<CdmBin> sortedConjunctions = new List<CdmBin>();

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (CdmConjunction cdm in conjunctions)
            {
                bool foundBin = false;
                foreach (CdmBin bin in sortedConjunctions)
                {
                    if ((cdm.Primary.SSC == bin.PrimarySSC || cdm.Primary.SSC == bin.SecondarySSC)
                        && (cdm.Secondary.SSC == bin.PrimarySSC || cdm.Secondary.SSC == bin.SecondarySSC)
                        && WithinTimeframe(cdm.TCA, bin.MeanTCA, 1800))
                    {
                        bin.Conjunctions.Add(cdm);
                        foundBin = true;
                        break;
                    }
                }

                if (!foundBin)
                {
                    sortedConjunctions.Add(new CdmBin(cdm));
                }
            }

            foreach (CdmBin item in sortedConjunctions.Where(a => a.Conjunctions.Count >= 2))
            {
                string safeName = Regex.Replace(item.Name, "[///(/)" + new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + " ]", "");
                string safePath = Path.Combine(directoryPath, safeName);
                if (!Directory.Exists(safePath))
                {
                    Directory.CreateDirectory(safePath);                    
                }
                foreach (CdmConjunction cdm in item.Conjunctions)
                {
                    File.Copy(cdm.CdmFilePath, Path.Combine(safePath, Path.GetFileName(cdm.CdmFilePath)));
                }
            }

        }

        public void GenerateStkCdmObjects()
        {
            StkAssistant.SetAnalysisIntervalFromTCA(this.Primary.EpochISOYMD);
            this.Primary.CreateCdmSatellite(true);
            this.Secondary.CreateCdmSatellite();
        }

        public double StkRangeAtCdmTca
        {
            get
            {
                if (!string.IsNullOrEmpty(Primary.StkCdmSatellitePath)
                    && !string.IsNullOrEmpty(Secondary.StkCdmSatellitePath)
                    && !string.IsNullOrEmpty(this.TCA)
                    && _stkRangeAtCdmTca == -1)
                {
                    double[] ric = StateCompare.GetRICDifferenceAtTCA(Primary.StkCdmSatellitePath, Secondary.StkCdmSatellitePath, this.TCA);
                    _stkRangeAtCdmTca = Math.Sqrt(ric[0] * ric[0] + ric[1] * ric[1] + ric[2] * ric[2]);
                }

                return _stkRangeAtCdmTca;
            }
        }

        public double StkMinimumRange
        {
            get
            {
                if (_stkMinimumRange == -1
                    && !string.IsNullOrEmpty(Primary.StkCdmSatelliteName)
                    && !string.IsNullOrEmpty(Secondary.StkCdmSatelliteName))
                {
                    GetStkTCA();
                }

                return _stkMinimumRange;
            }
        }

        public string StkTimeOfMinRange
        {
            get
            {
                if (string.IsNullOrEmpty(_stkTimeOfMinRange)
                    && !string.IsNullOrEmpty(Primary.StkCdmSatelliteName)
                    && !string.IsNullOrEmpty(Secondary.StkCdmSatelliteName))
                {
                    GetStkTCA();
                }
                return _stkTimeOfMinRange;
            }
        }

        private void GetStkTCA()
        {
            IAgCrdnVector diff = StateCompare.GetVectorBetweenObjects("*/Satellite/" + Primary.StkCdmSatelliteName, "*/Satellite/" + Primary.StkCdmSatelliteName, "*/Satellite/" + Secondary.StkCdmSatelliteName);
            object[] diffMin = StateCompare.GetTimeOfMinAndValue(diff);
            _stkTimeOfMinRange = (string)diffMin.GetValue(0);
            _stkMinimumRange = ((double)diffMin.GetValue(1));
        }

        public void EstimateProbability()
        {
            StkAssistant.SetAnalysisIntervalFromTCA(this.Primary.EpochISOYMD);
            string primaryPath = Primary.BaselineObjectPath;
            string primaryType = Primary.BaselineObjectType;
            string primaryName = primaryPath.Substring(primaryPath.LastIndexOf("/") + 1);
            string secondaryPath = Secondary.BaselineObjectPath;
            string secondaryName = secondaryPath.Substring(secondaryPath.LastIndexOf("/") + 1);

            string catName = "CAT_" + primaryName;

            int counter = 0;
            while (StkAssistant.Root.CurrentScenario.Children.Contains(AgESTKObjectType.eAdvCat, catName))
            {
                catName = "CAT_" + primaryName + "_" + (++counter).ToString();
            }

            IAgStkObject cat = StkAssistant.Root.CurrentScenario.Children.New(AgESTKObjectType.eAdvCat, catName);

            string cmdResult;
            StkAssistant.TryConnect("Save / " + primaryPath, out cmdResult);
            StkAssistant.TryConnect("Save / " + secondaryPath, out cmdResult);

            StkAssistant.TryConnect("ACAT */AdvCAT/" + catName + " TimePeriod \"" + StkAssistant.Scenario.StartTime + "\" \"" + StkAssistant.Scenario.StopTime + "\"", out cmdResult);


            StkAssistant.TryConnect("ACAT */AdvCAT/" + catName + " Primary Add \"" + primaryName + ".sa\" Cov", out cmdResult);
            StkAssistant.TryConnect("ACAT */AdvCAT/" + catName + " Secondary Add \"" + secondaryName + ".sa\" Cov", out cmdResult);

            StkAssistant.TryConnect("ACAT */AdvCAT/" + catName + " Compute ShowProgress On", out cmdResult);


            StkAssistant.TryConnect("VO */AdvCAT/" + catName + " AdvCATAttributes ShowSecondary On ShowAllSecondary On", out cmdResult);


            //StkAssistant.TryConnect("ReportCreate */AdvCAT/" + catName + " Type Display Style \"Close Approach By Min Range\"", out cmdResult);
            StkAssistant.TryConnect("ACATEvents_RM */AdvCAT/" + catName, out cmdResult);
            string catTCA;
            if (!cmdResult.Equals("No events found."))
            {
                catTCA = cmdResult.Split(',')[5];

                IAgStkObject catObject = StkAssistant.Root.GetObjectFromPath("*/AdvCAT/" + catName);
                IAgDataPrvInterval catDP = cat.DataProviders["Events by Min Range"] as IAgDataPrvInterval;
                IAgDrResult catResult = catDP.Exec(StkAssistant.Scenario.StartTime, StkAssistant.Scenario.StopTime);

                double catMaxProb = (double)catResult.DataSets.GetDataSetByName("Max Collision Probability").GetValues().GetValue(0);
                double catSigmaDilution = (double)catResult.DataSets.GetDataSetByName("Sigma Dilution Threshold").GetValues().GetValue(0);

                if (Probability == 0) Probability = catMaxProb;
                if (SigmaDilution == 0) SigmaDilution = Math.Round(catSigmaDilution, 3);
            }

            cat.Unload();
            
        }

        public GraphPane GraphConjunction(Graphics g)
        {
            StateCompare.RICResults ricOverTime = StateCompare.GetRICDifferenceOverTime("*/Satellite/" + this.Primary.StkCdmSatelliteName, "*/Satellite/" + this.Secondary.StkCdmSatelliteName, this.TCA);

            TextObj tcaComment = ZedGraphAssistant.CreateGraphLabel("TCA : " + this.TCA, .01f, .01f, Color.Black);
            TextObj missComment = ZedGraphAssistant.CreateGraphLabel("CDM Range at TCA (m): " + this.MissDistance.ToString("0.##"), .01f, .05f, Color.Black);
            TextObj createTimeComment = ZedGraphAssistant.CreateGraphLabel("CDM Creation Time: " + this.CreationDate, 1f, .01f, AlignH.Right, AlignV.Top, Color.Black);
            TextObj cdmIDComment = ZedGraphAssistant.CreateGraphLabel("CDM ID: " + this.ID, 1f, .05f, AlignH.Right, AlignV.Top, Color.Black);

            PointPairList pplR = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.R);
            PointPairList pplI = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.I);
            PointPairList pplC = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.C);
            PointPairList pplRange = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.Range);

            PointPairList[] ppl = new PointPairList[] { pplR, pplI, pplC, pplRange };
            string[] pplNames = new string[] { "Radial", "In-Track", "Cross-Track", "Range" };
            TextObj[] comments = new TextObj[] { tcaComment, missComment, createTimeComment, cdmIDComment };

            return ZedGraphAssistant.CreateGraph("Conjunction : " + Primary.SatName + " & " + Secondary.SatName, TCA, g, ppl, pplNames, comments);
        }

        public string[] SummaryOfConjunction
        {
            get
            {
                List<string> summary = new List<string>();
                summary.Add("Conjunction for " + Primary.SSC + "/" + Primary.SatName + "[+] and " + Secondary.SSC + "/" + Secondary.SatName + "[-]");
                summary.Add("CDM Creation Date    : " + this.CreationDate);
                summary.Add("CDM TCA              : " + this.TCA);
                summary.Add("CDM Min Range        : " + this.MissDistance.ToString("0.##") + " (m)");
                summary.Add("STK TCA              : " + this.StkTimeOfMinRange);
                summary.Add("STK Min Range        : " + this.StkMinimumRange.ToString("0.##") + " (m)");
                summary.Add("STK Range at CDM TCA : " + this.StkRangeAtCdmTca.ToString("0.##") + " (m)");

                return summary.ToArray();
            }
        }

        public string[] SummaryOfCdmTleComparison()
        {
            if (Primary.StkTleSatellitePath != null
                && Primary.StkCdmSatellitePath != null
                && Secondary.StkCdmSatellitePath != null
                && Secondary.StkTleSatellitePath != null)
            {
                object[] cdmCdmtca = StateCompare.GetTcaRangeInfo(this.Primary.StkCdmSatellitePath, this.Secondary.StkCdmSatellitePath);
                object[] cdmTletca = StateCompare.GetTcaRangeInfo(this.Primary.StkCdmSatellitePath, this.Secondary.StkTleSatellitePath);
                object[] tleCdmtca = StateCompare.GetTcaRangeInfo(this.Primary.StkTleSatellitePath, this.Secondary.StkCdmSatellitePath);
                object[] tleTletca = StateCompare.GetTcaRangeInfo(this.Primary.StkTleSatellitePath, this.Secondary.StkTleSatellitePath);

                List<string> result = new List<string>();
                result.Add("Primary CDM to Secondary CDM : TCA " + cdmCdmtca[0].ToString() + " - Range (m) " + ((double)cdmCdmtca[1]).ToString("0.##"));
                result.Add("Primary CDM to Secondary TLE : TCA " + cdmTletca[0].ToString() + " - Range (m) " + ((double)cdmTletca[1]).ToString("0.##"));
                result.Add("Primary TLE to Secondary CDM : TCA " + tleCdmtca[0].ToString() + " - Range (m) " + ((double)tleCdmtca[1]).ToString("0.##"));
                result.Add("Primary TLE to Secondary TLE : TCA " + tleTletca[0].ToString() + " - Range (m) " + ((double)tleTletca[1]).ToString("0.##"));

                return result.ToArray();
            }
            return null;
        }

        public string[] SummaryOfConjunctionsFromSecondaryStates()
        {
            bool tlePSuccess = this.Primary.StkTleSatellitePath != null;
            bool cdmPSuccess = this.Primary.StkCdmSatellitePath != null;
            bool ephPSuccess = this.Primary.StkEphSatellitePath != null;

            string primary = ephPSuccess ? this.Primary.StkEphSatellitePath : cdmPSuccess ? this.Primary.StkCdmSatellitePath : this.Primary.StkTleSatellitePath;

            bool tleSSuccess = this.Secondary.StkTleSatellitePath != null;
            bool cdmSSuccess = this.Secondary.StkCdmSatellitePath != null;
            bool ephSSuccess = this.Secondary.StkEphSatellitePath != null;

            List<string> secondaries = new List<string>();
            if (ephSSuccess) secondaries.Add(this.Secondary.StkEphSatellitePath);
            if (tleSSuccess) secondaries.Add(this.Secondary.StkTleSatellitePath);
            if (cdmSSuccess) secondaries.Add(this.Secondary.StkCdmSatellitePath);

            string primaryType = primary.Equals(this.Primary.StkEphSatellitePath) ? "Ephemeris" : primary.Equals(this.Primary.StkTleSatellitePath) ? "TLE" : primary.Equals(this.Primary.StkCdmSatellitePath) ? "CDM" : null;
            List<string> result = new List<string>();
            if (primary != null && secondaries.Count > 0)
            {
                string[] pplNames = secondaries.Select(s =>
                s.Equals(this.Secondary.StkEphSatellitePath) ? "Ephemeris"
                : s.Equals(this.Secondary.StkTleSatellitePath) ? "TLE"
                : s.Equals(this.Secondary.StkCdmSatellitePath) ? "CDM" : null).ToArray();
                for (int i = 0; i < secondaries.Count; i++)
                {
                    string second = secondaries[i];
                    object[] primaryToSecondary = StateCompare.GetTcaRangeInfo(primary, second);
                    result.Add("Primary " + primaryType + " to Secondary " + pplNames[i] + " = TCA " + primaryToSecondary[0].ToString() + " : Range (m) " + ((double)primaryToSecondary[1]).ToString("0.##"));
                }
                return result.ToArray();
            }

            return null;
        }
    }
}
