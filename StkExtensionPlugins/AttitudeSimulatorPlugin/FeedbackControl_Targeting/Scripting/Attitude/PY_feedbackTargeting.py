# python

global PY_feedbackTargeting_init
global PY_feedbackTargeting_Inputs

PY_feedbackTargeting_init = -1

#==========================================================================
# PY_feedbackTargeting() fctn
#==========================================================================
def PY_feedbackTargeting ( argList ):
	callMode = str(argList[0])
	if callMode == 'None' :
		retVal = PY_feedbackTargeting_compute( argList )	# do compute
	elif callMode == 'register' :
		global PY_feedbackTargeting_init 
		PY_feedbackTargeting_init = -1
		retVal = PY_feedbackTargeting_register()
	elif callMode == 'compute' :
		retVal = PY_feedbackTargeting_compute( argList )	# do compute
	else:
		retVal = []	# # bad call, return empty list
	return retVal


def PY_feedbackTargeting_register():
	arg1 = 'ArgumentType = Output ; Type = Parameter ; ArgumentName = Torque ; Name = Torque ; BasicType = Vector '
	arg2 = 'ArgumentType = Input ; ArgumentName = time ; Name = Epoch '
	arg3 = 'ArgumentType = Input ; ArgumentName = att ; Type = Attitude ; Derivative = Yes '
	arg4 = 'ArgumentType = Input ; ArgumentName = erratt ; Type = Attitude ; RefName = Body ; RefSource = Satellite/PerfectPointing '
	arg5 = 'ArgumentType = Input ; ArgumentName = IMtx ; Type = Inertia ; Name = Inertia '
	argList = [arg1, arg2, arg3, arg4, arg5]
	return argList

def PY_feedbackTargeting_compute( argList ):
	# NOTE: argList[0] is the call Mode, which is either None or 'compute'
	import numpy as np
	global PY_feedbackTargeting_init
	global PY_feedbackTargeting_Inputs
	if PY_feedbackTargeting_init < 0 :
		PY_feedbackTargeting_init = 1
		PY_feedbackTargeting_Inputs = g_PluginArrayInterfaceHash['PY_feedbackTargeting_Inputs']

	# Get inputs
	att = np.array(argList[PY_feedbackTargeting_Inputs['att']])
	erratt = np.array(argList[PY_feedbackTargeting_Inputs['erratt']])
	IMtxInput = np.array(argList[PY_feedbackTargeting_Inputs['IMtx']])

	# Since STK gives inertia matrix as 9x1, let's convert to 3x3 matrix instead
	IMtx = np.array([IMtxInput[0:3], IMtxInput[3:6], IMtxInput[6:9]])

	# Create gains
	k = 2
	c = 0.8

	# Apply control
	retList = -IMtx*(k*erratt[0:3]*erratt[3]+c*att[4:7])

	# This is the output format STK expects
	return np.array([retList])