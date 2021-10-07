using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// AGI references
using AGI.Ui.Application;
using AGI.STKObjects;
using AGI.STKUtil;

using ReentryCalculator.Utility;
using System.IO;

namespace ReentryCalculator
{
    public partial class frmMain : Form
    {
        // global variables
        AgStkObjectRoot root;
        AgUiApplication app;
        TLE currentTle;
        InitialState initState;
        Data satData;
        Uncertainty uncertainty;
        double nRuns;
        List<PropagationResults> resultList;
        string propagatorName;

        public frmMain()
        {
            InitializeComponent();
        }


        #region TLE Scenario
        private void btnCreateScenarioTle_Click(object sender, EventArgs e)
        {
            // create an instance of the BackgroundWorker class
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;

            // add the event handlers to the BackgroundWorker instance's events
            bw.DoWork += new DoWorkEventHandler(createScenarioTleDoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(createScenarioTleCompleted);

            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.statusLabel.Text = "Creating scenario...";
            // start running the background operation by calling the RunWorkerAsync method.
            bw.RunWorkerAsync();
        }

        private void createScenarioTleDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            app = new AgUiApplication();
            app.LoadPersonality("STK");
            root = (AgStkObjectRoot)app.Personality2;
            initState = new InitialState();
            satData = new Data(Convert.ToDouble(tbMass.Text), Convert.ToDouble(tbDragArea.Text), Convert.ToDouble(tbCd.Text), Convert.ToDouble(tbSolarArea.Text), Convert.ToDouble(tbCr.Text));
            STK.CreateScenarioFromTle(ref root, Convert.ToDouble(this.nudDuration.Value), ref initState, currentTle);
            STK.ConfigurePropagator(root);
            root.Rewind();
        }

        private void createScenarioTleCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar.Style = ProgressBarStyle.Blocks;
            this.statusLabel.Text = "Scenario created";
            this.gbSatData.Enabled = true;
            this.gbUncertainty.Enabled = true;
            this.gbAnalysis.Enabled = true;

        }
        #endregion

        #region SATCAT Scenario
        private void btnCreateScenarioSatcat_Click(object sender, EventArgs e)
        {
            // create an instance of the BackgroundWorker class
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;

            // add the event handlers to the BackgroundWorker instance's events
            bw.DoWork += new DoWorkEventHandler(createScenarioSatcatDoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(createScenarioSatcatCompleted);

            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.statusLabel.Text = "Creating scenario...";
            // start running the background operation by calling the RunWorkerAsync method.
            bw.RunWorkerAsync();
        }

        private void createScenarioSatcatDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            app = new AgUiApplication();
            app.LoadPersonality("STK");
            root = (AgStkObjectRoot)app.Personality2;
            initState = new InitialState();
            satData = new Data(Convert.ToDouble(tbMass.Text), Convert.ToDouble(tbDragArea.Text), Convert.ToDouble(tbCd.Text), Convert.ToDouble(tbSolarArea.Text), Convert.ToDouble(tbCr.Text));
            string satelliteID = this.dgvSatList.SelectedRows[0].Cells[1].Value.ToString();

            currentTle = STK.CreateScenarioFromSatcat(ref root, Convert.ToDouble(this.nudDuration.Value), ref initState, satelliteID);

            STK.ConfigurePropagator(root);
            root.Rewind();
        }

        private void createScenarioSatcatCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lblTleEpoch.Text = currentTle.GetTleEpoch().ToString("dd MMM yyyy hh:mm:ss.fff");
            this.lblSatNumber.Text = currentTle.GetSatNumber();
            this.lblInclination.Text = currentTle.GetInclination();
            this.lblEccentricity.Text = currentTle.GetEccentricity();
            this.lblRevNumber.Text = currentTle.GetRevNumber();
            this.lblMeanMotion.Text = currentTle.GetMeanMotion();

