using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using StkCdmLibrary;
using ZedGraph;

namespace CDMComparison
{
    public partial class CdmTrendViewer : Form
    {
        private Graphics g;
        private CdmConjunction[] cdmCollection;
        public CdmTrendViewer(CdmConjunction[] cdms)
        {
            InitializeComponent();
            cdmCollection = cdms;
            g = this.CreateGraphics();
            TrendCDMs();
        }

        private void TrendCDMs()
        {
            ZedGraphAssistant.colorArray = new Color[] { Color.Red};
            PointPairList pplMD = ZedGraphAssistant.CdmPlottableList("Miss Distance", cdmCollection);
            GraphPane missDisPlot = ZedGraphAssistant.CreateGraph("Conjunction Trending", null, g, 
                new PointPairList[] { pplMD }, new string[] { "Miss Distance" }, new TextObj[] { }, true);
            missDisPlot.YAxis.Title.Text = "Miss Distance (m)";
            missDisPlot.XAxis.Title.Text = "Creation Date (UTC)";

            zgMissDistance.GraphPane = missDisPlot;
            zgMissDistance.AxisChange();
            zgMissDistance.Invalidate();

            ZedGraphAssistant.colorArray = new Color[] { Color.Blue };
            PointPairList pplTca = ZedGraphAssistant.CdmPlottableList("TCA", cdmCollection);
            GraphPane tcaPlot = ZedGraphAssistant.CreateGraph("Conjunction Trending", null, g, new PointPairList[] { pplTca }, new string[] { "TCA" }, new TextObj[] { }, true);
            tcaPlot.YAxis.Type = AxisType.Date;
            tcaPlot.YAxis.Scale.Format = "MMM d\nHH:mm:ss.sss";
            tcaPlot.YAxis.Scale.MajorUnit = DateUnit.Minute;
            tcaPlot.YAxis.Scale.MinorUnit = DateUnit.Second;
            tcaPlot.YAxis.Scale.MinorStep = 10;
            tcaPlot.YAxis.Title.Text = "Time of Close Approach (UTC)";
            tcaPlot.XAxis.Title.Text = "Creation Date (UTC)";
            zgTCA.GraphPane = tcaPlot;
            zgTCA.AxisChange();
            zgTCA.Invalidate();

            ZedGraphAssistant.colorArray = new Color[] { Color.Green};
            PointPairList pplProb = ZedGraphAssistant.CdmPlottableList("Probability", cdmCollection);
            GraphPane probPlot = ZedGraphAssistant.CreateGraph("Conjunction Trending", null, g, new PointPairList[] { pplProb }, new string[] { "Probability" }, new TextObj[] { }, true);
            probPlot.YAxis.Title.Text = "Probability";
            probPlot.XAxis.Title.Text = "Creation Date (UTC)";
            zgProbability.GraphPane = probPlot;
            zgProbability.AxisChange();
            zgProbability.Invalidate();

            ZedGraphAssistant.colorArray = new Color[] { Color.Orange };
            PointPairList pplSD = ZedGraphAssistant.CdmPlottableList("Sigma Dilution", cdmCollection);
            GraphPane sdPlot = ZedGraphAssistant.CreateGraph("Conjunction Trending", null, g, new PointPairList[] { pplSD }, new string[] { "Sigma Dilution" }, new TextObj[] { }, true);
            sdPlot.YAxis.Title.Text = "Sigma Dilution (m)";
            sdPlot.XAxis.Title.Text = "Creation Date (UTC)";
            zgSigma.GraphPane = sdPlot;
            zgSigma.AxisChange();
            zgSigma.Invalidate();

            ZedGraphAssistant.colorArray = new Color[] { Color.Red, Color.Blue };
            GraphPane combinedPlot = ZedGraphAssistant.CreateGraph("Conjunction Trending", null, g, new PointPairList[] { pplMD }, new string[] { "Miss Distance" }, new TextObj[] { }, true);
            LineItem y2LineTCA = combinedPlot.AddCurve("TCA", pplTca, Color.Blue, ZedGraphAssistant.NextSymbol);
            y2LineTCA.IsY2Axis = true;
            y2LineTCA.Line.Width = 2.0f;
            y2LineTCA.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            combinedPlot.XAxis.Title.Text = "Creation Date (UTC)";
            combinedPlot.Y2Axis.Type = AxisType.Date;
            combinedPlot.Y2Axis.Scale.Format = "MMM d\nHH:mm:ss.sss";
            combinedPlot.Y2Axis.Scale.MajorUnit = DateUnit.Day;
            combinedPlot.Y2Axis.Scale.MinorUnit = DateUnit.Minute;
            combinedPlot.Y2Axis.Scale.MinorStep = 5;
            combinedPlot.Y2Axis.Scale.MinAuto = true;
            combinedPlot.Y2Axis.Scale.MaxAuto = true;
            combinedPlot.Y2Axis.Scale.FontSpec.Size = combinedPlot.YAxis.Scale.FontSpec.Size;
            combinedPlot.Y2Axis.Title.FontSpec = combinedPlot.YAxis.Title.FontSpec;
            combinedPlot.Y2Axis.Title.Text = "Time of Close Approach (UTC)";
            combinedPlot.Y2Axis.IsVisible = true;
            zgCombined.GraphPane = combinedPlot;
            zgCombined.AxisChange();
            zgCombined.Invalidate();

            SetSize();
            string safeName = Regex.Replace(cdmCollection[0].Primary.SatName +"and"+ cdmCollection[0].Secondary.SatName, "[///(/)" + new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + " ]", "");
            string outputDir = Path.Combine(CdmReader.CdmOutputDirectory, safeName);
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            if (probPlot != null) probPlot.GetImage(1280, 1024, 180).Save(Path.Combine(outputDir, "Probability.png"), System.Drawing.Imaging.ImageFormat.Png);
            if (tcaPlot != null) tcaPlot.GetImage(1280, 1024, 180).Save(Path.Combine(outputDir, "TCA.png"), System.Drawing.Imaging.ImageFormat.Png);
            if (missDisPlot != null) missDisPlot.GetImage(1280, 1024, 180).Save(Path.Combine(outputDir, "MissDistance.png"), System.Drawing.Imaging.ImageFormat.Png);
            if (combinedPlot != null) combinedPlot.GetImage(1280, 1024, 180).Save(Path.Combine(outputDir, "TCAandMissDistance.png"), System.Drawing.Imaging.ImageFormat.Png);
            if (sdPlot != null) sdPlot.GetImage(1280, 1024, 180).Save(Path.Combine(outputDir, "SigmaDilution.png"), System.Drawing.Imaging.ImageFormat.Png);
            
        }

