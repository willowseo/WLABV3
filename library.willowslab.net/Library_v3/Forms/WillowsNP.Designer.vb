<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WillowsNP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WillowsNP))
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.pTotal = New System.Windows.Forms.Panel()
        Me.pMain = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(8, 31)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.RichTextBox1.Size = New System.Drawing.Size(446, 200)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = "버들실험실 - 제2관 패키지 구조 연구소 버전 3.x by 버들서" & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(539, 279)
        Me.Panel1.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(1, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(537, 277)
        Me.Panel2.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.Black
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Location = New System.Drawing.Point(2, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(533, 273)
        Me.Panel3.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.PictureBox1)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.RichTextBox1)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Font = New System.Drawing.Font("굴림", 8.0!)
        Me.Panel4.Location = New System.Drawing.Point(2, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel4.Size = New System.Drawing.Size(529, 269)
        Me.Panel4.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.ct
        Me.Panel5.Controls.Add(Me.pTotal)
        Me.Panel5.Controls.Add(Me.pMain)
        Me.Panel5.Location = New System.Drawing.Point(13, 244)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(503, 13)
        Me.Panel5.TabIndex = 7
        '
        'pTotal
        '
        Me.pTotal.BackColor = System.Drawing.Color.Black
        Me.pTotal.Location = New System.Drawing.Point(1, 2)
        Me.pTotal.Name = "pTotal"
        Me.pTotal.Size = New System.Drawing.Size(50, 9)
        Me.pTotal.TabIndex = 0
        '
        'pMain
        '
        Me.pMain.BackColor = System.Drawing.Color.Black
        Me.pMain.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.progressbg
        Me.pMain.Location = New System.Drawing.Point(1, 2)
        Me.pMain.Name = "pMain"
        Me.pMain.Size = New System.Drawing.Size(100, 9)
        Me.pMain.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.Image = Global.버들라이브러리.My.Resources.Resources.rt
        Me.Label6.Location = New System.Drawing.Point(516, 244)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(2, 13)
        Me.Label6.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Image = Global.버들라이브러리.My.Resources.Resources.lf
        Me.Label5.Location = New System.Drawing.Point(11, 244)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(2, 13)
        Me.Label5.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.Image = Global.버들라이브러리.My.Resources.Resources.Mabinogi_127
        Me.Label4.Location = New System.Drawing.Point(489, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 11)
        Me.Label4.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Image = Global.버들라이브러리.My.Resources.Resources.Mabinogi_119
        Me.Label3.Location = New System.Drawing.Point(460, 56)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 19)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "복구"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Image = Global.버들라이브러리.My.Resources.Resources.Mabinogi_119
        Me.Label2.Location = New System.Drawing.Point(460, 31)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "적용"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.버들라이브러리.My.Resources.Resources.Mabinogi_134
        Me.PictureBox1.Location = New System.Drawing.Point(6, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 12)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(60, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 11)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "버들실험실 제2실험실"
        '
        'Panel6
        '
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(529, 24)
        Me.Panel6.TabIndex = 8
        '
        'WillowsNP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(539, 279)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WillowsNP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "부들팩"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents pTotal As System.Windows.Forms.Panel
    Friend WithEvents pMain As System.Windows.Forms.Panel
End Class
