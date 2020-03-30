using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AGI.STKUtil;
using AGI.STKObjects;

namespace SensorBoresightViewPlugin
{
    class SensorViewClass
    {
        private AgStkObjectRoot m_root;
        private IAgSensor sensor;
        private string installDir;
        private string pluginInstallDir;
        private string strWinID;

        public SensorViewClass(AgStkObjectRoot root, IAgStkObject selectedSensor)
        {
            m_root = root;
            sensor = (IAgSensor)selectedSensor;
            IAgExecCmdResult result = m_root.ExecuteCommand("GetDirectory / STKHome");
            installDir = result[0];
            pluginInstallDir = installDir + @"Plugins\SensorBoresightView";
        }

        public void CreateSensorWindow(int intVertPixels)
        {
            int windowID = 9999;
            string command = "Window3D * CreateWindow Type Normal Title \"Sensor View\"";
            AgExecCmdResult execResult = (AgExecCmdResult)m_root.ExecuteCommand(command);

            
            execResult = (AgExecCmdResult)m_root.ExecuteCommand("VO_R * MapID");
            string strResult = execResult[0];
            
            string[] strWindows = strResult.Split(Environment.NewLine.ToCharArray());
            
            foreach (string strWindow in strWindows)
            {
                if (strWindow.Contains("Sensor View"))
                {                    
                    string[] winID = strWindow.Split('-');
                    
                    windowID = Convert.ToInt16(winID[0]);
                }
            }
            strWinID = windowID.ToString();

            m_root.ExecuteCommand("VO * Annotation Time ShowTimeStep Off WindowID " + strWinID);
            m_root.ExecuteCommand("VO * Annotation Time Show Off WindowID " + strWinID);            
            m_root.ExecuteCommand("VO * Annotation ViewerPos Show Off WindowID " + strWinID);
            m_root.ExecuteCommand("VO * Annotation Frame Show Off WindowID " + strWinID);

            string[] toolbars = { "3D Window Defaults", "3D Graphics", "3D Camera Control", "3D Object Editing", "3D Aircraft Mission Modeler Editing",
                                "Animation", "ArcGIS", "Globe Manager", "Microsoft Bingtm Maps"};

            foreach (var item in toolbars)
            {
                try
                {
                    m_root.ExecuteCommand("Window3D * Toolbar Hide \"" + item + "\" WindowID" + strWinID);
                }
                catch
                {
                }
                
            }
            

            SetWindowFOV(intVertPixels);
        }

        private void SetWindowFOV(int intVertPixels)
        {
            double dblVertHalfAngle = 0.0;
            double dblHorzHalfAngle = 0.0;

            IAgStkObject sensorObj = (IAgStkObject)sensor;
            IAgStkObject parent = sensorObj.Parent;
            string truncatedSensorPath = parent.ClassName + "/" + parent.InstanceName + "/" + sensorObj.ClassName + "/" + sensorObj.InstanceName;

            if (sensor.PatternType == AgESnPattern.eSnSimpleConic)
            {
                IAgSnSimpleConicPattern sensorPattern = (IAgSnSimpleConicPattern)sensor.Pattern;
                dblVertHalfAngle = (double)sensorPattern.ConeAngle;
                dblHorzHalfAngle = (double)sensorPattern.ConeAngle;
                //Turn Sensor Graphics ON in the view window to see the Sensor Footprint
                m_root.ExecuteCommand("VO * ObjectStateInWin Show On Object " + truncatedSensorPath + " WindowId " + strWinID);
            }
            else if (sensor.PatternType == AgESnPattern.eSnRectangular)
            {
                IAgSnRectangularPattern sensorPattern = (IAgSnRectangularPattern)sensor.Pattern;
                dblVertHalfAngle = (double)sensorPattern.VerticalHalfAngle;
                dblHorzHalfAngle = (double)sensorPattern.HorizontalHalfAngle;
                //Turn Sensor Graphics OFF in the view window to not see the outline of the Sensor Footprint
                m_root.ExecuteCommand("VO * ObjectStateInWin Show Off Object " + truncatedSensorPath + " WindowId " + strWinID);
            }

            string strUpVector;
            double winFOV;
            int intHorzPixels ;
            
            if (dblVertHalfAngle > dblHorzHalfAngle)
            {
                winFOV = 2.0 * dblVertHalfAngle;
                strUpVector = "X";
                intHorzPixels = (int)(dblVertHalfAngle / dblHorzHalfAngle * intVertPixels);
            }
            else
            {
                winFOV = 2.0 * dblHorzHalfAngle;
                strUpVector = "-Y";
                intHorzPixels = (int)(dblHorzHalfAngle / dblVertHalfAngle * intVertPixels);
            }
            string strWinFOV = winFOV.ToString();


            m_root.ExecuteCommand("VO * ViewFromTo Parameters UpVector " + strUpVector + " UseUpVector On Distance From 0.0 WindowID " + strWinID);
            m_root.ExecuteCommand("VO * ViewFromTo Normal From \"" + truncatedSensorPath + " Body Axes\" To ZOutward WindowID " + strWinID);
                
            m_root.ExecuteCommand("Window3D * Float " + strWinID);
            m_root.ExecuteCommand("Window3D * Raise " + strWinID);

            m_root.ExecuteCommand("Window3D * InnerSize " + intHorzPixels + " " + intVertPixels + " " + strWinID);
            m_root.ExecuteCommand("Window3D * ViewVolume FieldOfView " + strWinFOV + " WindowID " + strWinID);
            m_root.ExecuteCommand("VO * ObjectStateInWin Show Off Object " + parent.ClassName + "/" + parent.InstanceName + " WindowId " + strWinID);
               

        }

