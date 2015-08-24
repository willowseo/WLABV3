Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Public Class Installer
    Private Sub resized() Handles Me.Resize
        Dim PS As Rectangle = Screen.PrimaryScreen.WorkingArea
        Me.Location = New Point(((PS.Right - PS.Left) / 2) - (Me.Width / 2), ((PS.Bottom - PS.Top) / 2) - (Me.Height / 2))
    End Sub
    Private Sub Installer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.BackColor = Color.Transparent
        'Me.TransparencyKey = Color.Transparent
        Me.Refresh()
        Me.Size = New Size(595, 372)
        Me.mainpanel.Location = New Point(1, 31)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Page1.Location = New Point(1, 31)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Page1.Location = New Point(600, 0)
    End Sub
End Class

Class AlphaForm
    Inherits PerPixelAlphaForm
    'only text designed... T_T
    Friend WithEvents mainPanel As System.Windows.Forms.Panel
    Friend WithEvents titlePanel As System.Windows.Forms.Panel
    Friend WithEvents iconImage As System.Windows.Forms.PictureBox
    Friend WithEvents CloseButton As System.Windows.Forms.PictureBox


    Public Sub New()
        Me.titlePanel = New Panel
        Me.mainPanel = New Panel


        Me.titlePanel.BackColor = Func.rgb(45, 45, 48)
        Me.titlePanel.Size = New Size(595, 30)

        Me.mainPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(121, Byte), Integer), CType(CType(203, Byte), Integer))
        Me.mainPanel.Controls.Add(Me.titlePanel)
        Me.mainPanel.Location = New System.Drawing.Point(7, 7)
        Me.mainPanel.Name = "Panel1"
        Me.mainPanel.Size = New System.Drawing.Size(595, 372)
        Me.mainPanel.TabIndex = 0

        Me.TopMost = False
        Me.ShowInTaskbar = True
        Me.Icon = My.Resources.Resources.setting
        Me.mainPanel.ResumeLayout(False)
        Me.titlePanel.ResumeLayout(False)
        Me.Controls.Add(Me.mainPanel)
        Me.Refresh()
    End Sub
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H84 Then
            'WM_NCHITTEST
            m.Result = CType(2, IntPtr)
            ' HTCLIENT
            Return
        End If
        MyBase.WndProc(m)
    End Sub
End Class