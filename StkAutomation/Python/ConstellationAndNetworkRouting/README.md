# Constellation and Network Routing

All of the notebooks attach to an open instance of STK.
The associated example scenarios are attached on the **Constellation and Network Routing** code example: https://www.agi.com/search?filter=content%3acodesample

**Terminology**
* Node = Object in STK: Satellites, Aircraft, Facilities, Sensors, Transmitters, etc.
* Edge = Access between two objects in STK, includes typical STK constraints
* Strand = The sequence of nodes and edges to complete access in a chain or between a starting and ending constellation. Path and Route are also sometimes used
 
**Types of Analysis Shown**
* Pulling intervals of access for nodes, edges and strands in a network into Python
* Summary statistics on network utilization and performance
* Find the "best" route through a network (fewest handoffs, shortest distance, least time latency)
* Supports multihop paths betweens constellations
* Identify important nodes in a network at an instant and over time
* See the impacts on network connectivity when nodes are removed
* Load data back into STK for visualization and for reports/graphs

## ConstellationAnalysisUsingPrebuiltChains.ipynb

Walks through types of analysis described above using prebuilt chains in STK
<img src="./chainPaths.jpg" alt="Drawing" style="width: 400px;"/>


## ConstellationAnalysisMultiDomainAndMultiHop.ipynb

Computes the "best" multihop path between a starting and ending constellations, utilizing different types of STK Objects. Investigates how the optimal path changes when nodes are removed
<img src="./MultiDomain.jpg" alt="Drawing" style="width: 600px;"/>


## ConstellationAnalysisTransRec.ipynb
Computes the "best" multihop path between a starting and ending constellations through a satellite constellation with transmitters and receivers. Investigates the effects of changing transmitter power and signal strength constraints
<img src="TransmitterAndReceiver.jpg" alt="Drawing" style="width: 600px;"/>
