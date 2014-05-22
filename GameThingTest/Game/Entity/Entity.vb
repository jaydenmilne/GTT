﻿Imports System.Drawing.Drawing2D

Public MustInherit Class Entity

    Public MustOverride Function EntityType() As EntityTypes.Entities

    Public MustOverride Function GetID() As Integer

    Public MustOverride Sub SetID(ByVal ID As Integer) ' Use only if you know exactly what you are doing

    Public MustOverride Function GetAngle() As Single

    Public MustOverride Function GetPen() As Pen

    Public MustOverride Function GetVector() As System.Windows.Vector

    Public MustOverride Function GetPublicGeometry() As PointF()

    Public MustOverride Sub Collided(ByVal OtherVector As System.Windows.Vector, ByVal OtherAngle As Single, ByVal OtherEntityType As Entities)

    Sub New(ByVal StartLocation As PointF, ByVal InitialAngle As Single, ByVal PassScheme As Integer)
    End Sub

    Sub New()
    End Sub

    Public MustOverride Sub Update(ByVal d As Double)

End Class
