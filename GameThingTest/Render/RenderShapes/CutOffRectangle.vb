

Public Class CutOffRectangle : Inherits Renderer

    '
    '
    '
    '
    '
    '
    'DONT USE THIS FOR NOW
    '
    '
    '
    '
    '
    '
    '
    '
    '

    Public Location As Point
    Dim Size As Size
    Dim TriLeg As Integer
    Dim Font As Font
    Dim text As String

    Sub New(ByVal PassedLoc As Point, ByVal PassedWidth As Integer, ByVal PassedString As String)

        Location = PassedLoc
        Size.Width = PassedWidth
        TriLeg = CInt(0.003 * ScreenSize.Height)


        'use Graphics.MeasureString to ensure that it dosen't run out of the box
        'eventually

        text = PassedString
        Font = New Font(pfc.Families(0), CInt(0.03 * ScreenSize.Height), System.Drawing.GraphicsUnit.Pixel)

        'Dim counter As Integer = 0

        'While Buffer.Graphics.MeasureString(text, Font).Width > Size.Width

        '    Font = New Font(Font.FontFamily, Size.Height - counter, System.Drawing.GraphicsUnit.Pixel)
        '    counter += 1

        'End While

        Size.Height = CInt(Buffer.Graphics.MeasureString(text, Font).Height)



    End Sub


    Sub Draw()

        Dim Points() As PointF =
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
                End
            End If

            Buffer.Graphics.FillPolygon(Brushes.White, Points)
            TextColor = Brushes.Black

        End If

        Dim TextFormat As New StringFormat()
        Dim BoxRectangle As New Rectangle(Location, Size)

        TextFormat.Alignment = StringAlignment.Center
        TextFormat.LineAlignment = StringAlignment.Center

        Buffer.Graphics.DrawString(text, Font, TextColor, BoxRectangle, TextFormat)


    End Sub

End Class
