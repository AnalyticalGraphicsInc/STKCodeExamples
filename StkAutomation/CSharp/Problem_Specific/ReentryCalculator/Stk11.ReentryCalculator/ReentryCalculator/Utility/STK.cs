using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.STKObjects;
using AGI.Ui.Application;
using AGI.STKObjects.Astrogator;
using System.Drawing;
using System.IO;
using AGI.STKUtil;
using AGI.STKVgt;

namespace ReentryCalculator.Utility
{
    class STK
    {
        public static void CreateScenarioFromTle(ref AgStkObjectRoot root, double duration, ref InitialState initState, TLE currentTle)
        {

            root.NewScenario(currentTle.GetSatNumber() + "_Reentry");
            DateTime startDate = currentTle.GetTleEpoch();
            DateTime stopDate = startDate.AddDays(duration);

            IAgScenario scenario = root.CurrentScenario as IAgScenario;
            // Set scenario start and stop times
            scenario.SetTimePeriod(startDate.ToString("dd MMM yyyy hh:mm:ss.fff"), stopDate.ToString("dd MMM yyyy hh:mm:ss.fff"));

            // remove the terrain option
            root.ExecuteCommand("Terrain * TerrainServer UseTerrainForAnalysis No");

            // create the SGP4 object from the TLE
            IAgSatellite decayingSat = (IAgSatellite)root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, currentTle.GetSatNumber());
            decayingSat.SetPropagatorType(AgEVePropagatorType.ePropagatorSGP4);
            IAgVePropagatorSGP4 sgp4 = decayingSat.Propagator as IAgVePropagatorSGP4;
            sgp4.CommonTasks.AddSegsFromFile(currentTle.GetSatNumber(), currentTle.GetFilePath());
            IAgVeGfxAttributesBasic att = decayingSat.Graphics.Attributes as IAgVeGfxAttributesBasic;
            att.Color = Color.Yellow;

            // Configure time period
            sgp4.EphemerisInterval.SetExplicitInterval(scenario.StartTime, scenario.StopTime);
            sgp4.Step = 60.0;
            // Propagate
            sgp4.Propagate();

            // change the 3D model
            try
            {
                root.ExecuteCommand("VO */Satellite/" + currentTle.GetSatNumber() + " Model File \"C:\\Program Files\\AGI\\STK 11\\STKData\\VO\\Models\\Misc\\explosion.mdl\"");
            }
            catch (Exception)
            {

            }

            //// get the initial state from the TLE            
            initState.Epoch = scenario.StartTime;

            IAgDataPrvTimeVar dpPos = ((IAgStkObject)decayingSat).DataProviders.GetDataPrvTimeVarFromPath("Cartesian Position//ICRF");
            IAgDrResult resultPos = dpPos.Exec(scenario.StartTime, scenario.StartTime, 60.0);
            IAgDrDataSetCollection datasetsPos = resultPos.DataSets;
            if (resultPos.DataSets.Count > 0)
            {
                initState.CartesianPosX = datasetsPos[1].GetValues().GetValue(0).ToString();
                initState.CartesianPosY = datasetsPos[2].GetValues().GetValue(0).ToString();
                initState.CartesianPosZ = datasetsPos[3].GetValues().GetValue(0).ToString();
            }

            IAgDataPrvTimeVar dpVel = ((IAgStkObject)decayingSat).DataProviders.GetDataPrvTimeVarFromPath("Cartesian Velocity//ICRF");
            IAgDrResult resultVel = dpVel.Exec(scenario.StartTime, scenario.StartTime, 60.0);
            IAgDrDataSetCollection datasetsVel = resultVel.DataSets;
            if (resultVel.DataSets.Count > 0)
            {
                initState.CartesianVelX = datasetsVel[1].GetValues().GetValue(0).ToString();
                initState.CartesianVelY = datasetsVel[2].GetValues().GetValue(0).ToString();
                initState.CartesianVelZ = datasetsVel[3].GetValues().GetValue(0).ToString();
            }

            // configure the 2D graphics
            root.ExecuteCommand("Graphics * BackgroundImage Show Off");
            root.ExecuteCommand("MapDetails * LatLon Lon On 15");
            root.ExecuteCommand("MapDetails * Map RWDB2_Islands State On Color green");
            root.ExecuteCommand("MapDetails * Map RWDB2_International_Borders State On ");
            root.ExecuteCommand("MapDetails * Map RWDB2_Lakes State On");

