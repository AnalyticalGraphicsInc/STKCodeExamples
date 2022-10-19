Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class GifProvider
	Public Sub New(imagePath As String)
		Try
            m_Image = DirectCast(Image.FromFile(imagePath), Bitmap)
		Catch generatedExceptionName As System.Exception
		End Try
		m_nFrameCount = m_Image.GetFrameCount(FrameDimension.Time)
	End Sub

	Public Function GetFrame(frame As Integer) As Bitmap
		Try
			m_Image.SelectActiveFrame(FrameDimension.Time, frame)
		Catch generatedExceptionName As System.Exception
		End Try
		Return m_Image
	End Function

	Public Function NextFrame() As Bitmap
		If m_nCurrentFrame = m_nFrameCount - 1 Then
			m_nCurrentFrame = 0
		End If
		Dim frame As Bitmap = GetFrame(m_nCurrentFrame)
		m_nCurrentFrame += 1
		Return frame
	End Function

	Public Function PreviousFrame() As Bitmap
		If m_nCurrentFrame = 0 Then
			m_nCurrentFrame = m_nFrameCount
		End If
		Dim frame As Bitmap = GetFrame(m_nCurrentFrame)
		m_nCurrentFrame -= 1
		Return frame
	End Function

	Public ReadOnly Property Size() As Size
		Get
			Return GetFrame(0).Size
		End Get
	End Property

	Public ReadOnly Property FrameCount() As Integer
		Get
			Return m_nFrameCount
		End Get
	End Property

	Private m_Image As Bitmap
	Private m_nFrameCount As Integer
	Private m_nCurrentFrame As Integer
End Class
