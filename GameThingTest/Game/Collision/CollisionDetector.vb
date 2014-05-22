Public Module CollisionDetector

    ' Liberated from http://stackoverflow.com/questions/9043805/test-if-two-lines-intersect-javascript-function

    Private Function CheckCollision(ByVal p1 As PointF, ByVal p2 As PointF, ByVal p3 As PointF, ByVal p4 As PointF) As Boolean

        Return (FancyMath(p1, p3, p4) <> FancyMath(p2, p3, p4)) And (FancyMath(p1, p2, p3) <> FancyMath(p1, p2, p4))

    End Function

    Private Function FancyMath(ByVal p1 As PointF, ByVal p2 As PointF, ByVal p3 As PointF) As Boolean
        Dim a As Single = p1.X
        Dim b As Single = p1.Y
        Dim c As Single = p2.X
        Dim d As Single = p2.Y
        Dim e As Single = p3.X
        Dim f As Single = p3.Y


        Return (f - b) * (c - a) > (d - b) * (e - a)

    End Function

    Public Sub CheckAllForCollission(ByVal entities() As TriangleShooter, ByVal NumberOfEntities As Integer)


        If NumberOfEntities <= 2 Then
            Exit Sub
        End If

        If IsNothing(entities(NumberOfEntities - 1)) Then
            Exit Sub
        End If

        For CurrentEntity As Integer = 0 To NumberOfEntities - 1 ' loop over all entities

            If IsNothing(entities(CurrentEntity)) Then ' skip nulls
                Continue For
            End If

            For CurrentGeometry As Integer = 0 To entities(CurrentEntity).PublicGeometry.Length - 2 ' -2 because .length returns 1 based value (array expects 0 based) and we never want (CurrentGeom + 1) to fail
                ' Loop over all points
                For LoopedEntity As Integer = 0 To NumberOfEntities - 1

                    If IsNothing(entities(LoopedEntity)) Then
                        Continue For
                    End If

                    If LoopedEntity = CurrentEntity Then ' Don't want to check own entity for collisions
                        Exit For
                    End If

                    For LoopedGeometry As Integer = 0 To entities(LoopedEntity).PublicGeometry.Length - 2
                        If CheckCollision(entities(CurrentEntity).PublicGeometry(CurrentGeometry),
                                       entities(CurrentEntity).PublicGeometry(CurrentGeometry + 1),
                                       entities(LoopedEntity).PublicGeometry(LoopedGeometry),
                                       entities(LoopedEntity).PublicGeometry(LoopedGeometry + 1)) Then

                            entities(LoopedEntity).Collided()
                            entities(CurrentEntity).Collided()


                        End If

                    Next
                Next
            Next
        Next


    End Sub





End Module
