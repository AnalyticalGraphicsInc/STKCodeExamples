using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Threading;
using AGI.STKUtil;
using AGI.Ui.Core;
using AGI.STKVgt;


namespace OrbitTunerUiPlugin
{



    public partial class OrbitTunerUserPage : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AgStkObjectRoot m_root;
        private CSharpPlugin m_uiPlugin;
        private IAgSatellite m_satellite;
        private bool m_init = true;
        
        
        public OrbitTunerUserPage()
        {
            InitializeComponent();
        }
        
        
        private IAgStkObject SatelliteStkObject
        {
            get { return m_satellite as IAgStkObject;  }
        }

        private AgSatellite SatelliteAgSatellite
        {
            get { return m_satellite as AgSatellite; }
        }
        
        private void InitGUI()
        {

            IAgStkObjectElementCollection allSats = m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eSatellite);

            int num_Sats = allSats.Count;

            if (num_Sats == 0)
            {
                // NO SATS IN SCENARIO
                STKHelper.CreateSat( m_satellite);
            }

            else
            {
                IAgSatellite temp = null;
                foreach (IAgStkObject obj in allSats)
                {
                    IAgSatellite currentSat = obj as IAgSatellite;
                    if (currentSat.PropagatorType == AgEVePropagatorType.ePropagatorJ2Perturbation)
                    {
                        temp = currentSat as IAgSatellite;
                    }
                }

                if (temp == null)
                {
                    STKHelper.CreateSat( m_satellite);
                }
                else
                {
                    m_satellite = temp;
                }
            }

