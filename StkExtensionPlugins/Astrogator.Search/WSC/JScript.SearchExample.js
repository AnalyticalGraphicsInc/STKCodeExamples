//=====================================================
//  Copyright 2006-2010, Analytical Graphics, Inc.          
//=====================================================

//===========================================
// AgEAttrAddFlags Enumeration
//===========================================
var eFlagNone			= 0;
var eFlagTransparent	= 2;
var eFlagHidden			= 4;
var eFlagTransient		= 8; 
var eFlagReadOnly		= 16;
var eFlagFixed			= 32;

//==========================================
// AgELogMsgType Enumeration
//==========================================
var eLogMsgDebug	 	= 0;
var eLogMsgInfo 		= 1;
var eLogMsgForceInfo 	= 2;
var eLogMsgWarning 		= 3;
var eLogMsgAlarm 		= 4;

//==========================================
// AgESearchControlTypes Enumeration
//==========================================
var eSearchControlTypesReal	= 0;

//==========================================
// Declare Global Variables
//==========================================
var m_AgUtPluginSite		= null;
var m_AgAttrScope			= null;

//======================================
// Declare Global 'Attribute' Variables
//======================================
var m_Name	= "JScript.SearchExample.wsc"   
var m_maxIters = 100;

//=======================
// GetPluginConfig method
//=======================
function GetPluginConfig( AgAttrBuilder )
{
	if( m_AgAttrScope == null )
	{
		m_AgAttrScope = AgAttrBuilder.NewScope();
		
		//===========================
		// General Plugin attributes
		//===========================
		AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "PluginName", "Human readable plugin name or alias", "Name", eFlagReadOnly );			
		AgAttrBuilder.AddIntDispatchProperty  ( m_AgAttrScope, "MaxIterations", "Maximum Iterations", "MaxIterations", 0 );
	}

	return m_AgAttrScope;
}

//===========================
// VerifyPluginConfig method
//===========================
function VerifyPluginConfig(AgUtPluginConfigVerifyResult)
{   
	var Result = true;
	var Message = "Ok";

	AgUtPluginConfigVerifyResult.Result  = Result;
	AgUtPluginConfigVerifyResult.Message = Message;
}  

//===========================
// Message method
//===========================
function Message( msgType, msg )
{
	if( m_AgUtPluginSite != null)
	{   	
		m_AgUtPluginSite.Message( msgType, msg );
	}
}

//======================
// Init Method
//======================
function Init( AgUtPluginSite )
{
	m_AgUtPluginSite = AgUtPluginSite;
	
	return true;
}

