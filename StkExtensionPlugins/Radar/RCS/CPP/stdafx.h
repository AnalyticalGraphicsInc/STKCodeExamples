// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently,
// but are changed infrequently

#pragma once

#ifndef STRICT
#define STRICT
#endif

// Modify the following defines if you have to target a platform prior to the ones specified below.
// Refer to MSDN for the latest info on corresponding values for different platforms.
#ifndef WINVER				// Allow use of features specific to Windows 7 or later.
#define WINVER 0x0601		// Change this to the appropriate value to target other Windows versions.
#endif

#ifndef _WIN32_WINNT		// Allow use of features specific to Windows 7 or later.
#define _WIN32_WINNT 0x0601		// Change this to the appropriate value to target other Windows versions.
#endif						

#ifndef _WIN32_WINDOWS		// Allow use of features specific to Windows 7 or later.
#define _WIN32_WINDOWS 0x0601 // Change this to the appropriate value to target other Windows versions.
#endif

#ifndef _WIN32_IE			// Allow use of features specific to IE 8.0 or later.
#define _WIN32_IE 0x0800	// Change this to the appropriate value to target other IE versions.
#endif

#define _ATL_APARTMENT_THREADED
#define _ATL_NO_AUTOMATIC_NAMESPACE

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS	// some CString constructors will be explicit

// turns off ATL's hiding of some common and often safely ignored warning messages
#define _ATL_ALL_WARNINGS
#define _ATL_APARTMENT_THREADED
#define _ATL_NO_AUTOMATIC_NAMESPACE

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS	// some CString constructors will be explicit

#include "resource.h"
#include <atlbase.h>
#include <atlcom.h>
#include <atlctl.h>
#include "AgAttrAutomation.tlh"
#include "AgUtPlugin.tlh"
#include "AgCrdnPlugin.tlh"
#include "AgCommRdrFoundation.tlh"
#include "AgSTKRadar.tlh"
#include "AgStkPlugin.tlh"

using namespace ATL;
