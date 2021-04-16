import numpy as np
import pandas as pd
from comtypes.gen import STKObjects
import networkx as nx
import itertools
from mpl_toolkits.mplot3d import Axes3D
import matplotlib.pyplot as plt
from collections import Counter

# node delays by node
def getNodeDelaysByNode(stkRoot,nodeDelays,constellationNames='',chainNames=''):
    nodeDelaysByNode = {}
    
    for conName,delay in nodeDelays.items():
        nodes = getNodesFromConstellation(stkRoot,conName)
        for node in nodes:
            nodeDelaysByNode.update({node:delay})
    
    if constellationNames:
        nodesInConstellations = [getNodesFromConstellation(stkRoot,conName) for conName in constellationNames]
        setOfNodesInConstellations = set([item for subList in nodesInConstellations for item in subList])
        for node in setOfNodesInConstellations:
            if node not in nodeDelaysByNode.keys():
                nodeDelaysByNode.update({node:0})
   
    if chainNames:        
        nodesInChains = [getNodesFromChain(stkRoot,chainName) for chainName in chainNames]
        temp = [item for subList in nodesInChains for item in subList] # Sometimes get lists of lists of lists
        setOfNodesInChains = set([item for subList in temp for item in subList])
        for node in setOfNodesInChains:
            if node not in nodeDelaysByNode.keys():
                nodeDelaysByNode.update({node:0})
    return nodeDelaysByNode


# Get nodes from chains
def getNodesFromChain(stkRoot,chainName):
    chain = stkRoot.GetObjectFromPath('Chain/'+chainName)
    chain2 = chain.QueryInterface(STKObjects.IAgChain)
    listNodes = []
    for ii in range(chain2.Objects.Count):
        path = chain2.Objects.Item(ii).Path
        if path.split('/')[-2] == 'Constellation': 
            listNodes.append(getNodesFromConstellation(stkRoot,path.split('/')[-1]))
        else:
            if path.split('/')[-2] == 'Sensor':
                nodePath = '/'.join(path.split('/')[-4:])  
            elif path.split('/')[-2] in ['Transmitter','Receiver','Antenna']:
                if path.split('/')[-4] == 'Sensor':
                    nodePath = '/'.join(path.split('/')[-6:])
                else:
                    nodePath = '/'.join(path.split('/')[-4:])                    
            else:
                nodePath = '/'.join(path.split('/')[-2:])
            listNodes.append(nodePath)
    return listNodes




# Compute strand accesses and merge
def getStrandsDoesntWork(stkRoot,chainName,start,stop):
    chain = stkRoot.GetObjectFromPath('Chain/'+chainName)
    chain2 = chain.QueryInterface(STKObjects.IAgChain)
    sa = chain.DataProviders.Item('Strand Access').QueryInterface(STKObjects.IAgDataPrvInterval)
    elems = ['Strand Name (Long)','Start Time','Stop Time']
    result = sa.ExecElements(start,stop,elems)
    strandNames = []
    starts = []
    stops = []
    data = result.DataSets.ToArray()[0]
    for ii in range(0,len(data),3):     
        paths = data[ii].split(' to ')
        strandName = []
        for path in paths:
            if path.split('/')[-2] == 'Sensor':
                nodePath = '/'.join(path.split('/')[-4:])  
            elif path.split('/')[-2] in ['Transmitter','Receiver','Antenna']:
                if path.split('/')[-4] == 'Sensor':
                    nodePath = '/'.join(path.split('/')[-6:])
                else:
                    nodePath = '/'.join(path.split('/')[-4:])                    
            else:
                nodePath = '/'.join(path.split('/')[-2:])
            strandName.append(nodePath)
        strandName = tuple(strandName)                           
        strandNames.append(strandName) 
        starts.append(data[ii+1])
        stops.append(data[ii+2])
    strands = tuple(zip(strandNames,starts,stops))
    return strands

def getStrands(stkRoot,chainName,start,stop):
    chain = stkRoot.GetObjectFromPath('Chain/'+chainName)
    chain2 = chain.QueryInterface(STKObjects.IAgChain)
    sa = chain.DataProviders.Item('Strand Access').QueryInterface(STKObjects.IAgDataPrvInterval)
    elems = ['Strand Name (Long)','Start Time','Stop Time']
    result = sa.ExecElements(start,stop,elems)
    strandNames = []
    starts = []
    stops = []
    data = [result.DataSets.Item(ii).GetValues() for ii in range(result.DataSets.Count)]
    for ii in range(0,len(data),3):     
        for jj in range(len(data[ii])):
            paths = data[ii][jj].split(' to ')
#             strandName = tuple('/'.join(path.split('/')[-2:]) for path in paths)
            strandName = []
            for path in paths:
                if path.split('/')[-2] == 'Sensor':
                    nodePath = '/'.join(path.split('/')[-4:])  
                elif path.split('/')[-2] in ['Transmitter','Receiver','Antenna']:
                    if path.split('/')[-4] == 'Sensor':
                        nodePath = '/'.join(path.split('/')[-6:])
                    else:
                        nodePath = '/'.join(path.split('/')[-4:])                    
                else:
                    nodePath = '/'.join(path.split('/')[-2:])
                strandName.append(nodePath)
            strandName = tuple(strandName)                           
            strandNames.append(strandName) 
            starts.append(data[ii+1][jj])
            stops.append(data[ii+2][jj])
    strands = tuple(zip(strandNames,starts,stops))
    return strands


def getAllStrands(stkRoot,chainNames,start,stop):
    strands = mergeStrands([getStrands(stkRoot,chainName,start,stop) for chainName in chainNames])
    dfStrands = pd.DataFrame(strands,columns=['strand','start','stop']).sort_values(['start','stop'])
    dfStrands['dur'] = dfStrands['stop'] - dfStrands['start']
    dfStrands['num hops'] = dfStrands['strand'].apply(lambda x: len(x)-2)
    dfStrands.loc[dfStrands['num hops'] < 0,'num hops'] = np.nan
    return strands,dfStrands

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
    nodesIntervals = [(node,strand[1],strand[2]) for strand in strands for node in strand[0]]
    dfNodesIntervals = pd.DataFrame(nodesIntervals,columns=['node','start','stop'])
    
    # Group by node and merge
    groups = dfNodesIntervals.groupby('node')
    mergedNodesIntervals = []
    for name,group in groups:
        group = group.sort_values('start')
        intervals = group[['start','stop']].to_numpy()
        mergedIntervals = mergeIntervals(intervals)
        for interval in mergedIntervals:
            mergedNodesIntervals.append((name,interval[0],interval[1]))
    dfNodesIntervalsMerged = pd.DataFrame(mergedNodesIntervals,columns=['node','start','stop'])
    dfNodesIntervalsMerged['dur'] = dfNodesIntervalsMerged['stop']-dfNodesIntervalsMerged['start']
    return dfNodesIntervalsMerged

