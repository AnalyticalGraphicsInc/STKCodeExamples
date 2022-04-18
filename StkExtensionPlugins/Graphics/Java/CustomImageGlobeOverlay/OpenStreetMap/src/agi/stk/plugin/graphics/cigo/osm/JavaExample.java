package agi.stk.plugin.graphics.cigo.osm;

// AGI Java API
import agi.core.*;
import agi.stk.plugin.graphics.*;

public class JavaExample
implements IAgStkGraphicsPluginCustomImageGlobeOverlay, IAgStkGraphicsPluginWithSite
{
    private Object 								m_extent;
    private OpenStreetMapTiler 					m_tiler;
    private AgEStkGraphicsPluginMapProjection	m_projection;
    private String 								m_ServerURL;

    public JavaExample()
    {
		//System.out.println("-> <init> enter");
		//System.out.println(Thread.currentThread().getName());

		this.m_ServerURL = "http://tile.openstreetmap.org";
    	this.m_projection = AgEStkGraphicsPluginMapProjection.E_STK_GRAPHICS_PLUGIN_MAP_PROJECTION_MERCATOR;
    	this.m_extent = new Object[]
        {
            -Math.PI,
            -85.0511 * Math.PI / 180.0,
            Math.PI,
            85.0511 * Math.PI / 180.0
        };
    	
    	this.m_tiler = new OpenStreetMapTiler(this.m_extent);

    	//System.out.println("<- <init> exit");
    }

	public void onStartUp(IAgStkGraphicsPluginSite arg0) 
	throws AgCoreException 
	{
		//System.out.println("-> onStartup enter");
		//System.out.println(Thread.currentThread().getName());
		//System.out.println("<- onStartup exit");
	}

	public void onShutDown() 
	throws AgCoreException 
	{
		//System.out.println("-> onShutDown enter");
		//System.out.println(Thread.currentThread().getName());
		//System.out.println("<- onShutDown exit");
	}

	public void onInitialize(IAgUnknown pScene, IAgStkGraphicsPluginCustomImageGlobeOverlayContext pContext)
    throws AgCoreException
    {
		//System.out.println("-> onInitialize enter");
		//System.out.println(Thread.currentThread().getName());

		pContext.setExtent(this.m_extent);
        pContext.setProjection(this.m_projection);
        pContext.setTiler(this.m_tiler);

		//System.out.println("<- onInitialize exit");
    }

    public void onUninitialize(IAgUnknown pScene, IAgStkGraphicsPluginCustomImageGlobeOverlayContext pContext)
    throws AgCoreException
    {
		//System.out.println("-> onUninitialize enter");
		//System.out.println(Thread.currentThread().getName());
		//System.out.println("<- onUninitialize exit");
    }

    public boolean read(AgSafeArray extent, String data, int width, int height, IAgStkGraphicsPluginCustomImageGlobeOverlayContext contextStrat)
	throws AgCoreException
	{
    	boolean goodRead = false;
	
    	try
	    {
//    		System.out.println("-> read enter");
//    		System.out.println(Thread.currentThread().getName());
//    		System.out.println("read data="+data);
   		
    		if (width == this.m_tiler.getRootTile().getWidth() &&
	        	height == this.m_tiler.getRootTile().getHeight())
	        {
   				byte[] values = OpenStreetMapWebSvcClient.writeTileToByteArray(data);
//     			System.out.println("got bytes of "+values.length);
   				if(values != null)
   				{
//        			System.out.println("setting from memory "+values.length);
   					contextStrat.getRasterAsBits().setFromMemory(values.length, values);
//        			System.out.println("set from memory "+values.length);
   					goodRead = true;
   				}
	        }
    		else
    		{
    			System.out.println("width and height were not equal");
    		}
    	}
    	catch (Throwable t)
	    {
    		t.printStackTrace();
    		goodRead = false;
    	}
    	finally
    	{
//    		System.out.println("<- read exit");
    	}
	
	    return goodRead;
	}
    
    public String getURI()
    throws AgCoreException
    {
//		System.out.println("-> getURI enter");
//		System.out.println(Thread.currentThread().getName());
//		System.out.println("<- getURI exit");
        return this.m_ServerURL;
    }
}