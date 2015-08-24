Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Security.AccessControl
Imports System.Net.Sockets

Public Class Assistant
#Region "Local Variable"
    Dim MABI_PATH As String
    Dim MABI_TEST_PATH As String
    Public MabiRunCount As Integer = 0
    Dim PathDialog = New FolderBrowserDialog()
    Dim selectedItem As New ListViewItem
    Dim selectedproc As Integer
    Dim myproc As Process
#End Region

#Region "Mabinogi Patch and Start"

    Dim patchinfo As String = "http://211.218.233.238/patch/patch.txt"
    Dim patchinfotest As String = "http://211.218.233.238/patch/patch_test.txt"
    Private Structure MabiPatchFileInfo

        Dim FileName As String
        Dim FileSize As String
        Dim FileCode As String

        Public Function setInfo(ByVal vLine As String)
            Try
                Dim Temp() As String = Split(vLine, ",")
                FileName = Trim(Temp(0))
                FileSize = Trim(Temp(1))
                FileCode = Trim(Temp(2))
                Return True
            Catch ex As Exception
                MsgBox(ex.ToString)
                Return False
            End Try
        End Function
    End Structure
    Private Structure MabiUpdateInfo

        Dim patch_accept As String
        Dim local_version As String
        Dim local_ftp As String
        Dim main_version As String
        Dim main_ftp As String
        Dim launcherinfo As String
        Dim login As String
        Dim arg As String
        Dim addin As String
        Dim main_fullversion As String

        Public Function setInfo(ByVal patchfile As String)

            If IO.File.Exists(patchfile) = True Then
                Dim Temp As String = IO.File.ReadAllText(patchfile)
                For Each vLine As String In Split(Temp, vbNewLine)
                    If InStr(vLine, "patch_accept=") > 0 Then
                        patch_accept = Replace(vLine, "patch_accept=", "")
                    ElseIf InStr(vLine, "local_version=") > 0 Then
                        local_version = Replace(vLine, "local_version=", "")
                        If Setting.isTestMabi Then
                            main_fullversion = local_version
                        End If
                    ElseIf InStr(vLine, "local_ftp=") > 0 Then
                        local_ftp = Replace(vLine, "local_ftp=", "")
                    ElseIf InStr(vLine, "main_version=") > 0 Then
                        main_version = Replace(vLine, "main_version=", "")
                    ElseIf InStr(vLine, "main_ftp=") > 0 Then
                        main_ftp = Replace(vLine, "main_ftp=", "")
                    ElseIf InStr(vLine, "launcherinfo=") > 0 Then
                        launcherinfo = Replace(vLine, "launcherinfo=", "")
                    ElseIf InStr(vLine, "login=") > 0 Then
                        login = Replace(vLine, "login=", "")
                    ElseIf InStr(vLine, "arg=") > 0 Then
                        arg = Replace(vLine, "arg=", "")
                    ElseIf InStr(vLine, "addin=") > 0 Then
                        addin = Replace(vLine, "addin=", "")
                    ElseIf InStr(vLine, "main_fullversion=") > 0 Then
                        main_fullversion = Replace(vLine, "main_fullversion=", "")
                    End If
                Next
                IO.File.Delete(patchfile)
                Return True
            Else
                Return False
            End If
        End Function
    End Structure
    Public Function ClientType_Kor() As String
        If Setting.isTestMabi = False Then
            Return "정식 서버"
        Else
            Return "테스트 서버"
        End If
    End Function
    Private Function IsPortOpen(ByVal Host As String, ByVal PortNumber As Integer) As Boolean
        Dim Client As TcpClient = Nothing
        Try
            Client = New TcpClient(Host, PortNumber)
            Return True
        Catch ex As SocketException
            Return False
        Finally
            If Not Client Is Nothing Then
                Client.Close()
            End If
        End Try
    End Function
    Private Function isServerOpen(ByVal Mabiinfo As MabiUpdateInfo) As Boolean
        Try
            If Mabiinfo.patch_accept.IndexOf("1") > -1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
        End Try
        Return False
    End Function

    Public Sub Start()
        Dim MabiInfo As MabiUpdateInfo = New MabiUpdateInfo
        Dim url As String = patchinfo
        If Setting.isTestMabi Then
            url = patchinfotest
        End If
        If Func.webClient(url, Setting.Data, "patch.txt") Then
            MabiInfo.setInfo(Setting.Data & "patch.txt")
            Dim Port As Integer = 11000
            Dim Hostname As String = MabiInfo.login
            'Call the function
            Dim PortOpen As Boolean = IsPortOpen(Hostname, Port)
            If PortOpen And Me.isServerOpen(MabiInfo) Then
                Run(MabiInfo.login, MabiInfo.arg)
            Else
                If MsgBox("마비노기가 패치중이거나 서버가 닫혀있습니다. 그래도 마비노기를 실행하시겠습니까?", MsgBoxStyle.OkCancel Or MsgBoxStyle.Information Or vbMsgBoxSetForeground) = MsgBoxResult.Ok Then
                    Run(MabiInfo.login, MabiInfo.arg)
                End If
            End If
        End If
    End Sub

    Public Function Run(login As String, arg As String) As Boolean
        Dim Prcs = New Process()
        Dim PrcsSI = New ProcessStartInfo
        Dim Result As String = "", Err As String = ""
        Dim mabipath As String = Setting.Mabi
        If Setting.isTestMabi Then
            mabipath = Setting.MabiTest
        End If
        Dim Arguments As String = "Client.exe code:1622 ver:" & Mabinogi.getVersion & " logip:" & login & " logport:11000 " & arg
        With PrcsSI
            .FileName = "CMD.exe"
            .WorkingDirectory = mabipath
            .WindowStyle = ProcessWindowStyle.Hidden
            .CreateNoWindow = True
            .UseShellExecute = False
            .RedirectStandardInput = True
            .RedirectStandardOutput = True
            .RedirectStandardError = True
        End With
        With Prcs
            .EnableRaisingEvents = False
            .StartInfo = PrcsSI
            .Start()
            .StandardInput.WriteLine(Arguments)
            .StandardInput.Close()
            .WaitForExit()
        End With

        Return True
    End Function
    '마비노기 버전 얻기
    Public Function getVersion() As Integer

        Dim fs As IO.FileStream = Nothing
        Dim br As IO.BinaryReader = Nothing
        Dim mVersion As UInteger = 0
        Dim FilePath As String = Setting.Mabi & "\version.dat"
        Dim mabitestv As String = "Mabinogi "
        If Setting.isTestMabi Then
            FilePath = Setting.MabiTest & "\version.dat"
            mabitestv = "Mabinogi - test "
        End If
        Try
            If My.Computer.FileSystem.FileExists(FilePath) = True Then
                fs = New IO.FileStream(FilePath, IO.FileMode.Open, IO.FileAccess.Read)
                br = New IO.BinaryReader(fs)
                mVersion = br.ReadUInt32()
            End If
        Catch ex As Exception
            'MsgBox(ex.GetBaseException().ToString)
        Finally
            If br Is Nothing = False Then
                br.Close()
            End If
            If fs Is Nothing = False Then
                fs.Close()
            End If
        End Try
        Return mVersion

    End Function
    Public Function setVersion(ByVal vVersion As String) As Integer

        If IO.File.Exists(Setting.Mabi & "\version.dat") = True Then

            Dim MabiByteCount As Integer = 4 '마비노기 버전의 바이트 수
            'Dim MabiByteCount As Integer = My.Computer.FileSystem.ReadAllBytes(vInfo.Dir_Mabinogi & "\version.dat").Length '마비노기 버전의 바이트 수
            Dim HexCode As String = Hex(vVersion)
            Dim HexCodeCount As Integer = HexCode.Length
            Dim ReserseHexCode = ""


            '0 붙여주기
            For i = 0 To (MabiByteCount * 2 - 1) - HexCodeCount
                HexCode = "0" & HexCode
            Next

            For i = (MabiByteCount) - 1 To 0 Step -1
                ReserseHexCode &= HexCode.Substring(i * 2, 2)
            Next

            Dim bytes(MabiByteCount - 1) As Byte

            For i As Integer = 0 To MabiByteCount - 1
                bytes(i) = Convert.ToByte(ReserseHexCode.Substring(i * 2, 2), 16)
            Next
            My.Computer.FileSystem.WriteAllBytes(Setting.Mabi & "\version.dat", bytes, False)

        End If
        Return 0
    End Function
