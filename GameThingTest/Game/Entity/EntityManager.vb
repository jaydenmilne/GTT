Public Class EntityManager

    Private Const MaxEntities As Integer = 1024

    Public WadOEntities(MaxEntities) As Entity
    Public AvailibleSpots As New List(Of Integer)
    Public NumOfEntities As Integer = 1
    Public DeathRow As New List(Of Integer)
    Public LastEntity As Integer = 0


    Private Sub AddEntity(ByVal Index As Integer, ByVal EntityToAdd As Entity)

        If Index > LastEntity Then
            LastEntity = Index
        End If

        WadOEntities(Index) = EntityToAdd

        WadOEntities(Index).SetID(Index)

        NumOfEntities += 1

    End Sub


    Public Function SpawnEntity(ByVal EntityToAdd As Entity) As Integer

        Dim Index As Integer = 0

        While Not IsNothing(WadOEntities(Index))
            Index += 1
            If Index > MaxEntities Then
                Throw New Exception("There are too many entities. Either you glitched it, which is your fault, or you hacked it, which is also your fault. In no way could it possibly be my fault, silly. You're bad!")
            End If
        End While

        AddEntity(Index, EntityToAdd)

        Return Index

    End Function



    Private Sub RemoveEntity(ByVal EntityToRemove As Integer)


        If IsNothing(WadOEntities(EntityToRemove)) Then
            Throw New Exception("Tried to remove an already removed entity. You're bad!")
        End If

        WadOEntities(EntityToRemove) = Nothing

        If EntityToRemove = LastEntity Then

            Dim Index As Integer = MaxEntities

            While IsNothing(WadOEntities(Index))
                Index -= 1
            End While

            LastEntity = Index


        End If


        NumOfEntities -= 1

    End Sub

    Public Sub ExecutePrisoners()

        For Each Item In DeathRow
            RemoveEntity(Item)
        Next

        DeathRow.Clear()

    End Sub


    Public Sub UpdateAll(ByVal Elapsed As Double)
        For i As Integer = 0 To LastEntity
            If Not IsNothing(WadOEntities(i)) Then
                WadOEntities(i).Update(Elapsed)
            End If

        Next


    End Sub

    Public Sub AddToDeathRow(ByVal ID As Integer)


        If Not DeathRow.Contains(ID) Then
            DeathRow.Add(ID)
        End If

    End Sub

End Class
