% This script will connect to a running instance of STK and generate
% persistent access lines over time. You must specify the from_objPath, the
% to_ObjPath, timestep, and option variables. With an option equal to 1, an
% MTO will be used to statically visualize the access lines between the two
% objects. With option equal to 2, primitives will be used to dynamically
% create persistent access lines throughout an access. Note that MTOs will
% take significantly longer to create than primitives. When the script has
% completed, you will receive a popup message saying 'Done.'

clear all, clc

%From Access Object
from_objPath = '*/Facility/Facility1';
%To Access Object
to_objPath = '*/Satellite/Satellite1';
%Access line gap in seconds
timestep = 5; 
%Set option equal to 1 to generate MTO or 2 for Primitives
option = 2;

%%
lineColor = 10; %Light Blue
uiapp = actxGetRunningServer('STK12.application');
root = uiapp.Personality2;
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
root.ExecuteCommand('SetUnits / EPSEC');
scen = root.CurrentScenario;
obj = root.GetObjectFromPath(from_objPath);
access = obj.GetAccess(to_objPath);
access.ComputeAccess;

elems = {'Time';'x';'y';'z'};
from_objDP = access.DataProviders.Item('From Position Velocity').Group.Item('Fixed').ExecElements(scen.StartTime, scen.StopTime, timestep, elems);
to_objDP = access.DataProviders.Item('To Position Velocity').Group.Item('Fixed').ExecElements(scen.StartTime, scen.StopTime, timestep, elems);

if option == 1
    mto = scen.Children.New('eMTO','AccessLines');
end

intCounter = 1;
root.ExecuteCommand('BatchGraphics * On');
for i=0:from_objDP.Intervals.Count-1
    %Whenever you pass an index to an array, you need to cast it to a long equivalent (int32)
    time = cell2mat(from_objDP.Intervals.Item(i).DataSets.Item(cast(0,'int32')).GetValues);
    x = cell2mat(from_objDP.Intervals.Item(i).DataSets.Item(cast(1,'int32')).GetValues);
    y = cell2mat(from_objDP.Intervals.Item(i).DataSets.Item(cast(2,'int32')).GetValues);
    z = cell2mat(from_objDP.Intervals.Item(i).DataSets.Item(cast(3,'int32')).GetValues);
    
    to_time = cell2mat(to_objDP.Intervals.Item(i).DataSets.Item(cast(0,'int32')).GetValues);
    to_x = cell2mat(to_objDP.Intervals.Item(i).DataSets.Item(cast(1,'int32')).GetValues);
    to_y = cell2mat(to_objDP.Intervals.Item(i).DataSets.Item(cast(2,'int32')).GetValues);
    to_z = cell2mat(to_objDP.Intervals.Item(i).DataSets.Item(cast(3,'int32')).GetValues);
    
    switch option
        case 1
            %% Create MTO to represent the Access lines (Graphics Only)
            for j=1:length(x)
                cart2LLA = root.ConversionUtility.ConvertPositionArray('eCartesian',{x(j),y(j),z(j)},'eGeodetic');
                to_cart2LLA = root.ConversionUtility.ConvertPositionArray('eCartesian',{to_x(j),to_y(j),to_z(j)},'eGeodetic');
                %mto.Tracks.AddTrack(intCounter,{time(j);to_time{(j)},{cart2LLA{1};to_cart2LLA{1}},{cart2LLA{2};to_cart2LLA{2}},{cart2LLA{3};to_cart2LLA{3}});
                track = mto.Tracks.Add(intCounter);
                track.Points.AddPoint(time(j),cart2LLA{1},cart2LLA{2},cart2LLA{3});
                track.Points.AddPoint(to_time(j),to_cart2LLA{1},to_cart2LLA{2},to_cart2LLA{3});
                intCounter = intCounter + 1;
            end
            
        case 2
            %% Creates Primitives to represent the Access lines (Graphics Only)
            for j=1:length(x)
                root.ExecuteCommand(['VO * Primitive Add ID ' num2str(intCounter) ' Type Line Show On CentralBody Earth Color ' num2str(lineColor) ' LineWidth 2 IntervalType UseIntervals AddIntervals 1 "' num2str(time(j)) '" "' num2str(time(length(time))) '"']);%
                root.ExecuteCommand(['VO * Primitive Modify ID ' num2str(intCounter) ' Type Line ExtendPoints 1 CBF ' num2str(x(j)*1000) ' ' num2str(y(j)*1000) ' ' num2str(z(j)*1000)]);
                root.ExecuteCommand(['VO * Primitive Modify ID ' num2str(intCounter) ' Type Line ExtendPoints 1 CBF ' num2str(to_x(j)*1000) ' ' num2str(to_y(j)*1000) ' ' num2str(to_z(j)*1000)]);
                intCounter = intCounter + 1;
            end
            
        otherwise
    end
end
root.ExecuteCommand('BatchGraphics * Off');
msgbox('Done');