#End Region

#Region "Enum"
    Public Enum WindowStyles As Long
        WS_OVERLAPPED
        WS_POPUP = 2147483648L
        WS_CHILD = 1073741824L
        WS_MINIMIZE = 536870912L
        WS_VISIBLE = 268435456L
        WS_DISABLED = 134217728L
        WS_CLIPSIBLINGS = 67108864L
        WS_CLIPCHILDREN = 33554432L
        WS_MAXIMIZE = 16777216L
        WS_BORDER = 8388608L
        WS_DLGFRAME = 4194304L
        WS_VSCROLL = 2097152L
        WS_HSCROLL = 1048576L
        WS_SYSMENU = 524288L
        WS_THICKFRAME = 262144L
        WS_GROUP = 131072L
        WS_TABSTOP = 65536L
        WS_MINIMIZEBOX = 131072L
        WS_MAXIMIZEBOX = 65536L
        WS_CAPTION = 12582912L
        WS_TILED = 0L
        WS_ICONIC = 536870912L
        WS_SIZEBOX = 262144L
        WS_TILEDWINDOW = 13565952L
        WS_OVERLAPPEDWINDOW = 13565952L
        WS_POPUPWINDOW = 2156396544L
        WS_CHILDWINDOW = 1073741824L
        WS_EX_DLGMODALFRAME = 1L
        WS_EX_NOPARENTNOTIFY = 4L
        WS_EX_TOPMOST = 8L
        WS_EX_ACCEPTFILES = 16L
        WS_EX_TRANSPARENT = 32L
        WS_EX_MDICHILD = 64L
        WS_EX_TOOLWINDOW = 128L
        WS_EX_WINDOWEDGE = 256L
        WS_EX_CLIENTEDGE = 512L
        WS_EX_CONTEXTHELP = 1024L
        WS_EX_RIGHT = 4096L
        WS_EX_LEFT = 0L
        WS_EX_RTLREADING = 8192L
        WS_EX_LTRREADING = 0L
        WS_EX_LEFTSCROLLBAR = 16384L
        WS_EX_RIGHTSCROLLBAR = 0L
        WS_EX_CONTROLPARENT = 65536L
        WS_EX_STATICEDGE = 131072L
        WS_EX_APPWINDOW = 262144L
        WS_EX_OVERLAPPEDWINDOW = 768L
        WS_EX_PALETTEWINDOW = 392L
        WS_EX_LAYERED = 524288L
        WS_EX_NOINHERITLAYOUT = 1048576L
        WS_EX_LAYOUTRTL = 4194304L
        WS_EX_COMPOSITED = 33554432L
        WS_EX_NOACTIVATE = 67108864L
    End Enum
    Public Enum WindowLongFlags As Integer
        GWL_EXSTYLE = -20
        GWLP_HINSTANCE = -6
        GWLP_HWNDPARENT = -8
        GWL_ID = -12
        GWL_STYLE = -16
        GWL_USERDATA = -21
        GWL_WNDPROC = -4
        DWLP_USER = &H8
        DWLP_MSGRESULT = &H0
        DWLP_DLGPROC = &H4
    End Enum
