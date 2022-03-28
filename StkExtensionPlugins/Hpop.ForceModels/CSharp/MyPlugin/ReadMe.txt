/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/

This 'empty' C# project is a bare bones example of a C# HPOP Force Model plugin.

To create your own plugin project, based upon this one, do the following:

(1) Copy the entire MyPlugin folder to a new folder.
(2) Rename the folder to name of your plugin. 

	Below, we'll use the term YourPlugin to refer to the name you have chosen.

(3) In the YourPlugin folder, rename:
	IMyPlugin.cs	--> IYourPlugin.cs
	MyPlugin.cs		--> YourPlugin.cs
	MyPlugin.csproj --> YourPlugin.csproj
	
Use a text editor to perform the changes to the contents of the files. WordPad is fine, but
notepad is limited and not recommended. TextPad is terrific, as is Ultra-Edit 32, and 
probably many others as well.
	
(4) You need to create the name of a namespace, based upon the YourPlugin name.
	As an example, we used:  AGI.Hpop.Plugin.Examples.Stk.ForceModeling.CSharp
	which has our company identifier and some description about which type of plugin
	point this plugin is intended for. We use the namespace when we create the COM ProgId,
	the human-readable program identifier that COM uses to find your plugin. It should be unique
	when installed on computers on which it will be run, so the more descriptive the
	value the better.
	
	Below, we'll use the term YourNamespace to mean the name you have chosen.
	
	Once you decide on a namespace identifier, search for string MYPLUGIN_NAMESPACE in all files
	in the folder and replace that text with YourNamespace.
	
(5) In AssemblyInfo.cs, replace MYPLUGIN_COMPANYNAME with your company identifier.

(6) In IYourPlugin.cs, replace IMYPLUGIN with IYourPlugin (i.e., prepend a capital I to your plugin name).

(7) In YourPlugin.cs, replace MYPLUGIN with YourPlugin. You'll see that the ProgId combines
	the namespace and your plugin name. You can change that if you want, but this is our recommendation.
	
	Also replace IMYPLUGIN with IYourPlugin.
	
(8) In YourPlugin.csproj, replace MYPLUGIN with YourPlugin. You should have already replaced
	MYPLUGIN_NAMESPACE as part of step 4.
	
	Replace the line: RelPath = "MyPlugin.cs"  with : RelPath = "YourPlugin.cs"
	Replace the line: RelPath = "IMyPlugin.cs"  with : RelPath = "IYourPlugin.cs"
	
(9) Load the project YourPlugin.csproj into Microsoft Visual Studio.NET . It should load properly.

(10) In the reference sections, there should be references to COM type libraries provided by AGI.
	 STK must be installed so that these type libraries are registered in the Windows registry.
	
(10) Your renamed project should be all set to go.  You can build it (and it can even be used in STK and ODTK,
	doing nothing but outputting messages to the Message Viewer if debug mode is true).
	
(11) To install on another machine:
	 - Copy the entire binary folder where YourPlugin.dll was built (including the interop dll's) 
	   to the new machine.
	 - Use RegAsm.exe to register YourPlugin.dll. STK must have already
	   been installed on that machine.

/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/