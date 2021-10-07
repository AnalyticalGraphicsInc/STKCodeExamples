# Get reference to running STK instance using the new API
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
stk = STKDesktop.AttachToApplication()

# Get the IAgStkObjectRoot interface
root = stk.Root

# Delete any existing Primitives (also clears ID numbers so new IDs can be defined)
# Can use this Delete All to play around & tailor spacing/altitude on each run as desired
root.ExecuteCommand("VO * Primitive Delete ID All")

############### VARIABLES TO CUSTOMIZE ###############
# Example: GEO Altitude
alt = 35800 # km
longspacing = 10 # degrees
latspacing = 10 # degrees
######################################################

# Convert to meters for Connect syntax
alt *= 1000
############### Lines of latitude ###############

# Create longitude array
longs = []
# Longitude begins at 0 (loops around to end back at 0)
begin = 0
# Append numbers 0 to 360 to the longitude array
while begin <= 360:
    longs.append(begin)
    begin += 1 # increment by 1 for good resolution (otherwise the grid will look like a spiderweb)
# Append a 0 to the longitude array to complete a circle
longs.append(0)

# Starting latitude is -80 (ends at +80)
startLat = -80
# Initialize an ID number to fill into the Connect command
idnum = 1

# Create points with the latitude, longitude, and altitude, then concatenate to a Connect command String with other line settings
# Loop until reached the top line of latitude
while startLat <= 90:  
    point = ""
    for i in range(len(longs)):
        point += " " + str(startLat) + " " + str(longs[i]) + " " + str(alt)
    cmd = "VO * Primitive Add ID " + str(idnum) + " Type Arc Color White LineStyle Dot Points " + str(i+1) + " LLA" + point
    # Execute current command in loop
    root.ExecuteCommand(cmd)
    # Increment latitude by 10 (up to +80)
    startLat += latspacing
    # Increment idnum to make the next circle in a new Primitive
    idnum += 1

############### Lines of longitude ###############

# Create latitude array
lats = []
# Latitude begins at -90 (ends at +90 to arc from pole to pole)
begin = -90
# Append numbers -90 to +90 to the latitude array
while begin <= 90:
    lats.append(begin)
    begin += 1
    
# Starting longitude is -180 (ends at +180)
startLong = -180

# ID number continues from previous loop

# Create points & concatenate to Connect command
# Loop until reached each point around a 360 degree circle
while startLong <= 360:
    point = ""
    for i in range(len(lats)):
        point += "  " + str(lats[i]) + " " + str(startLong) + " " + str(alt)
    cmd = "VO * Primitive Add ID " + str(idnum) + " Type Arc Color White LineStyle Dot Points " + str(i+1) + " LLA" + point
    # Execute current command
    root.ExecuteCommand(cmd)
    # Increment longitude by 10 (up to +180)
    startLong += longspacing
    # Incrememt idnum for new Primitive
    idnum += 1