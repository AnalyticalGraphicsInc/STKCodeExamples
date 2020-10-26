Tracking example (this scenario can be run (but not changed) without Matlab):

Load scenario and animate: it shows attitude trajectory generated using adaptive control. 
When access indicates that scud missile is visible, tracking satellite with the laser slews in to track the missile. 
This trajectory can be regenerated using attitude simulator run:
	- You need to uncheck "Precomputed" attitude for Tracking satellite
	- Go to Satellite - Attitude simulator
	- You can show the settings of the simulator just don't change anything - simulator uses multiple Matlab files working together
	- If you click Run, the same attitude trajectory that was used initially can be regenerated
To show alternative performance:
	- Again, uncheck "Precomputed" attitude for Tracking satellite
	- Open Scripting/Attitude folder under Scenario folder
	- Open gains.m file which contains initailization data used by this example
	- Find the line close to the top of the file that looks like this
		output.kq = 100.0;       %100 works a lot better than 10
	- Change value from 100.0 to 10.0
	- Save the m-file
	- Re-run the simulator and show the new trajectory

In case Matlab does not work, there are two .a files included in the scenario that have been pre-computed using two gain settings:
	- Tracking_kq_100.a uses kq = 100.0 - better performance - this file is loaded into Tracking satellite when you first open the scenario
	- Tracking_kq_10.a uses kq = 10.0 - worse performance

