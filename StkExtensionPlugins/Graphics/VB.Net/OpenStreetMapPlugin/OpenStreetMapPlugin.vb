Imports System
Imports System.Drawing
Imports System.Net
Imports System.Runtime.InteropServices
Imports AGI.STKGraphics.Plugins

Namespace OpenStreetMapPlugin
    <Guid("62BEE8AB-65C1-46c4-A2B7-0C229792218E")> _
    <ClassInterface(ClassInterfaceType.None)> _
    <ProgId("OpenStreetMapPlugin.VBNET")> _
    Public Class OpenStreetMapPlugin
        Implements IAgStkGraphicsPluginCustomImageGlobeOverlay

        Sub New()
            m_server = "http://tile.openstreetmap.org"
            m_projection = AgEStkGraphicsPluginMapProjection.eStkGraphicsPluginMapProjectionMercator
            m_extent = New Object() {Math.PI * -1.0, -85.0511 * Math.PI / 180.0, Math.PI, 85.0511 * Math.PI / 180.0}
            m_tiler = New OpenStreetMapTilerPlugin(m_extent)
        End Sub

        Public Function GetURI() As String Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginCustomImageGlobeOverlay.GetURI
            Return m_server
        End Function

        Public Sub OnInitialize(ByVal Scene As Object, ByVal Context As AGI.STKGraphics.Plugins.IAgStkGraphicsPluginCustomImageGlobeOverlayContext) Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginCustomImageGlobeOverlay.OnInitialize
            Context.Extent = m_extent
            Context.Projection = m_projection
            Context.Tiler = m_tiler
        End Sub

        Public Sub OnUninitialize(ByVal Scene As Object, ByVal Context As AGI.STKGraphics.Plugins.IAgStkGraphicsPluginCustomImageGlobeOverlayContext) Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginCustomImageGlobeOverlay.OnUninitialize
        End Sub

        Public Function Read(ByRef Extent As System.Array, ByVal Data As String, ByVal ImageWidth As Integer, ByVal ImageHeight As Integer, ByVal Context As AGI.STKGraphics.Plugins.IAgStkGraphicsPluginCustomImageGlobeOverlayContext) As Boolean Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginCustomImageGlobeOverlay.Read
            If (ImageWidth <> m_tiler.GetRootTile().Width Or ImageHeight <> m_tiler.GetRootTile().Height) Then
                Return False
            End If

            Dim goodread As Boolean = False
            Try
                Dim queryUrl As String = String.Concat(m_server, "/", Data, ".png")
                Dim request As HttpWebRequest = DirectCast(WebRequest.Create(queryUrl), HttpWebRequest)

                request.Method = "GET"
                request.UserAgent = "Insight/1.0"
                request.Timeout = 10000

                Dim response As WebResponse = request.GetResponse()


                If response IsNot Nothing Then
                    Using bmp As New Bitmap(response.GetResponseStream())
                        Context.RasterAsBitmap.SetBitmap(bmp.GetHbitmap())
                        goodread = True
                    End Using
                End If
            Catch ex As WebException
            End Try

            Return goodread
        End Function

        Private ReadOnly m_extent As Array
        Private ReadOnly m_tiler As OpenStreetMapTilerPlugin
        Private ReadOnly m_projection As AgEStkGraphicsPluginMapProjection
        Private ReadOnly m_server As String
    End Class
End Namespace

