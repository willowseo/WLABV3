Imports System.Threading
Imports System.IO
Imports System.Text
Imports System.ComponentModel


Public Class Init

    Public Shared splashImg As Bitmap = Global.버들라이브러리.My.Resources.Resources.lolipack

    Public Shared Function SplashImage(text As String, Optional percent As Integer = 0) As Bitmap
        If DateTime.Now.Month = 4 And DateTime.Now.Day = 1 Then
            Dim bitmap As New Bitmap(700, 500)
            Dim g As Graphics = Graphics.FromImage(bitmap)
            g.DrawImage(Global.버들라이브러리.My.Resources.Resources.lolipack, New Rectangle(0, 0, bitmap.Width, bitmap.Height), New Rectangle(0, 0, 700, 500), GraphicsUnit.Pixel)
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
            g.DrawString(text, New Font("맑은 고딕", 13), Brushes.White, New PointF(40, 288))
            g.DrawRectangle(New Pen(Color.FromArgb(200, 9, 147, 234), 6), New Rectangle(44, 269, CInt((CSng(percent) / 100) * 297), 6))
            g.DrawString(percent.ToString() + "%", New Font("맑은 고딕", 8), Brushes.Black, New PointF(172, 264))
            Return bitmap
        Else
            Dim bitmap As New Bitmap(300, 300)
            Dim g As Graphics = Graphics.FromImage(bitmap)
            g.DrawImage(Global.버들라이브러리.My.Resources.Resources.wlab2, New Rectangle(0, 0, bitmap.Width, bitmap.Height), New Rectangle(0, 0, 300, 300), GraphicsUnit.Pixel)

            Return bitmap
        End If
    End Function


    Private haveHandle As Boolean = False
    Protected Overrides Sub OnClosing(e As CancelEventArgs)
        If DateTime.Now.Month = 4 And DateTime.Now.Day = 1 Then
            e.Cancel = True
            MyBase.OnClosing(e)
            haveHandle = False
        End If
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        Me.Size = New Size(700, 500)
        InitializeStyles()
        MyBase.OnHandleCreated(e)
        haveHandle = True
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cParms As CreateParams = MyBase.CreateParams
            cParms.ExStyle = cParms.ExStyle Or &H80000
            ' WS_EX_LAYERED
            Return cParms
        End Get
    End Property

    Private Sub InitializeStyles()
        If DateTime.Now.Month = 4 And DateTime.Now.Day = 1 Then
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.UserPaint, True)
            UpdateStyles()
        End If
    End Sub


    Public Sub SetBits(bitmap__1 As Bitmap)
        If Not haveHandle Then
            Return
        End If

        If Not Bitmap.IsCanonicalPixelFormat(bitmap__1.PixelFormat) OrElse Not Bitmap.IsAlphaPixelFormat(bitmap__1.PixelFormat) Then
            Throw New ApplicationException("The picture must be 32bit picture with alpha channel.")
        End If

        Dim oldBits As IntPtr = IntPtr.Zero
        Dim screenDC As IntPtr = Win32.GetDC(IntPtr.Zero)
        Dim hBitmap As IntPtr = IntPtr.Zero
        Dim memDc As IntPtr = Win32.CreateCompatibleDC(screenDC)

        Try
            Dim topLoc As New Win32.Point(Left, Top)
            Dim bitMapSize As New Win32.Size(bitmap__1.Width, bitmap__1.Height)
            Dim blendFunc As New Win32.BLENDFUNCTION()
            Dim srcLoc As New Win32.Point(0, 0)

            hBitmap = bitmap__1.GetHbitmap(Color.FromArgb(0))
            oldBits = Win32.SelectObject(memDc, hBitmap)

            blendFunc.BlendOp = Win32.AC_SRC_OVER
            blendFunc.SourceConstantAlpha = 255
            blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA
            blendFunc.BlendFlags = 0

            Win32.UpdateLayeredWindow(Handle, screenDC, topLoc, bitMapSize, memDc, srcLoc, _
                0, blendFunc, Win32.ULW_ALPHA)
        Finally
            If hBitmap <> IntPtr.Zero Then
                Win32.SelectObject(memDc, oldBits)
                Win32.DeleteObject(hBitmap)
            End If
            Win32.ReleaseDC(IntPtr.Zero, screenDC)
            Win32.DeleteDC(memDc)
        End Try
    End Sub


    Public Sub Init()
        Try
            If Not Directory.Exists(WLAB.CONF) Then
                Directory.CreateDirectory(WLAB.CONF)
            End If
            If Not File.Exists(WLAB.ROOT & "DevIL.Net.dll") Then
                File.WriteAllBytes(WLAB.ROOT & "DevIL.Net.dll", My.Resources.Resources.DevIL_NET)
            End If
        Catch ex As Exception
            MsgBox("이미지 라이브러리 생성에 실패했습니다. 계속해서 이런 문제가 발생하는 경우 백신의 설정에서 예외처리를 해보시기 바랍니다. 그럼에도 계속 이 문제가 발생한다면 문의해 주시기 바랍니다.")
        End Try

        Try
            If Not Directory.Exists(WLAB.CONF) Then
                Directory.CreateDirectory(WLAB.CONF)
            End If
            If Not File.Exists(WLAB.ROOT & "SevenZipSharp.dll") Then
                File.WriteAllBytes(WLAB.ROOT & "SevenZipSharp.dll", My.Resources.Resources.SevenZipSharp)
            End If
        Catch ex As Exception
            MsgBox("압축 라이브러리 생성에 실패했습니다. 계속해서 이런 문제가 발생하는 경우 백신의 설정에서 예외처리를 해보시기 바랍니다. 그럼에도 계속 이 문제가 발생한다면 문의해 주시기 바랍니다.")
        End Try

        Try
            If Not Directory.Exists(WLAB.CONF) Then
                Directory.CreateDirectory(WLAB.CONF)
            End If
            If Not File.Exists(WLAB.CONF & "7zx86.dll") Then
                File.WriteAllBytes(WLAB.CONF & "7zx86.dll", My.Resources.Resources._7z)
            End If
            If Not File.Exists(WLAB.CONF & "7zx64.dll") Then
                File.WriteAllBytes(WLAB.CONF & "7zx64.dll", My.Resources.Resources._7z64)
            End If
        Catch ex As Exception
            MsgBox("압축 라이브러리 생성에 실패했습니다. 계속해서 이런 문제가 발생하는 경우 백신의 설정에서 예외처리를 해보시기 바랍니다. 그럼에도 계속 이 문제가 발생한다면 문의해 주시기 바랍니다.")
        End Try
    End Sub
    Delegate Sub TrdCB(text As String)

    Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hWnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long
    Declare Function Layer Lib "user32" Alias "SetLayeredWindowAttributes" (ByVal hWnd As Long, ByVal crKey As Long, ByVal bAlpha As Byte, ByVal dwFlags As Long) As Long
    Private Delegate Sub Dword(ByVal str As String, ByVal perc As Integer)
    Private Sub prog(ByVal text As String)
        If Me.Label1.InvokeRequired Then
            Dim work As New TrdCB(AddressOf prog)
            Me.Invoke(work, New Object() {text})
        Else
            Me.Label1.Text = text
        End If
    End Sub
    Private Sub prog2(ByVal text As String, ByVal perc As Integer)
        If Me.Label1.InvokeRequired Then
            Dim work As New Dword(AddressOf prog2)
            Me.Invoke(work, New Object() {text, perc})
        Else
            SetBits(SplashImage(text, perc))
        End If
    End Sub
    Private Sub Start_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If DateTime.Now.Month = 4 And DateTime.Now.Day = 1 Then
            Me.Size = New Size(700, 500)
            Me.Top = Screen.PrimaryScreen.WorkingArea.Height / 2 - 250
            Me.Left = Screen.PrimaryScreen.WorkingArea.Width / 2 - 350
            SetBits(SplashImage("초기화 중...", 10))
        Else
            Me.Size = New Size(300, 300)
            Me.Top = Screen.PrimaryScreen.WorkingArea.Height / 2 - 150
            Me.Left = Screen.PrimaryScreen.WorkingArea.Width / 2 - 150
            SetBits(SplashImage("초기화 중...", 10))
        End If

        If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
            End
        End If

        For Each Path As String In Directory.GetFiles(WLAB.MYDOC + "마비노기\캐시")
            If InStr(Path.ToLower, "검은검사키리토@mabikr5") Or InStr(Path.ToLower, "빛의루라바다@mabikr5") Or InStr(Path.ToLower, "가람달@mabikr1") Then
                MsgBox("프로그램 사용 금지 목록에 해당되는 데이터가 감지되어 버들 실험실을 종료합니다. " & vbCrLf & "자세한 사항은 문의처를 통해 문의해 주시기 바랍니다.", MsgBoxStyle.Critical Or vbMsgBoxSetForeground, "버들 실험실 패키지 라이브러리")
                Directory.Delete(Setting.Mabi & "\package\lib", True)
                End
            End If
        Next

        If My.User.IsInRole("Administrators") = False Then
            MsgBox("관리자 권한으로 실행해주시기 바랍니다.", MsgBoxStyle.Information Or vbMsgBoxSetForeground)
            End
        End If
        Init()

        If Not Func.ExistsFile(Setting.Dir & "\data\version.dat") Then
            Auth.Show()
            Me.Close()
            Exit Sub
        End If

        Directory.CreateDirectory(WLAB.LABS)
        My.Computer.Audio.Play(Global.버들라이브러리.My.Resources.Resources.emotion_success, AudioPlayMode.Background)
        Dim trd As New Thread(New ThreadStart(AddressOf init_step1))
        trd.IsBackground = True
        trd.Start()
    End Sub

    Public Sub init_step1()

        If Not Directory.Exists(WLAB.CONF) Then
            Directory.CreateDirectory(WLAB.CONF)
        End If

        For Each this As Process In Process.GetProcessesByName("버들라이브러리")
            MWHD.SetForegroundWindow(this.MainWindowHandle)
        Next
        init_step2(True)
    End Sub

    Private Sub ribonshow(ByVal txt As String)
        If Me.InvokeRequired Then
            Me.Invoke(New TrdCB(AddressOf ribonshow), New Object() {txt})
        Else
        End If
    End Sub

    Public Sub init_step2(Optional ByVal isStart As Boolean = False)
        prog("Program Personal ID 확인중...")
        ribonshow("")
        prog("패키지 인스턴스 초기화 중...")

        If Directory.Exists(Setting.MabiTest & "\Package\") Then

        End If
        If isStart Then
            prog2("버전 확인 중...", 25)
            prog("버전 확인 중...")
            If Not Func.ExistsFile(Setting.Dir & "\data\version.dat") Then
                Auth.Show()
                Me.Close()
            Else
                prog("버전 유효성 체크 중...")
                Me.trd = New Threading.Thread(New Threading.ThreadStart(AddressOf Me.trdFunc))
                Me.trd.Start()
            End If
        End If
    End Sub


    Private Sub trdFunc()
        If Auth.webClient("http://library.willowslab.net/auth/?val=updatePhase", Setting.Dir & "\data\", "auth_temp.xml") Then
            Try
                Dim xmlDoc As New Xml.XmlDocument
                Dim listExtract As Xml.XmlNodeList
                Dim ByteCount As Integer = 4
                Dim HexCode As String = Nothing
                Dim HexCodeCount As Integer = Nothing
                Dim ReserseHexCode = ""
                Dim bytes(ByteCount - 1) As Byte
                Dim myv As Integer = 0
                xmlDoc.Load(Setting.Dir & "\data\auth_temp.xml")
                listExtract = xmlDoc.SelectNodes("/Auth/VersionInfo")

                For Each tmp As Xml.XmlElement In listExtract
                    myv = CType(Val(tmp.GetAttribute("Version")), Integer)
                    HexCode = Hex(tmp.GetAttribute("Version"))
                Next

                HexCodeCount = HexCode.Length

                For i = 0 To (ByteCount * 2 - 1) - HexCodeCount
                    HexCode = "0" & HexCode
                Next
                For i = (ByteCount) - 1 To 0 Step -1
                    ReserseHexCode &= HexCode.Substring(i * 2, 2)
                Next
                For i As Integer = 0 To ByteCount - 1
                    bytes(i) = Convert.ToByte(ReserseHexCode.Substring(i * 2, 2), 16)
                Next

                If myver < myv Then
                    prog2("소프트웨어의 패치를 진행합니다...", 75)
                    prog("소프트웨어의 패치를 진행합니다...")
                    Try
                        If Func.webClient("http://library.willowslab.net/patch/" & myv & "/updater.exe", Application.StartupPath, "\updater.exe") Then

                        Else
                            Throw New Exception("Error")
                        End If
                        If Func.webClient("http://library.willowslab.net/patch/" & myv & "/library.willowslab.net.exe", Application.StartupPath, "\" & Application.ExecutablePath.Replace(WLAB.ROOT, "") & ".tmp") Then

                        Else
                            Throw New Exception("Error")
                        End If
                    Catch ex As Exception
                        MsgBox("업데이트에 실패했습니다. 다시 실행하여 시도해주세요.", MsgBoxStyle.Information)
                        End
                    End Try

                    Shell(WLAB.ROOT & "updater.exe del:" & Application.ExecutablePath.Replace(WLAB.ROOT, "") & "," & Application.ExecutablePath.Replace(WLAB.ROOT, "") & ".tmp")
                    End
                Else
                    Thread.Sleep(500)
                    prog2("마비노기 패치 확인 중...", 30)
                    Thread.Sleep(2000)
                    Dim tmp As Integer = 0
                    For Each str As String In Directory.GetFiles(Path.Combine(Setting.Mabi, "package"))
                        prog2(str.Substring(str.LastIndexOf("\") + 1), 35 + tmp)
                        tmp += 2
                    Next
                    Thread.Sleep(500)
                    prog2("인스턴스 초기화 중...", 90)
                    trd_end("")
                End If
                My.Computer.FileSystem.WriteAllBytes(Setting.Dir & "\data\version.dat", bytes, True)
                IO.File.Delete(Setting.Dir & "\data\auth_temp.xml")
            Catch ex As Exception
                Console.WriteLine(ex.GetBaseException.ToString)
                Func.FSmsg("프로그램 버전을 읽어오는 데 실패했습니다. 계속해서 이런 현상이 발생할 경우 문의해주시기 바랍니다." & ex.GetBaseException.ToString)
                End
            End Try
        End If
    End Sub

    Private Sub trd_end(ByVal text As String)
        If Me.InvokeRequired Then
            Dim work As New TrdCB(AddressOf trd_end)
            Me.Invoke(work, New Object() {text})
        Else
            Try
                Dim finfo As New FileInfo(WLAB.ROOT & "\userdata\userdata.usd")
                If Not File.Exists(WLAB.ROOT & "\userdata\userdata.usd") Or finfo.Length < 144 Then
                    prog("유저를 등록하고 있습니다...")
                    NewUser.Show()
                    Me.Close()
                    Me.Dispose()
                Else
                    prog("잠시 뒤 실행됩니다.")
                    FrmMain.Show()
                    Me.Close()
                    Me.Dispose()
                End If
            Catch ex As Exception
                prog("유저를 등록하고 있습니다...")
                NewUser.Show()
                Me.Close()
                Me.Dispose()
            End Try
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Static tick As Integer
        tick += 1
        If tick = 2 Then

        End If
        If tick = 4 Then
            If Not Func.ExistsFile(Setting.Dir & "\data\version.dat") Then
                Auth.Show()
                Me.Close()
                Me.Dispose()
            Else
                FrmMain.Show()
                Me.Close()
                Me.Dispose()
            End If
        End If
    End Sub
End Class