Public Class DeadMenu
    Public MenuItems As New List(Of MenuItem)

    Sub New()
        MenuItems.Add(New MenuItem("You died!", False))
        MenuItems.Add(New MenuItem("Press here to exit the game to start over", AddressOf EndButton))
        MenuItems.Add(New MenuItem("Press here to learn why you have to exit to start over", AddressOf Confess))
        MenuItems.Add(New MenuItem("Press here to load the last saved game", AddressOf MainForm.Game.MyPauseMenu.LoadWarning))
    End Sub

    Public Function EndButton() As Boolean
        End
    End Function

    Public Function Confess() As Boolean
        Input.MouseStates(Input.Mouse.Left) = False
        MessageBox.Show("Because I'm hacking a game together with the engine I spent the whole semester making. I don't want the real game to be like this, but I have no choice")
        Return False
    End Function


End Class