def getActiveDuration(dfIntervals,start,stop):
    groups = dfIntervals.groupby(dfIntervals.columns[0])
    dfActive = pd.DataFrame(groups['dur'].sum())
    dfActive.columns = ['sum dur']
    dfActive['% time active '] = dfActive['sum dur']/(stop-start)*100
    return dfActive

def mergeIntervals(intervals):
    stack = []
    stack.append(intervals[0])
    for i in range(1,len(intervals)):
        lastElement = stack[len(stack)-1]
        if lastElement[1] >= intervals[i][0]:
            lastElement[1] = max(intervals[i][1],lastElement[1])
            stack.pop(len(stack)-1)
            stack.append(lastElement)
        else:
            stack.append(intervals[i])
    return stack

# Get Edges
def getEdgesFromStrands(strands):
    edges = list(set([(strand[0][ii],strand[0][ii+1]) for strand in set(strands) for ii in range(len(strand[0])-1)]))
    return edges

# Get Edges for strandsAtTime
def getEdgesFromStrandsAtTime(strands):
    edges = list(set([(strand[ii],strand[ii+1]) for strand in set(strands) for ii in range(len(strand)-1)]))
    return edges

def getEdgesIntervalsFromStrands(strands):
    edgesIntervals = [((strand[0][ii],strand[0][ii+1]),strand[1],strand[2]) for strand in set(strands) for ii in range(len(strand[0])-1)]
    dfEdgesIntervals = pd.DataFrame(edgesIntervals,columns=['edge','start','stop'])
    
    # Group by edge and merge
    groups = dfEdgesIntervals.groupby('edge')
    mergedEdgesIntervals = []
    for name,group in groups:
        group = group.sort_values('start')
        intervals = group[['start','stop']].to_numpy()
        mergedIntervals = mergeIntervals(intervals)
        for interval in mergedIntervals:
            mergedEdgesIntervals.append((name,interval[0],interval[1]))
    dfEdgesIntervalsMerged = pd.DataFrame(mergedEdgesIntervals,columns=['edge','start','stop'])
    dfEdgesIntervalsMerged['dur'] = dfEdgesIntervalsMerged['stop']-dfEdgesIntervalsMerged['start']
    return dfEdgesIntervalsMerged





# Convert intervals into discrete time steps
def getStrandsAtTimes(strands,start,stop,step):
    strands = np.array(strands)
    starts = strands[:,1]
    stops = strands[:,2]
    strandsAtTimes = {}
    times = np.arange(start,stop+step,step)
    for t in times:
        strandsAtTime = list(strands[(t >= starts) & (t <= stops),0])
        strandsAtTimes.update({t:strandsAtTime})
    return strandsAtTimes



def computeNodesPosOverTime(stkRoot,strands,start,stop,step):
    # Get nodes
    nodes = getNodesFromStrands(strands)
    nodesToAppend = [node for node in nodes if len(node.split('/')) >2]
    parentNodes = ['/'.join(node.split('/')[0:2]) for node in nodes]
    # Get stationary nodes and remove from nodes
    stationaryNodes = []
    movingNodes = []
    for node in parentNodes:
        if node.split('/')[0] in ['Facility', 'Place', 'Target','AreaTarget']:
            stationaryNodes.append(node)
        else:
            movingNodes.append(node)
    # Compute position of stationary nodes in Fixed frame
    times = np.arange(start,stop+step,step)
    nodesTimesPos = {}
    for node in stationaryNodes:
        cmd = 'Position */'+node
        result = stkRoot.ExecuteCommand(cmd)
        xyzList = result.Item(0).split(' ')[3:6]
        pos = tuple([float(ii)/1000 for ii in xyzList])
        timePosDict = {}
        for t in times:
            timePosDict[t] = pos
        nodesTimesPos.update({node:timePosDict})
    # Pull distance over time for moving objects
    for node in movingNodes:
        obj = stkRoot.GetObjectFromPath('*/'+node)
        cartPos = obj.DataProviders.Item('Cartesian Position')
        cartPos = cartPos.QueryInterface(STKObjects.IAgDataProviderGroup)
        pos = cartPos.Group.Item('Fixed')
        pos = pos.QueryInterface(STKObjects.IAgDataPrvTimeVar)
        result = pos.Exec(start,stop,step)
        timePos = result.DataSets.ToArray()
        timePosDict = dict([(row[0],(row[1],row[2],row[3]))for row in timePos])
        nodesTimesPos.update({node:timePosDict})
    # Add in pos of children objects using the parent's position
    for node in nodesToAppend:
        nodesTimesPos.update({node:nodesTimesPos['/'.join(node.split('/')[0:2])]})
    return nodesTimesPos


def computeTimeStrandsDistancesDelays(strandsAtTimes,timeEdgesDistancesDelays,start,stop,step):
    timeStrandsDistances = {t: strandsAtTimeToStrandDistanceDelay(t,strandsAtTime,timeEdgesDistancesDelays[t]) for t,strandsAtTime in strandsAtTimes.items()}

    # Turn into sorted dataframe
    times = np.arange(start,stop+step,step)
    dfTimeStrandsDistances = pd.DataFrame([(t,strand,distanceDelay[0],distanceDelay[1]) for t in times for strand,distanceDelay in timeStrandsDistances[t].items()],columns=['time','strand','distance','time delay'])

    dfTimeStrandsDistances['num hops'] = dfTimeStrandsDistances['strand'].apply(lambda x: len(x)-2)
    dfTimeStrandsDistances.loc[dfTimeStrandsDistances['num hops'] < 0,'num hops'] = np.nan
    dfTimeStrandsDistances = dfTimeStrandsDistances.sort_values(['time','distance'])
    
    # Get minimum
    timesMinStrandsDistances = [(t,computeMinStrand(timeStrandsDistances[t])[0],computeMinStrand(timeStrandsDistances[t])[1][0],computeMinStrand(timeStrandsDistances[t])[1][1]) for t in times]
    dfMinStrandsDistances = pd.DataFrame(timesMinStrandsDistances,columns=['time','strand','distance','time delay'])
    dfMinStrandsDistances['num hops'] = dfMinStrandsDistances['strand'].apply(lambda x: len(x)-2)
    dfMinStrandsDistances.loc[dfMinStrandsDistances['num hops'] < 0,'num hops'] = np.nan
    
    return timeStrandsDistances,dfTimeStrandsDistances,dfMinStrandsDistances

