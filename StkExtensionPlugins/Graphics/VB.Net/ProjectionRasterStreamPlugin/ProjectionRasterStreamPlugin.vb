Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Text
Imports AGI.STKGraphics.Plugins

<Guid("6DE9FCA4-95F0-41d2-BE84-46CFF18767EB")> _
<ClassInterface(ClassInterfaceType.None)> _
<ProgId("ProjectionRasterStreamPlugin.VBNET")> _
Public Class ProjectionRasterStreamPlugin
    Implements IAgStkGraphicsPluginProjectionStream
    Implements IAgStkGraphicsPluginRasterStream
    Public Sub New()
        ' Set default values
        NearPlane = 1.0
        FarPlane = 10000
        FieldOfViewHorizontal = 0.5
        FieldOfViewVertical = 0.5
        Dates = New List(Of Double)()
        Positions = New List(Of Array)()
        Orientations = New List(Of Array)()
    End Sub

#Region "IAgStkGraphicsPluginProjectionStream Members"

    Private Function IAgStkGraphicsPluginProjectionStream_OnGetFirstProjection(ByVal Time As AGI.STKUtil.IAgDate, ByVal pContext As IAgStkGraphicsPluginProjectionStreamContext) As Boolean Implements IAgStkGraphicsPluginProjectionStream.OnGetFirstProjection
        pContext.NearPlane = NearPlane
        pContext.FarPlane = FarPlane
        pContext.FieldOfViewHorizontal = FieldOfViewHorizontal
        pContext.FieldOfViewVertical = FieldOfViewVertical

        Dim result As ProjectionPositionOrientation = EvaluateProjectionPositionOrientation(Double.Parse(Time.Format("epSec")), 0, Dates.Count)
        Dim xyz As Array = result.Position
        Dim quat As Array = result.Orientation

        pContext.SetPosition(xyz)
        pContext.SetOrientation(quat)

        Return True
    End Function

    Private Function IAgStkGraphicsPluginProjectionStream_OnGetNextProjection(ByVal Time As AGI.STKUtil.IAgDate, ByVal NextTime As AGI.STKUtil.IAgDate, ByVal pContext As IAgStkGraphicsPluginProjectionStreamContext) As Boolean Implements IAgStkGraphicsPluginProjectionStream.OnGetNextProjection
        pContext.NearPlane = NearPlane
        pContext.FarPlane = FarPlane
        pContext.FieldOfViewHorizontal = FieldOfViewHorizontal
        pContext.FieldOfViewVertical = FieldOfViewVertical

        Dim result As ProjectionPositionOrientation = EvaluateProjectionPositionOrientation(Double.Parse(Time.Format("epSec")), 0, Dates.Count)
        Dim xyz As Array = result.Position
        Dim quat As Array = result.Orientation

        pContext.SetPosition(xyz)
        pContext.SetOrientation(quat)

        Return True
    End Function

#End Region

#Region "IAgStkGraphicsPluginRasterStream Members"

    Private Function IAgStkGraphicsPluginRasterStream_OnGetRasterAttributes(ByVal pAttributes As IAgStkGraphicsPluginRasterStreamAttributes) As Boolean Implements IAgStkGraphicsPluginRasterStream.OnGetRasterAttributes
        ' Set gifProvider for the raster
        gifProvider = New GifProvider(RasterPath)

        ' Assign the raster's attributes
        pAttributes.Width = gifProvider.Size.Width
        pAttributes.Height = gifProvider.Size.Height
        pAttributes.RasterFormat = AgEStkGraphicsPluginRasterFormat.eStkGraphicsPluginRasterFormatBgra
        pAttributes.RasterType = AgEStkGraphicsPluginRasterType.eStkGraphicsPluginRasterTypeUnsignedByte

        Return True
    End Function

    Private Function IAgStkGraphicsPluginRasterStream_OnGetNextRaster(ByVal Time As AGI.STKUtil.IAgDate, ByVal NextTime As AGI.STKUtil.IAgDate, ByVal pContext As IAgStkGraphicsPluginRasterStreamContext) As Boolean Implements IAgStkGraphicsPluginRasterStream.OnGetNextRaster
        If m_LastTime Is Nothing Then
            m_LastTime = Time.Subtract("sec", 1.0)
        End If

        ' Only update if animating
        If Time.OLEDate > m_LastTime.OLEDate Then
            pContext.RasterAsBitmap.SetBitmap(gifProvider.NextFrame().GetHbitmap())
            m_LastTime = Time
        ElseIf Time.OLEDate < m_LastTime.OLEDate Then
            pContext.RasterAsBitmap.SetBitmap(gifProvider.PreviousFrame().GetHbitmap())
            m_LastTime = Time
        End If

        Return True
    End Function

