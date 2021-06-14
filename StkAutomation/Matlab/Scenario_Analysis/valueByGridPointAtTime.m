%% Script Overview
%
% Name: valueByGridPointAtTime.m
%
% Author:Andrew Arnold, Systems Engineer Analytical Graphics Inc
%
% Date Created: 5/21/2020
%
% Date Last Modified:5/26/2020
%
% Description: This script captures Instantaneous FOM Values by Grid point
%   for an STK Coverage Definition utilizing a Navigation Accuracy Type Figure of
%   Merit. 
% 
%   The user must first create an STK Scenario consisting of a Coverage
%   Definition and Figure of Merit of Type Naviation Accuracy. Before
%   running this script the user must first Compute Coverage Definition.
% 
%   IMPORTANT: The User must specify the name of the Coverage Definition
%   and Figure of Merit Objects flawlessly (no spelling errors or mistakes
%   with capitalising letters). The user must also specify the time step
%   for the generated FOM report. 
%   
% Inputs: 
% 
%   Cov_Def_Name - User specified name of Coverage Definition Object in STK
%   FOM_Name - User specified name of Coverage Definition Object in STK
%   Time_Step - Number of seconds between each time step
% 
% Outputs:
%   T1 - Table one showing Instantaneous FOM Value for each Lat/Lon point
%   at each time step
% 
%   T2 - Table one showing Percentile FOM value for each time step
% 
%   valueByGridPointAtTime - Exported Excel spreadsheet with Sheet 1
%   contatining T1 and Sheet 2 containing T2
% 
% Author Notes:
%   Incorrect spelling of Coverage Definition/FOM Name will result in
%   following error: 
%   
%   "Error using Interface.AGI_STK_Objects_11__IAgStkObjectRoot/GetObjectFromPath
%   Invoke Error, Dispatch Exception:
%   Source: AgStkObjects11.AgStkObjectRoot.1
%   Description: Invalid object path.
% 
%   Error in Value_by_Grid_Point_at_Time (line 63)
%   fom = root.GetObjectFromPath(str_cat);"

%% Script Operations

% clear, close all applications and variables
clear all; close all; clc;

% Prompt User for STK Object Names
Cov_Def_Name = input('What is the name of your Coverage Definition object? [Spelling,punctuation, capitalisation must be 100% correct]:','s');
newline;
newline;
FOM_Name = input('What is the name of your Figure of Merit object? [Spelling,punctuation, capitalisation must be 100% correct]:','s');
newline;
newline;

%Prompt User for TimeStep
Time_Step = input('What is your desired time step in seconeds? [only enter whole numbers (ex. 60)]:');
newline;
newline;
Percentile = input('What is your desired percentile value? [only enter whole numbers (ex. 90)]:');
newline;
newline;

%% Grab ahold of STK
% Concatenat
str_cat = strcat('CoverageDefinition/', Cov_Def_Name,'/FigureOfMerit/',FOM_Name);

% Get reference to running STK instance
uiApplication = actxGetRunningServer('STK11.application');

% Get our IAgStkObjectRoot interface
root = uiApplication.Personality2;
scenario = root.CurrentScenario;

%Set Epoch Time
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

%Time Step Calculations
Start_Time = scenario.StartTime;
Stop_Time = scenario.StopTime;
Time_Spacing = ((Stop_Time - Start_Time)/Time_Step)+1;
time = linspace(scenario.StartTime,scenario.StopTime,Time_Spacing);

%Grab Ahold of Object Model Root for Coverage Definition/Figure of Merit
fom = root.GetObjectFromPath(str_cat);
fomDP= fom.DataProviders.Item('Time Value By Point');

%Run For Loop to grab Latitude, Longitude, and Instantaneous FOM Values
for i=1:length(time)
   timeUTCG = root.ConversionUtility.ConvertDate("EpSec","UTCG", string(time(i)));
   fomDP.PreData = timeUTCG;
   result = fomDP.Exec();
   
   latitude = result.DataSets.GetDataSetByName('Latitude').GetValues();
   longitude = result.DataSets.GetDataSetByName('Longitude').GetValues();
   fomValue = result.DataSets.GetDataSetByName('FOM Value').GetValues();
   
   fomValue_mat(:,i) = fomValue;
   time_mat(1,i) = string(timeUTCG);
  
end

%Convert FOM Matrix to Numeric Array
FOM_Numeric = cell2mat(fomValue_mat);

%% Gather Percentile Data

%Determine Length of FOM Value Arrays
FOM_length_Vert = length(FOM_Numeric(:,1));
FOM_length_Horz = length(FOM_Numeric(1,:));
FOM_Int_Percentile = FOM_Numeric;

%For Loop to find Percentile value for each Time Stamp
for i = 1:FOM_length_Horz
   Time_0 = FOM_Int_Percentile(:,i);
   Time_0_sorted = sort(Time_0);
   time_perc = round(length(Time_0_sorted)*(Percentile/100));
   time_final_perc = Time_0_sorted(time_perc); 
   FOM_Int_Percentile(FOM_length_Vert + 1,i) = time_final_perc;
end

%Create an Array of the Percentile Value at each Time Step
FOM_Percentil_final = FOM_Int_Percentile(FOM_length_Vert + 1,:);

%% Post Processing Organization

%Package String Data (Names) and FOM Results into one Table
Final_String = ['Lat (Deg)', 'Lon (Deg)', time_mat];
Results_NoPerc_Cell = [latitude, longitude, fomValue_mat];
Results_NoPercentile = cell2mat(Results_NoPerc_Cell); 

%Create Tables
T1 = array2table(Results_NoPercentile,'VariableNames',Final_String);
T2 = array2table(FOM_Percentil_final,'VariableNames',time_mat);

%% Export to Excel
filename = 'valueByGridPointAtTime.xlsx';
writetable(T1,filename, 'Sheet',1)
writetable(T2,filename, 'Sheet',2)
