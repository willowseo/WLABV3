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
    Public Function USERVAR(ByVal doc As String, ByVal path As String) As String
        doc = doc.Replace("%MY_PROJECT%", path.Substring(0, path.LastIndexOf(".")))
        doc = doc.Replace("%OFFICIAL%", "http://library.willowslab.com/")
        doc = doc.Replace("%MABINOGI%", Setting.Mabi)
        doc = doc.Replace("%MABINOGI_DATA%", Setting.Mabi & "\data")
        doc = doc.Replace("%MABINOGI_PACK%", Setting.Mabi & "\package")
        doc = doc.Replace("%MABINOGI_MP3%", Setting.Mabi & "\mp3")
        doc = doc.Replace("%MABINOGI_DB%", Setting.Mabi & "\data\db")
        doc = doc.Replace("%MABINOGI_GFX%", Setting.Mabi & "\data\gfx")
        doc = doc.Replace("%MABINOGI_CHAR%", Setting.Mabi & "\data\gfx\char")
        doc = doc.Replace("%MABINOGI_FONT%", Setting.Mabi & "\data\gfx\font")
        doc = doc.Replace("%MABINOGI_FX%", Setting.Mabi & "\data\gfx\fx")
        doc = doc.Replace("%MABINOGI_GUI%", Setting.Mabi & "\data\gfx\gui")
        doc = doc.Replace("%MABINOGI_IMAGE%", Setting.Mabi & "\data\gfx\image")
        doc = doc.Replace("%MABINOGI_IMAGE2%", Setting.Mabi & "\data\gfx\image2")
        doc = doc.Replace("%MABINOGI_INTRO%", Setting.Mabi & "\data\gfx\intro")
        doc = doc.Replace("%MABINOGI_SCENE%", Setting.Mabi & "\data\gfx\scene")
        doc = doc.Replace("%MABINOGI_STYLE%", Setting.Mabi & "\data\gfx\style")
        doc = doc.Replace("%MABINOGI_LOCAL%", Setting.Mabi & "\data\local")
        doc = doc.Replace("%MABINOGI_CODE%", Setting.Mabi & "\data\local\code")
        doc = doc.Replace("%MABINOGI_XML%", Setting.Mabi & "\data\local\xml")
        doc = doc.Replace("%MABINOGI_LOCALE%", Setting.Mabi & "\data\locale")
        doc = doc.Replace("%MABINOGI_MATERIAL%", Setting.Mabi & "\data\material")
        doc = doc.Replace("%MABINOGI_SOUND%", Setting.Mabi & "\data\sound")
        doc = doc.Replace("%MABINOGI_WORLD%", Setting.Mabi & "\data\world")
        doc = doc.Replace("%MY_MABINOGI%", Setting.myDoc & "\마비노기")
        doc = doc.Replace("%PROGRAMDIR%", Setting.Dir)

        Return doc
    End Function
    Private Function compressd(ByVal str As String) As String
        Dim stb As New StringBuilder
        stb.Append(str)
        stb.Replace(Chr(10), " ")
        stb.Replace(Chr(13), "")
        stb.Replace(Chr(9), "")
        Return stb.ToString.Trim
    End Function
    Private Function ReturnNodeValue(ByVal vNode As XmlElement) As String
        Dim Front As String = Mid(vNode.InnerXml, 1, 15)
        If InStr(Mid(vNode.InnerXml, 1, 15), "<![CDATA[") > 0 Then
            Return vNode.InnerText
        Else
            Dim Pattern As String = "^<!\[CDATA\[(.*)\]\]>$"
            Return Regex.Replace(vNode.InnerXml, Pattern, "$1")
        End If
    End Function
    Public Sub runUnpack()
        '217341
        Try
            My.Computer.FileSystem.DeleteDirectory(WLAB.TEMP, FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception
        End Try

        Dim worked As Integer = 0
        Dim resDownload As Integer = 0
        Dim RES_ As New List(Of ResList)
        Dim patchpasslist As New List(Of String)
        ProgressMainTrd(10000)
        ProgressSubTrd(0)
        progressTextChange("패키지 파일을 복구하고 있습니다.")
        DataReplacer(False)
        progressTextChange("작업량을 계산하고 있습니다...")
        workQueue = 0

        Try
            Dim cwladder As New List(Of String)
            For Each lab As String In CMDworkList 'Directory.GetFiles(WLAB.ROOT & "resources\lab\")
                'MsgBox(lab)
                Dim DOC As New StringBuilder
                Dim XML As New XmlDocument
                Dim tmpdoc As String = ""
                Dim root As String = "/Library_Command/"
                Dim getFileList As New List(Of String)

                '
                ' BASE64인지 모호하므로 그냥 try로 잡아줌 + 주석제거
                '
                Try
                    tmpdoc = Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(lab)))
                Catch ex As Exception
                    tmpdoc = File.ReadAllText(lab)
                End Try

                tmpdoc = tmpdoc.Replace(Chr(10) & Chr(13), Chr(10))
                For Each line As String In tmpdoc.Split(Chr(10))
                    If line.Trim = "" Then
                        Continue For
                    Else
                        DOC.Append(line) '여기선 스트링빌더가 메모리를 더 적게 생성한다.
                    End If
                Next

                Dim document As String = DOC.ToString
                document = USERVAR(document, lab)
                XML.LoadXml(document)

                For Each node As XmlElement In XML.SelectNodes("/Library_Command/CMD/*")
                    Try
                        Select Case node.Name.ToLower
                            Case "getfile"
                                For Each attr As XmlAttribute In node.Attributes
                                    Select Case attr.Name.ToLower
                                        Case "path"
                                            getFileList.Add(attr.Value.Trim)
                                    End Select
                                Next
                        End Select
                    Catch ex As Exception
                        MsgBox(ex.GetBaseException.ToString)
                    End Try
                Next

                Directory.CreateDirectory(WLAB.LABS)
                Dim dirlist As New List(Of String)

                For Each str As String In Directory.GetFiles(WLAB.ROOT & "\resources\lab")
                    Dim fileinfo As New FileInfo(str)
                    If fileinfo.Extension = ".lab" Then
                        dirlist.Add(str)
                    End If
                Next

                For Each str As String In Directory.GetFiles(WLAB.LABS)
                    Dim fileinfo As New FileInfo(str)
                    If fileinfo.Extension = ".lab" Then
                        dirlist.Add(str)
                    End If
                Next

                For Each Path As String In dirlist
                    DOC.Clear()
                    Try
                        tmpdoc = Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(Path)))
                    Catch ex As Exception
                        tmpdoc = File.ReadAllText(Path)
                    End Try

                    tmpdoc = tmpdoc.Replace(Chr(10) & Chr(13), Chr(10))
                    For Each line As String In tmpdoc.Split(Chr(10))
                        If line.Trim = "" Then
                            Continue For
                        Else
                            DOC.Append(line) '여기선 스트링빌더가 메모리를 더 적게 생성한다.
                        End If
                    Next

                    document = DOC.ToString
                    document = USERVAR(document, Path)
                    XML.LoadXml(document)

                    Dim pathlisttrue As Boolean = False
                    For Each node As XmlElement In XML.SelectNodes("/Library_Command/CMD/*")
                        Try
                            Select Case node.Name.ToLower
                                Case "getfile"
                                    For Each attr As XmlAttribute In node.Attributes
                                        Select Case attr.Name.ToLower
                                            Case "path"
                                                For Each gfl As String In getFileList
                                                    If gfl.Trim = attr.Value.Trim Then
                                                        pathlisttrue = True
                                                    End If
                                                Next
                                        End Select
                                    Next
                            End Select
                        Catch ex As Exception
                            Console.WriteLine(ex.GetBaseException.ToString)
                        End Try
                    Next

                    If pathlisttrue Then
                        Dim tmp As Boolean = True
                        For Each dm As String In CMDworkList
                            If dm = Path Then
                                tmp = False
                            End If
                        Next

                        If lab.Trim.ToLower = Path.Trim.ToLower Then
                            tmp = False
                        End If

                        If tmp Then
                            cwladder.Add(Path)
                        End If
                    End If
                Next
            Next

            For Each str As String In cwladder
                Dim tmp As Boolean = True
                For Each pat As String In CMDworkList
                    If InStr(pat, str.Trim.Replace("\\", "\")) Then
                        tmp = False
                    End If
                Next

                If tmp Then
                    CMDworkList.Add(str)
                End If
            Next

        Catch ex As Exception
            addLog(ex.GetBaseException.ToString)
        End Try

        Try
            For Each lab As String In CMDworkList
                Dim DOC As New StringBuilder
                Dim XML As New XmlDocument
                Dim tmpdoc As String = ""
                Dim root As String = "/Library_Command/"
                Dim getFileList As New List(Of String)
                '
                ' BASE64인지 모호하므로 그냥 try로 잡아줌 + 주석제거
                '
                Try
                    tmpdoc = Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(lab)))
                Catch ex As Exception
                    tmpdoc = File.ReadAllText(lab)
                End Try

                tmpdoc = tmpdoc.Replace(Chr(10) & Chr(13), Chr(10))
                For Each line As String In tmpdoc.Split(Chr(10))
                    If line.Trim = "" Then
                        Continue For
                    Else
                        DOC.Append(line) '여기선 스트링빌더가 메모리를 더 적게 생성한다.
                    End If
                Next

                Dim document As String = DOC.ToString
                document = USERVAR(document, lab)
                XML.LoadXml(document)
                For Each node As XmlElement In XML.SelectNodes("/Library_Command/CMD/*")
                    Try
                        Select Case node.Name.ToLower
                            Case "getfile"
                                For Each attr As XmlAttribute In node.Attributes
                                    Select Case attr.Name.ToLower
                                        Case "path"
                                            getFileList.Add(attr.Value.Trim)
                                    End Select
                                Next
                        End Select
                    Catch ex As Exception
                    End Try
                Next

                If InStr(document.ToLower, "<selector") Then

                Else
                    Try
                        Dim thisver As Integer = BitConverter.ToUInt32(File.ReadAllBytes(Setting.Mabi & "\" & lab.Replace(WLAB.ROOT, "")), 0)
                        Dim mabiver As Integer = BitConverter.ToUInt32(File.ReadAllBytes(Setting.Mabi & "\version.dat"), 0)
                        Dim libs As String = ""
                        Dim patch As Boolean = False
                        For Each Path As String In Directory.GetFiles(WLAB.CONF & "lib\server_main\")
                            If InStr(Path, mabiver.ToString) Then
                                libs = File.ReadAllText(Path)
                                For Each f As String In getFileList
                                    If InStr(libs, f) Then
                                        patch = True
                                    End If
                                Next
                            End If
                        Next
                        If patch Then
                            Continue For
                        Else
                            patchpasslist.Add(lab)
                            File.WriteAllBytes(Setting.Mabi & "\" & lab.Replace(WLAB.ROOT, ""), File.ReadAllBytes(Setting.Mabi & "\version.dat"))
                        End If
                    Catch ex As Exception

                    End Try
                End If
            Next

        Catch ex As Exception
            addLog(ex.GetBaseException.ToString)
        End Try
        For Each URL As String In CMDworkList
            Try
                Dim DOC As New StringBuilder
                Dim XML As New XmlDocument
                Dim ELEMENT As XmlElement
                Dim NODE As XmlNodeList
                Dim tmpdoc As String = ""
                Dim root As String = "/Library_Command/"
                '
                ' BASE64인지 모호하므로 그냥 try로 잡아줌 + 주석제거
                '
                Try
                    tmpdoc = Regex.Replace(Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(URL))), "\<\!\-\-.*\-\-\>", "")
                Catch ex As Exception
                    tmpdoc = Regex.Replace(File.ReadAllText(URL), "\<\!\-\-.*\-\-\>", "")
                End Try
                While (tmpdoc.IndexOf("<!--") > -1 And tmpdoc.IndexOf("-->"))
                    Try
                        tmpdoc = tmpdoc.Replace(readCommand.ParseOuter(tmpdoc, "<!--", "-->"), "")
                    Catch ex As Exception
                        Exit While
                    End Try
                End While
                '
                ' 쓸모없는 빈 줄 제거
                '
                tmpdoc = tmpdoc.Replace(Chr(10) & Chr(13), Chr(10))
                For Each line As String In tmpdoc.Split(Chr(10))
                    If line.Trim = "" Then
                        Continue For
                    Else
                        DOC.Append(line) '여기선 스트링빌더가 메모리를 더 적게 생성한다.
                    End If
                Next

                Dim document As String = DOC.ToString
                document = USERVAR(document, URL)
                XML.LoadXml(document)

                Dim VER As Integer = 100 '이거보다 높으면 못읽음
                ELEMENT = XML.SelectSingleNode("/Library_Command")
                If Val(ELEMENT.GetAttribute("version")) < VER Then
                    Throw New Exception("이 실험 계획서는 해당 실험실에서는 진행할 수 없는 도구를 사용합니다. 버전 오류거나, 최신버전이 나온 경우입니다. 확인해주세요.")
                End If

                Dim Type As String = ""
                Dim Info As String = ""
                Dim Author As String = ""
                Dim Desc As String = ""
                Dim Auto As Boolean = False

                ELEMENT = XML.SelectSingleNode("/Library_Command/Head")
                For Each nd As XmlElement In ELEMENT
                    Dim tmp As XmlElement = Nothing
                    tmp = XML.SelectSingleNode("/Library_Command/Head/" & nd.Name)

                    Select Case nd.Name
                        Case "Info"
                            Type = tmp.GetAttribute("Type")
                            Info = tmp.GetAttribute("Info")
                            Author = tmp.GetAttribute("Author")
                        Case "Type"
                            If tmp.GetAttribute("set").ToUpper = "AUTO".ToUpper Then
                                Auto = True
                            Else
                                Auto = False
                            End If
                        Case "ResList"
                            tmp.SelectNodes("/Library_Command/Head/ResList/Res")
                            For Each node2 As XmlElement In tmp
                                Dim resitem As New ResList
                                resitem.path = node2.GetAttribute("path")
                                resitem.url = node2.GetAttribute("url").Replace("\", "/")
                                RES_.Add(resitem)
                            Next
                        Case "description"
                            Desc = ReturnNodeValue(tmp)
                    End Select
                Next

                ELEMENT = XML.SelectSingleNode(root & "CMD")
                NODE = XML.SelectNodes(root & "CMD/*")
                For Each nd As XmlElement In NODE
                    Select Case nd.Name.ToLower
                        Case "getfile"
                            workQueue += 1
                            progressTextChange("작업량을 계산하고 있습니다... " & workQueue.ToString)
                            For Each child As XmlElement In nd.ChildNodes
                                Select Case child.Name
                                    Case "ReplaceText"
                                        workQueue += 1
                                        progressTextChange("작업량을 계산하고 있습니다... " & workQueue.ToString)
                                    Case "ReplaceXPath"
                                        workQueue += 1
                                        progressTextChange("작업량을 계산하고 있습니다... " & workQueue.ToString)
                                    Case "DeleteXPath"
                                        workQueue += 1
                                        progressTextChange("작업량을 계산하고 있습니다... " & workQueue.ToString)
                                End Select
                            Next
                        Case "copyfile"
                            workQueue += 1
                            progressTextChange("작업량을 계산하고 있습니다... " & workQueue.ToString)
                        Case "movefile"
                            workQueue += 1
                            progressTextChange("작업량을 계산하고 있습니다... " & workQueue.ToString)
                        Case "deletefile"
                            workQueue += 1
                            progressTextChange("작업량을 계산하고 있습니다... " & workQueue.ToString)
                        Case "xmlcreate"
                            workQueue += 1
                            progressTextChange("작업량을 계산하고 있습니다... " & workQueue.ToString)
                    End Select
                Next
            Catch ex As XmlException
                Dim tmpdoc As String = ""
                Dim DOC As New StringBuilder
                '
                ' BASE64인지 모호하므로 그냥 try로 잡아줌 + 주석제거
                '
                Try
                    tmpdoc = Regex.Replace(Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(URL))), "\<\!\-\-.*\-\-\>", "")
                Catch
                    tmpdoc = Regex.Replace(File.ReadAllText(URL), "\<\!\-\-.*\-\-\>", "")
                End Try

                While (tmpdoc.IndexOf("<!--") > -1 And tmpdoc.IndexOf("-->"))
                    Try
                        tmpdoc = tmpdoc.Replace(readCommand.ParseOuter(tmpdoc, "<!--", "-->"), "")
                    Catch ccc As Exception
                        Exit While
                    End Try
                End While
                '
                ' 쓸모없는 빈 줄 제거
                '
                tmpdoc = tmpdoc.Replace(Chr(10) & Chr(13), Chr(10))
                For Each line As String In tmpdoc.Split(Chr(10))
                    If line.Trim = "" Then
                        Continue For
                    Else
                        DOC.Append(line) '여기선 스트링빌더가 메모리를 더 적게 생성한다.
                    End If
                Next

                Dim document As String = DOC.ToString
                Dim msg As String = ex.Message

                addLog("※ 이 실험 계획서에 문제가 있습니다!" & vbCrLf)
                addLog(msg.Substring(0, msg.LastIndexOf(".")) & vbCrLf & vbCrLf)
                Dim linetmp As String = msg.Substring(msg.LastIndexOf("줄") + 1).Trim
                linetmp = linetmp.Substring(0, linetmp.LastIndexOf(","))
                addLog(" * 줄 : " & linetmp & vbCrLf)

                Dim lentemp As String = msg.Substring(msg.LastIndexOf("위치") + 2).Trim
                addLog(" * 위치 : " & lentemp & vbCrLf)
                DOC.Replace(Chr(13), "")
                Exit For
            Catch ex As Exception
                Console.WriteLine(ex.GetBaseException.ToString)
            End Try
        Next

        Try
            progressTextChange("필요한 파일을 다운로드 하고 있습니다...")
            Dim curr As Integer = 0
            For Each Resource As ResList In RES_
                Console.WriteLine(Resource.path)
                Console.WriteLine(Resource.url)

                Dim path As String = Resource.path
                Dim url As String = Resource.url
                curr += 1

                Console.WriteLine(path.Substring(0, path.LastIndexOf("\") + 1))
                Console.WriteLine(path.Substring(path.LastIndexOf("\") + 1))
                If Func.webClient(url, path.Substring(0, path.LastIndexOf("\") + 1), path.Substring(path.LastIndexOf("\") + 1)) Then
                    progressTextChange("설치에 필요한 파일을 다운로드 하고 있습니다... " & curr & "/" & RES_.Count)
                End If
            Next
        Catch ex As Exception

        End Try
        Try
            For Each URL As String In CMDworkList
                Try
                    Dim DOC As New StringBuilder
                    Dim XML As New XmlDocument
                    Dim ELEMENT As XmlElement
                    Dim NODE As XmlNodeList
                    Dim tmpdoc As String = ""
                    Dim root As String = "/Library_Command/"
                    '
                    ' BASE64인지 모호하므로 그냥 try로 잡아줌 + 주석제거
                    '
                    Try
                        tmpdoc = Regex.Replace(Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(URL))), "\<\!\-\-.*\-\-\>", "")
                    Catch ex As Exception
                        tmpdoc = Regex.Replace(File.ReadAllText(URL), "\<\!\-\-.*\-\-\>", "")
                    End Try
                    While (tmpdoc.IndexOf("<!--") > -1 And tmpdoc.IndexOf("-->"))
                        Try
                            tmpdoc = tmpdoc.Replace(readCommand.Parse(tmpdoc, "<!--", "-->"), "")
                        Catch ex As Exception
                            Exit While
                        End Try
                    End While
                    '
                    ' 쓸모없는 빈 줄 제거
                    '
                    tmpdoc = tmpdoc.Replace(Chr(10) & Chr(13), Chr(10))
                    For Each line As String In tmpdoc.Split(Chr(10))
                        If line.Trim = "" Then
                            Continue For
                        Else
                            DOC.Append(line) '여기선 스트링빌더가 메모리를 더 적게 생성한다.
                        End If
                    Next

                    Dim document As String = DOC.ToString

                    If InStr(document.ToLower, "<selector") Then
                        Dim dpw As New datapackworker(document, URL)
                        If dpw.ShowDialog = Windows.Forms.DialogResult.OK Then
                            document = dpw.resultDocument
                        End If
                    End If

                    If InStr(document, ":\") Then
                        MsgBox("손상된 실험 문서입니다. 이 실험 계획서는 건너뜁니다.", MsgBoxStyle.Exclamation, "손상된 실험 문서")
                        Continue For
                    End If

                    document = USERVAR(document, URL)
                    XML.LoadXml(document)

                    ELEMENT = XML.SelectSingleNode(root & "CMD")
                    NODE = XML.SelectNodes(root & "CMD/*")
                    For Each snode As XmlElement In NODE
                        Select Case snode.Name.ToLower
                            Case "getfile"
                                Dim subject As String = snode.GetAttribute("Path")
                                Dim svpath As String = snode.GetAttribute("SavePath")
                                Dim isNew As String = snode.GetAttribute("isNew")
                                Dim excute As String = snode.GetAttribute("SavePath")
                                If compressd(subject) <> "" Then
                                    If Mabinogi.Export(subject) And excute.ToUpper <> "FALSE".ToUpper Then
                                        workingSubProgress(worked)
                                        For Each tnode As XmlElement In snode.ChildNodes
                                            Select Case tnode.Name
                                                Case "ReplaceText"
                                                    Dim search As String = ""
                                                    Dim replace As String = ""
                                                    For Each cnode As XmlElement In tnode.ChildNodes
                                                        Select Case cnode.Name
                                                            Case "Search"
                                                                search = ReturnNodeValue(cnode)
                                                            Case "Replace"
                                                                replace = ReturnNodeValue(cnode)
                                                        End Select
                                                    Next
                                                    ReplaceText(subject, search, replace)
                                                    workingSubProgress(worked)
                                                Case "ReplaceXPath"

                                                    Dim search As String = ""
                                                    Dim replace As String = ""
                                                    Dim mode As String = ""
                                                    Dim XPath As String = ""
                                                    Dim Attribute As String = ""
                                                    If tnode.GetAttribute("Excute").ToUpper = "FALSE" Then

                                                    Else
                                                        For Each cnode As XmlElement In tnode.ChildNodes
                                                            Select Case cnode.Name
                                                                Case "XPath"
                                                                    XPath = compressd(ReturnNodeValue(cnode))
                                                                Case "Attribute"
                                                                    Attribute = compressd(ReturnNodeValue(cnode))
                                                                Case "Value"
                                                                    mode = cnode.GetAttribute("Mode")
                                                                    For Each dnode As XmlElement In cnode.ChildNodes
                                                                        Select Case dnode.Name
                                                                            Case "Search"
                                                                                search = compressd(ReturnNodeValue(dnode))
                                                                            Case "Replace"
                                                                                replace = compressd(ReturnNodeValue(dnode))
                                                                        End Select
                                                                    Next
                                                            End Select
                                                        Next
                                                        If XPath <> "" And replace <> "" Then
                                                            If search = "" Then
                                                                search = replace
                                                                replace = ""
                                                                ReplaceXPath(subject, XPath, Attribute, search, append:=mode)
                                                            Else
                                                                ReplaceXPath(subject, XPath, Attribute, search, replace, mode)
                                                            End If
                                                        End If
                                                    End If
                                                    workingSubProgress(worked)
                                                Case "DeleteXPath"
                                                    If tnode.GetAttribute("Excute").ToUpper = "FALSE" Then

                                                    Else
                                                        Dim XPath As String = tnode.GetAttribute("XPath")
                                                        If XPath <> "" Then
                                                            DeleteXPath(subject, XPath)
                                                        End If
                                                        workingSubProgress(worked)
                                                    End If
                                            End Select
                                        Next
                                    End If
                                End If
                            Case "copyfile"
                                Dim SourcePath As String = snode.GetAttribute("SourcePath")
                                Dim DestinationPath As String = snode.GetAttribute("DestinationPath")
                                Dim Excute As String = snode.GetAttribute("Excute")
                                If SourcePath <> "" And DestinationPath <> "" And Excute.ToUpper <> "FALSE".ToUpper Then
                                    If File.Exists(SourcePath) Then
                                        If Not Directory.Exists(DestinationPath) Then
                                            Directory.CreateDirectory(DestinationPath.Substring(0, DestinationPath.LastIndexOf("\")))
                                        End If
                                        File.Copy(SourcePath, DestinationPath, True)
                                        Console.WriteLine("File Copy Source [" & SourcePath & "] to [" & DestinationPath & "]")
                                    End If
                                End If
                                workingSubProgress(worked)
                            Case "movefile"
                                Dim SourcePath As String = snode.GetAttribute("SourcePath")
                                Dim DestinationPath As String = snode.GetAttribute("DestinationPath")
                                Dim Excute As String = snode.GetAttribute("Excute")
                                If SourcePath <> "" And DestinationPath <> "" And Excute.ToUpper <> "FALSE".ToUpper Then
                                    If File.Exists(SourcePath) Then
                                        If Not Directory.Exists(DestinationPath) Then
                                            Directory.CreateDirectory(DestinationPath.Substring(0, DestinationPath.LastIndexOf("\")))
                                        End If
                                        File.Move(SourcePath, DestinationPath)
                                        Console.WriteLine("File Move Source [" & SourcePath & "] to [" & DestinationPath & "]")
                                    End If
                                End If
                                workingSubProgress(worked)
                            Case "deletefile"
                                Dim Path As String = snode.GetAttribute("Path")
                                Dim Excute As String = snode.GetAttribute("Excute")
                                If Path <> "" And Excute.ToUpper <> "FALSE".ToUpper Then
                                    If File.Exists(Path) Then
                                        Console.WriteLine("File Delete Source [" & Path & "]")
                                    End If
                                    File.Delete(Path)
                                End If
                                workingSubProgress(worked)
                            Case "xmlcreate"
                                Dim path As String = ""
                                Dim version As String = ""
                                Dim encodingset As Encoding = Encoding.UTF8
                                Dim encodingname As String = ""
                                Dim standalone As Boolean = False

                                For Each nd As XmlAttribute In snode.Attributes
                                    Select Case nd.Name.ToLower
                                        Case "path"
                                            path = nd.Value.Trim
                                        Case "version"
                                            version = nd.Value.Trim
                                        Case "standalone"
                                            If nd.Value.ToLower = "yes" Then
                                                standalone = True
                                            End If
                                        Case "encoding"
                                            If nd.Value.ToLower = "utf-16" Then
                                                encodingset = Encoding.Unicode
                                                encodingname = "UTF-16"
                                            ElseIf nd.Value.ToLower = "utf-8" Then
                                                encodingset = Encoding.UTF8
                                                encodingname = "UTF-8"
                                            ElseIf nd.Value.ToLower = "ansi" Then
                                                encodingset = Encoding.Default
                                                encodingname = "ANSI"
                                            End If
                                    End Select
                                Next

                                If Not Directory.Exists(path.Substring(0, path.LastIndexOf("\"))) Then
                                    Directory.CreateDirectory(path.Substring(0, path.LastIndexOf("\")))
                                End If

                                Dim xmldoc As String = "<?xml version=""" & version & """ encoding=""" & encodingname & """"

                                If standalone = True Then
                                    xmldoc &= " standalone=" & Chr(34) & "yes" & Chr(34) & "?>"
                                Else
                                    xmldoc &= "?>"
                                End If

                                xmldoc &= Chr(10)
                                xmldoc &= ReturnNodeValue(snode)

                                File.WriteAllText(path, xmldoc, encodingset)
                                workingSubProgress(worked)
                        End Select
                    Next
                Catch ex As XmlException
                    Dim tmpdoc As String = ""
                    Dim DOC As New StringBuilder
                    '
                    ' BASE64인지 모호하므로 그냥 try로 잡아줌 + 주석제거
                    '
                    Try
                        tmpdoc = Regex.Replace(Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(URL))), "\<\!\-\-.*\-\-\>", "")
                    Catch
                        tmpdoc = Regex.Replace(File.ReadAllText(URL), "\<\!\-\-.*\-\-\>", "")
                    End Try

                    While (tmpdoc.IndexOf("<!--") > -1 And tmpdoc.IndexOf("-->"))
                        Try
                            tmpdoc = tmpdoc.Replace(readCommand.ParseOuter(tmpdoc, "<!--", "-->"), "")
                        Catch ccc As Exception
                            Exit While
                        End Try
                    End While
                    '
                    ' 쓸모없는 빈 줄 제거
                    '
                    tmpdoc = tmpdoc.Replace(Chr(10) & Chr(13), Chr(10))
                    For Each line As String In tmpdoc.Split(Chr(10))
                        If line.Trim = "" Then
                            Continue For
                        Else
                            DOC.Append(line) '여기선 스트링빌더가 메모리를 더 적게 생성한다.
                        End If
                    Next

                    Dim document As String = DOC.ToString
                    Dim msg As String = ex.Message

                    addLog("※ 이 실험 계획서에 문제가 있습니다!" & vbCrLf)
                    addLog(msg.Substring(0, msg.LastIndexOf(".")) & vbCrLf & vbCrLf)
                    Dim linetmp As String = msg.Substring(msg.LastIndexOf("줄") + 1).Trim
                    linetmp = linetmp.Substring(0, linetmp.LastIndexOf(","))
                    addLog(" * 줄 : " & linetmp & vbCrLf)

                    Dim lentemp As String = msg.Substring(msg.LastIndexOf("위치") + 2).Trim
                    addLog(" * 위치 : " & lentemp & vbCrLf)
                    DOC.Replace(Chr(13), "")

                    Dim lines As String() = DOC.ToString.Split(Chr(10))
                    Dim errorline As String = lines(Val(linetmp) - 1)

                    'MsgBox(errorline.Length)
                    addLog("문서 :" & vbCrLf & errorline & vbCrLf)
                    Exit For
                Catch ex As Exception

                End Try
            Next
            Try
                File.Delete(WLAB.TEMP & "unpack.exe")
            Catch ex As Exception

            End Try
            My.Computer.FileSystem.CreateDirectory(WLAB.TEMP)
            My.Computer.FileSystem.MoveDirectory(WLAB.TEMP, Setting.Mabi & "\data\", True)
        Catch ex As Exception
            Console.WriteLine(ex.GetBaseException.ToString)
        End Try

        progressTextChange("Data 경로의 파일들을 확인하여 적용하고 있습니다...")
        DataReplacer(True)
        ProgressSubTrd(10000)
        progressTextChange("default")
        Me.plistEabledChange(True)
        Me.ShowHideProgress(True)
        listReloadThread()
    End Sub

    Public Sub deleteWorkThread()
        'MsgBox("Test")
        If Me.InvokeRequired Then
            Dim work As New SetCallback(AddressOf deleteWorkThread)
            Me.Invoke(work, New Object() {[Text]})
        Else
            Me.plistEabledChange(False)
            Me.ShowHideProgress(False)
            progressTextChange("패키지 파일을 복구하고 있습니다.")
            DataReplacer(False)
            If Me.isTestserver.Checked Then
                If Setting.MabiTest <> vbNullString Then
                    Me.mabiDir = Setting.Mabi
                    Setting.Mabi = Setting.MabiTest
                End If
            End If

            CMDworkList.Clear()
            Me.ShowHideProgress(False)
            Me.plistEabledChange(False)
            total = 0
            current = 0
            currentfile = 0
            Dim listitem As New ListView
            Dim final As New ListView
            Dim checkCount As Integer = 0
            '
            ' 리스트에 아이템 담기
            '
            If plist1.CheckedItems.Count > 0 Then
                For Each item As ListViewItem In plist1.CheckedItems
                    Dim tmpitem As ListViewItem = New ListViewItem({item.SubItems(3).Text, item.SubItems(5).Text, item.SubItems(6).Text, item.SubItems(7).Text, item.SubItems(0).Text, item.SubItems(4).Text})
                    listitem.Items.Add(tmpitem)
                Next
            End If
            If plist2.CheckedItems.Count > 0 Then
                For Each item As ListViewItem In plist2.CheckedItems
                    Dim tmpitem As ListViewItem = New ListViewItem({item.SubItems(3).Text, item.SubItems(5).Text, item.SubItems(6).Text, item.SubItems(7).Text, item.SubItems(0).Text, item.SubItems(4).Text})
                    listitem.Items.Add(tmpitem)
                Next
            End If
            If plist3.CheckedItems.Count > 0 Then
                For Each item As ListViewItem In plist3.CheckedItems
                    Dim tmpitem As ListViewItem = New ListViewItem({item.SubItems(3).Text, item.SubItems(5).Text, item.SubItems(6).Text, item.SubItems(7).Text, item.SubItems(0).Text, item.SubItems(4).Text})
                    listitem.Items.Add(tmpitem)
                Next
            End If
            If plist4.CheckedItems.Count > 0 Then
                For Each item As ListViewItem In plist4.CheckedItems
                    Dim tmpitem As ListViewItem = New ListViewItem({item.SubItems(3).Text, item.SubItems(5).Text, item.SubItems(6).Text, item.SubItems(7).Text, item.SubItems(0).Text, item.SubItems(4).Text})
                    listitem.Items.Add(tmpitem)
                Next
            End If

            For Each item As ListViewItem In listitem.Items
                total += Val(item.SubItems(2).Text)
            Next
            addLog(total)

            Dim gflist As New List(Of String)

            For Each item As ListViewItem In listitem.Items
                Dim path As String = item.SubItems(0).Text
                Dim url As String = item.SubItems(1).Text
                Dim size As String = item.SubItems(2).Text
                Dim mdate As Integer = Val(item.SubItems(3).Text)
                Dim dlflag As Boolean = False
                Dim dir As String = path.Substring(0, path.LastIndexOf("\") + 1)
                Dim fileinfo As New FileInfo(path)
                Dim df As String = url.Replace("/", "\")
                Dim dest As String = Setting.Mabi & "\" & df.Substring(0, df.LastIndexOf("\"))
                currentfile = size
                dlfilename = item.SubItems(4).Text
                Dim wc As New WebClient
                wc.Encoding = Encoding.UTF8

                If url.Substring(url.LastIndexOf(".") + 1) = "lab" Then
                    Dim sb As New StringBuilder
                    Try
                        If File.Exists(path) Then
                            Dim tmpdoc As String = ""
                            Try
                                tmpdoc = Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(path)))
                            Catch ex As Exception
                                tmpdoc = File.ReadAllText(path)
                            End Try
                            sb.Append(USERVAR(tmpdoc, path.Substring(0, path.LastIndexOf("\"))))
                        Else
                            Dim tmpdoc As String = ""
                            Try
                                tmpdoc = Encoding.UTF8.GetString(Convert.FromBase64String(wc.DownloadString(Setting.Server(1) & url)))
                            Catch ex As Exception
                                tmpdoc = wc.DownloadString(Setting.Server(1) & url)
                            End Try
                            sb.Append(USERVAR(tmpdoc, path.Substring(0, path.LastIndexOf("\"))))
                        End If
                        Dim xml As New XmlDocument
                        xml.LoadXml(sb.ToString)
                        For Each XmlNode As XmlElement In xml.SelectNodes("/Library_Command/CMD/*")
                            Select Case XmlNode.Name.ToLower
                                Case "getfile"
                                    For Each node As XmlAttribute In XmlNode.Attributes
                                        Select Case node.Name.ToLower
                                            Case "path"
                                                gflist.Add(node.Value.Trim)
                                                If File.Exists(Setting.Mabi & "\data\" & node.Value.Trim) Then
                                                    File.Delete(Setting.Mabi & "\data\" & node.Value.Trim)
                                                End If
                                        End Select
                                    Next
                                Case "copyfile", "movefile", "xmlcreate"
                                    For Each node As XmlAttribute In XmlNode.Attributes
                                        Select Case node.Name.ToLower
                                            Case "destinationpath", "path"
                                                If File.Exists(node.Value) Then
                                                    File.Delete(node.Value)
                                                End If
                                        End Select
                                    Next
                            End Select
                        Next
                        Try
                            File.Delete(path)
                        Catch ex As Exception

                        End Try
                    Catch ex As Exception

                    End Try
                    'addLog(sb.ToString)
                End If
                Try
                    File.Delete(path)
                Catch ex As Exception
                End Try
                If File.Exists(Setting.Mabi & "\" & url.Replace("/", "\")) Then
                    File.Delete(Setting.Mabi & "\" & url.Replace("/", "\"))
                End If
            Next

            Directory.CreateDirectory(WLAB.LABS)
            Dim dirlist As New List(Of String)
            Dim cwladder As New List(Of String)

            For Each str As String In Directory.GetFiles(WLAB.ROOT & "\resources\lab")
                Dim fileinfo As New FileInfo(str)
                If fileinfo.Extension = ".lab" Then
                    dirlist.Add(str)
                End If
            Next

            For Each str As String In Directory.GetFiles(WLAB.LABS)
                Dim fileinfo As New FileInfo(str)
                If fileinfo.Extension = ".lab" Then
                    dirlist.Add(str)
                End If
            Next

            For Each Path As String In dirlist
                Dim DOC As New StringBuilder
                Dim XML As New XmlDocument
                Dim tmpdoc As String = ""
                Dim root As String = "/Library_Command/"
                Dim getFileList As New List(Of String)
                Try
                    tmpdoc = Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(Path)))
                Catch ex As Exception
                    tmpdoc = File.ReadAllText(Path)
                End Try

                tmpdoc = tmpdoc.Replace(Chr(10) & Chr(13), Chr(10))
                For Each line As String In tmpdoc.Split(Chr(10))
                    If line.Trim = "" Then
                        Continue For
                    Else
                        DOC.Append(line) '여기선 스트링빌더가 메모리를 더 적게 생성한다.
                    End If
                Next

                Dim document As String = DOC.ToString
                document = USERVAR(document, Path)
                XML.LoadXml(document)

                Dim pathlisttrue As Boolean = False
                For Each node As XmlElement In XML.SelectNodes("/Library_Command/CMD/*")
                    Try
                        Select Case node.Name.ToLower
                            Case "getfile"
                                For Each attr As XmlAttribute In node.Attributes
                                    Select Case attr.Name.ToLower
                                        Case "path"
                                            For Each gfl As String In gflist
                                                If gfl.Trim = attr.Value.Trim Then
                                                    pathlisttrue = True
                                                End If
                                            Next
                                    End Select
                                Next
                        End Select
                    Catch ex As Exception
                        Console.WriteLine(ex.GetBaseException.ToString)
                    End Try
                Next

                If pathlisttrue Then
                    Dim tmp As Boolean = True
                    For Each dm As String In CMDworkList
                        If dm = Path Then
                            tmp = False
                        End If
                    Next

                    If tmp Then
                        cwladder.Add(Path)
                    End If
                End If
            Next

            CMDworkList.Clear()

            For Each str As String In cwladder
                Dim tmp As Boolean = True
                For Each pat As String In CMDworkList
                    If InStr(pat, str.Trim.Replace("\\", "\")) Then
                        tmp = False
                    End If
                Next

                If tmp Then
                    CMDworkList.Add(str)
                End If
            Next

            DataReplacer(True)

            Me.plistEabledChange(True)
            Me.ShowHideProgress(True)
            listReloadThread()

            runUnpack()
            progressTextChange("default")
        End If
    End Sub

End Class
