Public Class Debug

    Private Sub Debug_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(-Me.Width, 0)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label3.Text = CStr(Input.MouseStates(0))
        Label4.Text = CStr(Input.MouseStates(Input.Mouse.Right))

        Dim TempString As String = "Update: " & CStr(Diagnostics.UpdateTime) & vbNewLine & "Collision: " & CStr(Diagnostics.CollisionTime) & vbNewLine & "Render: " & CStr(Diagnostics.RenderTime) & vbNewLine & "Buffer Fill: " & Diagnostics.BufferRender
        TextBox2.Text = TempString




    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim Output As String = CStr(MainForm.Game.EntityManager.NumOfEntities + MainForm.Game.EntityManager.AvailibleSpots.Count) & " Entities"

        For i As Integer = 0 To MainForm.Game.EntityManager.NumOfEntities + MainForm.Game.EntityManager.AvailibleSpots.Count

            If Not IsNothing(MainForm.Game.EntityManager.WadOEntities(i)) Then

                Output = Output & vbNewLine & MainForm.Game.EntityManager.WadOEntities(i).ToString

            End If


        Next

        TextBox3.Text = String.Join(",", MainForm.Game.EntityManager.DeathRow.ToArray)

        TextBox1.Text = Output

        TextBox5.Text = Diagnostics.Caller


    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Diagnostics.OneToSpyOn = CInt(TextBox4.Text)
    End Sub
End Class