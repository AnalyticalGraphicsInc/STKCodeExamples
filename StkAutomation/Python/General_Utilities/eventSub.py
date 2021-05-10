# --------------------------------------------------------------------
# demonstrates how to subscribe to STKObjectRoot Events using comtypes
#
# Author: jpthompson212
# Email: support@agi.com
# --------------------------------------------------------------------
from comtypes.client import CreateObject, GetActiveObject, GetEvents, ShowEvents, PumpEvents
from comtypes.gen import STKObjects

class EventSink(object):
    """Class to sink to Events in STK"""
    def IAgStkObjectRootEvents_OnAnimUpdate(self, this, epoch):
        """Animation Update Event"""
        print("Animation epoch: " + str(epoch))

    def IAgStkObjectRootEvents_OnScenarioNew(self, this, scenario):
        """New Scenario Event"""
        print("Scenario new: " + scenario)

    def IAgStkObjectRootEvents_OnScenarioLoad(self, this, scenario):
        """Scenario Load Event"""
        print("Scenario load: " + scenario)

    def IAgStkObjectRootEvents_OnScenarioClose(self, this):
        """Scenario Close Event"""
        print("Scenario closed")

    def IAgStkObjectRootEvents_OnScenarioSave(self, this, scenarioPath):
        """Scenario Save Event"""
        print("Scenario saved to: " + scenarioPath)

    def IAgStkObjectRootEvents_OnLogMessage(self, this, message, messageType, errorCode, fileName, lineNumber, displayId):
        """Log Message Created Event"""
        print("Log Message")
        print("\tMessage: " + message)
        print("\tMessage type: " + str(messageType))
        print("\tError code: " + str(errorCode))
        print("\tFile name: " + fileName)
        print("\tLine number: " + str(lineNumber))
        print("\tDisplay ID: " + str(displayId))

    def IAgStkObjectRootEvents_OnStkObjectAdded(self, this, sender):
        """Object Added Event"""
        print("Object added: " + sender)

    def IAgStkObjectRootEvents_OnStkObjectDeleted(self, this, sender):
        """Object Deleted Event"""
        print("Object deleted: " + sender)

    def IAgStkObjectRootEvents_OnStkObjectRenamed(self, this, sender, oldPath, newPath):
        """Object Renamed Event"""
        print("Object renamed: " + sender)
        print("\tOld Path: " + oldPath)
        print("\tNew Path: " + newPath)

    def IAgStkObjectRootEvents_OnAnimationPlayback(self, this, currentTime, action, direction):
        """Animation Playback Event"""
        print("Animation playback")
        print("\tCurrent time: " + str(currentTime))
        print("\tAction: " + str(action))
        print("\tDirection " + str(direction))

    def IAgStkObjectRootEvents_OnAnimationRewind(self, this):
        """Animation Rewind Event"""
        print("Animation rewind")

    def IAgStkObjectRootEvents_OnAnimationPause(self, this, currentTime):
        """Animation Paused Event"""
        print("Animation pause")
        print("\tCurrent time: " + str(currentTime))

    def IAgStkObjectRootEvents_OnScenarioBeforeSave(self, this, args):
        """Scenario Before Save Event"""
        args = args.QueryInterface(STKObjects.IAgScenarioBeforeSaveEventArgs)
        print("Before save")
        print("\tContinue Save: " + str(args.ContinueSave))
        print("\tPath: " + args.Path)

    def IAgStkObjectRootEvents_OnAnimationStep(self, this, currentTime):
        """Animation Step Event"""
        print("Animation step")
        print("\tCurrent time: " + str(currentTime))

    def IAgStkObjectRootEvents_OnAnimationStepBack(self, this, currentTime):
        """Animation Step Backward Event"""
        print("Animation step back")
        print("\tCurrent time: " + str(currentTime))

    def IAgStkObjectRootEvents_OnAnimationSlower(self, this):
        """Animation Slower Event"""
        print("Animation slower")

    def IAgStkObjectRootEvents_OnAnimationFaster(self, this):
        """Animation Faster Event"""
        print("Animation faster")

    def IAgStkObjectRootEvents_OnPercentCompleteUpdate(self, this, args):
        """Percent Complete Update Event"""
        args = args.QueryInterface(STKObjects.IAgPctCmpltEventArgs)
        print("Percent complete update")
        print("\tCan cancel: " + str(args.CanCancel))
        print("\tCanceled: " + str(args.Canceled))
        print("\tMessage: " + args.Message)
        print("\tPercent completed: " + str(args.PercentCompleted))

    def IAgStkObjectRootEvents_OnPercentCompleteEnd(self, this):
        """Percent Complete End Event"""
        print("Percent complete end")

    def IAgStkObjectRootEvents_OnPercentCompleteBegin(self, this):
        """Percent Complete Begin Event"""
        print("Percent complete begin")

    def IAgStkObjectRootEvents_OnStkObjectChanged(self, this, args):
        """Object Changed Event"""
        args = args.QueryInterface(STKObjects.IAgStkObjectChangedEventArgs)
        print("Object changed: " + args.Path)

    def IAgStkObjectRootEvents_OnScenarioBeforeClose(self, this):
        """Scenario Before Close Event"""
        print("Scenario before close")

    def IAgStkObjectRootEvents_OnStkObjectPreDelete(self, this, args):
        """Object PreDelete Event"""
        args = args.QueryInterface(STKObjects.IAgStkObjectPreDeleteEventArgs)
        print("Object pre-delete")
        print("\tContinue: " + str(args.Continue))
        print("\tSender: " + args.Path)

### Testing Event Subscriptions

# Attach to STK 12 Application
app = GetActiveObject('STK12.Application')
root = app.Personality2

# Create EventSink
sink = EventSink()

# List all Events
ShowEvents(root, interface=STKObjects.IAgStkObjectRootEvents)

# Sink to all events in the EventSink class
connection = GetEvents(root, sink, interface=STKObjects.IAgStkObjectRootEvents)

# Defines a timeout necessary for COM
PumpEvents(100) 