#End Region

#Region "WinAPI"
    Public Structure RECT
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer
    End Structure

    Public Delegate Function EnumCallBackDelegate(hwnd As IntPtr, lparam As IntPtr) As Boolean
    Public Delegate Function EnumWindowsProc(ByVal hWnd As IntPtr, ByVal lParam As IntPtr) As Boolean

    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    Public Declare Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As IntPtr, ByRef lpRect As RECT) As Integer
    Public Declare Function SetWindowPos Lib "user32" (ByVal hWnd As IntPtr, ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal Y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
    Public Declare Auto Function SetWindowText Lib "user32" (ByVal hWnd As IntPtr, ByVal lpstring As String) As Boolean
    Public Declare Function EnumWindows Lib "user32" (lpEnumFunc As EnumCallBackDelegate, lParam As Integer) As Integer
    Public Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (hwnd As IntPtr, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef lpString As String, cch As Integer) As Integer
    Public Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (hWnd As IntPtr, nIndex As Integer) As Integer
    Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (hWnd As IntPtr, nIndex As Integer, dwNewLong As IntPtr) As Integer
    Public Declare Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    Public Declare Function ShowWindow Lib "user32" (ByVal handle As IntPtr, ByVal nCmdShow As Integer) As Integer
    Public Declare Ansi Function GetClassName Lib "user32" Alias "GetClassNameA" (hwnd As IntPtr, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef lpString As String, cch As Integer) As Integer
    Public Declare Auto Function SetForegroundWindow Lib "user32" (hWnd As IntPtr) As Boolean


#End Region

#Region "Remove Game Summary"
    Private Const FileName As String = "\GameSummary.exe"

    Public Function AdBlock() As Boolean
        Return Me.AdBlock(Path.GetTempPath())
    End Function

    Public Function AdBlockRemove() As Boolean
        Return Me.AdBlockRemove(Path.GetTempPath())
    End Function

    Private Function AdBlock(directory As String) As Boolean
        Dim result As Boolean
        Try
            If Me.AdBlockRemove(directory) Then
                Dim fileInfo As FileInfo = New FileInfo(directory & "\GameSummary.exe")
                Dim fileStream As FileStream = New FileStream(fileInfo.ToString(), FileMode.Create, FileAccess.Write)
                fileStream.Close()
                Me.SetFileAccess(fileInfo, AccessControlType.Deny)
                result = True
            Else
                result = False
            End If
        Catch ex_3D As Exception
            result = False
        End Try
        Return result
    End Function

    Private Function AdBlockRemove(directory As String) As Boolean
        Dim result As Boolean
        Try
            Dim fileInfo As FileInfo = New FileInfo(directory & "\GameSummary.exe")
            If fileInfo.Exists Then
                Me.SetFileAccess(fileInfo, AccessControlType.Allow)
                fileInfo.Delete()
            End If
            result = True
        Catch ex_2B As Exception
            result = False
        End Try
        Return result
    End Function

    Private Function SetFileAccess(file As FileInfo, accessControlType As AccessControlType) As Boolean
        Dim result As Boolean
        Try
            Dim accessControl As FileSecurity = file.GetAccessControl()
            accessControl.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, accessControlType))
            file.SetAccessControl(accessControl)
            result = True
        Catch ex_28 As Exception
            result = False
        End Try
        Return result
    End Function
