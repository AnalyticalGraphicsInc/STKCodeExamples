function satelliteUpdate(obj, event, fid, fid2, root)

dateString = datestr(event.Data.time, 'dd mmm yyyy HH:MM:SS.FFF');

[epsec, x_pos, y_pos, z_pos, x_vel, y_vel, z_vel] = get_posvel_data(fid);
[epsec, q1, q2, q3, q4] = get_attitude_data(fid2);

%We'll use the timer update as the scenario Epoch Seconds.  Convert the
%timer seconds into STK Local Gregorian
matlabEpSec = root.ConversionUtility.ConvertDate('LCLG', 'EpSec', dateString);

root.ExecuteCommand(['SetPosition */Satellite/Satellite1 ECI "' dateString '" ' x_pos ' ' y_pos ' ' z_pos ' ' x_vel ' ' y_vel ' ' z_vel]);
root.ExecuteCommand(['AddAttitude */Satellite/Satellite1 Quat "' dateString '" ' q1 ' ' q2 ' ' q3 ' ' q4]);
root.CurrentTime = str2double(matlabEpSec);

%output the time, quat, position to the matlab screen
disp(sprintf(['Time: ' dateString '\nPosition: ' x_pos ' ' y_pos ' ' z_pos ...
    '\nAttitude: ' q1 ' ' q2 ' ' q3 ' ' q4 '\n']));