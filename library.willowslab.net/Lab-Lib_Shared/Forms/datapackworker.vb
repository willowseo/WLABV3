Imports System.Xml
Imports System.Text
Imports System.Text.RegularExpressions
Imports 버들라이브러리.ColorHelper
Imports System.IO

Public Class datapackworker
#Region "Structure"

    Private Structure ControlSetting
        Public name As String
        Public text As String
        Public src As String
        Public tooltiptext As String

        Public Shared left As Integer = 10
        Public Shared top As Integer = 10
        Public Shared width As Integer = 120
        Public Shared height As Integer = 20

        Public Shared BackColor As Color = Color.Transparent
        Public Shared ForeColor As Color = Color.Black

        Public Shared FlatStyle As FlatStyle = FlatStyle.Standard
        Public Shared SizeMode As PictureBoxSizeMode = PictureBoxSizeMode.Normal
        Public Shared TextAlign As ContentAlignment = ContentAlignment.TopLeft
        Public Shared borderStyle As BorderStyle = borderStyle.Fixed3D
        Public Shared enabled As Boolean = True
        Public Shared multiline As Boolean = False
        Public Sub setmultiline(ByVal bool As Boolean)
            multiline = bool
        End Sub

        Public Function getmultiline()
            Return multiline
        End Function
        Public Sub setEnabled(ByVal bool As Boolean)
            enabled = bool
        End Sub

        Public Function getEnabled()
            Return enabled
        End Function
        Public Sub setBorderStyle(ByVal bstyle As BorderStyle)
            borderStyle = bstyle
        End Sub

        Public Function getBorderStyle()
            Return borderStyle
        End Function

        Public Sub setLeft(ByVal int As Integer)
            left = int
        End Sub
        Public Sub setTop(ByVal int As Integer)
            top = int
        End Sub
        Public Sub setWidth(ByVal int As Integer)
            width = int
        End Sub
        Public Sub setHeight(ByVal int As Integer)
            height = int
        End Sub
        Public Sub setBackColor(ByVal color As Color)
            BackColor = color
        End Sub
        Public Sub setForeColor(ByVal color As Color)
            ForeColor = color
        End Sub
        Public Sub setFlatStyle(ByVal style As FlatStyle)
            FlatStyle = style
        End Sub
        Public Sub setSizeMode(ByVal size As PictureBoxSizeMode)
            SizeMode = size
        End Sub
        Public Sub setTextAlign(ByVal align As ContentAlignment)
            TextAlign = align
        End Sub

        Public Function rleft()
            Return left
        End Function
        Public Function rtop()
            Return top
        End Function
        Public Function rwidth()
            Return width
        End Function
        Public Function rheight()
            Return height
        End Function
        Public Function rbc()
            Return BackColor
        End Function
        Public Function rfc()
            Return ForeColor
        End Function
        Public Function rfs()
            Return FlatStyle
        End Function
        Public Function rsm()
            Return SizeMode
        End Function
        Public Function rta()
            Return TextAlign
        End Function
    End Structure
    Private Function SettingReader(ByVal node As XmlElement)
        Dim tmp As New ControlSetting

        For Each attr As XmlAttribute In node.Attributes
            Select Case attr.Name.ToLower
                Case "name"
                    tmp.name = attr.Value.Trim
                Case "left"
                    tmp.setLeft(Val(attr.Value))
                Case "top"
                    tmp.setTop(Val(attr.Value))
                Case "width"
                    tmp.setWidth(Val(attr.Value))
                Case "height"
                    tmp.setHeight(Val(attr.Value))
                Case "text"
                    tmp.text = attr.Value.Trim
                Case "src"
                    tmp.src = attr.Value.Trim
                Case "tooltiptext"
                    tmp.tooltiptext = attr.Value.Trim
                Case "multiline"
                    If attr.Value.Trim.ToLower = "false" Then
                        tmp.setmultiline(False)
                    Else
                        tmp.setmultiline(True)
                    End If
                Case "enabled"
                    If attr.Value.Trim.ToLower = "false" Then
                        tmp.setEnabled(False)
                    Else
                        tmp.setEnabled(True)
                    End If
                Case "borderstyle"
                    Dim v As String = attr.Value.Trim.ToLower
                    If v = "fixedsingle" Then
                        tmp.setBorderStyle(BorderStyle.FixedSingle)
                    ElseIf v = "fixed3d" Then
                        tmp.setBorderStyle(BorderStyle.Fixed3D)
                    ElseIf v = "none" Then
                        tmp.setBorderStyle(BorderStyle.None)
                    End If
                Case "backcolor", "배경색"
                    Try
                        tmp.setBackColor(HEXtoColor(attr.Value))
                    Catch ex As Exception
                    End Try
                Case "forecolor", "전경색"
                    Try
                        tmp.setForeColor(HEXtoColor(attr.Value))
                    Catch ex As Exception
                    End Try
                Case "flatstyle"
                    Dim v As String = attr.Value.ToLower
                    If v = "flat" Then
                        tmp.setFlatStyle(FlatStyle.Flat)
                    ElseIf v = "popup" Then
                        tmp.setFlatStyle(FlatStyle.Popup)
                    ElseIf v = "standard" Then
                        tmp.setFlatStyle(FlatStyle.Standard)
                    ElseIf v = "system" Then
                        tmp.setFlatStyle(FlatStyle.System)
                    End If
                Case "text-align"
                    Dim v As String = attr.Value.ToLower
                    If v = "topleft" Then
                        tmp.setTextAlign(ContentAlignment.TopLeft)
                    ElseIf v = "topcenter" Then
                        tmp.setTextAlign(ContentAlignment.TopCenter)
                    ElseIf v = "topright" Then
                        tmp.setTextAlign(ContentAlignment.TopRight)
                    ElseIf v = "middleleft" Then
                        tmp.setTextAlign(ContentAlignment.MiddleLeft)
                    ElseIf v = "middlecenter" Then
                        tmp.setTextAlign(ContentAlignment.MiddleCenter)
                    ElseIf v = "middleright" Then
                        tmp.setTextAlign(ContentAlignment.MiddleRight)
                    ElseIf v = "bottomleft" Then
                        tmp.setTextAlign(ContentAlignment.BottomLeft)
                    ElseIf v = "bottomcenter" Then
                        tmp.setTextAlign(ContentAlignment.BottomCenter)
                    ElseIf v = "bottomright" Then
                        tmp.setTextAlign(ContentAlignment.BottomRight)
                    End If
                Case "sizemode", "조정방식"
                    If attr.Value.ToLower = "resize" Then
                        tmp.setSizeMode(PictureBoxSizeMode.StretchImage)
                    ElseIf attr.Value.ToLower = "normal" Then
                        tmp.setSizeMode(PictureBoxSizeMode.Normal)
                    ElseIf attr.Value.ToLower = "zoom" Then
                        tmp.setSizeMode(PictureBoxSizeMode.Zoom)
                    ElseIf attr.Value.ToLower = "center" Then
                        tmp.setSizeMode(PictureBoxSizeMode.CenterImage)
                    End If
            End Select
        Next

        Return tmp
    End Function
