import itertools
import os
import pickle
import time
from collections import Counter

import matplotlib.pyplot as plt
import networkx as nx
import numpy as np
import pandas as pd
from agi.stk12.stkobjects import (
    AgChain,
    AgChUserSpecifiedTimePeriod,
    AgConstellation,
    AgDataProviderGroup,
    AgDataPrvInterval,
    AgDataPrvTimeVar,
    AgEChTimePeriodType,
    AgESTKObjectType,
)
from agi.stk12.utilities.colors import Colors
from mpl_toolkits.mplot3d import Axes3D

# Potential Future Work: Switch to max flow problem in nx. After initial runtime investigation there does not appear to be much benefit, at most a factor of 2 better, but that estimate was missing a bunch of other processing steps
# Allow for possibility of stoping, saving and reusing data

# Upon further thought, my implementation is similar to Edmonds-Karp Algorithm. Edmonds-Karp does a BFS and then finds the max flow through that path,
# augments the paths by the capacity remaining and then does another BFS. I do the same thing but with djikstra's algorithm.

# Done: All sources and sinks could be linked to a super node, and then all of the nx algorithms would work out of the box, but there are less opportunities for customization.


def addNodeConstraints(
    stkRoot, nodeConstraints, timesEdgesDistancesDelaysBandwidths, start, stop, step
):
    def buildNodesWithConstraints(stkRoot, nodeConstraints):
        nodesWithConstraints = {}
        for constellationName, val in nodeConstraints.items():
            nodes = getNodesFromConstellation(stkRoot, constellationName)
            for node in nodes:
                nodesWithConstraints[node] = val
        return nodesWithConstraints

    # Get Node constraint for each node
    nodesWithConstraints = buildNodesWithConstraints(stkRoot, nodeConstraints)

    # Build times
    times = np.arange(start, stop, step)
    times = np.append(times, stop)

    if nodesWithConstraints:
        # Loop through each time and add in constraints
        for t in times:
            # Find where the edge ends in a node, force it to go to the constrained node instead and build an edge to map the node constraint onto the edge
            edgesToModify = [
                edge
                for edge in timesEdgesDistancesDelaysBandwidths[t].keys()
                if edge[1] in nodesWithConstraints
            ]

            for node1, node2 in edgesToModify:
                # Replace end node with constrained node and delete the original edge
                timesEdgesDistancesDelaysBandwidths[t][
                    (node1, node2 + "Constraint")
                ] = timesEdgesDistancesDelaysBandwidths[t][(node1, node2)]
                timesEdgesDistancesDelaysBandwidths[t].pop((node1, node2), None)
                # Add edge from nodeWithConstraint to the original node with the constrained value
                timesEdgesDistancesDelaysBandwidths[t][
                    (node2 + "Constraint", node2)
                ] = (
                    0,
                    0,
                    nodesWithConstraints[node2] * step,
                )  # rate*step size

            # Also need to check if the starting Nodes need to have a constraint added
            startingEdgesToModify = [
                edge
                for edge in timesEdgesDistancesDelaysBandwidths[t].keys()
                if edge[0] in nodesWithConstraints
            ]

            for node1, node2 in startingEdgesToModify:
                # Add edge from nodeWithConstraint to the original node with the constrained value
                timesEdgesDistancesDelaysBandwidths[t][
                    (node1, node1 + "StartConstraint")
                ] = (
                    0,
                    0,
                    nodesWithConstraints[node1] * step,
                )  # rate*step size
                # Replace end node with constrained node and delete the original edge
                timesEdgesDistancesDelaysBandwidths[t][
                    (node1 + "StartConstraint", node2)
                ] = timesEdgesDistancesDelaysBandwidths[t][(node1, node2)]
                timesEdgesDistancesDelaysBandwidths[t].pop((node1, node2), None)

    return timesEdgesDistancesDelaysBandwidths


# Added copyObjectForConstraints, since sometimes you need object constraints and filter a network by nodes
# In theory we don't need to do this. A fictitious version could be created
def copyObjectsForConstraints(stkRoot, objTypes):
    # objTypes = {'Facility','Place','Target'}
    scenario = stkRoot.CurrentScenario
    for ii in range(scenario.Children.Count - 1, 0, -1):
        object = scenario.Children.Item(ii)
        if object.ClassName in objTypes:
            object.CopyObject(f"{object.InstanceName}ForConstraints")
    return


def networkIncludingNode(nodesToInclude, df):
    includedNodes = []
    for id, row in df["strand"].iteritems():
        include = False
        for node in nodesToInclude:
            if node in row:
                include = True
                break
        includedNodes.append(include)
    dfOfInterest = df.loc[includedNodes, :]
    return dfOfInterest


def computeDataTransferThroughNetwork(
    start,
    stop,
    step,
    timesEdgesDistancesDelaysBandwidths,
    timesDataToTransfer,
    timesDataTransferred,
    startingNodes,
    endingNodes,
    priorityCase=1,
    nodeTransferPriority=[],
    nodeDestinationPriority=[],
    metric="distance",
    maxDataTransfer=1e15,
    breakOnceAllDataIsTransferred=True,
    overrideData=False,
    printTime=False,
    filename="",
    minPacketSize=0,
):

    # Use first nodes for the naming convention
    if not filename:
        filename1 = "SavedNetworkData/DataTransfer.pkl"
        filename2 = "SavedNetworkData/DataSources.pkl"
        filename3 = "SavedNetworkData/DataDestinations.pkl"
    else:
        filename1 = f"SavedNetworkData/{filename}DataTransfer.pkl"
        filename2 = f"SavedNetworkData/{filename}DataSources.pkl"
        filename3 = f"SavedNetworkData/{filename}DataDestinations.pkl"
    needToCompute = True
    if (
        os.path.exists(filename1)
        and os.path.exists(filename2)
        and os.path.exists(filename3)
        and not overrideData
    ):
        with open(filename1, "rb") as f:
            df = pickle.load(f)
        with open(filename2, "rb") as f:
            dfTimesDataToTransfer = pickle.load(f)
        with open(filename3, "rb") as f:
            dfTimesDataTransferred = pickle.load(f)
        needToCompute = False
        simulationStopTime = dfTimesDataToTransfer["time"].values[-1]
        return df, dfTimesDataToTransfer, dfTimesDataTransferred, simulationStopTime

    # Skip if data loaded
    if needToCompute:

        # Define initial variables
        timeStrandMetric = []

        # Determine time of last data added
        if breakOnceAllDataIsTransferred:
            timeWhenNoMoreDataIsAdded = findTimeOfLastDataAdded(timesDataToTransfer)
        else:
            timeWhenNoMoreDataIsAdded = stop

        # Loop through each time
        times = np.arange(start, stop, step)
        times = np.append(times, stop)

        # Build network, compute shortest path, fill up bandwidth, transfer data, update data storage, repeat
        for ii, t in enumerate(times):

            t1 = time.time()
            totalBandwidthTransferred = 0
            dataTransferred = 0

            #     # Add layer with highest bandwidth
            #     uniqueBandwidths = np.unique([v[2] for k,v in timesEdgesDistancesDelaysBandwidths[t].items()])
            #     ii = len(uniqueBandwidths)-1
            tDataToTransfer = timesDataToTransfer[t].copy()
            tDataTransferred = timesDataTransferred[t].copy()

            # Recompute priorities, recompute layers of network
            if priorityCase in [2, 3, 4]:
                layers = computePriorityNetworkLayers(
                    priorityCase, tDataToTransfer, tDataTransferred
                )
                numLayers = len(layers[:, 0])
            elif priorityCase == 5:
                layers = computePriorityNetworkLayers(
                    priorityCase, nodeTransferPriority, nodeDestinationPriority
                )
                numLayers = len(layers[:, 0])
            else:
                numLayers = 1
                startingNodesInLayer = startingNodes
                endingNodesInLayer = endingNodes

            # Loop through priority of layers
            for layer in range(numLayers):

                # layer of network
                if priorityCase in [2, 3, 4, 5]:
                    startingNodesInLayer = layers[layer, 0]
                    endingNodesInLayer = layers[layer, 1]

                pathsAvailable = True

                while (
                    totalBandwidthTransferred < maxDataTransfer and pathsAvailable
                ):  # see if there are more layers to add

                    G = nx.DiGraph()

                    noDataNodes = [
                        k for k, v in tDataToTransfer.items() if abs(v) < 1e-6
                    ]

                    # Add edges and nodes with highest bandwidth until data is transferred
                    edgesToRemove = []
                    for (
                        edge,
                        distanceDelayBandwidth,
                    ) in timesEdgesDistancesDelaysBandwidths[t].items():
                        if edge[0] in noDataNodes:
                            edgesToRemove.append(edge)
                        elif (
                            (
                                edge[0] not in startingNodes
                                and edge[1] not in endingNodes
                            )
                            or (edge[0] in startingNodesInLayer)
                            or (edge[1] in endingNodesInLayer)
                        ):
                            distance = distanceDelayBandwidth[0]
                            timeDelay = distanceDelayBandwidth[1]
                            bandwidth = 1 / (distanceDelayBandwidth[2])
                            G.add_edge(
                                *edge,
                                distance=distance,
                                timeDelay=timeDelay,
                                bandwidth=bandwidth,
                            )

                    # Remove edges with empty nodes
                    for edge in edgesToRemove:
                        timesEdgesDistancesDelaysBandwidths[t].pop(edge)

                    for node in startingNodes:
                        if not G.has_edge("Source", node):
                            G.add_edge(
                                *("Source", node),
                                distance=0,
                                timeDelay=0,
                                bandwidth=1 / maxDataTransfer,
                            )
                    for node in endingNodes:
                        if not G.has_edge(node, "Sink"):
                            G.add_edge(
                                *(node, "Sink"),
                                distance=0,
                                timeDelay=0,
                                bandwidth=1 / maxDataTransfer,
                            )

                    # LOOP here to keep doing while strands are available
                    # Find shortest strand metric
                    if any([node in G.nodes() for node in startingNodes]) and any(
                        [node in G.nodes() for node in endingNodes]
                    ):
                        strandShort, metricVal = shortestStrandDistanceOptimized(
                            G, metric=metric
                        )
                        dataTransferredFlag = False
                        # Count total data transferred
                        if not np.isnan(float(metricVal)):
                            strandShort = strandShort[
                                1:-1
                            ]  # Remove Source and Sink pseudo nodes
                            startNode = strandShort[0]
                            endNode = strandShort[-1]
                            edges = [
                                (strandShort[jj], strandShort[jj + 1])
                                for jj in range(len(strandShort) - 1)
                            ]
                            bandwidths = [
                                1 / (G.edges[edge]["bandwidth"]) for edge in edges
                            ]
                            bandwidths.append(
                                tDataToTransfer[startNode]
                            )  # Don't allow negative data at the source
                            bandwidths.append(
                                maxDataTransfer - totalBandwidthTransferred
                            )  # Don't go past maximum network transfer
                            dataTransferred = min(bandwidths)

                            # Handle minimum packet size, pop off edges with too little remaining bandwidth and repeat
                            transferData = True
                            if (
                                dataTransferred - minPacketSize
                            ) < -1e-6:  # Allow for some numerical issues, effectively less than 0
                                for edge in edges:
                                    if (
                                        timesEdgesDistancesDelaysBandwidths[t][edge][2]
                                        - minPacketSize
                                    ) < -1e-6:  # Allow for some numerical issues, effectively less than 0
                                        timesEdgesDistancesDelaysBandwidths[t].pop(edge)
                                        transferData = False

                            # If data transferred > minPacketSize. OR if the remaining data to transfer < min Packet size, and it isn't an edge constraint issue
                            if (
                                dataTransferred - minPacketSize
                            ) >= -1e-6 or transferData:
                                dataTransferredFlag = True
                                # Transfer the data
                                totalBandwidthTransferred += dataTransferred
                                # Modify Bandwidth and remove filled bandwidth, remove any used edge
                                for edge in edges:
                                    (
                                        distance,
                                        timeDelay,
                                        availableBandwidth,
                                    ) = timesEdgesDistancesDelaysBandwidths[t][edge]
                                    timesEdgesDistancesDelaysBandwidths[t].update(
                                        {
                                            edge: (
                                                distance,
                                                timeDelay,
                                                availableBandwidth - dataTransferred,
                                            )
                                        }
                                    )
                                    if (
                                        timesEdgesDistancesDelaysBandwidths[t][edge][2]
                                    ) < 1e-6:  # Allow for some numerical issues, effectively 0
                                        timesEdgesDistancesDelaysBandwidths[t].pop(edge)

                                # Update data transfer
                                tDataToTransfer.update(
                                    {
                                        startNode: tDataToTransfer[startNode]
                                        - dataTransferred
                                    }
                                )
                                tDataTransferred.update(
                                    {
                                        endNode: tDataTransferred[endNode]
                                        + dataTransferred
                                    }
                                )
                        else:
                            pathsAvailable = False

                        if dataTransferredFlag:
                            if (
                                totalBandwidthTransferred - dataTransferred
                            ) > 0:  # check if data has already been transferred
                                if not np.isnan(
                                    float(metricVal)
                                ):  # don't append nan values if data has already been transferred/paths have been found
                                    timeStrandMetric.append(
                                        (t, strandShort, metricVal, dataTransferred)
                                    )
                            else:
                                timeStrandMetric.append(
                                    (t, strandShort, metricVal, dataTransferred)
                                )
                    else:
                        pathsAvailable = False

            # update with index
            if t != times[-1]:
                for node in timesDataToTransfer[t].keys():
                    timesDataToTransfer[times[ii + 1]].update(
                        {
                            node: tDataToTransfer[node]
                            + timesDataToTransfer[times[ii + 1]][node]
                        }
                    )

                for node in timesDataTransferred[t].keys():
                    timesDataTransferred[times[ii + 1]].update(
                        {
                            node: tDataTransferred[node]
                            + timesDataTransferred[times[ii + 1]][node]
                        }
                    )

            if printTime:
                print("t=", t, "computation time=", time.time() - t1)

            # Break from processing if all data has been removed and time is greater than last data collected
            if (
                all(np.asarray(list(timesDataToTransfer[t].values())) == 0)
                and t >= timeWhenNoMoreDataIsAdded
            ):
                print("Stopped Processing at time", t)
                break

        simulationStopTime = t
        df = pd.DataFrame(
            timeStrandMetric, columns=["time", "strand", metric, "data transferred"]
        )
        df[metric] = df[metric].astype(float)
        if metric == "bandwidth":
            df = df.drop([metric], axis=1)
        df["strand"] = df["strand"].apply(
            lambda x: [node for node in x if "Constraint" not in node]
        )  # Remove constrained nodes
        df["strand"] = df["strand"].apply(
            lambda x: [node for node in x if "StartConstraint" not in node]
        )  # Remove constrained nodes
        df["num hops"] = df["strand"].apply(lambda x: len(x) - 2)
        df["num parent hops"] = df["strand"].apply(
            lambda x: len(set([str.split(obj, "/")[1] for obj in x])) - 2
        )  # numb parent hops
        df.loc[df["num hops"] < 0, "num hops"] = np.nan
        df.loc[df["num parent hops"] < 0, "num parent hops"] = np.nan

        dfTimesDataToTransfer = convertTimesDataToDataFrame(
            timesDataToTransfer, simulationStopTime
        )
        dfTimesDataTransferred = convertTimesDataToDataFrame(
            timesDataTransferred, simulationStopTime
        )

        # Save to file
        with open(filename1, "wb") as f:
            pickle.dump(df, f)
        with open(filename2, "wb") as f:
            pickle.dump(dfTimesDataToTransfer, f)
        with open(filename3, "wb") as f:
            pickle.dump(dfTimesDataTransferred, f)
        return (
            df,
            dfTimesDataToTransfer,
            dfTimesDataTransferred,
            timesEdgesDistancesDelaysBandwidths,
            simulationStopTime,
        )


