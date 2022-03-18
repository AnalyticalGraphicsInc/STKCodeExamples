# Post Processing Scripts

 Using post-processing scripts is a powerful way to take advantage of automating simple file manipulation. A post-processing script can be assigned to any report in STK (reports only, not available for graphs). The script is then triggered after the report is created in a 'temp' location, but before it is displayed/printed/saved/exported. Generally, the script is used to reformat this temp file. The post-processing script is called from a ‘system’ command that passes an argument, which is the path to the 'temp' file containing the original generated report, to the post-processing script. Any scripting or programming language can be used so long as is can be called directly form a system command (this is generally set as a file association), and accepts/reads the temp path as a command line argument. Upon script completion, the 'temp' file is then used to complete the selected operation (post-processing destination).

## Simple Post Processing Script

A simple example to show how to create and use post processing scripts. This script adds line numbers to the report before displaying it. Available in Perl, Python, VBSctipt, and Windows Batch File

---

## Access/AER

This is an STK Post Processing script that uses the default Access/AER report and creates an external ephemeris file of the ‘To’ object, using the ‘From’ object as a custom CoordinateSystem.

---

## Deck Access (SSC to Common Name)

This post-processing script, when assigned to a DeckAccess report which was computed to a Satellite Deck, will replace the SSC number with the common name as listed in the %ProgramData%\AGI\STK 12\Databases\Satellite\stkSatDbAll.sd file.

---

## LLA Position to Waypoint File 

This post-processing script will create an STK formatted Waypoint File (*.ga) from LLA data.

---

## Ordered Lighting Times

This reformats the Lighting Times data providers into a single, time ordered report of all conditions (as opposed to each condition in a separate report section).

---

## Time LLA to Ephemeris

This is an STK Post Processing script that works against any report where the first four columns are Time (UTCG), Lat(dec deg), Long (dec deg), Alt(m). Altitude is optional and will default to 0. The output is an STK formatted ephemeris file.

---

## Waypoint report to Waypoint File

This post-processing script will create an STK Waypoint File (*.ga) from a Waypoint report (available from GreatArc vehicles). All availablekeywords/value options included in the output to make it easier to toggle between them.

---