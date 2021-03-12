function [objects,objectNames] = GrabObjects(root,objectClass)


    %Grab scenario off root
    scenario = root.CurrentScenario;

    %Pulls all the children objects
    allChildrenConnect = root.ExecuteCommand(['ShowNames * SubObjects Class ' objectClass]); %Different classes for different things

    %Path to scenario
    scenarioPath = ['/Scenario/' scenario.InstanceName '/'];

    %Removes scenario path from all of the object names
    allChildren = strrep(allChildrenConnect.Item(0),scenarioPath,'');

    %Separates the object strings into their own cell arrays
    allChildrenShort = strsplit(allChildren,' ');

    %Removes the first empty array that was created due to how the names were
    %organized before using strrep and strsplit from STK
    allChildrenShort(1)=[];

    %Pulls objects into object array
    for i = 1 : length(allChildrenShort)

        objects(i) = root.GetObjectFromPath(allChildrenShort{i});

    end

    objectNames = allChildrenShort; 

end


