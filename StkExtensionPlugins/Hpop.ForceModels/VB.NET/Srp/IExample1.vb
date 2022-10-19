'//=====================================================//
'//  Copyright 2005, Analytical Graphics, Inc.          //
'//=====================================================//

Imports System.Array

' NOTE:  Name your custom COM Interface with the identical name as your
' plugin class's name, and append an I to the beginning of it.
Public Interface IExample1

    ' NOTE:  Add your custom COM Interface Property configuration settings here.
    '        Follow the standard VB.NET rules for exposing properties.
    Property MyName() As String
    Property Enabled() As Boolean
    Property DebugMode() As Boolean
    Property MsgInterval() As Integer
    Property SpecularReflectivity() As Double
    Property DiffuseReflectivity() As Double

End Interface

'//=====================================================//
'//  Copyright 2005, Analytical Graphics, Inc.          //
'//=====================================================//