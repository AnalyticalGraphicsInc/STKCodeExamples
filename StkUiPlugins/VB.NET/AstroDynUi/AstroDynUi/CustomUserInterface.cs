using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using stdole;
using AGI.STKUtil;

namespace AstroDynUi
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite embeddedControlSite;
        private Setup uiplugin;
        private AgStkObjectRoot root;
        double semimajorAxisSat1;
        double eccentricitySat1;
        double inclinationSat1;
        double argOfPerigeeSat1;
        double raanSat1;
        double trueAnomSat1;
        double semimajorAxisSat2;
        double eccentricitySat2;
        double inclinationSat2;
        double argOfPerigeeSat2;
        double raanSat2;
        double trueAnomSat2;

        public CustomUserInterface()
        {
            InitializeComponent();
        }

        #region Interfaces
        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            embeddedControlSite = Site;
            uiplugin = embeddedControlSite.Plugin as Setup;
            root = uiplugin.STKRoot;
        }

        public void OnClosing()
        {
            throw new NotImplementedException();
        }

        public void OnSaveModified()
        {
            throw new NotImplementedException();
        }

        public IPictureDisp GetIcon()
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region Propagation
        private void CreateSat1TwoBody()
        {
            try
            {
                try
                { root.ExecuteCommand("Unload / */Satellite/Sat1"); }
                catch
                { }

                IAgSatellite sat1 = root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Sat1") as IAgSatellite;
                sat1.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody);

                IAgVePropagatorTwoBody propSat1 = sat1.Propagator as IAgVePropagatorTwoBody;
                propSat1.Step = 60;

                IAgVeGfxAttributesOrbit sat1Graph = sat1.Graphics.Attributes as IAgVeGfxAttributesOrbit;
                sat1Graph.Color = Color.LimeGreen;
                sat1Graph.Line.Width = AgELineWidth.e2;

                //'Definisco i parametri Kepleriani classici del satellite
                IAgOrbitStateClassical classical2B = propSat1.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical) as IAgOrbitStateClassical;
                classical2B.CoordinateSystemType = AgECoordinateSystem.eCoordinateSystemJ2000;

                //'Uso il semiasse maggiore e l'eccentricità per definire la forma e la dimensione dell'orbita 
                classical2B.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
                IAgClassicalSizeShapeSemimajorAxis semi2B = classical2B.SizeShape as IAgClassicalSizeShapeSemimajorAxis;
                semi2B.SemiMajorAxis = semimajorAxisSat1;
                semi2B.Eccentricity = eccentricitySat1;

                //'Per definire l'orientamento dell'orbita nello spazio uso l'inclinazione, l'argomento del perigeo e la RAAN
                classical2B.Orientation.Inclination = inclinationSat1;
                classical2B.Orientation.ArgOfPerigee = argOfPerigeeSat1;
                classical2B.Orientation.AscNodeType = AgEOrientationAscNode.eAscNodeRAAN;
                IAgOrientationAscNodeRAAN raan = classical2B.Orientation.AscNode as IAgOrientationAscNodeRAAN;
                raan.Value = raanSat1;

                //'uso l'anomalia vera per definire la posizione iniziale del satellite lungo la sua orbita
                classical2B.LocationType = AgEClassicalLocation.eLocationTrueAnomaly;
                IAgClassicalLocationTrueAnomaly trueAnomaly = classical2B.Location as IAgClassicalLocationTrueAnomaly;
                trueAnomaly.Value = trueAnomSat1;

                //'Infine assegno i parametri orbtali così definiti al satellite e lo propago
                propSat1.InitialState.Representation.Assign(classical2B);
                propSat1.Propagate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateSat2TwoBody()
        {
            try
            {
                try
                { root.ExecuteCommand("Unload / */Satellite/Sat2"); }
                catch
                { }

                IAgSatellite sat2 = root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Sat2") as IAgSatellite;
                sat2.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody);

                IAgVePropagatorTwoBody propSat2 = sat2.Propagator as IAgVePropagatorTwoBody;
                propSat2.Step = 60;

                IAgVeGfxAttributesOrbit sat2Graph = sat2.Graphics.Attributes as IAgVeGfxAttributesOrbit;
                sat2Graph.Color = Color.Orange;
                sat2Graph.Line.Width = AgELineWidth.e2;

                //'Definisco i parametri Kepleriani classici del satellite
                IAgOrbitStateClassical classical2B = propSat2.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical) as IAgOrbitStateClassical;
                classical2B.CoordinateSystemType = AgECoordinateSystem.eCoordinateSystemJ2000;

                //'Uso il semiasse maggiore e l'eccentricità per definire la forma e la dimensione dell'orbita 
                classical2B.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
                IAgClassicalSizeShapeSemimajorAxis semi2B = classical2B.SizeShape as IAgClassicalSizeShapeSemimajorAxis;
                semi2B.SemiMajorAxis = semimajorAxisSat2;
                semi2B.Eccentricity = eccentricitySat2;

                //'Per definire l'orientamento dell'orbita nello spazio uso l'inclinazione, l'argomento del perigeo e la RAAN
                classical2B.Orientation.Inclination = inclinationSat2;
                classical2B.Orientation.ArgOfPerigee = argOfPerigeeSat2;
                classical2B.Orientation.AscNodeType = AgEOrientationAscNode.eAscNodeRAAN;
                IAgOrientationAscNodeRAAN raan = classical2B.Orientation.AscNode as IAgOrientationAscNodeRAAN;
                raan.Value = raanSat2;

                //'uso l'anomalia vera per definire la posizione iniziale del satellite lungo la sua orbita
                classical2B.LocationType = AgEClassicalLocation.eLocationTrueAnomaly;
                IAgClassicalLocationTrueAnomaly trueAnomaly = classical2B.Location as IAgClassicalLocationTrueAnomaly;
                trueAnomaly.Value = trueAnomSat2;

                //'Infine assegno i parametri orbtali così definiti al satellite e lo propago
                propSat2.InitialState.Representation.Assign(classical2B);
                propSat2.Propagate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateSat1J2()
        {
            try
            {
                try
                { root.ExecuteCommand("Unload / */Satellite/Sat1"); }
                catch
                { }

                IAgSatellite sat1 = root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Sat1") as IAgSatellite;
                sat1.SetPropagatorType(AgEVePropagatorType.ePropagatorJ2Perturbation);

                IAgVePropagatorJ2Perturbation propSat1 = sat1.Propagator as IAgVePropagatorJ2Perturbation;
                propSat1.Step = 60;

                IAgVeGfxAttributesOrbit sat1Graph = sat1.Graphics.Attributes as IAgVeGfxAttributesOrbit;
                sat1Graph.Color = Color.LimeGreen;
                sat1Graph.Line.Width = AgELineWidth.e2;

                IAgOrbitStateClassical classicalJ2 = propSat1.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical) as IAgOrbitStateClassical;
                classicalJ2.CoordinateSystemType = AgECoordinateSystem.eCoordinateSystemJ2000;

                classicalJ2.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
                IAgClassicalSizeShapeSemimajorAxis semiJ2 = classicalJ2.SizeShape as IAgClassicalSizeShapeSemimajorAxis;
                semiJ2.SemiMajorAxis = semimajorAxisSat1;
                semiJ2.Eccentricity = eccentricitySat1;

                classicalJ2.Orientation.Inclination = inclinationSat1;
                classicalJ2.Orientation.ArgOfPerigee = argOfPerigeeSat1;
                classicalJ2.Orientation.AscNodeType = AgEOrientationAscNode.eAscNodeRAAN;
                IAgOrientationAscNodeRAAN raan = classicalJ2.Orientation.AscNode as IAgOrientationAscNodeRAAN;
                raan.Value = raanSat1;

                classicalJ2.LocationType = AgEClassicalLocation.eLocationTrueAnomaly;
                IAgClassicalLocationTrueAnomaly trueAnomaly = classicalJ2.Location as IAgClassicalLocationTrueAnomaly;
                trueAnomaly.Value = trueAnomSat1;

                propSat1.InitialState.Representation.Assign(classicalJ2);
                propSat1.Propagate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateSat2J2()
        {
            try
            {
                try
                { root.ExecuteCommand("Unload / */Satellite/Sat2"); }
                catch
                { }

                IAgSatellite sat2 = root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Sat2") as IAgSatellite;
                sat2.SetPropagatorType(AgEVePropagatorType.ePropagatorJ2Perturbation);

                IAgVePropagatorJ2Perturbation propSat2 = sat2.Propagator as IAgVePropagatorJ2Perturbation;
                propSat2.Step = 60;

                IAgVeGfxAttributesOrbit sat2Graph = sat2.Graphics.Attributes as IAgVeGfxAttributesOrbit;
                sat2Graph.Color = Color.Orange;
                sat2Graph.Line.Width = AgELineWidth.e2;

                IAgOrbitStateClassical classicalJ2 = propSat2.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical) as IAgOrbitStateClassical;
                classicalJ2.CoordinateSystemType = AgECoordinateSystem.eCoordinateSystemJ2000;

                classicalJ2.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
                IAgClassicalSizeShapeSemimajorAxis semiJ2 = classicalJ2.SizeShape as IAgClassicalSizeShapeSemimajorAxis;
                semiJ2.SemiMajorAxis = semimajorAxisSat2;
                semiJ2.Eccentricity = eccentricitySat2;

                classicalJ2.Orientation.Inclination = inclinationSat2;
                classicalJ2.Orientation.ArgOfPerigee = argOfPerigeeSat2;
                classicalJ2.Orientation.AscNodeType = AgEOrientationAscNode.eAscNodeRAAN;
                IAgOrientationAscNodeRAAN raan = classicalJ2.Orientation.AscNode as IAgOrientationAscNodeRAAN;
                raan.Value = raanSat2;

                classicalJ2.LocationType = AgEClassicalLocation.eLocationTrueAnomaly;
                IAgClassicalLocationTrueAnomaly trueAnomaly = classicalJ2.Location as IAgClassicalLocationTrueAnomaly;
                trueAnomaly.Value = trueAnomSat2;

                propSat2.InitialState.Representation.Assign(classicalJ2);
                propSat2.Propagate();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Graphics
        private void GenerateGeometrySat1()
        {
            try
            {
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat1 Sat1PlnNorm Plane\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat1 Inclination Angle\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat1 RAAN Angle\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat1 TrueAnomaly Angle\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat1 Periapsis Vector\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat1 Position Vector\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat1 LineOfNodes Vector\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat1 Periapsis Vector\"");
            }
            catch (Exception)
            {
            }
            try
            {
                root.ExecuteCommand("VectorTool * Satellite/Sat1 Create Plane Sat1PlnNorm \"Normal\" \"Satellite/Sat1 Orbit_AngMomentum\" \"CentralBody/Earth ICRF-X\" \"CentralBody/Earth Center\" ");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat1 Sat1PlnNorm Plane\" Translucent On Translucency 0.5 DrawAtObject Off Color #32CD32");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat1 AngMomentum Vector\" Color #32CD32");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat1 Periapsis Vector\" Color #32CD32");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat1 Position Vector\" Color #32CD32");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat1 LineOfNodes Vector\" Color #32CD32");
                root.ExecuteCommand("VectorTool * Satellite/Sat1 Create Angle AoP \"Between Vectors\" \"Satellite/Sat1 LineOfNodes\" \"Satellite/Sat1 Periapsis\" ");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat1 AoP Angle\" Color #32CD32 Show Off");
                root.ExecuteCommand("VectorTool * Satellite/Sat1 Create Angle Inclination \"Between Vectors\" \"CentralBody/Earth ICRF-Z\" \"Satellite/Sat1 Orbit_AngMomentum\" ");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat1 Inclination Angle\" Color #32CD32 Show Off");
                root.ExecuteCommand("VectorTool * Satellite/Sat1 Create Angle RAAN \"Between Vectors\" \"CentralBody/Earth ICRF-X\" \"Satellite/Sat1 LineOfNodes\" ");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat1 RAAN Angle\" Color #32CD32 Show Off");
                root.ExecuteCommand("VectorTool * Satellite/Sat1 Create Angle TrueAnomaly \"Dihedral Angle\" \"Satellite/Sat1 Periapsis\" \"Satellite/Sat1 Position\" \"Satellite/Sat1 Orbit_Normal\" 0-360 Positive");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat1 TrueAnomaly Angle\" Color #32CD32 ShowDihedralAngleSupportingArcs Off Show Off  WindowID 1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerateGeometrySat2()
        {
            try
            {
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat2 Sat2PlnNorm Plane\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat2 Inclination Angle\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat2 RAAN Angle\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat2 TrueAnomaly Angle\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat2 Periapsis Vector\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat2 Position Vector\"");
                root.ExecuteCommand("VO * SetVectorGeometry Delete \"Satellite/Sat2 LineOfNodes Vector\"");
            }
            catch (Exception)
            {
            }
            try
            {
                root.ExecuteCommand("VectorTool * Satellite/Sat2 Create Plane Sat2PlnNorm \"Normal\" \"Satellite/Sat2 Orbit_AngMomentum\" \"CentralBody/Earth ICRF-X\" \"CentralBody/Earth Center\" ");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat2 Sat2PlnNorm Plane\" Translucent On Translucency 0.5 DrawAtObject Off Color #FFA500");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat2 AngMomentum Vector\" Color #FFA500");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat2 Periapsis Vector\" Color #FFA500");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat2 Position Vector\" Color #FFA500");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat2 LineOfNodes Vector\" Color #FFA500");
                root.ExecuteCommand("VectorTool * Satellite/Sat2 Create Angle AoP \"Between Vectors\" \"Satellite/Sat2 LineOfNodes\" \"Satellite/Sat2 Periapsis\" ");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat2 AoP Angle\" Color #FFA500 Show Off");
                root.ExecuteCommand("VectorTool * Satellite/Sat2 Create Angle Inclination \"Between Vectors\" \"CentralBody/Earth ICRF-Z\" \"Satellite/Sat2 Orbit_AngMomentum\" ");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat2 Inclination Angle\" Color #FFA500 Show Off");
                root.ExecuteCommand("VectorTool * Satellite/Sat2 Create Angle RAAN \"Between Vectors\" \"CentralBody/Earth ICRF-X\" \"Satellite/Sat2 LineOfNodes\" ");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat2 RAAN Angle\" Color #FFA500 Show Off");
                root.ExecuteCommand("VectorTool * Satellite/Sat2 Create Angle TrueAnomaly \"Dihedral Angle\" \"Satellite/Sat2 Periapsis\" \"Satellite/Sat2 Position\" \"Satellite/Sat2 Orbit_Normal\" 0-360 Positive");
                root.ExecuteCommand("VO * SetVectorGeometry Add \"Satellite/Sat2 TrueAnomaly Angle\" Color #FFA500 ShowDihedralAngleSupportingArcs Off Show Off  WindowID 1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InertialAxes()
        {
            if (this.cbInertialAxes.Checked)
            {
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"CentralBody/Earth Inertial Axes\" ShowLabel On Color white Show On");
                }
                catch (Exception)
                {
                }
            }
            else if (!this.cbInertialAxes.Checked)
            {
                root.ExecuteCommand("VO * SetVectorGeometry Modify \"CentralBody/Earth Inertial Axes\" Show Off");
            }
        }

        private void FixedAxes()
        {
            if (this.cbFixedAxes.Checked)
            {
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"CentralBody/Earth Fixed  Axes\" ShowLabel On Color white Show On");
                }
                catch (Exception)
                { }
            }
            else if (!this.cbFixedAxes.Checked)
            {
                root.ExecuteCommand("VO * SetVectorGeometry Modify \"CentralBody/Earth Fixed  Axes\" Show Off");
            }
        }

        private void SunVector()
        {
            if (this.cbSunVector.Checked)
            {
                root.ExecuteCommand("VO * SetVectorGeometry Modify \"CentralBody/Earth Sun Vector\" ShowLabel On Show On");
            }
            else if (!this.cbSunVector.Checked)
            {
                root.ExecuteCommand("VO * SetVectorGeometry Modify \"CentralBody/Earth Sun Vector\" Show Off");
            }
        }

        private void OrbitPlane()
        {
            if (this.cbOrbitPlane.Checked)
            {
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 Sat1PlnNorm Plane\" Show On");
                }
                catch (Exception)
                { }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 Sat2PlnNorm Plane\" Show On");
                }
                catch (Exception)
                { }
            }
            else if (!this.cbOrbitPlane.Checked)
            {
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 Sat1PlnNorm Plane\" Show Off");
                }
                catch (Exception)
                { }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 Sat2PlnNorm Plane\" Show Off");
                }
                catch (Exception)
                { }
            }
        }
        
        private void EquatorialPlane()
        {
            if (this.cbEquatorialPlane.Checked)
            {
                try
                {
                    root.ExecuteCommand("VO * Grids Space ShowECI On ECIColor gray ShowRadial On");
                }
                catch (Exception)
                { }
            }
            else if (!this.cbEquatorialPlane.Checked)
            {
                root.ExecuteCommand("VO * Grids Space ShowECI Off");
            }
        }

        private void Inclination()
        {
            if (this.cbInclination.Checked)
            {
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 AngMomentum Vector\" Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 Inclination Angle\" Show On");
                }
                catch (Exception)
                { }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 AngMomentum Vector\" Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 Inclination Angle\" Show On");
                }
                catch (Exception)
                { }
            }
            else if (!this.cbInclination.Checked)
            {
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 AngMomentum Vector\" Show Off");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 Inclination Angle\" Show Off");
                }
                catch (Exception)
                { }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 AngMomentum Vector\" Show Off");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 Inclination Angle\" Show Off");
                }
                catch (Exception)
                { }
            }
        }
        
        private void RAAN()
        {
            if (this.cbRaan.Checked)
            {
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 RAAN Angle\" Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 LineOfNodes Vector\" ShowLabel On Show On");
                }
                catch (Exception)
                { }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 RAAN Angle\" Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 LineOfNodes Vector\" ShowLabel On Show On");
                }
                catch (Exception)
                { }
            }
            else if (!this.cbRaan.Checked)
            {
                if (!this.cbAoP.Checked)
                {
                    try
                    {
                        root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 LineOfNodes Vector\" Show Off");
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 LineOfNodes Vector\" Show Off");
                    }
                    catch (Exception)
                    { }
                }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 RAAN Angle\" Show Off");
                }
                catch (Exception)
                { }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 RAAN Angle\" Show Off");
                }
                catch (Exception)
                { }
            }
        }

        private void TrueAnomaly()
        {
            if (this.cbTrueAnomaly.Checked)
            {
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 TrueAnomaly Angle\" Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 Periapsis Vector\" ShowLabel On Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 Position Vector\" ShowLabel On Show On");
                }
                catch (Exception)
                { }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 TrueAnomaly Angle\" Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 Periapsis Vector\" ShowLabel On Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 Position Vector\" ShowLabel On Show On");
                }
                catch (Exception)
                { }
            }
            else if (!this.cbTrueAnomaly.Checked)
            {
                if (!this.cbAoP.Checked)
                {
                    try
                    {
                        root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 Periapsis Vector\" Show Off");
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 Periapsis Vector\" Show Off");
                    }
                    catch (Exception)
                    { }
                }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 TrueAnomaly Angle\" Show Off");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 Position Vector\" Show Off");
                }
                catch (Exception)
                { }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 TrueAnomaly Angle\" Show Off");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 Position Vector\" Show Off");
                }
                catch (Exception)
                { }
            }
        }
        
        private void AoP()
        {
            if (this.cbAoP.Checked)
            {
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 AoP Angle\" Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 Periapsis Vector\" ShowLabel On Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 LineOfNodes Vector\" ShowLabel On Show On");
                }
                catch (Exception)
                { }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 AoP Angle\" Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 Periapsis Vector\" ShowLabel On Show On");
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 LineOfNodes Vector\" ShowLabel On Show On");
                }
                catch (Exception)
                { }
            }
            else if (!this.cbAoP.Checked)
            {
                if (!this.cbTrueAnomaly.Checked)
                {
                    try
                    {
                        root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 Periapsis Vector\" Show Off");
                    }
                    catch { }
                    try
                    {
                        root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 Periapsis Vector\" Show Off");
                    }
                    catch { }
                }
                if (!this.cbRaan.Checked)
                {
                    try
                    {
                        root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 LineOfNodes Vector\" Show Off");
                    }
                    catch { }
                    try
                    {
                        root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 LineOfNodes Vector\" Show Off");
                    }
                    catch { }
                }
                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat1 AoP Angle\" Show Off");
                }
                catch (Exception)
                { }

                try
                {
                    root.ExecuteCommand("VO * SetVectorGeometry Modify \"Satellite/Sat2 AoP Angle\" Show Off");
                }
                catch (Exception)
                { }
            }
        }
        #endregion

        #region Events
        private void btnSat1Prop_Click(object sender, EventArgs e)
        {
            string semimajorAxisString = this.tbSat1SemiMajorAxis.Text;
            if (String.IsNullOrEmpty(semimajorAxisString))
            {
                MessageBox.Show("The Semimajor axis field is empty. Please fill it");
                return;
            }
            try
            { semimajorAxisSat1 = Double.Parse(this.tbSat1SemiMajorAxis.Text); }
            catch
            {
                MessageBox.Show("The Semimajor axis field has a non valid format");
                return;
            }

            string eccentricityString = this.tbSat1Eccentricity.Text;
            if (String.IsNullOrEmpty(eccentricityString))
            {
                MessageBox.Show("The Eccentricity field is empty. Please fill it");
                return;
            }
            try
            { eccentricitySat1 = Double.Parse(this.tbSat1Eccentricity.Text); }
            catch
            {
                MessageBox.Show("The Eccentricity field has a non valid format");
                return;
            }

            string inclinationString = this.tbSat1Inclination.Text;
            if (String.IsNullOrEmpty(inclinationString))
            {
                MessageBox.Show("The Inclination field is empty. Please fill it");
                return;
            }
            try
            { inclinationSat1 = Double.Parse(this.tbSat1Inclination.Text); }
            catch
            {
                MessageBox.Show("The Inclination field has a non valid format");
                return;
            }

            string argOfPerigeeString = this.tbSat1AoP.Text;
            if (String.IsNullOrEmpty(argOfPerigeeString))
            {
                MessageBox.Show("The ArgOfPerigee field is empty. Please fill it");
                return;
            }
            try
            { argOfPerigeeSat1 = Double.Parse(this.tbSat1AoP.Text); }
            catch
            {
                MessageBox.Show("The ArgOfPerigee field has a non valid format");
                return;
            }

            string raanString = this.tbSat1RAAN.Text;
            if (String.IsNullOrEmpty(raanString))
            {
                MessageBox.Show("The RAAN field is empty. Please fill it");
                return;
            }
            try
            { raanSat1 = Double.Parse(this.tbSat1RAAN.Text); }
            catch
            {
                MessageBox.Show("The RAAN field has a non valid format");
                return;
            }

            string trueAnomString = this.tbSat1TrueAnomaly.Text;
            if (String.IsNullOrEmpty(trueAnomString))
            {
                MessageBox.Show("The True Anomaly field is empty. Please fill it");
                return;
            }
            try
            { trueAnomSat1 = Double.Parse(this.tbSat1TrueAnomaly.Text); }
            catch
            {
                MessageBox.Show("The True Anomaly field has a non valid format");
                return;
            }

            if (this.rbSat1TwoBody.Checked)
            {
                CreateSat1TwoBody();
            }
            else if (!this.rbSat1TwoBody.Checked)
            {
                CreateSat1J2();
            }

            //OrbitDisplay currentDisplay = new OrbitDisplay(this.cbOrbitPlane.Checked, this.cbInclination.Checked);

            GenerateGeometrySat1();

            if (this.cbOrbitPlane.Checked)
            {
                this.cbOrbitPlane.Checked = false;
                OrbitPlane();
                this.cbOrbitPlane.Checked = true;
                OrbitPlane();
            }

            if (this.cbInclination.Checked)
            {
                this.cbInclination.Checked = false;
                Inclination();
                this.cbInclination.Checked = true;
                Inclination();
            }

            if (this.cbRaan.Checked)
            {
                this.cbRaan.Checked = false;
                RAAN();
                this.cbRaan.Checked = true;
                RAAN();
            }

            if (this.cbTrueAnomaly.Checked)
            {
                this.cbTrueAnomaly.Checked = false;
                TrueAnomaly();
                this.cbTrueAnomaly.Checked = true;
                TrueAnomaly();
            }

            if (this.cbAoP.Checked)
            {
                this.cbAoP.Checked = false;
                AoP();
                this.cbAoP.Checked = true;
                AoP();
            }

            //OrbitPlane();
            //Inclination();
            //RAAN();
            //TrueAnomaly();
            //AoP();
        }

        private void btnSat2Prop_Click(object sender, EventArgs e)
        {
            string semimajorAxisString = this.tbSat2SemiMajorAxis.Text;
            if (String.IsNullOrEmpty(semimajorAxisString))
            {
                MessageBox.Show("The Semimajor axis field is empty. Please fill it");
                return;
            }
            try
            { semimajorAxisSat2 = Double.Parse(this.tbSat2SemiMajorAxis.Text); }
            catch
            {
                MessageBox.Show("The Semimajor axis field has a non valid format");
                return;
            }

            string eccentricityString = this.tbSat2Eccentricity.Text;
            if (String.IsNullOrEmpty(eccentricityString))
            {
                MessageBox.Show("The Eccentricity field is empty. Please fill it");
                return;
            }
            try
            { eccentricitySat2 = Double.Parse(this.tbSat2Eccentricity.Text); }
            catch
            {
                MessageBox.Show("The Eccentricity field has a non valid format");
                return;
            }

            string inclinationString = this.tbSat2Inclination.Text;
            if (String.IsNullOrEmpty(inclinationString))
            {
                MessageBox.Show("The Inclination field is empty. Please fill it");
                return;
            }
            try
            { inclinationSat2 = Double.Parse(this.tbSat2Inclination.Text); }
            catch
            {
                MessageBox.Show("The Inclination field has a non valid format");
                return;
            }

            string argOfPerigeeString = this.tbSat2AoP.Text;
            if (String.IsNullOrEmpty(argOfPerigeeString))
            {
                MessageBox.Show("The ArgOfPerigee field is empty. Please fill it");
                return;
            }
            try
            { argOfPerigeeSat2 = Double.Parse(this.tbSat2AoP.Text); }
            catch
            {
                MessageBox.Show("The ArgOfPerigee field has a non valid format");
                return;
            }

            string raanString = this.tbSat2RAAN.Text;
            if (String.IsNullOrEmpty(raanString))
            {
                MessageBox.Show("The RAAN field is empty. Please fill it");
                return;
            }
            try
            { raanSat2 = Double.Parse(this.tbSat2RAAN.Text); }
            catch
            {
                MessageBox.Show("The RAAN field has a non valid format");
                return;
            }

            string trueAnomString = this.tbSat2TrueAnomaly.Text;
            if (String.IsNullOrEmpty(trueAnomString))
            {
                MessageBox.Show("The True Anomaly field is empty. Please fill it");
                return;
            }
            try
            { trueAnomSat2 = Double.Parse(this.tbSat2TrueAnomaly.Text); }
            catch
            {
                MessageBox.Show("The True Anomaly field has a non valid format");
                return;
            }

            if (this.rbSat2TwoBody.Checked)
            {
                CreateSat2TwoBody();
            }
            else if (!this.rbSat2TwoBody.Checked)
            {
                CreateSat2J2();
            }

            GenerateGeometrySat2();
            OrbitPlane();
            Inclination();
            RAAN();
            TrueAnomaly();
            AoP();
        }

        private void btnSat1del_Click(object sender, EventArgs e)
        {
            try
            { root.ExecuteCommand("Unload / */Satellite/Sat1"); }
            catch
            { }
        }

        private void btnSat2Del_Click(object sender, EventArgs e)
        {
            try
            { root.ExecuteCommand("Unload / */Satellite/Sat2"); }
            catch
            { }
        }

        private void cbOrbitPlane_CheckedChanged(object sender, EventArgs e)
        {
            OrbitPlane();
        }

        private void cbInclination_CheckedChanged(object sender, EventArgs e)
        {
            Inclination();
        }

        private void cbInertialAxes_CheckedChanged(object sender, EventArgs e)
        {
            InertialAxes();
        }

        private void cbFixedAxes_CheckedChanged(object sender, EventArgs e)
        {
            FixedAxes();
        }

        private void cbEquatorialPlane_CheckedChanged(object sender, EventArgs e)
        {
            EquatorialPlane();
        }

        private void cbRaan_CheckedChanged(object sender, EventArgs e)
        {
            RAAN();
        }

        private void cbTrueAnomaly_CheckedChanged(object sender, EventArgs e)
        {
            TrueAnomaly();
        }

        private void cbAoP_CheckedChanged(object sender, EventArgs e)
        {
            AoP();
        }

        private void cbSunVector_CheckedChanged(object sender, EventArgs e)
        {
            SunVector();
        }

        private void btnCopySat2_Click(object sender, EventArgs e)
        {
            this.tbSat1AoP.Text = this.tbSat2AoP.Text;
            this.tbSat1Eccentricity.Text = this.tbSat2Eccentricity.Text;
            this.tbSat1Inclination.Text = this.tbSat2Inclination.Text;
            this.tbSat1RAAN.Text = this.tbSat2RAAN.Text;
            this.tbSat1SemiMajorAxis.Text = this.tbSat2SemiMajorAxis.Text;
            this.tbSat1TrueAnomaly.Text = this.tbSat2TrueAnomaly.Text;
        }

        private void btnCopySat1_Click(object sender, EventArgs e)
        {
            this.tbSat2AoP.Text = this.tbSat1AoP.Text;
            this.tbSat2Eccentricity.Text = this.tbSat1Eccentricity.Text;
            this.tbSat2Inclination.Text = this.tbSat1Inclination.Text;
            this.tbSat2RAAN.Text = this.tbSat1RAAN.Text;
            this.tbSat2SemiMajorAxis.Text = this.tbSat1SemiMajorAxis.Text;
            this.tbSat2TrueAnomaly.Text = this.tbSat1TrueAnomaly.Text;
        }
        #endregion
    }

    //public class OrbitDisplay
    //{
    //    private bool _showOrbitPlane;
        
    //    public OrbitDisplay(bool orbitPlane, bool inclination)
    //    {
    //        _showOrbitPlane = orbitPlane;
    //    }

    //    public bool ShowOrbitPlane
    //    {
    //        get { return _showOrbitPlane; }
    //    }
    //}
}
