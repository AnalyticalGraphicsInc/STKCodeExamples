using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using AGI.STKGraphics.Plugins;

namespace OpenStreetMapPlugin
{
    internal class OpenStreetMapTilerPlugin : IAgStkGraphicsPluginTiler
    {
        public OpenStreetMapTilerPlugin(Array extent)
        {
            m_extent = extent;
            m_rootTile = new OpenStreetMapTilePlugin(extent, m_tileSize, m_tileSize, 4, "0/0/0");
        }

        public IAgStkGraphicsPluginTile GetRootTile()
        {
            return m_rootTile;
        }

        public void GetTiles(IAgStkGraphicsPluginTile parentTile, IAgStkGraphicsPluginTileCollection tiles)
        {
            string data = parentTile.Data;
            int firstSlash = data.IndexOf('/');
            int lastSlash = data.LastIndexOf('/');

            int zoom = Int32.Parse(data.Substring(0, firstSlash));
            int x = Int32.Parse(data.Substring(firstSlash + 1, lastSlash - firstSlash - 1));
            int y = Int32.Parse(data.Substring(lastSlash + 1));

            tiles.Add(CreateTile(x << 1, y << 1, zoom + 1));
            tiles.Add(CreateTile(x << 1 | 1, y << 1, zoom + 1));
            tiles.Add(CreateTile(x << 1, y << 1 | 1, zoom + 1));
            tiles.Add(CreateTile(x << 1 | 1, y << 1 | 1, zoom + 1));
        }

        private IAgStkGraphicsPluginTile CreateTile(int x, int y, int zoom)
        {
            //
            // Latitude
            //
            double invZoom = 4.0 * Math.PI / (1 << zoom);
            double k = Math.Exp((2.0 * Math.PI) - (y * invZoom));
            double north = Math.Asin((k - 1.0) / (k + 1.0));
            k = Math.Exp((2.0 * Math.PI) - ((y + 1) * invZoom));
            double south = Math.Asin((k - 1.0) / (k + 1.0));

            //
            // Longitude
            //
            invZoom = Math.PI / (1 << (zoom - 1));
            double west = ((double)(x) * invZoom) - Math.PI;
            double east = ((double)(x + 1) * invZoom) - Math.PI;

            short numChildren = 0;

            if (zoom + 1 < m_maxLevels)
            {
                numChildren = 4;
            }

            Array extent = new object[] { west, south, east, north };

            return new OpenStreetMapTilePlugin(
                extent,
                m_tileSize,
                m_tileSize,
                numChildren,
                zoom + "/" + x + "/" + y);
        }

        private Array m_extent;
        private OpenStreetMapTilePlugin m_rootTile;
        static private readonly int m_tileSize = 256;
        static private readonly int m_maxLevels = 19;

    }
}