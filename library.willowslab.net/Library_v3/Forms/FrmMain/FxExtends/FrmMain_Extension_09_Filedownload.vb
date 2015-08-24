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
    Private Sub fdf(ByVal i As Long) Handles fdownloader.AmountDownloadedChanged
        Me.ProgressMainTrd(((triedSize + i) / totalSize) * 10000)
    End Sub
    Public Function FormatFileSize(ByVal Size As Long) As String
        Try
            Dim KB As Integer = 1024
            Dim MB As Integer = KB * KB
            ' Return size of file in kilobytes.
            If Size < KB Then
                Return (Size.ToString("D") & " bytes")
            Else
                Select Case Size / KB
                    'Case Is < 1024
                    'Return (Size / KB).ToString("0.0") & " KB"
                    Case Is < 1048576
                        Return (Size / MB).ToString("0.0") & " MB"
                    Case Is < 1073741824
                        Return (Size / MB / KB).ToString("0.0") & " GB"
                End Select
            End If
            Return Size.ToString
        Catch ex As Exception
            Return Size.ToString
        End Try
    End Function
    Private Sub _Downloader_FileDownloadSizeObtained(ByVal iFileSize As Long) Handles _Downloader.FileDownloadSizeObtained
        'currentfile = iFileSize
    End Sub

    Private Sub _Downloader_FileDownloadComplete() Handles _Downloader.FileDownloadComplete
        Try
            'ProgressMainTrd(100)
            progressTextChange(dlfilename & " 파일을 복사하는 중...")
        Catch ex As Exception
            addLog(ex.GetBaseException.ToString)
        End Try
    End Sub
    Private Sub _Downloader_FileDownloadFailed(ByVal ex As System.Exception) Handles _Downloader.FileDownloadFailed
        'current += currentfile
        addLog(ex.Message)
    End Sub

    Private Sub _Downloader_AmountDownloadedChanged(ByVal iNewProgress As Long) Handles _Downloader.AmountDownloadedChanged
        Try
            'Me.ProgressBar1.Value = CType(Math.Floor((current + iNewProgress) / total) * 100, Integer)
            'Me.ProgressBar2.Value = Math.Floor(iNewProgress / currentfile) * 100
            'ProgressMainTrd(((current + iNewProgress) / total) * 100)
            ProgressSubTrd(((current + iNewProgress) / total) * 10000)
            progressTextChange("전체 " & (((current + iNewProgress) / total)).ToString("0.0") & "% 완료" & vbCrLf & _
                               "현재 " & dlfilename & " 파일 다운로드 중 [ " & FormatFileSize(iNewProgress) & " / " & FormatFileSize(currentfile) & " ] ")
            Application.DoEvents()
        Catch ex As Exception
            addLog(ex.GetBaseException.ToString)
            Application.DoEvents()
        End Try
    End Sub
End Class
