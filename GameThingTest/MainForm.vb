﻿Public Class MainForm
    Dim MyWatch As New Stopwatch
    Public RenderMilliseconds As Double
    Public UpdateMilliseconds As Double
    Public Game As New Game()

    'no real code should go here, it all should be elsewhere

    Private Sub MainForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Input.KeyDown(e)
    End Sub

    Private Sub MainForm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        Input.KeyUp(e)
    End Sub

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Renderer.Initialize()
        Game.Initialize()
        Debug.Show()
    End Sub

    Private Sub MainForm_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Input.MouseDown(e)
    End Sub

    Private Sub MainForm_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        Input.MouseUp(e)
    End Sub

    Private Sub MainForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

    End Sub

    Private Sub MainForm_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        'pause game
    End Sub

    'main loop right here

    Public Sub Tick()

        Dim PrevTicks As Long = MyWatch.ElapsedTicks
        MyWatch.Restart()

        Game.Update(PrevTicks / Stopwatch.Frequency * 1000)

        UpdateMilliseconds = MyWatch.ElapsedTicks / Stopwatch.Frequency * 1000

        Renderer.Render(PrevTicks / Stopwatch.Frequency * 1000)

        RenderMilliseconds = (MyWatch.ElapsedTicks / Stopwatch.Frequency * 1000) - UpdateMilliseconds

    End Sub







    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Tick()
    End Sub
End Class
