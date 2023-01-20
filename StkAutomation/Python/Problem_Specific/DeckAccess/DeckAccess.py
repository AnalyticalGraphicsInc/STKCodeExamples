#!/usr/bin/env python
# coding: utf-8

# # Deck Access Visualization and Analysis

# ### Modify the file paths below to be your desired save location. Then click Cell->Run All

# Double '\\\\' are required in the filepath because Python uses the '\\' character as a break character.

# In[1]:


# The file path to save the Deck Access report and Deck Access TLEs
deckAccessFile = "C:\\deckAccessRpt.txt"
deckAccessTLE = "C:\\deckAccessTLE.tce"


# ### Connect and Configure STK

# In[2]:


# Import the needed packages
from comtypes.client import CreateObject, GetActiveObject
from comtypes.gen import STKObjects

# Sometimes autocomplete takes a while. This line fixes that.
get_ipython().run_line_magic("config", "Completer.use_jedi = False")


# In[3]:


# Get the current time and add 1 second
from datetime import datetime, timedelta

now = datetime.now()
nowSTK = now.strftime("%d %b %Y %H:%M:%S")
nowplus = now + timedelta(seconds=1)
nowSTKplus = nowplus.strftime("%d %b %Y %H:%M:%S")
startTime = nowSTK
stopTime = nowSTKplus
print("Now = " + nowSTK)

# Create times to set the scenario time period which includes now
strs = nowSTK.split(" ")
start = strs[0] + " " + strs[1] + " " + strs[2] + " 00:00:00.000"
stop = strs[0] + " " + strs[1] + " " + strs[2] + " 23:59:59.999"
print("Scenario Start = " + start)
print("Scenario Stop = " + stop)


# In[4]:


# Launch or connect to STK
try:
    app = GetActiveObject("STK12.Application")
    root = app.Personality2
    app.Visible = True
    app.UserControl = True
except:
    app = CreateObject("STK12.Application")
    app.Visible = True
    app.UserControl = True
    root = app.Personality2
    sc = root.NewScenario("DeckAccessVis")


# Set the scenario time period
sc = root.CurrentScenario
sc2 = sc.QueryInterface(STKObjects.IAgScenario)
sc2.StopTime = stop
sc2.StartTime = start
sc2.Animation.AnimStepValue = (
    30  # Set the animation time to be the same as the MTO data resolution
)

# Turn on Antialiasing for better visualization. Options are: Off,FXAA,2,3,4
cmd = "SoftVtr3d * AntiAlias 2"
root.ExecuteCommand(cmd)

# Set Animation time to the time used for the deck Access Report
root.CurrentTime = float(root.ConversionUtility.ConvertDate("UTCG", "EpSec", nowSTK))


# In[5]:


# Remove any deck access satellites from previous runs
from DeckAccessReader import FilterObjectsByType

objType = "Satellite"
name = "deck"
objPaths = FilterObjectsByType(objType, name)

for i in range(len(objPaths)):
    obj = root.GetObjectFromPath(objPaths[i])
    obj.Unload()

name = "tle"
objPaths = FilterObjectsByType(objType, name)
for i in range(len(objPaths)):
    obj = root.GetObjectFromPath(objPaths[i])
    obj.Unload()


# Try moving the ground station to a different location. Then rerun the script by clicking Kernel -> Restart & Run All

# In[6]:


# Add a facility
facName = "Observer"

if sc.Children.Contains(STKObjects.eFacility, facName):
    fac = root.GetObjectFromPath("Facility/" + facName)
    fac.Unload()
fac = sc.Children.New(STKObjects.eFacility, facName)

# Cast fac into an IAgFacility called fac2
fac2 = fac.QueryInterface(STKObjects.IAgFacility)
# Assign a new geodetic position
# San Diego
lat = 32.7157
lon = -117.1611
# AGI_HQ
# lat = 40.0326
# lon = -75.6275
# Kiruna (part of the European tracking network Estrack)
# lat = 67.8558
# lon = 20.2253
alt = 0
fac2.Position.AssignGeodetic(lat, lon, alt)


