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

namespace OperatorsToolbox
{

    [Guid("e3bd0800-0fe0-4bad-b140-dc71f539fbc9")]
    [ProgId("OperatorsToolbox")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Setup : IAgUiPlugin, IAgUiPlugin2, IAgUiPlugin3DNotify, IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite _mPSite;
        private CustomUserInterface _mCustomUserInterface;

        private IAgProgressTrackCancel _mProgress;
        private stdole.IPictureDisp _mPicture;

        private string _mStringValue;
        private double _mDoubleValue;
        private string _mPrefPath;

        #region Object passed between different parts of the application

        internal IAgUiPluginSite Site { get { return _mPSite; } }

        internal CustomUserInterface CustomUi
        {
            get { return _mCustomUserInterface; }
            set { _mCustomUserInterface = value; }
        }


        internal IAgProgressTrackCancel ProgressBar
        {
            get { return _mProgress; }
        }

        internal string StringValue
        {
            get { return _mStringValue; }
            set { _mStringValue = value; }
        }

        internal double DoubleValue
        {
            get { return _mDoubleValue; }
            set { _mDoubleValue = value; }
        }

        internal string PrefPath
        {
            get { return _mPrefPath; }
        }

        #endregion

        //string representing your unique command
        private const string MCommandText = "OperatorsToolbox";
        private const string MPluginTitle = "Operator's Toolbox";
        private const string MPluginDescription = "Custom UI plugins for operators";
        private const string MPluginConfigPage = "OperatorsToolbox Config Page";
        private const string MImageResource = "OperatorsToolbox.Images.operatorsToolBox.ico";
        private const string MConfigPath = "OperatorsToolbox_config.txt";

        #region IAgPluginCommandTarget Implementation
        public void Exec(string commandName, IAgProgressTrackCancel trackCancel, IAgUiPluginCommandParameters parameters)
        {
            //Controls what a command does
            if (string.Compare(commandName, MCommandText, true) == 0)
            {
                _mProgress = trackCancel;
                OpenUserInterface();
            }
        }

        public AgEUiPluginCommandState QueryState(string commandName)
        {
            //Enable commands
            if (string.Compare(commandName, MCommandText, true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }
        #endregion

        #region IAgUiPlugin Implementation
        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder configPageBuilder)
        {
            //Add a Configuration Page
            configPageBuilder.AddCustomUserControlPage(this, this.GetType().Assembly.Location, typeof(CustomConfigPage).FullName, MPluginConfigPage);

        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder menuBuilder)
        {
            //   if (m_root.GetObjectFromPath(m_pSite.Selection[0].Path).ClassName == "Scenario")
            //   {
            menuBuilder.AddMenuItem(MCommandText, MPluginTitle, MPluginDescription, _mPicture);
            //   }
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder toolbarBuilder)
        {
            //Add a Toolbar Button
            toolbarBuilder.AddButton(MCommandText, MPluginTitle, MPluginDescription, AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, _mPicture);

        }

        public void OnShutdown()
        {
            _mPSite = null;
        }

        public void OnStartup(IAgUiPluginSite pluginSite)
        {
            _mPSite = pluginSite;
            Initialize();
        }
        #endregion

        #region IAgUiPlugin2 Implementation
        public void OnDisplayMenu(string menuTitle, AgEUiPluginMenuBarKind menuBarKind, IAgUiPluginMenuBuilder2 menuBuilder)
        {
            if (menuTitle.Contains("Scenario"))
            {
                //Insert a Menu Item
                menuBuilder.InsertMenuItem(0, MCommandText, MPluginTitle, MPluginDescription, _mPicture);
            }
        }
        #endregion

        #region IAgUiPlugin3DNotify Implementation

        public void OnMouseClick(IAgUiPlugin3DMouseEventArgs eventArgs, IAgUiPlugin3DNotifyContext context)
        {
            Array position = new object[] { eventArgs.X, eventArgs.Y };
            IAgScenario scenario = (IAgScenario)CommonData.StkRoot.CurrentScenario;
            IAgStkGraphicsSceneManager sceneManager = scenario.SceneManager;
            IAgStkGraphicsScene scene = scenario.SceneManager.Scenes[eventArgs.SceneID - 1];

            //object[] cartographic = (object[])scene.Camera.WindowToCartographic("Earth", ref position);
            //string latitudeString = cartographic.GetValue(0).ToString();
            //string longitudeString = cartographic.GetValue(1).ToString();
            //double altitude = double.Parse(cartographic.GetValue(1).ToString());

            //NOTE:
            //If the angle unit is set to HMS or DMS, latitude and longitude string might not be directly converted to doubles.
        }

        public void OnPickInfo(IAgUiPlugin3DPickEventArgs eventArgs, IAgUiPlugin3DNotifyContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void Initialize()
        {
            if (_mPSite != null)
            {
                Image menuImage = null;

                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                menuImage = Image.FromStream(currentAssembly.GetManifestResourceStream(MImageResource));
                _mPicture = OlePictureHelper.OlePictureFromImage(menuImage);
                IAgUiApplication agUiApp = _mPSite.Application;
                CommonData.StkRoot = (AgStkObjectRoot)agUiApp.Personality2;
                CommonData.Preferences = new AppPreferences();

                //read preference file
                AGI.STKUtil.IAgExecCmdResult cmdResult = CommonData.StkRoot.ExecuteCommand("GetDirectory / DefaultUser");
                string userPath = cmdResult[0].ToString();
                _mPrefPath = Path.Combine(userPath, MConfigPath);
                if (File.Exists(_mPrefPath))
                {
                    using (StreamReader streamReader = new StreamReader(_mPrefPath))
                    {
                        _mStringValue = streamReader.ReadLine();
                        _mDoubleValue = Double.Parse(streamReader.ReadLine());
                        streamReader.Close();
                    }
                }
                else
                {
                    _mStringValue = "";
                    _mDoubleValue = 0.0;
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
            IAgUiPluginWindowSite windows = _mPSite as IAgUiPluginWindowSite;
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
                parameters.Caption = MPluginTitle;
                parameters.DockStyle = AgEDockStyle.eDockStyleDockedRight;
                parameters.Width = 65;
                object obj = windows.CreateNetToolWindowParam(this, parameters);
                
            }
        }


    }
}