# Updated methods of pushing data to STK and color bars
def createTimesEdgesCountFromDF(df, weightColumn=None):
    # Read df of discrete values to numpy array
    npArr = df.to_numpy()
    if weightColumn:
        mask = df.columns == "data transferred"
        column = np.arange(0, len(df.columns))[mask][0]
        edgeWeights = np.round(npArr[:, column].astype(float), 6)

    # Loop through time, break down edges at each time
    timesUnique = np.array(list(set(npArr[:, 0])))
    times = timesUnique[np.argsort(timesUnique)]
    timeEdgeCountAll = np.empty((0, 3))
    for t in times:
        rows = npArr[:, 0] == t
        npArrT = npArr[rows, :]
        strandsAtT = npArrT[:, 1]
        if weightColumn:
            weightsAtT = edgeWeights[rows]
        else:
            weightsAtT = np.ones((len(npArrT),))
        edgesAtT = np.asarray(
            [
                (t, (strand[ii], strand[ii + 1]), weightsAtT[jj])
                for jj, strand in enumerate(strandsAtT)
                for ii in range(len(strand) - 1)
            ]
        )
        # weights = edgesAtT[:, 2]
        uniqueEdges = np.unique(edgesAtT[:, 1])
        timeEdgeCount = np.asarray(
            [
                (
                    t,
                    edge,
                    np.round(
                        sum(
                            edgesAtT[
                                [
                                    ii
                                    for ii, val in enumerate(edgesAtT[:, 1])
                                    if val == edge
                                ],
                                2,
                            ]
                        ),
                        6,
                    ),
                )
                for edge in uniqueEdges
            ]
        )
        if len(timeEdgeCount) > 0:
            timeEdgeCountAll = np.append(timeEdgeCountAll, timeEdgeCount, axis=0)
    return timeEdgeCountAll


def createColorDict(cmap, data):
    colors = np.array(cmap(data / max(data))[:, 0:3] * 255, dtype=int)
    colorsDict = {
        data[ii]: "%{:03d}{:03d}{:03d}".format(
            colors[ii, 0], colors[ii, 1], colors[ii, 2]
        )
        for ii in range(colors.shape[0])
    }
    return colorsDict


def addTimesEdgesCountAsObjectLines(
    stkRoot,
    timeEdgeCountAll,
    step,
    color="yellow",
    lineWidth=4,
    deleteOldLines=True,
    colorMap=None,
    addTo2D=False,
):
    if deleteOldLines:
        stkRoot.ExecuteCommand("VO * ObjectLine DeleteAll")

    # Get unique Edges
    uniqueEdges = list(set(timeEdgeCountAll[:, 1]))

    # Color by count
    if colorMap:
        uniqueCountsAll = np.asarray(list(set(timeEdgeCountAll[:, 2].astype(float))))
        colorDict = createColorDict(colorMap, uniqueCountsAll)

        for uniqueEdge in uniqueEdges:
            # Get data for unique Edges
            mask = [
                True if edge == uniqueEdge else False for edge in timeEdgeCountAll[:, 1]
            ]
            timeEdgeUniqueCount = timeEdgeCountAll[mask, :]

            # Color each edge by count number at a time, unfortunately you cannot do this with line width
            uniqueCounts = list(set(timeEdgeUniqueCount[:, 2]))
            add = True
            for uniqueCount in uniqueCounts:
                mask2 = [
                    True if count == uniqueCount else False
                    for count in timeEdgeUniqueCount[:, 2]
                ]
                timeEdgeUniqueCountUnique = timeEdgeUniqueCount[mask2, :]

                # Create merged intervals
                timeUnique = timeEdgeUniqueCountUnique[:, 0]
                intervalsNum = np.stack(
                    (timeUnique - step / 2, timeUnique + step / 2), axis=1
                )
                mergedIntervals = np.array(mergeIntervals(intervalsNum))

                # Build and execute Connect Commaands to add Object Lines to STK
                strIntervals = mergedIntervals.astype(str)
                startStop = ['" "'.join(interval) for interval in strIntervals]
                node1 = uniqueEdge[0]
                node2 = uniqueEdge[1]
                numIntervals = len(mergedIntervals)
                intervals = '"' + '" "'.join(startStop) + '"'
                if add:
                    cmd = f"VO * ObjectLine Add FromObj {node1} ToObj {node2} Color {colorDict[uniqueCount]} LineWidth {str(lineWidth)} AddIntervals {str(numIntervals)} {intervals}"
                    stkRoot.ExecuteCommand(cmd)
                    cmd = f"VO * ObjectLine Modify FromObj {node1} ToObj {node2} IntervalType UseIntervals"
                    stkRoot.ExecuteCommand(cmd)
                    if addTo2D:
                        cmd = f"Graphics * ObjectLine Add FromObj {node1} ToObj {node2} Color {colorDict[uniqueCount]} LineWidth {str(lineWidth)} AddIntervals {str(numIntervals)} {intervals}"
                        stkRoot.ExecuteCommand(cmd)
                        cmd = f"Graphics * ObjectLine Modify FromObj {node1} ToObj {node2} IntervalType UseIntervals"
                        stkRoot.ExecuteCommand(cmd)
                    add = False
                else:  # If an edge already exists add additionalintervals, each interval color has to be modified seperately
                    cmd = f"VO * ObjectLine Modify FromObj {node1} ToObj {node2} LineWidth {str(lineWidth)} AddIntervals {str(numIntervals)} {intervals}"
                    stkRoot.ExecuteCommand(cmd)
                    if addTo2D:
                        cmd = f"Graphics * ObjectLine Modify FromObj {node1} ToObj {node2} LineWidth {str(lineWidth)} AddIntervals {str(numIntervals)} {intervals}"
                        stkRoot.ExecuteCommand(cmd)
                    for startStopInterval in startStop:
                        cmd = f'VO * ObjectLine Modify FromObj {node1} ToObj {node2} ModifyInterval "{startStopInterval}" Color {colorDict[uniqueCount]}'
                        stkRoot.ExecuteCommand(cmd)
                        if addTo2D:
                            cmd = f'Graphics * ObjectLine Modify FromObj {node1} ToObj {node2} ModifyInterval "{startStopInterval}" Color {colorDict[uniqueCount]}'
                            stkRoot.ExecuteCommand(cmd)
    else:
        for uniqueEdge in uniqueEdges:
            # Get data for unique Edges
            mask = [
                True if edge == uniqueEdge else False for edge in timeEdgeCountAll[:, 1]
            ]
            timeEdgeUniqueCount = timeEdgeCountAll[mask, :]

            # Create merged intervals
            timeUnique = timeEdgeUniqueCount[:, 0]
            intervalsNum = np.stack(
                (timeUnique - step / 2, timeUnique + step / 2), axis=1
            )
            mergedIntervals = np.array(mergeIntervals(intervalsNum))

            # Build and execute Connect Commands to add all intervals as Object Lines to STK
            strIntervals = mergedIntervals.astype(str)
            startStop = ['" "'.join(interval) for interval in strIntervals]
            node1 = uniqueEdge[0]
            node2 = uniqueEdge[1]
            numIntervals = len(mergedIntervals)
            intervals = '"' + '" "'.join(startStop) + '"'
            cmd = f"VO * ObjectLine Add FromObj {node1} ToObj {node2} Color {color} LineWidth {str(lineWidth)} AddIntervals {str(numIntervals)} {intervals}"
            stkRoot.ExecuteCommand(cmd)
            cmd = f"VO * ObjectLine Modify FromObj {node1} ToObj {node2} IntervalType UseIntervals"
            stkRoot.ExecuteCommand(cmd)
            if addTo2D:
                cmd = f"Graphics * ObjectLine Add FromObj {node1} ToObj {node2} Color {color} LineWidth {str(lineWidth)} AddIntervals {str(numIntervals)} {intervals}"
                stkRoot.ExecuteCommand(cmd)
                cmd = f"Graphics * ObjectLine Modify FromObj {node1} ToObj {node2} IntervalType UseIntervals"
                stkRoot.ExecuteCommand(cmd)
    return


def plotColorbar(timesEdgeCountAll, cmap, plotBoth=True, tickRotationInDeg=65):
    from mpl_toolkits.axes_grid1 import make_axes_locatable

    # display stacked bar chart
    barHeight = 1
    bottom = 0
    xtickCenters = []
    uniqueCountsAll = np.sort(
        np.asarray(list(set(timesEdgeCountAll[:, 2].astype(float))))
    )
    diffs = list(np.diff(uniqueCountsAll))
    if diffs:
        diffs.append(diffs[0])
        maxVal = max(uniqueCountsAll)
        fig, ax = plt.subplots(figsize=(8, 2))
        for val, width in zip(uniqueCountsAll, diffs):
            ax.barh(1, width, height=barHeight, left=bottom, color=cmap(val / maxVal))
            bottom += width
            xtickCenters.append(bottom - width / 2)
        ax.set_yticks([1])
        ax.set_yticklabels([])
        ax.set_xticks(xtickCenters)
        ax.set_xticklabels(uniqueCountsAll, rotation=tickRotationInDeg)
        ax.set_ylabel("data/sec")
        ax.set_ylim([1 - barHeight / 2, 1 + barHeight / 2])
        ax.set_xlim([xtickCenters[0] - diffs[0] / 2, xtickCenters[-1] + diffs[0] / 2])
        plt.show()

        # Scatter and color bar
        if plotBoth:
            fig, ax = plt.subplots(figsize=(8, 4))
            plt.scatter(
                uniqueCountsAll,
                np.ones((len(uniqueCountsAll),)),
                c=uniqueCountsAll,
                cmap=cmap,
                s=200,
            )
            divider = make_axes_locatable(ax)
            cax = divider.append_axes("bottom", size="100%", pad=0.7)
            cbar = plt.colorbar(
                ticks=uniqueCountsAll, orientation="horizontal", cax=cax
            )

            ax.set_yticks([1])
            ax.set_yticklabels([])
            ax.set_xticks(uniqueCountsAll)
            ax.set_xticklabels(uniqueCountsAll, rotation=tickRotationInDeg)
            ax.set_ylabel("data/sec")
            ax.set_ylim([1 - barHeight / 2, 1 + barHeight / 2])
            ax.set_xlim([xtickCenters[0], xtickCenters[-1] + diffs[0]])
            cbar.ax.set_xticklabels(uniqueCountsAll, rotation=tickRotationInDeg)
            plt.show()
    else:
        print("Only 1 value, no colorbar will be generated:", uniqueCountsAll[0])


