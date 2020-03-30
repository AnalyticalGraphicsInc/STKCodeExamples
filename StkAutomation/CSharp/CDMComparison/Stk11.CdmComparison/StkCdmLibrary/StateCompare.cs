using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
using ZedGraph;

namespace StkCdmLibrary
{
    public static class StateCompare
    {
        public static double[] GetRICDifferenceAtTCA(string sat1Path, string sat2Path, string epochISOYMD)
        {   
            IAgStkObject primary, secondary;
            try
            {
                primary = StkAssistant.Root.GetObjectFromPath(sat1Path);
                secondary = StkAssistant.Root.GetObjectFromPath(sat2Path);
            }
            catch
            {
                return null;
            }
            IAgDataProviderInfo dpInfo = primary.DataProviders["RIC Coordinates"];

            IAgDataProvider dataProvider = primary.DataProviders["RIC Coordinates"] as IAgDataProvider;
            dataProvider.PreData = secondary.Path.Replace(StkAssistant.Root.CurrentScenario.Path, "");
            IAgDataPrvTimeVar dpTimeVarying = dataProvider as IAgDataPrvTimeVar;
            Array elements = new object[] { "Radial", "In-Track", "Cross-Track" };
            IAgDrResult dpResult = dpTimeVarying.ExecSingleElements(epochISOYMD, elements);
            double[] ric = new double[3];
            ric[0] = (double)dpResult.DataSets[0].GetValues().GetValue(0) ;
            ric[1] = (double)dpResult.DataSets[1].GetValues().GetValue(0);
            ric[2] = (double)dpResult.DataSets[2].GetValues().GetValue(0) ;

            return ric;
        }

        public struct RICResults
        {
            public string[] Times;
            public double[] R, I, C, Range;
        }

        public static RICResults GetRICDifferenceOverTime(string sat1Path, string sat2Path, string epochISOYMD)
        {
            IAgDate tca = StkAssistant.Root.ConversionUtility.NewDate("ISO-YMD", epochISOYMD);

            IAgStkObject primary, secondary;
            try
            {
                primary = StkAssistant.Root.GetObjectFromPath(sat1Path);
                secondary = StkAssistant.Root.GetObjectFromPath(sat2Path);
            }
            catch
            {
                return new RICResults();
            }

            double period1 = StkAssistant.GetSatellitePeriod(primary.Path, epochISOYMD);
            double period2 = StkAssistant.GetSatellitePeriod(secondary.Path, epochISOYMD);

            double period = (period1 + period2) / 2;

            IAgDataProviderInfo dpInfo = primary.DataProviders["RIC Coordinates"];

            IAgDataProvider dataProvider = primary.DataProviders["RIC Coordinates"] as IAgDataProvider;
            dataProvider.PreData = secondary.Path.Replace(StkAssistant.Root.CurrentScenario.Path, "");
            IAgDataPrvTimeVar dpTimeVarying = dataProvider as IAgDataPrvTimeVar;
            Array elements = new object[] { "Time", "Radial", "In-Track", "Cross-Track", "Range" };
            IAgDrResult dpResult = dpTimeVarying.ExecElements(
                tca.Subtract("sec", .5 * period).Format("ISO-YMD"), 
                tca.Add("sec", .5 * period).Format("ISO-YMD"), 
                10, elements);
            RICResults ricResults = new RICResults();

            foreach (IAgDrDataSet dataset in dpResult.DataSets)
            {
                if (dataset.ElementName.Equals("Time"))
                {
                    List<string> times = new List<string>();
                    foreach (object item in dataset.GetValues())
                    {
                        times.Add(item.ToString());
                    }
                    ricResults.Times = times.ToArray();
                }
                else
                {
                    List<double> values = new List<double>();
                    foreach (object item in dataset.GetValues())
                    {
                        values.Add((double)item);
                    }

                    switch (dataset.ElementName)
                    {
                        case "Radial":
                            ricResults.R = values.ToArray();
                            break;
                        case "In-Track":
                            ricResults.I = values.ToArray();
                            break;
                        case "Cross-Track":
                            ricResults.C = values.ToArray();
                            break;
                        case "Range":
                            ricResults.Range = values.ToArray();
                            break;
                        default:
                            break;
                    }
                }
            }

            return ricResults;            
        }


        public static object[] GetTimeOfMinAndValue(IAgCrdnVector vector)
        {
            IAgStkObject parentObject = StkAssistant.Root.GetObjectFromPath("*/" + (vector as IAgCrdn).QualifiedPath.Split(' ')[0]);
            //Create new scalar to monitor Vector Magnitude value
            string newVgtBaseName = (vector as IAgCrdn).Name;
            string scalarVectorMagName = StkAssistant.GetUniqueAWBName(newVgtBaseName, parentObject, AgECrdnKind.eCrdnKindCalcScalar);
            IAgCrdnCalcScalar vectorMagnitudeScalar = parentObject.Vgt.CalcScalars.Factory.Create(
                scalarVectorMagName, "temp", AgECrdnCalcScalarType.eCrdnCalcScalarTypeVectorMagnitude);
            (vectorMagnitudeScalar as IAgCrdnCalcScalarVectorMagnitude).InputVector = vector;

