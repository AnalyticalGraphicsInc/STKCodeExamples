using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AGI.STKUtil;
using System.Text.RegularExpressions;

namespace StkCdmLibrary
{
    public static class CdmReader
    {
        public static string CdmOutputDirectory
        {
            get
            {
                DirectoryInfo appDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
                string cdmDirectory = Path.Combine(Path.Combine(appDir.FullName, "AGI"), "CDM");
                if (!Directory.Exists(cdmDirectory))
                {
                    Directory.CreateDirectory(cdmDirectory);
                }

                return cdmDirectory;
            }
        }

        private const double small = 0.001;
        
        private static double GetDoubleFromXmlTag(XmlDocument xDoc, string tagName, int index = 0)
        {
            double value = 0;
            try
            {
                if (xDoc.GetElementsByTagName(tagName).Count > index
                    && double.TryParse(xDoc.GetElementsByTagName(tagName)[index].InnerText, out value)
                    && xDoc.GetElementsByTagName(tagName)[index].Attributes["units"] != null
                    && (xDoc.GetElementsByTagName(tagName)[index].Attributes["units"].InnerText == "km"
                    || xDoc.GetElementsByTagName(tagName)[index].Attributes["units"].InnerText == "km/s"))
                {
                    value = value * 1000;
                }
            }
            catch { }
            return value;
        }

