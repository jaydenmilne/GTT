Public Class GameState
    'Inherits Game
    Enum States As Integer
        Running
        Paused
        SelfDestructing
    End Enum

    Private Shared GameState As States = States.Running

    Shared Sub UpdateGameState(ByVal NewState As States)
        GameState = NewState
    End Sub

    Shared Function CurrentState() As States
        Return GameState
    End Function

End Class