        private void SetSize()
        {
            zgMissDistance.Size = new Size(this.missDistance.ClientRectangle.Width,
                    this.missDistance.ClientRectangle.Height);

            zgTCA.Size = new Size(this.tca.ClientRectangle.Width,
                    this.tca.ClientRectangle.Height);

            zgProbability.Size = new Size(this.probability.ClientRectangle.Width,
                    this.probability.ClientRectangle.Height);

            zgCombined.Size = new Size(this.combined.ClientRectangle.Width,
                    this.combined.ClientRectangle.Height);

            zgSigma.Size = new Size(this.sigmaDilution.ClientRectangle.Width,
                    this.sigmaDilution.ClientRectangle.Height);
        }

        private void CdmTrendViewer_Load(object sender, EventArgs e)
        {
            SetSize();
        }
        private int snapCount = 0;
        private string Snap
        {
            get{ return "_snap" + (++snapCount).ToString(); }
        }
        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            string safeName = Regex.Replace(cdmCollection[0].Primary.SatName + "and" + cdmCollection[0].Secondary.SatName, "[///(/)" + new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + " ]", "");
            string outputDir = Path.Combine(CdmReader.CdmOutputDirectory, safeName);
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            switch (tabControl1.SelectedTab.Name)
            {
                case "combined":
                    zgCombined.GraphPane.GetImage(1280, 1024, 180).Save(Path.Combine(outputDir, "TCAandMissDistance"+ Snap +".png")
                        , System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case "tca":
                    zgTCA.GraphPane.GetImage(1280, 1024, 180).Save(Path.Combine(outputDir, "TCA" + Snap + ".png"), System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case "missDistance":
                    zgMissDistance.GraphPane.GetImage(1280, 1024, 180).Save(Path.Combine(outputDir, "MissDistance" + Snap + ".png"), System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case "probability":
                    zgProbability.GraphPane.GetImage(1280, 1024, 180).Save(Path.Combine(outputDir, "Probability" + Snap + ".png"), System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case "sigmaDilution":
                    zgSigma.GraphPane.GetImage(1280, 1024, 180).Save(Path.Combine(outputDir, "SigmaDilution" + Snap + ".png"), System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }
        }
    }
}