        private static string GetStringFromXmlTag(XmlDocument xDoc, string tagName, int index = 0)
        {
            string value = null;
            try
            {
                if (xDoc.GetElementsByTagName(tagName).Count > index)
                {
                    value = xDoc.GetElementsByTagName(tagName)[index].InnerText;
                }
            }
            catch { }
            return value;
        }



        
        public static CdmConjunction ReadCdmXmlByTag(string xmlPath, out string error)
        {
            error = "";
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                CdmConjunction conjunction = new CdmConjunction();

                CdmSatellite[] sats = new CdmSatellite[] { new CdmSatellite(), new CdmSatellite() };
                conjunction.CdmFilePath = xmlPath;
                conjunction.RelPosR = GetDoubleFromXmlTag(xmlDoc, "RELATIVE_POSITION_R");
                conjunction.RelPosT = GetDoubleFromXmlTag(xmlDoc, "RELATIVE_POSITION_T");
                conjunction.RelPosN = GetDoubleFromXmlTag(xmlDoc, "RELATIVE_POSITION_N");
                conjunction.RelVelR = GetDoubleFromXmlTag(xmlDoc, "RELATIVE_VELOCITY_R");
                conjunction.RelVelT = GetDoubleFromXmlTag(xmlDoc, "RELATIVE_VELOCITY_T");
                conjunction.RelVelN = GetDoubleFromXmlTag(xmlDoc, "RELATIVE_VELOCITY_N");
                conjunction.RelativeSpeed = GetDoubleFromXmlTag(xmlDoc, "RELATIVE_SPEED");

                conjunction.Probability = GetDoubleFromXmlTag(xmlDoc, "COLLISION_PROBABILITY");
                conjunction.ProbabilityMethod = GetStringFromXmlTag(xmlDoc, "COLLISION_PROBABILITY_METHOD");
                conjunction.ID = GetStringFromXmlTag(xmlDoc, "MESSAGE_ID");
                conjunction.CreationDate = GetStringFromXmlTag(xmlDoc, "CREATION_DATE");                
                conjunction.MissDistance = GetDoubleFromXmlTag(xmlDoc, "MISS_DISTANCE");
                conjunction.TCA = GetStringFromXmlTag(xmlDoc,"TCA");
                conjunction.TCA = StkAssistant.ValidateDateFormat(conjunction.TCA);
                conjunction.CreationDate = StkAssistant.ValidateDateFormat(conjunction.CreationDate);
                if (xmlDoc.GetElementsByTagName("relativeMetadataData").Count > 0)
                {
                    foreach (XmlNode node in xmlDoc.GetElementsByTagName("relativeMetadataData")[0].ChildNodes)
                    {
                        if (node.Name.ToUpper().Equals("COMMENT"))
                        {
                            string metadataComment = node.InnerText;
                            string pattern = "SIGMA_DILUTION=(?<sigmaDilution>\\d+(.\\d+)?)";

                            Regex dateRegex = new Regex(pattern);
                            Match match = dateRegex.Match(metadataComment);
                            double sd;
                            if (match.Groups["sigmaDilution"].Success
                                && double.TryParse(match.Groups["sigmaDilution"].Value,out sd))
                            {
                                conjunction.SigmaDilution = sd;
                            }
                            break;
                        }
                    }
                }   

                for (int i = 0; i < 2; i++)
                {
                    sats[i].isPrimary = i == 0;
                    sats[i].EpochISOYMD = GetStringFromXmlTag(xmlDoc,"TCA");
                    sats[i].EpochISOYMD = StkAssistant.ValidateDateFormat(sats[i].EpochISOYMD);
                    // Get satellite names
                    sats[i].SatName = GetStringFromXmlTag(xmlDoc,"OBJECT_NAME", i);
                    // Get International Designators
                    sats[i].InternationalDesignator = GetStringFromXmlTag(xmlDoc,"INTERNATIONAL_DESIGNATOR", i);
                    // Get Catalog Number
                    sats[i].SSC = GetStringFromXmlTag(xmlDoc,"OBJECT_DESIGNATOR", i);
                    if (sats[i].SSC == "KNOWN OBJECT")
                    {
                        sats[i].SatName = "KNOWN OBJECT";
                        sats[i].SSC = "99999";
                    }
                    if (sats[i].SatName.Trim() == "")
                        sats[i].SatName = "UNIDENTIFIED OBJECT";
                    // Get ephemeris check data
                    string ephTemp = GetStringFromXmlTag(xmlDoc, "EPHEMERIS_NAME", i);
                    sats[i].EphemerisFile = string.IsNullOrEmpty(ephTemp) || ephTemp.ToUpper().Equals("NONE") ? null : 
                        Path.Combine(Path.GetDirectoryName(xmlPath), ephTemp);

                    // Get Orbital Data
                    sats[i].ReferenceFrame = GetStringFromXmlTag(xmlDoc,"REF_FRAME", i);
                    sats[i].XPos = GetDoubleFromXmlTag(xmlDoc, "X", i);
                    sats[i].YPos = GetDoubleFromXmlTag(xmlDoc, "Y", i);
                    sats[i].ZPos = GetDoubleFromXmlTag(xmlDoc, "Z", i);
                    sats[i].XVel = GetDoubleFromXmlTag(xmlDoc, "X_DOT", i);
                    sats[i].YVel = GetDoubleFromXmlTag(xmlDoc, "Y_DOT", i);
                    sats[i].ZVel = GetDoubleFromXmlTag(xmlDoc, "Z_DOT", i);

                    // Get Force Model Definition
                    sats[i].GeopotentialModel = GetStringFromXmlTag(xmlDoc,"GRAVITY_MODEL", i);
                    sats[i].LunarsolarPerturbations = GetStringFromXmlTag(xmlDoc,"N_BODY_PERTURBATIONS", i);
                    sats[i].SolidEarthTidesPerturbation = GetStringFromXmlTag(xmlDoc,"EARTH_TIDES", i);
                    sats[i].SolarRadiationPerturbation = GetStringFromXmlTag(xmlDoc,"SOLAR_RAD_PRESSURE", i);
                    sats[i].SolarRadiationCoefficient = GetDoubleFromXmlTag(xmlDoc, "CR_AREA_OVER_MASS", i);
                    sats[i].DragModel = GetStringFromXmlTag(xmlDoc,"ATMOSPHERIC_MODEL", i);
                    sats[i].BallisticCoefficient = GetDoubleFromXmlTag(xmlDoc, "CD_AREA_OVER_MASS", i);
                    sats[i].CovarianceMethod = GetStringFromXmlTag(xmlDoc,"COVARIANCE_METHOD", i);

                    sats[i].xx = GetDoubleFromXmlTag(xmlDoc, "CR_R", i);
                    sats[i].yx = GetDoubleFromXmlTag(xmlDoc, "CT_R", i);
                    sats[i].yy = GetDoubleFromXmlTag(xmlDoc, "CT_T", i);
                    sats[i].zx = GetDoubleFromXmlTag(xmlDoc, "CN_R", i);
                    sats[i].zy = GetDoubleFromXmlTag(xmlDoc, "CN_T", i);
                    sats[i].zz = GetDoubleFromXmlTag(xmlDoc, "CN_N", i);
                    sats[i].Vxx = GetDoubleFromXmlTag(xmlDoc, "CRDOT_R", i);
                    sats[i].Vxy = GetDoubleFromXmlTag(xmlDoc, "CRDOT_T", i);
                    sats[i].Vxz = GetDoubleFromXmlTag(xmlDoc, "CRDOT_N", i);
                    sats[i].VxVx = GetDoubleFromXmlTag(xmlDoc, "CRDOT_RDOT", i);
                    sats[i].Vyx = GetDoubleFromXmlTag(xmlDoc, "CTDOT_R", i);
                    sats[i].Vyy = GetDoubleFromXmlTag(xmlDoc, "CTDOT_T", i);
                    sats[i].Vyz = GetDoubleFromXmlTag(xmlDoc, "CTDOT_N", i);
                    sats[i].VyVx = GetDoubleFromXmlTag(xmlDoc, "CTDOT_RDOT", i);
                    sats[i].VyVy = GetDoubleFromXmlTag(xmlDoc, "CTDOT_TDOT", i);
                    sats[i].Vzx = GetDoubleFromXmlTag(xmlDoc, "CNDOT_R", i);
                    sats[i].Vzy = GetDoubleFromXmlTag(xmlDoc, "CNDOT_T", i);
                    sats[i].Vzz = GetDoubleFromXmlTag(xmlDoc, "CNDOT_N", i);
                    sats[i].VzVx = GetDoubleFromXmlTag(xmlDoc, "CNDOT_RDOT", i);
                    sats[i].VzVy = GetDoubleFromXmlTag(xmlDoc, "CNDOT_TDOT", i);
                    sats[i].VzVz = GetDoubleFromXmlTag(xmlDoc, "CNDOT_NDOT", i);

                    if (sats[i].VxVx == 0) sats[i].VxVx = small;
                    if (sats[i].VyVy == 0) sats[i].VyVy = small;
                    if (sats[i].VzVz == 0) sats[i].VzVz = small;
                }

                conjunction.Primary = sats[0];
                conjunction.Secondary = sats[1];
                return conjunction;
            }
            catch (Exception e)
            {
                error += e.Message;
                return null;
            }
        }

