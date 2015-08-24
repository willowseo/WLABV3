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

    Public Sub DataReplacer(ByVal hide As Boolean)
        Try
            Dim cont As New List(Of String)
            Dim directories As New List(Of String)
            Dim CurrentSeekLast As Long = 0L
            Dim cache As Byte() = New Byte(4) {}
            Dim b7 As Byte() = New Byte(8) {}
            Dim bF As Byte() = New Byte(16) {}
            Dim count As Integer = 0

            fileTotal = 0
            Directory.CreateDirectory(Setting.Mabi & "\data\")
            For Each str As String In Directory.GetFiles(Setting.Mabi & "\data\", "*", SearchOption.AllDirectories)
                Dim fn As String = str.Replace(Setting.Mabi & "\data\", "")
                cont.Add(fn)
            Next

            For Each pack As String In Directory.GetFiles(Setting.Mabi & "\package\")
                If pack.Substring(pack.LastIndexOf(".") + 1).ToLower = "pack" And (InStr(pack, "_to_") Or InStr(pack, "_full")) Then
                    fileTotal += 1
                    directories.Add(pack)
                End If
            Next

            For Each pack As String In directories
                Dim FileStream As FileStream = New FileStream(pack, FileMode.Open, FileAccess.ReadWrite)
                Dim binaryReader As BinaryReader = New BinaryReader(FileStream)
                Dim binaryWriter As BinaryWriter = New BinaryWriter(FileStream)
                Dim FileCount As Long = 0L
                Dim ListSize As Long = 0L
                Dim ZeroSize As Long = 0L
                Dim CompressSize As Long = 0L
                Dim Readed As Long = 0L

                count += 1

                Using FHD As FileStream = FileStream

                    FHD.Seek(512, SeekOrigin.Current)

                    FHD.Read(cache, 0, 4)
                    FileCount = CType(BitConverter.ToInt32(cache, 0), Long)
                    FHD.Read(cache, 0, 4)
                    ListSize = CType(BitConverter.ToInt32(cache, 0), Long)
                    FHD.Read(cache, 0, 4)
                    ZeroSize = CType(BitConverter.ToInt32(cache, 0), Long)
                    FHD.Read(cache, 0, 4)
                    CompressSize = CType(BitConverter.ToInt32(cache, 0), Long)
                    FHD.Seek(16, SeekOrigin.Current)

                    While (FileCount > Readed And ListSize > binaryReader.BaseStream.Position)
                        CurrentSeekLast = CType(binaryReader.BaseStream.Position, Long)
                        Readed += 1
                        Dim currFilename As New StringBuilder
                        Dim linecount As Integer = 0
                        Dim dotseek As Long = 0L
                        Dim tmp1Bt As Byte() = New Byte(1) {}
                        FHD.Read(tmp1Bt, 0, 1)
                        linecount = Convert.ToInt32(tmp1Bt(0))

                        Dim readedByte As Integer = 15 + (linecount * 16)
                        Dim line4bitOvered As Boolean = False
                        Dim filePathX As String = ""

                        If readedByte > 256 Then
                            readedByte = 255
                        End If

                        Dim pathByte As Byte() = New Byte(readedByte - 1) {}

                        FHD.Read(pathByte, 0, readedByte)

                        If linecount > 4 Then
                            line4bitOvered = True
                        End If

                        Dim idx As Integer = 0
                        For Each chars As Byte In pathByte
                            idx += 1
                            If line4bitOvered And idx < 4 Then
                                Continue For
                            End If

                            If chars > 31 And chars < 127 Then
                                filePathX += Convert.ToChar(chars)
                            End If
                        Next


                        Dim tmpByte As Byte() = New Byte(7) {}
                        Dim isPathEnded As Boolean = False

                        While Not isPathEnded
                            FHD.Read(tmpByte, 0, 8)
                            Dim vers As ULong = BitConverter.ToUInt64(tmpByte, 0)
                            If vers < 2048 And vers > 651 Then
                                isPathEnded = True
                            Else
                                For Each chars As Byte In tmpByte
                                    If chars > 31 And chars < 127 Then
                                        filePathX += Convert.ToChar(chars)
                                    End If
                                Next
                            End If
                        End While

                        currFilename.Append(filePathX)

                        Dim cfn As String = currFilename.ToString

                        If hide Then
                            dotseek = CType(CurrentSeekLast + cfn.LastIndexOf(".") + 1, Long)
                        Else
                            dotseek = CType(CurrentSeekLast + cfn.LastIndexOf("*") + 1, Long)
                        End If

                        CurrentSeekLast = CType(binaryReader.BaseStream.Position, Long)

                        Dim dataarr(0) As Byte
                        Dim working As String = ""
                        If hide Then
                            For Each Path As String In cont
                                If Path.Contains(cfn) Then
                                    dataarr(0) = &H2A
                                    working = " 를 감추었습니다."
                                    '
                                    ' 헤더를 수정할 위치로 옮긴 뒤 수정
                                    '
                                    FHD.Seek(dotseek, SeekOrigin.Begin)
                                    progressTextChange("* " & pack.Replace(Setting.Mabi & "\package\", "") & " 에서 " & cfn & working & Chr(10).ToString)
                                    addLog("* " & pack.Replace(Setting.Mabi & "\package\", "") & " 에서 " & cfn & working)
                                    'offsetList.Add(dotseek)
                                    binaryWriter.Write(dataarr, SeekOrigin.Begin, dataarr.Length)

                                    '
                                    ' 수정 끝나면 돌려놓으셈
                                    '
                                    FHD.Seek(CurrentSeekLast, SeekOrigin.Begin)
                                    Exit For
                                End If
                            Next
                        Else
                            If cfn.Contains("*") Then
                                FHD.Seek(dotseek, SeekOrigin.Begin)
                                'offsetList.Add(dotseek)
                                dataarr(0) = &H2E
                                binaryWriter.Write(dataarr, SeekOrigin.Begin, dataarr.Length)
                                working = " 를 복원하였습니다."
                                progressTextChange("* " & pack.Replace(Setting.Mabi & "\package\", "") & " 에서 " & cfn.Replace("*", ".") & working & Chr(10).ToString)
                                addLog("* " & pack.Replace(Setting.Mabi & "\package\", "") & " 에서 " & cfn.Replace("*", ".") & working)
                                FHD.Seek(CurrentSeekLast, SeekOrigin.Begin)
                            End If
                        End If
                        'MsgBox(Convert.ToInt32(bF(0)))
                        FHD.Seek(56, SeekOrigin.Current)
                    End While
                End Using
                binaryReader.Close()
                binaryReader.Dispose()
                binaryWriter.Close()
                binaryWriter.Dispose()
                FileStream.Close()
                FileStream.Dispose()
                'RichTextBox1.AppendText(filename & "에서 숨겨주어야 할 파일들은 다음과 같습니다.")
            Next
        Catch ex As Exception
            Console.WriteLine(ex.GetBaseException.ToString)
        End Try
    End Sub
End Class