def strandsAtTimeToStrandDistanceDelay(t,strandsAtTime,edgesDistancesDelays):
    # Strands and their total distance
    strandsAtTimeDistanceDelay = {strandAtTime: computePathDistanceDelay(strandAtTime,edgesDistancesDelays) for strandAtTime in strandsAtTime}
    return strandsAtTimeDistanceDelay

def computePathDistanceDelay(strandAtTime,edgesDistancesDelays):
    strandDistance = sum(edgesDistancesDelays[strandAtTime[ii:ii+2]][0] for ii in range(len(strandAtTime)-1))
    strandDelay = sum(edgesDistancesDelays[strandAtTime[ii:ii+2]][1] for ii in range(len(strandAtTime)-1))
    return strandDistance,strandDelay






def computeTimeStrandsDistances3(strandsAtTimes,timeEdgesDistances,start,stop,step):
    timeStrandsDistances = {t: strandsAtTimeToStrandDistance3(t,strandsAtTime,timeEdgesDistances[t]) for t,strandsAtTime in strandsAtTimes.items()}

    # Turn into sorted dataframe
    times = np.arange(start,stop+step,step)
    dfTimeStrandsDistances = pd.DataFrame([(t,strand,distance) for t in times for strand,distance in timeStrandsDistances[t].items()],columns=['time','strand','distance'])
    dfTimeStrandsDistances['num hops'] = dfTimeStrandsDistances['strand'].apply(lambda x: len(x)-2)
    dfTimeStrandsDistances.loc[dfTimeStrandsDistances['num hops'] < 0,'num hops'] = np.nan
    dfTimeStrandsDistances = dfTimeStrandsDistances.sort_values(['time','distance'])
    
    # Get minimum
    timesMinStrandsDistances = [(t,computeMinStrand(timeStrandsDistances[t])[0],computeMinStrand(timeStrandsDistances[t])[1]) for t in times]
    dfMinStrandsDistances = pd.DataFrame(timesMinStrandsDistances,columns=['time','strand','distance'])
    dfMinStrandsDistances['num hops'] = dfMinStrandsDistances['strand'].apply(lambda x: len(x)-2)
    dfMinStrandsDistances.loc[dfMinStrandsDistances['num hops'] < 0,'num hops'] = np.nan
    
    return timeStrandsDistances,dfTimeStrandsDistances,dfMinStrandsDistances

def strandsAtTimeToStrandDistance3(t,strandsAtTime,edgesDistances):
    # Strands and their total distance
    strandsAtTimeDistance = {strandAtTime: computePathDistance(strandAtTime,edgesDistances) for strandAtTime in strandsAtTime}
    return strandsAtTimeDistance

def strandsAtTimeToEdges3(t,strandsAtTime,nodesTimesPos):
    # Get edges
    edges = getEdgesFromStrandsAtTime(strandsAtTime)
    # Compute edge distances
    edgesDistances = computeEdgeDistance3(t,edges,nodesTimesPos)
    return edgesDistances

def strandsAtTimeToNodes3(t,strandsAtTime,nodesTimesPos):
    # Get node
    nodes = getNodesFromStrandsAtTime(strandsAtTime)
    # get pos
    timeNodePos = {node :nodesTimesPos[node][t] for node in nodes}
    return timeNodePos

def computeTimeNodePos(strandsAtTimes,nodesTimesPos):
    timeNodePos = {t: strandsAtTimeToNodes3(t,strandsAtTime,nodesTimesPos) for t,strandsAtTime in strandsAtTimes.items()}
    return timeNodePos

def computeTimeEdgesDistances(strandsAtTimes,nodesTimesPos):
    timeEdgesDistances = {t: strandsAtTimeToEdges3(t,strandsAtTime,nodesTimesPos) for t,strandsAtTime in strandsAtTimes.items()}
    return timeEdgesDistances

###
def computeEdgeDistanceDelay(t,edges,nodesTimesPos,nodeDelaysByNode):
    edgePosDelay = np.array([(nodesTimesPos[edge[0]][t],nodesTimesPos[edge[1]][t]) for edge in edges])        
    dist = np.linalg.norm(edgePosDelay[:,0]-edgePosDelay[:,1],axis=1)
    timeDelay = dist/299792.458+np.array([nodeDelaysByNode[edge[1]] for edge in edges])
    return dict(zip(edges,zip(*(dist,timeDelay))))

def strandsAtTimeToEdgesDistancesDelays(t,strandsAtTime,nodesTimesPos,nodeDelaysByNode):
    # Get edges
    edges = getEdgesFromStrandsAtTime(strandsAtTime)
    # Compute edge distances
    edgesDistancesDelays = computeEdgeDistanceDelay(t,edges,nodesTimesPos,nodeDelaysByNode)
    return edgesDistancesDelays

def computeTimeEdgesDistancesDelays(strandsAtTimes,nodesTimesPos,nodeDelaysByNode):
    timeEdgesDistancesDelays = {t: strandsAtTimeToEdgesDistancesDelays(t,strandsAtTime,nodesTimesPos,nodeDelaysByNode) for t,strandsAtTime in strandsAtTimes.items()}
    return timeEdgesDistancesDelays


# Compute distance of edge and strand path

def computeEdgeDistance3(t,edges,nodesTimesPos):
    edgePos = np.array([(nodesTimesPos[edge[0]][t],nodesTimesPos[edge[1]][t]) for edge in edges])
    dist = np.linalg.norm(edgePos[:,0]-edgePos[:,1],axis=1)
    return dict(zip(edges,dist))


def computePathDistance(strandAtTime,edgesDistances):
    strandDistance = sum(edgesDistances[strandAtTime[ii:ii+2]] for ii in range(len(strandAtTime)-1))
    return strandDistance

# find minimum strand
def computeMinStrand(strandsDistances):
    if strandsDistances:
        minStrand = min(strandsDistances,key=strandsDistances.get)
        minStrandDistance = strandsDistances[minStrand] 
    else:
        minStrand = ''
        minStrandDistance = np.nan
    return (minStrand,minStrandDistance)

def computeNMinMetric(dfTimeStrandsDistances,n=1,metric='distance'):
    dfTimeStrandsDistances = dfTimeStrandsDistances.sort_values(['time',metric])
    groups = dfTimeStrandsDistances.groupby('time')
    indexes = []
    for name,group in groups:
        index = group.iloc[0:n].index
        for ii in index:
            indexes.append(ii)
    dfMinNStrandsDistances = dfTimeStrandsDistances.loc[indexes]
    return dfMinNStrandsDistances


