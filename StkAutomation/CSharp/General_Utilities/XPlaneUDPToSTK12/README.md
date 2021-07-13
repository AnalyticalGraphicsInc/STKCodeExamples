# XPlaneUDPToSTK12

This utility connects to X-Plane and reads the position (latitude, longitude, altitude) and attitude (yaw, pitch, roll) data through a UDP connection, then sends it to an aircraft in STK using the Real Time propagator. 

## Software

Requires: STK 12
Works with: X Plane 10 and 11

## Instructions

You must configure XPlane to broadcast the appropriate data and broadcast on the appropriate port.  This can be done in the XPlane program by first setting up the data output.

Next you need to make sure that you are broadcasting to the correct IP Address / Port combination (if running on the same machine, you can use the Localhost IP Address 127.0.0.1 and any port you want).

Next you need to launch the XPlane To STK utility and enter the port number you chose to broadcast on and hit the 'Connect' button to start bringing in the Lat/Lon/Alt and Yaw/Pitch/Roll information that STK will need.

You can then click the Connect to STK button, which will connect to an open STK instance or launch a new STK window if one is not open. If the "Use Current Scenario" option is checked, the utility will use the current scenario. If not, a new scenario will be opened.

Next use the Create Aircraft button to setup the aircraft inside of STK to start accepting the position/attitude information from XPlane.