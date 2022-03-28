function [output] = Matlab_TransmitterModel(input)
	% NOTE: the outputs that are returned
	%       MUST be in the same order as registered

	% NOTE: Please DO NOT change anything above or below the USER MODEL AREA.
	% 
	% This example shows static output parameters, but these parameters
      % may change value at each time step.

switch input.method
    
    case 'register'
 	% register output variables       
	
	% The frequency of the transmitter carrier is a double in Hz.
        Frequency= {'ArgumentName','Frequency',...
                     'Name','Frequency',...
                     'ArgumentType','Output'};

	% The final output power is a double in dBW.
        Power= {'ArgumentName','Power',...
                     'Name','Power',...
                     'ArgumentType','Output'};
					 
	% The gain of the radiating elements of the antenna is a double in dB or dBi.
        Gain= {'ArgumentName','Gain',...
                     'Name','Gain',...
                     'ArgumentType','Output'};
					 
	% The information bit rate is a double in bits per second.
        DataRate= {'ArgumentName','DataRate',...
                    'Name','DataRate',...
                    'ArgumentType','Output'};
					
	% The bandwidth of the RF spectrum is a double in Hz.
       Bandwidth= {'ArgumentName','Bandwidth',...
                    'Name','Bandwidth',...
                    'ArgumentType','Output'};
 
	% The type of modulation used by the transmitter is a String. This must be one of the STK registered modulation types. However,
	% users can add their own modulation types, and STK will register them (see online Help for the Comm module).
	   Modulation= {'ArgumentName','Modulation',...
                     'Name','Modulation',...
                     'ArgumentType','Output'};
					 
	% A collection of post-transmit losses, e.g. antenna dish coupling or radome loss is a double in dB.
        PostTransmitLoss= {'ArgumentName','PostTransmitLoss',...
                    'Name','PostTransmitLoss',...
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
	
	%	The polarization reference axis is an Integer that is used to align transmitter polarization to receiver polarization,	
	%	with 0, 1 and 2 representing the X, Y and Z axes, respectively.
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
    
    %  UseCDMASpreadGain is a flag (0 or 1) that specifies whether or not to use the bandwidth spreading gain is a Boolean.
		UseCDMASpreadGain= {'ArgumentName','UseCDMASpreadGain',...
                    'Name','UseCDMASpreadGain',...
                    'ArgumentType','Output'};
    
	%	The CDMA coding gain advantage is a double in dB.
        CDMAGain= {'ArgumentName','CDMAGain',...
                    'Name','CDMAGain',...
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
				  
	% The transmitter position in Central Body Fixed coordinates is a vector of doubles, of length 3, corresponding to the X, Y 
	% and Z values with the unit of meters.
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
   
        output = {Frequency, Power, Gain, DataRate, Bandwidth,...
                  Modulation, PostTransmitLoss,...
                  PolType, PolRefAxis, PolTiltAngle, PolAxialRatio,...
                  UseCDMASpreadGain, CDMAGain,...
                  date, CbName, EpochSec, XmtrPosCBF, XmtrAttitude, RcvrPosCBF, RcvrAttitude};
    
    case 'compute'
    
        computeData = input.methodData;

	% compute the Test Model : 
	% Example Model for testing only
	% Transmitter input & outpur parameter usage is shown
	% 

	% USER Transmitter MODEL AREA.
        
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
        output.Power= 35;
        output.Gain= 30;
        output.DataRate= 12.0e6;
        output.Bandwidth= 25.0e6;
        output.Modulation= 'BPSK';
        output.PostTransmitLoss= -2.3;
        output.PolType= 0;
        output.PolRefAxis= 0;
        output.PolTiltAngle= 0.0;
        output.PolAxialRatio= 1.0;
        output.UseCDMASpreadGain= 0;
        output.CDMAGain= 0.0;

	% END OF USER MODEL AREA

    otherwise
        output = [];
end

