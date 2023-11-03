import csv
import re

# Inputs
H_Incident_File_Path = r"C:\Bistatic_H_Incident.csv"
V_Incident_File_Path = r"C:\Bistatic_V_Incident.csv"
primaryPol = 'H' # H or V
outputFilePath = r"C:\Output.rcs"

with open(H_Incident_File_Path, mode='r') as hIncidentFile, open(V_Incident_File_Path, mode='r') as vIncidentFile:
    # Open and assign primary/orthogonal polarization data
    if primaryPol == 'H':
        primaryPolData = csv.reader(hIncidentFile)
        orthogonalPolData = csv.reader(vIncidentFile)
    elif primaryPol == 'V':
        primaryPolData = csv.reader(vIncidentFile)
        orthogonalPolData = csv.reader(hIncidentFile)
    else:
        print('Invalid primary polarization specified. Use H or V.')
        quit()

    # Read headers
    primaryPolHeader = next(primaryPolData)[1:]
    orthogonalPolHeader = next(orthogonalPolData)[1:]

    outputData = {}
    print('Processing primary polarization data...')
    for primaryPolRow in primaryPolData:

        incidentRho = primaryPolRow[0]
        for idx, element in enumerate(primaryPolRow[1:]):
            # Parse header for incident and reflected geometry
            incidentTheta = re.search(r"(?<=IWavePhi=').*?(?=[a-zA-Z])", primaryPolHeader[idx]).group(0)
            reflectedRho = re.search(r"(?<=Theta=').*?(?=[a-zA-Z])", primaryPolHeader[idx]).group(0)
            reflectedTheta = re.search(r"(?<= Phi=').*?(?=[a-zA-Z])", primaryPolHeader[idx]).group(0)
            geometryInfo = " ".join([incidentRho, incidentTheta, reflectedRho, reflectedTheta])

            # Remove whitespace, then convert to engineering notation j for imaginary number
            element = element.replace(' ', '')
            element = complex(element.replace('i', 'j'))

            # Store data in dictionary
            if geometryInfo in outputData:
                outputData[geometryInfo] += ' ' + str(element.real) + ' ' + str(element.imag)
            else:
                outputData[geometryInfo] = str(element.real) + ' ' + str(element.imag)
            
    print('Processing orthogonal polarization data...')
    for orthogonalPolRow in orthogonalPolData:
        incidentRho = orthogonalPolRow[0]
        for idx, element in reversed(list(enumerate(orthogonalPolRow[1:]))):
            # Parse header for incident and reflected geometry
            incidentTheta = re.search(r"(?<=IWavePhi=').*?(?=[a-zA-Z])", orthogonalPolHeader[idx]).group(0)
            reflectedRho = re.search(r"(?<=Theta=').*?(?=[a-zA-Z])", orthogonalPolHeader[idx]).group(0)
            reflectedTheta = re.search(r"(?<= Phi=').*?(?=[a-zA-Z])", orthogonalPolHeader[idx]).group(0)
            geometryInfo = " ".join([incidentRho, incidentTheta, reflectedRho, reflectedTheta])

            # Remove whitespace, then convert to engineering notation j for imaginary number
            element = element.replace(' ', '')
            element = complex(element.replace('i', 'j'))

            # Store data in dictionary
            outputData[geometryInfo] += ' ' + str(element.real) + ' ' + str(element.imag)

# Write output
print('Writing output RCS file...')
with open(outputFilePath, 'w') as outputFile:
    # Write header
    outputFile.write('BEGIN RCS\n')
    outputFile.write('RCSPatternType BiComplexScatterSparse\n')
    outputFile.write('RCSScatterMatrixBasis Horizontal_Vertical_OrthogonalPair\n')
    outputFile.write('RhoAxis RhoFromX\n')
    outputFile.write('NumPoints ' + str(len(outputData)) + '\n')
    outputFile.write('BEGIN rcsdata\n')
    
    # Write data
    for key, value in outputData.items(): 
        outputFile.write('%s %s\n' % (key, value))
        
    # Write footer
    outputFile.write('END rcsdata\n')
    outputFile.write('END RCS')

print('Done!')