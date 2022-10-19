using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.Ui.Application;
using AGI.Ui.Core;
using AGI.Ui.Plugins;
using stdole;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AGI.STKObjects;

namespace Stk12.UiPlugin.HorizonsEphemImporter
{
    [Guid("C006BB59-B1EC-48DA-8DBF-B174151EA4AB")]
    [ProgId("Stk12.UiPlugin.HorizonsEphemImporter")]
    [ClassInterface(ClassInterfaceType.None)]

    public class HorizonsEphemImporter: IAgUiPlugin, IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite m_psite;
        private AgStkObjectRoot m_root;

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            // Setup and attach to STK
            m_psite = PluginSite;
            IAgUiApplication AgUiApp = m_psite.Application;
            m_root = AgUiApp.Personality2 as AgStkObjectRoot;
        }

        public void OnShutdown()
        {
            m_psite = null;
        }

        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {

        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            // Limit plugin context menu to scenario object only
            bool isValidType = true;
            List<string> supportedObjectClasses = new List<string>(new string[] { "SCENARIO"});
            IAgStkObject stkobject;

            IAgUiPluginSelectedObjectCollection selectedobjects = m_psite.Selection;
            foreach (IAgUiPluginSelectedObject selectedobject in selectedobjects)
            {
                stkobject = m_root.GetObjectFromPath(selectedobject.Path);
                if (!supportedObjectClasses.Contains(stkobject.ClassName.ToUpperInvariant()))
                {
                    isValidType = false;
                }
            }

            if (isValidType)
            {
                MenuBuilder.AddMenuItem("HorizonsEphemImporter.OpenPlugin", "Horizons Importer", "JPL Horizons Ephemeris Importer", null);
            }
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            ToolbarBuilder.AddButton("HorizonsEphemImporter.OpenPlugin", "Horizons Importer", "JPL Horizons Ephemeris Importer", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, null);
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            if (string.Compare(CommandName, "HorizonsEphemImporter.OpenPlugin", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            if (string.Compare(CommandName, "HorizonsEphemImporter.OpenPlugin", true) == 0)
            {
                OpenUserInterface();
            }
        }

        public AgStkObjectRoot STKRoot
        {
            get { return m_root; }
        }

        public void OpenUserInterface()
        {
            IAgUiPluginWindowSite windows = m_psite as IAgUiPluginWindowSite;

            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
            }
            else
            {
                IAgUiPluginWindowCreateParameters winParams = windows.CreateParameters();
                winParams.AllowMultiple = false;
                winParams.AssemblyPath = this.GetType().Assembly.Location;
                winParams.UserControlFullName = typeof(ImporterGUI).FullName;
                winParams.Caption = "Horizons Ephemeris Importer";
                winParams.DockStyle = AgEDockStyle.eDockStyleIntegrated;
                winParams.Width = 335;
                winParams.Height = 390;
                winParams.X = 0;
                winParams.Y = 0;
                object obj = windows.CreateNetToolWindowParam(this, winParams);
            }
        }
    }
}
