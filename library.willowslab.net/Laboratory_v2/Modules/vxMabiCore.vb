Module vxMabiCore ' 직접 설정을 해줘야하는 것
    Public Myself_Version As String = "2014.08.16.0" ' 버전


    ' 직접 설정을 해줘야하는 것
    Public UpdateXMLUrl As String = "http://mabicooktool.web.fc2.com/update_info/mabicooktools_info.xml" ' 정보파일 URL

    Public UpdateXMLPath As String = ""
    Public PackListPath As String = ""

    Public PackInfoURL As String = "http://willowslab.com/willowspack/packinfo.php"
    Public TestPackInfoURL As String = "http://willowslab.com/willowspack/testpackinfo.php"
    '디렉토리
    Public Dir_Myself As String = ""
    Public Dir_Myself_Temp As String = ""
    Public Dir_Myself_PackBox As String = ""
    Public Dir_Myself_Config As String = ""
    Public Dir_Myself_Assistant As String = ""
    Public Dir_Myself_Temp_Install As String = ""
    Public Dir_Myself_Option As String = ""
    Public Dir_Myself_State As String = ""

    Public Dir_Mabinogi As String = ""
    Public Dir_Mabinogi_ToolData As String = ""
    Public Dir_Mabinogi_Config As String = ""
    Public Dir_Mabinogi_Option As String = ""
    Public Dir_Mabinogi_Installed As String = ""
    Public Dir_Mabinogi_Temp As String = ""

    Public Dir_MyMabinogi As String = ""

    ' 상태정보
    Public State_ClientType As String = ""
    Public State_UpdateType As String = ""
    Public ReadyPackDownload As Boolean = True

    '서버
    Public RegCodeFile As String = "reg_code.dat"

    Public URL_PackList As String = ""
    Public Login_Server As String = ""
    Public Notice As String = ""
    Public Log As String = ""
    Public Link As String = ""
    Public Zip7DownloadUrl As String = ""

    '서버 변수
    'Public Server As New List(Of Server_Info)

    'Addon
    Public UnloadPackage As String = "pack.exe"
    Public UnPack As String = "unpack.exe"
    Public Zip As String = "7za.exe"

    ' ------------ 마비 상태 정보 ------------ '
    Public Mabinogi_InfoUrl As String = ""
    Public Mabinogi_RegPath As String = ""
    Public Mabinogi_ms_Patch_Accept As Integer = 1
    Public Mabinogi_ms_Local_Version As Integer = 0
    Public Mabinogi_ms_Login As String = ""
    Public Mabinogi_ms_Arg As String = ""
    ' ------------ 마비 서버 상태 정보 ------------ '

    ' 상태정보 자동다운로드를 했는지.
    Public AutoDownload As Integer = 0

    ' 불러올시 에러난 요리재료 리스트
    Public ErrorCookerys As List(Of String) = New List(Of String)

    Public Sub setFolder(ByRef doc As Xml.XmlDocument, ByRef Var As String, ByVal VarName As String)

        Dim Node As Xml.XmlElement = doc.SelectSingleNode("/Define/MabiCookTools/Paths/Path[@Name='" & VarName & "']")

        ' 폴더 경로 입력
        Var = Node.GetAttribute("Value")

        ' 폴더 경로 컨버트
        Var = Replace(Var, "{Folder:Myself}", vxMabiCore.Dir_Myself)
        Var = Replace(Var, "{Folder:Mabinogi}", vxMabiCore.Dir_Mabinogi)
        Var = Replace(Var, "{Folder:Myself_Config}", vxMabiCore.Dir_Myself_Config)

        vxFS.Dir_Create(Var)

        ' 폴더 감추기
        If Node.GetAttribute("Hidden") = "True" Then
            System.IO.File.SetAttributes(Var, System.IO.FileAttributes.Hidden)
        End If

    End Sub

    Public Function setValue(ByRef doc As Xml.XmlDocument, ByVal XPath As String, ByVal AttributeName As String) As String

        Try

            Dim Node As Xml.XmlElement = doc.SelectSingleNode(XPath)
            Return Node.GetAttribute(AttributeName)

        Catch ex As Exception

            Return ""

        End Try

    End Function
    '마비노기 버전 얻기
    Public Function getVersion() As Integer

        Dim fs As IO.FileStream = Nothing
        Dim br As IO.BinaryReader = Nothing
        Dim mVersion As UInteger = 0

        Dim FilePath As String = vxConfig.MabiDir & "\version.dat"

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

        If vxFS.File_Exists(vxConfig.MabiDir & "\version.dat") = True Then

            Dim MabiByteCount As Integer = 4 '마비노기 버전의 바이트 수
            'Dim MabiByteCount As Integer = My.Computer.FileSystem.ReadAllBytes(vInfo.Dir_Mabinogi & "\version.dat").Length '마비노기 버전의 바이트 수
            Dim HexCode As String = Hex(vVersion)
            Dim HexCodeCount As Integer = HexCode.Length

            '0 붙여주기
            For i = 0 To (MabiByteCount * 2 - 1) - HexCodeCount

                HexCode = "0" & HexCode

            Next

            Dim ReserseHexCode = ""

            For i = (MabiByteCount) - 1 To 0 Step -1

                ReserseHexCode &= HexCode.Substring(i * 2, 2)

            Next

            Dim bytes(MabiByteCount - 1) As Byte

            For i As Integer = 0 To MabiByteCount - 1

                bytes(i) = Convert.ToByte(ReserseHexCode.Substring(i * 2, 2), 16)

            Next

            My.Computer.FileSystem.WriteAllBytes(vxConfig.MabiDir & "\version.dat", bytes, False)

        End If

        Return 0

    End Function
    Public Function getMabiDataVersion() As Integer

        If My.Computer.FileSystem.FileExists(Dir_Mabinogi_Config & "\data_version_info.uph") = False Then

            Return 0

        Else

            Return Val(vxFS.Read_File(Dir_Mabinogi_Config & "\data_version_info.uph"))

        End If

    End Function
    '마비 새로운 버전 체크
    Public Function NewMabiData_Check() As Boolean

        If getMabiDataVersion() < getVersion() Then

            Return True

        Else

            Return False

        End If

    End Function

    Public Function ClientType_Kor() As String

        If State_ClientType = "Mabinogi" Then

            Return "정식 서버"

        ElseIf State_ClientType = "Mabinogi_test" Then

            Return "테스트 서버"

        Else

            Return "알 수 없음"

        End If

    End Function

    Public Sub Main_Load()
    End Sub
End Module
