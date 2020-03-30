using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace SendEphemerisToSTK
{
    class Program
    {
        
        static void Main(string[] args)
        {
           SendToStk.SendEphemeris(args, out string errors);

            if (string.IsNullOrEmpty(errors))
            {
                return;
            }

            MessageBox.Show(errors, "Error", MessageBoxButtons.OK);
        }

       
    }

}
