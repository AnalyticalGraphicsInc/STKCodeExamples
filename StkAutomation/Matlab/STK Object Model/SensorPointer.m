clear;
clc;

app = actxGetRunningServer('STK12.application');
root = app.Personality2;

%Code that automates the pointing of sensors if you have a bunch of xmtr 
%and rcvr on sensors that you want to point to eachother

%These should be the pairs of comm objects you want to link together. Order
%is that transmitter is in the left column and receiver in the right. They
%need to be a child of a sensor. Make sure the comm object names are unique

%NOTE: This is only for comm links from one object to another. Does not
%account for sensors that have to point to multiple objects and need a
%pointing schedule setup
%NOTE: Reqiures recSearch.m

%Place the pairs of xmtrs and rcvrs as shown below.
commPairs = {'Transmitter1','Receiver1';
             'Transmitter2','Receiver2';       
            };

%Function to grab objects of the comm object types
[xmtrs] = filterObjectsByType_Recursive('eTransmitter', root.CurrentScenario);
[rcvrs] = filterObjectsByType_Recursive('eReceiver',root.CurrentScenario);


for i = 1:length(commPairs)
    
    for j = 1:length(xmtrs)
    
        if(strcmp(commPairs{i,1},xmtrs(j).InstanceName))

            for k = 1:length(rcvrs)
            
                if(strcmp(commPairs{i,2},rcvrs(k).InstanceName))
                    
                    %Grabs comm objects and parents
                    xmtr = xmtrs(j);
                    rcvr = rcvrs(k);               
                    
                    xmtrSen = xmtr.Parent;
                    rcvrSen = rcvr.Parent;
                    
                    xmtrSenObj = xmtrSen.Parent;
                    rcvrSenObj = rcvrSen.Parent;
                    
           
                    %Sets pointing to targeted
                    xmtrSen.SetPointingType('eSnPtTargeted');
                    xmtrSen.Pointing.Targets.RemoveAll; %Clears list
                    xmtrSen.Pointing.Targets.AddObject(rcvrSenObj);
                    
                    %Sets visuals to hide the sensor vis
                    xmtrSen.CommonTasks.SetPatternSimpleConic(.01,1);
                    xmtrSen.VO.ProjectionType = 'eProjectionNone';
                    xmtrSen.VO.PercentTranslucency = 100;
                    xmtrSen.VO.TranslucentLinesVisible = 1;
                    
                    %Changes sensor name to reflect object it's pointing to
                    xmtrSen.InstanceName = ['To' rcvrSenObj.InstanceName];
                    
                    %Sets pointing to targeted
                    rcvrSen.SetPointingType('eSnPtTargeted');
                    rcvrSen.Pointing.Targets.RemoveAll; %Clears list
                    rcvrSen.Pointing.Targets.AddObject(xmtrSenObj);
                    
                    %Sets visuals to hide the sensor vis
                    rcvrSen.CommonTasks.SetPatternSimpleConic(.01,1);
                    rcvrSen.VO.ProjectionType = 'eProjectionNone';
                    rcvrSen.VO.PercentTranslucency = 100;
                    rcvrSen.VO.TranslucentLinesVisible = 1;
                    
                    %Changes sensor name to reflect object it's pointing to
                    rcvrSen.InstanceName = ['To' xmtrSenObj.InstanceName];

                    
                end
  
            end

        end

    end

end

