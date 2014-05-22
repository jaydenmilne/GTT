Imports System.Runtime.Serialization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml.Serialization


Public Class Game

    Public Timer As Timer

    Public HudAnimator As HUDAnimator

    Private Random As New Random

    Public EntityManager As New EntityManager()

    Public GeneralWatch As New Stopwatch ' used for diagnostics

    Public GameWatch As New Stopwatch ' used for animations/game logic. Never ever reset it

    Public ScreenOffset As New PointF(0, 0)

    Public MenuToDraw As List(Of MenuItem)

    Public MenuName As String

    Public MyPauseMenu As PauseMenu

    Public PlayerID As Integer

    Public AlreadySerialized As Boolean = False

    Sub New()

    End Sub

    Public Sub ShowMenu(ByVal Menu As List(Of MenuItem), ByVal MenuTitle As String)

        MenuToDraw = Menu

        MenuName = MenuTitle

        HudAnimator.ToggleState()

    End Sub

    Public Sub Initialize()

        MyPauseMenu = New PauseMenu

        Dim Temp As HealthManager.CurrentHealthHolder

        Temp.Damage = 0
        Temp.ShieldPoints = 100
        Temp.HullPoints = 100
        Temp.Dead = False
        Temp.RechargeRate = 2000


        GameWatch.Start()

        Dim AdvancedTimer As New AdvancedTimer(3, 16, AddressOf MainForm.Tick)

        'AdvancedTimer.Start()

        MainForm.Timer1.Start()

        Dim asdf As New XWingModelCreator
        Dim asdf2 As New TriangleModelCreator

        PlayerID = EntityManager.SpawnEntity(New GenericShip(New PointF(500, 500), 0, 1, Temp, -1, AddressOf asdf.GetAModel, False))
        EntityManager.SpawnEntity(New GenericShip(New PointF(1000, 500), 247, 1, Temp, -1, AddressOf asdf2.GetAModel, True))
        asdf2 = New TriangleModelCreator
        EntityManager.SpawnEntity(New GenericShip(New PointF(500, 1000), 79, 1, Temp, -1, AddressOf asdf2.GetAModel, True))
        asdf2 = New TriangleModelCreator
        EntityManager.SpawnEntity(New GenericShip(New PointF(100, 100), 10, 1, Temp, -1, AddressOf asdf2.GetAModel, True))


        'EntityManager.SpawnEntity(New Square(New PointF(100, 100), 0, 0))


        LoadHighScore()


    End Sub

    Public Sub Update(ByVal ElapsedMilliseconds As Double)

        Dim NumOfShips As Integer = 0

        For i As Integer = 0 To EntityManager.LastEntity
            If Not IsNothing(EntityManager.WadOEntities(i)) Then
                If EntityManager.WadOEntities(i).ToString.Contains("Generic") Then
                    NumOfShips += 1
                End If
            End If



        Next

        If NumOfShips < 10 Then

            Dim Temp As HealthManager.CurrentHealthHolder
            Dim ModelCreator As New TriangleModelCreator


            Temp.Damage = 0
            Temp.ShieldPoints = 0
            Temp.HullPoints = 50
            Temp.Dead = False
            Temp.RechargeRate = 0

            Dim RandomLoc As New PointF(Random.Next(0, Renderer.ScreenSize.Width), Random.Next(0, Renderer.ScreenSize.Height))
            Try

                If Not DrawingUtils.PointInPolygon(RandomLoc, EntityManager.WadOEntities(PlayerID).GetCollisionable) Then
                    EntityManager.SpawnEntity(New GenericShip(RandomLoc,
                                        Random.Next(0, 360), 0, Temp, -1, AddressOf ModelCreator.GetAModel, True))
                End If
            Catch ex As Exception
                Console.WriteLine("Oops")
            End Try





        End If

        If Input.KeyStates(Keys.Escape) Then
            ShowMenu(MyPauseMenu.MenuItems, MyPauseMenu.MenuName)
        End If


        HudAnimator.Update(ElapsedMilliseconds)

        GeneralWatch.Reset()
        GeneralWatch.Start()

        If HudAnimator.CurrentState = GameThingTest.HUDAnimator.States.Fullscreen Then
            EntityManager.UpdateAll(ElapsedMilliseconds)
        End If

        GeneralWatch.Stop()
        Diagnostics.UpdateTime = MainForm.Game.GeneralWatch.ElapsedTicks / Stopwatch.Frequency * 1000


        EntityManager.ExecutePrisoners()

        GeneralWatch.Reset()
        GeneralWatch.Start()
        If HudAnimator.CurrentState = GameThingTest.HUDAnimator.States.Fullscreen Then
            CollisionDetector.CheckAllForCollision(EntityManager.WadOEntities, EntityManager.LastEntity) ' lets see if this works

            If GameWatch.IsRunning = True Then
                'GameWatch.Stop()
            End If

        Else
            If GameWatch.IsRunning = False Then
                'GameWatch.Start()
            End If


        End If

        GeneralWatch.Stop()
        Diagnostics.CollisionTime = MainForm.Game.GeneralWatch.ElapsedTicks / Stopwatch.Frequency * 1000

    End Sub

    Public Sub SaveGame()

        Dim Path As String = System.IO.Path.Combine(Application.StartupPath, "entities.bin")
        Dim TestFileStream As Stream = File.Create(Path)
        Dim Serializer As New BinaryFormatter



        Serializer.Serialize(TestFileStream, EntityManager)


        TestFileStream.Close()

        If Random.Next(0, 1000000) = 123456 Then
            Throw (New Exception("I randomly decided to fail" & vbNewLine & "There is a 1/1000000 chance of this happening"))
        End If

    End Sub

    Public Sub LoadGame()
        Dim Path As String = System.IO.Path.Combine(Application.StartupPath, "entities.bin")
        Dim Serializer As New BinaryFormatter
        Dim Stream As New FileStream(Path, FileMode.Open)

        EntityManager = CType(Serializer.Deserialize(Stream), EntityManager)

        Stream.Close()


    End Sub

    Public HighScoreThisOne As New HighScore

    Public Sub UpdateHighScore()

        Dim Path As String = System.IO.Path.Combine(Application.StartupPath, "highscore.xml")
        Dim writer As TextWriter = New StreamWriter(Path)

        Dim Serializer As New XmlSerializer(GetType(HighScore))

        Serializer.Serialize(writer, HighScoreThisOne)

        writer.Close()


    End Sub

    Public Sub LoadHighScore()

        Dim mySerializer As XmlSerializer = New XmlSerializer(GetType(HighScore))
        ' To read the file, create a FileStream.
        Dim Path As String = System.IO.Path.Combine(Application.StartupPath, "highscore.xml")
        Dim myFileStream As FileStream = _
        New FileStream(Path, FileMode.Open)
        ' Call the Deserialize method and cast to the object type.
        HighScoreThisOne = CType( _
        mySerializer.Deserialize(myFileStream), HighScore)

        myFileStream.Close()


    End Sub
End Class