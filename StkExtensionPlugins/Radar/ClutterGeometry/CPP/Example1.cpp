/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"
#include "Example1.h"
#include "Agi.Radar.ClutterGeometry.CPP.Example_i.c"

CExample1::CExample1() :
m_offsetAngle(0.01745329251994329576923690768489),
m_patchArea(1.0)
{
}

CExample1::~CExample1()
{
}

HRESULT CExample1::FinalConstruct()
{
    return m_pCbIntersectComputeParams.CoCreateInstance(__uuidof(AgStkRadarCBIntersectComputeParams));
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
            EXCEPTION_HR(pAttrBuilder->AddQuantityDispatchProperty2(m_pDispScope, CComBSTR("PatchArea"), CComBSTR("PatchArea"),
                                                                   CComBSTR("PatchArea"), CComBSTR("Rcs"), CComBSTR("sqm"), CComBSTR("sqm"), 0));
            EXCEPTION_HR(pAttrBuilder->AddQuantityDispatchProperty2(m_pDispScope, CComBSTR("OffsetAngle"), CComBSTR("OffsetAngle"),
                                                                   CComBSTR("OffsetAngle"), CComBSTR("Angle"), CComBSTR("deg"), CComBSTR("rad"), 0));
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
// IAgStkRadarClutterGeometryPlugin Methods
//=========================
STDMETHODIMP CExample1::Register(IAgStkRadarClutterGeometryPluginRegInfo* registrationInfo)
{
    EX_BEGIN_PARAMS()
        EX_IN_INTERFACE_PARAM(registrationInfo)
    EX_END_PARAMS()

    EX_HR(registrationInfo->put_ValidRadarSystems(eStkRadarAllSystems));

    return S_OK;
}

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

STDMETHODIMP CExample1::Compute(IAgStkRadarClutterGeometryComputeParams* computeParams)
{
    EX_BEGIN_PARAMS()
        EX_IN_INTERFACE_PARAM(computeParams)
    EX_END_PARAMS()

    HRESULT hr = S_OK;

    try
    {
        CComPtr<IAgStkRadarLink> pRadarLink;
        EXCEPTION_HR(computeParams->get_RadarLink(&pRadarLink));

        CComPtr<IAgStkRadarLinkGeometry> pRadarLinkGeom;
        EXCEPTION_HR(pRadarLink->get_Geometry(&pRadarLinkGeom));

        CComPtr<IAgStkRadarPosVelProvider> pXmtRdrPosVel;
        EXCEPTION_HR(pRadarLinkGeom->get_TransmitRadarPosVelProvider(&pXmtRdrPosVel));

        CartVec xmtRdrPosCBF;
        EXCEPTION_HR(pXmtRdrPosVel->GetPositionCBF(&xmtRdrPosCBF.x, &xmtRdrPosCBF.y, &xmtRdrPosCBF.z));

        EXCEPTION_HR(m_pCbIntersectComputeParams->SetBasePositionCBF(xmtRdrPosCBF.x, xmtRdrPosCBF.y, xmtRdrPosCBF.z));

        CComPtr<IAgStkRadarClutterPatchCollection> pPatchCollection;
        EXCEPTION_HR(computeParams->get_ClutterPatches(&pPatchCollection));

       VARIANT_BOOL intersectFound;
       double sinOffset = sin(AgCHALFPI - m_offsetAngle);
       double cosOffset = cos(AgCHALFPI - m_offsetAngle);

       //==============================  First Point Start ======================================================
       CartVec pt1Cbf;
       EXCEPTION_HR(pXmtRdrPosVel->ConvertBodyCartesianToCBFCartesian(cosOffset, 0.0, sinOffset, &pt1Cbf.x, &pt1Cbf.y, &pt1Cbf.z));
       EXCEPTION_HR(m_pCbIntersectComputeParams->SetDirectionCBF(pt1Cbf.x, pt1Cbf.y, pt1Cbf.z));

       CComPtr<IAgStkRadarCBIntersectComputeResult> pIntersectResult1;
       EXCEPTION_HR(pXmtRdrPosVel->ComputeCentralBodyIntersect(m_pCbIntersectComputeParams, &pIntersectResult1));
       EXCEPTION_HR(pIntersectResult1->get_IntersectionFound(&intersectFound));
       if(intersectFound == VARIANT_TRUE)
       {
           CComPtr<IAgStkRadarClutterPatch> pClutterPatch;
           EXCEPTION_HR(pPatchCollection->Add(&pClutterPatch));

           CartVec intersectPt;
           EXCEPTION_HR(pIntersectResult1->GetIntercept1CBF(&intersectPt.x, &intersectPt.y, &intersectPt.z));
           EXCEPTION_HR(pClutterPatch->SetPositionCBF(intersectPt.x, intersectPt.y, intersectPt.z));
           EXCEPTION_HR(pClutterPatch->put_Area(m_patchArea));
       }
       //==============================  Second Point Start ======================================================
       CartVec pt2Cbf;
       EXCEPTION_HR(pXmtRdrPosVel->ConvertBodyCartesianToCBFCartesian(-cosOffset, 0.0, sinOffset, &pt2Cbf.x, &pt2Cbf.y, &pt2Cbf.z));
       EXCEPTION_HR(m_pCbIntersectComputeParams->SetDirectionCBF(pt2Cbf.x, pt2Cbf.y, pt2Cbf.z));

       CComPtr<IAgStkRadarCBIntersectComputeResult> pIntersectResult2;
       EXCEPTION_HR(pXmtRdrPosVel->ComputeCentralBodyIntersect(m_pCbIntersectComputeParams, &pIntersectResult2));
       EXCEPTION_HR(pIntersectResult2->get_IntersectionFound(&intersectFound));
       if(intersectFound == VARIANT_TRUE)
       {
           CComPtr<IAgStkRadarClutterPatch> pClutterPatch;
           EXCEPTION_HR(pPatchCollection->Add(&pClutterPatch));

           CartVec intersectPt;
           EXCEPTION_HR(pIntersectResult2->GetIntercept1CBF(&intersectPt.x, &intersectPt.y, &intersectPt.z));
           EXCEPTION_HR(pClutterPatch->SetPositionCBF(intersectPt.x, intersectPt.y, intersectPt.z));
           EXCEPTION_HR(pClutterPatch->put_Area(m_patchArea));
       }
       //==============================  Third Point Start ======================================================
       CartVec pt3Cbf;
       EXCEPTION_HR(pXmtRdrPosVel->ConvertBodyCartesianToCBFCartesian(0.0, cosOffset, sinOffset, &pt3Cbf.x, &pt3Cbf.y, &pt3Cbf.z));
       EXCEPTION_HR(m_pCbIntersectComputeParams->SetDirectionCBF(pt3Cbf.x, pt3Cbf.y, pt3Cbf.z));

       CComPtr<IAgStkRadarCBIntersectComputeResult> pIntersectResult3;
       EXCEPTION_HR(pXmtRdrPosVel->ComputeCentralBodyIntersect(m_pCbIntersectComputeParams, &pIntersectResult3));
       EXCEPTION_HR(pIntersectResult3->get_IntersectionFound(&intersectFound));
       if(intersectFound == VARIANT_TRUE)
       {
           CComPtr<IAgStkRadarClutterPatch> pClutterPatch;
           EXCEPTION_HR(pPatchCollection->Add(&pClutterPatch));

           CartVec intersectPt;
           EXCEPTION_HR(pIntersectResult3->GetIntercept1CBF(&intersectPt.x, &intersectPt.y, &intersectPt.z));
           EXCEPTION_HR(pClutterPatch->SetPositionCBF(intersectPt.x, intersectPt.y, intersectPt.z));
           EXCEPTION_HR(pClutterPatch->put_Area(m_patchArea));
       }
       //==============================  Fourth Point Start ======================================================
       CartVec pt4Cbf;
       EXCEPTION_HR(pXmtRdrPosVel->ConvertBodyCartesianToCBFCartesian(0.0, -cosOffset, sinOffset, &pt4Cbf.x, &pt4Cbf.y, &pt4Cbf.z));
       EXCEPTION_HR(m_pCbIntersectComputeParams->SetDirectionCBF(pt4Cbf.x, pt4Cbf.y, pt4Cbf.z));

       CComPtr<IAgStkRadarCBIntersectComputeResult> pIntersectResult4;
       EXCEPTION_HR(pXmtRdrPosVel->ComputeCentralBodyIntersect(m_pCbIntersectComputeParams, &pIntersectResult4));
       EXCEPTION_HR(pIntersectResult4->get_IntersectionFound(&intersectFound));
       if(intersectFound == VARIANT_TRUE)
       {
           CComPtr<IAgStkRadarClutterPatch> pClutterPatch;
           EXCEPTION_HR(pPatchCollection->Add(&pClutterPatch));

           CartVec intersectPt;
           EXCEPTION_HR(pIntersectResult4->GetIntercept1CBF(&intersectPt.x, &intersectPt.y, &intersectPt.z));
           EXCEPTION_HR(pClutterPatch->SetPositionCBF(intersectPt.x, intersectPt.y, intersectPt.z));
           EXCEPTION_HR(pClutterPatch->put_Area(m_patchArea));
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

STDMETHODIMP CExample1::get_PatchArea(double* pPatchArea)
{
    EX_BEGIN_PARAMS()
        EX_OUT_RETVAL_PARAM(pPatchArea)
    EX_END_PARAMS()

    *pPatchArea = m_patchArea;
    return S_OK;
}

STDMETHODIMP CExample1::put_PatchArea(double patchArea)
{
    m_patchArea = patchArea;
    return S_OK;
}

STDMETHODIMP CExample1::get_OffsetAngle(double* pOffsetAngle)
{
    EX_BEGIN_PARAMS()
        EX_OUT_RETVAL_PARAM(pOffsetAngle)
    EX_END_PARAMS()

    *pOffsetAngle = m_offsetAngle;
    return S_OK;
}

STDMETHODIMP CExample1::put_OffsetAngle(double offsetAngle)
{
    m_offsetAngle = offsetAngle;
    return S_OK;
}
/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/