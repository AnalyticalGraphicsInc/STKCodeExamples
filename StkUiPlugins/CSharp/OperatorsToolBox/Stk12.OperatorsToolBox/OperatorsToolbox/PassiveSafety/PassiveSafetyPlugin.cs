using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKObjects.Astrogator;
using AGI.STKUtil;

namespace OperatorsToolbox.PassiveSafety
{
    public partial class PassiveSafetyPlugin : OpsPluginControl
    {
        public PassiveSafetyPlugin()
        {
            InitializeComponent();
            PopulateBoxes();
            CommonData.RunList = new List<PassiveRun>();
            CommonData.BeenRun = false;
        }

        private void Visualize_Click(object sender, EventArgs e)
        {
            if (CommonData.BeenRun)
            {
                PassiveRunOutput form = new PassiveRunOutput();
                form.Show();

            }
        }
        //Main Function
        private void Generate_Click(object sender, EventArgs e)
        {
            int check = FieldCheck();
            if (check == 0)
            {
                RemoveProximityGeometry();
                try
                {
                    CommonData.StkRoot.UnitPreferences.SetCurrentUnit("Distance", "km");
                    CommonData.RunList.Clear();
                    CommonData.TargetName = TargetSat.Text;
                    CommonData.ActorName = ActorSat.Text;
                    //Set user bounds for safety
                    double userMinRange = Double.Parse(SphericalMag.Text) / 1000;
                    double userMinR = Double.Parse(RMag.Text) / 1000;
                    double userMinI = Double.Parse(IMag.Text) / 1000;
                    double userMinC = Double.Parse(CMag.Text) / 1000;

                    IAgScenario scenario = CommonData.StkRoot.CurrentScenario as IAgScenario;
                    IAgStkObject satObj = CommonData.StkRoot.GetObjectFromPath("Satellite/" + ActorSat.Text);
                    IAgSatellite sat = satObj as IAgSatellite;

                    //Get all maneuver end times for actor satellite
                    IAgDataProviderGroup maneuverDpGroup = satObj.DataProviders["Astrogator Maneuver Ephemeris Block Final"] as IAgDataProviderGroup;
                    IAgDataPrvTimeVar maneuverDp = maneuverDpGroup.Group["Cartesian Elems"] as IAgDataPrvTimeVar;
                    IAgDrResult result = maneuverDp.Exec(scenario.StartTime, scenario.StopTime, 60);
                    IAgDrDataSetCollection maneuverData = result.DataSets;



                    //If there is maneuvers, run iterations for each maneuver. If no maneuvers then just pull closest RIC data for entire trajectory 
                    if (maneuverData.Count != 0)
                    {
                        CommonData.HasManeuvers = true;

                        //Get maneuver numbers
                        IAgDataPrvInterval summaryDp = satObj.DataProviders["Maneuver Summary"] as IAgDataPrvInterval;
                        IAgDrResult summaryResult = summaryDp.Exec(scenario.StartTime, scenario.StopTime);
                        Array maneuverNumbers = summaryResult.DataSets.GetDataSetByName("Maneuver Number").GetValues();
                        int maxManeuverNum = maneuverNumbers.Length;

                        //Get handles to cartesian position and velocity to seed passive safety runs
                        IAgDataProviderGroup cartPos = satObj.DataProviders["Cartesian Position"] as IAgDataProviderGroup;
                        IAgDataPrvTimeVar cartPosDP = cartPos.Group["ICRF"] as IAgDataPrvTimeVar;

                        IAgDataProviderGroup cartVel = satObj.DataProviders["Cartesian Velocity"] as IAgDataProviderGroup;
                        IAgDataPrvTimeVar cartVelDP = cartVel.Group["ICRF"] as IAgDataPrvTimeVar;

                        //Create passive safety satellite. Set to Astrogator and pull handles to initial state and propagate segments
                        IAgStkObject passiveSatObj = CreatorFunctions.GetCreateSatellite("PassiveCheck");
                        IAgSatellite passiveSat = passiveSatObj as IAgSatellite;
                        passiveSat.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator);
                        IAgVADriverMCS passiveDriver = passiveSat.Propagator as IAgVADriverMCS;

                        IAgVAMCSInitialState intState = passiveDriver.MainSequence[0] as IAgVAMCSInitialState;
                        IAgVAMCSPropagate prop = passiveDriver.MainSequence[1] as IAgVAMCSPropagate;
                        IAgVAStoppingConditionElement sc1 = prop.StoppingConditions[0];
                        IAgVAStoppingCondition sc = sc1.Properties as IAgVAStoppingCondition;
                        sc.Trip = PropTime.Text;

                        AgVAElementCartesian element = intState.Element as AgVAElementCartesian;
                        Array epoch;
                        Array vx;
                        Array vy;
                        Array vz;
                        Array x;
                        Array y;
                        Array z;
                        String epochCur;
                        DateTime dateCur;
                        //Assign cartesian elements to PassiveCheck satellite from actor maneuver maneuver data. Run each iteration to see if resulting trajectory violates constraints
                        for (int i = 0; i < maxManeuverNum; i ++)
                        {
                            //Get maneuver time and offset in time by 0.25 sec to account for boundrary conditions around impulsive maneuvers
                            epoch = maneuverData[0 + (i * 7)].GetValues();
                            epochCur = epoch.GetValue(0).ToString();
                            dateCur = DateTime.Parse(epochCur);
                            dateCur = dateCur.AddMilliseconds(250);
                            //dateCur = DateTime.ParseExact(epochCur, "dd MMM yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                            epochCur = dateCur.ToString("dd MMM yyyy HH:mm:ss.fff");

                            //Get cartesian state vector for given time
                            result = cartPosDP.ExecSingle(epochCur);
                            x = result.DataSets.GetDataSetByName("x").GetValues();
                            y = result.DataSets.GetDataSetByName("y").GetValues();
                            z = result.DataSets.GetDataSetByName("z").GetValues();

                            result = cartVelDP.ExecSingle(epochCur);
                            vx = result.DataSets.GetDataSetByName("x").GetValues();
                            vy = result.DataSets.GetDataSetByName("y").GetValues();
                            vz = result.DataSets.GetDataSetByName("z").GetValues();

                            //Create passive run output to be used in visualization
                            PassiveRun run = new PassiveRun();
                            run.UserMinRange = Double.Parse(SphericalMag.Text) / 1000;
                            run.UserMinR = Double.Parse(RMag.Text) / 1000;
                            run.UserMinI = Double.Parse(IMag.Text) / 1000;
                            run.UserMinC = Double.Parse(CMag.Text) / 1000;
                            intState.OrbitEpoch = epochCur;
                            element.Vx = Double.Parse(vx.GetValue(0).ToString());
                            element.Vy = Double.Parse(vy.GetValue(0).ToString());
                            element.Vz = Double.Parse(vz.GetValue(0).ToString());
                            element.X = Double.Parse(x.GetValue(0).ToString());
                            element.Y = Double.Parse(y.GetValue(0).ToString());
                            element.Z = Double.Parse(z.GetValue(0).ToString());

                            passiveDriver.RunMCS();

                            run.Vx = Double.Parse(vx.GetValue(0).ToString());
                            run.Vy = Double.Parse(vy.GetValue(0).ToString());
                            run.Vz = Double.Parse(vz.GetValue(0).ToString());
                            run.X = Double.Parse(x.GetValue(0).ToString());
                            run.Y = Double.Parse(y.GetValue(0).ToString());
                            run.Z = Double.Parse(z.GetValue(0).ToString());
                            run.PropTime = Double.Parse(PropTime.Text);

                            //Pull closest point to target for each iteration and save to passive run output
                            IAgDataProvider psatDp = passiveSatObj.DataProviders["RIC Coordinates"] as IAgDataProvider;
                            psatDp.PreData = "Satellite/" + TargetSat.Text;
                            IAgDataPrvTimeVar psatDpTimeVar = psatDp as IAgDataPrvTimeVar;
                            IAgDrResult psatDp2 = psatDpTimeVar.Exec(scenario.StartTime, scenario.StopTime, Double.Parse(TimeStep.Text));
                            run.Range = psatDp2.DataSets.GetDataSetByName("Range").GetValues();
                            run.Intrack = psatDp2.DataSets.GetDataSetByName("In-Track").GetValues();
                            run.Crosstrack = psatDp2.DataSets.GetDataSetByName("Cross-Track").GetValues();
                            run.Radial = psatDp2.DataSets.GetDataSetByName("Radial").GetValues();
                            run.MinRange = MathFunctions.ArrayMin(run.Range);
                            run.MinIntrack = MathFunctions.ArrayMinAbs(run.Intrack);
                            run.MinCrosstrack = MathFunctions.ArrayMinAbs(run.Crosstrack);
                            run.MinRadial = MathFunctions.ArrayMinAbs(run.Radial);
                            //run.ManeuverTime = epoch.GetValue(0).ToString();
                            run.ManeuverTime = epochCur;

                            //spherical
                            if (radioButton1.Checked)
                            {
                                run.IsSpherical = true;
                                if (run.MinRange < userMinRange)
                                {
                                    run.Safe = false;
                                }
                                else
                                {
                                    run.Safe = true;
                                }

                            }
                            //independent axis
                            else
                            {
                                run.IsSpherical = false;
                                if (Math.Abs(run.MinIntrack) < userMinI && Math.Abs(run.MinRadial) < userMinR && Math.Abs(run.MinCrosstrack) < userMinC)
                                {
                                    bool tripped = false;
                                    for (int j = 0; j < run.Range.Length; j++)
                                    {
                                        if (Math.Abs(Double.Parse(run.Intrack.GetValue(j).ToString())) < userMinI && Math.Abs(Double.Parse(run.Radial.GetValue(j).ToString())) < userMinR && Math.Abs(Double.Parse(run.Crosstrack.GetValue(j).ToString())) < userMinC)
                                        {
                                            run.Safe = false;
                                            tripped = true;
                                            break;
                                        }
                                    }
                                    if (!tripped)
                                    {
                                        run.Safe = true;
                                    }
                                }
                                else
                                {
                                    run.Safe = true;
                                }
                            }

                            CommonData.RunList.Add(run);
                        }
                    }
                    else
                    {
                        CommonData.HasManeuvers = false;
                        PassiveRun run = new PassiveRun();
                        IAgDataProvider satDp = satObj.DataProviders["RIC Coordinates"] as IAgDataProvider;
                        satDp.PreData = "Satellite/" + TargetSat.Text;
                        IAgDataPrvTimeVar satDpTimeVar = satDp as IAgDataPrvTimeVar;
                        IAgDrResult satDp2 = satDpTimeVar.Exec(scenario.StartTime, scenario.StopTime, Double.Parse(TimeStep.Text));
                        run.Range = satDp2.DataSets.GetDataSetByName("Range").GetValues();
                        run.Intrack = satDp2.DataSets.GetDataSetByName("In-Track").GetValues();
                        run.Crosstrack = satDp2.DataSets.GetDataSetByName("Cross-Track").GetValues();
                        run.Radial = satDp2.DataSets.GetDataSetByName("Radial").GetValues();
                        run.MinRange = MathFunctions.ArrayMin(run.Range);
                        run.MinIntrack = MathFunctions.ArrayMinAbs(run.Intrack);
                        run.MinCrosstrack = MathFunctions.ArrayMinAbs(run.Crosstrack);
                        run.MinRadial = MathFunctions.ArrayMinAbs(run.Radial);
                        run.ManeuverTime = "N/A";

                        //spherical
                        if (radioButton1.Checked)
                        {
                            run.IsSpherical = true;
                            if (run.MinRange < userMinRange)
                            {
                                run.Safe = false;
                            }
                            else
                            {
                                run.Safe = true;
                            }

                        }
                        //independent axis
                        else
                        {
                            run.IsSpherical = false;
                            if (Math.Abs(run.MinIntrack) < userMinI && Math.Abs(run.MinRadial) < userMinR && Math.Abs(run.MinCrosstrack) < userMinC)
                            {
                                bool tripped = false;
                                for (int j = 0; j < run.Range.Length; j++)
                                {
                                    if (Math.Abs(Double.Parse(run.Intrack.GetValue(j).ToString())) < userMinI && Math.Abs(Double.Parse(run.Radial.GetValue(j).ToString())) < userMinR && Math.Abs(Double.Parse(run.Crosstrack.GetValue(j).ToString())) < userMinC)
                                    {
                                        run.Safe = false;
                                        tripped = true;
                                        break;
                                    }
                                }
                                if (!tripped)
                                {
                                    run.Safe = true;
                                }
                            }
                            else
                            {
                                run.Safe = true;
                            }
                        }

                        CommonData.RunList.Add(run);
                    }
                    CommonData.BeenRun = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Passive Safety Check Failed");
                }
            }
        }