# Turn into intervals
def createDfIntervals(df,stop,step):
    for col in df.columns:
        if col not in ['time','strand','num hops']:
            df = df.drop(col,axis=1)
    df = df.loc[df['strand'].shift() != df['strand']]
    df['stop'] = df['time'].shift(-1)-step/2 # Switches at midpoint
    df['time'] = df['time'].astype(float)-step/2
    df.iloc[-1,-1] = stop
    df.iloc[0,0] = df.iloc[0,0]+step/2
    df['dur'] = df['stop']-df['time']
    df = df[['time','stop','dur','strand','num hops']]
    df.columns = ['start','stop','dur','strand','num hops']
    return df


# Add strands back to STK with object lines and intervals
def addStrandsAsObjectLines(stkRoot,dfIntervals,color='red',lineWidth=4):
    # Get Edges and intervals
    edges = []
    starts = []
    stops = []
    for index, row in dfIntervals.iterrows():
        for ii in range(len(row['strand'])-1):
            edge = (row['strand'][ii],row['strand'][ii+1])
            edges.append(edge)
            starts.append(row['start'])
            stops.append(row['stop'])

    # Turn into df
    dfEdges = pd.DataFrame([])
    dfEdges['edge'] = edges
    dfEdges['start'] = starts
    dfEdges['stop'] = stops
    dfEdges['start'] = dfEdges['start'].astype(str)
    dfEdges['stop'] = dfEdges['stop'].astype(str)
    dfEdges['startstop'] = dfEdges[['start','stop']].apply(lambda x: '" "'.join(x), axis=1)
    
    # Group by edge and add edge intervals to STK
    grouped = dfEdges.groupby('edge')
    stkRoot.ExecuteCommand('VO * ObjectLine DeleteAll')
    for name, group in grouped:
        node1 = group['edge'].iloc[0][0]
        node2 = group['edge'].iloc[0][1]
        numIntervals = len(group['start'])
        intervals = '"'+'" "'.join(group['startstop'])+'"'
        cmd = 'VO * ObjectLine Add FromObj '+node1+' ToObj '+node2+' Color '+color+' LineWidth '+str(lineWidth)+' AddIntervals '+str(numIntervals)+' '+intervals
        stkRoot.ExecuteCommand(cmd)
        cmd = 'VO * ObjectLine Modify FromObj '+node1+' ToObj '+node2+' IntervalType UseIntervals'
        stkRoot.ExecuteCommand(cmd)
    return

# Add data back to STK
def addDataToSTK(stkRoot,chainName,df,columns = '',invalidData = -1):

    # Convert lists to strings
    df = df.fillna(invalidData)
    
    # Merge any lists into strs
    if not columns:
        columns = list(df.drop('time',axis=1).columns)
    
    numVars = len(columns)
    numRows = len(df)
    colStrs = []
    for col in columns:
        if df[col].dtype == object:
            df[col] = df[col].apply(lambda x: ' '.join(x).replace(' ','_'))
            df.loc[df[col] == '',col] = 'invalid'
        else:
            df[col] = df[col].astype(str)

    # Assign units
    for col in columns:
        col = col.replace(' ','')
        if col == 'timeDelay':
            unit = 'Time'
        elif col == 'distance':
            unit = 'Distance'
        elif col == 'strand':
            unit = 'Char'
        else:
            unit = 'Custom'
        colStrs.append('{} {}'.format(col,unit))
    colStrs = ' '.join(colStrs)

    # Create Groups
    try:
        cmd = 'ExternalData */Chain/{} AddGroup "NetworkData" {} {}'.format(chainName,numVars,colStrs)
        stkRoot.ExecuteCommand(cmd)
    except:
        cmd = 'ExternalData */Chain/{} DeleteGroup "NetworkData"'.format(chainName)
        stkRoot.ExecuteCommand(cmd)
        cmd = 'ExternalData */Chain/{} AddGroup "NetworkData" {} {}'.format(chainName,numVars,colStrs)
        stkRoot.ExecuteCommand(cmd)

    # Add data
    array = df[['time']+columns].to_numpy()
    for ii in range(len(array)):
        t = array[ii,0]    
        data = ' '.join(array[ii,1:])
        cmd = 'ExternalData */Chain/{} AddData "NetworkData" {} {}'.format(chainName,t,data)
        stkRoot.ExecuteCommand(cmd)   
    return




# Pathing 
def computeFewestStrandSwitches(dfStrands,start,stop):
    dfStrands = dfStrands.sort_values(['start','stop']).reset_index(drop=True)
    intervals = dfStrands[['start','stop']].to_numpy()
    cur_timeSpan = start
    i = 0
    iVals = []
    max_step = 0
    while i < len(intervals) and cur_timeSpan < stop:
        while i < len(intervals) and intervals[i][0] <= cur_timeSpan:
            max_step = max(max_step, intervals[i][1])
            if max_step <= intervals[i][1]:
                max_step = intervals[i][1]
                minVal = intervals[i][0]
                iVal = i
            i += 1
        cur_timeSpan = max_step
        iVals.append(iVal)
    return dfStrands.iloc[iVals]

# Get set of starting/ending nodes in a chain

def getStartingNodesFromChain(stkRoot,chainName):
    chain = stkRoot.GetObjectFromPath('Chain/'+chainName)
    chain2 = chain.QueryInterface(STKObjects.IAgChain)
    obj = '/'.join(chain2.Objects.Item(0).Path.split('/')[-2:])
    if obj.split('/')[0] == 'Constellation': 
        listNodes = getNodesFromConstellation(stkRoot,obj.split('/')[1])
    else:
        listNodes = [obj]
    return listNodes

def getEndingNodesFromChain(stkRoot,chainName):
    chain = stkRoot.GetObjectFromPath('Chain/'+chainName)
    chain2 = chain.QueryInterface(STKObjects.IAgChain)
    obj = '/'.join(chain2.Objects.Item(chain2.Objects.Count-1).Path.split('/')[-2:])
    if obj.split('/')[0] == 'Constellation': 
        listNodes = getNodesFromConstellation(stkRoot,obj.split('/')[1])
    else:
        listNodes = [obj]
    return listNodes


def getStartingAndEndingNodes(stkRoot,chainNames):
    nodes = [getStartingNodesFromChain(stkRoot,chainName) for chainName in chainNames]
    startingNodes = list(set([item for sublist in nodes for item in sublist]))
    nodes = [getEndingNodesFromChain(stkRoot,chainName) for chainName in chainNames]
    endingNodes = list(set([item for sublist in nodes for item in sublist]))
    return startingNodes,endingNodes

