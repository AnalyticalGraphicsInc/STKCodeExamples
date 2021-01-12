using AGI.STKObjects;
using AGI.STKUtil;
using AGI.Ui.Application;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerConstellationBuilder.Routines
{
    class STK
    {

        public static void CheckStkActiveXInstance()
        {
            /// <summary>
            /// This routine verifies if there is an STK scenario opened
            /// </summary>

            AgStkObjectRoot root;
            AgUiApplication app;

            try
            {
                app = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application") as AGI.Ui.Application.AgUiApplication;
                root = (AgStkObjectRoot)app.Personality2;
            }
            catch (Exception)
            {
                throw new System.Exception();
            }
        }
        public static void CreateSatellite(string satName, double orbitPeriod, double eccentricity, double inclination, double rightAscension, double meanAnomaly, double argOfPerigee)
        {
            AgStkObjectRoot root;
            AgUiApplication app;

            app = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application") as AGI.Ui.Application.AgUiApplication;
            root = (AgStkObjectRoot)app.Personality2;
    
            // new satellite
            IAgSatellite sat = root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, satName) as IAgSatellite;

            // set the propagator to J2
            sat.SetPropagatorType(AgEVePropagatorType.ePropagatorJ2Perturbation);

            // get the propagator
            IAgVePropagatorJ2Perturbation j2 = (IAgVePropagatorJ2Perturbation) sat.Propagator;


            //Define the satellite's orbit using classical (Keplerian) orbital elements
            IAgOrbitStateClassical classical = (IAgOrbitStateClassical) j2.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical);

            //Use period and eccentricity to define the size and shape of the orbit
            classical.SizeShapeType = AgEClassicalSizeShape.eSizeShapePeriod;
            IAgClassicalSizeShapePeriod period = (IAgClassicalSizeShapePeriod)classical.SizeShape;
            period.Eccentricity = eccentricity;
            period.Period = orbitPeriod;

            //Use argument of perigee, inclination and RAAN to define the orientation of the orbit
            classical.Orientation.ArgOfPerigee = argOfPerigee;
            classical.Orientation.Inclination = inclination;
            classical.Orientation.AscNodeType = AgEOrientationAscNode.eAscNodeRAAN;
            IAgOrientationAscNodeRAAN raan = (IAgOrientationAscNodeRAAN)classical.Orientation.AscNode;
            raan.Value = rightAscension;

            //Use mean anomaly to specify the position of the satellite in orbit
            classical.LocationType = AgEClassicalLocation.eLocationMeanAnomaly;
            IAgClassicalLocationMeanAnomaly ma = (IAgClassicalLocationMeanAnomaly)classical.Location;
            ma.Value = meanAnomaly;

            //Assign the orbital elements to the satellite's propagator and propagate the orbit	
            j2.InitialState.Representation.Assign(classical);
            j2.Propagate();
        }

        public static void SetOrbitGraphics(string satName, bool showInertial, bool showFixed)
        {
            AgStkObjectRoot root;
            AgUiApplication app;

            // initialize variables
            app = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application") as AGI.Ui.Application.AgUiApplication;
            root = (AgStkObjectRoot)app.Personality2;

            // get the satellite object interfce
            string objectPath = " */Satellite/" + satName;
            IAgSatellite sat = root.GetObjectFromPath(objectPath) as IAgSatellite;

            // Configure the pass graphics
            IAgVeVOPass pass = sat.VO.Pass as IAgVeVOPass;
            pass.TrackData.PassData.Orbit.SetLeadDataType(AgELeadTrailData.eDataAll);
            pass.TrackData.PassData.Orbit.SetTrailDataType(AgELeadTrailData.eDataAll);

            // Change the orbit system
            IAgVeVOSystemsCollection orbitSystemsCollection = sat.VO.OrbitSystems as IAgVeVOSystemsCollection;

            if (showInertial)
            { orbitSystemsCollection.InertialByWindow.IsVisible = true; }
            else { orbitSystemsCollection.InertialByWindow.IsVisible = false; }

            if (showFixed)
            { orbitSystemsCollection.FixedByWindow.IsVisible = true; }
            else { orbitSystemsCollection.FixedByWindow.IsVisible = false; }
            
        }

        public static void SetColorSingle(string satName, Color color)
        {
            AgStkObjectRoot root;
            AgUiApplication app;

            // initialize variables
            app = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application") as AGI.Ui.Application.AgUiApplication;
            root = (AgStkObjectRoot)app.Personality2;

            // define the sat object
            IAgSatellite sat = (IAgSatellite)root.CurrentScenario.Children[satName];

            // set color               
            sat.Graphics.SetAttributesType(AgEVeGfxAttributes.eAttributesBasic);
            IAgVeGfxAttributesBasic basicAttributes = (IAgVeGfxAttributesBasic)sat.Graphics.Attributes;
            basicAttributes.Line.Width = AgELineWidth.e2;
            basicAttributes.Color = color;
        }

        public static void CreateConstellation(string constName, int satNumber)
        {
            AgStkObjectRoot root;
            AgUiApplication app;

            // initialize variables
            app = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application") as AGI.Ui.Application.AgUiApplication;
            root = (AgStkObjectRoot)app.Personality2;

            // define the const object
            IAgConstellation constell = (IAgConstellation)root.CurrentScenario.Children.New(AgESTKObjectType.eConstellation, constName);

            for (int i = 0; i < satNumber; i++)
            {
                constell.Objects.Add("Satellite/" + constName + "_" + (i + 1));
            }
        }
    }
}
