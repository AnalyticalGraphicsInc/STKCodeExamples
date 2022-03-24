// Agi.Radar.ClutterGeometry.CPP.Example.cpp : Implementation of DLL Exports.


#include "stdafx.h"
#include "resource.h"
#include <initguid.h>
#include "Agi.Radar.ClutterGeometry.CPP.Example_i.c"
#include "Agi.Radar.ClutterGeometry.CPP.Example.h"

class CAgiRadarClutterGeometryCPPExampleModule : public CAtlDllModuleT< CAgiRadarClutterGeometryCPPExampleModule >
{
public :
	DECLARE_LIBID(LIBID_AgiRadarClutterGeometryCPPExampleLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_AGIRADARCLUTTERGEOMETRYCPPEXAMPLE, "{742C78BB-B5CC-44E5-8E1C-7CC52666C0A5}")
};

CAgiRadarClutterGeometryCPPExampleModule _AtlModule;

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
