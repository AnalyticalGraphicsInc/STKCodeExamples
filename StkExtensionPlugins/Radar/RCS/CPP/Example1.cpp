/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"
#include "Example1.h"
#include "Agi.Radar.RCS.CPP.Example_i.c"

CExample1::CExample1() :
m_constantRCS(1.0),
m_enablePolarization(false)
{
}

CExample1::~CExample1()
{
}

HRESULT CExample1::FinalConstruct()
{
    return S_OK;
}

void CExample1::FinalRelease() 
{
}

//=========================
// IAgUtPluginConfig Methods
//=========================
STDMETHODIMP CExample1::GetPluginConfig(IAgAttrBuilder* pAttrBuilder, IDispatch** ppDispScope)
{
    EX_BEGIN_PARAMS()
        EX_IN_INTERFACE_PARAM( pAttrBuilder )
        EX_OUT_RETVAL_INTERFACE_PARAM( ppDispScope )
    EX_END_PARAMS()

    HRESULT hr = S_OK;

    try
    {
        if( !m_pDispScope )
        {
            EXCEPTION_HR(pAttrBuilder->NewScope( &m_pDispScope ));
            EXCEPTION_HR(pAttrBuilder->AddQuantityDispatchProperty2(m_pDispScope, CComBSTR("ConstantRCS"), CComBSTR("ConstantRCS"), CComBSTR("ConstantRCS"), CComBSTR("Rcs"),
                                                                   CComBSTR("dBsm"), CComBSTR("sqm"), 0));
            EXCEPTION_HR(pAttrBuilder->AddBoolDispatchProperty(m_pDispScope, CComBSTR("EnablePolarization"), CComBSTR("EnablePolarization"), CComBSTR("EnablePolarization"), 0));
        }
        EXCEPTION_HR( m_pDispScope.CopyTo( ppDispScope ) );
    }
    catch( HRESULT r )
    {
        hr = r;
    }
    catch(...)
    {
        hr = E_FAIL;
    }

    return hr;
}

STDMETHODIMP CExample1::VerifyPluginConfig(IAgUtPluginConfigVerifyResult* pPluginCfgResult)
{
    EX_BEGIN_PARAMS()
        EX_IN_INTERFACE_PARAM( pPluginCfgResult )
    EX_END_PARAMS()

    EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
    EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) ); 

    return S_OK;
}
//=========================
// IAgStkRadarRcsPlugin Methods
//=========================
STDMETHODIMP CExample1::Initialize(IAgUtPluginSite* site)
{
    EX_BEGIN_PARAMS()
        EX_IN_INTERFACE_PARAM( site )
    EX_END_PARAMS()

    HRESULT hr = S_OK;

    try
    {
        m_pUtPluginSite = site;

        if(m_pUtPluginSite)
        {
            EXCEPTION_HR(m_pUtPluginSite->QueryInterface(&m_pStkPluginSite));

            if(m_pStkPluginSite)
            {
                EXCEPTION_HR(m_pStkPluginSite->get_VectorToolProvider(&m_pVectorToolProvider));
                EXCEPTION_HR(m_pStkPluginSite->get_CalcToolProvider(&m_pCalcToolProvider));
            }
        }
    }
    catch( HRESULT r )
    {
        hr = r;
    }
    catch(...)
    {
        hr = E_FAIL;
    }

    return hr;
}

STDMETHODIMP CExample1::PreCompute(VARIANT_BOOL* pValidPreCompute)
{
    EX_BEGIN_PARAMS()
        EX_OUT_RETVAL_PARAM(pValidPreCompute)
    EX_END_PARAMS()

    *pValidPreCompute = VARIANT_TRUE;
    return S_OK;
}

