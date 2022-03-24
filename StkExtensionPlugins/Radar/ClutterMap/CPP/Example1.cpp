/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"
#include "Example1.h"
#include "Agi.Radar.ClutterMap.CPP.Example_i.c"

CExample1::CExample1() :
m_constantCoefficient(1.0),
m_applyGrazingMask(false)
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
STDMETHODIMP CExample1::GetPluginConfig(IAgAttrBuilder * pAttrBuilder, IDispatch** ppDispScope)
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
            EXCEPTION_HR(pAttrBuilder->AddQuantityDispatchProperty2(m_pDispScope, CComBSTR("ConstantCoefficient"), CComBSTR("ConstantCoefficient"),
                                                                   CComBSTR("ConstantCoefficient"), CComBSTR("Ratio"), CComBSTR("dB"), CComBSTR("units"), 0));
            EXCEPTION_HR(pAttrBuilder->AddBoolDispatchProperty(m_pDispScope, CComBSTR("ApplyGrazingMask"),
                                                               CComBSTR("ApplyGrazingMask"), CComBSTR("ApplyGrazingMask"), 0));
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

STDMETHODIMP CExample1::VerifyPluginConfig(IAgUtPluginConfigVerifyResult * pPluginCfgResult)
{
    EX_BEGIN_PARAMS()
        EX_IN_INTERFACE_PARAM( pPluginCfgResult )
    EX_END_PARAMS()

    EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
    EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) ); 

    return S_OK;
}
//=========================
// IAgStkRadarClutterMapPlugin Methods
//=========================

STDMETHODIMP CExample1::Initialize(IAgUtPluginSite* site)
{
    EX_BEGIN_PARAMS()
        EX_IN_INTERFACE_PARAM(site)
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

STDMETHODIMP CExample1::Compute(IAgStkRadarClutterMapComputeParams* computeParams)
{
    EX_BEGIN_PARAMS()
        EX_IN_INTERFACE_PARAM(computeParams)
    EX_END_PARAMS()

    HRESULT hr = S_OK;

    try
    {
        CComPtr<IAgCRSignal> pSignal;
        EXCEPTION_HR(computeParams->get_Signal(&pSignal));

        CComPtr<IAgStkRadarClutterPatch> pClutterPatch;
        EXCEPTION_HR(computeParams->get_ClutterPatch(&pClutterPatch));

        double signalPower = 0.0;
        EXCEPTION_HR(pSignal->get_Power(&signalPower));

        double patchArea = 0.0;
        EXCEPTION_HR(pClutterPatch->get_Area(&patchArea));

        double patchClutterCoeff = m_constantCoefficient;
        if(m_applyGrazingMask)
        {
            CComPtr<IAgStkRadarPosVelProvider> pPatchPosVel;
            EXCEPTION_HR(pClutterPatch->get_PosVelProvider(&pPatchPosVel));

            CartVec patchPosCBF;
            EXCEPTION_HR(pPatchPosVel->GetPositionCBF(&patchPosCBF.x, &patchPosCBF.y, &patchPosCBF.z));

            CComPtr<IAgStkRadarLink> pRadarLink;
            EXCEPTION_HR(computeParams->get_RadarLink(&pRadarLink));

            CComPtr<IAgStkRadarLinkGeometry> pRadarLinkGeom;
            EXCEPTION_HR(pRadarLink->get_Geometry(&pRadarLinkGeom));

            CComPtr<IAgStkRadarPosVelProvider> pRdrRcvrPosVel;
            EXCEPTION_HR(pRadarLinkGeom->get_ReceiveRadarPosVelProvider(&pRdrRcvrPosVel));

            CartVec rcvRdrPosCBF;
            EXCEPTION_HR(pRdrRcvrPosVel->GetPositionCBF(&rcvRdrPosCBF.x, &rcvRdrPosCBF.y, &rcvRdrPosCBF.z));

            CartVec relPosCbf = patchPosCBF - rcvRdrPosCBF;

            CartVec surfaceNorm;
            EXCEPTION_HR(pPatchPosVel->GetSurfaceNormalDetic(&surfaceNorm.x, &surfaceNorm.y, &surfaceNorm.z));

            double grazingAngle = AgCHALFPI - CartVec::AngleBetween(surfaceNorm, relPosCbf);
            if(grazingAngle < 0.0)
                grazingAngle = AgCHALFPI;

            patchClutterCoeff = patchClutterCoeff * (grazingAngle / AgCHALFPI);
        }

        EXCEPTION_HR(pSignal->put_Power(signalPower * patchClutterCoeff * patchArea));

        CComPtr<IAgCRPolarization> pSignalPol;
        EXCEPTION_HR(pSignal->get_Polarization(&pSignalPol));
        if(pSignalPol)
        {
            CComPtr<IAgCRPolarization> pSignalOrthoPol;
            EXCEPTION_HR(computeParams->ConstructOrthogonalPolarization(pSignalPol, &pSignalOrthoPol));
            EXCEPTION_HR(pSignal->put_Polarization(pSignalOrthoPol));
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

STDMETHODIMP CExample1::PostCompute()
{
    return S_OK;
}

STDMETHODIMP CExample1::Free()
{
    return S_OK;
}

STDMETHODIMP CExample1::get_ConstantCoefficient(double* pConstantCoefficient)
{
    EX_BEGIN_PARAMS()
        EX_OUT_RETVAL_PARAM(pConstantCoefficient)
    EX_END_PARAMS()

    *pConstantCoefficient = m_constantCoefficient;
    return S_OK;
}

STDMETHODIMP CExample1::put_ConstantCoefficient(double constantCoefficient)
{
    m_constantCoefficient = constantCoefficient;
    return S_OK;
}

STDMETHODIMP CExample1::get_ApplyGrazingMask(VARIANT_BOOL* pApplyGrazingMask)
{
    EX_BEGIN_PARAMS()
        EX_OUT_RETVAL_PARAM(pApplyGrazingMask)
    EX_END_PARAMS()

    *pApplyGrazingMask = m_applyGrazingMask ? VARIANT_TRUE : VARIANT_FALSE;
    return S_OK;
}

STDMETHODIMP CExample1::put_ApplyGrazingMask(VARIANT_BOOL applyGrazingMask)
{
    m_applyGrazingMask = applyGrazingMask == VARIANT_TRUE ? true : false;
    return S_OK;
}

/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/