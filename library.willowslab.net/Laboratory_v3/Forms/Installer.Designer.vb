<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Installer
    Inherits System.Windows.Forms.Form
    Public Sub New()
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        'Me.BackColor = Color.Transparent
        InitializeComponent()
        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
    End Sub
    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
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
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80000
            ' This form has to have the WS_EX_LAYERED extended style
            Return cp
        End Get
    End Property

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mainpanel = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Page1 = New System.Windows.Forms.Panel()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mainpanel.SuspendLayout()
        Me.Page1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(121, Byte), Integer), CType(CType(203, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(595, 372)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.Location = New System.Drawing.Point(1, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(593, 370)
        Me.Panel2.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Button2)
        Me.Panel4.Controls.Add(Me.Button1)
        Me.Panel4.Location = New System.Drawing.Point(0, 320)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(595, 50)
        Me.Panel4.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.ForeColor = System.Drawing.Color.Black
        Me.Button2.Location = New System.Drawing.Point(395, 13)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 23)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "다음 >"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(494, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(86, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "취소"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(595, 30)
        Me.Panel3.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.closebtn
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Location = New System.Drawing.Point(561, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(34, 26)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.버들라이브러리.My.Resources.Resources.ico_png
        Me.PictureBox2.Location = New System.Drawing.Point(6, 7)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("맑은 고딕", 11.0!)
        Me.Label1.Location = New System.Drawing.Point(3, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(587, 22)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "WillowsLAB - Package Library for WillowsLAB Install"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'mainpanel
        '
        Me.mainpanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.mainpanel.Controls.Add(Me.Label3)
        Me.mainpanel.Controls.Add(Me.Label2)
        Me.mainpanel.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.mainpanel.Location = New System.Drawing.Point(1, 377)
        Me.mainpanel.Name = "mainpanel"
        Me.mainpanel.Size = New System.Drawing.Size(593, 292)
        Me.mainpanel.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(470, 36)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "이 프로그램은 당신의 컴퓨터에 Package Libaray For WillowsLab을 설치할 것입니다." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "계속하시려면 ""다음""을 눌러주세요." & _
    "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("굴림", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(476, 19)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Package Library For WillowsLab 설치를 시작합니다." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Page1
        '
        Me.Page1.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Page1.Controls.Add(Me.Label5)
        Me.Page1.Controls.Add(Me.Label4)
        Me.Page1.Controls.Add(Me.Button5)
        Me.Page1.Controls.Add(Me.Button4)
        Me.Page1.Controls.Add(Me.Button3)
        Me.Page1.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Page1.Location = New System.Drawing.Point(602, 1)
        Me.Page1.Name = "Page1"
        Me.Page1.Size = New System.Drawing.Size(593, 340)
        Me.Page1.TabIndex = 1
        '
        'Button5
        '
        Me.Button5.ForeColor = System.Drawing.Color.Black
        Me.Button5.Location = New System.Drawing.Point(309, 303)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(86, 23)
        Me.Button5.TabIndex = 0
        Me.Button5.Text = "< 뒤로"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.ForeColor = System.Drawing.Color.Black
        Me.Button4.Location = New System.Drawing.Point(395, 303)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(86, 23)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "다음 >"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.ForeColor = System.Drawing.Color.Black
        Me.Button3.Location = New System.Drawing.Point(494, 303)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(86, 23)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "취소"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(8, 286)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(234, 12)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Package Library For WillowsLab Append"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(249, 291)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(338, 1)
        Me.Label5.TabIndex = 2
        '
        'Installer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1553, 882)
        Me.Controls.Add(Me.Page1)
        Me.Controls.Add(Me.mainpanel)
        Me.Controls.Add(Me.Panel1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Installer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Installer"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mainpanel.ResumeLayout(False)
        Me.mainpanel.PerformLayout()
        Me.Page1.ResumeLayout(False)
        Me.Page1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents mainpanel As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Page1 As System.Windows.Forms.Panel
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
