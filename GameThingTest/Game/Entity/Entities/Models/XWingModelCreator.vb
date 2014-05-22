Public Class XWingModelCreator : Inherits GeomCreator

    Dim Model As New DrawModel

    Dim BaseLayer() As PointF = {
 New PointF(0.5, 0.9075),
 New PointF(0.505, 0.9125),
 New PointF(0.53, 0.915),
 New PointF(0.565, 0.84),
 New PointF(0.6375, 0.8625),
 New PointF(0.895, 0.805),
 New PointF(0.9025, 0.82),
 New PointF(0.9275, 0.815),
 New PointF(0.9325, 0.7875),
 New PointF(0.94, 0.7775),
 New PointF(0.9375, 0.6425),
 New PointF(0.9275, 0.635),
 New PointF(0.92, 0.295),
 New PointF(0.9125, 0.2975),
 New PointF(0.9025, 0.64),
 New PointF(0.89, 0.64),
 New PointF(0.8925, 0.6775),
 New PointF(0.5675, 0.67),
 New PointF(0.5625, 0.6275),
 New PointF(0.52, 0.0575),
 New PointF(0.505, 0.01),
 New PointF(0.495, 0.01),
 New PointF(0.48, 0.0575),
 New PointF(0.4375, 0.6275),
 New PointF(0.4325, 0.67),
 New PointF(0.1075, 0.6775),
 New PointF(0.11, 0.64),
 New PointF(0.0975, 0.64),
 New PointF(0.0875, 0.2975),
 New PointF(0.08, 0.295),
 New PointF(0.0725, 0.635),
 New PointF(0.0625, 0.6425),
 New PointF(0.06, 0.7775),
 New PointF(0.0675, 0.7875),
 New PointF(0.0725, 0.815),
 New PointF(0.0975, 0.82),
 New PointF(0.105, 0.805),
 New PointF(0.3625, 0.8625),
 New PointF(0.435, 0.84),
 New PointF(0.47, 0.915),
 New PointF(0.495, 0.9125),
 New PointF(0.5, 0.9075)}

    Dim Engines() As PointF = {
 New PointF(0.57, 0.6675),
 New PointF(0.5675, 0.8375),
 New PointF(0.59, 0.8525),
 New PointF(0.5875, 0.965),
 New PointF(0.64, 0.97),
 New PointF(0.6375, 0.87),
 New PointF(0.6575, 0.665),
 New PointF(0.6575, 0.6375),
 New PointF(0.57, 0.6425),
 New PointF(0.5675, 0.665),
 New PointF(0.4325, 0.665),
 New PointF(0.43, 0.6425),
 New PointF(0.3425, 0.6375),
 New PointF(0.3425, 0.665),
 New PointF(0.3625, 0.87),
 New PointF(0.36, 0.97),
 New PointF(0.4125, 0.965),
 New PointF(0.41, 0.8525),
 New PointF(0.4325, 0.8375),
 New PointF(0.43, 0.6675)}


    Dim Hitbox() As PointF = {
 New PointF(0.5025, 0.915),
 New PointF(0.64, 0.9625),
 New PointF(0.6425, 0.8675),
 New PointF(0.9375, 0.7925),
 New PointF(0.895, 0.64),
 New PointF(0.5675, 0.655),
 New PointF(0.5525, 0.0125),
 New PointF(0.4475, 0.0125),
 New PointF(0.4325, 0.655),
 New PointF(0.105, 0.64),
 New PointF(0.0625, 0.7925),
 New PointF(0.3575, 0.8675),
 New PointF(0.36, 0.9625),
 New PointF(0.4975, 0.915)}

    Dim LeftDecalGeom() As PointF = {
 New PointF(0.8275, 0.6775),
 New PointF(0.8775, 0.68),
 New PointF(0.8725, 0.77),
 New PointF(0.775, 0.79),
 New PointF(0.78, 0.78),
 New PointF(0.8225, 0.7625),
 New PointF(0.825, 0.68)}

    Dim RightDecalGeom() As PointF = {
 New PointF(0.175, 0.68),
 New PointF(0.1775, 0.7625),
 New PointF(0.22, 0.78),
 New PointF(0.225, 0.79),
 New PointF(0.1275, 0.77),
 New PointF(0.1225, 0.68),
 New PointF(0.1725, 0.6775)}

    Dim Cockpit() As PointF = {
 New PointF(0.5025, 0.59),
 New PointF(0.51, 0.5925),
 New PointF(0.525, 0.5825),
 New PointF(0.535, 0.545),
 New PointF(0.5225, 0.4175),
 New PointF(0.5025, 0.4175),
 New PointF(0.4975, 0.4175),
 New PointF(0.4775, 0.4175),
 New PointF(0.465, 0.545),
 New PointF(0.475, 0.5825),
 New PointF(0.49, 0.5925),
 New PointF(0.4975, 0.59)}


    Public Overrides Function GetAModel(ByVal Size As System.Drawing.SizeF) As DrawModel

        Dim rendersize = CSng(0.09 * Renderer.ScreenSize.Width)
        Dim size2 As New SizeF(rendersize, rendersize)

        Model.DrawableLayers.Add(New ItemLayer(BaseLayer.ToArray(), Color.White))
        Model.DrawableLayers.Add(New ItemLayer(Engines.ToArray(), Color.Gray))
        Model.DrawableLayers.Add(New ItemLayer(RightDecalGeom.ToArray(), Color.Red)
                                  )
        Model.DrawableLayers.Add(New ItemLayer(LeftDecalGeom.ToArray(), Color.Red))
        Model.DrawableLayers.Add(New ItemLayer(Cockpit.ToArray(), Color.Black, 2))

        Model.UnsizedHitbox = Hitbox.ToArray()
        Model.Hitbox = Hitbox.ToArray()
        Model.UnsizedLaserSpawnPlayers = {New PointF(0.49, 0.0125), New PointF(0.52, 0.0125)}
        Model.Size = size2

        Model.RotateAt = {New PointF(0.5, 0.75)}

        Model.Rotate(180)
        Model.Resize()


        Return Model

    End Function
End Class
