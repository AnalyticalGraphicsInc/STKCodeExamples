/**********************************************************************/
/*           Copyright 2019, Analytical Graphics, Inc.                */
/**********************************************************************/

The following is a step by step procedure for building and using the 
Drag Plugin Samples written in C#.

1.  Install Visual Studio .NET.

2.  Install and register STK.

3.  In the Solution browser, right click the project and choose "Build".
	Make sure the project builds successfully.
    
4.  To register the component, create a command window (under the Start menu, hit "Run ...", type 'Command' and hit OK). 
    Change your directory (using cd) to that of the file. At the command prompt, type 'regasm /codebase YOUR_FILENAME'. For example:
        regasm /codebase Agi.As.Hpop.Plugin.CSharp.Examples.dll
        
    Your system may not recognize the regasm command. In that case, you will need to specify the full filepath to RegAsm.exe. 
    The file itself is found in a sub-folder of C:\WINDOWS\Microsoft.NET\Framework64\. 
    The actual folder depends on your version of the .NET Framework installed on your machine.
 
5.	This plugin has already been registered in the "Drag Model Plugins" Category in the
	"DragModels.xml" plugin point registration file; however the registration lies
	in a comment block that will not be executed. You must edit the file, and move
	the following line outside of the comment block:

	<Plugin DisplayName = "Drag.Lift.CSharp" ProgID = "AGI.Drag.Lift.CSharp.Example"/>
	
	The registration file can be found in the Solution Explorer under "Solution Items". 
	When creating your own plugins, you need to register your plugin similarly, either
	in a separate xml file or added to another plugin point registration file.

6.	The plugin point registration file must be copied to a Plugins folder which will be
	searched by STK when it starts. STK looks in the following locations:
		- INSTALL_DIR\Plugins		
		  For example: C:\Program Files\AGI\STK 12\Plugins
		- CONFIG_DIR\Plugins
		  WIN10: C:\Users\{user_name}\Documents\STK 12\Config\Plugins
	
7.  Start STK.  Check the message viewer to see if any errors occurred when attempting to
	load the registration file.

8.  Use STK documentation for directions on how to configure an Drag Plugin for a 
    given satellite.

/**********************************************************************/
/*           Copyright 2019, Analytical Graphics, Inc.                */
/**********************************************************************/