            try
            {
                root.ExecuteCommand("VO * Globe Show On File \"C:\\Program Files\\AGI\\STK 11\\STKData\\VO\\Globes\\Earth\\WhiteOnBlue.glb\"");
            }
            catch (Exception)
            {
            }

            // configure the 3D graphics
            root.ExecuteCommand("VO * GlobeDetails MapDetail Show On Map RWDB2_Coastlines ShowDetail On");
            root.ExecuteCommand("VO * GlobeDetails MapDetail Show On Map RWDB2_Islands ShowDetail On DetailColor green");
            root.ExecuteCommand("VO * GlobeDetails MapDetail Show On Map RWDB2_Lakes ShowDetail On DetailColor lightblue");


            //dynamic uiApp = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application"); 
            //try
            //{
            //    foreach (dynamic window in uiApp.Windows)
            //    {
            //        string windowCaption = (string)window.Caption;
            //        if (windowCaption.Contains("2D"))
            //        {
            //            window.DockStyle = 3;
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    string error = ex.Message;
            //}

        }

        public static TLE CreateScenarioFromSatcat(ref AgStkObjectRoot root, double duration, ref InitialState initState, string satId)
        {

            root.NewScenario(satId + "_Reentry");
            //DateTime startDate = currentTle.GetTleEpoch();
            //DateTime stopDate = startDate.AddDays(duration);

            IAgScenario scenario = root.CurrentScenario as IAgScenario;

            // remove the terrain option
            root.ExecuteCommand("Terrain * TerrainServer UseTerrainForAnalysis No");

            // import the satellite from the catalog
            root.ExecuteCommand("ImportFromDB * Satellite AGIServer Propagate On TimePeriod UseScenarioInterval SSCNumber " + satId + " \"");

            // get the satellite start time
            IAgStkObject satObj = root.CurrentScenario.Children[0];
            IAgSatellite decayingSat = satObj as IAgSatellite;
            IAgVePropagatorSGP4 sgp4 = decayingSat.Propagator as IAgVePropagatorSGP4;
            //IAgCrdnEventSmartEpoch startEpoch = sgp4.EphemerisInterval.GetStartEpoch();
            dynamic startEpoch= sgp4.Segments[0].SwitchTime;
            // Set scenario start and stop times
            DateTime startDate = STK.GetDateTimeFromStkTimeFormat(Convert.ToString(startEpoch));
            DateTime stopDate = startDate.AddDays(duration);
            scenario.SetTimePeriod(startDate.ToString("dd MMM yyyy hh:mm:ss.fff"), stopDate.ToString("dd MMM yyyy hh:mm:ss.fff"));

            // get the TLE data back from STK
            TLE tle = new TLE(startDate, sgp4.Segments[0].SSCNum, Convert.ToString(sgp4.Segments[0].Inclination),
                              Convert.ToString(sgp4.Segments[0].Eccentricity), Convert.ToString(sgp4.Segments[0].RevNumber),
                              Convert.ToString(sgp4.Segments[0].MeanMotion));

            IAgVeGfxAttributesBasic att = decayingSat.Graphics.Attributes as IAgVeGfxAttributesBasic;
            att.Color = Color.Yellow;

            // Configure time period
            sgp4.EphemerisInterval.SetExplicitInterval(scenario.StartTime, scenario.StopTime);
            sgp4.Step = 60.0;

            // change the 3D model
            try
            {
                root.ExecuteCommand("VO */Satellite/" + root.CurrentScenario.Children[0].InstanceName + " Model File \"C:\\Program Files\\AGI\\STK 11\\STKData\\VO\\Models\\Misc\\explosion.mdl\"");
            }
            catch (Exception)
            {

            }

            //// get the initial state from the TLE            
            initState.Epoch = scenario.StartTime;

            IAgDataPrvTimeVar dpPos = ((IAgStkObject)decayingSat).DataProviders.GetDataPrvTimeVarFromPath("Cartesian Position//ICRF");
            IAgDrResult resultPos = dpPos.Exec(scenario.StartTime, scenario.StartTime, 60.0);
            IAgDrDataSetCollection datasetsPos = resultPos.DataSets;
            if (resultPos.DataSets.Count > 0)
            {
                initState.CartesianPosX = datasetsPos[1].GetValues().GetValue(0).ToString();
                initState.CartesianPosY = datasetsPos[2].GetValues().GetValue(0).ToString();
                initState.CartesianPosZ = datasetsPos[3].GetValues().GetValue(0).ToString();
            }

            IAgDataPrvTimeVar dpVel = ((IAgStkObject)decayingSat).DataProviders.GetDataPrvTimeVarFromPath("Cartesian Velocity//ICRF");
            IAgDrResult resultVel = dpVel.Exec(scenario.StartTime, scenario.StartTime, 60.0);
            IAgDrDataSetCollection datasetsVel = resultVel.DataSets;
            if (resultVel.DataSets.Count > 0)
            {
                initState.CartesianVelX = datasetsVel[1].GetValues().GetValue(0).ToString();
                initState.CartesianVelY = datasetsVel[2].GetValues().GetValue(0).ToString();
                initState.CartesianVelZ = datasetsVel[3].GetValues().GetValue(0).ToString();
            }

            // configure the 2D graphics
            root.ExecuteCommand("Graphics * BackgroundImage Show Off");
            root.ExecuteCommand("MapDetails * LatLon Lon On 15");
            root.ExecuteCommand("MapDetails * Map RWDB2_Islands State On Color green");
            root.ExecuteCommand("MapDetails * Map RWDB2_International_Borders State On ");
            root.ExecuteCommand("MapDetails * Map RWDB2_Lakes State On");

            try
            {
                root.ExecuteCommand("VO * Globe Show On File \"C:\\Program Files\\AGI\\STK 11\\STKData\\VO\\Globes\\Earth\\WhiteOnBlue.glb\"");
            }
            catch (Exception)
            {
            }

            // configure the 3D graphics
            root.ExecuteCommand("VO * GlobeDetails MapDetail Show On Map RWDB2_Coastlines ShowDetail On");
            root.ExecuteCommand("VO * GlobeDetails MapDetail Show On Map RWDB2_Islands ShowDetail On DetailColor green");
            root.ExecuteCommand("VO * GlobeDetails MapDetail Show On Map RWDB2_Lakes ShowDetail On DetailColor lightblue");


            return tle;
        }


