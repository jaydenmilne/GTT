<Serializable()>
Public Class TriangleModelCreator : Inherits GeomCreator


    Dim Points() As PointF = {New PointF(0, 0),
                           New PointF(0.5, 1),
                           New PointF(1, 0),
                           New PointF(0.5, 0.5),
                           New PointF(0, 0),
                           New PointF(0.5, 1)} ' this is actually critically important for the collision detection system

    Public TriDrawModel As New DrawModel

    Sub PopulateGeom()
        Dim rendersize = CSng(0.03 * Renderer.ScreenSize.Width)
        Dim size As New SizeF(rendersize * 0.75F, rendersize)

        TriDrawModel.UnsizedLaserSpawnPlayers = {New PointF(0.5, 0)}

        TriDrawModel.UnsizedHitbox = Points.ToArray()
        ' THIS IS CRITICALLY IMPORTANT!!!!
        TriDrawModel.Hitbox = Points.ToArray()

        TriDrawModel.Size = size
        TriDrawModel.DrawableLayers.Add(New ItemLayer(Points, Color.Red))
        TriDrawModel.Resize()



    End Sub

    Overrides Function GetAModel(ByVal Size As SizeF) As DrawModel
        PopulateGeom()
        Return TriDrawModel
    End Function

    Sub New()

    End Sub
End Class
