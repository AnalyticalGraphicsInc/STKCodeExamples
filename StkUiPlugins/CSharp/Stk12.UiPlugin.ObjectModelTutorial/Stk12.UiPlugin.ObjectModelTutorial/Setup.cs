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


namespace Stk12.UiPlugin.ObjectModelTutorial
{

    [Guid("7f70d874-003f-4467-ad62-64d81620913f")]
    [ProgId("Stk12.UiPlugin.ObjectModelTutorial")]
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
        // private GlobeEventType m_globeEventType = GlobeEventType.None;

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
        private const string m_commandText = "Stk12.UiPlugin.ObjectModelTutorial";
        //private const string m_commandTextLocation = "Stk12.UiPlugin.ObjectModelTutorial.Location";
        //private const string m_commandTextGlobe = "Stk12.UiPlugin.ObjectModelTutorial.Globe";
        //private const string m_commandTextObject = "Stk12.UiPlugin.ObjectModelTutorial.Object";
        //private const string m_commandTextScreen = "Stk12.UiPlugin.ObjectModelTutorial.Screen";
        //private const string m_commandTextNone = "Stk12.UiPlugin.ObjectModelTutorial.None";
        private const string m_pluginTitle = "ObjectModelTutorial";
        private const string m_pluginDescription = "Object Model Tutorial";
        private const string m_pluginConfigPage = "Stk12.UiPlugin.ObjectModelTutorial Config Page";
        private const string m_imageResource = "Stk12.UiPlugin.ObjectModelTutorial.Images.Satellite.png";
        private const string m_configPath = "Stk12.UiPlugin.ObjectModelTutorial_config.txt";

        #region IAgPluginCommandTarget Implementation
        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            //Controls what a command does
            if (string.Compare(CommandName, m_commandText, true) == 0)
            {
                m_progress = TrackCancel;
                OpenUserInterface();
            }
            //if (string.Compare(CommandName, m_commandTextGlobe, true) == 0)
            //{
            //    m_globeEventType = GlobeEventType.Globe;
            //}
            //if (string.Compare(CommandName, m_commandTextLocation, true) == 0)
            //{
            //    m_globeEventType = GlobeEventType.Location;
            //}
            //if (string.Compare(CommandName, m_commandTextObject, true) == 0)
            //{
            //    m_globeEventType = GlobeEventType.Object;
            //}
            //if (string.Compare(CommandName, m_commandTextScreen, true) == 0)
            //{
            //    m_globeEventType = GlobeEventType.Screen;
            //}
            //if (string.Compare(CommandName, m_commandTextNone, true) == 0)
            //{
            //    m_globeEventType = GlobeEventType.None;
            //}
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            //Enable commands
            if (string.Compare(CommandName, m_commandText, true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            //if (string.Compare(CommandName, m_commandTextGlobe, true) == 0)
            //{
            //    return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            //}
            //if (string.Compare(CommandName, m_commandTextLocation, true) == 0)
            //{
            //    return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            //}
            //if (string.Compare(CommandName, m_commandTextNone, true) == 0)
            //{
            //    return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            //}
            //if (string.Compare(CommandName, m_commandTextObject, true) == 0)
            //{
            //    return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            //}
            //if (string.Compare(CommandName, m_commandTextScreen, true) == 0)
            //{
            //    return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            //}
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
            MenuBuilder.AddMenuItem(m_commandText, m_pluginTitle, m_pluginDescription, m_picture);
            //if (CommonData.StkRoot.GetObjectFromPath(m_pSite.Selection[0].Path).ClassName == "Scenario")
            //{
            //    IAgUiPluginMenuBuilder SubMenuBuilder = MenuBuilder.AddPopupMenu("Mouse Options");
            //    SubMenuBuilder.AddMenuItem(m_commandTextLocation, "Location", "Location", m_picture);
            //    SubMenuBuilder.AddMenuItem(m_commandTextObject, "Object", "Object", m_picture);
            //    SubMenuBuilder.AddMenuItem(m_commandTextGlobe, "Globe", "Globe", m_picture);
            //    SubMenuBuilder.AddMenuItem(m_commandTextScreen, "Screen", "Screen", m_picture);
            //    SubMenuBuilder.AddMenuItem(m_commandTextNone, "None", "None", m_picture);            
            //}
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //Add a Toolbar Button

            //ToolbarBuilder.AddButton(m_commandTextLocation, "Location", "Location", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture);
            //ToolbarBuilder.AddButton(m_commandTextObject, "Object", "Object", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture);
            //ToolbarBuilder.AddButton(m_commandTextGlobe, "Globe", "Globe", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture);
            //ToolbarBuilder.AddButton(m_commandTextScreen, "Screen", "Screen", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture);
            //ToolbarBuilder.AddButton(m_commandTextNone, "None", "None", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, m_picture);
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
