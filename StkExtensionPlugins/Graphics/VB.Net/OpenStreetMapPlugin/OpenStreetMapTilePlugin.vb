Imports System
Imports System.Drawing
Imports System.Net
Imports System.Runtime.InteropServices
Imports AGI.STKGraphics.Plugins

Namespace OpenStreetMapPlugin
    Class OpenStreetMapTilePlugin
        Implements IAgStkGraphicsPluginTile

        Public Sub New(ByVal extent As Array, ByVal width As Integer, ByVal Height As Integer, ByVal children As Short, ByVal data As String)
            m_extent = extent
            m_width = width
            m_height = Height
            m_children = children
            m_data = data
        End Sub

        Public ReadOnly Property Children() As Short Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginTile.Children
            Get
                Return m_children
            End Get
        End Property

        Public ReadOnly Property Data() As String Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginTile.Data
            Get
                Return m_data
            End Get
        End Property

        Public ReadOnly Property Extent() As System.Array Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginTile.Extent
            Get
                Return m_extent
            End Get
        End Property

        Public ReadOnly Property Height() As Integer Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginTile.Height
            Get
                Return m_height
            End Get
        End Property

        Public ReadOnly Property Width() As Integer Implements AGI.STKGraphics.Plugins.IAgStkGraphicsPluginTile.Width
            Get
                Return m_width
            End Get
        End Property

        Private m_extent As Array
        Private m_width As Integer
        Private m_height As Integer
        Private m_children As Short
        Private m_data As String
    End Class
End Namespace