            UpdateSelectSatComboBox();
            selectSatelliteComboBox_SelectedIndexChanged(null, null);

        }

        public void ChangeNameSatComboBox(string oldName, string newName)
        {
            for(int i = 0; i < selectSatelliteComboBox.Items.Count; ++i)
            {
                if(selectSatelliteComboBox.Items[i].ToString() == oldName)
                {
                    selectSatelliteComboBox.Items[i] = newName;
                    
                    
                    if(SatelliteStkObject.InstanceName == oldName)
                    {
                        m_satellite = m_root.GetObjectFromPath("Satellite/" + newName) as IAgSatellite;
                    }
                }
            }
                
        }

        public void UpdateSatAfterDelete()
        {
            if(m_root.CurrentScenario == null)
            {
                return;
            }
            if(m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eSatellite).Count == 0)
            {
                STKHelper.CreateSat( m_satellite);
            }


            UpdateSelectSatComboBox();
        }
        
        public void UpdateSatAfterAdd(string name)
        {
            selectSatelliteComboBox.Items.Add(name);

        }

        public void UpdateSelectSatComboBox()
        {           
            // load existing satellites into pull-down and create new one if there are no satellites
            selectSatelliteComboBox.Items.Clear();
            foreach (IAgStkObject thisSat in m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eSatellite))
            {
                IAgSatellite temp = thisSat as IAgSatellite;
                if (temp.PropagatorType == AgEVePropagatorType.ePropagatorJ2Perturbation)
                {
                    selectSatelliteComboBox.Items.Add(thisSat.InstanceName);
                }
            }

            if(selectSatelliteComboBox.Items.Count == 0 && m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eSatellite).Count == 0)
            {
                STKHelper.CreateSat( m_satellite);
            }

            if (!IsMSatInList() && selectSatelliteComboBox.Items.Count != 0)
            {
                m_satellite = m_root.GetObjectFromPath("Satellite/" + selectSatelliteComboBox.Items[0].ToString()) as IAgSatellite;
            }

            if(selectSatelliteComboBox.Items.Count != 0)
            {
                
                int index = getSatComboIndex(SatelliteStkObject.InstanceName);
                if(index != -1)
                {
                    this.selectSatelliteComboBox.SelectedIndexChanged -= new System.EventHandler(this.selectSatelliteComboBox_SelectedIndexChanged);
                    selectSatelliteComboBox.SelectedIndex = index;
                    selectSatelliteComboBox.Text = selectSatelliteComboBox.Items[index].ToString();
                    this.selectSatelliteComboBox.SelectedIndexChanged += new System.EventHandler(this.selectSatelliteComboBox_SelectedIndexChanged);

                }
                
            }
           
            
        }
        private int getSatComboIndex(string name)
        {
            for(int i = 0; i < selectSatelliteComboBox.Items.Count; ++i)
            {
                if(selectSatelliteComboBox.Items[i].ToString() == name)
                {
                    return i;
                }
            }
            return -1;
        }
        private bool IsMSatInList()
        {
            try
            {
                bool isInList = false;
                for (int i = 0; i < selectSatelliteComboBox.Items.Count; ++i)
                {
                    if (SatelliteStkObject.InstanceName == selectSatelliteComboBox.Items[i].ToString())
                    {
                        isInList = true;
                    }
                }

                return isInList;
            }
            catch
            { // occur if m_satellite has been deleted
                return false;
            }
        }

        // update sliders to current value when new satellite is selected
        public void selectSatelliteComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            // get selected satellite orbital elements

            IAgStkObject old_m_satellite = SatelliteStkObject;

            HideVectors(SatelliteAgSatellite);

            IAgStkObjectElementCollection allSats = m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eSatellite);
            for (int i = 0; i < allSats.Count; ++i)
            {
                if (allSats[i].InstanceName == selectSatelliteComboBox.SelectedItem.ToString())
                {
                    m_satellite = allSats[i] as IAgSatellite;
                }
            }

           
            if(SatelliteStkObject.InstanceName != old_m_satellite.InstanceName)
            {
                HideVectors(old_m_satellite as AgSatellite);
            }

           

            IAgDrResult dpResults = SatelliteStkObject.DataProviders.GetDataPrvTimeVarFromPath("Classical Elements//ICRF").ExecSingle(SatelliteStkObject.ObjectCoverage.StartTime);

            coordTypeComboBox.SelectedItem = coordTypeComboBox.Items[1];

            // initiallize sliders
            orbElLabel1.Text = "a";
            unitLabel1.Text = "km";
            trackBar1.Minimum = 6380;
            trackBar1.Maximum = 50000;
            trackBar1.Value = Convert.ToInt32(dpResults.DataSets.GetDataSetByName("Semi-major Axis").GetValues().GetValue(0));
            valueTextBox1.Text = trackBar1.Value.ToString();
            checkBox_orbElt1.Visible = true;

            orbElLabel2.Text = "e";
            unitLabel2.Text = "";
            trackBar2.Minimum = 0;
            trackBar2.Maximum = 999;
            trackBar2.Value = Convert.ToInt32(Convert.ToDouble(dpResults.DataSets.GetDataSetByName("Eccentricity").GetValues().GetValue(0)) * 1000.0);
            double val = Convert.ToDouble(trackBar2.Value) / 1000;
            valueTextBox2.Text = val.ToString();
            checkBox_orbElt2.Visible = true;

            orbElLabel3.Text = "i";
            unitLabel3.Text = "deg";
            trackBar3.Minimum = 0;
            trackBar3.Maximum = 180;
            trackBar3.Value = Convert.ToInt32(dpResults.DataSets.GetDataSetByName("Inclination").GetValues().GetValue(0)); ;
            valueTextBox3.Text = trackBar3.Value.ToString();
            checkBox_orbElt3.Visible = true;

            orbElLabel4.Text = "AoP";
            unitLabel4.Text = "deg";
            trackBar4.Minimum = 0;
            trackBar4.Maximum = 360;
            trackBar4.Value = Convert.ToInt32(dpResults.DataSets.GetDataSetByName("Arg of Perigee").GetValues().GetValue(0));
            valueTextBox4.Text = trackBar4.Value.ToString();
            checkBox_orbElt4.Visible = true;

            orbElLabel5.Text = "RAAN";
            unitLabel5.Text = "deg";
            trackBar5.Minimum = 0;
            trackBar5.Maximum = 360;
            trackBar5.Value = Convert.ToInt32(dpResults.DataSets.GetDataSetByName("RAAN").GetValues().GetValue(0)); ;
            valueTextBox5.Text = trackBar5.Value.ToString();
            checkBox_orbElt5.Visible = true;

            orbElLabel6.Text = "TA";
            unitLabel6.Text = "deg";
            trackBar6.Minimum = 0;
            trackBar6.Maximum = 360;
            trackBar6.Value = Convert.ToInt32(dpResults.DataSets.GetDataSetByName("True Anomaly").GetValues().GetValue(0)); ;
            valueTextBox6.Text = trackBar6.Value.ToString();
            checkBox_orbElt6.Visible = true;

            orbElLabel7.Visible = false;
            equinoctialComboBox.Visible = false;

            m_init = false;
            
            UpdateSTKHelper();
        }

       
        #region IAgUiPluginEmbeddedControl Members

        public stdole.IPictureDisp GetIcon()
        {
            return null;
        }

        public void OnClosing()
        {
            m_uiPlugin.customUI = null;

            return;
        }

        public void OnSaveModified()
        {
            return;
        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as CSharpPlugin;
            m_root = m_uiPlugin.STKRoot;

            m_uiPlugin.customUI = this;

            
          
            InitGUI();
        }

        #endregion


        

        private void SetVal(int value, System.Windows.Forms.TrackBar trackBar)
        {
            if(value >= trackBar.Maximum)
            {
                trackBar.Value = trackBar.Maximum;
            }
            else if(value <= trackBar.Minimum)
            {
                trackBar.Value = trackBar.Minimum;
            }
            trackBar.Value = value;
            
        }

        private void HideVectors(AgSatellite sat)
        {
            string coordType = "Classical";

            if (checkBox_orbElt1.Checked)
            {
                checkBox_orbElt1.Checked = false;
                STKHelper.HidePeriapsisApoapsis(sat);
            }

            if (checkBox_orbElt2.Checked)
            {
                checkBox_orbElt2.Checked = false;
                STKHelper.HideEcc(sat);
            }

            if (checkBox_orbElt3.Checked)
            {
                checkBox_orbElt3.Checked = false;
                STKHelper.HideInclination(sat);
;            }

            if (checkBox_orbElt4.Checked)
            {
                checkBox_orbElt4.Checked = false;
                STKHelper.HideAoP(sat);
            }

            if (checkBox_orbElt5.Checked)
            {
                checkBox_orbElt5.Checked = false;
                STKHelper.HideRAAN(sat);
            }

            if (checkBox_orbElt6.Checked)
            {
                checkBox_orbElt6.Checked = false;
                STKHelper.HideTA(sat);
            }
        }

        private void ChangeCheckBoxVisible(bool show)
        {
            label1.Visible = show;
            checkBox_orbElt1.Visible = show;
            checkBox_orbElt2.Visible = show;
            checkBox_orbElt3.Visible = show;
            checkBox_orbElt4.Visible = show;
            checkBox_orbElt5.Visible = show;
            checkBox_orbElt6.Visible = show;

            if (!show)
            {
                HideVectors(SatelliteAgSatellite);
            }
            
        }

        // update GUI when coordinate type changed
        private void coordTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputVal_errorLabel.Visible = false;
            m_init = true;

            IAgScenario sc = m_root.CurrentScenario as IAgScenario;
            IAgStkObject thisSat = SatelliteStkObject;

            // set up GUI
            switch (coordTypeComboBox.SelectedItem.ToString())
            {
                case "Cartesian ICRF":
                    


                    // get current state
                    IAgDrResult posResult = SatelliteStkObject.DataProviders.GetDataPrvTimeVarFromPath("Cartesian Position//ICRF").ExecSingle(sc.StartTime);
                    IAgDrResult velResult = SatelliteStkObject.DataProviders.GetDataPrvTimeVarFromPath("Cartesian Velocity//ICRF").ExecSingle(sc.StartTime);

                    // update GUI

                    ChangeCheckBoxVisible(false);
                    

                    orbElLabel1.Text = "x";
                    unitLabel1.Text = "km";
                    trackBar1.Minimum = -50000;
                    trackBar1.Maximum = 50000;
                    int val1 = Convert.ToInt32(posResult.DataSets[1].GetValues().GetValue(0));
                    SetVal(val1, trackBar1);
                    valueTextBox1.Text = Convert.ToString(trackBar1.Value);

                    orbElLabel2.Text = "y";
                    unitLabel2.Text = "km";
                    trackBar2.Minimum = -50000;
                    trackBar2.Maximum = 50000;
                    int val2 = Convert.ToInt32(posResult.DataSets[2].GetValues().GetValue(0));
                    SetVal(val2, trackBar2);
                    valueTextBox2.Text = posResult.DataSets[2].GetValues().GetValue(0).ToString();

                    orbElLabel3.Text = "z";
                    unitLabel3.Text = "km";
                    trackBar3.Minimum = -50000;
                    trackBar3.Maximum = 50000;
                    int val3 = Convert.ToInt32(posResult.DataSets[3].GetValues().GetValue(0));
                    SetVal(val3, trackBar3);
                    valueTextBox3.Text = posResult.DataSets[3].GetValues().GetValue(0).ToString();

                    orbElLabel4.Text = "dx";
                    unitLabel4.Text = "km/s";
                    trackBar4.Minimum = -100;
                    trackBar4.Maximum = 100;
                    int val4 = Convert.ToInt32(Convert.ToDouble(velResult.DataSets[1].GetValues().GetValue(0)) * 10.0);
                    SetVal(val4, trackBar4);
                    valueTextBox4.Text = velResult.DataSets[1].GetValues().GetValue(0).ToString();

                    orbElLabel5.Text = "dy";
                    unitLabel5.Text = "km/s";
                    trackBar5.Minimum = -100;
                    trackBar5.Maximum = 100;
                    int val5 = Convert.ToInt32(Convert.ToDouble(velResult.DataSets[2].GetValues().GetValue(0)) * 10.0);
                    SetVal(val5, trackBar5);
                    valueTextBox5.Text = velResult.DataSets[2].GetValues().GetValue(0).ToString();

                    orbElLabel6.Text = "dz";
                    unitLabel6.Text = "km/s";
                    trackBar6.Minimum = -100;
                    trackBar6.Maximum = 100;
                    int val6 = Convert.ToInt32(Convert.ToDouble(velResult.DataSets[3].GetValues().GetValue(0)) * 10.0);
                    valueTextBox6.Text = velResult.DataSets[3].GetValues().GetValue(0).ToString();

                    orbElLabel7.Visible = false;
                    equinoctialComboBox.Visible = false;
                    break;

                case "Classical":
                  

                    // get current state
                    IAgDrResult ceResult = SatelliteStkObject.DataProviders.GetDataPrvTimeVarFromPath("Classical Elements//ICRF").ExecSingle(sc.StartTime);

                    // update GUI


                    ChangeCheckBoxVisible(true);


                    orbElLabel1.Text = "a";
                    unitLabel1.Text = "km";
                    trackBar1.Minimum = 6380;
                    trackBar1.Maximum = 50000;
                    int val1cl = Convert.ToInt32(ceResult.DataSets[1].GetValues().GetValue(0));
                    SetVal(val1cl, trackBar1);
                    valueTextBox1.Text = trackBar1.Value.ToString();

                    orbElLabel2.Text = "e";
                    unitLabel2.Text = "";
                    trackBar2.Minimum = 0;
                    trackBar2.Maximum = 999;
                    int val2cl = Convert.ToInt32(Convert.ToDouble(ceResult.DataSets[2].GetValues().GetValue(0)) * 1000.0);
                    SetVal(val2cl, trackBar2);
                    valueTextBox2.Text = ceResult.DataSets[2].GetValues().GetValue(0).ToString();

                    orbElLabel3.Text = "i";
                    unitLabel3.Text = "deg";
                    trackBar3.Minimum = 0;
                    trackBar3.Maximum = 180;
                    int val3cl = Convert.ToInt32(ceResult.DataSets[3].GetValues().GetValue(0));
                    SetVal(val3cl, trackBar3);
                    valueTextBox3.Text = trackBar3.Value.ToString();

                    orbElLabel4.Text = "AoP";
                    unitLabel4.Text = "deg";
                    trackBar4.Minimum = 0;
                    trackBar4.Maximum = 360;
                    int val4cl = Convert.ToInt32(ceResult.DataSets[5].GetValues().GetValue(0));
                    SetVal(val4cl, trackBar4);
                    valueTextBox4.Text = trackBar4.Value.ToString();

                    orbElLabel5.Text = "RAAN";
                    unitLabel5.Text = "deg";
                    trackBar5.Minimum = 0;
                    trackBar5.Maximum = 360;
                    int val5cl = Convert.ToInt32(ceResult.DataSets[4].GetValues().GetValue(0));
                    SetVal(val5cl, trackBar5);
                    valueTextBox5.Text = trackBar5.Value.ToString();

                    orbElLabel6.Text = "TA";
                    unitLabel6.Text = "deg";
                    trackBar6.Minimum = 0;
                    trackBar6.Maximum = 360;
                    int val6cl = Convert.ToInt32(ceResult.DataSets[6].GetValues().GetValue(0));
                    SetVal(val6cl, trackBar6);
                    valueTextBox6.Text = ceResult.DataSets[6].GetValues().GetValue(0).ToString();

                    orbElLabel7.Visible = false;
                    equinoctialComboBox.Visible = false;
                    break;

                case "Equinoctial":

                    // get current state
                    IAgDrResult eqResult = SatelliteStkObject.DataProviders.GetDataPrvTimeVarFromPath("Equinoctial Elements//ICRF").ExecSingle(sc.StartTime);

                    // update GUI


                    ChangeCheckBoxVisible(false);

                    orbElLabel1.Text = "a";
                    unitLabel1.Text = "km";
                    trackBar1.Minimum = 6380;
                    trackBar1.Maximum = 50000;
                    int val6e = Convert.ToInt32(eqResult.DataSets[1].GetValues().GetValue(0));
                    SetVal(val6e, trackBar1);
                    valueTextBox1.Text = eqResult.DataSets[1].GetValues().GetValue(0).ToString();

                    orbElLabel2.Text = "h";
                    unitLabel2.Text = "";
                    trackBar2.Minimum = -99;
                    trackBar2.Maximum = 99;
                    SetVal(Convert.ToInt32(Convert.ToDouble(eqResult.DataSets[2].GetValues().GetValue(0)) * 100.0), trackBar2);
                    valueTextBox2.Text = eqResult.DataSets[2].GetValues().GetValue(0).ToString();

                    orbElLabel3.Text = "k";
                    unitLabel3.Text = "";
                    trackBar3.Minimum = -99;
                    trackBar3.Maximum = 99;
                    SetVal(Convert.ToInt32(Convert.ToDouble(eqResult.DataSets[3].GetValues().GetValue(0)) * 100.0), trackBar3);
                    valueTextBox3.Text = eqResult.DataSets[3].GetValues().GetValue(0).ToString();

                    orbElLabel4.Text = "p";
                    unitLabel4.Text = "";
                    trackBar4.Minimum = -100;
                    trackBar4.Maximum = 100;
                    SetVal(Convert.ToInt32(Convert.ToDouble(eqResult.DataSets[4].GetValues().GetValue(0)) * 100.0), trackBar4) ;
                    valueTextBox4.Text = eqResult.DataSets[4].GetValues().GetValue(0).ToString();

                    orbElLabel5.Text = "q";
                    unitLabel5.Text = "";
                    trackBar5.Minimum = -100;
                    trackBar5.Maximum = 100;
                    SetVal(Convert.ToInt32(Convert.ToDouble(eqResult.DataSets[5].GetValues().GetValue(0)) * 100.0),trackBar5);
                    valueTextBox5.Text = eqResult.DataSets[5].GetValues().GetValue(0).ToString();

                    orbElLabel6.Text = "mean long";
                    unitLabel6.Text = "deg";
                    trackBar6.Minimum = 0;
                    trackBar6.Maximum = 360;
                    SetVal(Convert.ToInt32(eqResult.DataSets[6].GetValues().GetValue(0)), trackBar6);
                    valueTextBox6.Text = eqResult.DataSets[6].GetValues().GetValue(0).ToString();

                    orbElLabel7.Visible = true;
                    equinoctialComboBox.Visible = true;
                    break;

                case "Delaunay":
                    // get current state
                    IAgDrResult deResult = SatelliteStkObject.DataProviders.GetDataPrvTimeVarFromPath("Delaunay Elements//ICRF").ExecSingle(sc.StartTime);
                    
                    MessageBox.Show("Delaunay Elements:" +
                                    Environment.NewLine + "l: " + deResult.DataSets[6].GetValues().GetValue(0).ToString() +
                                    Environment.NewLine + "g: " + deResult.DataSets[5].GetValues().GetValue(0).ToString() +
                                    Environment.NewLine + "h: " + deResult.DataSets[4].GetValues().GetValue(0).ToString() +
                                    Environment.NewLine + "L: " + deResult.DataSets[3].GetValues().GetValue(0).ToString() +
                                    Environment.NewLine + "G: " + deResult.DataSets[2].GetValues().GetValue(0).ToString() +
                                    Environment.NewLine + "H: " + deResult.DataSets[1].GetValues().GetValue(0).ToString());

                    // update GUI


                    ChangeCheckBoxVisible(false);

                    orbElLabel1.Text = "l";
                    unitLabel1.Text = "deg";
                    trackBar1.Minimum = 0;
                    trackBar1.Maximum = 360;
                    SetVal(Convert.ToInt32(deResult.DataSets[6].GetValues().GetValue(0)), trackBar1);
                    valueTextBox1.Text = deResult.DataSets[6].GetValues().GetValue(0).ToString();

                    orbElLabel2.Text = "g";
                    unitLabel2.Text = "deg";
                    trackBar2.Minimum = 0;
                    trackBar2.Maximum = 360;
                    SetVal(Convert.ToInt32(deResult.DataSets[5].GetValues().GetValue(0)), trackBar2);
                    valueTextBox2.Text = deResult.DataSets[5].GetValues().GetValue(0).ToString();

                    orbElLabel3.Text = "h";
                    unitLabel3.Text = "deg";
                    trackBar3.Minimum = 0;
                    trackBar3.Maximum = 360;
                    SetVal(Convert.ToInt32(deResult.DataSets[4].GetValues().GetValue(0)), trackBar3);
                    valueTextBox3.Text = deResult.DataSets[4].GetValues().GetValue(0).ToString();

                    orbElLabel4.Text = "L*1e-9";
                    unitLabel4.Text = "";
                    trackBar4.Minimum = 0;
                    trackBar4.Maximum = 100;
                    SetVal(Convert.ToInt32(Convert.ToDouble(deResult.DataSets[3].GetValues().GetValue(0)) / 1000000000.0), trackBar4);
                    valueTextBox4.Text = (Convert.ToDouble(deResult.DataSets[3].GetValues().GetValue(0)) / 1000000000.0).ToString();

                    orbElLabel5.Text = "G*1e-9";
                    unitLabel5.Text = "";
                    trackBar5.Minimum = 0;
                    trackBar5.Maximum = 100;
                    SetVal(Convert.ToInt32(Convert.ToDouble(deResult.DataSets[2].GetValues().GetValue(0)) / 1000000000.0), trackBar5);
                    valueTextBox5.Text = (Convert.ToDouble(deResult.DataSets[2].GetValues().GetValue(0)) / 1000000000.0).ToString();

                    orbElLabel6.Text = "H*1e-9";
                    unitLabel6.Text = "";
                    trackBar6.Minimum = 0;
                    trackBar6.Maximum = 100;
                    SetVal(Convert.ToInt32(Convert.ToDouble(deResult.DataSets[1].GetValues().GetValue(0)) / 1000000000.0), trackBar6);
                    valueTextBox6.Text = (Convert.ToDouble(deResult.DataSets[1].GetValues().GetValue(0)) / 1000000000.0).ToString();

                    orbElLabel7.Visible = false;
                    equinoctialComboBox.Visible = false;
                    break;

                case "Mixed Spherical":
                    // get current state
                    IAgDrResult msResult = SatelliteStkObject.DataProviders.GetDataPrvTimeVarFromPath("Mixed Spherical Elements//ICRF").ExecSingle(sc.StartTime);



                    // update GUI

                    ChangeCheckBoxVisible(false);

                    orbElLabel1.Text = "Lon";
                    unitLabel1.Text = "deg";
                    trackBar1.Minimum = 0;
                    trackBar1.Maximum = 360;

                    Int32 lonValue = Convert.ToInt32(msResult.DataSets[2].GetValues().GetValue(0));
                    if (Convert.ToInt32(msResult.DataSets[2].GetValues().GetValue(0)) < 0)
                    {
                        lonValue += 360;
                    }
                    SetVal(lonValue, trackBar1);
                    valueTextBox1.Text = lonValue.ToString();

                    orbElLabel2.Text = "Lat";
                    unitLabel2.Text = "deg";
                    trackBar2.Minimum = -89;
                    trackBar2.Maximum = 89;
                    SetVal(Convert.ToInt32(msResult.DataSets[1].GetValues().GetValue(0)), trackBar2);
                    valueTextBox2.Text = msResult.DataSets[1].GetValues().GetValue(0).ToString();

                    orbElLabel3.Text = "Alt";
                    unitLabel3.Text = "deg";
                    trackBar3.Minimum = 0;
                    trackBar3.Maximum = 50000;
                    SetVal(Convert.ToInt32(msResult.DataSets[3].GetValues().GetValue(0)), trackBar3);
                    valueTextBox3.Text = msResult.DataSets[3].GetValues().GetValue(0).ToString();

                    orbElLabel4.Text = "Hor FPA";
                    unitLabel4.Text = "deg";
                    trackBar4.Minimum = -89;
                    trackBar4.Maximum = 89;
                    SetVal(Convert.ToInt32(msResult.DataSets[4].GetValues().GetValue(0)), trackBar4);
                    valueTextBox4.Text = msResult.DataSets[4].GetValues().GetValue(0).ToString();

                    orbElLabel5.Text = "Az";
                    unitLabel5.Text = "deg";
                    trackBar5.Minimum = 0;
                    trackBar5.Maximum = 360;
                    SetVal(Convert.ToInt32(msResult.DataSets[5].GetValues().GetValue(0)), trackBar5);
                    valueTextBox5.Text = msResult.DataSets[5].GetValues().GetValue(0).ToString();

                    orbElLabel6.Text = "Vel";
                    unitLabel6.Text = "km/s";
                    trackBar6.Minimum = 1;
                    trackBar6.Maximum = 100;
                    SetVal(Convert.ToInt32(Convert.ToDouble(msResult.DataSets[6].GetValues().GetValue(0)) * 10.0), trackBar6);
                    valueTextBox6.Text = msResult.DataSets[6].GetValues().GetValue(0).ToString();

                    orbElLabel7.Visible = false;
                    equinoctialComboBox.Visible = false;
                    break;

                case "Spherical":
                    // get current state
                    IAgDrResult spResult = SatelliteStkObject.DataProviders.GetDataPrvTimeVarFromPath("Spherical Elements//ICRF").ExecSingle(sc.StartTime);



                    // update GUI

                    ChangeCheckBoxVisible(false);

                    orbElLabel1.Text = "RA";
                    unitLabel1.Text = "deg";
                    trackBar1.Minimum = 0;
                    trackBar1.Maximum = 360;
                    SetVal(Convert.ToInt32(spResult.DataSets[1].GetValues().GetValue(0)), trackBar1) ;
                    valueTextBox1.Text = spResult.DataSets[1].GetValues().GetValue(0).ToString();

                    orbElLabel2.Text = "Dec";
                    unitLabel2.Text = "deg";
                    trackBar2.Minimum = -89;
                    trackBar2.Maximum = 89;
                    SetVal(Convert.ToInt32(spResult.DataSets[2].GetValues().GetValue(0)), trackBar2);
                    valueTextBox2.Text = spResult.DataSets[2].GetValues().GetValue(0).ToString();

                    orbElLabel3.Text = "Radius";
                    unitLabel3.Text = "km";
                    trackBar3.Minimum = 6380;
                    trackBar3.Maximum = 50000;
                    SetVal(Convert.ToInt32(spResult.DataSets[3].GetValues().GetValue(0)), trackBar3);
                    valueTextBox3.Text = spResult.DataSets[3].GetValues().GetValue(0).ToString();

                    orbElLabel4.Text = "Hor FPA";
                    unitLabel4.Text = "deg";
                    trackBar4.Minimum = -89;
                    trackBar4.Maximum = 89;
                    SetVal(Convert.ToInt32(spResult.DataSets[4].GetValues().GetValue(0)), trackBar4);
                    valueTextBox4.Text = spResult.DataSets[4].GetValues().GetValue(0).ToString();

                    orbElLabel5.Text = "Az";
                    unitLabel5.Text = "deg";
                    trackBar5.Minimum = 0;
                    trackBar5.Maximum = 360;
                    SetVal(Convert.ToInt32(spResult.DataSets[5].GetValues().GetValue(0)), trackBar5);
                    valueTextBox5.Text = spResult.DataSets[5].GetValues().GetValue(0).ToString();

                    orbElLabel6.Text = "Vel";
                    unitLabel6.Text = "km/s";
                    trackBar6.Minimum = 1;
                    trackBar6.Maximum = 100;
                    SetVal(Convert.ToInt32(Convert.ToDouble(spResult.DataSets[6].GetValues().GetValue(0)) * 10.0), trackBar6);
                    valueTextBox6.Text = spResult.DataSets[6].GetValues().GetValue(0).ToString();

                    orbElLabel7.Visible = false;
                    equinoctialComboBox.Visible = false;
                    break;
            }

            m_init = false;
        }




        #region Update value TextBox
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            inputVal_errorLabel.Visible = false;
            if (!m_init)
            {
                valueTextBox1.Text = trackBar1.Value.ToString();
                UpdateSTKHelper();
            }
        }


        
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            inputVal_errorLabel.Visible = false;
            if (!m_init)
            {
                switch (coordTypeComboBox.SelectedItem.ToString())
                {
                    case "Classical":
                        valueTextBox2.Text = (Convert.ToDouble(trackBar2.Value) / 1000.0).ToString();
                        UpdateSTKHelper();
                        break;
                    case "Equinoctial":
                        valueTextBox2.Text = (Convert.ToDouble(trackBar2.Value) / 100.0).ToString();
                        UpdateSTKHelper();
                        break;
                    default:
                        valueTextBox2.Text = trackBar2.Value.ToString();
                        UpdateSTKHelper();
                        break;
                }
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            inputVal_errorLabel.Visible = false;
            if (!m_init)
            {
                switch (coordTypeComboBox.SelectedItem.ToString())
                {
                    case "Equinoctial":
                        valueTextBox3.Text = (Convert.ToDouble(trackBar3.Value) / 100.0).ToString();
                        UpdateSTKHelper();
                        break;
                    default:
                        valueTextBox3.Text = trackBar3.Value.ToString();
                        UpdateSTKHelper();
                        break;
                }
            }
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            inputVal_errorLabel.Visible = false;
            if (!m_init)
            {
                switch (coordTypeComboBox.SelectedItem.ToString())
                {
                    case "Cartesian ICRF":
                        valueTextBox4.Text = (Convert.ToDouble(trackBar4.Value) / 10.0).ToString();
                        UpdateSTKHelper();
                        break;
                    case "Equinoctial":
                        valueTextBox4.Text = (Convert.ToDouble(trackBar4.Value) / 100.0).ToString();
                        UpdateSTKHelper();
                        break;
                    default:
                        valueTextBox4.Text = trackBar4.Value.ToString();
                        UpdateSTKHelper();
                        break;
                }
            }
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            inputVal_errorLabel.Visible = false;
            if (!m_init)
            {
                switch (coordTypeComboBox.SelectedItem.ToString())
                {
                    case "Cartesian ICRF":
                        valueTextBox5.Text = (Convert.ToDouble(trackBar5.Value) / 10.0).ToString();
                        UpdateSTKHelper();
                        break;
                    case "Equinoctial":
                        valueTextBox5.Text = (Convert.ToDouble(trackBar5.Value) / 100.0).ToString();
                        UpdateSTKHelper();
                        break;
                    default:
                        valueTextBox5.Text = trackBar5.Value.ToString();
                        UpdateSTKHelper();
                        break;
                }
            }
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            inputVal_errorLabel.Visible = false;
            if (!m_init)
            {
                switch (coordTypeComboBox.SelectedItem.ToString())
                {
                    case "Cartesian ICRF":
                    case "Mixed Spherical":
                    case "Spherical":
                        valueTextBox6.Text = (Convert.ToDouble(trackBar6.Value) / 10.0).ToString();
                        UpdateSTKHelper();
                        break;
                    default:
                        valueTextBox6.Text = trackBar6.Value.ToString();
                        UpdateSTKHelper();
                        break;
                }
            }
        }
        #endregion



        #region Update slider value 

        
        
        
         

        

       

        

       

        
        #endregion


        private void UpdateSTKHelper()
        {
            
            double el1 = 0;
            double el2 = 0;
            double el3 = 0;
            double el4 = 0;
            double el5 = 0;
            double el6 = 0;
            
            bool el1r = IsError_TextChange(valueTextBox1.Text, trackBar1.Minimum, trackBar1.Maximum, orbElLabel1.Text, ref el1, 1);
            bool el2r = IsError_TextChange(valueTextBox2.Text, trackBar2.Minimum, trackBar2.Maximum, orbElLabel2.Text, ref el2, 1);
            bool el3r = IsError_TextChange(valueTextBox3.Text, trackBar3.Minimum, trackBar3.Maximum, orbElLabel3.Text, ref el3, 1);
            bool el4r = IsError_TextChange(valueTextBox4.Text, trackBar4.Minimum, trackBar4.Maximum, orbElLabel4.Text, ref el4, 1);
            bool el5r = IsError_TextChange(valueTextBox5.Text, trackBar5.Minimum, trackBar5.Maximum, orbElLabel5.Text, ref el5, 1);
            bool el6r = IsError_TextChange(valueTextBox6.Text, trackBar6.Minimum, trackBar6.Maximum, orbElLabel6.Text, ref el6, 1);

            if (!el1r && !el2r && !el3r && !el4r && !el5r && !el6r)
            {
                
                string type = "none";
                bool error = false;
                switch (coordTypeComboBox.Text)
                {
                    case "Cartesian ICRF":
                        error = STKHelper.UpdateCartesianOrbit(m_satellite, el1, el2, el3, el4, el5, el6, ref type);
                        break;
                    case "Classical":
                        error = STKHelper.UpdateClassicalOrbit(m_satellite, el1, el2, el3, el4, el5, el6, ref type);
                        break;
                    case "Equinoctial":
                        error = STKHelper.UpdateEquinocitalOrbit(m_satellite, el1, el2, el3, el4, el5, el6, ref type, equinoctialComboBox.Text);
                        break;
                    case "Delaunay":
                        error = STKHelper.UpdateDelaunayOrbit(m_satellite, el1, el2, el3, el4, el5, el6, ref type);
                        break;
                    case "Mixed Spherical":
                        error = STKHelper.UpdateMixedSphericalOrbit(m_satellite, el1, el2, el3, el4, el5, el6, ref type);
                        break;
                    case "Spherical":
                        error = STKHelper.UpdateSphericalOrbit(m_satellite, el1, el2, el3, el4, el5, el6, ref type);
                        break;
                }

                if (error)
                {
                    Change_InputVal_ErrorLabel(valueTextBox1.Text, type, "min", "max");
                }
            }
            
        }

        private void valueTextBox1_KeyDown(object sender, KeyEventArgs e)
        { // change text if key pressed was Enter
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            
            Text_Changed(valueTextBox1.Text, trackBar1, orbElLabel1.Text,  1);
        }

        private void valueTextBox1_Leave(object sender, EventArgs e)
        {
            Text_Changed(valueTextBox1.Text, trackBar1, orbElLabel1.Text, 1);
        }

        private void valueTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)
            {
                return;
            }

            UpdateBar2();
        }

        private void valueTextBox2_Leave(object sender, EventArgs e)
        {
            UpdateBar2();
        }

        private void UpdateBar2()
        {
            double multiplier = 1;
             switch (coordTypeComboBox.SelectedItem.ToString())
            {

                case "Classical":
                    multiplier = 1000;
                    break;
                case "Equinoctial":
                    multiplier = 100;
                    break;
                
            }

            Text_Changed(valueTextBox2.Text, trackBar2, orbElLabel2.Text, multiplier);
        }

        private void valueTextBox3_Leave(object sender, EventArgs e)
        {
            UpdateBar3();
        }

        private void valueTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)
            {
                return;
            }
            UpdateBar3();
        }

        private void UpdateBar3()
        {

            if (!m_init)
            {
                double multiplier = 1;
                switch (coordTypeComboBox.SelectedItem.ToString())
                {
                    case "Equinoctial":
                        multiplier = 100;
                        
                        break;
                    
                }
                Text_Changed(valueTextBox3.Text, trackBar3, orbElLabel3.Text, multiplier);
            }
        }


        private void valueTextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)
            {
                return;
            }
            UpdateBar4();
        }

        private void valueTextBox4_Leave(object sender, EventArgs e)
        {
            UpdateBar4();
        }

        private void UpdateBar4()
        {
            if (!m_init)
            {
                double multiplier = 1;
                switch (coordTypeComboBox.SelectedItem.ToString())
                {
                    case "Cartesian ICRF":
                        multiplier = 10.0;
                        
                        break;
                    case "Equinoctial":
                        multiplier = 100.0;
                        break;
                   
                }
                Text_Changed(valueTextBox4.Text, trackBar4, orbElLabel4.Text, multiplier);
            }
        }

        private void valueTextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            UpdateBar5();

        }

        private void valueTextBox5_Leave(object sender, EventArgs e)
        {
            UpdateBar5();
        }

        private void UpdateBar5()
        {
            if (!m_init)
            {
                double mult = 1.0;
                switch (coordTypeComboBox.SelectedItem.ToString())
                {
                    case "Cartesian ICRF":
                        mult = 10.0;
                        break;
                    case "Equinoctial":
                        mult = 100.0;
                        break;
                }
                Text_Changed(valueTextBox5.Text, trackBar5, orbElLabel5.Text, mult);
            }
        }

        private void valueTextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)
            {
                return;
            }

            UpdateBar6();
        }

        private void valueTextBox6_Leave(object sender, EventArgs e)
        {
            UpdateBar6();
        }

        private void UpdateBar6()
        {
            if (!m_init)
            {
                double mult = 1;
                switch (coordTypeComboBox.SelectedItem.ToString())
                {
                    case "Cartesian ICRF":
                    case "Mixed Spherical":
                    case "Spherical":
                        mult = 10.0;
                        break;

                }

                Text_Changed(valueTextBox6.Text, trackBar6, orbElLabel6.Text, mult);

            }
        }



        private void Text_Changed(string newVal, System.Windows.Forms.TrackBar trackBar, string eltLabel, double multiplier)
        {
            // newVal: value trying to set to
            // trackBar, eltLabel: associated track bar and orbit element label
            inputVal_errorLabel.Visible = false;

            double updateIntVal = 0;
            if (IsError_TextChange(newVal, trackBar.Minimum, trackBar.Maximum, eltLabel, ref updateIntVal, multiplier))
            {
                // there was an error, return without updating anything
                return;
            }
            
            // no error: continue with updating orbit
            double decVal = updateIntVal * multiplier;
            int updateVal = Convert.ToInt32(decVal);
            trackBar.Value = updateVal;
         
            UpdateSTKHelper();
        }

        private bool IsError_TextChange(string newVal, int min, int max, string eltLabel, ref double updateIntVal, double multiplier)
        { // return true if there is an error and output error message, false otherwise
          // newVal is the value that user is trying to change orb. element value to
          // trackBar and eltLabel are the associated track bar and orb. element label
          
            bool noError = Double.TryParse(newVal, out updateIntVal);

            if (noError)
            { // no error in changing string to double
                if (min <= (updateIntVal * multiplier) && (updateIntVal * multiplier) <= max)
                { // falls within bounds, return false --> no error
                    return false;
                }
                // value falls out of bounds, display error:
                Change_InputVal_ErrorLabel(eltLabel, "outside of values", min.ToString(), max.ToString());
                return true;

            }

            // was an error in converting, display error:
            Change_InputVal_ErrorLabel(eltLabel, "not a number", min.ToString(), max.ToString());
            return true;
        }

        private void Change_InputVal_ErrorLabel(string eltLabel, string errorType, string min, string max)
        {
           // change error message in Message Viewer based on error type
            inputVal_errorLabel.Visible = true;
            string cmd = "Message / 4 ";

            switch (errorType)
            {
                case "not a number":
                    cmd = cmd + "\"Orbit Tuner: Value for " + eltLabel + " must be a number\"";
                    break;
                case "outside of values":
                    cmd =  cmd + "\"Orbit Tuner: Value for " + eltLabel + " must be between " + min + " and " + max + "\"";
                    break;
                default:
                    cmd = cmd + "\"" + errorType + "\"";
                    break;
               
            }

            m_root.ExecuteCommand(cmd);
        }

        private void checkBox_orbElt1_CheckedChanged(object sender, EventArgs e)
        {
          
            if (checkBox_orbElt1.Checked)
            {
                // show orbital element
                STKHelper.ShowPeriapsisApoapsis(SatelliteAgSatellite);
            }
            else
            {
                STKHelper.HidePeriapsisApoapsis(SatelliteAgSatellite);
            }
           
        }

     

        private void checkBox_orbElt2_CheckedChanged(object sender, EventArgs e)
        {
         

            if (checkBox_orbElt2.Checked)
            {
                // show orbital elt
                STKHelper.ShowEcc(SatelliteAgSatellite);
               
            }
            else
            {
                STKHelper.HideEcc(SatelliteAgSatellite);
            }
          
        }

       

        private void checkBox_orbElt3_CheckedChanged(object sender, EventArgs e)
        {
          

            if (checkBox_orbElt3.Checked)
            {
                STKHelper.ShowInclination(m_satellite as AgSatellite);
            }
            else
            {
                STKHelper.HideInclination(m_satellite as AgSatellite);
            }

            

        }

        private void checkBox_orbElt4_CheckedChanged(object sender, EventArgs e)
        {
           
            if (checkBox_orbElt4.Checked)
            {
                STKHelper.ShowAoP(m_satellite as AgSatellite);
            }
            else
            {
                STKHelper.HideAoP(m_satellite as AgSatellite);
            }

           

        }

       
        private void checkBox_orbElt5_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox_orbElt5.Checked)
            {
                STKHelper.ShowRAAN(m_satellite as AgSatellite);
            }
            else
            {
                STKHelper.HideRAAN(m_satellite as AgSatellite);
            }
        }

       
        private void checkBox_orbElt6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_orbElt6.Checked)
            {
                STKHelper.ShowTA(m_satellite as AgSatellite);
            }
            else
            {
                STKHelper.HideTA(m_satellite as AgSatellite);
            }
        }

        
    }
}
