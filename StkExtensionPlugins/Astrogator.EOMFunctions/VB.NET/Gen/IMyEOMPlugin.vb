'//=====================================================//
'//  Copyright 2009, Analytical Graphics, Inc.          //
'//=====================================================//

Imports System.Array


    ' NOTE:  Name your custom COM Interface with the identical name as your
    ' plugin class's name, and append an I to the beginning of it.
Public Interface IMyEOMPlugin

    ' NOTE:  Add your custom COM Interface Property configuration settings here.
    '        Follow the standard VB.NET rules for exposing properties.

    Property DeltaVAxes() As String


End Interface

'//=====================================================//
'//  Copyright 2009, Analytical Graphics, Inc.          //
'//=====================================================//