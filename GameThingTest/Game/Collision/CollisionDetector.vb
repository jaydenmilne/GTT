Public Module CollisionDetector

    ' Liberated from http://stackoverflow.com/questions/9043805/test-if-two-lines-intersect-javascript-function
    ' this is a big, nasty buggy class, but I don't care! Wow!

    Private Function CheckCollision(ByVal p1 As PointF, ByVal p2 As PointF, ByVal p3 As PointF, ByVal p4 As PointF) As Boolean

        Return (FancyMath(p1, p3, p4) <> FancyMath(p2, p3, p4)) And (FancyMath(p1, p2, p3) <> FancyMath(p1, p2, p4))

    End Function

    Private Function FancyMath(ByVal p1 As PointF, ByVal p2 As PointF, ByVal p3 As PointF) As Boolean

        Return (p3.Y - p1.Y) * (p2.X - p1.X) > (p2.Y - p1.Y) * (p3.X - p1.X)

    End Function

    Public Sub CheckAllForCollision(ByVal entities() As Entity, ByVal LastEntity As Integer)


        If Input.KeyStates(Keys.L) Then
            Console.WriteLine("your mom") ' I have no idea why this is here
        End If

        If LastEntity <= 1 Then
            Exit Sub
        End If

        For CurrentEntity As Integer = 0 To LastEntity - 1 ' loop over all entities

            If IsNothing(entities(CurrentEntity)) Then ' skip nulls
                Continue For
            End If

            For CurrentGeometry As Integer = 0 To entities(CurrentEntity).GetPublicGeometry().Length - 2 ' -2 because .length returns 1 based value (array expects 0 based) and we never want (CurrentGeom + 1) to fail
                ' Loop over all points
                For LoopedEntity As Integer = 0 To LastEntity - 1

                    If LoopedEntity = CurrentEntity Then ' Don't want to check own entity for collisions
                        Exit For
                    End If

                    If IsNothing(entities(LoopedEntity)) Then
                        Continue For
                    End If

                    For LoopedGeometry As Integer = 0 To entities(LoopedEntity).GetPublicGeometry().Length - 2

                        If CheckCollision(entities(CurrentEntity).GetPublicGeometry()(CurrentGeometry),
                                       entities(CurrentEntity).GetPublicGeometry()(CurrentGeometry + 1),
                                       entities(LoopedEntity).GetPublicGeometry()(LoopedGeometry),
                                       entities(LoopedEntity).GetPublicGeometry()(LoopedGeometry + 1)) Then


                            Dim LoopedVector As System.Windows.Vector = entities(LoopedEntity).GetVector()
                            Dim OutsideVector As System.Windows.Vector = entities(CurrentEntity).GetVector()

                            entities(LoopedEntity).Collided(OutsideVector, entities(CurrentEntity).GetAngle, entities(CurrentEntity).EntityType(), entities(CurrentEntity).GetID)
                            entities(CurrentEntity).Collided(LoopedVector, entities(LoopedEntity).GetAngle, entities(LoopedEntity).EntityType, entities(LoopedEntity).GetID)






                        End If

                    Next
                Next
            Next
        Next


    End Sub





End Module