# Added network data transfer and various supportinf functions


def dictToArray(d):
    return np.asarray(list(d.items()))


def dictToKeysValues(d):
    return np.asarray(list(d.keys())), np.asarray(list(d.values()))


def computePriorityNetworkLayers(
    priorityCase=[], nodeTransferPriority=[], nodeDestinationPriority=[]
):
    # Pick from cases
    layers = []
    cases = [2, 3, 4, 5]

    if priorityCase in cases:
        reverseSource = False
        reverseDestination = False
        setTransferPrioritiesEqual = False
        setDestinationPrioritiesEqual = False

        # priorityCase = 2 # Priority is highest for the sources with the most data, destinations are treated equally, recomputed at each timestep,
        if priorityCase == 2:
            reverseSource = True  # Node with most data is the highest priority
            setDestinationPrioritiesEqual = True

        # priorityCase = 3 # Priority is highest for the destinaton with the least data, sources are treated equally, recomputed at each timestep
        elif priorityCase == 3:
            setTransferPrioritiesEqual = True

        # priorityCase = 4 # Priority is highest for the sources with the most data and priority is highest for the destinaton with the least data, recomputed at each timestep at each timestep
        elif priorityCase == 4:
            reverseSource = True  # Node with most data is the highest priority

        # priorityCase = 5 # A specified priority will be used, nodeTransferPriority and nodeDestinationPriority
        if priorityCase == 5:
            pass

        # Convert to array for easier indexing and sorting
        sourceNodes, sourcePriority = dictToKeysValues(nodeTransferPriority)
        destinationNodes, destinationPriority = dictToKeysValues(
            nodeDestinationPriority
        )

        # Set equal
        if setTransferPrioritiesEqual:
            sourcePriority[:] = 100

        if setDestinationPrioritiesEqual:
            destinationPriority[:] = 100

        # Get unique priorities
        sourcePriorities = np.unique(sourcePriority)
        destinationPriorities = np.unique(destinationPriority)
        slen = len(sourcePriorities)
        dlen = len(destinationPriorities)
        # Reverse
        if reverseSource:
            sourcePriorities = sourcePriorities[::-1]
        if reverseDestination:
            destinationPriorities = destinationPriorities[::-1]

        # Get nodes in each layer
        for ii in range(max(slen, dlen)):
            if ii < slen:
                if reverseSource:
                    sourceNodesToInclude = sourceNodes[
                        sourcePriority >= sourcePriorities[ii]
                    ]
                else:
                    sourceNodesToInclude = sourceNodes[
                        sourcePriority <= sourcePriorities[ii]
                    ]
            else:
                sourceNodesToInclude = sourceNodes
            if ii < dlen:
                if reverseDestination:
                    destinationNodesToInclude = destinationNodes[
                        destinationPriority >= destinationPriorities[ii]
                    ]
                else:
                    destinationNodesToInclude = destinationNodes[
                        destinationPriority <= destinationPriorities[ii]
                    ]
            else:
                destinationNodesToInclude = destinationNodes
            layers.append([sourceNodesToInclude, destinationNodesToInclude])

    else:
        # priorityCase = 1 # No priority of sources or destinations, also the default case
        pass

    return np.asarray(layers)


# Adds in total data transfer
def addDataMetrics(dfData, step, addColumnForEachEndLocation=True):
    dfData2 = dfData.copy()
    data = dfData2.to_numpy()
    dataTotal = np.sum(data[:, 1:], axis=1)
    transferedAtT = list(np.diff(dataTotal))
    transferedAtT2 = [transferedAtT[0]] + transferedAtT
    dfData2["Total Data"] = dataTotal
    dfData2["Data per time step"] = transferedAtT2
    dfData2["Data per sec"] = np.asarray(transferedAtT2) / step
    if addColumnForEachEndLocation:
        for ii in range(1, len(dfData.columns)):
            col = dfData.columns[ii]
            dataCol = data[:, ii]
            transferedAtT = list(np.diff(dataCol))
            transferedAtT2 = [transferedAtT[0]] + transferedAtT
            dfData2[col + " Data per sec"] = np.asarray(transferedAtT2) / step

    return dfData2


# Adding Multi path and load functions

# Build the dictionary of data storage. Use the buildTimesAdditionalNodeData. buildTimesNodeData looks at the data if none is transferred at each timestep
def buildTimesNodeData(
    stkRoot, bulkData, dataRate, start, stop, step, overrideData=False
):
    # sort data
    bulkData = dict(sorted(bulkData.items()))
    dataRate = dict(sorted(dataRate.items()))

    times = np.arange(start, stop, step)
    times = np.append(times, stop)

    timesNodeBulkData = {}
    timesNodeDataRate = {}
    for t, dataDict in bulkData.items():
        nodeData = {}
        for conName, data in dataDict.items():
            nodes = getNodesFromConstellation(
                stkRoot, conName, overrideData=overrideData
            )
            for node in nodes:
                nodeData.update({node: data})
        timesNodeBulkData.update({t: nodeData})

    for t, dataRateDict in dataRate.items():
        nodeData = {}
        for conName, data in dataRateDict.items():
            nodes = getNodesFromConstellation(
                stkRoot, conName, overrideData=overrideData
            )
            for node in nodes:
                nodeData.update({node: data})
        timesNodeDataRate.update({t: nodeData})

    timesNodeData = {}
    nodeData = {}
    nodeDataRate = {}
    bulkTimes = list(timesNodeBulkData.keys())
    dataRateTimes = list(timesNodeDataRate.keys())
    b = 0
    d = 0
    for t in times:
        # Only update the nodeData when new info is available
        if b < len(bulkTimes):
            bulkTime = bulkTimes[b]
        else:
            bulkTime = times[-1] + 1  # ensure there are no more updates

        if d < len(dataRateTimes):
            dataRateTime = dataRateTimes[d]
        else:
            dataRateTime = times[-1] + 1

        if t >= bulkTime:
            nodeBulkData = timesNodeBulkData[bulkTime]
            for node in nodeBulkData.keys():
                if node in nodeData.keys():
                    nodeData.update({node: nodeData[node] + nodeBulkData[node]})
                else:
                    nodeData.update({node: nodeBulkData[node]})
            b += 1

        if t >= dataRateTime:
            # Add old data rate
            for node in nodeDataRate.keys():
                if node in nodeData.keys():
                    nodeData.update(
                        {
                            node: nodeData[node]
                            + nodeDataRate[node] * (step - (t - dataRateTime))
                        }
                    )
                else:
                    nodeData.update(
                        {node: nodeDataRate[node] * (step - (t - dataRateTime))}
                    )
            # Add new data rate
            nodeDataRate = timesNodeDataRate[dataRateTime]
            for node in nodeDataRate.keys():
                if node in nodeData.keys():
                    nodeData.update(
                        {node: nodeData[node] + nodeDataRate[node] * (t - dataRateTime)}
                    )
                else:
                    nodeData.update({node: nodeDataRate[node] * (t - dataRateTime)})
            d += 1
        else:
            for node in nodeDataRate.keys():
                if node in nodeData.keys():
                    nodeData.update({node: nodeData[node] + nodeDataRate[node] * step})
                else:
                    nodeData.update({node: nodeDataRate[node] * (step)})

        timesNodeData.update({t: nodeData.copy()})

    return timesNodeData


# Build the dictionary of data storage. Use this one. Which is the amount of data added at each time step
def buildTimesNodeAdditionalData(
    stkRoot, bulkData, dataRate, start, stop, step, overrideData=False
):
    # sort data
    bulkData = dict(sorted(bulkData.items()))
    dataRate = dict(sorted(dataRate.items()))

    times = np.arange(start, stop, step)
    times = np.append(times, stop)

    timesNodeBulkData = {}
    timesNodeDataRate = {}
    for t, dataDict in bulkData.items():
        nodeData = {}
        for conName, data in dataDict.items():
            nodes = getNodesFromConstellation(
                stkRoot, conName, overrideData=overrideData
            )
            for node in nodes:
                nodeData.update({node: data})
        timesNodeBulkData.update({t: nodeData})

    for t, dataRateDict in dataRate.items():
        nodeData = {}
        for conName, data in dataRateDict.items():
            nodes = getNodesFromConstellation(
                stkRoot, conName, overrideData=overrideData
            )
            for node in nodes:
                nodeData.update({node: data})
        timesNodeDataRate.update({t: nodeData})

    #
    timesNodeData = {}
    nodeData = {}
    nodeDataRate = {}
    bulkTimes = list(timesNodeBulkData.keys())
    dataRateTimes = list(timesNodeDataRate.keys())
    nodeNames = timesNodeBulkData[
        bulkTimes[0]
    ].keys()  # Name of nodes with data, currently assuming the first time instance of data updates has all of the nodes we will ever care about
    b = 0
    d = 0
    decimalPlace = 6

    for ii, t in enumerate(times):
        nodeData = {node: 0 for node in nodeNames}
        # Only update the nodeData when new info is available
        if b < len(bulkTimes):
            bulkTime = bulkTimes[b]
        else:
            bulkTime = times[-1] + 1  # ensure there are no more updates

        if d < len(dataRateTimes):
            dataRateTime = dataRateTimes[d]
        else:
            dataRateTime = times[-1] + 1

        # Add all data between timesteps
        while t >= bulkTime:
            nodeBulkData = timesNodeBulkData[bulkTime]
            for node in nodeData.keys():
                nodeData.update({node: nodeData[node] + nodeBulkData[node]})
            b += 1
            if b < len(bulkTimes):
                bulkTime = bulkTimes[b]
            else:
                bulkTime = times[-1] + 1  # ensure there are no more updates

        # Add data rate for each timestep
        if t >= dataRateTime:
            # Add old data rate
            for node in nodeDataRate.keys():
                if node in nodeData.keys():
                    nodeData.update(
                        {
                            node: np.round(
                                nodeData[node]
                                + nodeDataRate[node] * (step - (t - dataRateTime)),
                                decimalPlace,
                            )
                        }
                    )
                else:
                    nodeData.update(
                        {
                            node: np.round(
                                nodeDataRate[node] * (step - (t - dataRateTime)),
                                decimalPlace,
                            )
                        }
                    )
            # Add new data rate
            nodeDataRate = timesNodeDataRate[dataRateTime]
            for node in nodeDataRate.keys():
                if node in nodeData.keys():
                    nodeData.update(
                        {
                            node: np.round(
                                nodeData[node]
                                + nodeDataRate[node] * (t - dataRateTime),
                                decimalPlace,
                            )
                        }
                    )
                else:
                    nodeData.update(
                        {
                            node: np.round(
                                nodeDataRate[node] * (t - dataRateTime), decimalPlace
                            )
                        }
                    )
            d += 1
        else:
            for node in nodeDataRate.keys():
                if node in nodeData.keys():
                    nodeData.update(
                        {
                            node: np.round(
                                nodeData[node] + nodeDataRate[node] * step, decimalPlace
                            )
                        }
                    )
                else:
                    nodeData.update(
                        {node: np.round(nodeDataRate[node] * (step), decimalPlace)}
                    )

        timesNodeData.update({t: nodeData.copy()})

    return timesNodeData


def convertTimesDataToDataFrame(timesDataTransfer, simulationStopTime):
    ts = np.asarray(list(timesDataTransfer.keys()))
    ts = ts[ts <= simulationStopTime]
    numberOfTimes = len(ts)
    numberOfNodes = len(timesDataTransfer[ts[0]])
    nodeNames = timesDataTransfer[ts[0]].keys()
    data = np.zeros((numberOfTimes, numberOfNodes + 1))
    data[:, 0] = ts
    columns = ["time"]
    for ii, item in enumerate(timesDataTransfer.items()):
        if ii >= len(ts):
            break
        nodeData = item[1]
        for jj, node in enumerate(nodeData):
            data[ii, jj + 1] = nodeData[node]
    for node in nodeNames:
        columns.append(node)
    dfDataTransfer = pd.DataFrame(data, columns=columns)
    return dfDataTransfer


