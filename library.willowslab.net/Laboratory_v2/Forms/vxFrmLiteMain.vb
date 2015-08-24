Public Class vxFrmLiteMain

    Private vList As List(Of String) = New List(Of String)
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        vxFrmMain.MabiStart()
    End Sub

    Private Sub FrmLiteMain_Load(sender As Object, e As EventArgs) Handles MyBase.Disposed
        End
    End Sub

    Private Sub 버들팩LiteModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 버들팩LiteModeToolStripMenuItem.Click

    End Sub

    Private Sub FrmLiteMain_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        vxLog.Show()
        vxLog.appendLog("Running on Lite Mode")

        If vxFS.Dir_Exists(vxConfig.dirme & "\Config") = False Then
            vxFS.Dir_Create(vxConfig.dirme & "\Config")
        End If

        vxFS.backGroundDownloader(vxMabiCore.PackInfoURL, vxConfig.dirme & "\Config\", "packinfo.nfo")
        ListXMLParse()
        Dim doc As New Xml.XmlDocument
        doc.Load(vxConfig.dirme & "\Config\packinfo.nfo")
        Dim Packs As Xml.XmlNodeList = doc.SelectNodes("/Packs/Notice")

        If vxFS.Dir_Exists(vxConfig.MabiDir & "\package\lumipack") Then
            Button5.Visible = True
        Else
            Button5.Visible = False
        End If

        For Each Pack As Xml.XmlElement In Packs
            TextBox2.Text = Pack.GetAttribute("text")
        Next
    End Sub

    Public Function ListXMLParse(Optional ByVal flag As Boolean = False)
        Try

            Dim doc As New Xml.XmlDocument
            list_default.Items.Clear()
            list_Select.Items.Clear()
            list_decoration.Items.Clear()
            list_Simpling.Items.Clear()
            list_View.Items.Clear()
            list_field.Items.Clear()
            list_Service.Items.Clear()

            doc.Load(vxConfig.dirme & "\Config\packinfo.nfo")

            Dim Packs As Xml.XmlNodeList = doc.SelectNodes("/Packs/PackInfo")
            Dim update_count As Integer = 0

            For Each Pack As Xml.XmlElement In Packs
                Dim packDir As String = Pack.GetAttribute("Path")

                If packDir = "default" Then
                    packDir = vxConfig.MabiDir & "\package\wlab"
                Else
                    packDir = vxConfig.MabiDir & "\package\" & Pack.GetAttribute("Path")
                End If

                Dim packinfodir As String = packDir & "\" & Pack.GetAttribute("PackageName") & ".xml"

                Dim xml_Filename As String = Pack.GetAttribute("Filename")
                Dim xml_Date As String = Pack.GetAttribute("Date")
                Dim xml_Info As String = Pack.GetAttribute("Info")
                Dim xml_Install As String = vxLang.xml_notinstall
                Dim xml_version As String = Pack.GetAttribute("PackageVer")

                If vxLang.lang_flag = False Then
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
                If vxFS.File_Exists(packinfodir) = True Then
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
                        vxLog.appendLog(xml_Filename & " Auto Update...")
                        Dim ClickTime As Date
                        ClickTime = Date.Today

                        Dim p() As Process
                        p = Process.GetProcessesByName("Client")
                        If p.Count = 0 Then
                            Dim tmpDirectory As String = vxConfig.MabiDir
                            If Pack.GetAttribute("Path") = "default" Then
                                tmpDirectory &= "\package\wlab\"
                            Else
                                tmpDirectory &= "\package\" & Pack.GetAttribute("Path") & "\"
                            End If
                            If vxLog.logFileDownloader(Pack.GetAttribute("RemotePath"), Pack.GetAttribute("PackageName"), tmpDirectory) = True Then
                                vxLog.appendLog("Complete!")

                                Dim FileInfo As String = ""
                                FileInfo &= "<?xml version=""1.0"" encoding=""utf-8""?>"
                                FileInfo &= "<PackInfo>"
                                FileInfo &= "<Info Type=""" & Pack.GetAttribute("Type") & """ Filename=""" & xml_Filename & """ Date=""" & ClickTime & """ Info=""" & xml_Info & """ Install=""" & vxLang.xml_installedtext & """ PackageVer=""" & Pack.GetAttribute("PackageVer") & """ />"
                                FileInfo &= "</PackInfo>"

                                My.Computer.FileSystem.WriteAllText(tmpDirectory & Pack.GetAttribute("PackageName") & ".xml", FileInfo, False)
                            Else
                                vxFS.FileDelete(tmpDirectory & Pack.GetAttribute("PackageName"))
                                vxFS.FileDelete(tmpDirectory & Pack.GetAttribute("PackageName") & ".xml")
                                vxLog.appendLog(xml_Filename & "다운로드 실패")
                            End If
                        End If
                    Else
                        xml_Filename &= " [Installed]"
                    End If
                Else

                End If

                Dim tmpArr As String() = {
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

                Dim typecut As String = Pack.GetAttribute("Type")

                If typecut.IndexOf("특수") <> -1 Then
                    tmpItem.BackColor = Color.LemonChiffon
                End If

                If typecut = "기본" Or typecut = "필수기본" Or typecut = "관리기본" Then
                    list_default.Items.Add(tmpItem)
                ElseIf typecut = "간소" Or typecut = "필수간소" Or typecut = "관리간소" Then
                    list_Simpling.Items.Add(tmpItem)
                ElseIf typecut = "꾸미기" Or typecut = "필수꾸미기" Or typecut = "관리꾸미기" Then
                    list_decoration.Items.Add(tmpItem)
                ElseIf typecut = "편의" Or typecut = "필수편의" Or typecut = "관리편의" Then
                    list_Service.Items.Add(tmpItem)
                ElseIf typecut = "선택" Or typecut = "필수선택" Or typecut = "관리선택" Then
                    list_Select.Items.Add(tmpItem)
                ElseIf typecut = "필드" Or typecut = "필수필드" Or typecut = "관리필드" Then
                    list_Select.Items.Add(tmpItem)
                ElseIf typecut = "시야" Or typecut = "필수시야" Or typecut = "관리시야" Then
                    list_Select.Items.Add(tmpItem)
                End If

                If vList.Contains(Pack.GetAttribute("PackageName").Trim.ToLower) = False Then
                    vList.Add(Pack.GetAttribute("PackageName").ToString.Trim.ToLower & ".pack")
                End If

                '웹에서 받아온 팩파일 목록 저장
            Next

            '먼저 디렉토리 읽기
            If My.Computer.FileSystem.DirectoryExists(vxConfig.MabiDir & "\package\wlab") = True And flag = False Then
                For Each vFile In My.Computer.FileSystem.GetFiles(vxConfig.MabiDir & "\package\wlab")
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
                                    Dim installed As String = vxLang.xml_installedtext
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
        Catch e As Exception
            '예외가 되면 메세지 띄우고 종료
            MsgBox("Have a problem on server or you can state that the service is terminated. Please contact Server Master.")
            Return False
        End Try
        Return True
    End Function

    Private Sub 리스트다시불러오기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 리스트다시불러오기ToolStripMenuItem.Click
        vxFS.backGroundDownloader(vxMabiCore.PackInfoURL & "?donorID=" & wlabID.Text & "&donorPW=" & vxMD5crypto.getMd5Hash(wlabPW.Text), vxConfig.dirme & "\Config\", "packinfo.nfo")
        ListXMLParse()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim p() As Process
        p = Process.GetProcessesByName("Client")
        If p.Count = 0 Then
            Dim ClickTime As Date
            ClickTime = Date.Today

            Me.Button1.Enabled = False
            Me.Button3.Enabled = False

            Dim checkedCount As Integer = 0

            checkedCount += Me.list_default.CheckedItems.Count
            checkedCount += Me.list_Select.CheckedItems.Count
            checkedCount += Me.list_decoration.CheckedItems.Count
            checkedCount += Me.list_Simpling.CheckedItems.Count
            checkedCount += Me.list_View.CheckedItems.Count
            checkedCount += Me.list_field.CheckedItems.Count
            checkedCount += Me.list_Service.CheckedItems.Count

            If checkedCount = 0 Then
                MsgBox(vxLang.xml_selecterror)
                Me.Button1.Enabled = True
                Me.Button3.Enabled = True
                Exit Sub
            End If

            Dim objArr As New ArrayList

            objArr.Add(Me.list_default)
            objArr.Add(Me.list_Select)
            objArr.Add(Me.list_decoration)
            objArr.Add(Me.list_Simpling)
            objArr.Add(Me.list_View)
            objArr.Add(Me.list_field)
            objArr.Add(Me.list_Service)

            Me.ListView1.Clear()

            For index = 0 To objArr.Count - 1
                For Each Item As ListViewItem In objArr(index).CheckedItems
                    Dim tmpArr As String() = {
                                                "",
                                                Item.SubItems(0).Text,
                                                Item.SubItems(1).Text,
                                                Item.SubItems(2).Text,
                                                Item.SubItems(3).Text,
                                                Item.SubItems(4).Text,
                                                Item.SubItems(5).Text,
                                                Item.SubItems(6).Text,
                                                Item.SubItems(7).Text,
                                                Item.SubItems(8).Text,
                                                Item.SubItems(9).Text
                                            }
                    Dim tmpItem = New ListViewItem(tmpArr)
                    ListView1.Items.Add(tmpItem)
                Next
            Next

            If My.Computer.FileSystem.DirectoryExists(vxConfig.MabiDir & "\package") = False Then
                MsgBox(vxLang.xml_mabiexsist, MsgBoxStyle.Critical)
            Else
                If Not IO.Directory.Exists(vxConfig.MabiDir & "\package\wlab") Then
                    vxFS.Dir_Create(vxConfig.MabiDir & "\package\wlab")
                End If

                For Each Item As ListViewItem In Me.ListView1.Items
                    Dim tmpDirectory As String = vxConfig.MabiDir
                    If Item.SubItems(5).Text = "default" Then
                        tmpDirectory &= "\package\wlab\"
                    Else
                        tmpDirectory &= "\package\" & Item.SubItems(5).Text & "\"
                    End If
                    vxLog.Show()
                    If vxLog.logFileDownloader(Item.SubItems(6).Text, Item.SubItems(7).Text, tmpDirectory) = True Then
                        vxLog.appendLog("Complete!")

                        Dim FileInfo As String = ""
                        FileInfo &= "<?xml version=""1.0"" encoding=""utf-8""?>"
                        FileInfo &= "<PackInfo>"
                        FileInfo &= "<Info Type=""" & Item.SubItems(0).Text & """ Filename=""" & Item.SubItems(1).Text & """ Date=""" & ClickTime & """ Info=""" & Item.SubItems(3).Text & """ Install=""" & vxLang.xml_installedtext & """ PackageVer=""" & Item.SubItems(9).Text & """ />"
                        FileInfo &= "</PackInfo>"

                        My.Computer.FileSystem.WriteAllText(tmpDirectory & Item.SubItems(7).Text & ".xml", FileInfo, False)
                    Else
                        vxFS.FileDelete(tmpDirectory & Item.SubItems(7).Text)
                        vxFS.FileDelete(tmpDirectory & Item.SubItems(7).Text & ".xml")
                        vxLog.appendLog(Item.SubItems(0).Text & "다운로드 실패")
                    End If
                Next
            End If
            vxLog.appendLog(vxLang.download_success)
            MsgBox(vxLang.download_success, MsgBoxStyle.Information)
            Me.Button1.Enabled = True
            Me.Button3.Enabled = True
            ListXMLParse()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Me.Button1.Enabled = False
        Me.Button3.Enabled = False

        Dim checkedCount As Integer = 0

        checkedCount += Me.list_default.CheckedItems.Count
        checkedCount += Me.list_Select.CheckedItems.Count
        checkedCount += Me.list_decoration.CheckedItems.Count
        checkedCount += Me.list_Simpling.CheckedItems.Count
        checkedCount += Me.list_View.CheckedItems.Count
        checkedCount += Me.list_field.CheckedItems.Count
        checkedCount += Me.list_Service.CheckedItems.Count

        If checkedCount = 0 Then
            MsgBox(vxLang.xml_selecterror)
            Me.Button1.Enabled = True
            Me.Button3.Enabled = True
            Exit Sub
        End If

        Dim objArr As New ArrayList

        objArr.Add(Me.list_default)
        objArr.Add(Me.list_Select)
        objArr.Add(Me.list_decoration)
        objArr.Add(Me.list_Simpling)
        objArr.Add(Me.list_View)
        objArr.Add(Me.list_field)
        objArr.Add(Me.list_Service)

        Me.ListView1.Clear()

        For index = 0 To objArr.Count - 1
            For Each Item As ListViewItem In objArr(index).CheckedItems
                Dim tmpArr As String() = {
                                            "",
                                            Item.SubItems(0).Text,
                                            Item.SubItems(1).Text,
                                            Item.SubItems(2).Text,
                                            Item.SubItems(3).Text,
                                            Item.SubItems(4).Text,
                                            Item.SubItems(5).Text,
                                            Item.SubItems(6).Text,
                                            Item.SubItems(7).Text,
                                            Item.SubItems(8).Text,
                                            Item.SubItems(9).Text
                                        }
                Dim tmpItem = New ListViewItem(tmpArr)
                ListView1.Items.Add(tmpItem)
            Next
        Next

        '...위
        If My.Computer.FileSystem.DirectoryExists(vxConfig.MabiDir & "\package") = False Then

            MsgBox(vxLang.xml_mabiexsist)

            Me.Button1.Enabled = True
            Me.Button3.Enabled = True
            Exit Sub

        End If

        For Each Item As ListViewItem In Me.ListView1.Items
            Dim tmpDirectory As String = vxConfig.MabiDir
            If Item.SubItems(5).Text = "default" Then
                tmpDirectory &= "\package\wlab\"
            Else
                tmpDirectory &= "\package\" & Item.SubItems(5).Text & "\"
            End If
            ' 팩 관련 파일들 삭제
            vxFS.FileDelete(tmpDirectory & Item.SubItems(7).Text)
            vxFS.FileDelete(tmpDirectory & Item.SubItems(7).Text & ".xml")

        Next

        Me.Button1.Enabled = True
        Me.Button3.Enabled = True

        ListXMLParse()
    End Sub

    Private Sub list_default_SelectedIndexChanged(sender As Object, e As EventArgs) Handles list_default.MouseClick, list_decoration.MouseClick, _
        list_field.MouseClick, list_Select.MouseClick, list_Service.MouseClick, list_Simpling.MouseClick, list_View.MouseClick
        If sender.Items(sender.FocusedItem.Index).SubItems(9).Text <> "" Then
            vxinfoView.Width = 320
            vxinfoView.Height = Me.Height
            vxinfoView.Location = New System.Drawing.Point(Me.Location.X + Me.Width, Me.Location.Y)

            vxinfoView.Show()
            vxinfoView.Focus()
            Me.Focus()
            Dim tmpURL As New System.Uri(sender.Items(sender.FocusedItem.Index).SubItems(9).Text)
            vxinfoView.WebBrowser1.Url = tmpURL
        End If

    End Sub

    Private Sub 마비노기공지보기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 마비노기공지보기ToolStripMenuItem.Click
        vxWebView.Show()
        Dim tmpURL As New System.Uri("http://mabinogi.nexon.com/PageletRef/notice_G2.html")
        vxWebView.WebBrowser1.Url = tmpURL
    End Sub

    Private Sub 프로그램정보ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 프로그램정보ToolStripMenuItem.Click
        vxWebView.Show()
        Dim tmpURL As New System.Uri("http://willowslab.com/willowspack.info.php")
        vxWebView.WebBrowser1.Url = tmpURL
    End Sub

    Private Sub 버전변경ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 버전변경ToolStripMenuItem.Click
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\willowsPack", "mode", "Pro")
        Process.Start(vxConfig.dirme & "\willowspack.exe")
        End
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("이 기능의 이용 약관은 버들웹의 가입 이용 약관을 준수합니다. 이에 동의하지 않으시면 기능의 사용을 중단해주시기 바랍니다. 동의하는 경우에만 예 를 눌러주시기 바랍니다.", MsgBoxStyle.OkCancel Or vbMsgBoxSetForeground) = MsgBoxResult.Ok Then
            Me.wlabID.Visible = True
            Me.wlabPW.Visible = True
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If vxFS.Dir_Exists(vxConfig.MabiDir & "\package\Lumipack\") Then
            vxFS.Dir_Delete(vxConfig.MabiDir & "\package\lumipack\")
        End If
        If vxFS.File_Exists(vxConfig.MabiDir & "\dinput8.dll") Then
            vxFS.File_Delete(vxConfig.MabiDir & "\dinput8.dll")
        End If
        If vxFS.File_Exists(vxConfig.MabiDir & "\hsluncher.exe") Then
            vxFS.File_Delete(vxConfig.MabiDir & "\hsluncher.exe")
        End If
        MsgBox("루미팩을 완벽하게 삭제하였습니다.")
        Me.Button5.Visible = False
    End Sub
End Class