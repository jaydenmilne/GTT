Public Class XWingFighter : Inherits GenericShip

    Overrides Function GetModel() As DrawModel

        Dim XWingFactory As New XWingModelCreator

        Return XWingFactory.GetAModel(New SizeF(0, 0))

    End Function

End Class
