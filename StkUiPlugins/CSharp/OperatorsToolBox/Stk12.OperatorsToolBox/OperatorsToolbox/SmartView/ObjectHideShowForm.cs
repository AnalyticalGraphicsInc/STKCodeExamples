using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;

namespace OperatorsToolbox.SmartView
{
    public partial class ObjectHideShowForm : Form
    {
        List<ObjectData> _data = new List<ObjectData>();
        public ObjectHideShowForm()
        {
            InitializeComponent();
            FilterType.Items.Add("Object Browser");
            FilterType.Items.Add("Constellation");
            FilterType.SelectedIndex = 0;
            PopulateList();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            int index;
            int childIndex;
            if (FilterType.SelectedIndex!=-1)
            {
                if (FilterType.SelectedIndex==0)
                {
                    foreach (TreeNode item in ObjectList.Nodes)
                    {
                        index = _data.IndexOf(_data.Where(p => p.SimpleName == item.Text).FirstOrDefault());
                        _data[index].HideShow = item.Checked;

                        if (item.Nodes.Count != 0)
                        {
                            foreach (TreeNode child in item.Nodes)
                            {
                                childIndex = _data.IndexOf(_data.Where(p => p.SimpleName == child.Text).FirstOrDefault());
                                if (childIndex!=-1)
                                {
                                    _data[childIndex].HideShow = child.Checked;
                                }
                            }
                        }
                    }
                }
                else if (FilterType.SelectedIndex == 1)
                {
                    foreach (TreeNode item in ObjectList.Nodes)
                    {
                        if (item.Nodes.Count != 0)
                        {
                            foreach (TreeNode child in item.Nodes)
                            {
                                childIndex = _data.IndexOf(_data.Where(p => p.SimpleName == child.Text).FirstOrDefault());
                                if (childIndex != -1)
                                {
                                    _data[childIndex].HideShow = child.Checked;
                                }
                            }
                        }
                    }
                }
                CommonData.CurrentViewObjectData = _data;
                this.Close();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectAll.Checked)
            {
                foreach (TreeNode item in ObjectList.Nodes)
                {
                    item.Checked = true;
                    foreach (TreeNode child in item.Nodes)
                    {
                        child.Checked = true;
                    }
                }
            }
            else
            {
                foreach (TreeNode item in ObjectList.Nodes)
                {
                    item.Checked = false;
                    foreach (TreeNode child in item.Nodes)
                    {
                        child.Checked = false;
                    }
                }
            }
        }

        private void ToggleSensors_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TreeNode item in ObjectList.Nodes)
            {
                if (item.Nodes.Count!=0)
                {
                    foreach (TreeNode child in item.Nodes)
                    {
                        foreach (ObjectData dataItem in _data)
                        {
                            if (dataItem.SimpleName==child.Text)
                            {
                                if (ToggleSensors.Checked)
                                {
                                    child.Checked = true;
                                }
                                else
                                {
                                    child.Checked = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void PopulateList()
        {
            ObjectList.Nodes.Clear();
            _data = CommonData.CurrentViewObjectData;
            string parentStr = null;
            string parentSimpleName = null;
            int parentCount = 0;
            int childCount = 0;
            if (FilterType.SelectedIndex==0)
            {
                for (int i = 0; i < _data.Count; i++)
                {
                    if (parentStr == null)
                    {
                        parentStr = _data[i].SimplePath;
                        parentSimpleName = _data[i].SimpleName;
                        ObjectList.Nodes.Add(_data[i].SimpleName);
                        if (_data[i].HideShow)
                        {
                            ObjectList.Nodes[0].Checked = true;
                        }
                        parentCount++;
                    }
                    else if (_data[i].SimplePath.Contains(parentStr))
                    {
                        ObjectList.Nodes[parentCount - 1].Nodes.Add(_data[i].SimpleName);
                        if (_data[i].HideShow)
                        {
                            ObjectList.Nodes[parentCount - 1].Nodes[childCount].Checked = true;
                        }
                        childCount++;
                    }
                    else
                    {
                        parentStr = _data[i].SimplePath;
                        parentSimpleName = _data[i].SimpleName;
                        ObjectList.Nodes.Add(_data[i].SimpleName);
                        parentCount++;
                        if (_data[i].HideShow)
                        {
                            ObjectList.Nodes[parentCount - 1].Checked = true;
                        }
                        childCount = 0;
                    }
                }
            }
            else if (FilterType.SelectedIndex == 1)
            {
                IAgExecCmdResult result;
                StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
                List<string> usedObjects = new List<string>();
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Constellation");
                if (result[0] != "None")
                {
                    string[] constArray = result[0].Split(null);
                    foreach (var item in constArray)
                    {
                        string simplePath = mStkObjectsLibrary.SimplifiedObjectPath(item);                       
                        string newItem = item.Split('/').Last();
                        if (newItem != "" && newItem != null)
                        {
                            ObjectList.Nodes.Add(newItem);
                            parentCount++; 
                            IAgConstellation currentConst = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgConstellation;
                            IAgObjectLinkCollection objects = currentConst.Objects;
                            childCount = 0;
                            if (objects.Count!=0)
                            {
                                foreach (IAgObjectLink thing in objects)
                                {
                                    ObjectList.Nodes[parentCount-1].Nodes.Add(thing.Name);
                                    childCount++;
                                    ObjectData element = _data.Find(p => p.SimpleName == thing.Name);
                                    if (element!=null)
                                    {
                                        if (element.HideShow)
                                        {
                                            ObjectList.Nodes[parentCount - 1].Nodes[childCount - 1].Checked = true;
                                        }
                                    }
                                    usedObjects.Add(thing.Name);
                                }
                            }
                        }
                    }
                    ObjectList.Nodes.Add("No Known Constellation");
                    parentCount++;
                    childCount = 0;
                    foreach (var item in _data)
                    {
                        if (!usedObjects.Contains(item.SimpleName))
                        {
                            ObjectList.Nodes[parentCount - 1].Nodes.Add(item.SimpleName);
                            childCount++;
                            if (item.HideShow)
                            {
                                ObjectList.Nodes[parentCount - 1].Nodes[childCount - 1].Checked = true;
                            }
                        }
                    }
                }
            }
        }

        private void FilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilterType.SelectedIndex!=-1)
            {
                ObjectList.Nodes.Clear();
                PopulateList();
            }
        }

        private void ObjectList_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void ObjectList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
        }

        private void ObjectList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
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
}
