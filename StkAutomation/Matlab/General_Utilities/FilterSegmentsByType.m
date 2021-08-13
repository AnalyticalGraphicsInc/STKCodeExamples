%% FilterSegmentsByType
% Author: Austin Claybrook
% Organization: Analytical Graphics Inc.
% Date Created: 03/20/19
% Last Modified: 03/22/19 by Austin Claybrook
% Description: For an Astrogator satellite, grabs all segments of a
% specified type and returns a list of handles to the segments.

%% Inputs:
% segs: Handle to list of Astrogator segments [IAgVAMCSSegmentCollection]
% segType: Segment type of an Astrogator satellite, i.e. TargetSequence,Maneuver, etc. [string]
% segHandles: Column vector storing the list of handles to the segments [nx1 column vector]

%% Code
function [segHandles] = FilterSegmentsByType(segs,segType,segHandles)
    for i = 1:segs.Count
        seg = segs.Item(i-1);
        if strcmp(seg.Type,'eVASegmentTypeTargetSequence') || strcmp(seg.Type,'eVASegmentTypeSequence') ||strcmp(seg.Type,'eVASegmentTypeBackwardSequence')
            if strcmp(seg.Type,['eVASegmentType',segType])
                segHandles = [segHandles;seg];
            end
            [segHandles] = FilterSegmentsByType(seg.Segments,segType,segHandles);
        elseif strcmp(seg.Type,['eVASegmentType',segType])
            segHandles = [segHandles;seg];
        end
    end
end