# Used to stop the data simulation once no more data is being added and all data has been transferred
def findTimeOfLastDataAdded(timesDataToTransfer):
    timesValues = np.asarray(
        [
            (t, value)
            for t, data in timesDataToTransfer.items()
            for value in data.values()
        ][::-1]
    )
    reverseTimes = np.unique(timesValues[:, 0])[::-1]
    for ii, t in enumerate(reverseTimes):
        values = timesValues[timesValues[:, 0] == t, 1]
        if not all(values == 0):
            if ii == 0:
                return reverseTimes[ii]
            else:
                return reverseTimes[ii - 1]

    return reverseTimes[0]


def generateDiNetworkBandwidth(t, timeEdgesDistancesDelaysBandwidth, timeNodePos):
    G = nx.DiGraph()
    for edge, distanceDelayBandwidth in timeEdgesDistancesDelaysBandwidth[t].items():
        distance = distanceDelayBandwidth[0]
        timeDelay = distanceDelayBandwidth[1]
        bandwidth = distanceDelayBandwidth[2]
        G.add_edge(*edge, distance=distance, timeDelay=timeDelay, bandwidth=bandwidth)
    for node, pos in timeNodePos[t].items():
        G.add_node(node, Type=node.split("/")[0], Position=pos)
    return G


def recomputeMissingData(
    stkRoot,
    strands,
    start,
    stop,
    step,
    chainNames,
    nodesTimesPos,
    strandsAtTimes,
    recomputeIfDataIsMissing=True,
):
    computedNodeTimes = np.unique(
        [
            item
            for nodeDict in list(nodesTimesPos.values())
            for item in list(nodeDict.keys())
        ]
    )
    missingDataTimes = [
        t for t in np.array(list(strandsAtTimes.keys())) if t not in computedNodeTimes
    ]

    if len(missingDataTimes) > 0:
        print("Data for nodes is missing at the following times")
        print(missingDataTimes)
        print(
            "Adjust the start, stop or step to match previously saved data time or the recompute data"
        )
        if recomputeIfDataIsMissing:
            print("Recalculating Strands")
            strands = getAllStrands(
                stkRoot, chainNames, start, stop, overrideData=True
            )[0]
            print("Recalculating Node Positions")
            nodesTimesPos = computeNodesPosOverTime(
                stkRoot, strands, start, stop, step, overrideData=True
            )  # Pull node position over time
            print("Rebuilding strandsAtTimes")
            strandsAtTimes = getStrandsAtTimes(
                strands, start, stop, step
            )  # Discretize strand intervals into times
    return strands, nodesTimesPos, strandsAtTimes


# Updates the timeEdgesDistancesDelays to include bandwidths
def addEdgeMetricToTimesEdges(
    stkRoot, timesEdgesDistancesDelays, constellationPairsDict, defaultValue=0
):
    # Get possible edges from the constellation pairs dict
    edgesBandwidth = []
    for constellationPair, bandwidth in constellationPairsDict.items():
        nodesCon1 = getNodesFromConstellation(stkRoot, constellationPair[0])
        nodesCon2 = getNodesFromConstellation(stkRoot, constellationPair[1])
        edgesBandwidth.append(
            [
                (start, end, bandwidth)
                for start, end in itertools.product(nodesCon1, nodesCon2)
            ]
        )
    edgesBandwidth = {
        (item[0], item[1]): item[2] for sublist in edgesBandwidth for item in sublist
    }

    # Add the new metric to timesEdgesDistancesDelays
    timesEdgesDistancesDelaysBandwidths = {}

    for t, edgesDistanceDelays in timesEdgesDistancesDelays.items():
        edgesDistanceDelayBandwidths = {}
        edges = edgesDistanceDelays.keys()
        for edge in edges:
            distance = edgesDistanceDelays[edge][0]
            delay = edgesDistanceDelays[edge][1]
            if edge in edgesBandwidth.keys():
                edgesDistanceDelayBandwidths.update(
                    {edge: (distance, delay, edgesBandwidth[edge])}
                )
            else:
                edgesDistanceDelayBandwidths.update(
                    {edge: (distance, delay, defaultValue)}
                )
        timesEdgesDistancesDelaysBandwidths.update(
            {t: edgesDistanceDelayBandwidths.copy()}
        )
    return timesEdgesDistancesDelaysBandwidths


def createColorRamp(rgb1, rgb2, data):
    rgb1 = np.array(rgb1)
    rgb2 = np.array(rgb2)
    data = np.array(data)

    minData = min(data)
    maxData = max(data)
    scale = maxData - minData
    dRGB = rgb2 - rgb1

    colors = np.array(
        [(item - minData) / (scale) * dRGB + rgb1 for item in data]
    ).astype(int)
    colorsDict = {
        data[ii]: "%{:03d}{:03d}{:03d}".format(
            colors[ii, 0], colors[ii, 1], colors[ii, 2]
        )
        for ii in range(colors.shape[0])
    }
    return colorsDict


def topNShortestPaths(G, t, startingNode, endingNode, metric, topN=3):
    allPaths = nx.shortest_simple_paths(G, startingNode, endingNode, weight=metric)
    ii = 1
    timeStrandMetricTopN = []
    try:
        for path in allPaths:
            metricVal = sum(
                (G.edges[path[jj], path[jj + 1]][metric] for jj in range(len(path) - 1))
            )
            timeStrandMetricTopN.append((t, path, metricVal))
            if ii >= topN:
                break
            ii += 1
    except Exception:
        timeStrandMetricTopN.append((t, "", np.nan))
    return timeStrandMetricTopN


def computeNetworkTopN(
    start,
    stop,
    step,
    timeNodePos,
    timesEdgesDistancesDelays,
    startingNode,
    endingNode,
    metric,
    topN=3,
    overrideData=False,
    printTime=False,
    filename="",
    removeUsedNodes=False,
    removeUsedEdges=False,
):
    # Build new network at each time and gather metrics
    t1 = time.time()

    # Use first nodes for the naming convention
    filename = "SavedNetworkData/dfTop{}{}{}{}.pkl".format(
        topN, filename, startingNode.split("/")[-1], endingNode.split("/")[-1]
    )

    needToCompute = True
    if os.path.exists(filename) and not overrideData:
        with open(filename, "rb") as f:
            df = pickle.load(f)
        computedTimes = np.array(df["time"])
        neededTimes = np.array(list(timesEdgesDistancesDelays.keys()))
        missingDataTimes = [t for t in neededTimes if t not in computedTimes]
        if len(missingDataTimes) == 0:
            needToCompute = False
            if len(computedTimes) != len(neededTimes):
                index = 0
                indices = []
                for t in computedTimes:
                    if t in neededTimes:
                        indices.append(index)
                    index += 1
                df = df.iloc[indices, :]

    if needToCompute:
        # Define initial variables
        timeStrandMetric = []
        # Loop through each time
        times = np.arange(start, stop, step)
        times = np.append(times, stop)
        for t in times:
            # Generate Network at each time
            if metric.lower() == "bandwidth":
                G = generateDiNetworkBandwidth(
                    t, timesEdgesDistancesDelays, timeNodePos
                )  # Build a directed network if two constellations are used
            else:
                G = generateDiNetwork(
                    t, timesEdgesDistancesDelays, timeNodePos
                )  # Build a directed network if two constellations are used

            # find unique paths, remove previously used nodes and edges if desired
            if removeUsedNodes:  # note removing nodes also removes all associated edges
                for ii in range(topN):
                    timePathDelay = topNShortestPaths(
                        G, t, startingNode, endingNode, metric, topN=1
                    )
                    timeStrandMetric.append(timePathDelay)
                    nodesToRemove = timePathDelay[0][1][1:-1]
                    Gnew = G.copy()
                    Gnew.remove_nodes_from(nodesToRemove)
                    G = Gnew.copy()
            elif removeUsedEdges:
                for ii in range(topN):
                    timePathDelay = topNShortestPaths(
                        G, t, startingNode, endingNode, metric, topN=1
                    )
                    timeStrandMetric.append(timePathDelay)
                    nodesToRemove = timePathDelay[0][1]
                    edgesToRemove = [
                        (x, y) for x, y in zip(nodesToRemove, nodesToRemove[1:])
                    ]
                    Gnew = G.copy()
                    Gnew.remove_edges_from(edgesToRemove)
                    G = Gnew.copy()
            else:
                timeStrandMetric.append(
                    topNShortestPaths(G, t, startingNode, endingNode, metric, topN=topN)
                )

        timeStrandMetric = [item for sublist in timeStrandMetric for item in sublist]

        # Build df
        df = pd.DataFrame(timeStrandMetric, columns=["time", "strand", metric])
        df["num hops"] = df["strand"].apply(lambda x: len(x) - 2)
        df["num parent hops"] = df["strand"].apply(
            lambda x: len(set([str.split(obj, "/")[1] for obj in x])) - 2
        )
        df.loc[df["num hops"] < 0, "num hops"] = np.nan
        df.loc[df["num parent hops"] < 0, "num parent hops"] = np.nan
        df[metric] = df[metric].astype(float)

        # Save df
        with open(filename, "wb") as f:
            pickle.dump(df, f)

    if printTime:
        print(time.time() - t1)
    return df


def loadNetworkDfTopN(nodePairs, topN, neededTimes=[], filenames=""):
    ii = 0
    for nodePair in nodePairs:
        # Figureout each filename
        startingNode = nodePair[0]
        endingNode = nodePair[1]
        if not filenames:
            filename = "SavedNetworkData/dfTop{}{}{}.pkl".format(
                topN, startingNode.split("/")[-1], endingNode.split("/")[-1]
            )
        else:
            filename = filenames[ii]

        # Read first file and get columns
        if ii == 0:
            with open(filename, "rb") as f:
                columns = pickle.load(f).columns
            df = np.empty((0, len(columns)))

        # Read each file
        with open(filename, "rb") as f:
            dfTemp = pickle.load(f).to_numpy()

        # Filter down to only neededTimes
        if len(neededTimes) > 0:
            computedTimes = dfTemp[:, 0]
            missingDataTimes = [t for t in neededTimes if t not in computedTimes]
            if len(missingDataTimes) == 0:
                if len(computedTimes) != len(neededTimes):
                    index = 0
                    indices = []
                    for t in computedTimes:
                        if t in neededTimes:
                            indices.append(index)
                        index += 1
                    dfTemp = dfTemp[indices, :]
            else:
                print(
                    "Missing some needed times in {} Please recompute the network".format(
                        filename
                    )
                )
        # Append dataframes
        df = np.append(df, dfTemp, axis=0)
        ii += 1

    # Assign columns and add delays
    df = pd.DataFrame(df, columns=columns)
    return df


def loadNetworkDf(nodePairs, neededTimes=[], filenames=""):
    ii = 0
    for nodePair in nodePairs:
        # Figureout each filename
        startingNode = nodePair[0]
        endingNode = nodePair[1]
        if not filenames:
            filename = "SavedNetworkData/df{}{}.pkl".format(
                startingNode.split("/")[-1], endingNode.split("/")[-1]
            )
        else:
            filename = filenames[ii]

        # Read first file and get columns
        if ii == 0:
            with open(filename, "rb") as f:
                columns = pickle.load(f).columns
            df = np.empty((0, len(columns)))

        # Read each file
        with open(filename, "rb") as f:
            dfTemp = pickle.load(f).to_numpy()

        # Filter down to only neededTimes
        if len(neededTimes) > 0:
            computedTimes = dfTemp[:, 0]
            missingDataTimes = [t for t in neededTimes if t not in computedTimes]
            if len(missingDataTimes) == 0:
                if len(computedTimes) != len(neededTimes):
                    index = 0
                    indices = []
                    for t in computedTimes:
                        if t in neededTimes:
                            indices.append(index)
                        index += 1
                    dfTemp = dfTemp[indices, :]
            else:
                print(
                    "Missing some needed times in {} Please recompute the network".format(
                        filename
                    )
                )

        # Append dataframes
        df = np.append(df, dfTemp, axis=0)
        ii += 1

    # Assign columns and add delays
    df = pd.DataFrame(df, columns=columns)
    return df


