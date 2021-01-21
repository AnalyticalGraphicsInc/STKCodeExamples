using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;

namespace OperatorsToolbox.StationAccess
{
    public partial class StationAccessPlugin : OpsPluginControl
    {
        private StkObjectsLibrary _mStkObjectsLibrary;

        public StationAccessPlugin()
        {
            InitializeComponent();
            CommonData.ToObjectNames = new List<string>();
            CommonData.FromObjectNames = new List<string>();
            CommonData.SensorNames = new List<string>();
            CommonData.SensorParentNames = new List<string>();
            PopulateToTypes();
            PopulateFromTypes();
            DataType.Items.Add("Complete Access");
            DataType.Items.Add("Individual Object Access");
            DataType.SelectedIndex = 0;
            AccessName.Text = "NewAccess";
        }

        private void FilterToObject_Click(object sender, EventArgs e)
        {

        }

        private void FilterFromObject_Click(object sender, EventArgs e)
        {

        }

        private void ToSelect_Click(object sender, EventArgs e)
        {
            string className = null;
            if (ToObjectList.FocusedItem != null)
            {
                if (ToObjectType.Text == "Constellation")
                {
                    CommonData.ToObjectNames.Clear();
                    foreach (ListViewItem item in ToObjectList.Items)
                    {
                        item.Font = new Font(item.Font, FontStyle.Regular);
                    }
                    int index = ToObjectList.SelectedIndices[0];
                    ToObjectList.Items[index].Font = new Font(ToObjectList.Items[index].Font, FontStyle.Bold);
                    className = GetClassName(ToObjectList.Items[index].SubItems[0].Text);
                    if (!CommonData.ToObjectNames.Contains(className + "/" + ToObjectList.Items[index].SubItems[0].Text))
                    {
                        CommonData.ToObjectNames.Add(className + "/" + ToObjectList.Items[index].SubItems[0].Text);
                    }
                }
                else
                {
                    foreach (int index in ToObjectList.SelectedIndices)
                    {
                        ToObjectList.Items[index].Font = new Font(ToObjectList.Items[index].Font, FontStyle.Bold);
                        className = GetClassName(ToObjectList.Items[index].SubItems[0].Text);
                        if (!CommonData.ToObjectNames.Contains(className + "/" + ToObjectList.Items[index].SubItems[0].Text))
                        {
                            CommonData.ToObjectNames.Add(className + "/" + ToObjectList.Items[index].SubItems[0].Text);
                        }
                    }
                }
            }
        }

