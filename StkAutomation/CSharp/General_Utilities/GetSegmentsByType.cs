// Function to grab all instances of <segment type> Astrogator segments.
// Returns a list containing the handles to all <segment type> segments.
//
//     Inputs:
//                       segmentType - Desired segment type, member of AgEVASegmentType enumeration
//                         satellite - Parent satellite from which all <segment type> segments are derived, as IAgSatellite
//
//    Outputs:
//             List<IAgVAMCSSegment> - List containing all <segment type> segment handles, as IAgVAMCSSegment
//
//     Syntax: List<IAgVAMCSSegment> segmentList = GetSegmentsByType(AgEVASegmentType.segmentType, satellite);
//
//  Example 1: Return list of all <maneuver> segments associated with a satellite:
//	    	   IAgSatellite satellite - Handle to satellite
//
//             List<IAgVAMCSSegment> maneuverList = GetSegmentsByType(AgEVASegmentType.eVASegmentTypeManeuver, satellite);
//
//  Required subroutines:
//      - SegmentSearch (defined herein)
//
public static List<IAgVAMCSSegment> GetSegmentsByType(AgEVASegmentType segmentType, AgSatellite satellite)
{
	// Initialize segment list:
	var segmentList = new List<IAgVAMCSSegment>();

	// Grab satellite propagator, Main Sequence and Autosequence collection handles:
	IAgVADriverMCS propagator = satellite.Propagator as IAgVADriverMCS;
	IAgVAMCSSegmentCollection mainSequence = propagator.MainSequence as IAgVAMCSSegmentCollection;
	IAgVAAutomaticSequenceCollection autoSequences = propagator.AutoSequence as IAgVAAutomaticSequenceCollection;

	// Loop through segments:
	foreach (IAgVAMCSSegment mcsSegment in mainSequence)
	{
		// Perform recursive search on each segment:
		SegmentSearch(mcsSegment);
	}

	// Loop through Autosequences:
	if (autoSequences.Count != 0)
	{
		for (int i = 0; i < autoSequences.Count; i++)
		{
			// Get this sequence's segment list:
			IAgVAAutomaticSequence sequence = autoSequences[i] as IAgVAAutomaticSequence;
			IAgVAMCSSegmentCollection sequenceSegments = sequence.Sequence as IAgVAMCSSegmentCollection;

			// Loop through each of these segments:
			foreach (IAgVAMCSSegment sequenceSegment in sequenceSegments)
			{
				// Perform recursive search on each sequence segment:
				SegmentSearch(sequenceSegment);
			}
		}
	}

	// Return segment list:
	return segmentList;

	// ===========================================================================
	// Define recursive, segment-based search function:
	void SegmentSearch(IAgVAMCSSegment segment)  
	{
		// Check this segment:
		if (segment.Type == segmentType)
		{
			// Add to list:
			segments.Add(segment);
		}

		// If this segment is a sequence, search sub-segments:
		if (segment.Type == AgEVASegmentType.eVASegmentTypeSequence)
		{
			// Search through its sub-segments:
			foreach (IAgVAMCSSegment subSegment in ((IAgVAMCSSequence)segment).Segments)
			{
				SegmentSearch(subSegment);
			}
		}

		// If this segment is a backward sequence, search sub-segments:
		if (segment.Type == AgEVASegmentType.eVASegmentTypeBackwardSequence)
		{
			// Search through its segments:
			foreach (IAgVAMCSSegment subSegment in ((IAgVAMCSBackwardSequence)segment).Segments)
			{
				SegmentSearch(subSegment);
			}
		}

		// If this segment is a target sequence, search sub-segments:
		if (segment.Type == AgEVASegmentType.eVASegmentTypeTargetSequence)
		{
			// Search through its segments:
			foreach (IAgVAMCSSegment subSegment in ((IAgVAMCSTargetSequence)segment).Segments)
			{
				SegmentSearch(subSegment);
			}
		}
	}
	// ===========================================================================
}