<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
    Inherits System.Windows.Forms.Form

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

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.titlePanel = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.frmTitle = New System.Windows.Forms.Label()
        Me.closebtn = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.titlePanel.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.closebtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'titlePanel
        '
        Me.titlePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.titlePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.titlePanel.Controls.Add(Me.PictureBox1)
        Me.titlePanel.Controls.Add(Me.frmTitle)
        Me.titlePanel.Controls.Add(Me.closebtn)
        Me.titlePanel.Location = New System.Drawing.Point(1, 1)
        Me.titlePanel.Name = "titlePanel"
        Me.titlePanel.Size = New System.Drawing.Size(398, 30)
        Me.titlePanel.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(7, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'frmTitle
        '
        Me.frmTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.frmTitle.Font = New System.Drawing.Font("맑은 고딕", 11.0!)
        Me.frmTitle.Location = New System.Drawing.Point(33, 4)
        Me.frmTitle.Name = "frmTitle"
        Me.frmTitle.Size = New System.Drawing.Size(317, 22)
        Me.frmTitle.TabIndex = 1
        Me.frmTitle.Text = "Package Library of WillowsLAB v"
        Me.frmTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'closebtn
        '
        Me.closebtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.closebtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.closebtn.BackgroundImage = CType(resources.GetObject("closebtn.BackgroundImage"), System.Drawing.Image)
        Me.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.closebtn.Location = New System.Drawing.Point(353, 0)
        Me.closebtn.Margin = New System.Windows.Forms.Padding(0)
        Me.closebtn.Name = "closebtn"
        Me.closebtn.Size = New System.Drawing.Size(46, 20)
        Me.closebtn.TabIndex = 0
        Me.closebtn.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.RichTextBox2)
        Me.Panel1.Controls.Add(Me.RichTextBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(1, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(398, 189)
        Me.Panel1.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(289, 107)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(83, 35)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "로그인"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "비밀번호"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "아이디"
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Location = New System.Drawing.Point(81, 26)
        Me.RichTextBox2.Multiline = False
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(291, 20)
        Me.RichTextBox2.TabIndex = 1
        Me.RichTextBox2.Text = ""
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(81, 51)
        Me.RichTextBox1.Multiline = False
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(291, 20)
        Me.RichTextBox1.TabIndex = 1
        Me.RichTextBox1.Text = ""
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(2, 160)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(394, 27)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "이 기능을 사용하면 버들팩도서관의 이용약관이 아닌" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "버들랩의 이용약관을 따르게 됩니다."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 220)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.titlePanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Login"
        Me.TopMost = True
        Me.titlePanel.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.closebtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents titlePanel As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents frmTitle As System.Windows.Forms.Label
    Friend WithEvents closebtn As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RichTextBox2 As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
