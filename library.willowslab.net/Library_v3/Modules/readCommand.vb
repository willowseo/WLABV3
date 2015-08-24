
Public Module readCommand
    Public chunk As Object
    Public data As Object

    Public Function Parse(ByVal text As String, ByVal header As String, ByVal closer As String) As String
        Dim inner As String = text.Substring(text.IndexOf(header) + header.Length, text.LastIndexOf(closer) - (text.IndexOf(header) + header.Length))
        Return inner
    End Function
    Public Function ParseOuter(ByVal text As String, ByVal header As String, ByVal closer As String) As String
        Dim inner As String = text.Substring(text.IndexOf(header) + header.Length, text.IndexOf(closer) - (text.IndexOf(header) + header.Length))
        Return header & inner & closer
    End Function
End Module
