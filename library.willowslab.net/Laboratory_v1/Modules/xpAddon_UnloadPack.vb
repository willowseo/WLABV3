Module xpAddon_UnloadPack

    Public Enum PackageType

        MabiPackage
        AddonPackage
        ALL

    End Enum

    'o : 마비 패키지에서 파일목록 추출
    'r : 마비 패키지에서 파일목록 읽고 복구
    'c : 마비 패키지에서 전부 복구
    'm : 마비 패키지에서 파일목록 읽고 적용
    Public Sub Run_UnloadPack(ByVal Mode As String)

        Dim Executable As String = "cmd.exe"
        Dim CommandLine As String = ""
        'Dim CommandLine As String = "chcp 65001"


        Dim MyStartInfo As New Diagnostics.ProcessStartInfo(Executable, CommandLine)

        MyStartInfo.UseShellExecute = False                                  ' CMD.EXE 등을 사용하지 않음, 직접실행
        MyStartInfo.RedirectStandardOutput = True                     ' 프로그램 출력(STDOUT)을 Redirect 함
        MyStartInfo.RedirectStandardInput = True                             ' STDIN 은 Redirect 하지 않음
        MyStartInfo.CreateNoWindow = True                               ' 프로그램 실행 윈도우즈를 만들지 않음
        MyStartInfo.WindowStyle = ProcessWindowStyle.Hidden

        Dim MyProcess As New Diagnostics.Process
        MyProcess.StartInfo = MyStartInfo
        MyProcess.Start()                                                            ' 프로세스를 실행함 

        MyProcess.StandardInput.Write("cd /D " & Chr(34) & xpMabiCore.Dir_Myself_Assistant & "" & Chr(34) & Environment.NewLine)
        If Mode = "cm" Then
            MyProcess.StandardInput.Write("Call " & Chr(34) & xpMabiCore.UnloadPackage & Chr(34) & " c" & Environment.NewLine)
            MyProcess.StandardInput.Write("Call " & Chr(34) & xpMabiCore.UnloadPackage & Chr(34) & " m" & Environment.NewLine)
        Else
            MyProcess.StandardInput.Write("Call " & Chr(34) & xpMabiCore.UnloadPackage & Chr(34) & " " & Mode & Environment.NewLine)
        End If
        MyProcess.StandardInput.Close()

        Dim STDOUT As New System.IO.StreamReader(MyProcess.StandardOutput.BaseStream)

        Dim result As String = ""
        result &= MyProcess.StandardOutput.ReadToEnd()

        MyProcess.WaitForExit()
        MyProcess.Close()

    End Sub

    Public Sub Add_FileList(ByVal Files As List(Of String))

        Dim Data As String = ""

        For Each File As String In Files

            Data &= File & vbNewLine

        Next

        xpFS.Text_Write(xpMabiCore.Dir_Myself_Assistant & "\file_list.txt", Trim(Replace(Data, xpMabiCore.Dir_Mabinogi & "\data\", "")), "Default")

    End Sub

    Public Sub Add_FileList(ByVal Data As String)


        xpFS.Text_Write(xpMabiCore.Dir_Myself_Assistant & "\file_list.txt", Trim(Replace(Data, xpMabiCore.Dir_Mabinogi & "\data\", "")), "Default")

    End Sub

    Public Sub Add_MabiPackList(ByVal Data As String)

        xpFS.Text_Write(xpMabiCore.Dir_Myself_Assistant & "\pack_list.txt", Trim(Data), "Default")

    End Sub

    Public Function getPackagesToString(ByVal vType As PackageType) As String

        Dim Result As String = ""

        For Each vPack As String In getPackagesToList(vType)

            Result &= vPack & vbNewLine

        Next

        Return Result

    End Function

    Public Function getPackagesToList(ByVal vType As PackageType) As List(Of String)

        Dim Target As String = xpMabiCore.Dir_Mabinogi & "\package"

        If My.Computer.FileSystem.DirectoryExists(Target) = True Then
            Dim MabiPackage As List(Of String) = New List(Of String)
            Dim AddonPackage As List(Of String) = New List(Of String)
            Dim ALLPackage As List(Of String) = New List(Of String)

            For Each vPack As String In xpFS.getFiles(Target, xpFS.getfilestype.FindExtension, ".pack")

                ALLPackage.Add(vPack)

                Dim PackName As String = System.IO.Path.GetFileNameWithoutExtension(vPack) '파일명 추출
                Dim isMabiPack As Boolean = False

                If InStr(PackName, "_full") > 0 Then

                    Dim Temp() As String = Split(PackName, "_full")

                    If IsNumeric(Temp(0)) = True Then

                        isMabiPack = True
                        MabiPackage.Add(vPack)

                    End If

                ElseIf InStr(PackName, "_to_") > 0 Then

                    Dim Temp() As String = Split(PackName, "_to_")

                    If Temp.Length = 2 Then
                        If IsNumeric(Temp(0)) = True And IsNumeric(Temp(1)) = True Then

                            isMabiPack = True
                            MabiPackage.Add(vPack)

                        End If
                    End If



                End If

                If isMabiPack = False Then

                    AddonPackage.Add(vPack)

                End If

            Next

            Select Case vType

                Case PackageType.MabiPackage

                    Return MabiPackage

                Case PackageType.AddonPackage

                    Return AddonPackage

                Case PackageType.ALL

                    Return ALLPackage

            End Select
        End If


        Return Nothing

    End Function

    ' 패키지에서 파일 목록 삭제
    Public Sub RemoveFileListByPack()
        Dim Target As String = xpMabiCore.Dir_Mabinogi & "\package"

        If My.Computer.FileSystem.DirectoryExists(Target) = True Then

            ' uph 파일 지우고
            For Each vFile As String In xpFS.getFiles(Target, xpFS.getfilestype.FindExtension, ".uph")
                xpFS.File_Delete(vFile)
            Next

        End If

    End Sub

    ' 패키지에서 파일 목록 삭제
    Public Sub RemoveFileIndexByPack()
        Dim Target As String = xpMabiCore.Dir_Mabinogi & "\package"

        If My.Computer.FileSystem.DirectoryExists(Target) = True Then

            ' uph 파일 지우고
            For Each vFile As String In xpFS.getFiles(Target, xpFS.getfilestype.FindExtension, ".uidx")
                xpFS.File_Delete(vFile)
            Next

        End If

    End Sub

    ' 패키지에서 파일 목록 추출
    Public Sub CreateFileListByPack()

        ' uph 파일 지우고
        RemoveFileListByPack()

        Add_MabiPackList(getPackagesToString(PackageType.MabiPackage))
        Run_UnloadPack("o")

    End Sub

    ' 패키지에서 파일 목록 추출
    Public Sub CreateFileIndexByPack()

        ' uidx 파일 지우고
        RemoveFileIndexByPack()

        Add_MabiPackList(getPackagesToString(PackageType.MabiPackage))
        Run_UnloadPack("u")

    End Sub

    Public Sub Run_MabiPackageClear()

        Dim vList As List(Of String) = New List(Of String)

        If xpConfig.is_Option_Run_PartUnPack_IncludeAddons() = True Then

            For Each vFile As String In getPackagesToList(PackageType.AddonPackage)

                If My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = True Then

                    My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = False
                    vList.Add(vFile)

                End If

            Next

            Add_MabiPackList(getPackagesToString(PackageType.ALL))

        Else

            Add_MabiPackList(getPackagesToString(PackageType.MabiPackage))

        End If

        Run_UnloadPack("c")

        If xpConfig.is_Option_Run_PartUnPack_IncludeAddons() = True Then

            For Each vFile As String In vList

                My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = True

            Next

        End If

    End Sub

    Public Sub Run_MabiPackageClear_And_Apply(ByVal FileList As String)

        Dim vList As List(Of String) = New List(Of String)

        If xpConfig.is_Option_Run_PartUnPack_IncludeAddons() = True Then

            For Each vFile As String In getPackagesToList(PackageType.AddonPackage)

                If My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = True Then

                    My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = False
                    vList.Add(vFile)

                End If

            Next

            Add_MabiPackList(getPackagesToString(PackageType.ALL))

        Else

            Add_MabiPackList(getPackagesToString(PackageType.MabiPackage))

        End If

        Add_FileList(FileList)
        Run_UnloadPack("cm")

        If xpConfig.is_Option_Run_PartUnPack_IncludeAddons() = True Then

            For Each vFile As String In vList

                My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = True

            Next

        End If

    End Sub

    Public Sub Run_MabiPackageApply(ByVal FileList As String)

        Dim vList As List(Of String) = New List(Of String)

        If xpConfig.is_Option_Run_PartUnPack_IncludeAddons() = True Then

            For Each vFile As String In getPackagesToList(PackageType.AddonPackage)

                If My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = True Then

                    My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = False
                    vList.Add(vFile)

                End If

            Next

            Add_MabiPackList(getPackagesToString(PackageType.ALL))

        Else

            Add_MabiPackList(getPackagesToString(PackageType.MabiPackage))

        End If

        Add_FileList(FileList)
        Run_UnloadPack("m")

        If xpConfig.is_Option_Run_PartUnPack_IncludeAddons() = True Then

            For Each vFile As String In vList

                My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = True

            Next

        End If

    End Sub

    Public Sub Run_MabiPackageApply(ByVal Files As List(Of String))

        Dim vList As List(Of String) = New List(Of String)

        If xpConfig.is_Option_Run_PartUnPack_IncludeAddons() = True Then

            For Each vFile As String In getPackagesToList(PackageType.AddonPackage)

                If My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = True Then

                    My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = False
                    vList.Add(vFile)

                End If

            Next

            Add_MabiPackList(getPackagesToString(PackageType.ALL))

        Else

            Add_MabiPackList(getPackagesToString(PackageType.MabiPackage))

        End If

        Add_FileList(Files)
        Run_UnloadPack("m")

        If xpConfig.is_Option_Run_PartUnPack_IncludeAddons() = True Then

            For Each vFile As String In vList

                My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = True

            Next

        End If

    End Sub

    Public Sub Run_MabiPackageRestore(ByVal FileList As String)

        Dim vList As List(Of String) = New List(Of String)

        If xpConfig.is_Option_Run_PartUnPack_IncludeAddons() = True Then

            For Each vFile As String In getPackagesToList(PackageType.AddonPackage)

                If My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = True Then

                    My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = False
                    vList.Add(vFile)

                End If

            Next

            Add_MabiPackList(getPackagesToString(PackageType.ALL))

        Else

            Add_MabiPackList(getPackagesToString(PackageType.MabiPackage))

        End If

        Add_FileList(FileList)
        Run_UnloadPack("r")

        If xpConfig.is_Option_Run_PartUnPack_IncludeAddons() = True Then

            For Each vFile As String In vList

                My.Computer.FileSystem.GetFileInfo(vFile).IsReadOnly = True

            Next

        End If

    End Sub

End Module
