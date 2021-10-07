using AGI.STKObjects;
using AGI.STKUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsToolbox.SmartView
{
    public partial class CustomLeadTrailForm : Form
    {
        private StkObjectsLibrary library;
        Tuple<int, int> currentCell;
        public CustomLeadTrailForm()
        {
            InitializeComponent();
            library = new StkObjectsLibrary();
            PopulateTable(CommonData.CurrentViewObjectData);
            currentCell = new Tuple<int, int>(0,0);
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            List<ObjectData> data = CommonData.CurrentViewObjectData;
            for (int i = 0; i < ObjectGrid.Rows.Count; i++)
            {
                int index = data.IndexOf(data.Where(p => p.SimpleName == (string)ObjectGrid.Rows[i].Cells[1].Value).FirstOrDefault());
                data[index].ModifyLeadTrail = (bool)ObjectGrid.Rows[i].Cells[0].Value;
                data[index].LeadSetting3D = SmartViewFunctions.GetLeadTrailObject(ObjectGrid.Rows[i].Cells[2].Value.ToString());
                if (SmartViewFunctions.GetLeadTrailObject(ObjectGrid.Rows[i].Cells[3].Value.ToString()) != AgELeadTrailData.eDataUnknown)
                {
                    data[index].TrailSetting3D = SmartViewFunctions.GetLeadTrailObject(ObjectGrid.Rows[i].Cells[3].Value.ToString());
                }
                else
                {
                    data[index].TrailSetting3D = SmartViewFunctions.GetLeadTrailObject(ObjectGrid.Rows[i].Cells[2].Value.ToString());
                }
                if (data[index].SimplePath.Contains("Satellite"))
                {
                    if (ObjectGrid.Rows[i].Cells[4].Value.ToString().Contains("Inertial"))
                    {
                        data[index].CoordSys = "Inertial";
                    }
                    else if (ObjectGrid.Rows[i].Cells[4].Value.ToString().Contains("Fixed"))
                    {
                        data[index].CoordSys = "Fixed";
                    }
                    else if (ObjectGrid.Rows[i].Cells[4].Value.ToString().Contains("VVLH"))
                    {
                        data[index].CoordSys = "Satellite/" + ObjectGrid.Rows[i].Cells[5].Value.ToString() + " VVLH System";
                    }
                }

            }
            CommonData.CurrentViewObjectData = data;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateTable(List<ObjectData> data)
        {
            List<string> temp = new List<string>();
            temp = CreatorFunctions.PopulateListByClass("Satellite");
            foreach (var item in temp)
            {
                int index = data.IndexOf(data.Where(p => p.SimplePath.Contains(item)).FirstOrDefault());
                if (index != -1)
                {
                    PopulateObjectRow(data[index]);
                }
            }
            temp.Clear();

            temp = CreatorFunctions.PopulateListByClass("Aircraft");
            foreach (var item in temp)
            {
                int index = data.IndexOf(data.Where(p => p.SimplePath.Contains(item)).FirstOrDefault());
                if (index != -1)
                {
                    PopulateObjectRow(data[index]);
                }
            }
            temp.Clear();

            temp = CreatorFunctions.PopulateListByClass("Ship");
            foreach (var item in temp)
            {
                int index = data.IndexOf(data.Where(p => p.SimplePath.Contains(item)).FirstOrDefault());
                if (index != -1)
                {
                    PopulateObjectRow(data[index]);
                }
            }
            temp.Clear();

            temp = CreatorFunctions.PopulateListByClass("LaunchVehicle");
            foreach (var item in temp)
            {
                int index = data.IndexOf(data.Where(p => p.SimplePath.Contains(item)).FirstOrDefault());
                if (index != -1)
                {
                    PopulateObjectRow(data[index]);
                }
            }
            temp.Clear();

            temp = CreatorFunctions.PopulateListByClass("GroundVehicle");
            foreach (var item in temp)
            {
                int index = data.IndexOf(data.Where(p => p.SimplePath.Contains(item)).FirstOrDefault());
                if (index != -1)
                {
                    PopulateObjectRow(data[index]);
                }
            }
            temp.Clear();

            temp = CreatorFunctions.PopulateListByClass("Missile");
            foreach (var item in temp)
            {
                int index = data.IndexOf(data.Where(p => p.SimplePath.Contains(item)).FirstOrDefault());
                if (index != -1)
                {
                    PopulateObjectRow(data[index]);
                }
            }
            temp.Clear();
        }

        private void PopulateLeadOptions(DataGridViewComboBoxCell box, ObjectData data)
        {
            List<string> objectList = new List<string>();
            if (data.SimplePath.Contains("Satellite"))
            {
                objectList.Add("OnePass");
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            else if (data.SimplePath.Contains("Aircraft"))
            {
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            else if (data.SimplePath.Contains("Ship"))
            {
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            else if (data.SimplePath.Contains("GroundVehicle"))
            {
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            else if (data.SimplePath.Contains("Missile"))
            {
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            else if (data.SimplePath.Contains("LaunchVehicle"))
            {
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            if (objectList.Count > 0)
            {
                box.DataSource = objectList;
            }
        }

        private void PopulateTrailOptions(DataGridViewComboBoxCell box, ObjectData data)
        {
            List<string> objectList = new List<string>();
            if (data.SimplePath.Contains("Satellite"))
            {
                objectList.Add("SameAsLead");
                objectList.Add("OnePass");
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            else if (data.SimplePath.Contains("Aircraft"))
            {
                objectList.Add("SameAsLead");
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            else if (data.SimplePath.Contains("Ship"))
            {
                objectList.Add("SameAsLead");
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            else if (data.SimplePath.Contains("GroundVehicle"))
            {
                objectList.Add("SameAsLead");
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            else if (data.SimplePath.Contains("Missile"))
            {
                objectList.Add("SameAsLead");
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            else if (data.SimplePath.Contains("LaunchVehicle"))
            {
                objectList.Add("SameAsLead");
                objectList.Add("All");
                objectList.Add("Full");
                objectList.Add("Half");
                objectList.Add("Quarter");
                objectList.Add("None");
            }
            if (objectList.Count > 0)
            {
                box.DataSource = objectList;
            }
        }

        private void PopulateFrameOptions(DataGridViewComboBoxCell box, ObjectData data)
        {
            List<string> objectList = new List<string>();
            if (data.SimplePath.Contains("Satellite"))
            {
                objectList.Add("Inertial");
                objectList.Add("Fixed");
                objectList.Add("VVLH");

                if (objectList.Count > 0)
                {
                    box.DataSource = objectList;
                 }
            }
        }

        private void PopulateVvlhObjects(DataGridViewComboBoxCell box, ObjectData data)
        {
            List<string> objectList = new List<string>();
            IAgExecCmdResult result;
            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Satellite");
            if (result[0] != "None")
            {
                string[] facArray = result[0].Split(null);
                foreach (var item in facArray)
                {
                    if (item != null && item != "")
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            objectList.Add(facName);
                        }
                    }
                }
            }
            if (objectList.Count > 0)
            {
                box.DataSource = objectList;
            }
        }

        private void PopulateObjectRow(ObjectData data)
        {
            ObjectGrid.Rows.Add();

            DataGridViewCheckBoxCell cell = ObjectGrid[0, ObjectGrid.Rows.Count - 1] as DataGridViewCheckBoxCell;
            if (data.ModifyLeadTrail)
            {
                cell.Value = true;
            }
            else
            {
                cell.Value = false;
            }

            DataGridViewTextBoxCell name = ObjectGrid[1, ObjectGrid.Rows.Count - 1] as DataGridViewTextBoxCell;
            name.Style.ForeColor = Color.White;
            name.Style.BackColor = Color.DimGray;
            name.Value = data.SimpleName;
            name.ReadOnly = true;

            //Lead Types
            DataGridViewComboBoxCell comboCell = ObjectGrid[2, ObjectGrid.Rows.Count - 1] as DataGridViewComboBoxCell;
            comboCell.Style.ForeColor = Color.White;
            comboCell.Style.BackColor = Color.DimGray;

            PopulateLeadOptions(comboCell, data);
            if (comboCell.Items.Contains(SmartViewFunctions.GetLeadTrailString(data.LeadSetting3D)))
            {
                comboCell.Value = comboCell.Items[comboCell.Items.IndexOf(SmartViewFunctions.GetLeadTrailString(data.LeadSetting3D))];
            }
            else
            {
                comboCell.Value = comboCell.Items[0];
            }

            //Trail Types
            comboCell = ObjectGrid[3, ObjectGrid.Rows.Count - 1] as DataGridViewComboBoxCell;
            comboCell.Style.ForeColor = Color.White;
            comboCell.Style.BackColor = Color.DimGray;

            PopulateTrailOptions(comboCell, data);
            if (comboCell.Items.Contains(SmartViewFunctions.GetLeadTrailString(data.TrailSetting3D)))
            {
                comboCell.Value = comboCell.Items[comboCell.Items.IndexOf(SmartViewFunctions.GetLeadTrailString(data.TrailSetting3D))];
            }
            else
            {
                comboCell.Value = comboCell.Items[0];
            }

            if (data.SimplePath.Contains("Satellite"))
            {
                //VVLH reference options
                comboCell = ObjectGrid[5, ObjectGrid.Rows.Count - 1] as DataGridViewComboBoxCell;
                comboCell.Style.ForeColor = Color.White;
                comboCell.Style.BackColor = Color.DimGray;
                PopulateVvlhObjects(comboCell, data);
                for (int i = 0; i < comboCell.Items.Count; i++)
                {
                    if (data.CoordSys.Contains((string)comboCell.Items[i]))
                    {
                        comboCell.Value = comboCell.Items[i];
                        break;
                    }
                    if (i == comboCell.Items.Count - 1)
                    {
                        comboCell.Value = comboCell.Items[0];
                    }
                }
                //Frame
                comboCell = ObjectGrid[4, ObjectGrid.Rows.Count - 1] as DataGridViewComboBoxCell;
                comboCell.Style.ForeColor = Color.White;
                comboCell.Style.BackColor = Color.DimGray;
                PopulateFrameOptions(comboCell, data);
                if (data.CoordSys.Contains("Inertial"))
                {
                    comboCell.Value = comboCell.Items[0];
                }
                else if (data.CoordSys.Contains("Fixed"))
                {
                    comboCell.Value = comboCell.Items[1];
                }
                else if (data.CoordSys.Contains("VVLH"))
                {
                    comboCell.Value = comboCell.Items[2];
                }
            }
            else
            {
                //ObjectGrid[5, ObjectGrid.Rows.Count - 1].ReadOnly = true;
                //ObjectGrid[4, ObjectGrid.Rows.Count - 1].ReadOnly = true;
            }
        }

        private void ObjectGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell cell = ObjectGrid.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;
                if ((bool)cell.EditingCellFormattedValue)
                {
                    ObjectGrid.Rows[e.RowIndex].ReadOnly = false;
                }
                else
                {
                    ObjectGrid.Rows[e.RowIndex].ReadOnly = true;
                    ObjectGrid.Rows[e.RowIndex].Cells[0].ReadOnly = false;
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                RightClickMenu.Show(Cursor.Position.X, Cursor.Position.Y);
                currentCell = new Tuple<int, int>(e.RowIndex, e.ColumnIndex);
            }
        }

        private void RightClickMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Apply to Column")
            {
                if (currentCell.Item2 == 0)
                {
                    foreach (DataGridViewRow row in ObjectGrid.Rows)
                    {
                        row.Cells[currentCell.Item2].Value = ObjectGrid.Rows[currentCell.Item1].Cells[currentCell.Item2].Value;
                    }
                }
                else
                {
                    DataGridViewComboBoxCell cell;
                    foreach (DataGridViewRow row in ObjectGrid.Rows)
                    {
                        cell = row.Cells[currentCell.Item2] as DataGridViewComboBoxCell;
                        if (cell.Items.Contains(ObjectGrid.Rows[currentCell.Item1].Cells[currentCell.Item2].Value))
                        {
                            row.Cells[currentCell.Item2].Value = ObjectGrid.Rows[currentCell.Item1].Cells[currentCell.Item2].Value;
                        }                       
                    }
                }
            }
        }
    }
}
