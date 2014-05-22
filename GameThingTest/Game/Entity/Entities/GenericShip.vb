Imports System
Imports System.Runtime.Serialization
Imports System.Xml
Imports System.Drawing.Drawing2D
Imports System.Windows
<Serializable()>
Public Class GenericShip : Inherits Entity

    '
    '
    'This class bloated as I added crap at the last second. Sorry.
    '
    '



    <OnDeserialized()> _
    Friend Sub Reinitialize(ByVal context As StreamingContext)

        If KeyScheme = 0 Then
            DesiredPen = New Pen(Brushes.Red, 1)
        Else
            DesiredPen = New Pen(Brushes.LightBlue, 1)
        End If

    End Sub

#Region "Geometry"


    Dim Model As DrawModel


#End Region

#Region "PublicVars"
    Public ShadowsCurrentAngle As Single = 0

    Public KeyScheme As Integer

    Public MomentumVector As New System.Windows.Vector(0, 0)

    Public PublicGeometry() As PointF

#End Region

#Region "PrivateVars"

    Dim DesiredAngle As Single = 0

    Dim OldAngle As Single = 0

    Dim CurrentAngle As Single

    Dim Weight As Double = 0.1

    Dim Location As PointF = New PointF(0, 0)

    Dim OldLocation As PointF

    Dim ThisType As EntityTypes.Entities = Entities.Ship

    <NonSerialized()>
    Dim DesiredPen As Pen

    Dim Size As Single

    Dim AI As New AIModule

    Dim UnsizedGeom() As PointF = {New PointF(0, 0),
                               New PointF(0.5, 1),
                               New PointF(1, 0),
                               New PointF(0.5, 0.5),
                               New PointF(0, 0),
                               New PointF(0.5, 1)} ' this is actually critically important for the collision detection "system"

    Dim SizedGeom() As PointF

    Dim FreakingHitbox() As PointF

    Dim MyHealthMgr As HealthManager

    Dim Creator As Integer

    Dim IsAI As Boolean

#End Region

#Region "GettersAndSetters"

    Public Overrides Function FillPolygon() As Boolean
        Return True
    End Function

    Public Overrides Function GetPen() As System.Drawing.Pen
        Return DesiredPen
    End Function

    Public Overrides Function GetCreator() As Integer
        Return Creator
    End Function

    Public Overrides Function GetVector() As System.Windows.Vector
        Return MomentumVector
    End Function

    Public Overrides Function EntityType() As EntityTypes.Entities
        Return ThisType
    End Function

    Dim ThisID As Integer

    Public Overrides Function GetID() As Integer
        Return ThisID
    End Function

    Public Overrides Sub SetID(ByVal ID As Integer)
        ThisID = ID
    End Sub

    Public Overrides Function GetAngle() As Single
        Return CurrentAngle
    End Function

    Public Overrides Function GetCollisionable() As PointF()
        Return FreakingHitbox.ToArray()
    End Function

    Public Overrides Function GetHealthWad() As HealthManager.CurrentHealthHolder
        Return MyHealthMgr.Health
    End Function

