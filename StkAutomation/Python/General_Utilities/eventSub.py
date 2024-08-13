from agi.stk12.stkengine import STKEngine
from agi.stk12.stkobjects import AgELogMsgType, AgELogMsgDispID, AgEAnimationActions, AgEAnimationDirections, IAgStkObjectPreDeleteEventArgs, IAgStkObjectChangedEventArgs, IAgPctCmpltEventArgs, IAgScenarioBeforeSaveEventArgs, IAgStkObjectRootEventHandler

def onAnimUpdate(epoch: float):
    """Animation Update Event"""
    print("Animation epoch: " + str(epoch))

def onScenarioNew(scenario: str):
    """New Scenario Event"""
    print("Scenario new: " + scenario)

def onScenarioLoad(scenario: str):
    """Scenario Load Event"""
    print("Scenario load: " + scenario)

def onScenarioClose():
    """Scenario Close Event"""
    print("Scenario closed")

def onScenarioSave(scenario_path: str):
    """Scenario Save Event"""
    print("Scenario saved to: " + scenario_path)

def onLogMessage(message: str, message_type: AgELogMsgType, error_code: int, filename: str, line_number: int, display_id: AgELogMsgDispID):
    """Log Message Created Event"""
    print("Log Message")
    print("\tMessage: " + message)
    print("\tMessage type: " + str(message_type))
    print("\tError code: " + str(error_code))
    print("\tFile name: " + filename)
    print("\tLine number: " + str(line_number))
    print("\tDisplay ID: " + str(display_id))

def onStkObjectAdded(sender):
    """Object Added Event"""
    print("Object added: " + sender)

def onStkObjectDeleted(sender):
    """Object Deleted Event"""
    print("Object deleted: " + sender)

def onStkObjectRenamed(sender, old_path: str, new_path: str):
    """Object Renamed Event"""
    print("Object renamed: " + sender)
    print("\tOld Path: " + old_path)
    print("\tNew Path: " + new_path)

def onAnimationPlayback(current_time: float, action: AgEAnimationActions, direction: AgEAnimationDirections):
    """Animation Playback Event"""
    print("Animation playback")
    print("\tCurrent time: " + str(current_time))
    print("\tAction: " + str(action))
    print("\tDirection " + str(direction))

def onAnimationRewind():
    """Animation Rewind Event"""
    print("Animation rewind")

def onAnimationPause(current_time: float):
    """Animation Paused Event"""
    print("Animation pause")
    print("\tCurrent time: " + str(current_time))

def onScenarioBeforeSave(args: IAgScenarioBeforeSaveEventArgs):
    """Scenario Before Save Event"""
    print("Before save")
    print("\tContinue Save: " + str(args.ContinueSave))
    print("\tPath: " + args.Path)
    print("\tSaveAs?: " + args.SaveAs)
    print("\tSDFSave?: " + args.SDFSave)
    print("\tVDFSave?: " + args.VDFSave)

def onAnimationStep(current_time: float):
    """Animation Step Event"""
    print("Animation step")
    print("\tCurrent time: " + str(current_time))

def onAnimationStepBack(current_time: float):
    """Animation Step Backward Event"""
    print("Animation step back")
    print("\tCurrent time: " + str(current_time))

def onAnimationSlower():
    """Animation Slower Event"""
    print("Animation slower")

def onAnimationFaster():
    """Animation Faster Event"""
    print("Animation faster")

def onPercentCompleteUpdate(args: IAgPctCmpltEventArgs):
    """Percent Complete Update Event"""
    print("Percent complete update")
    print("\tCan cancel?: " + str(args.CanCancel))
    print("\tCanceled: " + str(args.Canceled))
    print("\tMessage: " + args.Message)
    print("\tPercent completed: " + str(args.PercentCompleted))

def onPercentCompleteEnd():
    """Percent Complete End Event"""
    print("Percent complete end")

def onPercentCompleteBegin():
    """Percent Complete Begin Event"""
    print("Percent complete begin")

def onStkObjectChanged(args: IAgStkObjectChangedEventArgs):
    """Object Changed Event"""
    print("Object changed: " + args.Path)

def onScenarioBeforeClose():
    """Scenario Before Close Event"""
    print("Scenario before close")

def onStkObjectPreDelete(args: IAgStkObjectPreDeleteEventArgs):
    """Object PreDelete Event"""
    print("Object pre-delete")
    print("\tContinue: " + str(args.Continue))
    print("\tPath: " + args.Path)


