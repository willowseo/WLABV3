Public Class xpAuth_donor

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'xpMD5Crypto.getMd5Hash
        Dim ID As String = Me.TextBox1.Text
        Dim PW As String = Me.TextBox2.Text

        xpFS.backGroundDownloader(xpMabiCore.PackInfoURL & "?donorID=" & ID & "&donorPW=" & xpMD5crypto.getMd5Hash(PW), xpConfig.DirMe & "\", "packinfo.info")
        xpFrmMain.XMLparse()
    End Sub

    Private Sub auth_donor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        xpFS.backGroundDownloader(xpMabiCore.PackInfoURL & "?no0=" & TextBox3.Text & "&no1=" & TextBox4.Text & "&no2=" & TextBox5.Text & "&no3=" & TextBox6.Text & "&no3=" & TextBox7.Text, xpConfig.Dirme & "\", "packinfo.info")
    End Sub
End Class