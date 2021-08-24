using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.Ui.Application;
using AGI.Ui.Core;
using AGI.Ui.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace ConstrainedAttitude.UiPlugin
{

    [Guid("e88855d4-300e-4fa3-bd2c-e2fcf184510b")]
    [ProgId("ConstrainedAttitude.UiPlugin")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Setup : IAgUiPlugin, IAgUiPlugin2, IAgUiPlugin3DNotify, IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite m_pSite;
        private CustomUserInterface m_customUserInterface;

        private IAgProgressTrackCancel m_progress;
        private stdole.IPictureDisp m_picture;

        private string m_stringValue;
        private double m_doubleValue;
        private string m_prefPath;

        #region Object passed between different parts of the application

        internal IAgUiPluginSite Site { get { return m_pSite; } }

        internal CustomUserInterface CustomUI
        {
            get { return m_customUserInterface; }
            set { m_customUserInterface = value; }
        }


        internal IAgProgressTrackCancel ProgressBar
        {
            get { return m_progress; }
        }

        internal string StringValue
        {
            get { return m_stringValue; }
            set { m_stringValue = value; }
        }

        internal double DoubleValue
        {
            get { return m_doubleValue; }
            set { m_doubleValue = value; }
        }


        internal string PrefPath
        {
            get { return m_prefPath; }
        }

        #endregion

        //string representing your unique command
        private const string m_commandText = "ConstrainedAttitude.UiPlugin";
        private const string m_pluginTitle = "Attitude Constraint";
        private const string m_pluginDescription = "This plugin applies limits to your attitude and constrains it appropriately.";
        private const string m_pluginConfigPage = "ConstrainedAttitude.UiPlugin Config Page";
        private const string m_imageResource = "ConstrainedAttitude.UiPlugin.Images.Axes.png";
        private const string m_configPath = "ConstrainedAttitude.UiPlugin_config.txt";

        #region IAgPluginCommandTarget Implementation
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

        #region IAgUiPlugin Implementation
        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            // Add a Configuration Page
            // ConfigPageBuilder.AddCustomUserControlPage(this, this.GetType().Assembly.Location, typeof(CustomConfigPage).FullName, m_pluginConfigPage);

        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            string className = CommonData.StkRoot.GetObjectFromPath(m_pSite.Selection[0].Path).ClassName;
            if (className == "Satellite" || className == "Aircraft" || className == "Missile")
            {
                MenuBuilder.AddMenuItem(m_commandText, m_pluginTitle, m_pluginDescription, m_picture);
            }
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {

            //ToolbarBuilder.AddButton(m_commandText, m_pluginTitle, m_pluginDescription, AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture);

        }

        public void OnShutdown()
        {
            m_pSite = null;
        }

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            m_pSite = PluginSite;
            CommonData.Site = m_pSite;
            Initialize();
        }
        #endregion

        #region IAgUiPlugin2 Implementation
        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind, IAgUiPluginMenuBuilder2 MenuBuilder)
        {
            if (MenuTitle.Contains("Satellite"))
            {
                //Insert a Menu Item
                MenuBuilder.InsertMenuItem(0, m_commandText, m_pluginTitle, m_pluginDescription, m_picture);
            }
        }
        #endregion

        #region IAgUiPlugin3DNotify Implementation

        public void OnMouseClick(IAgUiPlugin3DMouseEventArgs EventArgs, IAgUiPlugin3DNotifyContext Context)
        {
   
        }

        public void OnPickInfo(IAgUiPlugin3DPickEventArgs EventArgs, IAgUiPlugin3DNotifyContext Context)
        {

        }

        #endregion


        public void Initialize()
        {
            if (m_pSite != null)
            {
                Image menuImage = null;

                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                menuImage = Image.FromStream(currentAssembly.GetManifestResourceStream(m_imageResource));
                m_picture = OlePictureHelper.OlePictureFromImage(menuImage);
                IAgUiApplication AgUiApp = m_pSite.Application;

                // Set STK Root object
                CommonData.StkRoot = (AgStkObjectRoot)AgUiApp.Personality2;

                // Read preference file
                AGI.STKUtil.IAgExecCmdResult cmdResult = CommonData.StkRoot.ExecuteCommand("GetDirectory / DefaultUser");
                string userPath = cmdResult[0].ToString();
                m_prefPath = Path.Combine(userPath, m_configPath);
                if (File.Exists(m_prefPath))
                {
                    using (StreamReader streamReader = new StreamReader(m_prefPath))
                    {
                        m_stringValue = streamReader.ReadLine();
                        m_doubleValue = Double.Parse(streamReader.ReadLine());
                        streamReader.Close();
                    }
                }
                else
                {
                    m_stringValue = "";
                    m_doubleValue = 0.0;
                }

            }
            else
            {
                MessageBox.Show("Error: Couldn't aquire STK Root Object");
            }
        }

        private void OpenUserInterface()
        {
            // Open a User Interface
            IAgUiPluginWindowSite windows = m_pSite as IAgUiPluginWindowSite;
            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
            }
            else
            {
                IAgUiPluginWindowCreateParameters parameters = windows.CreateParameters();
                parameters.AllowMultiple = false;
                parameters.AssemblyPath = this.GetType().Assembly.Location;
                parameters.UserControlFullName = typeof(CustomUserInterface).FullName;
                parameters.Caption = m_pluginTitle;
                parameters.DockStyle = AgEDockStyle.eDockStyleFloating;
                parameters.Width = 375;
                parameters.Height = 350;
                IAgUiWindow window = windows.CreateNetToolWindowParam(this, parameters);
            }
        }

    }

    public enum GlobeEventType
    {
        Location,
        Object,
        Globe,
        Screen,
        None
    }
}
