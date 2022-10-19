using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Agi.UiPlugin.MoveMtoTime
{
    internal class OlePictureHelper
    {
        private static Guid picture_guid;

        static OlePictureHelper()
        {
            picture_guid = typeof(stdole.IPicture).GUID;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class PICTDESCbmp
        {
            internal int cbSizeOfStruct;
            internal int picType;
            internal IntPtr hbitmap;
            internal IntPtr hpalette;
            internal int unused;
            public PICTDESCbmp(Bitmap bitmap)
            {
                this.cbSizeOfStruct = Marshal.SizeOf(typeof(PICTDESCbmp));
                this.picType = 1;
                this.hbitmap = IntPtr.Zero;
                this.hpalette = IntPtr.Zero;
                this.hbitmap = bitmap.GetHbitmap();
            }
        }
        [DllImport("oleaut32.dll", EntryPoint = "OleCreatePictureIndirect", ExactSpelling = true, PreserveSig = false)]
        internal static extern stdole.IPicture OleCreateIPictureIndirect([MarshalAs(UnmanagedType.AsAny)] object pictdesc, ref Guid iid, bool fOwn);


        internal static stdole.IPictureDisp OlePictureFromImage(Image image)
        {
            Bitmap bmp;
            if ((bmp = image as Bitmap) != null)
            {
                return OleCreateIPictureIndirect(new PICTDESCbmp(bmp), ref picture_guid, true) as stdole.IPictureDisp;
            }
            return null;
        }
    }
}
