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
using AGI.STKUtil;

namespace PlanetsToggle_10
{
    [Guid("692569be-0013-4f49-ae1b-b6c3e2a72a95")]
    [ProgId("PlanetsToggle_10")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PlanetsToggle_10_CSharpPlugin : IAgUiPlugin, IAgUiPluginCommandTarget, IAgUiPlugin2
    {
        private IAgUiPluginSite m_pSite;
        private AgStkObjectRootClass m_root;
        internal IAgUiPluginSite Site { get { return m_pSite; } }

        #region IAgUiPlugin Members

        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
           }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
           
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //converting an ico file to be used as the image for toolbat button
            stdole.IPictureDisp picture, picture2;
            //string imageResource = "PlanetsToggle_10.STK.ico";
            //Assembly currentAssembly = Assembly.GetExecutingAssembly();
            //Icon icon = new Icon(currentAssembly.GetManifestResourceStream(imageResource));
            
            picture = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.ImageToIPicture(PlanetsToggle_10.Properties.Resources.addPlanets);
            picture2 = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.ImageToIPicture(PlanetsToggle_10.Properties.Resources.zoomSolarSystem);
            //picture = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.IconToIPicture(icon);
            //picture2 = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.IconToIPicture(icon);
            //stdole.IPictureDisp m_picture = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.ImageToIPicture();
            //Add a Toolbar Button
            ToolbarBuilder.AddButton("AGI.PlanetsToggleCSharpPlugin.PlanetsToggle", "Toggle Plents", "Toggle Plents", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, picture);
            ToolbarBuilder.AddButton("AGI.PlanetsToggleCSharpPlugin.SetPlanetView", "Set Planetary View", "Set Planetary View", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, picture2);
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
            planetsShown = false;
        }

        #endregion

        #region IAgUiPlugin2 Members

        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind, IAgUiPluginMenuBuilder2 MenuBuilder)
        {
        }

        #endregion

        #region IAgUiPluginCommandTarget Members

        public void setViewVGT()
        {
            IAgExecCmdResult result = m_root.ExecuteCommand("VectorTool_R * Exists \"CentralBody/Sun SunView Point\"");
            if (result[0].CompareTo("1") != 0)
            {
                m_root.ExecuteCommand("VectorTool * CentralBody/Sun Create Point SunView \"Fixed in System\" Cartesian 1e13 1e13 1e13 \"CentralBody/Sun J2000\"");
            }
            m_root.ExecuteCommand("VO * ViewFromTo Normal From \"CentralBody/Sun SunView Point\" to CentralBody/Sun");

        }

        public void setViewCameraPath()
        {
            m_root.ExecuteCommand("VO * CameraControl SetAllPaths Off");
            try
            {
                m_root.ExecuteCommand("VO * CameraControl CameraPath Add Name \"SolarSystemView\"");
                m_root.ExecuteCommand("VO * CameraControl KeyframeProps \"SolarSystemView\" ReferenceAxes \"CentralBody/Sun J2000 Axes\"");
                m_root.ExecuteCommand("VO * CameraControl Keyframes \"SolarSystemView\" Add");
                m_root.ExecuteCommand("VO * CameraControl Keyframes \"SolarSystemView\" Modify 1 Position 1e13 1e13 1e13");
            }
            catch { }
            m_root.ExecuteCommand("VO * CameraControl Follow \"SolarSystemView\" SoftTransition Yes");
            m_root.ExecuteCommand("VO * CameraControl 3DWindowProps FollowMode On");

        }

        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            //Controls what a command does
            if (string.Compare(CommandName, "AGI.PlanetsToggleCSharpPlugin.SetPlanetView", true) == 0)
            {
                if (planetsShown)
                {
                    setViewCameraPath();
                }
                else
                {
                    m_root.ExecuteCommand("VO * View Home");
                }
            }
            if (string.Compare(CommandName, "AGI.PlanetsToggleCSharpPlugin.PlanetsToggle", true) == 0 ) 
            {
                if (planetsShown)
                {
                    m_root.ExecuteCommand("Window3D * ViewVolume MaxVisibleDist 1e11");
                    foreach (IAgStkObject planetObj in m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.ePlanet))
                    {
                        planetObj.Unload();
                    }
                    planetsShown = false;
        
                }
                else
                {
                    m_root.ExecuteCommand("Graphics * GlobalAttributes ShowPlanetOrbits On ShowPlanetCbiPos On ShowPlanetCbiLabel On ShowPlanetGroundPos Off ShowPlanetGroundLabel Off");
                    
                    foreach (string planet in planets)
                    {
                        createPlanet(planet);
                    }
                    m_root.ExecuteCommand("Window3D * ViewVolume MaxVisibleDist 1e14");
                    //m_root.ExecuteCommand("VO * Celestial Stars ShowTx On File \"" + @"C:\Program Files (x86)\AGI\Imagery\AGI Celestial Imagery\mwpan2.ctm" + "\"");

                    planetsShown = true;
        
                }
            }
        }
        bool planetsShown;
        string[] planets = { "Mercury", "Venus", "Earth", "Mars", "Saturn", "Jupiter", "Uranus", "Neptune", "Pluto" };

        public void createPlanet(string planetName)
        {
            m_root.ExecuteCommand("New / Planet " + planetName);
            m_root.ExecuteCommand("Define */Planet/" + planetName + " CentralBody " + planetName + " Analytic");
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            //Enable commands
            if (string.Compare(CommandName, "AGI.PlanetsToggleCSharpPlugin.PlanetsToggle", true) == 0 || string.Compare(CommandName, "AGI.PlanetsToggleCSharpPlugin.SetPlanetView", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        #endregion



        internal AgStkObjectRootClass STKRoot
        {
            get { return m_root; }
        }


    }
}