STDMETHODIMP CExample1::ProcessSignals(IAgStkRadarRcsProcessSignalsParams* processSignalsParams)
{
    EX_BEGIN_PARAMS()
        EX_IN_INTERFACE_PARAM(processSignalsParams)
    EX_END_PARAMS()

    HRESULT hr = S_OK;

    try
    {
        CComPtr<IAgCRSignal> pPrimPolSignal;
        EXCEPTION_HR(processSignalsParams->get_PrimaryPolChannelSignal(&pPrimPolSignal));

        double primPolSigPwr;
        EXCEPTION_HR(pPrimPolSignal->get_Power(&primPolSigPwr));
        EXCEPTION_HR(pPrimPolSignal->put_Power(primPolSigPwr * m_constantRCS));

        CComQIPtr<IAgStkRadarSignal> pPrimPolRdrSignal = pPrimPolSignal;
        if(pPrimPolRdrSignal)
            EXCEPTION_HR(pPrimPolRdrSignal->put_Rcs(m_constantRCS));

        CComPtr<IAgCRPolarization> pPrimPol;
        EXCEPTION_HR(pPrimPolSignal->get_Polarization(&pPrimPol));

        if(pPrimPol)
        {
            CComPtr<IAgCRPolarization> pPrimOrthPol = 0;
            if(m_enablePolarization)
            {
                EXCEPTION_HR(processSignalsParams->ConstructOrthogonalPolarization(pPrimPol, &pPrimOrthPol));
            }
            EXCEPTION_HR(pPrimPolSignal->put_Polarization(pPrimOrthPol));
        }

        CComPtr<IAgCRSignal> pOrthoPolSignal;
        EXCEPTION_HR(processSignalsParams->get_OrthoPolChannelSignal(&pOrthoPolSignal));

        if(pOrthoPolSignal)
        {
            double orthoPolSigPwr;
            EXCEPTION_HR(pOrthoPolSignal->get_Power(&orthoPolSigPwr));
            EXCEPTION_HR(pOrthoPolSignal->put_Power(orthoPolSigPwr * m_constantRCS));

            CComQIPtr<IAgStkRadarSignal> pOrthoPolRdrSignal = pOrthoPolSignal;
            if(pOrthoPolRdrSignal)
                EXCEPTION_HR(pOrthoPolRdrSignal->put_Rcs(m_constantRCS));

            CComPtr<IAgCRPolarization> pOrthoPol;
            EXCEPTION_HR(pOrthoPolSignal->get_Polarization(&pOrthoPol));

            if(pOrthoPol)
            {
                CComPtr<IAgCRPolarization> pOrthoOrthPol = 0;
                if(m_enablePolarization)
                {
                    EXCEPTION_HR(processSignalsParams->ConstructOrthogonalPolarization(pOrthoPol, &pOrthoOrthPol));
                }
                EXCEPTION_HR(pOrthoPolSignal->put_Polarization(pOrthoOrthPol));
            }
        }
    }
    catch( HRESULT r )
    {
        hr = r;
    }
    catch(...)
    {
        hr = E_FAIL;
    }

    return hr;
}

STDMETHODIMP CExample1::Compute(IAgStkRadarRcsComputeParams* computeRcsParams)
{
    EX_BEGIN_PARAMS()
        EX_IN_INTERFACE_PARAM(computeRcsParams)
    EX_END_PARAMS()

    HRESULT hr = S_OK;

    try
    {
        EXCEPTION_HR(computeRcsParams->put_PrimaryChannelRcs(m_constantRCS));
        EXCEPTION_HR(computeRcsParams->put_PrimaryChannelRcsCross(1.0e-20));
        EXCEPTION_HR(computeRcsParams->put_OrthoChannelRcs(m_constantRCS));
        EXCEPTION_HR(computeRcsParams->put_OrthoChannelRcsCross(1.0e-20));
    }
    catch( HRESULT r )
    {
        hr = r;
    }
    catch(...)
    {
        hr = E_FAIL;
    }

    return hr;
}

STDMETHODIMP CExample1::PostCompute()
{
    return S_OK;
}

STDMETHODIMP CExample1::Free()
{
    return S_OK;
}

STDMETHODIMP CExample1::get_IsDynamic(VARIANT_BOOL* pIsDynamic)
{
    EX_BEGIN_PARAMS()
        EX_OUT_RETVAL_PARAM(pIsDynamic)
    EX_END_PARAMS()

    *pIsDynamic = VARIANT_FALSE;
    return S_OK;
}

STDMETHODIMP CExample1::get_ConstantRCS(double* pConstantRCS)
{
    EX_BEGIN_PARAMS()
        EX_OUT_RETVAL_PARAM(pConstantRCS)
    EX_END_PARAMS()

    *pConstantRCS = m_constantRCS;
    return S_OK;
}

STDMETHODIMP CExample1::put_ConstantRCS(double constantRCS)
{
    m_constantRCS = constantRCS;
    return S_OK;
}

STDMETHODIMP CExample1::get_EnablePolarization(VARIANT_BOOL* pEnablePolarization)
{
    EX_BEGIN_PARAMS()
        EX_OUT_RETVAL_PARAM(pEnablePolarization)
    EX_END_PARAMS()

    *pEnablePolarization = m_enablePolarization ? VARIANT_TRUE : VARIANT_FALSE;
    return S_OK;
}

STDMETHODIMP CExample1::put_EnablePolarization(VARIANT_BOOL enablePolarization)
{
    m_enablePolarization = enablePolarization == VARIANT_TRUE ? true : false;
    return S_OK;
}
/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/