def addLightAndNodeDelays(df, timesEdgesDistancesDelays):
    if "distance" in df.columns:
        df["timeDelay"] = [
            computePathDistanceDelay(
                tuple(df.iloc[i, 1]), timesEdgesDistancesDelays[df.iloc[i, 0]]
            )[1]
            for i in range(len(df))
        ]
        df.loc[df["timeDelay"] == 0, "timeDelay"] = np.nan
        df["lightDelay"] = df["distance"] / 299792.458
        df["nodeDelay"] = df["timeDelay"] - df["lightDelay"]
    elif "timeDelay" in df.columns:
        df["distance"] = [
            computePathDistanceDelay(
                tuple(df.iloc[i, 1]), timesEdgesDistancesDelays[df.iloc[i, 0]]
            )[0]
            for i in range(len(df))
        ]
        df.loc[df["distance"] == 0, "distance"] = np.nan
        df["lightDelay"] = df["distance"] / 299792.458
        df["nodeDelay"] = df["timeDelay"] - df["lightDelay"]

    return df


def createDirectedChains(
    stkRoot,
    constellationOrderLists,
    start=[],
    stop=[],
    color=Colors.FromRGB(0, 196, 196),
):
    chainNames = []
    if type(constellationOrderLists[0]) == list:
        for constellationOrder in constellationOrderLists:
            chainNamesTemp = buildChains(
                stkRoot, constellationOrder, start=start, stop=stop, color=color
            )
            for chainName in chainNamesTemp:
                chainNames.append(chainName)
    else:
        chainNames = buildChains(
            stkRoot, constellationOrderLists, start=start, stop=stop, color=color
        )
    return chainNames


def buildChains(
    stkRoot, constellationOrder, start=[], stop=[], color=Colors.FromRGB(0, 196, 196)
):
    chainNames = []
    for ii in range(len(constellationOrder) - 1):
        con1 = stkRoot.GetObjectFromPath(f"*/Constellation/{constellationOrder[ii]}")
        con2 = stkRoot.GetObjectFromPath(f"*/Constellation/{constellationOrder[ii+1]}")
        chainName = f"{constellationOrder[ii]}To{constellationOrder[ii+1]}"
        chainNames.append(chainName)
        if stkRoot.CurrentScenario.Children.Contains(
            AgESTKObjectType.eChain, chainName
        ):
            chain = stkRoot.GetObjectFromPath(f"*/Chain/{chainName}")
        else:
            chain = AgChain(
                stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eChain, chainName)
            )
            chain.Graphics.Animation.LineWidth = 0
            chain.Graphics.Animation.Color = color
        chain.AutoRecompute = False

        if (
            start < stop
        ):  # If start and stop are not passed it will just use the default time period
            chain.SetTimePeriodType(AgEChTimePeriodType.eUserSpecifiedTimePeriod)
            timePeriod = AgChUserSpecifiedTimePeriod(chain.TimePeriod)
            timePeriod.TimeInterval.SetExplicitInterval(start, stop)
        chain.Objects.RemoveAll()
        chain.Objects.AddObject(con1)
        chain.Objects.AddObject(con2)

    return chainNames


# Modify graphics
def turnGraphicsOnOff(stkRoot, objPaths, onOrOff="On", parentsOnly=False):
    if not (onOrOff in ["On", "Off"]):
        onOrOff = "On"

    for objPath in objPaths:
        parentPath = "/".join(objPath.split("/")[0:2])
        stkRoot.ExecuteCommand(
            f"Graphics */{parentPath} Show {onOrOff}"
        )  # Turn parent object on first

        if not parentsOnly:
            try:
                stkRoot.ExecuteCommand(f"Graphics */{objPath} Show {onOrOff}")
            except Exception:
                pass


def possibleNodeConnections(t, nodeInterest, timesEdgesDistancesDelays):
    tempDict = {
        k: v for k, v in timesEdgesDistancesDelays[t].items() if nodeInterest in k
    }
    dfAtTime = pd.DataFrame([tempDict.keys(), tempDict.values()]).T
    dfAtTime[["node1", "node2"]] = pd.DataFrame(
        dfAtTime.loc[:, 0].tolist(), index=dfAtTime.index
    )
    dfAtTime[["distance", "timeDelay"]] = pd.DataFrame(
        dfAtTime.loc[:, 1].tolist(), index=dfAtTime.index
    )
    dfAtTime = dfAtTime.drop([0, 1], axis=1)
    return dfAtTime


# node delays by node
def getNodeDelaysByNode(
    stkRoot, nodeDelays, constellationNames="", chainNames="", overrideData=False
):
    nodeDelaysByNode = {}

    for conName, delay in nodeDelays.items():
        nodes = getNodesFromConstellation(stkRoot, conName, overrideData=overrideData)
        for node in nodes:
            nodeDelaysByNode.update({node: delay})

    if constellationNames:
        nodesInConstellations = [
            getNodesFromConstellation(stkRoot, conName, overrideData=overrideData)
            for conName in constellationNames
        ]
        setOfNodesInConstellations = set(
            [item for subList in nodesInConstellations for item in subList]
        )
        for node in setOfNodesInConstellations:
            if node not in nodeDelaysByNode.keys():
                nodeDelaysByNode.update({node: 0})

    if chainNames:
        nodesInChains = [
            getNodesFromChain(stkRoot, chainName, overrideData=overrideData)
            for chainName in chainNames
        ]
        temp = [
            item for subList in nodesInChains for item in subList
        ]  # Sometimes get lists of lists of lists
        setOfNodesInChains = set([item for subList in temp for item in subList])
        for node in setOfNodesInChains:
            if node not in nodeDelaysByNode.keys():
                nodeDelaysByNode.update({node: 0})
    return nodeDelaysByNode


# Get nodes from chains
def getNodesFromChain(stkRoot, chainName, overrideData=False):
    filename = f"SavedNodes/{chainName}.pkl"
    # Read in existing nodes
    if os.path.exists(filename) and not overrideData:
        with open(filename, "rb") as f:
            listNodes = pickle.load(f)
    # Get nodes in the chain
    else:
        chain = AgChain(stkRoot.GetObjectFromPath(f"Chain/{chainName}"))
        listNodes = []
        for ii in range(chain.Objects.Count):
            objPath = _getObjectShortPath(chain.Objects.Item(ii).Path)
            objType = chain.Objects.Item(ii).Type
            objName = chain.Objects.Item(ii).Name
            if objType == AgESTKObjectType.eConstellation:
                listNodes.append(getNodesFromConstellation(stkRoot, objName))
            else:
                listNodes.append(objPath)
        # Save nodes
        with open(filename, "wb") as f:
            pickle.dump(listNodes, f)
    return listNodes


def getNodesFromConstellation(stkRoot, constellationName, overrideData=False):
    filename = f"SavedNodes/{constellationName}.pkl"
    # Read in existing nodes
    if os.path.exists(filename) and not overrideData:
        with open(filename, "rb") as f:
            nodes = pickle.load(f)
    # Get nodes from constellation
    else:
        constellation = AgConstellation(
            stkRoot.GetObjectFromPath(f"Constellation/{constellationName}")
        )
        nodes = []
        for ii in range(constellation.Objects.Count):
            objPath = _getObjectShortPath(constellation.Objects.Item(ii).Path)
            nodes.append(objPath)
        # Save nodes
        with open(filename, "wb") as f:
            pickle.dump(nodes, f)
    return nodes


def getStrands(
    stkRoot, chainName, start, stop, overrideData=False, printLoadedMessage=True
):
    filename = f"SavedStrands/{chainName}.pkl"

    # Read in existing strands
    if os.path.exists(filename) and not overrideData:
        with open(filename, "rb") as f:
            strands = pickle.load(f)
        if printLoadedMessage:
            print(f"Loaded {chainName}.pkl")

    # Compute and parse strands
    else:
        chain = stkRoot.GetObjectFromPath(f"Chain/{chainName}")
        strandAccessDataProvider = AgDataPrvInterval(
            chain.DataProviders.Item("Strand Access")
        )
        elements = ["Strand Name (Long)", "Start Time", "Stop Time"]
        result = strandAccessDataProvider.ExecElements(start, stop, elements)
        strandNames = []
        starts = []
        stops = []
        data = [
            result.DataSets.Item(ii).GetValues() for ii in range(result.DataSets.Count)
        ]
        for ii in range(0, len(data), 3):
            for jj in range(len(data[ii])):
                paths = data[ii][jj].split(" to ")
                # strandName = tuple('/'.join(path.split('/')[-2:]) for path in paths)
                strandName = []
                for path in paths:
                    strandName.append(_getObjectShortPath(path))
                strandName = tuple(strandName)
                strandNames.append(strandName)
                starts.append(data[ii + 1][jj])
                stops.append(data[ii + 2][jj])
        strands = tuple(zip(strandNames, starts, stops))
        # Save strands
        with open(filename, "wb") as f:
            pickle.dump(strands, f)

    return strands


def getAllStrands(
    stkRoot, chainNames, start, stop, overrideData=False, printLoadedMessage=True
):
    strands = mergeStrands(
        [
            getStrands(
                stkRoot,
                chainName,
                start,
                stop,
                overrideData=overrideData,
                printLoadedMessage=printLoadedMessage,
            )
            for chainName in chainNames
        ]
    )
    dfStrands = pd.DataFrame(strands, columns=["strand", "start", "stop"]).sort_values(
        ["start", "stop"]
    )
    dfStrands["dur"] = dfStrands["stop"] - dfStrands["start"]
    dfStrands["num hops"] = dfStrands["strand"].apply(lambda x: len(x) - 2)
    dfStrands.loc[dfStrands["num hops"] < 0, "num hops"] = np.nan
    return strands, dfStrands


def mergeStrands(unmergedStrands):
    mergedStrands = [strand for strands in unmergedStrands for strand in strands]
    return mergedStrands


def getNodesFromStrands(strands):
    nodes = list(set([node for strand in strands for node in strand[0]]))
    return nodes


def getNodesFromStrandsAtTime(strands):
    nodes = list(set([node for strand in strands for node in strand]))
    return nodes


def getNodesIntervalsFromStrands(strands):
    nodesIntervals = [
        (node, strand[1], strand[2]) for strand in strands for node in strand[0]
    ]
    dfNodesIntervals = pd.DataFrame(nodesIntervals, columns=["node", "start", "stop"])
    # Group by node and merge
    groups = dfNodesIntervals.groupby("node")
    mergedNodesIntervals = []
    for name, group in groups:
        group = group.sort_values("start")
        intervals = group[["start", "stop"]].to_numpy()
        mergedIntervals = mergeIntervals(intervals)
        for interval in mergedIntervals:
            mergedNodesIntervals.append((name, interval[0], interval[1]))
    dfNodesIntervalsMerged = pd.DataFrame(
        mergedNodesIntervals, columns=["node", "start", "stop"]
    )
    dfNodesIntervalsMerged["dur"] = (
        dfNodesIntervalsMerged["stop"] - dfNodesIntervalsMerged["start"]
    )
    return dfNodesIntervalsMerged


def getActiveDuration(dfIntervals, start, stop):
    groups = dfIntervals.groupby(dfIntervals.columns[0])
    dfActive = pd.DataFrame(groups["dur"].sum())
    dfActive.columns = ["sum dur"]
    dfActive["% time active "] = dfActive["sum dur"] / (stop - start) * 100
    return dfActive


def mergeIntervals(intervals):
    stack = []
    stack.append(intervals[0])
    for i in range(1, len(intervals)):
        lastElement = stack[len(stack) - 1]
        if lastElement[1] >= intervals[i][0]:
            lastElement[1] = max(intervals[i][1], lastElement[1])
            stack.pop(len(stack) - 1)
            stack.append(lastElement)
        else:
            stack.append(intervals[i])
    return stack


# Get Edges
def getEdgesFromStrands(strands):
    edges = list(
        set(
            [
                (strand[0][ii], strand[0][ii + 1])
                for strand in set(strands)
                for ii in range(len(strand[0]) - 1)
            ]
        )
    )
    return edges


# Get Edges for strandsAtTime
def getEdgesFromStrandsAtTime(strands):
    edges = list(
        set(
            [
                (strand[ii], strand[ii + 1])
                for strand in set(strands)
                for ii in range(len(strand) - 1)
            ]
        )
    )
    return edges


