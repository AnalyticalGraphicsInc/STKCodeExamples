# --------------------------------------------------------------------
# demonstrates how to grab an array of all objects of a certain type
# with an optional string filter
#
# Author: AClaybrook
# Email: support@agi.com
# --------------------------------------------------------------------
from comtypes.client import GetActiveObject

def filter_objects_by_type(objectType, name=''):
    """Returns a list of paths for the specified object type optionally filtered by a name string"""

	# Only run if not already connected to STK
	app = GetActiveObject('STK11.Application')
    root = app.Personality2
    xml = root.AllInstanceNamesToXML()

    objs = xml.split('path=')
    objs = objs[1:]  # remove first string of '<'

    objPaths = []
    for i in range(len(objs)):
        obji = objs[i].split('"')
        objiPath = obji[1]  # the 2nd string is the file path
        objiSplit = objiPath.split('/')
        objiClass = objiSplit[-2]
        objiName = objiSplit[-1]
        if objiClass.lower() == objectType.lower():
            if name.lower() in objiName.lower():
                objPaths.append(objiPath)
    return objPaths