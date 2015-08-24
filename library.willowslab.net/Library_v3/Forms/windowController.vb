Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Net.Sockets

Public Class windowController
    Private Declare Function GetClassName Lib "user32" Alias "GetClassNameA" (hwnd As IntPtr, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef lpString As String, cch As Integer) As Integer

    Public proc As Object
    Public state As Boolean = False
    Public tmp As Process
    Private list As List(Of Object())
    Private Sub windowReLocated() Handles MyBase.LocationChanged
        reLocated()
    End Sub
    Private Sub notiForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(360, 24)
        reLocated()
        MabiList()
        If Me.Size.Height = 24 Then
            state = True
            Me.Label10.Image = My.Resources.small
        Else
            Me.Label10.Image = My.Resources.big
        End If
        Timer1.Enabled = True
    End Sub
    Public Sub MabiList()
        Dim selectIndex As String = ""
        Dim index As Integer = -1
        Dim count As Integer = Process.GetProcessesByName("Client").Count

        Try
            If ListView1.FocusedItem.Index > -1 Then
                selectIndex = ListView1.Items(ListView1.FocusedItem.Index).SubItems(1).Text
                index = ListView1.FocusedItem.Index
            End If
        Catch ex As Exception
        End Try

        Dim i As Integer = 0
        ListView1.Items.Clear()
        Dim [classname] As String = Strings.Space(256)
        For Each mabinogi As Process In Process.GetProcessesByName("Client")
            GetClassName(mabinogi.MainWindowHandle, [classname], 256)
            If [classname].IndexOf("Mabinogi") > -1 Then
                ListView1.Items.Add(New ListViewItem({mabinogi.MainWindowTitle, mabinogi.PeakWorkingSet64, mabinogi.Id}))
                If mabinogi.MainWindowTitle = selectIndex Then
                    index = i
                End If
                i += 1
            End If
        Next

        Try
            If index > -1 Then
                ListView1.FocusedItem = ListView1.Items(index)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub reLocated()
        Me.SetDesktopLocation(Screen.PrimaryScreen.WorkingArea.Width - Me.Size.Width, Screen.PrimaryScreen.WorkingArea.Height - Me.Size.Height)
    End Sub
    Public Sub mabiWindowControll()
        proc = Process.GetProcessById(Val(ListView1.Items(ListView1.FocusedItem.Index).SubItems(2).Text)).MainWindowHandle
        tmp = Process.GetProcessById(Val(ListView1.Items(ListView1.FocusedItem.Index).SubItems(2).Text))
        MWHD.getWindow()
    End Sub
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        mabiWindowControll()
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        MabiList()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        MWHD.setWindow()
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        If Me.state Then
            Me.Size = New Size(Me.Size.Width, 398)
            Me.Label10.Image = My.Resources.big
        Else
            Me.Size = New Size(Me.Size.Width, 24)
            Me.Label10.Image = My.Resources.small
        End If
        reLocated()
        Me.state = Not Me.state
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Me.Close()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        If Me.TopMost Then
            Label12.Image = My.Resources.not_pined
        Else
            Label12.Image = My.Resources.pined
        End If
        Me.TopMost = Not Me.TopMost
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        MWHD.fullScreen()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        MWHD.fullScreen(False)
    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click
        MWHD.checkpattern()
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        MWHD.checkpattern(False)
    End Sub
    Private Sub mouse_over_tools(sender As Object, e As EventArgs) Handles Label1.MouseHover, Label10.MouseHover, Label11.MouseHover, Label12.MouseHover, Label13.MouseHover, Label14.MouseHover, Label15.MouseHover, Label16.MouseHover
        Select Case sender.GetHashCode
            Case Label1.GetHashCode
                If Setting.isTestMabi Then
                    tooltipText("마비노기 실행(테스트 서버)")
                Else
                    tooltipText("마비노기 실행")
                End If
            Case Label10.GetHashCode
                If Me.state Then
                    tooltipText("창 관리도구 확장")
                Else
                    tooltipText("창 관리도구 최소화")
                End If
            Case Label11.GetHashCode
                tooltipText("닫기")
            Case Label12.GetHashCode
                If Me.TopMost Then
                    tooltipText("항상 위 해제")
                Else
                    tooltipText("항상 위")
                End If
            Case Label13.GetHashCode
                tooltipText("전체화면(무테)")
            Case Label14.GetHashCode
                tooltipText("바둑판식 정렬")
            Case Label15.GetHashCode
                tooltipText("바둑판식 정렬(무테)")
            Case Label16.GetHashCode
                tooltipText("전체화면")
        End Select
    End Sub

    Private Sub mouse_out_tools(sender As Object, e As EventArgs) Handles Label1.MouseLeave, Label10.MouseLeave, Label11.MouseLeave, Label12.MouseLeave, Label13.MouseLeave, Label14.MouseLeave, Label15.MouseLeave, Label16.MouseLeave
        tooltipText("마비노기 도구")
    End Sub

    Private Sub tooltipText(Optional ByVal text As String = "")
        Me.helpText.Text = text
    End Sub
    <DllImport("kernel32")> _
    Public Shared Sub CloseHandle(ByVal hObject As IntPtr)
    End Sub

    Public cnt As Integer = 0
    Public tic As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim loc_cnt As Integer = Process.GetProcessesByName("Client").Count
        tic += 1
        If Not loc_cnt = cnt Or tic = 5 Then
            tic = 0
            MabiList()
            cnt = loc_cnt
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Mabinogi.Start()
    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click
        Try
            MWHD.forceFocused()
            tmp.CloseMainWindow()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        MWHD.forceMinimized()
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        MWHD.forceFocused()
        MWHD.getWindow()
    End Sub

    Private Sub helpText_Click(sender As Object, e As EventArgs) Handles helpText.Click
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.MouseClick
        Setting.isTestMabi = Me.CheckBox2.Checked
        Setting.saveSetting()
    End Sub
End Class