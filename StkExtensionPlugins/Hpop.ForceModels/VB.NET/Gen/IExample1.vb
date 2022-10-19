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
        Property VectorName() As String
        Property AccelRefFrame() As String

        ReadOnly Property AccelRefFrameChoices() As Object()

        Property AccelX() As Double
        Property AccelY() As Double
        Property AccelZ() As Double

        Property MsgStatus() As Boolean
        Property EvalMsgInterval() As Integer
        Property PostEvalMsgInterval() As Integer
        Property PreNextMsgInterval() As Integer

    End Interface

'//=====================================================//
'//  Copyright 2005, Analytical Graphics, Inc.          //
'//=====================================================//