#End Region
#Region "USERVAR"
    Private Function USERVAR(ByVal doc As String, ByVal path As String) As String
        doc = doc.Replace("%MY_PROJECT%", path.Substring(0, path.LastIndexOf(".")))
        doc = doc.Replace("%OFFICIAL%", "http://lib.willowslab.com/")
        doc = doc.Replace("%MABINOGI%", Setting.Mabi)
        doc = doc.Replace("%MABINOGI_DATA%", Setting.Mabi & "\data")
        doc = doc.Replace("%MABINOGI_PACK%", Setting.Mabi & "\package")
        doc = doc.Replace("%MABINOGI_MP3%", Setting.Mabi & "\mp3")
        doc = doc.Replace("%MABINOGI_DB%", Setting.Mabi & "\data\db")
        doc = doc.Replace("%MABINOGI_GFX%", Setting.Mabi & "\data\gfx")
        doc = doc.Replace("%MABINOGI_CHAR%", Setting.Mabi & "\data\gfx\char")
        doc = doc.Replace("%MABINOGI_FONT%", Setting.Mabi & "\data\gfx\font")
        doc = doc.Replace("%MABINOGI_FX%", Setting.Mabi & "\data\gfx\fx")
        doc = doc.Replace("%MABINOGI_GUI%", Setting.Mabi & "\data\gfx\gui")
        doc = doc.Replace("%MABINOGI_IMAGE%", Setting.Mabi & "\data\gfx\image")
        doc = doc.Replace("%MABINOGI_IMAGE2%", Setting.Mabi & "\data\gfx\image2")
        doc = doc.Replace("%MABINOGI_INTRO%", Setting.Mabi & "\data\gfx\intro")
        doc = doc.Replace("%MABINOGI_SCENE%", Setting.Mabi & "\data\gfx\scene")
        doc = doc.Replace("%MABINOGI_STYLE%", Setting.Mabi & "\data\gfx\style")
        doc = doc.Replace("%MABINOGI_LOCAL%", Setting.Mabi & "\data\local")
        doc = doc.Replace("%MABINOGI_CODE%", Setting.Mabi & "\data\local\code")
        doc = doc.Replace("%MABINOGI_XML%", Setting.Mabi & "\data\local\xml")
        doc = doc.Replace("%MABINOGI_LOCALE%", Setting.Mabi & "\data\locale")
        doc = doc.Replace("%MABINOGI_MATERIAL%", Setting.Mabi & "\data\material")
        doc = doc.Replace("%MABINOGI_SOUND%", Setting.Mabi & "\data\sound")
        doc = doc.Replace("%MABINOGI_WORLD%", Setting.Mabi & "\data\world")
        doc = doc.Replace("%MY_MABINOGI%", Setting.myDoc & "\마비노기")
        doc = doc.Replace("%PROGRAMDIR%", Setting.Dir)

        Return doc
    End Function
