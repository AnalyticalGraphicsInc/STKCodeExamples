% This code is used in conjunctino with 'ChainAccessTimes.ipynb', it uses
% the files written by the Python code to plot the data storage onboard a satellite as a function of time
%
% author: Kyle Schmauk
% Date: 8/12/2020


% Graphing Data Storage On-board
clc; clear;

%% Import Data Rates

%Enter your values here
DataPics = 4.529; %DataPics is the rate of data that you are collecting
DataDL = 10; %DataDL is the rate that you can downlink the data


%% Reads and Stores EOIRAccess
%Make sure your access files names match the files being opened and read
%Creates a matrix of the start time, end time, and duration of accesses in MATLAB as "datavaluesEOIR'

EOIR = fopen('EOIRAccess.txt','rt'); %EOIRAccess represents the EOIR images/data collecting times
tlineEOIR = fgetl(EOIR);
headersEOIR = strsplit(tlineEOIR, ',');     %a cell array of strings
datacellEOIR = textscan(EOIR, '%d8 %f %f %f ', 'Delimiter', ',', 'CollectOutput', 1);
fclose(EOIR);
accessNumEOIR = length(datacellEOIR{1});
datavaluesEOIR = datacellEOIR{2};

%% Reads and Stores CommAccess
%Creates a matrix of the start time, end time, and duration of accesses in MATLAB as "datavaluesCOMM'

COMM = fopen('CommAccess.txt','rt'); %CommAccess represents the Communication DownLink access times
tlineCOMM = fgetl(COMM);
headersCOMM = strsplit(tlineCOMM, ',');     %a cell array of strings
datacellCOMM = textscan(COMM, '%d8 %f %f %f ', 'Delimiter', ',', 'CollectOutput', 1);
fclose(COMM);
accessNumCOMM = length(datacellCOMM{1});
datavaluesCOMM = datacellCOMM{2};

%% Combining Overlapping Accesses
%Combines overlapping times of "datavaluesEOIR" and "datavaluesCOMM" as a
%new matrix: "valuesBOTH" with 3rd column as the combined data rate
%Also creates matrixes "valuesEOIR" and "valuesCOMM" that removes the
%overlaping times and 3rd column is the relevant data rate for each

valuesBOTH = [   ];
k = 1;

%datavaluesEOIR = [datavaluesEOIR(1:3,:);datavaluesEOIR(3,:);datavaluesEOIR(4:end,:)]

% Int x and y count the number of times EOIR and COMM is replaced,
% respectively
x = 0; y = 0;
% valuesEOIR and valuesCOMM are the updated/editted data
valuesEOIR = datavaluesEOIR(:,1:2);
valuesCOMM = datavaluesCOMM(:,1:2);

for i = 1:accessNumEOIR
    for j = 1:accessNumCOMM
        if (datavaluesCOMM(j,1) > datavaluesEOIR(i,1) && datavaluesCOMM(j,1) < datavaluesEOIR(i,2))
            
            valuesBOTH(k,1) = datavaluesCOMM(j,1);            
            %Must add the changes to the original data
            if (datavaluesCOMM(j,2) > datavaluesEOIR(i,2))
                valuesBOTH(k,2) = datavaluesEOIR(i,2);
                
                valuesEOIR((i+x),2) = datavaluesCOMM(j,1);
                valuesCOMM((j+y),1) = datavaluesEOIR(i,2);
            else
                valuesBOTH(k,2) = datavaluesCOMM(j,2);
                
                %Changes earlier access split
                valuesEOIR((i+x),2) = datavaluesCOMM(j,1);
                %Adds row of new later access split
                valuesEOIR = [valuesEOIR(1:(i+x),:);datavaluesCOMM(j,1),datavaluesEOIR(i,2);valuesEOIR((i+x+1):end,:)];
                %Removes middle access split
                valuesCOMM = [valuesCOMM(1:(j+y-1),:);valuesCOMM((j+y+1):end,:)];
                
                x = x + 1;
                y = y - 1;
            end            
            k = k + 1;            
        end
        
        if (datavaluesEOIR(i,1) > datavaluesCOMM(j,1) && datavaluesEOIR(i,1) < datavaluesCOMM(j,2))
            
            valuesBOTH(k,1) = datavaluesEOIR(i,1);
            %Must add the changes to the original data
            if (datavaluesEOIR(i,2) > datavaluesCOMM(j,2))
                valuesBOTH(k,2) = datavaluesCOMM(j,2);
                
                valuesEOIR((i+x),1) = datavaluesCOMM(j,2);
                valuesCOMM((j+y),2) = datavaluesEOIR(i,1);
            else
                valuesBOTH(k,2) = datavaluesEOIR(i,2);
                
                %Changes earlier access split
                valuesCOMM((j+y),2) = datavaluesEOIR(i,1);
                %Adds row of new later access split
                valuesCOMM = [valuesCOMM(1:(j+y),:);datavaluesEOIR(i,1),datavaluesCOMM(j,2);valuesCOMM((j+y+1):end,:)];
                %Removes middle access split
                valuesEOIR = [valuesEOIR(1:(i+x-1),:);valuesEOIR((i+x+1):end,:)];
                
                x = x - 1;
                y = y + 1;
            end
            
            k = k + 1;
            
        end
    end
end

valuesBOTH(:,3) = (DataPics-DataDL);
valuesEOIR(:,3) = DataPics;
valuesCOMM(:,3) = -DataDL;

values = sortrows([valuesBOTH(:,:); valuesEOIR(:,:); valuesCOMM(:,:)],1);
    
%% Calculated Data Stored over 1 Day
%Currently set to calculate data stored over 0.1 second step sizes over 1
%day.
%To extend or decrease analysis time, change the bounds of dataStored, t,
%and the plot to the desired time and step sizes.

value = 0;
duration = [ ];
dataStored = zeros(864001,1);
n = 1; %Where within the values matrix
z = 1; %Stepping with each time step

for t = 0:0.1:86400
    if (t >= values(n,1) && t <= values(n,2))
        dataStored(z) = dataStored(z-1) + values(n,3);
        if (values(n,2) < (t+0.1))
            n = n + 1;
        end
        
        if (dataStored(z) < 0)
            dataStored(z) = 0;
        end
    end
        
    z = z + 1;
end

plot((0:0.1:86400)/60,dataStored)
title('
xlabel('Time (min)')
ylabel('Data Stored (Mbits)')