#End Region

#Region "Process Calculation"
    Private Sub TimerTick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim listv As New List(Of ListViewItem)
        For Each proc As Process In Process.GetProcessesByName("Client")
            Dim windClass As String = Strings.Space(256)
            GetClassName(proc.MainWindowHandle, windClass, 256)
            If InStr(windClass, "Mabinogi") Then
                listv.Add(New ListViewItem({proc.Id, proc.MainWindowTitle}))
            End If
        Next

        Try
            If Me.selectedproc > -1 Then
                If Process.GetProcessById(selectedproc).MainWindowTitle = vbNullString Then
                    CL()
                    Enable(False)
                    Me.selectedproc = -1
                End If
            End If
        Catch ex As Exception
            CL()
            Enable(False)
            Me.selectedproc = -1
        End Try

        If listv.Count <> MabiRunCount Then
            ListView1.Items.Clear()
            For Each li As ListViewItem In listv
                ListView1.Items.Add(li)
            Next
        End If
        MabiRunCount = listv.Count
    End Sub

    Private Sub ForceReload()
        Dim listv As New List(Of ListViewItem)
        For Each proc As Process In Process.GetProcessesByName("Client")
            Dim windClass As String = Strings.Space(256)
            GetClassName(proc.MainWindowHandle, windClass, 256)
            If InStr(windClass, "Mabinogi") Then
                listv.Add(New ListViewItem({proc.Id, proc.MainWindowTitle}))
            End If
        Next
        ListView1.Items.Clear()
        For Each li As ListViewItem In listv
            ListView1.Items.Add(li)
        Next
    End Sub
#End Region

