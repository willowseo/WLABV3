<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class vxFrmLiteMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(vxFrmLiteMain))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.버들팩LiteModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.버전변경ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.리스트다시불러오기ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.마비노기공지보기ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.프로그램정보ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.list_default = New System.Windows.Forms.ListView()
        Me.col_01 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.list_Service = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.list_Select = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.list_View = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.list_Simpling = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.list_field = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.list_decoration = New System.Windows.Forms.ListView()
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.wlabID = New System.Windows.Forms.TextBox()
        Me.wlabPW = New System.Windows.Forms.TextBox()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.버들팩LiteModeToolStripMenuItem, Me.리스트다시불러오기ToolStripMenuItem, Me.마비노기공지보기ToolStripMenuItem, Me.프로그램정보ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(557, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '버들팩LiteModeToolStripMenuItem
        '
        Me.버들팩LiteModeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.버전변경ToolStripMenuItem})
        Me.버들팩LiteModeToolStripMenuItem.Name = "버들팩LiteModeToolStripMenuItem"
        Me.버들팩LiteModeToolStripMenuItem.Size = New System.Drawing.Size(162, 20)
        Me.버들팩LiteModeToolStripMenuItem.Text = "버들팩 Lite Mode 실행중..."
        '
        '버전변경ToolStripMenuItem
        '
        Me.버전변경ToolStripMenuItem.Name = "버전변경ToolStripMenuItem"
        Me.버전변경ToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.버전변경ToolStripMenuItem.Text = "버전 변경"
        '
        '리스트다시불러오기ToolStripMenuItem
        '
        Me.리스트다시불러오기ToolStripMenuItem.Name = "리스트다시불러오기ToolStripMenuItem"
        Me.리스트다시불러오기ToolStripMenuItem.Size = New System.Drawing.Size(135, 20)
        Me.리스트다시불러오기ToolStripMenuItem.Text = "리스트 다시 불러오기"
        '
        '마비노기공지보기ToolStripMenuItem
        '
        Me.마비노기공지보기ToolStripMenuItem.Name = "마비노기공지보기ToolStripMenuItem"
        Me.마비노기공지보기ToolStripMenuItem.Size = New System.Drawing.Size(119, 20)
        Me.마비노기공지보기ToolStripMenuItem.Text = "마비노기 공지보기"
        '
        '프로그램정보ToolStripMenuItem
        '
        Me.프로그램정보ToolStripMenuItem.Name = "프로그램정보ToolStripMenuItem"
        Me.프로그램정보ToolStripMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.프로그램정보ToolStripMenuItem.Text = "프로그램 정보"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.wlabPW)
        Me.GroupBox1.Controls.Add(Me.wlabID)
        Me.GroupBox1.Controls.Add(Me.Button5)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(156, 124)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 158)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(156, 78)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Button3.Location = New System.Drawing.Point(82, 18)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(68, 23)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "선택 삭제"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Button2.Location = New System.Drawing.Point(6, 47)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(144, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "마비노기 실행"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Button1.Location = New System.Drawing.Point(6, 18)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(70, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "선택 다운"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TextBox2)
        Me.GroupBox3.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.DodgerBlue
        Me.GroupBox3.Location = New System.Drawing.Point(11, 242)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(156, 201)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "공지사항"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.MintCream
        Me.TextBox2.Location = New System.Drawing.Point(6, 22)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox2.Size = New System.Drawing.Size(144, 173)
        Me.TextBox2.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TextBox1)
        Me.GroupBox4.Location = New System.Drawing.Point(180, 28)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(314, 46)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.TextBox1.Location = New System.Drawing.Point(6, 15)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(302, 23)
        Me.TextBox1.TabIndex = 4
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(180, 87)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.Padding = New System.Drawing.Point(0, 0)
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(380, 370)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.AliceBlue
        Me.TabPage1.Controls.Add(Me.list_default)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(372, 342)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "기본"
        '
        'list_default
        '
        Me.list_default.BackColor = System.Drawing.Color.MintCream
        Me.list_default.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.list_default.CheckBoxes = True
        Me.list_default.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.col_01})
        Me.list_default.Dock = System.Windows.Forms.DockStyle.Fill
        Me.list_default.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.list_default.Location = New System.Drawing.Point(0, 0)
        Me.list_default.Name = "list_default"
        Me.list_default.Size = New System.Drawing.Size(372, 342)
        Me.list_default.TabIndex = 0
        Me.list_default.UseCompatibleStateImageBehavior = False
        Me.list_default.View = System.Windows.Forms.View.Details
        '
        'col_01
        '
        Me.col_01.Text = "List"
        Me.col_01.Width = 344
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.list_Service)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(372, 342)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "편의"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'list_Service
        '
        Me.list_Service.BackColor = System.Drawing.Color.MintCream
        Me.list_Service.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.list_Service.CheckBoxes = True
        Me.list_Service.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.list_Service.Dock = System.Windows.Forms.DockStyle.Fill
        Me.list_Service.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.list_Service.Location = New System.Drawing.Point(0, 0)
        Me.list_Service.Name = "list_Service"
        Me.list_Service.Size = New System.Drawing.Size(372, 342)
        Me.list_Service.TabIndex = 1
        Me.list_Service.UseCompatibleStateImageBehavior = False
        Me.list_Service.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "List"
        Me.ColumnHeader1.Width = 344
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.list_Select)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(372, 342)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "선택"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'list_Select
        '
        Me.list_Select.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.list_Select.BackColor = System.Drawing.Color.MintCream
        Me.list_Select.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.list_Select.CheckBoxes = True
        Me.list_Select.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.list_Select.Dock = System.Windows.Forms.DockStyle.Fill
        Me.list_Select.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.list_Select.HoverSelection = True
        Me.list_Select.Location = New System.Drawing.Point(0, 0)
        Me.list_Select.Name = "list_Select"
        Me.list_Select.Size = New System.Drawing.Size(372, 342)
        Me.list_Select.TabIndex = 1
        Me.list_Select.UseCompatibleStateImageBehavior = False
        Me.list_Select.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "List"
        Me.ColumnHeader2.Width = 344
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.list_View)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(372, 342)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "시야"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'list_View
        '
        Me.list_View.BackColor = System.Drawing.Color.MintCream
        Me.list_View.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.list_View.CheckBoxes = True
        Me.list_View.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3})
        Me.list_View.Dock = System.Windows.Forms.DockStyle.Fill
        Me.list_View.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.list_View.Location = New System.Drawing.Point(0, 0)
        Me.list_View.Name = "list_View"
        Me.list_View.Size = New System.Drawing.Size(372, 342)
        Me.list_View.TabIndex = 1
        Me.list_View.UseCompatibleStateImageBehavior = False
        Me.list_View.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "List"
        Me.ColumnHeader3.Width = 344
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.list_Simpling)
        Me.TabPage5.Location = New System.Drawing.Point(4, 24)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TabPage5.Size = New System.Drawing.Size(372, 342)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "간소화"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'list_Simpling
        '
        Me.list_Simpling.BackColor = System.Drawing.Color.MintCream
        Me.list_Simpling.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.list_Simpling.CheckBoxes = True
        Me.list_Simpling.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4})
        Me.list_Simpling.Dock = System.Windows.Forms.DockStyle.Fill
        Me.list_Simpling.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.list_Simpling.Location = New System.Drawing.Point(0, 0)
        Me.list_Simpling.Name = "list_Simpling"
        Me.list_Simpling.Size = New System.Drawing.Size(372, 342)
        Me.list_Simpling.TabIndex = 1
        Me.list_Simpling.UseCompatibleStateImageBehavior = False
        Me.list_Simpling.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "List"
        Me.ColumnHeader4.Width = 344
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.list_field)
        Me.TabPage6.Location = New System.Drawing.Point(4, 24)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(372, 342)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "필드"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'list_field
        '
        Me.list_field.BackColor = System.Drawing.Color.MintCream
        Me.list_field.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.list_field.CheckBoxes = True
        Me.list_field.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5})
        Me.list_field.Dock = System.Windows.Forms.DockStyle.Fill
        Me.list_field.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.list_field.Location = New System.Drawing.Point(0, 0)
        Me.list_field.Name = "list_field"
        Me.list_field.Size = New System.Drawing.Size(372, 342)
        Me.list_field.TabIndex = 1
        Me.list_field.UseCompatibleStateImageBehavior = False
        Me.list_field.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "List"
        Me.ColumnHeader5.Width = 344
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.list_decoration)
        Me.TabPage7.Location = New System.Drawing.Point(4, 24)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(372, 342)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "꾸미기"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'list_decoration
        '
        Me.list_decoration.BackColor = System.Drawing.Color.MintCream
        Me.list_decoration.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.list_decoration.CheckBoxes = True
        Me.list_decoration.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6})
        Me.list_decoration.Dock = System.Windows.Forms.DockStyle.Fill
        Me.list_decoration.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.list_decoration.Location = New System.Drawing.Point(0, 0)
        Me.list_decoration.Name = "list_decoration"
        Me.list_decoration.Size = New System.Drawing.Size(372, 342)
        Me.list_decoration.TabIndex = 1
        Me.list_decoration.UseCompatibleStateImageBehavior = False
        Me.list_decoration.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "List"
        Me.ColumnHeader6.Width = 344
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(502, 35)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(44, 44)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.MintCream
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.CheckBoxes = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7})
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView1.Location = New System.Drawing.Point(287, 265)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(165, 112)
        Me.ListView1.TabIndex = 6
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "List"
        Me.ColumnHeader7.Width = 344
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(7, 13)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(143, 24)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "버들팩 로그인"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(7, 41)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(143, 24)
        Me.Button5.TabIndex = 0
        Me.Button5.Text = "루미팩 삭제"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'wlabID
        '
        Me.wlabID.Location = New System.Drawing.Point(7, 70)
        Me.wlabID.Name = "wlabID"
        Me.wlabID.Size = New System.Drawing.Size(143, 21)
        Me.wlabID.TabIndex = 1
        Me.wlabID.Visible = False
        '
        'wlabPW
        '
        Me.wlabPW.Location = New System.Drawing.Point(7, 97)
        Me.wlabPW.Name = "wlabPW"
        Me.wlabPW.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.wlabPW.Size = New System.Drawing.Size(143, 21)
        Me.wlabPW.TabIndex = 1
        Me.wlabPW.Visible = False
        '
        'FrmLiteMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.MintCream
        Me.ClientSize = New System.Drawing.Size(557, 455)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.ListView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximumSize = New System.Drawing.Size(573, 494)
        Me.MinimumSize = New System.Drawing.Size(573, 494)
        Me.Name = "FrmLiteMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LumiDeck Pro"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 버들팩LiteModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents list_default As System.Windows.Forms.ListView
    Friend WithEvents col_01 As System.Windows.Forms.ColumnHeader
    Friend WithEvents list_Service As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents list_Select As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents list_View As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents list_Simpling As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents list_field As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents list_decoration As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents 리스트다시불러오기ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 마비노기공지보기ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 프로그램정보ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 버전변경ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents wlabPW As System.Windows.Forms.TextBox
    Friend WithEvents wlabID As System.Windows.Forms.TextBox
End Class
