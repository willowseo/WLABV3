<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Assistant
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Assistant))
        Me.Middle = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.WindowExpend = New System.Windows.Forms.Label()
        Me.EXPEND = New System.Windows.Forms.Label()
        Me.EXPEND_NB = New System.Windows.Forms.Label()
        Me.CHECKEDNB = New System.Windows.Forms.Label()
        Me.CHECKED = New System.Windows.Forms.Label()
        Me.NewMabinogi = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.PID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TitleText = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.WEnable = New System.Windows.Forms.Label()
        Me.SetAccept = New System.Windows.Forms.Label()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.WY = New System.Windows.Forms.TextBox()
        Me.WHeight = New System.Windows.Forms.TextBox()
        Me.WX = New System.Windows.Forms.TextBox()
        Me.WWidth = New System.Windows.Forms.TextBox()
        Me.Wtitle = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Selected = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.MabiTestPath = New System.Windows.Forms.Label()
        Me.MabiTestOpenDir = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MabiDir = New System.Windows.Forms.Label()
        Me.OpenMabiDir = New System.Windows.Forms.Label()
        Me.BM = New System.Windows.Forms.Panel()
        Me.TM = New System.Windows.Forms.Panel()
        Me.BR = New System.Windows.Forms.Label()
        Me.BL = New System.Windows.Forms.Label()
        Me.TR = New System.Windows.Forms.Label()
        Me.TL = New System.Windows.Forms.Label()
        Me.ML = New System.Windows.Forms.Panel()
        Me.MR = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Middle.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Middle
        '
        Me.Middle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Middle.Controls.Add(Me.Label1)
        Me.Middle.Controls.Add(Me.WindowExpend)
        Me.Middle.Controls.Add(Me.EXPEND)
        Me.Middle.Controls.Add(Me.EXPEND_NB)
        Me.Middle.Controls.Add(Me.CHECKEDNB)
        Me.Middle.Controls.Add(Me.CHECKED)
        Me.Middle.Controls.Add(Me.NewMabinogi)
        Me.Middle.Location = New System.Drawing.Point(5, 5)
        Me.Middle.Name = "Middle"
        Me.Middle.Size = New System.Drawing.Size(350, 26)
        Me.Middle.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(0, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(350, 2)
        Me.Label1.TabIndex = 2
        '
        'WindowExpend
        '
        Me.WindowExpend.Image = Global.버들라이브러리.My.Resources.Resources.window_big
        Me.WindowExpend.Location = New System.Drawing.Point(328, 2)
        Me.WindowExpend.Name = "WindowExpend"
        Me.WindowExpend.Size = New System.Drawing.Size(20, 20)
        Me.WindowExpend.TabIndex = 1
        '
        'EXPEND
        '
        Me.EXPEND.Image = Global.버들라이브러리.My.Resources.Resources.mabi_expend
        Me.EXPEND.Location = New System.Drawing.Point(24, 2)
        Me.EXPEND.Name = "EXPEND"
        Me.EXPEND.Size = New System.Drawing.Size(20, 20)
        Me.EXPEND.TabIndex = 0
        '
        'EXPEND_NB
        '
        Me.EXPEND_NB.Image = Global.버들라이브러리.My.Resources.Resources.mabi_expend_noborder
        Me.EXPEND_NB.Location = New System.Drawing.Point(46, 2)
        Me.EXPEND_NB.Name = "EXPEND_NB"
        Me.EXPEND_NB.Size = New System.Drawing.Size(20, 20)
        Me.EXPEND_NB.TabIndex = 0
        '
        'CHECKEDNB
        '
        Me.CHECKEDNB.Image = Global.버들라이브러리.My.Resources.Resources.mabi_Checked_nb
        Me.CHECKEDNB.Location = New System.Drawing.Point(90, 2)
        Me.CHECKEDNB.Name = "CHECKEDNB"
        Me.CHECKEDNB.Size = New System.Drawing.Size(20, 20)
        Me.CHECKEDNB.TabIndex = 0
        '
        'CHECKED
        '
        Me.CHECKED.Image = Global.버들라이브러리.My.Resources.Resources.mabi_Checked
        Me.CHECKED.Location = New System.Drawing.Point(68, 2)
        Me.CHECKED.Name = "CHECKED"
        Me.CHECKED.Size = New System.Drawing.Size(20, 20)
        Me.CHECKED.TabIndex = 0
        '
        'NewMabinogi
        '
        Me.NewMabinogi.Image = Global.버들라이브러리.My.Resources.Resources.Mabinogi_New
        Me.NewMabinogi.Location = New System.Drawing.Point(2, 2)
        Me.NewMabinogi.Name = "NewMabinogi"
        Me.NewMabinogi.Size = New System.Drawing.Size(20, 20)
        Me.NewMabinogi.TabIndex = 0
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.PID, Me.TitleText})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(342, 309)
        Me.ListView1.TabIndex = 3
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'PID
        '
        Me.PID.Text = "PID"
        '
        'TitleText
        '
        Me.TitleText.Text = "창 이름"
        Me.TitleText.Width = 262
        '
        'Timer1
        '
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(5, 31)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(350, 464)
        Me.TabControl1.TabIndex = 6
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.WEnable)
        Me.TabPage1.Controls.Add(Me.SetAccept)
        Me.TabPage1.Controls.Add(Me.CheckBox2)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.WY)
        Me.TabPage1.Controls.Add(Me.WHeight)
        Me.TabPage1.Controls.Add(Me.WX)
        Me.TabPage1.Controls.Add(Me.WWidth)
        Me.TabPage1.Controls.Add(Me.Wtitle)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Selected)
        Me.TabPage1.Controls.Add(Me.ListView1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(342, 438)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "마비노기 창 관리자"
        '
        'WEnable
        '
        Me.WEnable.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.WEnable.Font = New System.Drawing.Font("굴림", 8.0!)
        Me.WEnable.Image = Global.버들라이브러리.My.Resources.Resources.Mabinogi_119
        Me.WEnable.Location = New System.Drawing.Point(205, 416)
        Me.WEnable.Name = "WEnable"
        Me.WEnable.Size = New System.Drawing.Size(64, 19)
        Me.WEnable.TabIndex = 10
        Me.WEnable.Text = "창 활성화"
        Me.WEnable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SetAccept
        '
        Me.SetAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SetAccept.Font = New System.Drawing.Font("굴림", 8.0!)
        Me.SetAccept.Image = Global.버들라이브러리.My.Resources.Resources.Mabinogi_119
        Me.SetAccept.Location = New System.Drawing.Point(275, 416)
        Me.SetAccept.Name = "SetAccept"
        Me.SetAccept.Size = New System.Drawing.Size(64, 19)
        Me.SetAccept.TabIndex = 10
        Me.SetAccept.Text = "설정 적용"
        Me.SetAccept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(6, 416)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(156, 16)
        Me.CheckBox2.TabIndex = 9
        Me.CheckBox2.Text = "테두리를 활성화 합니다."
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(158, 394)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 12)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "세로 위치 (Y)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 394)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 12)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "창 높이"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(158, 367)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "가로 위치 (X)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 367)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 12)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "창 너비"
        '
        'WY
        '
        Me.WY.Location = New System.Drawing.Point(240, 389)
        Me.WY.Name = "WY"
        Me.WY.Size = New System.Drawing.Size(99, 21)
        Me.WY.TabIndex = 7
        '
        'WHeight
        '
        Me.WHeight.Location = New System.Drawing.Point(55, 389)
        Me.WHeight.Name = "WHeight"
        Me.WHeight.Size = New System.Drawing.Size(99, 21)
        Me.WHeight.TabIndex = 7
        '
        'WX
        '
        Me.WX.Location = New System.Drawing.Point(240, 362)
        Me.WX.Name = "WX"
        Me.WX.Size = New System.Drawing.Size(99, 21)
        Me.WX.TabIndex = 7
        '
        'WWidth
        '
        Me.WWidth.Location = New System.Drawing.Point(55, 362)
        Me.WWidth.Name = "WWidth"
        Me.WWidth.Size = New System.Drawing.Size(99, 21)
        Me.WWidth.TabIndex = 7
        '
        'Wtitle
        '
        Me.Wtitle.Location = New System.Drawing.Point(55, 335)
        Me.Wtitle.Name = "Wtitle"
        Me.Wtitle.Size = New System.Drawing.Size(284, 21)
        Me.Wtitle.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 340)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "창 이름"
        '
        'Selected
        '
        Me.Selected.BackColor = System.Drawing.Color.Gainsboro
        Me.Selected.Location = New System.Drawing.Point(-1, 312)
        Me.Selected.Name = "Selected"
        Me.Selected.Size = New System.Drawing.Size(342, 19)
        Me.Selected.TabIndex = 4
        Me.Selected.Text = "선택된 마비노기가 없습니다."
        Me.Selected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.White
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.CheckBox1)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(342, 438)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "설정"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Button5)
        Me.GroupBox4.Controls.Add(Me.Button4)
        Me.GroupBox4.Controls.Add(Me.Button3)
        Me.GroupBox4.Controls.Add(Me.Button2)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 201)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(330, 147)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "창 위치 설정"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(230, 119)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(94, 22)
        Me.Button5.TabIndex = 0
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(230, 20)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(94, 22)
        Me.Button4.TabIndex = 0
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(6, 119)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(94, 22)
        Me.Button3.TabIndex = 0
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(6, 20)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(94, 22)
        Me.Button2.TabIndex = 0
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TrackBar1)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 354)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(330, 52)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "이 창의 투명도 설정"
        '
        'TrackBar1
        '
        Me.TrackBar1.AutoSize = False
        Me.TrackBar1.Location = New System.Drawing.Point(6, 20)
        Me.TrackBar1.Maximum = 75
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(318, 26)
        Me.TrackBar1.TabIndex = 5
        Me.TrackBar1.TickFrequency = 5
        Me.TrackBar1.Value = 75
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(0, 412)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(340, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Willows Lab. 종료"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(6, 106)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(156, 16)
        Me.CheckBox1.TabIndex = 3
        Me.CheckBox1.Text = "마비노기 종료 광고 제거"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.MabiTestPath)
        Me.GroupBox2.Controls.Add(Me.MabiTestOpenDir)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 56)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(340, 44)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "마비노기(테스트) 설치경로"
        '
        'MabiTestPath
        '
        Me.MabiTestPath.BackColor = System.Drawing.Color.Transparent
        Me.MabiTestPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MabiTestPath.Location = New System.Drawing.Point(6, 17)
        Me.MabiTestPath.Name = "MabiTestPath"
        Me.MabiTestPath.Size = New System.Drawing.Size(304, 20)
        Me.MabiTestPath.TabIndex = 0
        Me.MabiTestPath.Text = "Mabinogi (Test) Path"
        Me.MabiTestPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MabiTestOpenDir
        '
        Me.MabiTestOpenDir.Image = Global.버들라이브러리.My.Resources.Resources.openDir
        Me.MabiTestOpenDir.Location = New System.Drawing.Point(314, 17)
        Me.MabiTestOpenDir.Name = "MabiTestOpenDir"
        Me.MabiTestOpenDir.Size = New System.Drawing.Size(20, 20)
        Me.MabiTestOpenDir.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MabiDir)
        Me.GroupBox1.Controls.Add(Me.OpenMabiDir)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(340, 44)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "마비노기 설치경로"
        '
        'MabiDir
        '
        Me.MabiDir.BackColor = System.Drawing.Color.Transparent
        Me.MabiDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MabiDir.Location = New System.Drawing.Point(6, 17)
        Me.MabiDir.Name = "MabiDir"
        Me.MabiDir.Size = New System.Drawing.Size(304, 20)
        Me.MabiDir.TabIndex = 0
        Me.MabiDir.Text = "Mabinogi Path"
        Me.MabiDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'OpenMabiDir
        '
        Me.OpenMabiDir.Image = Global.버들라이브러리.My.Resources.Resources.openDir
        Me.OpenMabiDir.Location = New System.Drawing.Point(314, 17)
        Me.OpenMabiDir.Name = "OpenMabiDir"
        Me.OpenMabiDir.Size = New System.Drawing.Size(20, 20)
        Me.OpenMabiDir.TabIndex = 1
        '
        'BM
        '
        Me.BM.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BM.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.bm
        Me.BM.Location = New System.Drawing.Point(5, 495)
        Me.BM.Name = "BM"
        Me.BM.Size = New System.Drawing.Size(350, 5)
        Me.BM.TabIndex = 4
        '
        'TM
        '
        Me.TM.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TM.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.tm
        Me.TM.Location = New System.Drawing.Point(5, 0)
        Me.TM.Name = "TM"
        Me.TM.Size = New System.Drawing.Size(350, 5)
        Me.TM.TabIndex = 3
        '
        'BR
        '
        Me.BR.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BR.Image = Global.버들라이브러리.My.Resources.Resources.br
        Me.BR.Location = New System.Drawing.Point(355, 495)
        Me.BR.Margin = New System.Windows.Forms.Padding(0)
        Me.BR.Name = "BR"
        Me.BR.Size = New System.Drawing.Size(5, 5)
        Me.BR.TabIndex = 0
        '
        'BL
        '
        Me.BL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BL.Image = Global.버들라이브러리.My.Resources.Resources.bl
        Me.BL.Location = New System.Drawing.Point(0, 495)
        Me.BL.Margin = New System.Windows.Forms.Padding(0)
        Me.BL.Name = "BL"
        Me.BL.Size = New System.Drawing.Size(5, 5)
        Me.BL.TabIndex = 0
        '
        'TR
        '
        Me.TR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TR.Image = Global.버들라이브러리.My.Resources.Resources.tr
        Me.TR.Location = New System.Drawing.Point(355, 0)
        Me.TR.Margin = New System.Windows.Forms.Padding(0)
        Me.TR.Name = "TR"
        Me.TR.Size = New System.Drawing.Size(5, 5)
        Me.TR.TabIndex = 0
        '
        'TL
        '
        Me.TL.Image = Global.버들라이브러리.My.Resources.Resources.tl
        Me.TL.Location = New System.Drawing.Point(0, 0)
        Me.TL.Margin = New System.Windows.Forms.Padding(0)
        Me.TL.Name = "TL"
        Me.TL.Size = New System.Drawing.Size(5, 5)
        Me.TL.TabIndex = 0
        '
        'ML
        '
        Me.ML.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ML.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.ml
        Me.ML.Location = New System.Drawing.Point(0, 5)
        Me.ML.Name = "ML"
        Me.ML.Size = New System.Drawing.Size(5, 490)
        Me.ML.TabIndex = 1
        '
        'MR
        '
        Me.MR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MR.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.mr
        Me.MR.Location = New System.Drawing.Point(355, 5)
        Me.MR.Name = "MR"
        Me.MR.Size = New System.Drawing.Size(5, 490)
        Me.MR.TabIndex = 2
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'Assistant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(360, 500)
        Me.Controls.Add(Me.Middle)
        Me.Controls.Add(Me.BM)
        Me.Controls.Add(Me.TM)
        Me.Controls.Add(Me.BR)
        Me.Controls.Add(Me.BL)
        Me.Controls.Add(Me.TR)
        Me.Controls.Add(Me.TL)
        Me.Controls.Add(Me.ML)
        Me.Controls.Add(Me.MR)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Assistant"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mabinogi Window Controller"
        Me.TopMost = True
        Me.Middle.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TL As System.Windows.Forms.Label
    Friend WithEvents TR As System.Windows.Forms.Label
    Friend WithEvents BL As System.Windows.Forms.Label
    Friend WithEvents BR As System.Windows.Forms.Label
    Friend WithEvents ML As System.Windows.Forms.Panel
    Friend WithEvents MR As System.Windows.Forms.Panel
    Friend WithEvents TM As System.Windows.Forms.Panel
    Friend WithEvents BM As System.Windows.Forms.Panel
    Friend WithEvents Middle As System.Windows.Forms.Panel
    Friend WithEvents NewMabinogi As System.Windows.Forms.Label
    Friend WithEvents WindowExpend As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents EXPEND As System.Windows.Forms.Label
    Friend WithEvents EXPEND_NB As System.Windows.Forms.Label
    Friend WithEvents CHECKED As System.Windows.Forms.Label
    Friend WithEvents CHECKEDNB As System.Windows.Forms.Label
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents PID As System.Windows.Forms.ColumnHeader
    Friend WithEvents TitleText As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Selected As System.Windows.Forms.Label
    Friend WithEvents OpenMabiDir As System.Windows.Forms.Label
    Friend WithEvents MabiDir As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents MabiTestPath As System.Windows.Forms.Label
    Friend WithEvents MabiTestOpenDir As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Wtitle As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents WHeight As System.Windows.Forms.TextBox
    Friend WithEvents WWidth As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents WY As System.Windows.Forms.TextBox
    Friend WithEvents WX As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents SetAccept As System.Windows.Forms.Label
    Friend WithEvents WEnable As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