//======================
// Run Method
//======================
function Run( AgSearchOperand, testing )
{
    var controls = AgSearchOperand.Controls;
    var results = AgSearchOperand.Results;
    
    var controlIndex = 0;
    
    while (controlIndex < controls.Count && !controls(controlIndex).IsActive)
	{
		++controlIndex;
	}
	
	var resultIndex = 0;
	while (resultIndex < results.Count && !results(resultIndex).IsActive)
	{
		++resultIndex;
	}
	
	if (controlIndex >= controls.Count || resultIndex >= results.Count)
	{
		Message(eLogMsgAlarm, "There must be at least one active control and result.");
		return false;
	}

	var count = 0;
	
	AgSearchOperand.Evaluate2(true); // the true flag lets the run appear on graphs
	
	var a = controls(controlIndex).CurrentValue;
	var b = a;
	var fa = results(resultIndex).CurrentValue;
	var fb = fa;
	
	var step = controls(controlIndex).Step;
	
	var desired = results(resultIndex).DesiredValue;
	
	var tolerance = results(resultIndex).Tolerance;
	
	// are we already within tolerance?
	if (Math.abs(fa - desired) < tolerance)
	{
		return true;
	}
	
	if (testing)
	{
		// if we're testing we won't do any searching
		return false;
	}

    // make the status grid
	AgSearchOperand.StatusGrid.CreateGrid(2, 6);
	AgSearchOperand.StatusGrid.SetColumnToTruncateLeft(0);
	AgSearchOperand.StatusGrid.SetColumnToTruncateLeft(2);
	AgSearchOperand.StatusGrid.SetHeaderCellString(0, 0, "Control Name");
	AgSearchOperand.StatusGrid.SetHeaderCellString(0, 1, "Control Value");
	AgSearchOperand.StatusGrid.SetHeaderCellString(0, 2, "Result Name");
	AgSearchOperand.StatusGrid.SetHeaderCellString(0, 3, "Result Value");
	AgSearchOperand.StatusGrid.SetHeaderCellString(0, 4, "Desired Value");
	AgSearchOperand.StatusGrid.SetHeaderCellString(0, 5, "Tolerance");

	AgSearchOperand.StatusGrid.SetCellString(1, 0, controls(controlIndex).ObjectName + " : " + controls(controlIndex).ControlName);
	AgSearchOperand.StatusGrid.SetCellString(1, 2, results(resultIndex).ObjectName + " : " + results(resultIndex).ResultName);
	AgSearchOperand.StatusGrid.SetCellResultValue(1, 4, resultIndex, results(resultIndex).DesiredValue, 8);
	// tolerance is in delta units
	AgSearchOperand.StatusGrid.SetCellResultDeltaValue(1, 5, resultIndex, results(resultIndex).Tolerance, 8);

	AgSearchOperand.StatusGrid.SetStatus("Initial run");
	
	var changedSign = false;
	
	var bounded = false;
	
	// find the initial set that bounds zero
	while(!bounded && count < m_maxIters)
	{
	    ++count;

		a = b;
		fa = fb;
		b = a + step;
		controls(controlIndex).CurrentValue = b;
	
		AgSearchOperand.Evaluate2(true);

		var fb = results(resultIndex).CurrentValue;

		AgSearchOperand.StatusGrid.SetCellControlValue(1, 1, controlIndex, b, 8);
		AgSearchOperand.StatusGrid.SetCellResultValue(1, 3, resultIndex, fb, 8);
		AgSearchOperand.StatusGrid.SetStatus("Iteration " + count + ": Searching for bounds");
		AgSearchOperand.StatusGrid.Refresh();

		// see if b hit the desired value
		if (Math.abs(fb - desired) < tolerance)
		{
		    AgSearchOperand.StatusGrid.SetStatus("Desired value reached while searching for bounds.");
		    AgSearchOperand.StatusGrid.Refresh();
			return true;
		}
		
		bounded = (fa > desired && fb < desired) || (fa < desired && fb > desired);
		
		// make sure we are getting closer to the desired value
		if ( !bounded && Math.abs(fb - desired) >= Math.abs(fa - desired) )
		{
			// search in the other direction, unless we've already changed the step once
			if (!changedSign)
			{
				changedSign = true;
				step = -step;
				b = a;
				fb = fa;
			}
			else
			{
			    // error out
			    AgSearchOperand.StatusGrid.SetStatus("Unable to bound desired value");
			    AgSearchOperand.StatusGrid.Refresh();
				Message(eLogMsgAlarm, "Unable to bound desired value with given initial guess and step");
				return false;
			}
		}
	}
	
	var c = b;
	var fc = fb;
	while (Math.abs(fc - desired) > tolerance && count < m_maxIters) 
	{
	    count++;

	    c = (a + b) / 2.0;
		
		controls(controlIndex).CurrentValue = c;
		
		AgSearchOperand.Evaluate2(true);

		fc = results(resultIndex).CurrentValue;

		if ( (fc > desired && fa > desired) || (fc < desired && fa < desired))
		{
			a = c;
			fa = fc;
		}
		else
		{
			b = c;
			fb = fc;
		}

		AgSearchOperand.StatusGrid.SetStatus("Iteration " + count + ": Searching for root");
		AgSearchOperand.StatusGrid.SetCellControlValue(1, 1, controlIndex, c, 8);
		AgSearchOperand.StatusGrid.SetCellResultValue(1, 3, resultIndex, fc, 8);
		AgSearchOperand.StatusGrid.Refresh();
	}
	
	if (Math.abs(fc - desired) > tolerance) {

	    AgSearchOperand.StatusGrid.SetStatus("Unable to converge within " + m_maxIters + " iterations.");
	    AgSearchOperand.StatusGrid.Refresh();
	    return false;
	}
	else
	{
	    AgSearchOperand.StatusGrid.SetStatus("Converged after  " + count + " iterations.");
	    AgSearchOperand.StatusGrid.Refresh();
	    return true;
	}	
}

//=============================
// Get Control's Prog Id method
//=============================
function GetControlsProgId(controlType)
{
	if (controlType == eSearchControlTypesReal)
	{
		return "JScript.SearchControlRealExample.WSC";
	}
	else
	{
		return "";
	}
}

//=============================
// Get Results's Prog Id method
//=============================
function GetResultsProgId()
{
	return "JScript.SearchResultExample.WSC";
}

//=================
// Free Method
//=================
function Free()
{
	// do nothing
}
    
//==================
// Name property
//==================
function GetName()
{
	return m_Name;
}

//========================
// Max iterations property
//========================
function GetMaxIterations()
{
	return m_maxIters;
}

function SetMaxIterations(val)
{
	m_maxIters = val;
}

//=====================================================
//  Copyright 2006-2010, Analytical Graphics, Inc.          
//=====================================================