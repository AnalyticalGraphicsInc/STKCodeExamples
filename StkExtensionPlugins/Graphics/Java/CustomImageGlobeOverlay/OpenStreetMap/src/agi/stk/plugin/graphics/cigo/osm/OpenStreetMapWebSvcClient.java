package agi.stk.plugin.graphics.cigo.osm;

// Java API
import java.io.*;
import java.net.*;
import java.awt.*;
import java.awt.image.*;
import javax.imageio.*;

public class OpenStreetMapWebSvcClient 
{
    private static final String s_SERVER_URL = "http://tile.openstreetmap.org";

    private static HttpURLConnection connect(String tileData)
    throws Exception
    {
		String tileURL = buildTileURL(tileData);
        
		URL url = new URL(tileURL);
        
        URLConnection uc = url.openConnection();

        HttpURLConnection huc = (HttpURLConnection)uc;
        huc.setRequestMethod("GET");
        huc.setRequestProperty("UserAgent", "STKEngine/10.0");
        huc.setConnectTimeout(60000);
        
        int code = huc.getResponseCode();
        if(code != HttpURLConnection.HTTP_OK) throw new Exception("Response Code was "+code);
        
        String response = huc.getResponseMessage();
        if(response == null) throw new Exception("Response Message was null");
        
        int contentLength = huc.getContentLength();
        if(contentLength < 1) throw new Exception("Content Length was not valid");

        String type = huc.getContentType();
        if(type == null) throw new Exception("Content Type was null");

        return huc;
    }

    private static String buildTileURL(String tileData)
    {
    	StringBuffer sb = new StringBuffer();
    	sb.append(s_SERVER_URL);
    	sb.append("/");
    	sb.append(tileData);
    	sb.append(".png");
    	String url = sb.toString();
		//System.out.println(url);
		return url;
    }
	
	public static void writeTileToFile(String tileData, String filePath)
    throws Exception
	{
		HttpURLConnection huc = connect(tileData);

        BufferedInputStream bis = new BufferedInputStream(huc.getInputStream());
        BufferedOutputStream bos = new BufferedOutputStream(new FileOutputStream(filePath));

        byte[] bytes = new byte[huc.getContentLength()];
        int numBytes = bis.read(bytes);
        int totalBytesRead = numBytes;
        while(numBytes != -1)
        {
           bos.write(bytes);
           numBytes = bis.read(bytes);
           totalBytesRead = totalBytesRead + numBytes;
        }

        System.out.println(totalBytesRead);
        
        bis.close();
        bos.close();
	}

	// NOTE: Retrieving the Tile via REST request as a BufferedImage takes most
	// of the computation time, the the conversions of the 
	// byte arrays are negligible web request.
	// Timing code is provided but commented out for your use if needed.
	public static byte[] writeTileToByteArray(String tileData)
    throws Exception
	{
		//System.out.println(Thread.currentThread().getName());
		
		//long totalstart = System.currentTimeMillis();
		
		//long webstart = System.currentTimeMillis();

		BufferedImage bi = null;
		bi = ImageIO.read(new URL(buildTileURL(tileData)));
		
		//long webstop = System.currentTimeMillis();
		//long webtime = webstop - webstart;
		//System.out.println("The Get OSM Tile took:");
		//System.out.println("Web Time (ms): "+webtime);
		
		//long convertstart = System.currentTimeMillis();
		byte[] values = to32BppABGR(bi);
		//long convertstop = System.currentTimeMillis();
		//long converttime = convertstop - convertstart;
		//System.out.println("The convert buffer took:");
		//System.out.println("Covnert Total Time (ms): "+converttime);
		
		if(values != null)
		{
			// setFromMemory() is expecting RGBA, and we have ABGR
			// so swap bytes around
			//long reorderstart = System.currentTimeMillis();
			for(int i = 0; i < values.length; i=i+4)
			{
				byte alpha = values[i];		// values Alpha
				byte blue = values[i+1];	// values Blue 
				byte green = values[i+2];	// values Green
				byte red = values[i+3];		// values Red

				values[i] = red;
				values[i+1] = green;
				values[i+2] = blue;
				values[i+3] = alpha;
			}
			//long reorderstop = System.currentTimeMillis();
			//long reordertime = reorderstop - reorderstart;
			//System.out.println("The reorder buffer took:");
			//System.out.println("Reorder Total Time (ms): "+reordertime);
		}

		//long totalstop = System.currentTimeMillis();
		//long totaltime = totalstop - totalstart;
		//System.out.println("The Get OSM Tile and convert to proper byte[] took:");
		//System.out.println("Total Time (ms): "+totaltime);
		return values;
	}

