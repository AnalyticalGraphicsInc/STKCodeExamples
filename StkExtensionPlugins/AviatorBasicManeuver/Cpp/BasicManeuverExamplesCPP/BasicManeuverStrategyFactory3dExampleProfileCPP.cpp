// BasicManeuverStrategyFactory3dExampleProfileCPP.cpp : Implementation of CBasicManeuverStrategyFactory3dExampleProfileCPP

#include "pch.h"
#include "BasicManeuverStrategyFactory3dExampleProfileCPP.h"


// CBasicManeuverStrategyFactory3dExampleProfileCPP

HRESULT __stdcall CBasicManeuverStrategyFactory3dExampleProfileCPP::get_StrategyType(BSTR* pVal)
{
    return CComBSTR(_T("BasicManeuverStrategy3dExampleCPP")).CopyTo(pVal);
}

HRESULT __stdcall CBasicManeuverStrategyFactory3dExampleProfileCPP::get_IsNavigationStrategy(VARIANT_BOOL* pVal)
{
	if (!pVal) return E_POINTER;
	*pVal = VARIANT_FALSE;
	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategyFactory3dExampleProfileCPP::get_IsProfileStrategy(VARIANT_BOOL* pVal)
{
    if (!pVal) return E_POINTER;
    *pVal = VARIANT_TRUE;
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategyFactory3dExampleProfileCPP::CanCreateStrategy(IAgFlightProcedure* pParentProc, IAgFlightBasicManeuverStrategy* pNavStrategy, BSTR* pErrorMessage)
{
	CComQIPtr< IBasicManeuverStrategy3dExampleNavCPP>	pNavExample(pNavStrategy);
	if (!pNavExample)
	{
		if (pErrorMessage)
		{
			CComBSTR(_T("Strategy must be paired with a 3dExampleNavCPP strategy")).CopyTo(pErrorMessage);
		}
		return S_FALSE;
	}

    if (pErrorMessage)
    {
        CComBSTR(_T("Strategy is valid")).CopyTo(pErrorMessage);
    }

	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategyFactory3dExampleProfileCPP::CreateStrategy(IAgFlightProcedure* pParentProc, IAgFlightBasicManeuverStrategy* pNavStrategy, IAgFlightBasicManeuverStrategy** ppNewStrategy)
{
	CComPtr< IAgFlightBasicManeuverStrategy>	pStrat;
	HRESULT hr = pStrat.CoCreateInstance(CLSID_BasicManeuverStrategy3dExampleProfileCPP);
	if (FAILED(hr)) return hr;
	hr = pStrat->Configure(pParentProc, pNavStrategy);
    if (FAILED(hr)) return hr;
	return pStrat.CopyTo(ppNewStrategy);
}
