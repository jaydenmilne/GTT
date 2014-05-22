Imports System.Runtime.Serialization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary


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

    Private MyPauseMenu As PauseMenu

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
        Temp.RechargeRate = 1000

        Timer = MainForm.GameTimer

        GameWatch.Start()

        Timer.Interval = 1
        Timer.Start()
        EntityManager.SpawnEntity(New TriangleShooter(New PointF(500, 500), 0, 1, Temp, -1))

        'EntityManager.SpawnEntity(New Square(New PointF(100, 100), 0, 0))



    End Sub

    Public Sub Update(ByVal ElapsedMilliseconds As Double)

        If EntityManager.NumOfEntities <= 25 Then

            Dim Temp As HealthManager.CurrentHealthHolder

            Temp.Damage = 0
            Temp.ShieldPoints = 0
            Temp.HullPoints = 50
            Temp.Dead = False
            Temp.RechargeRate = 0

            Do Until EntityManager.NumOfEntities > 25
                EntityManager.SpawnEntity(New TriangleShooter(
                                        New PointF(Random.Next(0, Renderer.ScreenSize.Width), Random.Next(0, Renderer.ScreenSize.Height)),
                                        Random.Next(0, 360), 0, Temp, -1))
            Loop
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
End Class