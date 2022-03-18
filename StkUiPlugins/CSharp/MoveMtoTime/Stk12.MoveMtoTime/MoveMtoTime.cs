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

namespace Agi.UiPlugin.MoveMtoTime
{
    [Guid("4d254910-b8a6-4a89-87af-569d6ae8d53a")]
    [ProgId("Agi.UiPlugin.MoveMtoTime")]
    [ClassInterface(ClassInterfaceType.None)]
    public class MoveMtoTime : IAgUiPlugin, IAgUiPluginCommandTarget, IAgUiPlugin2
    {
        private IAgUiPluginSite m_pSite;
        private CustomUserInterface m_customUserInterface;
        private AgStkObjectRootClass m_root;
        private IAgProgressTrackCancel m_progress;

        private bool m_integrate = true;

        private const string m_imageResource = "Agi.UiPlugin.MoveMtoTime.MTO.bmp";
        private const string m_commandText = "Agi.UiPlugin.MoveMtoTime";
        private const string m_pluginTitle = "Move MTO Time";
        private const string m_pluginDescription = "Shifts MTO tracks by the specified time.";

        internal IAgUiPluginSite Site { get { return m_pSite; } }

        #region IAgUiPlugin Members

        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            //Add a Configuration Page
            //ConfigPageBuilder.AddCustomUserControlPage(this, this.GetType().Assembly.Location, typeof(CustomConfigPage).FullName, "Basic CSharp Config Page");
        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            if (m_integrate)
            {
                stdole.IPictureDisp picture;
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                Image icon = Image.FromStream(currentAssembly.GetManifestResourceStream(m_imageResource));
                picture = OlePictureHelper.OlePictureFromImage(icon);
                //Add a Menu Item
                MenuBuilder.AddMenuItem(m_commandText, m_pluginTitle, m_pluginDescription, picture);
            }
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //converting an ico file to be used as the image for toolbar button
            stdole.IPictureDisp picture;
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Image icon = Image.FromStream(currentAssembly.GetManifestResourceStream(m_imageResource));
            picture = OlePictureHelper.OlePictureFromImage(icon);
            //Add a Toolbar Button
            ToolbarBuilder.AddButton(m_commandText, m_pluginTitle, m_pluginDescription, AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, picture);
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
            m_root = AgUiApp.Personality2 as AgStkObjectRootClass;
        }

        #endregion

        #region IAgUiPlugin2 Members

        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind, IAgUiPluginMenuBuilder2 MenuBuilder)
        {
            stdole.IPictureDisp picture;
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Image icon = Image.FromStream(currentAssembly.GetManifestResourceStream(m_imageResource));
            picture = OlePictureHelper.OlePictureFromImage(icon);
        }

        #endregion

        #region IAgUiPluginCommandTarget Members

        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            //Controls what a command does
            if (string.Compare(CommandName, m_commandText, true) == 0) 
            {
                m_progress = TrackCancel;
                OpenUserInterface();
            }
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            //Enable commands
            if (string.Compare(CommandName, m_commandText, true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        #endregion

        internal CustomUserInterface customUI
        {
            get { return m_customUserInterface; }
            set { m_customUserInterface = value; }
        }

        internal bool Integrate
        {
            get { return m_integrate; }
            set { m_integrate = value; }
        }

        internal AgStkObjectRootClass STKRoot
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
                @params.UserControlFullName = typeof(CustomUserInterface).FullName;
                @params.Caption = m_pluginTitle;
                @params.DockStyle = AgEDockStyle.eDockStyleDockedLeft;
                @params.Height = 200;
                object obj = windows.CreateNetToolWindowParam(this, @params);
            }
        }
    }
}
