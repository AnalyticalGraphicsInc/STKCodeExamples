# Python General Utility Samples

## [stkMetaDataExtractor.py](stkMetaDataExtractor.py)

Allows the user to output an xml or json file that includes each object in the scenario, the description of the scenario, and unique information for each object. The script will also try to output a CZML preview of the scenario. To use the tool, open a command line and navigate to the file location of the script. Run the script in a python environment and enter the filepath of the .sc or .vdf file to be used, as well as the output location of the xml/json and czml. The command line input would look like this:

`python stkMetaDataExtractor.py "C:\Users\username\Documents\STK 12\Scenario1.sc" C:\Users\username\Documents`

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [FilterObjectsByType.py](FilterObjectsByType.py)

Utility that demonstrates how to grab an array of all objects of a certain type. Returns the STK path of each object of that type. Includes an optional string filter.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [eventSub.py](eventSub.py)

Utility that demonstrates how to subscribe to STKObjectRoot Events using comtypes. When run, the command line will act as a log for STK events.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [AzElPolarPlot](AzElPolarPlot)

Notebooks that use comtypes or win32 to interact with STK and demonstrate how to pull azimuth and elevation measurements from a facility to a satellite object and then plot these on a polar graph using the Python matplotlib library.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [AnalyzerPlots](AnalyzerPlots)

This notebook allows for you to extract data from [STK Analyzer](https://help.agi.com/stk/index.htm#analyzer/analyzer.htm) and plot the data using matplotlib. Three example output files from Analyzer are provided.

### Dependencies

* Licenses: [STK Premium Space](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-space-brochure.pdf) or [STK Premium Air](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-premium-air-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [ImportFacilities](ImportFacilities)

A simple function to assist with the importing of Facility objects from information populated in an Excel spreadsheet. An example Excel spreadsheet is provided. Inputs include STK whole number version as an integer and Excel file path. Units are assumed to be degrees and meters with a header row in the Excel file for ID, LAT, LON, ALT.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: Pandas

---

## [CombineEphemeris.py](CombineEphemeris.py)

This tool combines multiple STK ephemerides (.e) into one long STK ephemeris. It takes as arguments a directory containing the .e files to join and the name and directory of the new, conjoined ephemeris. The script will create an STK scenario.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: Pandas

---

## [KmlToStkEphemeris.py](KmlToStkEphemeris.py)

This utility will parse a KML file (.kml) from ForeFlight, FlightAware and possibly other flight log software and output an STK ephemeris file (.e). This file can be loaded into an STK scenario by inserting an aircraft object, changing the propagator to "StkExternal" and specifying the filepath to the ephemeris file.

### Dependencies

* Licenses: N/A
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: Beautiful Soup 4

---

## [writeCsvToPg](writeCsvToPg)

This is a standalone script that writes a csv with columns Time in UTCG, Lat, Lon, and Alt to a great arc propagator file (.pg). Inputs of vehicle ID and full csv path are prompted from the user. Output is a .pg file following terrain in the same directory that can be imported into any STK object with a great arc propagator. Example .csv file for a ground vehicle is included.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A
* Third-Party Libraries: N/A

---

## [LatLonGridProjection.py](LatLonGridProjection.py)

Using STK Primitives, creates a latitude/longitude grid at a specifiable altitude off the globe (km) and line spacing (deg).

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [convertTLEState.py](convertTleState.py)

This simple function requires an open STK 12 scenario with a satellite that has been propagated with the SGP4 propagator. This function will pull out the initial state of all the SGP4 satellites in the scenario in the ICRF frame then create and propagate a new satellite of the specified propagator type using that initial state.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [getAllObjectHandles.py](getAllObjectHandles.py)

Connects to an existing STK instance and loops through every object in the scenario while assigning a local Python IDE variable with the same name as each object pointing to that object. This is commonly used for debugging getting more comfortable with the object model. Users can create complex scenarios manually and then run getAllObjectHandles to navigate through the object model and inspect properties.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: Any scenario open with at least one object

---

## [stkSocket.py](stkSocket.py)

Allows the user to open a TCP/IP socket to send Connect commands to STK using default IP address (localhost) and port (5001). Changing default host address and port can be completed in the Edit -> Preferences -> Connect section in STK's GUI.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [sunObscuration.py](sunObscuration.py)

This automates the use of the Sensor Obscuration Tool to determine when the sun's disk is in the FOV of a sensor. The script takes inputs of a sensor path, step size in seconds, and save location for a calc scalar file. The script executes over the scenario analysis period and writes a calculation scalar file that represents the percent obscuration of the sensor's FOV by the sun's disk. This gets imported back into STK and is used as a condition, where the condition is satisfied if the percent obscuration is greater than zero. This condition is used to create a satisfaction interval when the sun is in the sensor FOV. This script can be easily adapted for other obscuring objects as well.

### Dependencies

* Licenses: [STK Pro](https://www.ansys.com/content/dam/amp/2022/june/webpage-requests/stk-product-page/brochures/stk-pro-brochure.pdf)
* Other Scripts: N/A
* Scenario: N/A

---

## [writeComplexBistaticRCS.py](writeCopmlexBistaticRCS.py)

This accepts a bistatic RCS export from Ansys HFSS and converts it into *.rcs format for import into STK. Inputs are the horizontal and vertical incident data file paths, the primary polarization channel (horizontal or vertical), and the output file path.

### Dependencies

* Licenses: N/A
* Other Scripts: N/A
* Scenario: N/A