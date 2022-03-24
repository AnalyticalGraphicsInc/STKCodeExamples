// Agi.Radar.RCS.CPP.Example_VS2008.cpp : Implementation of DLL Exports.


#include "stdafx.h"
#include "resource.h"
#include <initguid.h>
#include "Agi.Radar.RCS.CPP.Example_i.c"
#include "Agi.Radar.RCS.CPP.Example.h"

class CAgiRadarRCSCPPExampleModule : public CAtlDllModuleT< CAgiRadarRCSCPPExampleModule >
{
public :
	DECLARE_LIBID(LIBID_AgiRadarRCSCPPExampleLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_AGIRADARRCSCPPEXAMPLE, "{C3A56D64-27E2-4524-91FB-B30A3D151598}")
};

CAgiRadarRCSCPPExampleModule _AtlModule;

// DLL Entry Point
extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	hInstance;
	return _AtlModule.DllMain(dwReason, lpReserved); 
}

// Used to determine whether the DLL can be unloaded by OLE
STDAPI DllCanUnloadNow(void)
{
    return _AtlModule.DllCanUnloadNow();
}

// Returns a class factory to create an object of the requested type
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
    return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}


// DllRegisterServer - Adds entries to the system registry
STDAPI DllRegisterServer(void)
{
    // registers object, typelib and all interfaces in typelib
    HRESULT hr = _AtlModule.DllRegisterServer();
	return hr;
}

// DllUnregisterServer - Removes entries from the system registry
STDAPI DllUnregisterServer(void)
{
	HRESULT hr = _AtlModule.DllUnregisterServer();
	return hr;
}

