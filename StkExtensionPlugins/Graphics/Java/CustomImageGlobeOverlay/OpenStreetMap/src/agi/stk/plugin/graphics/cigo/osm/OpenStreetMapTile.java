package agi.stk.plugin.graphics.cigo.osm;

import agi.core.*;
import agi.stk.plugin.graphics.*;

public class OpenStreetMapTile 
implements IAgStkGraphicsPluginTile
{
	private Object 		m_extent;
	private int 		m_width;
	private int 		m_height;
	private short 		m_children;
	private String 		m_data;

    public OpenStreetMapTile(Object extent, int width, int height, short children, String data)
    {
    	try
    	{
//			System.out.println("-> OpenStreetMapTile.<init> enter");
//			System.out.println(Thread.currentThread().getName());
	
			this.m_extent = extent;
	        this.m_width = width;
	        this.m_height = height;
	        this.m_children = children;
	        this.m_data = data;
    	}
    	finally
    	{
//    		System.out.println("<- OpenStreetMapTile.<init> exit");
    	}
    }

    public short getChildren()
	throws AgCoreException
    {
    	try
    	{
//			System.out.println("-> OpenStreetMapTile.getChildren enter");
//			System.out.println(Thread.currentThread().getName());
	        return this.m_children;
		}
		finally
		{
//	        System.out.println("<- OpenStreetMapTile.getChildren exit");
		}
    }

    public String getData()
	throws AgCoreException
    {
    	try
    	{
//			System.out.println("-> OpenStreetMapTile.getData enter");
//			System.out.println(Thread.currentThread().getName());
			return this.m_data;
    	}
    	finally
    	{
//            System.out.println("<- OpenStreetMapTile.getData exit");
    	}
    }

	public AgSafeArray getExtent() 
	throws AgCoreException 
	{
		try
		{
//			System.out.println("-> OpenStreetMapTile.getExtent enter");
//			System.out.println(Thread.currentThread().getName());
			return new AgSafeArray(this.m_extent);
		}
		finally
		{
//	        System.out.println("<- OpenStreetMapTile.getExtent exit");
		}
	}

    public int getHeight()
	throws AgCoreException
    {
    	try
    	{
//			System.out.println("-> OpenStreetMapTile.getHeight enter");
//			System.out.println(Thread.currentThread().getName());
	        return this.m_height;
    	}
    	finally
    	{
//            System.out.println("<- OpenStreetMapTile.getHeight exit");
    	}
    }

    public int getWidth()
	throws AgCoreException
    {
    	try
    	{
//			System.out.println("-> OpenStreetMapTile.getWidth enter");
//			System.out.println(Thread.currentThread().getName());
	        return this.m_width;
    	}
    	finally
    	{
//            System.out.println("<- OpenStreetMapTile.getWidth exit");
    	}
    }
}