# In[7]:


# Add an elevation angle constraint
elAng = 20

if fac2.AccessConstraints.IsConstraintActive(STKObjects.eCstrElevationAngle):
    elCon = fac2.AccessConstraints.GetActiveConstraint(STKObjects.eCstrElevationAngle)
else:
    elCon = fac2.AccessConstraints.AddConstraint(STKObjects.eCstrElevationAngle)

elCon2 = elCon.QueryInterface(STKObjects.IAgAccessCnstrMinMax)
elCon2.EnableMin = True
elCon2.Min = elAng


# In[8]:


# Add a constraint satellite template
satName = "ConstraintSat"

if sc.Children.Contains(STKObjects.eSatellite, satName):
    sat = root.GetObjectFromPath("Satellite/" + satName)
    sat.Unload()
sat = sc.Children.New(STKObjects.eSatellite, satName)

# Cast fac into an IAgFacility called fac2
sat2 = sat.QueryInterface(STKObjects.IAgSatellite)

# Add an elevation angle constraint
light = (
    STKObjects.eDirectSun
)  # eDirectSun,ePenumbra,ePenumbraOrDirectSun,ePenumbraOrUmbra,eUmbra,eUmbraOrDirectSun

if sat2.AccessConstraints.IsConstraintActive(STKObjects.eCstrLighting):
    lightCon = sat2.AccessConstraints.GetActiveConstraint(STKObjects.eCstrLighting)
else:
    lightCon = sat2.AccessConstraints.AddConstraint(STKObjects.eCstrLighting)

lightCon2 = lightCon.QueryInterface(STKObjects.IAgAccessCnstrCondition)
lightCon2.Condition = light


# In[9]:


# # Additional constraints options

# # Modify the deck access time to consider a custom time range and not just now
# startTime= start # 18 Oct 2019 00:00:00
# stopTime = stop # 18 Oct 2019 01:00:00

# # Add Umbra constraint for the facility
# light = STKObjects.eUmbra # eDirectSun,ePenumbra,ePenumbraOrDirectSun,ePenumbraOrUmbra,eUmbra,eUmbraOrDirectSun
# if fac2.AccessConstraints.IsConstraintActive(STKObjects.eCstrLighting):
#     lightCon = fac2.AccessConstraints.GetActiveConstraint(STKObjects.eCstrLighting)
# else:
#     lightCon = fac2.AccessConstraints.AddConstraint(STKObjects.eCstrLighting)
# lightCon2 = lightCon.QueryInterface(STKObjects.IAgAccessCnstrCondition)
# lightCon2.Condition = light


# ### Run Deck Access, Create a TLE file for all visible satellites, Import them into an MTO, Add in specific satellites

# In[10]:


# Deck Access for the current time. Save the deck access file to the specified
tleFilepath = "C:\\ProgramData\\AGI\\STK 11 (x64)\\Databases\\Satellite\\stkAllTLE.tce"
cmd = (
    "DeckAccess */Facility/"
    + facName
    + ' "'
    + startTime
    + '" "'
    + stopTime
    + '" Satellite "'
    + tleFilepath
    + '" SortObj OutFile "'
    + deckAccessFile
    + '" ConstraintObject */Satellite/'
    + satName
)
cmdOut = root.ExecuteCommand(cmd)
print(cmdOut.Item(0))


# In[11]:


# Read the deck access report and write the TLEs to a file
from DeckAccessReader import writeTLEs

NumOfSC = writeTLEs(tleFilepath, deckAccessFile, deckAccessTLE)
print("Number of Visible Satellites: " + str(NumOfSC))


# In[12]:


# Add all visibile satellites as an MTO
try:
    cmd = "Unload / */MTO/deckAccessMTO"
    root.ExecuteCommand(cmd)
except:
    print("Inserting MTO deckAccessMTO")

