Imports System.Net
Imports System.Web
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Xml
Imports System.Collections
Partial Class FrmMain
    Private Sub closebtn_enter(sender As Object, e As EventArgs) Handles closebtn.MouseEnter
        closebtn.BackColor = Func.rgb(224, 67, 67)
    End Sub
    Private Sub closebtn_leave(sender As Object, e As EventArgs) Handles closebtn.MouseLeave
        closebtn.BackColor = Func.rgb(199, 80, 80)
    End Sub

    Private Sub closebtn_Click(sender As Object, e As EventArgs) Handles closebtn.Click
        If Setting.allowTray Then
            Me.Hide()
        Else
            Me.TrayIcon.Visible = False
            End
        End If
    End Sub
    Private Sub DragStart() Handles titlePanel.MouseDown, frmTitle.MouseDown
        If dragable Then
            drag = True
            mousex = Windows.Forms.Cursor.Position.X - Me.Left
            mousey = Windows.Forms.Cursor.Position.Y - Me.Top
        End If
    End Sub

    Private Sub Dragging() Handles titlePanel.MouseMove, frmTitle.MouseMove
        If drag And dragable Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub DragStop() Handles titlePanel.MouseUp, frmTitle.MouseUp
        drag = False
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        readList(1, Me.SVID.Text, Me.SVLN.Text)
        Me.SVID.Text = ""
        Me.SVLN.Text = ""
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            readList(2, Me.ServerList.Items(Me.ServerList.FocusedItem.Index).SubItems(0).Text)
        Catch ex As Exception
            InfoMessage("리스트를 하나 이상 선택한 뒤 다시 시도해 주세요.")
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If Func.setMabiPath() Then
            Me.MabiPath.Text = Setting.Mabi
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Dim tmp = Me.ServerList.Items(Me.ServerList.FocusedItem.Index)
            Me.SelectedServer.Text = tmp.SubItems(0).Text
            Func.ServerSetChange(Me.SelectedServer.Text, tmp.SubItems(1).Text)
            My.Computer.FileSystem.WriteAllText(wlab.root & "data\serversetting.dat", tmp.SubItems(0).Text, False)
        Catch ex As Exception
            'MsgBox(ex.GetBaseException.ToString)
        End Try
        listReloadThread()
    End Sub

    Private Sub listschanged(sender As Object, e As EventArgs) Handles plist1.SelectedIndexChanged, plist2.SelectedIndexChanged, plist3.SelectedIndexChanged, plist4.SelectedIndexChanged
        If sender.Items(sender.FocusedItem.Index).SubItems(4).Text <> "" Then
            Dim tmpURL As New System.Uri(sender.Items(sender.FocusedItem.Index).SubItems(4).Text)
            infoviewer.Url = tmpURL
        End If
    End Sub

    Private Sub MinMaxStateChange() Handles titlePanel.DoubleClick, frmTitle.DoubleClick
        Dim windowSize As Rectangle = Screen.PrimaryScreen.WorkingArea
        If winState = 0 Then
            winState = 1
            Me.winX = Me.Top
            Me.winY = Me.Left
            Me.SetDesktopLocation(0, 0)
            Me.Size = New Size(windowSize.Width, windowSize.Height)
            Me.dragable = Not Me.dragable
        Else
            winState = 0
            Me.Top = Me.winX
            Me.Left = Me.winY
            Me.Size = New Size(800, 600)
            Me.dragable = Not Me.dragable
        End If
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles red_trackbar.Scroll, green_trackbar.Scroll, blue_trackbar.Scroll
        If sender.GetHashCode = red_trackbar.GetHashCode Then
            red_value.Text = red_trackbar.Value
        ElseIf sender.GetHashCode = green_trackbar.GetHashCode Then
            green_value.Text = green_trackbar.Value
        ElseIf sender.GetHashCode = blue_trackbar.GetHashCode Then
            blue_value.Text = blue_trackbar.Value
        End If
    End Sub

    Private Sub numberonly(sender As Object, e As KeyPressEventArgs) Handles red_value.KeyPress, green_value.KeyPress, blue_value.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub valueChanged(sender As Object, e As EventArgs) Handles red_value.TextChanged, green_value.TextChanged, blue_value.TextChanged
        If Not Val(sender.text) < 256 Then
            sender.text = "255"
            If sender.GetHashCode = red_value.GetHashCode Then
                red_trackbar.Value = 255
            ElseIf sender.GetHashCode = green_trackbar.GetHashCode Then
                green_trackbar.Value = 255
            ElseIf sender.GetHashCode = blue_trackbar.GetHashCode Then
                blue_trackbar.Value = 255
            End If
        Else
            If sender.GetHashCode = red_value.GetHashCode Then
                red_trackbar.Value = Val(red_value.Text)
            ElseIf sender.GetHashCode = green_value.GetHashCode Then
                green_trackbar.Value = Val(green_value.Text)
            ElseIf sender.GetHashCode = blue_value.GetHashCode Then
                blue_trackbar.Value = Val(blue_value.Text)
            End If
        End If

        Me.titlePanel.BackColor = Func.rgb(red_trackbar.Value, green_trackbar.Value, blue_trackbar.Value)

        Dim backColorVal As Color = Color.FromArgb(red_trackbar.Value, green_trackbar.Value, blue_trackbar.Value)

        Dim brightness As HSB = ColorHelper.RGBtoHSB(backColorVal)
        If brightness.Brightness > 0.245D Then
            brightness.Brightness = brightness.Brightness - 0.245D
        Else
            brightness.Brightness = 0.1225D
        End If

        Me.BackColor = ColorHelper.HSBtoColor(brightness)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        listReloadThread()
    End Sub

    Private Sub autoUpdate_CheckedChanged(sender As Object, e As EventArgs) Handles autoUpdate.MouseClick
        Setting.autoUpdated = Me.autoUpdate.Checked
        Setting.saveSetting()
    End Sub

    Private Sub allowTray_CheckedChanged(sender As Object, e As EventArgs) Handles allowTray.MouseClick
        Setting.allowTray = Not Me.allowTray.Checked
        Setting.saveSetting()
    End Sub

    Private Sub viewNotibar_CheckedChanged(sender As Object, e As EventArgs) Handles viewNotibar.MouseClick
        Setting.viewNotibar = Me.viewNotibar.Checked
        Setting.saveSetting()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        login.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        red_value.Text = Setting.windowColor(0)
        green_value.Text = Setting.windowColor(1)
        blue_value.Text = Setting.windowColor(2)

        red_trackbar.Value = Setting.windowColor(0)
        green_trackbar.Value = Setting.windowColor(1)
        blue_trackbar.Value = Setting.windowColor(2)

        Me.titlePanel.BackColor = Func.rgb(red_trackbar.Value, green_trackbar.Value, blue_trackbar.Value)
        Dim backColorVal As Color = Color.FromArgb(red_trackbar.Value, green_trackbar.Value, blue_trackbar.Value)
        Dim brightness As HSB = ColorHelper.RGBtoHSB(backColorVal)

        If brightness.Brightness > 0.245D Then
            brightness.Brightness = brightness.Brightness - 0.245D
        Else
            brightness.Brightness = 0.1225D
        End If

        Me.BackColor = ColorHelper.HSBtoColor(brightness)
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If MsgBox("현재 색상 설정을 저장합니다. 다시 확인합니다. 이 색상으로 변경하는 것이 맞습니까?", MsgBoxStyle.OkCancel Or MsgBoxStyle.Information Or vbMsgBoxSetForeground, "주의") = MsgBoxResult.Ok Then
            Setting.windowColor = {red_trackbar.Value, green_trackbar.Value, blue_trackbar.Value}
            Setting.saveSetting()
        End If
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        initLocalPackage()
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        For Each item As ListViewItem In PackageFileList.Items
            item.Checked = Not item.Checked
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Mabinogi.Start()
    End Sub

    Private Sub Button21_Click_1(sender As Object, e As EventArgs) Handles Button21.Click
        If Func.setMabiTestPath() Then
            Me.mabiTestPath.Text = Setting.MabiTest
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Process.GetProcessesByName("Client").Count > 0 Then
        Else
            Dim tmptrd As New Thread(New ThreadStart(AddressOf InstallFile))
            tmptrd.IsBackground = True
            tmptrd.Start()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        TextBox1.Text = ""
        If e.KeyCode = Keys.Escape Then
            keyTracer.Clear()
        Else
            keyTracer.Add(e.KeyCode)
            Dim kl As List(Of Integer) = keyTracer
            Try
                If kl(0) = Keys.Up And _
                    kl(1) = Keys.Up And _
                    kl(2) = Keys.Down And _
                    kl(3) = Keys.Down And _
                    kl(4) = Keys.Left And _
                    kl(5) = Keys.Right And _
                    kl(6) = Keys.Left And _
                    kl(7) = Keys.Right And _
                    kl(8) = Keys.B And _
                    kl(9) = Keys.A Then
                    'experiment_tester.Show()
                    My.Computer.Audio.Play(My.Resources.Resources.Card_Input, AudioPlayMode.Background)
                    keyTracer.Clear()
                End If
            Catch ex As Exception
            End Try
            Try
                If kl(0) = Keys.Left And _
                    kl(1) = Keys.Left And _
                    kl(2) = Keys.Down And _
                    kl(3) = Keys.Up Then
                    WillowsNP.Show()
                    My.Computer.Audio.Play(My.Resources.Resources.Card_Input, AudioPlayMode.Background)
                    keyTracer.Clear()
                End If
            Catch ex As Exception

            End Try
            Try
                If kl(0) = Keys.R And _
                    kl(1) = Keys.Y And _
                    kl(2) = Keys.A And _
                    kl(3) = Keys.L Then
                    TestAndEasterEgg.Show()
                    My.Computer.Audio.Play(My.Resources.Resources.Card_Input, AudioPlayMode.Background)
                    keyTracer.Clear()
                End If
            Catch ex As Exception

            End Try
            If keyTracer.Count = 10 Then
                keyTracer.Clear()
            End If
        End If
    End Sub

    Private Sub Button12_Click_1(sender As Object, e As EventArgs) Handles Button12.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'MsgBox(ColorDialog1.Color.R & vbCrLf & ColorDialog1.Color.G & vbCrLf & ColorDialog1.Color.B)
            Me.red_value.Text = ColorDialog1.Color.R
            Me.green_value.Text = ColorDialog1.Color.G
            Me.blue_value.Text = ColorDialog1.Color.B

            Me.titlePanel.BackColor = Func.rgb(ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)

            Dim backColorVal As Color = Color.FromArgb(ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)

            Dim brightness As HSB = ColorHelper.RGBtoHSB(backColorVal)
            If brightness.Brightness > 0.245D Then
                brightness.Brightness = brightness.Brightness - 0.245D
            Else
                brightness.Brightness = 0.1225D
            End If

            Me.BackColor = ColorHelper.HSBtoColor(brightness)
        End If
    End Sub

    Private Sub isTestserver_CheckedChanged(sender As Object, e As EventArgs) Handles isTestserver.CheckedChanged
        If Me.isTestserver.Checked Then
            Me.mabiDir = Setting.Mabi
            Setting.Mabi = Setting.MabiTest
            Setting.Data = Setting.MabiTest & "\data\"
        Else
            Me.mabiDir = Setting.Mabi
            Setting.Mabi = Setting.Mabi
            Setting.Data = Setting.Mabi & "\data\"
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If MsgBox("요리도구 등으로 설치한 요리재료와 다른 꾸러미 도구나 직접 받은 패키지도 함께 제거됩니다. (모든 언팩과 패키지가 제거됩니다...)" & vbCrLf & "계속 진행하시겠습니까?", MsgBoxStyle.OkCancel Or MsgBoxStyle.Exclamation Or vbMsgBoxSetForeground) = MsgBoxResult.Ok Then
            Dim trd As New Thread(New ThreadStart(AddressOf tmptrd))
            trd.IsBackground = True
            trd.Start()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        'Laboratory.Show()
        vxFrmMain.Show()
    End Sub

    Private Sub WebBrowser2_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser2.DocumentCompleted
        'MsgBox(WebBrowser2.Url.ToString)
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If MsgBox("정말 로그아웃 하시겠습니까? 리스트에서 보이지 않는 패키지는 라이브러리 도구의 패키지 관리 도구로 제거할 수 있습니다.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkCancel Or vbMsgBoxSetForeground, "알림") = MsgBoxResult.Ok Then

            Me.logoutUnset()
            Setting.ID = ""
            Setting.PW = ""
            Setting.autologin = False
            Setting.isDonor = False
            Dim tmp As New notificationwindow("로그아웃 되었습니다.", 3)
        End If
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If MsgBox("삭제한 패키지는 복구가 불가능합니다. 정말 진행하시겠습니까?", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkCancel Or vbMsgBoxSetForeground, "경고") = MsgBoxResult.Ok Then

            Dim p() As Process
            p = Process.GetProcessesByName("Client")
            If p.Count = 0 Then
                For Each item As ListViewItem In PackageFileList.Items
                    If item.Checked Then
                        Dim filepath As String = Setting.Mabi & "\" & item.SubItems(0).Text
                        If File.Exists(filepath) Then
                            File.Delete(filepath)
                        End If
                    End If
                Next
                initLocalPackage()
            Else
                MsgBox("마비노기가 실행중일 때에는 삭제가 불가능합니다.", MsgBoxStyle.Exclamation Or vbMsgBoxSetForeground, "경고")
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim thread As New Thread(New ThreadStart(AddressOf deleteWorkThread))
        thread.Name = "Tteasdfasdf"
        thread.IsBackground = True
        thread.Start()
    End Sub
End Class
