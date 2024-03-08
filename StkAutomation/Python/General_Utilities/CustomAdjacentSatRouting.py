import math

def createRoutingFile(filePath, objUsed, name, shells, planes, satsPerPl, comms, commInfo, planeReach, satReach):
    # Objective: Create a routing file that configures every satellite in a constellation/collection to only be able to link
    # to satellites within a specified, circular grid. This includes X satellites in front and behind in the same plane, as well as satellites
    # in the same position, but X planes over.
    #
    # Example: Satellites in a constellation are allowed to link to other satellites within 2 satellite positions away. This creates a circular
    # area with a radius of two satellite positions. Therefore, a satellite located in P3S3 (plane 3, satellite position 3) can talk to P3S5,
    # P3S1, P1S3, and P5S3 at the extremes. In addition, it can talk to satellites located "diagonally" as long as they are less than 2 
    # "satellite units" away. Thus, P3S3 can talk to P2S2, P2S4, P4S2, and P4S4. However, P3S3 cannot talk to P5S5, P1S5, etc since the
    # distance of these satellites are more than 2 ( sqrt(2^2 + 2^2) = 2.8284 ).
    #
    # Inputs:
    # filePath (string) - Full filepath, including file name for *.routing file (i.e. C:/Users/username/Documents/example.routing)
    # objUsed (int) - A number selecting either the use of a constellation or satellite collection object (0: Constellation | 1: Satellite Collection)
    # name (string) - Name of the seed satellite of a constellation or the name of the satellite collection. This name is included in the naming
    # of the children satellites.
    # shells (int) - Shell ID number used in a satellite collection. If a constellation object is used, ignore this value.
    # planes (int) - Number of planes used in your constellation/satellite collection.
    # satsPerPl (int) - Number of satellites per plane used in your constellation/satellite collection.
    # comms (int) - Boolean determining whether communication objects will be used or not.
    # commInfo (string) - If communication objects are used, this matrix contains the name of the seed receiver, seed transmitter, and (if using a
    # satellite collection) downlink object path.
    # planeReach (int) - Amount of satellites you would like each satellite to communicate across intra-planes. (planeReach of 2 allows Sat1 in
    # plane 1 to talk to Sat1 in plane 3.)
    # satReach (int) - Amount of satellites you would like each satellite to communicate across within the plane. (satReach of 2 allows Sat1 in
    # plane 1 to talk to Sat3 in plane 1.)
    #
    # Output: A *.routing file saved into the specified directory.
    if (objUsed != 1 and objUsed != 0):
        raise ValueError('Please enter a valid value for objectUsed')
    else:
        try:
            file = open(filePath, "w")
        except:
            raise ValueError('Please enter a valid file path')

    if (objUsed == 0):
        # Constellations
        if (comms == 0):
            # Constellation w/o Comm Objects
            file.write("Begin MultihopRules\n\tDefaultRule Never\n\n\tBegin Rules\n")
            for i in range(len(shells)):
                for j in range(planes[i]):
                    for k in range(satsPerPl[i]):
                        for m in range(planeReach+1):
                            for n in range(satReach+1):
                                line = ""
                                sat1_plane = j+1
                                sat1_sat = k+1
                                sat2_plane = sat1_plane + m
                                sat2_sat = sat1_sat + n
                                sat3_sat = sat1_sat - n
                                sat2_plane, sat2_sat, sat3_sat, xtra0 = constructSyntax(sat1_plane, sat1_sat, sat2_plane, sat2_sat, sat3_sat, planes[i], satsPerPl[i])
                                if (m == 0):
                                    if (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.pi/2)**2+satReach**2*math.cos(math.pi/2)**2)) and n + m != 0):
                                        line = "\t\tAccess Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + " Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[3] + str(sat2_sat) + " reciprocal\n"
                                else:
                                    if (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.atan(n/m))**2+satReach**2*math.cos(math.atan(n/m))**2)) and n == 0):
                                        line = "\t\tAccess Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + " Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[3] + str(sat2_sat) + " reciprocal\n"
                                    elif (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.atan(n/m))**2+satReach**2*math.cos(math.atan(n/m))**2))):
                                        line = "\t\tAccess Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + " Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[3] + str(sat2_sat) + " reciprocal\n"
                                        line += "\t\tAccess Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + " Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[4] + str(sat3_sat) + " reciprocal\n"
                                file.write(line)
            file.write("\tEnd Rules\nEnd MultihopRules")
        elif (comms == 1):
            # Constellation w/ Comm Objects
            file.write("Begin IncomingRules\n\tDefaultRule Never\n\n\tBegin Rules\n")
            file.write('\t\tAccess ' + str(commInfo[3]) + ' Satellite/*/Receiver/*\n')
            for i in range(len(shells)):
                for j in range(planes[i]):
                    for k in range(satsPerPl[i]):
                        line = ""
                        sat1_plane = j+1
                        sat1_sat = k+1
                        sat2_plane = sat1_plane
                        sat2_sat = sat1_sat
                        sat3_sat = sat2_sat
                        sat2_plane, sat2_sat, sat3_sat, xtra0 = constructSyntax(sat1_plane, sat1_sat, sat2_plane, sat2_sat, sat3_sat, planes[i], satsPerPl[i])
                        line = "\t\tAccess Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + "/Receiver/" + commInfo[1] + str((sat1_plane-1)*satsPerPl[i] + sat1_sat) + \
                        " Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + "/Transmitter/" + commInfo[0] + str((sat1_plane-1)*satsPerPl[i] + sat1_sat) + "\n"
                        file.write(line)
            file.write("\tEnd Rules\nEnd IncomingRules\n\n")
            
            file.write("Begin OutgoingRules\n\tDefaultRule Never\n\n\tBegin Rules\n")
            file.write('\t\tAccess Satellite/*/Transmitter/*' + ' ' + str(commInfo[2]) + '\n')
            for i in range(len(shells)):
                for j in range(planes[i]):
                    for k in range(satsPerPl[i]):
                        for m in range(planeReach+1):
                            for n in range(satReach+1):
                                line = ""
                                sat1_plane = j+1
                                sat1_sat = k+1
                                sat2_plane = sat1_plane + m
                                sat2_sat = sat1_sat + n
                                sat3_sat = sat1_sat - n
                                sat2_plane, sat2_sat, sat3_sat, xtra0 = constructSyntax(sat1_plane, sat1_sat, sat2_plane, sat2_sat, sat3_sat, planes[i], satsPerPl[i])
                                if (m == 0):
                                    if (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.pi/2)**2+satReach**2*math.cos(math.pi/2)**2)) and n + m != 0):
                                        line = "\t\tAccess Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + "/Transmitter/" + commInfo[0] + str((sat1_plane-1)*satsPerPl[i] + sat1_sat) + \
                                        " Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[3] + str(sat2_sat) + "/Receiver/" + commInfo[1] + str((sat2_plane-1)*satsPerPl[i] + sat2_sat) + "\n"
                                        line += "\t\tAccess Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[3] + str(sat2_sat) + "/Transmitter/" + commInfo[0] + str((sat2_plane-1)*satsPerPl[i] + sat2_sat) + \
                                        " Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + "/Receiver/" + commInfo[1] + str((sat1_plane-1)*satsPerPl[i] + sat1_sat) + "\n"
                                else:
                                    if (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.atan(n/m))**2+satReach**2*math.cos(math.atan(n/m))**2)) and n == 0):
                                        line = "\t\tAccess Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + "/Transmitter/" + commInfo[0] + str((sat1_plane-1)*satsPerPl[i] + sat1_sat) + \
                                        " Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[3] + str(sat2_sat) + "/Receiver/" + commInfo[1] + str((sat2_plane-1)*satsPerPl[i] + sat2_sat) + "\n"
                                        line += "\t\tAccess Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[3] + str(sat2_sat) + "/Transmitter/" + commInfo[0] + str((sat2_plane-1)*satsPerPl[i] + sat2_sat) + \
                                        " Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + "/Receiver/" + commInfo[1] + str((sat1_plane-1)*satsPerPl[i] + sat1_sat) + "\n"
                                    elif (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.atan(n/m))**2+satReach**2*math.cos(math.atan(n/m))**2))):
                                        line = "\t\tAccess Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + "/Transmitter/" + commInfo[0] + str((sat1_plane-1)*satsPerPl[i] + sat1_sat) + \
                                        " Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[3] + str(sat2_sat) + "/Receiver/" + commInfo[1] + str((sat2_plane-1)*satsPerPl[i] + sat2_sat) + "\n"
                                        line += "\t\tAccess Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[3] + str(sat2_sat) + "/Transmitter/" + commInfo[0] + str((sat2_plane-1)*satsPerPl[i] + sat2_sat) + \
                                        " Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + "/Receiver/" + commInfo[1] + str((sat1_plane-1)*satsPerPl[i] + sat1_sat) + "\n"
                                        line += "\t\tAccess Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + "/Transmitter/" + commInfo[0] + str((sat1_plane-1)*satsPerPl[i] + sat1_sat) + \
                                        " Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[4] + str(sat3_sat) + "/Receiver/" + commInfo[1] + str((sat2_plane-1)*satsPerPl[i] + sat3_sat) + "\n"
                                        line += "\t\tAccess Satellite/" + name + xtra0[2] + str(sat2_plane) + xtra0[4] + str(sat3_sat) + "/Transmitter/" + commInfo[0] + str((sat2_plane-1)*satsPerPl[i] + sat3_sat) + \
                                        " Satellite/" + name + xtra0[0] + str(sat1_plane) + xtra0[1] + str(sat1_sat) + "/Receiver/" + commInfo[1] + str((sat1_plane-1)*satsPerPl[i] + sat1_sat) + "\n"
                                file.write(line)
            file.write("\tEnd Rules\nEnd OutgoingRules")
        else:
            raise ValueError('Please enter a valid value for comms')
    elif (objUsed == 1):
        # Satellite Collections
        if (comms == 0):
            # Satellite Collections w/o Comm Objects
            file.write("Begin MultihopRules\n\tDefaultRule Never\n\n\tBegin Rules\n")
            for i in range(len(shells)):
                for j in range(planes[i]):
                    for k in range(satsPerPl[i]):
                        for m in range(planeReach+1):
                            for n in range(satReach+1):
                                line = ""
                                sat1_plane = j+1
                                sat1_sat = k+1
                                sat2_plane = sat1_plane + m
                                sat2_sat = sat1_sat + n
                                sat3_sat = sat1_sat - n
                                sat2_plane, sat2_sat, sat3_sat, xtra0 = constructSyntax(sat1_plane, sat1_sat, sat2_plane, sat2_sat, sat3_sat, planes[i], satsPerPl[i])
                                if (m == 0):
                                    if (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.pi/2)**2+satReach**2*math.cos(math.pi/2)**2)) and n + m != 0):
                                        line = '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllSatellites Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + \
                                        "_S" + xtra0[1] + str(sat1_sat) + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllSatellites Shell_' + str(shells[i]) + "_P" + xtra0[2] + \
                                        str(sat2_plane) + "_S" + xtra0[3] + str(sat2_sat) + '" reciprocal\n'
                                else:
                                    if (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.atan(n/m))**2+satReach**2*math.cos(math.atan(n/m))**2)) and n == 0):
                                        line = '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllSatellites Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + \
                                        "_S" + xtra0[1] + str(sat1_sat) + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllSatellites Shell_' + str(shells[i]) + "_P" + xtra0[2] + \
                                        str(sat2_plane) + "_S" + xtra0[3] + str(sat2_sat) + '" reciprocal\n'
                                    elif (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.atan(n/m))**2+satReach**2*math.cos(math.atan(n/m))**2))):
                                        line = '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllSatellites Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + \
                                        "_S" + xtra0[1] + str(sat1_sat) + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllSatellites Shell_' + str(shells[i]) + "_P" + xtra0[2] + \
                                        str(sat2_plane) + "_S" + xtra0[3] + str(sat2_sat) + '" reciprocal\n'
                                        line += '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllSatellites Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + \
                                        "_S" + xtra0[1] + str(sat1_sat) + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllSatellites Shell_' + str(shells[i]) + "_P" + xtra0[2] + \
                                        str(sat2_plane) + "_S" + xtra0[4] + str(sat3_sat) + '" reciprocal\n'
                                file.write(line)
            file.write("\tEnd Rules\nEnd MultihopRules")
        elif (comms == 1):
            # Satellite Collections w/ Comm Objects
            file.write("Begin OutgoingRules\n\tDefaultRule Never\n\n\tBegin Rules\n")
            file.write('\t\tAccess "CollectionSubsetEntry SatelliteCollection/')
            file.write(name)
            file.write('/Subset/AllTransmitters *.Transmitter.' + str(commInfo[0]) + '" "' + str(commInfo[2]) + '"\n')
            for i in range(len(shells)):
                for j in range(planes[i]):
                    for k in range(satsPerPl[i]):
                        for m in range(planeReach+1):
                            for n in range(satReach+1):
                                line = ""
                                sat1_plane = j+1
                                sat1_sat = k+1
                                sat2_plane = sat1_plane + m
                                sat2_sat = sat1_sat + n
                                sat3_sat = sat1_sat - n
                                sat2_plane, sat2_sat, sat3_sat, xtra0 = constructSyntax(sat1_plane, sat1_sat, sat2_plane, sat2_sat, sat3_sat, planes[i], satsPerPl[i])
                                if (m == 0):
                                    if (n == 0):
                                        line = '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllReceivers Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + "_S" + xtra0[1] + str(sat1_sat) + \
                                        '.Receiver.' + commInfo[1] + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllTransmitters Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + "_S" + xtra0[1] + \
                                        str(sat1_sat) + '.Transmitter.' + commInfo[0] + '"\n'
                                    elif (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.pi/2)**2+satReach**2*math.cos(math.pi/2)**2)) and n + m != 0):
                                        line = '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllTransmitters Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + "_S" + xtra0[1] + str(sat1_sat) + \
                                        '.Transmitter.' + commInfo[0] + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllReceivers Shell_' + str(shells[i]) + "_P" + xtra0[2] + str(sat2_plane) + "_S" + xtra0[3] + \
                                        str(sat2_sat) + '.Receiver.' + commInfo[1] + '"\n'
                                        line += '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllTransmitters Shell_' + str(shells[i]) + "_P" + xtra0[2] + str(sat2_plane) + "_S" + xtra0[3] + str(sat2_sat) + \
                                        '.Transmitter.' + commInfo[0] + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllReceivers Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + "_S" + xtra0[1] + \
                                        str(sat1_sat) + '.Receiver.' + commInfo[1] + '"\n'
                                else:
                                    if (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.atan(n/m))**2+satReach**2*math.cos(math.atan(n/m))**2)) and n == 0):
                                        line = '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllTransmitters Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + "_S" + xtra0[1] + str(sat1_sat) + \
                                        '.Transmitter.' + commInfo[0] + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllReceivers Shell_' + str(shells[i]) + "_P" + xtra0[2] + str(sat2_plane) + "_S" + xtra0[3] + \
                                        str(sat2_sat) + '.Receiver.' + commInfo[1] + '"\n'
                                        line += '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllTransmitters Shell_' + str(shells[i]) + "_P" + xtra0[2] + str(sat2_plane) + "_S" + xtra0[3] + str(sat2_sat) + \
                                        '.Transmitter.' + commInfo[0] + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllReceivers Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + "_S" + xtra0[1] + \
                                        str(sat1_sat) + '.Receiver.' + commInfo[1] + '"\n'
                                    elif (math.sqrt(n**2 + m**2) <= (planeReach*satReach)/(math.sqrt(planeReach**2*math.sin(math.atan(n/m))**2+satReach**2*math.cos(math.atan(n/m))**2))):
                                        line = '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllTransmitters Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + "_S" + xtra0[1] + str(sat1_sat) + \
                                        '.Transmitter.' + commInfo[0] + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllReceivers Shell_' + str(shells[i]) + "_P" + xtra0[2] + str(sat2_plane) + "_S" + xtra0[3] + \
                                        str(sat2_sat) + '.Receiver.' + commInfo[1] + '"\n'
                                        line += '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllTransmitters Shell_' + str(shells[i]) + "_P" + xtra0[2] + str(sat2_plane) + "_S" + xtra0[3] + str(sat2_sat) + \
                                        '.Transmitter.' + commInfo[0] + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllReceivers Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + "_S" + xtra0[1] + \
                                        str(sat1_sat) + '.Receiver.' + commInfo[1] + '"\n'
                                        line += '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllTransmitters Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + "_S" + xtra0[1] + str(sat1_sat) + \
                                        '.Transmitter.' + commInfo[0] + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllReceivers Shell_' + str(shells[i]) + "_P" + xtra0[2] + str(sat2_plane) + "_S" + xtra0[4] + \
                                        str(sat3_sat) + '.Receiver.' + commInfo[1] + '"\n'
                                        line += '\t\tAccess "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllTransmitters Shell_' + str(shells[i]) + "_P" + xtra0[2] + str(sat2_plane) + "_S" + xtra0[4] + str(sat3_sat) + \
                                        '.Transmitter.' + commInfo[0] + '" "CollectionSubsetEntry SatelliteCollection/' + str(name) + '/Subset/AllReceivers Shell_' + str(shells[i]) + "_P" + xtra0[0] + str(sat1_plane) + "_S" + xtra0[1] + \
                                        str(sat1_sat) + '.Receiver.' + commInfo[1] + '"\n'
                                file.write(line)


                        #line = writeSamePlane_SC_Comm(name, shells[i], j+1, k+1, satsPerPl[i])
                        #line = writeAdjacentPlane_SC_Comm(name, line, shells[i], j+1, k+1, planes[i])
                        #file.write(line)
            file.write("\tEnd Rules\nEnd OutgoingRules")
        else:
            raise ValueError('Please enter a valid value for comms')

