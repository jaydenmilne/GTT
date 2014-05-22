Public Class EntityManager

    Private Const MaxEntities As Integer = 1024

    Public WadOEntities(MaxEntities) As Entity
    Public AvailibleSpots As New List(Of Integer)
    Public NumOfEntities As Integer = 0
    Public DeathRow As New List(Of Integer)
    Sub New()
        ' woooooo
    End Sub



    Public Sub AddEntity(ByVal EntityToAdd As Entity)

        If AvailibleSpots.Count <> 0 Then ' if there are no holes in array this will prevent out of bounds exception

            WadOEntities(AvailibleSpots(0)) = EntityToAdd ' must check to ensure that any value in AvailibleSpots <= MaxEntities
            WadOEntities(AvailibleSpots(0)).SetID(0)


            NumOfEntities += 1

            AvailibleSpots.RemoveAt(0)

        Else ' There are no holes in the array, so we can just add something to it

            If NumOfEntities > MaxEntities Then
                Throw New Exception("Too many entities!")
            Else
                WadOEntities(NumOfEntities) = EntityToAdd
                WadOEntities(NumOfEntities).SetID(NumOfEntities)

                NumOfEntities += 1

            End If


        End If



    End Sub

    Public Sub RemoveEntity(ByVal EntityToRemove As Integer)

        If NumOfEntities > 0 Then


            WadOEntities(EntityToRemove) = Nothing

            NumOfEntities -= 1

            AvailibleSpots.Add(EntityToRemove)

            AvailibleSpots.Sort()



        End If

        

    End Sub

    Public Sub UpdateAll(ByVal Elapsed As Double)
        For i As Integer = 0 To NumOfEntities + AvailibleSpots.Count
            If Not IsNothing(WadOEntities(i)) Then
                WadOEntities(i).Update(Elapsed)
            End If

        Next

        If DeathRow.Count <> 0 Then

            For a As Integer = 0 To DeathRow.Count - 1
                RemoveEntity(DeathRow(a))


                If a > DeathRow.Count - 1 Then
                    Exit For
                End If

            Next

            DeathRow.Clear()

        End If

    End Sub

    Public Sub AddToDeathRow(ByVal ID As Integer)

        If Not DeathRow.Contains(ID) Then
            DeathRow.Add(ID)
        Else
            ' whoop de do
        End If

    End Sub

    Public Sub UnDelete(ByVal ID As Integer)

    End Sub






End Class
