Public Class OldCutOffRectangle : Inherits Renderer
    Public Inset As Integer
    Dim TriangleLegSize As Integer
    Dim HudPen As New Pen(Brushes.White)
    Dim Size As Size
    Dim MyRect As Rectangle
    Dim Location As Point

    Sub New(ByVal PassedLocation As Point, ByVal PassedSize As Size)

        If Not Initialized Then
            Throw New Exception("Rectangle thing tried to initialize before the renderer was done. You're bad!")
        End If

        Size = PassedSize
        Location = PassedLocation


        HudPen.Width = CInt(Size.Height * 0.5)

        TriangleLegSize = CInt(0.068 * Size.Height)

        HudPen.Width = CSng(Inset * 1 / 2)

    End Sub

    Public Sub Draw()

        Dim HUDPoints As New System.Drawing.Drawing2D.GraphicsPath


        HUDPoints.StartFigure()

        HUDPoints.AddLine(Inset + TriangleLegSize,
                          Inset, Size.Width - Inset,
                          Inset)

        HUDPoints.AddLine(Size.Width - Inset,
                          Inset,
                          Size.Width - Inset,
                          Size.Height - (Inset + TriangleLegSize))

        HUDPoints.AddLine(Size.Width - Inset,
                          Size.Height - (Inset + TriangleLegSize),
                          Size.Width - (Inset + TriangleLegSize),
                          Size.Height - Inset)

        HUDPoints.AddLine(Size.Width - (Inset + TriangleLegSize),
                          Size.Height - Inset,
                          Inset,
                          Size.Height - Inset)

        HUDPoints.AddLine(Inset,
                          Size.Height - Inset,
                          Inset,
                          Inset + TriangleLegSize)

        HUDPoints.AddLine(Inset,
                          Inset + TriangleLegSize,
                          Inset + TriangleLegSize,
                          Inset)

        Buffer.Graphics.DrawPath(HudPen, HUDPoints)

        HUDPoints.Dispose()

    End Sub


End Class
