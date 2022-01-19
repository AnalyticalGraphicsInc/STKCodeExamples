// BasicManeuverStrategyFactory3dExampleNavCPP.cpp : Implementation of CBasicManeuverStrategyFactory3dExampleNavCPP

#include "pch.h"
#include "BasicManeuverStrategyFactory3dExampleNavCPP.h"


// CBasicManeuverStrategyFactory3dExampleNavCPP

HRESULT __stdcall CBasicManeuverStrategyFactory3dExampleNavCPP::get_StrategyType(BSTR* pVal)
{
    return CComBSTR(_T("BasicManeuverStrategy3dExampleCPP")).CopyTo(pVal);
}

HRESULT __stdcall CBasicManeuverStrategyFactory3dExampleNavCPP::get_IsNavigationStrategy(VARIANT_BOOL* pVal)
{
    if (!pVal) return E_POINTER;
    *pVal = VARIANT_TRUE;
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategyFactory3dExampleNavCPP::get_IsProfileStrategy(VARIANT_BOOL* pVal)
{
    if (!pVal) return E_POINTER;
    *pVal = VARIANT_FALSE;
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategyFactory3dExampleNavCPP::CanCreateStrategy(IAgFlightProcedure* pParentProc, IAgFlightBasicManeuverStrategy* pNavStrategy, BSTR* pErrorMessage)
{
    if (pErrorMessage)
	{
		CComBSTR(_T("Strategy is valid")).CopyTo(pErrorMessage);   
	}
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategyFactory3dExampleNavCPP::CreateStrategy(IAgFlightProcedure* pParentProc, IAgFlightBasicManeuverStrategy* pNavStrategy, IAgFlightBasicManeuverStrategy** ppNewStrategy)
{
    CComPtr< IAgFlightBasicManeuverStrategy>	pStrat;
    HRESULT hr = pStrat.CoCreateInstance(CLSID_BasicManeuverStrategy3dExampleNavCPP);
    if (FAILED(hr)) return hr;
    hr = pStrat->Configure(pParentProc, pNavStrategy);
    if (FAILED(hr)) return hr;
    return pStrat.CopyTo(ppNewStrategy);
}
