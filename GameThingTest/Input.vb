Public Class Input
    ' Pretty amazing class, amiright?
    Public Shared KeyStates(256) As Boolean
    Public Shared MouseStates() As Boolean = {False, False, False, False}

    Public Enum Mouse
        Left = 0
        Right = 1
        Middle = 2
        FILE_NOT_FOUND = 3
    End Enum

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

    Shared Sub MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            MouseStates(Mouse.Left) = True
        ElseIf e.Button = MouseButtons.Right Then
            MouseStates(Mouse.Right) = True
        ElseIf e.Button = MouseButtons.Middle Then
            MouseStates(Mouse.Middle) = True
        Else
            MouseStates(3) = True
        End If
    End Sub

    Shared Sub MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            MouseStates(Mouse.Left) = False
        ElseIf e.Button = MouseButtons.Right Then
            MouseStates(Mouse.Right) = False
        ElseIf e.Button = MouseButtons.Middle Then
            MouseStates(Mouse.Middle) = False
        Else
            MouseStates(3) = False
        End If

    End Sub
    Public Shared Function DoNothing() As Boolean
        Return True
    End Function
End Class
