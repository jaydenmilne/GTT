<Serializable()>
Public Class LaserModelCreator : Inherits GeomCreator

    Dim Model As New DrawModel

    



    Public Overrides Function GetAModel(ByVal Size As SizeF) As DrawModel

        Dim Points() As PointF = {New PointF(0, 0),
               New PointF(0, Size.Width),
               New PointF(2, Size.Width),
               New PointF(2, 0),
               New PointF(0, 0),
               New PointF(0, Size.Width)}

        Model.UnsizedHitbox = Points.ToArray()
        Model.UnsizedLaserSpawnPlayers = {New PointF(100, 100)}
        Model.DrawableLayers.Add(New ItemLayer(Points.ToArray, Color.Green))
        Model.Size = New SizeF(5, Size.Width)
        Model.Resize()

        Return Model
    End Function
End Class
