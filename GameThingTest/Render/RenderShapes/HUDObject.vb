Public Class HUDObject : Inherits Renderer
    Public TriLeg As Integer
    Dim HudPen As Pen
    Public Size As Size
    Dim Location As Point
    Public WidthOffset As Single
    Public HeightOffset As Single
    Public MenuSize As SizeF
    Public Geom() As PointF


    Sub New(ByVal PassedLocation As Point, ByVal PassedSize As Size)

        If Not Initialized Then
            Throw New Exception("Rectangle thing tried to initialize before the renderer was done. You're bad!")
        End If

        Size = PassedSize
        Location = PassedLocation



        TriLeg = CInt(0.048 * Size.Height)

        HudPen = New Pen(Brushes.White, CSng(Size.Width * 0.002))

        WidthOffset = (ScreenSize.Width - Size.Width) / 2.0F
        HeightOffset = (ScreenSize.Height - Size.Height) / 2.0F


    End Sub

    Public Sub Draw(ByVal DrawMenu As Boolean, MenuToDraw As List(Of MenuItem), ByVal MenuName As String)

        Dim Points() As PointF =
            {
                New PointF(WidthOffset + TriLeg, HeightOffset),
                New PointF(ScreenSize.Width - (WidthOffset + TriLeg), HeightOffset),
                New PointF(ScreenSize.Width - WidthOffset, (HeightOffset + TriLeg)),
                New PointF(ScreenSize.Width - WidthOffset, ScreenSize.Height - (HeightOffset + TriLeg)),
                New PointF(ScreenSize.Width - (WidthOffset + TriLeg), ScreenSize.Height - HeightOffset),
                New PointF(WidthOffset + TriLeg, ScreenSize.Height - HeightOffset),
                New PointF(WidthOffset, ScreenSize.Height - (HeightOffset + TriLeg)),
                New PointF(WidthOffset, HeightOffset + TriLeg),
            New PointF(WidthOffset + TriLeg, HeightOffset)
            }




        If HUDAnimator.CurrentState = HUDAnimator.States.Menu And DrawMenu Then
            MenuDrawer.DrawMenu(TriLeg)
        End If


        Buffer.Graphics.DrawLines(HudPen, Points)

        Geom = Points

        MenuSize = New SizeF(ScreenSize.Width - (WidthOffset * 2), ScreenSize.Height - (HeightOffset * 2))

    End Sub

    Sub New()

    End Sub




End Class