def getEdgesIntervalsFromStrands(strands):
    edgesIntervals = [
        ((strand[0][ii], strand[0][ii + 1]), strand[1], strand[2])
        for strand in set(strands)
        for ii in range(len(strand[0]) - 1)
    ]
    dfEdgesIntervals = pd.DataFrame(edgesIntervals, columns=["edge", "start", "stop"])

    # Group by edge and merge
    groups = dfEdgesIntervals.groupby("edge")
    mergedEdgesIntervals = []
    for name, group in groups:
        group = group.sort_values("start")
        intervals = group[["start", "stop"]].to_numpy()
        mergedIntervals = mergeIntervals(intervals)
        for interval in mergedIntervals:
            mergedEdgesIntervals.append((name, interval[0], interval[1]))
    dfEdgesIntervalsMerged = pd.DataFrame(
        mergedEdgesIntervals, columns=["edge", "start", "stop"]
    )
    dfEdgesIntervalsMerged["dur"] = (
        dfEdgesIntervalsMerged["stop"] - dfEdgesIntervalsMerged["start"]
    )
    return dfEdgesIntervalsMerged


# Convert intervals into discrete time steps
def getStrandsAtTimes(strands, start, stop, step):
    strands = np.array(strands)
    starts = strands[:, 1]
    stops = strands[:, 2]
    strandsAtTimes = {}
    times = np.arange(start, stop, step)
    times = np.append(times, stop)
    for t in times:
        strandsAtTime = list(strands[(t >= starts) & (t <= stops), 0])
        strandsAtTimes.update({t: strandsAtTime})
    return strandsAtTimes


def computeNodesPosOverTime(stkRoot, strands, start, stop, step, overrideData=False):
    # Get nodes
    nodes = getNodesFromStrands(strands)
    nodesToAppend = [node for node in nodes if len(node.split("/")) > 2]
    parentNodes = ["/".join(node.split("/")[0:2]) for node in nodes]
    # Get stationary nodes and remove from nodes
    stationaryNodes = []
    movingNodes = []
    for node in parentNodes:
        if node.split("/")[0] in ["Facility", "Place", "Target", "AreaTarget"]:
            stationaryNodes.append(node)
        else:
            movingNodes.append(node)
    # Compute position of stationary nodes in Fixed frame
    times = np.arange(start, stop, step)
    times = np.append(times, stop)
    nodesTimesPos = {}
    for node in stationaryNodes:
        filename = f'SavedPositions/{node.split("/")[-1]}.pkl'
        # Read in node position
        if os.path.exists(filename) and not overrideData:
            with open(filename, "rb") as f:
                timePosDict = pickle.load(f)
        else:
            cmd = f"Position */{node}"
            result = stkRoot.ExecuteCommand(cmd)
            xyzList = result.Item(0).split(" ")[3:6]
            pos = tuple([float(ii) / 1000 for ii in xyzList])
            timePosDict = {}
            for t in times:
                timePosDict[t] = pos
            with open(filename, "wb") as f:
                pickle.dump(timePosDict, f)
        nodesTimesPos.update({node: timePosDict})
    # Pull distance over time for moving objects
    for node in movingNodes:
        filename = f'SavedPositions/{node.split("/")[-1]}.pkl'
        # Read in node position
        if os.path.exists(filename) and not overrideData:
            with open(filename, "rb") as f:
                timePosDict = pickle.load(f)
        else:
            obj = stkRoot.GetObjectFromPath(f"{node}")
            cartPos = AgDataProviderGroup(obj.DataProviders.Item("Cartesian Position"))
            pos = AgDataPrvTimeVar(cartPos.Group.Item("Fixed"))
            result = pos.Exec(start, stop, step)
            timePos = result.DataSets.ToArray()
            timePosDict = dict([(row[0], (row[1], row[2], row[3])) for row in timePos])
            with open(filename, "wb") as f:
                pickle.dump(timePosDict, f)
        nodesTimesPos.update({node: timePosDict})
    # Add in pos of children objects using the parent's position
    for node in nodesToAppend:
        nodesTimesPos.update({node: nodesTimesPos["/".join(node.split("/")[0:2])]})

    return nodesTimesPos


def computeTimeStrandsDistancesDelays(
    strandsAtTimes, timeEdgesDistancesDelays, start, stop, step
):
    timeStrandsDistances = {
        t: strandsAtTimeToStrandDistanceDelay(
            t, strandsAtTime, timeEdgesDistancesDelays[t]
        )
        for t, strandsAtTime in strandsAtTimes.items()
    }

    # Turn into sorted dataframe
    times = np.arange(start, stop, step)
    times = np.append(times, stop)
    dfTimeStrandsDistances = pd.DataFrame(
        [
            (t, strand, distanceDelay[0], distanceDelay[1])
            for t in times
            for strand, distanceDelay in timeStrandsDistances[t].items()
        ],
        columns=["time", "strand", "distance", "time delay"],
    )

    dfTimeStrandsDistances["num hops"] = dfTimeStrandsDistances["strand"].apply(
        lambda x: len(x) - 2
    )
    dfTimeStrandsDistances.loc[
        dfTimeStrandsDistances["num hops"] < 0, "num hops"
    ] = np.nan
    dfTimeStrandsDistances = dfTimeStrandsDistances.sort_values(["time", "distance"])

    # Get minimum
    timesMinStrandsDistances = [
        (
            t,
            computeMinStrand(timeStrandsDistances[t])[0],
            computeMinStrand(timeStrandsDistances[t])[1][0],
            computeMinStrand(timeStrandsDistances[t])[1][1],
        )
        for t in times
    ]
    dfMinStrandsDistances = pd.DataFrame(
        timesMinStrandsDistances, columns=["time", "strand", "distance", "time delay"]
    )
    dfMinStrandsDistances["num hops"] = dfMinStrandsDistances["strand"].apply(
        lambda x: len(x) - 2
    )
    dfMinStrandsDistances.loc[
        dfMinStrandsDistances["num hops"] < 0, "num hops"
    ] = np.nan

    return timeStrandsDistances, dfTimeStrandsDistances, dfMinStrandsDistances


def strandsAtTimeToStrandDistanceDelay(t, strandsAtTime, edgesDistancesDelays):
    # Strands and their total distance
    strandsAtTimeDistanceDelay = {
        strandAtTime: computePathDistanceDelay(strandAtTime, edgesDistancesDelays)
        for strandAtTime in strandsAtTime
    }
    return strandsAtTimeDistanceDelay


def computePathDistanceDelay(strandAtTime, edgesDistancesDelays):
    strandDistance = sum(
        edgesDistancesDelays[strandAtTime[ii : ii + 2]][0]
        for ii in range(len(strandAtTime) - 1)
    )
    strandDelay = sum(
        edgesDistancesDelays[strandAtTime[ii : ii + 2]][1]
        for ii in range(len(strandAtTime) - 1)
    )
    return strandDistance, strandDelay


def computeTimeStrandsDistances3(strandsAtTimes, timeEdgesDistances, start, stop, step):
    timeStrandsDistances = {
        t: strandsAtTimeToStrandDistance3(t, strandsAtTime, timeEdgesDistances[t])
        for t, strandsAtTime in strandsAtTimes.items()
    }

    # Turn into sorted dataframe
    times = np.arange(start, stop, step)
    times = np.append(times, stop)
    dfTimeStrandsDistances = pd.DataFrame(
        [
            (t, strand, distance)
            for t in times
            for strand, distance in timeStrandsDistances[t].items()
        ],
        columns=["time", "strand", "distance"],
    )
    dfTimeStrandsDistances["num hops"] = dfTimeStrandsDistances["strand"].apply(
        lambda x: len(x) - 2
    )
    dfTimeStrandsDistances.loc[
        dfTimeStrandsDistances["num hops"] < 0, "num hops"
    ] = np.nan
    dfTimeStrandsDistances = dfTimeStrandsDistances.sort_values(["time", "distance"])

    # Get minimum
    timesMinStrandsDistances = [
        (
            t,
            computeMinStrand(timeStrandsDistances[t])[0],
            computeMinStrand(timeStrandsDistances[t])[1],
        )
        for t in times
    ]
    dfMinStrandsDistances = pd.DataFrame(
        timesMinStrandsDistances, columns=["time", "strand", "distance"]
    )
    dfMinStrandsDistances["num hops"] = dfMinStrandsDistances["strand"].apply(
        lambda x: len(x) - 2
    )
    dfMinStrandsDistances.loc[
        dfMinStrandsDistances["num hops"] < 0, "num hops"
    ] = np.nan

    return timeStrandsDistances, dfTimeStrandsDistances, dfMinStrandsDistances


def strandsAtTimeToStrandDistance3(t, strandsAtTime, edgesDistances):
    # Strands and their total distance
    strandsAtTimeDistance = {
        strandAtTime: computePathDistance(strandAtTime, edgesDistances)
        for strandAtTime in strandsAtTime
    }
    return strandsAtTimeDistance


def strandsAtTimeToEdges3(t, strandsAtTime, nodesTimesPos):
    # Get edges
    edges = getEdgesFromStrandsAtTime(strandsAtTime)
    # Compute edge distances
    edgesDistances = computeEdgeDistance3(t, edges, nodesTimesPos)
    return edgesDistances


def strandsAtTimeToNodes3(t, strandsAtTime, nodesTimesPos):
    # Get node
    nodes = getNodesFromStrandsAtTime(strandsAtTime)
    # get pos
    timeNodePos = {node: nodesTimesPos[node][t] for node in nodes}
    return timeNodePos


def computeTimeNodePos(strandsAtTimes, nodesTimesPos):
    timeNodePos = {
        t: strandsAtTimeToNodes3(t, strandsAtTime, nodesTimesPos)
        for t, strandsAtTime in strandsAtTimes.items()
    }
    return timeNodePos


def computeTimeEdgesDistances(strandsAtTimes, nodesTimesPos):
    timeEdgesDistances = {
        t: strandsAtTimeToEdges3(t, strandsAtTime, nodesTimesPos)
        for t, strandsAtTime in strandsAtTimes.items()
    }
    return timeEdgesDistances


###
def computeEdgeDistanceDelay(t, edges, nodesTimesPos, nodeDelaysByNode):
    edgePosDelay = np.array(
        [(nodesTimesPos[edge[0]][t], nodesTimesPos[edge[1]][t]) for edge in edges]
    )
    dist = np.linalg.norm(edgePosDelay[:, 0] - edgePosDelay[:, 1], axis=1)
    timeDelay = dist / 299792.458 + np.array(
        [nodeDelaysByNode[edge[1]] for edge in edges]
    )
    return dict(zip(edges, zip(*(dist, timeDelay))))


def strandsAtTimeToEdgesDistancesDelays(
    t, strandsAtTime, nodesTimesPos, nodeDelaysByNode
):
    # Get edges
    edges = getEdgesFromStrandsAtTime(strandsAtTime)
    # Compute edge distances
    edgesDistancesDelays = computeEdgeDistanceDelay(
        t, edges, nodesTimesPos, nodeDelaysByNode
    )
    return edgesDistancesDelays


def computeTimeEdgesDistancesDelays(
    strandsAtTimes, nodesTimesPos, nodeDelaysByNode, overrideData=False
):
    timeEdgesDistancesDelays = {
        t: strandsAtTimeToEdgesDistancesDelays(
            t, strandsAtTime, nodesTimesPos, nodeDelaysByNode
        )
        for t, strandsAtTime in strandsAtTimes.items()
    }
    return timeEdgesDistancesDelays


# Compute distance of edge and strand path
def computeEdgeDistance3(t, edges, nodesTimesPos):
    edgePos = np.array(
        [(nodesTimesPos[edge[0]][t], nodesTimesPos[edge[1]][t]) for edge in edges]
    )
    dist = np.linalg.norm(edgePos[:, 0] - edgePos[:, 1], axis=1)
    return dict(zip(edges, dist))


def computePathDistance(strandAtTime, edgesDistances):
    strandDistance = sum(
        edgesDistances[strandAtTime[ii : ii + 2]] for ii in range(len(strandAtTime) - 1)
    )
    return strandDistance


# find minimum strand
def computeMinStrand(strandsDistances):
    if strandsDistances:
        minStrand = min(strandsDistances, key=strandsDistances.get)
        minStrandDistance = strandsDistances[minStrand]
    else:
        minStrand = ""
        minStrandDistance = np.nan
    return (minStrand, minStrandDistance)


def computeNMinMetric(dfTimeStrandsDistances, n=1, metric="distance"):
    dfTimeStrandsDistances = dfTimeStrandsDistances.sort_values(["time", metric])
    groups = dfTimeStrandsDistances.groupby("time")
    indexes = []
    for name, group in groups:
        index = group.iloc[0:n].index
        for ii in index:
            indexes.append(ii)
    dfMinNStrandsDistances = dfTimeStrandsDistances.loc[indexes]
    return dfMinNStrandsDistances


