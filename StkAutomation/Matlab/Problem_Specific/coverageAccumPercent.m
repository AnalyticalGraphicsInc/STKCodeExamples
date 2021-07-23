%Enter in a Percent Value for the accumulation
percentVal = 90;
%Percent tolerance to set bounds to find the desired percent value
percentTol = 1;

%Results step
timestep = 3600*24; %1 day step in seconds
%Output in hours if true / minutes if false
timeHr = true;
%Output in UTCG if true / EpSec if false
dateUTCG = true;
%% Create a new instance of STK
uiapp = actxserver('STK12.application');
root = uiapp.Personality2;
%Hide STK and do the analysis
uiapp.Visible = 1;

root.NewScenario('CoverageAccumPercent');
scen = root.CurrentScenario;
root.UnitPreferences.SetCurrentUnit('DateFormat','UTCG');
scen.SetTimePeriod('7 Oct 2019 16:00:00.000','6 Nov 2019 16:00:00.000');
%Set units to Epoch seconds for the correct output file units
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
%% Import US shapefile and remove all but the Continental US
root.ExecuteCommand('GIS * Import "C:\Program Files\AGI\STK 12\Data\Shapefiles\Countries\United_States_of_America\United_States_of_America.shp" AreaTarget');

ATs =scen.Children.GetElements('eAreaTarget');
atCount = ATs.Count;
for i = 1:(atCount-1)
    currAT = ATs.Item(int32(i));
    currAT.Unload;
end
%% Create Iss Satellite and Sensor
sat = scen.Children.New('eSatellite','Iss');
sat.SetPropagatorType('ePropagatorSGP4');
prop = sat.Propagator;
prop.CommonTasks.AddSegsFromOnlineSource('25544');
prop.AutoUpdateEnabled = true;
prop.Propagate;
res = sat.Graphics.Resolution;
res.Orbit = 60;

sen = sat.Children.New('eSensor','Sensor');
sen.Pattern.ConeAngle = 20;

%% Create the Coverage
Cov = scen.Children.New('eCoverageDefinition', 'Coverage');
Cov.Grid.BoundsType = 'eBoundsCustomRegions';
bounds = Cov.Grid.Bounds;
bounds.AreaTargets.Add('AreaTarget/United_States_of_America')

%Set the Sensor as the Asset
Cov.AssetList.Add('Satellite/Iss/Sensor/Sensor');

%Define the Grid Resolution then turn off Show Points
CovGrid = Cov.Grid;
Res = CovGrid.Resolution;
Res.LatLon = 1;
Cov.Graphics.Static.IsPointsVisible = false;

%Select the United States region in grid inspector
inspect = Cov.GridInspector;
inspect.SelectRegion('United_States_of_America');
%% Loop parameters
lowerBound = percentVal - percentTol;
upperBound = percentVal + percentTol;
counter = 0;

%Progress Bar
n = (scen.StopTime-scen.StartTime)/timestep; %Find number of loop iterations
h = waitbar(0,'Waiting ...');
%% Loop through the scenario analysis interval and find the time it takes to
%reach the the percent value of the coverage region defined at the top of this script 
for i = scen.StartTime:timestep:scen.StopTime
    start = scen.StartTime + timestep*counter;
    stop = scen.StopTime;
    
    Cov.Interval.AnalysisInterval.SetStartAndStopTimes(start,stop);
    Cov.ComputeAccesses;
    
    covValDP = Cov.DataProviders.Item('Selected Region Coverage').Exec(start,stop,60);  
    t = cell2mat(covValDP.DataSets.GetDataSetByName('Time').GetValues);
    accum = cell2mat(covValDP.DataSets.GetDataSetByName('Percent Accum Coverage').GetValues);

    array = [t accum];

    [row,col] = find(lowerBound < array & array < upperBound);
    
    %***Data Output***
    %Outputs the time step and the time it takes the grid to reach the %
    %value specified. If the time step doesn't reach the % then it outputs
    %a 0 value
    if (numel(row)==0)
        if(dateUTCG)
            timeToPercent{counter+1,1} = root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(start));
        else
            timeToPercent{counter+1,1} = start;
        end
        timeToPercent{counter+1,2} = 0;
    else
        %Converts output data based on the flags set at the top
        if(dateUTCG)
            timeToPercent{counter+1,1} = root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(start));
        else
            timeToPercent{counter+1,1} = start;
        end
        
        if(timeHr)
            timeToPercent{counter+1,2} = (t(row(1)) - t(1))/3600;
        else
            timeToPercent{counter+1,2} = t(row(1)) - t(1);
        end
    end
    counter = counter + 1;
    waitbar(counter/n);
end
%% Create the table of data
f = figure;

if(dateUTCG)
    colnames{1} = 'Time (UTCG)';
else
    colnames{1} = 'Time (EpSec)';
end

if(timeHr)
    colnames{2} = ['Time to Reach ' num2str(percentVal) '% Accum (hr)'];
else
    colnames{2} = ['Time to Reach ' num2str(percentVal) '% Accum (sec)'];
end

if(dateUTCG)
    columnformat = {'char', 'numeric'};
else
    columnformat = {'numeric', 'numeric'};
end

close(h);

columnformat = {'numeric', 'numeric'};
t = uitable(f, 'Data', timeToPercent, 'ColumnName', colnames, ...
            'ColumnFormat', columnformat,...           
            'Position', [10 10 500 400]);
%Make STK visible again        
uiapp.Visible = 1;