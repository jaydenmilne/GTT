Public Class MenuItem : Inherits Renderer

    Public Location As Point
    Dim Size As Size
    Dim TriLeg As Integer
    Dim Font As Font
    Dim text As String
    Dim Points() As PointF
    Dim OnClickAction As Func(Of Boolean)
    Dim ShowBorder As Boolean = True
    Dim TextColor As Color = Color.White



    Sub New(ByVal PassedString As String, ByVal PassedAction As Func(Of Boolean), ByVal ShowBorder As Boolean, ByVal TextColor As Color)
        Me.New(PassedString, PassedAction)
        Me.ShowBorder = ShowBorder
        Me.TextColor = TextColor
    End Sub

    Sub New(ByVal PassedString As String, ByVal PassedAction As Func(Of Boolean), ByVal TextColor As Color)
        Me.New(PassedString, PassedAction)
        Me.TextColor = TextColor
    End Sub

    Sub New(ByVal PassedString As String, ByVal PassedAction As Func(Of Boolean), ByVal ShowBorder As Boolean)
        Me.New(PassedString, PassedAction)
        Me.ShowBorder = ShowBorder
    End Sub

    Sub New(ByVal PassedString As String, ByVal ShowBorder As Boolean)
        Me.New(PassedString, AddressOf Input.DoNothing)
        Me.ShowBorder = ShowBorder
    End Sub

    Sub New(ByVal PassedString As String, ByVal ShowBorder As Boolean, ByVal TextColor As Color)
        Me.New(PassedString, AddressOf Input.DoNothing)
        Me.ShowBorder = ShowBorder
        Me.TextColor = TextColor
    End Sub

    Sub New(ByVal PassedString As String, ByVal PassedAction As Func(Of Boolean))


        TriLeg = CInt(0.003 * ScreenSize.Height)

        OnClickAction = PassedAction
        'use Graphics.MeasureString to ensure that it dosen't run out of the box
        'eventually

        text = PassedString
        Font = New Font(pfc.Families(0), CInt(0.03 * ScreenSize.Height), System.Drawing.GraphicsUnit.Pixel)






    End Sub


    Function Draw(ByVal PassedLoc As Point, ByVal PassedWidth As Integer) As Integer

        Location = PassedLoc
        Size.Height = CInt(Buffer.Graphics.MeasureString(text, Font, PassedWidth).Height)
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

        If ShowBorder Then
            Buffer.Graphics.DrawLines(Pens.White, Points)
        End If


        Dim TextBrush As New SolidBrush(TextColor)


        If DrawingUtils.PointInPolygon(Cursor.Position, Points) And ShowBorder Then

            If Input.MouseStates(Input.Mouse.Left) Then
                OnClickAction()
            End If

            Buffer.Graphics.FillPolygon(Brushes.White, Points)
            TextBrush = CType(Brushes.Black, SolidBrush)

        End If

        Dim TextFormat As New StringFormat()
        Dim BoxRectangle As New Rectangle(Location, Size)

        TextFormat.Alignment = StringAlignment.Center
        TextFormat.LineAlignment = StringAlignment.Center

        Buffer.Graphics.DrawString(text, Font, TextBrush, BoxRectangle, TextFormat)

        Return BoxRectangle.Height

    End Function

End Class
