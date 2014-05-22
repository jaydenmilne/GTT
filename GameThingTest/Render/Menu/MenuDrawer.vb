Public Class MenuDrawer : Inherits HUDObject

    Public Shared Sub DrawMenu(ByVal TriLeg As Integer)
        Dim MenuToDraw = MainForm.Game.MenuToDraw
        Dim Menuname As String = "Pause"


        Dim MenuFont As New Font(pfc.Families(0), TriLeg, System.Drawing.GraphicsUnit.Pixel)

        If IsNothing(HUD.Geom) Then
            Exit Sub
        End If



        Dim TitleTextLocation As New RectangleF(New PointF(HUD.WidthOffset, HUD.HeightOffset), HUD.MenuSize)


        Dim TitleTextFormat As New StringFormat
        TitleTextFormat.Alignment = StringAlignment.Center

        Buffer.Graphics.FillPolygon(Brushes.Black, HUD.Geom)


        Buffer.Graphics.DrawString(MenuName, MenuFont, Brushes.White, TitleTextLocation, TitleTextFormat)

        Dim FirstLocation As Point = New Point(CInt(HUD.WidthOffset + TriLeg), CInt(TitleTextLocation.Y + Buffer.Graphics.MeasureString("asdf", MenuFont).Height))

        Dim ItemWidth = CInt(ScreenSize.Width - (HUD.WidthOffset * 2 + HUD.TriLeg * 2))

        Dim TotalOffset As Integer = 0

        For Each MenuItem In MenuToDraw

            TotalOffset += MenuItem.Draw(New Point(FirstLocation.X, FirstLocation.Y + TotalOffset), ItemWidth) + CInt(ScreenSize.Height * 0.014)

        Next





    End Sub

    Shared Function DoStuff(ByVal UselessBoolean As Boolean) As Boolean
        End
    End Function






End Class
