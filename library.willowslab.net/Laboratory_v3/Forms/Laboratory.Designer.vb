<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Laboratory
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
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Laboratory))
        Me.x_MainCont = New System.Windows.Forms.Panel()
        Me.T_tab01 = New System.Windows.Forms.Label()
        Me.T_tab00 = New System.Windows.Forms.Label()
        Me.x_TabHeader = New System.Windows.Forms.Panel()
        Me.x_OuterCont = New System.Windows.Forms.Panel()
        Me.x_InnerCont = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.z_itemInfo = New System.Windows.Forms.Panel()
        Me.z_InfoInner = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.z_pattern_cont = New System.Windows.Forms.Panel()
        Me.z_InfoLabel = New System.Windows.Forms.Label()
        Me.z_listCont = New System.Windows.Forms.Panel()
        Me.F_listview = New System.Windows.Forms.ListView()
        Me.z_chead2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.z_chead1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.z_chead3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.z_chlabel3 = New System.Windows.Forms.Label()
        Me.z_chlabel1 = New System.Windows.Forms.Label()
        Me.z_chlabel2 = New System.Windows.Forms.Label()
        Me.x_TitleCont = New System.Windows.Forms.Panel()
        Me.w_IconPicBox = New System.Windows.Forms.PictureBox()
        Me.w_TitleLabel = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.x_MainCont.SuspendLayout()
        Me.x_OuterCont.SuspendLayout()
        Me.x_InnerCont.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.z_itemInfo.SuspendLayout()
        Me.z_InfoInner.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.z_listCont.SuspendLayout()
        Me.x_TitleCont.SuspendLayout()
        CType(Me.w_IconPicBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'x_MainCont
        '
        Me.x_MainCont.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.x_MainCont.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.x_MainCont.Controls.Add(Me.T_tab01)
        Me.x_MainCont.Controls.Add(Me.T_tab00)
        Me.x_MainCont.Controls.Add(Me.x_TabHeader)
        Me.x_MainCont.Controls.Add(Me.x_OuterCont)
        Me.x_MainCont.Controls.Add(Me.x_TitleCont)
        Me.x_MainCont.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.x_MainCont.Location = New System.Drawing.Point(1, 1)
        Me.x_MainCont.Name = "x_MainCont"
        Me.x_MainCont.Size = New System.Drawing.Size(918, 598)
        Me.x_MainCont.TabIndex = 0
        '
        'T_tab01
        '
        Me.T_tab01.AutoSize = True
        Me.T_tab01.BackColor = System.Drawing.Color.Transparent
        Me.T_tab01.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.T_tab01.Location = New System.Drawing.Point(169, 34)
        Me.T_tab01.Margin = New System.Windows.Forms.Padding(0)
        Me.T_tab01.Name = "T_tab01"
        Me.T_tab01.Padding = New System.Windows.Forms.Padding(2)
        Me.T_tab01.Size = New System.Drawing.Size(101, 19)
        Me.T_tab01.TabIndex = 3
        Me.T_tab01.Text = "ItemDB Searcher"
        '
        'T_tab00
        '
        Me.T_tab00.AutoSize = True
        Me.T_tab00.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.T_tab00.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.T_tab00.Location = New System.Drawing.Point(6, 34)
        Me.T_tab00.Margin = New System.Windows.Forms.Padding(0)
        Me.T_tab00.Name = "T_tab00"
        Me.T_tab00.Padding = New System.Windows.Forms.Padding(2)
        Me.T_tab00.Size = New System.Drawing.Size(163, 19)
        Me.T_tab00.TabIndex = 3
        Me.T_tab00.Text = "Laboratory - Experiment List"
        '
        'x_TabHeader
        '
        Me.x_TabHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.x_TabHeader.Location = New System.Drawing.Point(6, 53)
        Me.x_TabHeader.Name = "x_TabHeader"
        Me.x_TabHeader.Size = New System.Drawing.Size(906, 2)
        Me.x_TabHeader.TabIndex = 2
        '
        'x_OuterCont
        '
        Me.x_OuterCont.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.x_OuterCont.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.x_OuterCont.Controls.Add(Me.x_InnerCont)
        Me.x_OuterCont.Location = New System.Drawing.Point(6, 55)
        Me.x_OuterCont.Name = "x_OuterCont"
        Me.x_OuterCont.Size = New System.Drawing.Size(906, 536)
        Me.x_OuterCont.TabIndex = 1
        '
        'x_InnerCont
        '
        Me.x_InnerCont.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.x_InnerCont.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.x_InnerCont.Controls.Add(Me.Label3)
        Me.x_InnerCont.Controls.Add(Me.Label2)
        Me.x_InnerCont.Controls.Add(Me.Panel2)
        Me.x_InnerCont.Controls.Add(Me.z_itemInfo)
        Me.x_InnerCont.Controls.Add(Me.z_listCont)
        Me.x_InnerCont.Location = New System.Drawing.Point(1, 1)
        Me.x_InnerCont.Name = "x_InnerCont"
        Me.x_InnerCont.Size = New System.Drawing.Size(904, 534)
        Me.x_InnerCont.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(743, 295)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(156, 39)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "선택 문서를 실행합니다."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(583, 295)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(156, 39)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "실험을 진행합니다."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(5, 340)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(895, 189)
        Me.Panel2.TabIndex = 3
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Panel3.Controls.Add(Me.RichTextBox1)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Location = New System.Drawing.Point(1, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(893, 187)
        Me.Panel3.TabIndex = 0
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Font = New System.Drawing.Font("굴림", 9.0!)
        Me.RichTextBox1.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 45)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.RichTextBox1.Size = New System.Drawing.Size(893, 142)
        Me.RichTextBox1.TabIndex = 2
        Me.RichTextBox1.Text = ""
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(3, 21)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 21)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "출력 창 비우기"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.pattern
        Me.Panel4.Location = New System.Drawing.Point(36, 9)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(852, 5)
        Me.Panel4.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "출력"
        '
        'z_itemInfo
        '
        Me.z_itemInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.z_itemInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.z_itemInfo.Controls.Add(Me.z_InfoInner)
        Me.z_itemInfo.Location = New System.Drawing.Point(583, 6)
        Me.z_itemInfo.Name = "z_itemInfo"
        Me.z_itemInfo.Size = New System.Drawing.Size(317, 286)
        Me.z_itemInfo.TabIndex = 3
        '
        'z_InfoInner
        '
        Me.z_InfoInner.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.z_InfoInner.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.z_InfoInner.Controls.Add(Me.DataGridView1)
        Me.z_InfoInner.Controls.Add(Me.z_pattern_cont)
        Me.z_InfoInner.Controls.Add(Me.z_InfoLabel)
        Me.z_InfoInner.Location = New System.Drawing.Point(1, 1)
        Me.z_InfoInner.Name = "z_InfoInner"
        Me.z_InfoInner.Size = New System.Drawing.Size(315, 284)
        Me.z_InfoInner.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle22.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        DataGridViewCellStyle22.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        DataGridViewCellStyle22.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle22
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.ColumnHeadersVisible = False
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle23.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        DataGridViewCellStyle23.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        DataGridViewCellStyle23.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle23
        Me.DataGridView1.GridColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DataGridView1.Location = New System.Drawing.Point(0, 21)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle24.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        DataGridViewCellStyle24.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        DataGridViewCellStyle24.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        DataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle24
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 15
        Me.DataGridView1.RowTemplate.Height = 19
        Me.DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataGridView1.Size = New System.Drawing.Size(315, 263)
        Me.DataGridView1.TabIndex = 2
        '
        'Column1
        '
        Me.Column1.FillWeight = 148.0!
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 159
        '
        'Column2
        '
        Me.Column2.FillWeight = 145.0!
        Me.Column2.HeaderText = "Column2"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 155
        '
        'z_pattern_cont
        '
        Me.z_pattern_cont.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.pattern
        Me.z_pattern_cont.Location = New System.Drawing.Point(36, 9)
        Me.z_pattern_cont.Name = "z_pattern_cont"
        Me.z_pattern_cont.Size = New System.Drawing.Size(274, 5)
        Me.z_pattern_cont.TabIndex = 1
        '
        'z_InfoLabel
        '
        Me.z_InfoLabel.AutoSize = True
        Me.z_InfoLabel.Location = New System.Drawing.Point(3, 4)
        Me.z_InfoLabel.Name = "z_InfoLabel"
        Me.z_InfoLabel.Size = New System.Drawing.Size(31, 15)
        Me.z_InfoLabel.TabIndex = 0
        Me.z_InfoLabel.Text = "속성"
        '
        'z_listCont
        '
        Me.z_listCont.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.z_listCont.Controls.Add(Me.F_listview)
        Me.z_listCont.Controls.Add(Me.z_chlabel3)
        Me.z_listCont.Controls.Add(Me.z_chlabel1)
        Me.z_listCont.Controls.Add(Me.z_chlabel2)
        Me.z_listCont.Location = New System.Drawing.Point(5, 6)
        Me.z_listCont.Name = "z_listCont"
        Me.z_listCont.Size = New System.Drawing.Size(573, 329)
        Me.z_listCont.TabIndex = 2
        '
        'F_listview
        '
        Me.F_listview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.F_listview.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.F_listview.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.F_listview.CheckBoxes = True
        Me.F_listview.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.z_chead2, Me.z_chead1, Me.z_chead3})
        Me.F_listview.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.F_listview.FullRowSelect = True
        Me.F_listview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.F_listview.Location = New System.Drawing.Point(1, 21)
        Me.F_listview.Name = "F_listview"
        Me.F_listview.Size = New System.Drawing.Size(571, 307)
        Me.F_listview.TabIndex = 0
        Me.F_listview.UseCompatibleStateImageBehavior = False
        Me.F_listview.View = System.Windows.Forms.View.Details
        '
        'z_chead2
        '
        Me.z_chead2.DisplayIndex = 1
        Me.z_chead2.Text = "title"
        Me.z_chead2.Width = 368
        '
        'z_chead1
        '
        Me.z_chead1.DisplayIndex = 0
        Me.z_chead1.Text = "type"
        Me.z_chead1.Width = 65
        '
        'z_chead3
        '
        Me.z_chead3.Text = "install date"
        Me.z_chead3.Width = 110
        '
        'z_chlabel3
        '
        Me.z_chlabel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.z_chlabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.z_chlabel3.Location = New System.Drawing.Point(436, 1)
        Me.z_chlabel3.Margin = New System.Windows.Forms.Padding(0)
        Me.z_chlabel3.Name = "z_chlabel3"
        Me.z_chlabel3.Padding = New System.Windows.Forms.Padding(2)
        Me.z_chlabel3.Size = New System.Drawing.Size(136, 19)
        Me.z_chlabel3.TabIndex = 1
        Me.z_chlabel3.Text = "설치일"
        '
        'z_chlabel1
        '
        Me.z_chlabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.z_chlabel1.Location = New System.Drawing.Point(1, 1)
        Me.z_chlabel1.Margin = New System.Windows.Forms.Padding(0)
        Me.z_chlabel1.Name = "z_chlabel1"
        Me.z_chlabel1.Padding = New System.Windows.Forms.Padding(2)
        Me.z_chlabel1.Size = New System.Drawing.Size(65, 19)
        Me.z_chlabel1.TabIndex = 1
        Me.z_chlabel1.Text = "분류"
        '
        'z_chlabel2
        '
        Me.z_chlabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.z_chlabel2.Location = New System.Drawing.Point(67, 1)
        Me.z_chlabel2.Margin = New System.Windows.Forms.Padding(0)
        Me.z_chlabel2.Name = "z_chlabel2"
        Me.z_chlabel2.Padding = New System.Windows.Forms.Padding(2)
        Me.z_chlabel2.Size = New System.Drawing.Size(368, 19)
        Me.z_chlabel2.TabIndex = 1
        Me.z_chlabel2.Text = "　실험 문서 제목"
        '
        'x_TitleCont
        '
        Me.x_TitleCont.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.x_TitleCont.Controls.Add(Me.w_IconPicBox)
        Me.x_TitleCont.Controls.Add(Me.w_TitleLabel)
        Me.x_TitleCont.Location = New System.Drawing.Point(0, 0)
        Me.x_TitleCont.Name = "x_TitleCont"
        Me.x_TitleCont.Size = New System.Drawing.Size(918, 30)
        Me.x_TitleCont.TabIndex = 0
        '
        'w_IconPicBox
        '
        Me.w_IconPicBox.Image = Global.버들라이브러리.My.Resources.Resources.ico_png
        Me.w_IconPicBox.Location = New System.Drawing.Point(7, 7)
        Me.w_IconPicBox.Name = "w_IconPicBox"
        Me.w_IconPicBox.Size = New System.Drawing.Size(16, 16)
        Me.w_IconPicBox.TabIndex = 0
        Me.w_IconPicBox.TabStop = False
        '
        'w_TitleLabel
        '
        Me.w_TitleLabel.Font = New System.Drawing.Font("맑은 고딕", 11.0!)
        Me.w_TitleLabel.Location = New System.Drawing.Point(28, 1)
        Me.w_TitleLabel.Name = "w_TitleLabel"
        Me.w_TitleLabel.Size = New System.Drawing.Size(887, 28)
        Me.w_TitleLabel.TabIndex = 1
        Me.w_TitleLabel.Text = "Label1"
        Me.w_TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(121, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Panel1.BackgroundImage = Global.버들라이브러리.My.Resources.Resources.background
        Me.Panel1.Controls.Add(Me.x_MainCont)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(920, 600)
        Me.Panel1.TabIndex = 1
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "lab_project.png")
        '
        'Laboratory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(121, Byte), Integer), CType(CType(203, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(920, 600)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Laboratory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Laboratory"
        Me.x_MainCont.ResumeLayout(False)
        Me.x_MainCont.PerformLayout()
        Me.x_OuterCont.ResumeLayout(False)
        Me.x_InnerCont.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.z_itemInfo.ResumeLayout(False)
        Me.z_InfoInner.ResumeLayout(False)
        Me.z_InfoInner.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.z_listCont.ResumeLayout(False)
        Me.x_TitleCont.ResumeLayout(False)
        CType(Me.w_IconPicBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents x_MainCont As System.Windows.Forms.Panel
    Friend WithEvents x_TitleCont As System.Windows.Forms.Panel
    Friend WithEvents w_IconPicBox As System.Windows.Forms.PictureBox
    Friend WithEvents w_TitleLabel As System.Windows.Forms.Label
    Friend WithEvents x_OuterCont As System.Windows.Forms.Panel
    Friend WithEvents x_InnerCont As System.Windows.Forms.Panel
    Friend WithEvents x_TabHeader As System.Windows.Forms.Panel
    Friend WithEvents T_tab00 As System.Windows.Forms.Label
    Friend WithEvents T_tab01 As System.Windows.Forms.Label
    Friend WithEvents F_listview As System.Windows.Forms.ListView
    Friend WithEvents z_chead1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents z_chead2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents z_chead3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents z_chlabel1 As System.Windows.Forms.Label
    Friend WithEvents z_chlabel3 As System.Windows.Forms.Label
    Friend WithEvents z_chlabel2 As System.Windows.Forms.Label
    Friend WithEvents z_listCont As System.Windows.Forms.Panel
    Friend WithEvents z_itemInfo As System.Windows.Forms.Panel
    Friend WithEvents z_InfoInner As System.Windows.Forms.Panel
    Friend WithEvents z_InfoLabel As System.Windows.Forms.Label
    Friend WithEvents z_pattern_cont As System.Windows.Forms.Panel
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
