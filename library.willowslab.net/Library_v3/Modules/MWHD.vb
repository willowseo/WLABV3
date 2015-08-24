Imports System.Runtime.InteropServices

Public Module MWHD
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
    Public Sub killMabi()

    End Sub

    Public Sub checkpattern(Optional ByVal border As Boolean = True)
        Dim subNum As Integer = Process.GetProcessesByName("Client").Count
        If subNum <= 1 Then
            fullScreen(border)
        Else
            Dim mtparentn As Integer = Math.Ceiling(subNum / Math.Ceiling(subNum ^ 0.5))
            Dim parentn As Integer = Math.Ceiling(subNum ^ 0.5)
            Dim Mwidth As Integer = Math.Floor(Screen.PrimaryScreen.WorkingArea.Width / parentn)
            Dim Mheight As Integer = Math.Floor(Screen.PrimaryScreen.WorkingArea.Height / mtparentn)
            Dim i As Integer = 0
            Dim [classname] As String = Strings.Space(256)
            For Each mabinogi As Process In Process.GetProcessesByName("Client")
                GetClassName(mabinogi.MainWindowHandle, [classname], 256)
                If [classname].IndexOf("Mabinogi") > -1 Then
                    ShowWindow(mabinogi.MainWindowHandle, 1)
                    Dim tempX As Integer = i Mod parentn
                    Dim tempY As Integer = Math.Floor(i / parentn)
                    i += 1
                    SetWindowText(mabinogi.MainWindowHandle, "Mabinogi No." & i)
                    If border Then
                        SetWindowLong(mabinogi.MainWindowHandle, -16, CType(349044736, IntPtr))
                    Else
                        Dim value As WindowStyles = CType(276824064L, WindowStyles)
                        SetWindowLong(mabinogi.MainWindowHandle, -16, CType((CLng(value)), IntPtr))
                    End If
                    SetWindowPos(mabinogi.MainWindowHandle, 0, Screen.PrimaryScreen.WorkingArea.X + (Mwidth * tempX), Screen.PrimaryScreen.WorkingArea.Y + (Mheight * tempY), Mwidth, Mheight, 0)
                End If
            Next
        End If
    End Sub

    Public Sub fullScreen(Optional ByVal border As Boolean = True)
        If Process.GetProcessesByName("Client").Count = 0 Then
            MsgBox("실행된 마비노기가 없습니다.", MsgBoxStyle.Exclamation Or vbMsgBoxSetForeground, "오류")
        Else
            Dim i As Integer = 1
            Dim [classname] As String = Strings.Space(256)
            For Each mabinogi As Process In Process.GetProcessesByName("Client")
                GetClassName(mabinogi.MainWindowHandle, [classname], 256)
                If [classname].IndexOf("Mabinogi") > -1 Then
                    ShowWindow(mabinogi.MainWindowHandle, 1)
                    i += 1
                    SetWindowText(mabinogi.MainWindowHandle, "Mabinogi No." & i)
                    If border Then
                        SetWindowLong(mabinogi.MainWindowHandle, -16, CType(349044736, IntPtr))
                    Else
                        Dim value As WindowStyles = CType(276824064L, WindowStyles)
                        SetWindowLong(mabinogi.MainWindowHandle, -16, CType((CLng(value)), IntPtr))
                    End If
                    SetWindowPos(mabinogi.MainWindowHandle, 0, Screen.PrimaryScreen.WorkingArea.Location.X, Screen.PrimaryScreen.WorkingArea.Location.Y, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height, 0)
                End If
            Next
        End If
    End Sub

    Public Sub setWindow()
        ShowWindow(windowController.proc, 1)
        SetWindowText(windowController.proc, windowController.titlename.Text)
        If windowController.CheckBox1.Checked Then
            SetWindowLong(windowController.proc, -16, CType(349044736, IntPtr))
        Else
            Dim value As WindowStyles = CType(276824064L, WindowStyles)
            SetWindowLong(windowController.proc, -16, CType((CLng(value)), IntPtr))
        End If
        SetWindowPos(windowController.proc, 0, Val(windowController.winX.Text), Val(windowController.winY.Text), Val(windowController.winWidth.Text), Val(windowController.winHeight.Text), 0)
        SetForegroundWindow(windowController.proc)
        windowController.MabiList()
    End Sub

    Public Sub getWindow()
        Dim rECT As RECT
        Dim nIdx As Integer = GetWindowLong(windowController.proc, WindowLongFlags.GWL_STYLE)
        Dim value As WindowStyles = CType(276824064L, WindowStyles)
        Try
            windowController.titlename.Text = windowController.ListView1.Items(windowController.ListView1.FocusedItem.Index).SubItems(0).Text
            GetWindowRect(windowController.proc, rECT)
            GetWindowLong(windowController.proc, 0)
            windowController.winX.Text = rECT.left
            windowController.winY.Text = rECT.top
            windowController.winWidth.Text = rECT.right - rECT.left
            windowController.winHeight.Text = rECT.bottom - rECT.top
            If nIdx = 343932928 Then
                windowController.CheckBox1.Checked = False
            Else
                windowController.CheckBox1.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub forceFocused()
        Try
            Dim tmp As IntPtr = windowController.tmp.MainWindowHandle
            ShowWindow(tmp, 1)
            SetForegroundWindow(tmp)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub forceMinimized()
        Try
            Dim tmp As IntPtr = windowController.tmp.MainWindowHandle
            ShowWindow(tmp, 2)
        Catch ex As Exception
        End Try
    End Sub
End Module