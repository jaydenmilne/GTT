Public Class EntityManager

    Private Const MaxEntities As Integer = 10

    Public WadOEntities(MaxEntities) As TriangleShooter
    Public AvailibleSpots As New List(Of Integer)
    Public NumOfEntities As Integer = 0
    Sub New()

    End Sub



    Public Sub AddEntity(ByVal EntityToAdd As TriangleShooter)

        If AvailibleSpots.Count <> 0 Then ' if there are no holes in array this will prevent out of bounds exception

            WadOEntities(AvailibleSpots(0)) = EntityToAdd ' must check to ensure that any value in AvailibleSpots <= MaxEntities

            NumOfEntities += 1

            AvailibleSpots.RemoveAt(0)

        Else ' There are no holes in the array, so we can just add something to it

            If NumOfEntities > MaxEntities Then
                Throw New Exception("Too many entities!")
            Else
                WadOEntities(NumOfEntities) = EntityToAdd

                NumOfEntities += 1

            End If


        End If



    End Sub

    Public Sub RemoveEntity(ByVal EntityToRemove As Integer)

        WadOEntities(EntityToRemove) = Nothing

        NumOfEntities -= 1

        If EntityToRemove <= MaxEntities Then

            AvailibleSpots.Add(EntityToRemove)

            AvailibleSpots.Sort()

        Else
            Throw New Exception("Entity value is greater than MaxEntities!")
        End If



    End Sub

    Public Sub UpdateAll(ByVal Elapsed As Double)
        For i As Integer = 0 To NumOfEntities - 1
            WadOEntities(i).Update(Elapsed)
        Next
    End Sub






End Class