        public void EnableRulers()
        {
            string overlaypath = pluginInstallDir + "\\Images" ;
            // "Rulers" refers to 2 separate overlays:
            //   - Ruler.ppm should be docked to the Top Center of the screen, Scale 0.75 for 400px window, Y = 15px down from top
            //   - Ruler2.ppm should be docked to the Right of the screen, centered vertically, Same scale, X = 20px over from right
            // Defaults to ON.  If turned off, this should turn the display of these 2 overlays

            m_root.ExecuteCommand("VO * Overlay Add " + "\"" + overlaypath + "\\Ruler.ppm" + "\"" + 
                " XOrigin Center XPosition 0 YOrigin Top YPosition 20 Scale 0.7" + " WindowId " + strWinID);
            m_root.ExecuteCommand("VO * Overlay Add " + "\"" + overlaypath + "\\Ruler2.ppm" + "\"" + 
                " XOrigin Right YOrigin Center Scale 0.7" + " WindowId " + strWinID);
        }

        public void EnableCompass()
        {
            m_root.ExecuteCommand("VO * Compass Show On XOrigin Right XPosition 25 YPosition 25 Radius 25" + " WindowId " + strWinID);            
        }

        public void EnableLLA()
        {
            m_root.ExecuteCommand("VO * Annotation ViewerPos Show On XPos 5 YPos 10 Color White WindowId " + strWinID);
            m_root.ExecuteCommand("Vo * Annotation Time Show On XPos 5.0 YPos 25.0 Color White WindowId " + strWinID);            
        }

        public void EnableCrosshairs(CrosshairType type)
        {
            string overlaypath = pluginInstallDir + "\\Images";
            switch (type)
	            {
		            case CrosshairType.Grid:
                        m_root.ExecuteCommand("VO * Overlay Add " + "\"" + overlaypath +
                            "\\crosshairs-grid.ppm" + "\"" + " XOrigin Center YOrigin Center Scale 0.5" + " WindowId " + strWinID);                   
                        break;
                    case CrosshairType.Square:
                        m_root.ExecuteCommand("VO * Overlay Add " + "\"" + overlaypath +
                            "\\crosshairs-square.ppm" + "\"" + " XOrigin Center YOrigin Center Scale 0.5" + " WindowId " + strWinID);                    
                        break;
                    case CrosshairType.Circular:
                        m_root.ExecuteCommand("VO * Overlay Add " + "\"" + overlaypath +
                            "\\crosshairs-circular.ppm" + "\"" + " XOrigin Center YOrigin Center Scale 0.5" + " WindowId " + strWinID);                     
                        break;
                    default:
                        break;
	            }
            
        }

        public enum CrosshairType
        {
            Grid,
            Square,
            Circular            
        }

    }
}