        private static double GetDoubleValueFromTextLine(string parameterName, int i = 0)
        {
            //CR_AREA_OVER_MASS                  =0.000000                 [m**2/kg]
            if (textFileLines == null || textFileLines.Length == 0)
            {
                return 0;
            }

            double value = 0;
            List<double> values = new List<double>();
            foreach (string line in textFileLines)
            {
                Match match = _doubleRegex.Match(line);                
                if (match.Groups["parameter"].Success
                    && match.Groups["parameter"].Value.ToUpperInvariant().Equals(parameterName.ToUpperInvariant())
                    && match.Groups["value"].Success
                    && double.TryParse(match.Groups["value"].Value, out value))
                {
                    if (match.Groups["units"].Success
                        && (Regex.IsMatch(match.Groups["units"].Value.ToUpperInvariant(), "KM")
                        || Regex.IsMatch(match.Groups["units"].Value.ToUpperInvariant(), "KM/S(EC)?")))
                    {
                        value = value * 1000;
                    }

                    values.Add(value);
                }
            }

            if (values.Count > i)
            {
                return values[i];
            }
            else
            {
                return 0;
            }
            
        }

        

        private static string GetStringValueFromTextLine(string parameterName, int i = 0)
        {
            //CR_AREA_OVER_MASS                  =0.000000                 [m**2/kg]
            if (textFileLines == null || textFileLines.Length == 0)
            {
                return null;
            }

            List<string> values = new List<string>();
            foreach (string line in textFileLines)
            {   
                Match match = _stringRegex.Match(line);
                if (match.Groups["parameter"].Success
                    && match.Groups["parameter"].Value.ToUpperInvariant().Equals(parameterName.ToUpperInvariant())
                    && match.Groups["value"].Success)
                {
                    values.Add(match.Groups["value"].Value.Trim());
                }
            }

            if (values.Count > i)
            {
                return values[i];
            }
            else
            {
                return null;
            }

        }


        private static string[] textFileLines;
        private static Regex _stringRegex;
        private static Regex _doubleRegex;
        

