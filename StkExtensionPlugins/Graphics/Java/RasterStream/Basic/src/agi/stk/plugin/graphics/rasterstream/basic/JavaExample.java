package agi.stk.plugin.graphics.rasterstream.basic;

// AGI Java API
import java.io.*;

import agi.core.*;
import agi.stkutil.*;
import agi.stk.plugin.util.*;
import agi.stk.plugin.graphics.*;

public class JavaExample
implements IAgStkGraphicsPluginRasterStream, IAgStkGraphicsPluginWithSite
{
	protected IAgStkGraphicsPluginSite m_IAgStkGraphicsPluginSite;

	private String				m_RasterPath;
	private GifProvider			m_GifProvider;
	private IAgDate				m_LastTime;

	public JavaExample()
	{
	}
	
	//=============================================================
	// For IDispatch access of configuration through Plugin Driver
	//=============================================================
	public void setRasterPath(AgVariant value)
	{
		try
		{
			this.m_RasterPath = value.getString();
		}
		catch(Throwable t)
		{
			t.printStackTrace();
			throw new RuntimeException(t);
		}
	}

	//==============================
	// IAgStkGraphicsPluginWithSite
	//==============================
	public void onStartUp(IAgStkGraphicsPluginSite site)
	throws AgCoreException
	{
		try
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onStartUp ENTER");
			this.m_IAgStkGraphicsPluginSite = site;
		}
		finally
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onStartUp EXIT");
		}
	}

	public void onShutDown()
	throws AgCoreException
	{
		try
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onShutDown ENTER");
			if(this.m_IAgStkGraphicsPluginSite != null)
			{
				this.m_IAgStkGraphicsPluginSite.release();
				this.m_IAgStkGraphicsPluginSite = null;
			}
		}
		finally
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onShutDown EXIT");
		}
	}

	//==============================
	// IAgStkGraphicsPluginRasterStream
	//==============================

	public boolean onGetRasterAttributes(IAgStkGraphicsPluginRasterStreamAttributes attributes)
	throws AgCoreException
	{
		boolean result = false;
		try
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onGetRasterAttributes ENTER");

			// Set gifProvider for the raster
			this.m_GifProvider = new GifProvider(this.m_RasterPath);

			double width = this.m_GifProvider.getSize().getWidth();
			double height = this.m_GifProvider.getSize().getHeight();
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "width="+width);
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "height="+height);
			
			// Assign the raster's attributes
			attributes.setWidth((int)width);
			attributes.setHeight((int)height);
			attributes.setRasterFormat(AgEStkGraphicsPluginRasterFormat.E_STK_GRAPHICS_PLUGIN_RASTER_FORMAT_RGBA);
			attributes.setRasterType(AgEStkGraphicsPluginRasterType.E_STK_GRAPHICS_PLUGIN_RASTER_TYPE_UNSIGNED_BYTE);

			result = true;
		}
		catch(Throwable t)
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, AgCoreException.buildStackMessage(t, true));
			result = false;
		}
		finally
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onGetRasterAttributes EXIT");
		}
		return result;
	}

	public boolean onGetNextRaster(IAgDate time, IAgDate nextTime, IAgStkGraphicsPluginRasterStreamContext context)
	throws AgCoreException
	{
		boolean result = false;

		try
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onGetNextRaster ENTER");

			if(time != null) message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "time = "+time.getOLEDate());
			if(nextTime != null) message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "nextTime = "+nextTime.getOLEDate());
			if(m_LastTime != null) message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "lastTime = "+m_LastTime.getOLEDate());

			if(this.m_LastTime == null)
			{
				this.m_LastTime = time.subtract("sec", 1.0);
			}
	
			// Only update if animating
			if(time.getOLEDate() > this.m_LastTime.getOLEDate())
			{
				int[] values = this.m_GifProvider.nextFrameAsBitmapData().getIntData();
				if(values != null)
				{
					ByteArrayOutputStream baos = new ByteArrayOutputStream();
					DataOutputStream dos = new DataOutputStream(baos);
					for(int i = 0; i < values.length; ++i)
					{
						//http://www.javaranch.com/journal/200406/ScjpTipLine-BitShifting.html
						// setFromMemory() is expecting RGBA, and we have ARGB
						// so get alpha, shift by a byte, and add alpha to the right
						int value = values[i];
						if(value != 0)
						{
							int alpha = value >>> 24;	// unsigned right shift and hence always fill zeros to the left
							int newRGB = value << 8;	// always inserts 0 to the right hand side
							int newValue = newRGB | alpha;
							// Uncomment if you wish to see the raw data conversion
							//System.out.println("value="+Integer.toHexString(value)+" alpha="+Integer.toHexString(alpha)+" newRGB="+Integer.toHexString(newRGB)+"  =>   newValue="+Integer.toHexString(newValue));
							//System.out.println("value="+Integer.toBinaryString(value)+" alpha="+Integer.toBinaryString(alpha)+" newRGB="+Integer.toBinaryString(newRGB)+"  =>   newValue="+Integer.toBinaryString(newValue));
							value = newValue;
						}
						dos.writeInt(value);
					}
					byte[] ba = baos.toByteArray();
	
					// Expects RGBA
					if(ba != null)
					{
						//System.out.println(ba.length);
						context.getRasterAsBits().setFromMemory(ba.length, ba);
					}
				}
				this.m_LastTime = time;
			}
			else if(time.getOLEDate() < this.m_LastTime.getOLEDate())
			{
				int[] values = this.m_GifProvider.previousFrameAsBitmapData().getIntData();
				if(values != null)
				{
					ByteArrayOutputStream baos = new ByteArrayOutputStream();
					DataOutputStream dos = new DataOutputStream(baos);
					for(int i = 0; i < values.length; ++i)
					{
						//http://www.javaranch.com/journal/200406/ScjpTipLine-BitShifting.html
						// setFromMemory() is expecting RGBA, and we have ARGB
						// so get alpha, shift by a byte, and add alpha to the right
						int value = values[i];
						if(value != 0)
						{
							int alpha = value >>> 24;	// unsigned right shift and hence always fill zeros to the left
							int newRGB = value << 8;	// always inserts 0 to the right hand side
							int newValue = newRGB | alpha;
							// Uncomment if you wish to see the raw data conversion
							//System.out.println("value="+Integer.toHexString(value)+" alpha="+Integer.toHexString(alpha)+" newRGB="+Integer.toHexString(newRGB)+"  =>   newValue="+Integer.toHexString(newValue));
							//System.out.println("value="+Integer.toBinaryString(value)+" alpha="+Integer.toBinaryString(alpha)+" newRGB="+Integer.toBinaryString(newRGB)+"  =>   newValue="+Integer.toBinaryString(newValue));
							value = newValue;
						}

						dos.writeInt(value);
					}
					byte[] ba = baos.toByteArray();
	
					// Expects RGBA
					if(ba != null)
					{
						//System.out.println(ba.length);
						context.getRasterAsBits().setFromMemory(ba.length, ba);
					}
				}
				this.m_LastTime = time;
			}
	
			result = true;
		}
		catch(Throwable t)
		{
			t.printStackTrace();
			message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, AgCoreException.buildStackMessage(t, true));
			result = false;
		}
		finally
		{
			message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, "onGetNextRaster EXIT");
		}
		
		return result;
	}
	
	/**
	 * Logs a message to the STK Message Viewer
	 * @param severity One of the final static members of AgEUtLogMsgType.
	 * @param msg The message to display in the message viewer.
	 * @throws AgCoreException If an error occurred while logging the message. 
	 */
	protected void message(AgEUtLogMsgType severity, String msg)
	throws AgCoreException
	{
		message(severity, 0, "unknown", 0, msg);
	}

	protected void message(AgEUtLogMsgType severity, int errorCode, String sourceFile, int lineNo, String msg)
	throws AgCoreException
	{
		StringBuilder sb = new StringBuilder();
		sb.append("message [");
		sb.append(" severity = "+severity);
		sb.append(" errorCode = "+errorCode);
		sb.append(" sourceFile = "+sourceFile);
		sb.append(" lineNo = "+lineNo);
		sb.append(" msg = "+msg);
		sb.append("]");
		
		if(severity.equals(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM) ||
			severity.equals(AgEUtLogMsgType.E_UT_LOG_MSG_WARNING))
		{
			System.err.println(sb.toString());
		}
		else
		{
			if(severity.compareTo(AgEUtLogMsgType.E_UT_LOG_MSG_INFO) > 0)
			{
				System.out.println(sb.toString());
			}
		}
	}
}