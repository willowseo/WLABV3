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

Public Class FrmMain
    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TrayIcon.MouseDoubleClick
        Me.Show()
    End Sub

    Private Sub 모든마비노기전체화면ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 모든마비노기전체화면ToolStripMenuItem.Click
        MWHD.fullScreen()
    End Sub

    Private Sub 모든마비노기전체화면테두리없음ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 모든마비노기전체화면테두리없음ToolStripMenuItem.Click
        MWHD.fullScreen(False)
    End Sub

    Private Sub 버들패키지도서관종료ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 버들패키지도서관종료ToolStripMenuItem.Click
        Me.TrayIcon.Visible = False
        End
    End Sub

    Private Sub 알림영역열기닫기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 알림영역열기닫기ToolStripMenuItem.Click
        Assistant.Show()
    End Sub
    Private Sub 창관리자열기닫기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 창관리자열기닫기ToolStripMenuItem.Click
        If opened Then
            Assistant.Hide()
        Else
            Assistant.Show()
        End If
        opened = Not opened
    End Sub

    Private Sub wbURL(sender As Object, e As EventArgs) Handles WebBrowser2.Navigated
        Dim tmp As String = WebBrowser2.Url.ToString.ToLower
        If InStr(tmp, "lidcerror") Then
            MsgBox("아이디가 존재하지 않는다는 오류를 받았습니다. 유저 정보를 삭제하고 종료합니다.")
            My.Computer.FileSystem.DeleteDirectory(WLAB.ROOT & "\userdata\", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Application.Restart()
        End If
    End Sub
End Class