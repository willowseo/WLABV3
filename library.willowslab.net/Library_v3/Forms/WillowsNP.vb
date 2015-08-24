Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Drawing
Imports System.Text.RegularExpressions

Public Class WillowsNP
    Private Delegate Sub CallBack([object] As Object)
    Private Delegate Sub CallBackText([text] As String)
    Private fileTotal As Integer = 0

    Private Sub appendText(ByVal [text] As String)
        If Me.RichTextBox1.InvokeRequired Then
            Dim queue As New CallBack(AddressOf appendText)
            Me.Invoke(queue, New Object() {[text]})
        Else
            If [text] = "init" Then
                Me.RichTextBox1.Text = "버들실험실 - 제2관 패키지 구조 연구소 버전 3.x by 버들서" & Chr(10)
                appendText("* 마비노기 설치 경로 : " & Setting.Mabi & Chr(10))
            Else
                Me.RichTextBox1.AppendText([text])
            End If
        End If
    End Sub

    Private Sub ProgressBarMain(ByVal val As Integer)
        If Me.pMain.InvokeRequired Then
            Dim queue As New CallBack(AddressOf appendText)
            Me.Invoke(queue, New Object() {val})
        Else
            If val > 500 Then
                val = 500
            End If
            Me.pMain.Width = Math.Floor(val)
        End If
    End Sub

    Private Sub ProgressBarTotal(ByVal val As Integer)
        If Me.pTotal.InvokeRequired Then
            Dim queue As New CallBack(AddressOf appendText)
            Me.Invoke(queue, New Object() {val})
        Else
            If val > 500 Then
                val = 500
            End If
            Me.pTotal.Width = Math.Floor(val)
        End If
    End Sub

    Public Function RWPack(ByVal hide As Boolean, Optional ByVal filename As String = "") As Boolean
        Try
            Dim cont As New List(Of String)
            Dim directories As New List(Of String)
            Dim CurrentSeekLast As Long = 0L
            Dim cache As Byte() = New Byte(4) {}
            Dim b7 As Byte() = New Byte(8) {}
            Dim bF As Byte() = New Byte(16) {}
            Dim count As Integer = 0

            fileTotal = 0
            appendText("init")

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
                ProgressBarTotal((count / fileTotal) * 500)

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

                    While (FileCount > Readed)
                        CurrentSeekLast = CType(binaryReader.BaseStream.Position, Long)
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
                            linecount += linecount - 3
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
                                    appendText("* " & pack.Replace(Setting.Mabi & "\package\", "") & " 에서 " & cfn & working & Chr(10).ToString)
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
                                appendText("* " & pack.Replace(Setting.Mabi & "\package\", "") & " 에서 " & cfn.Replace("*", ".") & working & Chr(10).ToString)
                                FHD.Seek(CurrentSeekLast, SeekOrigin.Begin)
                            End If
                        End If
                        'MsgBox(Convert.ToInt32(bF(0)))
                        FHD.Seek(64, SeekOrigin.Current)
                    End While
                End Using
                'RichTextBox1.AppendText(filename & "에서 숨겨주어야 할 파일들은 다음과 같습니다.")
            Next
            Return True
        Catch ex As Exception
            Return False
            Console.WriteLine(ex.GetBaseException.ToString)
        End Try
    End Function

    Public Sub RWpackH()
        RWPack(True)
    End Sub
    Public Sub RWpackS()
        RWPack(False)
    End Sub
    Private Sub WillowsNP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        appendText("* 마비노기 설치 경로 : " & Setting.Mabi & Chr(10))
        ProgressBarMain(0)
        ProgressBarTotal(0)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub btnStyle_down(sender As System.Windows.Forms.Label, e As EventArgs) Handles Label2.MouseDown, Label3.MouseDown
        sender.Image = My.Resources.Resources.Mabinogi_121
    End Sub
    Private Sub btnStyle_up(sender As System.Windows.Forms.Label, e As EventArgs) Handles Label2.MouseUp, Label3.MouseUp
        sender.Image = My.Resources.Resources.Mabinogi_119
    End Sub
    Private Sub btnStyleClose_down(sender As System.Windows.Forms.Label, e As EventArgs) Handles Label4.MouseDown
        sender.Image = My.Resources.Resources.Mabinogi_129
    End Sub
    Private Sub btnStyleClose_up(sender As System.Windows.Forms.Label, e As EventArgs) Handles Label4.MouseUp
        sender.Image = My.Resources.Resources.Mabinogi_127
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        RWPack(True)
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        RWPack(False)
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        RichTextBox1.Focus()
        RichTextBox1.SelectionStart = RichTextBox1.TextLength
        RichTextBox1.ScrollToCaret()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Close()
    End Sub
End Class