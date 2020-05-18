using System;
using System.Collections.Generic;
using System.Text;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using AGI.STKUtil;
using System.Threading;

using System.Configuration;

using System.Windows.Forms;

namespace Agi.Ui.GreatArc.Stk12
{
    class GreatArcUpdate
    {
        private AgStkObjectRoot root;
        private string installDir;

        public GreatArcUpdate(AgStkObjectRoot m_root)
        {
            root = m_root;
            AGI.STKUtil.IAgExecCmdResult result = m_root.ExecuteCommand("GetDirectory / STKHome");
            installDir = result[0];

        }

        public enum WaypointProperty
        {
            Speed,
            Altitude,
            TurnRadius,
            Latitude,
            Longitude
        }

        public void UpdateWaypoint(IAgStkObject greatArcVehicle, 
            WaypointProperty waypointProp, double waypointParameter, string parameterUnit)
        {
            IAgVePropagatorGreatArc route = null;

            switch (parameterUnit)
            {
                case "mph":
                    root.UnitPreferences.SetCurrentUnit("Distance", "mi");
                    root.UnitPreferences.SetCurrentUnit("Time", "hr");
                    break;
                case "km/sec":
                    root.UnitPreferences.SetCurrentUnit("Distance", "km");
                    root.UnitPreferences.SetCurrentUnit("Time", "sec");
                    break;
                case "knots":
                    root.UnitPreferences.SetCurrentUnit("Distance", "nm");
                    root.UnitPreferences.SetCurrentUnit("Time", "hr");
                    break;
                case "m":
                    root.UnitPreferences.SetCurrentUnit("Distance", "m");                    
                    break;
                case "km":
                    root.UnitPreferences.SetCurrentUnit("Distance", "km");                    
                    break;
                case "ft":
                    root.UnitPreferences.SetCurrentUnit("Distance", "ft");                    
                    break;
                case "deg":
                    root.UnitPreferences.SetCurrentUnit("Angle", "deg");
                    break;
                case "rad":
                    root.UnitPreferences.SetCurrentUnit("Angle", "rad");
                    break;                
                default:
                    break;
            }


            switch (greatArcVehicle.ClassType)
            {
                case AgESTKObjectType.eAircraft:
                    if (((IAgAircraft)greatArcVehicle).RouteType == AgEVePropagatorType.ePropagatorGreatArc)
                    {
                        route = ((IAgAircraft)greatArcVehicle).Route as IAgVePropagatorGreatArc;
                    }
                    break;
                case AgESTKObjectType.eGroundVehicle:
                    if (((IAgGroundVehicle)greatArcVehicle).RouteType == AgEVePropagatorType.ePropagatorGreatArc)
                    {
                        route = ((IAgGroundVehicle)greatArcVehicle).Route as IAgVePropagatorGreatArc;
                    }
                    
                    break;
                case AgESTKObjectType.eShip:
                    if (((IAgShip)greatArcVehicle).RouteType == AgEVePropagatorType.ePropagatorGreatArc)
                    {
                        route = ((IAgShip)greatArcVehicle).Route as IAgVePropagatorGreatArc;
                    }
                    
                    break;
                    
                default:
                    break;
            }

            if (route != null)
            {
                foreach (IAgVeWaypointsElement waypoint in route.Waypoints)
                {
                    switch (waypointProp)
                    {
                        case WaypointProperty.Speed:
                            waypoint.Speed = waypointParameter;
                            break;
                        case WaypointProperty.Altitude:
                            waypoint.Altitude = waypointParameter;
                            break;
                        case WaypointProperty.TurnRadius:
                            waypoint.TurnRadius = waypointParameter;
                            break;
                        case WaypointProperty.Latitude:
                            waypoint.Latitude = (double)waypoint.Latitude + waypointParameter;
                            break;
                        case WaypointProperty.Longitude:
                            waypoint.Longitude= (double)waypoint.Longitude+ waypointParameter;
                            break;
                        default:
                            break;
                    }
                }
                route.Propagate();
                root.UnitPreferences.ResetUnits();
            }

        }      


    }
}
