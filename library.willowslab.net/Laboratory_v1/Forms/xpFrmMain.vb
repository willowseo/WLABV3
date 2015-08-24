Public Class xpFrmMain
    Inherits System.Windows.Forms.Form
    Private Const URL_MESSAGE As String = "Enter URL of file here"
    Private Const DIR_MESSAGE As String = "Enter directory to download to here"
    'DECLARE THIS WITHEVENTS SO WE GET EVENTS ABOUT DOWNLOAD PROGRESS
    Private WithEvents _Downloader As xpWebFileDownloader
    Private vList As List(Of String) = New List(Of String)

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

                Dim Temp As String = xpFS.Read_File(patchfile)

                For Each vLine As String In Split(Temp, vbNewLine)

                    If InStr(vLine, "patch_accept=") > 0 Then

                        patch_accept = Replace(vLine, "patch_accept=", "")

                    ElseIf InStr(vLine, "local_version=") > 0 Then

                        local_version = Replace(vLine, "local_version=", "")

                        If xpMabiCore.State_ClientType = "Mabinogi_test" Then

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
    Friend WithEvents ProgBar As System.Windows.Forms.ProgressBar
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdGetFolder As System.Windows.Forms.Button
    Friend WithEvents dlgFolderBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents lnkForums As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkMe As System.Windows.Forms.LinkLabel

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ProgBar = New System.Windows.Forms.ProgressBar()
        Me.dlgFolderBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.WebBrowser3 = New System.Windows.Forms.WebBrowser()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.WebBrowser2 = New System.Windows.Forms.WebBrowser()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.WebBrowser4 = New System.Windows.Forms.WebBrowser()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtDownloadTo = New System.Windows.Forms.TextBox()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MabiDir_tmp = New System.Windows.Forms.Label()
        Me.cmdDownload = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.RemotePath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PackageName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ParentPack = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PackageVer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.briefurl = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.infomation = New System.Windows.Forms.WebBrowser()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabPage5.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProgBar
        '
        Me.ProgBar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ProgBar.Location = New System.Drawing.Point(3, 567)
        Me.ProgBar.Name = "ProgBar"
        Me.ProgBar.Size = New System.Drawing.Size(775, 20)
        Me.ProgBar.TabIndex = 1
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblProgress.BackColor = System.Drawing.Color.Transparent
        Me.lblProgress.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.lblProgress.Location = New System.Drawing.Point(3, 591)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(918, 22)
        Me.lblProgress.TabIndex = 8
        Me.lblProgress.Text = "#Progress"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.BackColor = System.Drawing.Color.White
        Me.ProgressBar1.ForeColor = System.Drawing.Color.DodgerBlue
        Me.ProgressBar1.Location = New System.Drawing.Point(3, 543)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(775, 20)
        Me.ProgressBar1.TabIndex = 1
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button5.BackColor = System.Drawing.Color.White
        Me.Button5.Location = New System.Drawing.Point(780, 542)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(142, 67)
        Me.Button5.TabIndex = 15
        Me.Button5.Text = "Mabinogi Start!"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'TabPage6
        '
        Me.TabPage6.Location = New System.Drawing.Point(4, 29)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(914, 502)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Debug"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.WebBrowser3)
        Me.TabPage5.Location = New System.Drawing.Point(4, 29)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(914, 502)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "How to Use"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'WebBrowser3
        '
        Me.WebBrowser3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser3.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser3.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser3.Name = "WebBrowser3"
        Me.WebBrowser3.Size = New System.Drawing.Size(914, 502)
        Me.WebBrowser3.TabIndex = 0
        Me.WebBrowser3.Url = New System.Uri("http://willowslab.com/message/help.php", System.UriKind.Absolute)
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.WebBrowser2)
        Me.TabPage4.Location = New System.Drawing.Point(4, 29)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(914, 502)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Willow's Message"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'WebBrowser2
        '
        Me.WebBrowser2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser2.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser2.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser2.Name = "WebBrowser2"
        Me.WebBrowser2.Size = New System.Drawing.Size(914, 502)
        Me.WebBrowser2.TabIndex = 0
        Me.WebBrowser2.Url = New System.Uri("http://willowslab.com/message/index.php", System.UriKind.Absolute)
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.WebBrowser4)
        Me.TabPage3.Location = New System.Drawing.Point(4, 29)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(914, 502)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Program Infomation"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'WebBrowser4
        '
        Me.WebBrowser4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser4.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser4.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser4.Name = "WebBrowser4"
        Me.WebBrowser4.Size = New System.Drawing.Size(914, 502)
        Me.WebBrowser4.TabIndex = 0
        Me.WebBrowser4.Url = New System.Uri("http://willowslab.com/message/eula.php", System.UriKind.Absolute)
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.WebBrowser1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(914, 502)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Mabi KR Launcher WEB"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(3, 3)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(908, 496)
        Me.WebBrowser1.TabIndex = 0
        Me.WebBrowser1.Url = New System.Uri("http://mabinogi.nexon.com/PageletRef/notice_G2.html", System.UriKind.Absolute)
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.ListView1)
        Me.TabPage1.Controls.Add(Me.txtDownloadTo)
        Me.TabPage1.Controls.Add(Me.cmdDownload)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.Button4)
        Me.TabPage1.Controls.Add(Me.Button3)
        Me.TabPage1.Controls.Add(Me.Button7)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(914, 502)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Willow's Mabinogi Mod"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txtDownloadTo
        '
        Me.txtDownloadTo.Location = New System.Drawing.Point(262, 248)
        Me.txtDownloadTo.Name = "txtDownloadTo"
        Me.txtDownloadTo.Size = New System.Drawing.Size(100, 27)
        Me.txtDownloadTo.TabIndex = 15
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button7.Location = New System.Drawing.Point(772, 235)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(141, 40)
        Me.Button7.TabIndex = 11
        Me.Button7.Text = "Donor Service"
        Me.Button7.UseVisualStyleBackColor = True
        Me.Button7.Visible = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button3.Location = New System.Drawing.Point(772, 193)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(141, 40)
        Me.Button3.TabIndex = 11
        Me.Button3.Text = "Refresh"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button4.Location = New System.Drawing.Point(772, 151)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(141, 40)
        Me.Button4.TabIndex = 11
        Me.Button4.Text = "Update Files"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Location = New System.Drawing.Point(772, 109)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(141, 40)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Delete to Selected"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(912, 59)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Mabinogi Install Directory"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button2.Location = New System.Drawing.Point(877, 21)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(29, 29)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "..."
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.MabiDir_tmp)
        Me.Panel1.Location = New System.Drawing.Point(8, 22)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(865, 27)
        Me.Panel1.TabIndex = 14
        '
        'xpConfig.MabiDir_tmp
        '
        Me.MabiDir_tmp.AutoSize = True
        Me.MabiDir_tmp.Font = New System.Drawing.Font("맑은 고딕", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.MabiDir_tmp.ForeColor = System.Drawing.Color.White
        Me.MabiDir_tmp.Location = New System.Drawing.Point(3, 2)
        Me.MabiDir_tmp.Name = "xpConfig.MabiDir_tmp"
        Me.MabiDir_tmp.Size = New System.Drawing.Size(100, 20)
        Me.MabiDir_tmp.TabIndex = 0
        Me.MabiDir_tmp.Text = "xpConfig.MabiDir_tmp"
        '
        'cmdDownload
        '
        Me.cmdDownload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDownload.BackColor = System.Drawing.Color.DodgerBlue
        Me.cmdDownload.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdDownload.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDownload.Location = New System.Drawing.Point(772, 67)
        Me.cmdDownload.Name = "cmdDownload"
        Me.cmdDownload.Size = New System.Drawing.Size(141, 40)
        Me.cmdDownload.TabIndex = 3
        Me.cmdDownload.Text = "Installation to Selected"
        Me.cmdDownload.UseVisualStyleBackColor = False
        '
        'ListView1
        '
        Me.ListView1.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListView1.CheckBoxes = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.RemotePath, Me.PackageName, Me.ParentPack, Me.PackageVer, Me.briefurl})
        Me.ListView1.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.ListView1.HoverSelection = True
        Me.ListView1.Location = New System.Drawing.Point(0, 68)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(458, 432)
        Me.ListView1.TabIndex = 12
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Package Name"
        Me.ColumnHeader1.Width = 264
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Package Name"
        Me.ColumnHeader2.Width = 0
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Change Date"
        Me.ColumnHeader3.Width = 90
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Brief Info"
        Me.ColumnHeader4.Width = 0
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Installed"
        Me.ColumnHeader5.Width = 82
        '
        'RemotePath
        '
        Me.RemotePath.Width = 0
        '
        'PackageName
        '
        Me.PackageName.Width = 0
        '
        'ParentPack
        '
        Me.ParentPack.Width = 0
        '
        'PackageVer
        '
        Me.PackageVer.Width = 0
        '
        'briefurl
        '
        Me.briefurl.Text = "brief"
        Me.briefurl.Width = 0
        '
        'infomation
        '
        Me.infomation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.infomation.Location = New System.Drawing.Point(-1, 0)
        Me.infomation.MinimumSize = New System.Drawing.Size(20, 20)
        Me.infomation.Name = "infomation"
        Me.infomation.Size = New System.Drawing.Size(309, 431)
        Me.infomation.TabIndex = 1
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Location = New System.Drawing.Point(2, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(922, 535)
        Me.TabControl1.TabIndex = 16
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.infomation)
        Me.Panel2.Location = New System.Drawing.Point(461, 68)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(309, 432)
        Me.Panel2.TabIndex = 16
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 20)
        Me.ClientSize = New System.Drawing.Size(924, 611)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.ProgBar)
        Me.Font = New System.Drawing.Font("맑은 고딕", 11.0!)
        Me.MaximumSize = New System.Drawing.Size(940, 700)
        Me.MinimumSize = New System.Drawing.Size(940, 543)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Willow's Mabinogi MOD"
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'SUB MAIN WHERE WE ENABLE VISUAL STYLES, AND RUN MAIN FORM
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.DoEvents()
        Application.Run(New xpFrmMain)
        Application.Exit()
    End Sub

    'CLOSE PROGRAM
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    'returns all text from last "/" in URL, but puts a "\" in front of the file name..
    Private Function GetFileNameFromURL(ByVal URL As String) As String
        If URL.IndexOf("/"c) = -1 Then Return String.Empty

        Return "\" & URL.Substring(URL.LastIndexOf("/"c) + 1)
    End Function

    'GET A FOLDER TO DOWNLOAD THE FILE TO
    Private Sub cmdGetFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If dlgFolderBrowse.ShowDialog(Me) <> DialogResult.Cancel Then
            txtDownloadTo.Text = dlgFolderBrowse.SelectedPath
        End If
    End Sub

    'FIRES WHEN WE HAVE GOTTEN THE DOWNLOAD SIZE, SO WE KNOW WHAT BOUNDS TO GIVE THE PROGRESS BAR
    Private Sub _Downloader_FileDownloadSizeObtained(ByVal iFileSize As Long) Handles _Downloader.FileDownloadSizeObtained
        ProgBar.Maximum = Convert.ToInt32(iFileSize)
        ProgBar.Value = 0
    End Sub

    'FIRES WHEN DOWNLOAD IS COMPLETE
    Private Sub _Downloader_FileDownloadComplete() Handles _Downloader.FileDownloadComplete
        If ProgressBar1.Value < ProgressBar1.Maximum Then
            ProgressBar1.Value = ProgressBar1.Value + 1
        End If
        ProgBar.Value = ProgBar.Maximum
        'MessageBox.Show("File Download Complete")
    End Sub

    'FIRES WHEN DOWNLOAD FAILES. PASSES IN EXCEPTION INFO
    Private Sub _Downloader_FileDownloadFailed(ByVal ex As System.Exception) Handles _Downloader.FileDownloadFailed
        MessageBox.Show("An error has occured during download: " & ex.Message)
    End Sub

    'FIRES WHEN MORE OF THE FILE HAS BEEN DOWNLOADED
    Private Sub _Downloader_AmountDownloadedChanged(ByVal iNewProgress As Long) Handles _Downloader.AmountDownloadedChanged
        ProgBar.Value = Convert.ToInt32(iNewProgress)
        lblProgress.Text = "[" & ProgressBar1.Value & "/" & ProgressBar1.Maximum & "] " & xpConfig.nowdownloaded & xpLang.downloading & " " & xpWebFileDownloader.FormatFileSize(iNewProgress) & "/ " & xpWebFileDownloader.FormatFileSize(ProgBar.Maximum)
        Application.DoEvents()
    End Sub

    Public Sub MabiStart()
        Dim InfoFile As String = xpConfig.DirMe & "\" & xpMabiCore.State_ClientType & "_patch.txt"

        If xpFS.File_Download(xpMabiCore.Mabinogi_InfoUrl, InfoFile, False, 10) = True Then

            Dim vMabiUpdateInfo As MabiUpdateInfo = New MabiUpdateInfo

            If vMabiUpdateInfo.setInfo(InfoFile) = True Then

                If vMabiUpdateInfo.patch_accept = 0 Then

                    If MsgBox("지금 마비노기의 서버가 닫혀있습니다. 그래도 실행하시겠습니까?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then

                        End

                    End If

                End If

                Try

                    xpFS.Dir_Create(My.Computer.FileSystem.SpecialDirectories.Temp)
                    'My.Computer.FileSystem.WriteAllText(My.Computer.FileSystem.SpecialDirectories.Temp & "\GameSummary.exe", "", False)
                    'My.Computer.FileSystem.GetFileInfo(My.Computer.FileSystem.SpecialDirectories.Temp & "\GameSummary.exe").IsReadOnly = True

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                If vMabiUpdateInfo.local_version > xpMabiCore.getVersion() Then


                Else
                    Dim Executable As String = xpConfig.MabiDir & "\Client.exe"
                    Dim CommandLine As String = "code:1622 ver:" & xpMabiCore.getVersion() & " logip:" & vMabiUpdateInfo.login & " logport:11000 " & vMabiUpdateInfo.arg

                    Dim MyStartInfo As New Diagnostics.ProcessStartInfo(Executable, CommandLine)

                    MyStartInfo.UseShellExecute = False
                    MyStartInfo.RedirectStandardOutput = False
                    MyStartInfo.RedirectStandardInput = False
                    MyStartInfo.CreateNoWindow = False
                    MyStartInfo.WorkingDirectory = xpConfig.MabiDir

                    Dim MyProcess As New Diagnostics.Process
                    MyProcess.StartInfo = MyStartInfo
                    Try
                        MyProcess.Start()
                    Catch ex As Exception

                    End Try
                End If
            End If
        End If
    End Sub

    '====================================MAIN
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'packlist download
        XMLparse()

        'init
        xpConfig.setdir = xpConfig.MabiDir

        'Form load
        xpStart.Close()
        Me.Show()
        txtDownloadTo.Text = xpConfig.MabiDir
        Me.MabiDir_tmp.Text = xpConfig.MabiDir

    End Sub

    Private Sub frmMain_Unload(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.FormClosed
        'MsgBox("종료합니다")
        End
    End Sub

    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click

        Dim ClickTime As Date
        ClickTime = Date.Today
        inactive()

        If Me.ListView1.CheckedItems.Count = 0 Then
            MsgBox(xpLang.xml_selecterror)
            inactive(False)
            Exit Sub
        End If
        'VERIFY A DIRECTORY WAS PICKED AND THAT IT EXISTS
        If My.Computer.FileSystem.DirectoryExists(Me.txtDownloadTo.Text & "\package") = False Then
            MsgBox(xpLang.xml_mabiexsist, MsgBoxStyle.Critical)
        Else
            If Not IO.Directory.Exists(xpConfig.MabiDir & "\package\wlab") Then
                xpFS.dir_Create(xpConfig.MabiDir & "\package\wlab")
            End If
            ProgressBar1.Maximum = ListView1.CheckedItems.Count
            ProgressBar1.Value = 1

            For Each Item As ListViewItem In Me.ListView1.CheckedItems
                Dim tmpDirectory As String = xpConfig.MabiDir
                If Item.SubItems(5).Text = "default" Then
                    tmpDirectory &= "\package\wlab\"
                Else
                    tmpDirectory &= "\package\" & Item.SubItems(5).Text & "\"
                End If
                xpConfig.nowdownloaded = Item.SubItems(7).Text
                If FileDownloadSystem(Item.SubItems(6).Text, Item.SubItems(7).Text, tmpDirectory) = True Then

                    Dim FileInfo As String = ""
                    FileInfo &= "<?xml version=""1.0"" encoding=""utf-8""?>"
                    FileInfo &= "<PackInfo>"
                    FileInfo &= "<Info Type=""" & Item.SubItems(0).Text & """ Filename=""" & Item.SubItems(1).Text & """ Date=""" & ClickTime & """ Info=""" & Item.SubItems(3).Text & """ Install=""" & xpLang.xml_installedtext & """ PackageVer=""" & Item.SubItems(9).Text & """ />"
                    FileInfo &= "</PackInfo>"

                    'XML 파일 내용 저장
                    My.Computer.FileSystem.WriteAllText(tmpDirectory & Item.SubItems(7).Text & ".xml", FileInfo, False)
                Else
                    xpFS.FileDelete(tmpDirectory & Item.SubItems(7).Text)
                    xpFS.FileDelete(tmpDirectory & Item.SubItems(7).Text & ".xml")
                    'MsgBox(Item.SubItems(0).Text & vbNewLine & "다운로드 실패")
                End If
                'MsgBox("01 " & Item.SubItems(1).Text & vbNewLine & "02 " & Item.SubItems(2).Text & vbNewLine & "03 " & Item.SubItems(3).Text & vbNewLine & "04 " & Item.SubItems(4).Text & vbNewLine & "05 " & Item.SubItems(5).Text & vbNewLine & "06 " & Item.SubItems(6).Text & vbNewLine & "07 " & Item.SubItems(7).Text & vbNewLine & "08 " & Item.SubItems(8).Text)
            Next
            'DO THE DOWNLOAD
        End If
        lblProgress.Text = xpLang.download_success
        XMLparse()
    End Sub

    Public Function FileDownloadSystem(ByVal URL, ByVal Filename, ByVal dir)
        Try
            Dim downloadPath As String = dir.TrimEnd("\"c)
            If xpFS.Dir_Exists(downloadPath) Then

            End If

            _Downloader = New xpWebFileDownloader
            _Downloader.DownloadFileWithProgress(URL, dir & Filename)

            Return True
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function XMLparse(Optional ByVal flag As Boolean = False)
        '웹에서 받아온 XML을 파싱하는 함수 (팩리스트작성)
        Try

            Dim doc As New Xml.XmlDocument
            Dim msg As String = ""
            ListView1.Items.Clear()
            inactive()
            doc.Load(xpConfig.DirMe & "\packinfo.info")

            Dim Packs As Xml.XmlNodeList = doc.SelectNodes("/Packs/PackInfo")
            Dim update_count As Integer = 0

            For Each Pack As Xml.XmlElement In Packs
                Dim packDir As String = Pack.GetAttribute("Path")

                If packDir = "default" Then
                    packDir = xpConfig.MabiDir & "\package\wlab"
                Else
                    packDir = xpConfig.MabiDir & "\package\" & Pack.GetAttribute("Path")
                End If

                Dim packinfodir As String = packDir & "\" & Pack.GetAttribute("PackageName") & ".xml"

                Dim xml_Filename As String = Pack.GetAttribute("Filename")
                Dim xml_Date As String = Pack.GetAttribute("Date")
                Dim xml_Info As String = Pack.GetAttribute("Info")
                Dim xml_Install As String = xpLang.xml_notinstall
                Dim xml_version As String = Pack.GetAttribute("PackageVer")

                If xpLang.lang_flag = False Then
                    xml_Filename = Pack.GetAttribute("ENFilename")
                    xml_Info = Pack.GetAttribute("ENInfo")
                End If

                'packname_ver_type_subversion.pack
                If xml_Filename.ToUpper.IndexOf("FULL") <> -1 Then
                    'It's base pack
                Else
                    'expend pack
                End If
                Dim client_ver As Double = 0
                Dim server_ver As Double = 0

                '패키지 정보가 이미 설치된 폴더의 정보에 있으면 
                If xpFS.File_Exists(packinfodir) = True Then
                    Dim packPrivXML = New Xml.XmlDocument
                    doc.Load(packinfodir)

                    Dim loadedInfo As Xml.XmlNodeList = doc.SelectNodes("/PackInfo/Info")

                    For Each PrivInfo As Xml.XmlElement In loadedInfo
                        Dim myversion As String = PrivInfo.GetAttribute("PackageVer")
                        xml_Date = PrivInfo.GetAttribute("Date")
                        xml_Install = PrivInfo.GetAttribute("Install")

                        client_ver = Val(myversion)
                        server_ver = Val(xml_version)
                    Next

                    If server_ver > client_ver Then
                        msg &= "" & xml_Filename & "" & vbNewLine
                        xml_Filename &= "[Updated]"
                    End If
                Else

                End If

                Dim tmpArr As String() = {
                                                Pack.GetAttribute("Type"),
                                                xml_Filename,
                                                xml_Date,
                                                xml_Info,
                                                xml_Install,
                                                Pack.GetAttribute("Path"),
                                                Pack.GetAttribute("RemotePath"),
                                                Pack.GetAttribute("PackageName"),
                                                Pack.GetAttribute("ParentPack"),
                                                Pack.GetAttribute("PackageVer"),
                                                Pack.GetAttribute("BriefInfoUrl")
                                         }
                Dim tmpItem = New ListViewItem(tmpArr)

                '받아온 팩 정보를 교체와 교체가 아닌것으로 구분. 예외 따위...

                Dim typecut As String = Pack.GetAttribute("Type").Substring(0, 2)

                If typecut = "특수" Then
                    tmpItem.ForeColor = Color.Gold
                ElseIf typecut = "필수" Then
                    tmpItem.ForeColor = Color.DodgerBlue
                ElseIf typecut = "관리" Then
                    tmpItem.ForeColor = Color.OrangeRed
                ElseIf typecut = "간소" Then
                    tmpItem.ForeColor = Color.Silver
                End If

                tmpItem.SubItems(0).Text = xml_Filename

                If server_ver > client_ver Then
                    tmpItem.BackColor = Color.MistyRose
                    tmpItem.SubItems(4).Text = "업데이트 필요"
                End If

                If flag = True And client_ver < server_ver And Pack.GetAttribute("RemotePath") <> "" And xpFS.File_Exists(packinfodir) = True Then
                    ListView1.Items.Add(tmpItem)
                ElseIf flag = False Then
                    ListView1.Items.Add(tmpItem)
                End If

                If vList.Contains(Pack.GetAttribute("PackageName").Trim.ToLower) = False Then
                    vList.Add(Pack.GetAttribute("PackageName").ToString.Trim.ToLower & ".pack")
                End If

                '웹에서 받아온 팩파일 목록 저장
            Next

            '먼저 디렉토리 읽기
            If My.Computer.FileSystem.DirectoryExists(xpConfig.MabiDir & "\package\wlab") = True And flag = False Then
                For Each vFile In My.Computer.FileSystem.GetFiles(xpConfig.MabiDir & "\package\wlab")
                    If System.IO.Path.GetExtension(vFile).ToLower = ".pack" Then
                        If vList.Contains(System.IO.Path.GetFileName(vFile).ToString.Trim.ToLower) = False Then
                            If My.Computer.FileSystem.FileExists(vFile & ".xml") = True Then ' 팩정보가 존재하면 그걸 읽어라잉~~

                                'XML 로드
                                Dim sub_doc As Xml.XmlDocument = New Xml.XmlDocument()
                                sub_doc.Load(vFile & ".xml")
                                '팩정보파일의 정보 읽기
                                For Each subNode As Xml.XmlElement In sub_doc.SelectNodes("/PackInfo/Info")

                                    Dim localtype As String = subNode.GetAttribute("Type")
                                    Dim localname As String = subNode.GetAttribute("Filename")
                                    Dim localdate As String = subNode.GetAttribute("Date")
                                    Dim localinfo As String = subNode.GetAttribute("Info")
                                    Dim installed As String = xpLang.xml_installedtext
                                    Dim localvers As String = subNode.GetAttribute("PackageVer")

                                    Dim tmpArr As String() = {
                                               "",
                                               localname,
                                               localdate,
                                               localinfo,
                                               installed,
                                               "",
                                               "",
                                               "",
                                               "",
                                               localvers,
                                               ""
                                           }
                                    ' 목록에 추가
                                    Dim tmpItem = New ListViewItem(tmpArr)

                                    If localtype = "특수" Then
                                        tmpItem.ForeColor = Color.Orange
                                    Else
                                        tmpItem.ForeColor = Color.Gray
                                    End If

                                    'ListView1.Items.Add(tmpItem)
                                Next
                            Else

                                ' 목록에 추가할 정보 문자열 나열
                                Dim strArr = New String() {System.IO.Path.GetFileName(vFile), "", "", "", "", "알수없는파일"}
                                Dim lvt = New ListViewItem(strArr)

                                ' 목록에 추가
                                'ListView1.Items.Add(lvt)

                            End If
                        End If
                    End If
                Next
            End If

            If flag = True Then
                Dim ClickTime As Date
                ClickTime = Date.Today

                If ListView1.Items.Count = 0 Then
                    inactive(False)
                    Return False
                    Exit Function
                End If

                ProgressBar1.Maximum = ListView1.Items.Count
                ProgressBar1.Value = 1

                'VERIFY A DIRECTORY WAS PICKED AND THAT IT EXISTS
                If My.Computer.FileSystem.DirectoryExists(Me.txtDownloadTo.Text & "\package") = False Then
                    MsgBox(xpLang.xml_mabiexsist, MsgBoxStyle.Critical)
                Else
                    If Not IO.Directory.Exists(xpConfig.MabiDir & "\package\wlab") Then
                        xpFS.dir_Create(xpConfig.MabiDir & "\package\wlab")
                    End If

                    For Each Item As ListViewItem In Me.ListView1.Items
                        Dim tmpDirectory As String = xpConfig.MabiDir
                        If Item.SubItems(5).Text = "default" Then
                            tmpDirectory &= "\package\wlab\"
                        Else
                            tmpDirectory &= "\package\" & Item.SubItems(5).Text & "\"
                        End If
                        xpConfig.nowdownloaded = Item.SubItems(7).Text
                        If FileDownloadSystem(Item.SubItems(6).Text, Item.SubItems(7).Text, tmpDirectory) = True Then

                            Dim FileInfo As String = ""
                            FileInfo &= "<?xml version=""1.0"" encoding=""utf-8""?>"
                            FileInfo &= "<PackInfo>"
                            FileInfo &= "<Info Name=""" & Item.SubItems(1).Text & """ Date=""" & ClickTime & """ Info=""" & Item.SubItems(3).Text & """ Install=""" & xpLang.xml_installedtext & """ PackageVer=""" & Item.SubItems(9).Text & """ />"
                            FileInfo &= "</PackInfo>"

                            'XML 파일 내용 저장
                            My.Computer.FileSystem.WriteAllText(tmpDirectory & Item.SubItems(7).Text & ".xml", FileInfo, False)
                        Else
                            xpFS.FileDelete(tmpDirectory & Item.SubItems(7).Text)
                            xpFS.FileDelete(tmpDirectory & Item.SubItems(7).Text & ".xml")
                            'MsgBox(Item.SubItems(0).Text & vbNewLine & "다운로드 실패")
                        End If
                        'MsgBox("01 " & Item.SubItems(1).Text & vbNewLine & "02 " & Item.SubItems(2).Text & vbNewLine & "03 " & Item.SubItems(3).Text & vbNewLine & "04 " & Item.SubItems(4).Text & vbNewLine & "05 " & Item.SubItems(5).Text & vbNewLine & "06 " & Item.SubItems(6).Text & vbNewLine & "07 " & Item.SubItems(7).Text & vbNewLine & "08 " & Item.SubItems(8).Text)
                    Next
                    'DO THE DOWNLOAD
                End If
                lblProgress.Text = xpLang.download_success
            End If
            inactive(False)

            Return False
        Catch e As Exception
            '예외가 되면 메세지 띄우고 종료
            MsgBox("Have a problem on server or you can state that the service is terminated. Please contact Server Master.")
            Return False
        End Try
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If dlgFolderBrowse.ShowDialog(Me) <> DialogResult.Cancel Then

            xpConfig.setdir = dlgFolderBrowse.SelectedPath

        End If

    End Sub
    Private Function inactive(Optional ByVal flag = True)
        If flag = True Then
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button7.Enabled = False
            cmdDownload.Enabled = False
            ListView1.Enabled = False
            Return True
        Else
            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            Button7.Enabled = True
            cmdDownload.Enabled = True
            ListView1.Enabled = True
            Return False
        End If
    End Function

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' 위인가..아닌가
        If Me.ListView1.CheckedItems.Count = 0 Then

            MsgBox(xpLang.xml_selecterror)
            Exit Sub

        End If

        '...위
        If My.Computer.FileSystem.DirectoryExists(txtDownloadTo.Text & "\package") = False Then

            MsgBox(xpLang.xml_mabiexsist)
            Exit Sub

        End If

        '마찬가지 위에 있다잉
        For Each Item As ListViewItem In Me.ListView1.CheckedItems
            Dim tmpDirectory As String = xpConfig.MabiDir
            If Item.SubItems(5).Text = "default" Then
                tmpDirectory &= "\package\wlab\"
            Else
                tmpDirectory &= "\package\" & Item.SubItems(5).Text & "\"
            End If
            ' 팩 관련 파일들 삭제
            xpFS.FileDelete(tmpDirectory & Item.SubItems(7).Text)
            xpFS.FileDelete(tmpDirectory & Item.SubItems(7).Text & ".xml")

        Next

        XMLparse()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        xpFS.backGroundDownloader(xpMabiCore.PackInfoURL, xpConfig.DirMe & "\", "packinfo.info")
        XMLparse()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        xpFS.backGroundDownloader(xpMabiCore.PackInfoURL, xpConfig.DirMe & "\", "packinfo.info")
        XMLparse(True)
        XMLparse()
    End Sub
    Friend WithEvents Button5 As System.Windows.Forms.Button

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MabiStart()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        xpAuth_donor.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As EventArgs) Handles Panel1.Click
        Process.Start(Me.MabiDir_tmp.Text)
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub ListView1_SelectedIndexChanged_1(sender As Object, e As EventArgs)
        If sender.Items(sender.FocusedItem.Index).SubItems(10).Text <> "" Then
            Dim tmpURL As New System.Uri(sender.Items(sender.FocusedItem.Index).SubItems(10).Text)
            infomation.Url = tmpURL
        End If
    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel

    Private Sub ListView1_SelectedIndexChanged_2(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents WebBrowser3 As System.Windows.Forms.WebBrowser
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents WebBrowser2 As System.Windows.Forms.WebBrowser
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents WebBrowser4 As System.Windows.Forms.WebBrowser
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents infomation As System.Windows.Forms.WebBrowser
    Public WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents RemotePath As System.Windows.Forms.ColumnHeader
    Friend WithEvents PackageName As System.Windows.Forms.ColumnHeader
    Friend WithEvents ParentPack As System.Windows.Forms.ColumnHeader
    Friend WithEvents PackageVer As System.Windows.Forms.ColumnHeader
    Friend WithEvents briefurl As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtDownloadTo As System.Windows.Forms.TextBox
    Friend WithEvents cmdDownload As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MabiDir_tmp As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl

    Private Sub ListView1_SelectedIndexChanged_1(sender As Object, e As MouseEventArgs) Handles ListView1.MouseClick

    End Sub
End Class
