Public Class PauseMenu

    Sub New()

        MenuItems = New List(Of MenuItem)
        SubMenu = New List(Of MenuItem)

        MenuItems.Add(New MenuItem("Quit Game", AddressOf QuitButton))
        MenuItems.Add(New MenuItem("Why the creator of this game" & vbNewLine & "is so awesome", AddressOf AwesomeButton))

        SubMenu.Add(New MenuItem("Because he is such an excellent" & vbNewLine & "programmer who dosen't hack" & vbNewLine & "things together to make it work", AddressOf DoNothing))
        SubMenu.Add(New MenuItem("Back", AddressOf BackButton))

    End Sub

    Public MenuName As String = "Game Paused"

    Public MenuItems As List(Of MenuItem)

    Public SubMenu As List(Of MenuItem)

    Public Function QuitButton() As Boolean
        End
    End Function

    Public Function BackButton() As Boolean
        MainForm.Game.MenuToDraw = MenuItems
        MainForm.Game.Menuname = MenuName
        Return False
    End Function


    Public Function AwesomeButton() As Boolean
        MainForm.Game.MenuToDraw = SubMenu
        MainForm.Game.Menuname = "Why creator is awesome"
        Return False
    End Function

    Public Function DoNothing() As Boolean
        Return True
    End Function


End Class
