<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class xpAuth_donor
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(6, 42)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(177, 21)
        Me.TextBox1.TabIndex = 0
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(191, 42)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBox2.Size = New System.Drawing.Size(177, 21)
        Me.TextBox2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("맑은 고딕", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 19)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "User"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("맑은 고딕", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.Label2.Location = New System.Drawing.Point(191, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 19)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Password"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(386, 162)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBox2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(377, 136)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Account Auth"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Button2)
        Me.TabPage2.Controls.Add(Me.TextBox7)
        Me.TabPage2.Controls.Add(Me.TextBox6)
        Me.TabPage2.Controls.Add(Me.TextBox5)
        Me.TabPage2.Controls.Add(Me.TextBox4)
        Me.TabPage2.Controls.Add(Me.TextBox3)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(378, 136)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Serial number Auth"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(9, 42)
        Me.TextBox3.MaxLength = 5
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(64, 21)
        Me.TextBox3.TabIndex = 11
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(83, 42)
        Me.TextBox4.MaxLength = 5
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(64, 21)
        Me.TextBox4.TabIndex = 12
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(157, 42)
        Me.TextBox5.MaxLength = 5
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(64, 21)
        Me.TextBox5.TabIndex = 13
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(231, 42)
        Me.TextBox6.MaxLength = 5
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(64, 21)
        Me.TextBox6.TabIndex = 14
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("맑은 고딕", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.Button1.Location = New System.Drawing.Point(0, 94)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(377, 42)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Donor's Spacial List Load"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("맑은 고딕", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.Button2.Location = New System.Drawing.Point(0, 94)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(377, 42)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Get Serial Package List"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("맑은 고딕", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.Label3.Location = New System.Drawing.Point(5, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 19)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Serial Number"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(305, 42)
        Me.TextBox7.MaxLength = 5
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(64, 21)
        Me.TextBox7.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("맑은 고딕", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.Label4.Location = New System.Drawing.Point(71, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(15, 19)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "-"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("맑은 고딕", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.Label5.Location = New System.Drawing.Point(145, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(15, 19)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "-"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("맑은 고딕", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.Label6.Location = New System.Drawing.Point(219, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(15, 19)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "-"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("맑은 고딕", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.Label7.Location = New System.Drawing.Point(293, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(15, 19)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "-"
        '
        'auth_donor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 161)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximumSize = New System.Drawing.Size(400, 200)
        Me.MinimumSize = New System.Drawing.Size(400, 200)
        Me.Name = "auth_donor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "auth_donor"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
