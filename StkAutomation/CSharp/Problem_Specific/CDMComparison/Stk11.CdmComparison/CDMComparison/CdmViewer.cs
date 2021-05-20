using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using StkCdmLibrary;
using ZedGraph;
using System.Collections.Generic;
using System.Text;

namespace CDMComparison
{
    public partial class CdmViewer : Form
    {
        private CdmConjunction conjunction;
        private Graphics g;
        private string dirPath;
        public CdmViewer()
        {
            InitializeComponent();
            g = this.CreateGraphics();
        }
        public CdmViewer(CdmConjunction cdm)
        {
            InitializeComponent();
            g = this.CreateGraphics();
            conjunction = cdm;

            ExecuteSingleCDMComparison();
        }

      

        private void SetSize()
        {
            zConjunction.Size = new Size(this.cdmGraph.ClientRectangle.Width,
                    this.cdmGraph.ClientRectangle.Height);

            zPrimary.Size = new Size(this.primary.ClientRectangle.Width,
                    this.primary.ClientRectangle.Height);

            zSecondary.Size = new Size(this.secondary.ClientRectangle.Width,
                    this.secondary.ClientRectangle.Height);
        }

        private void ExecuteSingleCDMComparison()
        {
            if (conjunction == null) return;
            string safeName = Regex.Replace(conjunction.Primary.SatName +"and"+ conjunction.Secondary.SatName + "_"+ conjunction.ID, "[///(/)" + new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + " ]", "");
            
            conjunction.GenerateStkCdmObjects();
            string conjunctionIdFileName = safeName;//Regex.Replace(conjunction.ID,"[///(/)" + new string(Path.GetInvalidFileNameChars())+ new string(Path.GetInvalidPathChars()) + " ]", "");
            dirPath = Path.Combine(CdmReader.CdmOutputDirectory, conjunctionIdFileName);
            DirectoryInfo dir = Directory.CreateDirectory(dirPath);

            ZedGraphAssistant.colorArray = null;
            GraphPane conjunctionGraph = conjunction.GraphConjunction(g);
            GraphPane primaryGraph = conjunction.Primary.GraphSatelliteStateComparison(g);
            GraphPane secondaryGraph = conjunction.Secondary.GraphSatelliteStateComparison(g);            

            if (conjunctionGraph != null)
            {
                //conjunctionGraph.ReSize(g, zConjunction.GraphPane.Rect);
                zConjunction.GraphPane = conjunctionGraph;
                zConjunction.AxisChange();
                zConjunction.Invalidate();
            }

            if (primaryGraph != null)
            {
                //primaryGraph.ReSize(g, zPrimary.GraphPane.Rect);
                zPrimary.GraphPane = primaryGraph;
                zPrimary.AxisChange();
                zPrimary.Invalidate();
            }

            if (secondaryGraph != null)
            {
                //secondaryGraph.ReSize(g, zSecondary.GraphPane.Rect);
                zSecondary.GraphPane = secondaryGraph;
                zSecondary.AxisChange();
                zSecondary.Invalidate();
            }

            SetSize();

            if (conjunctionGraph != null) conjunctionGraph.GetImage(1280, 1024, 180).Save(Path.Combine(dirPath, "Conjunction.png"), System.Drawing.Imaging.ImageFormat.Png);
            if (primaryGraph != null) primaryGraph.GetImage(1280, 1024, 180).Save(Path.Combine(dirPath, "Primary.png"), System.Drawing.Imaging.ImageFormat.Png);
            if (secondaryGraph != null) secondaryGraph.GetImage(1280, 1024, 180).Save(Path.Combine(dirPath, "Secondary.png"), System.Drawing.Imaging.ImageFormat.Png);

            
            string summary = "Conjunction for " + conjunction.Primary.SSC + "/" + conjunction.Primary.SatName + "[+] and "
                + conjunction.Secondary.SSC + "/" + conjunction.Secondary.SatName + "[-]";
            summary += Environment.NewLine;
            summary += Environment.NewLine + "CDM Summary";
            summary += Environment.NewLine + "Created       : " + conjunction.CreationDate;
            summary += Environment.NewLine + "TCA           : " + conjunction.TCA;
            summary += Environment.NewLine + "TCA Range (m) : " + conjunction.MissDistance;

            summary += Environment.NewLine;
            summary += Environment.NewLine + "STK Validation of CDM";
            summary += Environment.NewLine + "STK Range at CDM TCA (m) : " + conjunction.StkRangeAtCdmTca;
            summary += Environment.NewLine + "STK TCA                  : " + conjunction.StkTimeOfMinRange;
            summary += Environment.NewLine + "STK Min Range (m)        : " + conjunction.MissDistance;

            string[] conjunctionToSecondariesSummary = conjunction.SummaryOfConjunctionsFromSecondaryStates();

            if (conjunctionToSecondariesSummary.Length > 0)
            {
                summary += Environment.NewLine + Environment.NewLine + "Conjunction Comparisons :";
                foreach (string item in conjunctionToSecondariesSummary)
                {
                    summary += Environment.NewLine + item;
                }
            }
            txtCdmResults.Text = summary;
            File.WriteAllText(Path.Combine(dirPath, "CdmSummary.txt"), summary);
        }
    

