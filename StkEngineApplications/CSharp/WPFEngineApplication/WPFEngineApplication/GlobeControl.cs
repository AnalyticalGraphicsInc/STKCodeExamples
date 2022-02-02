using System.Windows.Forms;
using AGI.STKX.Controls;

namespace WPFEngineApplication
{
    public partial class GlobeControl : UserControl
    {
        public GlobeControl()
        {
            InitializeComponent();
        }

        public AxAgUiAxVOCntrl Globe
        {
            get { return axAgUiAxVOCntrl1; }
        }
    }
}
