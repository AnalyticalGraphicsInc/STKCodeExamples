# Python General Utility Samples

## [stkMetaDataExtractor.py](EditIntervalFile.py)

Allows the user to output an xml or json file that includes each object in the scenario, the description of the scenario, and unique information for each object. The script will also try to output a CZML preview of the scenario. To use the tool, open a command line and navigate to the file location of the script. Run the script in a python environment and enter the filepath of the .sc or .vdf file to be used, as well as the output location of the xml/json and czml. The command line input would look like this:

`python stkMetaDataExtractor.py "C:\Users\username\Documents\STK 12\Scenario1.sc" C:\Users\username\Documents`

### Dependencies

* Licenses: Free, Integration
* Other Scripts: N/A
* Scenario: N/A

---

## [FilterObjectsByType.py](EditIntervalFile.py)

Utility that demonstrates how to grab an array of all objects of a certain type. Returns the STK path of each object of that type. Includes an optional string filter.

### Dependencies

* Licenses: Free, Integration
* Other Scripts: N/A
* Scenario: N/A

---

## [eventSub.py](eventSub.py)

Utility that demonstrates how to subscribe to STKObjectRoot Events using comtypes. When run, the command line will act as a log for STK events.

### Dependencies

* Licenses: Free, Integration
* Other Scripts: N/A
* Scenario: N/A

---

## [AzElPolarPlot](AzElPolarPlot)

Notebooks that use comtypes or win32 to interact with STK and demonstrate how to pull azimuth and elevation measurements from a facility to a satellite object and then plot these on a polar graph using the Python matplotlib library.

### Dependencies

* Licenses: Free, Integration
* Other Scripts: N/A
* Scenario: N/A

---

## [AnalyzerPlots](AnalyzerPlots)

This notebook allows for you to extract data from [STK Analyzer](https://help.agi.com/stk/index.htm#analyzer/analyzer.htm) and plot the data using matplotlib. Three example output files from Analyzer are provided.

### Dependencies

* Licenses: Free, Analyzer
* Other Scripts: N/A
* Scenario: N/A

---

## [ImportFacilities](ImportFacilities)

A simple function to assist with the importing of Facility objects from information populated in an Excel spreadsheet. An example Excel spreadsheet is provided. Inputs include STK whole number version as an integer and Excel file path. Units are assumed to be degrees and meters with a header row in the Excel file for ID, LAT, LON, ALT.

### Dependencies

* Licenses: Free, Integration
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: Pandas

--- 

## [CombineEphemeris.py](CombineEphemeris.py)

This tool combines multiple STK ephemerides (.e) into one long STK ephemeris. It takes as arguments a directory containing the .e files to join and the name and directory of the new, conjoined ephemeris. The script will create an STK scenario. 

### Dependencies

* Licenses: Free, Integration
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: Pandas 

---