print("Updating MTO deckAccessMTO")
cmd = "New / */MTO deckAccessMTO"
root.ExecuteCommand(cmd)
cmd = "VO */MTO/deckAccessMTO MTOAttributes ShowAlllabels off"
root.ExecuteCommand(cmd)
cmd = "VO */MTO/deckAccessMTO MTOAttributes ShowAllLines off"
root.ExecuteCommand(cmd)
cmd = (
    'Track */MTO/deckAccessMTO TleFile Filename "' + deckAccessTLE + '" TimeStep 30'
)  # Decrease the TimeStep for better resolution at the cost of computation time
root.ExecuteCommand(cmd)


# In[13]:


# Add in a specific satellite
scID = 43226  # Select a desired satellite
cmd = (
    'ImportTLEFile * "'
    + deckAccessTLE
    + '" SSCNumber '
    + str(scID)
    + ' AutoPropagate On Merge On StartStop "'
    + sc2.StartTime
    + '" "'
    + sc2.StopTime
    + '"'
)
try:
    cmdOut = root.ExecuteCommand(cmd)
except:
    print("Satellite " + str(scID) + " is not visible")


# In[14]:


# Add a fixed number of satellites from the deck access and pass back the deck access data for the entire day
NumSatsToAdd = 5
if NumSatsToAdd == 0:
    cmd = (
        "DeckAccess */Facility/"
        + facName
        + ' "'
        + sc2.StartTime
        + '" "'
        + sc2.StopTime
        + '" Satellite "'
        + deckAccessTLE
        + '" SortObj OutReport ConstraintObject */Satellite/'
        + satName
    )  # Doesn't add satellites but runs the report
else:
    cmd = (
        "DeckAccess */Facility/"
        + facName
        + ' "'
        + sc2.StartTime
        + '" "'
        + sc2.StopTime
        + '" Satellite "'
        + deckAccessTLE
        + '" SortObj OutReport AddSatellites '
        + str(NumSatsToAdd)
        + " ConstraintObject */Satellite/"
        + satName
    )
print(cmd)
cmdOutDA = root.ExecuteCommand(cmd)


# ### Looking at the Deck Access Data

# In[15]:


# Store the deck access data into a Pandas DataFrame for further analysis
import numpy as np
import pandas as pd

names = []
starts = []
stops = []
durs = []
for i in range(1, cmdOutDA.Count - 1):
    stri = cmdOutDA.Item(i)
    strs = stri.split(",")
    names.append(strs[0])
    starts.append(strs[1])
    stops.append(strs[2])
    durs.append(strs[3])

df = pd.DataFrame([names, starts, stops, durs])
df = df.T
df.columns = ["SSC Num", "Start", "Stop", "Dur (sec)"]  # cmdOutDA.Item(0).split(',')
df[df.columns[0]] = df[df.columns[0]].str.replace('"', "")
df.head(10)


# In[16]:


# Find satellites with access during the entire scenario time period
df[df["Dur (sec)"].astype(float) >= 86399].head(10)


# In[17]:


# Sort dataframe by duration of access
dfSortedByDur = df.iloc[df["Dur (sec)"].astype(float).sort_values().index]
dfSortedByDur.head(10)


# In[18]:


# Sort by SSC Num and secondarily by Duration
df.set_index(["SSC Num", "Dur (sec)"]).sort_values(["SSC Num", "Dur (sec)"]).head(10)


# ### Look at the TLE Data

# In[19]:


# Get TLE data into a dataframe for more analysis
# Find more details on the TLE format here: https://en.wikipedia.org/wiki/Two-line_element_set
from DeckAccessReader import getTLEs

tleList = getTLEs(deckAccessTLE, deckAccessFile)

for i in range(len(tleList)):
    tleList[i] = tleList[i].replace("  ", " ").replace("  ", " ")
dfTLEList = pd.DataFrame(tleList)

# new data frame with split value columns
tleSplit = dfTLEList[dfTLEList.columns[0]].str.split(" ", expand=True)
line1 = tleSplit[0::2]
line2 = tleSplit[1::2]

