using AGI.STKObjects;
using AGI.Ui.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace $safeprojectname$
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;


        public CustomUserInterface()
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

            //EXAMPLE: Hooking to STK Exents
            CommonData.StkRoot.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            CommonData.StkRoot.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);

            //EXAMPLE: Using preference value
            //m_uiPlugin.DoubleValue;

            //EXAMPLE: Populate combo box with STK Objects
            PopulateComboBox();
        }


        #endregion

        #region Sample code
        void m_root_OnStkObjectDeleted(object Sender)
        {
            string objectPath = Sender.ToString();
            string simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectPath);
            cbStkObjects.Items.Remove(simpleName);
        }

        void m_root_OnStkObjectAdded(object Sender)
        {
            string objectPath = Sender.ToString();
            string simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectPath);
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

                if (className != "Scenario")
                {
                    simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectName);
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
    }
}
