# python

global PY_CalcObject_init
global PY_CalcObject_Inputs

PY_CalcObject_init = -1

#==========================================================================
# PY_CalcObject() fctn
#==========================================================================
def PY_CalcObject ( argList ):
	callMode = str(argList[0])
	if callMode == 'None' :
		retVal = PY_CalcObject_compute( argList )	# do compute
	elif callMode == 'register' :
		global PY_CalcObject_init 
		PY_CalcObject_init = -1
		retVal = PY_CalcObject_register()
	elif callMode == 'compute' :
		retVal = PY_CalcObject_compute( argList )	# do compute
	else:
		retVal = []	# # bad call, return empty list
	return retVal


def PY_CalcObject_register():
	arg1 = 'ArgumentType = Output ; ArgumentName = Value ; Name = Value'
	arg2 = 'ArgumentType = Input ; ArgumentName = Inc ; Name = Inclination ; Type = CalcObject'
	arg3 = 'ArgumentType = Input ; ArgumentName = RightAsc ; Name = RAAN ; Type = CalcObject'
	argList = [arg1, arg2, arg3]
	return argList

def PY_CalcObject_compute( argList ):
	# NOTE: argList[0] is the call Mode, which is either None or 'compute'
	global PY_CalcObject_init
	global PY_CalcObject_Inputs
	if PY_CalcObject_init < 0 :
		PY_CalcObject_init = 1
		PY_CalcObject_Inputs = g_PluginArrayInterfaceHash['PY_CalcObject_Inputs']
	inc = float(argList[PY_CalcObject_Inputs['Inc']])	#inc = float(argList[1])
	raan = float(argList[PY_CalcObject_Inputs['RightAsc']])    #raan = float(argList[2])
	retList = [sin(inc)*sin(raan)]
	return retList