	public static BufferedImage writeTileToBufferedImage(String tileData)
    throws Exception
	{
		return ImageIO.read(new URL(buildTileURL(tileData)));
	}
	
	private static byte[] to32BppABGR(BufferedImage image)
	throws Exception
	{
		byte[] result = null;
		
		// What Java generally calls "BGR" order (meaning that the bytes appear
		// in the order B, G, R) is what .NET (and therefore the Mummra layer)
		// calls RGB. So these mapping sort of look backwards.
		int imageType = image.getType();

		if(imageType == BufferedImage.TYPE_BYTE_INDEXED || 
			imageType == BufferedImage.TYPE_BYTE_BINARY)
		{
			int w = image.getWidth();
			int h = image.getHeight();
			
			BufferedImage convertedImage = null;
			convertedImage = new BufferedImage(w, h, BufferedImage.TYPE_4BYTE_ABGR);

			Graphics gfx = null;
			gfx = convertedImage.getGraphics();
			gfx.drawImage(image, 0, 0, null);
			gfx.dispose();
			image = convertedImage;

			//====================================================================================
			// Tool to help demonstrate the location of 0,0 coordinate of OpenGL Tile
			// by visual inspection in the globe control (i.e. you will see a red square
			// of 100 x 100 pixels starting at the 0,0 coordinate and extending "Up" and "Right".
			//====================================================================================
			//int rgb = 0xFF0000;
			//	for(int i=0; i<100; i++)
			//{
			//	for(int j=0; j<100; j++)
			//	{
			//		image.setRGB(i, j, rgb);
			//	}
			//}
			
			DataBuffer buffer = image.getRaster().getDataBuffer();
			DataBufferByte dataBufferByte = (DataBufferByte)buffer;
			result = dataBufferByte.getData();
			
			result = convertToOpenGLCoordinates(result, 4, w, h);
		}
		else
		{
			throw new Exception("imageType was not TYPE_BYTE_INDEXED but was "+imageType);
		}
		
		return result;
	}

	//===============================================================================
	// To display properly in STK, we must convert the byte order to match the pixel
	// coordinate orientation difference between the BufferedImage and the OpenGL.
	//===============================================================================
	//
	//  BufferedImage coordinates...                   OpenGL coordinates ...
	//
	//    0
	//  0 +------------+                               +------------+
	//    |            |                               |            |
	//    |            |                               |            |
	//    |            |                               |            |
	//    |            |                               |            |
	//    +------------+                             0 +------------+
	//                                                 0
	//
	//============================================================================
	public static byte[] convertToOpenGLCoordinates(byte[] in, int bytesPerPixel, int width, int height)
	{
		byte[] out = new byte[in.length];

		int bytesPerRow = bytesPerPixel * width;

		int multiplier = width * 4;
		int outIndex = -1;
		for(int inIndex=0, outRowIndex=height-1, outColumnIndex=0; inIndex < in.length; inIndex++)
		{
			outIndex = (outRowIndex * multiplier) + outColumnIndex;

			byte temp = in[inIndex];
			out[outIndex] = temp;
			
			outColumnIndex++;
			
			if(outColumnIndex == bytesPerRow)
			{
				outRowIndex--;
				outColumnIndex = 0;
			}
		}
		
		return out;
	}
}
