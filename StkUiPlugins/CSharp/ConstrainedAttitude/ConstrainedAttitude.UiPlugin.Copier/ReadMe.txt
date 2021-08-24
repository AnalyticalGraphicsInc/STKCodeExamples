Step 1: Set installer (copier) project as Startup Project and run the project in the debug mode. You will see Setup form with four inputs: 
Display Name, Type Name, Assembly Name, and UI plugin binary directory location. Assembly Name corresponds to the namespace of your plugin. 
Type Name represent entry point class for the plugin. In other words, full path for the class that implements IAgUiPlugin interface. 
If you used my UI Plugin template, the Type Name would be <namespace>.Setup.

Step 2: Change the project configuration to “Release” and build the project.

Step 3: Distribute executable from the project as well as configuration file (*.exe.config) to your users.

Step 4: When the user runs executable, all they need to do select location where they want to copy the UI Plugin.
