Module xpConfig
    Public DirMe As String = System.Windows.Forms.Application.StartupPath
    Public PackinfoURL = "http://api.srinn.kr/package.xml"
    Public MabiDir = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "", "")
    Public MabiPackageDir = MabiDir & "\package\"
    Public SetDir = ""
    Public nowDownloaded = ""
    ' 옵션
    Dim Option_Run_PartUnPack As String = ""
    'Dim Option_Run_SearchPackFile As String = ""
    Dim Option_Run_AutomaticCheck_PackServer As String = xpMabiCore.Dir_Myself_Option & "\Run_AutomaticCheck_PackServer.option"
    Dim Option_Run_PartUnPack_IncludeAddons As String = xpMabiCore.Dir_Myself_Option & "\Run_PartUnPack_IncludeAddons.option"

    ' 상태
    Dim State_ClientType As String = xpMabiCore.Dir_Myself_State & "\ClientType.state"
    Dim State_UpdateType As String = xpMabiCore.Dir_Myself_State & "\UpdateType.state"
    Dim State_UpdateHideDay As String = xpMabiCore.Dir_Myself_State & "\UpdateHideDay.state"
    Dim State_XmlViewer As String = xpMabiCore.Dir_Myself_State & "\XmlViewer.state"


    Public Function get_State_ClientType() As String

        Return xpFS.Read_File(State_ClientType, "", "Mabinogi")

    End Function

    Public Function get_State_UpdateHideDay() As String

        Return xpFS.Read_File(State_UpdateHideDay)

    End Function

    Public Function get_State_UpdateType() As String

        Return xpFS.Read_File(State_UpdateType, "", "Latest")

    End Function

    Public Function get_State_XmlViewer() As String

        Return xpFS.Read_File(State_XmlViewer, "", "notepad.exe")

    End Function

    Public Sub set_State_ClientType(ByVal Value As String)

        xpFS.Text_Write(State_ClientType, Value)
        xpMabiCore.State_ClientType = Value

    End Sub

    Public Sub set_State_UpdateType(ByVal Value As String)

        xpFS.Text_Write(State_UpdateType, Value)
        xpMabiCore.State_UpdateType = Value

    End Sub

    Public Sub set_State_UpdateHideDay(ByVal Value As String)

        xpFS.Text_Write(State_UpdateHideDay, Value)

    End Sub

    Public Sub set_State_XmlViewer(ByVal Value As String)

        xpFS.Text_Write(State_XmlViewer, Value)

    End Sub

    Public Function is_Option_Run_PartUnPack() As Boolean

        Return My.Computer.FileSystem.FileExists(Option_Run_PartUnPack)

    End Function

    Public Function is_Option_Run_PartUnPack_IncludeAddons() As Boolean

        Return Not (My.Computer.FileSystem.FileExists(Option_Run_PartUnPack_IncludeAddons))

    End Function

    'Public Function is_Option_Run_SearchPackFile() As Boolean

    '    Return My.Computer.FileSystem.FileExists(Option_Run_SearchPackFile)

    'End Function

    Public Function is_Option_Run_AutomaticCheck_PackServer() As Boolean

        Return Not (My.Computer.FileSystem.FileExists(Option_Run_AutomaticCheck_PackServer))

    End Function

    Public Sub set_Option_Run_PartUnPack(ByVal MakeFile As Boolean)

        If MakeFile = True Then

            xpFS.Text_Write(Option_Run_PartUnPack, "")

        Else

            xpFS.File_Delete(Option_Run_PartUnPack)

        End If

    End Sub

    Public Sub set_Option_Run_PartUnPack_IncludeAddons(ByVal MakeFile As Boolean)

        MakeFile = Not (MakeFile)

        If MakeFile = True Then

            xpFS.Text_Write(Option_Run_PartUnPack_IncludeAddons, "")

        Else

            xpFS.File_Delete(Option_Run_PartUnPack_IncludeAddons)

        End If

    End Sub

    'Public Sub set_Option_Run_SearchPackFile(ByVal MakeFile As Boolean)

    '    If MakeFile = True Then

    '        FS.Text_Write(Option_Run_SearchPackFile, "")

    '    Else

    '        FS.File_Delete(Option_Run_SearchPackFile)

    '    End If


    'End Sub

    Public Sub set_Option_Run_AutomaticCheck_PackServer(ByVal MakeFile As Boolean)

        MakeFile = Not (MakeFile)

        If MakeFile = True Then

            xpFS.Text_Write(Option_Run_AutomaticCheck_PackServer, "")

        Else

            xpFS.File_Delete(Option_Run_AutomaticCheck_PackServer)

        End If

    End Sub
End Module
