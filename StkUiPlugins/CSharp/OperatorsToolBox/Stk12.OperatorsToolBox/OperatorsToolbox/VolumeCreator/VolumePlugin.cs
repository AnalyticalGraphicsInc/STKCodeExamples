using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.VolumeCreator
{
    public partial class VolumePlugin : OpsPluginControl
    {
        public VolumePlugin()
        {
            InitializeComponent();
            CommonData.VolumeList = new List<VolumeConfig>();
            CommonData.LocationList = new List<LocationConfig>();
            CommonData.MissileFilePath = Path.Combine(@CommonData.InstallDir, "Databases\\VolumeDirectory.json");
            CommonData.LocationFilePath = Path.Combine(@CommonData.InstallDir, "Databases\\LocationDirectory.json");
            PopulateLists();
        }

        private void CreateNew_Click(object sender, EventArgs e)
        {
            CommonData.FromEdit = false;
            NewConfigForm form = new NewConfigForm();
            form.ShowDialog();
            ExistingVolumes.Items.Clear();
            foreach (var item in CommonData.VolumeList)
            {
                ExistingVolumes.Items.Add(item.Name);
            }
            try
            {
                ExistingVolumes.SelectedIndex = ExistingVolumes.Items.Count - 1;
            }
            catch (Exception)
            {
            }
            ReadWrite.WriteVolumeConfigFile(CommonData.MissileFilePath);
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            if (ExistingVolumes.SelectedIndex != -1 && ExistingVolumes.Items.Count > 0 && LocationList.SelectedIndex != -1)
            {
                IAgFacility place;
                IAgStkObject placeObj;
                placeObj = CreatorFunctions.GetCreateFacility(CommonData.LocationList[CommonData.LocationIndex].Name);
                place = placeObj as IAgFacility;

                place.Position.AssignGeodetic(Double.Parse(CommonData.LocationList[CommonData.LocationIndex].Latitude), Double.Parse(CommonData.LocationList[CommonData.LocationIndex].Longitude), 0);
                place.AltRef = AgEAltRefType.eWGS84;
                //Create complex conic sensor
                IAgStkObject sensorObj = CreatorFunctions.GetCreateSensor(placeObj, CommonData.VolumeList[CommonData.TvSelectedIndex].Name + "_" + CommonData.LocationList[CommonData.LocationIndex].Name);
                IAgSensor sensor = sensorObj as IAgSensor;
                sensor.SetPatternType(AgESnPattern.eSnComplexConic);
                IAgSnComplexConicPattern pattern = sensor.Pattern as IAgSnComplexConicPattern;
                pattern.OuterConeHalfAngle = 180.0;
                sensor.VO.PercentTranslucency = 70.0;

                IAgAccessConstraintCollection constraints = sensor.AccessConstraints;
                IAgAccessCnstrMinMax elConstraint;
                IAgAccessCnstrMinMax rangeConstraint;
                IAgAccessCnstrMinMax altConstraint;
                //Add elevation angle constraint
                if (constraints.IsConstraintActive(AgEAccessConstraints.eCstrElevationAngle))
                {
                    elConstraint = constraints.GetActiveConstraint(AgEAccessConstraints.eCstrElevationAngle) as IAgAccessCnstrMinMax;
                }
                else
                {
                    elConstraint = constraints.AddConstraint(AgEAccessConstraints.eCstrElevationAngle) as IAgAccessCnstrMinMax;
                }
                elConstraint.EnableMin = true;
                elConstraint.Min = Double.Parse(CommonData.VolumeList[CommonData.TvSelectedIndex].MinEl);
                elConstraint.EnableMax = true;
                elConstraint.Max = Double.Parse(CommonData.VolumeList[CommonData.TvSelectedIndex].MaxEl);
                //Add range constraint
                if (constraints.IsConstraintActive(AgEAccessConstraints.eCstrRange))
                {
                    rangeConstraint = constraints.GetActiveConstraint(AgEAccessConstraints.eCstrRange) as IAgAccessCnstrMinMax;
                }
                else
                {
                    rangeConstraint = constraints.AddConstraint(AgEAccessConstraints.eCstrRange) as IAgAccessCnstrMinMax;
                }
                rangeConstraint.EnableMin = true;
                rangeConstraint.Min = Double.Parse(CommonData.VolumeList[CommonData.TvSelectedIndex].MinRange);
                rangeConstraint.EnableMax = true;
                rangeConstraint.Max = Double.Parse(CommonData.VolumeList[CommonData.TvSelectedIndex].MaxRange);
                //Add altitude constraint
                if (constraints.IsConstraintActive(AgEAccessConstraints.eCstrAltitude))
                {
                    altConstraint = constraints.GetActiveConstraint(AgEAccessConstraints.eCstrAltitude) as IAgAccessCnstrMinMax;
                }
                else
                {
                    altConstraint = constraints.AddConstraint(AgEAccessConstraints.eCstrAltitude) as IAgAccessCnstrMinMax;
                }
                altConstraint.EnableMin = true;
                altConstraint.Min = Double.Parse(CommonData.VolumeList[CommonData.TvSelectedIndex].MinAlt);
                altConstraint.EnableMax = true;
                altConstraint.Max = Double.Parse(CommonData.VolumeList[CommonData.TvSelectedIndex].MaxAlt);

                try
                {
                    sensor.Graphics.Projection.UseConstraints = true;
                    sensor.Graphics.Projection.EnableConstraint("ElevationAngle");
                    CommonData.StkRoot.ExecuteCommand("Animate * Refresh");
                }
                catch (Exception)
                {

                }
            }


        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (ExistingVolumes.SelectedIndex != -1 && ExistingVolumes.Items.Count > 0)
            {
                CommonData.VolumeName = ExistingVolumes.SelectedText;
                CommonData.FromEdit = true;
                NewConfigForm form = new NewConfigForm();
                form.ShowDialog();
                ExistingVolumes.Items.Clear();
                foreach (var item in CommonData.VolumeList)
                {
                    ExistingVolumes.Items.Add(item.Name);
                    if (item.Name == CommonData.VolumeName)
                    {
                        ExistingVolumes.SelectedItem = item.Name;
                    }
                }

            }


            ReadWrite.WriteVolumeConfigFile(CommonData.MissileFilePath);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (ExistingVolumes.SelectedIndex != -1 && ExistingVolumes.Items.Count > 0)
            {
                CommonData.VolumeList.RemoveAt(CommonData.TvSelectedIndex);
                ExistingVolumes.Items.RemoveAt(CommonData.TvSelectedIndex);

                //Write file
                ReadWrite.WriteVolumeConfigFile(CommonData.MissileFilePath);
                try
                {
                    ExistingVolumes.SelectedIndex = 0;
                }
                catch (Exception)
                {

                }
            }
        }

        private void ExistingMissiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ExistingVolumes.SelectedIndex != -1 && ExistingVolumes.Items.Count > 0)
            {
                CommonData.TvSelectedIndex = ExistingVolumes.SelectedIndex;
            }
        }

        private void LocationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LocationList.SelectedIndex != -1 && LocationList.Items.Count > 0)
            {
                CommonData.LocationIndex = LocationList.SelectedIndex;
            }
        }

        private void CreateLocation_Click(object sender, EventArgs e)
        {
            CommonData.FromEdit = false;
            LocationForm form = new LocationForm();
            form.ShowDialog();
            LocationList.Items.Clear();
            foreach (var item in CommonData.LocationList)
            {
                LocationList.Items.Add(item.Name);
            }
            try
            {
                LocationList.SelectedIndex = LocationList.Items.Count - 1;
            }
            catch (Exception)
            {
            }
            ReadWrite.WriteLocationFile(CommonData.LocationFilePath);
        }

        private void EditLocation_Click(object sender, EventArgs e)
        {
            if (LocationList.SelectedIndex != -1 && LocationList.Items.Count > 0)
            {
                CommonData.FromEdit = true;
                LocationForm form = new LocationForm();
                form.ShowDialog();
                LocationList.Items.Clear();
                foreach (var item in CommonData.LocationList)
                {
                    LocationList.Items.Add(item.Name);
                    if (item.Name == CommonData.LocationName)
                    {
                        try
                        {
                            LocationList.SelectedItem = item.Name;
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                ReadWrite.WriteLocationFile(CommonData.LocationFilePath);
            }
        }

        private void DeleteLocation_Click(object sender, EventArgs e)
        {
            if (LocationList.SelectedIndex != -1 && LocationList.Items.Count > 0)
            {
                CommonData.LocationList.RemoveAt(LocationList.SelectedIndex);
                LocationList.Items.RemoveAt(LocationList.SelectedIndex);

                ReadWrite.WriteLocationFile(CommonData.LocationFilePath);

                try
                {
                    LocationList.SelectedIndex = 0;
                }
                catch (Exception)
                {
                }
            }
        }

        private void PopulateLists()
        {
            List<VolumeConfig> volumeConfigs = new List<VolumeConfig>();
            if (File.Exists(CommonData.MissileFilePath))
            {
                volumeConfigs = ReadWrite.ReadVolumeConfigFile(CommonData.MissileFilePath);
                ExistingVolumes.Items.Clear();
                foreach (var item in volumeConfigs)
                {
                    CommonData.VolumeList.Add(item);
                    ExistingVolumes.Items.Add(item.Name);
                }
                try
                {
                    ExistingVolumes.SelectedIndex = ExistingVolumes.Items.Count - 1;
                }
                catch (Exception)
                {
                }
            }

            List<LocationConfig> fileLocations = new List<LocationConfig>();
            if (File.Exists(CommonData.LocationFilePath))
            {
                fileLocations = ReadWrite.ReadLocationFile(CommonData.LocationFilePath);
                LocationList.Items.Clear();
                foreach (var item in fileLocations)
                {
                    CommonData.LocationList.Add(item);
                    LocationList.Items.Add(item.Name);
                }
                try
                {
                    LocationList.SelectedIndex = LocationList.Items.Count - 1;
                }
                catch (Exception)
                {
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }
    }
}
