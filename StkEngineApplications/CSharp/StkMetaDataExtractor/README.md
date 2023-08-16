## STK Metadata Extractor

This STK Engine app will read an STK scenario with an instance of STK Engine to extract metadata from that scenario about each of the objects. It will also export a Cesium display summary file.

As a Code Example, this utility should demonstrate how users can iterate through all objects in a scenario and interrogate each object type for information related to that specific object type.

This is a work in progress but the goal will be to create a summary file (formatted in XML, JSON or other) to briefly describe the scenario contents.

### Usage

This utility is intended to be used from the command line with two arguments: 
1. the path to a scenario file (*.sc or *.vdf).
2. the desired directory for the output files (*.xml and *.czml)

Example:
StkMetadataExtractor.exe "c:\Temp\sampleScenario.vdf" "c:\Temp"

The output XML file and CZML file will have the same name as the scenario - in this example "c:\Temp\sampleScenario.xml" and "c:\Temp\sampleScenario.czml".
