# C++ Aviator Basic Maneuver

## **Overview**

This example builds a new Basic Maneuver Strategy for STK Aviator.

---

## **Build**
This plugin is built for STK 12.1 and requires the following licenses: AVIATOR, STK FREE

Copy the XML file to one of the following locations:

- C:\ProgramData\AGI\STK 12\Plugins
- C:\Program Files\AGI\STK 12\Plugins
- \<STK User Directory\>\Config\Plugins

Open a command prompt that will need to be elevated (admin permissions), change to the directory that your plugin code is in and then run the following:

`regsvr32 .\x64\bin\Release.x64\BasicManeuverExamplesCPP.dll`