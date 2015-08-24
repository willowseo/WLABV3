Public Class vxLog

    Inherits System.Windows.Forms.Form
    Private WithEvents logdownloader As vxWebFileDownloader

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(vxLog))
        Me.debugwindow = New System.Windows.Forms.TextBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'debugwindow
        '
        Me.debugwindow.BackColor = System.Drawing.Color.White
        Me.debugwindow.Location = New System.Drawing.Point(8, 31)
        Me.debugwindow.Multiline = True
        Me.debugwindow.Name = "debugwindow"
        Me.debugwindow.ReadOnly = True
        Me.debugwindow.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.debugwindow.Size = New System.Drawing.Size(284, 129)
        Me.debugwindow.TabIndex = 0
        Me.debugwindow.TabStop = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(8, 159)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(284, 12)
        Me.ProgressBar1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(277, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "×"
        '
        'Log
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(300, 180)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.debugwindow)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Log"
        Me.Opacity = 0.9R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Log"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents debugwindow As System.Windows.Forms.TextBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar

    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.DoEvents()
        Application.Run(New vxFrmMain)
        Application.Exit()
    End Sub
    'returns all text from last "/" in URL, but puts a "\" in front of the file name..
    Private Function GetFileNameFromURL(ByVal URL As String) As String
        If URL.IndexOf("/"c) = -1 Then Return String.Empty

        Return "\" & URL.Substring(URL.LastIndexOf("/"c) + 1)
    End Function

    'FIRES WHEN WE HAVE GOTTEN THE DOWNLOAD SIZE, SO WE KNOW WHAT BOUNDS TO GIVE THE PROGRESS BAR
    Private Sub _Downloader_FileDownloadSizeObtained(ByVal iFileSize As Long) Handles logdownloader.FileDownloadSizeObtained
        ProgressBar1.Maximum = Convert.ToInt32(iFileSize)
        ProgressBar1.Value = 0
    End Sub

    'FIRES WHEN DOWNLOAD IS COMPLETE
    Private Sub _Downloader_FileDownloadComplete() Handles logdownloader.FileDownloadComplete
        If ProgressBar1.Value < ProgressBar1.Maximum Then
            ProgressBar1.Value = ProgressBar1.Value + 1
        End If
        ProgressBar1.Value = ProgressBar1.Maximum
        'MessageBox.Show("File Download Complete")
    End Sub

    'FIRES WHEN DOWNLOAD FAILES. PASSES IN EXCEPTION INFO
    Private Sub _Downloader_FileDownloadFailed(ByVal ex As System.Exception) Handles logdownloader.FileDownloadFailed
        MessageBox.Show("An error has occured during download: " & ex.Message)
    End Sub

    'FIRES WHEN MORE OF THE FILE HAS BEEN DOWNLOADED
    Private Sub _Downloader_AmountDownloadedChanged(ByVal iNewProgress As Long) Handles logdownloader.AmountDownloadedChanged
        ProgressBar1.Value = Convert.ToInt32(iNewProgress)
        'lblProgress.Text = "[" & ProgressBar1.Value & "/" & ProgressBar1.Maximum & "] " & nowDownloaded & LanguageFile.downloading & " " & WebFileDownloader.FormatFileSize(iNewProgress) & "/ " & WebFileDownloader.FormatFileSize(ProgressBar1.Maximum)
        Application.DoEvents()
    End Sub

    Private Sub Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim vertmp As Long = (vxStart.myver - (vxStart.myver Mod 100)) / 100
        appendLog(vertmp & " 버전을 사용하고 있습니다.")

        Dim windowSize As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea
        Dim winSize As System.Drawing.Rectangle = Screen.PrimaryScreen.Bounds

        Dim myHeight As Integer = windowSize.Height
        Dim myWidth As Integer = windowSize.Width

        Me.SetDesktopLocation(0, 0)
        Dim winScaleA As System.Drawing.Point = Me.Location
        Dim winScaleB As System.Drawing.Point
        appendLog(winScaleA.X & ", " & winScaleA.Y)
        appendLog("to")
        If winScaleA.X > 0 Then
            myWidth += winScaleA.X
        End If

        Me.Location = New System.Drawing.Point(myWidth - 300, myHeight - 180)
        winScaleB = Me.Location
        appendLog(winScaleB.X & ", " & winScaleB.Y)
        appendLog("실제 작업 영역")
        appendLog(winScaleA.X & "~" & winScaleB.X + 300)
        appendLog(winScaleA.Y & "~" & winScaleB.Y + 180)
    End Sub

    Public Function logFileDownloader(ByVal URL, ByVal Filename, ByVal dir)
        Try
            Dim downloadPath As String = dir.TrimEnd("\"c)
            If vxFS.Dir_Exists(downloadPath) Then

            End If
            appendLog(Filename & " is Download Now...")
            logdownloader = New vxWebFileDownloader
            logdownloader.DownloadFileWithProgress(URL, dir & Filename)

            Return True
        Catch ex As Exception
            appendLog("Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function appendLog(ByVal log As String)
        debugwindow.AppendText(log & vbNewLine)
        Return True
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.TopMost = False
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Hide()
    End Sub
End Class