using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Threading;
using System.Collections.Specialized;
using AGI.STKUtil;
using AGI.STKVgt;

namespace Stk12.UiPlugin.Articulations
{
    public partial class LinkToListForm : Form
    {
        private StkObjectsLibrary m_stkObjectsLibrary;
        private int typeEnum = 0;
        private string offsetValue;
        IAgCrdnEventIntervalListGroup eventIntervalsCollections;
        IAgCrdnEventIntervalListGroup accessEventIntervalsCollections;
        IAgStkObject obj;
        IAgStkAccess access;

        public LinkToListForm()
        {
            InitializeComponent();
            m_stkObjectsLibrary = new StkObjectsLibrary();
            PopulateComboBox();
            IncrementStepValue.Enabled = false;
            CommonData.sectionList[CommonData.selectedArtic].linkedToStart = true;
        }

        private void Events_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbStkObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            string simpleName;
            string className;
            Events.Items.Clear();
            //Change options in Events box
            StringCollection objectPaths = m_stkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            simpleName = cbStkObjects.Text;

            foreach (string path in objectPaths)
            {
                string objectName = m_stkObjectsLibrary.ObjectName(path);
                if (objectName == simpleName)
                {
                    className = m_stkObjectsLibrary.ClassNameFromObjectPath(path);
                    string objectPath = className + "/" + simpleName;
                    obj = CommonData.StkRoot.GetObjectFromPath(objectPath);

                    eventIntervalsCollections = obj.Vgt.EventIntervalLists;

                    foreach (IAgCrdnEventIntervalList eventIntervalCollection in eventIntervalsCollections)
                    {
                        IAgCrdn crdn = eventIntervalCollection as IAgCrdn;
                        Events.Items.Add(crdn.Name);

                    }
                }
            }
            if (cbStkObjects.Text.Contains("Access"))
            {
                IAgScenario scenario = CommonData.StkRoot.CurrentScenario as IAgScenario;
                Array accesses = scenario.GetExistingAccesses();

                int numAccesses = accesses.GetLength(0);
                for (int i = 0; i < numAccesses; i++)
                {
                    string object1 = accesses.GetValue(i, 0).ToString();
                    string shortobject1 = object1.Substring(object1.IndexOf('/') + 1);
                    string object2 = accesses.GetValue(i, 1).ToString();
                    string shortobject2 = object2.Substring(object2.IndexOf('/') + 1);

                    if (cbStkObjects.Text.Contains(shortobject1) && cbStkObjects.Text.Contains(shortobject2))
                    {
                        access = scenario.GetAccessBetweenObjectsByPath(object1, object2);
                        accessEventIntervalsCollections = access.Vgt.EventIntervalLists;
                        foreach (IAgCrdnEventIntervalList eventIntervalCollection in accessEventIntervalsCollections)
                        {
                            IAgCrdn crdn = eventIntervalCollection as IAgCrdn;
                            Events.Items.Add(crdn.Name);

                        }
                    }

                }
            }
            Events.SelectedIndex = 0;
        }

