Imports System.Windows
Imports System.Drawing.Drawing2D
Public Class laser : Inherits Entity

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

#Region "GettersAndSetters"

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

    Sub New(ByVal PassedStartLocation As PointF, ByVal PassedLength As Integer, ByVal PassedAngle As Single, ByVal PassedPenColor As Brush, ByVal Spawner As Integer)
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

        Update(0)

    End Sub

    Public Overrides Sub Collided(ByVal OtherVector As System.Windows.Vector, ByVal OtherAngle As Single, ByVal OtherEntityType As Entities, ByVal OtherID As Integer)

        If OtherID <> Creator And OtherEntityType <> Entities.Laser Then
            MainForm.Game.EntityManager.AddToDeathRow(OtherID)
        Else
            'is is its creator, and we don't want to blow that up
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


        PublicGeom = ThisPoints.ToArray()

        If DrawingUtils.DistanceBetweenTwoPoints(CurrentLocation, StartLocation) > 3000 Then
            MainForm.Game.EntityManager.AddToDeathRow(ThisID)
        End If

    End Sub


End Class
