Public Class Game
    Public Shared Timer As Timer = MainForm.GameTimer
    Public Shared Shooter As New TriangleShooter
    Sub New()

    End Sub

    Public Shared Sub Initialize()
        Timer.Interval = 1
        Timer.Start()
    End Sub

    Public Shared Sub Update(ByVal ElapsedMilliseconds As Double)

        ' Debug.WriteLine(ElapsedMilliseconds)

        If Input.KeyStates(Keys.Escape) Then
            HUDAnimator.ToggleState()
        End If

        HUDAnimator.Update(ElapsedMilliseconds)
        Shooter.Update(ElapsedMilliseconds)


    End Sub
End Class