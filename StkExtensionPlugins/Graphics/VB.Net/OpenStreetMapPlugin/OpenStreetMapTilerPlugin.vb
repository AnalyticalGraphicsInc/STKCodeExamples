Imports System
Imports System.Drawing
Imports System.Net
Imports System.Runtime.InteropServices
Imports AGI.STKGraphics.Plugins

Namespace OpenStreetMapPlugin
    Class OpenStreetMapTilerPlugin
        Implements IAgStkGraphicsPluginTiler

        Sub New(ByVal extent As Array)
            m_extent = extent
            m_rootTile = New OpenStreetMapTileplugin(extent, m_tilesize, m_tilesize, 4, "0/0/0")
        End Sub

        Public Function GetRootTile() As AGI.STKGraphics.Plugins.IAgStkGraphicsPluginTile Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginTiler.GetRootTile
            Return m_rootTile
        End Function

        Public Sub GetTiles(ByVal ParentTile As AGI.STKGraphics.Plugins.IAgStkGraphicsPluginTile, ByVal Tiles As AGI.STKGraphics.Plugins.IAgStkGraphicsPluginTileCollection) Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginTiler.GetTiles
            Dim data As String = ParentTile.Data
            Dim firstSlash As Integer = data.IndexOf("/")
            Dim lastSlash As Integer = data.LastIndexOf("/")

            Dim zoom As Integer = Int32.Parse(data.Substring(0, firstSlash))
            Dim x As Integer = Int32.Parse(data.Substring(firstSlash + 1, lastSlash - firstSlash - 1))
            Dim y As Integer = Int32.Parse(data.Substring(lastSlash + 1))

            Tiles.Add(CreateTile(x << 1, y << 1, zoom + 1))
            Tiles.Add(CreateTile(x << 1 Or 1, y << 1, zoom + 1))
            Tiles.Add(CreateTile(x << 1, y << 1 Or 1, zoom + 1))
            Tiles.Add(CreateTile(x << 1 Or 1, y << 1 Or 1, zoom + 1))
        End Sub

        Private Function CreateTile(ByVal x As Integer, ByVal y As Integer, ByVal zoom As Integer)
            '
            'Latitude
            '
            Dim invzoom As Double = 4.0 * Math.PI / (1 << zoom)
            Dim k As Double = Math.Exp((2.0 * Math.PI) - (y * invzoom))
            Dim north As Double = Math.Asin((k - 1.0) / (k + 1.0))
            k = Math.Exp((2.0 * Math.PI) - ((y + 1) * invzoom))
            Dim south As Double = Math.Asin((k - 1.0) / (k + 1.0))

            '
            'Longitude
            '
            invzoom = Math.PI / (1 << (zoom - 1))
            Dim west As Double = (CDbl(x) * invzoom) - Math.PI
            Dim east As Double = (CDbl(x + 1) * invzoom) - Math.PI
            Dim numChildren As Short = 0

            If zoom + 1 < m_maxlevels Then
                numChildren = 4
            End If

            Dim extent As Array = New Object() {west, south, east, north}

            Return New OpenStreetMapTilePlugin(extent, m_tilesize, m_tilesize, numChildren, zoom.ToString() + "/" + x.ToString() + "/" + y.ToString())
        End Function

        Private m_extent As Array
        Private m_rootTile As OpenStreetMapTileplugin
        Private Shared ReadOnly m_tilesize As Integer = 256
        Private Shared ReadOnly m_maxlevels As Integer = 19
    End Class
End Namespace
