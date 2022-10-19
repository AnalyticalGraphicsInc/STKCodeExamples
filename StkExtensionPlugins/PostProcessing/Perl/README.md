# Perl Post Processing Examples

## [pps_simple.pl](pps_simple.pl)

A simple example to show how to create and use post processing scripts. This script adds line numbers to the report before displaying it.

---

## [pps_DeckAccess-SSCtoCommonName.pl](pps_DeckAccess-SSCtoCommonName.pl)

This post-processing script, when assigned to a DeckAccess report which was computed to a Satellite Deck, will replace the SSC number with the common name as listed in the %ProgramData%\AGI\STK 12\Databases\Satellite\stkSatDbAll.sd file.

---

## [pps_LLAPos2GA.pl](pps_LLAPos2GA.pl) 

This post-processing script will create an STK formatted Waypoint File (*.ga) from LLA data.

---

## [pps_Ordered_Lighting_Times.pl](pps_Ordered_Lighting_Times.pl)

This reformats the Lighting Times data providers into a single, time ordered report of all conditions (as opposed to each condition in a separate report section).

---

## [pps_TimeLLA-to-ephemeris.pl](pps_TimeLLA-to-ephemeris.pl)

This is an STK Post Processing script that works against any report where the first four columns are Time (UTCG), Lat(dec deg), Long (dec deg), Alt(m). Altitude is optional and will default to 0. The output is an STK formatted ephemeris file.

---