        public static CdmConjunction ReadCdmText(string textPath, out string error)
        {
            error = "";
            try
            {
                CdmConjunction conjunction = new CdmConjunction();
                string _doublePattern = "^(?<parameter>\\S+)\\s+=(\\s+)?(?<value>(-)?\\d+(.\\d+)?)(\\s+?\\[(?<units>\\S+)\\])?";
                _doubleRegex = new Regex(_doublePattern);
                string _stringPattern = "^(?<parameter>\\S+)\\s+=(\\s+)?(?<value>.+)";
                _stringRegex = new Regex(_stringPattern);
                
                textFileLines = File.ReadAllLines(textPath);

                CdmSatellite[] sats = new CdmSatellite[] { new CdmSatellite(), new CdmSatellite() };
                conjunction.CdmFilePath = textPath;
                conjunction.RelPosR = GetDoubleValueFromTextLine("RELATIVE_POSITION_R");
                conjunction.RelPosT = GetDoubleValueFromTextLine("RELATIVE_POSITION_T");
                conjunction.RelPosN = GetDoubleValueFromTextLine("RELATIVE_POSITION_N");
                conjunction.RelVelR = GetDoubleValueFromTextLine("RELATIVE_VELOCITY_R");
                conjunction.RelVelT = GetDoubleValueFromTextLine("RELATIVE_VELOCITY_T");
                conjunction.RelVelN = GetDoubleValueFromTextLine("RELATIVE_VELOCITY_N");
                conjunction.RelativeSpeed = GetDoubleValueFromTextLine("RELATIVE_SPEED");

                conjunction.Probability = GetDoubleValueFromTextLine("COLLISION_PROBABILITY");
                conjunction.ProbabilityMethod = GetStringValueFromTextLine("COLLISION_PROBABILITY_METHOD");
                conjunction.ID = GetStringValueFromTextLine("MESSAGE_ID");
                conjunction.CreationDate = GetStringValueFromTextLine("CREATION_DATE");


                conjunction.MissDistance = GetDoubleValueFromTextLine("MISS_DISTANCE");
                conjunction.TCA = GetStringValueFromTextLine("TCA");
                conjunction.TCA = StkAssistant.ValidateDateFormat(conjunction.TCA);
                conjunction.CreationDate = StkAssistant.ValidateDateFormat(conjunction.CreationDate);

                //if (xmlDoc.GetElementsByTagName("relativeMetadataData").Count > 0)
                //{
                //    foreach (XmlNode node in xmlDoc.GetElementsByTagName("relativeMetadataData")[0].ChildNodes)
                //    {
                //        if (node.Name.ToUpper().Equals("COMMENT"))
                //        {
                //            string metadataComment = node.InnerText;
                //            string pattern = "SIGMA_DILUTION=(?<sigmaDilution>\\d+(.\\d+)?)";

                //            Regex dateRegex = new Regex(pattern);
                //            Match match = dateRegex.Match(metadataComment);
                //            double sd;
                //            if (match.Groups["sigmaDilution"].Success
                //                && double.TryParse(match.Groups["sigmaDilution"].Value, out sd))
                //            {
                //                conjunction.SigmaDilution = sd;
                //            }
                //            break;
                //        }
                //    }
                //}

                for (int i = 0; i < 2; i++)
                {
                    sats[i].isPrimary = i == 0;
                    sats[i].EpochISOYMD = GetStringValueFromTextLine("TCA");
                    sats[i].EpochISOYMD = StkAssistant.ValidateDateFormat(sats[i].EpochISOYMD);
                    // Get satellite names
                    sats[i].SatName = GetStringValueFromTextLine("OBJECT_NAME", i);
                    // Get International Designators
                    sats[i].InternationalDesignator = GetStringValueFromTextLine("INTERNATIONAL_DESIGNATOR", i);
                    // Get Catalog Number
                    sats[i].SSC = GetStringValueFromTextLine("OBJECT_DESIGNATOR", i);
                    if (sats[i].SSC == "KNOWN OBJECT")
                    {
                        sats[i].SatName = "KNOWN OBJECT";
                        sats[i].SSC = "99999";
                    }
                    if (sats[i].SatName.Trim() == "")
                        sats[i].SatName = "UNIDENTIFIED OBJECT";
                    // Get ephemeris check data
                    string ephTemp = GetStringValueFromTextLine("EPHEMERIS_NAME", i);
                    sats[i].EphemerisFile = string.IsNullOrEmpty(ephTemp) || ephTemp.ToUpper().Equals("NONE") ? null :
                        Path.Combine(Path.GetDirectoryName(textPath), ephTemp);

                    // Get Orbital Data
                    sats[i].ReferenceFrame = GetStringValueFromTextLine("REF_FRAME", i);
                    sats[i].XPos = GetDoubleValueFromTextLine("X", i);
                    sats[i].YPos = GetDoubleValueFromTextLine("Y", i);
                    sats[i].ZPos = GetDoubleValueFromTextLine("Z", i);
                    sats[i].XVel = GetDoubleValueFromTextLine("X_DOT", i);
                    sats[i].YVel = GetDoubleValueFromTextLine("Y_DOT", i);
                    sats[i].ZVel = GetDoubleValueFromTextLine("Z_DOT", i);

                    // Get Force Model Definition
                    sats[i].GeopotentialModel = GetStringValueFromTextLine("GRAVITY_MODEL", i);
                    sats[i].LunarsolarPerturbations = GetStringValueFromTextLine("N_BODY_PERTURBATIONS", i);
                    sats[i].SolidEarthTidesPerturbation = GetStringValueFromTextLine("EARTH_TIDES", i);
                    sats[i].SolarRadiationPerturbation = GetStringValueFromTextLine("SOLAR_RAD_PRESSURE", i);
                    sats[i].SolarRadiationCoefficient = GetDoubleValueFromTextLine("CR_AREA_OVER_MASS", i);
                    sats[i].DragModel = GetStringValueFromTextLine("ATMOSPHERIC_MODEL", i);
                    sats[i].BallisticCoefficient = GetDoubleValueFromTextLine("CD_AREA_OVER_MASS", i);
                    sats[i].CovarianceMethod = GetStringValueFromTextLine("COVARIANCE_METHOD", i);

                    sats[i].xx = GetDoubleValueFromTextLine("CR_R", i);
                    sats[i].yx = GetDoubleValueFromTextLine("CT_R", i);
                    sats[i].yy = GetDoubleValueFromTextLine("CT_T", i);
                    sats[i].zx = GetDoubleValueFromTextLine("CN_R", i);
                    sats[i].zy = GetDoubleValueFromTextLine("CN_T", i);
                    sats[i].zz = GetDoubleValueFromTextLine("CN_N", i);
                    sats[i].Vxx = GetDoubleValueFromTextLine("CRDOT_R", i);
                    sats[i].Vxy = GetDoubleValueFromTextLine("CRDOT_T", i);
                    sats[i].Vxz = GetDoubleValueFromTextLine("CRDOT_N", i);
                    sats[i].VxVx = GetDoubleValueFromTextLine("CRDOT_RDOT", i);
                    sats[i].Vyx = GetDoubleValueFromTextLine("CTDOT_R", i);
                    sats[i].Vyy = GetDoubleValueFromTextLine("CTDOT_T", i);
                    sats[i].Vyz = GetDoubleValueFromTextLine("CTDOT_N", i);
                    sats[i].VyVx = GetDoubleValueFromTextLine("CTDOT_RDOT", i);
                    sats[i].VyVy = GetDoubleValueFromTextLine("CTDOT_TDOT", i);
                    sats[i].Vzx = GetDoubleValueFromTextLine("CNDOT_R", i);
                    sats[i].Vzy = GetDoubleValueFromTextLine("CNDOT_T", i);
                    sats[i].Vzz = GetDoubleValueFromTextLine("CNDOT_N", i);
                    sats[i].VzVx = GetDoubleValueFromTextLine("CNDOT_RDOT", i);
                    sats[i].VzVy = GetDoubleValueFromTextLine("CNDOT_TDOT", i);
                    sats[i].VzVz = GetDoubleValueFromTextLine("CNDOT_NDOT", i);

                    if (sats[i].VxVx == 0) sats[i].VxVx = small;
                    if (sats[i].VyVy == 0) sats[i].VyVy = small;
                    if (sats[i].VzVz == 0) sats[i].VzVz = small;
                }

                conjunction.Primary = sats[0];
                conjunction.Secondary = sats[1];
                return conjunction;
            }
            catch (Exception e)
            {
                error += e.Message;
                return null;
            }
        }

    }
}
