Public Class Compression

    ' 압축풀기(압축파일, 압축해제 폴더경로)
    Public Shared Function DeCompress(ByVal TargetZipFile As String, ByVal DeCompressFolder As String) As Boolean

        Dim Executable As String = "cmd.exe"
        Dim CommandLine As String = ""
        'Dim CommandLine As String = "chcp 65001"

        Try

            Dim MyStartInfo As New Diagnostics.ProcessStartInfo(Executable, CommandLine)

            MyStartInfo.UseShellExecute = False                                  ' CMD.EXE 등을 사용하지 않음, 직접실행
            MyStartInfo.RedirectStandardOutput = True                     ' 프로그램 출력(STDOUT)을 Redirect 함
            MyStartInfo.RedirectStandardInput = True                             ' STDIN 은 Redirect 하지 않음
            MyStartInfo.CreateNoWindow = True                               ' 프로그램 실행 윈도우즈를 만들지 않음

            Dim MyProcess As New Diagnostics.Process
            MyProcess.StartInfo = MyStartInfo
            MyProcess.Start()                                                            ' 프로세스를 실행함 

            MyProcess.StandardInput.Write("cd /D " & Chr(34) & vxConfig.DirMe & "" & Chr(34) & Environment.NewLine)
            MyProcess.StandardInput.Write("Call " & Chr(34) & "7za.exe" & Chr(34) & " x " & Chr(34) & TargetZipFile & Chr(34) & " -o" & Chr(34) & DeCompressFolder & Chr(34) & " -y" & Environment.NewLine)
            MyProcess.StandardInput.Close()

            Dim STDOUT As New System.IO.StreamReader(MyProcess.StandardOutput.BaseStream)

            Dim result As String = ""
            result &= MyProcess.StandardOutput.ReadToEnd()

            MyProcess.WaitForExit()
            MyProcess.Close()

            If InStr(result, "Error:", CompareMethod.Text) > 0 Then

                Dim StartLine As Integer = InStr(result, "Error", CompareMethod.Text) + 7
                Dim EndLine As Integer = InStr(StartLine, result, vbNewLine, CompareMethod.Text) - StartLine
                MsgBox("압축 풀기 에러 : " & Mid(result, StartLine + 1, EndLine))
                Return False

            ElseIf InStr(result, "OK", CompareMethod.Text) > 0 And InStr(result, "Compressed:", CompareMethod.Text) > 0 Then
                'MsgBox("압축 풀기 성공")
                Return True

            Else
                MsgBox("압축 풀기 에러 : 알 수 없는 에러가 발생하였습니다.")
                Return False

            End If


        Catch ex As Exception

            MsgBox(Executable & vbNewLine & ex.ToString)
            Return False

        End Try

        Return True

        Return True

    End Function

End Class
