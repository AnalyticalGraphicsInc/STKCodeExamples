function [output] = Matlab_ForceModel_Eval(input)

switch input.method
    
    case 'register'
        
        status = {  'ArgumentName','status',...
                    'Name','Status',...
                    'ArgumentType','Output'}
    
        accel = {   'ArgumentName','accel',...
                    'Name','Acceleration',...
                    'ArgumentType','Output',...
                    'RefName','CbiLVLH'};

        vel = { 'ArgumentName','Vel',...
                'Name','Velocity',...
                'ArgumentType','Input',...
                'RefName','Inertial'};
   
        date = {    'ArgumentName','Date',...
                    'Name','DateUTC',...
                    'ArgumentType','Input'};
    
        output = {status, accel, vel, date};
    
    case 'compute'
    
        computeData = input.methodData;

		% don't really need Date
        
        velocity = computeData.Vel;
        factor = 0.000001;
        cbiSpeed = norm(velocity);

        output.status = 'Still Okay';
        output.accel = [0.0  factor*cbiSpeed  0.0]';
        
    otherwise
        output = [];
end

