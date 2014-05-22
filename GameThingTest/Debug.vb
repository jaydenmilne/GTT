Public Class Debug

    Private Sub Debug_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(-Me.Width, 0)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label3.Text = CStr(Input.MouseStates(0))
        Label4.Text = CStr(Input.MouseStates(Input.Mouse.Right))




    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim Output As String = CStr(MainForm.Game.EntityManager.NumOfEntities + MainForm.Game.EntityManager.AvailibleSpots.Count) & " Entities"

        For i As Integer = 0 To MainForm.Game.EntityManager.NumOfEntities + MainForm.Game.EntityManager.AvailibleSpots.Count

            If Not IsNothing(MainForm.Game.EntityManager.WadOEntities(i)) Then

                Output = Output & vbNewLine & MainForm.Game.EntityManager.WadOEntities(i).ToString

            End If


        Next

        TextBox1.Text = Output
    End Sub
End Class