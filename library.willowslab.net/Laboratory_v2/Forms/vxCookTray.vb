Public Class vxCookTray
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim Mini As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Recipe1.Width = Math.Ceiling(23 * 5.5)
        Recipe2.Width = Math.Ceiling(Recipe1.Width + 23 * 3.5)
        Me.Height = 42
    End Sub
    Private Sub MoveReady(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown, Recipe1.MouseDown, Recipe2.MouseDown, Recipe1.MouseDown, Recipe3.MouseDown, Label1.MouseDown, Label2.MouseDown
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub
    Private Sub MoveStart(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove, Recipe2.MouseMove, Recipe1.MouseMove, Recipe3.MouseMove, Label1.MouseMove, Label2.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub
    Private Sub MoveEnd(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp, Recipe2.MouseUp, Recipe1.MouseUp, Recipe3.MouseUp, Label1.MouseUp, Label2.MouseUp
        drag = False
    End Sub

    Private Sub MiniMize(sender As Object, e As EventArgs) Handles Recipe1.DoubleClick, Recipe2.DoubleClick, Recipe3.DoubleClick
        If Mini Then
            Me.Height = 6
            While Me.Height < 42
                Me.Height += 1
                System.Threading.Thread.Sleep(2)
            End While
            Recipe1.Height = 6
            Recipe2.Height = 6
            Recipe3.Height = 6
            Mini = Not Mini
        Else
            Me.Height = 42
            While Me.Height > 6
                Me.Height -= 1
                System.Threading.Thread.Sleep(2)
            End While
            Recipe1.Height = 6
            Recipe2.Height = 6
            Recipe3.Height = 6
            Mini = Not Mini
        End If
    End Sub

    Private Sub arrowKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        'MsgBox(e.KeyCode)
        If e.KeyCode = 37 Then
            Me.Left -= 1
        ElseIf e.KeyCode = 38 Then
            Me.Top -= 1
        ElseIf e.KeyCode = 39 Then
            Me.Left += 1
        ElseIf e.KeyCode = 40 Then
            Me.Top += 1
        End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Close()
    End Sub
End Class