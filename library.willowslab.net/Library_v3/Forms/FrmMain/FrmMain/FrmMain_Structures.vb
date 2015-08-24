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
    Public Structure ResList
        Dim path As String
        Dim url As String
    End Structure
    Private Structure itemDBloc
        Public item As String
        Public loc As String
    End Structure
End Class
