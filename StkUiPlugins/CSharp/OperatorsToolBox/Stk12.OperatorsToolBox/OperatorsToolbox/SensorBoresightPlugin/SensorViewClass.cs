﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AGI.STKUtil;
using AGI.STKObjects;
using System.Windows.Forms;
using OperatorsToolbox.SmartView;

namespace OperatorsToolbox.SensorBoresightPlugin
{
    public class SensorViewClass
    {
        private readonly AgStkObjectRoot _root;
        private readonly IAgSensor _sensor;
        private readonly string _pluginInstallDir;
        private string _windowId;

        public SensorViewClass(AgStkObjectRoot root, IAgStkObject selectedSensor)
        {
            _root = root;
            _sensor = (IAgSensor)selectedSensor;

            _pluginInstallDir = Path.Combine(@CommonData.InstallDir, "Plugin Files\\SensorBoresightPlugin");
        }

        public void CreateSensorWindow(int intVertPixels, string upVector = "")
        {
            var windowId = 9999;
            Tuple<bool, int> exists = SmartViewFunctions.DoesWindowExist(((IAgStkObject)_sensor).InstanceName + "-Sensor View", 1);
            if (exists.Item1)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand("Window3d * Remove WindowID " + exists.Item2.ToString());
                }
                catch (Exception)
                {

                }
            }
            _root.ExecuteCommand("Window3D * CreateWindow Type Normal Title \""+((IAgStkObject)_sensor).InstanceName+"-Sensor View\"");
            windowId = SmartView.SmartViewFunctions.GetWindowId(((IAgStkObject)_sensor).InstanceName + "-Sensor View ", 1);
            
            //var execResult = (AgExecCmdResult)_root.ExecuteCommand("VO_R * MapID");
            //var strResult = execResult[0];
            
            //var strWindows = strResult.Split(Environment.NewLine.ToCharArray());
            
            //foreach (var strWindow in strWindows)
            //{
            //    if (!strWindow.Contains("Sensor View"))
            //    {
            //        continue;
            //    }
            //    var winId = strWindow.Split('-');
                    
            //    windowId = Convert.ToInt16(winId[0]);
            //}
            _windowId = windowId.ToString();

            _root.ExecuteCommand("VO * Annotation Time ShowTimeStep Off WindowID " + _windowId);
            _root.ExecuteCommand("VO * Annotation Time Show Off WindowID " + _windowId);            
            _root.ExecuteCommand("VO * Annotation ViewerPos Show Off WindowID " + _windowId);
            _root.ExecuteCommand("VO * Annotation Frame Show Off WindowID " + _windowId);

            string[] toolbars = { "3D Window Defaults", "3D Graphics", "3D Camera Control", "3D Object Editing", "3D Aviator Editing",
                                "Animation", "ArcGIS", "Globe Manager", "Microsoft Bing Maps"};

