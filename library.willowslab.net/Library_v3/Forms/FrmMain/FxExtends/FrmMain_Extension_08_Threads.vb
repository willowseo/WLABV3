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

    Public Sub listReloadThread()
        updated = False
        Me.trd = New Thread(New ThreadStart(AddressOf Me.readPackList))
        trd.IsBackground = True
        trd.Start()
    End Sub
    Public Sub listReloadThreadPatched()
        updated = True
        Me.trd = New Thread(New ThreadStart(AddressOf Me.readPackList))
        trd.IsBackground = True
        trd.Start()
    End Sub
    Private Sub workingSubProgress(ByRef current As Integer)
        current += 1
        ProgressSubTrd((current / workQueue) * 10000)
        Dim dt As Date
        Dim message As String = ""
        If dt.Month > 5 And dt.Month < 10 Then
            message = "시원한 마실 것 준비해서 마시면서 기다리세요 :)"
        Else
            message = "따뜻한 마실 것 준비해서 마시면서 기다리세요 :)"
        End If
        progressTextChange(message & vbCrLf & "설치를 진행하고 있습니다... " & current & "/" & workQueue)
    End Sub
    Private Sub tmptrd()
        Me.plistEabledChange(False)
        Me.ShowHideProgress(False)
        DataReplacer(False)
        Try
            My.Computer.FileSystem.DeleteDirectory(Setting.Mabi & "\data\", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception
            Console.WriteLine(ex.GetBaseException.ToString)
        End Try

        Try
            My.Computer.FileSystem.DeleteDirectory(Setting.Mabi & "\lab\", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception
            Console.WriteLine(ex.GetBaseException.ToString)
        End Try

        Try
            For Each str As String In Directory.GetFiles(Setting.Mabi & "\package\")
                If InStr(str, "_to_") Or InStr(str, "_full") Then

                Else
                    File.Delete(str)
                End If
            Next
        Catch ex As Exception
            Console.WriteLine(ex.GetBaseException.ToString)
        End Try

        Try
            For Each str As String In Directory.GetDirectories(Setting.Mabi & "\package\")
                Try
                    My.Computer.FileSystem.DeleteDirectory(str, FileIO.DeleteDirectoryOption.DeleteAllContents)
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
            Console.WriteLine(ex.GetBaseException.ToString)
        End Try

        listReloadThread()
        DataReplacer(True)
        Me.plistEabledChange(True)
        Me.ShowHideProgress(True)
        progressTextChange("default")
    End Sub
End Class
