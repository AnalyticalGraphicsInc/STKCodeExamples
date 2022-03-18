/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/

The following is a step by step procedure for registering 
Gator Attitude Controller Plugin Samples components.

1.  Install and register STK.

!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
NOTE:  Before registering the components, if you have Microsoft AntiSpyware 
application actively running, you will have to perform one of the following tasks( A or B ):

	A.  Allow the running of these scripts via the MS AntiSpyware applications options menu.
	B.  Run the script, and when it asks for it to allow the script, check the box indicating 
	    "Remember this action". Then close the editor that will appear containing your scripts code.  
            And perform step 3, to register it again, at which time the Antispyware application
            will not interfere with the registration.
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

2.  Locate the file containing your component in windows explorer.

	Components built as a Windows Script Component with a scripting environment (VBScript, JScript, 
	Perl, Python) should have a filename extension 'wsc'. This is a XML text file declaring the
	implementation of the plugin interface.

	Compiled components are contained in a dynamic link library, whose filename has an extension 'dll'.
	You will need to know whether the component was built using a .NET language like C# or VB.NET.

3.  This step registers your component as a COM component.

	NOTE: Please note that you will need admin privileges to register this plugin, but it only needs 
	to be done one time. You may need to ask a computer systems administrator to help you with this.

	WSC components

	Right click the file. If "Register ... " is available then choose it and confirm the dialog boxes. 
    If not, create a command window (under the Start menu, hit "Run ...", type 'Command' and hit OK).
    Change your directory (using cd) to that of the file. At the command prompt, type 
    'regsvr32 YOUR_FILENAME'. For example: regsvr32 Agi.Radar.RCS.WSC.Example.wsc  .

    Dll components not built using .NET

    Create a command window (under the Start menu, hit "Run ...", type 'Command' and hit OK).
    Change your directory (using cd) to that of the file. At the command prompt, type 
    'regsvr32 YOUR_FILENAME'. For example: regsvr32 Agi.Radar.RCS.WSC.Example.wsc.dll.
    
    Dll components built using .NET
    
    Create a command window (under the Start menu, hit "Run ...", type 'Command' and hit OK).
    Change your directory (using cd) to that of the file. At the command prompt, type 
    'regasm /codebase YOUR_FILENAME'. For example: 
    regasm /codebase Agi.Radar.RCS.WSC.Example.wsc.dll.
    
    NOTE: Your system may not recognize the regasm command. In that case, you will need to 
    specify the full filepath to RegAsm.exe. The file itself is found in a sub-folder of
    C:\WINDOWS\Microsoft.NET\Framework\. The actual folder depends on your version of the 
    .NET Framework installed on your machine.
	
	Registration on 64-bit machines
	
	When using regsvr32 on a 64-bit machine, it is important to know whether the COM object is 
	built using a 32 or 64 bit architecture. In most cases, it is using 32 bits. To register a 
	32 bit built COM object, you must use the 32 bit version of regsvr32. Normally this is found 
	in C:\Windows\SysWOW64. By default, 64 bit machines use the 64 bit version of regsvr32, so you should use 

	C:\Windows\SysWOW64\regsvr32.exe YOUR_FILENAME 
	
	when registering 32 bit COM objects. Currently, Windows Script Components are all 32 bit.

4.	This plugin has already been registered in the "Radar RCS Plugin" Category in the
	"Radar RCS Plugins.xml" plugin point registration file; however the registration lies
	in a comment block that will not be executed. You must edit the file, and move
	the appropriate line containing the ProgID of the plugin you registered outside of 
	the comment block.
	
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