# Turn into intervals
def createDfIntervals(df, stop, step):
    for col in df.columns:
        if col not in ["time", "strand", "num hops"]:
            df = df.drop(col, axis=1)
    df = df.loc[df["strand"].shift() != df["strand"]]
    df["stop"] = df["time"].shift(-1) - step / 2  # Switches at midpoint
    df["time"] = df["time"].astype(float) - step / 2
    df.iloc[-1, -1] = stop
    df.iloc[0, 0] = df.iloc[0, 0] + step / 2
    df["dur"] = df["stop"] - df["time"]
    df = df[["time", "stop", "dur", "strand", "num hops"]]
    df.columns = ["start", "stop", "dur", "strand", "num hops"]
    return df


# Add strands back to STK with object lines and intervals
def addStrandsAsObjectLines(
    stkRoot, dfIntervals, color="red", lineWidth=4, deleteOldLines=True, addTo2D=False
):
    # Get Edges and intervals
    edges = []
    starts = []
    stops = []
    for index, row in dfIntervals.iterrows():
        for ii in range(len(row["strand"]) - 1):
            edge = (row["strand"][ii], row["strand"][ii + 1])
            edges.append(edge)
            starts.append(row["start"])
            stops.append(row["stop"])

    # Turn into df
    dfEdges = pd.DataFrame([])
    dfEdges["edge"] = edges
    dfEdges["start"] = starts
    dfEdges["stop"] = stops
    dfEdges["start"] = dfEdges["start"].astype(str)
    dfEdges["stop"] = dfEdges["stop"].astype(str)
    dfEdges["startstop"] = dfEdges[["start", "stop"]].apply(
        lambda x: '" "'.join(x), axis=1
    )

    # Group by edge and add edge intervals to STK
    grouped = dfEdges.groupby("edge")
    if deleteOldLines:
        stkRoot.ExecuteCommand("VO * ObjectLine DeleteAll")
    for name, group in grouped:
        node1 = group["edge"].iloc[0][0]
        node2 = group["edge"].iloc[0][1]
        numIntervals = len(group["start"])
        intervals = '"' + '" "'.join(group["startstop"]) + '"'
        cmd = f"VO * ObjectLine Add FromObj {node1} ToObj {node2} Color {color} LineWidth {str(lineWidth)} AddIntervals {str(numIntervals)} {intervals}"
        stkRoot.ExecuteCommand(cmd)
        cmd = f"VO * ObjectLine Modify FromObj {node1} ToObj {node2} IntervalType UseIntervals"
        stkRoot.ExecuteCommand(cmd)
        if addTo2D:
            cmd = f"Graphics * ObjectLine Add FromObj {node1} ToObj {node2} Color {color} LineWidth {str(lineWidth)} AddIntervals {str(numIntervals)} {intervals}"
            stkRoot.ExecuteCommand(cmd)
            cmd = f"Graphics * ObjectLine Modify FromObj {node1} ToObj {node2} IntervalType UseIntervals"
            stkRoot.ExecuteCommand(cmd)
    return


# Add data back to STK
def addDataToSTK(stkRoot, chainName, df, columns="", invalidData=-1):
    # Convert lists to strings
    df = df.fillna(invalidData)

    # Merge any lists into strs
    if not columns:
        columns = list(df.drop("time", axis=1).columns)

    numVars = len(columns)
    colStrs = []
    for col in columns:
        if df[col].dtype == object:
            df[col] = df[col].apply(lambda x: " ".join(x).replace(" ", "_"))
            df.loc[df[col] == "", col] = "invalid"
        else:
            df[col] = df[col].astype(str)

    # Assign units
    for col in columns:
        col = col.replace(" ", "")
        if col in ["timeDelay", "lightDelay", "nodeDelay"]:
            unit = "Time"
        elif col == "distance":
            unit = "Distance"
        elif col == "strand":
            unit = "Char"
        else:
            unit = "Custom"
        colStrs.append("{} {}".format(col, unit))
    colStrs = " ".join(colStrs)

    # Create Groups
    try:
        cmd = f'ExternalData */Chain/{chainName} AddGroup "NetworkData" {numVars} {colStrs}'
        stkRoot.ExecuteCommand(cmd)
    except Exception:
        cmd = f'ExternalData */Chain/{chainName} DeleteGroup "NetworkData"'
        stkRoot.ExecuteCommand(cmd)
        cmd = f'ExternalData */Chain/{chainName} AddGroup "NetworkData" {numVars} {colStrs}'
        stkRoot.ExecuteCommand(cmd)

    # Add data
    array = df[["time"] + columns].to_numpy()
    for ii in range(len(array)):
        t = array[ii, 0]
        data = " ".join(array[ii, 1:])
        cmd = f'ExternalData */Chain/{chainName} AddData "NetworkData" {t} {data}'
        stkRoot.ExecuteCommand(cmd)
    return


# Pathing
def computeFewestStrandSwitches(dfStrands, start, stop):
    dfStrands = dfStrands.sort_values(["start", "stop"]).reset_index(drop=True)
    intervals = dfStrands[["start", "stop"]].to_numpy()
    cur_timeSpan = start
    i = 0
    iVals = []
    max_step = 0
    while i < len(intervals) and cur_timeSpan < stop:
        while i < len(intervals) and intervals[i][0] <= cur_timeSpan:
            max_step = max(max_step, intervals[i][1])
            if max_step <= intervals[i][1]:
                max_step = intervals[i][1]
                # minVal = intervals[i][0]
                iVal = i
            i += 1
        cur_timeSpan = max_step
        iVals.append(iVal)
    return dfStrands.iloc[iVals]


# Get set of starting/ending nodes in a chain
def getStartingNodesFromChain(stkRoot, chainName):
    chain = AgChain(stkRoot.GetObjectFromPath(f"Chain/{chainName}"))
    objType = chain.Objects.Item(0).Type
    objName = chain.Objects.Item(0).Name
    objPath = _getObjectShortPath(chain.Objects.Item(0).Path)
    if objType == AgESTKObjectType.eConstellation:
        listNodes = getNodesFromConstellation(stkRoot, objName)
    else:
        listNodes = [objPath]
    return listNodes


def getEndingNodesFromChain(stkRoot, chainName):
    chain = AgChain(stkRoot.GetObjectFromPath(f"Chain/{chainName}"))
    objType = chain.Objects.Item(chain.Objects.Count - 1).Type
    objName = chain.Objects.Item(chain.Objects.Count - 1).Name
    objPath = _getObjectShortPath(chain.Objects.Item(chain.Objects.Count - 1).Path)
    if objType == AgESTKObjectType.eConstellation:
        listNodes = getNodesFromConstellation(stkRoot, objName)
    else:
        listNodes = [objPath]
    return listNodes


def getStartingAndEndingNodes(stkRoot, chainNames):
    nodes = [getStartingNodesFromChain(stkRoot, chainName) for chainName in chainNames]
    startingNodes = list(set([item for sublist in nodes for item in sublist]))
    nodes = [getEndingNodesFromChain(stkRoot, chainName) for chainName in chainNames]
    endingNodes = list(set([item for sublist in nodes for item in sublist]))
    return startingNodes, endingNodes


def createChains(
    stkRoot,
    startingConstellation,
    firstConnectingConstellation,
    endingConstellation,
    secondConnectingConstellation="",
    color=Colors.FromRGB(0, 196, 196),
):
    conStart = stkRoot.GetObjectFromPath(f"*/Constellation/{startingConstellation}")
    conFirst = stkRoot.GetObjectFromPath(
        f"*/Constellation/{firstConnectingConstellation}"
    )
    conEnd = stkRoot.GetObjectFromPath(f"*/Constellation/{endingConstellation}")

    chainName = "StartingToConnecting"
    _chainCreationHelper(stkRoot, chainName, color, conStart, conFirst)

    if secondConnectingConstellation != "":
        conSecond = stkRoot.GetObjectFromPath(
            f"*/Constellation/{secondConnectingConstellation}"
        )
        chainName = "FirstToSecond"
        _chainCreationHelper(stkRoot, chainName, color, conFirst, conSecond)

        chainName = "SecondToFirst"
        _chainCreationHelper(stkRoot, chainName, color, conSecond, conFirst)

        chainName = "ConnectingToEnding"
        _chainCreationHelper(stkRoot, chainName, color, conSecond, conEnd)

        chainNames = [
            "StartingToConnecting",
            "FirstToSecond",
            "SecondToFirst",
            "ConnectingToEnding",
        ]
    else:
        chainName = "ConnectingToConnecting"
        _chainCreationHelper(stkRoot, chainName, color, conFirst, conFirst)

        chainName = "ConnectingToEnding"
        _chainCreationHelper(stkRoot, chainName, color, conFirst, conEnd)

        chainNames = [
            "StartingToConnecting",
            "ConnectingToConnecting",
            "ConnectingToEnding",
        ]

    return chainNames


def _chainCreationHelper(stkRoot, chainName, color, constellation1, constellation2):
    if stkRoot.CurrentScenario.Children.Contains(AgESTKObjectType.eChain, chainName):
        chain = AgChain(stkRoot.GetObjectFromPath(f"*/Chain/{chainName}"))
    else:
        chain = AgChain(
            stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eChain, chainName)
        )
        chain.Graphics.Animation.LineWidth = 0
        chain.Graphics.Animation.Color = color
    chain.Objects.RemoveAll()
    chain.Objects.AddObject(constellation1)
    chain.Objects.AddObject(constellation2)


def _getObjectShortPath(objectPath: str):
    return "/".join(objectPath.split("/")[5:])


def generateNetwork(t, timeEdgesDistancesDelays, timeNodePos):
    G = nx.Graph()
    _networkGen(G, t, timeEdgesDistancesDelays, timeNodePos)
    return G


def generateDiNetwork(t, timeEdgesDistancesDelays, timeNodePos):
    G = nx.DiGraph()
    _networkGen(G, t, timeEdgesDistancesDelays, timeNodePos)
    return G


def _networkGen(G, t, timeEdgesDistancesDelays, timeNodePos):
    for edge, distanceDelay in timeEdgesDistancesDelays[t].items():
        distance = distanceDelay[0]
        timeDelay = distanceDelay[1]
        G.add_edge(*edge, distance=distance, timeDelay=timeDelay)
    for node, pos in timeNodePos[t].items():
        G.add_node(node, Type=node.split("/")[0], Position=pos)


def getDegreeCentrality(G, topN=[]):
    if not topN:
        topN = len(G.nodes())
    scores = np.array(list(nx.degree_centrality(G).items()))
    scores = scores[np.argsort(scores[:, 1]), :][::-1]
    return scores[0:topN, :]


def getClosenessCentrality(G, topN=[]):
    if not topN:
        topN = len(G.nodes())
    scores = np.array(list(nx.closeness_centrality(G).items()))
    scores = scores[np.argsort(scores[:, 1]), :][::-1]
    return scores[0:topN, :]


def getBetweennessSubsetCentrality(G, startingNodes, endingNodes, topN=[]):
    if not topN:
        topN = len(G.nodes())
    scores = np.array(
        list(
            nx.betweenness_centrality_subset(
                G, startingNodes, endingNodes, normalized=True
            ).items()
        )
    )
    scores = scores[np.argsort(scores[:, 1]), :][::-1]
    return scores[0:topN, :]


def countNodesOverTime(listOfListOfNodesAtT, topN=""):
    if not topN:
        topN = len(listOfListOfNodesAtT)
    scores = np.array(
        list(
            Counter(
                [item for sublist in listOfListOfNodesAtT for item in sublist]
            ).items()
        )
    )
    scores = scores[np.argsort(scores[:, 1].astype(float)), :][::-1]
    return scores[0:topN, :]


def countEdgesOverTime(listOfEdges, topN=""):
    if not topN:
        topN = len(listOfEdges)
    scores = np.asarray(list(Counter(listOfEdges).items()))
    scores = scores[np.argsort(scores[:, 1].astype(float)), :][::-1]
    return scores[0:topN, :]


def getMaxDegrees(G):
    nodes, degs = zip(*G.degree(G.nodes()))
    indexes = np.argwhere(degs == np.amax(degs))
    mostEdgesAtT = []
    for index in indexes:
        mostEdgesAtT.append(nodes[index[0]])
    maxDeg = degs[index[0]]
    return maxDeg, mostEdgesAtT


# Find how many nodes need to be removed to lose access between starting and ending nodes
def shortestStrandDistance(G, startingNodes, endingNodes, metric="distance"):
    Gnodes = G.nodes()
    startingNodesSub = [node for node in startingNodes if node in Gnodes]
    endingNodesSub = [node for node in endingNodes if node in Gnodes]
    pathLengths = []
    for node1 in startingNodesSub:
        for node2 in endingNodesSub:
            try:
                pathLengths.append(
                    (
                        node1,
                        node2,
                        nx.shortest_path_length(G, node1, node2, weight=metric),
                    )
                )
            except Exception:
                pass
    pathLengths = np.asarray(pathLengths)
    try:
        nodesDist = pathLengths[np.argmin(pathLengths[:, 2])]
        strand = nx.shortest_path(G, nodesDist[0], nodesDist[1], weight=metric)
        distance = nodesDist[2]
    except Exception:
        strand = ""
        distance = np.nan
    return strand, distance