# Example of utilizing the above event callbacks

# Attach to STK 12 Engine Application
app = STKEngine.StartApplication(noGraphics=False)
root = app.NewObjectRoot()

# Subscribe to all root events
stkObjectRootEvents = root.Subscribe()

# Define a callback function for each event
stkObjectRootEvents.OnAnimUpdate += onAnimUpdate
stkObjectRootEvents.OnScenarioNew += onScenarioNew
stkObjectRootEvents.OnScenarioLoad += onScenarioLoad
stkObjectRootEvents.OnScenarioClose += onScenarioClose
stkObjectRootEvents.OnScenarioSave += onScenarioSave
stkObjectRootEvents.OnLogMessage += onLogMessage
stkObjectRootEvents.OnStkObjectAdded += onStkObjectAdded
stkObjectRootEvents.OnStkObjectDeleted += onStkObjectDeleted
stkObjectRootEvents.OnStkObjectRenamed += onStkObjectRenamed
stkObjectRootEvents.OnAnimationPlayback += onAnimationPlayback
stkObjectRootEvents.OnAnimationRewind += onAnimationRewind
stkObjectRootEvents.OnAnimationPause += onAnimationPause
stkObjectRootEvents.OnScenarioBeforeSave += onScenarioBeforeSave
stkObjectRootEvents.OnAnimationStep += onAnimationStep
stkObjectRootEvents.OnAnimationStepBack += onAnimationStepBack
stkObjectRootEvents.OnAnimationSlower += onAnimationSlower
stkObjectRootEvents.OnAnimationFaster += onAnimationFaster
stkObjectRootEvents.OnPercentCompleteUpdate += onPercentCompleteUpdate
stkObjectRootEvents.OnPercentCompleteEnd += onPercentCompleteEnd
stkObjectRootEvents.OnPercentCompleteBegin += onPercentCompleteBegin
stkObjectRootEvents.OnStkObjectChanged += onStkObjectChanged
stkObjectRootEvents.OnScenarioBeforeClose += onScenarioBeforeClose
stkObjectRootEvents.OnStkObjectPreDelete += onStkObjectPreDelete

# Exectute Code via STK
root.NewScenario()

# Remove the callback function for each event
stkObjectRootEvents.OnAnimUpdate -= onAnimUpdate
stkObjectRootEvents.OnScenarioNew -= onScenarioNew
stkObjectRootEvents.OnScenarioLoad -= onScenarioLoad
stkObjectRootEvents.OnScenarioClose -= onScenarioClose
stkObjectRootEvents.OnScenarioSave -= onScenarioSave
stkObjectRootEvents.OnLogMessage -= onLogMessage
stkObjectRootEvents.OnStkObjectAdded -= onStkObjectAdded
stkObjectRootEvents.OnStkObjectDeleted -= onStkObjectDeleted
stkObjectRootEvents.OnStkObjectRenamed -= onStkObjectRenamed
stkObjectRootEvents.OnAnimationPlayback -= onAnimationPlayback
stkObjectRootEvents.OnAnimationRewind -= onAnimationRewind
stkObjectRootEvents.OnAnimationPause -= onAnimationPause
stkObjectRootEvents.OnScenarioBeforeSave -= onScenarioBeforeSave
stkObjectRootEvents.OnAnimationStep -= onAnimationStep
stkObjectRootEvents.OnAnimationStepBack -= onAnimationStepBack
stkObjectRootEvents.OnAnimationSlower -= onAnimationSlower
stkObjectRootEvents.OnAnimationFaster -= onAnimationFaster
stkObjectRootEvents.OnPercentCompleteUpdate -= onPercentCompleteUpdate
stkObjectRootEvents.OnPercentCompleteEnd -= onPercentCompleteEnd
stkObjectRootEvents.OnPercentCompleteBegin -= onPercentCompleteBegin
stkObjectRootEvents.OnStkObjectChanged -= onStkObjectChanged
stkObjectRootEvents.OnScenarioBeforeClose -= onScenarioBeforeClose
stkObjectRootEvents.OnStkObjectPreDelete -= onStkObjectPreDelete

# Unsubscribe from all root events
stkObjectRootEvents.Unsubscribe()

# Shutdown STK Engine
root.CloseScenario()
app.ShutDown()