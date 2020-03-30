using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AGI.STKUtil;
using AGI.STKVgt;
using ZedGraph;

namespace StkCdmLibrary
{
    public class ZedGraphAssistant
    {
        private static int symbolCounter;
        private static int ColorCounter;
        public static Color[] colorArray;

        public static SymbolType NextSymbol
        {
            get
            {
                return (SymbolType)Enum.GetValues(typeof(SymbolType)).GetValue(++symbolCounter % (Enum.GetValues(typeof(SymbolType)).Length - 1));
            }
        }

        public static Color NextColor
        {
            get
            {
                if (colorArray == null || colorArray.Length == 0)
                {
                    colorArray = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Navy, Color.DarkGray };
                }

                int index = colorArray.Length > 1 ? (++ColorCounter % (colorArray.Length - 1)) : 0;
                return colorArray[index];
            }
        }
        
        public static PointPairList ArrayToPlottableList(string[] times, double[] values)
        {
            if (times.Length != values.Length)
            {
                return null;
            }

            PointPairList points = new PointPairList(times.Select(t => StkAssistant.ParseISOYMD(t).ToOADate()).ToArray(), values);

            return points;
        }

        public static PointPairList CdmPlottableList(string parameterName, params CdmConjunction[] conjunctions)
        {
            List<KeyValuePair<string, double>> stuff = new List<KeyValuePair<string, double>>();
            foreach (CdmConjunction cdm in conjunctions)
            {
                switch (parameterName)
                {
                    case "Miss Distance":
                        stuff.Add(new KeyValuePair<string, double>(cdm.CreationDate, cdm.MissDistance));
                        break;
                    case "TCA":
                        stuff.Add(new KeyValuePair<string, double>(cdm.CreationDate, StkAssistant.ParseISOYMD(cdm.TCA).ToOADate()));
                        break;
                    case "Probability":
                        stuff.Add(new KeyValuePair<string, double>(cdm.CreationDate, cdm.Probability));
                        break;
                    case "Sigma Dilution":
                        stuff.Add(new KeyValuePair<string, double>(cdm.CreationDate, cdm.SigmaDilution));
                        break;
                }
            }


            stuff = stuff.OrderBy(pair => StkAssistant.ParseISOYMD(pair.Key).ToOADate()).ToList();

            PointPairList cdmEvolve = ZedGraphAssistant.ArrayToPlottableList(stuff.Select(kvp => kvp.Key).ToArray(), stuff.Select(kvp => kvp.Value).ToArray());
            return cdmEvolve;
        }
        
        public static TextObj CreateGraphLabel(string text, float x, float y, Color color)
        {
            return CreateGraphLabel(text, x, y, AlignH.Left, AlignV.Top, color);
        }

        public static TextObj CreateGraphLabel(string text, float x, float y, AlignH hAlign, AlignV vAlign, Color color)
        {
            TextObj comment = new TextObj(text, x, y, CoordType.ChartFraction, hAlign, vAlign);
            comment.FontSpec.Border.IsVisible = false;
            comment.FontSpec.Fill.IsVisible = false;
            comment.FontSpec.Family = "Verdana";
            comment.FontSpec.Size = 7;
            comment.FontSpec.FontColor = color;
            return comment;

        }

        public static GraphPane NewBlankGraph(string startISOYMD, string stopISOYMD, string title)
        {

            GraphPane plot = new GraphPane(new RectangleF(0, 0, 1920, 1080), title, "Date/Time (UTC)", "Distance (m)");
            plot.Title.FontSpec.Family = "Verdana";
            plot.Title.FontSpec.Size = 12;
            // Set up x axis
            plot.XAxis.Type = AxisType.Date;
            plot.XAxis.Scale.Format = "MMM d\nHH:mm";
            plot.XAxis.Scale.MajorUnit = DateUnit.Day;
            plot.XAxis.Scale.MinorUnit = DateUnit.Hour;
            plot.XAxis.Scale.MinorStep = 1;
            if (!string.IsNullOrEmpty(startISOYMD) && !string.IsNullOrEmpty(stopISOYMD))
            {
                DateTime startDate = StkAssistant.ParseISOYMD(startISOYMD);
                DateTime endDate = StkAssistant.ParseISOYMD(stopISOYMD);
                plot.XAxis.Scale.Min = startDate.ToOADate();
                plot.XAxis.Scale.Max = endDate.ToOADate();
            }
            else
            {
                plot.XAxis.Scale.MinAuto = true;
                plot.XAxis.Scale.MaxAuto = true;
            }

            plot.XAxis.Title.FontSpec.Family = "Verdana";
            plot.XAxis.Title.FontSpec.Size = 10;
            plot.XAxis.Scale.FontSpec.Family = "Verdana";
            plot.XAxis.Scale.FontSpec.Size = 6;
            plot.XAxis.Scale.FontSpec.Angle = 0;
            plot.XAxis.MajorGrid.IsVisible = true;
            // Set up y axis
            plot.YAxis.Type = AxisType.Linear;
            //if (geoRegime)
            //    plot.YAxis.Scale.Format = "N0";
            //else
            //    plot.YAxis.Scale.Format = "N1";
            plot.YAxis.Scale.Mag = 0;
            plot.YAxis.Scale.MinAuto = true;
            //plot.YAxis.Scale.Min = 0;
            plot.YAxis.Scale.MaxAuto = true;
            plot.YAxis.Title.FontSpec.Family = "Verdana";
            plot.YAxis.Title.FontSpec.Size = 10;
            plot.YAxis.Scale.FontSpec.Family = "Verdana";
            plot.YAxis.Scale.FontSpec.Size = 6;
            plot.YAxis.Scale.Align = AlignP.Inside;
            plot.YAxis.MajorGrid.IsVisible = true;
            // Generate and position legend
            plot.Legend.Position = LegendPos.BottomCenter;
            plot.Legend.FontSpec.Family = "Verdana";
            plot.Legend.FontSpec.Size = 7;
            return plot;
        }

