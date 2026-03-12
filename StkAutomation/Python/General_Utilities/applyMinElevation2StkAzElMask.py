"""
This script takes an STK-generated Az/El mask file (*.aem) and applies
a minimum elevation angle to it. Any az/el pairs that have an elevation
lower than the minEl input value shall be set to minEl. The resulting
Az/El mask file is then written to a new *.aem file.
"""

inFilePath = 'Facility1.aem'
outFilePath = 'Facility1_modified.aem'
minEl = 5.0  # deg

readingData = False
with open(inFilePath, 'r') as inFile:
    # Read input STK AEM file
    inFileList = list(inFile)

    with open(outFilePath, 'w') as outFile:
        # Write output header
        i = 0
        while len(inFileList[i].strip().split()) != 3:
            outFile.write(inFileList[i])
            i+=1

        # Begin writing data
        while i < len(inFileList):
            # Read azimuth block header
            azimuth = float(inFileList[i].strip().split()[0])
            azData = []
            i+=1

            # Read azimuth block data
            dataLine = inFileList[i].strip().split()
            while len(dataLine) == 2:
                el = float(dataLine[0])
                r = float(dataLine[1])
                # Handle case where terrain mask elevation is lower than minimum specified elevation
                if el < minEl and (len(inFileList[i+1].strip().split()) == 0 or float(inFileList[i+1].strip().split()[0]) > minEl):
                    azData.append((minEl, r))
                # Handle case where terrain mask elevation is higher than minimum specified elevation
                elif el > minEl:
                    azData.append((el, r))
                # Read next azimuth data line
                i+=1
                dataLine = inFileList[i].strip().split()

            # Done parsing azimuth block, write azimuth header and data block to file
            outFile.write(f'{azimuth} {azData[-1][0]} {len(azData)}\n')
            for line in azData:
                outFile.write(f'{line[0]} {line[1]}\n')
            outFile.write('\n')

            # Next azimuth block to read
            i+=1