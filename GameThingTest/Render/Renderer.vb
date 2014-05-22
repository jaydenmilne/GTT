Public Class Renderer
#Region "Public"
    Public Shared Buffer As BufferedGraphics
    Public Shared ScreenSize As Size
    Public Shared HUD As CutOffRectangle
    Public Shared Initialized As Boolean = False
#End Region

#Region "Private"
    Private Shared GraphicsObject As Graphics
    Private Shared BufferContext As New BufferedGraphicsContext()

#End Region

    Public Shared Sub Initialize()

        'This makes it fullscreen, the order of these matters!
        'TODO - enable option for windowed mode

        Dim myScreen As Screen = Screen.FromHandle(MainForm.Handle)
        MainForm.TopMost = True
        MainForm.WindowState = FormWindowState.Normal
        MainForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        MainForm.WindowState = FormWindowState.Maximized
        MainForm.Bounds = myScreen.Bounds



        'Set up drawing surfaces...
        GraphicsObject = Graphics.FromHwnd(MainForm.Handle)
        Buffer = BufferContext.Allocate(GraphicsObject, _
                                        New Rectangle(New Point(0, 0),
                                        GraphicsObject.VisibleClipBounds.Size.ToSize))

        Buffer.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed

        ScreenSize = GraphicsObject.VisibleClipBounds.Size.ToSize
        Initialized = True

        Dim TempSize As Size = New Size(CInt(ScreenSize.Width * 0.9), CInt(ScreenSize.Height * 0.9))

        HUD = New CutOffRectangle(New Point(0, 0), TempSize)

    End Sub

    Public Shared Sub Render(ByVal time As Double)
        Buffer.Graphics.Clear(Color.Black)
        HUD.Draw()

        Buffer.Graphics.DrawString(CStr(Math.Round(time, 0)), New Font("Time New Roman", 10), Brushes.White, New Point(0, 0))

        For i As Integer = 0 To MainForm.Game.EntityManager.NumOfEntities - 1
            If IsNothing(MainForm.Game.EntityManager.WadOEntities(i).PublicGeometry) Then
                Exit For
            Else
                Buffer.Graphics.DrawLines(MainForm.Game.EntityManager.WadOEntities(i).DesiredPen, MainForm.Game.EntityManager.WadOEntities(i).PublicGeometry)
            End If
        Next


        Buffer.Render()



    End Sub





End Class
