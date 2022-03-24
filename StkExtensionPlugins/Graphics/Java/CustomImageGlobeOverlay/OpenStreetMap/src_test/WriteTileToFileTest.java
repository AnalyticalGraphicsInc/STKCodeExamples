// Sample API
import agi.stk.plugin.graphics.cigo.osm.*;

public class WriteTileToFileTest 
{
    public static void main(String[] args)
    throws Throwable
	{
    	int zoom = 0;
    	int x = 0;
    	int y = 0;
    	
    	if(args.length == 3)
    	{
    		zoom = Integer.parseInt(args[0]);
    		x = Integer.parseInt(args[1]);
    		y = Integer.parseInt(args[2]);
    	}

    	String tileData = zoom + "/" + x +"/" + y;
    	
    	String userDir = System.getProperty("user.dir");
    	String fileSep = System.getProperty("file.separator");
    	String filePath = userDir + fileSep + zoom+"_"+x+"_"+y + ".png";

    	OpenStreetMapWebSvcClient.writeTileToFile(tileData, filePath);
	}
}
