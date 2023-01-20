# Import the needed packages
import sys
from ConstellationWizardLib import *
from comtypes.client import CreateObject
from comtypes.client import GetActiveObject
from comtypes.gen import STKObjects
import matplotlib.pyplot as plt
import seaborn as sns
import os

print("Initializing")
# cwd = os.getcwd()
input()
objPath = sys.argv[1]
print("Object Path: " + sys.argv[1])
# print(sys.argv[2])
# print(sys.argv[3])
satName = ""
deckAccessFile = (
    "C:\\GitHub\\EngineeringLab\\OperatorsToolBox\\Stk12.OperatorsToolBox\\Plugin Files"
    + "\\deckAccessRpt.txt"
)
deckAccessTLE = (
    "C:\\GitHub\\EngineeringLab\\OperatorsToolBox\\Stk12.OperatorsToolBox\\Plugin Files"
    + "\\deckAccessTLE.tce"
)

stkVersion = "12"

# Launch or connect to STK
try:
    app = GetActiveObject("STK{}.Application".format(stkVersion))
    root = app.Personality2
#     app.Visible = True
#     app.UserControl= True
except:
    print("Error connecting to the scenario")
    input("Press Key to exit")

# Set the scenario time period
sc = root.CurrentScenario
sc2 = sc.QueryInterface(STKObjects.IAgScenario)
# sc2.Animation.AnimStepValue = 30 # Set the animation time to be the same as the MTO data resolution

# Turn on Antialiasing for better visualization. Options are: Off,FXAA,2,3,4
cmd = "SoftVtr3d * AntiAlias 2"
root.ExecuteCommand(cmd)

# Reset the scenario animation
root.Rewind()


# Deck Access for the current time. Save the deck access file to the specified
tleFilepath = "C:\\ProgramData\\AGI\\STK 12\\Databases\\Satellite\\stkAllTLE.tce"
# if len(sys.Argv) >= 3:
# startTime = sys.argv[2]
# stopTime = sys.argv[3]
# else:
startTime = sc2.StartTime
stopTime = sc2.StopTime
print("Analysis Start Time: " + startTime)
print("Analysis Stop Time: " + stopTime)
print("Output File:" + deckAccessFile)
print("Computing Deck Access")
if len(satName) == 0:
    cmd = (
        "DeckAccess */"
        + objPath
        + ' "'
        + startTime
        + '" "'
        + stopTime
        + '" Satellite "'
        + tleFilepath
        + '" SortObj OutFile "'
        + deckAccessFile
        + '"'
    )
else:
    print("Using Satellite Constraint Object")
    cmd = (
        "DeckAccess *"
        + objPath
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
# Read the deck access report and write the TLEs to a file
NumOfSC = writeTLEs(tleFilepath, deckAccessFile, deckAccessTLE)
print("Number of Visible Satellites: " + str(NumOfSC))
# Add all visibile satellites as an MTO
# Read the TLE file
tleList = getTLEs(deckAccessTLE)
dfTLE = tleListToDF(tleList)
MTOName = LoadMTO(
    root,
    deckAccessTLE,
    timestep=30,
    color="cyan",
    orbitsOnOrOff="off",
    orbitFrame="Inertial",
)
generatePlots = True
if generatePlots == True:

    # ### Plots of TLE Data Distribution

    # Import useful plotting libraries and change the default plotting style

    sns.set_style("white")
    sns.set_context("talk")

    # Plot the cumulative percentage of satellites vs inclination
    inc = dfTLE["i"][dfTLE["i"].sort_values().index].values
    plt.plot(inc, np.arange(1, len(dfTLE) + 1, 1) / len(dfTLE) * 100)
    plt.xlabel("Inc [deg]")
    plt.ylabel("Cumulative % of satellites")

    # Plot the density of inclination in a histogram
    plt.hist(dfTLE["i"], bins=np.arange(0, 110, 5))
    plt.xlabel("Inc [deg]")
    plt.ylabel("Count")
    plt.xlim(0, 105)
    print("Close Plot to Continue")
    plt.show()

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
    xlims = plt.xlim(0, 105)
    ylims = plt.ylim(6578, 50000)
    ax.ax_joint.set_xlim(xlims)
    ax.ax_joint.set_ylim(ylims)
    print("Close Plot to Continue")
    plt.show()
    print("Done")
