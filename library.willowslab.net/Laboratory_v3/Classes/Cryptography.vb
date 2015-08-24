Imports System.Security.Cryptography
Imports System.Text

Public Class Cryptography
    Public Shared Function getSHA512(ByVal input As String) As String
        Dim SHA As SHA512 = SHA512.Create()
        Dim data As Byte() = SHA.ComputeHash(Encoding.Default.GetBytes(input))
        Dim sBuilder As New StringBuilder()
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i
        Return sBuilder.ToString()
    End Function
    Public Shared Function getMd5Hash(ByVal input As String) As String
        Dim md5Hasher As MD5 = MD5.Create()
        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))
        Dim sBuilder As New StringBuilder()
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i
        Return sBuilder.ToString()
    End Function
    Public Shared Function verifyMd5Hash(ByVal input As String, ByVal hash As String) As Boolean
        Dim hashOfInput As String = getMd5Hash(input)
        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase
        If 0 = comparer.Compare(hashOfInput, hash) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
