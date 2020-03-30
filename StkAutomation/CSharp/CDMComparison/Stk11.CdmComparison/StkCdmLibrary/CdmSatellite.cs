using System;
using System.IO;
using System.Text.RegularExpressions;
using AGI.STKObjects;
using ZedGraph;
using System.Drawing;
using AGI.STKVgt;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace StkCdmLibrary
{
    public class CdmSatellite
    {
        private const double small = 0.001;
        private double _leoLimit = 2500;
        //private double _GeoLimit = 10;
        //private double _EarthGrav = 398600.4418; // km^3/s^2, EGM96
        //private double _EarthRadius = 6378.137;

        private string _stkCdmSatelliteName;
        private string _stkTleObjName;
        private string _stkEphObjName;
        private double _stkRangeAtCdmTca = -1;
        private double _stkMinimumRange = -1;
        private string _stkTimeOfMinRange;

        public bool isPrimary, isEphemerisBased;
        public string EpochISOYMD;
        public string SatName, InternationalDesignator, SSC;
        public string CovarianceMethod;

        public string ReferenceFrame;        
        public double XPos, YPos, ZPos, XVel, YVel, ZVel;
        public double xx, yx, yy, zx, zy, zz, Vxx, Vxy, Vxz, VxVx, Vyx, Vyy, Vyz, VyVx, VyVy, Vzx, Vzy, Vzz, VzVx, VzVy, VzVz;
        
        public string GeopotentialModel, SolidEarthTidesPerturbation, LunarsolarPerturbations, SolarRadiationPerturbation, DragModel;
        public double BallisticCoefficient, SolarRadiationCoefficient;

        public string EphemerisFile;

        public CdmSatellite()
        {

        }

        public CdmSatellite(CdmSatellite rhs)
        {
            this.isPrimary = rhs.isPrimary;
            this.isEphemerisBased = rhs.isEphemerisBased;
            this.EpochISOYMD = rhs.EpochISOYMD;
            this.SatName = rhs.SatName;
            this.InternationalDesignator = rhs.InternationalDesignator;
            this.SSC = rhs.SSC;
            this.CovarianceMethod = rhs.CovarianceMethod;
            this.ReferenceFrame = rhs.ReferenceFrame;
            this.XPos = rhs.XPos;
            this.YPos = rhs.YPos;
            this.ZPos = rhs.ZPos;
            this.XVel = rhs.XVel;
            this.YVel = rhs.YVel;
            this.ZVel = rhs.ZVel;
            this.xx = rhs.xx;
            this.yx = rhs.yx;
            this.yy = rhs.yy;
            this.zx = rhs.zx;
            this.zy = rhs.zy;
            this.zz = rhs.zz;
            this.Vxx = rhs.Vxx;
            this.Vxy = rhs.Vxy;
            this.Vxz = rhs.Vxz;
            this.VxVx = rhs.VxVx;
            this.Vyx = rhs.Vyx;
            this.Vyy = rhs.Vyy;
            this.Vyz = rhs.Vyz;
            this.VyVx = rhs.VyVx;
            this.VyVy = rhs.VyVy;
            this.Vzx = rhs.Vzx;
            this.Vzy = rhs.Vzy;
            this.Vzz = rhs.Vzz;
            this.VzVx = rhs.VzVx;
            this.VzVy = rhs.VzVy;
            this.VzVz = rhs.VzVz;
            this.GeopotentialModel = rhs.GeopotentialModel;
            this.SolidEarthTidesPerturbation = rhs.SolidEarthTidesPerturbation;
            this.LunarsolarPerturbations = rhs.LunarsolarPerturbations;
            this.SolarRadiationPerturbation = rhs.SolarRadiationPerturbation;
            this.DragModel = rhs.DragModel;
            this.BallisticCoefficient = rhs.BallisticCoefficient;
            this.SolarRadiationCoefficient = rhs.SolarRadiationCoefficient;
            this.EphemerisFile = rhs.EphemerisFile;

        }
        private string _safeSatName;
        public string SafeSatName
        {
            get
            {
                if (string.IsNullOrEmpty(_safeSatName))
                {
                    _safeSatName = Regex.Replace(SatName, "[///(/)" + new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + " ]", "");
                }
                return _safeSatName;
            }
        }
        public bool isGeoRegime
        {
            get
            {
                if (!string.IsNullOrEmpty(BaselineObjectPath))
                {
                    double period = StkAssistant.GetSatellitePeriod(BaselineObjectPath, EpochISOYMD);
                    return Math.Abs(86400 - period) / 864 < 8;
                }
                return false;
            }
        }

        public string BaselineObjectPath
        {
            get 
            {
                return StkEphSatellitePath != null ? StkEphSatellitePath
                    : StkCdmSatellitePath != null ? StkCdmSatellitePath
                    : StkTleSatellitePath != null ? StkTleSatellitePath
                    : null;
            }
        }

        public string BaselineObjectType
        {
            get
            {
                return BaselineObjectPath.Equals(StkEphSatellitePath) ? "Ephemeris" 
                    : BaselineObjectPath.Equals(StkCdmSatellitePath) ? "CDM" 
                    : BaselineObjectPath.Equals(StkTleSatellitePath) ? "TLE" 
                    : null;
            }
        }
            
        public string StkCdmSatelliteName
        {
            get
            {
                if (string.IsNullOrEmpty(_stkCdmSatelliteName))
                {
                    CreateCdmSatellite();
                }
                return _stkCdmSatelliteName;
            }
        }

        public string StkCdmSatellitePath
        {
            get
            {
                if (StkAssistant.Root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, StkCdmSatelliteName))
                {
                    return "*/Satellite/" + StkCdmSatelliteName;
                }
                else
                {
                    return null;
                }
            }
        }

        public string StkTleSatelliteName
        {
            get
            {
                if (string.IsNullOrEmpty(_stkTleObjName))
                {
                    CreateTleSatellite();
                }

                return _stkTleObjName;
            }
        }

        public string StkTleSatellitePath
        {
            get
            {
                if (StkTleSatelliteName != null
                    && StkAssistant.Root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, StkTleSatelliteName))
                {
                    return "*/Satellite/" + StkTleSatelliteName;
                }
                else
                {
                    return null;
                }
            }
        }

        public string StkEphSatelliteName
        {
            get
            {
                if (string.IsNullOrEmpty(_stkEphObjName))
                {
                    CreateEphSatellite();
                }

                return _stkEphObjName;
            }
        }

        public string StkEphSatellitePath
        {
            get
            {
                if (StkEphSatelliteName != null
                    && StkAssistant.Root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, StkEphSatelliteName))
                {
                    return "*/Satellite/" + StkEphSatelliteName;
                }
                else
                {
                    return null;
                }
            }
        }

        public double StkCdmTleDiffAtCdmTca
        {
            get
            {
                if (!string.IsNullOrEmpty(StkCdmSatellitePath)
                    && !string.IsNullOrEmpty(StkTleSatellitePath)
                    && !string.IsNullOrEmpty(this.EpochISOYMD)
                    && _stkRangeAtCdmTca == -1)
                {
                    double[] ric = StateCompare.GetRICDifferenceAtTCA(StkCdmSatellitePath, StkTleSatellitePath, this.EpochISOYMD);
                    _stkRangeAtCdmTca = Math.Sqrt(ric[0] * ric[0] + ric[1] * ric[1] + ric[2] * ric[2]);
                }

                return _stkRangeAtCdmTca;
            }
        }

        public double StkCdmTleMinimumRange
        {
            get
            {
                if (_stkMinimumRange == -1
                    && !string.IsNullOrEmpty(StkCdmSatelliteName)
                    && !string.IsNullOrEmpty(StkTleSatelliteName))
                {
                    GetStkCdmTleTCA();
                }

                return _stkMinimumRange;
            }
        }

        public string StkCdmTleTimeOfMinRange
        {
            get
            {
                if (string.IsNullOrEmpty(_stkTimeOfMinRange)
                    && !string.IsNullOrEmpty(StkCdmSatelliteName)
                    && !string.IsNullOrEmpty(StkTleSatelliteName))
                {
                    GetStkCdmTleTCA();
                }
                return _stkTimeOfMinRange;
            }
        }

        public bool CreateCdmSatellite(bool zoomTo = false)
        {
            string cmdResult;
            string command = "BatchGraphics * On";
            StkAssistant.TryConnect(command, out cmdResult);

            try
            {
                #region New Satellite
                string color;
                string zonal = "";
                string tesseral = "";
                if (isPrimary)
                {
                    _stkCdmSatelliteName = "cdmPrimary_" + SafeSatName;
                    color = "#00FF00";
                }
                else
                {
                    _stkCdmSatelliteName = "cdmSecondary_" + SafeSatName;
                    color = "#FF9900";
                }

                string position = String.Format("{0} {1} {2}", XPos, YPos, ZPos); // m
                double posMag = VecMagS(position);
                string velocity = String.Format("{0} {1} {2}", XVel, YVel, ZVel); // m/s

                if (posMag < 1)
                {
                    return false;
                }

                if (!StkAssistant.Root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, StkCdmSatelliteName))
                {
                    StkAssistant.Root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, StkCdmSatelliteName);
                }


                string stkReferenceFrame = "Fixed";

                switch (this.ReferenceFrame)
                {
                    case "ITRF":
                        stkReferenceFrame = "Fixed";
                        break;
                    case "ICRF":
                    case "GCRF":
                        stkReferenceFrame = "ICRF";
                        break;
                    case "EME2000":
                        stkReferenceFrame = "J2000";
                        break;
                    case "MeanOfDate":
                        stkReferenceFrame = "MeanOfDate";
                        break;
                    case "MeanOfEpoch":
                        stkReferenceFrame = "MeanOfEpoch";
                        break;
                    case "TrueOfDate":
                        stkReferenceFrame = "TrueOfDate";
                        break;
                    case "TrueOfEpoch":
                        stkReferenceFrame = "TrueOfEpoch";
                        break;
                    case "B1950":
                        stkReferenceFrame = "B1950";
                        break;
                    case "TEMEOfDate":
                        stkReferenceFrame = "TEMEOfDate";
                        break;
                    case "TEMEOfEpoch":
                        stkReferenceFrame = "TEMEOfEpoch";
                        break;
                    case "AlignmentAtEpoch":
                        stkReferenceFrame = "AlignmentAtEpoch";
                        break;
                }

                command = String.Format("SetStateIgnoreCB  */Satellite/{0} Cartesian HPOP \"{1}\" \"{2}\" 10 {3} \"{4}\" {5} {6}",
                    StkCdmSatelliteName, (StkAssistant.Root.CurrentScenario as IAgScenario).StartTime, (StkAssistant.Root.CurrentScenario as IAgScenario).StopTime,
                    stkReferenceFrame, EpochISOYMD, position, velocity);
                if (!StkAssistant.TryConnect(command, out cmdResult))
                {
                    return false; // object with no state
                }
                #endregion New Satellite


                SetSatVizOptions(StkCdmSatelliteName, color);


                #region Force Model
                if (!string.IsNullOrEmpty(GeopotentialModel))
                {
                    // Set geopotential: EGM-96, ... 
                    Regex regex = new Regex(@"(EGM-96):\s+(\d+)D\s+(\d+)O");
                    Match match = regex.Match(GeopotentialModel);
                    if (match.Success)
                    {
                        if (match.Groups[1].Value != "EGM-96")
                        {
                            //outputToDebugLog("WARNING: " + match.Groups[1].Value + " gravitational model");
                            //warning = true;
                        }
                        zonal = match.Groups[2].Value;
                        tesseral = match.Groups[3].Value;
                        command = String.Format("HPOP */Satellite/{0} Force Gravity \"{1}STKData\\CentralBodies\\Earth\\WGS84_EGM96.grv\" {2} {3}",
                            StkCdmSatelliteName, StkAssistant.StkInstallDirectory, zonal, tesseral);
                        StkAssistant.TryConnect(command, out cmdResult);

                        command = String.Format("HPOP */Satellite/{0} Covariance Gravity {1} {2}", StkCdmSatelliteName, zonal, tesseral);
                        StkAssistant.TryConnect(command, out cmdResult);
                    }
                }
                else
                {
                    //outputToDebugLog(String.Format("WARNING: Undefined gravitational model: {0}", geopotentialModel[index].InnerText));
                    //warning = true;
                }

                if (!string.IsNullOrEmpty(LunarsolarPerturbations))
                {
                    string nBody = LunarsolarPerturbations.ToUpper();
                    if ((nBody != "NONE") || ((isEphemerisBased) && (isGeoRegime)))
                    {
                        if (nBody.Contains("SUN"))
                        {
                            command = String.Format("HPOP */Satellite/{0} Force ThirdBodyGravity Sun On JPLDEFile", StkCdmSatelliteName);
                            StkAssistant.TryConnect(command, out cmdResult);
                        }
                        if (nBody.Contains("MOON"))
                        {
                            command = String.Format("HPOP */Satellite/{0} Force ThirdBodyGravity Moon On JPLDEFile", StkCdmSatelliteName);
                            StkAssistant.TryConnect(command, out cmdResult);
                        }
                    }
                }
                else
                {
                    command = String.Format("HPOP */Satellite/{0} Force ThirdBodyGravity Sun Off", StkCdmSatelliteName);
                    StkAssistant.TryConnect(command, out cmdResult);
                    command = String.Format("HPOP */Satellite/{0} Force ThirdBodyGravity Moon Off", StkCdmSatelliteName);
                    StkAssistant.TryConnect(command, out cmdResult);
                }

                if (!string.IsNullOrEmpty(SolidEarthTidesPerturbation)
                && SolidEarthTidesPerturbation.ToUpper() == "YES")
                {
                    command = String.Format("HPOP */Satellite/{0} Force SolidTides Full", StkCdmSatelliteName); // Permanent only or full tide?
                    StkAssistant.TryConnect(command, out cmdResult);
                }
                else
                {
                    command = String.Format("HPOP */Satellite/{0} Force SolidTides Off", StkCdmSatelliteName);
                    StkAssistant.TryConnect(command, out cmdResult);
                }

                command = String.Format("HPOP */Satellite/{0} Force OceanTides Off", StkCdmSatelliteName);
                StkAssistant.TryConnect(command, out cmdResult);

                if (!string.IsNullOrEmpty(DragModel))
                {
                    bool noDrag = (StkAssistant.GetSatellitePerigee(StkCdmSatellitePath, EpochISOYMD) > _leoLimit * 1E3); // m
                    if (!"JACCHIA70DCA|NONE".Contains(DragModel))
                    {
                        //outputToDebugLog(String.Format("WARNING: Undefined drag model: {0}", dragModel[index].InnerText));
                        //warning = true;
                    }
                    if ((DragModel.ToUpper() == "NONE") || noDrag)
                    {
                        command = String.Format("HPOP */Satellite/{0} Drag Off", StkCdmSatelliteName);
                        StkAssistant.TryConnect(command, out cmdResult);
                    }
                    else
                    {
                        command = String.Format("HPOP */Satellite/{0} Drag On {1} {2} \"Jacchia 1970\" File \"{3}SpaceWeather-v1.2.txt\"",
                            StkCdmSatelliteName, Math.Sign(BallisticCoefficient).ToString("F1"), Math.Abs(BallisticCoefficient).ToString("F6"), StkAssistant.StkDataDirectory);
                        StkAssistant.TryConnect(command, out cmdResult);
                    }
                }


                if (!string.IsNullOrEmpty(SolarRadiationPerturbation)
                    && SolarRadiationPerturbation.ToUpper() == "YES")
                {
                    command = String.Format("HPOP */Satellite/{0} Force SRP On Model Spherical {1} {2} ShadowModel Cylindrical BoundaryMitigation On",
                        StkCdmSatelliteName, Math.Sign(SolarRadiationCoefficient).ToString("F1"), Math.Abs(SolarRadiationCoefficient).ToString("F6"));
                    StkAssistant.TryConnect(command, out cmdResult);
                }
                else
                {
                    command = String.Format("HPOP */Satellite/{0} Force SRP Off", StkCdmSatelliteName);
                    StkAssistant.TryConnect(command, out cmdResult);
                }

                command = String.Format("HPOP */Satellite/{0} Force RadiationPressure Albedo Off Thermal Off", StkCdmSatelliteName);
                StkAssistant.TryConnect(command, out cmdResult);

                command = String.Format("HPOP */Satellite/{0} Integrator IntegMethod RKF78 StepControl RelativeError 1E-13", StkCdmSatelliteName);
                StkAssistant.TryConnect(command, out cmdResult);
                // Set HPOP Options
                //stkExecute(String.Format("HPOP */Satellite/{0} Options Mass " + FloatToStr(mass)), satObj));
                #endregion Force Model

                #region Covariance
                // Set up covariance
                command = String.Format("HPOP */Satellite/{0} Covariance On", StkCdmSatelliteName);
                StkAssistant.TryConnect(command, out cmdResult);

                command = String.Format("HPOP */Satellite/{0} Covariance Frame LVLH", StkCdmSatelliteName);
                StkAssistant.TryConnect(command, out cmdResult);
                command = String.Format("HPOP */Satellite/{0} Covariance Consider Off", StkCdmSatelliteName);
                StkAssistant.TryConnect(command, out cmdResult);

                // Force non-zero principal components
                if (xx == 0) xx = small;
                if (yy == 0) yy = small;
                if (zz == 0) zz = small;
                if (VxVx == 0) VxVx = small;
                if (VyVy == 0) VyVy = small;
                if (VzVz == 0) VzVz = small;
                string ldMatrix = xx + " ";
                ldMatrix += yx + " " + yy + " ";
                ldMatrix += zx + " " + zy + " " + zz + " ";
                ldMatrix += Vxx + " " + Vxy + " " + Vxz + " " + VxVx + " ";
                ldMatrix += Vyx + " " + Vyy + " " + Vyz + " " + VyVx + " " + VyVy + " ";
                ldMatrix += Vzx + " " + Vzy + " " + Vzz + " " + VzVx + " " + VzVy + " " + VzVz;
                string covCommand = String.Format("HPOP */Satellite/{0} Covariance PosVel {1}", StkCdmSatelliteName, ldMatrix);
                string covColor = color;
                if (!StkAssistant.TryConnect(covCommand, out cmdResult))
                {
                    //notes.Add("<span style=\"color:red\">WARNING: " + satObjName.Substring(3) + " CDM covariance matrix not positive definite</span>");
                    string zero = "0";
                    string nonzero = "1000";
                    ldMatrix = nonzero + " ";
                    ldMatrix += zero + " " + nonzero + " ";
                    ldMatrix += zero + " " + zero + " " + nonzero + " ";
                    ldMatrix += zero + " " + zero + " " + zero + " " + nonzero + " ";
                    ldMatrix += zero + " " + zero + " " + zero + " " + zero + " " + nonzero + " ";
                    ldMatrix += zero + " " + zero + " " + zero + " " + zero + " " + zero + " " + nonzero;
                    covColor = "#FF0000";
                    covCommand = String.Format("HPOP */Satellite/{0} Covariance PosVel {1}", StkCdmSatelliteName, ldMatrix);
                    StkAssistant.TryConnect(covCommand, out cmdResult);
                    command = String.Format("VO */Satellite/{0} Covariance Basic Show Off", StkCdmSatelliteName);
                    StkAssistant.TryConnect(command, out cmdResult);
                }
                else
                {
                    command = String.Format("VO */Satellite/{0} Covariance Scale 3.0", StkCdmSatelliteName);
                    StkAssistant.TryConnect(command, out cmdResult);
                    command = String.Format("VO */Satellite/{0} Covariance Basic Show On Color {1} LineWidth 2 Translucency 50", StkCdmSatelliteName, covColor);
                    StkAssistant.TryConnect(command, out cmdResult);
                }
                #endregion Covariance

                command = String.Format("Propagate */Satellite/{0} {1} {2}", StkCdmSatelliteName, StkAssistant.ScenarioStartTime, StkAssistant.ScenarioStopTime);
                StkAssistant.TryConnect(command, out cmdResult);

                if (zoomTo)
                {
                    StkAssistant.TryConnect(String.Format("VO * ViewFromTo Normal From */Satellite/{0}", StkCdmSatelliteName), out cmdResult);
                }
            }
            finally
            {
                command = "BatchGraphics * Off";
                StkAssistant.TryConnect(command, out cmdResult);
            }
            return true;
        } 

        public bool CreateTleSatellite(string tlePath = null)
        {
            string cmdResult;
            string command = "BatchGraphics * On";
            StkAssistant.TryConnect(command, out cmdResult);
            bool satCreated = false;
            try
            {
                
                string color;
                string tempName;
                if (isPrimary)
                {
                    tempName = "tlePrimary_" + SafeSatName;
                    color = "#00FF00";
                }
                else
                {
                    tempName = "tleSecondary_" + SafeSatName;
                    color = "#FF9900";
                }

                if (!StkAssistant.Root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, tempName))
                {
                    StkAssistant.Root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, tempName);
                }

                //If a file is provided, pull from it
                if (!string.IsNullOrEmpty(tlePath)
                    && File.Exists(tlePath))
                {
                    command = String.Format("SetState */Satellite/{0} SGP4 UseScenarioInterval 60.0 {1} TLESource Automatic Source File \"{2}\" UseTLE All SwitchMethod Midpoint UseCatalogModel",
                    tempName, SSC.ToString(), tlePath);
                    satCreated = StkAssistant.TryConnect(command, out cmdResult);
                }
                //If not created yet, try to get from the AGI server
                if (!satCreated)
                {
                    command = String.Format("SetState */Satellite/{0} SGP4 UseScenarioInterval 60.0 {1} TLESource Automatic Source AGIServer UseTLE All SwitchMethod Midpoint UseCatalogModel",
                    tempName, SSC.ToString());
                    satCreated = StkAssistant.TryConnect(command, out cmdResult);
                }
                //If not created yet, try to get from the local default file
                if (!satCreated)
                {
                    command = String.Format("SetState */Satellite/{0} SGP4 UseScenarioInterval 60.0 {1} TLESource Automatic Source DefaultFile UseTLE All SwitchMethod Midpoint UseCatalogModel",
                    tempName, SSC.ToString());
                    satCreated = StkAssistant.TryConnect(command, out cmdResult);
                }

                if (satCreated)
                {
                    _stkTleObjName = tempName;
                    SetSatVizOptions(tempName, color);                    
                }
            }
            finally
            {
                command = "BatchGraphics * Off";
                StkAssistant.TryConnect(command, out cmdResult);
            }
            return satCreated;
        }

        public bool CreateEphSatellite(string ephemerisPath = null)
        {
            if (!string.IsNullOrEmpty(ephemerisPath)
                && File.Exists(ephemerisPath))
            {
                EphemerisFile = ephemerisPath;
            }
            else if (!string.IsNullOrEmpty(EphemerisFile)
                && File.Exists(EphemerisFile))
            {
                ephemerisPath = EphemerisFile;
            }
            else
            {
                return false;
            }

            string cmdResult;
            string command = "BatchGraphics * On";
            StkAssistant.TryConnect(command, out cmdResult);

            bool satCreated = false;
            try
            {
                string color;
                string tempName;
                if (isPrimary)
                {
                    tempName = "ephPrimary_" + SafeSatName;
                    color = "#00FF00";
                }
                else
                {
                    tempName = "ephSecondary_" + SafeSatName;
                    color = "#FF9900";
                }

                
                if (!string.IsNullOrEmpty(ephemerisPath)
                    && File.Exists(ephemerisPath))
                {
                    if (!StkAssistant.Root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, tempName))
                    {
                        StkAssistant.Root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, tempName);
                    }

                    command = String.Format("SetState */Satellite/{0} FromFile \"{1}\"", tempName, ephemerisPath);
                    satCreated = StkAssistant.TryConnect(command, out cmdResult);
                }

                if (satCreated)
                {
                    _stkEphObjName = tempName;
                    SetSatVizOptions(tempName, color);                    
                }
            }
            finally
            {
                command = "BatchGraphics * Off";
                StkAssistant.TryConnect(command, out cmdResult);
            }
            return satCreated;
        }

        private void SetSatVizOptions(string tempName, string colorConnectString)
        {
            #region Viz
            string command, cmdResult;

            command = String.Format("Graphics */Satellite/{0} SetColor {1}", tempName, colorConnectString);
            StkAssistant.TryConnect(command, out cmdResult);
            command = String.Format("Graphics */Satellite/{0} Basic Orbit On", tempName);
            StkAssistant.TryConnect(command, out cmdResult);
            command = String.Format("Graphics */Satellite/{0} Basic LineWidth 2", tempName);
            StkAssistant.TryConnect(command, out cmdResult);
            command = String.Format("VO */Satellite/{0} Model Show Off", tempName);
            StkAssistant.TryConnect(command, out cmdResult);
            command = String.Format("VO */Satellite/{0} Point Show On Size 6", tempName);
            StkAssistant.TryConnect(command, out cmdResult);
            command = String.Format("VO */Satellite/{0} ModelDetail Off", tempName);
            StkAssistant.TryConnect(command, out cmdResult);
            command = String.Format("VO */Satellite/{0} Pass3D OrbitLead OnePass", tempName);
            StkAssistant.TryConnect(command, out cmdResult);
            command = String.Format("VO */Satellite/{0} Pass3D OrbitTrail OnePass", tempName);
            StkAssistant.TryConnect(command, out cmdResult);
            if (isGeoRegime)
            {
                command = String.Format("VO */Satellite/{0} OrbitSystem Modify System \"FixedByWindow\" Color Default Show On", tempName);
                StkAssistant.TryConnect(command, out cmdResult);
                command = String.Format("VO */Satellite/{0} OrbitSystem Modify System \"InertialByWindow\" Show Off", tempName);
                StkAssistant.TryConnect(command, out cmdResult);
            }
            #endregion Viz
        }

        
        public GraphPane GraphSatelliteStateComparison(Graphics g)
        {
            try
            {
                bool tleSuccess = StkTleSatellitePath != null, cdmSuccess = StkCdmSatellitePath != null, ephSuccess = StkEphSatellitePath != null;

                string primary = ephSuccess ? StkEphSatellitePath : cdmSuccess ? StkCdmSatellitePath : StkTleSatellitePath;
                List <string> secondaries = new List<string>();
                if (ephSuccess && !primary.Equals(StkEphSatellitePath)) secondaries.Add(StkEphSatellitePath);
                if (tleSuccess && !primary.Equals(StkTleSatellitePath)) secondaries.Add(StkTleSatellitePath);
                if (cdmSuccess && !primary.Equals(StkCdmSatellitePath)) secondaries.Add(StkCdmSatellitePath);
                
                if (primary != null && secondaries.Count > 0)
                {
                    string[] pplNames = secondaries.Select(s => s.Equals(StkEphSatellitePath) ? "Ephemeris" : s.Equals(StkTleSatellitePath) ? "TLE" : s.Equals(StkCdmSatellitePath) ? "CDM" : null).ToArray();

                    TextObj tcaComment = ZedGraphAssistant.CreateGraphLabel("TCA : " + this.EpochISOYMD, 1f, .01f, AlignH.Right, AlignV.Top, Color.Black);
                    //TextObj[] comments = new TextObj[] {tcaComment, tcaRangeComment };

                    List<PointPairList> ppls = new List<PointPairList>();
                    List<TextObj> comments = new List<TextObj>();
                    comments.Add(tcaComment);
                    for (int i = 0; i < secondaries.Count; i++)
                    {
                        string second = secondaries[i];
                        StateCompare.RICResults ricResults = StateCompare.GetRICDifferenceOverTime(primary, second, this.EpochISOYMD);
                        ppls.Add(ZedGraphAssistant.ArrayToPlottableList(ricResults.Times, ricResults.Range));

                        double[] ric = StateCompare.GetRICDifferenceAtTCA(primary, second, this.EpochISOYMD);
                        double rangeAtTca = Math.Sqrt(ric[0] * ric[0] + ric[1] * ric[1] + ric[2] * ric[2]);
                        comments.Add(ZedGraphAssistant.CreateGraphLabel(pplNames[i] + " Range at TCA (m): " + rangeAtTca.ToString("0.##"), 1f, (float)(.05*(i+1)), AlignH.Right, AlignV.Top, Color.Black));
                    }

                    PointPairList[] ppl = ppls.ToArray();
                    string primaryType = primary.Equals(StkEphSatellitePath) ? "Ephemeris" : primary.Equals(StkTleSatellitePath) ? "TLE" : primary.Equals(StkCdmSatellitePath) ? "CDM" : null;
                    GraphPane graph = ZedGraphAssistant.CreateGraph(SatName + " State Comparison: Baseline = " + primaryType, this.EpochISOYMD, g, ppl, pplNames, comments.ToArray());
                    graph.XAxis.Scale.Min = 0;
                    
                    return graph;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public GraphPane GraphCdmVsTle(Graphics g)
        {       
            try
            {
                if (StkTleSatellitePath == null)
                {
                    CreateTleSatellite();
                }
                if (StkTleSatellitePath != null)
                {
                    TextObj stktcaComment = ZedGraphAssistant.CreateGraphLabel("Time of Min Range: " + StkCdmTleTimeOfMinRange, .01f, .01f, Color.Black);
                    TextObj stkmissComment = ZedGraphAssistant.CreateGraphLabel("Minimum Range: " + StkCdmTleMinimumRange.ToString(), .01f, .05f, Color.Black);
                    TextObj tcaComment = ZedGraphAssistant.CreateGraphLabel("TCA : " + this.EpochISOYMD, 1f, .01f, AlignH.Right, AlignV.Top, Color.Black);
                    TextObj tcaRangeComment = ZedGraphAssistant.CreateGraphLabel("Range at TCA : " + StkCdmTleDiffAtCdmTca.ToString(), 1f, .05f, AlignH.Right, AlignV.Top, Color.Black);
                    TextObj[] comments = new TextObj[] { stktcaComment, stkmissComment, tcaComment, tcaRangeComment };

                    StateCompare.RICResults ricOverTime = StateCompare.GetRICDifferenceOverTime(this.StkCdmSatellitePath, this.StkTleSatellitePath, this.EpochISOYMD);

                    PointPairList pplR = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.R);
                    PointPairList pplI = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.I);
                    PointPairList pplC = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.C);
                    PointPairList pplRange = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.Range);
                    PointPairList pplTimeOfMinRange = new PointPairList();
                    pplTimeOfMinRange.Add(StkAssistant.ParseISOYMD(StkCdmTleTimeOfMinRange).ToOADate(), -5 * StkCdmTleMinimumRange);
                    pplTimeOfMinRange.Add(StkAssistant.ParseISOYMD(StkCdmTleTimeOfMinRange).ToOADate(), 5 * StkCdmTleMinimumRange);

                    PointPairList[] ppl = new PointPairList[] { pplR, pplI, pplC, pplRange, pplTimeOfMinRange };
                    string[] pplNames = new string[] { "Radial", "In-Track", "Cross-Track", "Range", "" };

                    return ZedGraphAssistant.CreateGraph("Comparison: " + SatName + " CDM to TLE", this.EpochISOYMD, g, ppl, pplNames, comments);
                }

                return null; 
            }
            catch
            {
                return null;
            }
        }

        private void GetStkCdmTleTCA()
        {
            IAgCrdnVector diff = StateCompare.GetVectorBetweenObjects(StkCdmSatellitePath, StkCdmSatellitePath, StkTleSatellitePath);
            object[] diffMin = StateCompare.GetTimeOfMinAndValue(diff);
            _stkTimeOfMinRange = (string)diffMin.GetValue(0);
            _stkMinimumRange = ((double)diffMin.GetValue(1));
        }

        private double VecMag(double[] vec)
        {
            return Math.Sqrt(vec[0] * vec[0] + vec[1] * vec[1] + vec[2] * vec[2]);
        } // end VecMag

        private double VecMagS(string vecS)
        {
            double[] vec = Array.ConvertAll<string, double>(vecS.Split(' '), Double.Parse);
            return VecMag(vec);
        } // end VecMagS

    }


}
