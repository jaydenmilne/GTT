Imports System.Drawing.Drawing2D

<Serializable()>
Public MustInherit Class Entity

    Public MustOverride Function EntityType() As EntityTypes.Entities

    Public MustOverride Function GetID() As Integer

    Public MustOverride Sub SetID(ByVal ID As Integer) ' Use only if you know exactly what you are doing

    Public MustOverride Function GetAngle() As Single

    Public MustOverride Function GetPen() As Pen

    Public MustOverride Function GetVector() As System.Windows.Vector

    Public MustOverride Function GetCollisionable() As PointF()

    Public MustOverride Function GetHealthWad() As HealthManager.CurrentHealthHolder

    Public MustOverride Function FillPolygon() As Boolean

    Public MustOverride Sub Collided(ByVal OtherVector As System.Windows.Vector, ByVal OtherAngle As Single, ByVal OtherEntityType As Entities, ByVal OtherID As Integer)

    Public MustOverride Function GetDrawModel() As DrawModel

    Sub New(ByVal StartLocation As PointF, ByVal InitialAngle As Single, ByVal PassScheme As Integer)
    End Sub

    Sub New()
    End Sub

    Public MustOverride Sub Update(ByVal d As Double)

    Public MustOverride Function GetCreator() As Integer



End Class