        private void ToUnselect_Click(object sender, EventArgs e)
        {
            string className = null;
            if (ToObjectList.FocusedItem != null)
            {
                foreach (int index in ToObjectList.SelectedIndices)
                {
                    ToObjectList.Items[index].Font = new Font(ToObjectList.Items[index].Font, FontStyle.Regular);
                    className = GetClassName(ToObjectList.Items[index].SubItems[0].Text);
                    if (CommonData.ToObjectNames.Contains(className + "/" + ToObjectList.Items[index].SubItems[0].Text))
                    {
                        CommonData.ToObjectNames.Remove(className + "/" + ToObjectList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void FromSelect_Click(object sender, EventArgs e)
        {
            string className = null;
            string path = null;
            if (FromObjectList.FocusedItem != null)
            {
                if (FromObjectType.Text == "Constellation")
                {
                    CommonData.FromObjectNames.Clear();
                    foreach (ListViewItem item in FromObjectList.Items)
                    {
                        item.Font = new Font(item.Font, FontStyle.Regular);
                    }
                    int index = FromObjectList.SelectedIndices[0];
                    FromObjectList.Items[index].Font = new Font(FromObjectList.Items[index].Font, FontStyle.Bold);
                    className = GetClassName(FromObjectList.Items[index].SubItems[0].Text);
                    if (!CommonData.FromObjectNames.Contains(className + "/" + FromObjectList.Items[index].SubItems[0].Text))
                    {
                        CommonData.FromObjectNames.Add(className + "/" + FromObjectList.Items[index].SubItems[0].Text);
                    }
                }
                else
                {
                    foreach (int index in FromObjectList.SelectedIndices)
                    {
                        FromObjectList.Items[index].Font = new Font(FromObjectList.Items[index].Font, FontStyle.Bold);
                        className = GetClassName(FromObjectList.Items[index].SubItems[0].Text);
                        if (!CommonData.FromObjectNames.Contains(className + "/" + FromObjectList.Items[index].SubItems[0].Text))
                        {
                            CommonData.FromObjectNames.Add(className + "/" + FromObjectList.Items[index].SubItems[0].Text);
                        }
                    }
                }
            }
        }

        private void FromUnselect_Click(object sender, EventArgs e)
        {
            string className = null;
            if (FromObjectList.FocusedItem != null)
            {
                foreach (int index in FromObjectList.SelectedIndices)
                {
                    FromObjectList.Items[index].Font = new Font(FromObjectList.Items[index].Font, FontStyle.Regular);
                    className = GetClassName(FromObjectList.Items[index].SubItems[0].Text);
                    if (CommonData.FromObjectNames.Contains(className + "/" + FromObjectList.Items[index].SubItems[0].Text))
                    {
                        CommonData.FromObjectNames.Remove(className + "/" + FromObjectList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void ToObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ToObjectType.SelectedIndex != -1)
            {
                CommonData.ToObjectNames.Clear();
                CreatorFunctions.PopulateObjectListByClass(ToObjectList, ToObjectType.Text);
                //if (ToObjectType.SelectedIndex == 0)
                //{
                //    PopulateObjectList(ToObjectList, "Satellite");
                //}
                //else if (ToObjectType.SelectedIndex == 1)
                //{
                //    PopulateObjectList(ToObjectList, "Aircraft");
                //}
                //else if (ToObjectType.SelectedIndex == 2)
                //{
                //    PopulateObjectList(ToObjectList, "Missile");
                //}
                //else if (ToObjectType.SelectedIndex == 3)
                //{
                //    PopulateObjectList(ToObjectList, "LaunchVehicle");
                //}
                //else if (ToObjectType.SelectedIndex == 4)
                //{
                //    PopulateObjectList(ToObjectList, "Constellation");
                //}
            }
        }

        private void FromObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FromObjectType.SelectedIndex != -1)
            {
                CommonData.FromObjectNames.Clear();
                CreatorFunctions.PopulateObjectListByClass(FromObjectList, FromObjectType.Text);
                //if (FromObjectType.SelectedIndex == 0)
                //{
                //    PopulateObjectList(FromObjectList, "Facility");
                //}
                //else if (FromObjectType.SelectedIndex == 1)
                //{
                //    PopulateObjectList(FromObjectList, "Place");
                //}
                //else if (FromObjectType.SelectedIndex == 2)
                //{
                //    PopulateObjectList(FromObjectList, "Target");
                //}
                //else if (FromObjectType.SelectedIndex == 3)
                //{
                //    PopulateObjectList(FromObjectList, "Sensor");
                //}
                //else if (FromObjectType.SelectedIndex == 4)
                //{
                //    PopulateObjectList(FromObjectList, "Constellation");
                //}
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            string accessName = "";
            if (AccessName.Text != null && AccessName.Text != "")
            {
                accessName = AccessName.Text;
            }
            else
            {
                accessName = "NewAccess";
            }
            IAgConstellation fromConst=null;
            IAgConstellation toConst=null;
            IAgChain accessChain;
            string cmd = null;
            //Add Objects to ToConst
            if (CommonData.ToObjectNames.Count > 0)
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Constellation/" + accessName + "_ToAccessConst");

                string objPath;
                if (ToObjectType.Text != "Constellation")
                {
                    if (result[0] == "0")
                    {
                        toConst = (IAgConstellation)CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eConstellation, accessName + "_ToAccessConst");
                    }
                    else
                    {
                        toConst = (IAgConstellation)CommonData.StkRoot.GetObjectFromPath("Constellation/" + accessName + "_ToAccessConst");
                        toConst.Objects.RemoveAll();
                    }
                    foreach (string item in CommonData.ToObjectNames)
                    {
                        toConst.Objects.Add(item);
                    }
                }
                else
                {
                    string constName = CommonData.ToObjectNames[0].Split('/').Last();
                    toConst = CreatorFunctions.GetCreateConstellation(constName) as IAgConstellation;
                }
            }
            //Add objects to FromConst
            if (CommonData.FromObjectNames.Count > 0)
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Constellation/" + accessName + "_FromAccessConst");
                string objPath;
                if (FromObjectType.Text != "Constellation")
                {
                    if (result[0] == "0")
                    {
                        fromConst = (IAgConstellation)CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eConstellation, accessName + "_FromAccessConst");
                    }
                    else
                    {
                        fromConst = (IAgConstellation)CommonData.StkRoot.GetObjectFromPath("Constellation/" + accessName + "_FromAccessConst");
                        fromConst.Objects.RemoveAll();
                    }
                    foreach (string item in CommonData.FromObjectNames)
                    {
                        fromConst.Objects.Add(item);
                    }
                }
                else
                {
                    string constName = CommonData.FromObjectNames[0].Split('/').Last();
                    fromConst = CreatorFunctions.GetCreateConstellation(constName) as IAgConstellation;
                }
            }

            //Create Chain
            if (CommonData.ToObjectNames.Count > 0 && CommonData.FromObjectNames.Count > 0)
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Chain/" + accessName);
                if (result[0] == "0")
                {
                    accessChain = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eChain, accessName) as IAgChain;
                }
                else
                {
                    accessChain = (IAgChain)CommonData.StkRoot.GetObjectFromPath("Chain/" + accessName);
                    accessChain.AutoRecompute = false;
                    accessChain.ClearAccess();
                    accessChain.Objects.RemoveAll();
                }
                //Compute chain
                accessChain.Objects.AddObject((IAgStkObject)toConst);
                accessChain.Objects.AddObject((IAgStkObject)fromConst);
                accessChain.ComputeAccess();
                //Timeline
                IAgStkObject accessObject = accessChain as IAgStkObject;
                cmd = "";
                if (DataType.SelectedIndex == 0)
                {
                    cmd = "Timeline * TimeComponent Remove ContentView \"Scenario Availability\" \"Chain/" + accessName + " CompleteChainAccessIntervals Interval List\"";
                    try
                    {
                        CommonData.StkRoot.ExecuteCommand(cmd);
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        cmd = "Timeline * TimeComponent Add ContentView \"Scenario Availability\" DisplayName \"" + accessName + "\"" + " \"Chain/" + accessName + " CompleteChainAccessIntervals Interval List\"";
                        CommonData.StkRoot.ExecuteCommand(cmd);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not generate timeline components");
                    }
                }
                else if (DataType.SelectedIndex == 1)
                {
                    string timeComponentName = null;
                    IAgCrdnEventIntervalCollection collection;
                    collection = accessObject.Vgt.EventIntervalCollections[1];
                    Array labels = collection.Labels;
                    IAgCrdnIntervalsVectorResult vecResult = collection.FindIntervalCollection();
                    int vecCount = vecResult.IntervalCollections.Count;
                    for (int i = 0; i < vecCount; i++)
                    {
                        timeComponentName = labels.GetValue(i).ToString();
                        cmd = "Timeline * TimeComponent Remove ContentView \"Scenario Availability\" \"Chain/" + accessName + " StrandAccessIntervals." + timeComponentName + " Interval List\"";
                        try
                        {
                            CommonData.StkRoot.ExecuteCommand(cmd);
                        }
                        catch (Exception)
                        {

                        }
                        try
                        {
                            cmd = "Timeline * TimeComponent Add ContentView \"Scenario Availability\" DisplayName \"" + timeComponentName + "\" \"Chain/" + accessName + " StrandAccessIntervals." + timeComponentName + " Interval List\"";
                            CommonData.StkRoot.ExecuteCommand(cmd);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                CommonData.StkRoot.ExecuteCommand("Timeline * Refresh");

                //Data Ouput
                if (ShowReport.Checked)
                {
                    cmd = "";
                    if (DataType.SelectedIndex == 0)
                    {
                        cmd = "ReportCreate */Chain/" + accessName + " Style \"Complete Chain Access\" Type Display";
                    }
                    else if (DataType.SelectedIndex == 1)
                    {
                        cmd = "ReportCreate */Chain/" + accessName + " Style \"Individual Strand Access\" Type Display";
                    }
                    try
                    {
                        CommonData.StkRoot.ExecuteCommand(cmd);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not show report");
                    }
                }
                if (ExportCSV.Checked)
                {
                    if (DataType.SelectedIndex == 0)
                    {
                        cmd = "ReportCreate */Chain/" + accessName + " Style \"Complete Chain Access\" Type Export File \"" + CommonData.DirectoryStr + "\\" + accessName + "_SavedAccessData.csv" + "\"";
                    }
                    else if (DataType.SelectedIndex == 1)
                    {
                        cmd = "ReportCreate */Chain/" + accessName + " Style \"Individual Strand Access\" Type Export File \"" + CommonData.DirectoryStr + "\\" + accessName + "_SavedAccessData.csv" + "\"";
                    }
                    try
                    {
                        CommonData.StkRoot.ExecuteCommand(cmd);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not save report");
                    }
                }
                MessageBox.Show("Computation Complete");
            }
            else
            {
                if (CommonData.ToObjectNames.Count == 0)
                {
                    MessageBox.Show("No To Objects selected");
                }
                if (CommonData.FromObjectNames.Count == 0)
                {
                    MessageBox.Show("No From Objects selected");
                }
            }
        }

        private void PopulateToTypes()
        {
            ToObjectType.Items.Add("Satellite");
            ToObjectType.Items.Add("Aircraft");
            ToObjectType.Items.Add("Missile");
            ToObjectType.Items.Add("LaunchVehicle");
            ToObjectType.Items.Add("Receiver");
            ToObjectType.Items.Add("Antenna");
            ToObjectType.Items.Add("Facility");
            ToObjectType.Items.Add("Constellation");
            ToObjectType.SelectedIndex = 0;
        }

        private void PopulateFromTypes()
        {
            FromObjectType.Items.Add("Facility");
            FromObjectType.Items.Add("Place");
            FromObjectType.Items.Add("Target");
            FromObjectType.Items.Add("Sensor");
            FromObjectType.Items.Add("Transmitter");
            FromObjectType.Items.Add("Antenna");
            FromObjectType.Items.Add("Radar");
            FromObjectType.Items.Add("Satellite");
            FromObjectType.Items.Add("Constellation");
            FromObjectType.SelectedIndex = 0;
        }

        private void PopulateObjectList(ListView view, string className)
        {
            _mStkObjectsLibrary = new StkObjectsLibrary();
            view.Items.Clear();
            string simpleName;
            string classNameTemp;
            StringCollection objectNames = _mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            foreach (string objectName in objectNames)
            {
                classNameTemp = _mStkObjectsLibrary.ClassNameFromObjectPath(objectName);
                if (className == "Sensor" && classNameTemp == className)
                {
                    if (objectName.Contains("Facility") || objectName.Contains("Place") || objectName.Contains("Target"))
                    {
                        ListViewItem item = new ListViewItem();
                        simpleName = _mStkObjectsLibrary.ObjectName(objectName);
                        CommonData.SensorNames.Add(simpleName);
                        string simplePath = _mStkObjectsLibrary.SimplifiedObjectPath(objectName);
                        CommonData.SensorParentNames.Add(simplePath.Split(new string[] { "/" }, StringSplitOptions.None).First());
                        item.Text = simpleName;
                        view.Items.Add(item);
                    }
                }
                else if (classNameTemp == className)
                {
                    ListViewItem item = new ListViewItem();
                    simpleName = _mStkObjectsLibrary.ObjectName(objectName);
                    item.Text = simpleName;
                    view.Items.Add(item);
                }
            }

        }

        private string GetClassName(string name)
        {
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            string simpleName;
            string classNameTemp = null;
            string className = null;
            string truncPath = null;
            simpleName = name;
            StringCollection objectPaths = mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            foreach (string path in objectPaths)
            {
                string objectName = mStkObjectsLibrary.ObjectName(path);
                classNameTemp = mStkObjectsLibrary.ClassNameFromObjectPath(path);
                if (objectName == simpleName && classNameTemp != "Scenario")
                {
                    if (classNameTemp == "Sensor" || classNameTemp == "Transmitter" || classNameTemp == "Receiver" || classNameTemp == "Antenna" || classNameTemp == "Radar")
                    {
                        truncPath = mStkObjectsLibrary.ObjectPathWithoutName(path);
                        truncPath = mStkObjectsLibrary.TruncatedObjectPath(truncPath);
                        //int index = truncPath.LastIndexOf('/');
                        //truncPath = truncPath.Substring(0, index);
                        className = truncPath;
                    }
                    else
                    {
                        className = mStkObjectsLibrary.ClassNameFromObjectPath(path);
                    }
                }
            }
            return className;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        #region Tool Tips
        private void ToSelect_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.ToSelect, "Add selected to calculation");
        }

        private void ToUnselect_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.ToUnselect, "Remove selected from calculation");
        }

        private void FromSelect_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.FromSelect, "Add selected to calculation");
        }

        private void FromUnselect_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.FromUnselect, "Remove selected from calculation");
        }
        #endregion
    }
}
