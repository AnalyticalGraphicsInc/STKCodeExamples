

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Mon Jan 18 22:14:07 2038
 */
/* Compiler settings for BasicManeuverExamplesCPP.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 8.01.0622 
    protocol : all , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */


#ifndef __BasicManeuverExamplesCPP_i_h__
#define __BasicManeuverExamplesCPP_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IBasicManeuverStrategyFactory3dExampleNavCPP_FWD_DEFINED__
#define __IBasicManeuverStrategyFactory3dExampleNavCPP_FWD_DEFINED__
typedef interface IBasicManeuverStrategyFactory3dExampleNavCPP IBasicManeuverStrategyFactory3dExampleNavCPP;

#endif 	/* __IBasicManeuverStrategyFactory3dExampleNavCPP_FWD_DEFINED__ */


#ifndef __IBasicManeuverStrategyFactory3dExampleProfileCPP_FWD_DEFINED__
#define __IBasicManeuverStrategyFactory3dExampleProfileCPP_FWD_DEFINED__
typedef interface IBasicManeuverStrategyFactory3dExampleProfileCPP IBasicManeuverStrategyFactory3dExampleProfileCPP;

#endif 	/* __IBasicManeuverStrategyFactory3dExampleProfileCPP_FWD_DEFINED__ */


#ifndef __IBasicManeuverStrategy3dExampleNavCPP_FWD_DEFINED__
#define __IBasicManeuverStrategy3dExampleNavCPP_FWD_DEFINED__
typedef interface IBasicManeuverStrategy3dExampleNavCPP IBasicManeuverStrategy3dExampleNavCPP;

#endif 	/* __IBasicManeuverStrategy3dExampleNavCPP_FWD_DEFINED__ */


#ifndef __IBasicManeuverStrategy3dExampleProfileCPP_FWD_DEFINED__
#define __IBasicManeuverStrategy3dExampleProfileCPP_FWD_DEFINED__
typedef interface IBasicManeuverStrategy3dExampleProfileCPP IBasicManeuverStrategy3dExampleProfileCPP;

#endif 	/* __IBasicManeuverStrategy3dExampleProfileCPP_FWD_DEFINED__ */


#ifndef __BasicManeuverStrategyFactory3dExampleNavCPP_FWD_DEFINED__
#define __BasicManeuverStrategyFactory3dExampleNavCPP_FWD_DEFINED__

#ifdef __cplusplus
typedef class BasicManeuverStrategyFactory3dExampleNavCPP BasicManeuverStrategyFactory3dExampleNavCPP;
#else
typedef struct BasicManeuverStrategyFactory3dExampleNavCPP BasicManeuverStrategyFactory3dExampleNavCPP;
#endif /* __cplusplus */

#endif 	/* __BasicManeuverStrategyFactory3dExampleNavCPP_FWD_DEFINED__ */


#ifndef __BasicManeuverStrategyFactory3dExampleProfileCPP_FWD_DEFINED__
#define __BasicManeuverStrategyFactory3dExampleProfileCPP_FWD_DEFINED__

#ifdef __cplusplus
typedef class BasicManeuverStrategyFactory3dExampleProfileCPP BasicManeuverStrategyFactory3dExampleProfileCPP;
#else
typedef struct BasicManeuverStrategyFactory3dExampleProfileCPP BasicManeuverStrategyFactory3dExampleProfileCPP;
#endif /* __cplusplus */

#endif 	/* __BasicManeuverStrategyFactory3dExampleProfileCPP_FWD_DEFINED__ */


#ifndef __BasicManeuverStrategy3dExampleNavCPP_FWD_DEFINED__
#define __BasicManeuverStrategy3dExampleNavCPP_FWD_DEFINED__

#ifdef __cplusplus
typedef class BasicManeuverStrategy3dExampleNavCPP BasicManeuverStrategy3dExampleNavCPP;
#else
typedef struct BasicManeuverStrategy3dExampleNavCPP BasicManeuverStrategy3dExampleNavCPP;
#endif /* __cplusplus */

