<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class vxStart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(vxStart))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.vers = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.Panel1.Controls.Add(Me.vers)
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(418, 35)
        Me.Panel1.TabIndex = 2
        '
        'vers
        '
        Me.vers.AutoSize = True
        Me.vers.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Bold)
        Me.vers.ForeColor = System.Drawing.Color.White
        Me.vers.Location = New System.Drawing.Point(4, 7)
        Me.vers.Name = "vers"
        Me.vers.Size = New System.Drawing.Size(149, 21)
        Me.vers.TabIndex = 1
        Me.vers.Text = "Willow's Package "
        Me.vers.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Location = New System.Drawing.Point(14, 49)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(124, 116)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("맑은 고딕", 22.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(144, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(266, 41)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Willow's Package"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(147, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "WillowsLAB"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'vStart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(420, 180)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(420, 180)
        Me.MinimumSize = New System.Drawing.Size(420, 180)
        Me.Name = "vStart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "vStart"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents vers As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
