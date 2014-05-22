Public Module AlternateCollisionDetector


    ' Next two functions liberated from http://stackoverflow.com/questions/924171/geo-fencing-point-inside-outside-polygon
    ' Ported to VB by yours truly
    ' The old way was faster for me, so I ditched this.

    Public Function PointInPolygon(p As PointF, poly As List(Of PointF)) As Boolean
        Dim n As Integer = poly.Count()

        poly.Add(New PointF(poly(0).X, poly(0).Y))

        Dim v() As PointF = poly.ToArray()

        Dim wn As Integer = 0
        ' the winding number counter
        ' loop through all edges of the polygon
        For i As Integer = 0 To n - 1
            ' edge from V[i] to V[i+1]
            If v(i).Y <= p.Y Then
                ' start y <= P.y
                If v(i + 1).Y > p.Y Then
                    ' an upward crossing
                    If isLeft(v(i), v(i + 1), p) > 0 Then
                        ' P left of edge
                        wn += 1
                    End If
                    ' have a valid up intersect
                End If
            Else
                ' start y > P.y (no test needed)
                If v(i + 1).Y <= p.Y Then
                    ' a downward crossing
                    If isLeft(v(i), v(i + 1), p) < 0 Then
                        ' P right of edge
                        wn -= 1
                    End If
                    ' have a valid down intersect
                End If
            End If
        Next
        If wn <> 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function isLeft(P0 As PointF, P1 As PointF, P2 As PointF) As Integer
        Dim calc As Double = ((P1.X - P0.X) * (P2.Y - P0.Y) - (P2.X - P0.X) * (P1.Y - P0.Y))

        If calc > 0 Then
            Return 1
        ElseIf calc < 0 Then
            Return -1
        Else
            Return 0
        End If

    End Function



    Public Sub CheckAllForCollision(ByVal entities() As Entity, ByVal LastEntity As Integer)


        If Input.KeyStates(Keys.L) Then
            Console.WriteLine("your mom") ' I have no idea why this is here
        End If

        If LastEntity <= 1 Then
            Exit Sub
        End If


        For CurrentEntity As Integer = 0 To LastEntity

            If IsNothing(entities(CurrentEntity)) Then
                Continue For
            End If


            If IsNothing(entities(CurrentEntity).GetPublicGeometry) Then
                Continue For
            End If

            For LoopedEntity As Integer = 0 To LastEntity

                If LoopedEntity = CurrentEntity Then
                    Continue For
                End If

                If IsNothing(entities(LoopedEntity)) Then
                    Continue For
                End If


                If IsNothing(entities(LoopedEntity).GetPublicGeometry) Then
                    Continue For
                End If

                For LoopedGeom As Integer = 0 To entities(LoopedEntity).GetPublicGeometry.Length - 1

                    If PointInPolygon(entities(LoopedEntity).GetPublicGeometry(LoopedGeom), entities(CurrentEntity).GetPublicGeometry.ToList) Then


                        Dim LoopedVector As System.Windows.Vector = entities(LoopedEntity).GetVector()
                        Dim OutsideVector As System.Windows.Vector = entities(CurrentEntity).GetVector()

                        entities(LoopedEntity).Collided(OutsideVector, entities(CurrentEntity).GetAngle, entities(CurrentEntity).EntityType(), entities(CurrentEntity).GetID)
                        entities(CurrentEntity).Collided(LoopedVector, entities(LoopedEntity).GetAngle, entities(LoopedEntity).EntityType, entities(LoopedEntity).GetID)
                    End If

                Next

            Next



        Next


    End Sub





End Module
