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
    Private Sub FrmLoaded() Handles MyBase.Load
        If Setting.Mabi = vbNullString Then
            If Func.setMabiPath() Then
                Me.MabiPath.Text = Setting.Mabi
            End If
        End If

        Init.Close()
        Dim tmp As String = Init.myver.ToString
        Dim mainv As String = tmp.Substring(0, 1)
        Dim subv As String = tmp.Substring(1, 1)
        Dim minorv As String = tmp.Substring(2)

        Me.Text = Init.programID & " Ver : " & mainv & "." & subv & "." & minorv & " Build : " & Init.myRelease
        Init.Close()
        Me.Show()
        Me.ProgressMainTrd(0)
        Me.ProgressSubTrd(0)
        Me.progressTextChange("부정한 방법으로 타인의 주민등록번호를 이용하실 경우 불이익을 받을 수 있습니다." & vbCrLf & "장시간의 게임이용은 건강을 해칠 수 있습니다.")
        'Me.resultboxpanel.Width = 0
        Me.Refresh()
        Setting.loadSetting()

        Me.BackColor = Func.rgb(80, 148, 184)
        Me.MabiPath.Text = Setting.Mabi
        Me.TrayIcon.Visible = True
        Me.ShowHideProgress(True)
        If Setting.Mabi = "" Then
            Me.MabiPath.BackColor = Color.Gray
        End If
        If Setting.MabiTest = "" Then
            Me.mabiTestPath.BackColor = Color.Gray
        End If
        Assistant.Show()
        Dim tmpnotice As New notificationwindow("업데이트가 필요한 항목을 자동으로 업데이트합니다.", 6)
        Dim mabicount As Integer = 0

        For Each proc As Process In Process.GetProcessesByName("Client")
            Dim windClass As String = Strings.Space(256)
            GetClassName(proc.MainWindowHandle, windClass, 256)
            If InStr(windClass, "Mabinogi") Then
                mabicount += 1
            End If
        Next

        If File.Exists(WLAB.ROOT & "userdata\") Then

        End If

        Directory.CreateDirectory(WLAB.TEMP)
        Directory.CreateDirectory(WLAB.RESOURCES)
        Directory.CreateDirectory(WLAB.LABS)

        Try
            Dim userdat As String = WLAB.ROOT & "userdata\userdata.usd"
            Using usd As New FileStream(userdat, FileMode.Open, FileAccess.Read)
                Using br As New BinaryReader(usd)
                    Dim data() As Byte = br.ReadBytes(16)
                    Dim conv As New StringBuilder
                    For i As Integer = 0 To 15
                        conv.Append(Convert.ToChar(data(i)))
                    Next
                    'MsgBox(conv.ToString)
                    Me.userLIDC = conv.ToString
                    conv.Clear()

                    data = br.ReadBytes(128)
                    For i As Integer = 0 To 127
                        conv.Append(Convert.ToChar(data(i)))
                    Next

                    Me.userPASS = conv.ToString
                    Dim url As String = "http://library.willowslab.net/intro/?user_id=" & Me.userLIDC & "&password=" & Me.userPASS
                    Dim uri As New System.Uri(url)
                    Me.WebBrowser2.Url = uri
                    'MsgBox(Me.WebBrowser2.Url.ToString)
                    'MsgBox(conv.ToString)
                End Using
            End Using
        Catch ex As Exception
            MsgBox("유저 정보를 읽는데 실패했습니다. 로그인에 실패했으므로 수동으로 로그인을 시도하셔야 합니다.")
        End Try


        If mabicount > 0 Then
            MsgBox("마비노기가 실행되어 있어 프로그램의 초기화를 완료하지 못했습니다. 마비노기를 종료한 뒤 다시 실행해주셔야 정상적인 동작이 가능합니다.")
            Me.frmTitle.Text &= " (Initialize Failed)"
        Else
            Dim trd As New Thread(New ThreadStart(AddressOf startTrd))
            trd.IsBackground = True
            trd.Start()
        End If
        Me.Text = Me.frmTitle.Text
    End Sub

    Private Sub startTrd()
        Try
            Dim msgb As Boolean = False

            Me.MabinogiPatch(False)
            Me.MabinogiPatch(True)

            If Setting.Mabi <> vbNullString Then
                If Directory.Exists(Setting.Mabi & "\Package\") Then
                    Directory.CreateDirectory(WLAB.CONF & "lib\server_main\")
                    For Each pack As String In Directory.GetFiles(WLAB.CONF & "lib\server_main\")
                        File.Delete(pack)
                    Next
                    For Each pack As String In Directory.GetFiles(Setting.Mabi & "\package\")
                        Try
                            Dim FileA As FileAttributes = File.GetAttributes(pack)
                            '대상 폴더 파일 속성이 읽기전용 이라면..
                            If (FileA And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
                                '파일 속성을 해제합니다.
                                File.SetAttributes(pack, FileAttributes.Normal)
                            End If
                            If pack.Substring(pack.LastIndexOf(".") + 1).ToUpper = "PACK" And (InStr(pack, "_to_") Or InStr(pack, "_full")) Then
                                progressTextChange(pack.Substring(pack.LastIndexOf("\") + 1) & " 라이브러리 생성중...")
                                Mabinogi.FileLists(pack)
                            End If
                        Catch ex As Exception
                            msgb = True
                        End Try
                    Next
                End If
            End If

            If Setting.MabiTest <> vbNullString Then
                If Directory.Exists(Setting.MabiTest & "\Package\") Then
                    Directory.CreateDirectory(WLAB.CONF & "lib\server_test\")
                    For Each pack As String In Directory.GetFiles(WLAB.CONF & "lib\server_test\")
                        File.Delete(pack)
                    Next
                    For Each pack As String In Directory.GetFiles(Setting.MabiTest & "\package\")
                        Try
                            Dim FileA As FileAttributes = File.GetAttributes(pack)
                            '대상 폴더 파일 속성이 읽기전용 이라면..
                            If (FileA And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
                                '파일 속성을 해제합니다.
                                File.SetAttributes(pack, FileAttributes.Normal)
                            End If
                            If pack.Substring(pack.LastIndexOf(".") + 1).ToUpper = "PACK" And (InStr(pack, "_to_") Or InStr(pack, "_full")) Then
                                progressTextChange(pack.Substring(pack.LastIndexOf("\") + 1) & " 라이브러리 생성중...")
                                Mabinogi.FileLists(pack)
                            End If
                        Catch ex As Exception
                            msgb = True
                        End Try
                    Next
                End If
            End If

            If msgb Then
                MsgBox("하나 이상의 패키지 라이브러리를 생성하지 못했습니다. 작동에 지장이 있을 수 있습니다. 이러한 현상이 계속되면 문의해주시기 바랍니다.", MsgBoxStyle.Exclamation, "오류")
            End If
            readList()

            If Setting.autoUpdated Then
                listReloadThreadPatched()
            Else
                listReloadThread()
            End If
        Catch ex As Exception
            MsgBox("서버가 점검중이거나 서버에 문제가 발생하여 버들 실험실 패키지 라이브러리 초기화에 실패했습니다.")
        End Try
    End Sub
    Private Sub MabinogiPatch(ByVal isTestserver As Boolean)
        Try
            Dim mabidir As String = Setting.Mabi
            Dim patchtxt As String = "http://211.218.233.238/patch/patch.txt"
            If isTestserver Or File.Exists(Setting.Mabi & "\mabinogi_test.exe") Then
                patchtxt = "http://211.218.233.238/patch/patch_test.txt"
                mabidir = Setting.MabiTest
            Else
                mabidir = Setting.Mabi
            End If
            If mabidir = vbNullString Then
                If isTestserver = False Then
                    MsgBox("마비노기 설치경로를 설정해준 다음 다시 이 프로그램을 실행해 주세요.")
                    Me.TabControl1.SelectedTab = Me.TabPage2
                End If
                Exit Sub
            Else
                Dim patch_accept As String = ""
                Dim local_version As String = ""
                Dim local_ftp As String = ""
                Dim main_version As String = ""
                Dim main_ftp As String = ""
                Dim launcherinfo As String = ""
                Dim login As String = ""
                Dim arg As String = ""
                Dim addin As String = ""
                Dim main_fullversion As String = ""
                Dim trycntmsg As String = ""
                If tryCount > 0 Then
                    trycntmsg = tryCount & " 회 재시도" & vbCrLf
                End If

                Me.totalSize = 0UL
                Me.triedSize = 0UL
                Dim nc As New WebClient
                nc.Encoding = Encoding.UTF8
                Dim patchinfo As String = nc.DownloadString(patchtxt)
                Try
                    For Each vLine As String In Split(patchinfo, vbNewLine)
                        If InStr(vLine, "patch_accept=") > 0 Then
                            patch_accept = vLine.Replace("patch_accept=", "")
                        ElseIf InStr(vLine, "local_version=") > 0 Then
                            local_version = vLine.Replace("local_version=", "")
                            If Setting.isTestMabi Then
                                main_fullversion = local_version
                            End If
                        ElseIf InStr(vLine, "local_ftp=") > 0 Then
                            local_ftp = vLine.Replace("local_ftp=", "")
                        ElseIf InStr(vLine, "main_version=") > 0 Then
                            main_version = vLine.Replace("main_version=", "")
                        ElseIf InStr(vLine, "main_ftp=") > 0 Then
                            main_ftp = vLine.Replace("main_ftp=", "")
                        ElseIf InStr(vLine, "launcherinfo=") > 0 Then
                            launcherinfo = vLine.Replace("launcherinfo=", "")
                        ElseIf InStr(vLine, "login=") > 0 Then
                            login = vLine.Replace("login=", "")
                        ElseIf InStr(vLine, "arg=") > 0 Then
                            arg = vLine.Replace("arg=", "")
                        ElseIf InStr(vLine, "addin=") > 0 Then
                            addin = vLine.Replace("addin=", "")
                        ElseIf InStr(vLine, "main_fullversion=") > 0 Then
                            main_fullversion = vLine.Replace("main_fullversion=", "")
                        End If
                    Next
                Catch ex As Exception
                    Console.WriteLine(ex.GetBaseException.ToString)
                End Try
                Dim myversion As Integer = 0
                Dim idx As Integer = -1

                Using readver As New FileStream(mabidir & "\version.dat", FileMode.Open, FileAccess.Read)
                    Dim temp As Byte() = New Byte(4) {}
                    readver.Read(temp, 0, 4)
                    myversion = BitConverter.ToUInt32(temp, 0)
                End Using

                Dim patchdir As String = mabidir & "\_mltemp_\" & myversion.ToString & "_to_" & local_version

                If myversion = Val(local_version) Then
                    Exit Sub
                Else
                    Me.ShowHideProgress(False)
                    Me.plistEabledChange(False)
                    If myversion < Val(main_fullversion) Then
                        MsgBox("마비노기가 너무 오래되었습니다.", MsgBoxStyle.Information, "알림")
                    Else
                        'progressTextChange
                        Dim patchinfostr As String = "http://" & local_ftp & local_version & "/" & myversion.ToString & "_to_" & local_version
                        patchinfo = nc.DownloadString(patchinfostr & ".txt")
                        Directory.CreateDirectory(patchdir)
                        Dim filecount As Integer = 0
                        Dim filelist As New List(Of String)
                        Dim filesize As New List(Of Integer)
                        Dim filehash As New List(Of String)
                        For Each line As String In patchinfo.Split(Chr(10))
                            If idx < 0 Then
                                filecount = Val(line)
                            Else
                                Try
                                    Dim temp() As String = line.Split(",")
                                    If temp(0).Trim <> vbNullString Then
                                        filelist.Add(temp(0))
                                        filesize.Add(Val(temp(1)))
                                        Me.totalSize += CULng(Val(temp(1)))
                                    End If
                                Catch ex As Exception
                                    Console.WriteLine(ex.GetBaseException.ToString)
                                End Try
                            End If
                            idx += 1
                        Next
                        My.Computer.FileSystem.DeleteDirectory(patchdir, FileIO.DeleteDirectoryOption.DeleteAllContents)

                        Directory.CreateDirectory(patchdir)

                        Dim i As Integer = 0
                        For Each f As String In filelist
                            Dim filenumber As String = ""
                            If i >= 100 Then
                                filenumber = "." & i.ToString
                            ElseIf i >= 10 Then
                                filenumber = ".0" & i.ToString
                            Else
                                filenumber = ".00" & i.ToString
                            End If
                            fdownloader = New WebFileDownloader
                            progressTextChange(trycntmsg & local_version & "/" & myversion.ToString & "_to_" & local_version & filenumber)
                            fdownloader.DownloadFileWithProgress(patchinfostr & filenumber, patchdir & "\" & myversion.ToString & "_to_" & local_version & filenumber)
                            triedSize += filesize(i)
                            'MsgBox(patchinfostr & filenumber)
                            i += 1
                        Next
                        i = 0

                        For Each f As String In filelist
                            progressTextChange(trycntmsg & "patch files merging... " & f)
                            If i = 0 Then
                                'MsgBox(f)
                                File.Copy(patchdir & "\" & f, patchdir & "\" & f.Replace(".000", ".zip"))
                                File.Delete(patchdir & "\" & f)
                            Else
                                My.Computer.FileSystem.WriteAllBytes(patchdir & "\" & myversion.ToString & "_to_" & local_version & ".zip", File.ReadAllBytes(patchdir & "\" & f), True)
                                File.Delete(patchdir & "\" & f)
                            End If
                            i += 1
                        Next

                        If SevenSharpZip.DeCompress(patchdir & "\" & myversion.ToString & "_to_" & local_version & ".zip", mabidir & "\") Then
                            progressTextChange(trycntmsg & "decompressing patch file...")
                            Dim time As Integer = 1500 * filecount
                            Thread.Sleep(time)
                            Try
                                My.Computer.FileSystem.DeleteDirectory(mabidir & "\_mltemp_\", FileIO.DeleteDirectoryOption.DeleteAllContents)
                            Catch ex As Exception
                            End Try
                            Dim nver As Integer = CType(Val(local_version), Integer)
                            File.WriteAllBytes(mabidir & "\version.dat", BitConverter.GetBytes(nver))
                        Else
                            Throw New Exception("압축 해제에 실패함")
                        End If
                    End If
                    Me.ShowHideProgress(True)
                    Me.plistEabledChange(True)
                End If
            End If
        Catch ex As Exception
            Thread.Sleep(1000)
            Try
                My.Computer.FileSystem.DeleteDirectory(mabiDir & "\_mltemp_\", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex2 As Exception

            End Try
            If tryCount < 5 Then
                tryCount += 1
                MabinogiPatch(isTestserver)
            Else
                MsgBox("마비노기 패치에 실패했습니다. 마비노기가 실행되어있거나 그 이외의 경우입니다. 자세한 내용은 log를 확인하시기 바랍니다.")
                addLog(ex.GetBaseException.ToString)
            End If
        End Try
    End Sub
End Class
