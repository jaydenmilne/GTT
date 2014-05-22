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
            ThisVector += New System.Windows.Vector(Math.Cos(AngleInRad) * 5, Math.Sin(AngleInRad) * 5)
        End If

        If Input.KeyStates(Keys.Down) Then
            CurrentAngle -= 1.0F / 2.0F * CSng(d)
        End If



        ThisPoints = UnsizedGeom.ToArray

        TransMatrix.RotateAt(CurrentAngle, New PointF(CSng(LocationX + 50 + ThisVector.X), CSng(LocationY + 50 + ThisVector.Y)))

        TransMatrix.Translate(CSng(ThisVector.X) + LocationX, CSng(ThisVector.Y + LocationY))

        TransMatrix.TransformPoints(ThisPoints)

        PublicGeometry = ThisPoints.ToArray



    End Sub


End Class
