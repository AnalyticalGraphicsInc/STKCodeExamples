using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.GroundEvents
{
    public partial class SubObjectsForm : Form
    {
        public SubObjectsForm()
        {
            List<SubObject> subObjectsList = new List<SubObject>();
            InitializeComponent();
            ZoomLevel.Items.Add("500 km");
            ZoomLevel.Items.Add("1,000 km");
            ZoomLevel.Items.Add("10,000 km");

            foreach (var type in CommonData.Preferences.EventTypeList)
            {
                SubObjectType.Items.Add(type);
            }

            SubObjectType.Enabled = false;
            ZoomLevel.Enabled = false;
            LatitudeValue.Enabled = false;
            LongitudeValue.Enabled = false;
            NameValue.Enabled = false;

            PopulateSubObjects();
        }

        private void SubObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (SubObjectList.SelectedItems.Count != 0)
            {
                try
                {
                    string placeName = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "-" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Name;
                    string cmd = "Unload / */Place/" + placeName;
                    CommonData.StkRoot.ExecuteCommand(cmd);

                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects.RemoveAt(CommonData.SubObjectIndex);
                    SubObjectList.SelectedItems[0].Remove();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            SubObjectList.SmallImageList = imageList1;
            SubObject newSub = new SubObject();
            newSub.Type = "Unknown";
            if (CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects.Count!=0)
            {
                newSub.Name = "SubObject" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects.Count.ToString();
            }
            else
            {
                newSub.Name = "SubObject";
            }
            //Assign default values to class
            newSub.Latitude = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Latitude;
            newSub.Longitude = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Longitude;
            newSub.ZoomLevel = "1000";
            CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects.Add(newSub);
            int index = GroundEventFunctions.GetSubObjectImageIndex(newSub);

            var listItem = new ListViewItem();
            listItem.ImageIndex = index;
            listItem.SubItems[0].Text = newSub.Name;
            SubObjectList.Items.Add(listItem);
            SubObjectList.FocusedItem = listItem;
            CommonData.SubObjectIndex = SubObjectList.Items.Count - 1;

            SubObjectType.Enabled = true;
            ZoomLevel.Enabled = true;
            LatitudeValue.Enabled = true;
            LongitudeValue.Enabled = true;
            NameValue.Enabled = true;

            //Assign GUI values
            SubObjectType.SelectedIndex = 0;
            ZoomLevel.SelectedIndex = 1;
            LongitudeValue.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Longitude.ToString();
            LatitudeValue.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Latitude.ToString();
            NameValue.Text = newSub.Name;

            //Create sub-object
            GroundEventFunctions.CreateSubObject(CommonData.CurrentEvents[CommonData.EventSelectedIndex],newSub);
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            int fieldCheck = FieldCheck();
            if (fieldCheck==0)
            {
                //Assign new data
                string placeName = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "-" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Name;

                string path = "*/Place/" + placeName;
                string currentType = CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Type;
                SubObjectList.SmallImageList = imageList1;

                CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Latitude = LatitudeValue.Text;
                CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Longitude = LongitudeValue.Text;
                CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Name = NameValue.Text;
                CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].ZoomLevel = GetZoomLevelFromIndex();
                CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Type = SubObjectType.Text;
                //Set interface values
                int index = GroundEventFunctions.GetSubObjectImageIndex(CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex]);
                SubObjectList.Items[CommonData.SubObjectIndex].ImageIndex = index;
                SubObjectList.Items[CommonData.SubObjectIndex].SubItems[0].Text = NameValue.Text;
                SubObjectList.Refresh();
                //Rename STK object
                string cmd;
                try
                {
                    cmd = "Rename " + path + " " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "-" + NameValue.Text;
                    CommonData.StkRoot.ExecuteCommand(cmd);
                }
                catch (Exception)
                {
                    //MessageBox.Show("Error renaming object");
                }

                //Reassign position. Check is for when the renaming is not successful
                placeName = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "-" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Name;
                path = "Place/" + placeName;
                if (CommonData.StkRoot.ObjectExists(path))
                {
                    IAgPlace place = CommonData.StkRoot.GetObjectFromPath(path) as IAgPlace;
                    place.Position.AssignGeodetic(Double.Parse(LatitudeValue.Text), Double.Parse(LongitudeValue.Text), 0);
                }

                //Change object marker if type changed
                if (currentType != SubObjectType.Text)
                {
                    string filePath = GroundEventFunctions.GetImagePath(CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Type);
                    try
                    {
                        cmd = "VO */Place/" + placeName + " marker show on markertype imagefile imagefile \"" + filePath + "\" Size 32";
                        CommonData.StkRoot.ExecuteCommand(cmd);
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Type = SubObjectType.Text;
                }
                //Apply zoom level
                string zoom = CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].ZoomLevel+"000";
                try
                {
                    cmd = "VO */Place/" + placeName + " ModelDetail Set ModelLabel " + zoom + " MarkerLabel " + zoom + " Marker " + zoom + " Point " + zoom;
                    CommonData.StkRoot.ExecuteCommand(cmd);
                }
                catch (Exception)
                {
                    string mes = "Could not Modify Zoom for SubObject";
                    MessageBox.Show(mes);
                }
                //Rewrite output file to reflect changes
                ReadWrite.WriteEventFile(CommonData.EventFileStr);
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ZoomLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SubObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SubObjectList.SelectedItems != null && SubObjectList.SelectedItems.Count > 0)
            {
                CommonData.SubObjectIndex = SubObjectList.FocusedItem.Index;
                if (SubObjectList.FocusedItem.Index!=-1 && SubObjectList.FocusedItem!=null)
                {
                    SubObjectType.SelectedIndex = GroundEventFunctions.GetSubObjectImageIndex(CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex]);
                    ZoomLevel.SelectedIndex = GetIndexFromZoom();
                    LongitudeValue.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Longitude;
                    LatitudeValue.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Latitude;
                    NameValue.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].Name;

                    SubObjectType.Enabled = true;
                    ZoomLevel.Enabled = true;
                    LatitudeValue.Enabled = true;
                    LongitudeValue.Enabled = true;
                    NameValue.Enabled = true;
                }
            }
        }

        private void PopulateSubObjects()
        {
            imageList1.Images.Clear();
            string msg = null;
            foreach (var item in CommonData.Preferences.EventImageLocations)
            {
                try
                {
                    imageList1.Images.Add(Image.FromFile(item));
                }
                catch (Exception)
                {
                    msg = msg + item + "\n";
                }
            }
            if (msg != null)
            {
                MessageBox.Show(msg);
            }
            if (CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects.Count>0)
            {
                foreach (var subObject in CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects)
                {
                    int index = GroundEventFunctions.GetSubObjectImageIndex(subObject);

                    var listItem = new ListViewItem();
                    listItem.ImageIndex = index;
                    listItem.SubItems[0].Text = subObject.Name;
                    SubObjectList.Items.Add(listItem);
                }
            }


        }

        private string GetZoomLevelFromIndex()
        {
            string zoom = null;
            int index = ZoomLevel.SelectedIndex;
            if (index==0)
            {
                zoom = "500";
            }
            else if (index == 1)
            {
                zoom = "1000";
            }
            else if (index == 2)
            {
                zoom = "10000";
            }
            return zoom;

        }

        private int GetIndexFromZoom()
        {
            int zoomInt = 0;
            if (CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].ZoomLevel=="500")
            {
                zoomInt = 0;
            }
            else if (CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].ZoomLevel == "1000")
            {
                zoomInt = 1;
            }
            else if (CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects[CommonData.SubObjectIndex].ZoomLevel == "10000")
            {
                zoomInt = 2;
            }
            else
            {
                zoomInt = 1;
            }
            return zoomInt;
        }

        private int FieldCheck()
        {
            int check = 0;
            double latitude;
            double longitude;

            bool isNumerical = Double.TryParse(LatitudeValue.Text, out latitude);
            if (!isNumerical)
            {
                MessageBox.Show("Latitude field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(LongitudeValue.Text, out longitude);
            if (!isNumerical)
            {
                MessageBox.Show("Longitude field not a number");
                check = 1;
            }

            //string objName = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "-" + NameValue.Text;
            //bool objExists = CommonData.StkRoot.ObjectExists("Place/" + objName);
            //if (objExists && objName != CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "-" + SubObjectList.Items[CommonData.SubObjectIndex].Name)
            //{
            //    MessageBox.Show("Object with that name already exists");
            //    check = 1;
            //}

            return check;

        }
    }
}