def constructSyntax(sat1_plane, sat1_sat, sat2_plane, sat2_sat, sat3_sat, planes, satsPerPl):
    if (sat2_plane > planes):
        sat2_plane = sat2_plane - planes
    if (sat2_sat > satsPerPl):
        sat2_sat = sat2_sat - satsPerPl
    if (sat3_sat <= 0):
        sat3_sat = satsPerPl - sat3_sat
    xtra0 = ["", "", "", "", ""]
    if (planes >= 10):
        if (sat1_plane < 10):
            xtra0[0] = "0"
        if (sat2_plane < 10):
            xtra0[2] = "0"
    if (satsPerPl >= 10):
        if (sat1_sat < 10):
            xtra0[1] = "0"
        if (sat2_sat < 10):
            xtra0[3] = "0"
        if (sat3_sat < 10):
            xtra0[4] = "0"
    return sat2_plane, sat2_sat, sat3_sat, xtra0

filePath = "C:\\Users\\username\\Documents\\Python\\sample.routing" # Ex: "C:\\Users\\username\\Documents\\adjacent.routing"
objectUsed = 0 # Constellation: 0 | Satellite Collection: 1
objName = "SeedSat" # Constellation: Name of seed satellite | Satellite Collection: Name of Satellite Collection
shells = [1] # Ignore if using Constellations
planes = [10]
satsPerPlane = [20]
comms = 1 # Without communication objects: 0 | With communication objects: 1
commInfo = ["Tx", "Rx", "Place/Finish/Receiver/Receiver2", "Place/Start/Transmitter/Transmitter2"] # If using communication objects, enter the name of the reference/seed satellite's transmitter, the name of the reference/seed satellite's receiver, the object path of the receiver receiving the downlink, then the object path of the transmitter transmitting the uplink (Ex: ["Transmitter1", "Receiver1", "Facility/Facility2/Receiver/Receiver2", "Facility/Facility1/Transmitter/Transmitter2"])
# WARNING: In the commInfo variable, if the transmitter/receiver name ends with a number, it will break the routing file for a Constellation object. If using a Constellation object, please remove any numbers at the end of your Transmitter's/Receiver's name.
planeReach = 1
satReach = 1

createRoutingFile(filePath, objectUsed, objName, shells, planes, satsPerPlane, comms, commInfo, planeReach, satReach)