            IAgCrdnCalcScalar minRange = (vectorMagnitudeScalar as IAgCrdn).EmbeddedComponents[(vectorMagnitudeScalar as IAgCrdn).Name + ".Min"] as IAgCrdnCalcScalar;
            IAgCrdnEvent timeOfMinRange = (vectorMagnitudeScalar as IAgCrdn).EmbeddedComponents[(vectorMagnitudeScalar as IAgCrdn).Name + ".TimeOfMin"] as IAgCrdnEvent;
            string tca = timeOfMinRange.FindOccurrence().Epoch.ToString();
            double minRangeValue = minRange.Evaluate(tca).Value;

            return new object[] { tca, minRangeValue };
        }
        public static IAgCrdnVector GetVectorBetweenObjects(string parentObjectPath, string fromObjectPath, string toObjectPath,
            string vectorName = "", bool addDisplay = false)
        {
            IAgStkObject parentObject = StkAssistant.Root.GetObjectFromPath(parentObjectPath);
            IAgStkObject fromObject = StkAssistant.Root.GetObjectFromPath(fromObjectPath);
            IAgStkObject toObject = StkAssistant.Root.GetObjectFromPath(toObjectPath);

            return GetVectorBetweenObjects(parentObject, fromObject, toObject, vectorName, addDisplay);

        }

        public static IAgCrdnVector GetVectorBetweenObjects(IAgStkObject parentObject, IAgStkObject fromObject, IAgStkObject toObject,
            string vectorName = "", bool addDisplay = false)
        {
            IAgCrdnVectorFactory vectorFactory = parentObject.Vgt.Vectors.Factory;

            string newVectorName;
            if (String.IsNullOrEmpty(vectorName))
            {
                newVectorName = "To_" + toObject.InstanceName;
            }
            else
            {
                newVectorName = vectorName;
            }

            IAgCrdnVectorDisplacement displacementVector;
            if (fromObject.Vgt.Vectors.Contains(newVectorName))
            {
                displacementVector = fromObject.Vgt.Vectors[newVectorName] as IAgCrdnVectorDisplacement;
            }
            else
            {
                displacementVector = vectorFactory.CreateDisplacementVector(newVectorName,
                   fromObject.Vgt.Points["Center"], toObject.Vgt.Points["Center"]);
            }

            if (addDisplay)
            {
                StkAssistant.DisplayVector((displacementVector as IAgCrdn).Name, parentObject, System.Drawing.Color.Yellow);
            }

            return displacementVector as IAgCrdnVector;
        }

        public static object[] GetTcaRangeInfo(string sat1Path, string sat2Path)
        {
            IAgCrdnVector diff = StateCompare.GetVectorBetweenObjects(sat1Path, sat1Path, sat2Path);
            object[] diffMin = StateCompare.GetTimeOfMinAndValue(diff);
            string stkTimeOfMinRange = (string)diffMin.GetValue(0);
            double stkMinimumRange = ((double)diffMin.GetValue(1));

            return new object[] { stkTimeOfMinRange, stkMinimumRange };
        }

        public static IAgCrdnAngle CreateAngleBetweenVectors(IAgCrdnVector fromVector, IAgCrdnVector toVector,
            IAgStkObject parentObject, string name = "", bool addDisplay = false)
        {
            IAgCrdnAngleFactory angleFactory = parentObject.Vgt.Angles.Factory;

            string newAngleName;
            if (!String.IsNullOrEmpty(name))
            {
                newAngleName = name;
            }
            else
            {
                newAngleName = "From_" + (fromVector as IAgCrdn).Name + "_To_" + (toVector as IAgCrdn).Name;
            }

            IAgCrdnAngleBetweenVectors angle;
            if (parentObject.Vgt.Angles.Contains(newAngleName))
            {
                angle = parentObject.Vgt.Angles[newAngleName] as IAgCrdnAngleBetweenVectors;
            }
            else
            {
                angle = angleFactory.Create(newAngleName, newAngleName,
                    AgECrdnAngleType.eCrdnAngleTypeBetweenVectors) as IAgCrdnAngleBetweenVectors;
                angle.FromVector.SetVector(fromVector);
                angle.ToVector.SetVector(toVector);
            }

            if (addDisplay)
            {
                StkAssistant.DisplayAngle(newAngleName, parentObject, System.Drawing.Color.Yellow);
            }

            return angle as IAgCrdnAngle;
        }
    }
}
