<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class datapackworker
    Inherits System.Windows.Forms.Form

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(datapackworker))
        Me.titlebar = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.closebtn = New System.Windows.Forms.PictureBox()
        Me.mainPanel = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolTipText = New System.Windows.Forms.ToolTip(Me.components)
        Me.titlebar.SuspendLayout()
        CType(Me.closebtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'titlebar
        '
        Me.titlebar.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.titlebar.Controls.Add(Me.Label1)
        Me.titlebar.Controls.Add(Me.closebtn)
        Me.titlebar.Location = New System.Drawing.Point(1, 1)
        Me.titlebar.Name = "titlebar"
        Me.titlebar.Size = New System.Drawing.Size(400, 30)
        Me.titlebar.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("맑은 고딕", 11.0!)
        Me.Label1.Location = New System.Drawing.Point(3, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(348, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "DJ YOSITAKA"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'closebtn
        '
        Me.closebtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.closebtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.closebtn.BackgroundImage = CType(resources.GetObject("closebtn.BackgroundImage"), System.Drawing.Image)
        Me.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.closebtn.Location = New System.Drawing.Point(354, 0)
        Me.closebtn.Margin = New System.Windows.Forms.Padding(0)
        Me.closebtn.Name = "closebtn"
        Me.closebtn.Size = New System.Drawing.Size(46, 20)
        Me.closebtn.TabIndex = 1
        Me.closebtn.TabStop = False
        Me.ToolTipText.SetToolTip(Me.closebtn, "닫기 버튼입니다.")
        '
        'mainPanel
        '
        Me.mainPanel.BackColor = System.Drawing.Color.White
        Me.mainPanel.Location = New System.Drawing.Point(1, 31)
        Me.mainPanel.Name = "mainPanel"
        Me.mainPanel.Size = New System.Drawing.Size(400, 300)
        Me.mainPanel.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Button1.Location = New System.Drawing.Point(267, 287)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 30)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "확인"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Location = New System.Drawing.Point(1, 271)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(400, 60)
        Me.Panel1.TabIndex = 2
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.Color.White
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Location = New System.Drawing.Point(6, 36)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(390, 229)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(402, 332)
        Me.Panel2.TabIndex = 3
        '
        'datapackworker
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(402, 332)
        Me.Controls.Add(Me.titlebar)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.mainPanel)
        Me.Controls.Add(Me.Panel2)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "datapackworker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "datapackworker"
        Me.titlebar.ResumeLayout(False)
        CType(Me.closebtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents titlebar As System.Windows.Forms.Panel
    Friend WithEvents mainPanel As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents closebtn As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ToolTipText As System.Windows.Forms.ToolTip
End Class
