Imports System.Text
Imports System.Xml
Imports DevIL

Public Class CoreSRINN
    ''' <summary>
    ''' DDS파일을 PNG파일로 변환해주는 작업에 필요한 값의 형식입니다.
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ConvertDDS2PNG
        Public sourcepath As String
        Public savepath As String
    End Structure
    ''' <summary>
    ''' XPath를 이용해 해당 노드를 제거하는 작업에 필요한 값의 형식입니다.
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure DeleteXPath
        Public sourcepath As String
        Public XPath As String
    End Structure
    ''' <summary>
    ''' 파일을 얻어오는 작업에 필요한 값의 형식입니다.
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure GetFile
        Public path As String
        Public save As String
        Public excute As Boolean
    End Structure
    ''' <summary>
    ''' 이미지를 합칠 때 필요한 값의 형식입니다.
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ImageMerge
        Public sourcepath As String
        Public requirepath As String
        Public destpath As String
        Public x As Integer
        Public y As Integer
    End Structure
    ''' <summary>
    ''' 지역 파일을 수정하는 작업에 필요한 값의 형식입니다.
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure RegionEdit
        Public sourcepath As String
        Public var As Dictionary(Of String, String)
        Public mode As Boolean
    End Structure
    ''' <summary>
    ''' 일반 치환하는 작업에 필요한 값의 형식입니다.
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ReplaceText
        Public sourcepath As String
        Public Search As String
        Public replace As String
        Public Excute As Boolean
    End Structure
    ''' <summary>
    ''' XPath 를 이용해 문서를 치환하는 작업에 필요한 값의 형식입니다.
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ReplaceXPath
        Public sourcepath As String
        Public XPath As String
        Public Attribute As String
        Public Value As String
        Public mode As String
        Public excute As Boolean
    End Structure
    ''' <summary>
    ''' 마비노기의 로켈 텍스트를 편집하는 작업에 필요한 값의 형식입니다.
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ReplaceLocale
        Public sourcepath As String
        Public Findnum As String
        Public value As String
        Public encoding As Encoding
        Public mode As String
    End Structure
    ''' <summary>
    ''' 쌍 따옴표 입니다.
    ''' </summary>
    ''' <remarks></remarks>
    Private dquot As Char = Chr(34)
    ''' <summary>
    ''' 요리재료를 읽어와 버들 실험실에서 읽을 수 있는 실험 문서 형태로 전환합니다.
    ''' </summary>
    ''' <param name="path">XML문서의 경로입니다.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertDocument(ByVal path As String) As String
        Return ""
    End Function
    ''' <summary>
    ''' 요리재료를 읽습니다.
    ''' </summary>
    ''' <param name="str">XML문서를 일반 스트링 형태로 읽어온 값입니다.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function cookery(ByVal str As String) As String
        Return ""
    End Function
End Class
