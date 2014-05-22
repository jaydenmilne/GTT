Public Class HUDAnimator : Inherits Renderer

    Enum States As Integer
        Fullscreen
        GoingFullscreen
        Menu
        GoingMenu
    End Enum

    Public Shared CurrentState As States = States.Fullscreen


    Public Shared Sub ToggleState()

        If CurrentState = States.Fullscreen Then

            CurrentState = States.GoingMenu


        ElseIf CurrentState = States.Menu Then

            CurrentState = States.GoingFullscreen

        End If

    End Sub

    Public Shared Sub Update(ByVal ElapsedMilleseconds As Double)

        If CurrentState = States.GoingMenu Then

            Shrink(2, 200, ElapsedMilleseconds)

        ElseIf CurrentState = States.GoingFullscreen Then

            Shrink(2, 400, ElapsedMilleseconds)

        End If


    End Sub

    Private Shared Sub Shrink(ByVal Velocity As Integer, ByVal StopVal As Integer, ByVal ElapsedMilliseconds As Double)
        'position = velocity *(CurrentTime - LastTimeSinceRender)

            If HUD.Inset <= StopVal Then

            HUD.Inset += CInt(Velocity * ElapsedMilliseconds)


            ElseIf HUD.Inset >= StopVal Then

                CurrentState = States.Menu


            End If


    End Sub

    Private Shared Sub Grow(ByVal Velocity As Integer, ByVal StopVal As Integer, ByVal ElapsedMilliseconds As Double)

        If HUD.Inset >= StopVal Then

            HUD.Inset -= CInt(Velocity * ElapsedMilliseconds)

        ElseIf HUD.Inset <= StopVal Then

            CurrentState = States.Menu


        End If
    End Sub



End Class
