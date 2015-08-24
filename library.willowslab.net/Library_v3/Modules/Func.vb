Imports System.IO

Public Module Func
    Public DefaultServerName As String = "버들실험실 공식 서버 I"
    Public DefaultServerURL As String = "http://library.willowslab.com/lib/"

    Dim PathDialog = New FolderBrowserDialog()
    Public Function webClient(ByVal fileURL, ByVal Dir, ByVal filename)
        Try
            Dim Down As New Net.WebClient
            If Not Directory.Exists(Dir) Then
                Directory.CreateDirectory(Dir)
            End If
            Down.DownloadFile(fileURL, Dir & filename)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function rgb(ByVal r As Integer, ByVal g As Integer, ByVal b As Integer) As Color
        Return System.Drawing.Color.FromArgb(CType(CType(r, Byte), Integer), CType(CType(g, Byte), Integer), CType(CType(b, Byte), Integer))
    End Function
    Public Function getReg(ByVal regPath As String, ByVal regKey As String) As String
        Return My.Computer.Registry.GetValue(regPath, regKey, "")
    End Function
    Public Sub setReg(ByVal regPath As String, ByVal regKey As String, ByVal regValue As Object)
        My.Computer.Registry.SetValue(regPath, regKey, regValue)
    End Sub
    Public Function setMabiPath() As Boolean
        If PathDialog.ShowDialog() <> DialogResult.Cancel Then
            Dim path As String = PathDialog.SelectedPath
            If ExistsFile(path & "\client.exe") And ExistsFile(path & "\version.dat") And (ExistsFile(path & "\mabinogi.exe") Or ExistsFile(path & "\mabinogi_test.exe")) Then
                setReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "", path)
                FSmsg("등록되었습니다. [" & path & "]")
                Setting.Mabi = getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "")
                Return True
            Else
                FSmsg("뭔가 이상한 곳을 고르지 않았나요?")
                setMabiPath()
            End If
        End If
        Return False
    End Function
    Public Function setMabiTestPath() As Boolean
        If PathDialog.ShowDialog() <> DialogResult.Cancel Then
            Dim path As String = PathDialog.SelectedPath
            If ExistsFile(path & "\client.exe") And ExistsFile(path & "\version.dat") And ExistsFile(path & "\mabinogi_test.exe") Then
                setReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi_test", "", path)
                FSmsg("등록되었습니다. [" & path & "]")
                Setting.MabiTest = getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi_test", "")
                Return True
            Else
                FSmsg("뭔가 이상한 곳을 고르지 않았나요?")
                setMabiTestPath()
            End If
        End If
        Return False
    End Function
    Public Sub readSet()

    End Sub
    Public Sub FSmsg(ByVal message As String)
        MsgBox(message, MsgBoxStyle.Information Or vbMsgBoxSetForeground, "지니나리마스!")
    End Sub
    Public Sub ServerSetChange(ByVal Name As String, ByVal URL As String)
        DefaultServerName = Name
        DefaultServerURL = URL
    End Sub
    Public Function ExistsFile(ByVal path As String) As Boolean
        If IO.File.Exists(path) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub ExistsDir(ByVal path As String)
        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
    End Sub

    Public Function getVersion(ByVal path As String) As Integer

        Dim fs As IO.FileStream = Nothing
        Dim br As IO.BinaryReader = Nothing
        Dim mVersion As UInteger = 0
        Dim FilePath As String = path

        Try
            If My.Computer.FileSystem.FileExists(FilePath) = True Then
                fs = New IO.FileStream(FilePath, IO.FileMode.Open, IO.FileAccess.Read)
                br = New IO.BinaryReader(fs)
                mVersion = br.ReadUInt32()
            End If
        Catch ex As Exception
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
End Module