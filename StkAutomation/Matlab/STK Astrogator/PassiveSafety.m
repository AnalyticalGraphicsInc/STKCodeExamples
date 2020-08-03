function [passiveCheck,minRangeP, badManeuverTimes] = PassiveSafety(root, SatName, TargetName, userTrip, userMinRange)
%Get initial handles
scenario = root.CurrentScenario;
sat = root.GetObjectFromPath(strcat('Satellite/',SatName));
maneuverDP = sat.DataProviders.Item('Astrogator Maneuver Ephemeris Block Final').Group.Item('Cartesian Elems').Exec(scenario.StartTime,scenario.StopTime,60);
maneuverData = maneuverDP.DataSets;
%Get min range of overall trajectory
satDP = sat.DataProviders.Item('RIC Coordinates');
satDP.PreData = strcat('Satellite/',TargetName);
satDP2 = satDP.Exec(scenario.StartTime,scenario.StopTime,60);
range = satDP2.DataSets.GetDataSetByName('Range').GetValues();
minRange = min(cell2mat(range));

%Get total number of maneuvers in MCS
summaryDP1 = sat.DataProviders.Item('Maneuver Summary');
summaryDP = summaryDP1.Exec(scenario.StartTime,scenario.StopTime);
maneuverNumbers = summaryDP.DataSets.GetDataSetByName('Maneuver Number').GetValues();
maxManeuverNum = cell2mat(maneuverNumbers(length(maneuverNumbers)));


mcount =1;
if maneuverData.Count~=0
    badManeuvers = 0;
    %Create or grab handle to PassiveCheck Satellite
    result = root.ExecuteCommand("DoesObjExist / */Satellite/PassiveCheck");
    if result.Item(0)=="0"
        passiveSat = scenario.Children.New('eSatellite','PassiveCheck');
    else
        passiveSat = root.GetObjectFromPath('Satellite/PassiveCheck');
    end
    passiveSat.SetPropagatorType('ePropagatorAstrogator');
    passiveDriver = passiveSat.Propagator;
    %Get handle to initial state and propagate segments in new sat
    is = passiveDriver.mainSequence.Item(0);
    prop = passiveDriver.mainSequence.Item(1);
    sc = prop.StoppingConditions.Item(0);
    sc.Properties.Trip = userTrip;
    %Loop through each maneuver end state and assign it to the PassiveCheck 
    %satellite and propagate
    for n=0:7:(maxManeuverNum*7-7)
        is.OrbitEpoch = char(maneuverData.Item(n).GetValues());
        is.Element.Vx = cell2mat(maneuverData.Item(n+1).GetValues());
        is.Element.Vy = cell2mat(maneuverData.Item(n+2).GetValues());
        is.Element.Vz = cell2mat(maneuverData.Item(n+3).GetValues());
        is.Element.X = cell2mat(maneuverData.Item(n+4).GetValues());
        is.Element.Y = cell2mat(maneuverData.Item(n+5).GetValues());
        is.Element.Z = cell2mat(maneuverData.Item(n+6).GetValues());
        passiveDriver.RunMCS();
        
        %Movie Record Steps (movie record on in GUI)
%         root.ExecuteCommand('Animate * Step Forward');
%         root.ExecuteCommand('Animate * Step Forward');
        
        %Pull minimum range data for run       
        psatDP = passiveSat.DataProviders.Item('RIC Coordinates');
        psatDP.PreData = strcat('Satellite/',TargetName);
        psatDP2 = psatDP.Exec(scenario.StartTime,scenario.StopTime,60);
        range = psatDP2.DataSets.GetDataSetByName('Range').GetValues();
        minRangeP(mcount) = min(cell2mat(range));
        %Determine if minimum range meets requirements       
        if minRangeP(mcount)<userMinRange
            badManeuvers = badManeuvers +1;
            passiveCheck(mcount) = -1;
            badManeuverTimes{badManeuvers} = maneuverData.Item(n).GetValues();
        else
            passiveCheck(mcount) = 1;
        end
        mcount = mcount + 1;
    end
%If there is no maneuvers then check the minimum range for the trajectory
elseif minRange < userMinRange %km
    passiveCheck(mcount) = -1;
    minRangeP(mcount) = minRange;
else
    passiveCheck(mcount) = 1;
    minRangeP(mcount) = minRange;
end

end