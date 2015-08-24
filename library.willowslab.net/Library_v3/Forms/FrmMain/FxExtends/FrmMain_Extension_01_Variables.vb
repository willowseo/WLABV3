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
    Private progressActivate As Boolean = False

    Private WithEvents _Downloader As WebFileDownloader
    Public dragable As Boolean = True
    Public trd As Thread
    Public working As Boolean = False
    Delegate Sub SetCallback([text] As String)
    Delegate Sub ThreadCallBack([obj] As Integer)
    Delegate Sub AddListItem([ListItem] As ListViewItem, [tabID] As Integer)
    Delegate Sub booleans([bool] As Boolean)

    Public Delegate Sub FileDownloadTrd([URL] As String, [DIR] As String, [FILE] As String)
    Public Delegate Sub ProgressBarControl([Value] As Integer)
    Public Delegate Sub ThreadInvoker([Obj] As Object)

    Dim mabiWindows As New List(Of Process)
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim winState As Integer = 0
    Dim winX As Integer
    Dim winY As Integer

    Dim userLIDC As String = ""
    Dim userPASS As String = ""

    Public myVersion As String = 0

    Private WithEvents fdownloader As WebFileDownloader
    Private tryCount As Integer = 0
    Private totalSize As ULong = 0UL
    Private triedSize As ULong = 0UL

    Private current As Long = 0
    Private total As Long = 0
    Private currentfile As Long = 0
    Private dlfilename As String = ""

    Private mabiDir As String = ""

    Public CMDworkList As New List(Of String)
    Public workQueue As Integer = 0
    Public fileTotal As Integer = 0

    Public updated As Boolean = False

    Dim keyTracer As New List(Of Integer)

    Public rbw As Integer = 0
    Dim opened As Boolean = True
End Class
