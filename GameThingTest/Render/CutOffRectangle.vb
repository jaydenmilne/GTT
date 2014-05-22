Public Class CutOffRectangle : Inherits Renderer
    Public Inset As Integer
    Dim TriLeg As Integer
    Dim HudPen As Pen
    Dim Size As Size
    Dim MyRect As Rectangle
    Dim Location As Point

    Sub New(ByVal PassedLocation As Point, ByVal PassedSize As Size)

        If Not Initialized Then
            Throw New Exception("Rectangle thing tried to initialize before the renderer was done. You're bad!")
        End If

        Size = PassedSize
        Location = PassedLocation



        TriLeg = CInt(0.068 * Size.Height)

        HudPen = New Pen(Brushes.White, CSng(Size.Width * 0.002))


    End Sub

    Public Sub Draw()

        Dim HUDPoints As New System.Drawing.Drawing2D.GraphicsPath


        HUDPoints.StartFigure()

        Dim WidthOffset As Single = (ScreenSize.Width - Size.Width) / 2.0F

        Dim HeightOffset As Single = (ScreenSize.Height - Size.Height) / 2.0F

        'Debug.WriteLine(WidthOffset)
        Debug.WriteLine(HeightOffset)


        HUDPoints.AddLine(WidthOffset + TriLeg, HeightOffset, Size.Width, HeightOffset)
        HUDPoints.AddLine(Size.Width, HeightOffset, Size.Width, HeightOffset + (Size.Height - TriLeg))







        Buffer.Graphics.DrawPath(HudPen, HUDPoints)


        HUDPoints.Dispose()

    End Sub


End Class
