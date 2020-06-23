using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Threading;
using AGI.STKUtil;
using AGI.Ui.Core;
using AGI.STKVgt;

namespace OrbitTunerUiPlugin
{
     static class STKHelper
    {
        public static AgStkObjectRoot StkRoot;

        public static bool UpdateCartesianOrbit(IAgSatellite sat, double x, double y, double z, double dx, double dy, double dz, ref string error)
            { 
                
                IAgVePropagatorJ2Perturbation prop = sat.Propagator as IAgVePropagatorJ2Perturbation;
                prop.InitialState.Representation.AssignCartesian(AgECoordinateSystem.eCoordinateSystemICRF, x, y, z, dx, dy, dz);
                return Is_Error_Propagator(prop, ref error);
            }

        public static bool UpdateClassicalOrbit(IAgSatellite sat, double a, double e, double i, double aop, double raan, double ta, ref string error)
        {

            IAgVePropagatorJ2Perturbation prop = sat.Propagator as IAgVePropagatorJ2Perturbation;
            // need to set everything individually because true vs mean anomaly
            IAgOrbitStateClassical keplerian = prop.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical) as IAgOrbitStateClassical;

            keplerian.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
            IAgClassicalSizeShapeSemimajorAxis sizeShape = keplerian.SizeShape as IAgClassicalSizeShapeSemimajorAxis;
            sizeShape.SemiMajorAxis = a;
            sizeShape.Eccentricity = e;

            keplerian.Orientation.Inclination = i;
            keplerian.Orientation.ArgOfPerigee = aop;
            (keplerian.Orientation.AscNode as IAgOrientationAscNodeRAAN).Value = raan;

            keplerian.LocationType = AgEClassicalLocation.eLocationTrueAnomaly;
            (keplerian.Location as IAgClassicalLocationTrueAnomaly).Value = ta;

            prop.InitialState.Representation.Assign(keplerian);

            return Is_Error_Propagator(prop, ref error);
        }

        public static bool UpdateEquinocitalOrbit(IAgSatellite sat, double aE, double hE, double kE, double pE, double qE, double mE, ref string error, string equinBox)
        {
            IAgVePropagatorJ2Perturbation prop = sat.Propagator as IAgVePropagatorJ2Perturbation;

            try
            {
                if (equinBox == "Posigrade")
                {
                    prop.InitialState.Representation.AssignEquinoctialPosigrade(AgECoordinateSystem.eCoordinateSystemICRF, aE, hE, kE, pE, qE, mE);
                }
                else
                {
                    prop.InitialState.Representation.AssignEquinoctialRetrograde(AgECoordinateSystem.eCoordinateSystemICRF, aE, hE, kE, pE, qE, mE);
                }
            }
            catch
            {
                
            }

            return Is_Error_Propagator(prop, ref error);
        }

        public static bool UpdateDelaunayOrbit(IAgSatellite sat, double lD, double gD, double hD, double LD, double GD, double HD, ref string error)
        {
            IAgVePropagatorJ2Perturbation prop = sat.Propagator as IAgVePropagatorJ2Perturbation;

            // there is no AssignDelaunay so we need to do this one at a time
            IAgOrbitStateDelaunay delaunay = prop.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateDelaunay) as IAgOrbitStateDelaunay;

            

            delaunay.MeanAnomaly = lD;
            delaunay.ArgOfPeriapsis = gD;
            delaunay.RAAN = hD;

            delaunay.LType = AgEDelaunayLType.eL;
            IAgDelaunayL delaunayL = delaunay.L as IAgDelaunayL;
            delaunayL.L = LD;

            delaunay.GType = AgEDelaunayGType.eG;
            IAgDelaunayG delaunayG = delaunay.G as IAgDelaunayG;
            delaunayG.G = GD;

            delaunay.HType = AgEDelaunayHType.eH;
            IAgDelaunayH delaunayH = delaunay.H as IAgDelaunayH;
            delaunayH.H = HD;

            prop.InitialState.Representation.Assign(delaunay);

