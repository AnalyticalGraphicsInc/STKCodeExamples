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
using Agi.Ui.GreatArc.Stk12.Properties;

namespace Agi.Ui.GreatArc.Stk12
{
    [Guid("5e183340-3017-45b8-8a6c-5fb40390a1bd")]
    [ProgId("Agi.Ui.GreatArc.Stk12")]
    [ClassInterface(ClassInterfaceType.None)]
    public class GreatArc_12 : IAgUiPlugin, IAgUiPluginCommandTarget, IAgUiPlugin2
    {
        private IAgUiPluginSite m_pSite;
        private AgStkObjectRoot m_root;
        private IAgProgressTrackCancel m_progress;

        private bool m_integrate = true;

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
           
        }

        public void OnShutdown()
        {
            m_pSite = null;
        }

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            //Get the AgStkObjectRoot
            m_pSite = PluginSite;
            IAgUiApplication AgUiApp = m_pSite.Application;
            m_root = AgUiApp.Personality2 as AgStkObjectRoot;
            m_root.Isolate();
            m_root.UnitPreferences.ResetUnits();
        }

        #endregion
        #region IAgUiPlugin2 Members

        public void OnDisplayMenu(string MenuTitle, AgEUiPluginMenuBarKind MenuBarKind, 
            IAgUiPluginMenuBuilder2 MenuBuilder)
        {
            if (MenuTitle.Contains("Insert"))
            {
                
                //Add a Menu Item to the Insert menu
                stdole.IPictureDisp picture1;
                picture1 = (stdole.IPictureDisp)Microsoft.VisualBasic.Compatibility.VB6.Support.ImageToIPicture(Agi.Ui.GreatArc.Stk12.Properties.Resources.acRoute64);
                MenuBuilder.InsertMenuItem(4,
                    "RasterSearch.LaunchInterface",
                    "Aircraft from Search Pattern...",
                    "Define an Aircraft based on search criteria",
                    picture1);
            }

            if (MenuBarKind == AgEUiPluginMenuBarKind.eUiPluginMenuBarContextMenu)
            {
                //the STK Object Root
                AgStkObjectRoot root = (AgStkObjectRoot)m_pSite.Application.Personality2;
                //the current Selected Object
                IAgStkObject oSelectedObject = root.GetObjectFromPath(m_pSite.Selection[0].Path);

                //Only show menu items for the Scenario or Satellite object
                if (oSelectedObject.ClassName.Equals("Aircraft") ||
                    oSelectedObject.ClassName.Equals("GroundVehicle") ||
                    oSelectedObject.ClassName.Equals("Ship"))
                {
                    MenuBuilder.InsertMenuItem(3, "UpdateVelocity", "Update Velocity", "Update Velocity", null);
                    MenuBuilder.InsertMenuItem(4, "UpdateAltitude", "Update Altitude", "Update Altitude", null);
                    MenuBuilder.InsertMenuItem(5, "UpdateTurnRadius", "Update Turn Radius", "Update Turn Radius", null);
                    MenuBuilder.InsertMenuItem(6, "EnterEditMode", "Enter 3D Edit Mode", "Enter 3D Edit Mode", null);
                    MenuBuilder.InsertSeparator(6);
                    MenuBuilder.InsertSeparator(3);
                }
                else if (oSelectedObject.ClassName.Equals("Place") ||
                    oSelectedObject.ClassName.Equals("Facility") ||
                    oSelectedObject.ClassName.Equals("Target") ||
                    oSelectedObject.ClassName.Equals("AreaTarget") ||
                    oSelectedObject.ClassName.Equals("LineTarget") )
                {
                    MenuBuilder.InsertMenuItem(3, "EnterEditMode", "Enter 3D Edit Mode", "Enter 3D Edit Mode", null);
                    MenuBuilder.InsertSeparator(3);
                }
            }

            if (MenuTitle.Contains("Ship") || 
                MenuTitle.Contains("GroundVehicle") || 
                MenuTitle.Contains("Aircraft"))
            {
                MenuBuilder.InsertMenuItem(0, "UpdateVelocity", "Update Velocity", "Update Velocity", null);
                MenuBuilder.InsertMenuItem(1, "UpdateAltitude", "Update Altitude", "Update Altitude", null);
                MenuBuilder.InsertMenuItem(2, "UpdateTurnRadius", "Update Turn Radius", "Update Turn Radius", null);
                MenuBuilder.InsertMenuItem(3, "ShiftLatitude", "Shift in Latitude", "Shift in Latitude", null);
                MenuBuilder.InsertMenuItem(4, "ShiftLongitude", "Shift in Longitude", "Shift in Longitude", null);
                MenuBuilder.InsertSeparator(5);
            }
        }

        #endregion

        #region IAgUiPluginCommandTarget Members

        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, 
            IAgUiPluginCommandParameters Parameters)
        {
                        
            if (string.Compare(CommandName, "EnterEditMode", true) == 0)
            {
                IAgStkObject oSelectedObject = m_root.GetObjectFromPath(m_pSite.Selection[0].Path);

                string command = "Window3d * InpDevMode Mode Edit Path ";
                string objPath = oSelectedObject.ClassName + "/" + oSelectedObject.InstanceName;
                m_root.ExecuteCommand(command + objPath);
                //To apply editing for current object, and turn off edit mode:
                //Window3d * InpDevMode Mode EditOK

            }
            
            if (string.Compare(CommandName, "RasterSearch.LaunchInterface", true) == 0)
            {
                m_progress = TrackCancel;
                OpenRasterUserInterface();
            }

            if (string.Compare(CommandName, "UpdateVelocity", true) == 0)
            {
                GreatArcUpdate updater = new GreatArcUpdate(m_root);
                GreatArcUpdateUI changeVelocity = new GreatArcUpdateUI("Speed");
                changeVelocity.ShowDialog();
                IAgStkObject oSelectedObject = m_root.GetObjectFromPath(m_pSite.Selection[0].Path);
                updater.UpdateWaypoint(oSelectedObject, GreatArcUpdate.WaypointProperty.Speed, 
                    changeVelocity.WaypointParameter, changeVelocity.ParameterUnits);
            }

            if (string.Compare(CommandName, "UpdateAltitude", true) == 0)
            {
                GreatArcUpdate updater = new GreatArcUpdate(m_root);
                GreatArcUpdateUI changeAltitude = new GreatArcUpdateUI("Altitude");
                changeAltitude.ShowDialog();
                IAgStkObject oSelectedObject = m_root.GetObjectFromPath(m_pSite.Selection[0].Path);
                updater.UpdateWaypoint(oSelectedObject,GreatArcUpdate.WaypointProperty.Altitude, 
                    changeAltitude.WaypointParameter, changeAltitude.ParameterUnits);
            }

            if (string.Compare(CommandName, "UpdateTurnRadius", true) == 0)
            {
                GreatArcUpdate updater = new GreatArcUpdate(m_root);
                GreatArcUpdateUI changeTurnRadius = new GreatArcUpdateUI("TurnRadius");
                changeTurnRadius.ShowDialog();
                IAgStkObject oSelectedObject = m_root.GetObjectFromPath(m_pSite.Selection[0].Path);
                updater.UpdateWaypoint(oSelectedObject,GreatArcUpdate.WaypointProperty.TurnRadius, 
                    changeTurnRadius.WaypointParameter, changeTurnRadius.ParameterUnits);
            }

            if (string.Compare(CommandName, "ShiftLatitude", true) == 0)
            {
                GreatArcUpdate updater = new GreatArcUpdate(m_root);
                GreatArcUpdateUI shiftLat = new GreatArcUpdateUI("Latitude");
                shiftLat.ShowDialog();
                IAgStkObject oSelectedObject = m_root.GetObjectFromPath(m_pSite.Selection[0].Path);
                updater.UpdateWaypoint(oSelectedObject, GreatArcUpdate.WaypointProperty.Latitude,
                    shiftLat.WaypointParameter, shiftLat.ParameterUnits);
            }
            
            if (string.Compare(CommandName, "ShiftLongitude", true) == 0)
            {
                GreatArcUpdate updater = new GreatArcUpdate(m_root);
                GreatArcUpdateUI shiftLon = new GreatArcUpdateUI("Longitude");
                shiftLon.ShowDialog();
                IAgStkObject oSelectedObject = m_root.GetObjectFromPath(m_pSite.Selection[0].Path);
                updater.UpdateWaypoint(oSelectedObject, GreatArcUpdate.WaypointProperty.Longitude,
                    shiftLon.WaypointParameter, shiftLon.ParameterUnits);
            }

        }



        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            //Enable commands
            if (string.Compare(CommandName, "GvFromDirections.LaunchInterface", true) == 0
                    || string.Compare(CommandName, "RasterSearch.LaunchInterface", true) == 0
                    || string.Compare(CommandName, "UpdateVelocity", true) == 0
                    || string.Compare(CommandName, "UpdateAltitude", true) == 0
                    || string.Compare(CommandName, "UpdateTurnRadius", true) == 0
                    || string.Compare(CommandName, "ShiftLatitude", true) == 0
                    || string.Compare(CommandName, "ShiftLongitude", true) == 0
                    || string.Compare(CommandName, "EnterEditMode", true) == 0
                )
            {
                if (m_root.CurrentScenario != null)
                {
                    return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
                }
                else
                {
                    return AgEUiPluginCommandState.eUiPluginCommandStateGrayed;
                }
            }

            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        #endregion

        
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

       
        private void OpenRasterUserInterface()
        {
            //Open a User Interface
            IAgUiPluginWindowSite windows = m_pSite as IAgUiPluginWindowSite;
            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
            }
            else
            {
                IntPtr hMainWnd = (IntPtr)Site.MainWindow;
                RasterUI acFromRaster = new RasterUI(STKRoot);
                NativeWindow nativeWindow = new NativeWindow();
                nativeWindow.AssignHandle(hMainWnd);
                acFromRaster.Show(nativeWindow);
            }
        }
    }
}
