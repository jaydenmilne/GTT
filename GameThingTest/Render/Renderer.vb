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

        Buffer.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ScreenSize = GraphicsObject.VisibleClipBounds.Size.ToSize
        Initialized = True

        HUD = New CutOffRectangle()

    End Sub

    Public Shared Sub Render(ByVal time As Double)
        Buffer.Graphics.Clear(Color.Black)
        HUD.Draw()
        Buffer.Graphics.DrawLines(Game.Shooter.DesiredPen, Game.Shooter.PublicGeometry)
        Buffer.Graphics.DrawString(CStr(Math.Round(time, 0)), New Font("Time New Roman", 10), Brushes.White, New Point(0, 0))
        'Debug.WriteLine(Game.Shooter.PublicGeometry(3))
        Buffer.Render()
    End Sub





End Class
