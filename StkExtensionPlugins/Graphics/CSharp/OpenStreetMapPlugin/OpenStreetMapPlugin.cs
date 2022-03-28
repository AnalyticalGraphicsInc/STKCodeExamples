using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using AGI.STKGraphics.Plugins;
using Microsoft.Win32;
using System.Text;

namespace OpenStreetMapPlugin
{
	[Guid("995A490F-595D-4bfc-A67F-76B419AADA9A")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("OpenStreetMapPlugin.CSharp")]
    public class OpenStreetMapPlugin : IAgStkGraphicsPluginCustomImageGlobeOverlay
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public OpenStreetMapPlugin()
        {
            m_server = "http://tile.openstreetmap.org";
            m_projection = AgEStkGraphicsPluginMapProjection.eStkGraphicsPluginMapProjectionMercator;
            m_extent = new object[]
            {
                -Math.PI,
                -85.0511 * Math.PI / 180.0,
                Math.PI,
                85.0511 * Math.PI / 180.0
            };
            m_tiler = new OpenStreetMapTilerPlugin(m_extent);
        }

        void IAgStkGraphicsPluginCustomImageGlobeOverlay.OnInitialize(object pScene, IAgStkGraphicsPluginCustomImageGlobeOverlayContext pContext)
        {
            pContext.Extent = m_extent;
            pContext.Projection = m_projection;
            pContext.Tiler = m_tiler;
        }

        void IAgStkGraphicsPluginCustomImageGlobeOverlay.OnUninitialize(object pScene, IAgStkGraphicsPluginCustomImageGlobeOverlayContext pContext)
        {
        }

        public bool Read(ref Array extent, string data, int width, int height, IAgStkGraphicsPluginCustomImageGlobeOverlayContext contextStrat)
        {
            if (width != m_tiler.GetRootTile().Width || height != m_tiler.GetRootTile().Height)
                return false;

            bool goodRead = false;
            try
            {
                string queryUrl = string.Concat(m_server, "/", data, ".png");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(queryUrl);
                request.Method = "GET";
                request.UserAgent = "Insight/1.0";
                request.Timeout = 10000;

                using (WebResponse response = request.GetResponse())
                {
                    if (response != null)
                    {
                        using (Bitmap bmp = new Bitmap(response.GetResponseStream()))
                        {
                            IntPtr hBitmap = bmp.GetHbitmap();
                            contextStrat.RasterAsBitmap.SetBitmap(hBitmap);
                            DeleteObject(hBitmap);

                            goodRead = true;
                        }
                    }
                }
            }
            catch (WebException)
            {
            }

            return goodRead;
        }

        public string GetURI()
        {
            return m_server;
        }

        #region Registration functions
        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        [ComRegisterFunction]
        [ComVisible(false)]
        public static void RegisterFunction(Type t)
        {
            RemoveOtherVersions(t);
        }

        /// <summary>
        /// Called when the assembly is unregistered for use from COM.
        /// </summary>
        /// <param name="t">The type exposed to COM.</param>
        [ComUnregisterFunctionAttribute]
        [ComVisible(false)]
        public static void UnregisterFunction(Type t)
        {
            // Do nothing.
        }

        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// Eliminates the other versions present in the registry for
        /// this type.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        public static void RemoveOtherVersions(Type t)
        {
            try
            {
                using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
                {
                    StringBuilder guidString = new StringBuilder("{");
                    guidString.Append(t.GUID.ToString());
                    guidString.Append("}");
                    using (RegistryKey guidKey = clsidKey.OpenSubKey(guidString.ToString()))
                    {
                        if (guidKey != null)
                        {
                            using (RegistryKey inproc32Key = guidKey.OpenSubKey("InprocServer32", true))
                            {
                                if (inproc32Key != null)
                                {
                                    string currentVersion = t.Assembly.GetName().Version.ToString();
                                    string[] subKeyNames = inproc32Key.GetSubKeyNames();
                                    if (subKeyNames.Length > 1)
                                    {
                                        foreach (string subKeyName in subKeyNames)
                                        {
                                            if (subKeyName != currentVersion)
                                            {
                                                inproc32Key.DeleteSubKey(subKeyName);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignore all exceptions...
            }
        }
        #endregion

        private readonly Array m_extent;
        private readonly OpenStreetMapTilerPlugin m_tiler;
        private readonly AgEStkGraphicsPluginMapProjection m_projection;
        private readonly string m_server;
    }
}
