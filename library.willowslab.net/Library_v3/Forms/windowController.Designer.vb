<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class windowController
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(windowController))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.helpText = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.winY = New System.Windows.Forms.TextBox()
        Me.winHeight = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.winX = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.winWidth = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.titlename = New System.Windows.Forms.TextBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.windowname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.Controls.Add(Me.helpText)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(360, 24)
        Me.Panel1.TabIndex = 2
        '
        'helpText
        '
        Me.helpText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.helpText.Location = New System.Drawing.Point(137, 4)
        Me.helpText.Name = "helpText"
        Me.helpText.Padding = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.helpText.Size = New System.Drawing.Size(220, 16)
        Me.helpText.TabIndex = 1
        Me.helpText.Text = "마비노기 도구"
        Me.helpText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Image = Global.버들라이브러리.My.Resources.Resources.close_btn
        Me.Label11.Location = New System.Drawing.Point(364, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(16, 16)
        Me.Label11.TabIndex = 0
        '
        'Label15
        '
        Me.Label15.Image = Global.버들라이브러리.My.Resources.Resources.checkpattern_noborder
        Me.Label15.Location = New System.Drawing.Point(80, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(16, 16)
        Me.Label15.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.Image = Global.버들라이브러리.My.Resources.Resources.checkpattern
        Me.Label14.Location = New System.Drawing.Point(61, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(16, 16)
        Me.Label14.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.Image = Global.버들라이브러리.My.Resources.Resources.max_noborder
        Me.Label13.Location = New System.Drawing.Point(42, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(16, 16)
        Me.Label13.TabIndex = 0
        '
        'Label16
        '
        Me.Label16.Image = Global.버들라이브러리.My.Resources.Resources.max
        Me.Label16.Location = New System.Drawing.Point(23, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(16, 16)
        Me.Label16.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.Image = Global.버들라이브러리.My.Resources.Resources.pined
        Me.Label12.Location = New System.Drawing.Point(118, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(16, 16)
        Me.Label12.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.Image = Global.버들라이브러리.My.Resources.Resources.small
        Me.Label10.Location = New System.Drawing.Point(99, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(16, 16)
        Me.Label10.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Me.Label1.Location = New System.Drawing.Point(4, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 16)
        Me.Label1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.winY)
        Me.GroupBox1.Controls.Add(Me.winHeight)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.winX)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.winWidth)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.titlename)
        Me.GroupBox1.Location = New System.Drawing.Point(1, 219)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(350, 128)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Mabinogi"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(8, 100)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(60, 16)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.Text = "테두리"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(307, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(3)
        Me.Label5.Size = New System.Drawing.Size(37, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "적용"
        '
        'winY
        '
        Me.winY.Location = New System.Drawing.Point(224, 74)
        Me.winY.Name = "winY"
        Me.winY.Size = New System.Drawing.Size(120, 21)
        Me.winY.TabIndex = 15
        '
        'winHeight
        '
        Me.winHeight.Location = New System.Drawing.Point(224, 47)
        Me.winHeight.Name = "winHeight"
        Me.winHeight.Size = New System.Drawing.Size(120, 21)
        Me.winHeight.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(177, 79)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(13, 12)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Y"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(177, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 12)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "창 높이"
        '
        'winX
        '
        Me.winX.Location = New System.Drawing.Point(53, 74)
        Me.winX.Name = "winX"
        Me.winX.Size = New System.Drawing.Size(120, 21)
        Me.winX.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(13, 12)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "X"
        '
        'winWidth
        '
        Me.winWidth.Location = New System.Drawing.Point(53, 47)
        Me.winWidth.Name = "winWidth"
        Me.winWidth.Size = New System.Drawing.Size(120, 21)
        Me.winWidth.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "창 너비"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "창 이름"
        '
        'titlename
        '
        Me.titlename.Location = New System.Drawing.Point(53, 20)
        Me.titlename.Name = "titlename"
        Me.titlename.Size = New System.Drawing.Size(291, 21)
        Me.titlename.TabIndex = 11
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.windowname})
        Me.ListView1.Location = New System.Drawing.Point(1, 4)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(350, 187)
        Me.ListView1.TabIndex = 5
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'windowname
        '
        Me.windowname.Text = "이름"
        Me.windowname.Width = 326
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("굴림", 8.0!)
        Me.Label8.Location = New System.Drawing.Point(1, 194)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(3)
        Me.Label8.Size = New System.Drawing.Size(61, 19)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "목록 갱신"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("굴림", 8.0!)
        Me.Label9.Location = New System.Drawing.Point(256, 194)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(3)
        Me.Label9.Size = New System.Drawing.Size(46, 19)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "활성화"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("굴림", 8.0!)
        Me.Label17.Location = New System.Drawing.Point(192, 194)
        Me.Label17.Name = "Label17"
        Me.Label17.Padding = New System.Windows.Forms.Padding(3)
        Me.Label17.Size = New System.Drawing.Size(61, 19)
        Me.Label17.TabIndex = 4
        Me.Label17.Text = "종료 요청"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(127, 198)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(57, 12)
        Me.Label18.TabIndex = 16
        Me.Label18.Text = "선택 요소"
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("굴림", 8.0!)
        Me.Label19.Location = New System.Drawing.Point(305, 194)
        Me.Label19.Name = "Label19"
        Me.Label19.Padding = New System.Windows.Forms.Padding(3)
        Me.Label19.Size = New System.Drawing.Size(46, 19)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "최소화"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 24)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.Padding = New System.Drawing.Point(0, 0)
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(362, 375)
        Me.TabControl1.TabIndex = 17
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.ListView1)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(354, 349)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "마비노기 창 관리"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.CheckBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(354, 349)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "설정"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(3, 5)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(338, 16)
        Me.CheckBox2.TabIndex = 0
        Me.CheckBox2.Text = "테스트 서버를 사용하고 있습니다. (마비노기 시작에 영향)"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'windowController
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(360, 398)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "windowController"
        Me.Text = "windowController"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents titlename As System.Windows.Forms.TextBox
    Friend WithEvents winWidth As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents winHeight As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents winY As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents winX As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents windowname As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents helpText As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
End Class
