function [output] = Matlab_CalcObject_Eval(input)

switch input.method
    
    case 'register'
        
        value = {  'ArgumentName','value',...
                    'Name','Value',...
                    'ArgumentType','Output'}
    
        inc = {   'ArgumentName','inc',...
                    'Name','Inclination',...
                    'ArgumentType','Input',...
                    'Type','CalcObject'};

        rightAsc = { 'ArgumentName','rightAsc',...
                'Name','RAAN',...
                'ArgumentType','Input',...
                'Type','CalcObject'};
    
        output = {value, inc, rightAsc};
    
    case 'compute'
    
        computeData = input.methodData;

        output.value = sin(computeData.inc)*sin(computeData.rightAsc);

    otherwise
        output = [];
end