def getNodesFromConstellation(stkRoot,constellationName):
    con = stkRoot.GetObjectFromPath('Constellation/'+constellationName)
    con2 = con.QueryInterface(STKObjects.IAgConstellation)
    nodes = []
    for ii in range(con2.Objects.Count):
        path = con2.Objects.Item(ii).Path
        if path.split('/')[-2] == 'Sensor':
            nodePath = '/'.join(path.split('/')[-4:])  
        elif path.split('/')[-2] in ['Transmitter','Receiver','Antenna']:
            if path.split('/')[-4] == 'Sensor':
                nodePath = '/'.join(path.split('/')[-6:])
            else:
                nodePath = '/'.join(path.split('/')[-4:])                    
        else:
            nodePath = '/'.join(path.split('/')[-2:])
        nodes.append(nodePath)
    return nodes

def createChains(stkRoot,startingConstellation,firstConnectingConstellation,endingConstellation,secondConnectingConstellation='',color=12895232):
    conStart = stkRoot.GetObjectFromPath('*/Constellation/'+startingConstellation)
    conFirst = stkRoot.GetObjectFromPath('*/Constellation/'+firstConnectingConstellation)
    conEnd = stkRoot.GetObjectFromPath('*/Constellation/'+endingConstellation)

    chainName = 'StartingToConnecting'
    if stkRoot.CurrentScenario.Children.Contains(STKObjects.eChain,chainName):
        chain = stkRoot.GetObjectFromPath('*/Chain/'+chainName)
        chain2 = chain.QueryInterface(STKObjects.IAgChain)
    else:
        chain = stkRoot.CurrentScenario.Children.New(STKObjects.eChain,chainName)
        chain2 = chain.QueryInterface(STKObjects.IAgChain)
        chain2.Graphics.Animation.LineWidth = 0
        chain2.Graphics.Animation.Color = color
    chain2.Objects.RemoveAll()
    chain2.Objects.AddObject(conStart)
    chain2.Objects.AddObject(conFirst)
    
    
    if secondConnectingConstellation != '':
        conSecond = stkRoot.GetObjectFromPath('*/Constellation/'+secondConnectingConstellation)
        chainName = 'FirstToSecond'
        if stkRoot.CurrentScenario.Children.Contains(STKObjects.eChain,chainName):
            chain = stkRoot.GetObjectFromPath('*/Chain/'+chainName)
            chain2 = chain.QueryInterface(STKObjects.IAgChain)
        else:
            chain = stkRoot.CurrentScenario.Children.New(STKObjects.eChain,chainName)
            chain2 = chain.QueryInterface(STKObjects.IAgChain)
            chain2.Graphics.Animation.LineWidth = 0
            chain2.Graphics.Animation.Color = color
        chain2.Objects.RemoveAll()
        chain2.Objects.AddObject(conFirst)
        chain2.Objects.AddObject(conSecond)

        chainName = 'SecondToFirst'
        if stkRoot.CurrentScenario.Children.Contains(STKObjects.eChain,chainName):
            chain = stkRoot.GetObjectFromPath('*/Chain/'+chainName)
            chain2 = chain.QueryInterface(STKObjects.IAgChain)
        else:
            chain = stkRoot.CurrentScenario.Children.New(STKObjects.eChain,chainName)
            chain2 = chain.QueryInterface(STKObjects.IAgChain)
            chain2.Graphics.Animation.LineWidth = 0
            chain2.Graphics.Animation.Color = color
        chain2.Objects.RemoveAll()
        chain2.Objects.AddObject(conSecond)
        chain2.Objects.AddObject(conFirst)
        
        chainName = 'ConnectingToEnding'
        if stkRoot.CurrentScenario.Children.Contains(STKObjects.eChain,chainName):
            chain = stkRoot.GetObjectFromPath('*/Chain/'+chainName)
            chain2 = chain.QueryInterface(STKObjects.IAgChain)
        else:
            chain = stkRoot.CurrentScenario.Children.New(STKObjects.eChain,chainName)
            chain2 = chain.QueryInterface(STKObjects.IAgChain)
            chain2.Graphics.Animation.LineWidth = 0
            chain2.Graphics.Animation.Color = color
        chain2.Objects.RemoveAll()
        chain2.Objects.AddObject(conSecond)
        chain2.Objects.AddObject(conEnd)
        
        chainNames  = ['StartingToConnecting','FirstToSecond','SecondToFirst','ConnectingToEnding']
    else:
        chainName = 'ConnectingToConnecting'
        if stkRoot.CurrentScenario.Children.Contains(STKObjects.eChain,chainName):
            chain = stkRoot.GetObjectFromPath('*/Chain/'+chainName)
            chain2 = chain.QueryInterface(STKObjects.IAgChain)
        else:
            chain = stkRoot.CurrentScenario.Children.New(STKObjects.eChain,chainName)
            chain2 = chain.QueryInterface(STKObjects.IAgChain)
            chain2.Graphics.Animation.LineWidth = 0
            chain2.Graphics.Animation.Color = color
        chain2.Objects.RemoveAll()
        chain2.Objects.AddObject(conFirst)
        chain2.Objects.AddObject(conFirst)
        
        chainName = 'ConnectingToEnding'
        if stkRoot.CurrentScenario.Children.Contains(STKObjects.eChain,chainName):
            chain = stkRoot.GetObjectFromPath('*/Chain/'+chainName)
            chain2 = chain.QueryInterface(STKObjects.IAgChain)
        else:
            chain = stkRoot.CurrentScenario.Children.New(STKObjects.eChain,chainName)
            chain2 = chain.QueryInterface(STKObjects.IAgChain)
            chain2.Graphics.Animation.LineWidth = 0
            chain2.Graphics.Animation.Color = color
        chain2.Objects.RemoveAll()
        chain2.Objects.AddObject(conFirst)
        chain2.Objects.AddObject(conEnd)
    
        chainNames  = ['StartingToConnecting','ConnectingToConnecting','ConnectingToEnding']


    return chainNames


########## Nx ###################

def generateNetwork(t,timeEdgesDistancesDelays,timeNodePos):
    G = nx.Graph()
    for edge,distanceDelay in timeEdgesDistancesDelays[t].items():
        distance = distanceDelay[0]
        timeDelay = distanceDelay[1]
        G.add_edge(*edge,distance=distance,timeDelay=timeDelay)
    for node,pos in timeNodePos[t].items():
        G.add_node(node,Type=node.split('/')[0],Position=pos)
    return G

