Imports System.Drawing.Drawing2D

Public Class TriangleShooter

    Dim CurrentAngle As Single = 0
    Dim DesiredAngle As Single = 0

    Public DesiredPen As New Pen(Brushes.OrangeRed, 4)

    Dim CurrentSize As Integer = 100
    Dim DesiredSize As Integer = 200

    Dim LocationX As Single = 0
    Dim LocationY As Single = 0

    Dim ThisVector As New System.Windows.Vector(0, 0)




    Public PublicGeometry() As PointF
    Dim UnsizedGeom() As PointF = {New PointF(0, 0),
                                   New PointF(0.5, 1),
                                   New PointF(1, 0),
                                   New PointF(0.5, 0.5),
                                   New PointF(0, 0)}
    Dim SizedGeom() As PointF
    Sub New()

        Dim TransMatrix As New Matrix
        TransMatrix.Scale(100, 100)

        SizedGeom = UnsizedGeom

        TransMatrix.TransformPoints(SizedGeom)

    End Sub
    Public Sub Update(ByVal d As Double)

        Dim TransMatrix As New Matrix
        Dim ThisPoints() As PointF

        Dim AngleInRad As Double = ((CurrentAngle + 90) * Math.PI) / 180

        If Input.KeyStates(Keys.Right) Then
            CurrentAngle += 1.0F / 2.0F * CSng(d)
        End If

        If Input.KeyStates(Keys.Left) Then
            CurrentAngle -= 1.0F / 2.0F * CSng(d)
        End If

        If Input.KeyStates(Keys.Up) Then
            ThisVector += New System.Windows.Vector(Math.Cos(AngleInRad), Math.Sin(AngleInRad))
        End If

        If Input.KeyStates(Keys.Down) Then
            ThisVector -= New System.Windows.Vector(Math.Cos(AngleInRad), Math.Sin(AngleInRad))
        End If


        ' This is hopefully creating a copy of Unsized Geom
        ' I don't actually know if it is, but I don't care,
        ' because it works the way I expect it to and dosen't
        ' deform due to floating point errors

        ThisPoints = UnsizedGeom.ToArray

        ' Increase location by the momentum vector

        LocationX = CSng(LocationX + ThisVector.X)
        LocationY = CSng(LocationY + ThisVector.Y)

        ' Rotate by current angle, assuming that the figure is 100 px by 100 px
        ' so 50 px is the center

        TransMatrix.RotateAt(CurrentAngle, New PointF(CSng(LocationX + 50), CSng(LocationY + 50)))

        ' Translate it 

        TransMatrix.Translate(LocationX, LocationY)

        ' Cause momentum vector to lose speed over time
        ' This is a made up value

        ThisVector.Y *= 0.99
        ThisVector.X *= 0.99

        ' Actually do the transformation on ThisPoints

        TransMatrix.TransformPoints(ThisPoints)

        ' Finish update

        PublicGeometry = ThisPoints.ToArray



    End Sub


End Class
