Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Net.Sockets

Public Class vxFrmMain
    Inherits System.Windows.Forms.Form
    Implements IComparer

#Region " StartCodes "
    Private col As Integer
    Public sort As String = "ASC"

    Private Const URL_MESSAGE As String = "Enter URL of file here"
    Private Const DIR_MESSAGE As String = "Enter directory to download to here"
    'DECLARE THIS WITHEVENTS SO WE GET EVENTS ABOUT DOWNLOAD PROGRESS
    Private WithEvents _Downloader As vxWebFileDownloader
    Private vList As List(Of String) = New List(Of String)

    Private Delegate Function EnumCallBackDelegate(hwnd As IntPtr, lparam As IntPtr) As Boolean

    Private Declare Function GetDesktopWindow Lib "user32.dll" () As Long

    Private Declare Function GetWindowRect Lib "user32.dll" (ByVal hwnd As Long, ByRef lpRect As RECT) As Long

    Private Declare Ansi Function FindWindow Lib "user32" Alias "FindWindowA" (<MarshalAs(UnmanagedType.VBByRefStr)> ByRef lpClassName As String, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef lpWindowName As String) As Integer

    Private Declare Ansi Function GetWindowThreadProcessId Lib "user32" (hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer

    Private Declare Ansi Function GetWindowRect Lib "user32" (hwnd As Integer, ByRef lpRect As RECT) As Integer

    Private Declare Ansi Function SetWindowPos Lib "user32" (hWnd As IntPtr, hWndInsertAfter As IntPtr, X As Integer, Y As Integer, cx As Integer, cy As Integer, uFlags As Integer) As Boolean

    Private Declare Ansi Function SetWindowText Lib "user32" Alias "SetWindowTextA" (hwnd As IntPtr, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef windowName As String) As Boolean

    Private Declare Ansi Function EnumWindows Lib "user32" (lpEnumFunc As EnumCallBackDelegate, lParam As Integer) As Integer

    Private Declare Ansi Function GetWindowText Lib "user32" Alias "GetWindowTextA" (hwnd As Integer, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef lpString As String, cch As Integer) As Integer

    Private Declare Ansi Function GetClassName Lib "user32" Alias "GetClassNameA" (hwnd As Integer, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef lpString As String, cch As Integer) As Integer

    Private Declare Ansi Function SetForegroundWindow Lib "user32" (hWnd As IntPtr) As Boolean

    Private Declare Ansi Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (hWnd As IntPtr, nIndex As Integer) As Integer

    Private Declare Ansi Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (hWnd As IntPtr, nIndex As Integer, dwNewLong As IntPtr) As Integer

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

    Private Structure RECT
        Public Lft As Integer

        Public Top As Integer

        Public Rgt As Integer

        Public Btm As Integer
    End Structure
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

            If My.Computer.FileSystem.FileExists(patchfile) = True Then

                Dim Temp As String = vxFS.Read_File(patchfile)

                For Each vLine As String In Split(Temp, vbNewLine)

                    If InStr(vLine, "patch_accept=") > 0 Then

                        patch_accept = Replace(vLine, "patch_accept=", "")

                    ElseIf InStr(vLine, "local_version=") > 0 Then

                        local_version = Replace(vLine, "local_version=", "")

                        If vxMabiCore.State_ClientType = "Mabinogi_test" Then

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

                Return True

            Else

                Return False

            End If

        End Function


    End Structure
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents dlgFolderBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents type_spc As System.Windows.Forms.Label
    Friend WithEvents type_mgm As System.Windows.Forms.Label
    Friend WithEvents type_chg As System.Windows.Forms.Label
    Friend WithEvents Progress As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents listreflash As System.Windows.Forms.Label
    Friend WithEvents deletebtn As System.Windows.Forms.Label
    Friend WithEvents downloadbtn As System.Windows.Forms.Label
    Friend WithEvents updatecheck As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabBtnPackage As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MabiInstallDir As System.Windows.Forms.Label
    Friend WithEvents CustomProgressBar_case1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents CustomProgressBar_case2 As System.Windows.Forms.Panel
    Friend WithEvents CProgress1 As System.Windows.Forms.Panel
    Friend WithEvents CProgress2 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents TabPagePackage As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabPageMabiNotice As System.Windows.Forms.Panel
    Friend WithEvents TabPageProgInfo As System.Windows.Forms.Panel
    Friend WithEvents TabPage04 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents closebtn As System.Windows.Forms.PictureBox
    Friend WithEvents PackInfo As System.Windows.Forms.WebBrowser
    Friend WithEvents MabiNotice As System.Windows.Forms.WebBrowser
    Friend WithEvents ProgramInfo As System.Windows.Forms.WebBrowser
    Friend WithEvents mabistartbtn As System.Windows.Forms.Label
    Friend WithEvents WLabLite As System.Windows.Forms.WebBrowser
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents wlabPW As System.Windows.Forms.TextBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents wlabID As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TmpList As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents UpdateTemp As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TmpTab As System.Windows.Forms.Label
    Friend WithEvents TabList01 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabList02 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabList03 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabBtnSetting As System.Windows.Forms.Label
    Friend WithEvents TabPageMain As System.Windows.Forms.Panel
    Friend WithEvents TabBtnLog As System.Windows.Forms.Label
    Friend WithEvents TabBtnMain As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Prog02 As System.Windows.Forms.Label
    Friend WithEvents WebBrowser2 As System.Windows.Forms.WebBrowser
    Friend WithEvents TabPageSetting As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents AutoInstrumentPlay As System.Windows.Forms.CheckBox
    Friend WithEvents IsSmartWeaponSetEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents IsEscWindowCloseEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents IsEscCancelEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents ShowDownGauge As System.Windows.Forms.CheckBox
    Friend WithEvents TargetPreparedAttack As System.Windows.Forms.CheckBox
    Friend WithEvents IsKeepCamera As System.Windows.Forms.CheckBox
    Friend WithEvents AutoMouseNearTargeting As System.Windows.Forms.CheckBox
    Friend WithEvents IsBagHotKeyEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TargetingMode_0 As System.Windows.Forms.RadioButton
    Friend WithEvents TargetingMode_1 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents IsAutoBattle As System.Windows.Forms.CheckBox
    Friend WithEvents ReserveAttack_2 As System.Windows.Forms.RadioButton
    Friend WithEvents ReserveAttack_1 As System.Windows.Forms.RadioButton
    Friend WithEvents ReserveAttack_0 As System.Windows.Forms.RadioButton
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents ShowPilotingManual As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox11 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents CheckBox13 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox14 As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents CheckBox15 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox16 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents TabPageLaboratory As System.Windows.Forms.Panel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents dellumiarte As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(vxFrmMain))
        Me.dlgFolderBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabBtnPackage = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPagePackage = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Clbtn2 = New System.Windows.Forms.PictureBox()
        Me.LoginFormTitle = New System.Windows.Forms.Label()
        Me.loginbtn = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.UpdateTemp = New System.Windows.Forms.ListView()
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TmpList = New System.Windows.Forms.ListView()
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.dellumiarte = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.updatecheck = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.listreflash = New System.Windows.Forms.Label()
        Me.deletebtn = New System.Windows.Forms.Label()
        Me.downloadbtn = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PackInfo = New System.Windows.Forms.WebBrowser()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabList01 = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TabList02 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabList03 = New System.Windows.Forms.ListView()
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.type_spc = New System.Windows.Forms.Label()
        Me.type_mgm = New System.Windows.Forms.Label()
        Me.type_chg = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MabiInstallDir = New System.Windows.Forms.Label()
        Me.CustomProgressBar_case1 = New System.Windows.Forms.Panel()
        Me.CProgress1 = New System.Windows.Forms.Panel()
        Me.CustomProgressBar_case2 = New System.Windows.Forms.Panel()
        Me.CProgress2 = New System.Windows.Forms.Panel()
        Me.Progress = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.closebtn = New System.Windows.Forms.PictureBox()
        Me.TabPageMabiNotice = New System.Windows.Forms.Panel()
        Me.MabiNotice = New System.Windows.Forms.WebBrowser()
        Me.TabPageProgInfo = New System.Windows.Forms.Panel()
        Me.ProgramInfo = New System.Windows.Forms.WebBrowser()
        Me.TabPage04 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.WLabLite = New System.Windows.Forms.WebBrowser()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.wlabPW = New System.Windows.Forms.TextBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.wlabID = New System.Windows.Forms.TextBox()
        Me.mabistartbtn = New System.Windows.Forms.Label()
        Me.TmpTab = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader16 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader18 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.TabPageLaboratory = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabBtnSetting = New System.Windows.Forms.Label()
        Me.TabPageMain = New System.Windows.Forms.Panel()
        Me.WebBrowser2 = New System.Windows.Forms.WebBrowser()
        Me.TabBtnLog = New System.Windows.Forms.Label()
        Me.TabBtnMain = New System.Windows.Forms.Label()
        Me.Prog02 = New System.Windows.Forms.Label()
        Me.TabPageSetting = New System.Windows.Forms.Panel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel25 = New System.Windows.Forms.Panel()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.CheckBox15 = New System.Windows.Forms.CheckBox()
        Me.CheckBox16 = New System.Windows.Forms.CheckBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.CheckBox13 = New System.Windows.Forms.CheckBox()
        Me.CheckBox14 = New System.Windows.Forms.CheckBox()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.ShowPilotingManual = New System.Windows.Forms.CheckBox()
        Me.CheckBox11 = New System.Windows.Forms.CheckBox()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.IsAutoBattle = New System.Windows.Forms.CheckBox()
        Me.ReserveAttack_2 = New System.Windows.Forms.RadioButton()
        Me.ReserveAttack_1 = New System.Windows.Forms.RadioButton()
        Me.ReserveAttack_0 = New System.Windows.Forms.RadioButton()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.ShowDownGauge = New System.Windows.Forms.CheckBox()
        Me.TargetPreparedAttack = New System.Windows.Forms.CheckBox()
        Me.AutoInstrumentPlay = New System.Windows.Forms.CheckBox()
        Me.IsKeepCamera = New System.Windows.Forms.CheckBox()
        Me.IsSmartWeaponSetEnabled = New System.Windows.Forms.CheckBox()
        Me.AutoMouseNearTargeting = New System.Windows.Forms.CheckBox()
        Me.IsBagHotKeyEnabled = New System.Windows.Forms.CheckBox()
        Me.IsEscWindowCloseEnabled = New System.Windows.Forms.CheckBox()
        Me.IsEscCancelEnabled = New System.Windows.Forms.CheckBox()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.TargetingMode_1 = New System.Windows.Forms.RadioButton()
        Me.TargetingMode_0 = New System.Windows.Forms.RadioButton()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.요리도우미ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.창활성화ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.종료ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.요리도우미ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.마비노기실행ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPagePackage.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.Clbtn2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.CustomProgressBar_case1.SuspendLayout()
        Me.CustomProgressBar_case2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.closebtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageMabiNotice.SuspendLayout()
        Me.TabPageProgInfo.SuspendLayout()
        Me.TabPage04.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.TabPageLaboratory.SuspendLayout()
        Me.TabPageMain.SuspendLayout()
        Me.TabPageSetting.SuspendLayout()
        Me.Panel25.SuspendLayout()
        Me.Panel26.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel23.SuspendLayout()
        Me.Panel24.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.DarkGray
        Me.Label2.Location = New System.Drawing.Point(50, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(822, 25)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Willow's Package Professional 2014"
        '
        'TabBtnPackage
        '
        Me.TabBtnPackage.AutoSize = True
        Me.TabBtnPackage.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.TabBtnPackage.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.TabBtnPackage.ForeColor = System.Drawing.Color.White
        Me.TabBtnPackage.Location = New System.Drawing.Point(40, 36)
        Me.TabBtnPackage.Margin = New System.Windows.Forms.Padding(0)
        Me.TabBtnPackage.Name = "TabBtnPackage"
        Me.TabBtnPackage.Padding = New System.Windows.Forms.Padding(2)
        Me.TabBtnPackage.Size = New System.Drawing.Size(75, 19)
        Me.TabBtnPackage.TabIndex = 18
        Me.TabBtnPackage.Text = "패키지 설치"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(197, 36)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(2)
        Me.Label4.Size = New System.Drawing.Size(87, 19)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "마비노기 공지"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(284, 36)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(2)
        Me.Label5.Size = New System.Drawing.Size(87, 19)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "프로그램 정보"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(371, 36)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(2)
        Me.Label6.Size = New System.Drawing.Size(68, 19)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "버들웹LITE"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(5, 55)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(930, 3)
        Me.Panel3.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.DarkGray
        Me.Label1.Location = New System.Drawing.Point(6, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 15)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "마비노기"
        '
        'TabPagePackage
        '
        Me.TabPagePackage.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TabPagePackage.Controls.Add(Me.Panel6)
        Me.TabPagePackage.Controls.Add(Me.UpdateTemp)
        Me.TabPagePackage.Controls.Add(Me.TmpList)
        Me.TabPagePackage.Controls.Add(Me.dellumiarte)
        Me.TabPagePackage.Controls.Add(Me.Label14)
        Me.TabPagePackage.Controls.Add(Me.updatecheck)
        Me.TabPagePackage.Controls.Add(Me.Label32)
        Me.TabPagePackage.Controls.Add(Me.listreflash)
        Me.TabPagePackage.Controls.Add(Me.deletebtn)
        Me.TabPagePackage.Controls.Add(Me.downloadbtn)
        Me.TabPagePackage.Controls.Add(Me.Panel4)
        Me.TabPagePackage.Controls.Add(Me.Panel2)
        Me.TabPagePackage.Controls.Add(Me.type_spc)
        Me.TabPagePackage.Controls.Add(Me.type_mgm)
        Me.TabPagePackage.Controls.Add(Me.type_chg)
        Me.TabPagePackage.Controls.Add(Me.Panel1)
        Me.TabPagePackage.Location = New System.Drawing.Point(5, 58)
        Me.TabPagePackage.Name = "TabPagePackage"
        Me.TabPagePackage.Size = New System.Drawing.Size(930, 505)
        Me.TabPagePackage.TabIndex = 24
        Me.TabPagePackage.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.Clbtn2)
        Me.Panel6.Controls.Add(Me.LoginFormTitle)
        Me.Panel6.Controls.Add(Me.loginbtn)
        Me.Panel6.Controls.Add(Me.Label33)
        Me.Panel6.Controls.Add(Me.TextBox2)
        Me.Panel6.Controls.Add(Me.RichTextBox1)
        Me.Panel6.Location = New System.Drawing.Point(335, 170)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(260, 173)
        Me.Panel6.TabIndex = 36
        Me.Panel6.Visible = False
        '
        'Clbtn2
        '
        Me.Clbtn2.BackgroundImage = CType(resources.GetObject("Clbtn2.BackgroundImage"), System.Drawing.Image)
        Me.Clbtn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Clbtn2.Location = New System.Drawing.Point(225, 0)
        Me.Clbtn2.Margin = New System.Windows.Forms.Padding(0)
        Me.Clbtn2.Name = "Clbtn2"
        Me.Clbtn2.Size = New System.Drawing.Size(34, 26)
        Me.Clbtn2.TabIndex = 29
        Me.Clbtn2.TabStop = False
        '
        'LoginFormTitle
        '
        Me.LoginFormTitle.AutoSize = True
        Me.LoginFormTitle.Location = New System.Drawing.Point(8, 8)
        Me.LoginFormTitle.Name = "LoginFormTitle"
        Me.LoginFormTitle.Size = New System.Drawing.Size(169, 20)
        Me.LoginFormTitle.TabIndex = 4
        Me.LoginFormTitle.Text = "버들웹 계정으로 로그인"
        '
        'loginbtn
        '
        Me.loginbtn.AutoSize = True
        Me.loginbtn.Location = New System.Drawing.Point(176, 107)
        Me.loginbtn.MinimumSize = New System.Drawing.Size(70, 50)
        Me.loginbtn.Name = "loginbtn"
        Me.loginbtn.Size = New System.Drawing.Size(70, 50)
        Me.loginbtn.TabIndex = 3
        Me.loginbtn.Text = "로그인"
        Me.loginbtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label33.Location = New System.Drawing.Point(12, 99)
        Me.Label33.MaximumSize = New System.Drawing.Size(157, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(155, 60)
        Me.Label33.TabIndex = 2
        Me.Label33.Text = "개인 패키지(준비중), 기부자 등의 시스템을 이용할 수 있는 기능입니다. 버들웹 이용약관을 따릅니다."
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Location = New System.Drawing.Point(11, 68)
        Me.TextBox2.MaximumSize = New System.Drawing.Size(237, 24)
        Me.TextBox2.MinimumSize = New System.Drawing.Size(237, 24)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(237, 24)
        Me.TextBox2.TabIndex = 1
        Me.TextBox2.UseSystemPasswordChar = True
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Location = New System.Drawing.Point(11, 38)
        Me.RichTextBox1.MaxLength = 20
        Me.RichTextBox1.Multiline = False
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(237, 24)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'UpdateTemp
        '
        Me.UpdateTemp.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.UpdateTemp.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.UpdateTemp.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader13, Me.ColumnHeader14, Me.ColumnHeader15})
        Me.UpdateTemp.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.UpdateTemp.Location = New System.Drawing.Point(972, 390)
        Me.UpdateTemp.Name = "UpdateTemp"
        Me.UpdateTemp.Size = New System.Drawing.Size(259, 174)
        Me.UpdateTemp.TabIndex = 35
        Me.UpdateTemp.UseCompatibleStateImageBehavior = False
        Me.UpdateTemp.View = System.Windows.Forms.View.Details
        Me.UpdateTemp.Visible = False
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "패키지명"
        Me.ColumnHeader13.Width = 282
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "갱신일"
        Me.ColumnHeader14.Width = 95
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "상태"
        Me.ColumnHeader15.Width = 95
        '
        'TmpList
        '
        Me.TmpList.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TmpList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TmpList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader12})
        Me.TmpList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.TmpList.Location = New System.Drawing.Point(1142, 445)
        Me.TmpList.Name = "TmpList"
        Me.TmpList.Size = New System.Drawing.Size(142, 260)
        Me.TmpList.TabIndex = 31
        Me.TmpList.UseCompatibleStateImageBehavior = False
        Me.TmpList.View = System.Windows.Forms.View.Details
        Me.TmpList.Visible = False
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "패키지명"
        Me.ColumnHeader10.Width = 282
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "갱신일"
        Me.ColumnHeader11.Width = 95
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "상태"
        Me.ColumnHeader12.Width = 95
        '
        'dellumiarte
        '
        Me.dellumiarte.AutoSize = True
        Me.dellumiarte.BackColor = System.Drawing.Color.Transparent
        Me.dellumiarte.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.dellumiarte.Location = New System.Drawing.Point(806, 459)
        Me.dellumiarte.Margin = New System.Windows.Forms.Padding(0)
        Me.dellumiarte.MinimumSize = New System.Drawing.Size(116, 40)
        Me.dellumiarte.Name = "dellumiarte"
        Me.dellumiarte.Size = New System.Drawing.Size(116, 40)
        Me.dellumiarte.TabIndex = 34
        Me.dellumiarte.Text = "루미팩 삭제"
        Me.dellumiarte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.dellumiarte.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.Label14.Location = New System.Drawing.Point(806, 415)
        Me.Label14.Margin = New System.Windows.Forms.Padding(0)
        Me.Label14.MinimumSize = New System.Drawing.Size(116, 40)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(116, 40)
        Me.Label14.TabIndex = 34
        Me.Label14.Text = "버전 변경"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'updatecheck
        '
        Me.updatecheck.AutoSize = True
        Me.updatecheck.BackColor = System.Drawing.Color.Transparent
        Me.updatecheck.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.updatecheck.Location = New System.Drawing.Point(806, 369)
        Me.updatecheck.Margin = New System.Windows.Forms.Padding(0)
        Me.updatecheck.MinimumSize = New System.Drawing.Size(116, 40)
        Me.updatecheck.Name = "updatecheck"
        Me.updatecheck.Size = New System.Drawing.Size(116, 40)
        Me.updatecheck.TabIndex = 34
        Me.updatecheck.Text = "업데이트 확인"
        Me.updatecheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.Label32.Location = New System.Drawing.Point(806, 193)
        Me.Label32.Margin = New System.Windows.Forms.Padding(0)
        Me.Label32.MinimumSize = New System.Drawing.Size(116, 40)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(116, 40)
        Me.Label32.TabIndex = 34
        Me.Label32.Text = "버들웹 로그인"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'listreflash
        '
        Me.listreflash.AutoSize = True
        Me.listreflash.BackColor = System.Drawing.Color.Transparent
        Me.listreflash.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.listreflash.Location = New System.Drawing.Point(806, 147)
        Me.listreflash.Margin = New System.Windows.Forms.Padding(0)
        Me.listreflash.MinimumSize = New System.Drawing.Size(116, 40)
        Me.listreflash.Name = "listreflash"
        Me.listreflash.Size = New System.Drawing.Size(116, 40)
        Me.listreflash.TabIndex = 34
        Me.listreflash.Text = "목록 갱신"
        Me.listreflash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'deletebtn
        '
        Me.deletebtn.AutoSize = True
        Me.deletebtn.BackColor = System.Drawing.Color.Transparent
        Me.deletebtn.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.deletebtn.Location = New System.Drawing.Point(806, 103)
        Me.deletebtn.Margin = New System.Windows.Forms.Padding(0)
        Me.deletebtn.MinimumSize = New System.Drawing.Size(116, 40)
        Me.deletebtn.Name = "deletebtn"
        Me.deletebtn.Size = New System.Drawing.Size(116, 40)
        Me.deletebtn.TabIndex = 34
        Me.deletebtn.Text = "선택 삭제"
        Me.deletebtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'downloadbtn
        '
        Me.downloadbtn.AutoSize = True
        Me.downloadbtn.BackColor = System.Drawing.Color.Transparent
        Me.downloadbtn.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.downloadbtn.Location = New System.Drawing.Point(806, 59)
        Me.downloadbtn.Margin = New System.Windows.Forms.Padding(0)
        Me.downloadbtn.MinimumSize = New System.Drawing.Size(116, 40)
        Me.downloadbtn.Name = "downloadbtn"
        Me.downloadbtn.Size = New System.Drawing.Size(116, 40)
        Me.downloadbtn.TabIndex = 34
        Me.downloadbtn.Text = "선택 다운로드"
        Me.downloadbtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.PackInfo)
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Location = New System.Drawing.Point(507, 59)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(290, 442)
        Me.Panel4.TabIndex = 33
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label11.ForeColor = System.Drawing.Color.LightGray
        Me.Label11.Location = New System.Drawing.Point(1, 1)
        Me.Label11.MinimumSize = New System.Drawing.Size(288, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.Label11.Size = New System.Drawing.Size(288, 21)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "상세 설명"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PackInfo
        '
        Me.PackInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PackInfo.Location = New System.Drawing.Point(1, 23)
        Me.PackInfo.MinimumSize = New System.Drawing.Size(20, 20)
        Me.PackInfo.Name = "PackInfo"
        Me.PackInfo.ScrollBarsEnabled = False
        Me.PackInfo.Size = New System.Drawing.Size(288, 418)
        Me.PackInfo.TabIndex = 32
        Me.PackInfo.Url = New System.Uri("http://willowslab.com/willowspack/packinfolist.php?file=", System.UriKind.Absolute)
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel5.Location = New System.Drawing.Point(1, 23)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(288, 418)
        Me.Panel5.TabIndex = 34
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel2.Controls.Add(Me.TabList01)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.TabList02)
        Me.Panel2.Controls.Add(Me.TabList03)
        Me.Panel2.Location = New System.Drawing.Point(4, 59)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(500, 442)
        Me.Panel2.TabIndex = 31
        '
        'TabList01
        '
        Me.TabList01.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TabList01.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TabList01.CheckBoxes = True
        Me.TabList01.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6})
        Me.TabList01.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.TabList01.ForeColor = System.Drawing.Color.LightGray
        Me.TabList01.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.TabList01.Location = New System.Drawing.Point(1, 23)
        Me.TabList01.Name = "TabList01"
        Me.TabList01.Size = New System.Drawing.Size(498, 418)
        Me.TabList01.TabIndex = 34
        Me.TabList01.UseCompatibleStateImageBehavior = False
        Me.TabList01.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "패키지명"
        Me.ColumnHeader4.Width = 282
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "갱신일"
        Me.ColumnHeader5.Width = 95
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "상태"
        Me.ColumnHeader6.Width = 95
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label7.ForeColor = System.Drawing.Color.LightGray
        Me.Label7.Location = New System.Drawing.Point(1, 1)
        Me.Label7.MinimumSize = New System.Drawing.Size(281, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.Label7.Size = New System.Drawing.Size(281, 21)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "패키지 이름"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label10.ForeColor = System.Drawing.Color.LightGray
        Me.Label10.Location = New System.Drawing.Point(473, 1)
        Me.Label10.MinimumSize = New System.Drawing.Size(26, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.Label10.Size = New System.Drawing.Size(26, 21)
        Me.Label10.TabIndex = 30
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label8.ForeColor = System.Drawing.Color.LightGray
        Me.Label8.Location = New System.Drawing.Point(283, 1)
        Me.Label8.MinimumSize = New System.Drawing.Size(94, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.Label8.Size = New System.Drawing.Size(94, 21)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "갱신일"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label9.ForeColor = System.Drawing.Color.LightGray
        Me.Label9.Location = New System.Drawing.Point(378, 1)
        Me.Label9.MinimumSize = New System.Drawing.Size(94, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.Label9.Size = New System.Drawing.Size(94, 21)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "상태"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabList02
        '
        Me.TabList02.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TabList02.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TabList02.CheckBoxes = True
        Me.TabList02.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.TabList02.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.TabList02.ForeColor = System.Drawing.Color.LightGray
        Me.TabList02.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.TabList02.Location = New System.Drawing.Point(1, 23)
        Me.TabList02.Name = "TabList02"
        Me.TabList02.Size = New System.Drawing.Size(498, 418)
        Me.TabList02.TabIndex = 35
        Me.TabList02.UseCompatibleStateImageBehavior = False
        Me.TabList02.View = System.Windows.Forms.View.Details
        Me.TabList02.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "패키지명"
        Me.ColumnHeader1.Width = 282
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "갱신일"
        Me.ColumnHeader2.Width = 95
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "상태"
        Me.ColumnHeader3.Width = 95
        '
        'TabList03
        '
        Me.TabList03.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TabList03.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TabList03.CheckBoxes = True
        Me.TabList03.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9})
        Me.TabList03.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.TabList03.ForeColor = System.Drawing.Color.LightGray
        Me.TabList03.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.TabList03.Location = New System.Drawing.Point(1, 23)
        Me.TabList03.Name = "TabList03"
        Me.TabList03.Size = New System.Drawing.Size(498, 418)
        Me.TabList03.TabIndex = 36
        Me.TabList03.UseCompatibleStateImageBehavior = False
        Me.TabList03.View = System.Windows.Forms.View.Details
        Me.TabList03.Visible = False
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "패키지명"
        Me.ColumnHeader7.Width = 282
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "갱신일"
        Me.ColumnHeader8.Width = 95
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "상태"
        Me.ColumnHeader9.Width = 95
        '
        'type_spc
        '
        Me.type_spc.AutoSize = True
        Me.type_spc.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.type_spc.ForeColor = System.Drawing.Color.Gainsboro
        Me.type_spc.Location = New System.Drawing.Point(102, 39)
        Me.type_spc.Name = "type_spc"
        Me.type_spc.Padding = New System.Windows.Forms.Padding(3)
        Me.type_spc.Size = New System.Drawing.Size(73, 21)
        Me.type_spc.TabIndex = 29
        Me.type_spc.Text = "선택설치형"
        '
        'type_mgm
        '
        Me.type_mgm.AutoSize = True
        Me.type_mgm.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.type_mgm.ForeColor = System.Drawing.Color.Gainsboro
        Me.type_mgm.Location = New System.Drawing.Point(53, 39)
        Me.type_mgm.Name = "type_mgm"
        Me.type_mgm.Padding = New System.Windows.Forms.Padding(3)
        Me.type_mgm.Size = New System.Drawing.Size(49, 21)
        Me.type_mgm.TabIndex = 28
        Me.type_mgm.Text = "관리형"
        '
        'type_chg
        '
        Me.type_chg.AutoSize = True
        Me.type_chg.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.type_chg.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.type_chg.ForeColor = System.Drawing.Color.Gainsboro
        Me.type_chg.Location = New System.Drawing.Point(4, 39)
        Me.type_chg.Name = "type_chg"
        Me.type_chg.Padding = New System.Windows.Forms.Padding(3)
        Me.type_chg.Size = New System.Drawing.Size(49, 21)
        Me.type_chg.TabIndex = 27
        Me.type_chg.Text = "교체형"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.MabiInstallDir)
        Me.Panel1.Location = New System.Drawing.Point(4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(922, 30)
        Me.Panel1.TabIndex = 25
        '
        'MabiInstallDir
        '
        Me.MabiInstallDir.AutoSize = True
        Me.MabiInstallDir.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.MabiInstallDir.ForeColor = System.Drawing.Color.DarkGray
        Me.MabiInstallDir.Location = New System.Drawing.Point(67, 8)
        Me.MabiInstallDir.Name = "MabiInstallDir"
        Me.MabiInstallDir.Size = New System.Drawing.Size(146, 15)
        Me.MabiInstallDir.TabIndex = 24
        Me.MabiInstallDir.Text = "Mabinogi Install Directory"
        Me.MabiInstallDir.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'CustomProgressBar_case1
        '
        Me.CustomProgressBar_case1.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(77, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.CustomProgressBar_case1.Controls.Add(Me.CProgress1)
        Me.CustomProgressBar_case1.Location = New System.Drawing.Point(6, 595)
        Me.CustomProgressBar_case1.Name = "CustomProgressBar_case1"
        Me.CustomProgressBar_case1.Size = New System.Drawing.Size(780, 10)
        Me.CustomProgressBar_case1.TabIndex = 25
        '
        'CProgress1
        '
        Me.CProgress1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CProgress1.Location = New System.Drawing.Point(0, 0)
        Me.CProgress1.Name = "CProgress1"
        Me.CProgress1.Size = New System.Drawing.Size(390, 20)
        Me.CProgress1.TabIndex = 0
        '
        'CustomProgressBar_case2
        '
        Me.CustomProgressBar_case2.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(77, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.CustomProgressBar_case2.Controls.Add(Me.CProgress2)
        Me.CustomProgressBar_case2.Location = New System.Drawing.Point(6, 632)
        Me.CustomProgressBar_case2.Name = "CustomProgressBar_case2"
        Me.CustomProgressBar_case2.Size = New System.Drawing.Size(780, 10)
        Me.CustomProgressBar_case2.TabIndex = 25
        '
        'CProgress2
        '
        Me.CProgress2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CProgress2.Location = New System.Drawing.Point(0, 0)
        Me.CProgress2.Name = "CProgress2"
        Me.CProgress2.Size = New System.Drawing.Size(390, 20)
        Me.CProgress2.TabIndex = 0
        '
        'Progress
        '
        Me.Progress.AutoSize = True
        Me.Progress.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Progress.Location = New System.Drawing.Point(9, 611)
        Me.Progress.Margin = New System.Windows.Forms.Padding(0)
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(59, 15)
        Me.Progress.TabIndex = 26
        Me.Progress.Text = "#Progress"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Location = New System.Drawing.Point(5, -4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(45, 30)
        Me.PictureBox1.TabIndex = 27
        Me.PictureBox1.TabStop = False
        '
        'closebtn
        '
        Me.closebtn.BackgroundImage = CType(resources.GetObject("closebtn.BackgroundImage"), System.Drawing.Image)
        Me.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.closebtn.Location = New System.Drawing.Point(907, -1)
        Me.closebtn.Margin = New System.Windows.Forms.Padding(0)
        Me.closebtn.Name = "closebtn"
        Me.closebtn.Size = New System.Drawing.Size(34, 26)
        Me.closebtn.TabIndex = 28
        Me.closebtn.TabStop = False
        '
        'TabPageMabiNotice
        '
        Me.TabPageMabiNotice.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TabPageMabiNotice.Controls.Add(Me.MabiNotice)
        Me.TabPageMabiNotice.Location = New System.Drawing.Point(5, 58)
        Me.TabPageMabiNotice.Name = "TabPageMabiNotice"
        Me.TabPageMabiNotice.Size = New System.Drawing.Size(930, 505)
        Me.TabPageMabiNotice.TabIndex = 29
        '
        'MabiNotice
        '
        Me.MabiNotice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MabiNotice.Location = New System.Drawing.Point(0, 0)
        Me.MabiNotice.MinimumSize = New System.Drawing.Size(20, 20)
        Me.MabiNotice.Name = "MabiNotice"
        Me.MabiNotice.ScrollBarsEnabled = False
        Me.MabiNotice.Size = New System.Drawing.Size(930, 505)
        Me.MabiNotice.TabIndex = 0
        Me.MabiNotice.Url = New System.Uri("http://willowspack.srin.kr/notice_g2.php", System.UriKind.Absolute)
        '
        'TabPageProgInfo
        '
        Me.TabPageProgInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TabPageProgInfo.Controls.Add(Me.ProgramInfo)
        Me.TabPageProgInfo.Location = New System.Drawing.Point(5, 58)
        Me.TabPageProgInfo.Name = "TabPageProgInfo"
        Me.TabPageProgInfo.Size = New System.Drawing.Size(930, 505)
        Me.TabPageProgInfo.TabIndex = 30
        Me.TabPageProgInfo.Visible = False
        '
        'ProgramInfo
        '
        Me.ProgramInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProgramInfo.Location = New System.Drawing.Point(0, 0)
        Me.ProgramInfo.MinimumSize = New System.Drawing.Size(20, 20)
        Me.ProgramInfo.Name = "ProgramInfo"
        Me.ProgramInfo.ScrollBarsEnabled = False
        Me.ProgramInfo.Size = New System.Drawing.Size(930, 505)
        Me.ProgramInfo.TabIndex = 0
        Me.ProgramInfo.Url = New System.Uri("http://willowspack.srin.kr/C6/info.php", System.UriKind.Absolute)
        '
        'TabPage04
        '
        Me.TabPage04.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TabPage04.Controls.Add(Me.Label13)
        Me.TabPage04.Controls.Add(Me.WLabLite)
        Me.TabPage04.Controls.Add(Me.Label12)
        Me.TabPage04.Controls.Add(Me.Panel8)
        Me.TabPage04.Controls.Add(Me.Panel7)
        Me.TabPage04.Location = New System.Drawing.Point(5, 58)
        Me.TabPage04.Name = "TabPage04"
        Me.TabPage04.Size = New System.Drawing.Size(930, 505)
        Me.TabPage04.TabIndex = 31
        Me.TabPage04.Visible = False
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.DimGray
        Me.Label13.Location = New System.Drawing.Point(457, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(467, 20)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "이 기능은 버들웹 이용약관에 동의한 상태에서 이용하시기 바랍니다."
        '
        'WLabLite
        '
        Me.WLabLite.Location = New System.Drawing.Point(0, 33)
        Me.WLabLite.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WLabLite.Name = "WLabLite"
        Me.WLabLite.ScrollBarsEnabled = False
        Me.WLabLite.Size = New System.Drawing.Size(930, 472)
        Me.WLabLite.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(321, 5)
        Me.Label12.MinimumSize = New System.Drawing.Size(80, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 22)
        Me.Label12.TabIndex = 101
        Me.Label12.Text = "Connect"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Panel8.Controls.Add(Me.wlabPW)
        Me.Panel8.Location = New System.Drawing.Point(163, 5)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel8.Size = New System.Drawing.Size(152, 22)
        Me.Panel8.TabIndex = 1
        '
        'wlabPW
        '
        Me.wlabPW.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.wlabPW.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.wlabPW.ForeColor = System.Drawing.Color.LightGray
        Me.wlabPW.Location = New System.Drawing.Point(1, 1)
        Me.wlabPW.Margin = New System.Windows.Forms.Padding(0)
        Me.wlabPW.Name = "wlabPW"
        Me.wlabPW.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.wlabPW.Size = New System.Drawing.Size(150, 20)
        Me.wlabPW.TabIndex = 100
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Panel7.Controls.Add(Me.wlabID)
        Me.Panel7.Location = New System.Drawing.Point(5, 5)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel7.Size = New System.Drawing.Size(152, 22)
        Me.Panel7.TabIndex = 1
        '
        'wlabID
        '
        Me.wlabID.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.wlabID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.wlabID.ForeColor = System.Drawing.Color.LightGray
        Me.wlabID.Location = New System.Drawing.Point(1, 1)
        Me.wlabID.Margin = New System.Windows.Forms.Padding(0)
        Me.wlabID.Name = "wlabID"
        Me.wlabID.Size = New System.Drawing.Size(150, 20)
        Me.wlabID.TabIndex = 99
        '
        'mabistartbtn
        '
        Me.mabistartbtn.AutoSize = True
        Me.mabistartbtn.BackColor = System.Drawing.Color.Transparent
        Me.mabistartbtn.Location = New System.Drawing.Point(794, 569)
        Me.mabistartbtn.Margin = New System.Windows.Forms.Padding(0)
        Me.mabistartbtn.MinimumSize = New System.Drawing.Size(140, 74)
        Me.mabistartbtn.Name = "mabistartbtn"
        Me.mabistartbtn.Size = New System.Drawing.Size(140, 74)
        Me.mabistartbtn.TabIndex = 32
        Me.mabistartbtn.Text = "마비노기 시작"
        Me.mabistartbtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TmpTab
        '
        Me.TmpTab.AutoSize = True
        Me.TmpTab.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.TmpTab.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.TmpTab.ForeColor = System.Drawing.Color.White
        Me.TmpTab.Location = New System.Drawing.Point(150, 36)
        Me.TmpTab.Margin = New System.Windows.Forms.Padding(0)
        Me.TmpTab.Name = "TmpTab"
        Me.TmpTab.Padding = New System.Windows.Forms.Padding(2)
        Me.TmpTab.Size = New System.Drawing.Size(47, 19)
        Me.TmpTab.TabIndex = 21
        Me.TmpTab.Text = "실험실"
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.CheckBoxes = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader16, Me.ColumnHeader17, Me.ColumnHeader18})
        Me.ListView1.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.ListView1.ForeColor = System.Drawing.Color.LightGray
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView1.Location = New System.Drawing.Point(221, 116)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(0, 0)
        Me.ListView1.TabIndex = 35
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "패키지명"
        Me.ColumnHeader16.Width = 282
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "갱신일"
        Me.ColumnHeader17.Width = 95
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "상태"
        Me.ColumnHeader18.Width = 95
        '
        'TabPageLaboratory
        '
        Me.TabPageLaboratory.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TabPageLaboratory.Controls.Add(Me.Label3)
        Me.TabPageLaboratory.Location = New System.Drawing.Point(5, 58)
        Me.TabPageLaboratory.Name = "TabPageLaboratory"
        Me.TabPageLaboratory.Size = New System.Drawing.Size(930, 505)
        Me.TabPageLaboratory.TabIndex = 36
        Me.TabPageLaboratory.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 5)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 20)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "마비노기 실험실"
        '
        'TabBtnSetting
        '
        Me.TabBtnSetting.AutoSize = True
        Me.TabBtnSetting.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.TabBtnSetting.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.TabBtnSetting.ForeColor = System.Drawing.Color.White
        Me.TabBtnSetting.Location = New System.Drawing.Point(115, 36)
        Me.TabBtnSetting.Margin = New System.Windows.Forms.Padding(0)
        Me.TabBtnSetting.Name = "TabBtnSetting"
        Me.TabBtnSetting.Padding = New System.Windows.Forms.Padding(2)
        Me.TabBtnSetting.Size = New System.Drawing.Size(35, 19)
        Me.TabBtnSetting.TabIndex = 20
        Me.TabBtnSetting.Text = "설정"
        '
        'TabPageMain
        '
        Me.TabPageMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TabPageMain.Controls.Add(Me.WebBrowser2)
        Me.TabPageMain.Location = New System.Drawing.Point(5, 58)
        Me.TabPageMain.Name = "TabPageMain"
        Me.TabPageMain.Size = New System.Drawing.Size(930, 505)
        Me.TabPageMain.TabIndex = 37
        '
        'WebBrowser2
        '
        Me.WebBrowser2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser2.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser2.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser2.Name = "WebBrowser2"
        Me.WebBrowser2.ScrollBarsEnabled = False
        Me.WebBrowser2.Size = New System.Drawing.Size(930, 505)
        Me.WebBrowser2.TabIndex = 0
        Me.WebBrowser2.Url = New System.Uri("http://willowspack.srin.kr/intro.php", System.UriKind.Absolute)
        '
        'TabBtnLog
        '
        Me.TabBtnLog.AutoSize = True
        Me.TabBtnLog.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.TabBtnLog.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.TabBtnLog.ForeColor = System.Drawing.Color.White
        Me.TabBtnLog.Location = New System.Drawing.Point(898, 36)
        Me.TabBtnLog.Margin = New System.Windows.Forms.Padding(0)
        Me.TabBtnLog.Name = "TabBtnLog"
        Me.TabBtnLog.Padding = New System.Windows.Forms.Padding(2)
        Me.TabBtnLog.Size = New System.Drawing.Size(35, 19)
        Me.TabBtnLog.TabIndex = 21
        Me.TabBtnLog.Text = "기록"
        '
        'TabBtnMain
        '
        Me.TabBtnMain.AutoSize = True
        Me.TabBtnMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.TabBtnMain.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.TabBtnMain.ForeColor = System.Drawing.Color.White
        Me.TabBtnMain.Location = New System.Drawing.Point(5, 36)
        Me.TabBtnMain.Margin = New System.Windows.Forms.Padding(0)
        Me.TabBtnMain.Name = "TabBtnMain"
        Me.TabBtnMain.Padding = New System.Windows.Forms.Padding(2)
        Me.TabBtnMain.Size = New System.Drawing.Size(35, 19)
        Me.TabBtnMain.TabIndex = 21
        Me.TabBtnMain.Text = "메인"
        '
        'Prog02
        '
        Me.Prog02.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Prog02.Location = New System.Drawing.Point(725, 576)
        Me.Prog02.Margin = New System.Windows.Forms.Padding(0)
        Me.Prog02.Name = "Prog02"
        Me.Prog02.Size = New System.Drawing.Size(59, 15)
        Me.Prog02.TabIndex = 38
        Me.Prog02.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabPageSetting
        '
        Me.TabPageSetting.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.TabPageSetting.Controls.Add(Me.Label30)
        Me.TabPageSetting.Controls.Add(Me.Label29)
        Me.TabPageSetting.Controls.Add(Me.Label26)
        Me.TabPageSetting.Controls.Add(Me.Label28)
        Me.TabPageSetting.Controls.Add(Me.Label25)
        Me.TabPageSetting.Controls.Add(Me.Label27)
        Me.TabPageSetting.Controls.Add(Me.Label24)
        Me.TabPageSetting.Controls.Add(Me.Label23)
        Me.TabPageSetting.Controls.Add(Me.Label16)
        Me.TabPageSetting.Controls.Add(Me.Panel25)
        Me.TabPageSetting.Controls.Add(Me.Panel13)
        Me.TabPageSetting.Controls.Add(Me.Panel15)
        Me.TabPageSetting.Location = New System.Drawing.Point(5, 58)
        Me.TabPageSetting.Name = "TabPageSetting"
        Me.TabPageSetting.Size = New System.Drawing.Size(930, 505)
        Me.TabPageSetting.TabIndex = 39
        Me.TabPageSetting.Visible = False
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label30.Location = New System.Drawing.Point(452, 8)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(122, 15)
        Me.Label30.TabIndex = 1
        Me.Label30.Text = "준비중인 기능입니다."
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(799, 6)
        Me.Label29.Name = "Label29"
        Me.Label29.Padding = New System.Windows.Forms.Padding(2)
        Me.Label29.Size = New System.Drawing.Size(35, 19)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "기타"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(694, 6)
        Me.Label26.Name = "Label26"
        Me.Label26.Padding = New System.Windows.Forms.Padding(2)
        Me.Label26.Size = New System.Drawing.Size(35, 19)
        Me.Label26.TabIndex = 0
        Me.Label26.Text = "성능"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(764, 6)
        Me.Label28.Name = "Label28"
        Me.Label28.Padding = New System.Windows.Forms.Padding(2)
        Me.Label28.Size = New System.Drawing.Size(35, 19)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "채팅"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(647, 6)
        Me.Label25.Name = "Label25"
        Me.Label25.Padding = New System.Windows.Forms.Padding(2)
        Me.Label25.Size = New System.Drawing.Size(47, 19)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "사운드"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Label27.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(729, 6)
        Me.Label27.Name = "Label27"
        Me.Label27.Padding = New System.Windows.Forms.Padding(2)
        Me.Label27.Size = New System.Drawing.Size(35, 19)
        Me.Label27.TabIndex = 0
        Me.Label27.Text = "조작"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(612, 6)
        Me.Label24.Name = "Label24"
        Me.Label24.Padding = New System.Windows.Forms.Padding(2)
        Me.Label24.Size = New System.Drawing.Size(35, 19)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "효과"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(577, 6)
        Me.Label23.Name = "Label23"
        Me.Label23.Padding = New System.Windows.Forms.Padding(2)
        Me.Label23.Size = New System.Drawing.Size(35, 19)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "화면"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Label16.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(4, 6)
        Me.Label16.Name = "Label16"
        Me.Label16.Padding = New System.Windows.Forms.Padding(2)
        Me.Label16.Size = New System.Drawing.Size(75, 19)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "버들팩 설정"
        '
        'Panel25
        '
        Me.Panel25.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel25.Controls.Add(Me.Panel26)
        Me.Panel25.Location = New System.Drawing.Point(4, 25)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(570, 476)
        Me.Panel25.TabIndex = 46
        '
        'Panel26
        '
        Me.Panel26.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel26.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel26.Controls.Add(Me.CheckBox3)
        Me.Panel26.Controls.Add(Me.CheckBox2)
        Me.Panel26.Controls.Add(Me.CheckBox1)
        Me.Panel26.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Panel26.Location = New System.Drawing.Point(1, 1)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Size = New System.Drawing.Size(568, 474)
        Me.Panel26.TabIndex = 0
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(8, 44)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(182, 19)
        Me.CheckBox3.TabIndex = 0
        Me.CheckBox3.Text = "버들팩 패키지 자동 업데이트" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(8, 26)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(182, 19)
        Me.CheckBox2.TabIndex = 0
        Me.CheckBox2.Text = "마비노기 업데이트 자동 감지"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(8, 8)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(130, 19)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = "버들팩 포터블 모드"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel13.Controls.Add(Me.Panel14)
        Me.Panel13.Location = New System.Drawing.Point(577, 25)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(349, 476)
        Me.Panel13.TabIndex = 44
        '
        'Panel14
        '
        Me.Panel14.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel14.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel14.Controls.Add(Me.Panel23)
        Me.Panel14.Controls.Add(Me.Label22)
        Me.Panel14.Controls.Add(Me.Label21)
        Me.Panel14.Controls.Add(Me.Label20)
        Me.Panel14.Controls.Add(Me.Panel21)
        Me.Panel14.Controls.Add(Me.Panel19)
        Me.Panel14.Controls.Add(Me.Panel17)
        Me.Panel14.Controls.Add(Me.Label17)
        Me.Panel14.Controls.Add(Me.Panel9)
        Me.Panel14.Controls.Add(Me.Panel11)
        Me.Panel14.Location = New System.Drawing.Point(1, 1)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(347, 474)
        Me.Panel14.TabIndex = 0
        '
        'Panel23
        '
        Me.Panel23.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel23.Controls.Add(Me.Panel24)
        Me.Panel23.Location = New System.Drawing.Point(5, 409)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(338, 52)
        Me.Panel23.TabIndex = 48
        '
        'Panel24
        '
        Me.Panel24.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel24.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel24.Controls.Add(Me.CheckBox15)
        Me.Panel24.Controls.Add(Me.CheckBox16)
        Me.Panel24.Location = New System.Drawing.Point(1, 1)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(336, 50)
        Me.Panel24.TabIndex = 0
        '
        'CheckBox15
        '
        Me.CheckBox15.AutoSize = True
        Me.CheckBox15.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.CheckBox15.Location = New System.Drawing.Point(7, 24)
        Me.CheckBox15.Name = "CheckBox15"
        Me.CheckBox15.Size = New System.Drawing.Size(206, 19)
        Me.CheckBox15.TabIndex = 1
        Me.CheckBox15.Text = "NPC 대화 창 글꼴 크기 자동 조절"
        Me.CheckBox15.UseVisualStyleBackColor = True
        '
        'CheckBox16
        '
        Me.CheckBox16.AutoSize = True
        Me.CheckBox16.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.CheckBox16.Location = New System.Drawing.Point(7, 7)
        Me.CheckBox16.Name = "CheckBox16"
        Me.CheckBox16.Size = New System.Drawing.Size(146, 19)
        Me.CheckBox16.TabIndex = 1
        Me.CheckBox16.Text = "스킬 카메라 연출 사용"
        Me.CheckBox16.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(8, 344)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(99, 15)
        Me.Label22.TabIndex = 43
        Me.Label22.Text = "타워 실린더 옵션"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(7, 278)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(59, 15)
        Me.Label21.TabIndex = 43
        Me.Label21.Text = "비행 옵션"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(7, 229)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(87, 15)
        Me.Label20.TabIndex = 43
        Me.Label20.Text = "공격 예약 옵션"
        '
        'Panel21
        '
        Me.Panel21.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel21.Controls.Add(Me.Panel22)
        Me.Panel21.Location = New System.Drawing.Point(5, 352)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(338, 52)
        Me.Panel21.TabIndex = 47
        '
        'Panel22
        '
        Me.Panel22.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel22.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel22.Controls.Add(Me.CheckBox13)
        Me.Panel22.Controls.Add(Me.CheckBox14)
        Me.Panel22.Location = New System.Drawing.Point(1, 1)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(336, 50)
        Me.Panel22.TabIndex = 0
        '
        'CheckBox13
        '
        Me.CheckBox13.AutoSize = True
        Me.CheckBox13.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.CheckBox13.Location = New System.Drawing.Point(7, 28)
        Me.CheckBox13.Name = "CheckBox13"
        Me.CheckBox13.Size = New System.Drawing.Size(230, 19)
        Me.CheckBox13.TabIndex = 1
        Me.CheckBox13.Text = "타워 실린더 설치 시 조종 메뉴얼 보기"
        Me.CheckBox13.UseVisualStyleBackColor = True
        '
        'CheckBox14
        '
        Me.CheckBox14.AutoSize = True
        Me.CheckBox14.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.CheckBox14.Location = New System.Drawing.Point(7, 11)
        Me.CheckBox14.Name = "CheckBox14"
        Me.CheckBox14.Size = New System.Drawing.Size(90, 19)
        Me.CheckBox14.TabIndex = 1
        Me.CheckBox14.Text = "조준점 모드"
        Me.CheckBox14.UseVisualStyleBackColor = True
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel19.Controls.Add(Me.Panel20)
        Me.Panel19.Location = New System.Drawing.Point(4, 286)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(339, 52)
        Me.Panel19.TabIndex = 47
        '
        'Panel20
        '
        Me.Panel20.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel20.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel20.Controls.Add(Me.ShowPilotingManual)
        Me.Panel20.Controls.Add(Me.CheckBox11)
        Me.Panel20.Location = New System.Drawing.Point(1, 1)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(337, 50)
        Me.Panel20.TabIndex = 0
        '
        'ShowPilotingManual
        '
        Me.ShowPilotingManual.AutoSize = True
        Me.ShowPilotingManual.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.ShowPilotingManual.Location = New System.Drawing.Point(7, 28)
        Me.ShowPilotingManual.Name = "ShowPilotingManual"
        Me.ShowPilotingManual.Size = New System.Drawing.Size(158, 19)
        Me.ShowPilotingManual.TabIndex = 1
        Me.ShowPilotingManual.Text = "이륙시 조종 메뉴얼 보기"
        Me.ShowPilotingManual.UseVisualStyleBackColor = True
        '
        'CheckBox11
        '
        Me.CheckBox11.AutoSize = True
        Me.CheckBox11.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.CheckBox11.Location = New System.Drawing.Point(7, 11)
        Me.CheckBox11.Name = "CheckBox11"
        Me.CheckBox11.Size = New System.Drawing.Size(282, 19)
        Me.CheckBox11.TabIndex = 1
        Me.CheckBox11.Text = "W, A, S, D 키와 스페이스로 비행 펫을 조종하기"
        Me.CheckBox11.UseVisualStyleBackColor = True
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel17.Controls.Add(Me.Panel18)
        Me.Panel17.Location = New System.Drawing.Point(4, 237)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(339, 35)
        Me.Panel17.TabIndex = 46
        '
        'Panel18
        '
        Me.Panel18.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel18.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel18.Controls.Add(Me.IsAutoBattle)
        Me.Panel18.Controls.Add(Me.ReserveAttack_2)
        Me.Panel18.Controls.Add(Me.ReserveAttack_1)
        Me.Panel18.Controls.Add(Me.ReserveAttack_0)
        Me.Panel18.Location = New System.Drawing.Point(1, 1)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(337, 33)
        Me.Panel18.TabIndex = 0
        '
        'IsAutoBattle
        '
        Me.IsAutoBattle.AutoSize = True
        Me.IsAutoBattle.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.IsAutoBattle.Location = New System.Drawing.Point(248, 11)
        Me.IsAutoBattle.Name = "IsAutoBattle"
        Me.IsAutoBattle.Size = New System.Drawing.Size(78, 19)
        Me.IsAutoBattle.TabIndex = 1
        Me.IsAutoBattle.Text = "자동 전투"
        Me.IsAutoBattle.UseVisualStyleBackColor = True
        '
        'ReserveAttack_2
        '
        Me.ReserveAttack_2.AutoSize = True
        Me.ReserveAttack_2.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.ReserveAttack_2.Location = New System.Drawing.Point(168, 11)
        Me.ReserveAttack_2.Name = "ReserveAttack_2"
        Me.ReserveAttack_2.Size = New System.Drawing.Size(61, 19)
        Me.ReserveAttack_2.TabIndex = 0
        Me.ReserveAttack_2.TabStop = True
        Me.ReserveAttack_2.Text = "여러번"
        Me.ReserveAttack_2.UseVisualStyleBackColor = True
        '
        'ReserveAttack_1
        '
        Me.ReserveAttack_1.AutoSize = True
        Me.ReserveAttack_1.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.ReserveAttack_1.Location = New System.Drawing.Point(104, 11)
        Me.ReserveAttack_1.Name = "ReserveAttack_1"
        Me.ReserveAttack_1.Size = New System.Drawing.Size(44, 19)
        Me.ReserveAttack_1.TabIndex = 0
        Me.ReserveAttack_1.TabStop = True
        Me.ReserveAttack_1.Text = "1회"
        Me.ReserveAttack_1.UseVisualStyleBackColor = True
        '
        'ReserveAttack_0
        '
        Me.ReserveAttack_0.AutoSize = True
        Me.ReserveAttack_0.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.ReserveAttack_0.Location = New System.Drawing.Point(7, 11)
        Me.ReserveAttack_0.Name = "ReserveAttack_0"
        Me.ReserveAttack_0.Size = New System.Drawing.Size(77, 19)
        Me.ReserveAttack_0.TabIndex = 0
        Me.ReserveAttack_0.TabStop = True
        Me.ReserveAttack_0.Text = "사용 안함"
        Me.ReserveAttack_0.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(7, 180)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(94, 15)
        Me.Label17.TabIndex = 43
        Me.Label17.Text = "Ctrl 타게팅 방식"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel9.Controls.Add(Me.Panel10)
        Me.Panel9.Location = New System.Drawing.Point(4, 4)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(339, 170)
        Me.Panel9.TabIndex = 40
        '
        'Panel10
        '
        Me.Panel10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel10.Controls.Add(Me.ShowDownGauge)
        Me.Panel10.Controls.Add(Me.TargetPreparedAttack)
        Me.Panel10.Controls.Add(Me.AutoInstrumentPlay)
        Me.Panel10.Controls.Add(Me.IsKeepCamera)
        Me.Panel10.Controls.Add(Me.IsSmartWeaponSetEnabled)
        Me.Panel10.Controls.Add(Me.AutoMouseNearTargeting)
        Me.Panel10.Controls.Add(Me.IsBagHotKeyEnabled)
        Me.Panel10.Controls.Add(Me.IsEscWindowCloseEnabled)
        Me.Panel10.Controls.Add(Me.IsEscCancelEnabled)
        Me.Panel10.Location = New System.Drawing.Point(1, 1)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(337, 168)
        Me.Panel10.TabIndex = 0
        '
        'ShowDownGauge
        '
        Me.ShowDownGauge.AutoSize = True
        Me.ShowDownGauge.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.ShowDownGauge.Location = New System.Drawing.Point(7, 143)
        Me.ShowDownGauge.Name = "ShowDownGauge"
        Me.ShowDownGauge.Size = New System.Drawing.Size(186, 19)
        Me.ShowDownGauge.TabIndex = 0
        Me.ShowDownGauge.Text = "타게팅 시 다운 게이지를 표시"
        Me.ShowDownGauge.UseVisualStyleBackColor = True
        '
        'TargetPreparedAttack
        '
        Me.TargetPreparedAttack.AutoSize = True
        Me.TargetPreparedAttack.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.TargetPreparedAttack.Location = New System.Drawing.Point(7, 126)
        Me.TargetPreparedAttack.Name = "TargetPreparedAttack"
        Me.TargetPreparedAttack.Size = New System.Drawing.Size(162, 19)
        Me.TargetPreparedAttack.TabIndex = 0
        Me.TargetPreparedAttack.Text = "타게팅 후 즉시 스킬 사용"
        Me.TargetPreparedAttack.UseVisualStyleBackColor = True
        '
        'AutoInstrumentPlay
        '
        Me.AutoInstrumentPlay.AutoSize = True
        Me.AutoInstrumentPlay.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.AutoInstrumentPlay.Location = New System.Drawing.Point(7, 58)
        Me.AutoInstrumentPlay.Name = "AutoInstrumentPlay"
        Me.AutoInstrumentPlay.Size = New System.Drawing.Size(102, 19)
        Me.AutoInstrumentPlay.TabIndex = 0
        Me.AutoInstrumentPlay.Text = "악기연주 반복"
        Me.AutoInstrumentPlay.UseVisualStyleBackColor = True
        '
        'IsKeepCamera
        '
        Me.IsKeepCamera.AutoSize = True
        Me.IsKeepCamera.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.IsKeepCamera.Location = New System.Drawing.Point(7, 109)
        Me.IsKeepCamera.Name = "IsKeepCamera"
        Me.IsKeepCamera.Size = New System.Drawing.Size(210, 19)
        Me.IsKeepCamera.TabIndex = 0
        Me.IsKeepCamera.Text = "장소 이동시 최적의 카메라로 설정"
        Me.IsKeepCamera.UseVisualStyleBackColor = True
        '
        'IsSmartWeaponSetEnabled
        '
        Me.IsSmartWeaponSetEnabled.AutoSize = True
        Me.IsSmartWeaponSetEnabled.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.IsSmartWeaponSetEnabled.Location = New System.Drawing.Point(7, 41)
        Me.IsSmartWeaponSetEnabled.Name = "IsSmartWeaponSetEnabled"
        Me.IsSmartWeaponSetEnabled.Size = New System.Drawing.Size(134, 19)
        Me.IsSmartWeaponSetEnabled.TabIndex = 0
        Me.IsSmartWeaponSetEnabled.Text = "자동 무기 슬롯 전환"
        Me.IsSmartWeaponSetEnabled.UseVisualStyleBackColor = True
        '
        'AutoMouseNearTargeting
        '
        Me.AutoMouseNearTargeting.AutoSize = True
        Me.AutoMouseNearTargeting.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.AutoMouseNearTargeting.Location = New System.Drawing.Point(7, 92)
        Me.AutoMouseNearTargeting.Name = "AutoMouseNearTargeting"
        Me.AutoMouseNearTargeting.Size = New System.Drawing.Size(118, 19)
        Me.AutoMouseNearTargeting.TabIndex = 0
        Me.AutoMouseNearTargeting.Text = "인접 자동 타게팅"
        Me.AutoMouseNearTargeting.UseVisualStyleBackColor = True
        '
        'IsBagHotKeyEnabled
        '
        Me.IsBagHotKeyEnabled.AutoSize = True
        Me.IsBagHotKeyEnabled.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.IsBagHotKeyEnabled.Location = New System.Drawing.Point(7, 75)
        Me.IsBagHotKeyEnabled.Name = "IsBagHotKeyEnabled"
        Me.IsBagHotKeyEnabled.Size = New System.Drawing.Size(210, 19)
        Me.IsBagHotKeyEnabled.TabIndex = 0
        Me.IsBagHotKeyEnabled.Text = "가방 안의 아이템을 단축기로 사용"
        Me.IsBagHotKeyEnabled.UseVisualStyleBackColor = True
        '
        'IsEscWindowCloseEnabled
        '
        Me.IsEscWindowCloseEnabled.AutoSize = True
        Me.IsEscWindowCloseEnabled.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.IsEscWindowCloseEnabled.Location = New System.Drawing.Point(7, 24)
        Me.IsEscWindowCloseEnabled.Name = "IsEscWindowCloseEnabled"
        Me.IsEscWindowCloseEnabled.Size = New System.Drawing.Size(139, 19)
        Me.IsEscWindowCloseEnabled.TabIndex = 0
        Me.IsEscWindowCloseEnabled.Text = "ESC키로 윈도우 닫기"
        Me.IsEscWindowCloseEnabled.UseVisualStyleBackColor = True
        '
        'IsEscCancelEnabled
        '
        Me.IsEscCancelEnabled.AutoSize = True
        Me.IsEscCancelEnabled.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.IsEscCancelEnabled.Location = New System.Drawing.Point(7, 7)
        Me.IsEscCancelEnabled.Name = "IsEscCancelEnabled"
        Me.IsEscCancelEnabled.Size = New System.Drawing.Size(127, 19)
        Me.IsEscCancelEnabled.TabIndex = 0
        Me.IsEscCancelEnabled.Text = "ESC키로 스킬 취소"
        Me.IsEscCancelEnabled.UseVisualStyleBackColor = True
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel11.Controls.Add(Me.Panel12)
        Me.Panel11.Location = New System.Drawing.Point(4, 188)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(339, 35)
        Me.Panel11.TabIndex = 41
        '
        'Panel12
        '
        Me.Panel12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel12.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel12.Controls.Add(Me.TargetingMode_1)
        Me.Panel12.Controls.Add(Me.TargetingMode_0)
        Me.Panel12.Location = New System.Drawing.Point(1, 1)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(337, 33)
        Me.Panel12.TabIndex = 0
        '
        'TargetingMode_1
        '
        Me.TargetingMode_1.AutoSize = True
        Me.TargetingMode_1.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.TargetingMode_1.Location = New System.Drawing.Point(174, 11)
        Me.TargetingMode_1.Name = "TargetingMode_1"
        Me.TargetingMode_1.Size = New System.Drawing.Size(105, 19)
        Me.TargetingMode_1.TabIndex = 0
        Me.TargetingMode_1.TabStop = True
        Me.TargetingMode_1.Text = "기존 타겟 우선"
        Me.TargetingMode_1.UseVisualStyleBackColor = True
        '
        'TargetingMode_0
        '
        Me.TargetingMode_0.AutoSize = True
        Me.TargetingMode_0.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.TargetingMode_0.Location = New System.Drawing.Point(7, 11)
        Me.TargetingMode_0.Name = "TargetingMode_0"
        Me.TargetingMode_0.Size = New System.Drawing.Size(117, 19)
        Me.TargetingMode_0.TabIndex = 0
        Me.TargetingMode_0.TabStop = True
        Me.TargetingMode_0.Text = "마우스 커서 우선"
        Me.TargetingMode_0.UseVisualStyleBackColor = True
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel15.Controls.Add(Me.Panel16)
        Me.Panel15.Location = New System.Drawing.Point(577, 25)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(349, 476)
        Me.Panel15.TabIndex = 45
        '
        'Panel16
        '
        Me.Panel16.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel16.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel16.Location = New System.Drawing.Point(1, 1)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(347, 474)
        Me.Panel16.TabIndex = 0
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(9, 574)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(167, 15)
        Me.Label31.TabIndex = 40
        Me.Label31.Text = "#Progress Version 2.75 min 2"
        '
        'TrayIcon
        '
        Me.TrayIcon.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TrayIcon.Icon = CType(resources.GetObject("TrayIcon.Icon"), System.Drawing.Icon)
        Me.TrayIcon.Text = "WillowsPackage"
        Me.TrayIcon.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.ContextMenuStrip1.DropShadowEnabled = False
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.요리도우미ToolStripMenuItem, Me.요리도우미ToolStripMenuItem1, Me.마비노기실행ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(179, 70)
        '
        '요리도우미ToolStripMenuItem
        '
        Me.요리도우미ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.창활성화ToolStripMenuItem, Me.종료ToolStripMenuItem})
        Me.요리도우미ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.요리도우미ToolStripMenuItem.Name = "요리도우미ToolStripMenuItem"
        Me.요리도우미ToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.요리도우미ToolStripMenuItem.Text = "버들팩 트레이 모드"
        '
        '창활성화ToolStripMenuItem
        '
        Me.창활성화ToolStripMenuItem.Name = "창활성화ToolStripMenuItem"
        Me.창활성화ToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.창활성화ToolStripMenuItem.Text = "창 활성화"
        '
        '종료ToolStripMenuItem
        '
        Me.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem"
        Me.종료ToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.종료ToolStripMenuItem.Text = "종료"
        '
        '요리도우미ToolStripMenuItem1
        '
        Me.요리도우미ToolStripMenuItem1.Name = "요리도우미ToolStripMenuItem1"
        Me.요리도우미ToolStripMenuItem1.Size = New System.Drawing.Size(178, 22)
        Me.요리도우미ToolStripMenuItem1.Text = "요리도우미"
        '
        '마비노기실행ToolStripMenuItem
        '
        Me.마비노기실행ToolStripMenuItem.Name = "마비노기실행ToolStripMenuItem"
        Me.마비노기실행ToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.마비노기실행ToolStripMenuItem.Text = "마비노기 실행"
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 20)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(940, 650)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.TabPagePackage)
        Me.Controls.Add(Me.TabPageMain)
        Me.Controls.Add(Me.Prog02)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.mabistartbtn)
        Me.Controls.Add(Me.closebtn)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.CustomProgressBar_case2)
        Me.Controls.Add(Me.CustomProgressBar_case1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.TmpTab)
        Me.Controls.Add(Me.TabBtnMain)
        Me.Controls.Add(Me.TabBtnLog)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TabBtnSetting)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TabBtnPackage)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TabPage04)
        Me.Controls.Add(Me.TabPageLaboratory)
        Me.Controls.Add(Me.TabPageSetting)
        Me.Controls.Add(Me.TabPageMabiNotice)
        Me.Controls.Add(Me.TabPageProgInfo)
        Me.Font = New System.Drawing.Font("맑은 고딕", 11.0!)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(940, 650)
        Me.MinimumSize = New System.Drawing.Size(940, 650)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Willow's Mabinogi MOD"
        Me.TabPagePackage.ResumeLayout(False)
        Me.TabPagePackage.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.Clbtn2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.CustomProgressBar_case1.ResumeLayout(False)
        Me.CustomProgressBar_case2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.closebtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageMabiNotice.ResumeLayout(False)
        Me.TabPageProgInfo.ResumeLayout(False)
        Me.TabPage04.ResumeLayout(False)
        Me.TabPage04.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.TabPageLaboratory.ResumeLayout(False)
        Me.TabPageLaboratory.PerformLayout()
        Me.TabPageMain.ResumeLayout(False)
        Me.TabPageSetting.ResumeLayout(False)
        Me.TabPageSetting.PerformLayout()
        Me.Panel25.ResumeLayout(False)
        Me.Panel26.ResumeLayout(False)
        Me.Panel26.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        Me.Panel23.ResumeLayout(False)
        Me.Panel24.ResumeLayout(False)
        Me.Panel24.PerformLayout()
        Me.Panel21.ResumeLayout(False)
        Me.Panel22.ResumeLayout(False)
        Me.Panel22.PerformLayout()
        Me.Panel19.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        Me.Panel17.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        Me.Panel18.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region " DownloadSettings "
    'SUB MAIN WHERE WE ENABLE VISUAL STYLES, AND RUN MAIN FORM
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.DoEvents()
        Application.Run(New vxFrmMain)
        Application.Exit()
    End Sub

    Public FilesizeMax As Long = 0
    Public Filecount As Long = 0
    Public CurrCount As Integer = 0
    Public ProgBarMax As Integer = 780
    Public DownloadedFile As String = ""
    Public CurrentFileSize As Long = 0

    Private Function GetFileNameFromURL(ByVal URL As String) As String
        If URL.IndexOf("/"c) = -1 Then Return String.Empty

        Return "\" & URL.Substring(URL.LastIndexOf("/"c) + 1)
    End Function

    Private Sub _Downloader_FileDownloadSizeObtained(ByVal iFileSize As Long) Handles _Downloader.FileDownloadSizeObtained
        FilesizeMax = Convert.ToInt32(iFileSize)
        CProgress2.Width = 0
    End Sub

    Private Sub _Downloader_FileDownloadComplete() Handles _Downloader.FileDownloadComplete
        CurrCount += CurrentFileSize
        CProgress1.Width = (((CurrCount / Filecount) * 100) * (ProgBarMax / 100))
        CProgress2.Width = ProgBarMax
    End Sub

    'FIRES WHEN DOWNLOAD FAILES. PASSES IN EXCEPTION INFO
    Private Sub _Downloader_FileDownloadFailed(ByVal ex As System.Exception) Handles _Downloader.FileDownloadFailed
        MessageBox.Show("An error has occured during download: " & ex.Message)
    End Sub

    Private Sub _Downloader_AmountDownloadedChanged(ByVal iNewProgress As Long) Handles _Downloader.AmountDownloadedChanged
        'Prog02.Text = CurrCount & " " & iNewProgress & " / " & Filecount
        Prog02.Text = Math.Floor(((CurrCount + Convert.ToInt32(iNewProgress)) / Filecount) * 100) & " %"
        CProgress1.Width = (((CurrCount + Convert.ToInt32(iNewProgress)) / Filecount) * 100) * (ProgBarMax / 100)
        CProgress2.Width = Math.Floor(((Convert.ToInt32(iNewProgress) / FilesizeMax) * 100) * (ProgBarMax / 100))
        'TextBox1.Text &= Math.Floor(((Convert.ToInt32(iNewProgress / FilesizeMax)) * 100) * (ProgBarMax / 100)) & vbNewLine
        Progress.Text = "[" & FormatFileSize(Convert.ToInt32(iNewProgress)) & " Bytes / " & FormatFileSize(FilesizeMax) & " Bytes] " & DownloadedFile & " 파일을 다운로드하고 있습니다."
        Application.DoEvents()
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
                    Case Is < 1073741824
                        Return (Size / MB).ToString("N") & " MB"
                    Case Is < 1099511627776
                        Return (Size / MB / KB) & " GB"
                End Select
            End If
            Return Size.ToString
        Catch ex As Exception
            Return Size.ToString
        End Try
    End Function
