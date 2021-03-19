This is just a quick README for further information on the two files:

MatlabSTKObjectModel.m

- Creates an instance of STK via COM - actxserver command in MATLAB
- Attaches the event listener to the root object. It is set so everything can be seen (more on the actual event listener in a sec)
- Shows how to use the STK Object Model to do a few things and uses Connect Commands via ExecuteCommand method for the rest
- Illustrates how to use access data providers and regular data providers in MATLAB

RootEvents.m

- Simple event listening M-file that prints to the screen the event that is occurring
- Parsed on the last cell of the cell array that is the event log
- Could easily add to this; it is a means to grab the rest of the data and do something with it