#Region "Setting Mabinogi Dir"
    Public Function getReg(ByVal regPath As String, ByVal regKey As String) As String
        Return My.Computer.Registry.GetValue(regPath, regKey, "")
    End Function
    Public Sub setReg(ByVal regPath As String, ByVal regKey As String, ByVal regValue As Object)
        My.Computer.Registry.SetValue(regPath, regKey, regValue)
    End Sub

    Public Function setMabiPath() As Boolean
        If PathDialog.ShowDialog() <> DialogResult.Cancel Then
            Dim path As String = PathDialog.SelectedPath
            If ExistsFile(path & "\client.exe") And ExistsFile(path & "\version.dat") And ExistsFile(path & "\mabinogi.exe") Then
                setReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "", path)
                FSmsg("등록되었습니다. [" & path & "]")
                Me.MABI_PATH = getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "")
                Setting.Mabi = getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "")
                Return True
            Else
                FSmsg("뭔가 이상한 곳을 고르지 않았나요?")
                Return setMabiPath()
            End If
        End If
        Return False
    End Function
    Public Function setMabiTestPath() As Boolean
        If PathDialog.ShowDialog() <> DialogResult.Cancel Then
            Dim path As String = PathDialog.SelectedPath
            If ExistsFile(path & "\client.exe") And ExistsFile(path & "\version.dat") And ExistsFile(path & "\mabinogi_test.exe") Then
                setReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi_test", "", path)
                FSmsg("등록되었습니다. [" & path & "]")
                Me.MABI_TEST_PATH = getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi_test", "")
                Setting.MabiTest = getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi_test", "")
                Return True
            Else
                FSmsg("뭔가 이상한 곳을 고르지 않았나요?")
                Return setMabiPath()
            End If
        End If
        Return False
    End Function
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub NewMabinogi_Click(sender As Object, e As EventArgs) Handles NewMabinogi.Click
        Mabinogi.Start()
    End Sub

    Public Sub CL()
        Me.Selected.Text = "선택된 마비노기가 없습니다."
        Me.Wtitle.Text = ""
        Me.WWidth.Text = ""
        Me.WHeight.Text = ""
        Me.WX.Text = ""
        Me.WY.Text = ""
        Me.CheckBox2.Checked = False
    End Sub

    Public Sub Enable(ByVal bool As Boolean)
        Me.Wtitle.Enabled = bool
        Me.WWidth.Enabled = bool
        Me.WHeight.Enabled = bool
        Me.WX.Enabled = bool
        Me.WY.Enabled = bool
        Me.CheckBox2.Enabled = bool
        Me.SetAccept.Enabled = bool
        Me.WEnable.Enabled = bool
    End Sub
    Private Sub breadcrumb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Refresh()

        Me.ResizeRedraw = True
        Me.Size = New Size(360, 34)
        Me.Timer1.Enabled = True

        Me.MABI_PATH = getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "")
        Me.MABI_TEST_PATH = getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi_test", "")

        If Me.MABI_PATH <> vbNullString Then
            Me.MabiDir.Text = Me.MABI_PATH
        End If

        If Me.MABI_TEST_PATH <> vbNullString Then
            Me.MabiTestPath.Text = Me.MABI_TEST_PATH
        End If

        If File.Exists(Application.StartupPath & "\data\adblock") Then
            Me.CheckBox1.Checked = True
        End If

        CL()
        RL()
        Enable(False)

        For Each proc As Process In Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName)
            Me.myproc = proc
        Next
    End Sub

    Private Sub NewMabinogi_MD(sender As Object, e As EventArgs) Handles NewMabinogi.MouseDown
        Me.NewMabinogi.Image = My.Resources.Resources.Mabinogi_New_h
    End Sub
    Private Sub NewMabinogi_MU(sender As Object, e As EventArgs) Handles NewMabinogi.MouseUp
        Me.NewMabinogi.Image = My.Resources.Resources.Mabinogi_New
    End Sub

    Private Sub disabled(sender As Label, e As EventArgs) Handles SetAccept.EnabledChanged, WEnable.EnabledChanged
        If sender.Enabled Then
            sender.Image = My.Resources.Resources.Mabinogi_119
            sender.BackColor = Color.White
        Else
            sender.Image = My.Resources.Resources.Mabinogi_120
            sender.BackColor = Color.White
        End If
    End Sub

    Private Sub clickedDN(sender As Label, e As EventArgs) Handles SetAccept.MouseDown, WEnable.MouseDown
        sender.Image = My.Resources.Resources.Mabinogi_121
    End Sub
    Private Sub clickedUP(sender As Label, e As EventArgs) Handles SetAccept.MouseUp, WEnable.MouseUp
        sender.Image = My.Resources.Resources.Mabinogi_119
    End Sub

    Private Sub WindowExpend_MD(sender As Object, e As EventArgs) Handles WindowExpend.MouseDown
        If Me.Height = 500 Then
            Me.WindowExpend.Image = My.Resources.Resources.window_small_h
        Else
            Me.WindowExpend.Image = My.Resources.Resources.window_big_h
        End If
    End Sub

    Private Sub WindowExpend_MU(sender As Object, e As EventArgs) Handles WindowExpend.MouseUp
        If Me.Height = 500 Then
            Me.WindowExpend.Image = My.Resources.Resources.window_small
        Else
            Me.WindowExpend.Image = My.Resources.Resources.window_big
        End If
    End Sub

    Private Sub WindowExpend_Click(sender As Object, e As EventArgs) Handles WindowExpend.Click
        If Me.Height = 500 Then
            Me.Size = New Size(360, 34)
            Me.WindowExpend.Image = My.Resources.Resources.window_big
            RL()
        Else
            Me.Size = New Size(360, 500)
            Me.WindowExpend.Image = My.Resources.Resources.window_small
            RL()
        End If

        If Me.Opacity <> 1 Then
            Me.Refresh()
        End If
    End Sub

    Private Sub EXPEND_MD(sender As Object, e As EventArgs) Handles EXPEND.MouseDown
        Me.EXPEND.Image = My.Resources.Resources.mabi_expend_h
    End Sub
    Private Sub EXPEND_MU(sender As Object, e As EventArgs) Handles EXPEND.MouseUp
        Me.EXPEND.Image = My.Resources.Resources.mabi_expend
    End Sub

    Private Sub EXPEND_NB_MD(sender As Object, e As EventArgs) Handles EXPEND_NB.MouseDown
        Me.EXPEND_NB.Image = My.Resources.Resources.mabi_expend_noborder_h
    End Sub
    Private Sub EXPEND_NB_MU(sender As Object, e As EventArgs) Handles EXPEND_NB.MouseUp
        Me.EXPEND_NB.Image = My.Resources.Resources.mabi_expend_noborder
    End Sub

    Private Sub CHECKED_MD(sender As Object, e As EventArgs) Handles CHECKED.MouseDown
        Me.CHECKED.Image = My.Resources.Resources.mabi_Checked_h
    End Sub

    Private Sub CHECKED_MU(sender As Object, e As EventArgs) Handles CHECKED.MouseUp
        Me.CHECKED.Image = My.Resources.Resources.mabi_Checked
    End Sub

    Private Sub CHECKEDNB_MD(sender As Object, e As EventArgs) Handles CHECKEDNB.MouseDown
        Me.CHECKEDNB.Image = My.Resources.Resources.mabi_Checked_nb_h
    End Sub

    Private Sub CHECKEDNB_MU(sender As Object, e As EventArgs) Handles CHECKEDNB.MouseUp
        Me.CHECKEDNB.Image = My.Resources.Resources.mabi_Checked_nb
    End Sub

    Private Sub OpenMabiDir_MD(sender As Object, e As EventArgs) Handles OpenMabiDir.MouseDown
        Me.OpenMabiDir.Image = My.Resources.Resources.openDir_h
    End Sub

    Private Sub OpenMabiDir_MU(sender As Object, e As EventArgs) Handles OpenMabiDir.MouseUp
        Me.OpenMabiDir.Image = My.Resources.Resources.openDir
    End Sub

    Private Sub MabiTestOpenDir_MD(sender As Object, e As EventArgs) Handles MabiTestOpenDir.MouseDown
        Me.MabiTestOpenDir.Image = My.Resources.Resources.openDir_h
    End Sub

    Private Sub MabiTestOpenDir_MU(sender As Object, e As EventArgs) Handles MabiTestOpenDir.MouseUp
        Me.MabiTestOpenDir.Image = My.Resources.Resources.openDir
    End Sub

    Private Sub OpenMabiDir_Click(sender As Object, e As EventArgs) Handles OpenMabiDir.Click
        If setMabiPath() Then
            Me.MabiDir.Text = Me.MABI_PATH
        End If
    End Sub

    Private Sub MabiTestOpenDir_Click(sender As Object, e As EventArgs) Handles MabiTestOpenDir.Click
        If setMabiTestPath() Then
            Me.MabiTestPath.Text = Me.MABI_TEST_PATH
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.Click
        Directory.CreateDirectory(Application.StartupPath & "\data\")
        If Me.CheckBox1.Checked Then
            File.WriteAllBytes(Application.StartupPath & "\data\adblock", {2, 1, 0, 0})
            AdBlock()
        Else
            File.Delete(Application.StartupPath & "\data\adblock")
            AdBlockRemove()
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.MouseClick
        Try
            Enable(True)
            Dim tempItem As ListViewItem = Me.ListView1.Items(Me.ListView1.FocusedItem.Index)
            Dim proc As IntPtr = Process.GetProcessById(CType(Val(tempItem.SubItems(0).Text), Integer)).MainWindowHandle
            Dim wIndex As Integer = GetWindowLong(proc, WindowLongFlags.GWL_STYLE)
            Dim windowInfo As RECT
            Me.selectedItem = tempItem
            Me.selectedproc = CType(Val(tempItem.SubItems(0).Text), Integer)
            GetWindowRect(proc, windowInfo)

            Me.Selected.Text = "PID" & tempItem.SubItems(0).Text & " : " & tempItem.SubItems(1).Text & " 의 창 정보"

            Me.Wtitle.Text = tempItem.SubItems(1).Text
            Me.WWidth.Text = windowInfo.right - windowInfo.left
            Me.WHeight.Text = windowInfo.bottom - windowInfo.top
            Me.WX.Text = windowInfo.left
            Me.WY.Text = windowInfo.top

            If wIndex = 343932928L Then
                Me.CheckBox2.Checked = False
            Else
                Me.CheckBox2.Checked = True
            End If
        Catch ex As Exception
            CL()
            Enable(False)
        End Try
    End Sub

    Private Sub SetAccept_Click(sender As Object, e As EventArgs) Handles SetAccept.Click
        Try
            Dim proc As IntPtr = Process.GetProcessById(CType(Val(Me.selectedproc), Integer)).MainWindowHandle
            ShowWindow(proc, 1)
            SetWindowText(proc, Me.Wtitle.Text)
            If Me.CheckBox2.Checked Then
                SetWindowLong(proc, -16, CType(349044736, IntPtr))
            Else
                Dim value As WindowStyles = CType(276824064L, WindowStyles)
                SetWindowLong(proc, -16, CType((CLng(value)), IntPtr))
            End If
            SetWindowPos(proc, 0, Val(Me.WX.Text), Val(Me.WY.Text), Val(Me.WWidth.Text), Val(Me.WHeight.Text), 0)
            'SetForegroundWindow(proc)
            Me.ForceReload()
        Catch ex As Exception
            Console.WriteLine(ex.GetBaseException.ToString)
            CL()
            Enable(False)
        End Try
    End Sub

    Private Sub WEnable_Click(sender As Object, e As EventArgs) Handles WEnable.Click
        Try
            Dim proc As IntPtr = Process.GetProcessById(CType(Val(Me.selectedproc), Integer)).MainWindowHandle
            ShowWindow(proc, 1)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Full(Optional ByVal bd As Boolean = True)
        If Me.ListView1.Items.Count < 1 Then
            MsgBox("실행된 마비노기가 없습니다.", MsgBoxStyle.Exclamation Or vbMsgBoxSetForeground, "오류")
        Else
            Try
                Dim index As Integer = 1
                For Each li As ListViewItem In Me.ListView1.Items
                    Dim proc As IntPtr = Process.GetProcessById(CType(Val(li.SubItems(0).Text), Integer)).MainWindowHandle
                    ShowWindow(proc, 1)
                    SetWindowText(proc, "마비노기 " & index)
                    If bd Then
                        SetWindowLong(proc, -16, CType(349044736, IntPtr))
                    Else

                        Dim value As WindowStyles = CType(276824064L, WindowStyles)
                        SetWindowLong(proc, -16, CType((CLng(value)), IntPtr))
                    End If
                    SetWindowPos(proc, _
                                 0, _
                                 Screen.PrimaryScreen.WorkingArea.Location.X, _
                                 Screen.PrimaryScreen.WorkingArea.Location.Y, _
                                 Screen.PrimaryScreen.WorkingArea.Width, _
                                 Screen.PrimaryScreen.WorkingArea.Height, _
                                 0)
                    index += 1
                    SetForegroundWindow(proc)
                Next
                Me.ForceReload()
                'SetForegroundWindow(Me.myproc.MainWindowHandle)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Pattern(Optional ByVal bd As Boolean = True)
        If Me.ListView1.Items.Count < 2 Then
            Me.Full(bd)
        Else
            Try
                Dim matrix As Integer() = New Integer(1) {Math.Ceiling(Me.ListView1.Items.Count ^ 0.5), 0}
                matrix(1) = Math.Ceiling(Me.ListView1.Items.Count / matrix(0))
                Dim vactor As Integer() = New Integer(1) {Math.Floor(Screen.PrimaryScreen.WorkingArea.Width / matrix(0)), _
                                                          Math.Floor(Screen.PrimaryScreen.WorkingArea.Height / matrix(1))}
                Dim index As Integer = 0
                For Each li As ListViewItem In Me.ListView1.Items
                    Dim proc As IntPtr = Process.GetProcessById(CType(Val(li.SubItems(0).Text), Integer)).MainWindowHandle
                    Dim coordinate As Integer() = New Integer(1) {index Mod matrix(0), Math.Floor(index / matrix(0))}
                    ShowWindow(proc, 1)
                    SetWindowText(proc, "마비노기 " & (index + 1))
                    If bd Then
                        SetWindowLong(proc, -16, CType(349044736, IntPtr))
                    Else
                        Dim value As WindowStyles = CType(276824064L, WindowStyles)
                        SetWindowLong(proc, -16, CType((CLng(value)), IntPtr))
                    End If
                    SetWindowPos(proc, _
                                0, _
                                Screen.PrimaryScreen.WorkingArea.X + (vactor(0) * coordinate(0)), _
                                Screen.PrimaryScreen.WorkingArea.Y + (vactor(1) * coordinate(1)), _
                                vactor(0), _
                                vactor(1), _
                                0)
                    index += 1
                    SetForegroundWindow(proc)
                Next
                Me.ForceReload()
                'SetForegroundWindow(Me.myproc.MainWindowHandle)
            Catch ex As Exception
                Console.WriteLine(ex.GetBaseException.ToString)
            End Try
        End If
    End Sub

    Private Sub SetForegroundMe()
        SetForegroundWindow(Me.myproc.MainWindowHandle)
    End Sub

    Private Sub EXPEND_Click(sender As Object, e As EventArgs) Handles EXPEND.Click
        Me.Full()
    End Sub

    Private Sub EXPEND_NB_Click(sender As Object, e As EventArgs) Handles EXPEND_NB.Click
        Me.Full(False)
    End Sub

    Private Sub CHECKED_Click(sender As Object, e As EventArgs) Handles CHECKED.Click
        Me.Pattern()
    End Sub

    Private Sub CHECKEDNB_Click(sender As Object, e As EventArgs) Handles CHECKEDNB.Click
        Me.Pattern(False)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FrmMain.TrayIcon.Dispose()
        End
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Me.Opacity = (Me.TrackBar1.Value + 25) / 100
    End Sub

    ' 0=bottom right
    ' 1=bottom left
    ' 2=top right
    ' 3=top left

    Private posmode As Integer = 0
    Private PS As Rectangle = Screen.PrimaryScreen.WorkingArea
    Public Sub RL()
        If posmode = 0 Then
            Me.Location = New Point(PS.Right - Me.Width, PS.Bottom - Me.Height)
        ElseIf posmode = 1 Then
            Me.Location = New Point(PS.Left, PS.Bottom - Me.Height)
        ElseIf posmode = 2 Then
            Me.Location = New Point(PS.Right - Me.Width, PS.Top)
        ElseIf posmode = 3 Then
            Me.Location = New Point(PS.Left, PS.Top)
        Else
            Me.Location = New Point(PS.Right - Me.Width, PS.Bottom - Me.Height)
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.posmode = 3
        Me.Location = New Point(PS.Left, PS.Top)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.posmode = 2
        Me.Location = New Point(PS.Right - Me.Width, PS.Top)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.posmode = 1
        Me.Location = New Point(PS.Left, PS.Bottom - Me.Height)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.posmode = 0
        Me.Location = New Point(PS.Right - Me.Width, PS.Bottom - Me.Height)
    End Sub
End Class