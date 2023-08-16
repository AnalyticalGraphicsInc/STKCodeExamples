# Loading external data into a CoverageDefinition
there are two parts to this
1. An access plugin that reads the external data and assigns the correct value to each grid point
2. A UI plugin that makes pushing external data into STK easier
both plugins are registry free.

## Installing the Plugins
Since they are both registry free
1. load the solution into VisualStudio and compile both projects
2. copy the required files to C:\ProgramData\AGI\STK 12\Plugins
	- the xml to C:\ProgramData\AGI\STK 12\Plugins
	- the contents of the ExternalCoverageDataAccessPlugin bin into C:\ProgramData\AGI\STK 12\Plugins\ExternalCoverageDataPlugins\ExternalCoverageDataAccessPlugin
	- the contents of the ExternalCoverageDataUiPlugin bin into C:\ProgramData\AGI\STK 12\Plugins\ExternalCoverageDataPlugins\ExternalCoverageDataUiPlugin

restart STK

## Running the Plugin
- after restarting STK, a new UI plugin named LoadExternalCoverageData should become available. Select it and start the UI plugin. 
- specify the data file. STK expects a csv in the following format
     Time (EpSec), lat (deg), lon(deg), value
- you most likely want to have the plugin create a .pnt file that contains the location of all gridpoints. Due to numerical issues, you most likely want this even if your grid already matches the data
- load the external data
- adjust the FigureOfMerit dynamic graphics contours. You will most likely want to use Contour interpolation