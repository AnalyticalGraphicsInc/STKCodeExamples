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


namespace Stk12.UiPlugin.TetkExample
{
    [Guid("c6a0bdb5-0820-4ebf-b27b-9fcce1ce262d")]
    [ProgId("Stk12.UiPlugin.TetkExample")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Setup : IAgUiPlugin, IAgUiPlugin2, IAgUiPlugin3DNotify, IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite m_pSite;
        private LoadDataUserInterface m_customUserInterface;

        private IAgProgressTrackCancel m_progress;
        private stdole.IPictureDisp m_picture;

        private string m_stringValue;
        private double m_doubleValue;
        private string m_prefPath;
        // private GlobeEventType m_globeEventType = GlobeEventType.None;

        #region Object passed between different parts of the application

        internal IAgUiPluginSite Site { get { return m_pSite; } }

        internal LoadDataUserInterface CustomUI
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
        private const string m_commandTextLoadData = "Stk12.UiPlugin.TetkExample.LoadData";
        private const string m_commandTextCreateTracks = "Stk12.UiPlugin.TetkExample.CreateTracks";
        private const string m_commandTextTrackComparison = "Stk12.UiPlugin.TetkExample.TrackComparison";
        private const string m_commandTextTestConnect = "Stk12.UiPlugin.TetkExample.TestConnect";

        private const string m_pluginTitleLoadData = "Load TETK Data";
        private const string m_pluginTitleCreateTracks = "Create TETK Tracks";
        private const string m_pluginTitleTrackComparison = "Compute Track Comparison";
        private const string m_pluginTitleTestConnect = "Test Connect Command";


        private const string m_pluginDescription = "PluginDESCRIPTION";
        private const string m_pluginConfigPage = "Stk12.UiPlugin.TetkExample Config Page";
        private const string m_imageResource = "Stk12.UiPlugin.TetkExample.Images.Target.png";
        private const string m_configPath = "Stk12.UiPlugin.TetkExample_config.txt";

        #region IAgPluginCommandTarget Implementation
        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            //Controls what a command does
            if (string.Compare(CommandName, m_commandTextLoadData, true) == 0)
            {
                m_progress = TrackCancel;
                OpenLoadDataUserInterface();
            }
            if (string.Compare(CommandName, m_commandTextCreateTracks, true) == 0)
            {
                m_progress = TrackCancel;
                OpenCreateTracksUserInterface();
            }
            if (string.Compare(CommandName, m_commandTextTrackComparison, true) == 0)
            {
                m_progress = TrackCancel;
                OpenTrackComparisonUserInterface();
            }
            if (string.Compare(CommandName, m_commandTextTestConnect, true) == 0)
            {
                m_progress = TrackCancel;
                OpenTestConnectUserInterface();
            }
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            //Enable commands
            if (string.Compare(CommandName, m_commandTextLoadData, true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }

            if (string.Compare(CommandName, m_commandTextCreateTracks, true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }

            if (string.Compare(CommandName, m_commandTextTrackComparison, true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            if (string.Compare(CommandName, m_commandTextTestConnect, true) == 0)
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
            // ConfigPageBuilder.AddCustomUserControlPage(this, this.GetType().Assembly.Location, typeof(CustomConfigPage).FullName, m_pluginConfigPage);
        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            //MenuBuilder.AddMenuItem(m_commandText, m_pluginTitle, m_pluginDescription, m_picture);
            //if (CommonData.StkRoot.GetObjectFromPath(m_pSite.Selection[0].Path).ClassName == "Scenario")
            //{
            //    IAgUiPluginMenuBuilder SubMenuBuilder = MenuBuilder.AddPopupMenu("Mouse Options");
            //    SubMenuBuilder.AddMenuItem(m_commandTextLocation, "Location", "Location", m_picture);
            //    SubMenuBuilder.AddMenuItem(m_commandTextObject, "Object", "Object", m_picture);      
            //}
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //Add Toolbar Buttons
            ToolbarBuilder.AddButton(m_commandTextLoadData, "Load Data", m_pluginDescription, AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture);
            ToolbarBuilder.AddButton(m_commandTextCreateTracks, "Create Tracks", m_pluginDescription, AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture);
            ToolbarBuilder.AddButton(m_commandTextTrackComparison, "Workflow 1", m_pluginDescription, AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, null);
            ToolbarBuilder.AddButton(m_commandTextTestConnect, "Test Connect", m_pluginDescription, AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, null);
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
                //MenuBuilder.InsertMenuItem(0, m_commandText, m_pluginTitle, m_pluginDescription, m_picture);
            }
        }
        #endregion

        #region IAgUiPlugin3DNotify Implementation

        public void OnMouseClick(IAgUiPlugin3DMouseEventArgs EventArgs, IAgUiPlugin3DNotifyContext Context)
        {
            //IAgScenario scenario = (IAgScenario)CommonData.StkRoot.CurrentScenario;
            //IAgStkGraphicsSceneManager sceneManager = scenario.SceneManager;
            //IAgStkGraphicsScene scene = scenario.SceneManager.Scenes[EventArgs.SceneID - 1];


            //switch (m_globeEventType)
            //{
            //    case GlobeEventType.Location:
            //        Array position = new object[] { EventArgs.X, EventArgs.Y };
            //        object[] cartographic = (object[])scene.Camera.WindowToCartographic("Earth", ref position);
            //        string latitudeString = cartographic.GetValue(0).ToString();
            //        string longitudeString = cartographic.GetValue(1).ToString();
            //        double altitude = double.Parse(cartographic.GetValue(1).ToString());
            //        break;
            //    case GlobeEventType.Object:
            //        var objects = Context.PickRectangular(EventArgs.X-10,EventArgs.Y+10, EventArgs.X + 10, EventArgs.Y -10);
            //        List<string> objectPaths = new List<string>();
            //        foreach (string s in objects)
            //        {

            //        }
            //        DeleteObjects(objectPaths);
            //        break;
            //    case GlobeEventType.Globe:
            //        Context.RubberBandLineWidth = 2;
            //        Context.RubberBandColor = System.Drawing.Color.White;
            //        Context.ActivateRubberBandOnCentralBody();
            //        break;
            //    case GlobeEventType.Screen:
            //        Context.RubberBandLineWidth = 2;
            //        Context.RubberBandColor = System.Drawing.Color.Yellow;
            //        Context.ActivateRubberBand();
            //        break;
            //    case GlobeEventType.None:
            //        break;
            //    default:
            //        break;
            //}
        }

        public void OnPickInfo(IAgUiPlugin3DPickEventArgs EventArgs, IAgUiPlugin3DNotifyContext Context)
        {
            //Array region = EventArgs.SelectedRegion;            
            //List<string> objectPaths = new List<string>();
            //switch (EventArgs.PickType)
            //{
            //    case AgEUiPlugin3DPickType.eUiPlugin3DPickTypeProjectedOnCentralBody:
            //        double west = (double)region.GetValue(0);
            //        double south = (double)region.GetValue(1);
            //        double east = (double)region.GetValue(2);
            //        double north = (double)region.GetValue(3);
            //        var objects = Context.PickExtent(west, south, east, north);
            //        foreach (string s in objects)
            //        {
            //            objectPaths.Add(s);
            //        }
            //        break;
            //    case AgEUiPlugin3DPickType.eUiPlugin3DPickTypeRubberBand:
            //        int left = (int)(double)region.GetValue(0);
            //        int bottom = (int)(double)region.GetValue(1);
            //        int right = (int)(double)region.GetValue(2);
            //        int top = (int)(double)region.GetValue(3);
            //        var objects1 = Context.PickRectangular(left, bottom, right, top);
            //        foreach (string s in objects1)
            //        {
            //            objectPaths.Add(s);
            //        }
            //        break;
            //    default:
            //        break;
            //}
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

        private void OpenLoadDataUserInterface()
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
                parameters.UserControlFullName = typeof(LoadDataUserInterface).FullName;
                parameters.Caption = m_pluginTitleLoadData;
                parameters.DockStyle = AgEDockStyle.eDockStyleDockedRight;
                parameters.Width = 400;
                parameters.Height = 400;
                IAgUiWindow window = windows.CreateNetToolWindowParam(this, parameters);
            }
        }

        private void OpenCreateTracksUserInterface()
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
                parameters.UserControlFullName = typeof(CreateTracksUserInterface).FullName;
                parameters.Caption = m_pluginTitleCreateTracks;
                parameters.DockStyle = AgEDockStyle.eDockStyleDockedRight;
                parameters.Width = 400;
                parameters.Height = 400;
                IAgUiWindow window = windows.CreateNetToolWindowParam(this, parameters);
            }
        }

        private void OpenTrackComparisonUserInterface()
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
                parameters.UserControlFullName = typeof(TrackComparisonUserInterface).FullName;
                parameters.Caption = m_pluginTitleTrackComparison;
                parameters.DockStyle = AgEDockStyle.eDockStyleDockedRight;
                parameters.Width = 400;
                parameters.Height = 400;
                IAgUiWindow window = windows.CreateNetToolWindowParam(this, parameters);
            }
        }

        private void OpenTestConnectUserInterface()
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
                parameters.UserControlFullName = typeof(TestConnectUserInterface).FullName;
                parameters.Caption = m_pluginTitleTestConnect;
                parameters.DockStyle = AgEDockStyle.eDockStyleDockedRight;
                parameters.Width = 400;
                parameters.Height = 400;
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