def generateDiNetwork(t,timeEdgesDistancesDelays,timeNodePos):
    G = nx.DiGraph()
    for edge,distanceDelay in timeEdgesDistancesDelays[t].items():
        distance = distanceDelay[0]
        timeDelay = distanceDelay[1]
        G.add_edge(*edge,distance=distance,timeDelay=timeDelay)
    for node,pos in timeNodePos[t].items():
        G.add_node(node,Type=node.split('/')[0],Position=pos)
    return G

def getDegreeCentrality(G,topN=[]):
    if not topN:
        topN = len(G.nodes())
    scores = np.array(list(nx.degree_centrality(G).items()))
    scores = scores[np.argsort(scores[:,1]),:][::-1]
    return scores[0:topN,:]

def getClosenessCentrality(G,topN=[]):
    if not topN:
        topN = len(G.nodes())
    scores = np.array(list(nx.closeness_centrality(G).items()))
    scores = scores[np.argsort(scores[:,1]),:][::-1]
    return scores[0:topN,:]

def getBetweennessSubsetCentrality(G,startingNodes,endingNodes,topN=[]):
    if not topN:
        topN = len(G.nodes())
    scores = np.array(list(nx.betweenness_centrality_subset(G,startingNodes,endingNodes,normalized=True).items()))
    scores = scores[np.argsort(scores[:,1]),:][::-1]
    return scores[0:topN,:]

def countNodesOverTime(listOfListOfNodesAtT,topN=''):
    if not topN:
        topN = len(listOfListOfNodesAtT)
    scores = np.array(list(Counter([item for sublist in listOfListOfNodesAtT for item in sublist]).items()))
    scores = scores[np.argsort(scores[:,1].astype(float)),:][::-1]
    return scores[0:topN,:]


def getMaxDegrees(G):
    nodes,degs = zip(*G.degree(G.nodes()))
    indexes = np.argwhere(degs == np.amax(degs))
    mostEdgesAtT= []
    for index in indexes:
        mostEdgesAtT.append(nodes[index[0]])
    maxDeg = degs[index[0]]
    return maxDeg, mostEdgesAtT

# Find how many nodes need to be removed to lose access between starting and ending nodes
def shortestStrandDistance(G,startingNodes,endingNodes,metric='distance'):
    Gnodes = G.nodes()
    startingNodesSub = [node for node in startingNodes if node in Gnodes]
    endingNodesSub = [node for node in endingNodes if node in Gnodes]
    pathLengths = []
    for node1 in startingNodesSub:
        for node2 in endingNodesSub:
            try:
                pathLengths.append((node1,node2,nx.shortest_path_length(G,node1,node2,weight=metric)))
            except:
                pass
    pathLengths = np.asarray(pathLengths)
    try:
        nodesDist = pathLengths[np.argmin(pathLengths[:,2])]
        strand = nx.shortest_path(G,nodesDist[0],nodesDist[1],weight=metric)
        distance = nodesDist[2]
    except:
        strand = ''
        distance = np.nan
    return strand,distance

# Find how many nodes need to be removed to lose access between starting and ending nodes
# def nodesToLoseAccessOld(G,startingNodes,endingNodes,loseAccessTo='all'):
#     Gnodes = G.nodes()
#     startingNodesSub = [node for node in startingNodes if node in Gnodes]
#     endingNodesSub = [node for node in endingNodes if node in Gnodes]
#     nodesToRemove = []
#     if loseAccessTo.lower() == 'all':
#         for node1 in startingNodesSub:
#             for node2 in endingNodesSub:
#                 nodesToRemoveAtT = nx.minimum_node_cut(G,node1,node2)
#                 for node in nodesToRemoveAtT:
#                     if node not in nodesToRemove:
#                         nodesToRemove.append(node)
#     elif loseAccessTo.lower() == 'any':
#         if len(startingNodesSub) == len(startingNodes) and len(endingNodesSub) == len(endingNodes):
#             for node1 in startingNodesSub:
#                 for node2 in endingNodesSub:
#                     nodesToRemove.append(nx.minimum_node_cut(G,node1,node2))
#             minNodes = min([len(nodes) for nodes in nodesToRemove])
#             nodesToRemove = list(np.unique([node for nodes in nodesToRemove if len(nodes)==minNodes for node in nodes]))
#     return nodesToRemove


def nodesToLoseAccess(G,startingNodes,endingNodes,loseAccessTo='all',topN=10):
    Gnodes = G.nodes()
    startingNodesSub = [node for node in startingNodes if node in Gnodes]
    endingNodesSub = [node for node in endingNodes if node in Gnodes]
   
    if loseAccessTo.lower() == 'all':
        nodesToRemove = nodesToLoseAccessAll(G,startingNodesSub,endingNodesSub,topN=topN)
    elif loseAccessTo.lower() == 'any':
        if len(startingNodesSub) == len(startingNodes) and len(endingNodesSub) == len(endingNodes):
            startingAndEnding = startingNodes+endingNodes
            nodesToRemove = [] 
            for cutSet in list(nx.all_node_cuts(G)):
                if not any(item in startingAndEnding for item in cutSet):
                    nodesToRemove.append(tuple(cutSet)) 
        else:
            nodesToRemove = []
    return nodesToRemove

def nodesToLoseAccessAll(G,startingNodesSub,endingNodesSub,topN=10):
    nodeScores = getBetweennessSubsetCentrality(G,startingNodesSub,endingNodesSub,topN=topN)

    # Remove 0 value and remove starting and ending nodes
    nodeScores = nodeScores[nodeScores[:,1].astype(float) > 0]
    indexes = [i for i in range(len(nodeScores)) if nodeScores[i,0] not in startingNodesSub+endingNodesSub]
    nodeScores = nodeScores[indexes,:]
    


    # Loop through each combination and remove nodes, break once a minimum number is found
    nodeSet = set()
    if len(nodeScores)>0:
    
        # Get possible combinations of nodes
        numToRemoveAndCombinations = {}
        for i in range(1,len(nodeScores)+1):
            numToRemoveAndCombinations.update({i:list(itertools.combinations(nodeScores[:,0],i))})

        solutionFound = False
        for numToRemove,nodesToRemove in numToRemoveAndCombinations.items():
            for nodes in nodesToRemove:
                GSub = G.copy()
                GSub.remove_nodes_from(nodes)
                bestScore = getBetweennessSubsetCentrality(GSub,startingNodesSub,endingNodesSub,topN=1)
                if float(bestScore[0,1]) == 0:
                    nodeSet.add(nodes)
                    solutionFound = True
            if solutionFound == True:
                break
        # Run again and append old set of nodes
        if solutionFound == False:
            nodeSet.add(nodes)
            nodeSetNew = nodesToLoseAccessAll(GSub,startingNodesSub,endingNodesSub,topN=topN)
            solutionSets = set()
            for solution in nodeSetNew:
                solution = list(solution)
                for nodes in list(nodeSet):
                    for node in nodes:
                        solution.append(node)
                solutionSets.add(tuple(solution))
            nodeSet = solutionSets 
    return list(nodeSet)


