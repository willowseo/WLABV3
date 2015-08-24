Public Class vxStart
    Public myver As Long = "275002"
    Public myModeSelect As String = "none"
    Private Sub vStart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = False

        'Dim Drv As IO.DriveInfo
        'For Each Drv In IO.DriveInfo.GetDrives
        '    'MsgBox(Drv.Name)
        '    If IO.Directory.GetDirectoryRoot(vxConfig.DirMe) = Drv.Name Then
        '        If Drv.DriveType = IO.DriveType.Removable Then
        '            If IO.File.Exists(vxConfig.DirMe & "\Config\willowspack.setting") Then
        '                Dim setting As Byte() = My.Computer.FileSystem.ReadAllBytes(vxConfig.DirMe & "\Config\willowspack.setting")

        '                MsgBox(System.Text.Encoding.ASCII.GetString(setting))
        '            End If
        '            If MsgBox("버들팩을 이동식 디스크에서 실행한 것 같습니다. 마비노기를 이동식으로 이용하고 있습니까?", MsgBoxStyle.OkCancel Or MsgBoxStyle.Information Or vbMsgBoxSetForeground) = MsgBoxResult.Ok and _
        '                Then
        '                Dim df As Boolean = False

        '                '마비노기랑 버들팩을 같이 둔 경우
        '                If IO.File.Exists(vxConfig.DirMe & "\version.dat") And IO.File.Exists(vxConfig.DirMe & "\client.exe") Then
        '                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "", vxConfig.DirMe)
        '                    df = True
        '                End If

        '                '그 이외
        '                For Each path As String In IO.Directory.GetDirectories(Drv.Name)
        '                    '마비노기랑 버들팩을 같이 둔 경우
        '                    If IO.File.Exists(path & "\version.dat") And IO.File.Exists(path & "\client.exe") Then
        '                        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "", path)
        '                        df = True
        '                    End If
        '                Next

        '                If df = False Then
        '                    If MsgBox("현재 드라이브에 마비노기가 존재하지 않습니다. 설치된 마비노기를 찾아 이동식으로 복제하시겠습니까?", MsgBoxStyle.OkCancel Or MsgBoxStyle.Information Or vbMsgBoxSetForeground) Then

        '                    End If
        '                End If
        '            End If
        '        End If
        '    End If
        'Next

        If Len(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\willowsPack", "", "")) = 0 Then
            vxFS.backGroundDownloader("http://willowspack.srin.kr/WlabInstall.exe", vxConfig.DirMe & "\", "installer.exe")
            Process.Start(vxConfig.DirMe & "\installer.exe")
            End
        Else
            If vxConfig.DirMe <> My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\willowsPack", "", "") Then
                If MsgBox("버들팩이 최초 설치된 위치와 다른 설치경로에 설치되었습니다. 현재 실행한 경로로 다시 설치하시겠습니까?", MsgBoxStyle.OkCancel Or MsgBoxStyle.Information Or vbMsgBoxSetForeground) = MsgBoxResult.Ok Then
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\willowsPack", "", vxConfig.DirMe)
                    Process.Start(vxConfig.DirMe & "\willowspack.exe")
                    End
                End If
            End If
        End If

        If My.User.IsInRole("Administrators") = False Then
            MsgBox("관리자 권한으로 실행해주시기 바랍니다.", MsgBoxStyle.Information Or vbMsgBoxSetForeground)
            End
        End If

        myModeSelect = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\willowsPack", "mode", "")

        If vxFS.File_Exists(vxMabiCore.Dir_Myself_Config & "\define.xml") = False Then
            vxFS.Dir_Create(vxConfig.DirMe & "\Config")
            vxFS.backGroundDownloader("http://api.srinn.kr/config/define.xml", vxConfig.DirMe & "\config\", "define.xml")
        End If
        If vxFS.Dir_Exists(vxConfig.DirMe & "\Config\setting") = False Then
            vxFS.Dir_Create(vxConfig.DirMe & "\Config\setting")
        End If

        If vxFS.File_Exists(vxConfig.DirMe & "\Config\packinfo.nfo") Then
            vxFS.File_Delete(vxConfig.DirMe & "\Config\packinfo.nfo")
        End If

        vxFS.backGroundDownloader(vxMabiCore.PackInfoURL, vxConfig.DirMe & "\Config\", "packinfo.nfo")


        Dim doc As New Xml.XmlDocument
        doc.Load(vxConfig.DirMe & "\Config\packinfo.nfo")
        Dim Packs As Xml.XmlNodeList = doc.SelectNodes("/Packs/VersionInfo")
        For Each Pack As Xml.XmlElement In Packs
            If Val(Pack.GetAttribute("Version")) > myver Then
                vxFS.backGroundDownloader("http://willowspack.srin.kr/WlabInstall.exe", vxConfig.DirMe & "\", "installer.exe")
                Process.Start(vxConfig.DirMe & "\installer.exe")
                MsgBox(vxLang.version_updated)
                End
            End If
        Next

        vxConfig.MabiDir = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "", "")

        Dim p() As Process

        p = Process.GetProcessesByName("willowspack")
        If p.Count > 1 Then
            MsgBox("버들팩이 이미 실행되어 있습니다.")
            End
        End If

        'p = Process.GetProcessesByName("Client")
        'If p.Count > 0 Then
        '    If Len(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "", "")) = 0 Then
        '        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "", p(0).MainModule.FileName.ToString)
        '    End If
        '    If MsgBox("마비노기가 실행되어 있습니다. 그래도 버들팩을 실행하시겠습니까? 새로 받는 경우에는 적용되지 않으며, 이미 설치된 항목을 업데이트 할 수 없습니다.", MsgBoxStyle.OkCancel Or MsgBoxStyle.Information Or vbMsgBoxSetForeground) = MsgBoxResult.Cancel Then
        '        End
        '    End If
        'End If

        p = Process.GetProcessesByName("lumipack")
        If p.Count > 0 Then
            MsgBox("루미팩을 이용하고 있습니다. 루미팩을 종료합니다. 버들팩은 루미팩과의 병행실행이 불가능합니다. 사유는 잘 아시리라 생각합니다.")
            For Each proc As Process In p
                Try
                    proc.Kill()
                    IO.File.Delete(proc.MainModule.FileName.ToString())
                    If Len(vxConfig.MabiDir) <> 0 Then
                        IO.File.Delete(vxConfig.MabiDir & "\hslauncher.exe")
                        IO.File.Delete(vxConfig.MabiDir & "\dinput8.dll")
                    End If
                Catch ex As Exception
                End Try
            Next
        End If

        Dim Language As InputLanguage

        Me.Show()

        For Each Language In InputLanguage.InstalledInputLanguages
            If Language.Culture.EnglishName = "Korean (Korea)" Then
                'MsgBox("한국어를 사용중입니다. 한국어로 전환합니다.")
                vxLang.xml_mabiexsist = "마비노기 폴더가 없는 것 같습니다. 설치되지 않습니다."

                vxLang.lang_flag = True

            ElseIf vxLang.lang_flag = False Then
                vxLang.xml_selecterror = "Please Select Package File"
                vxLang.xml_installedtext = "Installed"
                vxLang.xml_notinstall = "Not Install"
                vxLang.download_success = "Download Complete."
                vxLang.downloading = " Downloading Now..."
                vxLang.xml_installedtext = "Installed"
                vxLang.xml_notinstall = "Not Install"
                vxLang.pack_updated = "are updated. please update!"
            End If
        Next Language
        If myModeSelect <> "" Then
            If myModeSelect = "Pro" Then
                vers.Text &= "Professional Version"
            Else
                vers.Text &= "Beginner's Lite Version"
            End If
        Else
            vers.Text = "Install Procedure"
        End If

        Timer1.Enabled = True
    End Sub
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        Static intCount As Integer '****변수를 정적으로 선언하여 변수초기화를 막는다
        intCount = intCount + 1 '****이벤트의 횟수를 변수에 저장한다
        If intCount = 4 Then
            Me.Timer1.Enabled = False
            If myModeSelect = "" Then
                myModeSelect = "none"
                vxFS.backGroundDownloader("http://willowspack.srin.kr/WlabInstall.exe", vxConfig.DirMe & "\", "installer.exe")
                Process.Start(vxConfig.DirMe & "\installer.exe")
                End
            Else
                If myModeSelect = "Pro" Then
                    vxFrmMain.Show()
                Else
                    vxFrmLiteMain.Show()
                End If
            End If
            Me.Hide()
        End If
    End Sub
End Class