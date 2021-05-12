%% INFORMATION 
% Written By: Alexander Ridgeway
% %Date:       15 Jan 2018
% Code Sample: Creates a simple Vehicle Mission Sequence Used by STK/SOLIS 
% Mission Sequences. This sequences runs an On/Off sequence at AOS/LOS.
 
% Requirements: 
% have STK 11+ running (change line 31 to "11" if using 11)
% have a Scenario loaded 
% have a propagated satellite 
% have SOLIS open for this satellite (to create sequence folder directories)
% have a ground station of any type, facility, target or place
% can also have Constraints to the objects to provide realistic Contacts
% 
% Explanation: This sample is used to generate a simple sequence for
% communicating with a satellite. The Satellite and Uplink Site
% (place/target/facility) will have access to eachother. At AOS (Acquisition
% of Signal), the satellite will execute a Communications_ON sequence which 
% readies the vehicle for communicating with the satellite. At LOS (Loss of
% Signal) the satellite will execute a Communications_OFF sequence which
% stops the vehicle from communicating. These additional sequences can
% include any commands to the vehicle and are left as .seq files which
% allow for anything to be added to them.

%% Initialize: Get STK and Start Accessing the Root
clear;
clc;
disp('Beginning: Uplink Sequence Generation')
warning('off')

uiapp = actxGetRunningServer('STK12.application');
root = uiapp.Personality2;
uiapp.visible = 1;
scenario = root.CurrentScenario;
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

%% Enter info by Prompts: 
%Enter Information about the scenario objects using a Button and Input GUI

%Prompt 1: Askes for the OBJECT TYPE 
ButtonName = questdlg('What type of object is the Uplink Site?','Type Question', 'Facility', 'Place', 'Target','');
   switch ButtonName
     case 'Facility'
        type_plc = 'Facility/';
     case 'Place'
        type_plc = 'Place/';
     case 'Target'
        type_plc = 'Target/';
   end % switch

   %Prompt 2: Asks for the NAMES of OBJECTs
prompt={'Enter the name of the Satellite:','Enter the name of the Uplink Site:'};
   name='';
   numlines=1;
   defaultanswer={'Satellite1','Place1'};
   options.Resize='on';
   options.WindowStyle='normal';
   options.Interpreter='tex';
   answer=inputdlg(prompt,name,numlines,defaultanswer,options);
   name_sat_sm = cell2mat(answer(1)); 
   name_sat = strcat('Satellite/',name_sat_sm);
   name_plc_sm = cell2mat(answer(2));
   name_plc = strcat(type_plc,name_plc_sm);

   disp(strcat('Satellite :',name_sat))
   disp(strcat('UplinkSite:',name_plc))
   
%% Accessing satellite/place data: 
%Using the info from prompt pull the data
%Get Satellite and Place Object and will restart IF the object doesnt exist
try
    sat = root.GetObjectFromPath(name_sat);
catch
    msg_error = strcat('ERROR: Bad Satellite Name\n',...
        'Please check scenario for\n',...
        name_sat);
    uiwait(msgbox(compose(msg_error)));
    Solis_Create_Sequence_fromAccess;
end
try 
    plc = root.GetObjectFromPath(name_plc);
catch
    msg_error = strcat('ERROR: Bad Site Name\n',...
        'Please check scenario for\n',...
        name_plc);
    uiwait(msgbox(compose(msg_error)));
    Solis_Create_Sequence_fromAccess;

end

%% Compute Access and Get Times: 
%Compute Up/Downlink times for Sat to Ground,
acc = sat.GetAccessToObject(plc);
acc.ComputeAccess;
invCollection = acc.ComputedAccessIntervalTimes;
try 
    cmdIntervals = invCollection.ToArray(0,-1);
    numContacts = length(cmdIntervals);
    disp(strcat('# Contacts:',int2str(numContacts)))
catch
    msg_error = strcat('ERROR: No Access Found\n',...
        'Stopping the Script');
    uiwait(msgbox(compose(msg_error)));
    return    
end

%% Create Sequence File:
%Create the seq file in the STK Directory \ SEQS where the sequences for
%the scenario will go

file3 = sat.ObjectFiles;
file2 = cell2mat(file3(1));
file1 = file2(1:end-14);
file0 = strcat(file1,'\', name_sat_sm , '\seqs\Solis_Seq.seq');

fileID = fopen(file0,'w');
fprintf(fileID,'\n');

fprintf(fileID,'PROC Comm_system\n');
fprintf(fileID,'DATE SEQ_START_TIME\n');
fprintf(fileID,'BEGIN\n');
fprintf(fileID,'SEQ_START_TIME = NOW()\n\n');

%Create the sequence for every contact to the ground site!
%every contact STARTS with a Sequence called Communications_ON.seq
%every contact ENDS   with a Sequence called Communications_OFF.seq
%Place *.seq in folder {STKUserDir}\{Scenario}\{SatelliteName}\seqs
for i=1:numContacts
    
    fprintf(fileID,'// Contact Number: %1.0f\n',i); 
    fprintf(fileID,'// Comm System:   ON\n'); 
    fprintf(fileID,'WAIT_UNTIL (SEQ_START_TIME + ');      
    fprintf(fileID,'%10.1f',cell2mat(cmdIntervals(i,1)));
    fprintf(fileID,')\n');
    fprintf(fileID,'CMD "FJ_START_REL" (0, "Communications_ON","")\n');
    
    fprintf(fileID,'// Comm System:   OFF\n'); 
    fprintf(fileID,'WAIT_UNTIL (SEQ_START_TIME + ');      
    fprintf(fileID,'%10.1f',cell2mat(cmdIntervals(i,2)));
    fprintf(fileID,')\n');
    fprintf(fileID,'CMD "FJ_START_REL" (0, "Communications_OFF","")\n\n');
end
fclose(fileID);

disp('Complete: Uplink Sequence Generation')
disp(file0)