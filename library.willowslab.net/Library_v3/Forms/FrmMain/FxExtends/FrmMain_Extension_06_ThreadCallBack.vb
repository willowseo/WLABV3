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
    Public Sub InstallFile()
        If Me.InvokeRequired Then
            Dim work As New SetCallback(AddressOf InstallFile)
            Me.Invoke(work, New Object() {[Text]})
        Else
            CMDworkList.Clear()
            Me.ShowHideProgress(False)
            Me.plistEabledChange(False)
            total = 0
            current = 0
            currentfile = 0
            Dim listitem As New ListView
            Dim final As New ListView
            Dim checkCount As Integer = 0
            '
            ' 리스트에 아이템 담기
            '
            If plist1.CheckedItems.Count > 0 Then
                For Each item As ListViewItem In plist1.CheckedItems
                    Dim tmpitem As ListViewItem = New ListViewItem({item.SubItems(3).Text, item.SubItems(5).Text, item.SubItems(6).Text, item.SubItems(7).Text, item.SubItems(0).Text, item.SubItems(4).Text})
                    listitem.Items.Add(tmpitem)
                Next
            End If
            If plist2.CheckedItems.Count > 0 Then
                For Each item As ListViewItem In plist2.CheckedItems
                    Dim tmpitem As ListViewItem = New ListViewItem({item.SubItems(3).Text, item.SubItems(5).Text, item.SubItems(6).Text, item.SubItems(7).Text, item.SubItems(0).Text, item.SubItems(4).Text})
                    listitem.Items.Add(tmpitem)
                Next
            End If
            If plist3.CheckedItems.Count > 0 Then
                For Each item As ListViewItem In plist3.CheckedItems
                    Dim tmpitem As ListViewItem = New ListViewItem({item.SubItems(3).Text, item.SubItems(5).Text, item.SubItems(6).Text, item.SubItems(7).Text, item.SubItems(0).Text, item.SubItems(4).Text})
                    listitem.Items.Add(tmpitem)
                Next
            End If
            If plist4.CheckedItems.Count > 0 Then
                For Each item As ListViewItem In plist4.CheckedItems
                    Dim tmpitem As ListViewItem = New ListViewItem({item.SubItems(3).Text, item.SubItems(5).Text, item.SubItems(6).Text, item.SubItems(7).Text, item.SubItems(0).Text, item.SubItems(4).Text})
                    listitem.Items.Add(tmpitem)
                Next
            End If

            For Each item As ListViewItem In listitem.Items
                total += Val(item.SubItems(2).Text)
            Next
            addLog(total)

            For Each item As ListViewItem In listitem.Items
                Dim path As String = item.SubItems(0).Text
                Dim url As String = item.SubItems(1).Text
                Dim size As String = item.SubItems(2).Text
                Dim mdate As Integer = Val(item.SubItems(3).Text)
                Dim dlflag As Boolean = False
                Dim dir As String = path.Substring(0, path.LastIndexOf("\") + 1)
                Dim fileinfo As New FileInfo(path)
                Dim df As String = url.Replace("/", "\")
                Dim dest As String = Setting.Mabi & "\" & df.Substring(0, df.LastIndexOf("\"))
                currentfile = size
                dlfilename = item.SubItems(4).Text
                If File.Exists(path) Then
                    If My.Computer.FileSystem.GetFileInfo(path).LastWriteTime().ToString("yyyyMMddHH") < mdate Then
                        dlflag = True
                    End If
                    If fileinfo.Length <> size Then
                        dlflag = True
                    End If
                Else
                    dlflag = True
                End If
                If url.Substring(url.LastIndexOf(".") + 1) <> "lab" Then
                    dlflag = True
                End If
                If dlflag Then
                    If fd(Setting.Server(1) & url, dir, path.Substring(path.LastIndexOf("\") + 1)) Then
                        If Not Directory.Exists(dest) Then
                            Directory.CreateDirectory(dest)
                        End If
                        If url.Substring(url.LastIndexOf(".") + 1) <> "lab" Then
                            File.Copy(path, Setting.Mabi & "\" & url.Replace("/", "\"), True)
                        End If
                    End If
                Else
                    If Not Directory.Exists(dest) Then
                        Directory.CreateDirectory(dest)
                    End If
                    If url.Substring(url.LastIndexOf(".") + 1) <> "lab" Then
                        File.Copy(path, Setting.Mabi & "\" & url.Replace("/", "\"), True)
                    Else
                        'getVersion(Setting.Mabi & "\version.dat")
                    End If
                End If

                Dim localFileInfo As New FileInfo(path)
                If url.Substring(url.LastIndexOf(".") + 1) = "lab" Then
                    CMDworkList.Add(path)
                    File.WriteAllBytes(Setting.Mabi & "\" & url.Replace("/", "\"), File.ReadAllBytes(Setting.Mabi & "\version.dat"))
                End If
                current += size
                ProgressMainTrd(current / total * 10000)
            Next
            Me.trd = New Thread(New ThreadStart(AddressOf Me.runUnpack))
            trd.IsBackground = True
            trd.Start()
        End If
    End Sub
    Public Function fd(ByVal url As String, ByVal dir As String, ByVal file As String)
        Try
            dir = dir.Replace("/", "\")
            Directory.CreateDirectory(dir)
            _Downloader = New WebFileDownloader
            _Downloader.DownloadFileWithProgress(url, dir & file.Replace("%23", "#"))

            Return True
        Catch ex As Exception
            addLog(ex.GetBaseException.ToString)
            Return False
        End Try
    End Function
    Public Sub progressTextChange(ByVal text As String)
        If Me.ProgressSub.InvokeRequired Then
            Dim work As New ThreadInvoker(AddressOf progressTextChange)
            Me.Invoke(work, New Object() {text})
        Else
            If text = "default" Then
                progressTextChange("부정한 방법으로 타인의 주민등록번호를 이용하실 경우 불이익을 받을 수 있습니다." & vbCrLf & "장시간의 게임이용은 건강을 해칠 수 있습니다.")
            Else
                Me.progressText.Text = text
            End If
        End If
    End Sub
    Public Sub ProgressSubTrd(ByVal i As Integer)
        If Me.ProgressSub.InvokeRequired Then
            Dim work As New ProgressBarControl(AddressOf ProgressSubTrd)
            Me.Invoke(work, New Object() {i})
        Else
            Dim d As Double = i / 10000
            If d < 0 Then
                d = 0
            End If

            Me.ProgressSub.Width = Math.Floor(d * (Me.progPNL.Width - 2))
        End If
    End Sub

    Public Sub ProgressMainTrd(ByVal i As Integer)
        If Me.progressMain.InvokeRequired Then
            Dim work As New ProgressBarControl(AddressOf ProgressMainTrd)
            Me.Invoke(work, New Object() {i})
        Else
            Dim d As Double = i / 10000
            If d < 0 Then
                d = 0
            End If

            Me.progressMain.Width = Math.Floor(d * (Me.progPNL.Width - 2))
        End If
    End Sub
    Public Sub ProgBar(ByVal val As Integer)
        If Me.ProgressSub.InvokeRequired Then
            Dim work As New ProgressBarControl(AddressOf ProgressSubTrd)
            Me.Invoke(work, New Object() {val})
        Else
            If val > 711 Then
                val = 711
            End If
            Me.ProgressSub.Width = val
        End If
    End Sub

    Public Sub ShowHideProgress(ByVal bool As Integer)
        If Me.progressText.InvokeRequired Then
            Dim work As New ThreadInvoker(AddressOf ShowHideProgress)
            Me.Invoke(work, New Object() {bool})
        Else
            If bool Then
                Me.progressText.Height = 52
                Me.prgLFT.Visible = False
                Me.prgRGT.Visible = False
                Me.progPNL.Visible = False
            Else
                progressText.Height = 34
                Me.prgLFT.Visible = True
                Me.prgRGT.Visible = True
                Me.progPNL.Visible = True
            End If
        End If
    End Sub

    Public Sub changeInfopage(ByVal URL As Integer)
        If Me.infoviewer.InvokeRequired Then
            Dim work As New ThreadInvoker(AddressOf changeInfopage)
            Me.Invoke(work, New Object() {URL})
        Else
            Me.infoviewer.Url = New Uri(URL)
        End If
    End Sub

    Public Sub logoutUnset()
        If Me.InvokeRequired Then
            Try
                Dim add As New SetCallback(AddressOf logoutUnset)
                Me.Invoke(add, New Object() {})
            Catch ex As Exception
            End Try
        Else
            Setting.ID = ""
            Setting.PW = ""
            Setting.autologin = False
            Setting.useRegister = False

            Me.listReloadThread()
            Setting.saveSetting()
            settingInit()
        End If
    End Sub

    Public Sub settingInit()
        If Me.InvokeRequired Then
            Try
                Dim add As New SetCallback(AddressOf settingInit)
                Me.Invoke(add, New Object() {})
            Catch ex As Exception
            End Try
        Else

            Me.autoUpdate.Checked = Setting.autoUpdated
            Me.allowTray.Checked = Not Setting.allowTray
            windowController.CheckBox2.Checked = Setting.isTestMabi
        End If
    End Sub

    Public Sub plistEabledChange(ByVal [bool] As Boolean)
        If Me.plistother.InvokeRequired Then
            Try
                Dim add As New booleans(AddressOf plistEabledChange)
                Me.Invoke(add, New Object() {[bool]})
            Catch ex As Exception
            End Try
        Else
            Me.plistother.Enabled = [bool]
            Me.btns.Enabled = [bool]
            Me.refrashing.Visible = Not [bool]
            Assistant.Enabled = [bool]
            Assistant.TopMost = [bool]
            Me.Button1.Enabled = [bool]
        End If
    End Sub

    Public Sub addLog(ByVal [text] As String)
        If Me.Logbox.InvokeRequired Then
            Try
                Dim d As New SetCallback(AddressOf addLog)
                Me.Invoke(d, New Object() {[text]})
            Catch

            End Try
        Else
            Me.Logbox.Text &= [text] & vbNewLine
        End If
    End Sub

    Public Sub initLocalPackage()
        If Me.InvokeRequired Then
            Try
                Dim action As New SetCallback(AddressOf initLocalPackage)
                Me.Invoke(action, New Object() {})
            Catch ex As Exception
            End Try
        Else
            Me.PackageFileList.Items.Clear()
            For Each root As String In Directory.GetDirectories(Setting.Mabi & "\package")
                For Each filepath As String In Directory.GetFiles(root)
                    If filepath.Substring(filepath.LastIndexOf(".") + 1) = "pack" Then
                        PackageFileList.Items.Add(New ListViewItem({filepath.Replace(Setting.Mabi & "\", "")}))
                    End If
                Next
            Next
            For Each filepath As String In Directory.GetFiles(Setting.Mabi & "\package")
                If filepath.IndexOf("_to_") = -1 And filepath.IndexOf("_full") = -1 Then
                    If filepath.Substring(filepath.LastIndexOf(".") + 1) = "pack" Then
                        PackageFileList.Items.Add(New ListViewItem({filepath.Replace(Setting.Mabi & "\", "")}))
                    End If
                End If
            Next
        End If
    End Sub
End Class
