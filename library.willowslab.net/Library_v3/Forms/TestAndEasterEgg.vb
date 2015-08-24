Imports System.IO
Imports System.Text
Imports System.Threading

Public Class TestAndEasterEgg

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ne As New notificationwindow(TextBox1.Text, ListBox1.SelectedIndex)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub TestAndEasterEgg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fdg As New OpenFileDialog
        If fdg.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Me.Label1.Text = fdg.FileName
            Try
                Using FHD As New FileStream(Me.Label1.Text, FileMode.Open, FileAccess.ReadWrite)
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
                        While (FileCount > Readed)
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
                            FHD.Seek(44, SeekOrigin.Current)
                        End While
                        FHD.Close()
                        BinaryReader.Close()
                    Else
                    End If
                End Using
            Catch ex As Exception
                Console.WriteLine(ex.GetBaseException.ToString)
            End Try
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim trd As Thread = New Thread(New ThreadStart(AddressOf tmptrd))
        trd.IsBackground = True
        trd.Start()
    End Sub

    Private Sub tmptrd()
        'Mabinogi.Decompress("d:\mabinogi\package\744_to_746.pack", "db\propdb.xml", 746UI, 6717396UL, 138130UL, 4305466UL)
        If Export("db\propdb.xml") Then
            Console.WriteLine("성공")
        Else
            Console.WriteLine("실패")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim ofd As New FolderBrowserDialog
        If ofd.ShowDialog() <> Windows.Forms.DialogResult.Cancel Then
            SevenSharpZip.Compress(ofd.SelectedPath, WLAB.CONF & "test.comp")
        End If
    End Sub
End Class