Public Class MenuItem : Inherits Renderer

    Public Location As Point
    Dim Size As Size
    Dim TriLeg As Integer
    Dim Font As Font
    Dim text As String
    Dim Points() As PointF
    Dim OnClickAction As Func(Of Boolean)

    Sub New(ByVal PassedString As String, ByVal PassedAction As Func(Of Boolean))


        TriLeg = CInt(0.003 * ScreenSize.Height)

        OnClickAction = PassedAction
        'use Graphics.MeasureString to ensure that it dosen't run out of the box
        'eventually

        text = PassedString
        Font = New Font(pfc.Families(0), CInt(0.03 * ScreenSize.Height), System.Drawing.GraphicsUnit.Pixel)

        Size.Height = CInt(Buffer.Graphics.MeasureString(text, Font).Height)




    End Sub


    Function Draw(ByVal PassedLoc As Point, ByVal PassedWidth As Integer) As Integer

        Location = PassedLoc
        Size.Width = PassedWidth

        Points =
                            {
                                New PointF(Location.X + TriLeg, Location.Y),
                                New PointF(Location.X + (Size.Width - TriLeg), Location.Y),
                                New PointF(Location.X + Size.Width, Location.Y + TriLeg),
                                New PointF(Location.X + Size.Width, Location.Y + (Size.Height - TriLeg)),
                                New PointF(Location.X + (Size.Width - TriLeg), Location.Y + Size.Height),
                                New PointF(Location.X + TriLeg, Location.Y + (Size.Height)),
                                New PointF(Location.X, Location.Y + (Size.Height - TriLeg)),
                                New PointF(Location.X, Location.Y + TriLeg),
                                New PointF(Location.X + TriLeg, Location.Y)
                            }


        Buffer.Graphics.DrawLines(Pens.White, Points)

        Dim TextColor As Brush = Brushes.White

        If DrawingUtils.PointInPolygon(Cursor.Position, Points) Then

            If Input.MouseStates(Input.Mouse.Left) Then
                OnClickAction()
            End If

            Buffer.Graphics.FillPolygon(Brushes.White, Points)
            TextColor = Brushes.Black

        End If

        Dim TextFormat As New StringFormat()
        Dim BoxRectangle As New Rectangle(Location, Size)

        TextFormat.Alignment = StringAlignment.Center
        TextFormat.LineAlignment = StringAlignment.Center

        Buffer.Graphics.DrawString(text, Font, TextColor, BoxRectangle, TextFormat)

        Return BoxRectangle.Height

    End Function

End Class