def network_plot_3D(G,t=0,save=False,metric='distance',startingNodes=[],endingNodes=[]):
    # Get node positions
    pos = nx.get_node_attributes(G, 'Position')
    # Define color by type
    colors = []
    for node in G.nodes(data=True):
        if node[1]['Type'] in ('Place','Target','Facility','AreaTarget'):
            colors.append('red')
        else:
            colors.append('blue')
    # 3D network plot
#     with plt.style.context(('ggplot')):
    fig = plt.figure(figsize=(8,6))
    ax = Axes3D(fig)
    # Extract the x,y,z coordinates of each node
    xs = []
    ys= []
    zs = []
    degs = []
    for key, value in pos.items():
        xs.append(value[0])
        ys.append(value[1])
        zs.append(value[2])
        degs.append(G.degree(key))
    # Scatter plot
    ax.scatter(xs, ys, zs, c=colors, s=20+20*np.array(degs), edgecolors='k', alpha=0.7)
    # Loop over edges to get the x,y,z, coordinates of ends
    for i,j in enumerate(G.edges()):
        x = np.array((pos[j[0]][0], pos[j[1]][0]))
        y = np.array((pos[j[0]][1], pos[j[1]][1]))
        z = np.array((pos[j[0]][2], pos[j[1]][2]))
    # Plot connecting lines
        ax.plot(x, y, z, c='black', alpha=0.5)
    
    # Plot shortest Path
    if startingNodes and endingNodes:
        strand,distance = shortestStrandDistance(G,startingNodes,endingNodes,metric=metric)
        edges = [(strand[ii],strand[ii+1]) for ii in range(len(strand)-1)]
        for i,j in enumerate(edges):
            x = np.array((pos[j[0]][0], pos[j[1]][0]))
            y = np.array((pos[j[0]][1], pos[j[1]][1]))
            z = np.array((pos[j[0]][2], pos[j[1]][2]))
        # Plot connecting lines
            ax.plot(x, y, z, c='yellow', alpha=1,linewidth=2)
    set_axes_equal(ax)
    if save == True:
        plt.savefig(str(t)+'.png')
        plt.close('all')
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
    plot_radius = 0.5*max([x_range, y_range, z_range])
    ax.set_xlim3d([x_middle - plot_radius, x_middle + plot_radius])
    ax.set_ylim3d([y_middle - plot_radius, y_middle + plot_radius])
    ax.set_zlim3d([z_middle - plot_radius, z_middle + plot_radius])




def FilterObjectsByType(root,objType,name = ''):

    xml = root.AllInstanceNamesToXML()

    # split the xml by object paths
    objs = xml.split('path=')
    objs = objs[1:] # remove first string of '<'

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


def plotDegDistribution(degreesPerNodes):
    degreesOfNodes = [val for sublist in degreesPerNodes for val in sublist]
    numOfDegreeCounts = np.array([float(val) for val in Counter(degreesOfNodes).values()])
    plt.bar(Counter(degreesOfNodes).keys(),numOfDegreeCounts/len(degreesOfNodes))
    plt.xlabel('Degree')
    plt.ylabel('Fraction Of Nodes')
#     plt.show()
    return

######################## Old functions below #################################



##############################################################################



##############################################################################
# Get nodes
def GetNodesFromChain(stkRoot,chainName):
    chain = stkRoot.GetObjectFromPath('Chain/'+chainName)
    chain2 = chain.QueryInterface(STKObjects.IAgChain)
    listNodes = []
    objs = ['/'.join(chain2.Objects.Item(ii).Path.split('/')[-2:]) for ii in range(chain2.Objects.Count)]
    constellations = [obj for obj in objs if obj.split('/')[0] == 'Constellation']
    constellations    

    for obj in objs:
        if obj.split('/')[0] == 'Constellation': 
            con = stkRoot.GetObjectFromPath(obj)
            con2 = con.QueryInterface(STKObjects.IAgConstellation)
            conObjs = ['/'.join(con2.Objects.Item(ii).Path.split('/')[-2:]) for ii in range(con2.Objects.Count)]
            for conObj in conObjs:
                if not conObj in listNodes:
                    listNodes.append(conObj)

        else:
            if not obj in listNodes:
                listNodes.append(obj)
    return listNodes


# Get positions for at a time instant
def getPosOverTime(stkRoot,times,nodes):
    time_NodePos = []
    for t in times.astype(str):
        nodeAndPos = []
        for node in nodes:
            cmd = 'Position */'+node+' "'+t+'"'
            result = stkRoot.ExecuteCommand(cmd)
            if node.split('/')[0] in ['Facility', 'Place', 'Target','AreaTarget']:
                xyzList = result.Item(0).split(' ')[3:6]
            else:
                xyzList = result.Item(0).split(' ')[6:9]
            pos = tuple([float(ii) for ii in xyzList])
            nodeAndPos.append((node,pos))
        time_NodePos.append((t,nodeAndPos))
    time_NodePos
    return time_NodePos


def getStationaryPos(stkRoot,chainName):
    nodes = GetNodesFromChain(chainName)
    t = 0
    nodePos = {}
    for node in nodes:
        cmd = 'Position */'+node+' "'+str(t)+'"'
        result = stkRoot.ExecuteCommand(cmd)
        if node.split('/')[0] in ['Facility', 'Place', 'Target','AreaTarget']:
            xyzList = result.Item(0).split(' ')[3:6]
            pos = tuple([float(ii) for ii in xyzList])
            nodePos[node] = pos
    return nodePos

def getStationaryPos2(stkRoot,strands):
    nodes = getNodesFromStrands(strands)
    t = 0
    nodePos = {}
    for node in nodes:
        cmd = 'Position */'+node+' "'+str(t)+'"'
        result = stkRoot.ExecuteCommand(cmd)
        if node.split('/')[0] in ['Facility', 'Place', 'Target','AreaTarget']:
            xyzList = result.Item(0).split(' ')[3:6]
            pos = tuple([float(ii) for ii in xyzList])
            nodePos[node] = pos
    return nodePos

