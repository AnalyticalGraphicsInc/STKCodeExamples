/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
#pragma once
#include "resource.h"       // main symbols
#include "Agi.Radar.RCS.CPP.Example.h"

class ATL_NO_VTABLE CExample1 : 
    public CComObjectRootEx<CComSingleThreadModel>,
    public CComCoClass<CExample1, &CLSID_Example1>,
    public IDispatchImpl<IExample1, &IID_IExample1, &LIBID_AgiRadarRCSCPPExampleLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
    public IAgUtPluginConfig,
    public IAgStkRadarRcsPlugin
{
    public:

        DECLARE_REGISTRY_RESOURCEID(IDR_EXAMPLE1)
        DECLARE_PROTECT_FINAL_CONSTRUCT()

        BEGIN_COM_MAP(CExample1)
            COM_INTERFACE_ENTRY(IExample1)
            COM_INTERFACE_ENTRY(IAgUtPluginConfig)
            COM_INTERFACE_ENTRY(IAgStkRadarRcsPlugin)
            COM_INTERFACE_ENTRY(IDispatch)
        END_COM_MAP()

        //====================
        // C++ and ATL method
        //====================
        CExample1();
        ~CExample1();
        HRESULT FinalConstruct();
        void FinalRelease();

        //===========================
        // IAgUtPluginConfig Methods
        //===========================
        STDMETHOD(GetPluginConfig)(IAgAttrBuilder* pAttrBuilder, IDispatch** ppDispScope);
        STDMETHOD(VerifyPluginConfig)(IAgUtPluginConfigVerifyResult* pPluginCfgResult);

        //===================================
        // IAgStkRadarRcsPlugin Methods
        //===================================
        STDMETHOD(Initialize)(IAgUtPluginSite* site);
        STDMETHOD(PreCompute)(VARIANT_BOOL* pValidPreCompute);
        STDMETHOD(ProcessSignals)(IAgStkRadarRcsProcessSignalsParams* processSignalsParams);
        STDMETHOD(Compute)(IAgStkRadarRcsComputeParams* computeRcsParams);
        STDMETHOD(PostCompute)();
        STDMETHOD(Free)();
        STDMETHOD(get_IsDynamic)(VARIANT_BOOL* pIsDynamic);

        //==================================
        // Defined in IExample1
        //==================================
        STDMETHOD(get_ConstantRCS)(double* pConstantRCS);
        STDMETHOD(put_ConstantRCS)(double constantRCS);
        STDMETHOD(get_EnablePolarization)(VARIANT_BOOL* pEnablePolarization);
        STDMETHOD(put_EnablePolarization)(VARIANT_BOOL enablePolarization);

    private:

        CComPtr<IAgUtPluginSite> m_pUtPluginSite;
        CComPtr<IAgStkPluginSite> m_pStkPluginSite;
        CComPtr<IAgCrdnPluginProvider> m_pVectorToolProvider;
        CComPtr<IAgCrdnPluginCalcProvider> m_pCalcToolProvider;
        CComPtr<IDispatch> m_pDispScope;

        bool m_enablePolarization;
        double m_constantRCS;
};

#define EX_HR(exp)                                                       \
{                                                                        \
    HRESULT _hresult = exp;                                              \
    if( FAILED(_hresult) )                                               \
    {                                                                    \
        return _hresult;                                                 \
    }                                                                    \
}                                                                        \

#define EXCEPTION_HR(exp)                                                \
{                                                                        \
    HRESULT _hresult = exp;                                              \
    if( FAILED(_hresult) )                                               \
    {                                                                    \
        throw _hresult;                                                  \
    }                                                                    \
}                                                                        \

#define EX_BEGIN_PARAMS()                                                \
{                                                                        \
    HRESULT hr = S_OK;                                                   \

#define EX_OUT_RETVAL_PARAM(p)                                           \
    if (p == 0)                                                          \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_OUT_RETVAL_INTERFACE_PARAM(p)                                 \
    if (p)                                                               \
    {                                                                    \
        *p = 0;                                                          \
    }                                                                    \
    else                                                                 \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_OUT_PARAM(p)                                                  \
    if (p == 0)                                                          \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_OUT_INTERFACE_PARAM(p)                                        \
    if (p)                                                               \
    {                                                                    \
        *p = 0;                                                          \
    }                                                                    \
    else                                                                 \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_IN_INTERFACE_PARAM(p)                                         \
    if (p == 0)                                                          \
    {                                                                    \
        hr = E_INVALIDARG;                                               \
    }                                                                    \

#define EX_INOUT_INTERFACE_PARAM(p)                                      \
    if (p == 0)                                                          \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_IN_ARRAY_PARAM(p)                                             \
    if (p.vt != (VT_ARRAY | VT_VARIANT) &&                               \
        p.vt != (VT_ARRAY | VT_VARIANT | VT_BYREF))                      \
    {                                                                    \
        hr = E_INVALIDARG;                                               \
    }                                                                    \

#define EX_IN_BSTR_PARAM(p)                                              \
    if (p == 0)                                                          \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_END_PARAMS()                                                  \
    if (FAILED(hr))                                                      \
    {                                                                    \
        return hr;                                                       \
    }                                                                    \
}                                                                        \

OBJECT_ENTRY_AUTO(__uuidof(Example1), CExample1)

/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