#End Region
#Region "Component Adding"
    Public Function NewCombobox(ByVal parent As Object, ByVal node As XmlElement)

        Dim Component As New ComboBox
        Dim tempList As New ListView
        Dim setting As ControlSetting = SettingReader(node)

        Component.Name = setting.name

        For Each node2 As XmlElement In node.ChildNodes
            Component.Items.Add(node2.InnerText.Trim)
            tempList.Items.Add(node2.Attributes(0).Value.Trim)
        Next

        tempList.Location = New Point(-99, -99)
        tempList.Size = New Size(0, 0)
        tempList.Name = setting.name & "_list"

        Component.BackColor = setting.rbc
        Component.ForeColor = setting.rfc
        Component.Location = New Point(setting.rleft, setting.rtop)
        Component.Size = New Size(setting.rwidth, setting.rheight)
        Component.MaxDropDownItems = 8
        Component.DropDownStyle = ComboBoxStyle.DropDownList
        Component.SelectedIndex = 0

        Try
            parent.Controls.add(Component)
            parent.Controls.add(tempList)
            Me.variablelist.Items.Add(New ListViewItem({"select", setting.name}))
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Sub NewCheckBox(ByVal parent As Object, ByVal node As XmlElement)
        Try
            Dim tmpselect As New CheckBox
            Dim setting As ControlSetting = SettingReader(node)

            For Each nm As ListViewItem In Me.variablelist.Items
                If nm.SubItems(0).Text = setting.name Then
                    MsgBox("이 실험 문서에 오류가 있습니다. 계속해서 이런 현상이 나타나면 입력된 실험 계획서를 수정하거나 문의하시기 바랍니다.", MsgBoxStyle.Critical, "오류")
                    Exit Sub
                End If
            Next

            Me.variablelist.Items.Add(New ListViewItem({"checkbox", setting.name}))

            tmpselect.Size = New Size(setting.rwidth, setting.rheight)
            tmpselect.Location = New Drawing.Point(setting.rleft, setting.rtop)
            tmpselect.Text = node.InnerText.Trim
            tmpselect.Name = setting.name
            Me.ToolTipText.SetToolTip(tmpselect, setting.tooltiptext)
            parent.Controls.Add(tmpselect)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub NewLabel(ByVal parent As Object, ByVal node As XmlElement)
        Dim tmpselect As New Label
        Dim setting As ControlSetting = SettingReader(node)

        tmpselect.AutoSize = True
        tmpselect.BackColor = setting.rbc
        tmpselect.ForeColor = setting.rfc
        tmpselect.FlatStyle = setting.rfs
        tmpselect.TextAlign = setting.rta
        tmpselect.Location = New Point(setting.rleft, setting.rtop)
        tmpselect.Size = New Size(setting.rwidth, setting.rheight)
        Me.ToolTipText.SetToolTip(tmpselect, setting.tooltiptext)
        tmpselect.Text = node.InnerText

        parent.Controls.Add(tmpselect)
    End Sub

    Private Sub NewPicturebox(ByVal parent As Object, ByVal node As XmlElement)

        Dim tmpselect As New PictureBox
        Dim setting As ControlSetting = SettingReader(node)

        tmpselect.SizeMode = setting.rsm
        tmpselect.ImageLocation = setting.src
        tmpselect.ErrorImage = My.Resources.Resources.image_crushed
        tmpselect.Location = New Drawing.Point(setting.rleft, setting.rtop)
        tmpselect.Size = New Size(setting.rwidth, setting.rheight)
        Me.ToolTipText.SetToolTip(tmpselect, setting.tooltiptext)

        parent.Controls.Add(tmpselect)
    End Sub

    Private Function AddContextMenuItem(ByVal node As XmlElement)
        Dim cms As New MenuStrip

        Dim list As New List(Of ToolStripMenuItem)
        Dim i As Integer = 0

        For Each node2 As XmlElement In node.ChildNodes
            Dim cmsi As New ToolStripMenuItem
            If node2.Name.ToLower = "item" Then
                For Each attr As XmlAttribute In node2.Attributes
                    Select Case attr.Name.ToLower
                        Case "text"
                            cmsi.Text = attr.Value.Trim
                    End Select
                Next
                If node2.HasChildNodes = True Then
                    cmsi.DropDownItems.AddRange(AddContextMenuItem(node2))
                End If
                list.Add(cmsi)
            End If
            i += 1
        Next

        Dim listt As ToolStripMenuItem() = New ToolStripMenuItem(list.Count - 1) {}
        For ii As Integer = 0 To list.Count - 1
            listt(ii) = list(ii)
        Next

        Return listt
    End Function
