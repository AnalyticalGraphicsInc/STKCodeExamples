# STK Metadata Extractor

This script will read an STK scenario with an instance of STK Engine to extract metadata from that scenario.

As a Code Example, this script should demonstrate how users can iterate through all objects in a scenario and interrogate each object type for information related to that specific type.

This is a work in progress but the goal will be to create a summary file (formatted in XML, JSON or other) to briefly describe the scenario contents.

# Usage

This script is intended to be used from the command line with two arguments: 
1. the path to a scenario file (*.sc or *.vdf).
2. the desired path of the output file (*.xml or *.json) 

Example:
python StkMetadataExtractor.py "c:\Temp\sampleScenario.vdf" "c:\Temp\summary.xml"

The output summary.xml file will briefly describe the contents of the scenario.




## License

The Code Examples in this repository are licensed under the AGI Code Examples License Agreement, which is included in this repository as [License.pdf](License.pdf).

## Redistribution
If You redistribute the Code Examples, in whole or in part, You must provide a copy of this License Agreement to any other recipient of the Code Examples, and include the following copyright notice: 

© 2020 Analytical Graphics, Inc.

## What if I have questions about STK

Contact support@agi.com with any questions regarding STK, STK Engine or any other AGI products.

AGI's ready-to-use STK and ODTK families of products, enterprise software, and developer tools help customers deliver digital engineering value and make better-informed decisions in a mission context at any stage in the program lifecycle: from planning and design to training and operations.  

For more information, please visit the [AGI website](https://www.agi.com "AGI's Homepage"). 