def getPos(stkRoot,t,nodes,stationaryNodePos):
    nodePos = {}
    for node in nodes:
        if node.split('/')[0] in ['Facility', 'Place', 'Target','AreaTarget']:
            nodePos[node] = stationaryNodePos[node]
        else:
            cmd = 'Position */'+node+' "'+str(t)+'"'
            result = stkRoot.ExecuteCommand(cmd)
            xyzList = result.Item(0).split(' ')[6:9]
            pos = tuple([float(ii) for ii in xyzList])
            nodePos[node] = pos
    return nodePos



# Compute distance of edge and strand path
def computeEdgeDistance(edges,nodePos):
    edgesRanges = {edge : np.linalg.norm(np.subtract(nodePos[edge[0]],nodePos[edge[1]])) for edge in edges}
    return edgesRanges

def computeEdgeDistance2(edges,t,allEdgesOverTime):
    edgesRanges = {edge:allEdgesOverTime[edge][t] for edge in edges}
    return edgesRanges

# Merge results
# Merge strands into one dict and df
def mergerChainResults2(allChainResults):
    mergedTimesStrandsDistances = {}
    for t in times:
        tempDict = {}
        for ii in range(len(chainNames)):
            tempDict.update(allChainResults[ii][t])
        mergedTimesStrandsDistances.update({t:tempDict})
        
    times2 = []
    strands = []
    dists = []
    for t in times:
        strandDist = mergedTimesStrandsDistances[t]
        if len(strandDist.items()) == 0:
            times2.append(t)
            strands.append('')
            dists.append(np.nan)
        else:
            for strand,dist in strandDist.items():
                times2.append(t)
                strands.append(strand)
                dists.append(dist)
    df = pd.DataFrame([times2,strands,dists]).T
    df.columns = ['time','strand','distance']
    df = df.sort_values(['time','distance'])
    df
    return df

def computeAllEdgesOverTime(stkRoot,strands,start,stop,step):
    # Get edges
    edges = getEdgesFromStrands(strands)

    # Pull distance over time
    allEdgesOverTime = {}
    for edge in edges:
        obj = stkRoot.GetObjectFromPath('*/'+edge[0])
        vecDP = obj.DataProviders.Item('Vectors(ICRF)')
        vecGroup = vecDP.QueryInterface(STKObjects.IAgDataProviderGroup)
        vec = vecGroup.Group.Item(edge[1].split('/')[1])
        vec = vec.QueryInterface(STKObjects.IAgDataPrvTimeVar)
        elems = ['Time','Magnitude']
        result = vec.ExecElements(start,stop,step,elems)
        edgeDistOverTime = result.DataSets.ToArray()
        disOverTime = dict(edgeDistOverTime)
        strandDisOverTime = {edge:disOverTime}
        allEdgesOverTime.update(strandDisOverTime)
    return allEdgesOverTime

def computeMinStrandsDistances2(times,timeStrandsDistances,numStrands=1):
    df = pd.DataFrame()
    timeStrandsDistances = timeStrandsDistances.set_index('time')
    for t in times:
        df = df.append(timeStrandsDistances.loc[t][0:numStrands])
    df = df.reset_index()
    df['num hops'] = df['strand'].apply(lambda x: len(x)-2)
    df.loc[df['num hops'] < 0,'num hops'] = np.nan
    return df

def computeTimeStrandsDistances(strandsAtTimes,stationaryNodes):
    timeStrandsDistances = {strandsAtTime[0]: strandsAtTimeToEdges(strandsAtTime,stationaryNodePos)[0] for strandsAtTime in strandsAtTimes}
    return timeStrandsDistances


# strands to edges
def strandsAtTimeToEdges(strandsAtTime,stationaryNodePos):
    t = strandsAtTime[0]
        
    # Get set of nodes and their position
    nodes = [node for strandAtTime in strandsAtTime[1] for node in strandAtTime]
    uniqueNodes = list(set(nodes))
    nodePos = getPos(t,uniqueNodes,stationaryNodePos)
    
    # Get set of edges and their distances
    edges = []
    for strandAtTime in strandsAtTime[1]:
        for ii in range(len(strandAtTime)-1):
            edge = (strandAtTime[ii],strandAtTime[ii+1])
            edges.append(edge)
    uniqueEdges = list(set(edges))
    edgesDistances = computeEdgeDistance(uniqueEdges,nodePos)
    # Strands and their total distance
    strandsAtTimeDistance = {strandAtTime: computePathDistance(strandAtTime,edgesDistances) for strandAtTime in strandsAtTime[1]}

    return strandsAtTimeDistance,edgesDistances

def computeTimeStrandsDistances2(strandsAtTimes,allEdgesOverTime):
    timeStrandsDistances = {strandsAtTime[0]: strandsAtTimeToEdges2(strandsAtTime,allEdgesOverTime)[0] for strandsAtTime in strandsAtTimes}
    return timeStrandsDistances

def strandsAtTimeToEdges2(strandsAtTime,allEdgesOverTime):
    t = strandsAtTime[0]
    # Get set of edges and their distances
    edges = []
    for strandAtTime in strandsAtTime[1]:
        for ii in range(len(strandAtTime)-1):
            edge = (strandAtTime[ii],strandAtTime[ii+1])
            edges.append(edge)
    uniqueEdges = list(set(edges))
    edgesDistances = computeEdgeDistance2(uniqueEdges,t,allEdgesOverTime)
    # Strands and their total distance
    strandsAtTimeDistance = {strandAtTime: computePathDistance(strandAtTime,edgesDistances) for strandAtTime in strandsAtTime[1]}
    return strandsAtTimeDistance,edgesDistances


def minRangeIntervals2(timesMinStrandsDistances):
    strandList = list('-')
    starts = []
    for t,s,d in timesMinStrandsDistances:
        if s != strandList[-1]:
            strandList.append(s)
            starts.append(t)
    strandList = strandList[1:]  
    stops = starts.copy()
    stops.append(stop)
    stops = stops[1:]
    dfIntervals = pd.DataFrame(data=(starts,stops,strandList)).T    
    dfIntervals.columns = ['start','stop','strand']
    return dfIntervals

# Convert intervals into discrete time steps
def getStrandsAtTimesOld(strands,start,stop,step):
    times = np.arange(start,stop+step,step)
    strandsAtTimes = {}
    for t in times:
        strandsAtTime = []
        for strand in strands:
            if (t >=strand[1]) & (t <=strand[2]):
                strandsAtTime.append(strand[0])
        strandsAtTimes.update({t:strandsAtTime})
    return strandsAtTimes

def computeEdgeDistanceOld(t,edges,nodesTimesPos):
    edgesRanges = {edge : np.linalg.norm(np.subtract(nodesTimesPos[edge[0]][t],nodesTimesPos[edge[1]][t])) for edge in edges}
    return edgesRanges