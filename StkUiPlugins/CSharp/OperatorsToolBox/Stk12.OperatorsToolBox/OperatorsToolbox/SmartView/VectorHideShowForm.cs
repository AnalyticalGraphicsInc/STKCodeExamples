using AGI.STKObjects;
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
    public partial class VectorHideShowForm : Form
    {
        private List<ObjectData> _data;
        private Dictionary<string, AgEGeometricElemType> dict;
        private bool checkChange;
        public VectorHideShowForm()
        {
            InitializeComponent();
            _data = CommonData.CurrentViewObjectData;
            dict = new Dictionary<string, AgEGeometricElemType>();
            checkChange = false;
            PopulateList();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            int index;
            foreach (TreeNode item in AvailableVectorList.Nodes)
            {
                index = _data.IndexOf(_data.Where(p => p.SimpleName == item.Text).FirstOrDefault());
                _data[index].ActiveVgtComponents.Clear();
                foreach (TreeNode child in item.Nodes)
                {
                    if (child.Checked)
                    {
                        if (!_data[index].ActiveVgtComponents.Keys.Contains(child.Text))
                        {
                            _data[index].ActiveVgtComponents.Add(child.Text, dict[child.Text]);
                            //_data[index].ActiveVgtTypes.Add()
                        }
                    }
                }
            }
            CommonData.CurrentViewObjectData = _data;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AllOff_Click(object sender, EventArgs e)
        {
            checkChange = true;
            foreach (TreeNode item in AvailableVectorList.Nodes)
            {
                item.Checked = false;
                foreach (TreeNode child in item.Nodes)
                {
                    child.Checked = false;
                }
            }
            checkChange = false;
        }

        private void PopulateList()
        {
            AvailableVectorList.Nodes.Clear();
            string parentStr = null;
            string parentSimpleName = null;
            int parentCount = 0;
            int childCount = 0;
            string name = null;
            for (int i = 0; i < _data.Count; i++)
            {
                parentStr = _data[i].SimplePath;
                IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(parentStr);
                dynamic vo = SmartViewFunctions.GetObjectVO(obj);
                //Only populate objects that have valid vo properties
                if (vo != null)
                {
                    IAgVORefCrdnCollection vgtComponents = vo.Vector.RefCrdns;
                    //Only populate objects with valid vgt components in object properties list (i.e. 3D Graphics->Vector)
                    if (vgtComponents.Count > 0)
                    {
                        AvailableVectorList.Nodes.Add(_data[i].SimpleName);
                        parentCount++;
                        childCount = 0;
                        //Display all vgt components for the parent object. Add key value pairs into local dictionary and cross reference with existing options in saved view
                        for (int j = 0; j < vgtComponents.Count; j++)
                        {
                            name = vgtComponents[j].Name;
                            AvailableVectorList.Nodes[parentCount - 1].Nodes.Add(name);
                            if (!dict.ContainsKey(name))
                            {
                                dict.Add(name, vgtComponents[j].TypeID);
                            }
                            if (_data[i].ActiveVgtComponents.Keys.Contains(name))
                            {
                                AvailableVectorList.Nodes[parentCount - 1].Nodes[childCount].Checked = true;
                            }
                            else
                            {
                                AvailableVectorList.Nodes[parentCount - 1].Nodes[childCount].Checked = false;
                            }
                            childCount++;
                        }
                    }
                }
            }
        }

        private void TrimUnselected_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will remove all unchecked components from the object's 3D Graphics->Vector list. \n Do you wish to continue?","Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string parentStr = null;
                int index;
                IAgStkObject obj;
                dynamic vo;
                foreach (TreeNode item in AvailableVectorList.Nodes)
                {
                    index = _data.IndexOf(_data.Where(p => p.SimpleName == item.Text).FirstOrDefault());
                    parentStr = _data[index].SimplePath;
                    obj = CommonData.StkRoot.GetObjectFromPath(parentStr);
                    vo = SmartViewFunctions.GetObjectVO(obj);
                    if (vo != null)
                    {
                        IAgVORefCrdnCollection crdns = vo.Vector.RefCrdns;
                        foreach (TreeNode child in item.Nodes)
                        {
                            if (!child.Checked)
                            {
                                crdns.RemoveByName(dict[child.Text], child.Text);
                            }
                        }
                    }
                }
                PopulateList();
            }
        }

        private void AllOn_Click(object sender, EventArgs e)
        {
            checkChange = true;
            foreach (TreeNode item in AvailableVectorList.Nodes)
            {
                item.Checked = true;
                foreach (TreeNode child in item.Nodes)
                {
                    child.Checked = true;
                }
            }
            checkChange = false;
        }

        private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
        {
            if (!checkChange)
            {
                foreach (TreeNode item in node.Nodes)
                {
                    item.Checked = isChecked;

                    if (item.Nodes.Count > 0)
                    {
                        this.CheckTreeViewNode(item, isChecked);
                    }
                }
            }
        }

        private void AvailableVectorList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
        }
    }
}
