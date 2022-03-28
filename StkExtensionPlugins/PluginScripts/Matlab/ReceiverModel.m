function [output] = Matlab_ReceiverModel(input)
	% NOTE: the outputs that are returned
	%       MUST be in the same order as registered

	% NOTE: Please DO NOT change anything above or below the USER MODEL AREA.
	% 
	% This example shows static output parameters, but these parameters
      % may change value at each time step.

switch input.method
    
    case 'register'
 	% register output variables       
        
	% The frequency of the receiver is a double in Hz.
		Frequency= {'ArgumentName','Frequency',...
                     'Name','Frequency',...
                     'ArgumentType','Output'};
    
	% The bandwidth of the RF spectrum is a double in Hz.
	   Bandwidth= {'ArgumentName','Bandwidth',...
                    'Name','Bandwidth',...
                    'ArgumentType','Output'};

	% The gain of the radiating elements of the antenna is a double in dB or dBi.
        Gain= {'ArgumentName','Gain',...
                     'Name','Gain',...
                     'ArgumentType','Output'};
    
	% A collection of pre-receive losses is a double in dB.
        PreReceiveLoss= {'ArgumentName','PreReceiveLoss',...
                    'Name','PreReceiveLoss',...
                    'ArgumentType','Output'};

	% A collection of pre-demodulation losses is a double in dB.
	   PreDemodLoss= {'ArgumentName','PreDemodLoss',...
                     'Name','PreDemodLoss',...
                     'ArgumentType','Output'};
    
	% UseRainModel is a flag (0 or 1) that specifies whether a rain model is to be used which is Boolean.
        UseRainModel= {'ArgumentName','UseRainModel',...
                    'Name','UseRainModel',...
                    'ArgumentType','Output'};
					
	% The percent outage value that can be tolerated by the link is a double.
        RainOutagePercent= {'ArgumentName','RainOutagePercent',...
                     'Name','RainOutagePercent',...
                     'ArgumentType','Output'};
    
	% The type of polarization is an Integer:
	% Value 	Polarization Type 		Required Parameter(s)
	%	0 		None 					None
	%	1 		Linear 					Reference Axis
	%	2		Right Hand Circular 	None
	% 	3		Left Hand Circular 		None
	%	4 		Vertical 				Reference Axis, Tilt Angle
	%	5 		Horizontal 				Reference Axis, Tilt Angle
	%	6 		Elliptical 				Reference Axis, Tilt Angle, Axial Ratio
	
        PolType= {'ArgumentName','PolType',...
                    'Name','PolType',...
                    'ArgumentType','Output'};

	%	The polarization reference axis is an Integer that is used to align transmitter polarization to receiver polarization, with 	
	%	0, 1 and 2 representing the X, Y and Z axes, respectively.       
	   PolRefAxis= {'ArgumentName','PolRefAxis',...
                     'Name','PolRefAxis',...
                     'ArgumentType','Output'};
    
	%  The polarization tilt angle measured from the reference axis is a double in degrees.
        PolTiltAngle= {'ArgumentName','PolTiltAngle',...
                    'Name','PolTiltAngle',...
                    'ArgumentType','Output'};

    %  The polarization axial ratio is a real.  
		PolAxialRatio= {'ArgumentName','PolAxialRatio',...
                     'Name','PolAxialRatio',...
                     'ArgumentType','Output'};
					 
	% The Cross Polarization Leakage is a double in dB ranging from -9999.9 dB to 0 dB where -9999.9 represents the ideal case
	% of no cross polarization leakage.  
		CrossPolLeakage = {'ArgumentName','CrossPolLeakage',...
                    'Name','CrossPolLeakage',...
                    'ArgumentType','Output'};
					
	% CalcReceiverNoiseTemp is a flag (0 or 1) that specifies whether the Receiver Noise Temperature is to be calculated or a 
	% constant which is Boolean. 0 indicates a constant system noise temperature and 1 indicates calculated system noise 
	% temperature.
        CalcReceiverNoiseTemp = {'ArgumentName','CalcReceiverNoiseTemp',...
                    'Name','CalcReceiverNoiseTemp',...
                    'ArgumentType','Output'};
					
	% The constant system noise temperature (when CalcReceiverNoiseTemp = 0) is a double in K.
        ConstantNoiseTemp = {'ArgumentName','ConstantNoiseTemp',...
                    'Name','ConstantNoiseTemp',...
                    'ArgumentType','Output'};
    
    % The receiver pre-amplifier noise figure is a double in dB.  
		ReceiverNoiseFigure= {'ArgumentName','ReceiverNoiseFigure',...
                    'Name','ReceiverNoiseFigure',...
                    'ArgumentType','Output'};

	% The cable loss value is a double in dB.
        CableLoss= {'ArgumentName','CableLoss',...
                     'Name','CableLoss',...
                     'ArgumentType','Output'};
					 
    % The temperature of the cable is a double in K.
        CableNoiseTemp= {'ArgumentName','CableNoiseTemp',...
                    'Name','CableNoiseTemp',...
                    'ArgumentType','Output'};
    
	%	The receiver antenna noise temperature is a double in K.
        AntennaNoiseTemp= {'ArgumentName','AntennaNoiseTemp',...
                    'Name','AntennaNoiseTemp',...
                    'ArgumentType','Output'};

 	% register input variables       
   
	% The current date and time is a String variable. 
		date = {'ArgumentName','DateUTC',...
                'Name','DateUTC',...
                'ArgumentType','Input'};
 
    % The scenario central body is a String variable. 
		CbName = {'ArgumentName','CbName',...
                  'Name','CbName',...
                  'ArgumentType','Input'};
				  
	% The scenario simulation epoch time is a double in seconds.
	   EpochSec = {'ArgumentName','EpochSec',...
                'Name','EpochSec',...
                'ArgumentType','Input'};

    % The transmitter position in Central Body Fixed coordinates is a vector of doubles, of length 3, corresponding to the X, Y and Z
	% values with the unit of meters.
		XmtrPosCBF = {'ArgumentName','XmtrPosCBF',...
               'Name','XmtrPosCBF',...
               'ArgumentType','Input'};

	% The attitude quaternion of the transmitter is a vector of doubles, of length 4.
        XmtrAttitude= {'ArgumentName','XmtrAttitude',...
               'Name','XmtrAttitude',...
               'ArgumentType','Input'};
    
	% The receiver position in Central Body Fixed coordinates is a vector of doubles, of length 3, corresponding to the X, Y and Z
	% values with the unit of meters.
        RcvrPosCBF= {'ArgumentName','RcvrPosCBF',...
               'Name','RcvrPosCBF',...
               'ArgumentType','Input'};
 
	% The attitude quaternion of the receiver is a vector of doubles, of length 4.
        RcvrAttitude= {'ArgumentName','RcvrAttitude',...
               'Name','RcvrAttitude',...
               'ArgumentType','Input'};
   
        output = {Frequency, Bandwidth, Gain, PreReceiveLoss, PreDemodLoss,...
                  UseRainModel, RainOutagePercent,...
                  PolType, PolRefAxis, PolTiltAngle, PolAxialRatio,...
				  CrossPolLeakage, CalcReceiverNoiseTemp, ConstantNoiseTemp,...
                  ReceiverNoiseFigure, CableLoss, CableNoiseTemp, AntennaNoiseTemp,...
                  date, CbName, EpochSec, XmtrPosCBF, XmtrAttitude, RcvrPosCBF, RcvrAttitude};
    
    case 'compute'
    
        computeData = input.methodData;

	% compute the Test Model : 
	% Example Model for testing only
	% Receiver input & output parameter usage is shown
	% 

	% USER Receiver MODEL AREA.
        

	  xmtrPos = [ 0 0 0 ];
	  rcvrPos = [ 0 0 0 ];
        xmAttQuat = [ 0 0 0 0 ];
        rcAttQuat = [ 0 0 0 0 ];
	  xmtrPos = computeData.XmtrPosCBF;
	  rcvrPos = computeData.RcvrPosCBF;
        xmAttQuat = computeData.XmtrAttitude;
        rcAttQuat = computeData.RcvrAttitude;

	  range = norm(xmtrPos  - rcvrPos);

        output.Frequency= 12.3e9;
        output.Bandwidth= 25.0e6;
        output.Gain= 30;
        output.PreReceiveLoss= -2.0;
        output.PreDemodLoss= 0.0;
        output.UseRainModel= 0;
        output.RainOutagePercent= 0.3;
        output.PolType= 0;
        output.PolRefAxis= 0;
        output.PolTiltAngle= 0.0;
        output.PolAxialRatio= 1.0;
		output.CrossPolLeakage= -60.0;
		output.CalcReceiverNoiseTemp = 0;
		output.ConstantNoiseTemp = 290;
        output.ReceiverNoiseFigure= 0.25;
        output.CableLoss= 0.5;
        output.CableNoiseTemp= 290;
        output.AntennaNoiseTemp= 50.0;

	% END OF USER MODEL AREA

    otherwise
        output = [];
end

