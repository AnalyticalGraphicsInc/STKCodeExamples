using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsToolbox.SatelliteCreator
{
    public partial class FilterForm : Form
    {
        public FilterConfig fconfig;
        public NewAssetForm baseForm;
        private bool inside;
        private List<string> options;
        private bool onstart;
        public FilterForm(FilterConfig config, List<string> filterTypes, NewAssetForm form)
        {
            InitializeComponent();
            fconfig = config;
            inside = false;
            baseForm = form;
            options = filterTypes;
            onstart = true;
            FilterOptionTypes.Items.Add("None");
            foreach (var item in filterTypes)
            {
                FilterOptionTypes.Items.Add(item);
                if (config.FilterType != null)
                {
                    if (item == config.FilterType)
                    {
                        FilterOptionTypes.SelectedItem = item;
                    }
                }
            }
            if (FilterOptionTypes.SelectedIndex == -1)
            {
                FilterOptionTypes.SelectedIndex = 0;
            }
            if (config.IsActive && FilterOptionTypes.SelectedIndex != 0)
            {
                OptionList.Items.Clear();
                PopulateTable(fconfig.FilterMetadataID, fconfig.SelectedOptions);
            }
            onstart = false;
        }

        //protected override void OnShown(EventArgs e)
        //{
        //    base.OnShown(e);
        //    this.Capture = true;
        //    //this.Activate();
        //}

        //protected override void OnMouseCaptureChanged(EventArgs e)
        //{
        //    if (!this.Capture)
        //    {
        //        if (!this.RectangleToScreen(this.DisplayRectangle).Contains(Cursor.Position))
        //        {
        //            this.Close();
        //        }
        //        else
        //        {
        //            this.Capture = true;
        //        }
        //    }
        //    base.OnMouseCaptureChanged(e);
        //}

        private void FilterOptionTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilterOptionTypes.SelectedIndex != -1)
            {
                OptionList.Items.Clear();
                if (FilterOptionTypes.Text.Contains("None"))
                {
                    fconfig.IsActive = false;
                    fconfig.FilterMetadataID = 0;
                }
                else
                {
                    fconfig.FilterMetadataID = FilterOptionTypes.SelectedIndex;
                    fconfig.IsActive = true;
                }
                if (!onstart)
                {
                    fconfig.SelectedOptions.Clear();
                }
                fconfig.FilterType = FilterOptionTypes.Text;
                PopulateTable(fconfig.FilterMetadataID, fconfig.SelectedOptions);
                baseForm.PopulateSatelliteList();
            }
        }
        
        private void PopulateTable(int MetadataID, List<string> selectedOptions)
        {
            List<string> possibleOptions = null;
            switch (MetadataID)
            {
                case 1:
                    possibleOptions = CommonData.MetadataOptions1;
                    break;
                case 2:
                    possibleOptions = CommonData.MetadataOptions2;
                    break;
                case 3:
                    possibleOptions = CommonData.MetadataOptions3;
                    break;
                case 4:
                    possibleOptions = CommonData.MetadataOptions4;
                    break;
                case 5:
                    possibleOptions = CommonData.MetadataOptions5;
                    break;
                case 6:
                    possibleOptions = CommonData.SatCatFofo;
                    break;
                default:
                    break;
            }
            if (possibleOptions != null)
            {
                for (int i = 0; i < possibleOptions.Count; i++)
                {
                    if (selectedOptions != null)
                    {
                        if (selectedOptions.Contains(possibleOptions[i]))
                        {
                            OptionList.Items.Add(possibleOptions[i], true);
                        }
                        else
                        {
                            OptionList.Items.Add(possibleOptions[i], false);
                        }
                    }
                    else
                    {
                        OptionList.Items.Add(possibleOptions[i], false);
                    }
                }
            }
        }

        private void OptionList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!onstart)
            {
                fconfig.SelectedOptions.Clear();
                //Preexisting checks
                foreach (string item in OptionList.CheckedItems)
                {
                    fconfig.SelectedOptions.Add(item);
                }
                //Handle new check
                if (e.NewValue == CheckState.Checked)
                {
                    fconfig.SelectedOptions.Add(OptionList.Items[e.Index].ToString());
                }
                else
                {
                    if (fconfig.SelectedOptions.Contains(OptionList.Items[e.Index].ToString()))
                    {
                        fconfig.SelectedOptions.Remove(OptionList.Items[e.Index].ToString());
                    }
                }
                //Populate main table based on changes
                baseForm.PopulateSatelliteList();
            }
        }

        private void FilterForm_MouseEnter(object sender, EventArgs e)
        {
            inside = true;
        }

        private void FilterForm_MouseLeave(object sender, EventArgs e)
        {
            if (this.GetChildAtPoint(this.PointToClient(Cursor.Position)) != null)
            {

            }
            else
            {
                this.Close();
            }
        }

        private void FilterForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.DarkRed,3), this.DisplayRectangle);
        }
    }
}
