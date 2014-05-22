Public Class Game

    Public Timer As Timer

    Public HudAnimator As HUDAnimator

    Private Random As New Random

    Public EntityManager As New EntityManager()

    Private CheckingForUp As Boolean = True

    Sub New()

    End Sub

    Public Sub Initialize()

        Timer = MainForm.GameTimer

        Timer.Interval = 1
        Timer.Start()
        EntityManager.AddEntity(New TriangleShooter(New PointF(500, 500), 0, 1))
        EntityManager.AddEntity(New Square(New PointF(100, 100), 0, 0))
        EntityManager.AddEntity(New laser(New PointF(600, 100), 100, 0, Brushes.Green))


    End Sub

    Public Sub Update(ByVal ElapsedMilliseconds As Double)

        If Input.KeyStates(Keys.Escape) Then
            HudAnimator.ToggleState()
        End If


        If CheckingForUp And Not (Input.KeyStates(Keys.Up)) Then
            CheckingForUp = False
        End If

        If Input.KeyStates(Keys.Space) And Not CheckingForUp Then
            EntityManager.RemoveEntity(Random.Next(0, EntityManager.NumOfEntities))
            CheckingForUp = True
        End If


        HudAnimator.Update(ElapsedMilliseconds)

        If HudAnimator.CurrentState = GameThingTest.HUDAnimator.States.Fullscreen Then
            EntityManager.UpdateAll(ElapsedMilliseconds)
        End If


        CollisionDetector.CheckAllForCollission(EntityManager.WadOEntities, EntityManager.NumOfEntities + EntityManager.AvailibleSpots.Count) ' lets see if this works


    End Sub
End Class