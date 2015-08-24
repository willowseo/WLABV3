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
    Dim ispatched As Integer = 0
    Private Sub readList(Optional ByVal mode As Integer = 0, Optional ByVal serverID As String = "", Optional ByVal serverURL As String = "")
        '어떤 상황에서도 예외는 있을 것.
        Try
            Dim settingserver As String = Nothing
            If Not IO.Directory.Exists(WLAB.ROOT & "data\") Then
                IO.Directory.CreateDirectory(Setting.Dir & " \data\")
            End If

            If IO.File.Exists(WLAB.ROOT & "data\serversetting.dat") Then
                settingserver = My.Computer.FileSystem.ReadAllText(WLAB.ROOT & "data\serversetting.dat")
            Else
                My.Computer.FileSystem.WriteAllText(WLAB.ROOT & "data\serversetting.dat", "버들실험실 공식 서버 I", False)
            End If

            Me.ServerList.Items.Clear()

            'added default server
            Me.ServerList.Items.Add(New ListViewItem({"버들실험실 공식 서버 I", "http://library.willowslab.com/lib/"}))

            'read server List XML
            Dim listfile As String = ""
            listfile &= "<?xml version=""1.0"" encoding=""utf-8""?>" & vbNewLine
            listfile &= "<ServerList>" & vbNewLine

            If Not IO.File.Exists(WLAB.ROOT & "data\serverlist.xml") Then
                My.Computer.FileSystem.WriteAllText(WLAB.ROOT & "data\serverlist.xml", listfile & "</ServerList>", False)
            End If

            Dim serverListFile As New Xml.XmlDocument
            serverListFile.Load(WLAB.ROOT & "data\serverlist.xml")
            Dim listExtract As Xml.XmlNodeList = serverListFile.SelectNodes("/ServerList/ServerInfo")
            For Each Server As Xml.XmlElement In listExtract
                Dim SVID As String = Server.GetAttribute("ServerID")
                Dim SVLN As String = Server.GetAttribute("ServerURL")
                If Not SVID = "" And Not SVLN = "" Then
                    If SVID = settingserver Then
                        Func.DefaultServerName = SVID
                        Setting.Server(1) = SVLN
                    End If
                    If mode = 2 And serverID = SVID Then
                    Else
                        Me.ServerList.Items.Add(New ListViewItem({SVID, SVLN}))
                        listfile &= vbTab & "<ServerInfo ServerID=""" & SVID & """ ServerURL=""" & SVLN & """ />" & vbNewLine
                    End If
                End If
            Next
            Me.SelectedServer.Text = settingserver

            'add new server
            If mode = 1 Then
                If serverID = "" Or serverURL = "" Then
                    InfoMessage("서버 이름이나 리스트 주소가 비어있습니다.")
                    Exit Sub
                ElseIf serverURL.IndexOf("http://") <> -1 Or serverURL.IndexOf("ftp://") <> -1 Then
                    listfile &= vbTab & "<ServerInfo ServerID=""" & serverID & """ ServerURL=""" & serverURL & """ />" & vbNewLine
                Else
                    InfoMessage("서버 주소가 올바르지 않은 것 같습니다.")
                    Exit Sub
                End If
            End If

            listfile &= "</ServerList>"
            If mode = 1 Or mode = 2 Then
                If mode = 1 Then
                    Me.ServerList.Items.Add(New ListViewItem({serverID, serverURL}))
                End If
                IO.File.Delete(WLAB.ROOT & "data\serverlist.xml")
                My.Computer.FileSystem.WriteAllText(WLAB.ROOT & "data\serverlist.xml", listfile, False)
            End If
        Catch ex As Exception
        End Try
    End Sub


    Public Sub readPackList()
        Try
            If Me.working Then
                'MsgBox("이미 리스트가 갱신중입니다!", MsgBoxStyle.Critical Or vbMsgBoxSetForeground)
                Exit Sub
            End If
            Me.clearList()
            Me.plistEabledChange(False)
            Me.working = True
            Dim url As String = Setting.Server(1)

            If Setting.Server(1).IndexOf("willowslab.com/lib/") <> -1 Then
                If Setting.ID.Length > 0 And Setting.PW.Length > 0 Then
                    url &= "?id=" & Setting.ID & "&pw=" & Setting.PW
                End If
            End If

            If Func.webClient(url, Setting.Dir, "\data\serverlistdata.xml") Then
                Try
                    Dim listfile As New Xml.XmlDocument
                    Dim list As Xml.XmlNodeList = Nothing
                    addLog("목록 갱신 시작...")
                    addLog("목록 읽는 중...")

                    listfile.Load(WLAB.ROOT & "data\serverlistdata.xml")
                    list = listfile.SelectNodes("/Files/Message")
                    For Each node As Xml.XmlElement In list
                        Dim msg As String = node.GetAttribute("message")
                        If msg.Length > 0 Then
                            MsgBox(msg, MsgBoxStyle.Information Or MsgBoxStyle.RetryCancel Or vbMsgBoxSetForeground, "젠장, 환장하겠군")
                        End If
                    Next

                    listfile.Load(WLAB.ROOT & "data\serverlistdata.xml")
                    list = listfile.SelectNodes("/Files/FileInfo")
                    For Each node As Xml.XmlElement In list
                        Dim type As String = node.GetAttribute("type")
                        Dim dir As String = node.GetAttribute("dir")
                        Dim remote As String = node.GetAttribute("remote")
                        Dim size As Long = Val(node.GetAttribute("size"))
                        Dim mdate As String = node.GetAttribute("modify_date")
                        Dim info As String = node.GetAttribute("info")
                        Dim filename As String = dir.Substring(dir.LastIndexOf("/") + 1)
                        Dim infoname As String = filename.Replace(filename.Substring(filename.LastIndexOf(".") + 1), "php")
                        Dim packname As String = filename.Replace(".pack", "")
                        Dim patchflag As Boolean = False

                        Try
                            Dim Down As New Net.WebClient
                            Down.Encoding = Encoding.UTF8
                            Dim file As String = Down.DownloadString(Setting.Server(1) & "information/" & type & "/" & infoname)

                            If file.Length > 10 Then
                                filename = readCommand.Parse(file, "<title>", "</title>")
                            End If
                        Catch ex As Exception
                        End Try
                        Dim installpath As String = WLAB.ROOT & "resources\" & dir.Replace("/", "\")
                        If remote.Trim <> "default" Then
                            installpath = remote
                        End If
                        Dim localmodifytime As Long = Nothing

                        If File.Exists(Setting.Mabi & "\" & dir.Replace("/", "\")) Then

                            Dim fpath As String = Setting.Mabi & "\" & dir.Replace("/", "\")
                            Dim extension As String = fpath.Substring(fpath.LastIndexOf(".") + 1)

                            If extension = "lab" Then
                                Dim filever As Integer = BitConverter.ToUInt32(File.ReadAllBytes(Setting.Mabi & "\" & dir.Replace("/", "\")), 0)
                                Dim mabiver As Integer = BitConverter.ToUInt32(File.ReadAllBytes(Setting.Mabi & "\version.dat"), 0)
                                addLog("")
                                addLog("CHECK FILE... " & fpath)
                                addLog("[-------- L A B O R A T O R Y _ E X P E R I M E N T _ D O C U M E N T _ C H E C K --------]")
                                addLog("[-                                             PATCHED : " & filever & "                             -]")
                                addLog("[-                                             CURRENT : " & mabiver & "                             -]")
                                If filever < mabiver Then
                                    patchflag = True
                                    addLog("[-                                        PATCH NEED                                     -]")
                                End If
                                addLog("[-----------------------------------------------------------------------------------------]")
                            End If
                        End If


                        If File.Exists(Setting.Mabi & "\" & dir.Replace("/", "\")) Then
                            localmodifytime = My.Computer.FileSystem.GetFileInfo(Setting.Mabi & "\" & dir.Replace("/", "\")).LastWriteTime().ToString("yyyyMMddHH")
                            If localmodifytime < mdate Then
                                patchflag = True
                            End If
                        End If

                        Dim infourl As String = Setting.Server(1) & "information/" & type & "/"

                        If type = "select" And infoname.IndexOf("_") > -1 Then
                            infourl &= infoname.Replace(infoname.Substring(infoname.LastIndexOf("_"), infoname.LastIndexOf(".") - infoname.LastIndexOf("_")), "")
                            Dim tmpfilename As String = filename.Substring(filename.LastIndexOf("_") + 1, filename.LastIndexOf(".") - filename.LastIndexOf("_") - 1)
                            infourl &= "?part=" & tmpfilename
                        Else
                            infourl &= infoname
                        End If

                        Dim localtime As String = Nothing

                        If localmodifytime > 0 Then
                            localtime = localmodifytime
                            localtime = localtime.Substring(2, 2) & "-" & localtime.Substring(4, 2) & "-" & localtime.Substring(6, 2)
                        End If

                        Dim list_item As New ListViewItem({filename, mdate.Substring(2, 2) & "-" & mdate.Substring(4, 2) & "-" & mdate.Substring(6, 2), localtime, installpath, infourl, dir, size, mdate})

                        If patchflag Then
                            list_item.BackColor = Color.MistyRose
                            list_item.Checked = True
                            ispatched += 1
                        End If

                        If type = "replace" Then
                            addPackList(list_item, 1)
                        ElseIf type = "refresh" Then
                            addPackList(list_item, 2)
                        ElseIf type = "select" Then
                            addPackList(list_item, 3)
                        Else
                            addPackList(list_item, 4)
                        End If
                    Next

                    If ispatched > 0 Then
                        MsgBox("패치된 파일이 " & ispatched & " 개 있습니다. 패치를 진행해 주세요.", MsgBoxStyle.Information)
                    End If

                    If updated = False Then
                        Me.working = False
                        Me.plistEabledChange(True)
                    Else
                        updated = False
                        Me.working = False
                        InstallFile()
                    End If
                Catch ex As Exception
                    addLog("오류가 발생했습니다. 문의로 해당 로그를 모두 복사하여 제출해주시면 오류 해결에 많은 도움이 됩니다." & vbNewLine & ex.GetBaseException.ToString)
                End Try
            Else

            End If
        Catch ex As Exception
            addLog("오류가 발생했습니다. 문의로 해당 로그를 모두 복사하여 제출해주시면 오류 해결에 많은 도움이 됩니다." & vbNewLine & ex.GetBaseException.ToString)
        End Try
    End Sub
    Public Sub addPackList(ByVal [ListItem] As ListViewItem, ByVal [tabID] As Integer)
        If Me.plist1.InvokeRequired Or Me.plist2.InvokeRequired Or Me.plist3.InvokeRequired Or Me.plist4.InvokeRequired Then
            Try
                Dim add As New AddListItem(AddressOf addPackList)
                Me.Invoke(add, New Object() {[ListItem], [tabID]})
            Catch ex As Exception
            End Try
        Else
            Select Case [tabID]
                Case 1
                    Me.plist1.Items.Add([ListItem])
                Case 2
                    Me.plist2.Items.Add([ListItem])
                Case 3
                    Me.plist3.Items.Add([ListItem])
                Case 4
                    Me.plist4.Items.Add([ListItem])
            End Select

            Me.plist1.Sorting = SortOrder.Ascending
            Me.plist1.Sort()
            Me.plist2.Sorting = SortOrder.Ascending
            Me.plist2.Sort()
            Me.plist3.Sorting = SortOrder.Ascending
            Me.plist3.Sort()
            Me.plist4.Sorting = SortOrder.Ascending
            Me.plist4.Sort()
        End If
    End Sub

    Public Sub clearList(Optional ByVal [bool] As Boolean = False)

        If Me.plist1.InvokeRequired Or Me.plist2.InvokeRequired Or Me.plist3.InvokeRequired Or Me.plist4.InvokeRequired Then
            Try
                Dim add As New booleans(AddressOf clearList)
                Me.Invoke(add, New Object() {[bool]})
            Catch ex As Exception
            End Try
        Else
            Me.plist1.Items.Clear()
            Me.plist2.Items.Clear()
            Me.plist3.Items.Clear()
            Me.plist4.Items.Clear()
        End If
    End Sub
End Class