            this.progressBar.Style = ProgressBarStyle.Blocks;
            this.statusLabel.Text = "Scenario created";
            this.gbSatData.Enabled = true;
            this.gbUncertainty.Enabled = true;
            this.gbAnalysis.Enabled = true;

        }
        #endregion

        #region Run Analysis
        private void btnRunAnalysis_Click(object sender, EventArgs e)
        {
            resultList = new List<PropagationResults>();
            // define the uncertainty level
            uncertainty = new Uncertainty(Convert.ToDouble(tbIntrackPosUnc.Text), Convert.ToDouble(tbRadialPosUnc.Text), Convert.ToDouble(tbCrosstrackPosUnc.Text),
                                          Convert.ToDouble(tbIntrackVelUnc.Text), Convert.ToDouble(tbRadialVelUnc.Text), Convert.ToDouble(tbCrosstrackVelUnc.Text));
            // get the number of runs
            nRuns = Convert.ToDouble(this.nudNRuns.Value);
            try
            {
                // get the propagator to use
                if (this.cbPropagator.SelectedItem.ToString().Equals("Jacchia-Roberts"))
                {
                    propagatorName = "Jacchia-Roberts";
                }
                else if (this.cbPropagator.SelectedItem.ToString().Equals("NRLMSISE 2000"))
                {
                    propagatorName = "NRLMSISE 2000";
                }

                this.tbEventLogger.AppendText("Reentry analysis - Input data" + Environment.NewLine);
                this.tbEventLogger.AppendText("Satellite catalog number: " + currentTle.GetSatNumber() + Environment.NewLine);
                this.tbEventLogger.AppendText("TLE epoch: " + currentTle.GetTleEpoch() + Environment.NewLine);
                this.tbEventLogger.AppendText("Satellite mass: " + this.tbMass.Text + Environment.NewLine);
                this.tbEventLogger.AppendText("Density model: " + this.cbPropagator.SelectedItem.ToString() + Environment.NewLine);
                this.tbEventLogger.AppendText("Satellite drag area: " + this.tbDragArea.Text + Environment.NewLine);
                this.tbEventLogger.AppendText("Drag coefficient: " + this.tbCd.Text + Environment.NewLine);
                this.tbEventLogger.AppendText("Satellite solar area: " + this.tbSolarArea.Text + Environment.NewLine);
                this.tbEventLogger.AppendText("SRP coefficient: " + this.tbCr.Text + Environment.NewLine);
                this.tbEventLogger.AppendText(Environment.NewLine);

                // create an instance of the BackgroundWorker class
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;

                // add the event handlers to the BackgroundWorker instance's events
                bw.DoWork += new DoWorkEventHandler(runAnalysisDoWork);
                bw.ProgressChanged += new ProgressChangedEventHandler(runAnalysisProgressChanged);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(runAnalysisCompleted);

                this.progressBar.Style = ProgressBarStyle.Blocks;
                this.statusLabel.Text = "Analysis started...";
                // start running the background operation by calling the RunWorkerAsync method.
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select a propagator type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void runAnalysisDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 0; i < nRuns; i++)
            {
                PropagationResults results = STK.PropagateAstrogatorSatellite(root, app, initState, currentTle, satData, uncertainty, i + 1, Convert.ToInt32(nRuns), propagatorName);
                resultList.Add(results);

                // report the progress
                worker.ReportProgress((i + 1) * 100 / (Convert.ToInt32(nRuns)));
            }
        }

        private void runAnalysisProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PropagationResults results = resultList[resultList.Count - 1];
            this.tbEventLogger.AppendText("Run ID: " + results.RunNumber + Environment.NewLine);
            this.tbEventLogger.AppendText("Satellite Decayed: " + results.IsDecayed + Environment.NewLine);
            if (results.IsDecayed)
            {
                this.tbEventLogger.AppendText("Impact Epoch: " + results.ImpactEpoch.Split('.')[0] + Environment.NewLine);
                this.tbEventLogger.AppendText("Impact Latitude (deg): " + results.ImpactLat + Environment.NewLine);
                this.tbEventLogger.AppendText("Impact Longitude (deg): " + results.ImpactLon + Environment.NewLine);
                this.tbEventLogger.AppendText(Environment.NewLine);
            }
            this.tbEventLogger.ScrollToCaret();

            this.progressBar.Value = e.ProgressPercentage;
            this.statusLabel.Text = "Propagating satellites... " + e.ProgressPercentage + "%.";
        }

        private void runAnalysisCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar.Value = 0;
            string[] results = STK.CalculateStatistics(root, resultList);
            this.tbEventLogger.AppendText("Statisitcal summary" + Environment.NewLine);
            this.tbEventLogger.AppendText("Mean Impact Epoch: " + results[0] + Environment.NewLine);
            this.tbEventLogger.AppendText("1-Sigma St. Dev. (min): " + results[1] + Environment.NewLine);
            this.tbEventLogger.AppendText(Environment.NewLine);
            this.tbEventLogger.ScrollToCaret();
            root.Rewind();
            this.statusLabel.Text = "Analysis ended";
            this.btnGenerateMto.Enabled = true;
            this.btnCalculateFoM.Enabled = true;
        }
        #endregion

        #region GenerateMto

        private void btnGenerateMto_Click(object sender, EventArgs e)
        {
            // create an instance of the BackgroundWorker class
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;

            // add the event handlers to the BackgroundWorker instance's events
            bw.DoWork += new DoWorkEventHandler(GenerateMtoDoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GenerateMtoCompleted);
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.statusLabel.Text = "Creating MTO... ";
            // start running the background operation by calling the RunWorkerAsync method.
            bw.RunWorkerAsync();
        }

        private void GenerateMtoDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                STK.CreateMto(root, resultList);
            }
            catch (Exception)
            {
                MessageBox.Show("MTO already exists", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GenerateMtoCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar.Value = 0;
            this.progressBar.Style = ProgressBarStyle.Blocks;
            string[] results = STK.CalculateStatistics(root, resultList);
            this.statusLabel.Text = "MTO created";
            root.Rewind();
        }
        #endregion

        #region Calculate FOM
        private void btnCalculateFoM_Click(object sender, EventArgs e)
        {
            // create an instance of the BackgroundWorker class
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;

            // add the event handlers to the BackgroundWorker instance's events
            bw.DoWork += new DoWorkEventHandler(CalculateFoMDoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CalculateFoMCompleted);
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.statusLabel.Text = "Calculating Coverage... ";
            // start running the background operation by calling the RunWorkerAsync method.
            bw.RunWorkerAsync();
        }

        private void CalculateFoMDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                STK.CreateCoverage(root, currentTle);
            }
            catch (Exception)
            {
                MessageBox.Show("Coverage already calculated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CalculateFoMCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar.Value = 0;
            this.progressBar.Style = ProgressBarStyle.Blocks;
            string[] results = STK.CalculateStatistics(root, resultList);
            this.statusLabel.Text = "Coverage calculated";
            root.Rewind();
        }
        #endregion


        private void btnImportTle_Click(object sender, EventArgs e)
        {
            if (openTleFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentTle = new TLE(openTleFileDialog.FileName);
                this.lblTleEpoch.Text = currentTle.GetTleEpoch().ToString("dd MMM yyyy hh:mm:ss.fff");
                this.lblSatNumber.Text = currentTle.GetSatNumber();
                this.lblInclination.Text = currentTle.GetInclination();
                this.lblEccentricity.Text = currentTle.GetEccentricity();
                this.lblRevNumber.Text = currentTle.GetRevNumber();
                this.lblMeanMotion.Text = currentTle.GetMeanMotion();

                this.btnCreateScenarioTle.Enabled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.tbKeyword.Text.ToUpper();
            string satcatFilePath = Directory.GetCurrentDirectory() + "\\data\\satcat.txt";

            if (!File.Exists(satcatFilePath))
            {
                MessageBox.Show("SATCAT file not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.dgvSatList.Rows.Clear();
                StreamReader satcatFile = new StreamReader(satcatFilePath);
                while (satcatFile.Peek() > -1)
                {
                    string line = satcatFile.ReadLine();
                    if (line.Contains(keyword))
                    {
                        this.dgvSatList.Rows.Add(line.Substring(0, 11).TrimEnd(' '), line.Substring(13, 5).TrimEnd(' '), line.Substring(23, 26).TrimEnd(' '));
                    }
                }
            }
        }

        private void dgvSatList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.btnCreateScenarioSatcat.Enabled = true;
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tbEventLogger.Clear();
        }

        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "text files (*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Save File to .txt
                FileStream fParameter = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter m_WriterParameter = new StreamWriter(fParameter);
                m_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
                m_WriterParameter.Write(this.tbEventLogger.Text);
                m_WriterParameter.Flush();
                m_WriterParameter.Close();
            }
        }
    }      
}
