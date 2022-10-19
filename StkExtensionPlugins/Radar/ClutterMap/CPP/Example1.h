/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
#pragma once
#include "resource.h"       // main symbols
#include "Agi.Radar.ClutterMap.CPP.Example.h"
#include <math.h>

#define AgCHALFPI 1.5707963267948966192

class ATL_NO_VTABLE CExample1 : 
    public CComObjectRootEx<CComSingleThreadModel>,
    public CComCoClass<CExample1, &CLSID_Example1>,
    public IDispatchImpl<IExample1, &IID_IExample1, &LIBID_AgiRadarClutterMapCPPExampleLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
    public IAgUtPluginConfig,
    public IAgStkRadarClutterMapPlugin
{
    public:
        struct CartVec
        {
            CartVec() : x(0.0), y(0.0), z(0.0) {}

            CartVec operator-(const CartVec& rhs) const
            {
                CartVec result;
                result.x = x - rhs.x;
                result.y = y - rhs.y;
                result.z = z - rhs.z;
                return result;
            }

            CartVec Cross(const CartVec& rhs) const
            {
                CartVec result;
                result.x = y * rhs.z - z * rhs.y;
                result.y = z * rhs.x - x * rhs.z;
                result.z = x * rhs.y - y * rhs.x;
                return result;
            }

            double Mag() const
            {
                return sqrt(x * x + y * y + z * z);
            }

            double Dot(const CartVec& rhs) const
            {
                return x * rhs.x + y * rhs.y + z * rhs.z;
            }

            static double AngleBetween(const CartVec& lhs, const CartVec& rhs)
            {
                CartVec cross = lhs.Cross(rhs);
                double sinTheta = cross.Mag();
                double cosTheta = lhs.Dot(rhs);
                return fabs(atan2(sinTheta, cosTheta));
            }

            double x;
            double y;
            double z;
        };

        DECLARE_REGISTRY_RESOURCEID(IDR_EXAMPLE1)
        DECLARE_PROTECT_FINAL_CONSTRUCT()

        BEGIN_COM_MAP(CExample1)
            COM_INTERFACE_ENTRY(IExample1)
            COM_INTERFACE_ENTRY(IAgUtPluginConfig)
            COM_INTERFACE_ENTRY(IAgStkRadarClutterMapPlugin)
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
        STDMETHOD(Compute)(IAgStkRadarClutterMapComputeParams* computeParams);
        STDMETHOD(PostCompute)();
        STDMETHOD(Free)();

        //==================================
        // Defined in IExample1
        //==================================
        STDMETHOD(get_ConstantCoefficient)(double* pConstantCoefficient);
        STDMETHOD(put_ConstantCoefficient)(double constantCoefficient);
        STDMETHOD(get_ApplyGrazingMask)(VARIANT_BOOL* pApplyGrazingMask);
        STDMETHOD(put_ApplyGrazingMask)(VARIANT_BOOL applyGrazingMask);

    private:

        CComPtr<IAgUtPluginSite> m_pUtPluginSite;
        CComPtr<IAgStkPluginSite> m_pStkPluginSite;
        CComPtr<IAgCrdnPluginProvider> m_pVectorToolProvider;
        CComPtr<IAgCrdnPluginCalcProvider> m_pCalcToolProvider;
        CComPtr<IDispatch> m_pDispScope;

        double m_constantCoefficient;
        bool m_applyGrazingMask;
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