        private void TargetSat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ActorSat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
                SphericalMag.Enabled = true;
                RMag.Enabled = false;
                IMag.Enabled = false;
                CMag.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
                SphericalMag.Enabled = false;
                RMag.Enabled = true;
                IMag.Enabled = true;
                CMag.Enabled = true;
            }
        }

        private int FieldCheck()
        {
            int check = 0;
            double temp;
            if (TargetSat.SelectedIndex == -1 || TargetSat.SelectedItem == null)
            {
                MessageBox.Show("No valid Target satellite selection");
                check = 1;
            }

            if (ActorSat.SelectedIndex == -1 || ActorSat.SelectedItem == null)
            {
                MessageBox.Show("No valid Actor satellite selection");
                check = 1;
            }

            if (TargetSat.Text == ActorSat.Text)
            {
                MessageBox.Show("Target and Actor satellite must be different objects");
                check = 1;
            }

            bool isNumerical = Double.TryParse(SphericalMag.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Spherical Magnitude not a valid number");
                check = 1;
            }
            else
            {
                if (temp < 0)
                {
                    MessageBox.Show("Spherical Magnitude cannot be less than 0");
                    check = 1;
                }
            }
            isNumerical = Double.TryParse(RMag.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Radial Magnitude not a valid number");
                check = 1;
            }
            else
            {
                if (temp < 0)
                {
                    MessageBox.Show("Radial Magnitude cannot be less than 0");
                    check = 1;
                }
            }

            isNumerical = Double.TryParse(IMag.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Intrack Magnitude not a valid number");
                check = 1;
            }
            else
            {
                if (temp < 0)
                {
                    MessageBox.Show("Intrack Magnitude cannot be less than 0");
                    check = 1;
                }
            }

            isNumerical = Double.TryParse(CMag.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Crosstrack Magnitude not a valid number");
                check = 1;
            }
            else
            {
                if (temp < 0)
                {
                    MessageBox.Show("Crosstrack Magnitude cannot be less than 0");
                    check = 1;
                }
            }

            isNumerical = Double.TryParse(PropTime.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Propagation time not a valid number");
                check = 1;
            }
            else
            {
                if (temp < 0)
                {
                    MessageBox.Show("Propagation time cannot be less than 0");
                    check = 1;
                }
            }

            isNumerical = Double.TryParse(TimeStep.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Time step not a valid number");
                check = 1;
            }
            else
            {
                if (temp < 0)
                {
                    MessageBox.Show("Time step cannot be less than 0");
                    check = 1;
                }
            }
            return check;
        }

        private void PopulateBoxes()
        {
            RMag.Text = "100";
            IMag.Text = "2000";
            CMag.Text = "1000";
            SphericalMag.Text = "1000";
            PropTime.Text = "86400";
            TimeStep.Text = "60";
            radioButton2.Checked = false;
            SphericalMag.Enabled = true;
            RMag.Enabled = false;
            IMag.Enabled = false;
            CMag.Enabled = false;
            //TimeUnit.Items.Add("sec");
            //TimeUnit.Items.Add("min");
            //TimeUnit.Items.Add("hr");
            //TimeUnit.SelectedIndex = 0;

            radioButton1.Checked = true;

            IAgExecCmdResult result;
            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Satellite");
            if (result[0] != "None")
            {
                string[] satArray = result[0].Split(null);
                foreach (var item in satArray)
                {
                    string newItem = item.Split('/').Last();
                    if (newItem != "" && newItem != null)
                    {
                        TargetSat.Items.Add(newItem);
                        ActorSat.Items.Add(newItem);
                    }
                }
                try
                {
                    TargetSat.SelectedIndex = 0;
                    ActorSat.SelectedIndex = 0;
                }
                catch (Exception)
                {

                }
            }

        }

        private void RemoveProximityGeometry()
        {
            IAgExecCmdResult result;
            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Satellite");
            if (result[0] != "None")
            {
                string[] satArray = result[0].Split(null);
                foreach (var item in satArray)
                {
                    string newItem = item.Split('/').Last();
                    if (newItem != "" && newItem != null)
                    {
                        try
                        {
                            IAgSatellite sat = CommonData.StkRoot.GetObjectFromPath("Satellite/" + newItem) as IAgSatellite;
                            sat.VO.Proximity.Ellipsoid.IsVisible = false;
                            sat.VO.Proximity.ControlBox.IsVisible = false;
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }
    }
}
