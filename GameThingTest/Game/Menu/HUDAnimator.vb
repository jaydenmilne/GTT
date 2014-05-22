Public Class HUDAnimator : Inherits Renderer

    Enum States As Integer
        Fullscreen
        GoingFullscreen
        Menu
        GoingMenu
    End Enum

    Public Shared CurrentState As States = States.Fullscreen

    Private Shared OldHeight As Single
    Private Shared OldWidth As Single

    Private Shared DesiredSize As SizeF

    Public Shared Sub ToggleState()

        If CurrentState = States.Fullscreen Then

            OldHeight = HUD.HeightOffset
            OldWidth = HUD.WidthOffset

            DesiredSize = New SizeF(ScreenSize.Width * 0.3F, ScreenSize.Height * 0.2F)

            CurrentState = States.GoingMenu

        ElseIf CurrentState = States.Menu Then

            CurrentState = States.GoingFullscreen

        End If

    End Sub

    Public Shared Sub Update(ByVal ElapsedMilleseconds As Double)

        If CurrentState = States.GoingMenu Then
            Shrink(2, ElapsedMilleseconds)

            If HUD.HeightOffset > DesiredSize.Height And HUD.WidthOffset > DesiredSize.Width Then
                CurrentState = States.Menu


            End If

        ElseIf CurrentState = States.GoingFullscreen Then

            Grow(2, ElapsedMilleseconds)

            If HUD.HeightOffset < OldHeight And HUD.WidthOffset < OldWidth Then

                CurrentState = States.Fullscreen
                HUD.HeightOffset = OldHeight
                HUD.WidthOffset = OldWidth


            End If
        ElseIf CurrentState = States.Menu Then



        End If

    End Sub

    Shared Sub Shrink(ByVal Velocity As Single, ByVal Elapsed As Double)

        If HUD.HeightOffset < DesiredSize.Height Then

            HUD.HeightOffset += CSng(Velocity * Elapsed)

        End If

        If HUD.WidthOffset < DesiredSize.Width Then

            HUD.WidthOffset += CSng(Velocity * Elapsed)

        End If

    End Sub

    Shared Sub Grow(ByVal Velocity As Single, ByVal Elapsed As Double)

        If HUD.HeightOffset > OldHeight Then

            HUD.HeightOffset -= CSng(Velocity * Elapsed)

        End If
        If HUD.WidthOffset > OldWidth Then

            HUD.WidthOffset -= CSng(Velocity * Elapsed)

        End If

    End Sub




End Class
