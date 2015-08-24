<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class vxCookTray
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
        Me.Recipe3 = New System.Windows.Forms.Label()
        Me.Recipe2 = New System.Windows.Forms.Label()
        Me.Recipe1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Recipe3
        '
        Me.Recipe3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Recipe3.BackColor = System.Drawing.Color.DarkRed
        Me.Recipe3.Location = New System.Drawing.Point(0, 0)
        Me.Recipe3.Name = "Recipe3"
        Me.Recipe3.Size = New System.Drawing.Size(232, 6)
        Me.Recipe3.TabIndex = 4
        '
        'Recipe2
        '
        Me.Recipe2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Recipe2.BackColor = System.Drawing.Color.Orange
        Me.Recipe2.Location = New System.Drawing.Point(0, 0)
        Me.Recipe2.Name = "Recipe2"
        Me.Recipe2.Size = New System.Drawing.Size(156, 6)
        Me.Recipe2.TabIndex = 5
        '
        'Recipe1
        '
        Me.Recipe1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Recipe1.BackColor = System.Drawing.Color.DodgerBlue
        Me.Recipe1.Location = New System.Drawing.Point(0, 0)
        Me.Recipe1.Name = "Recipe1"
        Me.Recipe1.Size = New System.Drawing.Size(78, 6)
        Me.Recipe1.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("돋움", 8.0!)
        Me.Label1.Location = New System.Drawing.Point(19, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 11)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "CookProductItemName"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("돋움", 8.0!)
        Me.Label2.Location = New System.Drawing.Point(19, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 11)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "RecipeItems"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("돋움", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(217, 7)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 12)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "×"
        '
        'CookTray
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(232, 42)
        Me.Controls.Add(Me.Recipe1)
        Me.Controls.Add(Me.Recipe2)
        Me.Controls.Add(Me.Recipe3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Font = New System.Drawing.Font("맑은 고딕", 10.0!)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "CookTray"
        Me.Opacity = 0.5R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CookTray"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Recipe3 As System.Windows.Forms.Label
    Friend WithEvents Recipe2 As System.Windows.Forms.Label
    Friend WithEvents Recipe1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
