using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AGI.STKX.Controls;

namespace WPFEngineApplication
{
    public partial class MapControl : UserControl
    {
        public AxAgUiAx2DCntrl Map
        {
            get
            {
                return axAgUiAx2DCntrl1;
            }
        }

        public MapControl()
        {
            InitializeComponent();
        }
    }
}
