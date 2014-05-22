Public Class HUDAnimator : Inherits Renderer

    Enum States As Integer
        Fullscreen
        GoingFullscreen
        Menu
        GoingMenu
    End Enum

    Shared CurrentState As States = States.Fullscreen


    Public Shared Sub ToggleState()

        If CurrentState = States.Fullscreen Then

            CurrentState = States.GoingMenu


        ElseIf CurrentState = States.Menu Then

            CurrentState = States.GoingFullscreen

        End If

    End Sub

    Public Shared Sub Update(ByVal ElapsedMilleseconds As Double)

        If CurrentState = States.GoingMenu Then

            TimeSkipAnimate(2, 200, ElapsedMilleseconds)
        ElseIf CurrentState = States.GoingFullscreen Then

            TimeSkipAnimate(-2, 400, ElapsedMilleseconds)
        End If


    End Sub

    Private Shared Sub TimeSkipAnimate(ByVal Velocity As Integer, ByVal StopVal As Integer, ByVal ElapsedMilliseconds As Double)
        'position = velocity *(CurrentTime - LastTimeSinceRender)

        If CurrentState = States.GoingMenu Then
            If HUD.Inset <= StopVal Then

                Dim Change As Integer

                Change = CInt(Velocity * ElapsedMilliseconds)

                HUD.Inset += Change


            ElseIf HUD.Inset >= StopVal Then

                CurrentState = States.Menu


            End If

        ElseIf CurrentState = States.GoingFullscreen Then
            If HUD.Inset >= StopVal Then

                Dim Change As Integer

                Change = CInt(Velocity * ElapsedMilliseconds)

                HUD.Inset += Change


            ElseIf HUD.Inset <= StopVal Then

                CurrentState = States.Fullscreen


            End If

        End If


    End Sub



End Class
