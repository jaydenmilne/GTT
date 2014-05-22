Public Class CutOffRectangle : Inherits Renderer
    Public Inset As Integer
    Dim TriangleLegSize As Integer
    Dim HudPen As New Pen(Brushes.White)

    Sub New()

        If Not Initialized Then
            Throw New Exception("Rectangle thing tried to initialize before the renderer was done. You're bad!")
        End If

        HudPen.LineJoin = Drawing2D.LineJoin.Round


        Inset = CInt(0.01 * ScreenSize.Height)
        HudPen.Width = CInt(Inset / 3)

        TriangleLegSize = CInt(0.068 * ScreenSize.Height)

        HudPen.Width = CSng(Inset * 1 / 2)

    End Sub

    Public Sub Draw()

        Dim HUDPoints As New System.Drawing.Drawing2D.GraphicsPath


        HUDPoints.StartFigure()

        HUDPoints.AddLine(Inset + TriangleLegSize,
                          Inset, ScreenSize.Width - Inset,
                          Inset)

        HUDPoints.AddLine(ScreenSize.Width - Inset,
                          Inset,
                          ScreenSize.Width - Inset,
                          ScreenSize.Height - (Inset + TriangleLegSize))

        HUDPoints.AddLine(ScreenSize.Width - Inset,
                          ScreenSize.Height - (Inset + TriangleLegSize),
                          ScreenSize.Width - (Inset + TriangleLegSize),
                          ScreenSize.Height - Inset)

        HUDPoints.AddLine(ScreenSize.Width - (Inset + TriangleLegSize),
                          ScreenSize.Height - Inset,
                          Inset,
                          ScreenSize.Height - Inset)

        HUDPoints.AddLine(Inset,
                          ScreenSize.Height - Inset,
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
