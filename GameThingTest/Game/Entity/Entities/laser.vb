﻿Imports System.Windows
Imports System.Drawing.Drawing2D

'
''
'
'
'
''
'   THIS CLASS IS DEPRECIATED, USE RECTANGLELASER INSTEAD
'
'
'
' kthanks jayden
'
'

'

Public Class laser : Inherits Entity

    Public Enum LaserAnimationStatus
        Starting
        Cruising
        Ending
    End Enum


    Dim ThisType As Entities = Entities.Laser

    Dim ThisID As Integer

    Dim StartLocation As PointF

    Dim CurrentLocation As PointF

    Dim Length As Integer

    Dim Angle As Single

    Dim MyPen As Pen

    Dim MyVector As Vector = New Vector(0, 0)

    Dim PublicGeom() As PointF

    Dim UnMoved() As PointF

    Dim Size As Single

    Dim AngleInRad As Double

    Dim Creator As Integer

    Dim Damage As Double


#Region "GettersAndSetters"

    Public Overrides Function GetCreator() As Integer
        Return Creator
    End Function

    Public Overrides Function GetHealthWad() As HealthManager.CurrentHealthHolder

        Dim Temp As New HealthManager.CurrentHealthHolder
        Temp.Dead = False
        Temp.Damage = Damage
        Temp.ShieldPoints = 0
        Temp.HullPoints = 0

        Return Temp



    End Function


    Public Overrides Function GetAngle() As Single
        Return Angle
    End Function

    Public Overrides Function GetPen() As System.Drawing.Pen
        Return MyPen
    End Function

    Public Overrides Function GetPublicGeometry() As System.Drawing.PointF()
        Return PublicGeom
    End Function

    Public Overrides Function GetVector() As System.Windows.Vector

    End Function

    Public Overrides Function GetID() As Integer
        Return ThisID
    End Function

    Public Overrides Sub SetID(ByVal ID As Integer)
        ThisID = ID
    End Sub

    Public Overrides Function EntityType() As EntityTypes.Entities
        Return ThisType
    End Function

#End Region

    Sub New(ByVal PassedStartLocation As PointF, ByVal PassedLength As Integer, ByVal PassedAngle As Single, ByVal PassedPenColor As Brush, ByVal Spawner As Integer, ByVal PassedDamage As Double)
        Length = PassedLength
        StartLocation = PassedStartLocation
        CurrentLocation = StartLocation
        Angle = PassedAngle
        Creator = Spawner
        MyPen = New Pen(PassedPenColor, 5)

        AngleInRad = ((Angle + 90) * Math.PI) / 180

        MyVector = New Vector(Math.Cos(AngleInRad), Math.Sin(AngleInRad))

        UnMoved = {New PointF(0, 0),
                   New PointF(CSng(MyVector.X * Length), CSng(MyVector.Y * Length))}

        Size = Length

        Damage = PassedDamage

        Update(0)

    End Sub

    Public Overrides Function FillPolygon() As Boolean
        Return False
    End Function

    Public Overrides Sub Collided(ByVal OtherVector As System.Windows.Vector, ByVal OtherAngle As Single, ByVal OtherEntityType As Entities, ByVal OtherID As Integer)

        If OtherEntityType <> Entities.Laser Then
            MainForm.Game.EntityManager.AddToDeathRow(ThisID)
        End If

    End Sub

    Public Overrides Function ToString() As String
        Return "A laser at " & CurrentLocation.ToString & "With vector {" & CStr(MyVector.X) & ":" & CStr(MyVector.Y) & "}"
    End Function


    Public Overrides Sub Update(ByVal d As Double)
        Dim TransMatrix As New Matrix

        Dim ThisPoints() As PointF

        ThisPoints = UnMoved.ToArray()

        CurrentLocation.X = CSng(CurrentLocation.X + (MyVector.X * d))

        CurrentLocation.Y = CSng(CurrentLocation.Y + (MyVector.Y * d))


        TransMatrix.Translate(CurrentLocation.X, CurrentLocation.Y)

        TransMatrix.TransformPoints(ThisPoints)

        TransMatrix.Reset()

        TransMatrix.Translate(MainForm.Game.ScreenOffset.X, MainForm.Game.ScreenOffset.Y)

        TransMatrix.TransformPoints(ThisPoints)


        PublicGeom = ThisPoints.ToArray()

        If DrawingUtils.DistanceBetweenTwoPoints(CurrentLocation, StartLocation) > 3000 Then
            MainForm.Game.EntityManager.AddToDeathRow(ThisID)
        End If

    End Sub



End Class
