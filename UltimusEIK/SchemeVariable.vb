Public Class SchemeVariable
    Private _path As String
    Private _tagName As String

    Public Sub New(ByVal path As String, ByVal tagName As String)
        _path = path
        _tagName = tagName
    End Sub

    Public Function GetSchemeVariablePath() As String
        Return _path
    End Function

    Public Function GetSchemeVariableTagName() As String
        Return _tagName
    End Function
End Class