#End Region
    Private variablelist As New ListView
    Private xmldocument As New StringBuilder
    Private nodeStarter As String = ""

    Private Function addComponentWork(ByVal node As XmlElement, ByVal parent As Object)
        Select Case node.Name.ToLower
            Case "formsetting"
                Dim width As Integer = 400
                Dim height As Integer = 300
                Dim windowstyle As String = ""

                Dim buttonleft As Integer = 0
                Dim buttontop As Integer = 0
                Dim buttonwidth As Integer = 0
                Dim buttonheight As Integer = 0

                For Each args As XmlElement In node.ChildNodes

                    For Each attr As XmlAttribute In args.Attributes
                        If attr.Name.ToLower = "name" Then
                            Select Case attr.Value.ToLower
                                Case "x", "width"
                                    width = Val(args.InnerText)
                                Case "y", "height"
                                    height = Val(args.InnerText)
                                Case "title"
                                    Me.Text = args.InnerText.Trim
                                    Me.Label1.Text = args.InnerText.Trim
                                Case "backcolor"
                                    Try
                                        Me.BackColor = HEXtoColor(args.InnerText.Trim)
                                    Catch ex As Exception
                                    End Try
                                Case "forecolor"
                                    Try
                                        Me.ForeColor = HEXtoColor(args.InnerText.Trim)
                                    Catch ex As Exception
                                    End Try
                                Case "mainbackcolor"
                                    Try
                                        Me.mainPanel.BackColor = HEXtoColor(args.InnerText.Trim)
                                    Catch ex As Exception
                                    End Try
                                Case "mainforecolor"
                                    Try
                                        Me.mainPanel.ForeColor = HEXtoColor(args.InnerText.Trim)
                                    Catch ex As Exception
                                    End Try
                                Case "windowstyle"
                                    windowstyle = args.InnerText.Trim.ToLower
                                Case "buttontop"
                                    buttontop = Val(args.InnerText)
                                Case "buttonleft"
                                    buttonleft = Val(args.InnerText)
                                Case "buttonwidth"
                                    buttonwidth = Val(args.InnerText)
                                Case "buttonheight"
                                    buttonheight = Val(args.InnerText)
                                Case "buttontext"
                                    Me.Button1.Text = args.InnerText.Trim
                            End Select
                        End If
                    Next
                Next
                If buttonleft > 0 Or buttontop > 0 Then
                    Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
                    Me.Button1.Location = New Point(buttonleft, buttontop)
                End If

                If buttonwidth > 0 And buttonheight > 0 Then
                    Me.Button1.Size = New Size(buttonwidth, buttonheight)
                End If

                If windowstyle = "default" Or windowstyle = "" Then
                    If width < 400 Then
                        width = 400
                    End If
                    If height < 240 Then
                        height = 240
                    End If
                    Me.titlebar.Size = New Size(width, 30)
                    Me.mainPanel.Size = New Size(width, height)
                    Me.mainPanel.Refresh()
                    Me.Size = New Size(width + 2, height + 32)
                    Me.Refresh()
                Else
                    If windowstyle = "fixedsingle" Then
                        If width < 400 Then
                            width = 400
                        End If
                        If height < 240 Then
                            height = 240
                        End If
                        Me.titlebar.Visible = False
                        Me.Panel1.Visible = False
                        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                        Me.MaximizeBox = False
                        Me.mainPanel.Size = New Size(width, height)
                        Me.mainPanel.Location = New Point(0, 0)
                        Me.mainPanel.Refresh()
                        Me.Size = New Size(width + 6, height + 29)
                        Me.Refresh()
                    End If
                End If
            Case "contextmenustrip"
                Try
                    Dim cms As New MenuStrip
                    cms.BackColor = Color.WhiteSmoke
                    Dim list As New List(Of ToolStripMenuItem)
                    Dim i As Integer = 0

                    For Each node2 As XmlElement In node.ChildNodes
                        Dim cmsi As New ToolStripMenuItem
                        If node2.Name.ToLower = "item" Then
                            For Each attr As XmlAttribute In node2.Attributes
                                Select Case attr.Name.ToLower
                                    Case "text"
                                        cmsi.Text = attr.Value.Trim
                                End Select
                            Next
                            If node2.HasChildNodes = True Then
                                cmsi.DropDownItems.AddRange(AddContextMenuItem(node2))
                            End If
                            list.Add(cmsi)
                        End If
                        i += 1
                    Next

                    Dim listt As ToolStripMenuItem() = New ToolStripMenuItem(list.Count - 1) {}
                    For ii As Integer = 0 To list.Count - 1
                        listt(ii) = list(ii)
                    Next

                    cms.Items.AddRange(listt)
                    parent.Controls.Add(cms)
                Catch ex As Exception
                    MsgBox(ex.GetBaseException.ToString)
                End Try
            Case "button"
                Dim attr As ControlSetting = SettingReader(node)
                Dim comp As New Button
                comp.Size = New Size(attr.rwidth, attr.rheight)
                comp.Location = New Point(attr.rleft, attr.rtop)
                If attr.rbc = Color.Transparent Then
                    comp.BackColor = SystemColors.Control
                Else
                    comp.BackColor = attr.rbc
                End If
                comp.FlatStyle = attr.rfs
                comp.ForeColor = attr.rfc
                comp.Enabled = attr.getEnabled
                comp.Text = node.InnerText.Trim
                Me.ToolTipText.SetToolTip(comp, attr.tooltiptext)

                parent.controls.add(comp)
            Case "textbox"
                Dim attr As ControlSetting = SettingReader(node)
                Dim comp As New TextBox
                comp.Size = New Size(attr.rwidth, attr.rheight)
                comp.Location = New Point(attr.rleft, attr.rtop)
                comp.BorderStyle = attr.getBorderStyle
                If attr.rbc = Color.Transparent Then
                    comp.BackColor = Color.White
                Else
                    comp.BackColor = attr.rbc
                End If
                comp.ForeColor = attr.rfc
                comp.ReadOnly = attr.getEnabled
                comp.Text = node.InnerText.Trim
                comp.Multiline = attr.getmultiline
                Me.ToolTipText.SetToolTip(comp, attr.tooltiptext)
                If attr.getmultiline Then
                    comp.ScrollBars = ScrollBars.Vertical
                End If

                parent.controls.add(comp)
            Case "tabpage"
                Try
                    Dim tabcontrol As New TabControl
                    Dim left As Integer = 10
                    Dim top As Integer = 10
                    Dim width As Integer = 200
                    Dim height As Integer = 200
                    tabcontrol.Font = New System.Drawing.Font("맑은 고딕", 9.0!)


                    For Each attr As XmlAttribute In node.Attributes
                        Select Case attr.Name.ToLower
                            Case "left"
                                left = Val(attr.Value)
                            Case "top"
                                top = Val(attr.Value)
                            Case "width"
                                width = Val(attr.Value)
                            Case "height"
                                height = Val(attr.Value)
                            Case "text"
                                tabcontrol.Text = attr.Value.Trim
                            Case "backcolor", "배경색"
                                Try
                                    tabcontrol.BackColor = HEXtoColor(attr.Value)
                                Catch ex As Exception
                                End Try
                            Case "forecolor", "전경색"
                                Try
                                    tabcontrol.ForeColor = HEXtoColor(attr.Value)
                                Catch ex As Exception
                                End Try
                        End Select
                    Next

                    Dim i As Integer = 1
                    For Each tabnode As XmlElement In node.ChildNodes
                        Select Case tabnode.Name.ToLower
                            Case "tab"
                                Dim tabpage As New TabPage
                                tabpage.BackColor = Color.White
                                tabpage.ForeColor = Color.Black
                                tabpage.Text = "TabPage " & i
                                For Each attr As XmlAttribute In tabnode.Attributes
                                    Select Case attr.Name.ToLower
                                        Case "name"
                                            tabpage.Text = attr.Value.Trim
                                        Case "backcolor"
                                            Try
                                                tabpage.BackColor = HEXtoColor(attr.Value)
                                            Catch ex As Exception
                                            End Try
                                        Case "forecolor"
                                            Try
                                                tabpage.ForeColor = HEXtoColor(attr.Value)
                                            Catch ex As Exception
                                            End Try
                                    End Select
                                Next

                                For Each tpnode As XmlElement In tabnode.ChildNodes
                                    addComponentWork(tpnode, tabpage)
                                Next
                                tabcontrol.Controls.Add(tabpage)
                                i += 1
                        End Select
                    Next

                    tabcontrol.Location = New Point(left, top)
                    tabcontrol.Size = New Size(width, height)
                    parent.Controls.add(tabcontrol)
                Catch ex As Exception
                    MsgBox(ex.GetBaseException.ToString)
                End Try
            Case "groupbox"
                Try
                    Dim groupbox As New GroupBox
                    Dim left As Integer = 10
                    Dim top As Integer = 10
                    Dim width As Integer = 120
                    Dim height As Integer = 30
                    groupbox.Font = New System.Drawing.Font("맑은 고딕", 9.0!)

                    For Each attr As XmlAttribute In node.Attributes
                        Select Case attr.Name.ToLower
                            Case "left"
                                left = Val(attr.Value)
                            Case "top"
                                top = Val(attr.Value)
                            Case "width"
                                width = Val(attr.Value)
                            Case "height"
                                height = Val(attr.Value)
                            Case "text"
                                groupbox.Text = attr.Value.Trim
                            Case "backcolor", "배경색"
                                Try
                                    groupbox.BackColor = HEXtoColor(attr.Value)
                                Catch ex As Exception
                                End Try
                            Case "forecolor", "전경색"
                                Try
                                    groupbox.ForeColor = HEXtoColor(attr.Value)
                                Catch ex As Exception
                                End Try
                            Case "flatstyle"
                                Dim v As String = attr.Value.ToLower
                                If v = "flat" Then
                                    groupbox.FlatStyle = FlatStyle.Flat
                                ElseIf v = "popup" Then
                                    groupbox.FlatStyle = FlatStyle.Popup
                                ElseIf v = "standard" Then
                                    groupbox.FlatStyle = FlatStyle.Standard
                                ElseIf v = "system" Then
                                    groupbox.FlatStyle = FlatStyle.System
                                End If
                        End Select
                    Next

                    For Each tabnode As XmlElement In node.ChildNodes
                        addComponentWork(tabnode, groupbox)
                    Next

                    groupbox.Location = New Point(left, top)
                    groupbox.Size = New Size(width, height)
                    parent.Controls.add(groupbox)
                Catch ex As Exception
                    MsgBox(ex.GetBaseException.ToString)
                End Try
            Case "checkbox"
                NewCheckBox(parent, node)
            Case "select"
                NewCombobox(parent, node)
            Case "label"
                NewLabel(parent, node)
            Case "img"
                NewPicturebox(parent, node)
        End Select
        Return True
    End Function

    Public Sub New(ByVal document As String, ByVal url As String)
        InitializeComponent()
        Try
            Dim str As String = document
            Dim src As String = url
            Dim XML As New XmlDocument
            str = ErrorRemove(str)

            While (str.IndexOf("<!--") > -1 And str.IndexOf("-->"))
                Try
                    str = str.Replace(readCommand.ParseOuter(str, "<!--", "-->"), "")
                Catch ex As Exception
                    Exit While
                End Try
            End While
            xmldocument.Append(str)
            str = USERVAR(str, src)
            readedocument = str
            XML.LoadXml(str)

            Me.titlebar.Width = 300
            Me.mainPanel.Width = 300
            Me.width = 302
            Me.mainPanel.Height = 180
            Me.height = 212
            Me.Label1.Text = "Selection Procedure"


            For Each node As XmlElement In XML.SelectNodes("/Library_Command/Selector/*")
                addComponentWork(node, Me.mainPanel)
            Next
            Me.RichTextBox1.Hide()
            Me.Hide()
        Catch ex As XmlException
            Dim msg As String = ex.Message

            Me.RichTextBox1.AppendText("※ 이 실험 계획서에 문제가 있습니다!" & vbCrLf)
            Me.RichTextBox1.AppendText(msg.Substring(0, msg.LastIndexOf(".")) & vbCrLf & vbCrLf)
            Dim linetmp As String = msg.Substring(msg.LastIndexOf("줄") + 1).Trim
            linetmp = linetmp.Substring(0, linetmp.LastIndexOf(","))
            Me.RichTextBox1.AppendText(" * 줄 : " & linetmp & vbCrLf)

            Dim lentemp As String = msg.Substring(msg.LastIndexOf("위치") + 2).Trim
            Me.RichTextBox1.AppendText(" * 위치 : " & lentemp & vbCrLf)
            Dim doc As String = Me.xmldocument.ToString
            doc.Replace(Chr(13), "")

            Dim lines As String() = doc.ToString.Split(Chr(10))
            Dim errorline As String = lines(Val(linetmp) - 1)

            'MsgBox(errorline.Length)
            Me.RichTextBox1.AppendText("문서 :" & vbCrLf & errorline & vbCrLf)

        Catch ex As Exception
        End Try
    End Sub

    Private Function ErrorRemove(ByVal xmlDoc As String)
        Dim doc As New StringBuilder

        xmlDoc = Regex.Replace(xmlDoc, "([=]{1}[\x22]{1})([^\x22]*)([\x22]{1})([^ ]{1})", "$1$2$3 $4", RegexOptions.Multiline)

        doc.Append(xmlDoc)
        doc.Replace("<<", "<")
        doc.Replace(">>", ">")
        doc.Replace("utf-8", "UTF-8")
        doc.Replace("utf-16", "UTF-16")
        doc.Replace("&qot;", "&quot;")

        Return doc.ToString
    End Function

    Private Function compressd(ByVal str As String) As String
        Dim stb As New StringBuilder
        stb.Append(str)
        stb.Replace(Chr(10), " ")
        stb.Replace(Chr(13), "")
        stb.Replace(Chr(9), "")
        Return stb.ToString.Trim
    End Function

    Private Function ReturnNodeValue(ByVal vNode As XmlElement) As String
        Dim Front As String = Mid(vNode.InnerXml, 1, 15)
        If InStr(Mid(vNode.InnerXml, 1, 15), "<![CDATA[") > 0 Then
            Return vNode.InnerText
        Else
            Dim Pattern As String = "^<!\[CDATA\[(.*)\]\]>$"
            Return Regex.Replace(vNode.InnerXml, Pattern, "$1")
        End If
    End Function

    Public document As String = ""

    Private manual As New List(Of resultlist)

    Private Sub components_calc(ByVal ctrl As Control)
        For Each obj As Control In ctrl.Controls
            'MsgBox(obj.Name.ToString)
            For Each tmp As ListViewItem In Me.variablelist.Items
                If obj.Name = tmp.SubItems(1).Text Then
                    Dim type As String = tmp.SubItems(0).Text
                    If type = "select" Then
                        Dim index As Integer = CType(obj, ComboBox).SelectedIndex
                        Dim value As String = ""
                        For Each obj2 As Control In ctrl.Controls
                            If obj2.Name = tmp.SubItems(1).Text & "_list" Then
                                value = CType(obj2, ListView).Items(index).SubItems(0).Text
                            End If
                        Next
                        'MsgBox(value)
                        resultDocument = resultDocument.Replace("[$_VAR[" & obj.Name & "]]", value.ToString)
                    ElseIf type = "checkbox" Then
                        resultDocument = resultDocument.Replace("[$_VAR[" & obj.Name & "]]", CType(obj, CheckBox).Checked.ToString)
                    End If
                End If
            Next
            If obj.HasChildren Then
                components_calc(obj)
            End If
        Next
    End Sub
    Private Structure resultlist
        Public type As String
        Public name As String
        Public value As String
    End Structure
    Private Sub act()
        xmldocument.Replace(readCommand.ParseOuter(xmldocument.ToString, "<Selector", "</Selector>"), "")
        resultDocument = xmldocument.ToString
        manual.Clear()
        components_calc(Me.mainPanel)
    End Sub

    Public readedocument As String = ""
    Public resultDocument As String = ""

    Private Sub closebtn_enter(sender As Object, e As EventArgs) Handles closebtn.MouseEnter
        closebtn.BackColor = Func.rgb(224, 67, 67)
    End Sub

    Private Sub closebtn_leave(sender As Object, e As EventArgs) Handles closebtn.MouseLeave
        closebtn.BackColor = Func.rgb(199, 80, 80)
    End Sub
    Private Sub Button1_Click(sender As Control, e As EventArgs) Handles Button1.Click
        act()
    End Sub
    Public dragable As Boolean = True
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

    Private Sub DragStart() Handles Label1.MouseDown, titlebar.MouseDown
        If dragable Then
            drag = True
            mousex = Windows.Forms.Cursor.Position.X - Me.left
            mousey = Windows.Forms.Cursor.Position.Y - Me.top
        End If
    End Sub

    Private Sub Dragging() Handles Label1.MouseMove, titlebar.MouseMove
        If drag And dragable Then
            Me.top = Windows.Forms.Cursor.Position.Y - mousey
            Me.left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub DragStop() Handles Label1.MouseUp, titlebar.MouseUp
        drag = False
    End Sub

    Private Sub closebtn_Click(sender As Object, e As EventArgs) Handles closebtn.Click
        If MsgBox("정말로 해당 실험을 중단하시겠습니까?", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkCancel, "경고") = MsgBoxResult.Ok Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub
End Class