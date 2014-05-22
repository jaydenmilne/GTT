Module DrawingUtils
    Public Function PointInPolygon(ByVal p As PointF, ByVal poly As PointF()) As Boolean
        Dim p1 As PointF, p2 As PointF

        Dim inside As Boolean = False

        If poly.Length < 3 Then
            Return inside
        End If

        Dim oldPoint As New PointF(poly(poly.Length - 1).X, poly(poly.Length - 1).Y)

        For i As Integer = 0 To poly.Length - 1
            Dim newPoint As New PointF(poly(i).X, poly(i).Y)

            If newPoint.X > oldPoint.X Then
                p1 = oldPoint
                p2 = newPoint
            Else
                p1 = newPoint
                p2 = oldPoint
            End If

            If (newPoint.X < p.X) = (p.X <= oldPoint.X) AndAlso (CLng(p.Y) - CLng(p1.Y)) * CLng(p2.X - p1.X) < (CLng(p2.Y) - CLng(p1.Y)) * CLng(p.X - p1.X) Then
                inside = Not inside
            End If

            oldPoint = newPoint
        Next

        Return inside
    End Function

    Public Function DistanceBetweenTwoPoints(ByVal Point1 As PointF, ByVal Point2 As PointF) As Double
        Return ((Point1.X - Point2.X) * (Point1.X - Point2.X) + (Point1.Y - Point2.Y) * (Point1.Y - Point2.Y)) ^ (1 / 2)
    End Function
End Module
