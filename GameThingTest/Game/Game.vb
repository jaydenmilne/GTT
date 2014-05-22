Public Class Game

    Public Timer As Timer

    Public HudAnimator As HUDAnimator

    Private Random As New Random

    Public EntityManager As New EntityManager()

    Sub New()

    End Sub

    Public Sub Initialize()

        Timer = MainForm.GameTimer

        Timer.Interval = 1
        Timer.Start()

        EntityManager.AddEntity(New TriangleShooter(New PointF(100, 100), 0, 0))
        EntityManager.AddEntity(New TriangleShooter(New PointF(500, 500), 0, 1))
        EntityManager.AddEntity(New TriangleShooter(New PointF(1000, 1000), 0, 0))


    End Sub

    Public Sub Update(ByVal ElapsedMilliseconds As Double)


        If Input.KeyStates(Keys.Escape) Then
            HudAnimator.ToggleState()
        End If

        HudAnimator.Update(ElapsedMilliseconds)



        EntityManager.UpdateAll(ElapsedMilliseconds)


        CollisionDetector.CheckAllForCollission(EntityManager.WadOEntities, EntityManager.NumOfEntities) ' lets see if this works


    End Sub
End Class