#endif 	/* __BasicManeuverStrategy3dExampleNavCPP_FWD_DEFINED__ */


#ifndef __BasicManeuverStrategy3dExampleProfileCPP_FWD_DEFINED__
#define __BasicManeuverStrategy3dExampleProfileCPP_FWD_DEFINED__

#ifdef __cplusplus
typedef class BasicManeuverStrategy3dExampleProfileCPP BasicManeuverStrategy3dExampleProfileCPP;
#else
typedef struct BasicManeuverStrategy3dExampleProfileCPP BasicManeuverStrategy3dExampleProfileCPP;
#endif /* __cplusplus */

#endif 	/* __BasicManeuverStrategy3dExampleProfileCPP_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "shobjidl.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __BasicManeuverExamplesCPPLib_LIBRARY_DEFINED__
#define __BasicManeuverExamplesCPPLib_LIBRARY_DEFINED__

/* library BasicManeuverExamplesCPPLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_BasicManeuverExamplesCPPLib;

#ifndef __IBasicManeuverStrategyFactory3dExampleNavCPP_INTERFACE_DEFINED__
#define __IBasicManeuverStrategyFactory3dExampleNavCPP_INTERFACE_DEFINED__

/* interface IBasicManeuverStrategyFactory3dExampleNavCPP */
/* [unique][nonextensible][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IBasicManeuverStrategyFactory3dExampleNavCPP;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("ce4dc251-35d4-437d-bf91-0f3c532fc8c6")
    IBasicManeuverStrategyFactory3dExampleNavCPP : public IUnknown
    {
    public:
    };
    
    
#else 	/* C style interface */

    typedef struct IBasicManeuverStrategyFactory3dExampleNavCPPVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IBasicManeuverStrategyFactory3dExampleNavCPP * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IBasicManeuverStrategyFactory3dExampleNavCPP * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IBasicManeuverStrategyFactory3dExampleNavCPP * This);
        
        END_INTERFACE
    } IBasicManeuverStrategyFactory3dExampleNavCPPVtbl;

    interface IBasicManeuverStrategyFactory3dExampleNavCPP
    {
        CONST_VTBL struct IBasicManeuverStrategyFactory3dExampleNavCPPVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IBasicManeuverStrategyFactory3dExampleNavCPP_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IBasicManeuverStrategyFactory3dExampleNavCPP_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IBasicManeuverStrategyFactory3dExampleNavCPP_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IBasicManeuverStrategyFactory3dExampleNavCPP_INTERFACE_DEFINED__ */


#ifndef __IBasicManeuverStrategyFactory3dExampleProfileCPP_INTERFACE_DEFINED__
#define __IBasicManeuverStrategyFactory3dExampleProfileCPP_INTERFACE_DEFINED__

/* interface IBasicManeuverStrategyFactory3dExampleProfileCPP */
/* [unique][nonextensible][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IBasicManeuverStrategyFactory3dExampleProfileCPP;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("9f94a67a-0e38-4e8b-9941-f30212cf859c")
    IBasicManeuverStrategyFactory3dExampleProfileCPP : public IUnknown
    {
    public:
    };
    
    
#else 	/* C style interface */

    typedef struct IBasicManeuverStrategyFactory3dExampleProfileCPPVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IBasicManeuverStrategyFactory3dExampleProfileCPP * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IBasicManeuverStrategyFactory3dExampleProfileCPP * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IBasicManeuverStrategyFactory3dExampleProfileCPP * This);
        
        END_INTERFACE
    } IBasicManeuverStrategyFactory3dExampleProfileCPPVtbl;

    interface IBasicManeuverStrategyFactory3dExampleProfileCPP
    {
        CONST_VTBL struct IBasicManeuverStrategyFactory3dExampleProfileCPPVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IBasicManeuverStrategyFactory3dExampleProfileCPP_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IBasicManeuverStrategyFactory3dExampleProfileCPP_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IBasicManeuverStrategyFactory3dExampleProfileCPP_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IBasicManeuverStrategyFactory3dExampleProfileCPP_INTERFACE_DEFINED__ */


#ifndef __IBasicManeuverStrategy3dExampleNavCPP_INTERFACE_DEFINED__
#define __IBasicManeuverStrategy3dExampleNavCPP_INTERFACE_DEFINED__

/* interface IBasicManeuverStrategy3dExampleNavCPP */
/* [unique][nonextensible][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IBasicManeuverStrategy3dExampleNavCPP;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("a4b0baf8-eba2-44a1-ad9e-d7bc15fc38dd")
    IBasicManeuverStrategy3dExampleNavCPP : public IUnknown
    {
    public:
    };
    
    
#else 	/* C style interface */

    typedef struct IBasicManeuverStrategy3dExampleNavCPPVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IBasicManeuverStrategy3dExampleNavCPP * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IBasicManeuverStrategy3dExampleNavCPP * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IBasicManeuverStrategy3dExampleNavCPP * This);
        
        END_INTERFACE
    } IBasicManeuverStrategy3dExampleNavCPPVtbl;

    interface IBasicManeuverStrategy3dExampleNavCPP
    {
        CONST_VTBL struct IBasicManeuverStrategy3dExampleNavCPPVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IBasicManeuverStrategy3dExampleNavCPP_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IBasicManeuverStrategy3dExampleNavCPP_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IBasicManeuverStrategy3dExampleNavCPP_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IBasicManeuverStrategy3dExampleNavCPP_INTERFACE_DEFINED__ */


#ifndef __IBasicManeuverStrategy3dExampleProfileCPP_INTERFACE_DEFINED__
#define __IBasicManeuverStrategy3dExampleProfileCPP_INTERFACE_DEFINED__

/* interface IBasicManeuverStrategy3dExampleProfileCPP */
/* [unique][nonextensible][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IBasicManeuverStrategy3dExampleProfileCPP;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("0455ddf3-7ca7-461d-a567-194d15f6b88d")
    IBasicManeuverStrategy3dExampleProfileCPP : public IUnknown
    {
    public:
    };
    
    
#else 	/* C style interface */

    typedef struct IBasicManeuverStrategy3dExampleProfileCPPVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IBasicManeuverStrategy3dExampleProfileCPP * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IBasicManeuverStrategy3dExampleProfileCPP * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IBasicManeuverStrategy3dExampleProfileCPP * This);
        
        END_INTERFACE
    } IBasicManeuverStrategy3dExampleProfileCPPVtbl;

    interface IBasicManeuverStrategy3dExampleProfileCPP
    {
        CONST_VTBL struct IBasicManeuverStrategy3dExampleProfileCPPVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IBasicManeuverStrategy3dExampleProfileCPP_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IBasicManeuverStrategy3dExampleProfileCPP_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IBasicManeuverStrategy3dExampleProfileCPP_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IBasicManeuverStrategy3dExampleProfileCPP_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_BasicManeuverStrategyFactory3dExampleNavCPP;

#ifdef __cplusplus

class DECLSPEC_UUID("d2dd28eb-40ec-4b85-8a52-4a99b2b93399")
BasicManeuverStrategyFactory3dExampleNavCPP;
#endif

EXTERN_C const CLSID CLSID_BasicManeuverStrategyFactory3dExampleProfileCPP;

#ifdef __cplusplus

class DECLSPEC_UUID("3d3751ae-5f3e-4c89-9f71-5a33586fe3f2")
BasicManeuverStrategyFactory3dExampleProfileCPP;
#endif

EXTERN_C const CLSID CLSID_BasicManeuverStrategy3dExampleNavCPP;

#ifdef __cplusplus

class DECLSPEC_UUID("a36bc8e8-2f41-4123-92d4-5d9d0b0e7bc0")
BasicManeuverStrategy3dExampleNavCPP;
#endif

EXTERN_C const CLSID CLSID_BasicManeuverStrategy3dExampleProfileCPP;

#ifdef __cplusplus

class DECLSPEC_UUID("eb8f1dd8-8782-4116-acda-781d44e445a6")
BasicManeuverStrategy3dExampleProfileCPP;
#endif
#endif /* __BasicManeuverExamplesCPPLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


