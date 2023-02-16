using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKVgt;
using AGI.Ui.Plugins;

namespace Stk12.UiPlugin.TetkExample
{
    public partial class TrackComparisonUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;
        string trackName;
        string truthObjName;
        string measuredObjName;

        public TrackComparisonUserInterface()
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
            //CommonData.StkRoot.OnStkObjectAdded -= m_root_OnStkObjectAdded;
            //CommonData.StkRoot.OnStkObjectDeleted -= m_root_OnStkObjectDeleted;
        }

        public void OnSaveModified()
        {

        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as Setup;
            m_stkObjectsLibrary = new StkObjectsLibrary();

            PopulateComboBoxes();
        }

        #endregion

        void PopulateComboBoxes()
        {
            string simpleName;
            string className;
            StringCollection objectNames = m_stkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");

            foreach (string objectName in objectNames)
            {
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(objectName);

                // Populate track selection combo box
                if (className == "MTO")
                {
                    simpleName = m_stkObjectsLibrary.TruncatedObjectPath(objectName);
                    simpleName = simpleName.Substring(4); // Trim off class name "MTO/"
                    comboBox_TcTracks.Items.Add(simpleName);
                }

                if (className == "Ground Vehicle" || className == "Ship" || className == "Aircraft" || className == "Facility" || className == "Missile" || className == "Place" || className == "Satellite" || className == "Target")
                {
                    simpleName = m_stkObjectsLibrary.TruncatedObjectPath(objectName);
                    comboBox_TruthObj.Items.Add(simpleName);
                    comboBox_MeasuredObj.Items.Add(simpleName);
                }
            }
        }

        private void button_ComputeTrackComparison_Click(object sender, EventArgs e)
        {
            trackName = comboBox_TcTracks.Text;
            truthObjName = comboBox_TruthObj.Text;
            measuredObjName = comboBox_MeasuredObj.Text;
            string cmd = String.Format(@"TE_TrackComparison * Add Name ""TC_{0}"" AnalysisObject ""F35"" Track ""{0}"" TruthObject ""{1}"" TruthPointing ""StkObject"" MeasuredObject ""{2}"" ReferenceSystem ""Aircraft/F35 NorthEastDown System"" TimePath ""{1} AvailabilityTimeSpan EventInterval""", trackName, truthObjName, measuredObjName);
            CommonData.StkRoot.ExecuteCommand(cmd);
            CommandList.cmdList.Add(cmd);
        }

        private void button_CreateSlantRangeDiffVsTimeGraph_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd = String.Format(@"TE_Graph * Add Name ""Slant_Range_Diff_Over_Time_TC_{0}"" AnalysisObject ""F35"" GraphXY Segment Color ""Blue"" DataElement ""TC_{0}_RangeDifference"" XIsTime Labels ""Slant Range Difference"" Units ""ft"" Step ""{1}"" AnimationLine ""Black""", trackName, measuredObjName);
                CommonData.StkRoot.ExecuteCommand(cmd);
                CommandList.cmdList.Add(cmd);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error:" + exception);
                return;
            }
        }

        private void button_CreateMissAngleVsTimeGraph_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd = String.Format(@"TE_Graph * Add Name ""Miss_Angle_Over_Time_TC_{0}"" AnalysisObject ""F35"" GraphXY Segment Color ""Red"" DataElement ""TC_{0}_MissAngle"" XIsTime Labels ""Miss Angle"" Units ""deg"" Step ""{1}"" AnimationLine ""Black""", trackName, measuredObjName);
                CommonData.StkRoot.ExecuteCommand(cmd);
                CommandList.cmdList.Add(cmd);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error:" + exception);
                return;
            }
        }

        private void button_CreateAzimuthDifferenceDataDisplay_Click(object sender, EventArgs e)
        {
            // May need to create track comparison event interval first to then use as a time constraint in the data display
            IAgStkObject aircraftOwnship = CommonData.StkRoot.GetObjectFromPath("Aircraft/F35");
            IAgStkObject measuredObject = CommonData.StkRoot.GetObjectFromPath(measuredObjName);
            if (aircraftOwnship.Vgt.EventIntervals.Contains(String.Format("TC_{0}_ComparisonInterval", trackName)))
            {
                // do nothing
            }
            else
            {
                IAgCrdnEventInterval eventInterval = aircraftOwnship.Vgt.EventIntervals.Factory.CreateEventIntervalFromIntervalList(String.Format("TC_{0}_ComparisonInterval", trackName), "Single time interval created from reference interval list.");
                IAgCrdnEventIntervalFromIntervalList eventIntervalFromList = eventInterval as IAgCrdnEventIntervalFromIntervalList;
                eventIntervalFromList.ReferenceIntervals = measuredObject.Vgt.EventIntervalLists["AvailabilityIntervals"];
                eventIntervalFromList.IntervalSelection = AgECrdnIntervalSelection.eCrdnIntervalSelectionSpan;
            }

            try
            {
                string cmd = String.Format(@"TE_DataDisplay * Add Name ""Azimuth_Difference_TC_{0}"" AnalysisObject ""F35"" Numeric DataElement ""TC_{0}_AzimuthDifference, Azimuth Angle Error, deg, On"" Color ""Yellow"" Location ""1, 10, 10"" Digits ""3"" Width ""Auto"" TimeConstraint ""TC_{0}_ComparisonInterval""", trackName);
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
