using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.SmartView
{
    public partial class EditTargetThreatForm : Form
    {

        private List<string> _threatNames;
        private int _onStart = 0;
        public EditTargetThreatForm()
        {
            InitializeComponent();
            _onStart = 1;
            CommonData.CurrentViewObjectData = SmartViewFunctions.GetObjectData();
            _threatNames = new List<string>();

            PopulateComboBoxes();
            _onStart = 0;
        }

        private void TargetSatellite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TargetSatellite.SelectedIndex != -1)
            {
                if (_onStart==0)
                {
                    PopulateThreats();
                }
            }
        }

        private void Select_Click(object sender, EventArgs e)
        {
            string className = null;
            if (ThreatList.FocusedItem != null)
            {
                foreach (int index in ThreatList.SelectedIndices)
                {
                    ThreatList.Items[index].Font = new Font(ThreatList.Items[index].Font, FontStyle.Bold);
                    if (!_threatNames.Contains(ThreatList.Items[index].SubItems[0].Text))
                    {
                        className = SmartViewFunctions.GetClassName(ThreatList.Items[index].SubItems[0].Text);
                        _threatNames.Add(className + "/" + ThreatList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void Unselect_Click(object sender, EventArgs e)
        {
            string className = null;
            if (ThreatList.FocusedItem != null)
            {
                foreach (int index in ThreatList.SelectedIndices)
                {
                    ThreatList.Items[index].Font = new Font(ThreatList.Items[index].Font, FontStyle.Regular);
                    if (_threatNames.Contains(ThreatList.Items[index].SubItems[0].Text))
                    {
                        className = SmartViewFunctions.GetClassName(ThreatList.Items[index].SubItems[0].Text);
                        _threatNames.Remove(className + "/" + ThreatList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void TTDisplayObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TTDisplayObject.SelectedIndex != -1)
            {
                TTDisplayReport.Items.Clear();
                if (TTDisplayObject.SelectedIndex == 0)
                {
                    TTDisplayLocation.Enabled = true;
                    TTDisplayReport.Items.Add("LLA Position");
                    TTDisplayReport.Items.Add("Classical Orbital Elements");
                    TTDisplayReport.Items.Add("Inertial Position Velocity");
                    TTDisplayReport.Items.Add("Fixed Position Velocity");
                }
                else if (TTDisplayObject.SelectedIndex == 1)
                {
                    TTDisplayLocation.Enabled = false;
                    TTDisplayReport.Items.Add("RIC");
                    TTDisplayReport.Items.Add("LLA Position");
                    TTDisplayReport.Items.Add("Classical Orbital Elements");
                    TTDisplayReport.Items.Add("Inertial Position Velocity");
                    TTDisplayReport.Items.Add("Fixed Position Velocity");
                }
                else
                {
                    TTDisplayLocation.Enabled = true;
                    string className = SmartViewFunctions.GetClassName(TTDisplayObject.Text);
                    IAgVODataDisplayCollection ddCollection = null;
                    if (className == "Satellite")
                    {
                        IAgSatellite myObject = CommonData.StkRoot.GetObjectFromPath(className + "/" + TTDisplayObject.Text) as IAgSatellite;
                        ddCollection = myObject.VO.DataDisplay;
                    }
                    Array reportNames = ddCollection.AvailableData;
                    foreach (var name in reportNames)
                    {
                        TTDisplayReport.Items.Add(name);
                        if (name.ToString() == CommonData.SavedViewList[CommonData.SelectedIndex].TtDataDisplayReportName)
                        {
                            TTDisplayReport.SelectedIndex = TTDisplayReport.Items.Count - 1;
                        }
                    }
                }
                if (TTDisplayReport.SelectedIndex==-1)
                {
                    TTDisplayReport.SelectedIndex = 0;
                }
            }
        }

        private void TTDisplayReport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ObjectHideShow_CheckedChanged(object sender, EventArgs e)
        {
            if (ObjectHideShow.Checked)
            {
                HideShowOptions.Enabled = true;
            }
            else
            {
                HideShowOptions.Enabled = false;
            }
        }

        private void HideShowOptions_Click(object sender, EventArgs e)
        {
            ObjectHideShowForm form = new ObjectHideShowForm();
            form.ShowDialog();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            ViewData current = new ViewData();
            current.WindowName = WindowSelect.Text;
            current.ViewType = "Target/Threat";
            current.ThreatSatNames = _threatNames;
            string className = SmartViewFunctions.GetClassName(TargetSatellite.Text);
            current.TargetSatellite = className + "/" + TargetSatellite.Text;
            current.Name = TTViewName.Text;

            if (TTUseDataDisplay.Checked)
            {
                current.TtDataDisplayActive = true;
                current.TtDataDisplayLocation = TTDisplayLocation.Text;
                if (TTDisplayObject.SelectedIndex == 0)
                {
                    className = SmartViewFunctions.GetClassName(TargetSatellite.Text);
                    current.TtDataDisplayObject = className + "/" + TargetSatellite.Text;
                    current.TtDataDisplayReportName = TTDisplayReport.Text;
                }
                else if (TTDisplayObject.SelectedIndex == 1)
                {
                    current.TtDataDisplayObject = "AllThreat";
                    current.TtDataDisplayReportName = TTDisplayReport.Text;
                }
                else
                {
                    className = SmartViewFunctions.GetClassName(TTDisplayObject.Text);
                    current.TtDataDisplayObject = className + "/" + TTDisplayObject.Text;
                    current.TtDataDisplayReportName = TTDisplayReport.Text;
                }
            }
            else
            {
                current.TtDataDisplayActive = false;
                current.TtDataDisplayLocation = TTDisplayLocation.Text;
                className = SmartViewFunctions.GetClassName(TTDisplayObject.Text);
                current.TtDataDisplayObject = className + "/" + TTDisplayObject.Text;
                current.TtDataDisplayReportName = TTDisplayReport.Text;
            }

            if (UseProxBox.Checked)
            {
                current.EnableProximityBox = true;
            }
            else
            {
                current.EnableProximityBox = false;
            }

            if (EnableEllipsoid.Checked)
            {
                current.EnableProximityEllipsoid = true;
                current.EllipsoidX = EllipsoidX.Text;
                current.EllipsoidY = EllipsoidY.Text;
                current.EllipsoidZ = EllipsoidZ.Text;
            }
            else
            {
                current.EnableProximityEllipsoid = false;
                current.EllipsoidX = "100";
                current.EllipsoidY = "100";
                current.EllipsoidZ = "100";
            }

            if (UseCurrentTime.Checked)
            {
                IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
                double currentTime = animationRoot.CurrentTime;
                current.UseAnimationTime = true;
                current.AnimationTime = CurrentTime.Text;
            }
            else
            {
                current.UseAnimationTime = false;
                IAgScenario scenario = (IAgScenario)(CommonData.StkRoot.CurrentScenario);
                current.AnimationTime = scenario.StartTime;
            }

            if (ObjectHideShow.Checked)
            {
                current.ObjectHideShow = true;
                List<ObjectData> data = new List<ObjectData>();
                data = CommonData.CurrentViewObjectData;
                current.ViewObjectData = data;
            }
            else
            {
                current.ObjectHideShow = false;
            }
            CommonData.SavedViewList[CommonData.SelectedIndex] = current;

            try
            {
                ReadWrite.WriteSavedViews(CommonData.DirectoryStr + "\\StoredViewData.json");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Write Stored Views File");
            }

            //try
            //{
            //    ReadWrite.WriteObjectData(CommonData.DirectoryStr + "\\StoredObjectData.txt");
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Could not Write Object Data File");
            //}
            CommonData.UpdatedView = true;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateThreats()
        {
            ThreatList.Items.Clear();
            _threatNames.Clear();
            foreach (ObjectData item in CommonData.CurrentViewObjectData)
            {
                if (item.ClassName == "Satellite" && item.SimpleName != TargetSatellite.Text)
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = item.SimpleName;
                    int index = ThreatList.Items.IndexOf(listItem);
                    if (index == -1)
                    {
                        ThreatList.Items.Add(listItem);
                    }
                }
            }
        }

        private void PopulateComboBoxes()
        {
            TTViewName.Text = CommonData.SavedViewList[CommonData.SelectedIndex].Name;
            List<string> windowNames = SmartViewFunctions.GetWindowNames(1);
            foreach (var item in windowNames)
            {
                WindowSelect.Items.Add(item);
                if (item.Contains(CommonData.SavedViewList[CommonData.SelectedIndex].WindowName))
                {
                    WindowSelect.SelectedIndex = WindowSelect.Items.Count - 1;
                }
            }

            foreach (ObjectData item in CommonData.CurrentViewObjectData)
            {
                if (item.ClassName == "Satellite")
                {
                    TargetSatellite.Items.Add(item.SimpleName);
                    if (CommonData.SavedViewList[CommonData.SelectedIndex].TargetSatellite.Contains(item.SimpleName))
                    {
                        TargetSatellite.SelectedItem = item.SimpleName;
                    }
                }
            }
            if (TargetSatellite.SelectedIndex == -1)
            {
                TargetSatellite.SelectedIndex = 0;
            }

            TTDisplayObject.Items.Add("Target Satellite");
            TTDisplayObject.Items.Add("All Threats (Up to 4)");
            if (CommonData.SavedViewList[CommonData.SelectedIndex].TtDataDisplayObject =="AllThreat")
            {
                TTDisplayObject.SelectedIndex = TTDisplayObject.Items.Count - 1;
            }
            foreach (ObjectData item in CommonData.CurrentViewObjectData)
            {
                if (item.ClassName == "Satellite")
                {
                    if (item.SimpleName != TargetSatellite.Text)
                    {
                        TTDisplayObject.Items.Add(item.SimpleName);
                        if (CommonData.SavedViewList[CommonData.SelectedIndex].TtDataDisplayObject.Contains(item.SimpleName))
                        {
                            TTDisplayObject.SelectedIndex = CommonData.CurrentViewObjectData.IndexOf(item);
                        }
                    }
                }
            }
            if (TTDisplayObject.SelectedIndex==-1)
            {
                TTDisplayObject.SelectedIndex = 0;
            }

            ThreatList.Items.Clear();
            foreach (ObjectData item in CommonData.CurrentViewObjectData)
            {
                if (item.ClassName == "Satellite" && item.SimpleName != TargetSatellite.Text)
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = item.SimpleName;
                    int index = ThreatList.Items.IndexOf(listItem);
                    string className = SmartViewFunctions.GetClassName(item.SimpleName);
                    if (index == -1)
                    {
                        if (CommonData.SavedViewList[CommonData.SelectedIndex].ThreatSatNames.Contains(className+"/"+item.SimpleName))
                        {
                            listItem.Font = new Font(listItem.Font, FontStyle.Bold);
                            _threatNames.Add(className + "/" + item.SimpleName);
                        }

                        ThreatList.Items.Add(listItem);
                    }
                }
            }

            TTDisplayLocation.Items.Add("TopLeft");
            TTDisplayLocation.Items.Add("TopCenter");
            TTDisplayLocation.Items.Add("TopRight");
            TTDisplayLocation.Items.Add("CenterLeft");
            TTDisplayLocation.Items.Add("Center");
            TTDisplayLocation.Items.Add("CenterRight");
            TTDisplayLocation.Items.Add("BottomLeft");
            TTDisplayLocation.Items.Add("BottomCenter");
            TTDisplayLocation.Items.Add("BottomRight");
            TTDisplayLocation.SelectedIndex = 0;

            if (CommonData.SavedViewList[CommonData.SelectedIndex].EnableProximityBox)
            {
                UseProxBox.Checked = true;
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].TtDataDisplayActive)
            {
                TTDataDisplayOptions.Enabled = true;
                TTUseDataDisplay.Checked = true;
            }
            else
            {
                TTDataDisplayOptions.Enabled =false;
                TTUseDataDisplay.Checked = false;
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].ObjectHideShow)
            {
                ObjectHideShow.Checked = true;
                HideShowOptions.Enabled = true;
            }
            else
            {
                HideShowOptions.Enabled = false;
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].UseAnimationTime)
            {
                string currentTime = CommonData.SavedViewList[CommonData.SelectedIndex].AnimationTime;
                UseCurrentTime.Checked = true;
                CurrentTime.Text = currentTime;
            }

            if (CommonData.SavedViewList[CommonData.SelectedIndex].EnableProximityEllipsoid)
            {
                EnableEllipsoid.Checked = true;
                EllipsoidX.Text = CommonData.SavedViewList[CommonData.SelectedIndex].EllipsoidX;
                EllipsoidY.Text = CommonData.SavedViewList[CommonData.SelectedIndex].EllipsoidY;
                EllipsoidZ.Text = CommonData.SavedViewList[CommonData.SelectedIndex].EllipsoidZ;

            }
            else
            {
                EllipsoidDefinition.Enabled = false;
                EllipsoidX.Text = "100";
                EllipsoidY.Text = "100";
                EllipsoidZ.Text = "100";
            } 

        }

        private void TTUseDataDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (TTUseDataDisplay.Checked)
            {
                TTDataDisplayOptions.Enabled = true;
            }
            else
            {
                TTDataDisplayOptions.Enabled = false;
            }
        }

        private void RefreshTime_Click(object sender, EventArgs e)
        {
            IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
            double currentTime = animationRoot.CurrentTime;
            string newTime = CommonData.StkRoot.ConversionUtility.ConvertDate("EpSec", "UTCG", currentTime.ToString());
            CurrentTime.Text = newTime;
        }

        private void UseCurrentTime_CheckedChanged(object sender, EventArgs e)
        {
            if (UseCurrentTime.Checked)
            {
                CurrentTime.Enabled = true;
                RefreshTime.Enabled = true;
            }
            else
            {
                CurrentTime.Enabled = false;
                RefreshTime.Enabled = false;
            }
        }

        private void CurrentTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void EnableEllipsoid_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableEllipsoid.Checked)
            {
                EllipsoidDefinition.Enabled = true;
            }
            else
            {
                EllipsoidDefinition.Enabled = false;
            }
        }
    }
}
