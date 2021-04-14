# -*- coding: utf-8 -*-
"""
Created on Wed Jul  8 11:56:06 2020

@author: Jackson A Artis
"""

# Change working directory
import os

# Needs to be the directory holding the interval file
os.chdir(r'C:\temp');
#print(os.getcwd());



# Read in first interval list
# Be sure to change 'interval.int' to the name of your interval file
oldFile = open('interval.int', 'r');

# Read through and save every line of the original interval list
lines = oldFile.readlines();

i = 0;
beginningOffset = "     \t" 
lineEnding = "Show on Default Attributes\n"
lastInterval = ""

while lines[i-1] != 'Begin Intervals\n':
    #print(lines[i])
    i = i + 1
    
    
lineIndicies = []
newLines = []

# Be sure to set the scenario start time to whatever it is for your scenario
scenarioStart = '"27 Feb 2020 9:00:00.000" '


while lines[i + 1] != '     \t\n':
    firstTime = lines[i].split()
    secondTime = lines[i+1].split()
    if (lines[i-1] == 'Begin Intervals\n'):
        firstInterval = firstTime[0] + " " + firstTime[1] + " " + firstTime[2] + " " + firstTime[3] + " "
        startLine = beginningOffset + scenarioStart + firstInterval + lineEnding
        
    date = ""
    month = ""
    year = ""
    time = ""
    
    
    
    # Compare the end time of interval 1 and the start time of interval 2
    if (firstTime[4] != secondTime[0]):
        date = secondTime[0]
    else:
        date = firstTime[4]
        
    if (firstTime[5] != secondTime[1]):
        month = secondTime[1]
    else:
        month = firstTime[5]
        
    if (firstTime[6] != secondTime[2]):
        year = secondTime[2]
    else:
        year = firstTime[6]
        
    if (firstTime[7] != secondTime[3]):
        time = secondTime[3]
    else:
        time = firstTime[7]
    
    # Create the new intervals that turn on the default attributes
    newInterval = firstTime[4] + " " + firstTime[5] + " " + firstTime[6] + " " + firstTime[7] + " "
    
    secondInterval = date + " " + month + " " + year + " " + time + " "
    
    if (lines[i + 2] == '     \t\n'):
        lastInterval = secondTime[4] + " " + secondTime[5] + " " + secondTime[6] + " " + secondTime[7]
    
    if (newInterval != secondInterval):
        lineIndicies.append(i)
        newLine = beginningOffset +  newInterval + " " +  secondInterval + lineEnding
        newLines.append(newLine)
        #print(newLine)
    
    i = i + 1
    
oldFile.close();

#Re-open the file to append it

newFile = open('newIntervals.int', 'w+')

lineIndiciesCounter = 0
newLinesCounter = 0

# Make sure to change the scenario end time to whatever your actual scenario end time is 
scenarioEnd = '"1 Mar 2020 10:00:00.000" '

for x in range(len(lines)):
    newFile.write(lines[x])
    if (lines[x] == 'Begin Intervals\n'):
        newFile.write(startLine)
        
    if (x == lineIndicies[lineIndiciesCounter]):
        newFile.write(newLines[newLinesCounter])
        if (lineIndiciesCounter + 1 != len(lineIndicies)):
            lineIndiciesCounter = lineIndiciesCounter + 1
            newLinesCounter = newLinesCounter + 1
            
         
    if (x + 1 != len(lines) and x + 2 != len(lines) and lines[x + 2] ==  'END Intervals\n'):
            endLine = beginningOffset +  lastInterval + " " + scenarioEnd + lineEnding
            newFile.write(endLine)  
            
 
newFile.close() 