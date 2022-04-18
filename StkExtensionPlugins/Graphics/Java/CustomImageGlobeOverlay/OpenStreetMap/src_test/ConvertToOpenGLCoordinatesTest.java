import agi.stk.plugin.graphics.cigo.osm.OpenStreetMapWebSvcClient;

public class ConvertToOpenGLCoordinatesTest 
{
	public static void main(String[] args)
	{
		byte[] in = new byte[]
		{
			 0, 1, 2, 3, 4, 5, 6, 7,
			 8, 9,10,11,12,13,14,15,
		};
		
		OpenStreetMapWebSvcClient.convertToOpenGLCoordinates(in, 4, 2, 2);

		System.out.println("done");
	}
}
