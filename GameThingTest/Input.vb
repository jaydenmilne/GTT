Public Class Input
    ' Pretty amazing class, amiright?
    Public Shared KeyStates(256) As Boolean

    Shared Sub KeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        KeyStates(e.KeyCode) = True

        If e.KeyCode = Keys.End Then
            End
        End If

    End Sub

    Shared Sub KeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
        KeyStates(e.KeyCode) = False

        If e.KeyCode = Keys.End Then
            MessageBox.Show("You did something horrible to my input loop, you monster")
            End
        End If


    End Sub

End Class
