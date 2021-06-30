using AGI.STKObjects;
using AGI.STKGraphics;
using AGI.STKVgt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Stk12.UiPlugin.ModelPixelUpdater
{
    internal class ModelUpdateClass
    {
        internal IAgStkObjectRoot m_root;
        internal AgStkGraphicsSceneManager m_scene;
        internal Dictionary<string, double> magDiff;
        public double scaleFactor;
        private ArrayList objectTypeArray = new ArrayList();

        public ModelUpdateClass(IAgStkObjectRoot root)
        {
            m_root = root;
            m_root.UnitPreferences["DateFormat"].SetCurrentUnit("EpSec");
            IAgScenario scenario = (IAgScenario)m_root.CurrentScenario;
            m_scene = scenario.SceneManager as AgStkGraphicsSceneManager;

            objectTypeArray.Add(AgESTKObjectType.eAircraft);
            objectTypeArray.Add(AgESTKObjectType.eFacility);
            objectTypeArray.Add(AgESTKObjectType.eGroundVehicle);
            objectTypeArray.Add(AgESTKObjectType.eLaunchVehicle);
            objectTypeArray.Add(AgESTKObjectType.eMissile);
            objectTypeArray.Add(AgESTKObjectType.eSatellite);
            objectTypeArray.Add(AgESTKObjectType.eShip);

            for (int i = 0; i < m_scene.Scenes.Count; i++)
            {
                ((AgStkGraphicsScene)m_scene.Scenes[i]).Rendering += new IAgStkGraphicsSceneEvents_RenderingEventHandler(ModelUpdateClass_Rendering);
            }
            foreach (AgESTKObjectType objectType in objectTypeArray)
            {
                foreach (IAgStkObject objectInstance in m_root.CurrentScenario.Children.GetElements(objectType))
                {
                    if (objectType == AgESTKObjectType.eAircraft)
                    {
                        ((IAgAircraft)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = false;
                    }
                    else if (objectType == AgESTKObjectType.eFacility)
                    {
                        ((IAgFacility)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = false;
                    }
                    else if (objectType == AgESTKObjectType.eGroundVehicle)
                    {
                        ((IAgGroundVehicle)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = false;
                    }
                    else if (objectType == AgESTKObjectType.eLaunchVehicle)
                    {
                        ((IAgLaunchVehicle)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = false;
                    }
                    else if (objectType == AgESTKObjectType.eMissile)
                    {
                        ((IAgMissile)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = false;
                    }
                    else if (objectType == AgESTKObjectType.eSatellite)
                    {
                        ((IAgSatellite)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = false;
                    }
                    else if (objectType == AgESTKObjectType.eShip)
                    {
                        ((IAgShip)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = false;
                    }
                }
            }

            magDiff = new Dictionary<string, double>();
            scaleFactor = 1;
        }

        public void DisableModelUpdate()
        {
            for (int i = 0; i < m_scene.Scenes.Count; i++)
            {
                ((AgStkGraphicsScene)m_scene.Scenes[i]).Rendering -= new IAgStkGraphicsSceneEvents_RenderingEventHandler(ModelUpdateClass_Rendering);
            }

            foreach (AgESTKObjectType objectType in objectTypeArray)
            {
                foreach (IAgStkObject objectInstance in m_root.CurrentScenario.Children.GetElements(objectType))
                {
                    if (objectType == AgESTKObjectType.eAircraft)
                    {
                        ((IAgAircraft)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = true;
                        ((IAgAircraft)objectInstance).VO.Model.ScaleValue = 1;
                    }
                    else if (objectType == AgESTKObjectType.eFacility)
                    {
                        ((IAgFacility)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = true;
                        ((IAgFacility)objectInstance).VO.Model.ScaleValue = 1;
                    }
                    else if (objectType == AgESTKObjectType.eGroundVehicle)
                    {
                        ((IAgGroundVehicle)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = true;
                        ((IAgGroundVehicle)objectInstance).VO.Model.ScaleValue = 1;
                    }
                    else if (objectType == AgESTKObjectType.eLaunchVehicle)
                    {
                        ((IAgLaunchVehicle)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = true;
                        ((IAgLaunchVehicle)objectInstance).VO.Model.ScaleValue = 1;
                    }
                    else if (objectType == AgESTKObjectType.eMissile)
                    {
                        ((IAgMissile)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = true;
                        ((IAgMissile)objectInstance).VO.Model.ScaleValue = 1;
                    }
                    else if (objectType == AgESTKObjectType.eSatellite)
                    {
                        ((IAgSatellite)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = true;
                        ((IAgSatellite)objectInstance).VO.Model.ScaleValue = 1;
                    }
                    else if (objectType == AgESTKObjectType.eShip)
                    {
                        ((IAgShip)objectInstance).VO.Model.DetailThreshold.EnableDetailThreshold = true;
                        ((IAgShip)objectInstance).VO.Model.ScaleValue = 1;
                    }
                }
            }

        }


        public void updateModelSize(double epsec)
        {
            Array cameraPosition = m_scene.Scenes[0].Camera.Position;
            IAgCrdnSystem cameraReference = m_scene.Scenes[0].Camera.PositionReferenceFrame;

            ArrayList objectTypeArray = new ArrayList();
            objectTypeArray.Add(AgESTKObjectType.eAircraft);
            objectTypeArray.Add(AgESTKObjectType.eFacility);
            objectTypeArray.Add(AgESTKObjectType.eGroundVehicle);
            objectTypeArray.Add(AgESTKObjectType.eLaunchVehicle);
            objectTypeArray.Add(AgESTKObjectType.eMissile);
            objectTypeArray.Add(AgESTKObjectType.eSatellite);
            objectTypeArray.Add(AgESTKObjectType.eShip);

            foreach (AgESTKObjectType objectType in objectTypeArray)
            {
                foreach (IAgStkObject objectInstance in m_root.CurrentScenario.Children.GetElements(objectType))
                {
                    if (!magDiff.ContainsKey(objectInstance.Path))
                    {
                        magDiff.Add(objectInstance.Path, 1);
                    }
                    IAgCrdnPoint centerPoint = objectInstance.Vgt.Points["Center"];
                    IAgCrdnPointLocateInSystemResult pointResult = centerPoint.LocateInSystem(epsec, cameraReference);

                    double xDiff = (double)cameraPosition.GetValue(0) - pointResult.Position.X;
                    double yDiff = (double)cameraPosition.GetValue(1) - pointResult.Position.Y;
                    double zDiff = (double)cameraPosition.GetValue(2) - pointResult.Position.Z;

                    double thisMagDiff = Math.Sqrt(xDiff * xDiff + yDiff * yDiff + zDiff * zDiff);

                    if (thisMagDiff > 1.05 * magDiff[objectInstance.Path] || thisMagDiff < .95 * magDiff[objectInstance.Path])
                    {
                        magDiff[objectInstance.Path] = thisMagDiff;
                        if (Math.Log10(scaleFactor * thisMagDiff) > 0)
                        {
                            var temp = Math.Log10(scaleFactor * thisMagDiff);
                            if (objectType == AgESTKObjectType.eAircraft)
                            {
                                ((IAgAircraft)objectInstance).VO.Model.ScaleValue = temp;
                            }
                            else if (objectType == AgESTKObjectType.eFacility)
                            {
                                if ((temp - 0.5) < 0)
                                {
                                    temp = 1;
                                }
                                ((IAgFacility)objectInstance).VO.Model.ScaleValue = temp - 0.5;
                            }
                            else if (objectType == AgESTKObjectType.eGroundVehicle)
                            {
                                if ((temp + 0.5) > 10)
                                {
                                    temp = 10;
                                }
                                ((IAgGroundVehicle)objectInstance).VO.Model.ScaleValue = temp + 0.5;
                            }
                            else if (objectType == AgESTKObjectType.eLaunchVehicle)
                            {
                                ((IAgLaunchVehicle)objectInstance).VO.Model.ScaleValue = temp;
                            }
                            else if (objectType == AgESTKObjectType.eMissile)
                            {
                                if ((temp + 1.0) > 10)
                                {
                                    temp = 10;
                                }
                                ((IAgMissile)objectInstance).VO.Model.ScaleValue = temp + 1.0;
                            }
                            else if (objectType == AgESTKObjectType.eSatellite)
                            {
                                if ((temp + 1.0) > 10)
                                {
                                    temp = 10;
                                }
                                ((IAgSatellite)objectInstance).VO.Model.ScaleValue = temp + 1.0;
                            }
                            else if (objectType == AgESTKObjectType.eShip)
                            {
                                if ((temp - 0.5) < 0)
                                {
                                    temp = 1;
                                }
                                ((IAgShip)objectInstance).VO.Model.ScaleValue = temp - 0.5;
                            }
                        }
                    }
                }
            }
        }

        void ModelUpdateClass_Rendering(object sender, IAgStkGraphicsRenderingEventArgs args)
        {
            updateModelSize(args.TimeInEpSecs);
        }

    }
}