line1 = line1.reset_index(drop=True)
line2 = line2.reset_index(drop=True)
line1.loc[:, line1.columns[-1]] = line1[line1.columns[-1]].str.replace("\n", "").values
line2.loc[:, line2.columns[-1]] = line2[line2.columns[-1]].str.replace("\n", "").values
line2.loc[:, line2.columns[-2]] = line2[line2.columns[-2]].str.replace("\n", "").values
line1.columns = [
    "Line1",
    "Ssc",
    "Launch",
    "Epoch",
    "Mean motion 1st",
    "Mean motion 2nd",
    "Drag",
    "Eph Type",
    "Elem Set",
]
line2.columns = ["Line2", "Ssc2", "i", "RAAN", "e", "AoP", "MA", "Mean motion", "temp"]
# Need to handle the space in some of the second lines. Replacing this with a 0
tempVal = line2["temp"][line2["temp"].values != None]
mmVal = line2["Mean motion"][line2["temp"].values != None]
mmValnew = mmVal + "0" + tempVal
line2["Mean motion"][line2["temp"].values != None] = mmValnew
line2 = line2.drop("temp", axis=1)

# Create new data frame with both lines in the same row
dfTLE = pd.concat([line1, line2], axis=1)

# Convert mean motion to approximate semimajor axis and add this as a column to the dataframe
dfTLE["i"] = dfTLE["i"].astype(float)
dfTLE["Mean motion"] = dfTLE["Mean motion"].astype(float)
mu = 3.986004e14
n = (
    dfTLE["Mean motion"] / (86400) * 2 * np.pi
)  # Technically the mean motion is only the first 8 digits past the decimal but removing the extra digits won't affect much
a = (mu / (n**2)) ** (1 / 3) / 1000
dfTLE["a"] = a

dfTLE.head()


# In[20]:


# Start to filter objects by orbital elements
dfTLE[dfTLE["i"].astype(float) < 1].head()  # sort by inclination


# ### Plots of TLE Data Distribution

# In[21]:


# Import useful plotting libraries and change the default plotting style
import matplotlib.pyplot as plt

get_ipython().run_line_magic("matplotlib", "inline")
import seaborn as sns

sns.set_style("white")
sns.set_context("talk")


# In[22]:


# Plot the cumulative percentage of satellites vs inclination
inc = dfTLE["i"][dfTLE["i"].sort_values().index].values
plt.plot(inc, np.arange(1, len(dfTLE) + 1, 1) / len(dfTLE) * 100)
plt.xlabel("Inc [deg]")
plt.ylabel("Cumulative % of satellites")


# In[23]:


# Plot the density of inclination in a histogram
plt.hist(dfTLE["i"], bins=np.arange(0, 110, 5))
plt.xlabel("Inc [deg]")
plt.ylabel("Count")
plt.xlim(0, 105)


# In[24]:


# Look at inclination vs semimajor axis utilizing pandas built in plots
dfTLE.plot.scatter("i", "a", alpha=0.2, s=50, figsize=(5, 5))
xlims = plt.xlim(0, 105)
ylims = plt.ylim(6578, 50000)


# In[25]:


# Utilize Seaborn's built in jointplot to learn more about the distribution of inclination vs semimajor axis
ax = sns.jointplot(
    dfTLE["i"],
    dfTLE["a"],
    kind="kde",
    cut=0,
    space=0,
    n_levels=100,
    height=10,
    kernel="epa",
    bw="silverman",
    marginal_kws={"kernel": "epa", "bw": "silverman"},
)
ax.plot_joint(plt.scatter, c="k", s=50, linewidth=0.8, marker="+", alpha=0.2)
# ax.set_axis_labels('i [deg]','a [km]')
ax.ax_joint.set_xlim(xlims)
ax.ax_joint.set_ylim(ylims)


# ### Remember to also look at STK! You can see all of the satellites in the scenario and can animate to watch the satellites move.
