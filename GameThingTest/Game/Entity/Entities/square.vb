Imports System.Drawing.Drawing2D

Public Class square




    Dim CurrentAngle As Integer = 0
    Dim DesiredAngle As Integer = 0

    Dim CurrentSize As Integer = 100
    Dim DesiredSize As Integer = 200

    Dim LocationX As Single = 0
    Dim LocationY As Single = 0

    Public PublicGeometry(5) As PointF
    Dim UnsizedGeom() As PointF = {New PointF(0.01, 0.01),
                                   New PointF(0.01, 1),
                                   New PointF(1, 1),
                                   New PointF(1, 0.01),
                                    New PointF(0.01, 0.01)}
    Dim SizedGeom() As PointF
    Sub New()

        Dim TransMatrix As New Matrix
        TransMatrix.Scale(100, 100)

        SizedGeom = UnsizedGeom

        TransMatrix.TransformPoints(SizedGeom)

    End Sub
    Public Sub Update(ByVal d As Double)

        If CurrentAngle < DesiredAngle Then
            CurrentAngle += 1
        End If

        Dim TransMatrix As New Matrix
        Dim ThisPoints() As PointF

        If Input.KeyStates(Keys.Left) Then
            LocationX += 1 * CSng(d)
        End If

        If Input.KeyStates(Keys.Right) Then
            LocationX -= 1 * CSng(d)
        End If

        ThisPoints = UnsizedGeom.ToArray

        TransMatrix.RotateAt(CurrentAngle, New Point(CInt(CurrentSize / 2), CInt(CurrentSize / 2)))

        TransMatrix.Translate(LocationX, LocationY)

        TransMatrix.TransformPoints(ThisPoints)

        PublicGeometry = ThisPoints.ToArray



    End Sub


End Class
