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
    public partial class LinkToTimeForm : Form
    {
        private StkObjectsLibrary m_stkObjectsLibrary;
        IAgCrdnEventGroup events;
        IAgCrdnEventGroup accessEvents;

        public LinkToTimeForm()
        {
            InitializeComponent();
            m_stkObjectsLibrary = new StkObjectsLibrary();
            PopulateComboBox();
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
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(path);
                if (objectName == simpleName && className!="Scenario")
                {
                    className = m_stkObjectsLibrary.ClassNameFromObjectPath(path);
                    string objectPath = className + "/" + simpleName;
                    IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(objectPath);

                    events = obj.Vgt.Events;
                    int eventCount = obj.Vgt.Events.Count;

                    for (int i = 0; i < eventCount; i++)
                    {

                        IAgCrdn currentEvent = (IAgCrdn)events[i];
                        Events.Items.Add(currentEvent.Name);
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
                        IAgStkAccess access = scenario.GetAccessBetweenObjectsByPath(object1, object2);
                        accessEvents = access.Vgt.Events;
                        for (int j = 0; j < accessEvents.Count; j++)
                        {
                            IAgCrdn currentEvent = (IAgCrdn)accessEvents[j];
                            Events.Items.Add(currentEvent.Name);
                        }
                    }

                }
            }
        }

        private void Events_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Link_Click(object sender, EventArgs e)
        {
            if (Events.SelectedIndex==-1)
            {
                MessageBox.Show("Please select Time Instance");
            }
            else
            {
                CommonData.sectionList[CommonData.selectedArtic].isLinked = true;
                string linkedText;
                string line1 = "BEGIN SMARTEPOCH \n";
                string line2 = "BEGIN EVENT \n";
                string line3 = "Type EVENT_LINKTO \n";
                string line4 = null;
                string line5 = null;
                linkedText = line1 + line2 + line3;

                if (cbStkObjects.Text.Contains("Access"))
                {
                    IAgCrdn currentEvent = (IAgCrdn)accessEvents[Events.SelectedIndex];
                    string eventName = currentEvent.Name;
                    CommonData.sectionList[CommonData.selectedArtic].linkTimeInstanceName = eventName;
                    string pathStr = currentEvent.Path;
                    line4 = "Name " + eventName + " \n";
                    line5 = "RelativePath " + pathStr.Substring(0, pathStr.IndexOf(" ")) + " \n";

                }
                else
                {
                    IAgCrdn currentEvent = (IAgCrdn)events[Events.SelectedIndex];
                    string eventName = currentEvent.Name;
                    CommonData.sectionList[CommonData.selectedArtic].linkTimeInstanceName = eventName;
                    string pathStr = currentEvent.Path;
                    line4 = "Name " + eventName + " \n";
                    line5 = "RelativePath " + pathStr.Substring(0, pathStr.IndexOf(" ")) + " \n";

                }
                string line6 = "END EVENT" + " \n";
                string line7 = "END SMARTEPOCH" + " \n";

                linkedText = linkedText + line4 + line5 + line6 + line7;
                CommonData.sectionList[CommonData.selectedArtic].linkString = linkedText;
                ArticFunctions.CreateFile(CommonData.fileStr);
                this.Close();
            }

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
            
            //Add accesses to list
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


        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
