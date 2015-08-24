Imports System.Net
Imports System.Web
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Xml
Imports System.Collections
Partial Class FrmMain
    Private Sub itemdbInitalize()
        Try
            Me.plistEabledChange(False)
            DataReplacer(False)
            If Mabinogi.Export("db\itemdb.xml") And Mabinogi.Export("local\xml\itemdb.korea.txt") Then
                If File.Exists(WLAB.CONF & "itemdb.xml") Then
                    File.Delete(WLAB.CONF & "itemdb.xml")
                End If
                If File.Exists(WLAB.CONF & "itemdb.loc") Then
                    File.Delete(WLAB.CONF & "itemdb.loc")
                End If
                File.Move(WLAB.TEMP & "db\itemdb.xml", WLAB.CONF & "itemdb.xml")
                File.Move(WLAB.TEMP & "local\xml\itemdb.korea.txt", WLAB.CONF & "itemdb.loc")
                DataReplacer(True)
                Me.plistEabledChange(False)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub InfoMessage(ByVal message As String)
        MsgBox(message, MsgBoxStyle.Information Or vbMsgBoxSetForeground, "이런일이 일어날 걸 알고 있었지.")
    End Sub
    Private Sub readSetting()
        Dim myDocument As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Dim myMabinogi As String = myDocument & "\마비노기\"

        Dim Cache As String = myMabinogi & "캐시"
        Dim Setting As String = myMabinogi & "설정"

        Dim chardata As String = ""
        For Each character As String In IO.Directory.GetFiles(Cache)
            Dim path As String = character.Substring(character.LastIndexOf("\") + 1)
            Dim ext As String = ""
            ext = path.Substring(path.LastIndexOf(".") + 1)
            path = path.Substring(0, path.LastIndexOf("."))
            If path.IndexOf("@") > -1 Then
                If path.IndexOf("_sc") = -1 And path.IndexOf("_re") = -1 Then
                    Dim charname As String = path.Substring(0, path.LastIndexOf("@"))
                    Dim charserv As String = path.Substring(path.LastIndexOf("@") + 1)
                    Dim servname As String = "A"
                    Select Case charserv
                        Case "mabikr1"
                            servname = "A"
                        Case "mabikr2"
                            servname = "B"
                        Case "mabikr3"
                            servname = "C"
                        Case "mabikr5"
                            servname = "E"
                    End Select
                    If chardata.IndexOf("[" & servname & "]" & charname) > -1 Then
                        If ext = "ecvc" Then
                            chardata = chardata.Replace("[" & servname & "]" & charname, "[*" & servname & "]" & charname)
                        End If
                    Else
                        If ext = "ecvc" Then
                            chardata &= "[*" & servname & "]" & charname & "|"
                        Else
                            chardata &= "[" & servname & "]" & charname & "|"
                        End If
                    End If
                End If
            End If
        Next
        'addLog(chardata.Replace("|", vbNewLine))
        'addLog(Me.URLEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(chardata))))
    End Sub
End Class
