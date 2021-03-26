This script is used to select the best object for a certain facility to point at based on min or max elevation angle or range. The user must create an STK 12 scenario, add a facility/place or any other object to calculate the access "from", then edit the script inputs and run the script. The inputs are below:

fromObjectType - type of object to compute access from
fromObjectName - name of object
toObjectTypes - type of object to compute access to, it can be multiple types
optionalToObjectName - optionally, only use objects that contain this string in their name
fileName - the filename you want the data to be exported to (without the extension)
conditionMetric - the conditional metric you want to extract (Range and
	Elevation are the only two metrics for now
timeStep - this is how often the data is sampled from STK. By default it
	is at 1 second which does take some time to run, so change with caution
extrema - this is what determines if you're looking for minimum or maximum

The output of this script will be an interval file, or an .int file that contains the targeted pointing intervals for the optimal pointing schedule based on the optimization metric chosen. This .int file can then be used for targeted pointing on a sensor mounted on the "from" access object. 