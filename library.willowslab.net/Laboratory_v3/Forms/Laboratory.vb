Imports System.IO
Imports System.Xml
Imports System.Drawing
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices

Public Class Laboratory

    Public ROOT As String = LDR(Application.StartupPath)
    Public CONF As String = LDR(ROOT) & "data\"
    Public TEMP As String = LDR(ROOT) & "temp\"
    Public LABS As String = LDR(ROOT) & "labdata\"
    Public LABINSTALLED As String = LDR(ROOT) & "실험문서\"
    Public RESOURCES As String = LDR(ROOT) & "resources\"
    Public MYDOC As String = LDR(My.Computer.FileSystem.SpecialDirectories.MyDocuments)
    Private aeroEnabled As Boolean

    Public dragable As Boolean = True

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim path As String

    Public Class NativeStructs
        Public Structure MARGINS
            Public leftWidth As Integer
            Public rightWidth As Integer
            Public topHeight As Integer
            Public bottomHeight As Integer
        End Structure
    End Class
    Public Class NativeMethods
        <DllImport("dwmapi")> _
        Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarInset As NativeStructs.MARGINS) As Integer
        End Function
        <DllImport("dwmapi")> _
        Friend Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
        End Function
        <DllImport("dwmapi.dll")> _
        Public Shared Function DwmIsCompositionEnabled(ByRef pfEnabled As Integer) As Integer
        End Function
    End Class
    Public Class NativeConstants
        Public Const CS_DROPSHADOW As Integer = &H20000
        Public Const WM_NCPAINT As Integer = &H85
    End Class
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            CheckAeroEnabled()
            Dim cp As CreateParams = MyBase.CreateParams
            If Not aeroEnabled Then
                cp.ClassStyle = cp.ClassStyle Or NativeConstants.CS_DROPSHADOW
                Return cp
            Else
                Return cp
            End If
        End Get
    End Property
    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case NativeConstants.WM_NCPAINT
                If aeroEnabled Then
                    NativeMethods.DwmSetWindowAttribute(Handle, 2, 2, 4)
                    Dim bla As New NativeStructs.MARGINS()
                    With bla
                        .bottomHeight = 1
                        .leftWidth = 1
                        .rightWidth = 1
                        .topHeight = 1
                    End With
                    NativeMethods.DwmExtendFrameIntoClientArea(Handle, bla)
                End If
                Exit Select
        End Select
        MyBase.WndProc(m)
    End Sub
    Private Sub CheckAeroEnabled()
        If Environment.OSVersion.Version.Major >= 6 Then
            Dim enabled As Integer = 0
            Dim response As Integer = NativeMethods.DwmIsCompositionEnabled(enabled)
            aeroEnabled = (enabled = 1)
        Else
            aeroEnabled = False
        End If
    End Sub
    Public Function rgb(ByVal r As Integer, ByVal g As Integer, ByVal b As Integer) As Color
        Return System.Drawing.Color.FromArgb(CType(CType(r, Byte), Integer), CType(CType(g, Byte), Integer), CType(CType(b, Byte), Integer))
    End Function
    Private Sub resized() Handles Me.Resize
        Dim PS As Rectangle = Screen.PrimaryScreen.WorkingArea
        Me.Location = New Point(((PS.Right - PS.Left) / 2) - (Me.Width / 2), ((PS.Bottom - PS.Top) / 2) - (Me.Height / 2))
    End Sub
    Private Sub titleChanged() Handles MyBase.TextChanged
        Me.w_TitleLabel.Text = Me.Text
    End Sub

    Private Sub DragStart() Handles w_TitleLabel.MouseDown
        If dragable Then
            drag = True
            mousex = Windows.Forms.Cursor.Position.X - Me.Left
            mousey = Windows.Forms.Cursor.Position.Y - Me.Top
        End If
    End Sub

    Private Sub Dragging() Handles w_TitleLabel.MouseMove
        If drag And dragable Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub DragStop() Handles w_TitleLabel.MouseUp
        drag = False
    End Sub

    Private Sub experiment_list()
        Directory.CreateDirectory(Me.LABS)
        For Each projects As String In Directory.GetDirectories(Me.LABS)
            For Each files As String In Directory.GetFiles(projects)
                Dim fileinfo As New FileInfo(files)
                If fileinfo.Extension.ToLower = ".lab" Then
                    Dim xDoc As New XmlDocument
                    Dim PackName As String = ""
                    Dim Type As String = ""
                    Dim author As String = ""
                    xDoc.Load(files)
                    For Each Node As XmlElement In xDoc.SelectNodes("/*")
                        Try
                            If Node.Name.ToLower = "library_command" Then
                                For Each ChildA As XmlElement In Node.ChildNodes
                                    If ChildA.Name.ToLower = "head" Then
                                        For Each ChildB As XmlElement In ChildA.ChildNodes
                                            If ChildB.Name.ToLower = "info" Then
                                                For Each Attrs As XmlAttribute In ChildB.Attributes
                                                    Select Case Attrs.Name.ToLower
                                                        Case "type"
                                                            Type = Attrs.Value
                                                        Case "info"
                                                            PackName = Attrs.Value
                                                        Case "author"
                                                            author = Attrs.Value
                                                    End Select
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    Dim listitem As New ListViewItem({PackName, Type, "", files.Replace("\\", "\"), author})
                    Me.F_listview.Items.Add(listitem)
                End If
            Next
        Next
    End Sub
    Private Function EmptyLineEscape(ByVal str As String) As String
        str = str.Replace(Chr(13), vbNullString)
        Dim strs() As String = str.Split(Chr(10))
        Dim result As New StringBuilder
        For Each line As String In strs
            If line.Trim <> vbNullString Then
                result.AppendLine(line)
            End If
        Next
        Return result.ToString
    End Function

    Private Sub labFileAnalyzer()

    End Sub
    Private Sub Laboratory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(920, 600)
        Me.x_MainCont.Size = New Size(918, 598)
        experiment_list()
        Me.BackColor = rgb(0, 121, 204)
    End Sub
    Private Function LDR(ByVal dir As String)
        If dir.LastIndexOf("\") < dir.Length Then
            Return dir & "\"
        End If
        Return dir
    End Function

    Private Sub F_listview_SelectedIndexChanged(sender As Object, e As EventArgs) Handles F_listview.SelectedIndexChanged

        Dim xDoc As New XmlDocument

        Dim xVer As String = ""

        Dim PackName As String = ""
        Dim Type As String = ""
        Dim author As String = ""
        Dim ReleaseDate As String = ""
        Dim RemoteResListCount As Integer = 0

        Dim isAuto As Boolean = False
        Dim isSelect As Boolean = False

        Dim litem As ListViewItem = F_listview.Items(F_listview.FocusedItem.Index)
        xDoc.Load(litem.SubItems(3).Text)
        For Each Node As XmlElement In xDoc.SelectNodes("/*")
            Try
                If Node.Name.ToLower = "library_command" Then
                    For Each attr As XmlAttribute In Node.Attributes
                        Select Case attr.Name.ToLower
                            Case "version"
                                xVer = (Val(attr.Value) / 100).ToString("0.0")
                        End Select
                    Next
                    For Each ChildA As XmlElement In Node.ChildNodes
                        Select Case ChildA.Name.ToLower
                            Case "head"
                                For Each ChildB As XmlElement In ChildA.ChildNodes
                                    Select Case ChildB.Name.ToLower
                                        Case "info"
                                            For Each Attrs As XmlAttribute In ChildB.Attributes
                                                Select Case Attrs.Name.ToLower
                                                    Case "type"
                                                        Type = Attrs.Value
                                                    Case "info"
                                                        PackName = Attrs.Value
                                                    Case "author"
                                                        author = Attrs.Value
                                                    Case "releasedate"
                                                        ReleaseDate = Attrs.Value
                                                End Select
                                            Next
                                        Case "reslist"
                                            RemoteResListCount = ChildB.ChildNodes.Count
                                        Case "type"
                                            For Each attrs As XmlAttribute In ChildB.Attributes
                                                Select Case attrs.Name.ToLower
                                                    Case "set"
                                                        If attrs.Value.ToLower = "auto" Then
                                                            isAuto = True
                                                        End If
                                                End Select
                                            Next
                                    End Select
                                Next
                            Case "selector"
                                isSelect = True
                        End Select
                    Next
                End If
            Catch ex As Exception

            End Try
        Next
        DataGridView1.Rows.Clear()

        DataGridView1.Rows.Add(New String() {"문서 버전", xVer})
        DataGridView1.Rows.Add(New String() {"분류", Type})
        DataGridView1.Rows.Add(New String() {"제작자", author})
        DataGridView1.Rows.Add(New String() {"제작일", ReleaseDate})
        DataGridView1.Rows.Add(New String() {"갱신형", isAuto.ToString})
        DataGridView1.Rows.Add(New String() {"선택형", isSelect.ToString})
        Dim mainpath As String = litem.SubItems(3).Text
        Path = mainpath
    End Sub

    Private Sub rtnFiles(ByVal dir As String)

    End Sub
    Private Sub buttonOver(ByVal ctrl As Control)
        ctrl.BackColor = rgb(63, 63, 65)
    End Sub
    Private Sub ButtonUp(ByVal ctrl As Control)
        ctrl.BackColor = rgb(63, 63, 65)
    End Sub
    Private Sub buttonDown(ByVal ctrl As Control)
        ctrl.BackColor = rgb(0, 122, 204)
    End Sub
    Private Sub buttonLeave(ByVal ctrl As Control)
        ctrl.BackColor = Color.Transparent
    End Sub
    Private Sub buttonLeave2(ByVal ctrl As Control)
        ctrl.BackColor = rgb(37, 37, 38)
    End Sub

    Private Sub LBLEnter(sender As Object, e As EventArgs) Handles Label2.MouseEnter, _
        Label3.MouseEnter, _
        Label4.MouseEnter, _
        z_chlabel1.MouseEnter, _
        z_chlabel2.MouseEnter, _
        z_chlabel3.MouseEnter, Label4.MouseEnter
        buttonOver(sender)
    End Sub

    Private Sub LBLDown(sender As Object, e As EventArgs) Handles Label2.MouseDown, _
        Label3.MouseDown, _
        Label4.MouseDown, _
        z_chlabel1.MouseDown, _
        z_chlabel2.MouseDown, _
        z_chlabel3.MouseDown
        buttonDown(sender)
    End Sub

    Private Sub LBLUp(sender As Object, e As EventArgs) Handles Label2.MouseUp, _
        Label3.MouseUp, _
        Label4.MouseUp, _
        z_chlabel1.MouseUp, _
        z_chlabel2.MouseUp, _
        z_chlabel3.MouseUp
        ButtonUp(sender)
    End Sub

    Private Sub LBLLeave(sender As Object, e As EventArgs) Handles Label2.MouseLeave, _
        Label3.MouseLeave, _
        Label4.MouseLeave
        buttonLeave(sender)
    End Sub

    Private Sub LBLLeave2(sender As Object, e As EventArgs) Handles z_chlabel1.MouseLeave, _
        z_chlabel2.MouseLeave, _
        z_chlabel3.MouseLeave
        buttonLeave2(sender)
    End Sub
    Private Sub TabOver(ByVal ctrl As Control)
        ctrl.BackColor = rgb(63, 63, 65)
    End Sub
    Private Sub TabUp(ByVal ctrl As Control)
        ctrl.BackColor = rgb(63, 63, 65)
    End Sub
    Private Sub TabDown(ByVal ctrl As Control)
        ctrl.BackColor = rgb(0, 122, 204)
    End Sub
    Private Sub TabLeave(ByVal ctrl As Control)
        ctrl.BackColor = Color.Transparent
    End Sub

    Private Sub TabClick(sender As Object, e As EventArgs) Handles T_tab00.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Try
            Dim document As New datapackworker(IO.File.ReadAllText(Path), Path)
            addLog(Path & "를 로드했습니다.")

            If document.ShowDialog = Windows.Forms.DialogResult.OK Then
                addLog(Path & "를 언로드 했습니다.")
                addLog("결과 문서는 해당 프로젝트 내의 bin 내에 저장되었습니다.")
                Directory.CreateDirectory(Path.Substring(0, Path.LastIndexOf("\") + 1) & "bin\")
                File.WriteAllText(path.Substring(0, path.LastIndexOf("\") + 1) & "bin\" & path.Substring(path.LastIndexOf("\") + 1), EmptyLineEscape(document.resultDocument), System.Text.Encoding.UTF8)
                'MsgBox("실험 결과 문서는 해당 프로젝트 파일의 bin 폴더 내에 저장되어 있습니다.", MsgBoxStyle.Information, "실험을 완료했습니다.")
            End If
        Catch ex As Exception
            addLog("선택된 문서가 없습니다.")
        End Try
    End Sub

    Private Sub LBLUp(sender As Object, e As MouseEventArgs) Handles z_chlabel3.MouseUp, z_chlabel2.MouseUp, z_chlabel1.MouseUp, Label3.MouseUp, Label2.MouseUp, Label4.MouseUp

    End Sub

    Private Sub LBLDown(sender As Object, e As MouseEventArgs) Handles z_chlabel3.MouseDown, z_chlabel2.MouseDown, z_chlabel1.MouseDown, Label3.MouseDown, Label2.MouseDown, Label4.MouseDown

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.RichTextBox1.Clear()
    End Sub

    Private Sub addLog(ByVal text As String)
        Me.RichTextBox1.AppendText(" '" & text & vbCrLf)
    End Sub
End Class

