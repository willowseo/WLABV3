Imports System.IO

Public Module Setting
    'thread
    Delegate Sub str([str] As String)
    'directory settings
    Public Dir As String = Application.StartupPath
    Public Data As String = Setting.Dir & "\data\"
    Public Mabi As String = Func.getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi", "")
    Public MabiTest As String = Func.getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi_test", "")
    Public myDoc As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    Public myMabi As String = Setting.myDoc & "\마비노기\"
    'program settings
    Public personalData As Boolean = False
    Public autoUpdated As Boolean = False
    Public autoGen As Boolean = False
    Public autologin As Boolean = False
    Public useRegister As Boolean = False
    Public Server As String() = {"버들실험실 공식 서버 I", "http://library.willowslab.com/lib/"}
    Public windowColor As Integer() = {105, 193, 240}
    Public ID As String = ""
    Public PW As String = ""
    Public isDonor As Boolean = False
    Public allowTray As Boolean = True
    Public viewNotibar As Boolean = True
    Public isTestMabi As Boolean = False

    Public Function procLogin(Optional ByVal id As String = "", Optional ByVal pw As String = "") As Boolean
        Dim listfile As New Xml.XmlDocument
        Dim list As Xml.XmlNodeList = Nothing
        Dim result As Boolean = False

        If id.Length = 0 Then
            id = Setting.ID
        End If

        If pw.Length = 0 Then
            pw = Setting.PW
        End If

        If File.Exists(Setting.Data & "authtemp.xml") Then
            File.Delete(Setting.Data & "authtemp.xml")
        End If
        If Func.webClient(Func.DefaultServerURL & "?auth=true&id=" & id & "&pw=" & pw, Setting.Dir & "\data\", "authtemp.xml") Then

            listfile.Load(Setting.Dir & "\data\authtemp.xml")
            list = listfile.SelectNodes("/Auth/Return")

            For Each node As Xml.XmlElement In list
                If Setting.toBool(node.GetAttribute("bool")) Then
                    result = Setting.toBool(node.GetAttribute("bool"))
                End If

                If result Then
                    Setting.ID = id
                    Setting.PW = pw
                    If Setting.toBool(node.GetAttribute("donor")) Then
                        Setting.isDonor = Setting.toBool(node.GetAttribute("donor"))
                    End If
                End If
            Next
        End If
        File.Delete(Setting.Data & "authtemp.xml")
        Return result
    End Function
    Public Sub base64encode(ByVal original As String)
    End Sub
    Public Sub setSendToServer()
        Dim alarm As String = File.ReadAllText(Setting.myMabi & "설정\custom.alarm")
        Dim keyboard As String = File.ReadAllText(Setting.myMabi & "설정\custom.keyboard")
        If File.Exists(Setting.Data & "authtemp.xml") Then
            File.Delete(Setting.Data & "authtemp.xml")
        End If
        If Setting.ID.Length > 0 And Setting.PW.Length > 0 Then
            If Func.webClient(Func.DefaultServerURL & "?auth=true&id=" & Setting.ID & "&pw=" & Setting.PW, Setting.Dir & "\data\", "authtemp.xml") Then
                Dim listfile As New Xml.XmlDocument
                Dim list As Xml.XmlNodeList = Nothing

                listfile.Load(Setting.Dir & "\data\authtemp.xml")
                list = listfile.SelectNodes("/Auth/Return")
                For Each node As Xml.XmlElement In list
                    If node.GetAttribute("bool") = "False" Then
                        MsgBox("계정 또는 비밀번호가 올바르지 않습니다.", MsgBoxStyle.Critical Or vbMsgBoxSetForeground, "서버로부터의 메세지")
                    Else
                        If Func.webClient("http://library.willowslab.com/setting/?id=" & Setting.ID & "&pw=" & Setting.PW & "&alarm=" & alarm & "&keyset=" & keyboard, Setting.Data, "tmp.xml") Then
                            listfile.Load(Setting.Dir & "\data\authtemp.xml")
                            list = listfile.SelectNodes("/Auth/Return")
                            For Each returnval As Xml.XmlElement In list
                                If returnval.GetAttribute("bool") = "False" Then
                                    MsgBox("서버에 저장하는 데 실패하였습니다.", MsgBoxStyle.Critical Or vbMsgBoxSetForeground, "서버로부터의 메세지")
                                Else
                                End If
                            Next
                        End If
                    End If
                Next
            End If
        Else
            MsgBox("계정이나 비밀번호 항목이 비어 있습니다.", MsgBoxStyle.Critical Or vbMsgBoxSetForeground, "서버로부터의 메세지")
        End If
    End Sub
    Public Sub saveSetting()
        Func.ExistsDir(Setting.Data)
        Dim settingfile As String = ""
        settingfile &= "<?xml version=""1.0"" encoding=""utf-8""?>" & vbNewLine
        settingfile &= "<Setting>" & vbNewLine
        settingfile &= vbTab & "<property name=""personal"" val=""" & Setting.toStr(Setting.personalData) & """/>" & vbNewLine
        settingfile &= vbTab & "<property name=""autoUpdate"" val=""" & Setting.toStr(Setting.autoUpdated) & """/>" & vbNewLine
        settingfile &= vbTab & "<property name=""autoGen"" val=""" & Setting.toStr(Setting.autoGen) & """/>" & vbNewLine
        settingfile &= vbTab & "<property name=""autoLogin"" val=""" & Setting.toStr(Setting.autologin) & """/>" & vbNewLine
        settingfile &= vbTab & "<property name=""useRegister"" val=""" & Setting.toStr(Setting.useRegister) & """/>" & vbNewLine
        settingfile &= vbTab & "<property name=""allowTray"" val=""" & Setting.toStr(Setting.allowTray) & """/>" & vbNewLine
        settingfile &= vbTab & "<property name=""isTestMabi"" val=""" & Setting.toStr(Setting.isTestMabi) & """/>" & vbNewLine
        settingfile &= vbTab & "<property name=""viewNotibar"" val=""" & Setting.toStr(Setting.viewNotibar) & """/>" & vbNewLine
        settingfile &= vbTab & "<property name=""windowColor"" R=""" & Setting.windowColor(0) & """ G=""" & Setting.windowColor(1) & """ B=""" & Setting.windowColor(2) & """/>" & vbNewLine
        settingfile &= vbTab & "<property name=""Login"" id=""" & Setting.ID & """ pw=""" & Setting.PW & """/>" & vbNewLine
        settingfile &= "</Setting>" & vbNewLine
        My.Computer.FileSystem.WriteAllText(Setting.Data & "setting.xml", settingfile, False)
    End Sub
    Public Sub loadSetting()

        FrmMain.MabiTest.Text = Func.getReg("HKEY_CURRENT_USER\Software\Nexon\Mabinogi_test", "")
        Func.ExistsDir(Setting.Data)
        If File.Exists(Setting.Data & "setting.xml") Then
            Dim settingPath As String = Setting.Data & "setting.xml"
            Dim xmldata As New Xml.XmlDocument
            Dim list As Xml.XmlNodeList
            xmldata.Load(settingPath)
            list = xmldata.SelectNodes("/Setting/property")
            For Each node As Xml.XmlElement In list
                Dim name As String = node.GetAttribute("name")
                Dim value As String = node.GetAttribute("val")
                Select Case name
                    Case "personal"
                        Setting.personalData = Setting.toBool(value)
                    Case "autoUpdate"
                        Setting.autoUpdated = Setting.toBool(value)
                    Case "autoGen"
                        Setting.autoGen = Setting.toBool(value)
                    Case "useRegister"
                        Setting.useRegister = Setting.toBool(value)
                    Case "autoLogin"
                        Setting.autologin = Setting.toBool(value)
                    Case "allowTray"
                        Setting.allowTray = Setting.toBool(value)
                    Case "viewNotibar"
                        Setting.viewNotibar = Setting.toBool(value)
                    Case "isTestMabi"
                        Setting.isTestMabi = Setting.toBool(value)
                    Case "windowColor"
                        Setting.windowColor = {node.GetAttribute("R"), node.GetAttribute("G"), node.GetAttribute("B")}
                    Case "Login"
                        If Setting.autologin = True Then
                            Setting.ID = node.GetAttribute("id")
                            Setting.PW = node.GetAttribute("pw")
                        End If
                End Select
            Next
            FrmMain.settingInit()
        Else
            Dim settingfile As String = ""
            settingfile &= "<?xml version=""1.0"" encoding=""utf-8""?>" & vbNewLine
            settingfile &= "<Setting>" & vbNewLine
            settingfile &= vbTab & "<property name=""personal"" val=""False""/>" & vbNewLine
            settingfile &= vbTab & "<property name=""autoUpdate"" val=""True""/>" & vbNewLine
            settingfile &= vbTab & "<property name=""autoGen"" val=""False""/>" & vbNewLine
            settingfile &= vbTab & "<property name=""autoLogin"" val=""False""/>" & vbNewLine
            settingfile &= vbTab & "<property name=""useRegister"" val=""False""/>" & vbNewLine
            settingfile &= vbTab & "<property name=""allowTray"" val=""True""/>" & vbNewLine
            settingfile &= vbTab & "<property name=""isTestMabi"" val=""False""/>" & vbNewLine
            settingfile &= vbTab & "<property name=""viewNotibar"" val=""True""/>" & vbNewLine
            settingfile &= vbTab & "<property name=""windowColor"" R=""105"" G=""193"" B=""240""/>" & vbNewLine
            settingfile &= vbTab & "<property name=""Login"" id="""" pw=""""/>" & vbNewLine
            settingfile &= "</Setting>" & vbNewLine
            My.Computer.FileSystem.WriteAllText(Setting.Data & "setting.xml", settingfile, False)
        End If
    End Sub
    Public Function toBool(ByVal str As String) As Boolean
        If str = "False" Then
            Return False
        ElseIf str = "True" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function toStr(ByVal bool As Boolean) As String
        If bool Then
            Return "True"
        Else
            Return "False"
        End If
    End Function
    Public Function URLEncode(sRawURL As String) As String
        Dim iLoop As Integer
        Dim sRtn As String = ""
        Dim sTmp As String = ""
        Dim result As String = ""

        Const sValidChars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz:/.?_-$(){}~&"
        Try
            If Len(sRawURL) > 0 Then
                ' Loop through each char
                For iLoop = 1 To Len(sRawURL)
                    sTmp = Mid(sRawURL, iLoop, 1)
                    If InStr(1, sValidChars, sTmp, vbBinaryCompare) = 0 Then
                        ' If not ValidChar, convert to HEX and prefix with %
                        sTmp = Hex(Asc(sTmp))
                        If sTmp = "20" Then
                            sTmp = "+"
                        ElseIf Len(sTmp) = 1 Then
                            sTmp = "%0" & sTmp
                        Else
                            sTmp = "%" & sTmp
                        End If
                    End If
                    sRtn = sRtn & sTmp
                Next iLoop
                result = sRtn
            End If
        Catch ex As Exception
            result = ""
        End Try
        Return result
    End Function

End Module