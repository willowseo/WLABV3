Imports System.Threading
Public Module notifiController
    Public int As Integer = 0
    Public privWind As Boolean = False
    Public minCount As Integer = 0
End Module

Public Class notificationwindow
    Inherits System.Windows.Forms.Form

    Delegate Sub setCB()

    Private components As System.ComponentModel.IContainer
    Private WithEvents NotificationText As System.Windows.Forms.Label
    Private WithEvents borderPanel As System.Windows.Forms.Panel
    Private WithEvents LifeTimer1 As System.Windows.Forms.Timer
    Private WithEvents iconBox As System.Windows.Forms.PictureBox
    Private window As Object = Screen.PrimaryScreen.WorkingArea
    Private trd As Thread
    Private lifeTimeMax As Integer = 0
    Private disposeCount As Integer = 0
    Private ht As Integer = notifiController.int
    Private hxName As String = Hex(Math.Floor(Rnd() * 999999))
    Public Sub New(ByVal message As String, Optional ByVal icon As Integer = 0)
        notifiController.int += 1
        Me.borderPanel = New System.Windows.Forms.Panel
        Me.LifeTimer1 = New System.Windows.Forms.Timer
        Me.NotificationText = New System.Windows.Forms.Label
        Me.iconBox = New System.Windows.Forms.PictureBox
        'MsgBox(Screen.PrimaryScreen.WorkingArea.Width & " " & Screen.PrimaryScreen.WorkingArea.Height)
        'setting Window
        Me.components = New System.ComponentModel.Container()
        Me.ClientSize = New Size(250, 75)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Text = "notification." & hxName
        Me.ShowInTaskbar = False
        Me.ShowIcon = False
        Me.BackColor = Color.White
        Me.ForeColor = Color.Black
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Location = New System.Drawing.Point((window.Width + window.X) - Me.Width, (window.Height + window.Y) - (Me.Height * notifiController.int) - 34)
        Me.TopMost = True
        Me.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.breadnotification
        Me.SuspendLayout()
        'setting iconBox
        Me.iconBox.Location = New System.Drawing.Point(13, 13)
        Me.iconBox.Size = New Size(48, 48)
        Me.iconBox.Image = Global.버들라이브러리.My.Resources.Resources.noti_information
        Me.iconBox.BackColor = Color.Transparent
        Select Case icon
            Case 1
                Me.iconBox.Image = Global.버들라이브러리.My.Resources.Resources.noti_completedownload
            Case 2
                Me.iconBox.Image = Global.버들라이브러리.My.Resources.Resources.noti_completeupload
            Case 3
                Me.iconBox.Image = Global.버들라이브러리.My.Resources.Resources.noti_login
            Case 4
                Me.iconBox.Image = Global.버들라이브러리.My.Resources.Resources.noti_warn
            Case 5
                Me.iconBox.Image = Global.버들라이브러리.My.Resources.Resources.noti_completefiledownload
        End Select
        'setting Label
        Me.NotificationText.Location = New System.Drawing.Point(68, 8)
        Me.NotificationText.Font = New System.Drawing.Font("굴림", 9.0!)
        Me.NotificationText.Size = New Size(Me.Width - 74, Me.Height - 16)
        Me.NotificationText.Text = message.Replace("<br>", vbNewLine)
        Me.NotificationText.TextAlign = ContentAlignment.MiddleLeft
        Me.NotificationText.BackColor = Color.Transparent
        Me.ShowInTaskbar = False
        'timer set
        Me.LifeTimer1.Interval = 10
        Me.LifeTimer1.Enabled = True
        'add components
        Me.Controls.Add(Me.iconBox)
        Me.Controls.Add(Me.NotificationText)
        'reset name
        Me.Name = hxName
        'pop!
        Me.Show()
        Me.Refresh()
        Me.Refresh()
        Me.Refresh()
    End Sub

    Private Sub lifeCount1() Handles LifeTimer1.Tick
        notifiController.privWind = True
        If lifeTimeMax > 200 Then
            Me.reSizedForm()
        End If
        lifeTimeMax += 1
    End Sub

    Private Sub reSizedForm()
        Me.LifeTimer1.Enabled = False
        Dim cntM As Boolean = False
        For i As Integer = 0 To 80
            If Me.Height > 2 Then
                notifiController.privWind = True
                Me.Height -= 1
                Me.Opacity -= 0.02R
                Me.Location = New System.Drawing.Point((window.Width + window.X) - Me.Width, (window.Height + window.Y) - (75 * ht) - Me.Height - 34)
                System.Threading.Thread.Sleep(2)
            Else
                Me.LifeTimer1.Enabled = Nothing
                notifiController.privWind = False
                System.Threading.Thread.Sleep(10)
                If notifiController.privWind Then
                Else
                    cntM = True
                End If
                notifiController.privWind = False
                Me.Close()
                Me.Dispose()
            End If
        Next
        If notifiController.int > 0 Then
            notifiController.int -= 2
            If notifiController.int < 0 Then
                notifiController.int = 0
            End If
        End If
        Exit Sub
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
End Class
