The MATLAB scripts contained in this folder are simple demonstrations of how
to pass real-time data into STK.  All scripts will read position and attitude data
from simple text files, then pass those values into STK in simulated realtime.  No
setup is required for the scripts, simply open and run.  The script will grab an
existing instance of STK12 or create a new one if necessary.  It will then create 
a new scenario, add the realtime object, and send the position/attitude data.

realtimeSatellite.m - creates a satellite and passes position and attitude data.

realtimeSatellite_MatlabClock.m - creates a satellite and passes position and 
attitude data.  This script uses the MATLAB timer function to update the current 
time in STK.  This demonstrates the ability to have MATLAB control the clock in STK. 
 
realtimeAircraftExample.m - creates an aircraft and passes position and attitude
data.  Note line 61 where the model file for the aircraft is changed to the f-18 
that is installed with STK.  Make sure the file path is correct for your installation

realtimeMissileWithArticulation.m - creates a missile and passes position and attitude
data, as well as commands stage separation articulations at 60 second intervals.  This
shows how model articulations can be sent in realtime.