Imports System.Runtime.Serialization
<Serializable()>
Public Class ItemLayer

    Public IsPolygon As Boolean

    Public UnsizedGeom() As PointF

    Public PublicGeom() As PointF

    Public Color As Color

    Public Width As Integer

    Public DrawThisLayer As Boolean = True

    <NonSerialized()>
    Public DesiredPen As Pen

    '<OnDeserialized()>
    'Sub Reinitialize()
    '    DesiredPen = New Pen(New SolidBrush(Color), Width)
    'End Sub


    Sub New(ByVal UnsizedGeom() As PointF, ByVal Color As Color)
        Me.IsPolygon = True
        Me.UnsizedGeom = UnsizedGeom
        Me.Color = Color
    End Sub

    Sub New(ByVal UnsizedGeom() As PointF, ByVal Color As Color, ByVal Width As Integer)
        Me.IsPolygon = False
        Me.UnsizedGeom = UnsizedGeom
        Me.Color = Color
        Me.Width = Width

        Me.DesiredPen = New Pen(New SolidBrush(Me.Color), Width)

    End Sub

End Class
