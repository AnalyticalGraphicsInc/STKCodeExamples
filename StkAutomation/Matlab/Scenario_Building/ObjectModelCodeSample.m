%Establish the connection
%For an existing instance of STK
% app = actxGetRunningServer('STK12.Application');

%to create a new instance of STK
app = actxserver('STK12.Application');

app.UserControl = 1;
app.Visible = 1;

%get the root from the personality
%it has two... get the second, its the newer STK Object Model Interface as
%documented in the STK Help
root = app.Personality2;

%create new scenario
root.NewScenario('Test');

%create satellite and fac
root.ExecuteCommand('New / */Satellite Sat');
root.ExecuteCommand('New / */Facility Fac');

%propagate sat...
start = root.CurrentScenario.StartTime;
stop = root.CurrentScenario.StopTime;
root.ExecuteCommand(['SetState */Satellite/Sat Cartesian J4Perturbation "', start,'" "', stop,'" 60 J2000 "', start,'" -5465000.513055 4630000.194365 0.0 712.713627 841.292034 7377.687805']);

%set units to epoch seconds because this works the easiest in matlab
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EPSEC');

%HOW TO GET NORMAL DATA PROVIDERS - GENERIC REPORT TYPES
%get LLA data providers
LLADataPrv = root.CurrentScenario.Children.Item('Sat').DataProviders.Item('LLA State').Group.Item('Fixed');
LLAResult = LLADataPrv.Exec(0,1000,100);
LLAInterval = LLAResult.Intervals;
LLAInt = LLAInterval.Item(0);

%iterate through result interface to obtain info
%values come back in a cell array form
for i=1:(LLAInt.DataSets.Count - 1)
    ds = LLAInt.DataSets.Item(i-1);
    dsvalue = ds.GetValues;
    switch (ds.ElementName)
        
        case 'Time'
            timeArray = dsvalue;
        case 'Lat'
            latArray = dsvalue;
        case 'Lon'
            lonArray = dsvalue;
        case 'Alt MSL'
            altArray = dsvalue;
    end
end

%print
fprintf('   %s\t %s\t %s\t %s\n','Time (s)','Lat','Lon', 'Alt');

%create regular matrices to convert the cells for printing
Time = zeros(0);
Lat = zeros(0);
Lon = zeros(0);
Alt = zeros(0);

for i=1:length(latArray)
    
    %populate regular matrices then print out
    %converts from cell array to regular matrix/array
    Time(i) = timeArray{i};
    Lat(i) = latArray{i};
    Lon(i) = lonArray{i};
    Alt(i) = altArray{1};
    
    
    fprintf('%8.2f\t%6.3f\t%6.3f\t%6.3f\n',Time(i),Lat(i),Lon(i),Alt(i));
end


%create access
Satellite = root.CurrentScenario.Children.Item('Sat');
Facility = root.CurrentScenario.Children.Item('Fac');
access = Satellite.GetAccessToObject(Facility);
access.ComputeAccess;

%GET ACCESS DATA PROVIDERS HERE
%see above
AccInfo = access.DataProviders.Item('Access Data');
AccResult = AccInfo.Exec(0,86400);
AccIntervalList = AccResult.Intervals;

%iterate through each interval to get the data
for i=1:(AccIntervalList.Count)
    %grab the current interval - this example only has 1 item
    interval = AccIntervalList.Item(0);
    
    %loop through the current interval
    for j=1:(interval.DataSets.Count-1)
        
        %obtain the values from the data sets...
        dsA = interval.Datasets.Item(j-1);
        dsAvalue = dsA.GetValues;
        
        %switch on the name to get the data provider you want
        switch (dsA.ElementName)
            
            %remember - cell arrays come back
            case 'Start Time'
                startArray = dsAvalue;
            case 'Stop Time'
                stopArray = dsAvalue;
            case 'Duration'
                durArray = dsAvalue;
        end
    end
end

%print out the access times

%prepopulate generic arrays
start = zeros(0);
stop = zeros(0);
dur = zeros(0);

%header
fprintf('\n\n%s\n\n   %s\t%s\t%s\n','Access Data','Start','Stop','Duration');

for i=1:length(startArray)
    
    %populate regular matrices then print out
    start(i) = startArray{i};
    stop(i) = stopArray{i};
    dur(i) = durArray{i};
    
    fprintf('%8.3f\t%8.3f\t%8.3f\n',start(i),stop(i),dur(i));
end