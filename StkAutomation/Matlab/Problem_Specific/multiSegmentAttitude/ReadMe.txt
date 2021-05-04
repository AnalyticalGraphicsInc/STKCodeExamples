---Purpose---

This function is written to address a specific "problem" however it serves as an example of custom Analysis Workbench components as well as multi segment attitude through STK Integration. The specific "problem" that this script addresses is a satellite that has multiple attitude segments: a non-operational profile, a pre-operational attitude profile, and an operational profile. The  definition and timing of each profile is as follows:

Non-Operational Profile -- Satellite Body X aligned with the velocity vector and body Z constrained by the Nadir vector. This is the default state for the satellite
Pre-Operational Profile -- Satellite Body Y aligned to the Sun vector and body Z constrained by the Nadir vector. This is to simulate an operational constraint that the satellite must point its solar panels at the sun for a specified amount of time before each access (or Operational segment) to the facility.
Operational Profile -- Satellite Boxy X aligned to the vector from the satellite to the facility and body Z constrained to the Nadir vector. This is to simulate a transmit/receive attitude between the satellite and the facility.

Each profile switch is accompanied by a fixed time slew. The fixed time for these slews is an input to the function.

In this hypothetical situation, the satellite must be in sunlight to be eligible for access and an interval list is created that contains each sunlight interval for the satellite minus the first x amount of seconds in each period corresponding to the required pre-pass time.

The functionality demonstrated in this script is primarily custom time components through Analysis Workbench and custom attitude segments through the Multi-Segment attitude type.

---Instructions---

- Open an STK 12.1 scenario (this script is only compatible with STK 12.1 and later)
- Add a satellite and facility that will access one another at some point during your scenario interval
- Run the function in MATLAB as follows: multiSegmentAttitude(<satName>,<facName>,<prePassTime(sec)>,<slewTime(sec)>)
	ex: multiSegmentAttitude("Satellite1","Facility1",600,300)
- Zoom to the satellite and animate the scenario. Note the "Pre-Operational" and "Operational" annotations that come up in 3D graphics window and observe the body axes orientation as the satellite works through the different attitude profiles

