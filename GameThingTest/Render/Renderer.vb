﻿Imports System.Drawing.Text
Imports System.IO
Public Class Renderer
#Region "Public"
    Public Shared Buffer As BufferedGraphics
    Public Shared ScreenSize As Size
    Public Shared HUD As HUDObject
    Public Shared Initialized As Boolean = False
    Public Shared pfc As New PrivateFontCollection()
#End Region

#Region "Private"
    Private Shared GraphicsObject As Graphics
    Private Shared BufferContext As New BufferedGraphicsContext()



#End Region


    Public Shared Sub Initialize()

        'Awesome Font

        Dim Path As String = System.IO.Path.Combine(Application.StartupPath, "ratio.ttf")

        pfc.AddFontFile(Path)




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

        Dim TempSize As Size = New Size(CInt(ScreenSize.Width * 0.99), CInt(ScreenSize.Height * 0.99))

        HUD = New HUDObject(New Point(0, 0), TempSize)

    End Sub

    Public Shared Sub Render(ByVal time As Double)
        Buffer.Graphics.Clear(Color.Black)

        MainForm.Game.GeneralWatch.Restart()

        Buffer.Graphics.DrawString("Frame Time: " & CStr(Math.Round(time, 0)) & " FPS: " & CStr(Math.Round(1000 / time, 1)) & " Entities:" & CStr(MainForm.Game.EntityManager.NumOfEntities), New Font("Consolas", 8, FontStyle.Regular), Brushes.White, New Point(0, 30))

        Dim Temp = MainForm.Game.EntityManager

        For i As Integer = 0 To Temp.LastEntity
            If Not IsNothing(Temp.WadOEntities(i)) Then
                Buffer.Graphics.DrawLines(Pens.Green, Temp.WadOEntities(i).GetCollisionable)
                For y As Integer = 0 To Temp.WadOEntities(i).GetDrawModel.DrawableLayers.Count - 1
                    If Temp.WadOEntities(i).GetDrawModel.DrawableLayers(y).DrawThisLayer Then
                        If Temp.WadOEntities(i).GetDrawModel.DrawableLayers(y).IsPolygon Then
                            Buffer.Graphics.FillPolygon(New SolidBrush(Temp.WadOEntities(i).GetDrawModel.DrawableLayers(y).Color), Temp.WadOEntities(i).GetDrawModel.DrawableLayers(y).PublicGeom)
                        Else
                            Buffer.Graphics.DrawLines(New Pen(New SolidBrush(Temp.WadOEntities(i).GetDrawModel.DrawableLayers(y).Color)), Temp.WadOEntities(i).GetDrawModel.DrawableLayers(y).PublicGeom)
                        End If
                    End If
                Next
            End If
            '    'If Temp.WadOEntities(i).FillPolygon Then
            '    '    Buffer.Graphics.FillPolygon(Temp.WadOEntities(i).GetPen.Brush, Temp.WadOEntities(i).GetCollisionable)
            '    'Else
            '    '    Buffer.Graphics.DrawLines(Temp.WadOEntities(i).GetPen, Temp.WadOEntities(i).GetCollisionable)
            '    'End If

            '    Buffer.Graphics.DrawLines(Pens.Green, Temp.WadOEntities(i).GetCollisionable)

            '    For Each Item In Temp.WadOEntities(i).GetDrawModel.DrawableLayers
            '        If Item.DrawThisLayer Then
            '            If Item.IsPolygon Then
            '                Buffer.Graphics.FillPolygon(New SolidBrush(Item.Color), Item.PublicGeom)
            '            Else
            '                Buffer.Graphics.DrawLines(New Pen(New SolidBrush(Item.Color)), Item.PublicGeom)
            '            End If
            '        End If

            '    Next

            'End If

            

        Next
        ' hacked together for last minute
        Dim MenuFont As New Font(pfc.Families(0), 40, System.Drawing.GraphicsUnit.Pixel)
        Dim HUDString As String
        Try
            HUDString = "High Score: " & MainForm.Game.HighScoreThisOne.HighScore.ToString & vbNewLine & "Shields: " & MainForm.Game.EntityManager.WadOEntities(MainForm.Game.PlayerID).GetHealthWad.ShieldPoints.ToString & _
            vbNewLine & "Hull: " & MainForm.Game.EntityManager.WadOEntities(MainForm.Game.PlayerID).GetHealthWad.HullPoints.ToString & _
            vbNewLine & "Enemies Killed: " & MainForm.Game.EntityManager.SHipsKilled
        Catch ex As Exception
            HUDString = "You dead!"
        End Try
        

        Buffer.Graphics.DrawString(HUDString, MenuFont, Brushes.White, New PointF(HUD.WidthOffset, ScreenSize.Height - (Buffer.Graphics.MeasureString(HUDString, MenuFont).Height)))


        HUD.Draw(True, MainForm.Game.MenuToDraw, MainForm.Game.MenuName)

        MainForm.Game.GeneralWatch.Stop()
        Diagnostics.RenderTime = MainForm.Game.GeneralWatch.ElapsedTicks / Stopwatch.Frequency * 1000
        MainForm.Game.GeneralWatch.Restart()

        Buffer.Render()

        MainForm.Game.GeneralWatch.Stop()

        Diagnostics.BufferRender = MainForm.Game.GeneralWatch.ElapsedTicks / Stopwatch.Frequency * 1000


    End Sub





End Class
