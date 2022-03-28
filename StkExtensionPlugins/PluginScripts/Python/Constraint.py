# python

global PY_Constraint_init
global PY_Constraint_Inputs

PY_Constraint_init = -1

# Constraint Plugin component PY_Constraint
# Author:     Guy Smith
# Company:    Analytical Graphics, Inc.
# Description: 
#  This function is called by the AGI Constraint Script Plugin software and is not intended 
#   for general use. This function takes a structure as input that contains
#   a 'method' string parameter that is utilized to determine what the purpose
#   of the call was. Four methods are supported. ( 'compute', 'register', 'GetAccessList',
#   'GetConstraintDisplayName' ) Refer to the Constraint Script Plugin documentation 
#   for an in-depth explaination of the functionality.
#
#  This file provides an example of the capabilities of the Constraint Script Plugin
#   functionality. The simple Constraint that is implemented by this script associates itself
#   with the Facility class and configures itself to return the the Constraint iteration 
#   step Epoch time (in STK Epoch seconds) passed in on every iteration.

# Notes:
# 1)  This source file is loaded into a general script engine namespace. Therefore,
#      any function names and global variables must be named appropriately or other
#      script source files could corrupt the data or call the functions. It is
#      possible to take advantage of the common namespace as a means to communicate
#      between other scripts/plugins, but, you must design the scripts to do this.
#      A good method to avoid namespace collision is to preface the functions and
#      variables with the name of the file, as was done in this example.
# 2)  Using MsgBox calls in this script is a good way to debug the functionality,
#      but will interupt STK, since the MsgBox must be Acknowledged before control
#      is passed back to the STK process.

# Copyright 2006, Analytical Graphics Incorporated

#==========================================================================
# PY_Constraint_GetConstraintDisplayName() fctn
#==========================================================================
def PY_Constraint_GetConstraintDisplayName () :
	return 'PythonPluginConstraint'

#==========================================================================
# PY_Constraint_GetAccessList() fctn
#==========================================================================

G_PY_Constraint_AllClasses = 'Aircraft,AreaTarget,Facility,GroundVehicle,'
G_PY_Constraint_AllClasses +='LaunchVehicle,Missile,Planet,Radar,Satellite,Ship,Star,Target'

def PY_Constraint_GetAccessList( argList ) :
	#str(argList[0]) is the name of the call mode 'GetAccessList'
	strBaseClass = str(argList[1])

	strReturnValue = ""
	if strBaseClass == "Facility" :
		strReturnValue = G_PY_Constraint_AllClasses

	return strReturnValue


#==========================================================================
# PY_Constraint fctn
#==========================================================================
def PY_Constraint( argList ) :
	callMode = str(argList[0])
	if callMode == 'None' :
		retVal = PY_Constraint_compute( argList )	# do compute
	elif callMode == 'register' :
		global PY_Constraint_init 
		PY_Constraint_init = -1
		retVal = PY_Constraint_register()
	elif callMode == 'compute' :
		retVal = PY_Constraint_compute( argList )	# do compute
	elif callMode == "GetAccessList" :
		retVal = PY_Constraint_GetAccessList( argList )
	elif callMode == "GetConstraintDisplayName" :
		retVal = PY_Constraint_GetConstraintDisplayName()
	else:
		retVal = []	# # bad call, return empty list
	return retVal

#==========================================================================
# PY_Constraint_register
#==========================================================================
def PY_Constraint_register() :

	args = []

	# Outputs

	args.append('ArgumentType = Output ; ArgumentName = Status ; Name = Status ') 
	args.append('ArgumentType = Output ; ArgumentName = Result ; Name = Result ') 

	# Inputs

	args.append('ArgumentType = Input ; ArgumentName = Epoch ; Name = Epoch ') 

	args.append('ArgumentType = Input ; ArgumentName = fromPos ; Name = fromPosition ; RefName = Fixed ') 

	args.append('ArgumentType = Input ; ArgumentName = fromVel ; Name = fromVelocity ; RefName = Inertial ') 

	args.append('ArgumentType = Input ; ArgumentName = fromQuat ; Name = fromQuaternion; RefName = Fixed ')

	args.append('ArgumentType = Input ; ArgumentName = fromObj ; Name = fromObjectPath ')
 
	args.append('ArgumentType = Input ; ArgumentName = toPos ; Name = toPosition ; RefName = Fixed ') 

	args.append('ArgumentType = Input ; ArgumentName = toVel ; Name = toVelocity ; RefName = Inertial ') 

	args.append('ArgumentType = Input ; ArgumentName = toQuat ; Name = toQuaternion; RefName = Fixed ')

	args.append('ArgumentType = Input ; ArgumentName = toObj ; Name = toObjectPath ') 

	# Get an input from the Vector Geometry Tool
	descrip = 'ArgumentType = Input ; ArgumentName = toEarthFromMoonInSunFixed ; '
	descrip += 'Type = Vector ; Name = Earth ; Source = CentralBody/Moon ; RefSource = CentralBody/Sun ;'
	descrip += 'RefName = Fixed ; Derivative = Yes'
	args.append(descrip) 

	return args


def PY_Constraint_compute( argList ) :
	# NOTE: argList[0] is the call Mode, which is either None or 'compute'
	global debug
	global PY_Constraint_init
	global PY_Constraint_Inputs
	status = 'Okay'
	if PY_Constraint_init < 0 :
		PY_Constraint_init = 1
		PY_Constraint_Inputs = g_PluginArrayInterfaceHash['PY_Constraint_Inputs']
		status = 'MESSAGE: [Info] PY_Constraint- Everything is fine;  CONTROL: OK'
	time = float(argList[PY_Constraint_Inputs['Epoch']])
	
	retList = []
	retList.append(status) # Status
	retList.append(time)	# return epoch as the value
	
	return retList