#End Region

#Region " MabiStart "
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

    Public Sub MabiStart()
        Dim patchinfo As String = "http://211.218.233.238/patch/patch.txt"
        Dim MabiInfo As MabiUpdateInfo = New MabiUpdateInfo

        If vxFS.backGroundDownloader(patchinfo, vxConfig.dirme & "\Config\Patch\", "patch.txt") Then
            MabiInfo.setInfo(vxConfig.dirme & "\Config\Patch\patch.txt")

            Dim Port As Integer = 11000
            Dim Hostname As String = MabiInfo.login
            'Call the function
            Dim PortOpen As Boolean = IsPortOpen(Hostname, Port)

            'MsgBox(IsPortOpen("willowslab.com", 80))

            If PortOpen Then
                MabiRun(MabiInfo.login, MabiInfo.arg)
                Me.WindowState = FormWindowState.Minimized
            Else
                If MsgBox("마비노기가 패치중이거나 서버가 닫혀있습니다. 그래도 마비노기를 실행하시겠습니까?", MsgBoxStyle.OkCancel Or MsgBoxStyle.Information Or vbMsgBoxSetForeground) = MsgBoxResult.Ok Then
                    MabiRun(MabiInfo.login, MabiInfo.arg)
                    Me.WindowState = FormWindowState.Minimized
                End If
            End If
        End If
    End Sub

    Public Function MabiRun(login As String, arg As String) As Boolean
        Dim Prcs = New Process()
        Dim PrcsSI = New ProcessStartInfo
        Dim Result As String = "", Err As String = ""
        Dim Arguments As String = "Client.exe code:1622 ver:" & vxMabiCore.getVersion & " logip:" & login & " logport:11000 " & arg
        With PrcsSI
            .FileName = "CMD.exe"
            .WorkingDirectory = vxConfig.MabiDir
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
#End Region

    Public Function mabiPatch()
        If Directory.Exists(vxConfig.dirme & "\Config\Patch") = False Then
            Directory.CreateDirectory(vxConfig.dirme & "\Config\Patch")
        End If

        Dim patchinfo As String = "http://211.218.233.238/patch/patch.txt"
        Dim myMabiVer As Double = 0
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
        Dim fcount As Integer = 0
        Dim mabiPatchFile As List(Of MabiPatchFileInfo) = New List(Of MabiPatchFileInfo)
        Dim updatetype As Boolean = False

        AllDisabled(True)
        Me.Progress.Text = "마비노기 정보 파일을 다운받는 중..."

        System.Threading.Thread.Sleep(150)
        Me.Progress.Text = "마비노기 버전 정보 파일을 읽는 중..."

        If File.Exists(vxConfig.MabiDir & "\version.dat") = False Then
            MsgBox("마비노기 버전 정보를 불러오지 못해 패치에 실패했습니다.", MsgBoxStyle.Information Or vbMsgBoxSetForeground)
            resets()
            Return False
        Else
            myMabiVer = vxMabiCore.getVersion()
        End If

        Me.Progress.Text = "마비노기 패치 정보 파일을 읽는 중..."
        If vxFS.backGroundDownloader(patchinfo, vxConfig.dirme & "\Config\Patch\", "patch.txt") Then
            Dim Temp As String = vxFS.Read_File(vxConfig.dirme & "\Config\Patch\patch.txt")
            For Each readLine As String In Split(Temp, vbNewLine)
                If InStr(readLine, "patch_accept=") > 0 Then
                    patch_accept = Replace(readLine, "patch_accept=", "")
                ElseIf InStr(readLine, "local_version=") > 0 Then
                    local_version = Replace(readLine, "local_version=", "")
                    main_fullversion = local_version
                ElseIf InStr(readLine, "local_ftp=") > 0 Then
                    local_ftp = Replace(readLine, "local_ftp=", "")
                ElseIf InStr(readLine, "main_version=") > 0 Then
                    main_version = Replace(readLine, "main_version=", "")
                ElseIf InStr(readLine, "main_ftp=") > 0 Then
                    main_ftp = Replace(readLine, "main_ftp=", "")
                ElseIf InStr(readLine, "launcherinfo=") > 0 Then
                    launcherinfo = Replace(readLine, "launcherinfo=", "")
                ElseIf InStr(readLine, "login=") > 0 Then
                    login = Replace(readLine, "login=", "")
                ElseIf InStr(readLine, "arg=") > 0 Then
                    arg = Replace(readLine, "arg=", "")
                ElseIf InStr(readLine, "addin=") > 0 Then
                    addin = Replace(readLine, "addin=", "")
                ElseIf InStr(readLine, "main_fullversion=") > 0 Then
                    main_fullversion = Replace(readLine, "main_fullversion=", "")
                End If
            Next
        Else
            MsgBox("마비노기 버전 정보를 불러오지 못해 패치에 실패했습니다.", MsgBoxStyle.Information Or vbMsgBoxSetForeground)
            resets()
            Return False
        End If

        Dim patchFile = "http://" & local_ftp & main_fullversion & "/" & main_fullversion & "_full.txt"
        Dim patchfilename = main_fullversion & "_full"
        Dim localTemp As String = vxConfig.MabiDir & "\_mltemp_\" & local_version & "\"
        Dim patchServer As String = "http://" & local_ftp & "/" & local_version & "/"
        Dim idx = -1

        Me.Progress.Text = "마비노기를 업데이트 해야 하는 지 확인 중..."
        If Val(local_version) > myMabiVer Then
            If Not Val(main_fullversion) > myMabiVer Then
                patchFile = "http://" & local_ftp & local_version & "/" & vxMabiCore.getVersion() & "_to_" & local_version & ".txt"
                patchfilename = vxMabiCore.getVersion() & "_to_" & local_version
                updatetype = True
            End If
            If MsgBox("마비노기가 확인된 버전보다 낮습니다. 패치를 진행하시겠습니까?", MsgBoxStyle.Information Or vbMsgBoxSetForeground Or MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                MsgBox("마비노기 패치를 중단했습니다." & vbNewLine & patchFile, MsgBoxStyle.Information Or vbMsgBoxSetForeground)
                resets()
                Return False
            End If
        Else
            Me.Progress.Text = "버들팩을 사용해주셔서 전혀 감사합니다."
            AllDisabled()
            Return False
        End If
        vxFS.Dir_Delete(localTemp)
        Me.Progress.Text = "패치할 파일 리스트를 다운받는 중..."

        Filecount = 0

        If vxFS.backGroundDownloader(patchFile, vxConfig.dirme & "\Config\Patch\", "patchinfo.txt") Then
            For Each readLine As String In Split(vxFS.Read_File(vxConfig.dirme & "\Config\Patch\patchinfo.txt"), vbNewLine)
                Try

                    Dim Temp() As String = Split(readLine, ",")

                    Filecount += Val(Trim(Temp(1)))
                Catch ex As Exception

                End Try

                If idx = -1 Then
                    fcount = Val(Trim(readLine))
                    Filecount += fcount + 1
                    If Directory.Exists(vxConfig.MabiDir & "\_mltemp_") Then
                        Directory.CreateDirectory(vxConfig.MabiDir & "\_mltemp_")
                    End If
                Else
                    Dim Temp As MabiPatchFileInfo = New MabiPatchFileInfo
                    If readLine <> "" Then
                        If Temp.setInfo(readLine) Then
                            mabiPatchFile.Add(Temp)
                        Else
                            MsgBox("마비노기 패치 정보를 불러오지 못해 패치에 실패했습니다." & vbNewLine & patchFile, MsgBoxStyle.Information Or vbMsgBoxSetForeground)
                            resets()
                            Return False
                        End If
                    End If
                End If
                idx += 1
            Next
            Me.Progress.Text = "마비노기 패치를 준비하는 중..."


            vxFS.Dir_Create(localTemp)
            For Each PatchFiles As MabiPatchFileInfo In mabiPatchFile
                CurrentFileSize = PatchFiles.FileSize
                If File.Exists(localTemp & PatchFiles.FileName) Then
                    If FileSystem.FileLen(localTemp & PatchFiles.FileName) = PatchFiles.FileSize Then
                        'CProgress1.Width = (((CurrCount / (Filecount - 1)) * 100) * (ProgBarMax / 100))
                        'MsgBox(CurrCount & " / " & Filecount)
                    End If
                    Continue For
                End If

                DownloadedFile = PatchFiles.FileName
                If FileDownloadSystem(patchServer & PatchFiles.FileName, PatchFiles.FileName, localTemp) Then
                    If FileSystem.FileLen(localTemp & PatchFiles.FileName) = PatchFiles.FileSize Then
                        'CProgress1.Width = (((CurrCount / (Filecount - 1)) * 100) * (ProgBarMax / 100))
                    Else
                        MsgBox("마비노기 패치 정보를 불러오지 못해 패치에 실패했습니다.", MsgBoxStyle.Information Or vbMsgBoxSetForeground)
                        resets()
                        Return False
                    End If
                Else
                    MsgBox("마비노기 패치 정보를 불러오지 못해 패치에 실패했습니다.", MsgBoxStyle.Information Or vbMsgBoxSetForeground)
                    resets()
                    Return False
                End If
            Next
        Else
            MsgBox("마비노기 패치 정보를 불러오지 못해 패치에 실패했습니다." & patchFile, MsgBoxStyle.Information Or vbMsgBoxSetForeground)
            resets()
            Return False
        End If

        Filecount = fcount

        My.Computer.FileSystem.WriteAllText(localTemp & patchfilename & ".zip", "", False)
        For Each vPatchFile As MabiPatchFileInfo In mabiPatchFile
            Try
                Me.Progress.Text = vPatchFile.FileName & " 파일을 합치는 중입니다. " & "..."
                My.Computer.FileSystem.WriteAllBytes(localTemp & patchfilename & ".zip", My.Computer.FileSystem.ReadAllBytes(localTemp & vPatchFile.FileName), True)
                'CurrCount += 1
                'CProgress1.Width = (((CurrCount / (Filecount - 1)) * 100) * (ProgBarMax / 100))
                'MsgBox(CurrCount & " / " & Filecount)
            Catch ex As Exception
                MsgBox("파일을 합치는 작업에 실패했습니다. 패치를 다시 시도합니다.", MsgBoxStyle.Information Or vbMsgBoxSetForeground)
                'FS.Dir_Delete(localTemp)
                'mabiPatch()
                resets()
                Return False
            End Try
        Next

        If updatetype = False Then

            vxFS.Dir_Create(vxConfig.MabiDir & "\Backup_Package_ByPatch")
            System.IO.File.SetAttributes(vxConfig.MabiDir & "\Backup_Package_ByPatch", System.IO.FileAttributes.Hidden)

            vxFS.Dir_Move(vxConfig.MabiDir & "\package", vxConfig.MabiDir & "\Backup_Package_ByPatch")
        Else
            Me.Progress.Text = "압축 해제 중입니다..."
            If Compression.DeCompress(localTemp & "\" & patchfilename & ".zip", vxConfig.MabiDir) = True Then

                vxMabiCore.setVersion(local_version)
                vxFS.Dir_Delete(vxConfig.MabiDir & "\Backup_Package_ByPatch")
                vxFS.Dir_Delete(localTemp)

                CurrCount += 1
                'CProgress1.Width = (((CurrCount / (Filecount - 1)) * 100) * (ProgBarMax / 100))
                'MsgBox(CurrCount & " / " & Filecount)
                MsgBox("마비노기 패치를 정상적으로 마쳤습니다.", MsgBoxStyle.Information Or vbMsgBoxSetForeground)
                resets()
                Return True
            Else
                vxFS.Dir_Move(vxConfig.MabiDir & "\Backup_Package_ByPatch", vxConfig.MabiDir & "\package")
                AllDisabled()
                Return False
            End If
        End If

        Return True
    End Function

    Public Function resets()
        Me.Progress.Text = "버들팩을 사용해주셔서 전혀 감사합니다."
        Prog02.Text = ""
        AllDisabled()
        Return True
    End Function

    '====================================MAIN
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'packlist download
        Me.ListXMLparse(False, True)
        vxStart.Close()
        Me.Show()
        Me.Focus()

        CProgress1.Width = 0
        CProgress2.Width = 0

        Me.MabiInstallDir.Text = vxConfig.MabiDir
        If vxConfig.MabiDir = "" Then
            Me.MabiInstallDir.Text = "여기를 클릭하여 마비노기 설치경로를 등록합니다."
        Else
            mabiPatch()
        End If

        If vxFS.dir_exists(vxConfig.MabiDir & "\package\Lumipack\") Then
            dellumiarte.Visible = True
        End If


    End Sub

    Public Function FileDownloadSystem(ByVal URL, ByVal Filename, ByVal dir)
        Try
            Dim downloadPath As String = dir.TrimEnd("\"c)
            If vxFS.dir_exists(downloadPath) Then

            End If

            _Downloader = New vxWebFileDownloader
            _Downloader.DownloadFileWithProgress(URL, dir & Filename)

            Return True
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function returnRGB(ByVal r As Integer, ByVal g As Integer, ByVal b As Integer)
        Return System.Drawing.Color.FromArgb(CType(CType(r, Byte), Integer), CType(CType(g, Byte), Integer), CType(CType(b, Byte), Integer))
    End Function

    Public curTab As Integer = 1
    Private Sub Label3_menter(sender As Object, e As EventArgs) Handles TabBtnPackage.MouseEnter, Label4.MouseEnter, Label5.MouseEnter, Label6.MouseEnter, TmpTab.MouseEnter, TabBtnSetting.MouseEnter, TabBtnLog.MouseEnter, TabBtnMain.MouseEnter, TabBtnLog.MouseEnter
        If (curTab = 1 And sender.GetHashCode <> TabBtnMain.GetHashCode) _
        Or (curTab = 2 And sender.GetHashCode <> TabBtnPackage.GetHashCode) _
        Or (curTab = 3 And sender.GetHashCode <> TabBtnSetting.GetHashCode) _
        Or (curTab = 4 And sender.GetHashCode <> Label4.GetHashCode) _
        Or (curTab = 5 And sender.GetHashCode <> Label5.GetHashCode) _
        Or (curTab = 6 And sender.GetHashCode <> Label6.GetHashCode) Then
            sender.BackColor = returnRGB(28, 151, 234)
        End If
    End Sub

    Private Sub Label3_mleave(sender As Object, a As EventArgs) Handles TabBtnPackage.MouseLeave, Label4.MouseLeave, Label5.MouseLeave, Label6.MouseLeave, TmpTab.MouseLeave, TabBtnSetting.MouseLeave, TabBtnLog.MouseLeave, TabBtnMain.MouseLeave, TabBtnLog.MouseLeave
        If (curTab = 1 And sender.GetHashCode <> TabBtnMain.GetHashCode) _
        Or (curTab = 2 And sender.GetHashCode <> TabBtnPackage.GetHashCode) _
        Or (curTab = 3 And sender.GetHashCode <> TabBtnSetting.GetHashCode) _
        Or (curTab = 4 And sender.GetHashCode <> Label4.GetHashCode) _
        Or (curTab = 5 And sender.GetHashCode <> Label5.GetHashCode) _
        Or (curTab = 6 And sender.GetHashCode <> Label6.GetHashCode) Then
            sender.BackColor = Color.Transparent
        End If

    End Sub

    Private Sub label3_mClick(sender As Object, e As EventArgs) Handles TabBtnPackage.MouseClick, Label4.MouseClick, Label5.MouseClick, Label6.MouseClick, TabBtnMain.MouseClick, TabBtnSetting.MouseClick
        If (curTab = 1 And sender.GetHashCode <> TabBtnMain.GetHashCode) _
        Or (curTab = 2 And sender.GetHashCode <> TabBtnPackage.GetHashCode) _
        Or (curTab = 3 And sender.GetHashCode <> TabBtnSetting.GetHashCode) _
        Or (curTab = 4 And sender.GetHashCode <> Label4.GetHashCode) _
        Or (curTab = 5 And sender.GetHashCode <> Label5.GetHashCode) _
        Or (curTab = 6 And sender.GetHashCode <> Label6.GetHashCode) Then
            If sender.GetHashCode = TabBtnMain.GetHashCode Then
                curTab = 1
                TabBtnPackage.BackColor = Color.Transparent
                TabBtnSetting.BackColor = Color.Transparent
                Label4.BackColor = Color.Transparent
                Label5.BackColor = Color.Transparent
                Label6.BackColor = Color.Transparent

                TabPageMain.Visible = True
                TabPagePackage.Visible = False
                TabPageSetting.Visible = False
                TabPageMabiNotice.Visible = False
                TabPageProgInfo.Visible = False
                TabPage04.Visible = False
            ElseIf sender.GetHashCode = TabBtnPackage.GetHashCode Then
                curTab = 2
                TabBtnMain.BackColor = Color.Transparent
                TabBtnSetting.BackColor = Color.Transparent
                Label4.BackColor = Color.Transparent
                Label5.BackColor = Color.Transparent
                Label6.BackColor = Color.Transparent

                TabPageMain.Visible = False
                TabPagePackage.Visible = True
                TabPageSetting.Visible = False
                TabPageMabiNotice.Visible = False
                TabPageProgInfo.Visible = False
                TabPage04.Visible = False
            ElseIf sender.GetHashCode = TabBtnSetting.GetHashCode Then
                curTab = 3
                TabBtnMain.BackColor = Color.Transparent
                TabBtnPackage.BackColor = Color.Transparent
                Label4.BackColor = Color.Transparent
                Label5.BackColor = Color.Transparent
                Label6.BackColor = Color.Transparent

                TabPageMain.Visible = False
                TabPagePackage.Visible = False
                TabPageSetting.Visible = True
                TabPageMabiNotice.Visible = False
                TabPageProgInfo.Visible = False
                TabPage04.Visible = False
            ElseIf sender.GetHashCode = Label4.GetHashCode Then
                curTab = 4
                TabBtnMain.BackColor = Color.Transparent
                TabBtnPackage.BackColor = Color.Transparent
                TabBtnSetting.BackColor = Color.Transparent
                Label5.BackColor = Color.Transparent
                Label6.BackColor = Color.Transparent

                TabPageMain.Visible = False
                TabPagePackage.Visible = False
                TabPageSetting.Visible = False
                TabPageMabiNotice.Visible = True
                TabPageProgInfo.Visible = False
                TabPage04.Visible = False
            ElseIf sender.GetHashCode = Label5.GetHashCode Then
                curTab = 5
                TabBtnMain.BackColor = Color.Transparent
                TabBtnPackage.BackColor = Color.Transparent
                TabBtnSetting.BackColor = Color.Transparent
                Label4.BackColor = Color.Transparent
                Label6.BackColor = Color.Transparent

                TabPageMain.Visible = False
                TabPagePackage.Visible = False
                TabPageSetting.Visible = False
                TabPageMabiNotice.Visible = False
                TabPageProgInfo.Visible = True
                TabPage04.Visible = False
            ElseIf sender.GetHashCode = Label6.GetHashCode Then
                curTab = 6
                TabBtnMain.BackColor = Color.Transparent
                TabBtnPackage.BackColor = Color.Transparent
                TabBtnSetting.BackColor = Color.Transparent
                Label4.BackColor = Color.Transparent
                Label5.BackColor = Color.Transparent

                TabPageMain.Visible = False
                TabPagePackage.Visible = False
                TabPageSetting.Visible = False
                TabPageMabiNotice.Visible = False
                TabPageProgInfo.Visible = False
                TabPage04.Visible = True
            End If
            sender.BackColor = returnRGB(0, 122, 204)
        End If
    End Sub

    Public curType As Integer = 1
    Private Sub InnerTabMEnter(sender As Object, e As EventArgs) Handles type_spc.MouseEnter, type_mgm.MouseEnter, type_chg.MouseEnter
        If (curType = 1 And sender.GetHashCode <> type_chg.GetHashCode) _
        Or (curType = 2 And sender.GetHashCode <> type_mgm.GetHashCode) _
        Or (curType = 3 And sender.GetHashCode <> type_spc.GetHashCode) Then
            sender.BackColor = returnRGB(28, 151, 234)
        End If
    End Sub

    Private Sub InnerTabMLeave(sender As Object, e As EventArgs) Handles type_spc.MouseLeave, type_mgm.MouseLeave, type_chg.MouseLeave
        If (curType = 1 And sender.GetHashCode <> type_chg.GetHashCode) _
        Or (curType = 2 And sender.GetHashCode <> type_mgm.GetHashCode) _
        Or (curType = 3 And sender.GetHashCode <> type_spc.GetHashCode) Then
            sender.BackColor = Color.Transparent
        End If
    End Sub
    Private Sub InnerTabMClick(sender As Object, e As EventArgs) Handles type_spc.Click, type_mgm.Click, type_chg.Click
        If (curType = 1 And sender.GetHashCode <> type_chg.GetHashCode) _
        Or (curType = 2 And sender.GetHashCode <> type_mgm.GetHashCode) _
        Or (curType = 3 And sender.GetHashCode <> type_spc.GetHashCode) Then
            If sender.GetHashCode = type_chg.GetHashCode Then
                curType = 1
                type_mgm.BackColor = Color.Transparent
                type_spc.BackColor = Color.Transparent

                TabList01.Visible = True
                TabList02.Visible = False
                TabList03.Visible = False

                TabList01.Focus()
            ElseIf sender.GetHashCode = type_mgm.GetHashCode Then
                curType = 2
                type_chg.BackColor = Color.Transparent
                type_spc.BackColor = Color.Transparent

                TabList01.Visible = False
                TabList02.Visible = True
                TabList03.Visible = False

                TabList02.Focus()
            ElseIf sender.GetHashCode = type_spc.GetHashCode Then
                curType = 3
                type_chg.BackColor = Color.Transparent
                type_mgm.BackColor = Color.Transparent

                TabList01.Visible = False
                TabList02.Visible = False
                TabList03.Visible = True

                TabList03.Focus()
            End If
            sender.BackColor = returnRGB(0, 122, 204)
        End If
    End Sub

    Private Sub tmp01(sender As Object, e As EventArgs) Handles downloadbtn.MouseEnter, listreflash.MouseEnter, deletebtn.MouseEnter, updatecheck.MouseEnter, closebtn.MouseEnter, mabistartbtn.MouseEnter, Label14.MouseEnter, Label12.MouseEnter, dellumiarte.MouseEnter, Label7.MouseEnter, Label8.MouseEnter, Label9.MouseEnter, Label32.MouseEnter, _
        Clbtn2.MouseEnter, loginbtn.MouseEnter
        sender.BackColor = returnRGB(63, 63, 65)
    End Sub

    Private Sub btnleave2(sender As Object, e As EventArgs) Handles Label7.MouseLeave, Label8.MouseLeave, Label9.MouseLeave
        sender.BackColor = returnRGB(37, 37, 38)
    End Sub

    Private Sub btnsLeave(sender As Object, e As EventArgs) Handles downloadbtn.MouseLeave, listreflash.MouseLeave, deletebtn.MouseLeave, updatecheck.MouseLeave, closebtn.MouseLeave, mabistartbtn.MouseLeave, Label14.MouseLeave, Label12.MouseLeave, dellumiarte.MouseLeave, Label32.MouseLeave, _
        loginbtn.MouseLeave, Clbtn2.MouseLeave
        sender.BackColor = Color.Transparent
    End Sub
    Private Sub tmp02(sender As Object, e As EventArgs) Handles downloadbtn.MouseDown, listreflash.MouseDown, deletebtn.MouseDown, updatecheck.MouseDown, closebtn.MouseDown, mabistartbtn.MouseDown, Label14.MouseDown, Label12.MouseDown, Label7.MouseDown, Label8.MouseDown, Label9.MouseDown, _
        Clbtn2.MouseDown, loginbtn.MouseDown
        sender.BackColor = returnRGB(0, 122, 204)
    End Sub
    Private Sub tmp03(sender As Object, e As EventArgs) Handles downloadbtn.MouseUp, listreflash.MouseUp, deletebtn.MouseUp, updatecheck.MouseUp, closebtn.MouseUp, mabistartbtn.MouseUp, Label14.MouseUp, Label12.MouseUp, Label7.MouseUp, Label8.MouseUp, Label9.MouseUp, _
        Clbtn2.MouseUp, loginbtn.MouseUp
        sender.BackColor = returnRGB(63, 63, 65)
    End Sub
    Private Sub closebtn_Click(sender As Object, e As EventArgs) Handles closebtn.Click
        If MsgBox("트레이 모드로 전환하려면 확인을, 종료하려면 취소를 눌러주세요. 이 설정은 저장됩니다.", MsgBoxStyle.Information Or MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Me.WindowState = FormWindowState.Minimized
            Me.ShowInTaskbar = False
        Else
            End
        End If
    End Sub

    Public Function initList()
        Me.TabList01.Items.Clear()
        Me.TabList02.Items.Clear()
        Me.TabList03.Items.Clear()
        Me.TmpList.Items.Clear()
        Return True
    End Function

    Public vSelectList As List(Of String) = New List(Of String)
    Public Function ListXMLparse(Optional ByVal flag As Boolean = False, Optional ByVal reflash As Boolean = False)
        Try

            Dim xmlDocument As New Xml.XmlDocument
            Dim updateCount As Integer = 0
            initList()

            If reflash = True Then
                If vxFS.dir_exists(vxConfig.dirme & "\Config\") = False Then
                    vxFS.Dir_Create(vxConfig.dirme & "\Config\")
                End If
                vxFS.backGroundDownloader(vxMabiCore.PackInfoURL & "?donorID=" & wlabID.Text & "&donorPW=" & vxMD5crypto.getMd5Hash(wlabPW.Text), vxConfig.dirme & "\Config\", "packinfo.nfo")
            End If

            xmlDocument.Load(vxConfig.dirme & "\Config\packinfo.nfo")
            Dim Packs As Xml.XmlNodeList = xmlDocument.SelectNodes("/Packs/PackInfo")

            For Each Pack As Xml.XmlElement In Packs

                Dim Type As String = Pack.GetAttribute("Type")
                Dim Filename As String = Pack.GetAttribute("Filename")
                Dim ENFilename As String = Pack.GetAttribute("ENFilename")
                Dim pDate As String = Pack.GetAttribute("Date")
                Dim Info As String = Pack.GetAttribute("Info")
                Dim ENInfo As String = Pack.GetAttribute("ENInfo")
                Dim Path As String = Pack.GetAttribute("Path")
                Dim Install As String = ""
                Dim RemotePath As String = Pack.GetAttribute("RemotePath")
                Dim PackageName As String = Pack.GetAttribute("PackageName")
                Dim ParentPack As String = Pack.GetAttribute("ParentPack")
                Dim PackageVer As String = Pack.GetAttribute("PackageVer")
                Dim BriefInfoUrl As String = Pack.GetAttribute("BriefInfoUrl")
                Dim ENBriefInfoUrl As String = Pack.GetAttribute("ENBriefInfoUrl")
                Dim vType As String = Pack.GetAttribute("vType").Replace("형", "")
                Dim FileSize As Long = Val(Pack.GetAttribute("FileSize"))

                Dim PackName As String = ""
                Dim InfoURL As String = ""

                Dim ServerFileVer As Double = Val(PackageVer)
                Dim ClientFileVer As Double = 0.0
                Dim PackExist As Boolean = False
                Dim PackInfoExist As Boolean = False
                Dim pathType As Boolean = False
                Dim selectpackage As Boolean = False

                If Path = "default" Then
                    pathType = True
                    PackExist = vxFS.file_exists(vxConfig.MabiDir & "\Package\wlab\" & PackageName)
                    PackInfoExist = vxFS.file_exists(vxConfig.MabiDir & "\Package\wlab\" & PackageName & ".xml")
                Else
                    pathType = False
                    PackExist = vxFS.file_exists(Path & PackageName)
                    PackInfoExist = vxFS.file_exists(Path & PackageName & ".xml")
                End If

                If vxLang.lang_flag = False Then
                    PackName = ENFilename
                    InfoURL = ENBriefInfoUrl
                End If

                If ParentPack <> "" Then
                    selectpackage = True
                    vSelectList.Add(Filename)
                End If

                If PackExist And PackInfoExist Then
                    Dim packPrivXML = New Xml.XmlDocument
                    If pathType Then
                        packPrivXML.Load(vxConfig.MabiDir & "\Package\wlab\" & PackageName & ".xml")
                    Else
                        packPrivXML.Load(Path & PackageName & ".xml")
                    End If

                    Dim loadedInfo As Xml.XmlNodeList = packPrivXML.SelectNodes("/PackInfo/Info")

                    For Each PrivInfo As Xml.XmlElement In loadedInfo
                        Dim myversion As String = PrivInfo.GetAttribute("PackageVer")
                        pDate = PrivInfo.GetAttribute("Date")
                        Install = PrivInfo.GetAttribute("Install")
                        ClientFileVer = Val(myversion)
                    Next

                    If ClientFileVer < ServerFileVer Then
                        Install = "업데이트 필요"
                    End If
                End If

                Dim tmpArr As String() = {Filename, pDate, Install, Path, RemotePath, PackageName, PackageVer, BriefInfoUrl, FileSize}
                Dim tmpItem = New ListViewItem(tmpArr)

                If ClientFileVer < ServerFileVer And PackExist = True Then
                    tmpItem.ForeColor = returnRGB(255, 127, 127)
                End If

                If vType = "교체" Then
                    TabList01.Items.Add(tmpItem)
                ElseIf vType = "관리" Then
                    TabList02.Items.Add(tmpItem)
                ElseIf vType = "선택설치" Then
                    TabList03.Items.Add(tmpItem)
                End If


                If vList.Contains(Pack.GetAttribute("PackageName").Trim.ToLower) = False Then
                    vList.Add(Pack.GetAttribute("PackageName").ToString.Trim.ToLower & ".pack")
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
        Return False
    End Function

    Public Function AllDisabled(Optional ByVal flag As Boolean = False)
        If flag = True Then
            downloadbtn.Enabled = False
            deletebtn.Enabled = False
            listreflash.Enabled = False
            mabistartbtn.Enabled = False
            updatecheck.Enabled = False
            Label14.Enabled = False
            closebtn.Enabled = False
            Return False
        Else
            downloadbtn.Enabled = True
            deletebtn.Enabled = True
            listreflash.Enabled = True
            mabistartbtn.Enabled = True
            updatecheck.Enabled = True
            Label14.Enabled = True
            closebtn.Enabled = True
            Return True
        End If
    End Function

    Public Function DownloadList(Optional ByVal Update As Boolean = False)

        Dim p() As Process
        p = Process.GetProcessesByName("Client")
        If p.Count = 0 Then
            If vxConfig.MabiDir <> "" Then
                Dim ClickTime As Date
                ClickTime = Date.Today

                AllDisabled(True)

                Dim listArr As New ArrayList
                Dim checkedItems As Integer = 0
                Dim TotalFileSize As Long = 0
                listArr.Add(TabList01.CheckedItems)
                listArr.Add(TabList02.CheckedItems)
                listArr.Add(TabList03.CheckedItems)

                checkedItems += TabList01.CheckedItems.Count
                checkedItems += TabList02.CheckedItems.Count
                checkedItems += TabList03.CheckedItems.Count

                TmpList.Items.Clear()
                If checkedItems > 0 Then
                    For index = 0 To listArr.Count - 1
                        For Each Item As ListViewItem In listArr(index)
                            Dim tmpArr As String() = {Item.SubItems(0).Text, Item.SubItems(1).Text, Item.SubItems(2).Text, Item.SubItems(3).Text, Item.SubItems(4).Text, Item.SubItems(5).Text, Item.SubItems(6).Text, Item.SubItems(7).Text, Item.SubItems(8).Text}

                            Dim tmpItem = New ListViewItem(tmpArr)
                            TmpList.Items.Add(tmpItem)

                            TotalFileSize += Val(Item.SubItems(8).Text)
                        Next
                    Next

                    Filecount = TotalFileSize
                    CurrCount = 0
                    CProgress1.Width = 0

                    For Each Item As ListViewItem In TmpList.Items
                        CurrentFileSize = Val(Item.SubItems(8).Text)
                        Dim installDir As String = Item.SubItems(3).Text
                        Dim RemotePath As String = Item.SubItems(4).Text
                        Dim PackageName As String = Item.SubItems(5).Text
                        Dim PackageVer As String = Item.SubItems(6).Text

                        Dim PackExist As Boolean = False
                        Dim PackInfoExist As Boolean = False
                        Dim pathType As Boolean = False

                        If installDir = "default" Then
                            pathType = True
                            PackExist = vxFS.file_exists(vxConfig.MabiDir & "\Package\wlab\" & PackageName)
                            PackInfoExist = vxFS.file_exists(vxConfig.MabiDir & "\Package\wlab\" & PackageName & ".xml")
                        Else
                            pathType = False
                            PackExist = vxFS.file_exists(installDir & PackageName)
                            PackInfoExist = vxFS.file_exists(installDir & PackageName & ".xml")
                        End If

                        Dim downloadDir As String
                        If pathType = True Then
                            downloadDir = vxConfig.MabiDir & "\Package\wlab\"
                        Else
                            downloadDir = installDir.Replace("(MABI_DIR)", vxConfig.MabiDir).Replace("(LUMIPACKDELETE)", vxConfig.MabiDir).Replace("(DIR_MY_MABINOGI)", My.Computer.FileSystem.SpecialDirectories.MyDocuments)
                        End If

                        If vxFS.dir_exists(downloadDir) = False Then
                            vxFS.Dir_Create(downloadDir)
                        End If

                        DownloadedFile = Item.SubItems(0).Text

                        If FileDownloadSystem(RemotePath, PackageName, downloadDir) = True Then
                            Dim FileInfo As String = ""
                            FileInfo &= "<?xml version=""1.0"" encoding=""utf-8""?>"
                            FileInfo &= "<PackInfo>"
                            FileInfo &= "<Info Type="""" Filename=""" & PackageName & """ Date=""" & ClickTime & """ Info="""" Install=""" & vxLang.xml_installedtext & """ PackageVer=""" & PackageVer & """ />"
                            FileInfo &= "</PackInfo>"

                            My.Computer.FileSystem.WriteAllText(downloadDir & PackageName & ".xml", FileInfo, False)
                        Else
                            CurrCount += CurrentFileSize
                            vxFS.FileDelete(downloadDir & PackageName)
                            vxFS.FileDelete(downloadDir & PackageName & ".xml")
                        End If

                    Next
                    resets()
                    MsgBox("다운로드가 완료되었습니다.", MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
                Else
                    MsgBox("설치할 팩파일을 선택해주시기 바랍니다.", MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
                    AllDisabled()
                End If
                Me.downloadbtn.BackColor = Color.Transparent
                AllDisabled()
                ListXMLparse()
            Else
                MsgBox("마비노기 설치경로가 등록되어 있지 않습니다. 화면 내의 경로 설정 기능을 통해 마비노기 경로를 다시 설정해주세요.", MsgBoxStyle.Information, "Not Found Mabinogi Install Path")
                Me.downloadbtn.BackColor = Color.Transparent
            End If
        Else
            MsgBox("마비노기가 실행되어 있습니다. 안전성을 위해 다운로드를 제한했습니다.")
        End If
        Return True
    End Function
    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Panel1.Click, MabiInstallDir.Click
        If vxConfig.MabiDir <> "" Then
            Process.Start(vxConfig.MabiDir)
        Else
            If dlgFolderBrowse.ShowDialog(Me) <> DialogResult.Cancel Then
                Dim stringtmp As String
                stringtmp = dlgFolderBrowse.SelectedPath

                If vxFS.file_exists(stringtmp & "\client.exe") And vxFS.file_exists(stringtmp & "\version.dat") Then
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "", stringtmp)
                    MsgBox("Registered! : " & stringtmp, MsgBoxStyle.Information)
                Else
                    MsgBox("마비노기가 설치된 경로가 아닙니다!" & vbNewLine & "This path isn't Mabinogi Installed Directory!", MsgBoxStyle.Critical)
                End If
            End If
            vxConfig.MabiDir = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "", "")
            Me.MabiInstallDir.Text = vxConfig.MabiDir
        End If
    End Sub

    Private Sub downloadbtn_Click(sender As Object, e As EventArgs) Handles downloadbtn.Click
        DownloadList()
    End Sub

    Private Sub label3_mClick(sender As Object, e As MouseEventArgs) Handles TmpTab.MouseClick, TabBtnPackage.MouseClick, Label4.MouseClick, Label5.MouseClick, Label6.MouseClick, TabBtnSetting.MouseClick, TabBtnLog.MouseClick, TabBtnMain.MouseClick

    End Sub

    Private Sub listreflash_Click(sender As Object, e As EventArgs) Handles listreflash.Click
        initList()
        ListXMLparse(False, True)
    End Sub

    Private Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click

        AllDisabled(True)

        Dim checkedCount As Integer = 0

        checkedCount += Me.TabList01.CheckedItems.Count
        checkedCount += Me.TabList02.CheckedItems.Count
        checkedCount += Me.TabList03.CheckedItems.Count

        If checkedCount = 0 Then
            MsgBox(vxLang.xml_selecterror)
            AllDisabled()
            Exit Sub
        End If

        Dim objArr As New ArrayList

        objArr.Add(Me.TabList01)
        objArr.Add(Me.TabList02)
        objArr.Add(Me.TabList03)

        Me.ListView1.Clear()

        For index = 0 To objArr.Count - 1
            For Each Item As ListViewItem In objArr(index).CheckedItems
                Dim tmpArr As String() = {Item.SubItems(0).Text, Item.SubItems(1).Text, Item.SubItems(2).Text, Item.SubItems(3).Text, Item.SubItems(4).Text, Item.SubItems(5).Text, Item.SubItems(6).Text, Item.SubItems(7).Text}
                Dim tmpItem = New ListViewItem(tmpArr)
                ListView1.Items.Add(tmpItem)
            Next
        Next

        '...위
        If My.Computer.FileSystem.DirectoryExists(vxConfig.MabiDir & "\package") = False Then

            MsgBox(vxLang.xml_mabiexsist)

            AllDisabled()
            Exit Sub

        End If

        For Each Item As ListViewItem In Me.ListView1.Items
            Dim tmpDirectory As String = vxConfig.MabiDir
            If Item.SubItems(3).Text = "default" Then
                tmpDirectory &= "\package\wlab\"
            Else
                tmpDirectory &= Item.SubItems(3).Text & "\"
            End If
            ' 팩 관련 파일들 삭제
            vxFS.FileDelete(tmpDirectory & Item.SubItems(5).Text)
            vxFS.FileDelete(tmpDirectory & Item.SubItems(5).Text & ".xml")

        Next
        AllDisabled()
        Me.downloadbtn.BackColor = Color.Transparent
        MsgBox("선택한 파일을 깨끗히 지웠습니다.", MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        ListXMLparse()
    End Sub

    Private Sub btnsUp(sender As Object, e As MouseEventArgs) Handles closebtn.MouseUp, deletebtn.MouseUp, downloadbtn.MouseUp, listreflash.MouseUp, mabistartbtn.MouseUp, updatecheck.MouseUp, Label14.MouseUp, Label12.MouseUp, dellumiarte.MouseUp, Label7.MouseUp, Label8.MouseUp, Label9.MouseUp
        sender.BackColor = Color.Transparent
    End Sub

    Private Sub btnsDown(sender As Object, e As MouseEventArgs) Handles closebtn.MouseDown, deletebtn.MouseDown, downloadbtn.MouseDown, listreflash.MouseDown, mabistartbtn.MouseDown, updatecheck.MouseDown, Label14.MouseDown, Label12.MouseDown, dellumiarte.MouseDown, Label7.MouseDown, Label8.MouseDown, Label9.MouseDown
        sender.BackColor = returnRGB(0, 122, 204)
    End Sub

    Private Sub mabistartbtn_Click(sender As Object, e As EventArgs) Handles mabistartbtn.Click
        MabiStart()
    End Sub

    Private Sub TabPage01_Paint(sender As Object, e As PaintEventArgs) Handles TabPagePackage.Paint

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\willowsPack", "mode", "Lite")
        Process.Start(vxConfig.dirme & "\willowspack.exe")
        End
    End Sub

    Private Sub TabList01_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabList01.ItemSelectionChanged, TabList02.ItemSelectionChanged, TabList03.ItemSelectionChanged
        Try
            If PackInfo.Visible = False Then
                PackInfo.Visible = True
            End If
            If sender.Items(sender.FocusedItem.Index).SubItems(7).Text <> "" Then

                Dim tmpURL As New System.Uri(sender.Items(sender.FocusedItem.Index).SubItems(7).Text)
                PackInfo.Url = tmpURL
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

        Dim tmpURL As New System.Uri("http://willowslab.com/packlistshop.php?user_id=" & wlabID.Text & "&password=" & wlabPW.Text)
        WLabLite.Url = tmpURL
    End Sub

    Public cnt As Integer = 0
    Private Sub TmpTab_Click(sender As Object, e As EventArgs) Handles TmpTab.Click
        If cnt > 21 Then
            MsgBox("쳇...어쩔 수 없지. 보여주겠어 실험실...!")
        Else
            MsgBox("준비중입니다. ^_^/ ", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles dellumiarte.Click
        If vxFS.dir_exists(vxConfig.MabiDir & "\package\Lumipack\") Then
            vxFS.Dir_Delete(vxConfig.MabiDir & "\package\lumipack\")
        End If
        If vxFS.file_exists(vxConfig.MabiDir & "\dinput8.dll") Then
            vxFS.File_Delete(vxConfig.MabiDir & "\dinput8.dll")
        End If
        If vxFS.file_exists(vxConfig.MabiDir & "\hsluncher.exe") Then
            vxFS.File_Delete(vxConfig.MabiDir & "\hsluncher.exe")
        End If
        MsgBox("루미팩을 완벽하게 삭제하였습니다.")
        Me.dellumiarte.Visible = False
    End Sub

    Public ascdesc01 As Integer = 0
    Public ascdesc02 As Integer = 0
    Public ascdesc03 As Integer = 0

    Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare

        If sort = "ASC" Then

            Return String.Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)

        Else

            Return String.Compare(CType(y, ListViewItem).SubItems(col).Text, CType(x, ListViewItem).SubItems(col).Text)

        End If

    End Function
    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        If curType = 1 Then
            If ascdesc01 = 0 Then
                ascdesc01 = 1
                TabList01.Sorting = SortOrder.Ascending
            Else
                ascdesc01 = 0
                TabList01.Sorting = SortOrder.Descending
            End If
            TabList01.Refresh()
        End If
        If curType = 2 Then
            If ascdesc02 = 0 Then
                ascdesc02 = 1
                TabList02.Sorting = SortOrder.Ascending
            Else
                ascdesc02 = 0
                TabList02.Sorting = SortOrder.Descending
            End If
            TabList02.Refresh()
        End If
        If curType = 3 Then
            If ascdesc03 = 0 Then
                ascdesc03 = 1
                TabList03.Sorting = SortOrder.Ascending
            Else
                ascdesc03 = 0
                TabList03.Sorting = SortOrder.Descending
            End If
            TabList03.Refresh()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)
        cnt += 1
    End Sub

    Public Function unlockReadOnly(ByVal path As String, Optional ByVal flag As Boolean = False)

        Dim Executable As String = "cmd.exe"
        Dim CommandLine As String = ""

        Try

            Dim mProcess As New Diagnostics.ProcessStartInfo(Executable, CommandLine)
            mProcess.UseShellExecute = False
            mProcess.RedirectStandardOutput = True
            mProcess.RedirectStandardInput = True
            mProcess.CreateNoWindow = True
            mProcess.WindowStyle = ProcessWindowStyle.Hidden
            mProcess.Verb = "runas"

            Dim MyProcess As New Diagnostics.Process
            MyProcess.StartInfo = mProcess
            MyProcess.Start()

            If flag = False Then
                MyProcess.StandardInput.Write("attrib -r " & path)
            Else
                MyProcess.StandardInput.Write("attrib +r " & path)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub Label15_Click_1(sender As Object, e As EventArgs)
        vxFS.getFiles(vxConfig.MabiDir & "\package", True)
    End Sub

    Private Sub updatecheck_Click(sender As Object, e As EventArgs) Handles updatecheck.Click

        Dim p() As Process
        p = Process.GetProcessesByName("Client")
        If p.Count = 0 Then
            Me.ListXMLparse(False, True)
            If vxConfig.MabiDir <> "" Then
                Dim ClickTime As Date
                ClickTime = Date.Today

                AllDisabled(True)

                Dim listArr As New ArrayList
                Dim checkedItems As Integer = 0
                Dim TotalFileSize As Long = 0
                listArr.Add(TabList01.Items)
                listArr.Add(TabList02.Items)
                listArr.Add(TabList03.Items)

                TmpList.Items.Clear()
                For index = 0 To listArr.Count - 1
                    For Each Item As ListViewItem In listArr(index)
                        Dim tmpArr As String() = {Item.SubItems(0).Text, Item.SubItems(1).Text, Item.SubItems(2).Text, Item.SubItems(3).Text, Item.SubItems(4).Text, Item.SubItems(5).Text, Item.SubItems(6).Text, Item.SubItems(7).Text, Item.SubItems(8).Text}

                        If Item.SubItems(2).Text = "업데이트 필요" Then
                            Dim tmpItem = New ListViewItem(tmpArr)
                            TmpList.Items.Add(tmpItem)
                            TotalFileSize += Val(Item.SubItems(8).Text)
                        End If
                    Next
                Next

                Filecount = TotalFileSize
                CurrCount = 0
                CProgress1.Width = 0

                If TmpList.Items.Count = 0 Then
                    MsgBox("업데이트된 패키지가 없습니다.", MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
                    AllDisabled()
                    Exit Sub
                End If

                For Each Item As ListViewItem In TmpList.Items
                    CurrentFileSize = Val(Item.SubItems(8).Text)
                    Dim installDir As String = Item.SubItems(3).Text
                    Dim RemotePath As String = Item.SubItems(4).Text
                    Dim PackageName As String = Item.SubItems(5).Text
                    Dim PackageVer As String = Item.SubItems(6).Text

                    Dim PackExist As Boolean = False
                    Dim PackInfoExist As Boolean = False
                    Dim pathType As Boolean = False

                    If installDir = "default" Then
                        pathType = True
                        PackExist = vxFS.file_exists(vxConfig.MabiDir & "\Package\wlab\" & PackageName)
                        PackInfoExist = vxFS.file_exists(vxConfig.MabiDir & "\Package\wlab\" & PackageName & ".xml")
                    Else
                        pathType = False
                        PackExist = vxFS.file_exists(installDir & PackageName)
                        PackInfoExist = vxFS.file_exists(installDir & PackageName & ".xml")
                    End If

                    Dim downloadDir As String
                    If pathType = True Then
                        downloadDir = vxConfig.MabiDir & "\Package\wlab\"
                    Else
                        downloadDir = installDir.Replace("(MABI_DIR)", vxConfig.MabiDir).Replace("(LUMIPACKDELETE)", vxConfig.MabiDir).Replace("(DIR_MY_MABINOGI)", My.Computer.FileSystem.SpecialDirectories.MyDocuments)
                    End If

                    If vxFS.dir_exists(downloadDir) = False Then
                        vxFS.Dir_Create(downloadDir)
                    End If

                    DownloadedFile = Item.SubItems(0).Text

                    If FileDownloadSystem(RemotePath, PackageName, downloadDir) = True Then
                        Dim FileInfo As String = ""
                        FileInfo &= "<?xml version=""1.0"" encoding=""utf-8""?>"
                        FileInfo &= "<PackInfo>"
                        FileInfo &= "<Info Type="""" Filename=""" & PackageName & """ Date=""" & ClickTime & """ Info="""" Install=""" & vxLang.xml_installedtext & """ PackageVer=""" & PackageVer & """ />"
                        FileInfo &= "</PackInfo>"

                        My.Computer.FileSystem.WriteAllText(downloadDir & PackageName & ".xml", FileInfo, False)
                    Else
                        CurrCount += CurrentFileSize
                        vxFS.FileDelete(downloadDir & PackageName)
                        vxFS.FileDelete(downloadDir & PackageName & ".xml")
                    End If

                Next
                resets()
                MsgBox("다운로드가 완료되었습니다.", MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
                Me.downloadbtn.BackColor = Color.Transparent
                AllDisabled()
                ListXMLparse()
            Else
                MsgBox("마비노기 설치경로가 등록되어 있지 않습니다. 화면 내의 경로 설정 기능을 통해 마비노기 경로를 다시 설정해주세요.", MsgBoxStyle.Information, "Not Found Mabinogi Install Path")
                Me.downloadbtn.BackColor = Color.Transparent
            End If
        Else
            MsgBox("마비노기가 실행되어 있습니다. 안전성을 위해 다운로드를 제한했습니다.")
        End If
    End Sub

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

    Private Sub Label2_Click() Handles Label2.MouseDown
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub windowMove() Handles Label2.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub windowStop() Handles Label2.MouseUp
        drag = False
    End Sub
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 요리도우미ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 요리도우미ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 마비노기실행ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 창활성화ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 종료ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Private Sub 종료ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 종료ToolStripMenuItem.Click
        TrayIcon.Dispose()
        End
    End Sub

    Private Sub 요리도우미ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles 요리도우미ToolStripMenuItem1.Click
        vxCookTray.Show()
    End Sub

    Private Sub FormUnload() Handles Me.Disposed
        End
    End Sub
    Friend WithEvents Label32 As System.Windows.Forms.Label

    Private Sub tmp03(sender As Object, e As MouseEventArgs) Handles Label32.MouseUp, closebtn.MouseUp, deletebtn.MouseUp, downloadbtn.MouseUp, Label12.MouseUp, Label14.MouseUp, Label7.MouseUp, Label8.MouseUp, Label9.MouseUp, listreflash.MouseUp, mabistartbtn.MouseUp, updatecheck.MouseUp, Clbtn2.MouseUp, loginbtn.MouseUp

    End Sub
    Private Sub tmp02(sender As Object, e As MouseEventArgs) Handles Label32.MouseDown, closebtn.MouseDown, deletebtn.MouseDown, downloadbtn.MouseDown, Label12.MouseDown, Label14.MouseDown, Label7.MouseDown, Label8.MouseDown, Label9.MouseDown, listreflash.MouseDown, mabistartbtn.MouseDown, updatecheck.MouseDown, Clbtn2.MouseDown, loginbtn.MouseDown

    End Sub

    Private Sub Label32_Click() Handles Label32.Click
        Panel6.Visible = True
    End Sub
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents loginbtn As System.Windows.Forms.Label

    Private Sub Label34_Click(sender As Object, e As EventArgs) Handles loginbtn.Click
        Panel6.Visible = False
        wlabID.Text = RichTextBox1.Text
        wlabPW.Text = TextBox2.Text
        ListXMLparse(False, True)
    End Sub
    Friend WithEvents LoginFormTitle As System.Windows.Forms.Label
    Friend WithEvents Clbtn2 As System.Windows.Forms.PictureBox

    Private Sub Clbtn2_Click(sender As Object, e As EventArgs) Handles Clbtn2.Click
        Panel6.Visible = False
    End Sub

    Private Sub 창활성화ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 창활성화ToolStripMenuItem.Click
        Me.Show()
        Me.Focus()
        Me.TopMost = True
        System.Threading.Thread.Sleep(10)
        Me.TopMost = False
        Me.ShowInTaskbar = True
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub
End Class
