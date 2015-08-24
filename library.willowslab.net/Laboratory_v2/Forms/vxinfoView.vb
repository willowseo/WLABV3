Public Class vxinfoView

    Private Sub infoView_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Width = 320
        Me.Height = vxFrmLiteMain.Height
        Me.Location = New System.Drawing.Point(vxFrmLiteMain.Location.X + vxFrmLiteMain.Width, vxFrmLiteMain.Location.Y)
    End Sub
End Class