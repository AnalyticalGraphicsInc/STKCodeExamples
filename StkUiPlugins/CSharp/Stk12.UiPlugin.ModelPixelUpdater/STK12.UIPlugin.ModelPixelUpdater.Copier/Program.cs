using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace STK12.UIPlugin.ModelPixelUpdater.Copier
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
            Application.Run(new SetupForm());
#else
            Application.Run(new InstallForm());
#endif
        }
    }
}
