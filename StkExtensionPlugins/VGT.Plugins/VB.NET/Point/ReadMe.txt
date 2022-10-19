/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/

The following is a step by step procedure for building and using the 
VGT Point Plugin Samples written in C#.

1.  Install Visual Studio .NET.

2.  Install and register STK.

3.  This project's References folder must reference the following COM dll's
    which can be found in the bin directory of the STK install.
    
    AgAttrAutomation.dll
    AgUtPlugin.dll
    AgCrdnPlugin.dll
    AgStkPlugin.dll

	If this is not done, compile errors will occur.
	
4.  In the Solution browser, right click the project and choose "Build".
	Make sure the project builds successfully.
    
5.  To register the component, create a command window (under the Start menu, hit "Run ...", type 'Command' and hit OK). 
    Change your directory (using cd) to that of the file. At the command prompt, type 'regasm /codebase YOUR_FILENAME'. For example:
        regasm /codebase Agi.As.Hpop.Plugin.CSharp.Examples.dll
        
    Your system may not recognize the regasm command. In that case, you will need to specify the full filepath to RegAsm.exe. 
    The file itself is found in a sub-folder of C:\WINDOWS\Microsoft.NET\Framework64\. 
    The actual folder depends on your version of the .NET Framework installed on your machine.
 
6.	This plugin has already been registered in the "VGT Point Plugins" Category in the
	"VGT Plugins.xml" plugin point registration file; however the registration lies
	in a comment block that will not be executed. You must edit the file, and move
	the following line outside of the comment block:

	<Plugin DisplayName = "VB_NET Point Example" ProgID = "Agi.VGT.Point.Plugin.Examples.VB_NET.Example1"/>
	
	The registration file can be found in the Solution Explorer under "Solution Items". 
	When creating your own plugins, you need to register your plugin similarly, either
	in a separate xml file or added to another plugin point registration file.

7.	The plugin point registration file must be copied to a Plugins folder which will be
	searched by STK when it starts. STK looks in the following locations:
		- INSTALL_DIR\Plugins		
		  For example: C:\Program Files\AGI\STK <version>\Plugins
		- CONFIG_DIR\Plugins
		  WIN10: C:\Users\<user_name>\Documents\STK <version>\Config\Plugins
	
8.  Start STK.  Check the message viewer to see if any errors occurred when attempting to
	load the registration file.

9.  Use STK documentation for directions on how to configure an VGT Point Plugin for a 
    given STK Object.

/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/