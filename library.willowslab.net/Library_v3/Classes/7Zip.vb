Imports SevenZip
Imports System.IO

Public Class SevenSharpZip
    Public Shared Function Compress(ByVal source As String, ByVal dest As String)
        Try
            Dim reqDLL As String = WLAB.CONF & "7zx86.dll"
            If Environment.Is64BitProcess Then
                reqDLL = WLAB.CONF & "7zx64.dll"
            End If
            SevenZipBase.SetLibraryPath(reqDLL)
            Dim _7zip As New SevenZipCompressor

            With _7zip
                .CompressionMethod = CompressionMethod.Lzma
                .CompressionMode = CompressionMode.Create
                .CompressionLevel = CompressionLevel.Normal
                .CompressDirectory(source, dest)
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function DeCompress(ByVal source As String, ByVal dest As String)
        Try
            Dim reqDLL As String = WLAB.CONF & "7zx86.dll"
            If Environment.Is64BitProcess Then
                reqDLL = WLAB.CONF & "7zx64.dll"
            End If
            SevenZipBase.SetLibraryPath(reqDLL)
            Dim _7zip As New SevenZipExtractor(source)
            _7zip.BeginExtractArchive(dest)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
