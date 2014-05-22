Imports System.Windows
Imports System.Drawing.Drawing2D
Imports System.Runtime.Serialization
Imports System.Xml
<Serializable()>
Public Class rectanglelaser : Inherits Entity

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

    <NonSerialized()>
    Dim MyPen As Pen

    Dim MyVector As Vector = New Vector(0, 0)

    Dim PublicGeom() As PointF

    Dim UnMoved() As PointF

    Dim Size As Single

    Dim AngleInRad As Double

    Dim Creator As Integer

    Dim Damage As Double

    Dim PenColor As Color


#Region "GettersAndSetters"

    Sub New()
        'serialization
    End Sub

    Public Overrides Function GetCreator() As Integer
        Return Creator
    End Function

    Public Overrides Function FillPolygon() As Boolean
        Return True
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

    <OnDeserialized()> _
    Friend Sub Reinitialize(ByVal context As StreamingContext)
        MyPen = New Pen(PenColor)
    End Sub

    Sub New(ByVal PassedStartLocation As PointF, ByVal PassedLength As Integer, ByVal PassedAngle As Single, ByVal PassedPenColor As Color, ByVal Spawner As Integer, ByVal PassedDamage As Double)
        Length = PassedLength
        StartLocation = PassedStartLocation
        CurrentLocation = StartLocation
        Angle = PassedAngle
        Creator = Spawner
        MyPen = New Pen(PassedPenColor)
        PenColor = PassedPenColor

        AngleInRad = ((Angle + 90) * Math.PI) / 180

        MyVector = New Vector(Math.Cos(AngleInRad), Math.Sin(AngleInRad))

        ' 5 is a hacked in width value, must be changed later

        UnMoved = {New PointF(0, 0),
                   New PointF(0, Length),
                   New PointF(5, Length),
                   New PointF(5, 0),
                   New PointF(0, 0),
                   New PointF(0, Length)}

        Size = Length

        Damage = PassedDamage


        Update(0)

    End Sub

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

        TransMatrix.Rotate(Angle)


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