        public static GraphPane CreateGraph(string title, string tcaISOYMD, Graphics g, PointPairList[] points, string[] pointListNames, TextObj[] comments, bool isDashedLine = false)
        {
            ColorCounter = -1;
            GraphPane graph;
            if (!string.IsNullOrEmpty(tcaISOYMD))
            {
                IAgDate tca = StkAssistant.Root.ConversionUtility.NewDate("ISO-YMD", tcaISOYMD);
                graph = NewBlankGraph(tca.Subtract("day", 1).Format("ISO-YMD"), tca.Add("day", 1).Format("ISO-YMD"), title);
            }
            else
            {
                graph = NewBlankGraph(null, null, title);
            }

            graph.Legend.IsVisible = true;
            graph.Legend.Draw(g, graph, .75f);
            foreach (TextObj comment in comments)
            {
                graph.GraphObjList.Add(comment);
            }
            for (int i = 0; i < points.Length; i++)
            {
                LineItem newCurve = new LineItem(pointListNames[i], points[i], NextColor, isDashedLine ? NextSymbol : SymbolType.None, isDashedLine ? 2.0f : 5.0f);
                newCurve.Line.Style = isDashedLine ? System.Drawing.Drawing2D.DashStyle.Dash : System.Drawing.Drawing2D.DashStyle.Solid;
                graph.CurveList.Add(newCurve);
            }

            graph.AxisChange(g);

            if (!string.IsNullOrEmpty(tcaISOYMD))
            {
                PointPairList tcaLine = new PointPairList();
                tcaLine.Add(StkAssistant.ParseISOYMD(tcaISOYMD).ToOADate(), graph.YAxis.Scale.Min);
                tcaLine.Add(StkAssistant.ParseISOYMD(tcaISOYMD).ToOADate(), graph.YAxis.Scale.Max);
                graph.AddCurve("", tcaLine, Color.Black);
            }

            //graph.GetImage().Save(path, ImageFormat.Png);

            return graph;

        }

        public static GraphPane GenericSatComparisonGraph(Graphics g, string sat1Path, string sat2Path, string centerEpochISOYMD)
        {
            IAgCrdnVector diff = StateCompare.GetVectorBetweenObjects(sat1Path, sat1Path, sat2Path);
            object[] diffMin = StateCompare.GetTimeOfMinAndValue(diff);
            string timeOfMinRange = (string)diffMin.GetValue(0);
            double minRange = ((double)diffMin.GetValue(1));
            double[] ric = StateCompare.GetRICDifferenceAtTCA(sat1Path, sat2Path, centerEpochISOYMD);
            double rangeAtTCA = Math.Sqrt(ric[0] * ric[0] + ric[1] * ric[1]+ ric[2] * ric[2]);

            TextObj stktcaComment = ZedGraphAssistant.CreateGraphLabel("Time of Min Range: " + timeOfMinRange, .01f, .01f, Color.Black);
            TextObj stkmissComment = ZedGraphAssistant.CreateGraphLabel("Minimum Range: " + minRange.ToString(), .01f, .05f, Color.Black);
            TextObj tcaComment = ZedGraphAssistant.CreateGraphLabel("Comparison Time : " + centerEpochISOYMD, 1f, .01f, AlignH.Right, AlignV.Top, Color.Black);
            TextObj tcaRangeComment = ZedGraphAssistant.CreateGraphLabel("Range : " + rangeAtTCA.ToString(), 1f, .05f, AlignH.Right, AlignV.Top, Color.Black);
            TextObj[] comments = new TextObj[] { stktcaComment, stkmissComment, tcaComment, tcaRangeComment };

            StateCompare.RICResults ricOverTime = StateCompare.GetRICDifferenceOverTime(sat1Path, sat2Path, centerEpochISOYMD);
            PointPairList pplR = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.R);
            PointPairList pplI = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.I);
            PointPairList pplC = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.C);
            PointPairList pplRange = ZedGraphAssistant.ArrayToPlottableList(ricOverTime.Times, ricOverTime.Range);
            
            PointPairList[] ppl = new PointPairList[] { pplR, pplI, pplC, pplRange};
            string[] pplNames = new string[] { "Radial", "In-Track", "Cross-Track", "Range", "" };
            string sat1Name = sat1Path.Substring(sat1Path.LastIndexOf("/") + 1);
            string sat2Name = sat2Path.Substring(sat2Path.LastIndexOf("/") + 1);

            return ZedGraphAssistant.CreateGraph("Comparison: " + sat1Name + " to " + sat2Name, centerEpochISOYMD, g, ppl, pplNames, comments);
        }
    }
}
