/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/

The following is a step by step procedure for building and using the 
Attitude Controller Plugin Samples written in C++.

1.  Install Visual Studio .NET.

2.  Install and register STK.

3.   Ensure that the Active solution platform in the Build Configuration is correctly configured. If you are targeting STK Desktop or STK Engine x86, it should be set to x86. If you are targeting STK Engine x64, it should be set to x64. Then, right click the project and choose "Build". Make sure the project builds successfully.

4.	This plugin has already been registered in the "Radar RCS Plugin" Category in the
	"Radar RCS Plugins.xml" plugin point registration file; however the registration lies
	in a comment block that will not be executed. You must edit the file, and move
	the following line outside of the comment block:
	
	<Plugin DisplayName = "RCS CPP Example" ProgID = "Agi.Radar.RCS.CPP.Example1"/>

	The registration file can be found in the Radar\RCS directory. 
	When creating your own plugins, you need to register your plugin similarly, either
	in a separate xml file or added to another plugin point registration file.
	
5.	The plugin point registration file must be copied to a Plugins folder which will be
	searched by STK when it starts. STK looks in the following locations:
		- INSTALL_DIR\Plugins		
		  For example: C:\Program Files\AGI\STK <version>\Plugins
		- CONFIG_DIR\Plugins
		  WIN10: C:\Users\<user_name>\Documents\STK <version>\Config\Plugins
	
6.  Start STK.  Check the message viewer to see if any errors occurred when attempting to
	load the registration file.

/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/