uiapp = actxserver('STK12.Application');
root = uiapp.Personality2;
uiapp.visible = 1;    

%create a new scenario and specify the time
try
    root.CloseScenario();
    root.LoadScenario('C:\Users\dhonaker\Documents\STK 11\SimulinkAttitudeControl\SimulinkAttitudeControl.sc');        
catch
    root.LoadScenario('C:\Users\dhonaker\Documents\STK 11\SimulinkAttitudeControl\SimulinkAttitudeControl.sc'); 
end
   
%set the unit preferences
root.UnitPreferences.SetCurrentUnit('DateFormat', 'EpSec');
root.UnitPreferences.SetCurrentUnit('AngleUnit', 'rad');

%get the realtime sat
realSat = root.CurrentScenario.Children.Item('Simulink');

%get the sun pointing sat (desired attitude)
perfectPointingSat = root.CurrentScenario.Children.Item('PerfectPointing');

%sun VVLH
perfectBodyAxes = perfectPointingSat.Vgt.Axes.Item('Body');

realSat.SetAttitudeType('eAttitudeRealTime');
attRealtime = realSat.Attitude;
attRealtime.LookAheadMethod = 'eHold';
%attRealtime.DataReference.SetProfileType('eProfileFixedInAxes');
%profile = attRealtime.DataReference.Profile;
%profile.ReferenceAxes = ['Satellite/' realSat.InstanceName ' VVLH'];

stkParameters = cell(5,1);
stkParameters{1} = uiapp;
stkParameters{2} = root;
stkParameters{3} = realSat;
stkParameters{4} = perfectPointingSat;
stkParameters{5} = perfectBodyAxes;

%assign the structure we'll use later to the UserData
set_param(gcb,'UserData', stkParameters);  