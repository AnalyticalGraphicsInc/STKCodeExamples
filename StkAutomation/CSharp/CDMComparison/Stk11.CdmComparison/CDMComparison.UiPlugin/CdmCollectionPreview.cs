using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CDMComparison.Properties;
using StkCdmLibrary;
using ComspocDownloader;

namespace CDMComparison
{
    public partial class CdmCollectionPreview : Form
    {

        List<CdmConjunction> cdmCollection;

        public CdmCollectionPreview()
        {
            InitializeComponent();
            InitializeDGV();

            dtpCreateDate.Format = DateTimePickerFormat.Custom;
            dtpCreateDate.CustomFormat = "yyyy-MM-dd hh:mm:ss";

        }

        private DirectoryInfo dir;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            
            cdmCollection = new List<CdmConjunction>();

            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                dir = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                lblDirectory.Text = folderBrowserDialog1.SelectedPath;
                this.Refresh();

                PopulateCdmView();
                
            }
        }


        private void PopulateCdmView()
        {
            string error;
            string[] extensions = new string[] { ".xml", ".cfe" };
            FileInfo[] cdms = dir.GetFiles().ToList().Where(f => extensions.Contains(f.Extension)).ToArray();
            foreach (FileInfo cdmFile in cdms)
            {
                CdmConjunction cdm = null;
                switch (cdmFile.Extension)
                {
                    case ".xml":
                        cdm = CdmReader.ReadCdmXmlByTag(cdmFile.FullName, out error);
                        break;
                    case ".cfe":
                        cdm = CdmReader.ReadCdmText(cdmFile.FullName, out error);
                        break;
                    default:
                        break;
                }

                if (cdm != null) cdmCollection.Add(cdm);
            }

            cdmCollection.Sort(delegate(CdmConjunction a, CdmConjunction b)
            {
                if ((a != null && b == null)
                    || (a == null && b == null))
                {
                    return 0;
                }
                else if (a == null && b != null)
                {
                    return 1;
                }

                if (a.Primary == null
                    || a.Primary.SatName == null) return 0;
                if (b.Primary == null
                    || b.Primary.SatName == null) return 1;

                int xdiff = a.Primary.SatName.CompareTo(b.Primary.SatName);
                if (xdiff != 0)
                {
                    return xdiff;
                }

                if (a.Secondary == null
                    || a.Secondary.SatName == null
                    || a.TCA == null) return 0;
                if (b.Secondary == null
                    || b.Secondary.SatName == null) return 1;

                xdiff = a.Secondary.SatName.CompareTo(b.Secondary.SatName);
                if (xdiff != 0)
                {
                    return xdiff;
                }

                if (a.TCA == null) return 0;
                if (b.TCA == null) return 1;
                return a.TCA.CompareTo(b.TCA);


            });

            dgvCDMs.Rows.Clear();
            foreach (CdmConjunction cdm in cdmCollection)
            {
                AddRow(cdm);
            }
        }

        private const string nameCreate = "CreateColumn";
        private const string nameID = "IdColumn";
        private const string namePrimary = "PrimaryColumn";
        private const string nameSecondary = "SecondaryColumn";
        private const string namePrimarySsc = "SscPrimaryColumn";
        private const string nameSecondarySsc = "SscSecondaryColumn";
        private const string nameTCA = "TcaColumn";
        private const string nameMissDis = "MissDistanceColumn";
        private const string nameProbability = "ProbabilityColumn";
        private const string nameSigma = "SigmaDilutionColumn";
        private void InitializeDGV()
        {
            DataGridViewTextBoxColumn colCreateDate = new DataGridViewTextBoxColumn();
            colCreateDate.Name = nameCreate;
            colCreateDate.HeaderText = "Create Date";
            colCreateDate.ReadOnly = true;
            colCreateDate.Width = 100;
            colCreateDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn colID = new DataGridViewTextBoxColumn();
            colID.Name = nameID;
            colID.HeaderText = "CDM ID";
            colID.ReadOnly = true;
            colID.Width = 100;
            colID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn colPrimary = new DataGridViewTextBoxColumn();
            colPrimary.Name = namePrimary;
            colPrimary.HeaderText = "Primary";
            colPrimary.ReadOnly = true;
            colPrimary.Width = 100;
            colPrimary.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn colPrimarySsc = new DataGridViewTextBoxColumn();
            colPrimary.Name = namePrimarySsc;
            colPrimary.HeaderText = "Primary SSc";
            colPrimary.ReadOnly = true;
            colPrimary.Width = 100;
            colPrimary.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn colSecondary = new DataGridViewTextBoxColumn();
            colSecondary.Name = nameSecondary;
            colSecondary.HeaderText = "Secondary";
            colSecondary.ReadOnly = true;
            colSecondary.Width = 100;
            colSecondary.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn colSecondarySsc = new DataGridViewTextBoxColumn();
            colSecondary.Name = nameSecondarySsc;
            colSecondary.HeaderText = "Secondary SSC";
            colSecondary.ReadOnly = true;
            colSecondary.Width = 100;
            colSecondary.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn colTCA = new DataGridViewTextBoxColumn();
            colTCA.Name = nameTCA;
            colTCA.HeaderText = "TCA";
            colTCA.ReadOnly = true;
            colTCA.Width = 100;
            colTCA.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn colMissDis = new DataGridViewTextBoxColumn();
            colMissDis.Name = nameMissDis;
            colMissDis.HeaderText = "Miss Distance";
            colMissDis.ReadOnly = true;
            colMissDis.Width = 100;
            colMissDis.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn colProb = new DataGridViewTextBoxColumn();
            colProb.Name = nameProbability;
            colProb.HeaderText = "Probability";
            colProb.ReadOnly = true;
            colProb.Width = 100;
            colProb.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn colSigma = new DataGridViewTextBoxColumn();
            colSigma.Name = nameSigma;
            colSigma.HeaderText = "Sigma Dilution";
            colSigma.ReadOnly = true;
            colSigma.Width = 100;
            colSigma.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvCDMs.Columns.Add(colCreateDate);
            dgvCDMs.Columns.Add(colID); 
            dgvCDMs.Columns.Add(colPrimary);
            dgvCDMs.Columns.Add(colPrimarySsc);
            dgvCDMs.Columns.Add(colSecondary);
            dgvCDMs.Columns.Add(colSecondarySsc);
            dgvCDMs.Columns.Add(colTCA);
            dgvCDMs.Columns.Add(colMissDis);
            dgvCDMs.Columns.Add(colProb);
            dgvCDMs.Columns.Add(colSigma);
        }

        private void AddRow(CdmConjunction cdm)
        {
            if (cbAutoEstimate.Checked 
                && ( cdm.Probability == 0 || cdm.SigmaDilution == 0))
            {
                cdm.EstimateProbability();
            }

            int rowIndex = dgvCDMs.Rows.Add(new string[]
                    {
                        cdm.CreationDate, cdm.ID, cdm.Primary.SatName, cdm.Primary.SSC, cdm.Secondary.SatName, cdm.Secondary.SSC, cdm.TCA, cdm.MissDistance.ToString(), cdm.Probability.ToString(), cdm.SigmaDilution.ToString()
                    });

            dgvCDMs.Rows[rowIndex].Tag = cdm;
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            ExecuteSingleCdmAnalysis();
        }

        private void ExecuteSingleCdmAnalysis()
        {
            foreach (DataGridViewRow row in dgvCDMs.SelectedRows)
            {
                if (row.Tag is CdmConjunction)
                {
                    CdmViewer cdmAnalysis = new CdmViewer(row.Tag as CdmConjunction);
                    cdmAnalysis.StartPosition = FormStartPosition.CenterParent;
                    //cdmAnalysis.Parent = this;
                    cdmAnalysis.Show();
                }
            }
        }

        private void dgvCDMs_DoubleClick(object sender, EventArgs e)
        {
            ExecuteSingleCdmAnalysis();
        }

        private void btnMixMatch_Click(object sender, EventArgs e)
        {
            if (dgvCDMs.SelectedRows.Count == 2)
            {   
                CdmConjunction cdm1 = dgvCDMs.SelectedRows[0].Tag as CdmConjunction;
                CdmConjunction cdm2 = dgvCDMs.SelectedRows[1].Tag as CdmConjunction;

                CdmConjunction cdmMix1 = new CdmConjunction(cdm1);
                CdmConjunction cdmMix2 = new CdmConjunction(cdm2);
                cdmMix1.Primary = new CdmSatellite(cdm1.Primary);
                cdmMix1.Primary.SatName = "m1_" + cdm1.Primary.SatName;
                cdmMix1.Secondary = new CdmSatellite(cdm2.Secondary);
                cdmMix1.Secondary.SatName = "m1_" + cdm2.Secondary.SatName;
                cdmMix2.Primary = new CdmSatellite(cdm2.Primary);
                cdmMix2.Primary.SatName = "m2_" + cdm2.Primary.SatName;
                cdmMix2.Secondary = new CdmSatellite(cdm1.Secondary);
                cdmMix2.Secondary.SatName = "m2_" + cdm1.Secondary.SatName;

                CdmViewer cdm1Analysis = new CdmViewer(cdmMix1);
                cdm1Analysis.StartPosition = FormStartPosition.CenterParent;
                //cdmAnalysis.Parent = this;
                cdm1Analysis.Show();

                CdmViewer cdm2Analysis = new CdmViewer(cdmMix2);
                cdm2Analysis.StartPosition = FormStartPosition.CenterParent;
                //cdmAnalysis.Parent = this;
                cdm2Analysis.Show();


            }
            else
            {
                MessageBox.Show("Please select 2, and only 2, CDMs to Mix and Match");
            }
        }

        private void btnTrend_Click(object sender, EventArgs e)
        {
            if (dgvCDMs.SelectedRows.Count > 1)
            {
                List<CdmConjunction> cdms = new List<CdmConjunction>();
                foreach (DataGridViewRow row in dgvCDMs.SelectedRows)
                {
                    if (row.Tag is CdmConjunction)
                    {
                        CdmConjunction cdm = row.Tag as CdmConjunction;
                        cdms.Add(cdm);
                    }
                }

                CdmTrendViewer cdmTrend = new CdmTrendViewer(cdms.ToArray());
                cdmTrend.StartPosition = FormStartPosition.CenterParent;
                //cdmTrend.Parent = this;
                cdmTrend.Show();
            }
            else
            {
                MessageBox.Show("Please select multiple rows to trend");
            }
        }

        private void btnBinCdms_Click(object sender, EventArgs e)
        {
            List<CdmConjunction> cdms = new List<CdmConjunction>();
            foreach (DataGridViewRow row in dgvCDMs.SelectedRows)
            {
                cdms.Add(row.Tag as CdmConjunction);
            }

            CdmConjunction.BinConjunctionsByRange(dir.FullName, cdms.Distinct().ToArray());
        }

        private void btnConjunctionBin_Click(object sender, EventArgs e)
        {
            List<CdmConjunction> cdms = new List<CdmConjunction>();
            foreach (DataGridViewRow row in dgvCDMs.SelectedRows)
            {
                cdms.Add(row.Tag as CdmConjunction);
            }

            CdmConjunction.BinConjunctions(dir.FullName, cdms.ToArray());
        }

        private void btnGetCDMsFromComspoc_Click(object sender, EventArgs e)
        {
            if (dir == null)
            {
                MessageBox.Show("Please choose a working directory");
                return;
            }
            List<int> caJobs = new List<int>();
            string[] jobs = txtJobNumber.Text.Split(',');

            foreach (string jobString in jobs)
            {
                string[] jobRange = jobString.Split('-');

                int jobStart, jobStop, jobID;
                if (jobRange.Length == 2
                    && int.TryParse(jobRange[0], out jobStart)
                    && int.TryParse(jobRange[1], out jobStop))
                {
                    for (int i = jobStart; i <= jobStop; i++)
                    {
                        caJobs.Add(i);
                    }
                }
                else if (int.TryParse(jobString, out jobID)) 
                {
                    caJobs.Add(jobID);
                }
            }
            
            ComspocDownloader.
            ComspocHelper.GetConjunctionsForJobAndUri(dir.FullName, caJobs.Select(j => (long)j).ToArray());
            PopulateCdmView();
            

        }

        private void btnGetCDMsFromJSPOC_Click(object sender, EventArgs e)
        {
            if (dir == null)
            {
                MessageBox.Show("Please choose a working directory");
                return;
            }

            JspocHelper.GetConjunctionsForCreateDate(dir.FullName, dtpCreateDate.Text.Replace(' ', 'T'), Settings.Default.SpaceTrackUser, Settings.Default.SpaceTrackPassword);
            PopulateCdmView();
            
        }


        
    }
}
