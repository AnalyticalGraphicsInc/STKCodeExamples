import math


def createRoutingFile(
    filePath,
    objUsed,
    name,
    shells,
    planes,
    satsPerPl,
    comms,
    commInfo,
    planeReach,
    satReach,
    shape,
    skip
):
    """Objective: Create a routing file that configures every satellite in a
    constellation/collection to only be able to link to satellites within a specified,
    circular grid. This includes X satellites in front and behind in the same plane,
    as well as satellites in the same position, but X planes over.

    Example: Satellites in a constellation are allowed to link to other satellites
    within 2 satellite positions away. This creates a circular area with a radius of
    two satellite positions. Therefore, a satellite located in P3S3 (plane 3, satellite
    position 3) can talk to P3S5, P3S1, P1S3, and P5S3 at the extremes. In addition, it
    can talk to satellites located "diagonally" as long as they are less than 2
    "satellite units" away. Thus, P3S3 can talk to P2S2, P2S4, P4S2, and P4S4. However,
    P3S3 cannot talk to P5S5, P1S5, etc since the distance of these satellites are more
    than 2 ( sqrt(2^2 + 2^2) = 2.8284 ).

    Output: A *.routing file saved into the specified directory.

    Args:
        filePath (str): Full filepath, including file name for *.routing file
        (i.e. C:/Users/username/Documents/example.routing)
        objUsed (int): A number selecting either the use of a constellation or satellite
        collection object (0: Constellation | 1: Satellite Collection)
        name (str): Name of the seed satellite of a constellation or the name of the
        satellite collection. This name is included in the naming of the children
        satellites.
        shells (int/string): Shell ID number or name used in a satellite collection. 
        If a constellation object is used, ignore this value.
        planes (int): Number of planes used in your constellation/satellite collection.
        satsPerPl (int): Number of satellites per plane used in your
        constellation/satellite collection.
        comms (bool): Boolean determining whether communication objects will be used or
        not.
        commInfo (str): If communication objects are used, this matrix contains the name
        of the seed receiver, seed transmitter, and (if using a satellite collection)
        downlink object path.
        planeReach (int): Amount of satellites you would like each satellite to
        communicate across intra-planes. (planeReach of 2 allows Sat1 in plane 1 to talk
        to Sat1 in plane 3.)
        satReach (int): Amount of satellites you would like each satellite to
        communicate across within the plane. (satReach of 2 allows Sat1 in plane 1 to
        talk to Sat3 in plane 1.)
        shape (int): A number selecting the shape of the mesh used for the routing file.
        See bottom of the script for visuals. (0: slanted mesh | 1: "W" shaped mesh)
        skip (bool): Boolean determining whether you would like to skip every other
        connection. This would allow you to toggle between your satellites having four
        communication terminals/links or three communication terminals/links.
    """
    if objUsed != 1 and objUsed != 0:
        raise ValueError("Please enter a valid value for objectUsed")
    else:
        try:
            file = open(filePath, "w")
        except:
            raise ValueError("Please enter a valid file path")

    if objUsed == 0:
        # Constellations
        if comms == 0:
            # Constellation w/o Comm Objects
            # Writing routing file header information
            file.write("Begin MultihopRules\n\tDefaultRule Never\n\n\tBegin Rules\n")
            # Begin looping through routing file contents
            for j in range(planes[0]): # Loop through each plane
                for k in range(satsPerPl[0]): # Loop through each satellite in each plane
                    for m in range(planeReach + 1): # Loop through all possible connections intra-plane
                        for n in range(satReach + 1): # Loop through all possible connections inter-plane
                            # Construct naming for current satellite and its connecting satellites
                            line = ""
                            sat1_plane = j + 1
                            sat1_sat = k + 1
                            sat2_plane = sat1_plane + m
                            sat2_sat = sat1_sat + n
                            sat3_sat = sat1_sat - n
                            sat1_plane_str, sat2_plane_str, sat1_sat_str, sat2_sat_str, sat3_sat_str = constructSyntax(
                                sat1_plane,
                                sat1_sat,
                                sat2_plane,
                                sat2_sat,
                                sat3_sat,
                                planes[0],
                                satsPerPl[0],
                            )
                            if m == 0: # Satellite connection is inter-plane
                                if (0 < n <= satReach):
                                    line = (constNoCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                            else:
                                if (n == 0 and m <= planeReach): # Satellite connection is intra-plane
                                    # The below if-else statement determines if a connection needs to be skipped or not according to the shape and skip input variables
                                    if (shape == 0):
                                        if (skip == 0 or (skip == 1 and (sat1_plane%2 + sat1_sat%2) == 1)):
                                            line = (constNoCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                    elif (shape == 1):
                                        if (sat1_plane % 2 == 0):
                                            continue
                                        elif (skip == 0 or (skip == 1 and ((sat1_plane//2)%2 + sat1_sat%2) == 1)):
                                            line = (constNoCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                elif math.sqrt(n**2 + m**2) <= (
                                    planeReach * satReach
                                ) / (
                                    math.sqrt(
                                        planeReach**2
                                        * math.sin(math.atan(n / m)) ** 2
                                        + satReach**2
                                        * math.cos(math.atan(n / m)) ** 2
                                    )
                                ): # Satellite connection is diagonal (reaching across planes and satellite positions). The diagonal is only valid if the connection is within the above ellipse equation.
                                    line = (constNoCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                    line += (constNoCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat3_sat_str))
                                elif (shape == 1 and sat1_plane % 2 == 0): # If shape == 1, the satellite connection intra-plane is a diagonal for every other plane
                                    if (skip == 0 or (skip == 1 and ((sat1_plane//2)%2 + sat1_sat%2) == 1)):
                                        line = (constNoCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat3_sat_str))
                            file.write(line) # Write line to routing file
            # Writing routing file footer
            file.write("\tEnd Rules\nEnd MultihopRules")
        elif comms == 1:
            # Constellation w/ Comm Objects
            # Writing routing file header information
            file.write("Begin IncomingRules\n\tDefaultRule Never\n\n\tBegin Rules\n")
            # Write a incoming rule contents (for Rx -> Tx connections on the same parent)
            file.write("\t\tAccess " + str(commInfo[3]) + " Satellite/*/Receiver/*\n")
            # Begin looping through incoming rule contents
            for j in range(planes[0]): # Loop through each plane
                for k in range(satsPerPl[0]): # Loop through each satellite in each plane
                    # Construct naming for current satellite and its connecting satellites
                    line = ""
                    sat1_plane = j + 1
                    sat1_sat = k + 1
                    sat2_plane = sat1_plane
                    sat2_sat = sat1_sat
                    sat3_sat = sat2_sat
                    sat1_plane_str, sat2_plane_str, sat1_sat_str, sat2_sat_str, sat3_sat_str = constructSyntax(
                        sat1_plane,
                        sat1_sat,
                        sat2_plane,
                        sat2_sat,
                        sat3_sat,
                        planes[0],
                        satsPerPl[0],
                    )
                    # Write Rx -> Tx connection on the same parent
                    line = (
                        "\t\tAccess Satellite/"
                        + name
                        + sat1_plane_str
                        + sat1_sat_str
                        + "/Receiver/"
                        + commInfo[1]
                        + str((sat1_plane - 1) * satsPerPl[0] + sat1_sat)
                        + " Satellite/"
                        + name
                        + sat1_plane_str
                        + sat1_sat_str
                        + "/Transmitter/"
                        + commInfo[0]
                        + str((sat1_plane - 1) * satsPerPl[0] + sat1_sat)
                        + "\n"
                    )
                    file.write(line)
            # Wrinting routing file incoming rules footer
            file.write("\tEnd Rules\nEnd IncomingRules\n\n")
            # Writing routing file outgoing rules header information (for different parent Tx -> Rx connections)
            file.write("Begin OutgoingRules\n\tDefaultRule Never\n\n\tBegin Rules\n")
            file.write(
                "\t\tAccess Satellite/*/Transmitter/*" + " " + str(commInfo[2]) + "\n"
            )
            # Begin looping through outgoing rule contents
            for j in range(planes[0]): # Loop through each plane
                for k in range(satsPerPl[0]): # Loop through each satellite in each plane
                    for m in range(planeReach + 1): # Loop through all possible connections intra-plane
                        for n in range(satReach + 1): # Loop through all possible connections inter-plane
                            # Construct naming for current satellite and its connecting satellites
                            line = ""
                            sat1_plane = j + 1
                            sat1_sat = k + 1
                            sat2_plane = sat1_plane + m
                            sat2_sat = sat1_sat + n
                            sat3_sat = sat1_sat - n
                            sat1_plane_str, sat2_plane_str, sat1_sat_str, sat2_sat_str, sat3_sat_str = constructSyntax(
                                sat1_plane,
                                sat1_sat,
                                sat2_plane,
                                sat2_sat,
                                sat3_sat,
                                planes[0],
                                satsPerPl[0],
                            )
                            if m == 0: # Satellite connection is inter-plane
                                if (0 < n <= satReach):
                                    line = (constCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str, satsPerPl[0]))
                                    line += (constCommsLine(name, sat2_plane_str, sat2_sat_str, sat1_plane_str, sat1_sat_str, satsPerPl[0]))
                            else:
                                if (n == 0 and m <= planeReach): # Satellite connection is intra-plane
                                    # The below if-else statement determines if a connection needs to be skipped or not according to the shape and skip input variables
                                    if (shape == 0):
                                        if (skip == 0 or (skip == 1 and (sat1_plane%2 + sat1_sat%2) == 1)):
                                            line = (constCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str, satsPerPl[0]))
                                            line += (constCommsLine(name, sat2_plane_str, sat2_sat_str, sat1_plane_str, sat1_sat_str, satsPerPl[0]))
                                    elif (shape == 1):
                                        if (sat1_plane % 2 == 0):
                                            continue
                                        elif (skip == 0 or (skip == 1 and ((sat1_plane//2)%2 + sat1_sat%2) == 1)):
                                            line = (constCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str, satsPerPl[0]))
                                            line += (constCommsLine(name, sat2_plane_str, sat2_sat_str, sat1_plane_str, sat1_sat_str, satsPerPl[0]))
                                elif math.sqrt(n**2 + m**2) <= (
                                    planeReach * satReach
                                ) / (
                                    math.sqrt(
                                        planeReach**2
                                        * math.sin(math.atan(n / m)) ** 2
                                        + satReach**2
                                        * math.cos(math.atan(n / m)) ** 2
                                    )
                                ): # Satellite connection is diagonal (reaching across planes and satellite positions). The diagonal is only valid if the connection is within the above ellipse equation.
                                    line = (constCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str, satsPerPl[0]))
                                    line += (constCommsLine(name, sat2_plane_str, sat2_sat_str, sat1_plane_str, sat1_sat_str, satsPerPl[0]))
                                    line += (constCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat3_sat_str, satsPerPl[0]))
                                    line += (constCommsLine(name, sat2_plane_str, sat3_sat_str, sat1_plane_str, sat1_sat_str, satsPerPl[0]))
                                elif (shape == 1 and sat1_plane % 2 == 0): # If shape == 1, the satellite connection intra-plane is a diagonal for every other plane
                                    if (skip == 0 or (skip == 1 and ((sat1_plane//2)%2 + sat1_sat%2) == 1)):
                                        line = (constCommsLine(name, sat1_plane_str, sat1_sat_str, sat2_plane_str, sat3_sat_str, satsPerPl[0]))
                                        line += (constCommsLine(name, sat2_plane_str, sat3_sat_str, sat1_plane_str, sat1_sat_str, satsPerPl[0]))
                            file.write(line) # Write line to routing file
            # Writing routing file outgoing rules footer
            file.write("\tEnd Rules\nEnd OutgoingRules")
        else:
            raise ValueError("Please enter a valid value for comms")
    elif objUsed == 1:
        # Satellite Collections
        # Format shell ID's according to naming convention
        shell_ID = []
        for i in range(len(shells)):
            if isinstance(shells[i], int):
                ID = "Shell_" + str(shells[i])
                shell_ID.append(ID)
            else:
                shell_ID.append(shells[i])
        if comms == 0:
            # Satellite Collections w/o Comm Objects
            # Writing routing file header information
            file.write("Begin MultihopRules\n\tDefaultRule Never\n\n\tBegin Rules\n")
            # Begin looping through routing file contents
            for i in range(len(shells)): # Loop through each shell
                for j in range(planes[i]): # Loop through each plane
                    for k in range(satsPerPl[i]): # Loop through each satellite in each plane
                        for m in range(int(planeReach + 1)): # Loop through all possible connections intra-plane
                            for n in range(int(satReach + 1)): # Loop through all possible connections inter-plane
                                # Construct naming for current satellite and its connecting satellites
                                line = ""
                                sat1_plane = j + 1
                                sat1_sat = k + 1
                                sat2_plane = sat1_plane + m
                                sat2_sat = sat1_sat + n
                                sat3_sat = sat1_sat - n
                                sat1_plane_str, sat2_plane_str, sat1_sat_str, sat2_sat_str, sat3_sat_str = constructSyntax(
                                    sat1_plane,
                                    sat1_sat,
                                    sat2_plane,
                                    sat2_sat,
                                    sat3_sat,
                                    planes[i],
                                    satsPerPl[i],
                                )
                                if m == 0: # Satellite connection is inter-plane
                                    if (0 < n <= satReach):
                                        line = (satCollNoCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                else:
                                    if (n == 0 and m <= planeReach): # Satellite connection is intra-plane
                                        # The below if-else statement determines if a connection needs to be skipped or not according to the shape and skip input variables
                                        if (shape == 0):
                                            if (skip == 0 or (skip == 1 and (sat1_plane%2 + sat1_sat%2) == 1)):
                                                line = (satCollNoCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                        elif (shape == 1):
                                            if (sat1_plane % 2 == 0):
                                                continue
                                            elif (skip == 0 or (skip == 1 and ((sat1_plane//2)%2 + sat1_sat%2) == 1)):
                                                line = (satCollNoCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                    elif math.sqrt(n**2 + m**2) <= (
                                        planeReach * satReach
                                    ) / (
                                        math.sqrt(
                                            planeReach**2
                                            * math.sin(math.atan(n / m)) ** 2
                                            + satReach**2
                                            * math.cos(math.atan(n / m)) ** 2
                                        )
                                    ): # Satellite connection is diagonal (reaching across planes and satellite positions). The diagonal is only valid if the connection is within the above ellipse equation.
                                        line = (satCollNoCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                        line += (satCollNoCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat3_sat_str))
                                    elif (shape == 1 and sat1_plane % 2 == 0): # If shape == 1, the satellite connection intra-plane is a diagonal for every other plane
                                        if (skip == 0 or (skip == 1 and ((sat1_plane//2)%2 + sat1_sat%2) == 1)):
                                            line = (satCollNoCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat3_sat_str))
                                file.write(line) # Write line to routing file
            # Writing routing file footer
            file.write("\tEnd Rules\nEnd MultihopRules")
        elif comms == 1:
            # Satellite Collections w/ Comm Objects
            # Writing routing file header information
            file.write("Begin OutgoingRules\n\tDefaultRule Never\n\n\tBegin Rules\n")
            # Since satellite collection subsets are treated as separate objects, incoming/outgoing rules must be used.
            # Therefore, you must write the appropriate incoming/outgoing rule for the connection entering/leaving your satellite collection.
            file.write('\t\tAccess "CollectionSubsetEntry SatelliteCollection/')
            file.write(name)
            file.write(
                "/Subset/AllTransmitters *.Transmitter."
                + str(commInfo[0])
                + '" "'
                + str(commInfo[2])
                + '"\n'
            )
            # Begin looping through routing file contents
            for i in range(len(shells)): # Loop through each shell
                for j in range(planes[i]): # Loop through each plane
                    for k in range(satsPerPl[i]): # Loop through each satellite in each plane
                        for m in range(planeReach + 1): # Loop through all possible connections intra-plane
                            for n in range(satReach + 1): # Loop through all possible connections inter-plane
                                # Construct naming for current satellite and its connecting satellites
                                line = ""
                                sat1_plane = j + 1
                                sat1_sat = k + 1
                                sat2_plane = sat1_plane + m
                                sat2_sat = sat1_sat + n
                                sat3_sat = sat1_sat - n
                                sat1_plane_str, sat2_plane_str, sat1_sat_str, sat2_sat_str, sat3_sat_str = constructSyntax(
                                    sat1_plane,
                                    sat1_sat,
                                    sat2_plane,
                                    sat2_sat,
                                    sat3_sat,
                                    planes[i],
                                    satsPerPl[i],
                                )
                                if m == 0: # Satellite connection is inter-plane
                                    if n == 0:
                                        # Write Rx -> Tx same parent connection
                                        line = (
                                            '\t\tAccess "CollectionSubsetEntry SatelliteCollection/'
                                            + str(name)
                                            + "/Subset/AllReceivers "
                                            + shell_ID[i]
                                            + "_P"
                                            + sat1_plane_str
                                            + "_S"
                                            + sat1_sat_str
                                            + ".Receiver."
                                            + commInfo[1]
                                            + '" "CollectionSubsetEntry SatelliteCollection/'
                                            + str(name)
                                            + "/Subset/AllTransmitters "
                                            + shell_ID[i]
                                            + "_P"
                                            + sat1_plane_str
                                            + "_S"
                                            + sat1_sat_str
                                            + ".Transmitter."
                                            + commInfo[0]
                                            + '"\n'
                                        )
                                    elif (0 < n <= satReach): # Satellite connection is inter-plane
                                        line = (satCollCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                        line += (satCollCommsLine(name, shell_ID[i], sat2_plane_str, sat2_sat_str, sat1_plane_str, sat1_sat_str))
                                else:
                                    if (n == 0 and m <= planeReach): # Satellite connection is intra-plane
                                        # The below if-else statement determines if a connection needs to be skipped or not according to the shape and skip input variables
                                        if (shape == 0):
                                            if (skip == 0 or (skip == 1 and (sat1_plane%2 + sat1_sat%2) == 1)):
                                                line = (satCollCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                                line += (satCollCommsLine(name, shell_ID[i], sat2_plane_str, sat2_sat_str, sat1_plane_str, sat1_sat_str))
                                        elif (shape == 1):
                                            if (sat1_plane % 2 == 0):
                                                continue
                                            elif (skip == 0 or (skip == 1 and ((sat1_plane//2)%2 + sat1_sat%2) == 1)):
                                                line = (satCollCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                                line += (satCollCommsLine(name, shell_ID[i], sat2_plane_str, sat2_sat_str, sat1_plane_str, sat1_sat_str))
                                    elif math.sqrt(n**2 + m**2) <= (
                                        planeReach * satReach
                                    ) / (
                                        math.sqrt(
                                            planeReach**2
                                            * math.sin(math.atan(n / m)) ** 2
                                            + satReach**2
                                            * math.cos(math.atan(n / m)) ** 2
                                        )
                                    ): # Satellite connection is diagonal (reaching across planes and satellite positions). The diagonal is only valid if the connection is within the above ellipse equation.
                                        line = (satCollCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat2_sat_str))
                                        line += (satCollCommsLine(name, shell_ID[i], sat2_plane_str, sat2_sat_str, sat1_plane_str, sat1_sat_str))
                                        line += (satCollCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat3_sat_str))
                                        line += (satCollCommsLine(name, shell_ID[i], sat2_plane_str, sat3_sat_str, sat1_plane_str, sat1_sat_str))
                                    elif (shape == 1 and sat1_plane % 2 == 0): # If shape == 1, the satellite connection intra-plane is a diagonal for every other plane
                                        if (skip == 0 or (skip == 1 and ((sat1_plane//2)%2 + sat1_sat%2) == 1)):
                                            line = (satCollCommsLine(name, shell_ID[i], sat1_plane_str, sat1_sat_str, sat2_plane_str, sat3_sat_str))
                                            line += (satCollCommsLine(name, shell_ID[i], sat2_plane_str, sat3_sat_str, sat1_plane_str, sat1_sat_str))
                                file.write(line) # Write line to routing file
            # Writing routing file footer
            file.write("\tEnd Rules\nEnd OutgoingRules")
        else:
            raise ValueError("Please enter a valid value for comms")


def constructSyntax(
    sat1_plane, sat1_sat, sat2_plane, sat2_sat, sat3_sat, planes, satsPerPl
):
    # This function has two purposes:
    # 1. Loop around any connections between the last plane and the first plane of the constellation (e.g. If the constellation has 8 planes, create a connection between Plane 8 and Plane 1).
    # 2. Adds a "0" in front of the plane/satellite ID number if the total number of planes or satellites per plane exceed 9 (e.g. If the constellation has 20 planes, the 9th plane would be numbered "09").
    # Checks if the current connection needs to loop around via plane and/or satellite position
    if sat2_plane > planes:
        sat2_plane = sat2_plane - planes
    if sat2_sat > satsPerPl:
        sat2_sat = sat2_sat - satsPerPl
    if sat3_sat <= 0:
        sat3_sat = satsPerPl - sat3_sat
    # Check if the plane ID(s) of the current connection needs a "0" added to it
    if planes >= 10:
        if sat1_plane < 10:
            sat1_plane_str = "0" + str(sat1_plane)
        else:
            sat1_plane_str = str(sat1_plane)
        if sat2_plane < 10:
            sat2_plane_str = "0" + str(sat2_plane)
        else:
            sat2_plane_str = str(sat2_plane)
    else:
        sat1_plane_str = str(sat1_plane)
        sat2_plane_str = str(sat2_plane)
    # Check if the satellite position ID(s) of the current connection needs a "0" added to it
    if satsPerPl >= 10:
        if sat1_sat < 10:
            sat1_sat_str = "0" + str(sat1_sat)
        else:
            sat1_sat_str = str(sat1_sat)
        if sat2_sat < 10:
            sat2_sat_str = "0" + str(sat2_sat)
        else:
            sat2_sat_str = str(sat2_sat)
        if sat3_sat < 10:
            sat3_sat_str = "0" + str(sat3_sat)
        else:
            sat3_sat_str = str(sat3_sat)
    else:
        sat1_sat_str = str(sat1_sat)
        sat2_sat_str = str(sat2_sat)
        sat3_sat_str = str(sat3_sat)
    return sat1_plane_str, sat2_plane_str, sat1_sat_str, sat2_sat_str, sat3_sat_str

def constNoCommsLine(name, sat1_plane, sat1_sat, sat2_plane, sat2_sat):
    # This function writes a satellite to satellite connection for a constellation object's routing file
    return(
        "\t\tAccess Satellite/"
        + name
        + sat1_plane
        + sat1_sat
        + " Satellite/"
        + name
        + sat2_plane
        + sat2_sat
        + " reciprocal\n"
    )

def constCommsLine(name, sat1_plane, sat1_sat, sat2_plane, sat2_sat, satsPerPl):
    # This function writes a transmitter to receiver connection for a constellation object's routing file
    return(
        "\t\tAccess Satellite/"
        + name
        + sat1_plane
        + sat1_sat
        + "/Transmitter/"
        + commInfo[0]
        + str(
        (int(sat1_plane) - 1) * satsPerPl
        + int(sat1_sat)
        )
        + " Satellite/"
        + name
        + sat2_plane
        + sat2_sat
        + "/Receiver/"
        + commInfo[1]
        + str(
        (int(sat2_plane) - 1) * satsPerPl
        + int(sat2_sat)
        )
        + "\n"
    )

def satCollNoCommsLine(name, shell_ID, sat1_plane, sat1_sat, sat2_plane, sat2_sat):
    # This function writes a satellite to satellite connection for a satellite collection object's routing file
    return(
        '\t\tAccess "CollectionSubsetEntry SatelliteCollection/'
        + str(name)
        + "/Subset/AllSatellites "
        + shell_ID
        + "_P"
        + sat1_plane
        + "_S"
        + sat1_sat
        + '" "CollectionSubsetEntry SatelliteCollection/'
        + str(name)
        + "/Subset/AllSatellites "
        + shell_ID
        + "_P"
        + sat2_plane
        + "_S"
        + sat2_sat
        + '" reciprocal\n'
    )

def satCollCommsLine(name, shell_ID, sat1_plane, sat1_sat, sat2_plane, sat2_sat):
    # This function writes a transmitter to receiver connection to a satellite collection object's routing file
    return(
        '\t\tAccess "CollectionSubsetEntry SatelliteCollection/'
        + str(name)
        + "/Subset/AllTransmitters "
        + shell_ID
        + "_P"
        + sat1_plane
        + "_S"
        + sat1_sat
        + ".Transmitter."
        + commInfo[0]
        + '" "CollectionSubsetEntry SatelliteCollection/'
        + str(name)
        + "/Subset/AllReceivers "
        + shell_ID
        + "_P"
        + sat2_plane
        + "_S"
        + sat2_sat
        + ".Receiver."
        + commInfo[1]
        + '"\n'
    )

if __name__ == "__main__":
    filePath = r"C:\Users\Username\Documents\STK_ODTK 13\ScenarioFolder\Example.routing"  # Ex: "C:\\Users\\username\\Documents\\adjacent.routing"
    objectUsed = 1  # Constellation: 0 | Satellite Collection: 1
    objName = "SatelliteCollection"  # Constellation: Name of seed satellite | Satellite Collection: Name of Satellite Collection
    shells = [1]  # Ignore if using Constellations. Note that this script does not handle intra-shell connections.
    planes = [12] # Ensure this value is an array, even when using a constellation object.
    satsPerPlane = [24] # Ensure this value is an array, even when using a constellation object.
    comms = 0  # Without communication objects: 0 | With communication objects: 1
    commInfo = [
        "Tx",
        "Rx",
        "Facility/Downlink/Receiver/DownlinkRx",
        "Facility/Uplink/Transmitter/UplinkTx",
    ]  # If using communication objects, enter the name of the reference/seed satellite's transmitter, the name of the reference/seed satellite's receiver, the object path of the receiver receiving the downlink, then the object path of the transmitter transmitting the uplink (Ex: ["Transmitter1", "Receiver1", "Facility/Facility2/Receiver/Receiver2", "Facility/Facility1/Transmitter/Transmitter2"])
    # WARNING: In the commInfo variable, if the transmitter/receiver name ends with a number, it will break the routing file for a Constellation object. If using a Constellation object, please remove any numbers at the end of your Transmitter's/Receiver's name.
    planeReach = 1
    satReach = 1
    shape = 1 # slant shaped mesh: 0 | "W" shaped mesh: 1 (see below for visuals)
    #            If shape = 0:                                  If shape = 1:
    #
    #   X--__  |      |    --X--__  |                  X--__  |  _-  |    --X--    |
    #   |    --X--__  |      |    --X                  |    --X--    |      |    _-X
    #   |      |    --X--__  |      |                  |      |    _-X--__  |  _-  |
    #   X--__  |      |    --X--__  |                  X--__  |  _-  |    --X--    |
    #   |    --X--__  |      |    --X                  |    --X--    |      |    _-X
    #   |      |    --X--__  |      |                  |      |    _-X--__  |  _-  |
    #   X--__  |      |    --X--__  |                  X--__  |  _-  |    --X--    |
    #   |    --X--__  |      |    --X                  |    --X--    |      |    _-X
    #   |      |    --X--__  |      |                  |      |    _-X--__  |  _-  |
    #   X--__  |      |    --X--__  |                  X--__  |  _-  |    --X--    |
    #   |    --X--__  |      |    --X                  |    --X--    |      |    _-X
    #   |      |    --X      |      |                  |      |    _-X--__  |  _-  |

    skip = 1 # Use all intra-plane connections: 1 | Skip every other intra-plane connection (see below for visuals with above meshes)
    #            If shape = 0:                                  If shape = 1:
    #
    #   X--__  |      |      X--__  |                  X--__  |      |    --X      |
    #   |    --X      |      |    --X                  |    --X      |      |    _-X
    #   |      |      X--__  |      |                  |      |    _-X      |  _-  |
    #   X      |      |    --X      |                  X      |  _-  |      X--    |
    #   |      X--__  |      |      X                  |      X--    |      |      X
    #   |      |    --X      |      |                  |      |      X--__  |      |
    #   X--__  |      |      X--__  |                  X--__  |      |    --X      |
    #   |    --X      |      |    --X                  |    --X      |      |    _-X
    #   |      |      X--__  |      |                  |      |    _-X      |  _-  |
    #   X      |      |    --X      |                  X      |  _-  |      X--    |
    #   |      X--__  |      |      X                  |      X--    |      |      X
    #   |      |    --X      |      |                  |      |    _-X--__  |      |


    createRoutingFile(
        filePath,
        objectUsed,
        objName,
        shells,
        planes,
        satsPerPlane,
        comms,
        commInfo,
        planeReach,
        satReach,
        shape,
        skip
    )
