/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/

The following is a step by step procedure for building and using the 
Light Reflection Plugin Sample written in C#.

1.  Install Visual Studio .NET.

2.  In the Solution browser, right click the project and choose "Build".
	Make sure the project builds successfully.
    
3.  To register the component, create a command window (under the Start menu, hit "Run ...", type 'Command' and hit OK). 
    Change your directory (using cd) to that of the file. At the command prompt, type 'regasm /codebase YOUR_FILENAME'. For example:
        regasm /codebase Agi.As.Hpop.Plugin.CSharp.Examples.dll
        
    Your system may not recognize the regasm command. In that case, you will need to specify the full filepath to RegAsm.exe. 
    The file itself is found in a sub-folder of C:\WINDOWS\Microsoft.NET\Framework64\. 
    The actual folder depends on your version of the .NET Framework installed on your machine.

4.	This plugin has already been registered in the "LightReflection" Category in the
	"SRP.LightReflection.xml" plugin point registration file; however the registration lies
	in a comment block that will not be executed. You must edit the file, and move
	the following line outside of the comment block:

	<Plugin DisplayName = "SRP.Spherical.CSharp" ProgID = "AGI.SRP.LightReflection.Spherical.CSharp.Example"/>
	
	The registration file can be found in the Solution Explorer under "Solution Items". 
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

7.  Use STK documentation for directions on how to configure the satellite to use the plugin
    reflection model.

/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/