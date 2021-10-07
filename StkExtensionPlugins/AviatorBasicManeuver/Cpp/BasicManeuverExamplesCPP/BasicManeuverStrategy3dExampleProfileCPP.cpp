// BasicManeuverStrategy3dExampleProfileCPP.cpp : Implementation of CBasicManeuverStrategy3dExampleProfileCPP

#include "pch.h"
#include "BasicManeuverStrategy3dExampleProfileCPP.h"


// CBasicManeuverStrategy3dExampleProfileCPP

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::GetClassID(CLSID* pClassID)
{
	if (!pClassID) return E_POINTER;
	*pClassID = CLSID_BasicManeuverStrategy3dExampleProfileCPP;
	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::InitNew(void)
{
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::Load(IPropertyBag* pPropBag, IErrorLog* pErrorLog)
{
	CComVariant var;
    HRESULT hr = pPropBag->Read(CComBSTR(_T("StratExampleType")), &var, pErrorLog);
	return hr;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::Save(IPropertyBag* pPropBag, BOOL fClearDirty, BOOL fSaveAllProperties)
{
    CComVariant var(CComBSTR(_T("Example3dProfileCPP")));
    return pPropBag->Write(CComBSTR(_T("StratExampleType")), &var);;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::get_StrategyType(BSTR* pVal)
{
	return CComBSTR(_T("BasicManeuverStrategy3dExampleCPP")).CopyTo(pVal);
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::get_StrategyDescription(BSTR* pVal)
{
    return CComBSTR(_T("Example 3D Strategy for C++")).CopyTo(pVal);
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::get_IsNavigationStrategy(VARIANT_BOOL* pVal)
{
	if (!pVal) return E_POINTER;
	*pVal = VARIANT_FALSE;
	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::get_IsProfileStrategy(VARIANT_BOOL* pVal)
{
    if (!pVal) return E_POINTER;
    *pVal = VARIANT_TRUE;
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::Configure(IAgFlightProcedure* pParentProc, IAgFlightBasicManeuverStrategy* pOtherStrategy)
{
	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::GetValidProfileStrategyFactories(IAgComUnkCollection* pRegisteredStratFactories, IAgComUnkCollection** ppValidStratFactories)
{
	CComPtr<IAgFlightBasicManeuverStrategyFactory> pProfileStratFactory;
	HRESULT hr = pProfileStratFactory.CoCreateInstance(CLSID_BasicManeuverStrategyFactory3dExampleProfileCPP);
	if (FAILED(hr)) return hr;
	CComPtr<IAgComUnkCollectionInit>	pValidStratFactoryColl;
	hr = pValidStratFactoryColl.CoCreateInstance(__uuidof(AgComUnkCollection));
	if (FAILED(hr)) return hr;
    hr = pValidStratFactoryColl->Add(pProfileStratFactory);
    if (FAILED(hr)) return hr;
    return pValidStratFactoryColl.QueryInterface(ppValidStratFactories);
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::SetInitialState(AgEFlightPerformanceMode ePerfMode, AgEFlightPhaseOfFlight ePhaseOfFlight, IAgFlightBasicManeuverState* pInitState, IAgFlightDoubleArray* presultAuxInitState)
{
	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::QueryDerivatives(IAgFlightBasicManeuverState* pCurrentState, IAgFlightBasicManeuverDerivs* presultCurrentDerivs, VARIANT_BOOL vbAllowThresholdDetection, VARIANT_BOOL* pvbThresholdCrossed)
{
	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::RefineIntegrateThreshold(AgEFlightIntegThresholdType eThresholdType, IAgFlightBasicManeuverState* pLastGoodState, IAgFlightBasicManeuverState* pCurrentState, VARIANT_BOOL* pvbContinue)
{
	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::DataIsHeld(IUnknown* pDataUnknown, VARIANT_BOOL* vbIsHeld)
{
	if (!vbIsHeld) return E_POINTER;
	*vbIsHeld = VARIANT_FALSE;
	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::GetSpecialTimes(SAFEARRAY** pTimes)
{
	if (!pTimes) return E_POINTER;
    CComSafeArray<VARIANT>	specTimes((ULONG)0);
    *pTimes = specTimes.Detach();
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::Copy()
{
	return S_FALSE;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::get_CanPaste(VARIANT_BOOL* pVal)
{
	if (!pVal) return E_POINTER;
	*pVal = VARIANT_FALSE;
	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleProfileCPP::Paste()
{
	ATLASSERT(FALSE);
	return E_UNEXPECTED;
}
