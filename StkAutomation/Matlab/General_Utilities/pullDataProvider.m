function [dataFull] = pullDataProvider(root,dataProvString,dataProvElem,times,grouping,predata,object)

%This function takes in the data provider parameters and outputs the
%desired data, skipping the setup that is usually needed. It automates the
%process without the user having to get into the semantics of how data
%providers work in object model. 

%It is also useful to have the report & graph manager open as if you were
%trying to create a custom report so you can see the possible data provider
%and proper subfolders to use as function inputs

%root is the STK root.

%dataProvString is the data provider from STK.

%times corresponds to the start, stop and step time (put in values
%accordingly). Pass in as a cell array.

%dataProvElem is the actual elements of data you are looking for. Pass in
%as a cell array. If you leave this as empty in the input (as a []), the
%function will pull ALL of the data elements in the data provider. They
%will not have the name of the data elements above them but they correspond
%to the order found in the report & graph manager in STK.

%grouping is the subfolder under the data provider type in STK. Not every
%data provider will have this so leave empty (as a []) in the inputs if 
%that is the case. Pass it in as a string

%predata is the input of predata in case your dataprovider needs predata.
%Leave this empty (as a []) in the inputs if no predata is needed.

%object is the STK object the data provider is on. Pass on the handle to the
%object

%--------SPECIFIC INSTRUCTIONS FOR DATA PROVIDERS THAT NEED PREDATA--------

%You can enter in predata one of two ways into the script. The first way is
%to manually know your predata string and place that into the predata input
%listed above. The second is to allow a UI window to open up in STK that
%allows you to select the predata as you run this function. The predata
%input method allows you to keep the code streamlined, while having the UI
%window open up makes it easier for the user to select the correct predata
%with less room for error and figuring out what predata is needed. If you 
%don't know if your data provider has predata, enable the option so the
%ui window will automatically popup even if your predata input is empty
%(as a [])


%To enable the noncoding UI option for predata, set the value of 
%UIPredata = 1 below. If you want it off, leave it as UIPredata = 0.


%----------------Example of variable inputs for the function---------------

%     root is your handle to Personality2
%     dataProvString = 'Axes Choose Axes';
%     dataProvElem = {'Time','q1','q2','q3','q4'};
%     times = {0,2400,60};
%     grouping = 'Body';
%     predata = [CentralBodies/Earth];
%     object = root.GetObjectFromPath('Satellite/Satellite1');
%     [outputData] = pullDataProvider(root,dataProvString,dataProvElem,times,grouping,predata,object);


%----------------------------------Code------------------------------------

    %Grabs this to convert units properly
    scenario = root.CurrentScenario;

    %Refer to the predata readme section
    
    UIPredata = 1;
    
    %Grabs universal data provider interfaces
    
    dataProviderValue = object.DataProviders.Item(dataProvString);
    dataProviderType = dataProviderValue.Type;

    
    %Determines if time is in UTCG or EpSec
    
    if strcmp(class(times{1}),'double') %Epsec are double inputs

        root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');  

    elseif strcmp(class(times{1}),'char') %UTCG is char inputs

        root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');  

    end

    %Determines if there is a subfolder on the dataprovider

    if dataProviderValue.IsGroup == 0

        objectDP = dataProviderValue;

    elseif dataProviderValue.IsGroup == 1

        try
            
            objectDP = dataProviderValue.Group.Item(grouping);
            
        catch
            
            error(['This data provider has a grouping input that needs to'...
                ' be assigned. Look at the report & graph manager in STK'...
                ' for the proper subfolder in the data provider that you are'...
                ' trying to extract data for.'])
            
        end
        
    end

    
    %Determines if there is predata included

    objectDP.AllowUI = UIPredata;
   
    if ~isempty(predata)
        
        if UIPredata == 0

            objectDP.PreData = predata;

        end 

    end
    
    disp(objectDP.PreData)

    %This is where things differ depending on report type

    switch (dataProviderType)

        case 'eDrTimeVar'

            dataProviderFinal = objectDP.Exec(times{1},times{2},times{3});

        case 'eDrIntvl'

           dataProviderFinal = objectDP.Exec(times{1},times{2});

        case 'eDrFixed'

           dataProviderFinal = objectDP.Exec;  

    end

    %Determines if you want all data elements or a specified list
    
    dataCell = [];
    
     if ~isempty(dataProvElem)

        %Checks if object and if it has more than one data interval. For
        %example, access data have more than one data set for it that
        %corresponds to each access interval
        
        if dataProviderFinal.Interval.Count > 1
            
            
            
            for i = 1:length(dataProvElem)

                for j = 0:dataProviderFinal.Interval.Count-1

                    data =  dataProviderFinal.Interval.Item(cast(j,'int32')).DataSets.GetDataSetByName(dataProvElem{i}).GetValues;
                    dataCell = [dataCell; data];      

                end

                dataElem{i} = dataCell;
                dataCell = [];
            end

        elseif dataProviderFinal.Interval.Count == 1

            for i = 1:length(dataProvElem)

                dataElem{i} = dataProviderFinal.DataSets.GetDataSetByName(dataProvElem{i}).GetValues;

            end
            
        else

                dataElem = {['No ' dataProvString ' Data Available']};
                dataFull = [];
                return
        end

     else
         
        %Checks if object and if it has more than one data interval. For
        %example, access data have more than one data set for it that
        %corresponds to each access interval
                
        if dataProviderFinal.Interval.Count > 1

                for j = 0:dataProviderFinal.Interval.Count-1

                    data = dataProviderFinal.Interval.Item(cast(j,'int32')).DataSets.ToArray();
                    dataCell = [dataCell;  data]; 

                end

                dataElem = dataCell;
                dataCell = [];
                
        elseif dataProviderFinal.Interval.Count == 1
            
                dataElem = dataProviderFinal.DataSets.ToArray();
                
        else

                dataElem = {['No ' dataProvString ' Data Available']};
                dataFull = [];
                return
               
        end

     end
     
     
    for i = 1:length(dataProvElem)

        dataColumn = [dataProvElem{i} ; dataElem{i}];
        dataFull(:,i) = dataColumn;

    end

     
    
end