# VBScript Post Processing Examples

## [AccelHist.vbs](AccelHist.vbs)

This is a PostProcessing script to export an Astrogator Maneuver Sequence as an ODTK accelHist file. This script takes a report containing: 
    Astrogator Maneuver Ephemeris Block History - Top - Epoch
    Astrogator Maneuver Ephemeris Block History - Top - Time
    Astrogator Maneuver Ephemeris Block History - Maneuver - Thrust_Vector_X
    Astrogator Maneuver Ephemeris Block History - Maneuver - Thrust_Vector_Y
    Astrogator Maneuver Ephemeris Block History - Maneuver - Thrust_Vector_Z
    Astrogator Maneuver Ephemeris Block History - Maneuver - Total_Mass
and reformats it into an accesHist file. Note that Epoch is expected to be in UTCG and Time in EpSec.

---

## [AccessDurationPerDay.vbs](AccessDurationPerDay.vbs)

To use this script use a regular access report. This script will compute the access times per day. This script currently only works for access from one object to one other, not multiple in the same report. 

Date needs to be in Gregorian format, time needs to be in seconds.

---

## [pps_simple.vbs](pps_simple.vbs)

A simple example to show how to create and use post processing scripts. This script adds line numbers to the report before displaying it.

---
