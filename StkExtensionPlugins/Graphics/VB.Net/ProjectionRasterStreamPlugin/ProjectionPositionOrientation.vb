Imports System.Collections.Generic
Imports System.Text
Imports AGI.STKUtil

Public Class ProjectionPositionOrientation
	Public Sub New(position As Array, rotation As Array)
		m_Position = position
		m_Orientation = rotation
	End Sub

	Public Property Position() As Array
		Get
			Return m_Position
		End Get
		Set
			m_Position = value
		End Set
	End Property

	Public Property Orientation() As Array
		Get
			Return m_Orientation
		End Get
		Set
			m_Orientation = value
		End Set
	End Property

	Private m_Position As Array
	Private m_Orientation As Array
End Class
