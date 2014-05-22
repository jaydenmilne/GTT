Imports System.Reflection

Public Class PauseMenu

    Sub New()

        MenuItems = New List(Of MenuItem)

        SubMenu = New List(Of MenuItem)

        Dim version As String = Assembly.GetEntryAssembly().GetName().Version.ToString

        MenuItems.Add(New MenuItem("Quit Game", AddressOf QuitButton))

        MenuItems.Add(New MenuItem("Save Game", AddressOf SaveWarning))
        MenuItems.Add(New MenuItem("Load Game", AddressOf LoadWarning))
        'MenuItems.Add(New MenuItem("Options", AddressOf Options))
        MenuItems.Add(New MenuItem("About GameThingTest", AddressOf AboutWindow))
        MenuItems.Add(New MenuItem("Reset High Score", AddressOf ResetHS))
        MenuItems.Add(New MenuItem("GTT Version " & version, False))

        SubMenu.Add(New MenuItem("About", AddressOf DoNothing, False))
        SubMenu.Add(New MenuItem("Back", AddressOf BackButton, True))

    End Sub

    Public MenuName As String = "Game Paused"

    Public MenuItems As List(Of MenuItem)

    Public SubMenu As List(Of MenuItem)

    Dim ExceptionText As String = "This should always be initialized. If you are seeing this, it didn't initialize right, and something really bad happened."

    Public Function QuitButton() As Boolean
        End
    End Function

    Public Function ResetHS() As Boolean
        Input.MouseStates(Input.Mouse.Left) = False
        MainForm.Game.HighScoreThisOne.HighScore = 0
        MainForm.Game.UpdateHighScore()
        Return False
    End Function

    Public Function AboutWindow() As Boolean
        Input.MouseStates(Input.Mouse.Left) = False


        MessageBox.Show("This game was created in 2013/14 by Jayden Milne." & vbNewLine & "There is a lot more that I wanted to do, but this is it I guess" & vbNewLine & vbNewLine & "Features: Fully vectorized graphics (no images/pictureboxes), automatically scalable (no hard coded coordinates in the whole game!) Double buffering, AntiAliasing (Disabled in renderer.vb, set smoothingmode to HighQuality), Very accurate collisions (too accurate, collision detection slows the whole thing down), flexible menu system (menuitems are added dynamically and there are no hardcoded locations), easy to create new ships/classes, health manager (shields/hull health), model creator GUI (GTTGUI) to help make layers, lasers, an xwing model, flexible model saving scheme (easy to add layers), beginnings of AI (commented out to make deadline), dynamic entity manager, diagnostics form, bugs, lag and many more. " & vbNewLine & vbNewLine & "Things I wanted to add: mouse steers ship, a plot, view is centered on player ship, an actual HUD, more baddies, more projectiles, faster/more efficient collisons, options, sound, and many more." & vbNewLine & vbNewLine & "Please keep in mind that this is my first multifile code project ever. I also think that this won't be very impressive because people won't realize all the work that went on under the hood (more than a half baked version of Monopoly). Oh well, wish I was more motivated outside school." & vbNewLine & vbNewLine & "For questions/job offers please email me at roguenerd@gmail.com")


        Return False

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
        PauseScreen.Add(New MenuItem("Continue", AddressOf SaveGame))
        PauseScreen.Add(New MenuItem("Back", AddressOf BackButton))

        MainForm.Game.MenuToDraw = PauseScreen
        MainForm.Game.MenuName = "Save Game"

        Return True
    End Function

    Public Function LoadWarning() As Boolean
        Dim PauseScreen As New List(Of MenuItem)
        PauseScreen.Add(New MenuItem("Warning!", False, Color.Red))
        PauseScreen.Add(New MenuItem("This will overwrite the currently running game, and it will be gone forever. If you screw up and erase your progress, its not my fault", False))
        PauseScreen.Add(New MenuItem("Continue", AddressOf LoadGame))
        PauseScreen.Add(New MenuItem("Back", AddressOf BackButton))

        MainForm.Game.MenuToDraw = PauseScreen
        MainForm.Game.MenuName = "Load Game"

        Return True

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

        Return True
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

    Public Function Options() As Boolean
        Input.MouseStates(Input.Mouse.Left) = False

        Dim OptionsMenu As New List(Of MenuItem)
        OptionsMenu.Add(New MenuItem("Buffer: " & GameOptions.Buffer.ToString, AddressOf ToggleBuffer))
        OptionsMenu.Add(New MenuItem("Antialiasing: " & GameOptions.AntiAlias.ToString, AddressOf ToggleAA))
        OptionsMenu.Add(New MenuItem("Back", AddressOf BackButton))

        MainForm.Game.MenuToDraw = OptionsMenu
        MainForm.Game.MenuName = "Options"

        Return False
    End Function

    Public Function ToggleBuffer() As Boolean
        If GameOptions.Buffer Then
            GameOptions.Buffer = False
        Else
            GameOptions.Buffer = True
        End If
        Return False


    End Function

    Public Function ToggleAA() As Boolean
        If GameOptions.AntiAlias Then
            GameOptions.AntiAlias = False
        Else
            GameOptions.AntiAlias = True
        End If
        Return False
    End Function

End Class
