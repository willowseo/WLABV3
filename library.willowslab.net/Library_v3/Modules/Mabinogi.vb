Imports System.Net.Sockets
Imports System.IO
Imports System.Text
Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports 버들라이브러리.MabiPackClass
Imports System.Drawing.Imaging
Imports System.Xml


Module WLAB
    '
    ' 버들팩의 기본적인 경로를 저장하고 있음. 설정이랑은 사용용도가 조금 다르긴 하나 중복된다고 봐도 무관
    '
    Public ROOT As String = LDR(Application.StartupPath)
    Public CONF As String = ROOT & "data\"
    Public TEMP As String = ROOT & "temp\"
    Public LABS As String = ROOT & "labdata\"
    Public LABINSTALLED As String = ROOT & "실험문서\"
    Public MYDOC As String = LDR(My.Computer.FileSystem.SpecialDirectories.MyDocuments)
    Public RESOURCES As String = ROOT & "resources\"

    Public Function LDR(ByVal dir As String)
        If dir.LastIndexOf("\") < dir.Length Then
            Return dir & "\"
        End If
        Return dir
    End Function

    Public Sub ChangeTestAndMain()

    End Sub
End Module
Module Mabinogi
#Region "Mabinogi Patch Procedure"

    Dim patchinfo As String = "http://211.218.233.238/patch/patch.txt"
    Dim patchinfotest As String = "http://211.218.233.238/patch/patch_test.txt"
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
    Public Structure MabiUpdateInfo

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

            If IO.File.Exists(patchfile) = True Then
                Dim Temp As String = IO.File.ReadAllText(patchfile)
                For Each vLine As String In Split(Temp, vbNewLine)
                    If InStr(vLine, "patch_accept=") > 0 Then
                        patch_accept = Replace(vLine, "patch_accept=", "")
                    ElseIf InStr(vLine, "local_version=") > 0 Then
                        local_version = Replace(vLine, "local_version=", "")
                        If Setting.isTestMabi Then
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
                IO.File.Delete(patchfile)
                Return True
            Else
                Return False
            End If
        End Function
    End Structure
    Public Function ClientType_Kor() As String
        If Setting.isTestMabi = False Then
            Return "정식 서버"
        Else
            Return "테스트 서버"
        End If
    End Function
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
    Private Function isServerOpen(ByVal Mabiinfo As MabiUpdateInfo) As Boolean
        Try
            If Mabiinfo.patch_accept.IndexOf("1") > -1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
        End Try
        Return False
    End Function

    Public Sub Start()
        Dim MabiInfo As MabiUpdateInfo = New MabiUpdateInfo
        Dim url As String = patchinfo
        If Setting.isTestMabi Or File.Exists(Setting.Mabi & "\mabinoti_test.exe") Then
            url = patchinfotest
        End If
        If Func.webClient(url, Setting.Data, "patch.txt") Then
            MabiInfo.setInfo(Setting.Data & "patch.txt")

            Dim Port As Integer = 11000
            Dim Hostname As String = MabiInfo.login
            'Call the function
            Dim PortOpen As Boolean = IsPortOpen(Hostname, Port)

            'MsgBox(IsPortOpen("willowslab.com", 80))

            If PortOpen And Mabinogi.isServerOpen(MabiInfo) Then
                Run(MabiInfo.login, MabiInfo.arg)
            Else
                If MsgBox("마비노기가 패치중이거나 서버가 닫혀있습니다. 그래도 마비노기를 실행하시겠습니까?", MsgBoxStyle.OkCancel Or MsgBoxStyle.Information Or vbMsgBoxSetForeground) = MsgBoxResult.Ok Then
                    Run(MabiInfo.login, MabiInfo.arg)
                End If
            End If
        End If
    End Sub

    Public Function Run(login As String, arg As String) As Boolean
        Dim Prcs = New Process()
        Dim PrcsSI = New ProcessStartInfo
        Dim Result As String = "", Err As String = ""
        Dim mabipath As String = Setting.Mabi
        If Setting.isTestMabi Then
            mabipath = Setting.MabiTest
        End If
        Dim Arguments As String = "Client.exe code:1622 ver:" & Mabinogi.getVersion & " logip:" & login & " logport:11000 " & arg
        With PrcsSI
            .FileName = "CMD.exe"
            .WorkingDirectory = mabipath
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
    '마비노기 버전 얻기
    Public Function getVersion() As Integer

        Dim fs As IO.FileStream = Nothing
        Dim br As IO.BinaryReader = Nothing
        Dim mVersion As UInteger = 0
        Dim FilePath As String = Setting.Mabi & "\version.dat"
        Dim mabitestv As String = "Mabinogi "
        If Setting.isTestMabi Then
            FilePath = Setting.MabiTest & "\version.dat"
            mabitestv = "Mabinogi - test "
        End If
        Try
            If My.Computer.FileSystem.FileExists(FilePath) = True Then
                fs = New IO.FileStream(FilePath, IO.FileMode.Open, IO.FileAccess.Read)
                br = New IO.BinaryReader(fs)
                mVersion = br.ReadUInt32()
            End If
        Catch ex As Exception
            'MsgBox(ex.GetBaseException().ToString)
        Finally
            If br Is Nothing = False Then
                br.Close()
            End If
            If fs Is Nothing = False Then
                fs.Close()
            End If
        End Try
        Return mVersion

    End Function
    Public Function setVersion(ByVal vVersion As String) As Integer

        If IO.File.Exists(Setting.Mabi & "\version.dat") = True Then

            Dim MabiByteCount As Integer = 4 '마비노기 버전의 바이트 수
            'Dim MabiByteCount As Integer = My.Computer.FileSystem.ReadAllBytes(vInfo.Dir_Mabinogi & "\version.dat").Length '마비노기 버전의 바이트 수
            Dim HexCode As String = Hex(vVersion)
            Dim HexCodeCount As Integer = HexCode.Length
            Dim ReserseHexCode = ""


            '0 붙여주기
            For i = 0 To (MabiByteCount * 2 - 1) - HexCodeCount
                HexCode = "0" & HexCode
            Next

            For i = (MabiByteCount) - 1 To 0 Step -1
                ReserseHexCode &= HexCode.Substring(i * 2, 2)
            Next

            Dim bytes(MabiByteCount - 1) As Byte

            For i As Integer = 0 To MabiByteCount - 1
                bytes(i) = Convert.ToByte(ReserseHexCode.Substring(i * 2, 2), 16)
            Next
            My.Computer.FileSystem.WriteAllBytes(Setting.Mabi & "\version.dat", bytes, False)

        End If
        Return 0
    End Function
#End Region
#Region "Mabinogi Package Management"


    Public Sub makeLists()

    End Sub
    ''' <summary>
    ''' 패키지 파일의 내부 리스트를 분석하여 파일로 내보냅니다.
    ''' </summary>
    ''' <param name="pack">패키지 파일의 경로</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FileLists(ByVal pack As String)
        Dim doc As New StringBuilder
        Try
            Using FHD As New FileStream(pack, FileMode.Open, FileAccess.ReadWrite)
                Dim BinaryReader As New BinaryReader(FHD)

                Dim bArrData As Byte() = New Byte(8) {}
                Dim PackSize As Long = CType(FHD.Length, Long)

                Dim FileCount As Long = 0L
                Dim ListSize As Long = 0L
                Dim ZeroSize As Long = 0L
                Dim CompressSize As Long = 0L

                Dim Readed As Long = 0L
                Dim CurrentSeekLast As Long = 0L

                Dim cache As Byte() = New Byte(4) {}
                Dim b7 As Byte() = New Byte(8) {}
                Dim bF As Byte() = New Byte(16) {}

                Dim validHeader As Byte() = {&H50, &H41, &H43, &H4B, &H2, &H1, &H0, &H0}
                Dim Checksum As Boolean = True
                '
                'read Pack Header
                '
                FHD.Read(b7, 0, 8)
                For i = 0 To 7
                    If validHeader(i) <> b7(i) Then
                        Checksum = False
                    End If
                Next

                If Checksum Then
                    FHD.Seek(512, SeekOrigin.Begin)
                    FHD.Read(cache, 0, 4)
                    FileCount = CType(BitConverter.ToUInt32(cache, 0), Long)
                    FHD.Read(cache, 0, 4)
                    ListSize = CType(BitConverter.ToUInt32(cache, 0), Long)
                    FHD.Read(cache, 0, 4)
                    ZeroSize = CType(BitConverter.ToUInt32(cache, 0), Long)
                    FHD.Read(cache, 0, 4)
                    CompressSize = CType(BitConverter.ToUInt32(cache, 0), Long)
                    FHD.Seek(16, SeekOrigin.Current)
                    doc.Append("//FileCount:" & FileCount & ";ListSize:" & ListSize & ";Zerobytes:" & ZeroSize & Chr(10))
                    While (FileCount > Readed And ListSize > BinaryReader.BaseStream.Position)
                        Dim version As Long = 0L
                        Dim fileData As Long = 0L
                        Dim compress As Long = 0L
                        Dim decompress As Long = 0L

                        CurrentSeekLast = CType(BinaryReader.BaseStream.Position, Long)
                        Readed += 1
                        Dim currFilename As New StringBuilder
                        Dim linecount As Integer = 0
                        Dim dotseek As Long = 0L

                        FHD.Read(bF, 0, 16)
                        Dim search As Integer = 0
                        For Each Stack As Byte In bF
                            If Stack > 33 And Stack < 123 Then
                                currFilename.Append(Chr(Convert.ToInt32(Stack)))
                            End If
                            search += 1
                        Next

                        linecount = Convert.ToInt32(bF(0))
                        If linecount > 3 Then
                            linecount += linecount Mod 3
                        End If

                        If linecount > 0 Then
                            For i As Integer = 1 To linecount
                                search = 0
                                FHD.Read(bF, 0, 16)
                                For Each Stack As Byte In bF
                                    If Stack > 33 And Stack < 123 Then
                                        currFilename.Append(Chr(Convert.ToInt32(Stack)))
                                    End If
                                    search += 1
                                Next
                            Next
                        End If

                        FHD.Read(cache, 0, 4)
                        version = CLng(BitConverter.ToUInt32(cache, 0))
                        FHD.Read(cache, 0, 4)
                        FHD.Read(cache, 0, 4)
                        fileData = CLng(BitConverter.ToUInt32(cache, 0))
                        FHD.Read(cache, 0, 4)
                        compress = CLng(BitConverter.ToUInt32(cache, 0))
                        FHD.Read(cache, 0, 4)
                        decompress = CLng(BitConverter.ToUInt32(cache, 0))

                        Dim cfn As String = currFilename.ToString
                        doc.Append(cfn.Replace("*", ".") & "," & CurrentSeekLast.ToString & "," & version & "," & fileData & "," & compress & "," & decompress & ";" & Chr(10).ToString)
                        FHD.Seek(44, SeekOrigin.Current)
                    End While
                    FHD.Close()
                    BinaryReader.Close()
                    Dim dir As String = "server_main\"
                    If Setting.MabiTest <> vbNullString And InStr(pack, Setting.MabiTest) And Setting.Mabi <> Setting.MabiTest Then
                        dir = "server_test\"
                    End If
                    If Not Directory.Exists(Setting.Data & "lib\" & dir) Then
                        Directory.CreateDirectory(Setting.Data & "lib\" & dir)
                    End If
                    My.Computer.FileSystem.WriteAllText(Setting.Data & "lib\" & dir & pack.Substring(pack.LastIndexOf("\") + 1) & ".lib", doc.ToString, False, Encoding.UTF8)
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception

        End Try
        Return False
    End Function

    Public Function Export(ByVal filename As String, Optional ByVal isTest As Boolean = False)
        Try
            Dim libdoc As String = ""
            Dim libs As New List(Of String)
            Dim sortedlib As New List(Of String)
            Dim destPath As String = ""
            Dim dir As String = "server_main\"
            If isTest Then
                dir = "server_test\"
            End If

            Try
                For Each Path As String In Directory.GetFiles(Setting.Data & "lib\" & dir)
                    libs.Add(Path)
                Next
            Catch ex As Exception
                If isTest Then
                    For Each pack As String In Directory.GetFiles(Setting.MabiTest & "\package\")
                        FileLists(pack)
                    Next
                Else
                    For Each pack As String In Directory.GetFiles(Setting.Mabi & "\package\")
                        FileLists(pack)
                    Next
                End If
                Return Export(filename, isTest)
                Exit Function
            End Try

            Console.WriteLine("리스트를 정렬하는 중...")
            For i As Integer = (libs.Count - 1) To 0 Step -1
                sortedlib.Add(libs(i))
            Next
            Console.WriteLine("추출할 파일을 찾는 중...")
            For Each Path As String In sortedlib
                Dim text As String = File.ReadAllText(Path)
                If text.IndexOf(filename) > -1 Then
                    libdoc = text
                    Console.WriteLine("추출해야할 팩 파일을 찾았습니다...")
                    destPath = Path.Substring(Path.LastIndexOf("\") + 1)
                    Exit For
                End If
            Next
            If destPath = "" Then
                Throw New Exception("추출할 파일이 정규 팩파일에 없습니다.")
                Console.WriteLine("추출할 파일이 정규 팩파일에 없습니다.")
            End If
            Dim destPack As String = Setting.Mabi & "\package\" & destPath.Replace(".lib", "")
            Console.WriteLine(destPack)
            Return CallUnpackExec(filename, destPack)
        Catch ex As Exception
            Console.WriteLine("추출 실패" & ex.GetBaseException.ToString)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' 초미지향님의 Unpack.exe를 이용하여 패키지 파일을 추출합니다.
    ''' </summary>
    ''' <param name="filename">추출할 파일 이름입니다. data/경로를 제외하여 기술합니다.</param>
    ''' <param name="package">추출 대상이 되는 패키지의 경로입니다.</param>
    ''' <returns>추출 성공 여부를 반환합니다.</returns>
    ''' <remarks></remarks>
    Public Function CallUnpackExec(ByVal filename As String, ByVal package As String) As Boolean
        Try
            If Not Directory.Exists(WLAB.TEMP) Then
                Directory.CreateDirectory(WLAB.TEMP)
            End If
            File.WriteAllBytes(WLAB.TEMP & "unpack.exe", My.Resources.unpack)
        Catch ex As Exception
            Console.WriteLine("추출 실패" & ex.GetBaseException.ToString)
            Return False
        End Try

        Try
            Dim Prcs = New Process()
            Dim PrcsSI = New ProcessStartInfo
            Dim Result As String = "", Err As String = ""
            Dim Arguments As String = "Call " & Chr(34) & "Unpack.exe" & Chr(34) & " " & Chr(34) & filename & Chr(34) & " " & Chr(34) & package & Chr(34) & " " & Environment.NewLine
            With PrcsSI
                .FileName = "CMD.exe"
                .WorkingDirectory = WLAB.TEMP
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
            Result &= Prcs.StandardOutput.ReadToEnd()

            If InStr(Result, "uncompressing", CompareMethod.Text) > 0 Then
                If File.Exists(Setting.Mabi & "\package\data\" & filename) Then
                    If Not Directory.Exists(WLAB.TEMP & filename.Substring(0, filename.LastIndexOf("\"))) Then
                        Directory.CreateDirectory(WLAB.TEMP & filename.Substring(0, filename.LastIndexOf("\")))
                    End If
                    If File.Exists(WLAB.TEMP & filename) Then
                        Return True
                        Exit Function
                    End If
                    File.Move(Setting.Mabi & "\package\data\" & filename, WLAB.TEMP & filename)
                End If
                Console.WriteLine(WLAB.TEMP & filename)
                Console.WriteLine("추출 성공")
                File.Delete(WLAB.TEMP & "unpack.exe")
                Return True
            Else
                Console.WriteLine(Result)
                Console.WriteLine("추출 실패")
                File.Delete(WLAB.TEMP & "unpack.exe")
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("추출 실패" & ex.GetBaseException.ToString)
            File.Delete(WLAB.TEMP & "unpack.exe")
            Return False
        End Try
    End Function
    Dim rand As Random = New Random()
    Public Sub XmlErrorRemove(ByVal Target As String)
        Try
            Dim doc As New StringBuilder
            Dim ecd As System.Text.Encoding = Nothing
            '토큰 제거 알고리즘 : 망할 토큰 "ABC 이런거 띄워줌
            Dim ReadFile As String = My.Computer.FileSystem.ReadAllText(Target, Encoding.UTF8)
            doc.Append(ReadFile)
            ReadFile = Regex.Replace(ReadFile, "([=]{1}[\x22]{1})([^\x22]*)([\x22]{1})([^ ]{1})", "$1$2$3 $4", RegexOptions.Multiline)

            doc.Replace("<<", "<")
            doc.Replace(">>", ">")
            doc.Replace("utf-8", "UTF-8")
            doc.Replace("utf-16", "UTF-16")
            doc.Replace("&qot;", "&quot;")

            If InStr(doc.ToString, "UTF-16") Then
                File.Delete(Target)
                File.WriteAllText(Target, doc.ToString, Encoding.Unicode)
            ElseIf InStr(doc.ToString, "UTF-8") Then
                File.Delete(Target)
                File.WriteAllText(Target, doc.ToString, Encoding.UTF8)
            Else
                File.Delete(Target)
                File.WriteAllText(Target, doc.ToString)
            End If
            Console.WriteLine("XML파일의 오류 제거를 마쳤습니다.")
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
    Public Function ReplaceText(ByVal subject As String, ByVal search As String, ByVal replace As String)
        Try
            Dim s1 As Stopwatch = Stopwatch.StartNew
            subject = WLAB.TEMP & subject
            Dim finfo As New FileInfo(subject)
            Dim doc As New StringBuilder

            search = search.Replace("""", Chr(34))
            replace = replace.Replace("""", Chr(34))

            doc.Append(File.ReadAllText(subject))
            doc.Replace(search, replace)
            File.WriteAllText(subject, doc.ToString, Encoding.Unicode)
            Console.WriteLine("치환작업 성공 (" & search & " > " & replace & ")")
            s1.Stop()
            Console.WriteLine(s1.Elapsed.TotalMilliseconds.ToString("0.00 ms"))
            Return True
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Console.WriteLine("치환작업 실패 (" & search & " > " & replace & ")")
            Return False
        End Try
    End Function

    Private Function XmlReplace(ByVal Target As String, ByVal FindElement As String, ByVal FindAttribute As String, ByVal FindValue As String, ByVal SetAttribute As String, ByVal SetValue As String) As Boolean

        If FindAttribute = "" Then
            Return ReplaceXPath(Target, "//" & FindElement, SetAttribute, SetValue)
        Else
            Return ReplaceXPath(Target, "//" & FindElement & "[translate(@" & FindAttribute & ", 'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='" & FindValue.ToLower & "']", SetAttribute, SetValue)
        End If

    End Function
    Public Function ReplaceXPath(ByVal target As String, ByVal xpath As String, ByVal setattrib As String, ByVal setval As String, Optional replaceV As String = "", Optional ByVal append As String = "")
        Try
            target = WLAB.TEMP & target
            XmlErrorRemove(target)
            setval = ConvertXMLReplace(setval, XMLReplaceType.Restore)
            Dim doc As New XmlDocument
            Dim vSelectNodes As XmlNodeList
            doc.Load(target)
            vSelectNodes = doc.SelectNodes(ConvertXMLReplace(xpath, XMLReplaceType.Restore))

            For i = 0 To vSelectNodes.Count - 1

                If vSelectNodes(i).Name <> "xml" And vSelectNodes(i).Name <> "#comment" Then

                    Dim Tmp_Element As Xml.XmlElement = vSelectNodes(i)

                    If setattrib = "" Then
                        If append = "" Then
                            Tmp_Element.InnerXml = setval '비어있을땐 엘레멘탈 값을 바꿀 수 있도록
                        ElseIf append = "After" Or append = "앞" Then
                            Tmp_Element.InnerXml = setval & Tmp_Element.InnerXml '비어있을땐 엘레멘탈 값을 바꿀 수 있도록
                        ElseIf append = "Before" Or append = "뒤" Then
                            Tmp_Element.InnerXml = Tmp_Element.InnerXml & setval
                        ElseIf append = "Replace" Or append = "치환" Or append = "변경" Then
                            If replaceV <> "" Then
                                Tmp_Element.InnerXml = Tmp_Element.InnerXml.Replace(setval, replaceV)
                            End If
                        ElseIf append = "Change" Then
                            Dim ReplaceValue() As String = Split(setval, "[,]")
                            Tmp_Element.InnerXml = Replace(Tmp_Element.InnerXml, ReplaceValue(0), ReplaceValue(1))
                        End If
                    Else
                        If append = "" Then
                            Tmp_Element.SetAttribute(setattrib, setval) 'SetAttribute, SetValue
                        ElseIf append = "Front" Or append = "앞" Then
                            Tmp_Element.SetAttribute(setattrib, setval & Tmp_Element.GetAttribute(setattrib)) 'SetAttribute, SetValue
                        ElseIf append = "Behind" Or append = "뒤" Then
                            Tmp_Element.SetAttribute(setattrib, Tmp_Element.GetAttribute(setattrib) & setval) 'SetAttribute, SetValue
                        ElseIf append = "Replace" Or append = "치환" Or append = "변경" Then
                            If replaceV <> "" Then
                                Tmp_Element.SetAttribute(setattrib, Tmp_Element.GetAttribute(setattrib).Replace(setval, replaceV))
                            End If
                        ElseIf append = "Change" Then
                            Dim ReplaceValue() As String = Split(setval, "[,]")
                            Tmp_Element.InnerXml = Replace(Tmp_Element.InnerXml, ReplaceValue(0), ReplaceValue(1))
                        End If
                    End If
                    Tmp_Element = Nothing
                End If
            Next
            Console.WriteLine("ReplaceXML XPath 명령어를 수행했습니다.")

            doc.Save(target)
            Dim stringb As New StringBuilder
            stringb.Append(File.ReadAllText(target))
            stringb.Replace("  ", Chr(9))
            My.Computer.FileSystem.WriteAllText(target, stringb.ToString, False)
            Return False
        Catch ex As Exception
            Console.WriteLine("ReplaceXML XPath 수행에 실패했습니다. " & ex.ToString)
            Return False
        End Try
    End Function


    Public Function DeleteXPath(ByVal Target As String, ByVal XPath As String) As Boolean

        Try
            Target = WLAB.TEMP & Target
            XmlErrorRemove(Target)
            Dim doc As New Xml.XmlDocument
            Dim vSelectNodes As XmlNodeList
            doc.Load(Target)
            vSelectNodes = doc.SelectNodes(ConvertXMLReplace(XPath, XMLReplaceType.Restore))

            For i = 0 To vSelectNodes.Count - 1
                If vSelectNodes(i).Name <> "xml" And vSelectNodes(i).Name <> "#comment" Then

                    Dim Tmp_Element As Xml.XmlElement = vSelectNodes(i)
                    Dim Parent_Element As Xml.XmlElement = Tmp_Element.ParentNode()

                    Parent_Element.RemoveChild(Tmp_Element)

                    Parent_Element = Nothing
                    Tmp_Element = Nothing

                End If
            Next

            doc.Save(Target)
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function
    ''' <summary>
    ''' 마비노기의 TXT파일의 내용을 치환합니다.
    ''' </summary>
    ''' <param name="Target">수정할 파일의 위치입니다. 임시 디렉터리에서 작업하므로 DATA\이후의 경로를 적어주어야 합니다.</param>
    ''' <param name="FindNum"></param>
    ''' <param name="SetValue"></param>
    ''' <param name="Encoding"></param>
    ''' <param name="Append"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CmdMabiTextReplace(ByVal Target As String, ByVal FindNum As String, ByVal SetValue As String, ByRef Encoding As System.Text.Encoding, Optional ByVal Append As String = "") As Boolean

        Try
            Target = WLAB.TEMP & Target
            If File.Exists(Target) = True Then
                Dim ReadFile As String = ""

                ReadFile = File.ReadAllText(Target, Encoding)

                Dim Pattern As String = "^" & FindNum & "" & vbTab & "([^\n\r]*)[\n\r]*$"

                If Append = "" Then
                    ReadFile = Regex.Replace(ReadFile, Pattern, FindNum & vbTab & SetValue, RegexOptions.Multiline)
                ElseIf Append = "Front" Or Append = "앞" Then
                    ReadFile = Regex.Replace(ReadFile, Pattern, FindNum & vbTab & SetValue & "$1", RegexOptions.Multiline)
                ElseIf Append = "Behind" Or Append = "뒤" Then
                    ReadFile = Regex.Replace(ReadFile, Pattern, FindNum & vbTab & "$1" & SetValue, RegexOptions.Multiline)
                ElseIf Append = "Replace" Or Append = "치환" Or Append = "변경" Then
                    Dim ReplaceValue() As String = Split(SetValue, "[,]")
                    Dim vMatch As Match = Regex.Match(ReadFile, Pattern, RegexOptions.Multiline)

                    If vMatch.Success = True Then

                        Dim ReplaceText As String = vMatch.Groups(1).Value

                        ReplaceText = Replace(ReplaceText, ReplaceValue(0), ReplaceValue(1))
                        ReadFile = Regex.Replace(ReadFile, Pattern, FindNum & vbTab & ReplaceText, RegexOptions.Multiline)

                    End If
                End If
                File.Delete(Target)
                File.WriteAllText(Target, ReadFile, Encoding)

                '비우기
                Return True
            End If
        Catch ex As Exception

            'MsgBox(target & vbNewLine & vbNewLine & ex.ToString)
            Return False

        End Try

        Return False

    End Function
    ' 이미지 품질 관련해서 써먹는건데 그냥 인터넷 검색..
    Public Function GetEncoderInfo(ByVal mimeType As String) As ImageCodecInfo
        Dim j As Integer
        Dim encoders As ImageCodecInfo()
        encoders = ImageCodecInfo.GetImageEncoders()
        For j = 0 To encoders.Length
            If encoders(j).MimeType = mimeType Then
                Return encoders(j)
            End If
        Next j
        Return Nothing
    End Function

    Public Function ConvertDDS2Png(ByVal DDSFile As String, Optional ByVal SaveName As String = "") As String

        Try

            Dim vPath As String = System.IO.Path.GetDirectoryName(DDSFile)

            If SaveName = "" Then

                SaveName = System.IO.Path.GetFileName(DDSFile) & "_" & System.DateTime.Now.Ticks.ToString() & "_" & rand.Next(0, 9999) & ".png"
            End If

            SaveName = vPath & "\" & SaveName
            File.Delete(SaveName)
            DevIL.DevIL.LoadBitmap(DDSFile).Save(SaveName, ImageFormat.Png)
            File.Delete(DDSFile)

        Catch ex As Exception

            MsgBox(ex.GetBaseException().ToString())
            Return ""

        End Try

        Return SaveName

    End Function

    ''' <summary>
    ''' 패키지 파일의 압축된 파일을 복호화하여 하나의 파일로 반환합니다. 반환되는 파일은 프로그램의 temp디렉터리에 생성됩니다.
    ''' </summary>
    ''' <param name="targetPackage">추출 대상이 되는 패키지입니다.</param>
    ''' <param name="version">패키지의 압축 파일 버전입니다.</param>
    ''' <param name="offset">반환할 압축된 데이터의 시작 위치입니다.</param>
    ''' <param name="len">압축된 데이터의 길이입니다.</param>
    ''' <param name="dlen">압축되지 않았을 때 데이터의 길이입니다.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Decompress(ByVal targetPackage As String, ByVal savePath As String, ByVal version As UInteger, ByVal offset As ULong, ByVal len As ULong, ByVal dlen As ULong)
        Try
            If File.Exists(targetPackage) Then
                Dim tmp As Byte() = New Byte(len) {}
                Using FHD As New FileStream(targetPackage, FileMode.Open, FileAccess.ReadWrite)
                    'Dim BinaryReader As New BinaryReader(FHD)
                    FHD.Seek(offset, SeekOrigin.Begin)
                    FHD.Read(tmp, 0, len)
                    FHD.Close()
                End Using
                Dim mt As CMersenneTwister
                Dim rseed As ULong = (Convert.ToUInt64(version) << 7) Xor &HA9C36DE1UL
                mt.init_genrand(rseed)

                For fsize As Long = 0L To CLng(len - 1)
                    tmp(fsize) = tmp(fsize) Xor mt.genrand_int32()
                Next
                File.WriteAllBytes(Application.StartupPath & "\" & savePath, tmp)
            End If
        Catch ex As Exception
            Console.WriteLine(ex.GetBaseException.ToString)
            Return False
        End Try
        Return False
    End Function
    ''' <summary>
    ''' Convert to VB.NET Language for WillowsLAB
    ''' Original Source from MabinogiResourceDotNet
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure CMersenneTwister
        Private Shared N As Integer = 624
        Private Shared M As Integer = 397
        Private Shared MATRIX_A As ULong = &H9908B0DFUL
        Private Shared UPPER_MASK As ULong = &H80000000UL
        Private Shared LOWER_MASK As ULong = &H7FFFFFFFUL

        Private Shared mt As ULong() = New ULong(N) {}
        Private Shared mti As Integer = 1

        Public Sub init_genrand(ByVal rseed As ULong)
            mt(0) = rseed And &HFFFFFFFFUL
            Console.WriteLine(mt(0))
            While (mti < N)
                mti += 1
                mt(mti) = (1812433253UL * (mt(mti - 1) Xor (mt(mti - 1) >> 30)) + mti)
                mt(mti) = mt(mti) And &HFFFFFFFFUL
                'Console.WriteLine(mt(mti))
            End While
        End Sub

        Public Function genrand_int32()
            Dim y As ULong
            Dim mag01 As ULong() = New ULong(2) {&H0UL, MATRIX_A, 0}
            If mti >= N Then
                Dim kk As Integer = 0
                If mti = (N + 1) Then
                    init_genrand(5489UL)
                End If

                While (kk < (N - M))
                    kk += 1
                    y = (mt(kk) And UPPER_MASK) Or (mt(kk + 1) And LOWER_MASK)
                    mt(kk) = mt(kk + M) Xor (y >> 1) Xor mag01(y And &H1UL)
                End While
                While (kk < (N - 1))
                    y = (mt(kk) And UPPER_MASK) Or (mt(kk + 1) And LOWER_MASK)
                    mt(N - 1) = mt(M - 1) Xor (y >> 1) Xor mag01(y And &H1UL)
                End While
                y = (mt(N - 1) And UPPER_MASK) Or (mt(0) And LOWER_MASK)
                mt(N - 1) = mt(M - 1) Xor (y >> 1) Xor mag01(y And &H1UL)
                mti = 0
            End If
            mti += 1
            y = mt(mti)

            y = y Xor (y >> 11)
            y = y Xor (y << 7) And &H9D2C5680UL
            y = y Xor (y << 15) And &HEFC60000UL
            y = y Xor (y >> 18)
            Return y
        End Function
    End Structure
#End Region
End Module

Public Class MabiPackClass
    Public Enum XMLReplaceType

        HTML
        Restore
        Apply
        vName

    End Enum
    Public Shared Function ConvertXMLReplace(ByVal Str As String, ByVal vType As XMLReplaceType) As String

        Select Case vType
            Case XMLReplaceType.HTML
                Str = Replace(Str, "&amp;", "&amp;amp;")
                Str = Replace(Str, "&lt;", "&amp;lt;")
                Str = Replace(Str, "&gt;", "&amp;gt;")
                Str = Replace(Str, "&apos;", "&amp;apos;")
                Str = Replace(Str, "&quot;", "&amp;quot;")

            Case XMLReplaceType.Apply

                Str = Replace(Str, "<", "[:!AND:]lt;")
                Str = Replace(Str, ">", "[:!AND:]gt;")
                Str = Replace(Str, "'", "[:!AND:]apos;")
                Str = Replace(Str, Chr(34), "[:!AND:]quot;")
                Str = Replace(Str, "&lt;", "[:!AND:]lt;")
                Str = Replace(Str, "&gt;", "[:!AND:]gt;")
                Str = Replace(Str, "&apos;", "[:!AND:]apos;")
                Str = Replace(Str, "&quot;", "[:!AND:]quot;")

                Str = Replace(Str, "&", "&amp;")
                Str = Replace(Str, "[:!AND:]", "&")

            Case XMLReplaceType.Restore

                Str = Replace(Str, "&amp;", "&")
                Str = Replace(Str, "&lt;", "<")
                Str = Replace(Str, "&gt;", ">")
                Str = Replace(Str, "&apos;", "'")
                Str = Replace(Str, "&quot;", Chr(34))

            Case XMLReplaceType.vName

                Str = Replace(Str, "\", "￦")
                Str = Replace(Str, "/", "／")
                Str = Replace(Str, ":", "：")
                Str = Replace(Str, "*", "＊")
                Str = Replace(Str, "&", "＆") 'XML
                Str = Replace(Str, "?", "？")
                Str = Replace(Str, "'", "＇") 'XML
                Str = Replace(Str, """", "″") 'XML
                Str = Replace(Str, "<", "＜") ' XML
                Str = Replace(Str, ">", "＞") ' XML
                Str = Replace(Str, "|", "­｜")

        End Select

        Return Str

    End Function
End Class
