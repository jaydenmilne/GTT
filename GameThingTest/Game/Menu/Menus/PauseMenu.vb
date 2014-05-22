Imports System.Reflection

Public Class PauseMenu

    Sub New()

        MenuItems = New List(Of MenuItem)

        SubMenu = New List(Of MenuItem)

        Dim version As String = Assembly.GetEntryAssembly().GetName().Version.ToString

        MenuItems.Add(New MenuItem("Quit Game", AddressOf QuitButton))

        MenuItems.Add(New MenuItem("Save Game", AddressOf SaveWarning))
        MenuItems.Add(New MenuItem("Load Game", AddressOf LoadWarning))
        MenuItems.Add(New MenuItem("Why the creator of this game is so awesome", AddressOf AwesomeButton))
        MenuItems.Add(New MenuItem("GTT Version " & version, False))

        SubMenu.Add(New MenuItem("Because he is such an excellent programmer who dosen't hack things together to make it work", AddressOf DoNothing, False))
        SubMenu.Add(New MenuItem("Back", AddressOf BackButton))

    End Sub

    Public MenuName As String = "Game Paused"

    Public MenuItems As List(Of MenuItem)

    Public SubMenu As List(Of MenuItem)

    Dim ExceptionText As String = "This should always be initialized. If you are seeing this, it didn't initialize right, and something really bad happened."

    Public Function QuitButton() As Boolean
        End
    End Function



    Public Function SaveGame() As Boolean

        Dim SaveScreen As New List(Of MenuItem)
        Dim Worked As Boolean = True
        Input.MouseStates(Input.Mouse.Left) = False

        Try
            MainForm.Game.SaveGame()
        Catch ex As Exception
            Worked = False
            ExceptionText = ex.ToString
        End Try

        If Not Worked Then
            SaveScreen.Add(New MenuItem("There was an error saving the game. I'm sorry, I have failed you.", False))
            SaveScreen.Add(New MenuItem("Error Text", AddressOf ShowErrorText))
            SaveScreen.Add(New MenuItem("Back", AddressOf BackButton))
        Else
            SaveScreen.Add(New MenuItem("It worked.", False))
            SaveScreen.Add(New MenuItem("Back", AddressOf BackButton))
        End If

        MainForm.Game.MenuToDraw = SaveScreen
        MainForm.Game.MenuName = "Save Game"

        Return True
    End Function

    Public Function SaveWarning() As Boolean
        Dim PauseScreen As New List(Of MenuItem)
        PauseScreen.Add(New MenuItem("Warning!", False, Color.Red))
        PauseScreen.Add(New MenuItem("This will overwrite the currently saved game, and it will be gone forever. If you screw up and erase your progress, its not my fault", False))
        PauseScreen.Add(New MenuItem("Continue", AddressOf LoadGame))
        PauseScreen.Add(New MenuItem("Back", AddressOf BackButton))

        MainForm.Game.MenuToDraw = PauseScreen
        MainForm.Game.MenuName = "Save Game"

    End Function

    Public Function LoadWarning() As Boolean
        Dim PauseScreen As New List(Of MenuItem)
        PauseScreen.Add(New MenuItem("Warning!", False, Color.Red))
        PauseScreen.Add(New MenuItem("This will overwrite the currently running game, and it will be gone forever. If you screw up and erase your progress, its not my fault", False))
        PauseScreen.Add(New MenuItem("Continue", AddressOf SaveGame))
        PauseScreen.Add(New MenuItem("Back", AddressOf BackButton))

        MainForm.Game.MenuToDraw = PauseScreen
        MainForm.Game.MenuName = "Load Game"

    End Function

    Public Function LoadGame() As Boolean
        Dim LoadScreen As New List(Of MenuItem)
        Dim Worked As Boolean = True

        Try
            MainForm.Game.LoadGame()
        Catch ex As Exception
            Worked = False
            ExceptionText = ex.ToString()
        End Try

        If Worked Then
            LoadScreen.Add(New MenuItem("Load was sucessful", False))
            LoadScreen.Add(New MenuItem("Back", AddressOf BackButton))

        Else
            LoadScreen.Add(New MenuItem("Oops, I accidentally a exception", False, Color.Red))
            LoadScreen.Add(New MenuItem("Exception Text", AddressOf ShowErrorText))
            LoadScreen.Add(New MenuItem("Back", AddressOf BackButton))
        End If

        MainForm.Game.MenuToDraw = LoadScreen

    End Function

    Public Function BackButton() As Boolean
        Input.MouseStates(Input.Mouse.Left) = False
        MainForm.Game.MenuToDraw = MenuItems
        MainForm.Game.MenuName = MenuName
        Return False
    End Function


    Public Function AwesomeButton() As Boolean
        MainForm.Game.MenuToDraw = SubMenu
        Return False
    End Function

    Public Function DoNothing() As Boolean
        Return True
    End Function

    Public Function ShowErrorText() As Boolean
        Input.MouseStates(Input.Mouse.Left) = False
        MessageBox.Show(ExceptionText)
        Return False
    End Function

End Class
