Imports System.Drawing.Drawing2D
<Serializable()>
Public Class DrawModel

    Public DrawableLayers As New List(Of ItemLayer)

    Public Hitbox() As PointF

    Public UnsizedHitbox() As PointF


    Public LaserSpawnPlaces() As PointF
    Public UnsizedLaserSpawnPlayers() As PointF

    ' this is an array b/c TransForm points can only do arrays
    ' this is also used for computing where it is in relation to other ships
    ' because I need to git er done

    Public RotateAt() As PointF = {New PointF(0.5, 0.5)}


    Sub New()

    End Sub



    Public Size As SizeF

    Public Sub Resize()
        Dim TransMatrix As New Matrix
        TransMatrix.Scale(Size.Width, Size.Height)

        For Each Item In DrawableLayers
            TransMatrix.TransformPoints(Item.UnsizedGeom)
        Next

        TransMatrix.TransformPoints(UnsizedHitbox)

        TransMatrix.TransformPoints(UnsizedLaserSpawnPlayers)

        TransMatrix.TransformPoints(RotateAt)


    End Sub

    Public Sub Rotate(ByVal Angle As Single)
        Dim TransMatrix As New Matrix
        TransMatrix.RotateAt(Angle, New PointF(0.5, 0.5))
        For Each layer In DrawableLayers
            TransMatrix.TransformPoints(layer.UnsizedGeom)
        Next

        TransMatrix.TransformPoints(RotateAt)
        TransMatrix.TransformPoints(UnsizedHitbox)
        TransMatrix.TransformPoints(UnsizedLaserSpawnPlayers)


    End Sub

End Class