#End Region

    Public Overrides Function ToString() As String

        Return "A GenericShip at " & Location.ToString & " with momentum " & MomentumVector.ToString & vbNewLine _
                    & vbTab & " Hull Health: " & MyHealthMgr.Health.HullPoints.ToString & " Shield Health: " & MyHealthMgr.Health.ShieldPoints.ToString & vbNewLine _
                    & vbTab & "LastHit: " & MyHealthMgr.LastHit.ToString & " CurrentTimer " & MainForm.Game.GameWatch.ElapsedMilliseconds.ToString

    End Function

    Public Overrides Sub Collided(ByVal OtherVector As System.Windows.Vector, ByVal OtherAngle As Single, ByVal OtherEntityType As Entities, ByVal OtherID As Integer)

        If OtherEntityType = Entities.Ship Then ' need to add math to do damage based on mass of other ship

            If OtherVector.Length > MomentumVector.Length Then

                MomentumVector += OtherVector * Weight

            ElseIf OtherVector.Length < MomentumVector.Length Then

                MomentumVector -= OtherVector * Weight

            ElseIf OtherVector.Length = MomentumVector.Length Then

                MomentumVector = New System.Windows.Vector(0, 0)

            End If

            Location = OldLocation

            CurrentAngle = OldAngle

        End If

        Dim EntityManager = MainForm.Game.EntityManager

        If EntityManager.WadOEntities(OtherID).GetCreator <> ThisID Then ' Don't want to be killed by your own children
            MyHealthMgr.Hit(OtherID, OtherEntityType)
        End If


    End Sub

    Overridable Function GetModel() As DrawModel
        Dim XWingFactory As New XWingModelCreator

        Return XWingFactory.GetAModel(New SizeF(0, 0))

    End Function

    Public Sub New(ByVal StartLocation As PointF, ByVal InitialAngle As Single, ByVal PassScheme As Integer, ByVal HealthWad As HealthManager.CurrentHealthHolder, ByVal Creator As Integer, ByVal Geom As Func(Of SizeF, DrawModel), ByVal IsAI As Boolean)

        Model = Geom(New SizeF(0, 0))

        CurrentAngle = InitialAngle
        Me.Location = StartLocation
        KeyScheme = PassScheme

        If Not IsAI Then
            LaserCooldown = 20
        End If

        MyHealthMgr = New HealthManager(HealthWad)

        If PassScheme = 0 Then
            DesiredPen = New Pen(Brushes.Red, 1)
        Else
            DesiredPen = New Pen(Brushes.LightBlue, 1)
        End If

        Me.IsAI = IsAI

        Update(0.1)

    End Sub

    Sub New()
        'serialization
    End Sub

    Dim DrawnAFrame As Boolean = False

    Public Overrides Sub Update(ByVal d As Double)

        If MyHealthMgr.Health.Dead Then

            MainForm.Game.EntityManager.AddToDeathRow(ThisID)

            Exit Sub

        End If

        MyHealthMgr.Update(d)

        OldLocation = Location

        OldAngle = CurrentAngle

        Dim AngleInRad As Double = ((CurrentAngle + 90) * Math.PI) / 180


        Dim ThisInput As List(Of Integer)

        If IsAI Then
            ThisInput = ComputeAI()
        Else

            ThisInput = New List(Of Integer)

            If Input.KeyStates(Keys.W) Then
                ThisInput.Add(Keys.W)
            End If

            If Input.KeyStates(Keys.A) Then
                ThisInput.Add(Keys.A)
            End If

            If Input.KeyStates(Keys.S) Then
                ThisInput.Add(Keys.S)
            End If
            If Input.KeyStates(Keys.D) Then
                ThisInput.Add(Keys.D)
            End If

            If Input.KeyStates(Keys.Q) Then
                ThisInput.Add(Keys.Q)
            End If
        End If

        ' I'm not 100% sure how this works, but it does
        ' So I'm not touching it

        If ThisInput.Contains(Keys.D) Then
            AdjustAngle(1.0F / 4.0F * CSng(d))
        End If

        If ThisInput.Contains(Keys.A) Then
            AdjustAngle(-1.0F / 4.0F * CSng(d))
        End If

        If ThisInput.Contains(Keys.W) Then
            MomentumVector += New System.Windows.Vector(Math.Cos(AngleInRad), Math.Sin(AngleInRad))
        End If

        If ThisInput.Contains(Keys.S) Then
            MomentumVector -= New System.Windows.Vector(0.25F * Math.Cos(AngleInRad), 0.25F * Math.Sin(AngleInRad))
        End If

        If ThisInput.Contains(Keys.Q) Then
            FireLaser()
        End If


        ' no point updating if nothing has changed

        'If (ThisInput.Count <> 0 And MomentumVector <> New Vector(0, 0)) Or Not DrawnAFrame Then

        DrawnAFrame = True
        Dim TransMatrix As New Matrix
        Dim ScreenOffsetMatrix As New Matrix

        ' Increase location by the momentum vector
        ScreenOffsetMatrix.Translate(MainForm.Game.ScreenOffset.X, MainForm.Game.ScreenOffset.Y)

        Location.X = CSng(Location.X + MomentumVector.X)
        Location.Y = CSng(Location.Y + MomentumVector.Y)

        ' Rotate by current angle
        Dim Temp22 = New PointF(CSng(Location.X + (Model.RotateAt(0).X)), CSng(Location.Y + (Model.RotateAt(0).Y)))

        Diagnostics.Thingie = Temp22
        TransMatrix.RotateAt(CurrentAngle, Temp22)

        TransMatrix.Translate(Location.X, Location.Y)



        ' This is hopefully creating a copy of Unsized Geom
        ' I don't actually know if it is, but I don't care,
        ' because it works the way I expect it to and dosen't
        ' deform due to floating point errors
        Try
            Dim Temp = Model.DrawableLayers(0).UnsizedGeom.ToArray()
            TransMatrix.TransformPoints(Temp)

            If Not DrawingUtils.PointInPolygon(Temp(0), {New PointF(0, 0), New PointF(Renderer.ScreenSize.Width, 0), New PointF(Renderer.ScreenSize.Width, Renderer.ScreenSize.Height), New PointF(0, Renderer.ScreenSize.Height), New PointF(0, 0)}) Then
                If Not IsAI Then
                    Location = OldLocation
                    MomentumVector = New Vector(0, 0)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("Oops")
        End Try
        

        For Each Item In Model.DrawableLayers

            ' Actually do the transformation on ThisPoints

            Item.PublicGeom = Item.UnsizedGeom.ToArray

            TransMatrix.TransformPoints(Item.PublicGeom)


        Next

        FreakingHitbox = Model.UnsizedHitbox.ToArray

        TransMatrix.TransformPoints(FreakingHitbox)

        Model.LaserSpawnPlaces = Model.UnsizedLaserSpawnPlayers.ToArray()

        TransMatrix.TransformPoints(Model.LaserSpawnPlaces)


        ' Cause momentum vector to lose speed over time
        ' This is a made up value

        MomentumVector.Y *= 0.99
        MomentumVector.X *= 0.99

    End Sub





    Public Overrides Function GetDrawModel() As DrawModel
        Model.Hitbox = PublicGeometry
        Return Model

    End Function


    Private Function ComputeAI() As List(Of Integer) ' I really want this to be a seperate, more flexible class, but I need to git er done


        'Dim Player As Entity = MainForm.Game.EntityManager.WadOEntities(MainForm.Game.PlayerID)
        'Dim PlayerLoc As PointF = Player.GetDrawModel.RotateAt(0)


        'Dim List As New List(Of Integer)

        '' Determine if we are close enough to actually do anything

        'If DrawingUtils.DistanceBetweenTwoPoints(Location, Player.GetDrawModel.RotateAt(0)) > 10000 Then
        '    Return List
        'End If



        'Dim Radians = Math.Atan2(PlayerLoc.X, PlayerLoc.Y)

        'Dim NeededAngle = Radians * (180 / Math.PI)


        'NeededAngle += 270



        'If NeededAngle < CurrentAngle Then
        '    List.Add(Keys.A)

        'ElseIf NeededAngle > CurrentAngle Then
        '    List.Add(Keys.D)

        'End If
        'Dim Fire As Boolean = False
        Dim List As New List(Of Integer)

        'If CurrentAngle >= NeededAngle - 10 And CurrentAngle <= NeededAngle + 10 Then
        '    List.Add(Keys.Q)
        'End If

        List.Add(Keys.Q)

        Return List



    End Function

    Sub AdjustAngle(ByVal AdjAngle As Single)

        CurrentAngle += AdjAngle

        If CurrentAngle > 360 Then
            CurrentAngle -= 360
        ElseIf CurrentAngle < 0 Then
            CurrentAngle += 360
        End If

    End Sub

    Dim LastShot As Integer
    Dim LastCannon As Integer
    Dim LaserCooldown As Integer = 500

    Sub FireLaser()
        If IsNothing(LastShot) And IsNothing(LastCannon) Then
            LastShot = 0
            LastCannon = 1
        End If

        If IsNothing(Model.LaserSpawnPlaces) Then
            Exit Sub
        End If

        If LastCannon = Model.LaserSpawnPlaces.Count - 1 Then
            LastCannon = 0
        Else
            LastCannon += 1
        End If

        If MainForm.Game.GameWatch.ElapsedMilliseconds - LastShot > LaserCooldown Then

            Dim TempEntity = New rectanglelaser(Model.LaserSpawnPlaces(LastCannon), 10, CurrentAngle, Color.Green, ThisID, 10)
            MainForm.Game.EntityManager.SpawnEntity(TempEntity)

            LastShot = CInt(MainForm.Game.GameWatch.ElapsedMilliseconds)


        End If
    End Sub
End Class
