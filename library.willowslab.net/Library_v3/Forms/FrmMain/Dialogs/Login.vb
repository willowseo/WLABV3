Imports System.Windows.Forms
Public Class Login
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = FrmMain.BackColor
        Me.frmTitle.Text = "버들랩 로그인"
        Me.titlePanel.BackColor = FrmMain.titlePanel.BackColor
        Me.Panel1.BackColor = Func.rgb(240, 240, 240)
        FrmMain.dragable = False
        FrmMain.Enabled = False

        Me.Top = FrmMain.Top + (FrmMain.Height / 2) - (Me.Height / 2)
        Me.Left = FrmMain.Left + (FrmMain.Width / 2) - (Me.Width / 2)
    End Sub
    Private Sub closebtn_enter(sender As Object, e As EventArgs) Handles closebtn.MouseEnter
        closebtn.BackColor = Func.rgb(224, 67, 67)
    End Sub
    Private Sub closebtn_leave(sender As Object, e As EventArgs) Handles closebtn.MouseLeave
        closebtn.BackColor = Func.rgb(199, 80, 80)
    End Sub
    Private Sub closebtn_Click(sender As Object, e As EventArgs) Handles closebtn.Click
        FrmMain.Enabled = True
        FrmMain.dragable = True

        Me.Close()
    End Sub

    Public passwordTmp As String = ""
    Private Sub RichTextBox1_TextChanged(sender As Object, e As KeyPressEventArgs) Handles RichTextBox1.KeyPress
        'MsgBox(Asc(e.KeyChar))
        Dim length As Integer = RichTextBox1.Text.Length
        passwordTmp = passwordTmp.Substring(0, RichTextBox1.Text.Length)
        If Asc(e.KeyChar) = 8 And passwordTmp.Length > 0 Then
            passwordTmp = passwordTmp.Substring(0, passwordTmp.Length - 1)
        ElseIf Asc(e.KeyChar) = 13 Then
            e.Handled = False
        Else
            If Asc(e.KeyChar) > 32 And Asc(e.KeyChar) <= 126 Then
                passwordTmp &= e.KeyChar
                e.KeyChar = "*"
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'MsgBox(passwordTmp)
        Setting.ID = RichTextBox2.Text
        Setting.PW = MD5Crypto.getMd5Hash(MD5Crypto.getMd5Hash(passwordTmp))

        If Setting.procLogin() Then
            FrmMain.Enabled = True
            FrmMain.dragable = True
            FrmMain.listReloadThread()
            Dim tmp As New notificationwindow("로그인 되었습니다.", 3)
            Me.Close()
        Else
            MsgBox("계정 또는 비밀번호가 올바르지 않습니다." & vbNewLine & Setting.ID & " " & Setting.PW, MsgBoxStyle.Critical Or vbMsgBoxSetForeground, "서버로부터의 메세지")
            Setting.ID = ""
            Setting.PW = ""
        End If
    End Sub
End Class