/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/

The following is a step by step procedure for building and using the 
Projection Raster Stream Plugin Sample written in VB.NET.

1.  Install Visual Studio .NET.

2.  Install and register STK.

3.  In the Solution browser, right click the project and choose "Build".
	Make sure the project builds successfully.
 
4.	This plugin has already been registered in the "GfxPlugins" Category in the
	"Graphics.xml" plugin point registration file; however the registration lies
	in a comment block that will not be executed. You must edit the file, and move
	the following line outside of the comment block:

	<Plugin ProgID="ProjectionRasterStreamPlugin.VBNET" DisplayName="ProjectionRasterStreamPlugin.VBNET"/>
	
	The registration file can be found in the Solution Explorer under the project. 
	When creating your own plugins, you need to register your plugin similarly, either
	in a separate xml file or added to another plugin point registration file.

5.	The plugin point registration file must be copied to a Plugins folder which will be
	searched by STK when it starts. STK looks in the following locations:
		- INSTALL_DIR\Plugins		
		  For example: C:\Program Files\AGI\STK <version>\Plugins
		- CONFIG_DIR\Plugins
		  WIN10: C:\Users\<user_name>\Documents\STK <version>\Config\Plugins
		  
6.	The plugin dll has to be registered using regasm which can be found at the following locations:
		-32 bit
		C:\Windows\Microsoft.NET\Framework\<.NET Version>\
		-64 bit 
		C:\Windows\Microsoft.NET\Framework64\<.NET Version>\
	
	Open a command prompt in the appropriate directory and run the following command:
		regasm /codebase "<install dir>\<CodeSamples>\Extend\Graphics\VB.Net\ProjectionRasterStreamPlugin\bin\<Config>\ProjectionRasterStreamPlugin.dll"
	
7.  Run the GraphicsHowTo sample and select the "Add projected imagery to the globe" code snippet
	under the Globe Overlays node on the left.  Check that a dialog does not appear
	stating there is a com exception.

/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/