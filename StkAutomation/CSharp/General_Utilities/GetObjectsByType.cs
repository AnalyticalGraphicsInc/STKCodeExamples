// Function to grab all instances of <object type> STK objects. Returns a list containing the
// handles to all <object type> objects. 
//
//  Inputs:
//                     objectType - Desired object type, member of AgESTKObjectType enumeration
//                   parentObject - Parent object from which all <object type> objects are derived, as IAgStkObject
//
//  Outputs:
//             List<IAgStkObject> - List containing all <object type> object handles, as IAgStkObject
//
//  Syntax:    List<IAgStkObject> objectList = GetObjectsByType(AgESTKObjectType.objectType, parentObject);
//
//  Example 1: Return list of all <facility> handles in current scenario:
//			   IAgStkObject scenario - Handle to current scenario
//
//             List<IAgStkObject> satelliteList = GetObjectsByType(AgESTKObjectType.eSatellite, scenario);
//
//  Example 2: Return list of all <sensors> attached to a satellite:
//			   IAgStkObject satellite - Handle to satellite
//
//             List<IAgStkObject> sensorList = GetObjectsByType(AgESTKObjectType.eSensor, satellite);
//
//  Required subroutines:
//      - None
//
public static List<IAgStkObject> GetObjectsByType(AgESTKObjectType objectType, IAgStkObject parentObject)
{
	// Initialize list:
	var objectList = new List<IAgStkObject>();

	// Loop through children of the parent object:
	foreach (IAgStkObject child in parentObject.Children)
	{
		// Check the child type:
		if (child.ClassType == objectType)
		{
			// Add child to the list:
			objectList.Add(child);
		}

		// Search grandchildren:
		objectList.AddRange(GetObjectsByType(objectType, child));
	}

	// Return object list:
	return objectList;
}