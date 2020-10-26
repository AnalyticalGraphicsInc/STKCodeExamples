function [output] = DetumbleTorqueDiagnosis(input)

switch input.method
    
    case 'compute'
        computeData = input.methodData;
        
		% Output Control Torque in form expected by Attitude Simulator
        MagField = computeData.MagFieldIGRF(1:3);
        MagFieldDot = computeData.MagFieldIGRF(4:6);
        CrossProduct = cross(MagFieldDot, MagField);
        output.Torque = -4000 * CrossProduct;
        
    case 'register'
        
        param1 = {  'ArgumentName','time',...
            'Name','Epoch',...
            'ArgumentType','Input'};
        
        param2 = {  'ArgumentName','MagFieldIGRF',...
            'Type','Vector',...
            'Name','MagField(IGRF)',...
            'RefType', 'Attitude',... 
            'Derivative', 'Yes',...
            'ArgumentType','Input'};
                   
        param3 = {  'ArgumentName','Torque',...
            'Type','Parameter',...
            'Name','Torque',...
            'BasicType','Vector',...
            'ArgumentType','Output'};
                
        output = {param1, param2, param3 }; 
        
    otherwise
        output = [];
end
