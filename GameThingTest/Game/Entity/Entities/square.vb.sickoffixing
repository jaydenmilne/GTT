Imports System.Drawing.Drawing2D

' this is for testing purposes only

Public Class Square : Inherits Entity



    Public ShadowsCurrentAngle As Single = 0

    Dim DesiredAngle As Single = 0

    Dim OldAngle As Single = 0

    Dim CurrentAngle As Single

    Dim Weight As Double = 0.1

    Public KeyScheme As Integer

    Dim ThisID As Integer

    Private DesiredPen As New Pen(Brushes.OrangeRed, 4)

    Public Overrides Function GetPen() As System.Drawing.Pen
        Return DesiredPen
    End Function

    Public Overrides Function GetVector() As System.Windows.Vector
        Return MomentumVector
    End Function

    Dim CurrentSize As Integer = 100
    Dim DesiredSize As Integer = 200

    Dim Location As PointF = New PointF(0, 0)
    Dim OldLocation As PointF

    Public Shadows MomentumVector As New System.Windows.Vector(0, 0)

    Public Shadows PublicGeometry() As PointF

    Dim Size As Single

    Public Overrides Function GetAngle() As Single
        Return CurrentAngle
    End Function

    Dim UnsizedGeom() As PointF = {New PointF(0, 0),
                                   New PointF(0, 1),
                                   New PointF(1, 1),
                                   New PointF(1, 0),
                                   New PointF(0, 0),
                                   New PointF(0, 1)}


    Dim SizedGeom() As PointF

    Public Overrides Function GetPublicGeometry() As PointF()
        Return PublicGeometry
    End Function

    Public Overrides Sub Collided(ByVal OtherVector As System.Windows.Vector, ByVal OtherAngle As Single, ByVal OtherEntityType As Entities, ByVal OtherID As Integer)

        If OtherVector.Length > MomentumVector.Length Then

            MomentumVector += OtherVector * Weight

        ElseIf OtherVector.Length < MomentumVector.Length Then

            MomentumVector -= OtherVector * Weight

        ElseIf OtherVector.Length = MomentumVector.Length Then

            MomentumVector = New System.Windows.Vector(0, 0)

        End If

        Location = OldLocation
        CurrentAngle = OldAngle



    End Sub

    Sub New(ByVal StartLocation As PointF, ByVal InitialAngle As Single, ByVal PassScheme As Integer)


        Dim TransMatrix As New Matrix
        Size = CSng(0.1 * Renderer.ScreenSize.Width)
        TransMatrix.Scale(Size * 0.75F, Size)

        Location = StartLocation

        CurrentAngle = InitialAngle

        SizedGeom = UnsizedGeom

        TransMatrix.TransformPoints(SizedGeom)

        KeyScheme = PassScheme

    End Sub

    Public Overrides Sub Update(ByVal d As Double)

        Dim TransMatrix As New Matrix
        Dim ThisPoints() As PointF

        OldLocation = Location
        OldAngle = CurrentAngle

        Dim AngleInRad As Double = ((CurrentAngle + 90) * Math.PI) / 180

        If KeyScheme = 0 Then
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

        ' Finish update

        PublicGeometry = ThisPoints.ToArray



    End Sub

    Dim ThisType As EntityTypes.Entities = Entities.Laser

    Public Overrides Function EntityType() As EntityTypes.Entities
        Return ThisType
    End Function

    Public Overrides Function GetID() As Integer
        Return ThisID
    End Function

    Public Overrides Sub SetID(ByVal ID As Integer)
        ThisID = ID
    End Sub
End Class
