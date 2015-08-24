Public Class xpStart
    Private Sub vStart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        xpFS.backGroundDownloader(xpMabiCore.PackInfoURL, xpConfig.DirMe & "\", "packinfo.info")

        If xpFS.File_Exists(xpMabiCore.Dir_Myself_Config & "\define.xml") = False Then
            xpFS.Dir_Create(xpConfig.DirMe & "\config")
            xpFS.backGroundDownloader("http://api.srinn.kr/config/define.xml", xpConfig.DirMe & "\config\", "define.xml")
        End If

        Dim doc As New Xml.XmlDocument
        doc.Load(xpConfig.DirMe & "\packinfo.info")
        Dim Packs As Xml.XmlNodeList = doc.SelectNodes("/Packs/VersionInfo")
        Dim myver As Double = "20140819"
        For Each Pack As Xml.XmlElement In Packs
            If Val(Pack.GetAttribute("ver")) > myver Then
                MsgBox(xpLang.version_updated)
                End
            End If
        Next

        xpMabiCore.Main_Load()

        Dim Language As InputLanguage

        For Each Language In InputLanguage.InstalledInputLanguages
            If Language.Culture.EnglishName = "Korean (Korea)" Then
                'MsgBox("한국어를 사용중입니다. 한국어로 전환합니다.")
                xpFrmMain.GroupBox1.Text = "마비노기가 설치된 경로"
                xpFrmMain.ColumnHeader1.Text = "패키지 이름"
                xpFrmMain.ColumnHeader2.Text = "패키지명"
                xpFrmMain.ColumnHeader3.Text = "변경 일자"
                xpFrmMain.ColumnHeader4.Text = "짧은 설명"
                xpFrmMain.ColumnHeader5.Text = "설치 여부"
                xpFrmMain.cmdDownload.Text = "선택한 것 설치"
                xpFrmMain.Button1.Text = "선택된 것 삭제"
                xpFrmMain.Button3.Text = "목록 수동 갱신"
                xpFrmMain.Button4.Text = "업데이트"
                xpFrmMain.Button7.Text = "기부자 혜택"
                xpFrmMain.TabPage1.Text = "버들서의 내맘대로 패키지"
                xpFrmMain.TabPage2.Text = "마비노기 공지보기"
                xpFrmMain.TabPage3.Text = "프로그램 정보"
                xpFrmMain.TabPage4.Text = "버들서의 메세지"
                xpFrmMain.TabPage5.Text = "도움말"
                xpFrmMain.lblProgress.Text = "버들서의 내맘대로 패키지를 사용해주셔서 전혀 감사합니다."
                xpLang.xml_mabiexsist = "마비노기 폴더가 없는 것 같습니다. 설치되지 않습니다."

                xpLang.lang_flag = True
            Else
                xpFrmMain.lblProgress.Text = "never Thx for Willows Mabinogi Mod use."
                xpLang.xml_selecterror = "Please Select Package File"
                xpLang.xml_installedtext = "Installed"
                xpLang.xml_notinstall = "Not Install"
                xpLang.download_success = "Download Complete."
                xpLang.downloading = " Downloading Now..."
                xpLang.xml_installedtext = "Installed"
                xpLang.xml_notinstall = "Not Install"
                xpLang.pack_updated = "are updated. please update!"
            End If
        Next Language

    End Sub
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        Static intCount As Integer '****변수를 정적으로 선언하여 변수초기화를 막는다
        intCount = intCount + 1 '****이벤트의 횟수를 변수에 저장한다
        If intCount = 5 Then
            Me.Timer1.Enabled = False
            xpFrmMain.Show()
            Me.Hide()
        End If
    End Sub
End Class