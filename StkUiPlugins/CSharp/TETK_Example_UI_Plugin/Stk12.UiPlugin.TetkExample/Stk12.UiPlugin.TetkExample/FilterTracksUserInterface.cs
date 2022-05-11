using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.Ui.Plugins;

namespace Stk12.UiPlugin.TetkExample
{
    public partial class CreateTracksUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;

        public CreateTracksUserInterface()
        {
            InitializeComponent();
        }

        #region IAgUiPluginEmbeddedControl Implementation
        public stdole.IPictureDisp GetIcon()
        {
            return null;
        }

        public void OnClosing()
        {
            CommonData.StkRoot.OnStkObjectAdded -= m_root_OnStkObjectAdded;
            CommonData.StkRoot.OnStkObjectDeleted -= m_root_OnStkObjectDeleted;
        }

        public void OnSaveModified()
        {

        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as Setup;
            m_stkObjectsLibrary = new StkObjectsLibrary();

            CommonData.StkRoot.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            CommonData.StkRoot.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);

            // Populate combo box with current MTO objects
            PopulateComboBox_TracksToPromote();
        }

        #endregion

        void m_root_OnStkObjectDeleted(object Sender)
        {
            string objectPath = Sender.ToString();
            string simpleName = m_stkObjectsLibrary.TruncatedObjectPath(objectPath);

            if (objectPath.Contains("MTO"))
                comboBox_TracksToPromote.Items.Remove(simpleName.Substring(4));

        }

        void m_root_OnStkObjectAdded(object Sender)
        {
            string objectPath = Sender.ToString();
            string simpleName = m_stkObjectsLibrary.TruncatedObjectPath(objectPath).Remove(0, 0);

            if (objectPath.Contains("MTO"))
                comboBox_TracksToPromote.Items.Add(simpleName.Substring(4));
        }

        private void button_AddRawTrack_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd = String.Format(@"TE_Track * Add Name ""RadarTrack_Raw"" AnalysisObject ""F35"" Mapping ""Flight385_Tracks"" PickMapping ""Flight385_Tracks_PickInfo""");
                CommonData.StkRoot.ExecuteCommand(cmd);
                CommandList.cmdList.Add(cmd);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error:" + exception);
                return;
            }

        }

        private void button_AddIndividualTrack_Click(object sender, EventArgs e)
        {
            if (comboBox_TrackID.Text != "")
            {
                string selectedTrackId = comboBox_TrackID.Text;

                try
                {
                    string cmd = String.Format(@"TE_Track * Add Name ""RadarTrack_{0}"" AnalysisObject ""F35"" Mapping ""Flight385_Tracks"" PickMapping ""Flight385_Tracks_PickInfo"" TrackIDs ""{0}""", selectedTrackId);
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    CommandList.cmdList.Add(cmd);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error:" + exception);
                    return;
                }
            }
        }

        private void button_AddAllTracks_Click(object sender, EventArgs e)
        {
            int numTracks = comboBox_TrackID.Items.Count;
            for (int i = 0; i < numTracks; i++)
            {
                try
                {
                    string trackId = comboBox_TrackID.Items[i].ToString();
                    string cmd = String.Format(@"TE_Track * Add Name ""RadarTrack_{0}"" AnalysisObject ""F35"" Mapping ""Flight385_Tracks"" PickMapping ""Flight385_Tracks_PickInfo"" TrackIDs ""{0}""", trackId);
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    CommandList.cmdList.Add(cmd);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error:" + exception);
                    return;
                }
            }
        }

        void PopulateComboBox_TracksToPromote()
        {
            string simpleName;
            string className;
            StringCollection objectNames = m_stkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");

            foreach (string objectName in objectNames)
            {
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className == "MTO")
                {
                    simpleName = m_stkObjectsLibrary.TruncatedObjectPath(objectName);
                    simpleName = simpleName.Substring(4);
                    comboBox_TracksToPromote.Items.Add(simpleName);
                }
            }
        }

        private void button_PromoteTrack_Click(object sender, EventArgs e)
        {
            if (comboBox_TracksToPromote.Text != "")
            {
                string selectedTrackId = comboBox_TracksToPromote.Text;

                try
                {
                    string cmd = String.Format(@"TE_Track * Promote Name ""{0}""", selectedTrackId);
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    CommandList.cmdList.Add(cmd);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error:" + exception);
                    return;
                }
            }
        }
    }
}
