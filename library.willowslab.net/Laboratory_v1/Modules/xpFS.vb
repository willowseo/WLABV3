Imports System.Text.RegularExpressions
Imports System.Net

Module xpFS

    'Private Delegate Function EnumCallBackDelegate(ByVal hwnd As Integer, ByVal lParam As Integer) As Integer
    'Private Declare Function EnumWindows Lib "user32" (ByVal lpEnumFunc As EnumCallBackDelegate, ByVal lParam As Integer) As Integer
    'Public Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer

    'Private Delegate Function EnumCallBackDelegate(ByVal hwnd As Integer, ByVal lParam As Integer) As Integer
    'Private Declare Function EnumWindows Lib "user32" (ByVal lpEnumFunc As EnumCallBackDelegate, ByVal lParam As Integer) As Integer
    'Public Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    'Public Declare Function GetClassName Lib "user32" Alias "GetClassNameA" (ByVal hwnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer

    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer

    'Public Function EnumWindowProc(ByVal app_hWnd As Integer, ByVal lParam As Integer) As Integer
    '    Dim sTitle As String = Space(256)
    '    Dim cTitle As String = Space(256)

    '    Call GetWindowText(app_hWnd, sTitle, 256)  '   .
    '    Call GetClassName(app_hWnd, cTitle, 256)  '   .

    '    If InStr(sTitle, "마비노기") > 0 Then
    '        MsgBox(sTitle)
    '        MsgBox(cTitle)
    '    End If

    '    Return 1
    'End Function

    '다운로드 함수
    Public Function File_Download(ByVal Server As String, ByVal Client As String, Optional ByVal View As Boolean = True, Optional ByVal ReConnectValue As Integer = 2) As Boolean

        Dim reconnect As Integer = 0
        Dim TimeT As Long

        '재연결횟수
        TimeT = 0

        While True
            If Date.Now.Ticks - TimeT >= 5000000 Then

                If reconnect > ReConnectValue Then

                    Exit While

                End If

                Try

                    xpFS.File_Delete(Client)

                    My.Computer.Network.DownloadFile(Server, Client, "", "", View, 100 * 1000, True)

                    If xpFS.File_Exists(Client) = True Then

                        '404에러에 대한 HTML 파일이 다운로드 받아질 가능성에 대한 검사
                        If InStr(xpFS.Read_File(Client), "<HTML") > 0 Or InStr(xpFS.Read_File(Client), "<html") > 0 Then

                            Return False

                        End If

                        Return True

                    End If

                Catch ex As System.OperationCanceledException

                    Return False

                Catch ex As System.ArgumentException

                    Return False

                    'Catch ex As System.Net.WebException
                    '원격이름을 확인할 수 없을 때
                Catch ex As Exception

                    reconnect += 1
                    TimeT = Date.Now.Ticks
                    'MsgBox(ex.ToString)
                    'System.IO.Path.GetFileName(Server) & " (재시도 횟수 : " & reconnect & ")" & vbNewLine)

                End Try

            End If

        End While

        Return False

    End Function

    Public Enum GetFilesType

        Normal
        FindExtension
        RegularExpression

    End Enum


    Public Function backGroundDownloader(ByVal fileURL, ByVal Dir, ByVal filename)
        Try
            Dim Down As New WebClient
            Down.DownloadFile(fileURL, Dir & filename)
            Return True
        Catch ex As Exception
            MsgBox("파일 다운로드 중 문제가 발생했습니다.")
            Return False
        End Try
    End Function

    Public Function getFiles(ByVal vFolder As String, ByVal RunType As GetFilesType, Optional ByVal ExtensionOrRegularExpression As String = "") As List(Of String)

        Dim vList As List(Of String) = New List(Of String)

        If My.Computer.FileSystem.DirectoryExists(vFolder) = True Then

            Select Case RunType

                Case GetFilesType.Normal

                    For Each vFile As String In My.Computer.FileSystem.GetFiles(vFolder)

                        vList.Add(vFile)

                    Next

                    For Each Folder As String In My.Computer.FileSystem.GetDirectories(vFolder)

                        For Each vFile As String In getFiles(Folder, RunType)

                            vList.Add(vFile)

                        Next

                    Next

                Case GetFilesType.FindExtension

                    For Each vFile As String In My.Computer.FileSystem.GetFiles(vFolder)

                        If ExtensionOrRegularExpression = "" Or (Len(ExtensionOrRegularExpression) > 0 And System.IO.Path.GetExtension(vFile).ToLower = ExtensionOrRegularExpression) Then
                            vList.Add(vFile)
                        End If

                    Next

                    For Each Folder As String In My.Computer.FileSystem.GetDirectories(vFolder)

                        For Each vFile As String In getFiles(Folder, RunType, ExtensionOrRegularExpression)

                            If ExtensionOrRegularExpression = "" Or (Len(ExtensionOrRegularExpression) > 0 And System.IO.Path.GetExtension(vFile).ToLower = ExtensionOrRegularExpression) Then
                                vList.Add(vFile)
                            End If

                        Next

                    Next

                Case GetFilesType.RegularExpression

                    For Each vFile As String In My.Computer.FileSystem.GetFiles(vFolder)

                        Dim vMatch As Match = Regex.Match(System.IO.Path.GetFileName(vFile), ExtensionOrRegularExpression)

                        If vMatch.Success = True Then

                            vList.Add(vFile)

                        End If

                    Next

                    For Each Folder As String In My.Computer.FileSystem.GetDirectories(vFolder)

                        For Each vFile As String In getFiles(Folder, RunType, ExtensionOrRegularExpression)

                            Dim vMatch As Match = Regex.Match(System.IO.Path.GetFileName(vFile), ExtensionOrRegularExpression)

                            If vMatch.Success = True Then

                                vList.Add(vFile)

                            End If

                        Next

                    Next

            End Select

            vList.Sort()

        End If

        Return vList

    End Function

    Public Function Dir_Exists(ByVal OriginalPath As String) As Boolean

        Dir_Exists = My.Computer.FileSystem.DirectoryExists(OriginalPath)

    End Function

    Public Sub Dir_Hide(ByVal OriginalPath As String)
        If Dir_Exists(OriginalPath) = True Then
            System.IO.File.SetAttributes(OriginalPath, IO.FileAttributes.Hidden)
        End If
    End Sub

    Public Sub Dir_Create(ByVal OriginalPath As String)
        If My.Computer.FileSystem.DirectoryExists(OriginalPath) = False Then
            My.Computer.FileSystem.CreateDirectory(OriginalPath)
        End If
    End Sub

    Public Sub Dir_Rename(ByVal OriginalPath As String, ByVal ReName As String)
        If My.Computer.FileSystem.DirectoryExists(OriginalPath) = True Then
            ReName = ReplaceSaveName(ReName, SaveNameType.Name)
            My.Computer.FileSystem.RenameDirectory(OriginalPath, ReName)
        End If
    End Sub


    Public Sub Dir_Move(ByVal OriginalPath As String, ByVal MovePath As String)

        If My.Computer.FileSystem.DirectoryExists(OriginalPath) = True Then

            MovePath = ReplaceSaveName(MovePath, SaveNameType.Path)
            My.Computer.FileSystem.MoveDirectory(OriginalPath, MovePath, True)

        End If
    End Sub

    Public Sub Dir_Copy(ByVal OriginalPath As String, ByVal CopyPath As String)

        If My.Computer.FileSystem.DirectoryExists(OriginalPath) = True Then

            CopyPath = ReplaceSaveName(CopyPath, SaveNameType.Path)
            My.Computer.FileSystem.CopyDirectory(OriginalPath, CopyPath, True)

        End If
    End Sub

    Public Sub Dir_Delete(ByVal OriginalPath As String)
        If My.Computer.FileSystem.DirectoryExists(OriginalPath) = True Then
            My.Computer.FileSystem.DeleteDirectory(OriginalPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If
    End Sub

    Public Function File_Exists(ByVal OriginalPath As String) As Boolean

        File_Exists = My.Computer.FileSystem.FileExists(OriginalPath)

    End Function

    Public Sub File_Delete(ByVal OriginalPath As String)

        If My.Computer.FileSystem.FileExists(OriginalPath) = True Then

            My.Computer.FileSystem.DeleteFile(OriginalPath)

        End If

    End Sub

    Public Function File_Rename(ByVal OriginalPath As String, ByVal ReName As String) As Boolean
        Try

            If My.Computer.FileSystem.FileExists(OriginalPath) = True Then

                My.Computer.FileSystem.RenameFile(OriginalPath, ReName)

            End If

        Catch ex As Exception

            Return False

        End Try

        Return True

    End Function

    Public Function File_Move(ByVal OriginalPath As String, ByVal MovePath As String) As Boolean
        'MsgBox(AD)
        If My.Computer.FileSystem.FileExists(OriginalPath) = True Then
            Try

                My.Computer.FileSystem.MoveFile(OriginalPath, MovePath, True)
                Return True

            Catch ex As Exception

                Return False

            End Try

        End If

        Return False
    End Function


    Public Function File_Copy(ByVal OriginalPath As String, ByVal CopyPath As String)

        If My.Computer.FileSystem.FileExists(OriginalPath) = True Then
            Try

                My.Computer.FileSystem.CopyFile(OriginalPath, CopyPath, True)

                Return True

            Catch ex As Exception

                Return False

            End Try

        Else

            Return False

        End If
    End Function

    Public Enum FileEncoding

        Unicode
        DefaultType
        UTF8
        ASCII

    End Enum

    Public Sub Text_Write(ByVal AD As String, ByVal Data As String, Optional ByVal Encoding As String = "")

        xpFS.Dir_Create(System.IO.Path.GetDirectoryName(AD))

        If Encoding = "Unicode" Then
            My.Computer.FileSystem.WriteAllText(AD, Data, False, System.Text.Encoding.Unicode)
        ElseIf Encoding = "Default" Then
            My.Computer.FileSystem.WriteAllText(AD, Data, False, System.Text.Encoding.Default)
        ElseIf Encoding = "UTF8" Then
            My.Computer.FileSystem.WriteAllText(AD, Data, False, System.Text.Encoding.UTF8)
        ElseIf Encoding = "ASCII" Then
            My.Computer.FileSystem.WriteAllText(AD, Data, False, System.Text.Encoding.ASCII)
        Else
            My.Computer.FileSystem.WriteAllText(AD, Data, False)
        End If

    End Sub

    Public Function Read_File(ByVal AD As String, Optional ByVal Encoding As String = "", Optional ByVal FailValue As String = "") As String

        If My.Computer.FileSystem.FileExists(AD) = True Then
            If Encoding = "Unicode" Then
                Return My.Computer.FileSystem.ReadAllText(AD, System.Text.Encoding.Unicode)
            ElseIf Encoding = "UTF8" Then
                Return My.Computer.FileSystem.ReadAllText(AD, System.Text.Encoding.UTF8)
            ElseIf Encoding = "Default" Then
                Return My.Computer.FileSystem.ReadAllText(AD, System.Text.Encoding.Default)
            ElseIf Encoding = "ASCII" Then
                Return My.Computer.FileSystem.ReadAllText(AD, System.Text.Encoding.ASCII)
            Else
                Return My.Computer.FileSystem.ReadAllText(AD)
            End If
        Else
            Return FailValue
        End If

    End Function

    Public Sub ProgramOn(ByVal Path As String, Optional ByVal Wait As Boolean = False, Optional ByVal Hide As Boolean = False)

        If My.Computer.FileSystem.FileExists(Path) = True Then
            If Wait = True And Hide = True Then
                Shell(Path, AppWinStyle.Hide, True)
            ElseIf Wait = True And Hide = False Then
                Shell(Path, AppWinStyle.NormalFocus, True)
            ElseIf Wait = False And Hide = True Then
                Shell(Path, AppWinStyle.Hide, False)
            Else
                System.Diagnostics.Process.Start(Path)
            End If
        Else
            MsgBox("파일이 없습니다" & vbNewLine & "(" & Path & ")")
        End If

    End Sub

    Public Sub ProgramOn2(ByVal Path As String, Optional ByVal Hide As Boolean = False)
        'System.Diagnostics.Process.Start(Path)

        If Hide = True Then
            Shell(Path, AppWinStyle.Hide)
        Else
            'MsgBox(Path)
            Shell(Path, AppWinStyle.NormalFocus)
        End If

    End Sub

    Public Sub ProgramOff(ByVal Name As String)

        Dim PC() As System.Diagnostics.Process
        PC = System.Diagnostics.Process.GetProcessesByName(Name)

        If PC.Length = 0 Then

            '실행되지 않음

        Else

            '실행되어있음
            For i = 0 To UBound(PC)

                PC(i).Kill()

            Next

        End If

    End Sub

    Public Function ProgramRunCheck(ByVal Name As String, Optional ByVal WindowTitle As String = "") As Boolean

        'Call EnumWindows(AddressOf EnumWindowProc, &H0)

        ' ClassName
        '   devcat_launcher : 마비노기 런처
        '   Mabinogi : Client

        If FindWindow("devcat_launcher", vbNullString) > 0 Or FindWindow("Mabinogi", vbNullString) Then
            '실행되어있음
            Return True
        End If
        '실행 되어있지 않음
        Return False
    End Function

    Public Sub FolderOn(ByVal Path As String)

        If My.Computer.FileSystem.DirectoryExists(Path) = True Then
            System.Diagnostics.Process.Start(Path)
        Else
            MsgBox("폴더가 없습니다" & vbNewLine & "(" & Path & ")")
        End If

    End Sub

    Public Function ExtensionCheck(ByVal Path As String, ByVal ext As String) As Boolean

        If System.IO.Path.GetExtension(Path) = ext Then

            ExtensionCheck = True

        Else

            ExtensionCheck = False

        End If

    End Function

    Public Enum SaveNameType

        Path
        Name

    End Enum

    Public Function ReplaceSaveName(ByVal vName As String, ByVal vSaveNameType As SaveNameType) As String

        Dim getPath As String = ""
        Dim getName As String = ""

        Select Case vSaveNameType

            Case SaveNameType.Name

                getPath = ""
                getName = vName

            Case SaveNameType.Path

                getPath = System.IO.Path.GetDirectoryName(vName)
                getName = System.IO.Path.GetFileName(vName)

        End Select

        getName = Replace(getName, "\", "￦")
        getName = Replace(getName, "/", "／")
        getName = Replace(getName, ":", "：")
        getName = Replace(getName, "*", "＊")
        getName = Replace(getName, "&", "＆")
        getName = Replace(getName, "?", "？")
        getName = Replace(getName, """", "″")
        getName = Replace(getName, "<", "＜")
        getName = Replace(getName, ">", "＞")
        getName = Replace(getName, "|", "­｜")

        Return getPath & "\" & getName

    End Function

    Public Sub FileDelete(ByVal OriginalPath As String)

        If My.Computer.FileSystem.FileExists(OriginalPath) = True Then

            My.Computer.FileSystem.DeleteFile(OriginalPath)

        End If

    End Sub
End Module
