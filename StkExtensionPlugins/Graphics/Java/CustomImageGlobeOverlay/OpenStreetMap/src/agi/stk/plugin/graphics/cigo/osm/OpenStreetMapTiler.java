package agi.stk.plugin.graphics.cigo.osm;

import agi.core.*;
import agi.stk.plugin.graphics.*;

public class OpenStreetMapTiler 
implements IAgStkGraphicsPluginTiler
{
    private final static int 	s_DefaultTileSize = 256;
    private final static int 	s_DefaultMaxLevels = 19;
    private final static short 	s_DefaultChildren = 4;
    private final static String s_DefaultData = "0/0/0";
    
    private OpenStreetMapTile 	m_rootTile;
    
    public OpenStreetMapTiler(Object extent)
    {
    	try
    	{
//			System.out.println("-> OpenStreetMapTiler.<init> enter");
	this.m_rootTile = new OpenStreetMapTile(extent, s_DefaultTileSize, s_DefaultTileSize, s_DefaultChildren, s_DefaultData);
		}
		finally
		{
//	        System.out.println("<- OpenStreetMapTiler.<init> exit");
		}
    }

    public IAgStkGraphicsPluginTile getRootTile()
	throws AgCoreException
    {
    	try
    	{
//			System.out.println("-> OpenStreetMapTiler.getRootTile enter");
//			System.out.println(Thread.currentThread().getName());
	        return this.m_rootTile;
    	}
    	finally
    	{
//            System.out.println("<- OpenStreetMapTiler.getRootTile exit");
    	}
    }

    public void getTiles(IAgStkGraphicsPluginTile parentTile, IAgStkGraphicsPluginTileCollection tiles)
	throws AgCoreException
    {
    	try
    	{
//			System.out.println("-> OpenStreetMapTiler.getTiles enter");
//			System.out.println(Thread.currentThread().getName());

			String data = parentTile.getData();
	        int firstSlash = data.indexOf('/');
	        int lastSlash = data.lastIndexOf('/');
		
	        int zoom = Integer.parseInt(data.substring(0, firstSlash));
	        int x = Integer.parseInt(data.substring(firstSlash + 1, lastSlash));
	        int y = Integer.parseInt(data.substring(lastSlash + 1));
		
	        tiles.add(createTile(x << 1, y << 1, zoom + 1));
	        tiles.add(createTile(x << 1 | 1, y << 1, zoom + 1));
	        tiles.add(createTile(x << 1, y << 1 | 1, zoom + 1));
	        tiles.add(createTile(x << 1 | 1, y << 1 | 1, zoom + 1));
    	}
    	finally
    	{
//            System.out.println("<- OpenStreetMapTiler.getTiles exit");
    	}
    }

    private IAgStkGraphicsPluginTile createTile(int x, int y, int zoom)
	throws AgCoreException
    {
    	try
    	{
//			System.out.println("-> OpenStreetMapTiler.createTile enter");
//			System.out.println(Thread.currentThread().getName());

	        // Latitude
	        double invZoom = 4.0 * Math.PI / (1 << zoom);
	        double k = Math.exp((2.0 * Math.PI) - (y * invZoom));
	        double north = Math.asin((k - 1.0) / (k + 1.0));
	        k = Math.exp((2.0 * Math.PI) - ((y + 1) * invZoom));
	        double south = Math.asin((k - 1.0) / (k + 1.0));

	        // Longitude
	        invZoom = Math.PI / (1 << (zoom - 1));
	        double west = ((double)(x) * invZoom) - Math.PI;
	        double east = ((double)(x + 1) * invZoom) - Math.PI;

	        short numChildren = 0;

	        if (zoom + 1 < s_DefaultMaxLevels)
	        {
	            numChildren = 4;
	        }

	        Object extent = new Object[] { west, south, east, north };

	        OpenStreetMapTile tile = null;
	        tile = new OpenStreetMapTile(
	            extent,
	            s_DefaultTileSize,
	            s_DefaultTileSize,
	            numChildren,
	            zoom + "/" + x + "/" + y);
	        
	        return tile;
    	}
    	finally
    	{
//            System.out.println("<- OpenStreetMapTiler.createTile exit");
    	}
    }
}