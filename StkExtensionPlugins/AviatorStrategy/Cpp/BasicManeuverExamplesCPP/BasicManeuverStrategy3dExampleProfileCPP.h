// BasicManeuverStrategy3dExampleProfileCPP.h : Declaration of the CBasicManeuverStrategy3dExampleProfileCPP

#pragma once
#include "resource.h"       // main symbols



#include "BasicManeuverExamplesCPP_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CBasicManeuverStrategy3dExampleProfileCPP

class ATL_NO_VTABLE CBasicManeuverStrategy3dExampleProfileCPP :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CBasicManeuverStrategy3dExampleProfileCPP, &CLSID_BasicManeuverStrategy3dExampleProfileCPP>,
	public IBasicManeuverStrategy3dExampleProfileCPP,
    public IPersistPropertyBag,
    public IAgFlightBasicManeuverStrategy
{
public:
	CBasicManeuverStrategy3dExampleProfileCPP()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_BASICMANEUVERSTRATEGY3DEXAMPLEPROFILECPP)


BEGIN_COM_MAP(CBasicManeuverStrategy3dExampleProfileCPP)
	COM_INTERFACE_ENTRY(IBasicManeuverStrategy3dExampleProfileCPP)
    COM_INTERFACE_ENTRY(IAgFlightBasicManeuverStrategy)
    COM_INTERFACE_ENTRY(IPersistPropertyBag)
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




    // Inherited via IPersistPropertyBag
    virtual HRESULT __stdcall GetClassID(CLSID* pClassID) override;

    virtual HRESULT __stdcall InitNew(void) override;

    virtual HRESULT __stdcall Load(IPropertyBag* pPropBag, IErrorLog* pErrorLog) override;

    virtual HRESULT __stdcall Save(IPropertyBag* pPropBag, BOOL fClearDirty, BOOL fSaveAllProperties) override;


    // Inherited via IAgFlightBasicManeuverStrategy
    virtual HRESULT __stdcall get_StrategyType(BSTR* pVal) override;

    virtual HRESULT __stdcall get_StrategyDescription(BSTR* pVal) override;

    virtual HRESULT __stdcall get_IsNavigationStrategy(VARIANT_BOOL* pVal) override;

    virtual HRESULT __stdcall get_IsProfileStrategy(VARIANT_BOOL* pVal) override;

    virtual HRESULT __stdcall Configure(IAgFlightProcedure* pParentProc, IAgFlightBasicManeuverStrategy* pOtherStrategy) override;

    virtual HRESULT __stdcall GetValidProfileStrategyFactories(IAgComUnkCollection* pRegisteredStratFactories, IAgComUnkCollection** ppValidStratFactories) override;

    virtual HRESULT __stdcall SetInitialState(AgEFlightPerformanceMode ePerfMode, AgEFlightPhaseOfFlight ePhaseOfFlight, IAgFlightBasicManeuverState* pInitState, IAgFlightDoubleArray* presultAuxInitState) override;

    virtual HRESULT __stdcall QueryDerivatives(IAgFlightBasicManeuverState* pCurrentState, IAgFlightBasicManeuverDerivs* presultCurrentDerivs, VARIANT_BOOL vbAllowThresholdDetection, VARIANT_BOOL* pvbThresholdCrossed) override;

    virtual HRESULT __stdcall RefineIntegrateThreshold(AgEFlightIntegThresholdType eThresholdType, IAgFlightBasicManeuverState* pLastGoodState, IAgFlightBasicManeuverState* pCurrentState, VARIANT_BOOL* pvbContinue) override;

    virtual HRESULT __stdcall DataIsHeld(IUnknown* pDataUnknown, VARIANT_BOOL* vbIsHeld) override;

    virtual HRESULT __stdcall GetSpecialTimes(SAFEARRAY** pTimes) override;

    virtual HRESULT __stdcall Copy() override;

    virtual HRESULT __stdcall get_CanPaste(VARIANT_BOOL* pVal) override;

    virtual HRESULT __stdcall Paste() override;

};

OBJECT_ENTRY_AUTO(__uuidof(BasicManeuverStrategy3dExampleProfileCPP), CBasicManeuverStrategy3dExampleProfileCPP)