        private void TestUI_Resize(object sender, EventArgs e)
        {
            SetSize();
        }
        

        public static string ConvertDataTableToHtml(string[][] targetTable)
        {
            string htmlString = "";
            if (targetTable == null)
            {
                throw new System.ArgumentNullException("targetTable");
            }

            StringBuilder htmlBuilder = new StringBuilder();
            //Create Top Portion of HTML Document
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<head>");
            htmlBuilder.Append("<title>");
            htmlBuilder.Append("Page-");
            htmlBuilder.Append(Guid.NewGuid().ToString());
            htmlBuilder.Append("</title>");
            htmlBuilder.Append("</head>");
            htmlBuilder.Append("<body>");
            htmlBuilder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            htmlBuilder.Append("style='border: solid 1px Black; font-size: small;'>");
            //Create Header Row
            //htmlBuilder.Append("<tr align='left' valign='top'>");
            //foreach (string[] targetColumn in targetTable)
            //{
            //    htmlBuilder.Append("<td align='left' valign='top'>");
            //    htmlBuilder.Append(targetColumn.ColumnName);
            //    htmlBuilder.Append("</td>");
            //}
            //htmlBuilder.Append("</tr>");
            //Create Data Rows
            foreach (string[] row in targetTable)
            {
                htmlBuilder.Append("<tr align='left' valign='top'>");
                foreach (string rowItem in row)
                {
                    htmlBuilder.Append("<td align='left' valign='top'>");
                    htmlBuilder.Append(rowItem);
                    htmlBuilder.Append("</td>");
                }
                htmlBuilder.Append("</tr>");
            }
            //Create Bottom Portion of HTML Document
            htmlBuilder.Append("</table>");
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");
            //Create String to be Returned

            htmlString = htmlBuilder.ToString();
            return htmlString;

        }
        private int snapCount = 0;
        private string Snap
        {
            get { return "_snap" + (++snapCount).ToString(); }
        }
        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            switch (tabControl1.SelectedTab.Name)
            {
                case "cdmGraph":
                    zConjunction.GraphPane.GetImage(1280, 1024, 180).Save(Path.Combine(dirPath, "Conjunction" + Snap + ".png")
                        , System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case "primary":
                    zPrimary.GraphPane.GetImage(1280, 1024, 180).Save(Path.Combine(dirPath, "Primary" + Snap + ".png"), System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case "secondary":
                    zSecondary.GraphPane.GetImage(1280, 1024, 180).Save(Path.Combine(dirPath, "Secondary" + Snap + ".png"), System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conjunction.EstimateProbability();
        }
    }
}

