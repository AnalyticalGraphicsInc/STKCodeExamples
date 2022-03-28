// BasicManeuverStrategy3dExampleNavCPP.cpp : Implementation of CBasicManeuverStrategy3dExampleNavCPP

#include "pch.h"
#include "BasicManeuverStrategy3dExampleNavCPP.h"


// CBasicManeuverStrategy3dExampleNavCPP

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::GetClassID(CLSID* pClassID)
{
    if (!pClassID) return E_POINTER;
    *pClassID = CLSID_BasicManeuverStrategy3dExampleNavCPP;
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::InitNew(void)
{
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::Load(IPropertyBag* pPropBag, IErrorLog* pErrorLog)
{
    CComVariant var;
    HRESULT hr = pPropBag->Read(CComBSTR(_T("StratExampleType")), &var, pErrorLog);
    return hr;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::Save(IPropertyBag* pPropBag, BOOL fClearDirty, BOOL fSaveAllProperties)
{
    CComVariant var(CComBSTR(_T("Example3dNavCPP")));
    return pPropBag->Write(CComBSTR(_T("StratExampleType")), &var);;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::get_StrategyType(BSTR* pVal)
{
    return CComBSTR(_T("BasicManeuverStrategy3dExampleCPP")).CopyTo(pVal);
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::get_StrategyDescription(BSTR* pVal)
{
    return CComBSTR(_T("Example 3D Strategy for C++")).CopyTo(pVal);
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::get_IsNavigationStrategy(VARIANT_BOOL* pVal)
{
    if (!pVal) return E_POINTER;
    *pVal = VARIANT_TRUE;
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::get_IsProfileStrategy(VARIANT_BOOL* pVal)
{
    if (!pVal) return E_POINTER;
    *pVal = VARIANT_FALSE;
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::Configure(IAgFlightProcedure* pParentProc, IAgFlightBasicManeuverStrategy* pOtherStrategy)
{
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::GetValidProfileStrategyFactories(IAgComUnkCollection* pRegisteredStratFactories, IAgComUnkCollection** ppValidStratFactories)
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

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::SetInitialState(AgEFlightPerformanceMode ePerfMode, AgEFlightPhaseOfFlight ePhaseOfFlight, IAgFlightBasicManeuverState* pInitState, IAgFlightDoubleArray* presultAuxInitState)
{
	if (!pInitState) return E_POINTER;

    CComPtr<IAgQuaternion> pRefAxesQuat;
    CComPtr<IAgCartVec3> pRefAxesAngVel;
    HRESULT hr = pRefAxesQuat.CoCreateInstance(__uuidof(AgQuaternion));
	if (FAILED(hr)) return hr;
    hr = pRefAxesAngVel.CoCreateInstance(__uuidof(AgCartVec3));
    if (FAILED(hr)) return hr;

    // generate the default ref axes coordinate frame ...
    hr = pInitState->ComputeStandardECFToRefAxesFrame(
        VARIANT_TRUE, AgEFlightAxesOrientation::eFwdRightDown, pRefAxesQuat, pRefAxesAngVel);
    if (FAILED(hr)) return hr;

    // tell the system to use that ref axes frame (can only be done during SetInitialState)...
    hr = pInitState->SetInitialECFToRefAxesFrame(AgEFlightAxesOrientation::eFwdRightDown, pRefAxesQuat, pRefAxesAngVel);
    if (FAILED(hr)) return hr;

	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::QueryDerivatives(IAgFlightBasicManeuverState* pCurrentState, IAgFlightBasicManeuverDerivs* presultCurrentDerivs, VARIANT_BOOL vbAllowThresholdDetection, VARIANT_BOOL* pvbThresholdCrossed)
{
	if (!pCurrentState || !presultCurrentDerivs) return E_POINTER;

    // the strategy extrapolates the starting flight condition ...

    // model the effects of coriolis without compensating for it ...
    presultCurrentDerivs->SetVertPlaneCompensateForCoriolisAccel(VARIANT_FALSE);
    presultCurrentDerivs->SetHorizPlaneCompensateForCoriolisAccel(VARIANT_FALSE);

    // totalECFAccel = pitchRateNormalAccel + speedDotLongAccel + coriolisAccel - centripetalAccel
    pCurrentState->QueryECFCoriolisAccel(m_coriolisAccel);
    pCurrentState->QueryECFCentripetalAccel(m_centripetalAccel);
    m_accelCommand->SetToZero(); // pitchRateNormalAccel = speedDotLongAccel = 0
    m_accelCommand->Add(m_coriolisAccel);
    m_accelCommand->Subtract(m_centripetalAccel);

    presultCurrentDerivs->SetEcfAccelCommand(m_accelCommand);

    // compute ref axes PQRs
    pCurrentState->QueryECFToRefAxesAttRefFrame(m_ecfToRefAxesQuat, nullptr);
    pCurrentState->QueryOmegaEarthInFrame(m_ecfToRefAxesQuat, m_earthOmegaInRefAxes);

    double dCommandFPADot = 0.0;
    m_PQR->ConstructFromComponents(0.0, dCommandFPADot, 0.0);
    m_PQR->Add(m_earthOmegaInRefAxes);

    presultCurrentDerivs->SetRefAxesOmegaCommand(m_PQR);

    // Tgo is used for calc progress and to refine stopping condition
    presultCurrentDerivs->put_NavTimeToGo(1.0);
    presultCurrentDerivs->put_ProfileTimeToGo(1.0);

	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::RefineIntegrateThreshold(AgEFlightIntegThresholdType eThresholdType, IAgFlightBasicManeuverState* pLastGoodState, IAgFlightBasicManeuverState* pCurrentState, VARIANT_BOOL* pvbContinue)
{
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::DataIsHeld(IUnknown* pDataUnknown, VARIANT_BOOL* vbIsHeld)
{
    if (!vbIsHeld) return E_POINTER;
    *vbIsHeld = VARIANT_FALSE;
    return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::GetSpecialTimes(SAFEARRAY** pTimes)
{
    if (!pTimes) return E_POINTER;
    CComSafeArray<VARIANT>	specTimes((ULONG)0);
    *pTimes = specTimes.Detach();
    return S_OK;
}

CComPtr<IPersistPropertyBag> CBasicManeuverStrategy3dExampleNavCPP::gm_pStratCopy;

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::Copy()
{
    gm_pStratCopy.Release();

	CComPtr<IPersistPropertyBag> pCopy;
	HRESULT hr = pCopy.CoCreateInstance(CLSID_BasicManeuverStrategy3dExampleNavCPP);
	if (FAILED(hr)) return hr;


    CComPtr<IAgFlightPersistHelper> pPersistHelper;
    hr = pPersistHelper.CoCreateInstance(__uuidof(AgFlightPersistHelper));
    if (FAILED(hr)) return hr;

    hr = pPersistHelper->Merge(this, pCopy);
	if (FAILED(hr)) return hr;

    gm_pStratCopy = pCopy;

	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::get_CanPaste(VARIANT_BOOL* pVal)
{
    if (!pVal) return E_POINTER;

	if (!gm_pStratCopy)
	{
		*pVal = VARIANT_FALSE;
	}
    else
    {
        *pVal = VARIANT_TRUE;
    }

	return S_OK;
}

HRESULT __stdcall CBasicManeuverStrategy3dExampleNavCPP::Paste()
{
    CComPtr<IAgFlightPersistHelper> pPersistHelper;
	HRESULT hr = pPersistHelper.CoCreateInstance(__uuidof(AgFlightPersistHelper));
	if (FAILED(hr)) return hr;
    return pPersistHelper->Merge(gm_pStratCopy, this);
}
