using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.Ui.Application;
using AGI.Ui.Core;
using AGI.Ui.Plugins;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;


//************************************************************
//Modify Plugin Title and Description 
//Rename xml file and modify "DisplayName" inside xml file
//************************************************************

namespace Stk12.UiPlugin.Articulations
{

    [Guid("87a8e802-d196-428e-b27b-8e16e0f49f07")]
    [ProgId("Stk12.UiPlugin.Articulations")]
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
        private const string m_commandText = "Stk12.UiPlugin.Articulations";
        private const string m_pluginTitle = "Articulation Creator";
        private const string m_pluginDescription = "Articulation Creator";
        private const string m_pluginConfigPage = "Stk12.UiPlugin.Articulations Config Page";
        private const string m_imageResource = "Stk12.UiPlugin.Articulations.Images.STK.ico";
        private const string m_configPath = "Stk12.UiPlugin.Articulations_config.txt";

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
            //Add a Configuration Page
            ConfigPageBuilder.AddCustomUserControlPage(this, this.GetType().Assembly.Location, typeof(CustomConfigPage).FullName, m_pluginConfigPage);

        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            //   if (m_root.GetObjectFromPath(m_pSite.Selection[0].Path).ClassName == "Scenario")
            //   {
            MenuBuilder.AddMenuItem(m_commandText, m_pluginTitle, m_pluginDescription, m_picture);
            //   }
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //Add a Toolbar Button
            ToolbarBuilder.AddButton(m_commandText, m_pluginTitle, m_pluginDescription, AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture);

        }

        public void OnShutdown()
        {
            m_pSite = null;
        }

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            m_pSite = PluginSite;
            Initialize();
        }
        #endregion

        #region IAgUiPlugin2 Implementation
        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind, IAgUiPluginMenuBuilder2 MenuBuilder)
        {
            if (MenuTitle.Contains("Scenario"))
            {
                //Insert a Menu Item
                MenuBuilder.InsertMenuItem(0, m_commandText, m_pluginTitle, m_pluginDescription, m_picture);
            }
        }
        #endregion

        #region IAgUiPlugin3DNotify Implementation

        public void OnMouseClick(IAgUiPlugin3DMouseEventArgs EventArgs, IAgUiPlugin3DNotifyContext Context)
        {
            //Array position = new object[] { EventArgs.X, EventArgs.Y };
            //IAgScenario scenario = (IAgScenario)CommonData.StkRoot.CurrentScenario;
            //IAgStkGraphicsSceneManager sceneManager = scenario.SceneManager;
            //IAgStkGraphicsScene scene = scenario.SceneManager.Scenes[EventArgs.SceneID - 1];

           // object[] cartographic = (object[])scene.Camera.WindowToCartographic("Earth", ref position);
           // string latitudeString = cartographic.GetValue(0).ToString();
           // string longitudeString = cartographic.GetValue(1).ToString();
           // double altitude = double.Parse(cartographic.GetValue(1).ToString());

            //NOTE:
            //If the angle unit is set to HMS or DMS, latitude and longitude string might not be directly converted to doubles.
        }

        public void OnPickInfo(IAgUiPlugin3DPickEventArgs EventArgs, IAgUiPlugin3DNotifyContext Context)
        {
           // throw new NotImplementedException();
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
                CommonData.StkRoot = (AgStkObjectRoot)AgUiApp.Personality2;

                //read preference file
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
            //Open a User Interface
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
                parameters.DockStyle = AgEDockStyle.eDockStyleIntegrated;
                parameters.Width = 1000;
                object obj = windows.CreateNetToolWindowParam(this, parameters);
            }
        }


    }
}
