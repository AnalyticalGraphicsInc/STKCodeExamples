# python

global PY_DetumbleTorque_init
global PY_DetumbleTorque_Inputs

PY_DetumbleTorque_init = -1

#==========================================================================
# PY_DetumbleTorque() fctn
#==========================================================================
def PY_DetumbleTorque ( argList ):
	callMode = str(argList[0])
	if callMode == 'None' :
		retVal = PY_DetumbleTorque_compute( argList )	# do compute
	elif callMode == 'register' :
		global PY_DetumbleTorque_init 
		PY_DetumbleTorque_init = -1
		retVal = PY_DetumbleTorque_register()
	elif callMode == 'compute' :
		retVal = PY_DetumbleTorque_compute( argList )	# do compute
	else:
		retVal = []	# # bad call, return empty list
	return retVal


def PY_DetumbleTorque_register():
	arg1 = 'ArgumentType = Output ; Type = Parameter ; ArgumentName = Torque ; Name = Torque ; BasicType = Vector '
	arg2 = 'ArgumentType = Input ; ArgumentName = time ; Name = Epoch '
	arg3 = 'ArgumentType = Input ; ArgumentName = MagFieldIGRF ; Name = MagField(IGRF) ; Type = Vector ; RefType = Attitude ; Derivative = Yes '
	argList = [arg1, arg2, arg3]
	return argList

def PY_DetumbleTorque_compute( argList ):
	# NOTE: argList[0] is the call Mode, which is either None or 'compute'
	import numpy as np
	global PY_DetumbleTorque_init
	global PY_DetumbleTorque_Inputs
	if PY_DetumbleTorque_init < 0 :
		PY_DetumbleTorque_init = 1
		PY_DetumbleTorque_Inputs = g_PluginArrayInterfaceHash['PY_DetumbleTorque_Inputs']

	# Get inputs
	epoch = argList[PY_DetumbleTorque_Inputs['time']]
	magFieldVec = np.array([argList[PY_DetumbleTorque_Inputs['MagFieldIGRF']]])

	# MagFieldIGRF is 6x1 tuple of magField and magFieldDot
	magField = magFieldVec[0:3]
	magFieldDot = magFieldVec[3:6]
	crossProduct = np.cross(magFieldDot, magField)

	# Apply some negative value to counter the change relative to the magnetic field
	retList = np.array(-4000*crossProduct)

	# This is the output format STK expects
	return np.array([retList])