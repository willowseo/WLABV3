Imports System.Runtime.InteropServices
Imports System.IO
Public Class zlib
    <DllImport("data\zlib.dll", EntryPoint:="compress")> _
    Private Shared Function CompressByteArray(ByVal dest As Byte(), _
                                              ByRef destLen As Integer, ByVal src As Byte(), ByVal srcLen As Integer) As Integer
        ' Leave function empty - DLLImport attribute forwards calls to CompressByteArray to compress in zlib.dLL
    End Function
    <DllImport("data\zlib.dll", EntryPoint:="uncompress")> _
    Private Shared Function UncompressByteArray(ByRef dest As Byte(), _
                                                ByRef destLen As Integer, _
                                                ByVal src As Byte(), _
                                                ByVal srcLen As Integer) As Integer
        ' Leave function empty - DLLImport attribute forwards calls to UnCompressByteArray to Uncompress in zlib.dLL
    End Function

    Public Function decompress(ByVal savePath As String, _
                               ByVal destlen As Long, _
                               ByVal src As Byte()) As Boolean

        Dim dest As Byte() = New Byte(destlen) {}
        UncompressByteArray(dest, destlen, src, src.Length)

        Select Case destlen
            Case -6, -5, -4, -3, -2, -1, 1, 2
                Return False
            Case 0
                If Directory.Exists(Application.StartupPath & "\Temp\" & _
                                    savePath.Substring(0, savePath.LastIndexOf("\"))) Then
                    Directory.CreateDirectory(Application.StartupPath & "\Temp\" & _
                                              savePath.Substring(0, savePath.LastIndexOf("\")))
                End If
                File.WriteAllBytes(Application.StartupPath & "\temp\" & _
                                   savePath, dest)

                Return True
        End Select

        Return False
    End Function
End Class