# Description

Ephemeris Differencer is a simple mini-Application which takes two ephemeris files (STK ".e" or TwoLineElements ".tle") or manually entered Cartesian or Keplerian initial conditions. The differencer can analyze the following: - Cartesian error in Earth Inertial - Cartesian error in Earth Fixed - Radial-InTrack-CrossTrack Error - Apparent vs True displacement, accounting for light travel time - Units in Kilometers or Meters If the "Account for Light Travel Time" option is turned on, the difference will be expressed in terms of the ApparentDisplacement rather than the instantaneous TrueDisplacement. Keywords: ephemeris trajectory error difference comparison (Updated for 2010R9)

# Instructions

To run the application, open up the VisualStudio solution. You will need to obtain and import the following DLLs (which require an STK Components license in the same directory that the DLLs exist):

AGI.Foundation.Core
AGI.Foundation.Models
AGI.Foundation.Navigation
ZedGraph (url=http://sourceforge.net/projects/zedgraph/)

You can do this by right-clicking on "References" in the SolutionExplorer, hitting 'add', and browsing to find the DLLs.
After that, you should be able to run the application from VisualStudio. I have provided some example ephemeris files in the "files" folder.

# Download

If you simply want to utilize this utility you can find the download [here](https://agiweb.secure.force.com/code/articles/Custom_Solution/Ephemeris-Differencer).
