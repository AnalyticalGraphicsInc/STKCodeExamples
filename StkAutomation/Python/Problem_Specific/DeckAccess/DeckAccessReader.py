# Helper functions to create MTOs (visual representations of objects, no analysis)

# Run a deck access report then the writeTLE function can be called to create a TLE file with all objects fromt the deck access report.


# Deck Access Report Format
# =============================================================================
#                                                            2 Jul 2019 08:50:41
# Facility-Facility1
# 
# 
#  Name        Start Time (UTCG)           Stop Time (UTCG)        Duration (sec)
# -----    ------------------------    ------------------------    --------------
# 00124    19 Jun 2019 16:00:00.000    19 Jun 2019 16:00:00.177             0.177
# 00020    19 Jun 2019 16:00:00.000    19 Jun 2019 16:00:00.194             0.194
# 00054    19 Jun 2019 16:00:00.000    19 Jun 2019 16:00:00.540             0.540
# 00040    19 Jun 2019 16:00:00.000    19 Jun 2019 16:00:03.785             3.785
# =============================================================================

# Data begins at line 7
# SCID = cols 0-4


def readDeck(deckAccessRpt):
    
    report = open(deckAccessRpt, "r")
    lines = report.readlines()
    scn = []
    for i in range(6, len(lines)):
        tokenLine = lines[i].split()
        scid = tokenLine[0]
        if scid in scn:
            #do nothing
            scid = scid
        else:
            scn.append(scid) 
    report.close()
    #print(len(scn))
    return scn
#readDeck()
# Able to get unique spacecraft id's out of D.A. Report

def getTLEs(TLEFile,deckAccessRpt):
    
    tleFile = open(TLEFile, "r")
    scnList = readDeck(deckAccessRpt)
   # print(len(scnList))
    tleList = []
    tles = tleFile.readlines()
    #print(scnList[7])
    for i in range(1, int(round(len(tles)/2))):
        line = tles[2*i - 1].split()
        #print(line[1])
        if line[1] in scnList:
            tleList.append(tles[2*i - 2])
            tleList.append(tles[2*i - 1])
    #print(len(tleList))
    #print(tleList)
    tleFile.close()
    return tleList

def writeTLEs(TLEFile,deckAccessRpt,deckAccessTLE):
    
    satFile = open(deckAccessTLE, "w")
    tleList = getTLEs(TLEFile,deckAccessRpt)
    for item in tleList:
        satFile.write("%s" % item)
    satFile.close()
    return int(len(tleList)/2)
    
def FilterObjectsByType(objType,name = ''):
    from comtypes.client import GetActiveObject
    # Attach to STK
    app = GetActiveObject('STK12.Application')
    root = app.Personality2
    # Send objects to an xml
    xml = root.AllInstanceNamesToXML()

    # split the xml by object paths
    objs = xml.split('path=')
    objs = objs[1:-1] # remove first string of '<'

    # Loop through each object and parse by object path
    objPaths = []

    for i in range(len(objs)):
        obji = objs[i].split('"')
        objiPath = obji[1] # the 2nd string is the file path
        objiSplit = objiPath.split('/')
        objiClass = objiSplit[-2]
        objiName = objiSplit[-1]
        if objiClass.lower() == objType.lower():
            if name.lower() in objiName.lower():
                objPaths.append(objiPath)
    return objPaths
	

