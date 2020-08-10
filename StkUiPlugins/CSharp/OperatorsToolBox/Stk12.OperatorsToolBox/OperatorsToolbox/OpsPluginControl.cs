using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsToolbox
{
    public class OpsPluginControl : UserControl
    {
        public delegate void PanelCloseHandler(UserControl sender);

        public event PanelCloseHandler PanelClose;

        protected virtual void RaisePanelCloseEvent()
        {
            PanelClose?.Invoke(this);
        }
    }
}
