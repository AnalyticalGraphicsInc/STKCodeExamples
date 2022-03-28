using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Threading;

namespace Agi.UiPlugin.MoveMtoTime
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AgStkObjectRoot m_root;
        private MoveMtoTime m_uiPlugin;

        public CustomUserInterface()
        {
            InitializeComponent();
            
        }

        #region IAgUiPluginEmbeddedControl Members

        public stdole.IPictureDisp GetIcon()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void OnClosing()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void OnSaveModified()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as MoveMtoTime;
            m_root = m_uiPlugin.STKRoot;
            DisplayUI();

        }

        #endregion


        private void DisplayUI()
        {
            // clear all previous results
            mtoTreeView.Nodes.Clear();

            // check if scenario is loaded
            if (m_root.CurrentScenario == null)
            {
                MessageBox.Show("Please load a scenario.");
            }
            else
            {
                // get all MTOs
                IAgStkObjectElementCollection mtos = m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eMTO);

                if (mtos.Count > 0)
                {
                    foreach (IAgStkObject thisMto in mtos)
                    {
                        TreeNode thisNode = mtoTreeView.Nodes.Add(thisMto.InstanceName);
                        thisNode.Checked = true;

                        // get tracks
                        IAgMto thisRealMto = thisMto as IAgMto;
                        foreach (IAgMtoTrack thisTrack in thisRealMto.Tracks)
                        {
                            TreeNode thisSubNode = thisNode.Nodes.Add(thisTrack.Id.ToString());
                            thisSubNode.Checked = true;
                        }
                    }
                }


            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            DisplayUI();
        }

        // check all subnodes if parent is checked
        private void mtoTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode subNode in e.Node.Nodes)
                {
                    if (e.Node.Checked)
                    {
                        subNode.Checked = true;
                    }
                    else
                    {
                        subNode.Checked = false;
                    }
                }
            }
        }

        // move the time
        private void moveButton_Click(object sender, EventArgs e)
        {
            // get time
            double days = Convert.ToDouble(daysTextBox.Text);
            double hour = Convert.ToDouble(hoursTextBox.Text);
            double mins = Convert.ToDouble(minutesTextBox.Text);
            double secs = Convert.ToDouble(secondsTextBox.Text);
            double totalSecs = days*86400.0 + hour*3600.0 + mins*60.0 + secs;
            Int32 fullSecs = Convert.ToInt32(Math.Floor(totalSecs));
            Int32 mSecs = Convert.ToInt32((totalSecs - Math.Floor(totalSecs))*1000.0);

            TimeSpan timeShift = new TimeSpan(0, 0, 0, fullSecs, mSecs);
            double timeShiftDirection = 1.0;
            if (earlierRadioButton.Checked)
            {
                timeShiftDirection = -1.0;
            }

            // loop through all treenodes
            foreach (TreeNode thisNode in mtoTreeView.Nodes)
            {
                IAgMto thisMto = m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eMTO)[thisNode.Text] as IAgMto;

                foreach (TreeNode thisSubNode in thisNode.Nodes)
                {
                    if (thisSubNode.Checked)
                    {
                        // find associated track
                        foreach (IAgMtoTrack thisTrack in thisMto.Tracks)
	                    {
                            if (thisTrack.Id.ToString() == thisSubNode.Text)
                            {
                                 // loop through all points to copy out data
                                List<LLAPt> trackPts = new List<LLAPt>();
                                foreach (IAgMtoTrackPoint thisPoint in thisTrack.Points)
                                {
                                    trackPts.Add(new LLAPt(thisPoint.Time.ToString(), thisPoint.Latitude, thisPoint.Longitude, thisPoint.Altitude));
                                }

                                // remove all points
                                thisTrack.Points.RemoveAll();

                                // add points with new time
                                foreach (LLAPt thisPoint in trackPts)
                                {
                                    // compute new time
                                    string oldTime = m_root.ConversionUtility.ConvertDate("UTCG", "EpSec", thisPoint.Time);
                                    double newTimeEpSec = Convert.ToDouble(oldTime) + totalSecs * timeShiftDirection;
                                    string newTimeUTCG = m_root.ConversionUtility.ConvertDate("EpSec", "UTCG", newTimeEpSec.ToString());

                                    // add point again
                                    thisTrack.Points.AddPoint(newTimeUTCG, thisPoint.Latitude, thisPoint.Longitude, thisPoint.Altitude);

                                }

                            }
	                    }
                        
                    }
                }
            }

        }

        class LLAPt
        {
            public string Time { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public double Altitude { get; set; }

            public LLAPt(string time, double latitude, double longitude, double altitude)
            {
                Time = time;
                Latitude = latitude;
                Longitude = longitude;
                Altitude = altitude;
            }
        }
    }
}
