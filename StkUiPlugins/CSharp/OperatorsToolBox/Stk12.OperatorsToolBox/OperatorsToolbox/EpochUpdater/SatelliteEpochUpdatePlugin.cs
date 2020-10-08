using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.EpochUpdater
{
    public partial class SatelliteEpochUpdatePlugin : OpsPluginControl
    {
        public string[] ParsingFormats = new string[] { "dd MMM yyyy HH:mm:ss", "d MMM yyyy HH:mm:ss", "dd MMM yyyy HH:mm:ss.000", "d MMM yyyy HH:mm:ss.000" }; //add others to this as we find them
        List<string> _satelliteUpdateList;
        IAgScenario _cScenario = null;

        public SatelliteEpochUpdatePlugin()
        {
            InitializeComponent();
            _satelliteUpdateList = new List<string>();
            ASTGRunCheck.Checked = false;
            _cScenario = CommonData.StkRoot.CurrentScenario as IAgScenario;

            dtp_start.Value = Convert.ToDateTime(_cScenario.StartTime.ToString());
            dtp_end.Value = Convert.ToDateTime(_cScenario.StopTime.ToString());

            CreatorFunctions.PopulateObjectListByClass(SatelliteList, "Satellite");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            int errorCount = 0;
            string errorStr = "Could not update epoch for the following objects: \n";
            foreach (var item in _satelliteUpdateList)
            {
                IAgSatellite sat = CommonData.StkRoot.GetObjectFromPath(item) as IAgSatellite;
                try
                {
                    CreatorFunctions.ChangeSatelliteInterval(sat, dtp_start.Value.ToString("dd MMM yyyy HH:mm:ss.000"), dtp_end.Value.ToString("dd MMM yyyy HH:mm:ss.000"), ASTGRunCheck.Checked);
                }
                catch (Exception)
                {
                    errorCount++;
                    errorStr += item + "\n";
                }
            }
            if (errorCount>0)
            {
                MessageBox.Show(errorStr);
            }
        }

        private void ASTGRunCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AddSatellite_Click(object sender, EventArgs e)
        {
            if (SatelliteList.FocusedItem!=null && SatelliteList.FocusedItem.Index!=-1)
            {
                foreach (int index in SatelliteList.SelectedIndices)
                {
                    SatelliteList.Items[index].Font = new Font(SatelliteList.Items[index].Font, FontStyle.Bold);
                    string className = "Satellite";
                    if (!_satelliteUpdateList.Contains(className + "/" + SatelliteList.Items[index].SubItems[0].Text))
                    {
                        _satelliteUpdateList.Add(className + "/" + SatelliteList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void RemoveSatellite_Click(object sender, EventArgs e)
        {
            if (SatelliteList.FocusedItem != null && SatelliteList.FocusedItem.Index != -1)
            {
                foreach (int index in SatelliteList.SelectedIndices)
                {
                    SatelliteList.Items[index].Font = new Font(SatelliteList.Items[index].Font, FontStyle.Regular);
                    string className = "Satellite";
                    if (_satelliteUpdateList.Contains(className + "/" + SatelliteList.Items[index].SubItems[0].Text))
                    {
                        _satelliteUpdateList.Remove(className + "/" + SatelliteList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
