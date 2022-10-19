using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using AGI.STKGraphics.Plugins;

namespace OpenStreetMapPlugin
{
    internal class OpenStreetMapTilePlugin : IAgStkGraphicsPluginTile
    {
        public OpenStreetMapTilePlugin(Array extent, int width, int height, short children, string data)
        {
            m_extent = extent;
            m_width = width;
            m_height = height;
            m_children = children;
            m_data = data;
        }

        public short Children
        {
            get { return m_children; }
        }

        public string Data
        {
            get { return m_data; }
        }

        public Array Extent
        {
            get { return m_extent; }
        }

        public int Height
        {
            get { return m_height; }
        }

        public int Width
        {
            get { return m_width; }
        }

        private Array m_extent;
        private int m_width;
        private int m_height;
        private short m_children;
        private string m_data;
    }
}