            return Is_Error_Propagator(prop, ref error);
        }

        public static bool UpdateMixedSphericalOrbit(IAgSatellite sat, double lonM, double latM, double altM, double fpaM, double azM, double velM, ref string error)
        {
            IAgVePropagatorJ2Perturbation prop = sat.Propagator as IAgVePropagatorJ2Perturbation;

            prop.InitialState.Representation.AssignMixedSpherical(AgECoordinateSystem.eCoordinateSystemICRF, latM, lonM, altM, fpaM, azM, velM);

            return Is_Error_Propagator(prop, ref error);
        }

        public static bool UpdateSphericalOrbit(IAgSatellite sat, double raS, double decS, double radS, double fpaS, double azS, double velS, ref string error)
        {
            IAgVePropagatorJ2Perturbation prop = sat.Propagator as IAgVePropagatorJ2Perturbation;

            prop.InitialState.Representation.AssignSpherical(AgECoordinateSystem.eCoordinateSystemICRF, raS, decS, radS, fpaS, azS, velS);


            return Is_Error_Propagator(prop, ref error);

        }
        
        public static bool Is_Error_Propagator(IAgVePropagatorJ2Perturbation prop, ref string type)
        {
            // returns true if there is an error with propagator and "type" what was issue
            // returns false if there isn't an error and will propagate
            if (IsCircular(prop) && IsApogeeAboveGround(prop) && IsPerigeePositive(prop))
            {
                prop.Propagate();
                return false;
            }

            else if (!IsCircular(prop))
            {
                type = "Parabolic or hyperbolic orbit";
            }

            else if (!IsApogeeAboveGround(prop))
            {
                type = "Apogee too low";
            }
            else
            {
                type = "Perigee too low";
            }
            return true;
        }

        // check if the resulting orbit is circular or parabolic/hyperbolic
        private static bool IsCircular(IAgVePropagatorJ2Perturbation prop)
        {
            IAgOrbitStateClassical testOrbit = prop.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical) as IAgOrbitStateClassical;
            testOrbit.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
            IAgClassicalSizeShapeSemimajorAxis testSizeShape = testOrbit.SizeShape as IAgClassicalSizeShapeSemimajorAxis;

            if (testSizeShape.Eccentricity < 1.0)
            {
                return true;
            }

            return false;
        }
        
        // check if apogee is above the Earth
        private static bool IsApogeeAboveGround(IAgVePropagatorJ2Perturbation prop)
        {
            IAgOrbitStateClassical testOrbit = prop.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical) as IAgOrbitStateClassical;
            testOrbit.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
            IAgClassicalSizeShapeSemimajorAxis testSizeShape = testOrbit.SizeShape as IAgClassicalSizeShapeSemimajorAxis;

            // apogee radius = a(1+e)
            double rApo = testSizeShape.SemiMajorAxis * (1 + testSizeShape.Eccentricity);


            if (rApo > 6380.0)
            {
                return true;
            }

            return false;
        }

        // check if perigee radius is positive
        private static bool IsPerigeePositive(IAgVePropagatorJ2Perturbation prop)
        {
            IAgOrbitStateClassical testOrbit = prop.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical) as IAgOrbitStateClassical;
            testOrbit.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
            IAgClassicalSizeShapeSemimajorAxis testSizeShape = testOrbit.SizeShape as IAgClassicalSizeShapeSemimajorAxis;

            // perigee radius = a(1-e)
            double rPeri = testSizeShape.SemiMajorAxis * (1 - testSizeShape.Eccentricity);

            if (rPeri > 3000.0)
            {
                return true;
            }

            return false;
        }

        public static void SetMaxViewing(IAgSatellite sat)
        {
            sat.VO.Model.DetailThreshold.All = 1e12;
        }


        public static void ShowPeriapsisApoapsis(AgSatellite sat)
        {
            
                    ShowVector(sat, "Periapsis Vector");
                    ShowVector(sat, "Apoapsis Vector");
               
            
        }

        public static void HidePeriapsisApoapsis(AgSatellite sat)
        {
            
                    HideVector(sat, "Periapsis");
                    HideVector(sat, "Apoapsis");
                
            
        }

        public static void ShowEcc(AgSatellite satObj)
        {

            
               
                    ShowVector(satObj, "Ecc Vector");
                 
        }

        public static void HideEcc(AgSatellite satObj)
        {
            
                    HideVector(satObj, "Ecc");
                   
        }

        private static void TryShowVGT(string name, string onOff, bool translucentOn, string color)
        {
            try
            {
                ShowVGT(name,  "Add", onOff, translucentOn, color);
            }
            catch
            {
                ShowVGT(name, "Modify", onOff, translucentOn, color);
            }
        }

        private static void ShowVGT( string name, string addMod, string onOff,  bool translucentOn, string color)
        {
            
            string show;
            if (translucentOn)
            {
                show = "VO * SetVectorGeometry " + addMod + " \"" + name + "\" Show " + onOff + " Translucency 0.8 Color " + color + " ShowLabel On ArrowType 3D";
            }
            else
            {
            
                show = "VO * SetVectorGeometry " + addMod + " \"" + name + "\" Show " + onOff + " Color " + color + " ShowLabel On ArrowType 3D";
                
            }
            StkRoot.ExecuteCommand(show);
        }

        public static void ShowInclination(AgSatellite sat)
        {
           
            

            if (!(sat.Vgt.Planes.Contains("Orbit")))
            {
                // create orbit plane

                string orbitPlaneCmd = "VectorTool * Satellite/" + sat.InstanceName + " Create Plane Orbit \"Normal\" \"Satellite/" + sat.InstanceName + " Orbit_Normal\" \"Satellite/" + sat.InstanceName + " Velocity\" \"Satellite/" + sat.InstanceName + " Center\" \"X\" \"Y\"";
                StkRoot.ExecuteCommand(orbitPlaneCmd);
                ShowVGT("Satellite/" + sat.InstanceName + " Orbit Plane", "Add", "On", true, "White");
            }
            else
            {
                ShowVGT("Satellite/" + sat.InstanceName + " Orbit Plane", "Modify", "On", true, "White");
            }
            
           TryShowVGT("CentralBody/Earth Equator Plane", "On", true, "White");
            
            if (!(sat.Vgt.Angles.Contains("Inclination")))
            {
                string createInclination = "VectorTool * Satellite/" + sat.InstanceName + " Create Angle Inclination \"Between Planes\" \"Satellite/" + sat.InstanceName + " Orbit\" \"CentralBody/Earth Equator\"";
                StkRoot.ExecuteCommand(createInclination);
                ShowVGT("Satellite/" + sat.InstanceName +  " Inclination Angle", "Add", "On", false, "Green");
            }
            else
            {
                ShowVGT("Satellite/" + sat.InstanceName + " Inclination Angle", "Modify", "On", false, "Green");
            }

            
        }

        public static void HideInclination(AgSatellite sat)
        {


            ShowVGT("Satellite/" + sat.InstanceName + " Orbit Plane", "Modify", "Off", false, "White");
            ShowVGT("CentralBody/Earth Equator Plane" , "Modify", "Off", false, "White");
            ShowVGT("Satellite/" + sat.InstanceName + " Inclination Angle", "Modify", "Off", false, "Green");
              
          
        }

        public static void ShowAoP(AgSatellite sat)
        {

            TryShowVGT("Satellite/" + sat.InstanceName + " Ecc Vector", "On", false, "Red");
            TryShowVGT("Satellite/" + sat.InstanceName + " LineOfNodes Vector", "On", false, "Red");


            if (!(sat.Vgt.Angles.Contains("AoP")))
            {
                string cmd = "VectorTool * Satellite/" + sat.InstanceName + " Create Angle AoP \"Between Vectors\" \"Satellite/" + sat.InstanceName + " LineOfNodes\" \"Satellite/" + sat.InstanceName + " Ecc\"";
                StkRoot.ExecuteCommand(cmd);
                ShowVGT("Satellite/" + sat.InstanceName + " AoP Angle", "Add", "On", false, "Red");
            }
            
            else
            {
                ShowVGT("Satellite/" + sat.InstanceName + " AoP Angle", "Modify", "On", false, "Red");

            }
              
            

        }

        public static void HideAoP(AgSatellite sat)
        {


            ShowVGT("Satellite/" + sat.InstanceName + " LineOfNodes Vector", "Modify", "Off", false, "Red");
            ShowVGT("Satellite/" + sat.InstanceName + " Ecc Vector", "Modify", "Off", false, "Red");
            ShowVGT("Satellite/" + sat.InstanceName + " AoP Angle", "Modify", "Off", false, "Red");
              
            
        }

        public static void ShowRAAN(AgSatellite sat)
        {

            if (!(sat.Vgt.Angles.Contains("RAAN")))
            {
                string parent = "Satellite/" + sat.InstanceName;
                CreateShowDihedralAngle(parent, "RAAN", "CentralBody/Earth Inertial.X", parent + " LineOfNodes", "CentralBody/Earth Inertial.Z", "Red");
            }

            else
            {
                ShowVGT("Satellite/" + sat.InstanceName + " RAAN Angle", "Modify", "On", false, "Red");
            }

            TryShowVGT("Satellite/" + sat.InstanceName + " LineOfNodes Vector", "On", false, "Pink");
            TryShowVGT("CentralBody/Earth Inertial.X Vector", "On", false, "Pink");
            TryShowVGT("CentralBody/Earth Inertial.Z Vector", "On", false, "Pink");
                   
        }

        public static void HideRAAN(AgSatellite sat)
        {
            ShowVGT("Satellite/" + sat.InstanceName + " RAAN Angle", "Modify", "Off", false, "Red");
            ShowVGT("CentralBody/Earth Inertial.X Vector", "Modify", "Off", false, "Pink");
            ShowVGT("CentralBody/Earth Inertial.Z Vector", "Modify", "Off", false, "Pink");
            ShowVGT("Satellite/" + sat.InstanceName + " LineOfNodes Vector", "Modify", "Off", false, "Pink");
        }

        public static void ShowTA(AgSatellite sat)
        { 
            if (!(sat.Vgt.Angles.Contains("TrueAnomaly")))
            { 
                string parent = "Satellite/" + sat.InstanceName;
                CreateShowDihedralAngle(parent, "TrueAnomaly", parent + " Ecc", parent + " Position", parent + " AngMomentum", "White");
            }

            else
            {
                ShowVGT("Satellite/" + sat.InstanceName + " TrueAnomaly Angle", "Modify", "On", false, "White");
            }

            
            TryShowVGT("Satellite/" + sat.InstanceName + " Ecc Vector", "On", false, "Blue");
            TryShowVGT("Satellite/" + sat.InstanceName + " Position Vector", "On", false, "Blue");
            TryShowVGT("Satellite/" + sat.InstanceName + " AngMomentum Vector", "On", false, "Blue");
            TurnOnTrueScale(sat, "Position Vector");
        }
        
    

        public static void HideTA(AgSatellite sat)
        {

            ShowVGT("Satellite/" + sat.InstanceName + " TrueAnomaly Angle", "Modify", "Off", false, "Blue");
            ShowVGT("Satellite/" + sat.InstanceName + " Ecc Vector", "Modify", "Off", false, "Blue");
            ShowVGT("Satellite/" + sat.InstanceName + " Position Vector", "Modify", "Off", false, "Blue");
            ShowVGT("Satellite/" + sat.InstanceName + " AngMomentum Vector", "Modify", "Off", false, "Blue");
                
        }

       private static void CreateShowDihedralAngle(string parent, string angleName, string fromVec, string toVec, string aboutVec, string color)
        {
            string cmd = "VectorTool * " + parent + " Create Angle " + angleName + " \"Dihedral Angle\" \"" + fromVec + "\" \"" + toVec + "\" \"" + aboutVec + "\"";
            StkRoot.ExecuteCommand(cmd);
            ShowVGT(parent + " " + angleName + " Angle", "Add", "On", false, color);
        }

        private static IAgVORefCrdn GetVector(AgSatellite satObj, string name)
        {
            try
            {
                IAgVORefCrdn vector = satObj.VO.Vector.RefCrdns.GetCrdnByName(0, name);
                return vector;
            }
            catch
            {
                satObj.VO.Vector.RefCrdns.Add(0, name);
                return satObj.VO.Vector.RefCrdns.GetCrdnByName(0, name);
            }
        }

        private static void TurnOnTrueScale(AgSatellite satObj, string type)
        {
            string name = "Satellite/" + satObj.InstanceName + " " + type;
            IAgVORefCrdnVector ecc = GetVector(satObj, name) as IAgVORefCrdnVector;

            if(type.Contains("Periapsis") || type.Contains("Apoapsis"))
            {
                ecc.TrueScale = true;
            }
        }

        private static void ShowVector(AgSatellite satObj, string type)
        {
            
            string name = "Satellite/" + satObj.InstanceName + " " + type;
            IAgVORefCrdn ecc = GetVector(satObj, name);
            ecc.Visible = true;
            ecc.LabelVisible = true;
            IAgVORefCrdnVector eccVector = ecc as IAgVORefCrdnVector;
            eccVector.DrawAtCB = true;
            TurnOnTrueScale(satObj, type);

            
        }
        
        private static void HideVector(AgSatellite satObj, string type)
        {
            IAgVORefCrdn ecc = GetVector(satObj, "Satellite/" + satObj.InstanceName + " " + type + " Vector");
            ecc.Visible = false;
            
        }

        public static void CreateSat(IAgSatellite m_satellite)
        {
            
            if (StkRoot.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, "OrbitTunerSat"))
            {
                return;
            }
            m_satellite = StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "OrbitTunerSat") as IAgSatellite;

            // set prop. to J2 pertubation
            m_satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorJ2Perturbation);

            STKHelper.SetMaxViewing(m_satellite);
            // get the j2 perturbation propagator
            IAgVePropagatorJ2Perturbation prop = m_satellite.Propagator as IAgVePropagatorJ2Perturbation;
            prop.Propagate();

        }


    }
}
