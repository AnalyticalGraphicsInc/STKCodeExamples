using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Threading;
using System.Collections.Specialized;
using AGI.STKUtil;

namespace Stk12.UiPlugin.Articulations
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;
        public string objectName;
        public string articName;
        

        public CustomUserInterface()
        {
            InitializeComponent();
            CommonData.sectionList = new List<Section>();
            CommonData.ClipboardSectionList = new List<Section>();
            CommonData.objectPaths = new List<string>();
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

            //EXAMPLE: Hooking to STK Exents
            CommonData.StkRoot.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            CommonData.StkRoot.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);
            //EXAMPLE: Using preference value
            //m_uiPlugin.DoubleValue;

            //EXAMPLE: Populate combo box with STK Objects
            PopulateComboBox();
            AttitudeCoordFrame.Enabled = false;
            MainBody.Enabled = false;

        }


        #endregion

        #region Sample code
        void m_root_OnStkObjectDeleted(object Sender)
        {
            string objectPath = Sender.ToString();
            string longName = m_stkObjectsLibrary.SimplifiedObjectPath(objectPath);
            string simpleName = m_stkObjectsLibrary.ObjectName(longName);
            cbStkObjects.Items.Remove(simpleName);
        }

        void m_root_OnStkObjectAdded(object Sender)
        {
            string objectPath = Sender.ToString();
            string longName = m_stkObjectsLibrary.SimplifiedObjectPath(objectPath);
            string simpleName = m_stkObjectsLibrary.ObjectName(longName);
            cbStkObjects.Items.Add(simpleName);
        }

        void PopulateComboBox()
        {
            string simpleName;
            string className;
            StringCollection objectNames = m_stkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");

            foreach (string objectName in objectNames)
            {
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario" && className != "Sensor" && className != "Antenna" && className != "Transmitter" && className != "Receiver" && className != "CoverageDefinition" && className != "FigureOfMerit" && className != "Radar" && className != "Constellation" && className != "Chain" && className != "CommSystem" && className != "Volumetric")
                {
                    simpleName = m_stkObjectsLibrary.ObjectName(objectName);
                    
                    cbStkObjects.Items.Add(simpleName);
                }
            }

            if (cbStkObjects.Items.Count > 0)
            {
                cbStkObjects.SelectedIndex = 0;
            }
        }
        #endregion
        //EXAMPLE: Progress bar
        private void TestProgressBar()
        {
            m_uiPlugin.ProgressBar.BeginTracking(AgEProgressTrackingOptions.eProgressTrackingOptionNoCancel, AgEProgressTrackingType.eTrackAsProgressBar);
            for (int i = 0; i <= 100; i++)
            {
                m_uiPlugin.ProgressBar.SetProgress(i, string.Format("Progress is at {0}...", i));
                Thread.Sleep(100);
                if (!m_uiPlugin.ProgressBar.Continue)
                    break;
            }
            m_uiPlugin.ProgressBar.EndTracking();
        }

        private void btnTestProgressBar_Click(object sender, EventArgs e)
        {
            TestProgressBar();
        }

        private void AddArtic_Click(object sender, EventArgs e)
        {
            CommonData.added = false;
            //Load add articulation form
            var form = new AddArticForm();
            form.ShowDialog();
            //change CreatedArtic list if added
            if (CommonData.added)
            {
                ArticFunctions.CreateFile(CommonData.fileStr);
                int index = CommonData.sectionList.Count - 1;
                if (!String.IsNullOrWhiteSpace(CommonData.sectionList[index].sectionName))
                {
                    CreatedArtic.Items.Add(CommonData.sectionList[index].sectionName);
                }
                else
                {
                    CreatedArtic.Items.Add(CommonData.sectionList[index].objectName + " " + CommonData.sectionList[index].articName + " | " + " Start Time: " + CommonData.sectionList[index].startTimeValue);
                }
            }
        }

        private void LoadArticFile_Click(object sender, EventArgs e)
        {
            ArticFunctions.LoadArticFile();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            
        }

        private void RemoveAll_Click(object sender, EventArgs e)
        {
            //removes all lines from text file
            ArticFunctions.RemoveAll(CommonData.fileStr);
            CreatedArtic.Items.Clear();

        }

        private void Remove_Click(object sender, EventArgs e)
        {
            //remove the section of the text file related to the chosen listing
            //int sectionNum = CreatedArtic.SelectedIndex; 
            //ArticFunctions.RemoveSection(CommonData.fileStr, sectionNum);
            //if (sectionNum!=-1)
            //{
            //    CreatedArtic.Items.RemoveAt(sectionNum);
            //}
            if (CreatedArtic.SelectedIndex!=-1)
            {
                List<string> selectedNames = new List<string>();
                foreach (string name in CreatedArtic.SelectedItems)
                {
                    selectedNames.Add(name);
                }
                foreach (var item in selectedNames)
                {
                    for (int i = 0; i < CommonData.sectionList.Count; i++)
                    {
                        if (CreatedArtic.Items[i].ToString() == item)
                        {
                            ArticFunctions.RemoveSection(CommonData.fileStr, i);
                            CreatedArtic.Items.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void cbStkObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            string simpleName;
            string className;
            string ext;
            CommonData.totalSectionCount = 0;
            //Change options in articulation box
            StringCollection objectPaths = m_stkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            foreach (string path in objectPaths)
            {
                CommonData.objectPaths.Add(path);
            }
            simpleName = cbStkObjects.Text;
            CreatedArtic.Items.Clear();
            MainBody.Items.Clear();
            LinkToAttitude.Checked = false;
            int startnum = CommonData.sectionList.Count;
            for (int i = (startnum-1); i > -1; i--)
            {
                CommonData.sectionList.RemoveAt(i);
            }
            foreach (string path in objectPaths)
            {
                string objectName = m_stkObjectsLibrary.ObjectName(path);
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(path);
                if (objectName == simpleName && className!="Scenario")
                {
                    className = m_stkObjectsLibrary.ClassNameFromObjectPath(path);
                    CommonData.objectClass = className;
                    CommonData.simpleName = simpleName;
                    string objectPath = className + "/" + simpleName;
                    IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(objectPath);

                    //Clear possible articulations if another object was previously selected
                    PossibleArtic.Items.Clear();

                    //Add possible articulations to dropdown menu
                    Array names = ArticFunctions.GetArticulations(obj);
                    foreach (var item in names)
                    {
                        if (item.ToString().Contains("Time"))
                        {

                        }
                        else
                        {
                            PossibleArtic.Items.Add(item);
                        }
                    }

                    //Get the right file extension based on object class
                    ext = ArticFunctions.GetExtension(className);
                    AgExecCmdResult directory = (AgExecCmdResult)CommonData.StkRoot.ExecuteCommand("GetDirectory / Scenario");
                    CommonData.directoryStr = m_stkObjectsLibrary.GetScenarioDirectory();
                    CommonData.fileStr = CommonData.directoryStr + "\\" + simpleName + ext;

                    //If file exists read the file and add current articulations to list of sections
                    if (File.Exists(CommonData.fileStr))
                    {
                        //read in file
                        List<Section> fileSections=ArticFunctions.ReadFile(CommonData.fileStr);
                        //populate created articulation list based on file
                        foreach (Section item in fileSections)
                        {
                            CommonData.sectionList.Add(item);
                            if (!String.IsNullOrWhiteSpace(item.sectionName))
                            {
                                if (item.isLinked)
                                {
                                    CreatedArtic.Items.Add(item.sectionName + "  |  LINKED");
                                }
                                else if(item.linkedToList)
                                {
                                    CreatedArtic.Items.Add(item.sectionName + "  |  LINKED TO INTERVAL LIST");

                                }
                                else
                                {
                                    CreatedArtic.Items.Add(item.sectionName);
                                }
                            }
                            else
                            {
                                if (item.isLinked)
                                {
                                    CreatedArtic.Items.Add(item.objectName + item.articName + " Start Time: " + item.startTimeValue + "sec" + "  |  LINKED");
                                }
                                else if (item.linkedToList)
                                {
                                    CreatedArtic.Items.Add(item.objectName + item.articName + " Start Time: " + item.startTimeValue + "sec" + "  |  LINKED TO INTERVAL LIST");
                                }
                                else
                                {
                                    CreatedArtic.Items.Add(item.objectName + item.articName + " Start Time: " + item.startTimeValue + "sec");
                                }
                            }
                        }
                    }
                    

                }
            }

        }

        private void PossibleArtic_SelectedIndexChanged(object sender, EventArgs e)
        {
            //reassign selected object and articulation
            string articFullName = PossibleArtic.Text;
            string[] articNameSplit = articFullName.Split(new Char[]{'-'}, 2);
            objectName = articNameSplit[0];
            CommonData.objectName = objectName;
            articName = articNameSplit[1];
            CommonData.articName = articName;

        }

        private void CreatedArtic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CommonData.selectedArtic!= -1 && CommonData.sectionList.Count != 0)
            {
                CommonData.selectedArtic = CreatedArtic.SelectedIndex;
                //change possible artic value based on value in section
                try
                {
                    int index = PossibleArtic.FindString(CommonData.sectionList[CommonData.selectedArtic].objectName + "-" + CommonData.sectionList[CommonData.selectedArtic].articName);
                    PossibleArtic.SelectedIndex = index;
                }
                catch (Exception)
                {

                }
            }
            if (CommonData.selectedArtic == -1)
            {
                CommonData.selectedArtic = CommonData.previousSelected;
            }
            CommonData.linkChanged = false;

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (CommonData.selectedArtic!=-1 && CommonData.sectionList.Count != 0)
            {
                //open edit form
                CommonData.applied = false;
                EditArticForm form = new EditArticForm(CommonData.selectedArtic);
                form.ShowDialog();
                if (CommonData.applied)
                {
                    //change names in articulations list 
                    CommonData.previousSelected = CommonData.selectedArtic;
                    if (CommonData.sectionList[CommonData.selectedArtic].isLinked)
                    {
                        if (!String.IsNullOrWhiteSpace(CommonData.sectionList[CommonData.selectedArtic].sectionName))
                        {
                            CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].sectionName + "  |  LINKED";
                        }
                        else
                        {
                            CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].objectName + CommonData.sectionList[CommonData.selectedArtic].articName + " Start Time: " + CommonData.sectionList[CommonData.selectedArtic].startTimeValue + "sec" + "  |  LINKED";
                        }

                    }
                    else if (CommonData.sectionList[CommonData.selectedArtic].linkedToList)
                    {
                        if (!String.IsNullOrWhiteSpace(CommonData.sectionList[CommonData.selectedArtic].sectionName))
                        {
                            CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].sectionName + "  |  LINKED TO INTERVAL LIST";
                        }
                        else
                        {
                            CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].objectName + CommonData.sectionList[CommonData.selectedArtic].articName + " Start Time: " + CommonData.sectionList[CommonData.selectedArtic].startTimeValue + "sec" + "  |  LINKED TO INTERVAL LIST";
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(CommonData.sectionList[CommonData.selectedArtic].sectionName))
                        {
                            CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].sectionName;
                        }
                        else
                        {
                            CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].objectName + CommonData.sectionList[CommonData.selectedArtic].articName + " Start Time: " + CommonData.sectionList[CommonData.selectedArtic].startTimeValue + "sec"; ;
                        }
                    }
                    ArticFunctions.CreateFile(CommonData.fileStr);

                }
            }

        }


        private void LinkToTime_Click(object sender, EventArgs e)
        {
            if (CommonData.selectedArtic != -1  && CommonData.sectionList.Count!=0)
            {
                //Open link to time form
                CommonData.sectionList[CommonData.selectedArtic].isLinked = false;
                LinkToTimeForm form = new LinkToTimeForm();
                form.ShowDialog();
                //change name in list 
                if (CommonData.sectionList[CommonData.selectedArtic].isLinked == true)
                {
                    CommonData.previousSelected = CommonData.selectedArtic;
                    CommonData.linkChanged = true;
                    if (!String.IsNullOrWhiteSpace(CommonData.sectionList[CommonData.selectedArtic].sectionName))
                    {
                        CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].sectionName + "  |  LINKED";
                    }
                    else
                    {
                        CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].objectName + CommonData.sectionList[CommonData.selectedArtic].articName + " Start Time: " + CommonData.sectionList[CommonData.selectedArtic].startTimeValue + "sec" + "  |  LINKED";
                    }
                }
            }
        }
        //remove selected index in list and remove from section list
        private void RemoveLink_Click(object sender, EventArgs e)
        {
            if (CommonData.selectedArtic != -1 && CommonData.sectionList.Count != 0)
            {
                CommonData.sectionList[CommonData.selectedArtic].isLinked = false;
                string className = CommonData.objectClass;
                string simpleName = CommonData.simpleName;
                string objectPath = className + "/" + simpleName;
                IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(objectPath);
                if (CommonData.sectionList[CommonData.selectedArtic].linkedToList == true)
                {
                    bool sameInst = false;
                    foreach (string name in CommonData.sectionList[CommonData.selectedArtic].linkedToListInstantNames)
                    {
                        for (int i = 0; i < CommonData.sectionList.Count; i++)
                        {
                            if (i == CommonData.selectedArtic)
                            {

                            }
                            else if (CommonData.sectionList[i].linkedToList)
                            {
                                foreach (string instanceString in CommonData.sectionList[i].linkedToListInstantNames)
                                {
                                    if (instanceString == name)
                                    {
                                        sameInst = true;
                                    }
                                }
                            }
                        }
                        if (obj.Vgt.Events.Contains(name) && sameInst == false)
                        {
                            obj.Vgt.Events.Remove(name);
                        }
                        sameInst = false;
                    }
                }
                CommonData.sectionList[CommonData.selectedArtic].linkedToList = false;
                CommonData.sectionList[CommonData.selectedArtic].linkedToListStrings.Clear();
                CommonData.sectionList[CommonData.selectedArtic].linkedListSections.Clear();
                ArticFunctions.CreateFile(CommonData.fileStr);
                CommonData.linkChanged = true;
                CommonData.previousSelected = CommonData.selectedArtic;
                if (!String.IsNullOrWhiteSpace(CommonData.sectionList[CommonData.selectedArtic].sectionName))
                {
                    CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].sectionName;
                }
                else
                {
                    CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].objectName + CommonData.sectionList[CommonData.selectedArtic].articName + " Start Time: " + CommonData.sectionList[CommonData.selectedArtic].startTimeValue + "sec";
                }
            }


        }

        private void LinkToList_Click(object sender, EventArgs e)
        {
            if (CommonData.selectedArtic != -1 && CommonData.sectionList.Count != 0)
            {
                if (CommonData.sectionList[CommonData.selectedArtic].isLinked == true)
                {
                    MessageBox.Show("Articulation is already linked to time instance. \n Please remove current link to link to interval list");
                }
                else if (CommonData.sectionList[CommonData.selectedArtic].linkedToList == true)
                {
                    MessageBox.Show("Articulation is already linked to interval list. \n Please remove current link to link to another interval list");

                }
                else
                {
                    CommonData.sectionList[CommonData.selectedArtic].linkedToList = false;
                    CommonData.sectionList[CommonData.selectedArtic].isIncremented = false;
                    LinkToListForm form = new LinkToListForm();
                    form.ShowDialog();
                    if (CommonData.sectionList[CommonData.selectedArtic].linkedToList == true)
                    {
                        CommonData.previousSelected = CommonData.selectedArtic;
                        CommonData.linkChanged = true;
                        if (!String.IsNullOrWhiteSpace(CommonData.sectionList[CommonData.selectedArtic].sectionName))
                        {
                            CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].sectionName + "  |  LINKED TO INTERVAL LIST";
                        }
                        else
                        {
                            CreatedArtic.Items[CommonData.selectedArtic] = CommonData.sectionList[CommonData.selectedArtic].objectName + CommonData.sectionList[CommonData.selectedArtic].articName + " Start Time: " + CommonData.sectionList[CommonData.selectedArtic].startTimeValue + "sec" + "  |  LINKED TO INTERVAL LIST";
                        }
                    }

                }
            }
        }

        private void LinkToAttitude_CheckedChanged(object sender, EventArgs e)
        {

            CommonData.StkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec");
            string objectPath = CommonData.objectClass + "/" + CommonData.simpleName;
            IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(objectPath);
            if (LinkToAttitude.Checked)
            {
                AttitudeCoordFrame.Enabled = true;
                MainBody.Enabled = true;
                AttitudeCoordFrame.Items.Add("VVLH");
                //AttitudeCoordFrame.Items.Add("ICRF");
                AttitudeCoordFrame.SelectedIndex = 0;
                foreach (Section item in CommonData.sectionList)
                {
                    item.linkedToAttitude = false;
                    if (item.articName.Contains("Yaw") || item.articName.Contains("Pitch") || item.articName.Contains("Roll"))
                    {
                        item.linkedToAttitude = true;
                    }
                }
                //Recreate and load articulation file
                ArticFunctions.CreateFile(CommonData.fileStr);
                ArticFunctions.LoadArticFile();

                //Add possible Main Body Options to dropdown menu
                //Defined as possible articulation objects, not articulation movements
                Array names = ArticFunctions.GetArticulations(obj);
                string currentName = null;
                foreach (string name in names)
                {
                    if (name.Split('-')[0]==currentName)
                    {

                    }
                    else
                    {
                        if (name.Split('-')[0]=="Time")
                        {

                        }
                        else
                        {
                            MainBody.Items.Add(name.Split('-')[0]);
                            currentName = name.Split('-')[0];
                        }
                    }
                }
                MainBody.SelectedIndex = 0;

            }
            else
            {
                foreach (Section item in CommonData.sectionList)
                {
                    item.linkedToAttitude = false;
                }
                //recreate articulation file and reload
                ArticFunctions.CreateFile(CommonData.fileStr);
                ArticFunctions.LoadArticFile();
                //turn off attitude file in object settings and disable settings on form
                if (CommonData.objectClass == "Aircraft")
                {
                    IAgGreatArcVehicle aircraft = obj as IAgGreatArcVehicle;
                    IAgVeRouteAttitudeStandard attitude = aircraft.Attitude as IAgVeRouteAttitudeStandard;
                    attitude.External.Disable();
                }
                else if (CommonData.objectClass == "Satellite")
                {
                    IAgSatellite sat = obj as IAgSatellite;
                    IAgVeOrbitAttitudeStandard attitude = sat.Attitude as IAgVeOrbitAttitudeStandard;
                    attitude.External.Disable();
                }
                MainBody.Enabled = false;
                AttitudeCoordFrame.Enabled = false;
                AttitudeCoordFrame.Items.Clear();
                MainBody.Items.Clear();
            }
        }

        private void AttitudeCoordFrame_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadAttitude_Click(object sender, EventArgs e)
        {
            foreach (Section item in CommonData.sectionList)
            {
                item.linkedToAttitude = false;
                if (item.articName.Contains("Yaw") || item.articName.Contains("Pitch") || item.articName.Contains("Roll"))
                {
                    item.linkedToAttitude = true;
                }
            }
            string objectPath = CommonData.objectClass + "/" + CommonData.simpleName;
            IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(objectPath);
            string fileName = CommonData.directoryStr + "\\" + CommonData.simpleName + ".a";
            if (AttitudeCoordFrame.SelectedIndex == 0)
            {
                ArticFunctions.WriteVVLHAttitudeFile(fileName);
            }
            else if (AttitudeCoordFrame.SelectedIndex == 1)
            {
                ArticFunctions.WriteICRFAttitudeFile(fileName);
            }

            if (CommonData.objectClass == "Aircraft")
            {
                try
                {
                    AgAircraft aircraft = obj as AgAircraft;
                    IAgVeRouteAttitudeStandard attitude = aircraft.Attitude as IAgVeRouteAttitudeStandard;
                    attitude.External.Load(fileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not load attitude file");
                    
                }
            }
            else if (CommonData.objectClass == "Satellite")
            {
                try
                {
                    IAgSatellite sat = obj as IAgSatellite;
                    IAgVeOrbitAttitudeStandard attitude = sat.Attitude as IAgVeOrbitAttitudeStandard;
                    attitude.External.Load(fileName);
                }
                catch (Exception)
                {

                    MessageBox.Show("Could not load attitude file");
                }

            }
        }

        private void MainBody_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonData.MainBody = MainBody.Text;
        }

        private void UnloadArtic_Click(object sender, EventArgs e)
        {
            string objectStr = "*/" + CommonData.objectClass + "/" + CommonData.simpleName;
            string cmd = "VO " + objectStr + " ArticulationFile EnableArticFile No FilePath " + "\"" + CommonData.fileStr + "\"";
            try
            {
                CommonData.StkRoot.ExecuteCommand(cmd);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not unload file", "Error");
            }
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            CommonData.ClipboardSectionList.Clear();
            foreach (string name in CreatedArtic.SelectedItems)
            {
                for (int i = 0; i < CommonData.sectionList.Count; i++)
                {
                    if (CreatedArtic.Items[i].ToString()==name)
                    {
                        Section clone = new Section();
                        clone =  CommonData.sectionList[i].Clone(CommonData.sectionList[i]);
                        CommonData.ClipboardSectionList.Add(clone);
                    }
                }
            }
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            string name = null;
            int count = 0;
            foreach (Section item in CommonData.ClipboardSectionList)
            {
                CommonData.sectionList.Add(item);
                count = CommonData.sectionList.Count - 1;
                if (!String.IsNullOrWhiteSpace(item.sectionName))
                {
                    name = CommonData.sectionList[count].sectionName;
                }
                else
                {
                    name = CommonData.sectionList[count].objectName + CommonData.sectionList[count].articName + " Start Time: " + CommonData.sectionList[count].startTimeValue + "sec";
                }
                if (item.isLinked && !item.linkedToList)
                {
                    name = name + "  |  LINKED";
                    CreatedArtic.Items.Add(name);
                }
                else if (item.linkedToList)
                {
                    name = name + "  |  LINKED TO INTERVAL LIST";
                    CreatedArtic.Items.Add(name);
                }
                else
                {
                    CreatedArtic.Items.Add(name);
                }
                count++;
            }
            ArticFunctions.CreateFile(CommonData.fileStr);
        }
    }
}
