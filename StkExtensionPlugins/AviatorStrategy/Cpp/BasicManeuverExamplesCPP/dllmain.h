// dllmain.h : Declaration of module class.

class CBasicManeuverExamplesCPPModule : public ATL::CAtlDllModuleT< CBasicManeuverExamplesCPPModule >
{
public :
	DECLARE_LIBID(LIBID_BasicManeuverExamplesCPPLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_BASICMANEUVEREXAMPLESCPP, "{d0293f97-34cc-4137-91ae-d9f476b2cd0b}")
};

extern class CBasicManeuverExamplesCPPModule _AtlModule;
