Imports System.Drawing.Drawing2D

Public Class TriangleShooter : Inherits Entity

    Dim CurrentAngle As Single = 0
    Dim DesiredAngle As Single = 0

    Public KeyScheme As Integer

    Public Shadows DesiredPen As New Pen(Brushes.OrangeRed, 4)

    Dim CurrentSize As Integer = 100
    Dim DesiredSize As Integer = 200

    Dim Location As PointF = New PointF(0, 0)

    Dim MomentumVector As New System.Windows.Vector(0, 0)

    Public Shadows PublicGeometry() As PointF

    Dim Size As Single

    Dim UnsizedGeom() As PointF = {New PointF(0, 0),
                                   New PointF(0.5, 1),
                                   New PointF(1, 0),
                                   New PointF(0.5, 0.5),
                                   New PointF(0, 0),
                                   New PointF(0.5, 1)}

    Dim SizedGeom() As PointF

    Public Sub Collided()
        MomentumVector = New System.Windows.Vector(0, 0)
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
    Public Overloads Sub Update(ByVal d As Double)

        Dim TransMatrix As New Matrix
        Dim ThisPoints() As PointF

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


End Class
