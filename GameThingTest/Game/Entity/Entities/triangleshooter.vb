Imports System
Imports System.Runtime.Serialization
Imports System.Xml
Imports System.Drawing.Drawing2D
<Serializable()>
Public Class TriangleShooter : Inherits Entity

    <OnDeserialized()> _
    Friend Sub Reinitialize(ByVal context As StreamingContext)

        If KeyScheme = 0 Then
            DesiredPen = New Pen(Brushes.Red, 1)
        Else
            DesiredPen = New Pen(Brushes.LightBlue, 1)
        End If

    End Sub


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

    Dim UnsizedGeom() As PointF = {New PointF(0, 0),
                               New PointF(0.5, 1),
                               New PointF(1, 0),
                               New PointF(0.5, 0.5),
                               New PointF(0, 0),
                               New PointF(0.5, 1)} ' this is actually critically important for the collision detection system

    Dim SizedGeom() As PointF

    Dim MyHealthMgr As HealthManager

    Dim Creator As Integer

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

    Public Overrides Function GetPublicGeometry() As PointF()
        Return PublicGeometry
    End Function

    Public Overrides Function GetHealthWad() As HealthManager.CurrentHealthHolder
        Return MyHealthMgr.Health
    End Function

#End Region

    Public Overrides Function ToString() As String

        Return "A TriangleShooter at " & Location.ToString & " with momentum " & MomentumVector.ToString & " and keyscheme " & KeyScheme.ToString & vbNewLine _
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

    Sub New(ByVal StartLocation As PointF, ByVal InitialAngle As Single, ByVal PassScheme As Integer, ByVal HealthWad As HealthManager.CurrentHealthHolder, ByVal Creator As Integer)

        Dim TransMatrix As New Matrix

        Size = CSng(0.03 * Renderer.ScreenSize.Width)

        TransMatrix.Scale(Size * 0.75F, Size)

        Location = StartLocation

        CurrentAngle = InitialAngle

        SizedGeom = UnsizedGeom

        TransMatrix.TransformPoints(SizedGeom)

        KeyScheme = PassScheme

        MyHealthMgr = New HealthManager(HealthWad)

        If PassScheme = 0 Then
            DesiredPen = New Pen(Brushes.Red, 1)
        Else
            DesiredPen = New Pen(Brushes.LightBlue, 1)
        End If

    End Sub

    Sub New()
        'serialization
    End Sub

    Public Overrides Sub Update(ByVal d As Double)

        If MyHealthMgr.Health.Dead Then

            MainForm.Game.EntityManager.AddToDeathRow(ThisID)

            Exit Sub

        End If

        MyHealthMgr.Update(d)

        Dim TransMatrix As New Matrix

        Dim ThisPoints() As PointF

        OldLocation = Location

        OldAngle = CurrentAngle

        Dim AngleInRad As Double = ((CurrentAngle + 90) * Math.PI) / 180

        Dim rand As New Random()


        If KeyScheme = 0 Then ' this is a dirty hack that I need to fix
            If Input.KeyStates(Keys.Right) Then
                CurrentAngle += 1.0F / 2.0F * CSng(d)
            End If

            If Input.KeyStates(Keys.Left) Then
                CurrentAngle -= 1.0F / 2.0F * CSng(d)
            End If


            ' I'm not 100% sure how this works, but it does
            ' So I'm not touching it

            If Input.KeyStates(Keys.Up) Then
                MomentumVector += New System.Windows.Vector(Math.Cos(AngleInRad), Math.Sin(AngleInRad))
            End If

            If Input.KeyStates(Keys.Down) Then
                MomentumVector -= New System.Windows.Vector(0.25F * Math.Cos(AngleInRad), 0.25F * Math.Sin(AngleInRad))
            End If

            If Input.KeyStates(Keys.NumPad0) Then
                If Not IsNothing(PublicGeometry) Then
                    MainForm.Game.EntityManager.SpawnEntity(New rectanglelaser(PublicGeometry(1), 100, CurrentAngle, Color.Green, ThisID, 10))
                End If
            End If
        Else
            If Input.KeyStates(Keys.D) Then
                CurrentAngle += 1.0F / 2.0F * CSng(d)
            End If

            If Input.KeyStates(Keys.A) Then
                CurrentAngle -= 1.0F / 2.0F * CSng(d)
            End If


            ' I'm not 100% sure how this works, but it does
            ' So I'm not touching it

            If Input.KeyStates(Keys.W) Then
                MomentumVector += New System.Windows.Vector(Math.Cos(AngleInRad), Math.Sin(AngleInRad))
            End If

            If Input.KeyStates(Keys.S) Then
                MomentumVector -= New System.Windows.Vector(0.25F * Math.Cos(AngleInRad), 0.25F * Math.Sin(AngleInRad))
            End If

            If Input.KeyStates(Keys.Q) Then
                MainForm.Game.EntityManager.SpawnEntity(New rectanglelaser(PublicGeometry(1), 100, CurrentAngle, Color.Green, ThisID, 10))
            End If

        End If




        ' This is hopefully creating a copy of Unsized Geom
        ' I don't actually know if it is, but I don't care,
        ' because it works the way I expect it to and dosen't
        ' deform due to floating point errors

        ThisPoints = UnsizedGeom.ToArray

        ' Do rotation momentum stuff

        'CurrentAngle += DesiredAngle

        'DesiredAngle *= 0.999D

        ' Increase location by the momentum vector

        Location.X = CSng(Location.X + MomentumVector.X)
        Location.Y = CSng(Location.Y + MomentumVector.Y)

        ' Rotate by current angle

        TransMatrix.RotateAt(CurrentAngle, New PointF(CSng(Location.X + Size * 0.75F / 2), CSng(Location.Y + Size / 2)))

        TransMatrix.Translate(Location.X, Location.Y)

        ' Cause momentum vector to lose speed over time
        ' This is a made up value

        MomentumVector.Y *= 0.99
        MomentumVector.X *= 0.99

        ' Actually do the transformation on ThisPoints

        TransMatrix.TransformPoints(ThisPoints)

        TransMatrix.Reset()
        TransMatrix.Translate(MainForm.Game.ScreenOffset.X, MainForm.Game.ScreenOffset.Y)
        TransMatrix.TransformPoints(ThisPoints)

        ' Finish update

        PublicGeometry = ThisPoints.ToArray

    End Sub





End Class