        public static void ConfigurePropagator(AgStkObjectRoot root)
        {
            try
            {
                // duplicate the default HPOP propagator % works good
                //root.ExecuteCommand("ComponentBrowser */ Duplicate Propagators \"Earth HPOP Default v10\" \"CustomProp\"");
                //root.ExecuteCommand("ComponentBrowser */ SetValue Propagators \"CustomProp\" PropFuncs.Gravitational_Force.Degree 70");
                //root.ExecuteCommand("ComponentBrowser */ SetValue Propagators \"CustomProp\" PropFuncs.Gravitational_Force.Order 70");
                //root.ExecuteCommand("ComponentBrowser */ SetValue Propagators \"CustomProp\" PropFuncs.Jacchia-Roberts.AtmosDataSource \"Data File\"");
                

                // load external propagators
                IAgScenario scen = root.CurrentScenario as IAgScenario;
                string propagatorsPath = Directory.GetCurrentDirectory() + "\\Propagators\\";
                scen.ComponentDirectory.GetComponents(AgEComponent.eComponentAstrogator).GetFolder("Propagators").LoadComponent(propagatorsPath + "NRLMSISE 2000.propagator");
                scen.ComponentDirectory.GetComponents(AgEComponent.eComponentAstrogator).GetFolder("Propagators").LoadComponent(propagatorsPath + "Jacchia-Roberts.propagator");


            }
            catch { }
        }

        public static PropagationResults PropagateAstrogatorSatellite(AgStkObjectRoot root, AgUiApplication app, InitialState state, TLE tle, Data satData, Uncertainty uncertainty, int runId, int nRuns, string propName)
        {
            PropagationResults propResults = new PropagationResults();
            propResults.RunNumber = runId.ToString().PadLeft(3,'0');
            IAgSatellite sat = root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, tle.GetSatNumber() + "Astrogator") as IAgSatellite;

