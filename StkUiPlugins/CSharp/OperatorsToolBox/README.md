# Description   
   
Operator’s Toolbox is a UI plugin designed specifically to improve the efficiency of common operational tasks. 
The tools made available in this plugin are mainly based on customer requests to automate certain processes within STK in an effort to reduce analysis time for real world scenarios. 
Operator’s Toolbox contains a total of 12 tools to automate various processes including but not limited to: TLE import and update functionality, improved stored views functionality, 
automated chain/tracking interval creation, and new custom ground classification types. A more detailed description of each tool can be found below and the full overview can be found 
in the documentation.The documentation can be found in the attached zip file. Unzip the zip onto your local drive and then read through the Getting Started and Setup section of the documentation at a minimum. 
It is recommended to read the documentation prior to usage. Please make sure to read the section regarding known issues, specifically relating to blocked dll issues and GUI graphics issues.
 
TEMPLATES
Templates is one of the most powerful tools available in Operator’s Toolbox because of its ability to quickly recreate entire object configurations of any type. The tools purpose is to allow the user to 
save any number of objects, with their associated properties and object dependencies, as a “Template” such that it can be imported into any new scenario. While there are already tools to do this on a single object basis, 
like the Standard Object Database, Templates allows any number of objects to be imported at once and it is far faster than any other method currently available.

SATELLITE CREATOR
The satellite creator is meant as a one-stop-shop for satellite creation and updates. The tool includes import options for TLE/TCE, ephemeris, orbital elements, and a custom satellite database. 
The custom satellite database allows a user to define custom metadata for a user specific TLE database and then import satellites based on that metadata. These tools will also update satellites to the most 
recent TLE data if the source is updated

UNIFIED DATA LIBRARY TLE IMPORT
The Unified Data Library (UDL) is online data library that contains various data that can be used inside STK. This is tool specifically designed to download TLE files from the UDL database. 
It is important to note that you will need to request a UDL account before being able to use this tool. Additionally this tool can be used in both unclassified and classified environments. 
By default the tool will be configured for unclassified use, but this can be changed by modifying the UDL URL that is shown on the settings page. If you are working in a classified environment and need the high-side 
address please reach out to obtain this address.

SATELLITE EPOCH UPDATER
There are many instances when the scenario analysis interval changes but the object availability interval does not change with it. The Epoch Updater is a simple tool that allows the user 
to quickly update the availability times for any satellite in the scenario, including Astrogator satellites.

FACILITY CREATOR
Facility Creator allows the user to save and modify sensor cadences. Sensor cadences, in this sense, are defined as a set of radar or optical trackers that make up a whole tracking network. 
This tools not only creates the site locations, but also creates the attached sensor objects will all the required constraints. The tool also allows the cadences to be saved to a database so 
they can be used in any scenario once they are originally created.

GROUND EVENTS
The Ground Events tool serves two major purposes. Mainly it allows the user to classify stationary ground objects into any number of user defined types beyond the three basic types given in STK
(i.e. place, facility, and target). This also allows the user to specify specific images to be associated with each type when they are created. These types and images can be set on the Settings
page of the main Operator’s Toolbox GUI. The second purpose of the tool is to associate a time interval and metadata with a stationary ground object. This helps define a schedule of when events 
are happening around the globe, and provides an easy interface to see important information.

SMART VIEW
Smart View is meant to boost the capabilities already found in the default stored views. A default stored view allows a user to save a camera position, time, and timestep. 
A smart view allows a user to not only include those features but also data displays, universal lead/trail settings for moving vehicles, and options to hide or shows groups of objects with each view. 
There are also new built in view types for the 2D graphics window, Target/Actor scenarios (i.e. close proximity), and GEO Drift (i.e. Station Keeping).

STATION ACCESS UTILITY
The Station Access tool serves to expedite the process of creating large scale access computations that involve constellations and single link chains. The tool will not only create the chain and constellations 
if required, but also add any time components to the time view, export data, or create access reports if required.

COVERAGE UTILITY
The Coverage Utility serves to expedite the process of creating coverage definitions and figure of merits. It will create the coverage definition and figure of merit for the user 
along with settings common user settings and configuring the FOM graphics based on the FOM type. There are built in options for global coverage, Country/Region (with custom user area 
targets as an input options), and Object Area of Interest coverage. Object AOI coverage is new to this tool and allows the user to specify any number of ground objects as centers for coverage areas, 
along with their defining area around the centers.

PASSIVE SAFETY CHECKER
The Passive Safety Checker is meant for use in Proximity Operations or Close approach trajectories. The main goal of the tool is to verify that the acting satellite will 
not come within close proximity of the target if something goes wrong. Passive Safety guarantees that if something goes wrong with one of the maneuvers, the naturally generated trajectory, 
without any additional maneuvers, does not come within a specified user volume around the target. The volume of the satellite can be defined as a sphere or a box surrounding the target, 
with dimensions in the RIC frame.

VOLUME CREATOR
The Volume Creator allows the user to create analytical volumes in space based on ground range, altitude, and elevation constraints. The tool will create the volume at a specified ground location.
This can be used for situational awareness, threat assessments, or any other use case involving a 3D keep out zone or area of regard. The tool saves the volumes and locations in a database so they 
can be used in any scenario after they are initially created.
 
SOLAR ANGLE UTILITY
This tool is designed to calculate the beta angle and/or Solar Phase/CATS angle between any number of observers and targets. 
This tool will create all of the Analysis Workbench components required to report out the angle values.

PLANE CROSSING UTILITY
This tool is designed to calculate the times when a satellite or stationary ground object crosses the orbital plane of a satellite of interest. The tool will create all required AWB components for the calculation.
The tool has the option to constrain the times to specific angle bounds, allowing the user to report out crossing times in and out of a region relative to orbital plane. 
The tool can export the data as a report, a text file, and provides an interface to see the results internally. 

# Instructions  
 
This plugin can only be run on STK 12 or higher and requires the following licenses: ASTROGATOR, STK PRO, ANALYSIS WORKBENCH, COVERAGE
The first requirement to run this application is to have the accompanying xml in the right folder and pointing to the correct dll location. 
Place the accompanying xml in one of the following three directories:

C:\ProgramData\AGI\STK 12\Plugins
C:\Program Files\AGI\STK 12\Plugins
<STK User Directory>\Config\Plugins

To run the application, open up the VisualStudio solution. You will need to obtain and import the following DLLs:   
 
AGI.Astrogator.Interop
AGI.Astrogator.Plugin.Interop 
AGI.Plugin.Interop
AGI.STK.Plugin.Interop
AGI.STKGraphics.Interop
AGI.STKObjects.Astrogator.Interop
AGI.STKObjects.Interop
AGI.STKUtil.Interop
AGI.STKVgt.Interop
AGI.Ui.Application.Interop
AGI.Ui.Core.Interop
AGI.Ui.Plugins.Interop  

 
You can do this by right-clicking on "References" in the SolutionExplorer, hitting 'add', and browsing to find the DLLs. 
These are located in the STK install bin, which is typically here: C:\Program Files\AGI\STK 12\bin\Primary Interop Assemblies.   
Running the program from within Visual Studio also requires a startup application. Open the project properties and go to the Debug Tab.
On the Debug tab, set the start action to "Start External Program" and set it to the AgUiApplication (normally C:\Program Files\AGI\STK 12\bin\AgUiApplication.exe)
When this is set then the project can be built and debugged from within visual studio

 
# Download  
 
If you simply want to utilize this utility you can find the download [here] (https://agiweb.secure.force.com/faqs/articles/Keyword/Operator-s-Toolbox-Plugin)

