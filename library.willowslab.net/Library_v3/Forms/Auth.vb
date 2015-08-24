Public Class Auth
    Public int As Integer

    Public Function webClient(ByVal fileURL, ByVal Dir, ByVal filename)
        Try
            Dim Down As New Net.WebClient
            Down.DownloadFile(fileURL, Dir & filename)
            Return True
        Catch ex As Exception
            MsgBox("인증 코드를 서버에서 확인하지 못했습니다.")
            Return False
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Func.ExistsDir(Setting.Dir & "\data\")
        If webClient("http://library.willowslab.com/auth/?key=" & Me.TextBox1.Text, Setting.Dir & "\data\", "auth_temp.xml") Then
            Try
                Dim xmlDoc As New Xml.XmlDocument
                Dim listExtract As Xml.XmlNodeList
                Dim ByteCount As Integer = 4
                Dim HexCode As String = Nothing
                Dim HexCodeCount As Integer = Nothing
                Dim ReserseHexCode = ""
                Dim bytes(ByteCount - 1) As Byte

                xmlDoc.Load(Setting.Dir & "\data\auth_temp.xml")
                listExtract = xmlDoc.SelectNodes("/Auth/VersionInfo")

                For Each tmp As Xml.XmlElement In listExtract
                    HexCode = Hex(tmp.GetAttribute("Version"))
                Next

                HexCodeCount = HexCode.Length

                For i = 0 To (ByteCount * 2 - 1) - HexCodeCount
                    HexCode = "0" & HexCode
                Next
                For i = (ByteCount) - 1 To 0 Step -1
                    ReserseHexCode &= HexCode.Substring(i * 2, 2)
                Next
                For i As Integer = 0 To ByteCount - 1
                    bytes(i) = Convert.ToByte(ReserseHexCode.Substring(i * 2, 2), 16)
                Next

                My.Computer.FileSystem.WriteAllBytes(Setting.Dir & "\data\version.dat", bytes, True)
                IO.File.Delete(Setting.Dir & "\data\auth_temp.xml")
                Func.FSmsg("인증에 성공했습니다.")
                Init.Show()
                Me.Close()
            Catch ex As Exception
                IO.File.Delete(Setting.Dir & "\data\auth_temp.xml")
                Func.FSmsg("인증 코드가 틀렸습니다!" & vbNewLine & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub
End Class