            foreach (var item in toolbars)
            {
                try
                {
                    _root.ExecuteCommand($"Window3D * Toolbar Hide \"{item}\" WindowID {_windowId}");
                }
                catch
                {
                }
            }
            SetWindowFOV(intVertPixels, upVector);
        }

        private void SetWindowFOV(int intVertPixels, string upVector = "")
        {
            var dblVertHalfAngle = 0.0;
            var dblHorzHalfAngle = 0.0;

            var sensorObj = (IAgStkObject)_sensor;
            var parent = sensorObj.Parent;
            var truncatedSensorPath = $"{parent.ClassName}/{parent.InstanceName}/{sensorObj.ClassName}/{sensorObj.InstanceName}";

            switch (_sensor.PatternType)
            {
                case AgESnPattern.eSnSimpleConic:
                {
                    var sensorPattern = (IAgSnSimpleConicPattern)_sensor.Pattern;
                    dblVertHalfAngle = (double)sensorPattern.ConeAngle;
                    dblHorzHalfAngle = (double)sensorPattern.ConeAngle;
                    //Turn Sensor Graphics ON in the view window to see the Sensor Footprint
                    _root.ExecuteCommand("VO * ObjectStateInWin Show On Object " + truncatedSensorPath + " WindowId " + _windowId);
                    break;
                }
                case AgESnPattern.eSnRectangular:
                {
                    var sensorPattern = (IAgSnRectangularPattern)_sensor.Pattern;
                    dblVertHalfAngle = (double)sensorPattern.VerticalHalfAngle;
                    dblHorzHalfAngle = (double)sensorPattern.HorizontalHalfAngle;
                    //Turn Sensor Graphics OFF in the view window to not see the outline of the Sensor Footprint
                    _root.ExecuteCommand("VO * ObjectStateInWin Show Off Object " + truncatedSensorPath + " WindowId " + _windowId);
                    break;
                }
                case AgESnPattern.eSnSAR:
                {
                    break;
                }
                case AgESnPattern.eSnEOIR:
                {
                    var sensorPattern = (IAgSnEOIRPattern)_sensor.Pattern;
                    dblVertHalfAngle = (double)sensorPattern.Bands[0].VerticalHalfAngle;
                    dblHorzHalfAngle = (double)sensorPattern.Bands[0].HorizontalHalfAngle;
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            string strUpVector;
            double winFOV;
            int intHorzPixels ;

            if (dblVertHalfAngle >= dblHorzHalfAngle)
            {
                winFOV = 2.0 * dblVertHalfAngle;
                if (_sensor.PatternType == AgESnPattern.eSnEOIR)
                {
                    strUpVector = "-Y";
                    intHorzPixels = (int)(dblVertHalfAngle / dblHorzHalfAngle * intVertPixels);
                }
                else
                {
                    strUpVector = "X";
                    intHorzPixels = (int)(dblVertHalfAngle / dblHorzHalfAngle * intVertPixels);
                }
            }
            else
            {
                winFOV = 2.0 * dblHorzHalfAngle;
                if (_sensor.PatternType == AgESnPattern.eSnEOIR)
                {
                    strUpVector = "X";
                    intHorzPixels = (int)(dblHorzHalfAngle / dblVertHalfAngle * intVertPixels);
                }
                else
                {
                    strUpVector = "-Y";
                    intHorzPixels = (int)(dblHorzHalfAngle / dblVertHalfAngle * intVertPixels);
                }
            }
            if (!String.IsNullOrEmpty(upVector))
            {
                strUpVector = upVector;
            }

            var strWinFOV = winFOV.ToString();
            _root.ExecuteCommand("VO * ViewFromTo Normal From \"" + truncatedSensorPath + " Body Axes\" To ZOutward WindowID " + _windowId);
            _root.ExecuteCommand("VO * ViewFromTo Parameters UpVector " + strUpVector + " UseUpVector On Distance From 0.0 WindowID " + _windowId);

            _root.ExecuteCommand("Window3D * Float " + _windowId);
            _root.ExecuteCommand("Window3D * Raise " + _windowId);

            _root.ExecuteCommand("Window3D * InnerSize " + intHorzPixels + " " + intVertPixels + " " + _windowId);

            if (!(winFOV < 0.1) && !(winFOV > 160))
            {
                _root.ExecuteCommand("Window3D * ViewVolume FieldOfView " + strWinFOV + " WindowID " + _windowId);
            }
            else
            {
                MessageBox.Show("FOV outside limits. Limits between 0.1 deg and 160 deg");
            }
            //_root.ExecuteCommand("VO * ObjectStateInWin Show Off Object " + parent.ClassName + "/" + parent.InstanceName + " WindowId " + _windowId);
               

        }

        public void EnableRulers()
        {
            // "Rulers" refers to 2 separate overlays:
            //   - Ruler.ppm should be docked to the Top Center of the screen, Scale 0.75 for 400px window, Y = 15px down from top
            //   - Ruler2.ppm should be docked to the Right of the screen, centered vertically, Same scale, X = 20px over from right
            // Defaults to ON.  If turned off, this should turn the display of these 2 overlays

            _root.ExecuteCommand("VO * Overlay Add " + "\"" + _pluginInstallDir + "\\Ruler.ppm" + "\"" + 
                " XOrigin Center XPosition 0 YOrigin Top YPosition 20 Scale 0.7" + " WindowId " + _windowId);
            _root.ExecuteCommand("VO * Overlay Add " + "\"" + _pluginInstallDir + "\\Ruler2.ppm" + "\"" + 
                " XOrigin Right YOrigin Center Scale 0.7" + " WindowId " + _windowId);
        }

        public void EnableCompass()
        {
            _root.ExecuteCommand("VO * Compass Show On XOrigin Right XPosition 25 YPosition 25 Radius 25" + " WindowId " + _windowId);            
        }

        public void EnableLLA()
        {
            _root.ExecuteCommand("VO * Annotation ViewerPos Show On XPos 5 YPos 10 Color White WindowId " + _windowId);
            _root.ExecuteCommand("Vo * Annotation Time Show On XPos 5.0 YPos 25.0 Color White WindowId " + _windowId);            
        }

        public void EnableCrosshairs(CrosshairType type)
        {
            switch (type)
	            {
		            case CrosshairType.Grid:
                        _root.ExecuteCommand("VO * Overlay Add " + "\"" + _pluginInstallDir +
                            "\\crosshairs-grid.ppm" + "\"" + " XOrigin Center YOrigin Center Scale 0.5" + " WindowId " + _windowId);                   
                        break;
                    case CrosshairType.Square:
                        _root.ExecuteCommand("VO * Overlay Add " + "\"" + _pluginInstallDir +
                            "\\crosshairs-square.ppm" + "\"" + " XOrigin Center YOrigin Center Scale 0.5" + " WindowId " + _windowId);                    
                        break;
                    case CrosshairType.Circular:
                        _root.ExecuteCommand("VO * Overlay Add " + "\"" + _pluginInstallDir +
                            "\\crosshairs-circular.ppm" + "\"" + " XOrigin Center YOrigin Center Scale 0.5" + " WindowId " + _windowId);                     
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