            //Set the propagator to Astrogator
            sat.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator);
            //get the Driver for the Propagator
            IAgVADriverMCS driver = sat.Propagator as IAgVADriverMCS;
            //Clear all segments from the MCS
            driver.MainSequence.RemoveAll();

            //// Target Sequence ////
            IAgVAMCSTargetSequence ts = driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "SetupState", "-") as IAgVAMCSTargetSequence;
            ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles;

            // add the initial state segment in the target sequence 
            IAgVAMCSInitialState initState = ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeInitialState, "InitialState", "-") as IAgVAMCSInitialState;
            initState.OrbitEpoch = ((IAgScenario)root.CurrentScenario).StartTime;
            // define elements
            initState.SetElementType(AgEVAElementType.eVAElementTypeCartesian);
            IAgVAElementCartesian cart = initState.Element as IAgVAElementCartesian;
            cart.X = Convert.ToDouble(state.CartesianPosX);
            cart.Y = Convert.ToDouble(state.CartesianPosY);
            cart.Z = Convert.ToDouble(state.CartesianPosZ);
            cart.Vx = Convert.ToDouble(state.CartesianVelX);
            cart.Vy = Convert.ToDouble(state.CartesianVelY);
            cart.Vz = Convert.ToDouble(state.CartesianVelZ);

            // spacecraft parameters 
            IAgVASpacecraftParameters spacecraftParameters = (IAgVASpacecraftParameters)initState.SpacecraftParameters;
            spacecraftParameters.DryMass = satData.Mass;
            spacecraftParameters.Cd = satData.Cd;
            spacecraftParameters.DragArea = satData.DragArea;
            spacecraftParameters.Cr = satData.Cr;
            spacecraftParameters.SolarRadiationPressureArea = satData.SunArea;
            spacecraftParameters.RadiationPressureArea = 1e-10;
            IAgVAFuelTank fuelTank = (IAgVAFuelTank)initState.FuelTank;
            fuelTank.FuelMass = 0;

            // enable the control parameter for the state variables
            initState.EnableControlParameter(AgEVAControlInitState.eVAControlInitStateCartesianX);
            initState.EnableControlParameter(AgEVAControlInitState.eVAControlInitStateCartesianY);
            initState.EnableControlParameter(AgEVAControlInitState.eVAControlInitStateCartesianZ);
            initState.EnableControlParameter(AgEVAControlInitState.eVAControlInitStateCartesianVx);
            initState.EnableControlParameter(AgEVAControlInitState.eVAControlInitStateCartesianVy);
            initState.EnableControlParameter(AgEVAControlInitState.eVAControlInitStateCartesianVz);

            // add the results
            ((IAgVAMCSSegment)initState).Results.Add("Relative Motion/InTrack");
            IAgVAStateCalcRelMotion intrackRel = ((IAgVAMCSSegment)initState).Results[0] as IAgVAStateCalcRelMotion;
            intrackRel.ReferenceSelection = AgEVACalcObjectReference.eVACalcObjectReferenceSpecified;
            IAgLinkToObject link_1 = intrackRel.Reference as IAgLinkToObject;
            link_1.BindTo("Satellite/" + tle.GetSatNumber().ToString());

            ((IAgVAMCSSegment)initState).Results.Add("Relative Motion/Radial");
            IAgVAStateCalcRelMotion radialRel = ((IAgVAMCSSegment)initState).Results[1] as IAgVAStateCalcRelMotion;
            radialRel.ReferenceSelection = AgEVACalcObjectReference.eVACalcObjectReferenceSpecified;
            IAgLinkToObject link_2 = radialRel.Reference as IAgLinkToObject;
            link_2.BindTo("Satellite/" + tle.GetSatNumber().ToString());

            ((IAgVAMCSSegment)initState).Results.Add("Relative Motion/CrossTrack");
            IAgVAStateCalcRelMotion crosstrackRel = ((IAgVAMCSSegment)initState).Results[2] as IAgVAStateCalcRelMotion;
            crosstrackRel.ReferenceSelection = AgEVACalcObjectReference.eVACalcObjectReferenceSpecified;
            IAgLinkToObject link_3 = crosstrackRel.Reference as IAgLinkToObject;
            link_3.BindTo("Satellite/" + tle.GetSatNumber().ToString());

            ((IAgVAMCSSegment)initState).Results.Add("Relative Motion/InTrackRate");
            IAgVAStateCalcRelMotion intrackrateRel = ((IAgVAMCSSegment)initState).Results[3] as IAgVAStateCalcRelMotion;
            intrackrateRel.ReferenceSelection = AgEVACalcObjectReference.eVACalcObjectReferenceSpecified;
            IAgLinkToObject link_4 = intrackrateRel.Reference as IAgLinkToObject;
            link_4.BindTo("Satellite/" + tle.GetSatNumber().ToString());

            ((IAgVAMCSSegment)initState).Results.Add("Relative Motion/RadialRate");
            IAgVAStateCalcRelMotion radialrateRel = ((IAgVAMCSSegment)initState).Results[4] as IAgVAStateCalcRelMotion;
            radialrateRel.ReferenceSelection = AgEVACalcObjectReference.eVACalcObjectReferenceSpecified;
            IAgLinkToObject link_5 = radialrateRel.Reference as IAgLinkToObject;
            link_5.BindTo("Satellite/" + tle.GetSatNumber().ToString());

            ((IAgVAMCSSegment)initState).Results.Add("Relative Motion/CrossTrackRate");
            IAgVAStateCalcRelMotion crosstrackrateRel = ((IAgVAMCSSegment)initState).Results[5] as IAgVAStateCalcRelMotion;
            crosstrackrateRel.ReferenceSelection = AgEVACalcObjectReference.eVACalcObjectReferenceSpecified;
            IAgLinkToObject link_6 = crosstrackrateRel.Reference as IAgLinkToObject;
            link_6.BindTo("Satellite/" + tle.GetSatNumber().ToString());

            /// differential corrector setup ///
            IAgVAProfileDifferentialCorrector dc = ts.Profiles["Differential Corrector"] as IAgVAProfileDifferentialCorrector;

            // control parameters
            IAgVADCControl xControlParam = dc.ControlParameters.GetControlByPaths("InitialState", "InitialState.Cartesian.X");
            xControlParam.Enable = true;
            xControlParam.MaxStep = 1;
            xControlParam.Perturbation = 0.1;
            IAgVADCControl yControlParam = dc.ControlParameters.GetControlByPaths("InitialState", "InitialState.Cartesian.Y");
            yControlParam.Enable = true;
            yControlParam.MaxStep = 1;
            yControlParam.Perturbation = 0.1;
            IAgVADCControl zControlParam = dc.ControlParameters.GetControlByPaths("InitialState", "InitialState.Cartesian.Z");
            zControlParam.Enable = true;
            zControlParam.MaxStep = 1;
            zControlParam.Perturbation = 0.1;
            IAgVADCControl vxControlParam = dc.ControlParameters.GetControlByPaths("InitialState", "InitialState.Cartesian.Vx");
            vxControlParam.Enable = true;
            vxControlParam.MaxStep = 0.001;
            vxControlParam.Perturbation = 1e-04;
            IAgVADCControl vyControlParam = dc.ControlParameters.GetControlByPaths("InitialState", "InitialState.Cartesian.Vy");
            vyControlParam.Enable = true;
            vyControlParam.MaxStep = 0.001;
            vyControlParam.Perturbation = 1e-04;
            IAgVADCControl vzControlParam = dc.ControlParameters.GetControlByPaths("InitialState", "InitialState.Cartesian.Vz");
            vzControlParam.Enable = true;
            vzControlParam.MaxStep = 0.001;
            vzControlParam.Perturbation = 1e-04;

            // results
            double[] deviations = uncertainty.GetRandomDeviation();

            IAgVADCResult intrackResult = dc.Results.GetResultByPaths("InitialState", "InTrack");
            intrackResult.Enable = true;
            intrackResult.DesiredValue = deviations[0];
            intrackResult.Tolerance = 0.01;
            IAgVADCResult radialResult = dc.Results.GetResultByPaths("InitialState", "Radial");
            radialResult.Enable = true;
            radialResult.DesiredValue = deviations[1];
            radialResult.Tolerance = 0.01;
            IAgVADCResult crosstrackResult = dc.Results.GetResultByPaths("InitialState", "CrossTrack");
            crosstrackResult.Enable = true;
            crosstrackResult.DesiredValue = deviations[2];
            crosstrackResult.Tolerance = 0.01;
            IAgVADCResult intrackRateResult = dc.Results.GetResultByPaths("InitialState", "InTrackRate");
            intrackRateResult.Enable = true;
            intrackRateResult.DesiredValue = deviations[3] / 1000;
            intrackRateResult.Tolerance = 0.001;
            IAgVADCResult radialRateResult = dc.Results.GetResultByPaths("InitialState", "RadialRate");
            radialRateResult.Enable = true;
            radialRateResult.DesiredValue = deviations[4] / 1000;
            radialRateResult.Tolerance = 0.001;
            IAgVADCResult crosstrackRateResult = dc.Results.GetResultByPaths("InitialState", "CrossTrackRate");
            crosstrackRateResult.Enable = true;
            crosstrackRateResult.DesiredValue = deviations[5] / 1000;
            crosstrackRateResult.Tolerance = 0.001;

            /// Propagator ///
            IAgVAMCSPropagate propagate = driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "ToGround", "-") as IAgVAMCSPropagate;
            ((IAgVAMCSSegment)propagate).Properties.Color = Color.Red;
            //propagate.PropagatorName = "CustomProp";
            propagate.PropagatorName = propName;

            // add an Epoch stopping condition
            IAgVAStoppingConditionCollection propStoppingConditions = propagate.StoppingConditions as IAgVAStoppingConditionCollection;
            IAgVAStoppingConditionElement epochElement = propStoppingConditions.Add("Epoch");
            IAgVAStoppingCondition epoch = (IAgVAStoppingCondition)epochElement.Properties;
            epoch.Trip = ((IAgScenario)root.CurrentScenario).StopTime;
            // add an Altitude stopping condition
            IAgVAStoppingConditionElement altitudeElement = propStoppingConditions.Add("Altitude");
            IAgVAStoppingCondition altitude = (IAgVAStoppingCondition)altitudeElement.Properties;
            altitude.Trip = 0;
            // remove the original stopping condition
            propagate.StoppingConditions.Remove("Duration");

            // run the MCS
            driver.RunMCS();
            driver.ClearDWCGraphics();


            // get the stop time
            IAgDataPrvInterval dp = ((IAgStkObject)sat).DataProviders.GetDataPrvIntervalFromPath("Astrogator MCS Ephemeris Segments") as IAgDataPrvInterval;
            IAgDrResult result = dp.Exec(((IAgScenario)root.CurrentScenario).StartTime, ((IAgScenario)root.CurrentScenario).StopTime);
            string satStopTime = result.DataSets[3].GetValues().GetValue(1).ToString();

            if (satStopTime.Equals((Convert.ToString(((IAgScenario)root.CurrentScenario).StopTime))))
            {
                // the satellite does not decay (it is propagated until the scenario stop time)
                propResults.IsDecayed = false;
            }
            else
            {
                propResults.IsDecayed = true;
                propResults.ImpactEpoch = satStopTime;
                // remove the millisecond part
                satStopTime = satStopTime.Split('.')[0];

                //ask for LLA data at stop time
                IAgDataPrvTimeVar dpInfo = ((IAgStkObject)sat).DataProviders.GetDataPrvInfoFromPath("LLA State//Fixed") as IAgDataPrvTimeVar;
                IAgDrResult resInfo = dpInfo.ExecSingle(satStopTime);
                string lat = resInfo.DataSets[1].GetValues().GetValue(0).ToString();
                string lon = resInfo.DataSets[2].GetValues().GetValue(0).ToString();
                string alt = resInfo.DataSets[3].GetValues().GetValue(0).ToString();
                propResults.ImpactLat = lat;
                propResults.ImpactLon = lon;
                propResults.ImpactAlt = alt;

                // create a target object
                IAgTarget target = root.CurrentScenario.Children.New(AgESTKObjectType.eTarget, "Target" + (runId).ToString().PadLeft(3, '0')) as IAgTarget;
                target.Graphics.Color = Color.Red;
                target.Graphics.LabelVisible = false;
                IAgPosition pos = target.Position;
                pos.AssignGeodetic(lat, lon, 0);

                // create and display the time event
                IAgCrdnProvider provider = root.CurrentScenario.Children["Target" + (runId).ToString().PadLeft(3, '0')].Vgt;
                IAgCrdnEventEpoch eventEpoch = provider.Events.Factory.CreateEventEpoch(runId.ToString().PadLeft(3, '0') + "_Impact", "Impact Epoch") as IAgCrdnEventEpoch;
                eventEpoch.Epoch = satStopTime;
                try { root.ExecuteCommand("Timeline * TimeComponent Add ContentView \"Scenario Availability\" \"Target/Target" + (runId).ToString().PadLeft(3, '0') + " " + runId.ToString().PadLeft(3, '0') + "_Impact Time Instant\""); }
                catch (Exception) { }
                root.ExecuteCommand("Timeline * Refresh");

                // create a unique ephemeris file for each Astrogator run
                DateTime now = DateTime.Now;
                string satEphemerisPath = Directory.GetCurrentDirectory() + "\\Ephemeris\\Reentry_run_" + (runId).ToString().PadLeft(3, '0') + ".e";
                root.ExecuteCommand("ExportDataFile */Satellite/" + tle.GetSatNumber() + "Astrogator Ephemeris \"" + satEphemerisPath + "\" Type STK CoordSys ICRF CentralBody Earth InterpBoundaries Include");
                propResults.EphemerisFilePath = satEphemerisPath;
                // unload Astrogator satellite
                root.CurrentScenario.Children[tle.GetSatNumber() + "Astrogator"].Unload();

                if (runId == nRuns)
                {
                    // remove the TLE sat
                   // root.CurrentScenario.Children[tle.GetSatNumber()].Unload();
                }
            }

            try
            {
                AGI.Ui.Core.IAgUiWindowsCollection windows = app.Windows;
                foreach (dynamic window in app.Windows)
                {
                    string windowCaption = (string)window.Caption;
                    if (windowCaption.Contains("Setup"))
                    {
                        window.Close();
                    }
                }


            }
            catch (Exception ex)
            {
            }

            return propResults;
        }

        public static string[] CalculateStatistics(AgStkObjectRoot root, List<PropagationResults> propResults)
        {
            IAgConversionUtility converter = root.ConversionUtility;
            double[] impactEpochEpsecList = new double[propResults.Count];
            //List<double> impactEpochEpsecList = new List<double>();

            for (int i = 0; i < propResults.Count; i++)
            {
                string impactEpochUtc = propResults[i].ImpactEpoch;
                string epsec = converter.ConvertDate("UTCG", "Epsec", impactEpochUtc);
                impactEpochEpsecList[i] = Convert.ToDouble(epsec);
            }

            // Get the mean
            double mean = impactEpochEpsecList.Sum() / impactEpochEpsecList.Count();
            double sumOfSquares = 0;
            for (int i = 0; i < impactEpochEpsecList.Count(); i++)
            {
                sumOfSquares += (impactEpochEpsecList[1] - mean) * (impactEpochEpsecList[1] - mean);
            }


            // get the standard deviation (in hours)
            double stdDev =  Math.Sqrt(sumOfSquares / impactEpochEpsecList.Count()) / 60;

            // get the UTC epoch back
            string meanUtcg = converter.ConvertDate("EpSec", "UTCG", Convert.ToString(mean));

            // returns the information
            string[] results = new string[2];

            results[0] = meanUtcg;
            results[1] = stdDev.ToString("0.00");

            return results;
        }

        public static void CreateMto(AgStkObjectRoot root, List<PropagationResults> propResults)
        {
            try
            {
                IAgMto mto = root.CurrentScenario.Children.New(AgESTKObjectType.eMTO, "MTO") as IAgMto;
                IAgMtoTrackCollection trackCollection = mto.Tracks as IAgMtoTrackCollection;
                for (int i = 0; i < propResults.Count; i++)
                {
                    trackCollection.Add(i + 1);
                    IAgMtoTrack track = trackCollection[i];
                    track.Points.LoadPoints(propResults[i].EphemerisFilePath);
                    root.ExecuteCommand("Track */MTO/MTO Interpolate " + (i + 1).ToString() + " On");
                    root.ExecuteCommand("Track2d */MTO/MTO MarkerStyle " + (i + 1).ToString() + " Circle");

                    // show 2d marker
                    root.ExecuteCommand("Track2d */MTO/MTO ShowMarker " + (i + 1).ToString() + " On");
                    root.ExecuteCommand("Track2d */MTO/MTO Color " + (i + 1).ToString() + " Red");
                    root.ExecuteCommand("Track2d */MTO/MTO LineWidth " + (i + 1).ToString() + " 3");
                    mto.Graphics.Tracks[i].LeadTrailTimes.UseLeadTrail = true;
                    mto.Graphics.Tracks[i].LeadTrailTimes.LeadTime = 0;
                    mto.Graphics.Tracks[i].LeadTrailTimes.TrailTime = 300;
                    mto.Graphics.Tracks[i].FadeTimes.UsePostFade = false;
                    mto.Graphics.Tracks[i].FadeTimes.UsePreFade = true;
                    mto.Graphics.Tracks[i].FadeTimes.PreFadeTime = 200;
                    // don't show 2d track
                    //root.ExecuteCommand("Track2d */MTO/MTO ShowLine " + (i + 1).ToString() + " Off");
                    root.ExecuteCommand("Track3d */MTO/MTO TranslucentTrackTrail " + (i + 1).ToString() + " On");
                    root.ExecuteCommand("Track3d */MTO/MTO Point " + (i + 1).ToString() + " Show On Size 15");
                    mto.VO.Tracks[i].Point.Size = 10;
                    mto.VO.Tracks[i].Marker.PixelSize = 15;
                    //root.ExecuteCommand("VO */MTO/MTO Marker " + (i + 1).ToString() + " Size 12");

                }
            }
            catch (Exception)
            {
                throw new Exception();
            }         
        }

        public static void CreateCoverage(AgStkObjectRoot root, TLE tle)
        {
            try
            {
                IAgCoverageDefinition covDef = root.CurrentScenario.Children.New(AgESTKObjectType.eCoverageDefinition, "CovDef") as IAgCoverageDefinition;

                // add all target objects as assets
                IAgStkObjectElementCollection allChildrenOfType = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eTarget);
                IAgCvAssetListCollection assetCollection = covDef.AssetList;
                foreach (IAgStkObject o in allChildrenOfType)
                {
                    string satAssetName = "Target/" + o.InstanceName;
                    assetCollection.Add(satAssetName);
                }

                covDef.Graphics.Static.IsPointsVisible = false;

                // set the grid resolution
                IAgCvGrid cvGrid = covDef.Grid;
                cvGrid.ResolutionType = AgECvResolution.eResolutionLatLon;
                IAgCvResolutionLatLon res = cvGrid.Resolution as IAgCvResolutionLatLon;
                res.LatLon = 1;

                // set the points altitude
                IAgCvPointDefinition pointDefinition = covDef.PointDefinition;
                pointDefinition.AltitudeMethod = AgECvAltitudeMethod.eAltitude;
                pointDefinition.Altitude = 20.0;
                covDef.PointDefinition.GroundAltitudeMethod = AgECvGroundAltitudeMethod.eCvGroundAltitudeMethodUsePointAlt;

                // set the start/stop time
                DateTime startDate = tle.GetTleEpoch();
                DateTime stopDate = startDate.AddMilliseconds(1);
                covDef.Interval.AnalysisInterval.SetExplicitInterval(startDate.ToString("dd MMM yyyy hh:mm:ss.fff"), stopDate.ToString("dd MMM yyyy hh:mm:ss.fff"));

                covDef.Advanced.AutoRecompute = false;
                covDef.ComputeAccesses();


                IAgFigureOfMerit fom = root.CurrentScenario.Children["CovDef"].Children.New(AgESTKObjectType.eFigureOfMerit, "Likelihood") as IAgFigureOfMerit;
                fom.SetDefinitionType(AgEFmDefinitionType.eFmNAssetCoverage);
                fom.Graphics.Animation.IsVisible = false;
                fom.Graphics.Static.FillTranslucency = 20;
                IAgFmGfxContours contours = fom.Graphics.Static.Contours;
                contours.IsVisible = true;
                contours.ContourType = AgEFmGfxContourType.eSmoothFill;
                contours.ColorMethod = AgEFmGfxColorMethod.eExplicit;
                contours.LevelAttributes.RemoveAll();

                IAgFmGfxLevelAttributesElement level1 = contours.LevelAttributes.AddLevel(1);
                IAgFmGfxLevelAttributesElement level2 = contours.LevelAttributes.AddLevel(2);
                IAgFmGfxLevelAttributesElement level3 = contours.LevelAttributes.AddLevel(3);
                IAgFmGfxLevelAttributesElement level4 = contours.LevelAttributes.AddLevel(4);

                level1.Color = Color.Khaki;
                level2.Color = Color.Gold;
                level3.Color = Color.Orange;
                level4.Color = Color.Red;

                root.ExecuteCommand("Graphics */CoverageDefinition/CovDef/FigureOfMerit/Likelihood SmoothFillParameters NNSampleMode Smooth");
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public static DateTime GetDateTimeFromStkTimeFormat(string stkTime)
        {
            //parse the STK data into Integer variables
            String[] epochOfYear = stkTime.Split(' ');
            int day = Convert.ToInt32(epochOfYear[0]);
            String monthString = epochOfYear[1];
            int month = 0;
            if ("Jan".Equals(monthString))
                month = 0;
            if ("Feb".Equals(monthString))
                month = 1;
            if ("Mar".Equals(monthString))
                month = 2;
            if ("Apr".Equals(monthString))
                month = 3;
            if ("May".Equals(monthString))
                month = 4;
            if ("Jun".Equals(monthString))
                month = 5;
            if ("Jul".Equals(monthString))
                month = 6;
            if ("Aug".Equals(monthString))
                month = 7;
            if ("Sep".Equals(monthString))
                month = 8;
            if ("Oct".Equals(monthString))
                month = 9;
            if ("Nov".Equals(monthString))
                month = 10;
            if ("Dec".Equals(monthString))
                month = 11;
            int year = Convert.ToInt32(epochOfYear[2]);

            String[] epochOfDay = epochOfYear[3].Split(':');
            int hour = Convert.ToInt32(epochOfDay[0]);
            int minute = Convert.ToInt32(epochOfDay[1]);

            int seconds = Convert.ToInt32(epochOfDay[2].Substring(0, 2));
            int millisecond = Convert.ToInt32(epochOfDay[2].Split('.')[1]);
            // end of parsing

            DateTime date = new DateTime(year, month, day, hour, minute, seconds);
            //GregorianCalendar cal = new GregorianCalendar(year, month, day, hour, minute, seconds);
            date.AddMilliseconds(millisecond);

            return date;
        }
    }
}