#End Region

    Private Function EvaluateProjectionPositionOrientation(ByVal searchDate As Double, ByVal startIndex As Integer, ByVal searchLength As Integer) As ProjectionPositionOrientation
        ' Find the midpoint of the length
        Dim midpoint As Integer = startIndex + (searchLength \ 2)

        ' Base cases
        If Dates(startIndex) = searchDate Then
            Return New ProjectionPositionOrientation(Positions(startIndex), Orientations(startIndex))
        End If
        If Dates(midpoint) = searchDate Then
            Return New ProjectionPositionOrientation(Positions(midpoint), Orientations(midpoint))
        End If
        If searchLength = 3 Then
            If searchDate < Dates(midpoint) Then
                Return InterpolatePositionAndOrientation(midpoint - 1, midpoint, searchDate)
            Else
                Return InterpolatePositionAndOrientation(midpoint, midpoint + 1, searchDate)
            End If
        End If
        If searchLength = 2 Then
            InterpolatePositionAndOrientation(startIndex, startIndex + 1, searchDate)
        End If
        If searchLength < 2 Then
            ' This will only occur if animating backwards, so try to interpolate with the lower index first.
            If startIndex - 1 > 0 Then
                Return InterpolatePositionAndOrientation(startIndex, startIndex + 1, searchDate)
            ElseIf startIndex + 1 < Dates.Count Then
                Return InterpolatePositionAndOrientation(startIndex - 1, startIndex, searchDate)
            Else
                Return New ProjectionPositionOrientation(Positions(startIndex), Orientations(startIndex))
            End If
        End If

        ' Normal case: binary search
        If searchDate < Dates(midpoint) Then
            Return EvaluateProjectionPositionOrientation(searchDate, startIndex, midpoint - startIndex)
        Else
            Return EvaluateProjectionPositionOrientation(searchDate, midpoint + 1, startIndex + searchLength - (midpoint + 1))
        End If
    End Function

    ''' <summary>
    ''' Interpolates a ProjectionPositionOrientation that would suffice at a <paramref name="desiredDate"/> between
    ''' Dates[<paramref name="lowerIndex"/>] and Dates[<paramref name="higherIndex"/>].
    ''' </summary>
    ''' <param name="lowerIndex">The index reprenting the position in Dates that serves as the lower bound of interpolation.</param>
    ''' <param name="higherIndex">The index reprenting the position in Dates that serves as the upper bound of interpolation.</param>
    ''' <param name="desiredDate">The date to interpolate for.</param>
    ''' <returns>A ProjectionPositionOrientation containing the interpolated position and orientation.</returns>
    Private Function InterpolatePositionAndOrientation(ByVal lowerIndex As Integer, ByVal higherIndex As Integer, ByVal desiredDate As Double) As ProjectionPositionOrientation
        Dim range As Double = Dates(higherIndex) - Dates(lowerIndex)
        Dim locationInRange As Double = desiredDate - Dates(lowerIndex)
        Dim ratio As Double = locationInRange / range

        Return New ProjectionPositionOrientation(InterpolatePosition(Positions(lowerIndex), Positions(higherIndex), ratio), InterpolateOrientation(Orientations(lowerIndex), Orientations(higherIndex), ratio))
    End Function

    ''' <summary>
    ''' This function will calculate an interpolated cartesian position according to the linear interpolation (LERP) formula
    ''' such that its location is an amount <paramref name="t"/> between <paramref name="pos1"/> and <paramref name="pos2"/>.
    ''' </summary>
    ''' <param name="pos1">The first IAgCartesian3Vector. Serves as the lower position of the interpolation.</param>
    ''' <param name="pos2">The second IAgCartesian3Vector. Serves as the uppoer position of the interpolation.</param>
    ''' <param name="t">The amount of interpolation (i.e. the desired amount between <paramref name="pos1"/> and <paramref name="pos2"/> to calculate).</param>
    ''' <returns>An Array of cartesian position components that is an amount <paramref name="t"/> between <paramref name="pos1"/> and <paramref name="pos2"/>.</returns>
    Private Shared Function InterpolatePosition(ByVal pos1 As Array, ByVal pos2 As Array, ByVal t As Double) As Array
        Return New Object() {CDbl(pos1.GetValue(0)) + (CDbl(pos2.GetValue(0)) - CDbl(pos1.GetValue(0))) * t, CDbl(pos1.GetValue(1)) + (CDbl(pos2.GetValue(1)) - CDbl(pos1.GetValue(1))) * t, CDbl(pos1.GetValue(2)) + (CDbl(pos2.GetValue(2)) - CDbl(pos1.GetValue(2))) * t}
    End Function

    ''' <summary>
    ''' This function will calculate an interpolated quaternion according to the spherical linear interpolation (SLERP) formula
    ''' such that its values lay an amount <paramref name="t"/> between <paramref name="q1"/> and <paramref name="q2"/>.
    ''' </summary>
    ''' <param name="q1">The first IAgOrientation. Serves as the lower quaterion of the interpolation.</param>
    ''' <param name="q2">the second IAgOrientation. Serves as the upper quaterion of the interpolation.</param>
    ''' <param name="t">The amount of interpolation (i.e. the desired amount between <paramref name="q1"/> and <paramref name="q1"/> to calculate).</param>
    ''' <returns>An Array of quaterion components that lay an amount <paramref name="t"/> between <paramref name="q1"/> and <paramref name="q2"/>.</returns>
    Private Shared Function InterpolateOrientation(ByVal q1 As Array, ByVal q2 As Array, ByVal t As Double) As Array
        Dim EPS As Double = 0.001

        Dim theta As Double, cosineTheta As Double, sineTheta As Double, scale0 As Double, scale1 As Double
        Dim qi As Double() = New Double(3) {}

        Dim q1x As Double = CDbl(q1.GetValue(0)), q1y As Double = CDbl(q1.GetValue(1)), q1z As Double = CDbl(q1.GetValue(2)), q1w As Double = CDbl(q1.GetValue(3))

        Dim q2x As Double = CDbl(q2.GetValue(0)), q2y As Double = CDbl(q2.GetValue(1)), q2z As Double = CDbl(q2.GetValue(2)), q2w As Double = CDbl(q2.GetValue(3))

        ' Do a linear interpolation between two quaternions (0 <= t <= 1).
        cosineTheta = q1x * q2x + q1y * q2y + q1z * q2z + q1w * q2w
        ' dot product
        If cosineTheta < 0 Then
            cosineTheta = -cosineTheta
            qi(0) = -q2x
            qi(1) = -q2y
            qi(2) = -q2z
            qi(3) = -q2w
        Else
            qi(0) = q2x
            qi(1) = q2y
            qi(2) = q2z
            qi(3) = q2w
        End If

        If (1 - cosineTheta) <= EPS Then
            ' If the quaternions are really close, do a simple linear interpolation.
            scale0 = 1 - t
            scale1 = t
        Else
            ' Otherwise SLERP.
            theta = CSng(Math.Acos(cosineTheta))
            sineTheta = CSng(Math.Sin(theta))
            scale0 = CSng(Math.Sin((1 - t) * theta)) / sineTheta
            scale1 = CSng(Math.Sin(t * theta)) / sineTheta
        End If

        ' Calculate interpolated quaternion:
        Dim resultX As Double, resultY As Double, resultZ As Double, resultW As Double
        resultX = scale0 * q1x + scale1 * qi(0)
        resultY = scale0 * q1y + scale1 * qi(1)
        resultZ = scale0 * q1z + scale1 * qi(2)
        resultW = scale0 * q1w + scale1 * qi(3)

        Return New Object() {resultX, resultY, resultZ, resultW}
    End Function

    Public Property NearPlane() As Double
        Get
            Return m_NearPlane
        End Get
        Set(ByVal value As Double)
            m_NearPlane = value
        End Set
    End Property
    Private m_NearPlane As Double
    Public Property FarPlane() As Double
        Get
            Return m_FarPlane
        End Get
        Set(ByVal value As Double)
            m_FarPlane = value
        End Set
    End Property
    Private m_FarPlane As Double
    Public Property FieldOfViewHorizontal() As Double
        Get
            Return m_FieldOfViewHorizontal
        End Get
        Set(ByVal value As Double)
            m_FieldOfViewHorizontal = value
        End Set
    End Property
    Private m_FieldOfViewHorizontal As Double
    Public Property FieldOfViewVertical() As Double
        Get
            Return m_FieldOfViewVertical
        End Get
        Set(ByVal value As Double)
            m_FieldOfViewVertical = value
        End Set
    End Property
    Private m_FieldOfViewVertical As Double
    Public Property Dates() As IList(Of Double)
        Get
            Return m_Dates
        End Get
        Set(ByVal value As IList(Of Double))
            m_Dates = value
        End Set
    End Property
    Private m_Dates As IList(Of Double)
    Public Property Positions() As IList(Of Array)
        Get
            Return m_Positions
        End Get
        Set(ByVal value As IList(Of Array))
            m_Positions = value
        End Set
    End Property
    Private m_Positions As IList(Of Array)
    Public Property Orientations() As IList(Of Array)
        Get
            Return m_Orientations
        End Get
        Set(ByVal value As IList(Of Array))
            m_Orientations = value
        End Set
    End Property
    Private m_Orientations As IList(Of Array)

    Public Property RasterPath() As String
        Get
            Return m_RasterPath
        End Get
        Set(ByVal value As String)
            m_RasterPath = value
        End Set
    End Property
    Private m_RasterPath As String
    Private gifProvider As GifProvider
    Private m_LastTime As AGI.STKUtil.IAgDate
End Class
