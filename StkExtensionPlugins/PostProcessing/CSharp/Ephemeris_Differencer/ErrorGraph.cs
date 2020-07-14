using System;
using System.Collections.Generic;
using System.Windows.Forms;
// The reference for the ZedGraph charting package
using ZedGraph;
using System.Drawing;


namespace EphemerisDifferencer
{
    public partial class ErrorGraph : Form
    {
        public ErrorGraph()
        {
            InitializeComponent();
        }

        public ZedGraph.ZedGraphControl PositionErrorGraph
        {
            get { return m_PositionErrorGraph; }
        }
        public ZedGraph.ZedGraphControl VelocityErrorGraph
        {
            get { return m_VelocityErrorGraph; }
        }

        public static void UpdateGraph(ZedGraph.ZedGraphControl graph, string title, IList<double> epSec, IList<double>[] yList)
        {
            graph.GraphPane.Title.Text = title;
            if (title.Contains("RIC"))
            {
                AddLine(graph, "Radial error", Color.Red, epSec, yList[0]);
                AddLine(graph, "In-Track error", Color.Green, epSec, yList[1]);
                AddLine(graph, "Cross-Track error", Color.Blue, epSec, yList[2]);
            }
            else
            {
                AddLine(graph, "X error", Color.Red, epSec, yList[0]);
                AddLine(graph, "Y error", Color.Green, epSec, yList[1]);
                AddLine(graph, "Z error", Color.Blue, epSec, yList[2]);
            }
        }

        public static void AddLine(ZedGraph.ZedGraphControl graph, string name, Color color, IList<double> xList, IList<double> yList)
        {
            PointPairList dataList = new PointPairList();
            double xMin = 1.0e20;
            double xMax = 0.0;
            double yMin = 1.0e20;
            double yMax = 0.0;
            for (int i = 0; i < Math.Min(xList.Count, yList.Count); i++)
            {
                PointPair data = new PointPair(xList[i], yList[i]);
                dataList.Add(data);
                if (xList[i] < xMin) xMin = xList[i];
                if (xList[i] > xMax) xMax = xList[i];
                if (yList[i] < yMin) yMin = yList[i];
                if (yList[i] > yMax) yMax = yList[i];
            }            
        
            // Now that the DOPData PointPairList array is filled in, we can update the DOP graph
            GraphPane myPane = graph.GraphPane;

            // use the AddCurve method to add a new series of data to an existing graph
            // The text supplied in the first parameter will be used in the legend.
            LineItem myCurve = myPane.AddCurve(name, dataList, color, SymbolType.None);
            // Fill the symbols with the same color as the lines
            //myCurve.Symbol.Fill = new Fill(color);            
            myCurve.Line.Color = color;
            myCurve.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            
            // update the X-Axis Min and Max values.  XDate is a ZedGraph type that takes a .Net DateTime structure.
            // Use the JulianDate method ToDateTime to retrieve this structure from a JulianDate
            graph.GraphPane.XAxis.Scale.Min = xMin;
            graph.GraphPane.XAxis.Scale.Max = xMax;
            graph.GraphPane.YAxis.Scale.MinAuto = true;
            graph.GraphPane.YAxis.Scale.MaxAuto = true;

            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = true;

            // make x-axis a date type
            myPane.XAxis.Type = AxisType.Linear;

            // Custom Axis string for display
            //myPane.XAxis.Scale.Format = "dd-MMM\nhh:mm";

            // change the angle of the string if desired
            //myPane.XAxis.Scale.FontSpec.Angle = 40;            

            // set the Axis Title
            myPane.XAxis.Title.Text = "Epoch Seconds";
            myPane.YAxis.Title.Text = "Error";
            
            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.Scale.Align = AlignP.Inside;

            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

            // Enable scrollbars if needed
            graph.IsShowHScrollBar = true;
            graph.IsShowVScrollBar = true;
            graph.IsAutoScrollRange = true;
            graph.IsScrollY2 = true;

            // Make sure the Graph gets redrawn
            graph.AxisChange();
            graph.Invalidate();
        }

        private void ErrorGraphBox_Load(object sender, EventArgs e)
        {

        }
    }
}