        private void Link_Click(object sender, EventArgs e)
        {
            if (IncrementCheck.Checked && String.IsNullOrWhiteSpace(IncrementStepValue.Text))
            {
                    MessageBox.Show("IncrementStep Value Required");
            }
            else if (Events.SelectedIndex==-1)
            {
                MessageBox.Show("Please select Interval List");
            }
            else
            {
                int index = Events.SelectedIndex;
                IAgCrdnEventIntervalList currentList;
                List<string> startTimes = new List<string>();
                List<string> stopTimes = new List<string>();
                string listName;

                if (cbStkObjects.Text.Contains("Access"))
                {
                    currentList = accessEventIntervalsCollections[index];
                    IAgCrdn currentListRe = accessEventIntervalsCollections[index] as IAgCrdn;
                    listName = currentListRe.Name;

                }
                else
                {
                    currentList = eventIntervalsCollections[index];
                    IAgCrdn currentListRe = eventIntervalsCollections[index] as IAgCrdn;
                    listName = currentListRe.Name;

                }
                IAgCrdnIntervalListResult intervals = currentList.FindIntervals();
                int numIntervals = intervals.Intervals.Count;
                for (int i = 0; i < numIntervals; i++)
                {
                    IAgCrdnInterval interval = intervals.Intervals[i];
                    string startStr = interval.Start.ToString();
                    string stopStr = interval.Stop.ToString();
                    startTimes.Add(startStr);
                    stopTimes.Add(stopStr);
                }
                CommonData.sectionList[CommonData.selectedArtic].linkedToList = true;
                string linkedText;
                string line1 = "BEGIN SMARTEPOCH \n";
                string line2 = "BEGIN EVENT \n";
                string line3 = "Type EVENT_LINKTO \n";
                string line4 = null;
                string line5 = null;
                linkedText = line1 + line2 + line3;


                if (typeEnum == 0)
                {
                    if (cbStkObjects.Text.Contains("Access"))
                    {
                        for (int i = 0; i < startTimes.Count; i++)
                        {
                            string name = "ArticCreatorList_" + listName + "_" + "StartTime" + i.ToString();
                            CommonData.sectionList[CommonData.selectedArtic].linkedToListInstantNames.Add(name);
                            IAgCrdnEvent timeEvent = null;
                            if (access.Vgt.Events.Contains(name))
                            {
                                
                                foreach (IAgCrdnEvent @event in access.Vgt.Events)
                                {
                                    // All events implement IAgCrdn interface which provides 
                                    // information about the event instance and its type. 
                                    IAgCrdn crdn = @event as IAgCrdn;
                                    if (crdn.Name==name)
                                    {
                                        timeEvent = @event;
                                    }
                                }
                            }
                            else
                            {
                                timeEvent = access.Vgt.Events.Factory.CreateEventEpoch(name, "Description");
                            }
                            IAgCrdnEventEpoch asEpoch = timeEvent as IAgCrdnEventEpoch;
                            asEpoch.Epoch = startTimes[i];
                            IAgCrdn currentEvent = (IAgCrdn)timeEvent;
                            string eventName = currentEvent.Name;
                            string pathStr = currentEvent.Path;
                            line4 = "Name " + eventName + " \n";
                            line5 = "RelativePath " + pathStr.Substring(0, pathStr.IndexOf(" ")) + " \n";
                            string line6 = "END EVENT" + " \n";
                            string line7 = "END SMARTEPOCH" + " \n";

                            string linkedTextFinal = linkedText + line4 + line5 + line6 + line7;
                            CommonData.sectionList[CommonData.selectedArtic].linkedToListStrings.Add(linkedTextFinal);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < startTimes.Count; i++)
                        {
                            string name = "ArticCreatorList_" + listName + "_" + "StartTime" + i.ToString();
                            CommonData.sectionList[CommonData.selectedArtic].linkedToListInstantNames.Add(name);
                            IAgCrdnEvent timeEvent = null;
                            if (obj.Vgt.Events.Contains(name))
                            {

                                foreach (IAgCrdnEvent @event in obj.Vgt.Events)
                                {
                                    // All events implement IAgCrdn interface which provides 
                                    // information about the event instance and its type. 
                                    IAgCrdn crdn = @event as IAgCrdn;
                                    if (crdn.Name == name)
                                    {
                                        timeEvent = @event;
                                    }
                                }
                            }
                            else
                            {
                                timeEvent = obj.Vgt.Events.Factory.CreateEventEpoch(name, "Description");
                            }
                            
                            IAgCrdnEventEpoch asEpoch = timeEvent as IAgCrdnEventEpoch;
                            asEpoch.Epoch = startTimes[i];

                            IAgCrdn currentEvent = (IAgCrdn)timeEvent;
                            string eventName = currentEvent.Name;
                            string pathStr = currentEvent.Path;
                            line4 = "Name " + eventName + " \n";
                            line5 = "RelativePath " + pathStr.Substring(0, pathStr.IndexOf(" ")) + " \n";
                            string line6 = "END EVENT" + " \n";
                            string line7 = "END SMARTEPOCH" + " \n";

                            string linkedTextFinal = linkedText + line4 + line5 + line6 + line7;
                            CommonData.sectionList[CommonData.selectedArtic].linkedToListStrings.Add(linkedTextFinal);
                        }
                    }
                }
                else if (typeEnum == 1)
                {
                    if (cbStkObjects.Text.Contains("Access"))
                    {
                        for (int i = 0; i < stopTimes.Count; i++)
                        {
                            string name = "ArticCreatorList_" + listName + "_" + "StopTime" + i.ToString();
                            CommonData.sectionList[CommonData.selectedArtic].linkedToListInstantNames.Add(name);
                            IAgCrdnEvent timeEvent = null;
                            if (access.Vgt.Events.Contains(name))
                            {

                                foreach (IAgCrdnEvent @event in access.Vgt.Events)
                                {
                                    // All events implement IAgCrdn interface which provides 
                                    // information about the event instance and its type. 
                                    IAgCrdn crdn = @event as IAgCrdn;
                                    if (crdn.Name == name)
                                    {
                                        timeEvent = @event;
                                    }
                                }
                            }
                            else
                            {
                                timeEvent = access.Vgt.Events.Factory.CreateEventEpoch(name, "Description");
                            }
                            IAgCrdnEventEpoch asEpoch = timeEvent as IAgCrdnEventEpoch;
                            asEpoch.Epoch = stopTimes[i];

                            IAgCrdn currentEvent = (IAgCrdn)timeEvent;
                            string eventName = currentEvent.Name;
                            string pathStr = currentEvent.Path;
                            line4 = "Name " + eventName + " \n";
                            line5 = "RelativePath " + pathStr.Substring(0, pathStr.IndexOf(" ")) + " \n";
                            string line6 = "END EVENT" + " \n";
                            string line7 = "END SMARTEPOCH" + " \n";

                            string linkedTextFinal = linkedText + line4 + line5 + line6 + line7;
                            CommonData.sectionList[CommonData.selectedArtic].linkedToListStrings.Add(linkedTextFinal);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < startTimes.Count; i++)
                        {
                            string name = "ArticCreatorList_" + listName + "_" + "StopTime" + i.ToString();
                            CommonData.sectionList[CommonData.selectedArtic].linkedToListInstantNames.Add(name);
                            IAgCrdnEvent timeEvent = null;
                            if (obj.Vgt.Events.Contains(name))
                            {

                                foreach (IAgCrdnEvent @event in obj.Vgt.Events)
                                {
                                    // All events implement IAgCrdn interface which provides 
                                    // information about the event instance and its type. 
                                    IAgCrdn crdn = @event as IAgCrdn;
                                    if (crdn.Name == name)
                                    {
                                        timeEvent = @event;
                                    }
                                }
                            }
                            else
                            {
                                timeEvent = obj.Vgt.Events.Factory.CreateEventEpoch(name, "Description");
                            }
                            IAgCrdnEventEpoch asEpoch = timeEvent as IAgCrdnEventEpoch;
                            asEpoch.Epoch = stopTimes[i];
                            IAgCrdn currentEvent = (IAgCrdn)timeEvent;
                            string eventName = currentEvent.Name;
                            string pathStr = currentEvent.Path;
                            line4 = "Name " + eventName + " \n";
                            line5 = "RelativePath " + pathStr.Substring(0, pathStr.IndexOf(" ")) + " \n";
                            string line6 = "END EVENT" + " \n";
                            string line7 = "END SMARTEPOCH" + " \n";

                            string linkedTextFinal = linkedText + line4 + line5 + line6 + line7;
                            CommonData.sectionList[CommonData.selectedArtic].linkedToListStrings.Add(linkedTextFinal);
                        }

                    }

                }
                if (IncrementCheck.Checked)
                {
                    CommonData.sectionList[CommonData.selectedArtic].isIncremented = true;
                    int numSections = intervals.Intervals.Count;
                    double startValueOG = Convert.ToDouble(CommonData.sectionList[CommonData.selectedArtic].startValue);
                    double stepValueDouble = Convert.ToDouble(IncrementStepValue.Text);
                    for (int i = 0; i < numSections; i++)
                    {
                        LinkedListSection current = new LinkedListSection();

                        current.startValue = Convert.ToString((startValueOG + i * stepValueDouble));
                        current.endValue = Convert.ToString((startValueOG + (i + 1) * stepValueDouble));

                        current.startTimeValue = CommonData.sectionList[CommonData.selectedArtic].startTimeValue;
                        current.durationValue = CommonData.sectionList[CommonData.selectedArtic].durationValue;
                        current.deadbandValue = CommonData.sectionList[CommonData.selectedArtic].deadbandValue;
                        current.accelValue = CommonData.sectionList[CommonData.selectedArtic].accelValue;
                        current.dutyValue = CommonData.sectionList[CommonData.selectedArtic].dutyValue;
                        current.decelValue = CommonData.sectionList[CommonData.selectedArtic].decelValue;
                        current.periodValue = CommonData.sectionList[CommonData.selectedArtic].periodValue;
                        current.sectionName = CommonData.sectionList[CommonData.selectedArtic].sectionName;
                        string section = ArticFunctions.CreateSection(CommonData.sectionList[CommonData.selectedArtic].objectName, CommonData.sectionList[CommonData.selectedArtic].articName, current.startTimeValue, current.durationValue, current.startValue, current.endValue, current.deadbandValue, current.accelValue, current.decelValue, current.dutyValue, current.periodValue, current.sectionName);
                        current.sectionText = section;

                        CommonData.sectionList[CommonData.selectedArtic].linkedListSections.Add(current);
                    }
                }
                ArticFunctions.CreateFile(CommonData.fileStr);
                this.Close();
            }
            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartTimeLink_CheckedChanged(object sender, EventArgs e)
        {
            if (StartTimeLink.Checked==true)
            {
                StopTimeLink.Checked = false;
                typeEnum = 0;
                CommonData.sectionList[CommonData.selectedArtic].linkedToStart = true;
                CommonData.sectionList[CommonData.selectedArtic].linkedToStop = false;
            }
        }

        private void StopTimeLink_CheckedChanged(object sender, EventArgs e)
        {
            if (StopTimeLink.Checked == true)
            {
                StartTimeLink.Checked = false;
                typeEnum = 1;
                CommonData.sectionList[CommonData.selectedArtic].linkedToStart = false;
                CommonData.sectionList[CommonData.selectedArtic].linkedToStop = true;
            }
        }

        private void OffsetStartLink_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void OffsetStartValue_TextChanged(object sender, EventArgs e)
        {
        }

        private void OffsetStopValue_TextChanged(object sender, EventArgs e)
        {
        }

        private void OffsetStopLink_CheckedChanged(object sender, EventArgs e)
        {
        }
        void PopulateComboBox()
        {
            string simpleName;
            string className;
            StringCollection objectNames = m_stkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");

            foreach (string objectName in objectNames)
            {
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario")
                {
                    simpleName = m_stkObjectsLibrary.ObjectName(objectName);
                    cbStkObjects.Items.Add(simpleName);
                }
            }

            if (cbStkObjects.Items.Count > 0)
            {
                cbStkObjects.SelectedIndex = 0;
            }
            IAgScenario scenario = CommonData.StkRoot.CurrentScenario as IAgScenario;
            Array accesses = scenario.GetExistingAccesses();

            int numAccesses = accesses.GetLength(0);
            for (int i = 0; i < numAccesses; i++)
            {
                string object1 = accesses.GetValue(i, 0).ToString();
                object1 = object1.Substring(object1.IndexOf('/') + 1);
                string object2 = accesses.GetValue(i, 1).ToString();
                object2 = object2.Substring(object2.IndexOf('/') + 1);

                //IAgStkAccess access = scenario.GetAccessBetweenObjectsByPath(object1, object2);
                cbStkObjects.Items.Add("Access from " + object1 + " to " + object2);
            }
        }

        private void IncrementStartValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void IncrementStepValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (IncrementCheck.Checked==true)
            {
                IncrementStepValue.Enabled = true;
            }
            else
            {
                IncrementStepValue.Enabled = false;
            }
        }
    }
}
