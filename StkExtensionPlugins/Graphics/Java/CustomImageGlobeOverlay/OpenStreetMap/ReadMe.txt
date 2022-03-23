========
NOTE:
========
To build this project in Eclipse, refer to the ReadMe.txt file in the SharedResources 
project for configuration information.

This sample is an "Extension application" or "Plugin" to STK and STK Engine.  They are not applications 
that have a main method or typically any User Interface.  This sample demonstrates the use of the 
STK Graphics's Custom Image Globe Overlay (CIGO) Plugin point.   This plugin point allows a developer and end
application to provide its own (custom) image to drape over the globe (overlay).  You must uncomment the 
"JavaPlugin" element tag containing the plugin with the below package space name in the Graphics.xml file
located in the "Extend/Graphics" folder.  Note this xml file must be placed in your STK user data area's plugins
directory, see STK's plugin documentation for further details.  This example utilizes tile
imagery from OpenStreetMap.  Some useful URLs to familiarize you with this online initiative include:

	http://www.openstreetmap.org/
	http://wiki.openstreetmap.org/wiki/Slippy_map_tilenames
	
In this sample directory you will note that there are two Eclipse project source (abbrev. src) folders, a description include:

	src - The src folder contains the plugin and its helper classes that is configured, instantiated and used by STK or 
	      STK Engine's Graphics CIGO plugin point.  The package spaces name is...
	      
	      			agi.stk.plugin.graphics.cigo.osm
	      	
	      The files contained in this directory include the following:
	      
	      		JavaExample.java  -  The plugin class that the STK or STK Engine instantiates to get the 
	      							 CIGO data ( in this example is OpenStreetMap tiles).  You will note this
	      							 plugin implementation must implement the IAgStkGraphicsPluginCustomImageGlobeOverlay 
	      							 interface.  It can also implement IAgStkGraphicsPluginWithSite interface if it wishes
	      							 to obtain additional information from STK or STK Engine via the IAgStkGraphicsPluginSite
	      							 interface passed as a parameter to the implemented "OnStartup(...)" method.  Refer to 
	      							 the STK Graphic Plugin Chm file or javadocs for more descriptions of these interfaces and
	      							 methods.  Make note that in the "OnInitialize" method an instance of IAgStkGraphicsPluginTiler
	      							 is set on the context passed in as a parameter.  Also note in the "read" method, the actually
	      							 tile byte array is set on the context parameter as well.
	      							 
	      		OpenStreetMapTile.java - A class to hold a singular Tile data.  It must implement the IAgStkGraphicsPluginTile interface.
	      		
	      		OpenStreetMapTiler.java - A class to provide the mulitple Tile data children based on parent tiles zoom/x/y data.  
	      		                          It must implement the IAgStkGraphicsPluginTiler interface.
	      		
	      		OpenStreetMapWebSvcClient.java - 
	      		
	 src_test - The src_test folder contains runnable test applications that utilizes the plugin helper classes
	            in the src folder to help demonstrate and debug any issues seen when running this sample.  These test applications
	            are not used during the plugin runtime within STK or STK Engine because it is not configured to know
	            about this folder in the Graphics.xml file that is used to configure the java plugin.  The files contained
	            in this directory include the following:

				ConvertToOpenGLCoordinatesTest.java - Application to test the Orientation of the byte array from top/bottom to bottom/top.
				
				OpenStreetMapTileViewerTest.java - Application to download and display a tile in a Java AWT User Interface.
												   The default tile displayed is zoom=0, x=0, y=0.  Uses the OpenStreetMapWebSvcClient.java 
												   class algorithms. 
				
				WriteTileToFileTest.java - Application to download and write a tile to a file in the sample driectory.  
										   Uses the OpenStreetMapWebSvcClient.java class algorithms.The main method
										   takes 3 arguments of zoom, x, y of tile to write out as an image file to the 
										   base of the sample directory.  The name of the tile will contain the zoom, 
										   x, y of the tile.  For instance...
										   
										   		java.exe WriteTileToFileTest 0 0 0
										   
										   whose output would be...

												0_0_0.png										   		
										   		
										   or
										   
										   		java.exe WriteTileToFileTest 1 0 0
										   		java.exe WriteTileToFileTest 1 0 1
										   		java.exe WriteTileToFileTest 1 1 0
										   		java.exe WriteTileToFileTest 1 1 1
										   		
										  etc.
										   
												1_0_0.png
												1_0_1.png
												1_1_0.png
												1_1_1.png
					      		  
	      