using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.Ui.Application;
using AGI.Ui.Core;
using AGI.Ui.Plugins;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AGI.STKObjects;

namespace AstroDynUi
{
    [Guid("AAE081AD-A3CC-4A58-B6FF-4081859A38FA")]
    [ProgId("AstrodynUi")]
    [ClassInterface(ClassInterfaceType.None)]


    public class Setup : IAgUiPlugin, IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite site;
        private AgStkObjectRoot root;

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            site = PluginSite;
            IAgUiApplication AgUiApp = site.Application;
            root = AgUiApp.Personality2 as AgStkObjectRoot;
        }

        public void OnShutdown()
        {
            site = null;
        }

        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            throw new NotImplementedException();
        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            MenuBuilder.AddMenuItem("AstrodynUi.MenuCommand", "Astrodyn UI", "An educational plugin for studying Astrodynamics", null);
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //throw new NotImplementedException();
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            if (string.Compare(CommandName, "AstrodynUi.MenuCommand", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            if (string.Compare(CommandName, "AstrodynUi.MenuCommand", true) == 0)
            {
                try
                {
                    OpenUserInterface();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        public AgStkObjectRoot STKRoot
        {
            get { return root; }
        }

        public void OpenUserInterface()
        {
            IAgUiPluginWindowSite windows = site as IAgUiPluginWindowSite;

            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
            }
            else
            {
                IAgUiPluginWindowCreateParameters winParams = windows.CreateParameters();
                winParams.AllowMultiple = false;
                winParams.AssemblyPath = this.GetType().Assembly.Location;
                winParams.UserControlFullName = typeof(CustomUserInterface).FullName;
                winParams.Caption = "Astrodynamics UI Plugin";
                winParams.DockStyle = AgEDockStyle.eDockStyleDockedRight;
                winParams.Height = 400;
                winParams.Width = 550;
                object obj = windows.CreateNetToolWindowParam(this, winParams);
            }
        }

    }
}