def shortestStrandDistanceOptimized(G, metric="distance"):
    try:
        distance = nx.shortest_path_length(G, "Source", "Sink", weight=metric)
        strand = nx.shortest_path(G, "Source", "Sink", weight=metric)
    except Exception:
        strand = ""
        distance = np.nan
    return strand, distance


def numNodesToLoseAccessBetweenAnyPair(G, startingNodes, endingNodes):
    # Only loop through nodes in G
    Gnodes = G.nodes()
    startingNodesSub = [node for node in startingNodes if node in Gnodes]
    endingNodesSub = [node for node in endingNodes if node in Gnodes]

    if len(startingNodesSub) == 0 or len(endingNodesSub) == 0:  # Check if empty
        maxNumNodesToRemove = 0
        minNumNodesToRemove = 0
    else:  # Look through each node pair
        numNodesToRemove = []
        for source in startingNodesSub:
            for target in endingNodesSub:
                numNodesToRemove.append(
                    len(
                        nx.algorithms.connectivity.cuts.minimum_st_node_cut(
                            G, s=source, t=target
                        )
                    )
                )

        maxNumNodesToRemove = max(
            numNodesToRemove
        )  # Most nodes to remove to disconnect a pair

        if len(startingNodesSub) != len(startingNodes) or len(endingNodesSub) != len(
            endingNodes
        ):
            minNumNodesToRemove = 0  # If starting or ending nodes are missing, then the least nodes to remove is 0
        else:
            minNumNodesToRemove = min(
                numNodesToRemove
            )  # Least nodes to remove to disconnect a pair

    return minNumNodesToRemove, maxNumNodesToRemove


def nodesToLoseAccess(G, startingNodes, endingNodes, loseAccessTo="all", topN=10):
    Gnodes = G.nodes()
    startingNodesSub = [node for node in startingNodes if node in Gnodes]
    endingNodesSub = [node for node in endingNodes if node in Gnodes]

    if loseAccessTo.lower() == "all":
        nodesToRemove = nodesToLoseAccessAll(
            G, startingNodesSub, endingNodesSub, topN=topN
        )
    elif loseAccessTo.lower() == "any":
        if len(startingNodesSub) == len(startingNodes) and len(endingNodesSub) == len(
            endingNodes
        ):
            startingAndEnding = startingNodes + endingNodes
            nodesToRemove = []
            for cutSet in list(nx.all_node_cuts(G)):
                if not any(item in startingAndEnding for item in cutSet):
                    nodesToRemove.append(tuple(cutSet))
        else:
            nodesToRemove = []
    return nodesToRemove


def nodesToLoseAccessAll(G, startingNodesSub, endingNodesSub, topN=10):
    nodeScores = getBetweennessSubsetCentrality(
        G, startingNodesSub, endingNodesSub, topN=topN
    )

    # Remove 0 value and remove starting and ending nodes
    nodeScores = nodeScores[nodeScores[:, 1].astype(float) > 0]
    indexes = [
        i
        for i in range(len(nodeScores))
        if nodeScores[i, 0] not in startingNodesSub + endingNodesSub
    ]
    nodeScores = nodeScores[indexes, :]

    # Loop through each combination and remove nodes, break once a minimum number is found
    nodeSet = set()
    if len(nodeScores) > 0:

        # Get possible combinations of nodes
        numToRemoveAndCombinations = {}
        for i in range(1, len(nodeScores) + 1):
            numToRemoveAndCombinations.update(
                {i: list(itertools.combinations(nodeScores[:, 0], i))}
            )

        solutionFound = False
        for numToRemove, nodesToRemove in numToRemoveAndCombinations.items():
            for nodes in nodesToRemove:
                GSub = G.copy()
                GSub.remove_nodes_from(nodes)
                bestScore = getBetweennessSubsetCentrality(
                    GSub, startingNodesSub, endingNodesSub, topN=1
                )
                if float(bestScore[0, 1]) == 0:
                    nodeSet.add(nodes)
                    solutionFound = True
            if solutionFound:
                break
        # Run again and append old set of nodes
        if not solutionFound:
            nodeSet.add(nodes)
            nodeSetNew = nodesToLoseAccessAll(
                GSub, startingNodesSub, endingNodesSub, topN=topN
            )
            solutionSets = set()
            for solution in nodeSetNew:
                solution = list(solution)
                for nodes in list(nodeSet):
                    for node in nodes:
                        solution.append(node)
                solutionSets.add(tuple(solution))
            nodeSet = solutionSets
    return list(nodeSet)


def network_plot_3D(
    G, t=0, save=False, metric="distance", startingNodes=[], endingNodes=[]
):
    # Get node positions
    pos = nx.get_node_attributes(G, "Position")
    # Define color by type
    colors = []
    for node in G.nodes(data=True):
        if node[1]["Type"] in ("Place", "Target", "Facility", "AreaTarget"):
            colors.append("red")
        else:
            colors.append("blue")
    # 3D network plot
    #     with plt.style.context(('ggplot')):
    fig = plt.figure(figsize=(8, 6))
    ax = Axes3D(fig)
    # Extract the x,y,z coordinates of each node
    xs = []
    ys = []
    zs = []
    degs = []
    for key, value in pos.items():
        xs.append(value[0])
        ys.append(value[1])
        zs.append(value[2])
        degs.append(G.degree(key))
    # Scatter plot
    ax.scatter(
        xs, ys, zs, c=colors, s=20 + 20 * np.array(degs), edgecolors="k", alpha=0.7
    )
    # Loop over edges to get the x,y,z, coordinates of ends
    for i, j in enumerate(G.edges()):
        x = np.array((pos[j[0]][0], pos[j[1]][0]))
        y = np.array((pos[j[0]][1], pos[j[1]][1]))
        z = np.array((pos[j[0]][2], pos[j[1]][2]))
        # Plot connecting lines
        ax.plot(x, y, z, c="black", alpha=0.5)

    # Plot shortest Path
    if startingNodes and endingNodes:
        strand, distance = shortestStrandDistance(
            G, startingNodes, endingNodes, metric=metric
        )
        edges = [(strand[ii], strand[ii + 1]) for ii in range(len(strand) - 1)]
        for i, j in enumerate(edges):
            x = np.array((pos[j[0]][0], pos[j[1]][0]))
            y = np.array((pos[j[0]][1], pos[j[1]][1]))
            z = np.array((pos[j[0]][2], pos[j[1]][2]))
            # Plot connecting lines
            ax.plot(x, y, z, c="yellow", alpha=1, linewidth=2)
    set_axes_equal(ax)
    if save:
        plt.savefig(str(t) + ".png")
        plt.close("all")
    else:
        plt.show()
    return


# https://stackoverflow.com/questions/13685386/matplotlib-equal-unit-length-with-equal-aspect-ratio-z-axis-is-not-equal-to
def set_axes_equal(ax):
    x_limits = ax.get_xlim3d()
    y_limits = ax.get_ylim3d()
    z_limits = ax.get_zlim3d()
    x_range = abs(x_limits[1] - x_limits[0])
    x_middle = np.mean(x_limits)
    y_range = abs(y_limits[1] - y_limits[0])
    y_middle = np.mean(y_limits)
    z_range = abs(z_limits[1] - z_limits[0])
    z_middle = np.mean(z_limits)
    plot_radius = 0.5 * max([x_range, y_range, z_range])
    ax.set_xlim3d([x_middle - plot_radius, x_middle + plot_radius])
    ax.set_ylim3d([y_middle - plot_radius, y_middle + plot_radius])
    ax.set_zlim3d([z_middle - plot_radius, z_middle + plot_radius])


def computeNetworkMetrics(
    start,
    stop,
    step,
    timeNodePos,
    timesEdgesDistancesDelays,
    startingNodes,
    endingNodes,
    metric,
    computeNumNodesToLoseAccessBetweenAnyPair=False,
    overrideData=False,
    printTime=False,
    filename="",
    diNetwork=False,
):
    # Build new network at each time and gather metrics
    t1 = time.time()

    # Use first nodes for the naming convention
    startingNode = startingNodes[0].split("/")[-1]
    endingNode = endingNodes[0].split("/")[-1]
    filename = f"SavedNetworkData/df{filename}{startingNode}{endingNode}.pkl"

    needToCompute = True
    if os.path.exists(filename) and not overrideData:
        with open(filename, "rb") as f:
            df = pickle.load(f)
        computedTimes = np.array(df["time"])
        neededTimes = np.array(list(timesEdgesDistancesDelays.keys()))
        missingDataTimes = [t for t in neededTimes if t not in computedTimes]
        if len(missingDataTimes) == 0:
            needToCompute = False
            if len(computedTimes) != len(neededTimes):
                index = 0
                indices = []
                for t in computedTimes:
                    if t in neededTimes:
                        indices.append(index)
                    index += 1
                df = df.iloc[indices, :]

    if needToCompute:
        # Define initial variables
        timeStrandMetric = []
        lowestNumOfMinNodesToLoseAccess = []
        highestNumOfMinNodesToLoseAccess = []

        # Loop through each time
        times = np.arange(start, stop, step)
        times = np.append(times, stop)
        for t in times:

            # Generate Network at each time
            if diNetwork:
                if metric.lower() == "bandwidth":
                    G = generateDiNetworkBandwidth(
                        t, timesEdgesDistancesDelays, timeNodePos
                    )  # Build a directed network if two constellations are used
                else:
                    G = generateDiNetwork(
                        t, timesEdgesDistancesDelays, timeNodePos
                    )  # Build a directed network if two constellations are used
            else:
                G = generateNetwork(t, timesEdgesDistancesDelays, timeNodePos)
            # Find shortest strand metric
            if any([node in G.nodes() for node in startingNodes]) and any(
                [node in G.nodes() for node in endingNodes]
            ):
                strandShort, metricVal = shortestStrandDistance(
                    G, startingNodes, endingNodes, metric=metric
                )
                timeStrandMetric.append((t, strandShort, metricVal))
            else:
                timeStrandMetric.append((t, "", np.nan))

            # Min num of nodes to remove to lose access between pairs of starting and ending nodes. This takes awhile to compute
            if computeNumNodesToLoseAccessBetweenAnyPair:
                lowestNum, highestNum = numNodesToLoseAccessBetweenAnyPair(
                    G, startingNodes, endingNodes
                )
                lowestNumOfMinNodesToLoseAccess.append(lowestNum)
                highestNumOfMinNodesToLoseAccess.append(highestNum)

        df = pd.DataFrame(timeStrandMetric, columns=["time", "strand", metric])
        df["num hops"] = df["strand"].apply(lambda x: len(x) - 2)
        df["num parent hops"] = df["strand"].apply(
            lambda x: len(set([str.split(obj, "/")[1] for obj in x])) - 2
        )  # numb parent hops
        df.loc[df["num hops"] < 0, "num hops"] = np.nan
        df.loc[df["num parent hops"] < 0, "num parent hops"] = np.nan
        df[metric] = df[metric].astype(float)
        if computeNumNodesToLoseAccessBetweenAnyPair:
            df[
                "Highest Num Nodes Removed To Lose Access"
            ] = highestNumOfMinNodesToLoseAccess
            df[
                "Lowest Num Nodes Removed To Lose Access"
            ] = lowestNumOfMinNodesToLoseAccess

        #         dfIntervals = createDfIntervals(df,stop,step)
        #     addStrandsAsObjectLines(stkRoot,dfIntervals,color='yellow',deleteOldLines = False)
        with open(filename, "wb") as f:
            pickle.dump(df, f)

    if printTime:
        print(time.time() - t1)
    return df


def filterObjectsByType(stkRoot, objectType, parentObject, filterName):
    listOfObjects = []
    for child in parentObject.Children:
        if child.ClassName == objectType and filterName in child.InstanceName:
            listOfObjects.append(child.InstanceName)
        listOfObjects.extend(
            filterObjectsByType(stkRoot, objectType, child, filterName)
        )
    return listOfObjects


def plotDegDistribution(degreesPerNodes):
    degreesOfNodes = [val for sublist in degreesPerNodes for val in sublist]
    numOfDegreeCounts = np.array(
        [float(val) for val in Counter(degreesOfNodes).values()]
    )
    plt.bar(Counter(degreesOfNodes).keys(), numOfDegreeCounts / len(degreesOfNodes))
    plt.xlabel("Degree")
    plt.ylabel("Fraction Of Nodes")
    return
