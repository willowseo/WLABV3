Imports System.Threading
Imports System.Net
Imports System.IO
Imports 버들라이브러리.Cryptography
Public Class NewUser

    Private Sub NewAccount_MD(sender As Object, e As EventArgs) Handles NewAccount.MouseDown
        Me.NewAccount.Image = My.Resources.Resources.newuser_down
    End Sub
    Private Sub NewAccount_MU(sender As Object, e As EventArgs) Handles NewAccount.MouseUp
        Me.NewAccount.Image = My.Resources.Resources.newuser_normal1
    End Sub

    Private Sub DataTransfer_MD(sender As Object, e As EventArgs) Handles dataTransFer.MouseDown
        Me.dataTransFer.Image = My.Resources.Resources.datatransfer_down
    End Sub
    Private Sub DataTransfer_MU(sender As Object, e As EventArgs) Handles dataTransFer.MouseUp
        Me.dataTransFer.Image = My.Resources.Resources.datatransfer_normal
    End Sub
    Private Sub NewUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Directory.CreateDirectory(WLAB.ROOT & "\userdata\")
        Me.Size = New Size(402, 367)
        tmp1.BackColor = Color.Black
        tmp2.BackColor = Color.Black
        tmp1.Size = New Size(1, 335)
        tmp2.Size = New Size(1, 335)
        tmp1.Top = 30
        tmp2.Top = 30
        tmp1.Left = 0
        tmp2.Left = 401

        Try
            Dim netw As New WebClient
            netw.Encoding = System.Text.Encoding.UTF8
            Dim swid As String = netw.DownloadString("http://library.willowslab.net/userdat.php")
        Catch ex As Exception
            MsgBox("네트워크에 연결할 수 없습니다. 잠시 뒤 다시 실행해주세요.")
            End
        End Try
    End Sub
    Private Sub relocate() Handles MyBase.Resize
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Bottom / 2) - (Me.Height / 2) + Screen.PrimaryScreen.WorkingArea.Top
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Right / 2) - (Me.Width / 2) + Screen.PrimaryScreen.WorkingArea.Left
    End Sub

    Private mode As String = "new"
    Private arg As String = ""
    Private swid As String = ""
    Private password As String = ""
    Private Sub accountSubmit()
        Thread.Sleep(1000)
        Try
            Dim netw As New WebClient
            netw.Encoding = System.Text.Encoding.UTF8
            Dim swid As String = netw.DownloadString("http://library.willowslab.net/userdat.php?mode=" & mode & "&pw=" & MD5Crypto.getMd5Hash(Me.password) & arg)
            Try
                If swid = "SUCCESS" Then
                    'MsgBox("성공")
                    'File.WriteAllBytes(WLAB.CONF & "userinfo.usd", System.Text.Encoding.UTF8.GetBytes(Me.swid))
                    successed()
                Else
                    Throw New Exception(swid)
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Catch ex As Exception
            MsgBox("네트워크에 연결할 수 없습니다. 잠시 뒤 다시 실행해주세요.")
            End
        End Try
    End Sub

    Private Delegate Sub Callback()

    Private Sub successed()
        If Me.InvokeRequired Then
            Dim work As New Callback(AddressOf successed)
            Me.Invoke(work, New Object() {})
        Else
            FrmMain.Show()
            Me.Close()
        End If
    End Sub

    Private Sub NewAccount_Click(sender As Object, e As EventArgs) Handles NewAccount.Click
        While (Me.newACC.Left > -400)
            Me.newACC.Left -= 4
            Me.Refresh()
        End While
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        While (Me.newACC.Left < 0)
            Me.newACC.Left += 4
            Me.Refresh()
        End While
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If pwbox1.Text <> vbNullString And pwbox2.Text <> vbNullString And pwbox1.Text = pwbox2.Text Then

            While (Me.newACC.Left > -800)
                Me.newACC.Left -= 4
                Me.Refresh()
            End While
            Try
                Dim netw As New WebClient
                netw.Encoding = System.Text.Encoding.UTF8
                Dim swid As String = netw.DownloadString("http://library.willowslab.net/userdat.php?mode=new&pw=" & getSHA512(Me.pwbox1.Text) & arg)
                Try
                    If InStr(swid, "*ERROR :") Then
                        Throw New Exception(swid)
                    Else
                        Me.SWID_01.Text = swid
                        File.WriteAllBytes(WLAB.ROOT & "\userdata\userdata.usd", System.Text.Encoding.UTF8.GetBytes(Me.SWID_01.Text.Replace("-", "") & getSHA512(Me.pwbox1.Text)))
                        Me.Button1.Hide()
                        Me.Button2.Hide()
                        Dim trd As New Thread(New ThreadStart(AddressOf gotoSWIDCheckForm))
                        trd.IsBackground = True
                        trd.Start()
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                    Dim trd As New Thread(New ThreadStart(AddressOf gotoNewAccountForm))
                    trd.IsBackground = True
                    trd.Start()
                End Try
            Catch ex As Exception
                Thread.Sleep(1500)
                MsgBox("네트워크에 연결할 수 없습니다. 잠시 뒤 다시 실행해주세요.", MsgBoxStyle.Critical)
                End
            End Try
        Else
            MsgBox("비밀번호 항목이 비어있거나 두 항목이 서로 일치하지 않은 경우입니다. 다시 확인해 주시기 바랍니다.", MsgBoxStyle.Exclamation, "오류")
        End If
    End Sub
    Private Sub gotoNewAccountForm()
        If Me.InvokeRequired Then
            Thread.Sleep(2000)
            Dim work As New Callback(AddressOf gotoNewAccountForm)
            Me.Invoke(work, New Object() {})
        Else
            Me.Button1.Show()
            Me.Button2.Show()
            While (Me.newACC.Left < -400)
                Me.newACC.Left += 4
                Me.Refresh()
            End While
        End If
    End Sub
    Private Sub gotoSWIDCheckForm()
        If Me.InvokeRequired Then
            Thread.Sleep(2000)
            Dim work As New Callback(AddressOf gotoSWIDCheckForm)
            Me.Invoke(work, New Object() {})
        Else
            While (Me.newACC.Left > -1196)
                Me.newACC.Left -= 4
                Me.Refresh()
            End While
        End If
    End Sub

    Private Sub dataTransFer_Click(sender As Object, e As EventArgs) Handles dataTransFer.Click
        Me.TransF.Top = 31
        While (Me.TransF.Left > -400)
            Me.TransF.Left -= 4
            Me.Refresh()
        End While
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        While (Me.TransF.Left < 0)
            Me.TransF.Left += 4
            Me.Refresh()
        End While
        Me.TransF.Top = 366
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("정말 종료하시겠습니까?", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkCancel, "경고") = MsgBoxResult.Ok Then
            End
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("이 과정을 건너뛰면 설정 동기화 등의 기능을 이용할 수 없게 됩니다. 그래도 계속하시겠습니까?", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkCancel, "경고") = MsgBoxResult.Ok Then
            successed()
        End If
    End Sub

    Private msg As String = ""

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If MsgBox("신규 익명계정이 생성되어 데이터가 이전된 경우 대상이 되는 SWID는 영구히 삭제됩니다. 계속 진행하시겠습니까?", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkCancel, "경고") = MsgBoxResult.Ok Then

            If Me.pswid.Text <> vbNullString And Me.ppw.Text <> vbNullString And Me.wid.Text <> vbNullString And Me.wpw.Text <> vbNullString And _
                Me.npwa.Text <> vbNullString And Me.npwp.Text <> vbNullString And Me.npwa.Text = Me.npwp.Text Then
                Me.msg = ""
                While (Me.TransF.Left > -800)
                    Me.TransF.Left -= 4
                    Me.Refresh()
                End While
                Try
                    Dim netw As New WebClient
                    netw.Encoding = System.Text.Encoding.UTF8
                    Dim swid As String = netw.DownloadString("http://library.willowslab.net/userdat.php?mode=transfer&privswid=" & _
                                                             Me.pswid.Text & "&privswidpw=" & MD5Crypto.getMd5Hash(Me.ppw.Text) & "&wlabid=" & _
                                                             Me.wid.Text & "&wlabpw=" & MD5Crypto.getMd5Hash(Me.wpw.Text) & "&newpw=" & MD5Crypto.getMd5Hash(Me.npwa.Text))
                    Try
                        If InStr(swid, "*ERROR :") Then
                            Throw New Exception(swid)
                        Else
                            If swid.Length > 19 Then
                                Me.SWID_02.Text = swid.Substring(0, 19)
                                File.WriteAllBytes(WLAB.ROOT & "\userdata\userdata.usd", System.Text.Encoding.UTF8.GetBytes(Me.SWID_02.Text.Replace("-", "") & MD5Crypto.getMd5Hash(Me.npwa.Text)))
                                Me.msg = swid.Substring(19)
                            Else
                                Me.SWID_02.Text = swid.Substring(0, 19)
                                Me.msg = swid.Substring(19)
                            End If
                            Me.Button1.Hide()
                            Me.Button2.Hide()
                            Dim trd As New Thread(New ThreadStart(AddressOf gotoSWIDCheckForm2))
                            trd.IsBackground = True
                            trd.Start()
                        End If
                    Catch ex As Exception
                        Console.WriteLine(ex.GetBaseException.ToString)
                        MsgBox(ex.Message, MsgBoxStyle.Critical)
                        Dim trd As New Thread(New ThreadStart(AddressOf gotoNewAccountForm2))
                        trd.IsBackground = True
                        trd.Start()
                    End Try
                Catch ex As Exception
                    Thread.Sleep(1500)
                    MsgBox("네트워크에 연결할 수 없습니다. 잠시 뒤 다시 실행해주세요.", MsgBoxStyle.Critical)
                    End
                End Try
            Else
                MsgBox("비밀번호 항목이 비어있거나 두 항목이 서로 일치하지 않은 경우입니다. 다시 확인해 주시기 바랍니다.", MsgBoxStyle.Exclamation, "오류")
            End If
        End If
    End Sub
    Private Sub gotoNewAccountForm2()
        If Me.InvokeRequired Then
            Thread.Sleep(2000)
            Dim work As New Callback(AddressOf gotoNewAccountForm2)
            Me.Invoke(work, New Object() {})
        Else
            Me.Button1.Show()
            Me.Button2.Show()
            While (Me.TransF.Left < -400)
                Me.TransF.Left += 4
                Me.Refresh()
            End While
        End If
    End Sub
    Private Sub gotoSWIDCheckForm2()
        If Me.InvokeRequired Then
            Thread.Sleep(2000)
            Dim work As New Callback(AddressOf gotoSWIDCheckForm2)
            Me.Invoke(work, New Object() {})
        Else
            While (Me.TransF.Left > -1196)
                Me.TransF.Left -= 4
                Me.Refresh()
            End While
            If Me.msg <> vbNullString Then
                MsgBox(msg, MsgBoxStyle.Information, "알림")
                Me.msg = ""
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        successed()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        successed()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click, Button10.Click
        Process.Start(WLAB.ROOT & "\userdata")
    End Sub
End Class