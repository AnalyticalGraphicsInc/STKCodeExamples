// BasicManeuverStrategyFactory3dExampleNavCPP.h : Declaration of the CBasicManeuverStrategyFactory3dExampleNavCPP

#pragma once
#include "resource.h"       // main symbols



#include "BasicManeuverExamplesCPP_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CBasicManeuverStrategyFactory3dExampleNavCPP

class ATL_NO_VTABLE CBasicManeuverStrategyFactory3dExampleNavCPP :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CBasicManeuverStrategyFactory3dExampleNavCPP, &CLSID_BasicManeuverStrategyFactory3dExampleNavCPP>,
	public IBasicManeuverStrategyFactory3dExampleNavCPP,
	public IAgFlightBasicManeuverStrategyFactory
{
public:
	CBasicManeuverStrategyFactory3dExampleNavCPP()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_BASICMANEUVERSTRATEGYFACTORY3DEXAMPLENAVCPP)


BEGIN_COM_MAP(CBasicManeuverStrategyFactory3dExampleNavCPP)
	COM_INTERFACE_ENTRY(IBasicManeuverStrategyFactory3dExampleNavCPP)
    COM_INTERFACE_ENTRY(IAgFlightBasicManeuverStrategyFactory)
END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:




    // Inherited via IAgFlightBasicManeuverStrategyFactory
    virtual HRESULT __stdcall get_StrategyType(BSTR* pVal) override;

    virtual HRESULT __stdcall get_IsNavigationStrategy(VARIANT_BOOL* pVal) override;

    virtual HRESULT __stdcall get_IsProfileStrategy(VARIANT_BOOL* pVal) override;

    virtual HRESULT __stdcall CanCreateStrategy(IAgFlightProcedure* pParentProc, IAgFlightBasicManeuverStrategy* pNavStrategy, BSTR* pErrorMessage) override;

    virtual HRESULT __stdcall CreateStrategy(IAgFlightProcedure* pParentProc, IAgFlightBasicManeuverStrategy* pNavStrategy, IAgFlightBasicManeuverStrategy** ppNewStrategy) override;

};

OBJECT_ENTRY_AUTO(__uuidof(BasicManeuverStrategyFactory3dExampleNavCPP), CBasicManeuverStrategyFactory3dExampleNavCPP)
