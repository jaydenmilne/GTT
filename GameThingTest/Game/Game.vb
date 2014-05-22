Public Class Game

    Public Timer As Timer

    Public HudAnimator As HUDAnimator

    Private Random As New Random

    Public EntityManager As New EntityManager()

    Public GeneralWatch As New Stopwatch ' used for diagnostics

    Sub New()

    End Sub

    Public Sub Initialize()

        Timer = MainForm.GameTimer

        Timer.Interval = 1
        Timer.Start()
        EntityManager.SpawnEntity(New TriangleShooter(New PointF(500, 500), 0, 1))
        EntityManager.SpawnEntity(New Square(New PointF(100, 100), 0, 0))



    End Sub

    Public Sub Update(ByVal ElapsedMilliseconds As Double)

        If EntityManager.NumOfEntities <= 50 Then
            Do Until EntityManager.NumOfEntities > 50
                EntityManager.SpawnEntity(New TriangleShooter(
                                        New PointF(Random.Next(0, Renderer.ScreenSize.Width), Random.Next(0, Renderer.ScreenSize.Height)),
                                        Random.Next(0, 360), 0))
            Loop
        End If

        If Input.KeyStates(Keys.Escape) Then
            HudAnimator.ToggleState()
        End If


        HudAnimator.Update(ElapsedMilliseconds)

        GeneralWatch.Reset()
        GeneralWatch.Start()
        If HudAnimator.CurrentState = GameThingTest.HUDAnimator.States.Fullscreen Then
            EntityManager.UpdateAll(ElapsedMilliseconds)
        End If

        GeneralWatch.Stop()
        Diagnostics.UpdateTime = MainForm.Game.GeneralWatch.ElapsedTicks / Stopwatch.Frequency * 1000


        EntityManager.ExecutePrisoners()

        GeneralWatch.Reset()
        GeneralWatch.Start()
        If HudAnimator.CurrentState = GameThingTest.HUDAnimator.States.Fullscreen Then
            CollisionDetector.CheckAllForCollission(EntityManager.WadOEntities, EntityManager.LastEntity) ' lets see if this works

        End If

        GeneralWatch.Stop()
        Diagnostics.CollisionTime = MainForm.Game.GeneralWatch.ElapsedTicks / Stopwatch.Frequency * 1000


    End Sub
End Class