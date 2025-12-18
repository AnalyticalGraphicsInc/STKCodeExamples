using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using AGI.Ui.Plugins;
using AGI.Ui.Core;
using AGI.STKObjects;
using AGI.Ui.Application;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using Agi.Ui.Directions.Properties;

namespace Agi.Ui.Directions
{
    [Guid("AADD6E66-CF7C-43aa-BFBD-866FC675676C")]
    [ProgId("Agi.Ui.Directions")]
    [ClassInterface(ClassInterfaceType.None)]
    public class BasicCSharpPlugin : IAgUiPlugin, IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite m_pSite;
        private DirectionsUserInterface m_customUserInterface;
        private AgStkObjectRoot m_root;
        private IAgProgressTrackCancel m_progress;

        private bool m_integrate = true;

        internal IAgUiPluginSite Site { get { return m_pSite; } }

        #region IAgUiPlugin Members

        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            //don't really need a Configuration Page in this example
            // ConfigPageBuilder.AddCustomUserControlPage(this, this.GetType().Assembly.Location, typeof(CustomConfigPage).FullName, "Basic CSharp Config Page");
        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            if (m_integrate)
            {
                stdole.IPictureDisp picture;
                picture = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.IconToIPicture(Resources.direction64);
                //Add a Menu Item
                MenuBuilder.AddMenuItem("AGI.BasicCSharpPlugin.MyFirstContextMenuCommand", "Directions", "Open a Custom user interface.", picture);
            }
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //converting an ico file to be used as the image for toolbat button
            stdole.IPictureDisp picture;
            picture = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.IconToIPicture(Resources.direction64);
            //Add a Toolbar Button
            ToolbarBuilder.AddButton("AGI.BasicCSharpPlugin.MyFirstCommand", "Add new GroundVehicle from directions", "Open a Custom user interface.", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, picture);
        }

        public void OnShutdown()
        {
            m_pSite = null;
        }

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            m_pSite = PluginSite;
            //Get the AgStkObjectRoot
            IAgUiApplication AgUiApp = m_pSite.Application;
            m_root = AgUiApp.Personality2 as AgStkObjectRoot;
        }

        #endregion

        #region IAgUiPluginCommandTarget Members

        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            //Controls what a command does
            if (string.Compare(CommandName, "AGI.BasicCSharpPlugin.MyFirstCommand", true) == 0 || string.Compare(CommandName, "AGI.BasicCSharpPlugin.MyFirstContextMenuCommand", true) == 0)
            {
                m_progress = TrackCancel;
                OpenUserInterface();
            }
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            //Enable commands
            if (string.Compare(CommandName, "AGI.BasicCSharpPlugin.MyFirstCommand", true) == 0 || string.Compare(CommandName, "AGI.BasicCSharpPlugin.MyFirstContextMenuCommand", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        #endregion

        internal DirectionsUserInterface customUI
        {
            get { return m_customUserInterface; }
            set { m_customUserInterface = value; }
        }

        internal bool Integrate
        {
            get { return m_integrate; }
            set { m_integrate = value; }
        }

        internal AgStkObjectRoot STKRoot
        {
            get { return m_root; }
        }

        internal IAgProgressTrackCancel ProgressBar
        {
            get { return m_progress; }
        }

        private void OpenUserInterface()
        {
            //Open a User Interface
            IAgUiPluginWindowSite windows = m_pSite as IAgUiPluginWindowSite;
            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
            }
            else
            {
                IAgUiPluginWindowCreateParameters @params = windows.CreateParameters();
                @params.AllowMultiple = false;
                @params.AssemblyPath = this.GetType().Assembly.Location;
                @params.UserControlFullName = typeof(DirectionsUserInterface).FullName;
                @params.Caption = "Direction User Interface";
                @params.DockStyle = AgEDockStyle.eDockStyleDockedBottom;
                @params.Height = 200;
                object obj = windows.CreateNetToolWindowParam(this, @params);
            }
        }
    }
}
