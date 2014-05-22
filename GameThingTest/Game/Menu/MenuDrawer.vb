Public Class MenuDrawer : Inherits HUDObject

    Public Shared ABox As MenuItem

    Public Shared Sub DrawMenu(ByVal TriLeg As Integer)


        Dim MenuFont As New Font(pfc.Families(0), TriLeg, System.Drawing.GraphicsUnit.Pixel)

        If IsNothing(HUD.Geom) Then
            Exit Sub
        End If



        Dim TitleTextLocation As New RectangleF(New PointF(HUD.WidthOffset, HUD.HeightOffset), HUD.MenuSize)


        Dim TitleTextFormat As New StringFormat
        TitleTextFormat.Alignment = StringAlignment.Center

        Buffer.Graphics.FillPolygon(Brushes.Black, HUD.Geom)


        Buffer.Graphics.DrawString("Game Paused", MenuFont, Brushes.White, TitleTextLocation, TitleTextFormat)

        Dim Location As Point = New Point(CInt(HUD.WidthOffset + TriLeg), CInt(TitleTextLocation.Y + Buffer.Graphics.MeasureString("asdf", MenuFont).Height))



        ABox = New MenuItem(Location,
                                   CInt(ScreenSize.Width - (HUD.WidthOffset * 2 + HUD.TriLeg * 2)),
                                   "Quit Game", AddressOf DoStuff)


        ABox.Draw()






    End Sub

    Shared Function DoStuff(ByVal UselessBoolean As Boolean) As Boolean
        End
    End Function






End Class
